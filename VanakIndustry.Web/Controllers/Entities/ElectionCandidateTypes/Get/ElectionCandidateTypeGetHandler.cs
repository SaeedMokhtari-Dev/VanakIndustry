using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.DataAccess.Contexts;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.Get
{
    public class ElectionCandidateTypeGetHandler : ApiRequestHandler<ElectionCandidateTypeGetRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;

        public ElectionCandidateTypeGetHandler(
            VanakIndustryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<ActionResult> Execute(ElectionCandidateTypeGetRequest request)
        {
            var query = _context.ElectionCandidateTypes.OrderByDescending(w => w.Id)
                .Skip(request.PageIndex * request.PageSize).Take(request.PageSize)
                .AsQueryable();

            var result = await query.ToListAsync();

            var mappedResult = _mapper.Map<List<ElectionCandidateTypeGetResponseItem>>(result);

            ElectionCandidateTypeGetResponse response = new ElectionCandidateTypeGetResponse();
            response.TotalCount = await _context.ElectionCandidateTypes.CountAsync();
            response.Items = mappedResult;
            return ActionResult.Ok(response);
        }
    }
}
