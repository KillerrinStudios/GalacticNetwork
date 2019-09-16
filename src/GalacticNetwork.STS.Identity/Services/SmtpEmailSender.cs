﻿using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using GalacticNetwork.STS.Identity.Configuration;

namespace GalacticNetwork.STS.Identity.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly SmtpConfiguration _configuration;
        private readonly SmtpClient _client;

        public SmtpEmailSender(SmtpConfiguration configuration, ILogger<EmailSender> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _client = new SmtpClient
            {
                Host = _configuration.Host,
                Port = _configuration.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = _configuration.UseSSL,
                Credentials = new System.Net.NetworkCredential(_configuration.Login, _configuration.Password)
            };
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogInformation($"Sending email: {email}, subject: {subject}, message: {htmlMessage}");
            try
            {
                var mail = new MailMessage(_configuration.Login, email);
                mail.IsBodyHtml = true;
                mail.Subject = subject;
                mail.Body = htmlMessage;
                _client.Send(mail);
                _logger.LogInformation($"Email: {email}, subject: {subject}, message: {htmlMessage} successfully sent");
                return Task.CompletedTask;
            }
            catch (SmtpException ex)
            {
                _logger.LogError($"Exception {ex} during sending email: {email}, subject: {subject}");
                throw;
            }
        }
    }
}
