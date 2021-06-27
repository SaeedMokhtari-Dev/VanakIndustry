using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Delete
{
    public class ElectionDeleteValidator : AbstractValidator<ElectionDeleteRequest>
    {
        public ElectionDeleteValidator()
        {
            RuleFor(x => x.ElectionId).NotEmpty().WithMessage(ApiMessages.IdRequired);
        }
    }
}
