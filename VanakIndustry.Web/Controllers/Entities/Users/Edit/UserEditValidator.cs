using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Users.Edit
{
    public class UserEditValidator : AbstractValidator<UserEditRequest>
    {
        public UserEditValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(ApiMessages.IdRequired);
            RuleFor(x => x.Password).Matches(PasswordConstants.PasswordRegex).WithMessage(ApiMessages.MinPasswordLengthError);
        }
    }
}
