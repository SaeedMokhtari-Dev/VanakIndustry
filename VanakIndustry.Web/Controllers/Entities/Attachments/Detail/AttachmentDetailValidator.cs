using FluentValidation;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Attachments.Detail
{
    public class AttachmentDetailValidator : AbstractValidator<AttachmentDetailRequest>
    {
        public AttachmentDetailValidator()
        {
            RuleFor(x => x.AttachmentId).NotEmpty().WithMessage(ApiMessages.IdRequired);
        }
    }
}
