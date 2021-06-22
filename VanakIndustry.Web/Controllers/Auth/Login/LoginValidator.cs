using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Auth.Login
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage(ApiMessages.Auth.UsernameRequired);
            RuleFor(x => x.Password).NotEmpty().WithMessage(ApiMessages.Auth.PasswordRequired);
        }
    }
}
