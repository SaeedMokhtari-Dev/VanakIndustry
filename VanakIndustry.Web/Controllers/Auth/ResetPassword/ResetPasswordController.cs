using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Auth.ResetPassword
{
    [Route(Endpoints.ApiAuthResetPassword)]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class ResetPasswordController : ApiController<ResetPasswordRequest>
    {
        public ResetPasswordController(IApiRequestHandler<ResetPasswordRequest> handler, IValidator<ResetPasswordRequest> validator) : base(handler, validator)
        {
        }
    }
}
