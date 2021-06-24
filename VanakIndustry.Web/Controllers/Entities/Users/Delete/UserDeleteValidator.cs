using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Users.Delete
{
    public class UserDeleteValidator : AbstractValidator<UserDeleteRequest>
    {
        public UserDeleteValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(ApiMessages.IdRequired);
        }
    }
}
