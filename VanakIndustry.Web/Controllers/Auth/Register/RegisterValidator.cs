using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Auth.Register
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage(ApiMessages.Auth.UsernameRequired);
            RuleFor(x => x.Password).NotEmpty().WithMessage(ApiMessages.Auth.PasswordRequired);
        }
    }
}
