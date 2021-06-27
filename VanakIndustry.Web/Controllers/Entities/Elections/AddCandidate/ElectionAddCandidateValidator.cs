using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Elections.AddCandidate
{
    public class ElectionAddCandidateValidator : AbstractValidator<ElectionAddCandidateRequest>
    {
        public ElectionAddCandidateValidator()
        {
            RuleFor(x => x.ElectionId).NotEmpty().WithMessage(ApiMessages.IdRequired);
            RuleFor(x => x.ElectionCandidateTypeId).NotEmpty().WithMessage(ApiMessages.ElectionMessage.ElectionCandidateTypeIdRequired);
        }
    }
}
