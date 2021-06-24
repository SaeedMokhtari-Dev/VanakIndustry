using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Users.Active
{
    [Route(Endpoints.ApiUserActive)]
    [ApiExplorerSettings(GroupName = "User")]
    [Authorize]
    public class UserActiveController : ApiController<UserActiveRequest>
    {
        public UserActiveController(IApiRequestHandler<UserActiveRequest> handler, IValidator<UserActiveRequest> validator) : base(handler, validator)
        {
        }
    }
}
