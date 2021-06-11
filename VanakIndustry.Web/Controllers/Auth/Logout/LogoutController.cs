using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Auth.Logout
{
    [Route(Endpoints.ApiAuthLogout)]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class LogoutController : ApiController<LogoutRequest>
    {
        public LogoutController(IApiRequestHandler<LogoutRequest> handler, IValidator<LogoutRequest> validator) : base(handler, validator)
        {
        }
    }
}
