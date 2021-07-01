using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.SelectElectionCandidates.Get
{
    public class SelectElectionCandidateGetValidator : AbstractValidator<SelectElectionCandidateGetRequest>
    {
        public SelectElectionCandidateGetValidator()
        {
            RuleFor(x => x.UserId).GreaterThanOrEqualTo(0).WithMessage(ApiMessages.IdRequired);
        }
    }
}
