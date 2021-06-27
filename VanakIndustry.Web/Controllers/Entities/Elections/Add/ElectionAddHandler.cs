using System;
using System.Collections.Generic;
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

namespace VanakIndustry.Web.Controllers.Entities.Elections.Add
{
    public class ElectionAddHandler : ApiRequestHandler<ElectionAddRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;
        private readonly PasswordService _passwordService;
        
        public ElectionAddHandler(
            VanakIndustryContext context, IMapper mapper, PasswordService passwordService)
        {
            _context = context;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        protected override async Task<ActionResult> Execute(ElectionAddRequest request)
        {
            Election election = await AddElection(request);
            
            return ActionResult.Ok(ApiMessages.ElectionMessage.AddedSuccessfully);
        }
        
        private async Task<Election> AddElection(ElectionAddRequest request)
        {
            Election election = await _context.ExecuteTransactionAsync(async () =>
            {
                Election newElection = _mapper.Map<Election>(request);
                newElection.ElectionLimits = new List<ElectionLimit>();
                foreach (var requestElectionLimitItem in request.ElectionLimitItems)
                {
                    newElection.ElectionLimits.Add(
                        new ElectionLimit()
                        {
                            ElectionCandidateTypeId = requestElectionLimitItem.ElectionCandidateTypeId,
                            LimitCount = requestElectionLimitItem.LimitCount
                        });
                }
                
                newElection = (await _context.Elections.AddAsync(newElection)).Entity;
                await _context.SaveChangesAsync();

                return newElection;
            });
            return election;
        }
    }
}