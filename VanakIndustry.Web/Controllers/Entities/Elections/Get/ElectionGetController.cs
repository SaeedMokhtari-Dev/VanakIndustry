using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Get
{
    [Route(Endpoints.ApiElectionGet)]
    [ApiExplorerSettings(GroupName = "Election")]
    [Authorize]
    public class ElectionGetController : ApiController<ElectionGetRequest>
    {
        public ElectionGetController(IApiRequestHandler<ElectionGetRequest> handler, IValidator<ElectionGetRequest> validator) : base(handler, validator)
        {
        }
    }
}
