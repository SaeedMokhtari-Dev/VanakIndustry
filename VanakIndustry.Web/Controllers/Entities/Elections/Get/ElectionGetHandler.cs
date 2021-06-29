using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.DataAccess.Contexts;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Get
{
    public class ElectionGetHandler : ApiRequestHandler<ElectionGetRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;

        public ElectionGetHandler(
            VanakIndustryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<ActionResult> Execute(ElectionGetRequest request)
        {
            var query = _context.Elections
                .Include(w => w.ElectionLimits).ThenInclude(w => w.ElectionCandidateType)
                .Where(w => !w.Deleted)
                .OrderByDescending(w => w.Id)
                .Skip(request.PageIndex * request.PageSize).Take(request.PageSize)
                .AsQueryable();

            var result = await query.ToListAsync();

            var mappedResult = _mapper.Map<List<ElectionGetResponseItem>>(result);

            ElectionGetResponse response = new ElectionGetResponse();
            response.TotalCount = await _context.Elections.CountAsync();
            response.Items = mappedResult;
            return ActionResult.Ok(response);
        }
    }
}
