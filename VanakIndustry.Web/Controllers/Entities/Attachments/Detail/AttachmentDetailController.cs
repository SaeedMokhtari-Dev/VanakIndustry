using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Constants;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;
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
        /*private readonly VanakIndustryContext _context;
        public AttachmentDetailController(VanakIndustryContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [ResponseCache(Duration = 300)]
        public async Task<ActionResult> Get([FromQuery] long attachmentId)
        {
            Attachment attachment = await _context.Attachments
                .FindAsync(attachmentId);

            if (attachment == null)
            {
                return Problem(ApiMessages.ResourceNotFound);
            }

            //AttachmentDetailResponse response = _mapper.Map<AttachmentDetailResponse>(election);
            
            return Ok(String.Join("", attachment.Image.Select(Convert.ToChar)));
        }*/
    }
}
