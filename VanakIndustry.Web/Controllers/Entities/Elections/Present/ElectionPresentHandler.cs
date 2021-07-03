using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.Core.Enums;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.Web.Identity.Contexts;

namespace VanakIndustry.Web.Controllers.Entities.Elections.Present
{
    public class ElectionPresentHandler : ApiRequestHandler<ElectionPresentRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;
        
        public ElectionPresentHandler(
            VanakIndustryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<ActionResult> Execute(ElectionPresentRequest request)
        {
            var user = await _context.Users
                .AnyAsync(w => w.IsActive && w.Id == request.UserId);
                
            if(!user)
                return ActionResult.Error(ApiMessages.ResourceNotFound);

            var currentElection = await _context.Elections
                .Where(w => !w.Deleted && w.StartDate <= DateTime.Now && w.EndDate >= DateTime.Now)
                .FirstOrDefaultAsync();
            
            if(currentElection == null)
                return ActionResult.Error(ApiMessages.ElectionMessage.DontFindCurrentElection);
            
            if(!await _context.ElectionPresentUsers
                .AnyAsync(w => w.ElectionId == currentElection.Id && w.UserId == request.UserId))
                return ActionResult.Error(ApiMessages.ElectionMessage.UserIsNotPresent);
            
            ElectionPresentResponse response = new ElectionPresentResponse()
            {
                ElectionId = currentElection.Id,
                ElectionTitle = currentElection.Title
            };
            return ActionResult.Ok(response);
        }
    }
}
