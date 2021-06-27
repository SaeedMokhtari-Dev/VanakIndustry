using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Edit
{
    public class ElectionEditValidator : AbstractValidator<ElectionEditRequest>
    {
        public ElectionEditValidator()
        {
            RuleFor(x => x.ElectionId).NotEmpty().WithMessage(ApiMessages.IdRequired);
        }
    }
}
