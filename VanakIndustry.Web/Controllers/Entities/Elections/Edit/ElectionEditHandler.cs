using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Itenso.TimePeriod;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;
using VanakIndustry.Web.Identity.Services;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Edit
{
    public class ElectionEditHandler : ApiRequestHandler<ElectionEditRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;
        private readonly PasswordService _passwordService;

        public ElectionEditHandler(
            VanakIndustryContext context, IMapper mapper, PasswordService passwordService)
        {
            _context = context;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        protected override async Task<ActionResult> Execute(ElectionEditRequest request)
        {
            Election editElection = await _context.Elections.Include(w => w.ElectionLimits)
                .FirstOrDefaultAsync(w => w.Id == request.ElectionId);

            if (editElection == null)
            {
                return ActionResult.Error(ApiMessages.ResourceNotFound);
            }
            
            if(editElection.StartDate <= DateTime.Now)
                return ActionResult.Error(ApiMessages.Forbidden);
            
            if(editElection.Deleted || editElection.Finalize)
                return ActionResult.Error(ApiMessages.Forbidden);

            await EditElection(editElection, request);
            return ActionResult.Ok(ApiMessages.ElectionMessage.EditedSuccessfully);
        }

        private async Task EditElection(Election editElection, ElectionEditRequest request)
        {
            await _context.ExecuteTransactionAsync(async () =>
            {
                _mapper.Map(request, editElection);
             
                foreach (var requestElectionLimitItem in request.ElectionLimitItems)
                {
                    ElectionLimit electionLimit =
                        editElection.ElectionLimits.FirstOrDefault(w => w.Id == requestElectionLimitItem.Id);
                    if (electionLimit == null)
                    {
                        editElection.ElectionLimits.Add(new ElectionLimit()
                        {
                            LimitCount = requestElectionLimitItem.LimitCount,
                            ElectionCandidateTypeId = requestElectionLimitItem.ElectionCandidateTypeId
                        });
                    }
                    else
                    {
                        electionLimit.LimitCount = requestElectionLimitItem.LimitCount;
                        electionLimit.ElectionCandidateTypeId = requestElectionLimitItem.ElectionCandidateTypeId;
                    }
                }
                
                await _context.SaveChangesAsync();

                return editElection;
            });
        }
    }
}