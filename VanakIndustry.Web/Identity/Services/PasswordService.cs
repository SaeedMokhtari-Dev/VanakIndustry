using System;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VanakIndustry.Core.Interfaces;
using VanakIndustry.Web.Configuration.Models;

namespace VanakIndustry.Web.Identity.Services
{
    public class PasswordService: ISingleton
    {
        private readonly BCryptOptions _options;
        private readonly ILogger<PasswordService> _logger;

        public PasswordService(IOptions<BCryptOptions> options, ILogger<PasswordService> logger)
        {
            _logger = logger;
            _options = options.Value;
        }

        public string GetPasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, _options.WorkFactor, _options.HashType);
        }

        public bool IsPasswordValid(string password, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordHash)) return false;

            try
            {
                return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash, _options.HashType);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return false;
        }

        public string GenerateRandomPassword(int length)
        {
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";

            var res = new StringBuilder();

            var rnd = new Random();

            while (0 < length--)
            {
                res.Append(characters[rnd.Next(characters.Length)]);
            }

            return res.ToString();
        }
    }
}
