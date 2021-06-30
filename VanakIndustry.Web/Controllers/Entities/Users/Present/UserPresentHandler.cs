using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;

namespace VanakIndustry.Web.Controllers.Entities.Users.Present
{
    public class UserPresentHandler : ApiRequestHandler<UserPresentRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;

        public UserPresentHandler(
            VanakIndustryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<ActionResult> Execute(UserPresentRequest request)
        {
            Election election = await _context.Elections
                .FindAsync(request.ElectionId);
            
            if(election == null)
                return ActionResult.Error(ApiMessages.ResourceNotFound);
            
            User user = await _context.Users
                .FirstOrDefaultAsync(w => w.Barcode == request.Barcode);

            if (user == null)
            {
                return ActionResult.Error(ApiMessages.UserMessage.UserNotFound);
            }

            if(await _context.ElectionPresentUsers.AnyAsync(w => w.UserId == user.Id && w.ElectionId == election.Id))
                return ActionResult.Error(ApiMessages.UserMessage.UserWasPresentedBefore);
            
            await _context.ExecuteTransactionAsync(async () =>
            {
                user.Present = true;

                await _context.ElectionPresentUsers.AddAsync(new ElectionPresentUser()
                {
                    Election = election,
                    User = user,
                    CreatedAt = DateTime.Now
                });
                await _context.SaveChangesAsync();
            });

            StringBuilder returnMessage = new StringBuilder().Append("کاربر ").Append(user.FullName)
                .Append(" در انتخابات ").Append(election.Title).Append(" حاضر شدند.");
            return ActionResult.Ok(returnMessage.ToString());
        }
    }
}
