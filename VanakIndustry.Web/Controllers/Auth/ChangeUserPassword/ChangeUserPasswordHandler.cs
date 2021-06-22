using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.Core.Enums;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.Web.Identity.Contexts;
using VanakIndustry.Web.Identity.Services;

namespace VanakIndustry.Web.Controllers.Auth.ChangeUserPassword
{
    public class ChangeUserPasswordHandler : ApiRequestHandler<ChangeUserPasswordRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly UserContext _userContext;
        private readonly PasswordService _passwordService;

        public ChangeUserPasswordHandler(
            VanakIndustryContext context, UserContext userContext, PasswordService passwordService)
        {
            this._context = context;
            _userContext = userContext;
            _passwordService = passwordService;
        }

        protected override async Task<ActionResult> Execute(ChangeUserPasswordRequest request)
        {
            if (request.NewPassword != request.ConfirmPassword)
                return ActionResult.Error(ApiMessages.Auth.ChangePasswordNotEqualsPasswords);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.IsActive && x.Id == _userContext.Id);
            if(user == null)
                return ActionResult.Error(ApiMessages.ResourceNotFound);

            if (!_passwordService.IsPasswordValid(request.CurrentPassword, user.Password))
            {
                return ActionResult.Error(ApiMessages.Auth.ChangePasswordCurrentPasswordIsNotCorrect);
            }

            user.Password = _passwordService.GetPasswordHash(request.NewPassword);
            await _context.SaveChangesAsync();
            
            return ActionResult.Ok(ApiMessages.Auth.ChangePasswordSuccessful);
        }
    }
}
