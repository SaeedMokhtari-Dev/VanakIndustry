using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Users.Detail
{
    public class UserDetailValidator : AbstractValidator<UserDetailRequest>
    {
        public UserDetailValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(ApiMessages.IdRequired);
        }
    }
}
