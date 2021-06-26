using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.Get
{
    [Route(Endpoints.ApiElectionCandidateTypeGet)]
    [ApiExplorerSettings(GroupName = "ElectionCandidateType")]
    [Authorize]
    public class ElectionCandidateTypeGetController : ApiController<ElectionCandidateTypeGetRequest>
    {
        public ElectionCandidateTypeGetController(IApiRequestHandler<ElectionCandidateTypeGetRequest> handler, IValidator<ElectionCandidateTypeGetRequest> validator) : base(handler, validator)
        {
        }
    }
}
