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
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is requied.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

        }
    }
}