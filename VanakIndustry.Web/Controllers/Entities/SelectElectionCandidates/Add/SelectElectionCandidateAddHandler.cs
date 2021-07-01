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
using VanakIndustry.Web.Extensions;
using VanakIndustry.Web.Identity.Services;

namespace VanakIndustry.Web.Controllers.Entities.SelectElectionCandidates.Add
{
    public class SelectElectionCandidateAddHandler : ApiRequestHandler<SelectElectionCandidateAddRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;

        public SelectElectionCandidateAddHandler(
            VanakIndustryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<ActionResult> Execute(SelectElectionCandidateAddRequest request)
        {
            var user = await _context.Users.Include(w => w.SelectElectionCandidates)
                .Include(w => w.ElectionPresentUsers).FirstOrDefaultAsync(w => w.Id == request.UserId);
            
            if(user == null)
                return ActionResult.Error(ApiMessages.ResourceNotFound);
            
            if(user.ElectionPresentUsers.All(w => w.ElectionId != request.ElectionId))
                return ActionResult.Error(ApiMessages.InvalidRequest);
                
            
            await AddSelectElectionCandidate(user, request);
            
            return ActionResult.Ok(ApiMessages.SelectElectionCandidateMessage.AddedSuccessfully);
        }
        
        private async Task AddSelectElectionCandidate(User user, SelectElectionCandidateAddRequest request)
        {
            await _context.ExecuteTransactionAsync(async () =>
            {
                List<long> userIds = user.SelectElectionCandidates
                    .Select(w => w.ElectionCandidateId).ToList();
                
                var shouldRemoveElectionCandidateIds = userIds.Except(request.ElectionCandidateIds).ToList();
                foreach (var shouldRemoveElectionCandidateId in shouldRemoveElectionCandidateIds)
                {
                    var removeEntity = user.SelectElectionCandidates.Single(w => w.ElectionCandidateId == shouldRemoveElectionCandidateId);
                    _context.Remove(removeEntity);
                }
                
                var shouldAdded = request.ElectionCandidateIds.Except(userIds).ToList();
                foreach (var w in shouldAdded)
                {
                    user.SelectElectionCandidates.Add(new SelectElectionCandidate()
                    {
                        ElectionCandidateId = w,
                        Finalize = false,
                        UserId = user.Id
                    });
                }
                await _context.SaveChangesAsync();
            });
        }
    }
}