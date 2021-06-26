using System.Threading.Tasks;
using AutoMapper;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;

namespace VanakIndustry.Web.Controllers.Entities.Users.Active
{
    public class UserActiveHandler : ApiRequestHandler<UserActiveRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;

        public UserActiveHandler(
            VanakIndustryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<ActionResult> Execute(UserActiveRequest request)
        {
            User user = await _context.Users
                .FindAsync(request.UserId);

            if (user == null)
            {
                return ActionResult.Error(ApiMessages.ResourceNotFound);
            }

            user.IsActive = true;
            await _context.SaveChangesAsync();
            
            return ActionResult.Ok(ApiMessages.UserMessage.ActivatedSuccessfully);
        }
    }
}
