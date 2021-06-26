using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.Core.Enums;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.Web.Identity.Contexts;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.List
{
    public class ElectionCandidateTypeListHandler : ApiRequestHandler<ElectionCandidateTypeListRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;
        
        public ElectionCandidateTypeListHandler(
            VanakIndustryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<ActionResult> Execute(ElectionCandidateTypeListRequest request)
        {
            var query = _context.ElectionCandidateTypes
                .OrderByDescending(w => w.Id)
                .AsQueryable();

            var response = await query.Select(w =>
            new ElectionCandidateTypeListResponseItem() {
                Key = w.Id, 
                Title = w.Title
            }).ToListAsync();
            
            return ActionResult.Ok(response);
        }
    }
}
