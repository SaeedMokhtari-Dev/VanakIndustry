using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.SelectElectionCandidates.Add
{
    [Route(Endpoints.ApiSelectElectionCandidateAdd)]
    [ApiExplorerSettings(GroupName = "SelectElectionCandidate")]
    [Authorize]
    public class SelectElectionCandidateAddController : ApiController<SelectElectionCandidateAddRequest>
    {
        public SelectElectionCandidateAddController(IApiRequestHandler<SelectElectionCandidateAddRequest> handler, IValidator<SelectElectionCandidateAddRequest> validator) : base(handler, validator)
        {
        }
    }
}
