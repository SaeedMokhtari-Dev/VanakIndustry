using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Edit
{
    [Route(Endpoints.ApiElectionEdit)]
    [ApiExplorerSettings(GroupName = "Election")]
    [Authorize]
    public class ElectionEditController : ApiController<ElectionEditRequest>
    {
        public ElectionEditController(IApiRequestHandler<ElectionEditRequest> handler, IValidator<ElectionEditRequest> validator) : base(handler, validator)
        {
        }
    }
}
