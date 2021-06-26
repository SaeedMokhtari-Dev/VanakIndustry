using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.List
{
    [Route(Endpoints.ApiElectionCandidateTypeList)]
    [ApiExplorerSettings(GroupName = "ElectionCandidateType")]
    [Authorize]
    public class ElectionCandidateTypeListController : ApiController<ElectionCandidateTypeListRequest>
    {
        public ElectionCandidateTypeListController(IApiRequestHandler<ElectionCandidateTypeListRequest> handler, IValidator<ElectionCandidateTypeListRequest> validator) : base(handler, validator)
        {
        }
    }
}
