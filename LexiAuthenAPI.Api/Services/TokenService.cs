// src/LexiAuthenAPI.Api/Services/TokenService.cs
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using LexiAuthenAPI.Api.Configuration;
using LexiAuthenAPI.Api.Models.Auth;
using LexiAuthenAPI.Domain.Entities;
using LexiAuthenAPI.Domain.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Api.Services
{
    /// <summary>
    /// Service responsible for generating and managing JWT access tokens and refresh tokens.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="jwtSettings">The settings for JWT configuration.</param>
        /// <param name="userRepository">The repository for accessing user data.</param>
        /// <param name="refreshTokenRepository">The repository for managing refresh tokens.</param>
        public TokenService(IOptions<JwtSettings> jwtSettings, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        /// <summary>
        /// Generates a JWT access token and a refresh token for the given user.
        /// </summary>
        /// <param name="user">The <see cref="User"/> object containing user details.</param>
        /// <returns>A <see cref="TokenResponseDto"/> containing the generated access token and refresh token.</returns>
        public async Task<TokenResponseDto> GenerateTokenAsync(User user)
        {
            // Generate a new access token
            var accessToken = GenerateAccessToken(user);

            // Generate a new refresh token
            var refreshToken = GenerateRefreshToken();

            // Save the refresh token to the database
            var refreshTokenEntity = new Refreshtoken
            {
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays), // Set expiration
                Created = DateTime.UtcNow, // Set creation time
                UserId = user.Id // Associate with the user
            };

            // Add and save the refresh token
            await _refreshTokenRepository.AddAsync(refreshTokenEntity);
            await _refreshTokenRepository.SaveChangesAsync();

            // Return the tokens as a response DTO
            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        /// <summary>
        /// Refreshes an existing access token using the provided refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token to use for obtaining a new access token.</param>
        /// <returns>A new <see cref="TokenResponseDto"/> if the refresh token is valid; otherwise, null.</returns>
        public async Task<TokenResponseDto> RefreshTokenAsync(string refreshToken)
        {
            // Retrieve the refresh token from the database
            var existingRefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

            // Check if the token is valid and active
            if (existingRefreshToken == null || !existingRefreshToken.IsActive)
            {
                return null;
            }

            // Retrieve the associated user
            var user = await _userRepository.GetByIdAsync(existingRefreshToken.UserId);
            if (user == null)
            {
                return null;
            }

            // Revoke the old refresh token
            existingRefreshToken.Revoked = DateTime.UtcNow;
            _refreshTokenRepository.Update(existingRefreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            // Generate new tokens for the user
            return await GenerateTokenAsync(user);
        }

        /// <summary>
        /// Generates a JWT access token for a given user, including user information and assigned roles.
        /// </summary>
        /// <param name="user">The <see cref="User"/> object containing user details and associated roles.</param>
        /// <returns>
        /// A string representation of the JWT token with the following payload structure:
        /// {
        ///   "sub": "1234567890",                // The user ID (unique identifier for the user)
        ///   "name": "John Doe",                 // The user's name (username)
        ///   "roles": "[{\"id\":1,\"name\":\"Admin\"},{\"id\":2,\"name\":\"User\"}]", // Serialized JSON array of roles with IDs and names
        ///   "exp": 1672540799,                  // The expiration time of the token in Unix time format
        ///   "iss": "your-issuer",               // The issuer of the token
        ///   "aud": "your-audience"              // The intended audience for the token
        /// }
        /// </returns>
        /// <remarks>
        /// This function uses the <see cref="JwtSecurityTokenHandler"/> to create and encode the token. The "roles" claim is serialized as a JSON array string
        /// representing all roles assigned to the user, with each role containing an "id" and "name" property.
        /// </remarks>
        private string GenerateAccessToken(User user)
        {
            // Create a new JWT token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret); // Convert secret to byte array for key

            // Create claims for the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // User ID claim
                new Claim(ClaimTypes.Name, user.Username), // Username claim
            };

            // Add a custom claim for roles as an array with both ID and name
            if (user.UserRoles != null && user.UserRoles.Any())
            {
                var roles = user.UserRoles.Select(ur => new
                {
                    id = ur.Role.Id,
                    name = ur.Role.Name
                }).ToList();

                // Add the roles claim as a serialized JSON string
                claims.Add(new Claim("roles", System.Text.Json.JsonSerializer.Serialize(roles)));
            }

            // Define the token descriptor with claims, expiration, and signing credentials
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes), // Set token expiration
                Issuer = _jwtSettings.Issuer, // Set token issuer
                Audience = _jwtSettings.Audience, // Set token audience
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) // Use HMAC with SHA256 for signing
            };

            // Create and write the token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Generates a secure refresh token.
        /// </summary>
        /// <returns>A base64-encoded string representing the refresh token.</returns>
        private string GenerateRefreshToken()
        {
            // Create a random byte array for the refresh token
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber); // Fill array with random bytes

            // Convert the byte array to a base64 string and return
            return Convert.ToBase64String(randomNumber);
        }
    }
}
