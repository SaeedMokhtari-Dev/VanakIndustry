using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Api.Controllers;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Add
{
    [Route(Endpoints.ApiElectionAdd)]
    [ApiExplorerSettings(GroupName = "Election")]
    [Authorize]
    public class ElectionAddController : ApiController<ElectionAddRequest>
    {
        public ElectionAddController(IApiRequestHandler<ElectionAddRequest> handler, IValidator<ElectionAddRequest> validator) : base(handler, validator)
        {
        }
    }
}
