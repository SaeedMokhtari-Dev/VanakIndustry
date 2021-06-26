using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.Add
{
    public class ElectionCandidateTypeAddValidator : AbstractValidator<ElectionCandidateTypeAddRequest>
    {
        public ElectionCandidateTypeAddValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage(ApiMessages.ElectionCandidateTypeMessage.TitleRequired);
        }
    }
}
