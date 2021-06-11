using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Auth.Login
{
    [Route(Endpoints.ApiAuthLogin)]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class LoginController : ApiController<LoginRequest>
    {
        public LoginController(IApiRequestHandler<LoginRequest> handler, IValidator<LoginRequest> validator) : base(handler, validator)
        {
        }
    }
}
