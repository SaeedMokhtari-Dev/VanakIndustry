using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Users.Get
{
    [Route(Endpoints.ApiUserGet)]
    [ApiExplorerSettings(GroupName = "User")]
    [Authorize]
    public class UserGetController : ApiController<UserGetRequest>
    {
        public UserGetController(IApiRequestHandler<UserGetRequest> handler, IValidator<UserGetRequest> validator) : base(handler, validator)
        {
        }
    }
}
