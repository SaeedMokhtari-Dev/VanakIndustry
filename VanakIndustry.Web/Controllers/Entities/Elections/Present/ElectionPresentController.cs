using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Present
{
    [Route(Endpoints.ApiElectionPresent)]
    [ApiExplorerSettings(GroupName = "Election")]
    [Authorize]
    public class ElectionPresentController : ApiController<ElectionPresentRequest>
    {
        public ElectionPresentController(IApiRequestHandler<ElectionPresentRequest> handler, IValidator<ElectionPresentRequest> validator) : base(handler, validator)
        {
        }
    }
}
