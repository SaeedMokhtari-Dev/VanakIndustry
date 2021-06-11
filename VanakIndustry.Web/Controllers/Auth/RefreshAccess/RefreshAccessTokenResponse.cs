namespace VanakIndustry.Web.Controllers.Auth.RefreshAccess
{
    public class RefreshAccessTokenResponse
    {
        public bool IsLoginRequired { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
