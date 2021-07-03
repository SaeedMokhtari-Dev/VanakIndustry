using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.DataAccess.Contexts;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidates.Get
{
    public class ElectionCandidateGetHandler : ApiRequestHandler<ElectionCandidateGetRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;

        public ElectionCandidateGetHandler(
            VanakIndustryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<ActionResult> Execute(ElectionCandidateGetRequest request)
        {
            var electionLimits = await _context.ElectionLimits.Where(w => w.ElectionId == request.ElectionId)
                .Include(w => w.Election)
                .Include(w => w.ElectionCandidateType)
                .ToListAsync();
            List<ElectionCandidateGetResponse> response = new List<ElectionCandidateGetResponse>();
            foreach (var electionLimit in electionLimits)
            {
                ElectionCandidateGetResponse electionCandidateGetResponse = new ElectionCandidateGetResponse();
                electionCandidateGetResponse.ElectionId = electionLimit.ElectionId;
                electionCandidateGetResponse.ElectionTitle = electionLimit.Election.Title;
                electionCandidateGetResponse.ElectionCandidateTypeId = electionLimit.ElectionCandidateTypeId;
                electionCandidateGetResponse.ElectionCandidateTypeTitle = electionLimit.ElectionCandidateType.Title;
                electionCandidateGetResponse.LimitCount = electionLimit.LimitCount;
                electionCandidateGetResponse.Items = await _context.ElectionCandidates
                    .Where(w => w.ElectionId == electionLimit.ElectionId &&
                                w.ElectionCandidateTypeId == electionLimit.ElectionCandidateTypeId)
                    .Include(w => w.User)
                    .Select(w => new ElectionCandidateGetResponseItem()
                    {
                        Key = w.Id,
                        UserId = w.UserId,
                        UserFullName = w.User.FullName,
                        CandidatePictureId = w.User.CandidatePictureId ?? 0
                    })
                    .ToListAsync();
                response.Add(electionCandidateGetResponse);
            }
            
            return ActionResult.Ok(response);
        }
    }
}
