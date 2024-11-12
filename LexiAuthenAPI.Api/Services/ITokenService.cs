// src/LexiAuthenAPI.Api/Services/ITokenService.cs
using LexiAuthenAPI.Api.Models.Auth;
using LexiAuthenAPI.Domain.Entities;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Api.Services
{
    public interface ITokenService
    {
        Task<TokenResponseDto> GenerateTokenAsync(User user);
        Task<TokenResponseDto> RefreshTokenAsync(string refreshToken);

    }
}
