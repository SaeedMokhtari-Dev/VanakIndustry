using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Users.Active
{
    public class UserActiveValidator : AbstractValidator<UserActiveRequest>
    {
        public UserActiveValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(ApiMessages.IdRequired);
        }
    }
}
