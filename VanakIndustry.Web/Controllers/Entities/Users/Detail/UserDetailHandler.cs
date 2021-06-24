using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;

namespace VanakIndustry.Web.Controllers.Entities.Users.Detail
{
    public class UserDetailHandler : ApiRequestHandler<UserDetailRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;

        public UserDetailHandler(
            VanakIndustryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<ActionResult> Execute(UserDetailRequest request)
        {
            User user = await _context.Users
                .FindAsync(request.UserId);

            if (user == null)
            {
                return ActionResult.Error(ApiMessages.ResourceNotFound);
            }

            UserDetailResponse response = _mapper.Map<UserDetailResponse>(user);
            
            return ActionResult.Ok(response);
        }
    }
}
