using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.Delete
{
    [Route(Endpoints.ApiElectionCandidateTypeDelete)]
    [ApiExplorerSettings(GroupName = "ElectionCandidateType")]
    [Authorize]
    public class ElectionCandidateTypeDeleteController : ApiController<ElectionCandidateTypeDeleteRequest>
    {
        public ElectionCandidateTypeDeleteController(IApiRequestHandler<ElectionCandidateTypeDeleteRequest> handler, IValidator<ElectionCandidateTypeDeleteRequest> validator) : base(handler, validator)
        {
        }
    }
}
