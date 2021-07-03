using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Present
{
    public class ElectionPresentValidator : AbstractValidator<ElectionPresentRequest>
    {
        public ElectionPresentValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(ApiMessages.IdRequired);
        }
    }
}
