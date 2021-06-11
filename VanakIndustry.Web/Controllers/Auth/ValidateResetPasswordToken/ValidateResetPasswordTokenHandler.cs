using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.DataAccess.Contexts;

namespace VanakIndustry.Web.Controllers.Auth.ValidateResetPasswordToken
{
    public class ValidateResetPasswordTokenHandler : ApiRequestHandler<ValidateResetPasswordTokenRequest>
    {
        private readonly VanakIndustryContext _context;
        private readonly IConfiguration _configuration;

        public ValidateResetPasswordTokenHandler(
            VanakIndustryContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        protected override async Task<ActionResult> Execute(ValidateResetPasswordTokenRequest request)
        {
            if(!Guid.TryParse(request.Token, out var token))
                return ActionResult.Error(ApiMessages.Auth.ValidateResetPasswordTokenInvalidToken);
            
            var passwordResetToken = await _context.PasswordResetTokens.Include(w => w.User).SingleOrDefaultAsync(w => w.Token == token);

            if(passwordResetToken == null) return ActionResult.Error(ApiMessages.Auth.ValidateResetPasswordTokenInvalidToken);

            if (passwordResetToken.User.IsActive == false)
            {
                int activationAccountExpirationHour = _configuration.GetValue<int>("ActivationAccountExpirationHour");
                if(passwordResetToken.ResetRequestDate.AddHours(activationAccountExpirationHour) <= DateTime.UtcNow)
                    return ActionResult.Error(ApiMessages.Auth.ValidateResetPasswordTokenInvalidToken);
            }
            else
            {
                int resetPasswordExpirationHour = _configuration.GetValue<int>("ResetPasswordExpirationHour");
                if(passwordResetToken.ResetRequestDate.AddHours(resetPasswordExpirationHour) <= DateTime.UtcNow)
                    return ActionResult.Error(ApiMessages.Auth.ValidateResetPasswordTokenInvalidToken);    
            }
            return ActionResult.Ok(ApiMessages.Auth.ValidateResetPasswordTokenValidToken);
        }
    }
}
