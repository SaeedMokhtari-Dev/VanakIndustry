using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Constants;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Auth.GetUserInfo
{
    [Authorize(Policy = nameof(Policies.ActiveUser))]
    [Route(Endpoints.ApiUserInfo)]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class GetUserInfoController : ApiController<GetUserInfoRequest>
    {
        public GetUserInfoController(IApiRequestHandler<GetUserInfoRequest> handler, IValidator<GetUserInfoRequest> validator) : base(handler, validator)
        {
        }
    }
}
