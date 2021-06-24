using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Users.Edit
{
    [Route(Endpoints.ApiUserEdit)]
    [ApiExplorerSettings(GroupName = "User")]
    [Authorize]
    public class UserEditController : ApiController<UserEditRequest>
    {
        public UserEditController(IApiRequestHandler<UserEditRequest> handler, IValidator<UserEditRequest> validator) : base(handler, validator)
        {
        }
    }
}
