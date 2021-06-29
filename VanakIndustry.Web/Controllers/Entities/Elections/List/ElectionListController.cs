using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Elections.List
{
    [Route(Endpoints.ApiElectionList)]
    [ApiExplorerSettings(GroupName = "Election")]
    [Authorize]
    public class ElectionListController : ApiController<ElectionListRequest>
    {
        public ElectionListController(IApiRequestHandler<ElectionListRequest> handler, IValidator<ElectionListRequest> validator) : base(handler, validator)
        {
        }
    }
}
