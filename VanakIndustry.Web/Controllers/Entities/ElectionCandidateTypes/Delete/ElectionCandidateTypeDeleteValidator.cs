using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.Delete
{
    public class ElectionCandidateTypeDeleteValidator : AbstractValidator<ElectionCandidateTypeDeleteRequest>
    {
        public ElectionCandidateTypeDeleteValidator()
        {
            RuleFor(x => x.ElectionCandidateTypeId).NotEmpty().WithMessage(ApiMessages.IdRequired);
        }
    }
}
