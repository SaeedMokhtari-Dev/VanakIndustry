using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Auth.ValidateResetPasswordToken
{
    public class ValidateResetPasswordTokenValidator : AbstractValidator<ValidateResetPasswordTokenRequest>
    {
        public ValidateResetPasswordTokenValidator()
        {
            RuleFor(x => x.Token).NotEmpty().WithMessage(ApiMessages.Auth.TokenRequired);
        }
    }
}
