using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidates.Get
{
    [Route(Endpoints.ApiElectionCandidateGet)]
    [ApiExplorerSettings(GroupName = "ElectionCandidate")]
    [Authorize]
    public class ElectionCandidateGetController : ApiController<ElectionCandidateGetRequest>
    {
        public ElectionCandidateGetController(IApiRequestHandler<ElectionCandidateGetRequest> handler, IValidator<ElectionCandidateGetRequest> validator) : base(handler, validator)
        {
        }
    }
}
