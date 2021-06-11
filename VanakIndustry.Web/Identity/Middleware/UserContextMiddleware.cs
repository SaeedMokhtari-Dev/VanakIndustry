using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.Web.Identity.Contexts;

namespace VanakIndustry.Web.Identity.Middleware
{
   public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, UserContext userContext, VanakIndustryContext databaseContext)
        {
            if (httpContext.User.Identity?.IsAuthenticated ?? false)
            {
                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId != null && Int64.TryParse(userId, out long currentUserId))
                {
                    var context = await databaseContext.Users
                        .Include("Roles")
                        .Where(x => x.Id == currentUserId)
                        .Select(x => new UserContext
                        {
                            Id = x.Id,
                            Roles = x.Roles.ToList(),
                            IsActive = x.IsActive,
                            IsAuthenticated = true
                        })
                        .FirstOrDefaultAsync();

                    if (context == null || !context.IsActive)
                    {
                        httpContext.Response.StatusCode = 401;
                    }
                    else
                    {
                        userContext.Id = context.Id;
                        userContext.Roles = context.Roles;
                        userContext.IsActive = context.IsActive;
                        userContext.IsAuthenticated = context.IsAuthenticated;
                    }
                }
            }

            await _next(httpContext);
        }
    }
}
