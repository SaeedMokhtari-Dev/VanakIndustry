using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Auth.ValidateResetPasswordToken
{
    [Route(Endpoints.ApiAuthValidateResetPasswordToken)]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class ValidateResetPasswordTokenController : ApiController<ValidateResetPasswordTokenRequest>
    {
        public ValidateResetPasswordTokenController(IApiRequestHandler<ValidateResetPasswordTokenRequest> handler, IValidator<ValidateResetPasswordTokenRequest> validator) : base(handler, validator)
        {
        }
    }
}
