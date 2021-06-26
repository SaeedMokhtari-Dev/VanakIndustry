using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.Add
{
    [Route(Endpoints.ApiElectionCandidateTypeAdd)]
    [ApiExplorerSettings(GroupName = "ElectionCandidateType")]
    [Authorize]
    public class ElectionCandidateTypeAddController : ApiController<ElectionCandidateTypeAddRequest>
    {
        public ElectionCandidateTypeAddController(IApiRequestHandler<ElectionCandidateTypeAddRequest> handler, IValidator<ElectionCandidateTypeAddRequest> validator) : base(handler, validator)
        {
        }
    }
}
