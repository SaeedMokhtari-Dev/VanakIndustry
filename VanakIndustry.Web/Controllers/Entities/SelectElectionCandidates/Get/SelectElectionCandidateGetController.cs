using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.SelectElectionCandidates.Get
{
    [Route(Endpoints.ApiSelectElectionCandidateGet)]
    [ApiExplorerSettings(GroupName = "SelectElectionCandidate")]
    [Authorize]
    public class SelectElectionCandidateGetController : ApiController<SelectElectionCandidateGetRequest>
    {
        public SelectElectionCandidateGetController(IApiRequestHandler<SelectElectionCandidateGetRequest> handler, IValidator<SelectElectionCandidateGetRequest> validator) : base(handler, validator)
        {
        }
    }
}
