using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Users.Present
{
    public class UserPresentValidator : AbstractValidator<UserPresentRequest>
    {
        public UserPresentValidator()
        {
            RuleFor(x => x.Barcode).NotEmpty().WithMessage(ApiMessages.UserMessage.BarcodeRequired);
        }
    }
}
