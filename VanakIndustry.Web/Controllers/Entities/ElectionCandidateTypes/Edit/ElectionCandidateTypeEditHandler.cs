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

namespace VanakIndustry.Web.Controllers.Entities.ElectionCandidateTypes.Edit
{
    public class ElectionCandidateTypeEditHandler : ApiRequestHandler<ElectionCandidateTypeEditRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IMapper _mapper;
        private readonly PasswordService _passwordService;

        public ElectionCandidateTypeEditHandler(
            VanakIndustryContext context, IMapper mapper, PasswordService passwordService)
        {
            _context = context;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        protected override async Task<ActionResult> Execute(ElectionCandidateTypeEditRequest request)
        {
            ElectionCandidateType editElectionCandidateType = await _context.ElectionCandidateTypes
                .FindAsync(request.ElectionCandidateTypeId);

            if (editElectionCandidateType == null)
            {
                return ActionResult.Error(ApiMessages.ResourceNotFound);
            }

            await EditElectionCandidateType(editElectionCandidateType, request);
            return ActionResult.Ok(ApiMessages.ElectionCandidateTypeMessage.EditedSuccessfully);
        }

        private async Task EditElectionCandidateType(ElectionCandidateType editElectionCandidateType, ElectionCandidateTypeEditRequest request)
        {
            await _context.ExecuteTransactionAsync(async () =>
            {
                _mapper.Map(request, editElectionCandidateType);
                await _context.SaveChangesAsync();

                return editElectionCandidateType;
            });
        }
    }
}