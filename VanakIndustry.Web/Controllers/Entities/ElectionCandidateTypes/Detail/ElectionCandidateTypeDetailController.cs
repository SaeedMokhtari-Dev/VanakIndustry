using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.Detail
{
    [Route(Endpoints.ApiElectionCandidateTypeDetail)]
    [ApiExplorerSettings(GroupName = "ElectionCandidateType")]
    [Authorize]
    public class ElectionCandidateTypeDetailController : ApiController<ElectionCandidateTypeDetailRequest>
    {
        public ElectionCandidateTypeDetailController(IApiRequestHandler<ElectionCandidateTypeDetailRequest> handler, IValidator<ElectionCandidateTypeDetailRequest> validator) : base(handler, validator)
        {
        }
    }
}
