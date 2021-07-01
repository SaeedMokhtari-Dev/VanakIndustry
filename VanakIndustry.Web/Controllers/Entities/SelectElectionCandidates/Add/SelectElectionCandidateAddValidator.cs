using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.SelectElectionCandidates.Add
{
    public class SelectElectionCandidateAddValidator : AbstractValidator<SelectElectionCandidateAddRequest>
    {
        public SelectElectionCandidateAddValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(ApiMessages.IdRequired);
        }
    }
}
