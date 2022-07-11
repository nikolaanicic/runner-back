using Contracts.Configuration.Email;
using Contracts.Dtos.Email;
using Contracts.Services;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;

namespace Services.EmailService
{
    public class EmailManager : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailManager(EmailConfiguration config)
        {
            _emailConfig = config;
        }



        public async Task SendEmail(Message message)
        {
            await Send(CreateEmailMessage(message));
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email",_emailConfig.From));
            emailMessage.To.Add(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private async Task Send(MimeMessage message)
        {
            using var client = new SmtpClient();

            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.Username, _emailConfig.Password);

                await client.SendAsync(message);
            }catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }

        }
    }
}
