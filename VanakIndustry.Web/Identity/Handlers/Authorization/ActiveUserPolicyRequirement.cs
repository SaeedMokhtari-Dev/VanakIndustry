using Microsoft.AspNetCore.Authorization;

namespace VanakIndustry.Web.Identity.Handlers.Authorization
{
    public class ActiveUserPolicyRequirement : IAuthorizationRequirement
    {
        public bool IsActive { get; }

        public ActiveUserPolicyRequirement(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
