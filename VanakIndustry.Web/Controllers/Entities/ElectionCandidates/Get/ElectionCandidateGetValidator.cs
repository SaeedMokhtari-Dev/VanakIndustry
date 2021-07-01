using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidates.Get
{
    public class ElectionCandidateGetValidator : AbstractValidator<ElectionCandidateGetRequest>
    {
        public ElectionCandidateGetValidator()
        {
            RuleFor(x => x.ElectionId).GreaterThanOrEqualTo(0).WithMessage(ApiMessages.IdRequired);
        }
    }
}
