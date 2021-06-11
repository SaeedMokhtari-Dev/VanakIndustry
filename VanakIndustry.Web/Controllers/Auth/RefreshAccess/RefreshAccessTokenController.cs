using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Auth.RefreshAccess
{
    [Route(Endpoints.ApiAuthRefreshAccessToken)]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class RefreshAccessTokenController : ApiController<RefreshAccessTokenRequest>
    {
        public RefreshAccessTokenController(IApiRequestHandler<RefreshAccessTokenRequest> handler, IValidator<RefreshAccessTokenRequest> validator) : base(handler, validator)
        {
        }
    }
}
