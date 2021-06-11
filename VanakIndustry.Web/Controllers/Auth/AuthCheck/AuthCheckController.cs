using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.Core.Interfaces;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Auth.AuthCheck
{
    [Authorize(Policy = nameof(Policies.ActiveUser))]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class AuthCheckController: ControllerBase, IApiController
    {

        [HttpGet]
        [Route(Endpoints.ApiAuthCheck)]
        public IActionResult AuthCheck()
        {
            return ApiResponse.Ok();
        }
    }
}
