namespace VanakIndustry.Core.Constants
{
    public static class ApiMessages
    {
        public const string ResourceNotFound = "Api.Resource.Not.Found";
        public const string InvalidRequest = "Api.Invalid.Request";
        public const string GenericError = "Api.Generic.Error";
        public const string Forbidden = "Api.Forbidden";
        public const string PageSize = "Api.PageSize";
        public const string PageIndex = "Api.PageIndex";
        public const string DuplicateUserName = "Api.Username.Duplicate";
        public const string DuplicateNationalId = "Api.NationalId.Duplicate";
        public const string DuplicateBarcode = "Api.Barcode.Duplicate";
        public const string DuplicateEmail = "Api.Email.Duplicate";
        public const string NotEnoughBalance = "Api.NotEnoughBalance";
        public const string MinPasswordLengthError = "Api.MinPasswordLengthError";
        public const string IdRequired = "Api.Validate.IdRequired";

        public static class Auth
        {
            public const string EmailRequired = "Api.Auth.Email.Required";
            public const string UsernameRequired = "Api.Auth.Username.Required";
            public const string PasswordRequired = "Api.Auth.Password.Required";
            public const string RoleTypeRequired = "Api.Auth.RoleType.Required";
            public const string TokenRequired = "Api.Auth.Token.Required";
            public const string CurrentPasswordRequired = "Api.Auth.CurrentPassword.Required";
            public const string InvalidCredentials = "Api.Auth.Invalid.Credentials";
            public const string ResetPasswordResponse = "Api.Auth.ResetPassword.Response";
            
            public const string ValidateResetPasswordTokenInvalidToken = "Api.Auth.ValidateResetPasswordToken.InvalidToken";
            public const string ValidateResetPasswordTokenValidToken = "Api.Auth.ValidateResetPasswordToken.ValidToken";
            
            public const string ChangePasswordNotEqualsPasswords = "Api.Auth.ChangePassword.NotEqualPasswords";
            public const string ChangePasswordCurrentPasswordIsNotCorrect = "Api.Auth.ChangePassword.CurrentIsNotCorrect";
            public const string ChangePasswordSuccessful = "Api.Auth.ChangePassword.Successful";

            public const string MinPasswordLengthError = "Api.Auth.MinPasswordLengthError";
        }
        public static class UserMessage
        {
            public const string UserIdRequired = "Api.User.UserId.Required";
            public const string FirstNameRequired = "Api.User.FirstName.Required";    
            public const string LastNameRequired = "Api.User.LastName.Required";    
            public const string EmailRequired = "Api.User.Email.Required";    
            public const string FaxRequired = "Api.User.Fax.Required";    
            public const string BarcodeRequired = "Api.User.Barcode.Required";    
            public const string MinPasswordLengthError = "Api.User.Password.MinPasswordLengthError";
            
            public const string RegisteredSuccessfully = "Api.User.Register.Successful";
            public const string AddedSuccessfully = "Api.User.Add.Successful";
            public const string EditedSuccessfully = "Api.User.Edit.Successful";
            public const string ArchivedSuccessfully = "Api.User.Archived.Successful";
            public const string ActivatedSuccessfully = "Api.User.Activated.Successful";
            public const string PresentedSuccessfully = "Api.User.Presented.Successful";
            public const string DeletedSuccessfully = "Api.User.Deleted.Successful";
            
            public const string UserNotFound = "Api.User.NotFound";
            public const string UserWasPresentedBefore = "Api.User.PresentedBefore";
        }

        public static class ElectionCandidateTypeMessage
        {
            public const string TitleRequired = "Api.ElectionCandidateType.Title.Required";

            public const string AddedSuccessfully = "Api.ElectionCandidateType.Add.Successful";
            public const string EditedSuccessfully = "Api.ElectionCandidateType.Edit.Successful";
            public const string DeletedSuccessfully = "Api.ElectionCandidateType.Deleted.Successful";
        }

        public static class SelectElectionCandidateMessage
        {
            public const string AddedSuccessfully = "Api.SelectElectionCandidate.Add.Successful";
            public const string EditedSuccessfully = "Api.SelectElectionCandidate.Edit.Successful";
            public const string DeletedSuccessfully = "Api.SelectElectionCandidate.Deleted.Successful";
            
            public const string MoreThanLimit = "Api.SelectElectionCandidate.MoreThanLimit.NotAllowed";
        }
        public static class ElectionMessage
        {
            public const string TitleRequired = "Api.Election.Title.Required";    
            public const string ElectionCandidateTypeIdRequired = "Api.Election.ElectionCandidateTypeId.Required";    
            
            public const string AddedSuccessfully = "Api.Election.Add.Successful";
            public const string EditedSuccessfully = "Api.Election.Edit.Successful";
            public const string DeletedSuccessfully = "Api.Election.Deleted.Successful";
            public const string CandidateAddedSuccessfully = "Api.Election.CandidateAdded.Successful";
            
            public const string EndDateShouldGreaterThanStartDate = "Api.Election.EndDateShouldGreaterThanStartDate";
            public const string DontFindCurrentElection = "Api.Election.DontFindCurrentElection";
            public const string UserIsNotPresent = "Api.Election.UserIsNotPresent";
        }
    }
}
