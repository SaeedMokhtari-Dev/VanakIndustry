using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.Detail
{
    public class ElectionCandidateTypeDetailHandler : ApiRequestHandler<ElectionCandidateTypeDetailRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;

        public ElectionCandidateTypeDetailHandler(
            VanakIndustryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<ActionResult> Execute(ElectionCandidateTypeDetailRequest request)
        {
            ElectionCandidateType electionCandidateType = await _context.ElectionCandidateTypes
                .FindAsync(request.ElectionCandidateTypeId);

            if (electionCandidateType == null)
            {
                return ActionResult.Error(ApiMessages.ResourceNotFound);
            }

            ElectionCandidateTypeDetailResponse response = _mapper.Map<ElectionCandidateTypeDetailResponse>(electionCandidateType);
            
            return ActionResult.Ok(response);
        }
    }
}
