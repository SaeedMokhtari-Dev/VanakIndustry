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

namespace VanakIndustry.Web.Controllers.Entities.Elections.Delete
{
    public class ElectionDeleteHandler : ApiRequestHandler<ElectionDeleteRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;

        public ElectionDeleteHandler(
            VanakIndustryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<ActionResult> Execute(ElectionDeleteRequest request)
        {
            Election election = await _context.Elections
                .FindAsync(request.ElectionId);

            if (election == null)
            {
                return ActionResult.Error(ApiMessages.ResourceNotFound);
            }

            await _context.ExecuteTransactionAsync(async () =>
            {
                election.Deleted = true;
                await _context.SaveChangesAsync();
            });
            
            return ActionResult.Ok(ApiMessages.ElectionMessage.DeletedSuccessfully);
        }

    }
}
