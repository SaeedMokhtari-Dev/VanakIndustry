using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Auth.ResetPassword
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage(ApiMessages.Auth.EmailRequired);
        }
    }
}
