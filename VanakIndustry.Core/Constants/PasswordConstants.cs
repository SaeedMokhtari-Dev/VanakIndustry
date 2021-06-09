namespace VanakIndustry.Core.Constants
{
    public static class PasswordConstants
    {
        public const string PasswordRegex = "^.*(?=.{8,})((?=.*[!@#$%^&*()\\-_=+{};:,<.>]){1})(?=.*\\d)((?=.*[a-z]){1})((?=.*[A-Z]){1}).*$";
    }
}