using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;
using VanakIndustry.Web.Services;

namespace VanakIndustry.Web.Controllers.Auth.ResetPassword
{
    public class ResetPasswordHandler : ApiRequestHandler<ResetPasswordRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly EmailService _emailService;

        public ResetPasswordHandler(
            VanakIndustryContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        protected override async Task<ActionResult> Execute(ResetPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.IsActive && x.Email.ToUpper() == request.Email.ToUpper().Trim());

            if(user == null) return ActionResult.Ok(ApiMessages.Auth.ResetPasswordResponse);

            Guid token = await GenerateANewTokenAndSave(user);

            
            await _emailService.SendResetPasswordEmail(user, token);

            return ActionResult.Ok(ApiMessages.Auth.ResetPasswordResponse);
        }

        

        private async Task<Guid> GenerateANewTokenAndSave(User user)
        {
            PasswordResetToken passwordResetToken = 
                await _context.PasswordResetTokens.SingleOrDefaultAsync(w => w.UserId == user.Id);

            if (passwordResetToken != null)
            {
                passwordResetToken.Token = Guid.NewGuid();
                passwordResetToken.ResetRequestDate = DateTime.UtcNow;
            }
            else
            {
                passwordResetToken = new PasswordResetToken()
                {
                    Token = Guid.NewGuid(),
                    UserId = user.Id,
                    ResetRequestDate = DateTime.UtcNow
                };
                await _context.PasswordResetTokens.AddAsync(passwordResetToken);
            }

            await _context.SaveChangesAsync();

            return passwordResetToken.Token;
        }

        /*private async Task SetLastResetPassword(User user)
        {
            user.LastResetPasswordAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }*/
    }
}
