using System;
using System.Collections.Generic;
using MailKit.Security;

namespace VanakIndustry.Web.Configuration.Models
{
    public class SmtpOptions
    {
        private readonly Dictionary<string, SecureSocketOptions> _secureSocketOptionsMapping =
            new Dictionary<string, SecureSocketOptions>(StringComparer.OrdinalIgnoreCase)
            {
                { string.Empty, SecureSocketOptions.None },
                { "StarTls", SecureSocketOptions.StartTls },
                { "SSL", SecureSocketOptions.SslOnConnect }
            };

        public string Hostname { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string SecureSocketOption { get; set; }

        public string FromAddress { get; set; }

        public string FromName { get; set; }

        public SecureSocketOptions GetSecureSocketOption() => _secureSocketOptionsMapping[SecureSocketOption];
    }
}
