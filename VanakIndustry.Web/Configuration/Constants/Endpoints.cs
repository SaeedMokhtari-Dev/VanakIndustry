namespace VanakIndustry.Web.Configuration.Constants
{
    public static class Endpoints
    {
        /*Auth APIs*/
        public const string ApiAuthLogin = "/api/auth/login";
        public const string ApiAuthResetPassword = "/api/auth/reset-password";
        public const string ApiAuthValidateResetPasswordToken = "/api/auth/validate-reset-password-token";
        public const string ApiAuthChangePassword = "/api/auth/change-password";
        public const string ApiAuthLogout = "/api/auth/logout";
        public const string ApiAuthCheck = "/api/auth/check";
        public const string ApiAuthRefreshAccessToken = "/api/auth/refresh-access-token";
        public const string ApiUserInfo = "/api/user/info";
        
        /*AuditingCompany APIs*/
        public const string ApiAuditingCompanyAdd = "/api/auditing-company/add";
        public const string ApiAuditingCompanyEdit = "/api/auditing-company/edit";
        public const string ApiAuditingCompanyArchive = "/api/auditing-company/archive";
        public const string ApiAuditingCompanyGet = "/api/auditing-company/get";
        public const string ApiAuditingCompanyDetail = "/api/auditing-company/detail";
        
        /*admin APIs*/
        public const string ApiUserGet = "/api/user/get";
        public const string ApiUserAdd = "/api/user/add";
        public const string ApiUserEdit = "/api/user/edit";
        public const string ApiUserDetail = "/api/user/detail";
        

        public const string ApiLog = "/api/log";
        public const string Swagger = "/swagger/v1/swagger.json";
    }
}
