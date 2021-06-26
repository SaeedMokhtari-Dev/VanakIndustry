using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.Get
{
    public class ElectionCandidateTypeGetValidator : AbstractValidator<ElectionCandidateTypeGetRequest>
    {
        public ElectionCandidateTypeGetValidator()
        {
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(0).WithMessage(ApiMessages.PageSize);
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(0).WithMessage(ApiMessages.PageIndex);
        }
    }
}
