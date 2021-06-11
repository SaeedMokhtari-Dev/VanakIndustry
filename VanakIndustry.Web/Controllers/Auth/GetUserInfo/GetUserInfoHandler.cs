using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.Web.Identity.Contexts;

namespace VanakIndustry.Web.Controllers.Auth.GetUserInfo
{
    public class GetUserInfoHandler : ApiRequestHandler<GetUserInfoRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly UserContext _userContext;

        public GetUserInfoHandler(VanakIndustryContext context, UserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        protected override async Task<ActionResult> Execute(GetUserInfoRequest request)
        {
            var user = await _context.Users.Include(w => w.Roles).FirstOrDefaultAsync(x => x.Id == _userContext.Id);

            if (user == null)
            {
                return ActionResult.Error(ApiMessages.ResourceNotFound);
            }

            var response = new GetUserInfoResponse
            {
                Id = user.Id,
                Email = user.Email,
                Name = $"{user.FirstName} {user.LastName}",
                Roles = user.Roles.Select(x => x.Role).ToList()
            };

            return ActionResult.Ok(response);
        }
    }
}
