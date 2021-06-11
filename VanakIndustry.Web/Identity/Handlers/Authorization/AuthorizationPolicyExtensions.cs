using Microsoft.AspNetCore.Authorization;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Identity.Handlers.Authorization
{
    public static class AuthorizationPolicyExtensions
    {
        public static AuthorizationOptions RequireActiveUser(this AuthorizationOptions options)
        {
            options.AddPolicy(nameof(Policies.ActiveUser), x => x.Requirements.Add(new ActiveUserPolicyRequirement(true)));

            return options;
        }
    }
}
