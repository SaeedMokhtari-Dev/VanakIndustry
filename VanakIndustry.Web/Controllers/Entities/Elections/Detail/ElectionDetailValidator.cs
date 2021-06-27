using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Detail
{
    public class ElectionDetailValidator : AbstractValidator<ElectionDetailRequest>
    {
        public ElectionDetailValidator()
        {
            RuleFor(x => x.ElectionId).NotEmpty().WithMessage(ApiMessages.IdRequired);
        }
    }
}
