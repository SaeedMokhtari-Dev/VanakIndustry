using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Users.List
{
    [Route(Endpoints.ApiUserList)]
    [ApiExplorerSettings(GroupName = "User")]
    [Authorize]
    public class UserListController : ApiController<UserListRequest>
    {
        public UserListController(IApiRequestHandler<UserListRequest> handler, IValidator<UserListRequest> validator) : base(handler, validator)
        {
        }
    }
}
