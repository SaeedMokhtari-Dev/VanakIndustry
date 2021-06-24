using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.Core.Enums;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.Web.Identity.Contexts;

namespace VanakIndustry.Web.Controllers.Entities.Users.List
{
    public class UserListHandler : ApiRequestHandler<UserListRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;
        private readonly UserContext _userContext;

        public UserListHandler(
            VanakIndustryContext context, IMapper mapper, UserContext userContext)
        {
            _context = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        protected override async Task<ActionResult> Execute(UserListRequest request)
        {
            var query = _context.Users
                .OrderByDescending(w => w.Id)
                .AsQueryable();

            var response = await query.Select(w =>
            new UserListResponseItem() {
                Key = w.Id, 
                FullName = $"{w.FirstName} {w.LastName}"
            }).ToListAsync();
            
            return ActionResult.Ok(response);
        }
    }
}
