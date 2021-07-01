using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Attachments.Detail
{
    [Route(Endpoints.ApiAttachmentDetail)]
    [ApiExplorerSettings(GroupName = "Attachment")]
    [Authorize]
    public class AttachmentDetailController : ApiController<AttachmentDetailRequest>
    {
        public AttachmentDetailController(IApiRequestHandler<AttachmentDetailRequest> handler, IValidator<AttachmentDetailRequest> validator) : base(handler, validator)
        {
        }
    }
}
