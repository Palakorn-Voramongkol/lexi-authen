// LexiAuthenAPI.Api/Services/AuthService.cs
using LexiAuthenAPI.Domain.Interfaces;
using LexiAuthenAPI.Api.DTOs.Auth;
using LexiAuthenAPI.Domain.Entities;

namespace LexiAuthenAPI.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.FindByUsernameAsync(registerDto.Username);
            if (existingUser != null)
            {
                throw new Exception("Username already exists.");
            }

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = _passwordHasher.HashPassword(registerDto.Password)
            };

            await _userRepository.AddAsync(user);
            // Note: SaveChangesAsync is called within AddAsync in UserRepository

            // Call the async token generation method
            var tokenResponse = await _tokenService.GenerateTokenAsync(user);

            return new AuthResponseDto
            {
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken // Ensure this is part of the TokenResponseDto
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.FindByUsernameAsync(loginDto.Username);
            if (user == null || !_passwordHasher.VerifyPassword(user.PasswordHash, loginDto.Password))
            {
                throw new Exception("Invalid username or password.");
            }

            // Call the async token generation method
            var tokenResponse = await _tokenService.GenerateTokenAsync(user);
            return new AuthResponseDto
            {
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken // Ensure this is part of the TokenResponseDto
            };
        }
    }
}
