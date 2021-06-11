using System.Threading.Tasks;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Web.Identity.Contexts;
using VanakIndustry.Web.Identity.Services;

namespace VanakIndustry.Web.Controllers.Auth.Logout
{
    public class LogoutHandler : ApiRequestHandler<LogoutRequest>
    {
        private readonly UserContext _userContext;
        private readonly RefreshTokenService _refreshTokenService;

        public LogoutHandler(UserContext userContext, RefreshTokenService refreshTokenService)
        {
            _userContext = userContext;
            _refreshTokenService = refreshTokenService;
        }

        protected override async Task<ActionResult> Execute(LogoutRequest request)
        {
            if (_userContext.IsAuthenticated)
            {
                await _refreshTokenService.DeactivateToken(request.Token, _userContext.Id);
            }

            return ActionResult.Ok();
        }
    }
}
