using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Delete
{
    [Route(Endpoints.ApiElectionDelete)]
    [ApiExplorerSettings(GroupName = "Election")]
    [Authorize]
    public class ElectionDeleteController : ApiController<ElectionDeleteRequest>
    {
        public ElectionDeleteController(IApiRequestHandler<ElectionDeleteRequest> handler, IValidator<ElectionDeleteRequest> validator) : base(handler, validator)
        {
        }
    }
}
