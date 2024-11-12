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
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public TokenService(IOptions<JwtSettings> jwtSettings, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<TokenResponseDto> GenerateTokenAsync(User user)
        {
            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            // Save refresh token to database
            var refreshTokenEntity = new Refreshtoken
            {
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
                Created = DateTime.UtcNow,
                UserId = user.Id
            };

            await _refreshTokenRepository.AddAsync(refreshTokenEntity);
            await _refreshTokenRepository.SaveChangesAsync();

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var existingRefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

            if (existingRefreshToken == null || !existingRefreshToken.IsActive)
            {
                return null;
            }

            // Get the user
            var user = await _userRepository.GetByIdAsync(existingRefreshToken.UserId);
            if (user == null)
            {
                return null;
            }

            // Revoke the old refresh token
            existingRefreshToken.Revoked = DateTime.UtcNow;
            _refreshTokenRepository.Update(existingRefreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            // Generate new tokens
            return await GenerateTokenAsync(user);
        }

        private string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                
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

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
