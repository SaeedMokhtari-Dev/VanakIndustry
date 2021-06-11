namespace VanakIndustry.Web.Controllers.Auth.ChangePassword
{
    public class ChangePasswordRequest
    {
        public string Token { get; set; }
        
        public string NewPassword { get; set; }
        
        public string ConfirmPassword { get; set; }
    }
}
