using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.Edit
{
    [Route(Endpoints.ApiElectionCandidateTypeEdit)]
    [ApiExplorerSettings(GroupName = "ElectionCandidateType")]
    [Authorize]
    public class ElectionCandidateTypeEditController : ApiController<ElectionCandidateTypeEditRequest>
    {
        public ElectionCandidateTypeEditController(IApiRequestHandler<ElectionCandidateTypeEditRequest> handler, IValidator<ElectionCandidateTypeEditRequest> validator) : base(handler, validator)
        {
        }
    }
}
