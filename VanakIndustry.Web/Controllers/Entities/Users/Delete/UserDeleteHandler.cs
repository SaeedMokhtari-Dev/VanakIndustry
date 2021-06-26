using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;

namespace VanakIndustry.Web.Controllers.Entities.Users.Delete
{
    public class UserDeleteHandler : ApiRequestHandler<UserDeleteRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;

        public UserDeleteHandler(
            VanakIndustryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<ActionResult> Execute(UserDeleteRequest request)
        {
            User user = await _context.Users
                .FindAsync(request.UserId);

            if (user == null)
            {
                return ActionResult.Error(ApiMessages.ResourceNotFound);
            }

            await _context.ExecuteTransactionAsync(async () =>
            {
                var userAttachments = await _context.Attachments.Where(w => getUserAttachmentIds(user).Contains(w.Id)).ToListAsync(); 
                _context.Attachments.RemoveRange(userAttachments);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            });
            
            return ActionResult.Ok(ApiMessages.UserMessage.DeletedSuccessfully);
        }

        private List<long> getUserAttachmentIds(User user)
        {
            List<long> Ids = new List<long>();
            Ids.Add(user.NationalCardId);
            Ids.Add(user.FirstPageCertificateId);
            if(user.SecondPageCertificateId.HasValue)
                Ids.Add(user.SecondPageCertificateId.Value);
            if(user.CardId.HasValue)
                Ids.Add(user.CardId.Value);
            if(user.CandidatePictureId.HasValue)
                Ids.Add(user.CandidatePictureId.Value);
            if(user.PictureId.HasValue)
                Ids.Add(user.PictureId.Value);
            return Ids;
        }
    }
}
