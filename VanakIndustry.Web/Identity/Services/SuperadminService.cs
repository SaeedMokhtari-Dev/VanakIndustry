using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NLog;
using VanakIndustry.Core.Constants;
using VanakIndustry.Core.Enums;
using VanakIndustry.Core.Interfaces;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.DataAccess.Entities;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Identity.Services
{
    public class SuperadminService: IScoped
    {
        private readonly VanakIndustryContext _context;
        private readonly IConfiguration _configuration;
        private readonly PasswordService _passwordService;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public SuperadminService(VanakIndustryContext context, IConfiguration configuration, PasswordService passwordService)
        {
            _context = context;
            _configuration = configuration;
            _passwordService = passwordService;
        }

        public async Task CreateSuperadminIfRequired()
        {
            var exists = await IsSuperadminPresent();

            if (exists) return;

            await _context.ExecuteTransactionAsync(async () =>
            {
                var user = GetSuperadminUser();

                _context.Entry(user).State = EntityState.Added;

                await _context.SaveChangesAsync();

                var role = new UserRole
                {
                    UserId = user.Id,
                    Role = RoleType.Admin
                };

                _context.Entry(role).State = EntityState.Added;

                await _context.SaveChangesAsync();
            });
        }

        private User GetSuperadminUser()
        {
            var section = _configuration.GetSection(nameof(SettingsKeys.Identity));

            var password = section.GetValue<string>(SettingsKeys.Identity.SuperadminPassword);
            var email = section.GetValue<string>(SettingsKeys.Identity.SuperadminEmail);

            /*var user = new ApiMessages.User
            {
                Email = email,
                Password = _passwordService.GetPasswordHash(password),
                FirstName = "Super",
                LastName = "Admin",
                IsActive = true,
                PasswordChangeRequired = true,
                IsAccountActivated = true
            };*/

            return new User();
        }

        public Task<bool> IsSuperadminPresent()
        {
            try
            {
                return
                    Task.FromResult(true); //await _context.Users.Include(x => x.Roles).AnyAsync(x => x.Roles.Any(y => y.Role == RoleType.Superadmin));
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }
    }
}
