using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.Core.Enums;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;
using VanakIndustry.Web.Identity.Services;

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.Add
{
    public class ElectionCandidateTypeAddHandler : ApiRequestHandler<ElectionCandidateTypeAddRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;
        private readonly PasswordService _passwordService;
        
        public ElectionCandidateTypeAddHandler(
            VanakIndustryContext context, IMapper mapper, PasswordService passwordService)
        {
            _context = context;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        protected override async Task<ActionResult> Execute(ElectionCandidateTypeAddRequest request)
        {
            ElectionCandidateType electionCandidateType = await AddElectionCandidateType(request);
            
            return ActionResult.Ok(ApiMessages.ElectionCandidateTypeMessage.AddedSuccessfully);
        }
        
        private async Task<ElectionCandidateType> AddElectionCandidateType(ElectionCandidateTypeAddRequest request)
        {
            ElectionCandidateType electionCandidateType = await _context.ExecuteTransactionAsync(async () =>
            {
                ElectionCandidateType newElectionCandidateType = _mapper.Map<ElectionCandidateType>(request);
                
                newElectionCandidateType = (await _context.ElectionCandidateTypes.AddAsync(newElectionCandidateType)).Entity;
                await _context.SaveChangesAsync();

                return newElectionCandidateType;
            });
            return electionCandidateType;
        }
    }
}