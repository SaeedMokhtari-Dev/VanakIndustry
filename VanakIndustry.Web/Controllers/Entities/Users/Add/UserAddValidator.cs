using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Users.Add
{
    public class UserAddValidator : AbstractValidator<UserAddRequest>
    {
        public UserAddValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage(ApiMessages.Auth.UsernameRequired);
            RuleFor(x => x.Password).NotEmpty().WithMessage(ApiMessages.Auth.PasswordRequired);
        }
    }
}
