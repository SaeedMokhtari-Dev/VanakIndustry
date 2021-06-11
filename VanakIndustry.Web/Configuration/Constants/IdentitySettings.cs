using System;

namespace VanakIndustry.Web.Configuration.Constants
{
    public static class IdentitySettings
    {
        public static readonly TimeSpan AccessTokenExpireTime = TimeSpan.FromMinutes(30);
        public static readonly TimeSpan RefreshTokenExpireTime = TimeSpan.FromDays(365);
        public static readonly TimeSpan AccessTokenValidationClockSkew = TimeSpan.FromMinutes(5);

        public const int MinPasswordLength = 8;
    }
}
