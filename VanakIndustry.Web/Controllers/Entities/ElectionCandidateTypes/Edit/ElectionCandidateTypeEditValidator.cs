using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.Edit
{
    public class ElectionCandidateTypeEditValidator : AbstractValidator<ElectionCandidateTypeEditRequest>
    {
        public ElectionCandidateTypeEditValidator()
        {
            RuleFor(x => x.ElectionCandidateTypeId).NotEmpty().WithMessage(ApiMessages.IdRequired);
        }
    }
}
