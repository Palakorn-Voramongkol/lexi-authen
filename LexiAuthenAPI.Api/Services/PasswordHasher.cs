// LexiAuthenAPI.Api/Services/PasswordHasher.cs
using Microsoft.AspNetCore.Identity;

namespace LexiAuthenAPI.Api.Services
{
    public class PasswordHasherService : IPasswordHasher
    {
        private readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();

        public string HashPassword(string password)
        {
            // The parameter can be null as we're not using the user object here
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
            return result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
