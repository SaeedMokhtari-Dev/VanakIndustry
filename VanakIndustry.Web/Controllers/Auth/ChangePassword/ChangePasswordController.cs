using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Auth.ChangePassword
{
    [Route(Endpoints.ApiAuthChangePassword)]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class ChangePasswordController : ApiController<ChangePasswordRequest>
    {
        public ChangePasswordController(IApiRequestHandler<ChangePasswordRequest> handler, IValidator<ChangePasswordRequest> validator) : base(handler, validator)
        {
        }
    }
}
