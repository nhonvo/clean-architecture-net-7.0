using FluentValidation;
using Models.User;

namespace Api.Presentation.Validation
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty().WithMessage("UserName must not empty")
                .MinimumLength(8).WithMessage("UserName must be at least 8 characters long.");
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password must not empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Za-z]").WithMessage("Password must contain at least one letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches("[^A-Za-z0-9]").WithMessage("Password must contain at least one special character.")
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z0-9]).{9,}$")
                .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.");
        }
    }
}