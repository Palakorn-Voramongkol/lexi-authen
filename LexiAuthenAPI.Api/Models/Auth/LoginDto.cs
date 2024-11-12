// src/LexiAuthenAPI.Api/Models/Auth/LoginDto.cs
namespace LexiAuthenAPI.Api.Models.Auth
{
    public class LoginDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

// src/LexiAuthenAPI.Api/Models/Auth/RegisterDto.cs
namespace LexiAuthenAPI.Api.Models.Auth
{
    public class RegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

// src/LexiAuthenAPI.Api/Models/Auth/TokenResponseDto.cs
namespace LexiAuthenAPI.Api.Models.Auth
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
