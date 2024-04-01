using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace kurs.Services.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _fromAddress;
        private readonly string _username;
        private readonly string _password;

        public EmailSender(IConfiguration configuration)
        {
            _smtpServer = configuration["EmailSettings:SmtpServer"];
            _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            _fromAddress = configuration["EmailSettings:FromAddress"];
            _username = configuration["EmailSettings:SmtpUsername"];
            _password = configuration["EmailSettings:SmtpPassword"];
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(MailboxAddress.Parse(_fromAddress));
            emailMessage.To.Add(MailboxAddress.Parse(email));
            emailMessage.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = htmlMessage };
            emailMessage.Body = builder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.SslOnConnect);
            await client.AuthenticateAsync(_username, _password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}