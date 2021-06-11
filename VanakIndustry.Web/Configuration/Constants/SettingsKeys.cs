namespace VanakIndustry.Web.Configuration.Constants
{
    public static class SettingsKeys
    {
        public const string AllowedCorsOrigins = "AllowedCorsOrigins";
        public const string ConnectionString = "ConnectionString";
        public const string BCrypt = "BCrypt";
        public const string Smtp = "Smtp";

        public static class Jwt
        {
            public const string SecurityKey = "SecurityKey";
            public const string Issuer = "Issuer";
            public const string Audience = "Audience";
        }

        public static class Logging
        {
            public const string LogLevel = "LogLevel";
            public const string KeepMaxLogs = "KeepMaxLogs";
            public const string ServerDirectory = "ServerDirectory";
            public const string ClientDirectory = "ClientDirectory";
        }

        public static class Identity
        {
            public const string SuperadminPassword = "SuperadminPassword";
            public const string SuperadminEmail = "SuperadminEmail";
        }

        public static class Kestrel
        {
            public const string Ip = "Ip";
            public const string Port = "Port";
        }
    }
}
