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

namespace VanakIndustry.Web.Controllers.Entities.Elections.List
{
    public class ElectionListHandler : ApiRequestHandler<ElectionListRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;
        
        public ElectionListHandler(
            VanakIndustryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<ActionResult> Execute(ElectionListRequest request)
        {
            var query = _context.Elections
                .OrderByDescending(w => w.Id)
                .AsQueryable();

            var response = await query.Select(w =>
            new ElectionListResponseItem() {
                Key = w.Id, 
                Title = w.Title
            }).ToListAsync();
            
            return ActionResult.Ok(response);
        }
    }
}
