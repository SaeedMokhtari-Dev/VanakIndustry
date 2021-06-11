namespace VanakIndustry.Web.Identity.Models
{
    public class AccessTokenValidationResult
    {
        public bool IsValid { get; private set; }

        public bool IsExpired { get; private set; }

        public long UserId { get; private set; }

        private AccessTokenValidationResult() { }

        public static AccessTokenValidationResult Valid(long userId)
        {
            return new AccessTokenValidationResult
            {
                IsValid = true,
                IsExpired = false,
                UserId = userId
            };
        }

        public static AccessTokenValidationResult Invalid()
        {
            return new AccessTokenValidationResult
            {
                IsValid = false
            };
        }

        public static AccessTokenValidationResult Expired(long userId)
        {
            return new AccessTokenValidationResult
            {
                IsValid = true,
                IsExpired = true,
                UserId = userId
            };
        }
    }
}
