// LexiAuthenAPI.Api/Validators/UpdateUserDtoValidator.cs
using FluentValidation;
using LexiAuthenAPI.Api.DTOs.User;

namespace LexiAuthenAPI.Api.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.Username)
                .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Username));

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format.")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Password)
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
                .When(x => !string.IsNullOrEmpty(x.Password));
        }
    }
}
