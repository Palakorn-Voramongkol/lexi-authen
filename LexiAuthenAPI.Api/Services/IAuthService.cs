// LexiAuthenAPI.Api/Services/IAuthService.cs
using LexiAuthenAPI.Api.DTOs.Auth;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Api.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    }
}
