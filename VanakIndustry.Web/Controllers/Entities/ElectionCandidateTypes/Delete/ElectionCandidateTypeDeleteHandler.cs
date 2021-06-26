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

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.Delete
{
    public class ElectionCandidateTypeDeleteHandler : ApiRequestHandler<ElectionCandidateTypeDeleteRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;

        public ElectionCandidateTypeDeleteHandler(
            VanakIndustryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<ActionResult> Execute(ElectionCandidateTypeDeleteRequest request)
        {
            ElectionCandidateType electionCandidateType = await _context.ElectionCandidateTypes
                .FindAsync(request.ElectionCandidateTypeId);

            if (electionCandidateType == null)
            {
                return ActionResult.Error(ApiMessages.ResourceNotFound);
            }

            await _context.ExecuteTransactionAsync(async () =>
            {
                _context.ElectionCandidateTypes.Remove(electionCandidateType);
                await _context.SaveChangesAsync();
            });
            
            return ActionResult.Ok(ApiMessages.ElectionCandidateTypeMessage.DeletedSuccessfully);
        }

    }
}
