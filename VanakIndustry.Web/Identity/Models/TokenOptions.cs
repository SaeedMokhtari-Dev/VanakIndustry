using System;
using Microsoft.IdentityModel.Tokens;

namespace VanakIndustry.Web.Identity.Models
{
    public class TokenOptions
    {
        public TokenValidationParameters TokenValidationParameters { get; set;  }

        public TokenValidationParameters RefreshAccessTokenValidationParameters { get; set; }

        public TimeSpan AccessTokenExpireTime { get; set; }

        public TimeSpan DownloadTokenExpireTime { get; set; }

        public TimeSpan RefreshTokenExpireTime { get; set; }

        public string Issuer => TokenValidationParameters?.ValidIssuer;

        public string Audience => TokenValidationParameters?.ValidAudience;

        public SecurityKey SecurityKey => TokenValidationParameters?.IssuerSigningKey;
    }
}
