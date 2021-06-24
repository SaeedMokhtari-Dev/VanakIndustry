using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Users.Add
{
    [Route(Endpoints.ApiUserAdd)]
    [ApiExplorerSettings(GroupName = "User")]
    [Authorize]
    public class UserAddController : ApiController<UserAddRequest>
    {
        public UserAddController(IApiRequestHandler<UserAddRequest> handler, IValidator<UserAddRequest> validator) : base(handler, validator)
        {
        }
    }
}
