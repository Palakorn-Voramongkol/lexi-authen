// LexiAuthenAPI.Api/Services/IPasswordHasher.cs
namespace LexiAuthenAPI.Api.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string password);
    }
}
