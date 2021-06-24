using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Users.Detail
{
    [Route(Endpoints.ApiUserDetail)]
    [ApiExplorerSettings(GroupName = "User")]
    [Authorize]
    public class UserDetailController : ApiController<UserDetailRequest>
    {
        public UserDetailController(IApiRequestHandler<UserDetailRequest> handler, IValidator<UserDetailRequest> validator) : base(handler, validator)
        {
        }
    }
}
