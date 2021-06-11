using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;
using VanakIndustry.Web.Identity.Services;

namespace VanakIndustry.Web.Controllers.Auth.ChangePassword
{
    public class ChangePasswordHandler : ApiRequestHandler<ChangePasswordRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly PasswordService _passwordService;

        public ChangePasswordHandler(
            VanakIndustryContext context, PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        protected override async Task<ActionResult> Execute(ChangePasswordRequest request)
        {
            if (request.NewPassword != request.ConfirmPassword)
            {
                return ActionResult.Error(ApiMessages.Auth.ChangePasswordNotEqualsPasswords);
            }

            if (!Guid.TryParse(request.Token, out var token))
            {
                return ActionResult.Error(ApiMessages.Auth.ValidateResetPasswordTokenInvalidToken);
            }

            var passwordResetToken = await _context.PasswordResetTokens.SingleOrDefaultAsync(w => w.Token == token);

            if (passwordResetToken == null)
            {
                return ActionResult.Error(ApiMessages.Auth.ValidateResetPasswordTokenInvalidToken);
            }
            
            var user = await _context.Users.FirstOrDefaultAsync(x => x.IsActive && x.Id == passwordResetToken.UserId);

            if (user == null)
            {
                return ActionResult.Error(ApiMessages.ResourceNotFound);
            }

            await ChangePasswordAndDeletePasswordResetToken(user, request.NewPassword, passwordResetToken);

            return ActionResult.Ok(ApiMessages.Auth.ChangePasswordSuccessful);
        }
        private async Task ChangePasswordAndDeletePasswordResetToken(User user, string requestPassword, PasswordResetToken passwordResetToken)
        {
            var hashedPassword = _passwordService.GetPasswordHash(requestPassword);
            user.Password = hashedPassword;
            user.IsActive = true;
            
            await _context.ExecuteTransactionAsync( async () =>
            {
                _context.PasswordResetTokens.Remove(passwordResetToken);
                await _context.SaveChangesAsync();
            });
        }
    }
}
