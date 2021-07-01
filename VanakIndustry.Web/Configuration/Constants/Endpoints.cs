namespace VanakIndustry.Web.Configuration.Constants
{
    public static class Endpoints
    {
        /*Auth APIs*/
        public const string ApiAuthRegister = "/api/auth/register";
        public const string ApiAuthLogin = "/api/auth/login";
        public const string ApiAuthResetPassword = "/api/auth/reset-password";
        public const string ApiAuthValidateResetPasswordToken = "/api/auth/validate-reset-password-token";
        public const string ApiAuthChangePassword = "/api/auth/change-password";
        public const string ApiAuthChangeUserPassword = "/api/auth/change-user-password";
        public const string ApiAuthLogout = "/api/auth/logout";
        public const string ApiAuthCheck = "/api/auth/check";
        public const string ApiAuthRefreshAccessToken = "/api/auth/refresh-access-token";
        public const string ApiUserInfo = "/api/user/info";
        
        /*admin APIs*/

        #region  Users

        public const string ApiUserGet = "/api/user/get";
        public const string ApiUserAdd = "/api/user/add";
        public const string ApiUserEdit = "/api/user/edit";
        public const string ApiUserDetail = "/api/user/detail";
        public const string ApiUserDelete = "/api/user/delete";
        public const string ApiUserList = "/api/user/list";
        public const string ApiUserActive = "/api/user/active";
        public const string ApiUserPresent = "/api/user/present";

        #endregion
        
        #region  ElectionCandidateTypes

        public const string ApiElectionCandidateTypeGet = "/api/election-candidate-type/get";
        public const string ApiElectionCandidateTypeAdd = "/api/election-candidate-type/add";
        public const string ApiElectionCandidateTypeEdit = "/api/election-candidate-type/edit";
        public const string ApiElectionCandidateTypeDetail = "/api/election-candidate-type/detail";
        public const string ApiElectionCandidateTypeDelete = "/api/election-candidate-type/delete";
        public const string ApiElectionCandidateTypeList = "/api/election-candidate-type/list";

        #endregion
        
        #region  Election

        public const string ApiElectionGet = "/api/election/get";
        public const string ApiElectionAdd = "/api/election/add";
        public const string ApiElectionEdit = "/api/election/edit";
        public const string ApiElectionDetail = "/api/election/detail";
        public const string ApiElectionDelete = "/api/election/delete";
        public const string ApiElectionAddCandidate = "/api/election/add-candidate";
        public const string ApiElectionList = "/api/election/list";

        #endregion
        
        
        #region  ElectionCandidate

        public const string ApiElectionCandidateGet = "/api/election-candidate/get";

        #endregion
        
        #region  SelectElectionCandidate

        public const string ApiSelectElectionCandidateGet = "/api/select-election-candidate/get";
        public const string ApiSelectElectionCandidateAdd = "/api/select-election-candidate/add";

        #endregion
        
        #region  Attachment

        public const string ApiAttachmentDetail = "/api/attachment/detail";

        #endregion
        
        

        public const string ApiLog = "/api/log";
        public const string Swagger = "/swagger/v1/swagger.json";
    }
}
