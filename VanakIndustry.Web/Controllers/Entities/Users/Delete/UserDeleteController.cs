using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Users.Delete
{
    [Route(Endpoints.ApiUserDelete)]
    [ApiExplorerSettings(GroupName = "User")]
    [Authorize]
    public class UserDeleteController : ApiController<UserDeleteRequest>
    {
        public UserDeleteController(IApiRequestHandler<UserDeleteRequest> handler, IValidator<UserDeleteRequest> validator) : base(handler, validator)
        {
        }
    }
}
