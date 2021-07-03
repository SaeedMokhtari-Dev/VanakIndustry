using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;

namespace VanakIndustry.Web.Controllers.Entities.Attachments.Detail
{
    public class AttachmentDetailHandler : ApiRequestHandler<AttachmentDetailRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;

        public AttachmentDetailHandler(
            VanakIndustryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<ActionResult> Execute(AttachmentDetailRequest request)
        {
            Attachment attachment = await _context.Attachments
                .FindAsync(request.AttachmentId);

            if (attachment == null)
            {
                return ActionResult.Error(ApiMessages.ResourceNotFound);
            }

            //AttachmentDetailResponse response = _mapper.Map<AttachmentDetailResponse>(election);
            AttachmentDetailResponse response = new AttachmentDetailResponse();
            response.Image = String.Join("", attachment.Image.Select(Convert.ToChar));
            return ActionResult.Ok(response);
        }
    }
}
