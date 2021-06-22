using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Interfaces;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Identity.Services
{
   public class RefreshTokenService: IScoped
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly VanakIndustryContext _context;

        public RefreshTokenService(VanakIndustryContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> GenerateRefreshToken(long userId)
        {
            var token = new RefreshToken
            {
                UserId = userId,
                Token = GetRefreshTokenString(),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.Add(IdentitySettings.RefreshTokenExpireTime),
                IsActive = true
            };

            await SaveToken(token);

            return token;
        }

        private string GetRefreshTokenString()
        {
            return Guid.NewGuid().ToString();
        }

        public async Task SaveToken(RefreshToken token)
        {
            try
            {
                
                _context.Entry(token).State = EntityState.Added;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

                throw;
            }
        }

        public async Task<RefreshToken> GetUpdatedRefreshToken(string tokenStr, long userId)
        {
            try
            {
                var token = await _context.RefreshTokens
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive && x.Token == tokenStr && x.User.IsActive);

                if (token == null) return null;

                if (token.ExpiresAt < DateTime.UtcNow)
                {
                    token.IsActive = false;

                    await _context.SaveChangesAsync();

                    return null;
                }
                else
                {
                    UpdateTokenExpiration(token);

                    return token;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        public async Task<ActionResult> DeactivateToken(string tokenStr, long userId)
        {
            try
            {
                var token = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive && x.Token == tokenStr);

                if (token != null)
                {
                    token.IsActive = false;

                    await _context.SaveChangesAsync();
                }

                return ActionResult.Ok();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return ActionResult.Error();
            }
        }

        private void UpdateTokenExpiration(RefreshToken token)
        {
            try
            {
                token.ExpiresAt = DateTime.UtcNow.Add(IdentitySettings.RefreshTokenExpireTime);

                var entry = _context.RefreshTokens.Attach(token);

                entry.Property(x => x.ExpiresAt).IsModified = true;

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
    }
}
