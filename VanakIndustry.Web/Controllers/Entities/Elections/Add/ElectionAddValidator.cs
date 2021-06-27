using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Add
{
    public class ElectionAddValidator : AbstractValidator<ElectionAddRequest>
    {
        public ElectionAddValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage(ApiMessages.ElectionMessage.TitleRequired);
        }
    }
}
