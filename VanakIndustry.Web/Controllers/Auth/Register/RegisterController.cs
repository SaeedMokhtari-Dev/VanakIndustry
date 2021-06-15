using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Auth.Register
{
    [Route(Endpoints.ApiAuthRegister)]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class RegisterController : ApiController<RegisterRequest>
    {
        public RegisterController(IApiRequestHandler<RegisterRequest> handler, IValidator<RegisterRequest> validator) : base(handler, validator)
        {
        }
    }
}
