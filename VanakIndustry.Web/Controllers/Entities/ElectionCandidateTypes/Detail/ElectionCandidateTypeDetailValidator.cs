using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.Detail
{
    public class ElectionCandidateTypeDetailValidator : AbstractValidator<ElectionCandidateTypeDetailRequest>
    {
        public ElectionCandidateTypeDetailValidator()
        {
            RuleFor(x => x.ElectionCandidateTypeId).NotEmpty().WithMessage(ApiMessages.IdRequired);
        }
    }
}
