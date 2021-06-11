using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using VanakIndustry.Core.Constants;
using VanakIndustry.Core.Interfaces;
using VanakIndustry.DataAccess.Entities;
using VanakIndustry.Web.Configuration.Models;

namespace VanakIndustry.Web.Services
{
    public class EmailService: ITransient
    {
        private readonly SmtpOptions _smtpOptions;
        private readonly IConfiguration _configuration;

        public EmailService(IOptions<SmtpOptions> smtpOptions, IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpOptions = smtpOptions.Value;
        }

        public async Task SendMail(string receiverEmail, string subject, string htmlBody, string name)
        {
            var message = new MimeMessage();
            var bodyBuilder = new BodyBuilder();
            
            // from
            message.From.Add(new MailboxAddress(_smtpOptions.FromName, _smtpOptions.FromAddress));
            // to
            message.To.Add(new MailboxAddress(name, receiverEmail));
            
            message.Subject = subject;
            bodyBuilder.HtmlBody = htmlBody;
            message.Body = bodyBuilder.ToMessageBody();

            var client = new SmtpClient
            {
                ServerCertificateValidationCallback = (s, c, h, e) => true
            };

            await client.ConnectAsync(_smtpOptions.Hostname, _smtpOptions.Port, _smtpOptions.GetSecureSocketOption());
            await client.AuthenticateAsync(_smtpOptions.Username, _smtpOptions.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        public async Task SendResetPasswordEmail(User user, Guid token)
        {
            var clientBaseUrl = _configuration.GetValue<string>("ClientBaseUrl");
            string htmlBody = $"<a href='{clientBaseUrl}/auth/change-password/{token}'>click here to reset password</a>";
            await SendMail(user.Email, "Reset Password", htmlBody, $"{user.FirstName} {user.LastName}");
        }
        public async Task SendUserActivationEmail(User user)
        {
            var clientBaseUrl = _configuration.GetValue<string>("ClientBaseUrl");
            string htmlBody = $"<a href='{clientBaseUrl}/auth/change-password/{user.PasswordResetToken.Token}'>click here to activate</a>";
            await SendMail(user.Email, "Activate Account", htmlBody, $"{user.FirstName} {user.LastName}");
        }
    }
}
