using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.DataAccess.Contexts;

namespace VanakIndustry.Web.Controllers.Entities.SelectElectionCandidates.Get
{
    public class SelectElectionCandidateGetHandler : ApiRequestHandler<SelectElectionCandidateGetRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;

        public SelectElectionCandidateGetHandler(
            VanakIndustryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<ActionResult> Execute(SelectElectionCandidateGetRequest request)
        {
            var result = await _context.SelectElectionCandidates
                .Include(w => w.ElectionCandidate).ThenInclude(w => w.ElectionCandidateType)
                .Include(w => w.ElectionCandidate).ThenInclude(w => w.User)
                .Where(w => w.UserId == request.UserId)
                .OrderByDescending(w => w.Id)
                .Select(w => new SelectElectionCandidateGetResponse()
                {
                    Key = w.ElectionCandidateId,
                    ElectionId = w.ElectionCandidate.ElectionId,
                    UserId = w.ElectionCandidate.UserId,
                    UserFullName = w.ElectionCandidate.User.FullName,
                    CandidatePictureId = w.ElectionCandidate.User.CandidatePictureId ?? 0,
                    ElectionCandidateTypeId = w.ElectionCandidate.ElectionCandidateTypeId,
                    ElectionCandidateTypeTitle = w.ElectionCandidate.ElectionCandidateType.Title
                })
                .ToListAsync();
            
            return ActionResult.Ok(result);
        }
    }
}
