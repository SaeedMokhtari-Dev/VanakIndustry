using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;
using VanakIndustry.Web.Controllers.Entities.Elections.Add;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Detail
{
    public class ElectionDetailHandler : ApiRequestHandler<ElectionDetailRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;

        public ElectionDetailHandler(
            VanakIndustryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<ActionResult> Execute(ElectionDetailRequest request)
        {
            Election election = await _context.Elections
                .Include(w => w.ElectionLimits).ThenInclude(w => w.ElectionCandidateType)
                .Include(w => w.ElectionCandidates).ThenInclude(w => w.User)
                .Include(w => w.ElectionCandidates).ThenInclude(w => w.ElectionCandidateType)
                .FirstOrDefaultAsync(w => w.Id == request.ElectionId);

            if (election == null)
            {
                return ActionResult.Error(ApiMessages.ResourceNotFound);
            }

            ElectionDetailResponse response = _mapper.Map<ElectionDetailResponse>(election);
            /*response.ElectionLimitItems = new List<ElectionLimitItem>();
            foreach (var electionElectionLimit in election.ElectionLimits)
            {
                response.ElectionLimitItems.Add(new ElectionLimitItem()
                {
                    Key = electionElectionLimit.Id,
                    ElectionCandidateTypeId = electionElectionLimit.ElectionCandidateTypeId,
                    LimitCount = electionElectionLimit.LimitCount
                });
            }*/
            
            return ActionResult.Ok(response);
        }
    }
}
