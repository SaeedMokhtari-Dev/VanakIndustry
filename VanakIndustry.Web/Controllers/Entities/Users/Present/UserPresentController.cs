using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Users.Present
{
    [Route(Endpoints.ApiUserPresent)]
    [ApiExplorerSettings(GroupName = "User")]
    [Authorize]
    public class UserPresentController : ApiController<UserPresentRequest>
    {
        public UserPresentController(IApiRequestHandler<UserPresentRequest> handler, IValidator<UserPresentRequest> validator) : base(handler, validator)
        {
        }
    }
}
