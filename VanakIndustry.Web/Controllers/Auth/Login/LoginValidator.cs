using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Auth.Login
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage(ApiMessages.Auth.EmailRequired);
            RuleFor(x => x.Password).NotEmpty().WithMessage(ApiMessages.Auth.PasswordRequired);
        }
    }
}
