using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Elections.AddCandidate
{
    [Route(Endpoints.ApiElectionAddCandidate)]
    [ApiExplorerSettings(GroupName = "Election")]
    [Authorize]
    public class ElectionAddCandidateController : ApiController<ElectionAddCandidateRequest>
    {
        public ElectionAddCandidateController(IApiRequestHandler<ElectionAddCandidateRequest> handler, IValidator<ElectionAddCandidateRequest> validator) : base(handler, validator)
        {
        }
    }
}
