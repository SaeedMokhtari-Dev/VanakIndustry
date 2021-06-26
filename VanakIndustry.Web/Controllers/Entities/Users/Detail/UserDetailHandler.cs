using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.Core.Enums;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;
using VanakIndustry.Web.Identity.Contexts;

namespace VanakIndustry.Web.Controllers.Entities.Users.Detail
{
    public class UserDetailHandler : ApiRequestHandler<UserDetailRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;
        private readonly UserContext _userContext;

        public UserDetailHandler(
            VanakIndustryContext context, IMapper mapper, UserContext userContext)
        {
            _context = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        protected override async Task<ActionResult> Execute(UserDetailRequest request)
        {
            if (_userContext.Roles.Any(w => w.Role == RoleType.User) && request.UserId != _userContext.Id)
            {
                return ActionResult.Error(ApiMessages.Forbidden);
            }
            User user = await _context.Users.Include(w => w.Picture)
                .Include(w => w.Card)
                .Include(w => w.Roles)
                .Include(w => w.CandidatePicture)
                .Include(w => w.NationalCard)
                .Include(w => w.FirstPageCertificate)
                .Include(w => w.SecondPageCertificate)
                .FirstOrDefaultAsync(w => w.Id == request.UserId);

            if (user == null)
            {
                return ActionResult.Error(ApiMessages.ResourceNotFound);
            }

            UserDetailResponse response = _mapper.Map<UserDetailResponse>(user);
            
            return ActionResult.Ok(response);
        }
    }
}
