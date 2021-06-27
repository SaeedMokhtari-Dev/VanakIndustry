using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Detail
{
    [Route(Endpoints.ApiElectionDetail)]
    [ApiExplorerSettings(GroupName = "Election")]
    [Authorize]
    public class ElectionDetailController : ApiController<ElectionDetailRequest>
    {
        public ElectionDetailController(IApiRequestHandler<ElectionDetailRequest> handler, IValidator<ElectionDetailRequest> validator) : base(handler, validator)
        {
        }
    }
}
