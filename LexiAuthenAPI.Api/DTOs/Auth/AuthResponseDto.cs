// LexiAuthenAPI.Api/DTOs/Auth/AuthResponseDto.cs
namespace LexiAuthenAPI.Api.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty; // Optional: If implementing refresh tokens
    }
}
