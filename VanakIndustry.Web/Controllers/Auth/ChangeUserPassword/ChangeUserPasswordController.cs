using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Auth.ChangeUserPassword
{
    [Route(Endpoints.ApiAuthChangeUserPassword)]
    [ApiExplorerSettings(GroupName = "Auth")]
    [Authorize]
    public class ChangeUserPasswordController : ApiController<ChangeUserPasswordRequest>
    {
        public ChangeUserPasswordController(IApiRequestHandler<ChangeUserPasswordRequest> handler, IValidator<ChangeUserPasswordRequest> validator) : base(handler, validator)
        {
        }
    }
}
