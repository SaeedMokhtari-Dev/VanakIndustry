using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.Core.Enums;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;
using VanakIndustry.Web.Identity.Services;

namespace VanakIndustry.Web.Controllers.Entities.Elections.AddCandidate
{
    public class ElectionAddCandidateHandler : ApiRequestHandler<ElectionAddCandidateRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;
        private readonly PasswordService _passwordService;
        
        public ElectionAddCandidateHandler(
            VanakIndustryContext context, IMapper mapper, PasswordService passwordService)
        {
            _context = context;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        protected override async Task<ActionResult> Execute(ElectionAddCandidateRequest request)
        {
            Election election = await _context.Elections.Include(w => w.ElectionCandidates)
                .Include(w => w.ElectionLimits)
                .FirstOrDefaultAsync(w => w.Id == request.ElectionId);
            
            if(election == null)
                return ActionResult.Error(ApiMessages.ResourceNotFound);
            
            if(election.StartDate <= DateTime.Now)
                return ActionResult.Error(ApiMessages.Forbidden);
            
            if(election.Deleted || election.Finalize)
                return ActionResult.Error(ApiMessages.Forbidden);
            
            await AddCandidateElection(election, request);
            
            return ActionResult.Ok(ApiMessages.ElectionMessage.CandidateAddedSuccessfully);
        }
        
        private async Task AddCandidateElection(Election election, ElectionAddCandidateRequest request)
        {
            await _context.ExecuteTransactionAsync(async () =>
            {
                List<long> userIds = election.ElectionCandidates
                    .Where(w => w.ElectionCandidateTypeId == request.ElectionCandidateTypeId)
                    .Select(w => w.UserId).ToList();
                
                var shouldRemoveUserIds = userIds.Except(request.UserIds).ToList();
                foreach (var shouldRemoveUserId in shouldRemoveUserIds)
                {
                    var removeEntity = election.ElectionCandidates.Single(w => w.UserId == shouldRemoveUserId);
                    _context.Remove(removeEntity);
                }
                
                var shouldAdded = request.UserIds.Except(userIds).ToList();
                foreach (var w in shouldAdded)
                {
                    election.ElectionCandidates.Add(new ElectionCandidate()
                    {
                        UserId = w,
                        ElectionCandidateTypeId = request.ElectionCandidateTypeId,
                        ElectionId = request.ElectionId
                    });
                }
                await _context.SaveChangesAsync();
            });
        }
    }
}