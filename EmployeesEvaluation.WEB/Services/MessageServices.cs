using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Logging;

namespace EmployeesEvaluation.WEB.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager
        private readonly ILogger _logger;

        public AuthMessageSender(IOptions<AuthMessageSenderOptions> optionsAccessor, ILogger<AuthMessageSender> logger)
        {
            Options = optionsAccessor.Value;
            this._logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            MailKitSend(subject, message, email);
            //Execute(Options.SendGridKey, subject, message, email).Wait();
            return Task.FromResult(0);
        }

        public void MailKitSend(string subject, string message, string email)
        {
            try
            {
                //Smtp Server 
                string SmtpServer = "smtp.gmail.com";
                //Smtp Port Number 
                int SmtpPortNumber = 587;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = message;
                bodyBuilder.TextBody = message;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress("CHI Emloyees Evaluation", "chiemployess@gmail.com"));
                mimeMessage.To.Add(new MailboxAddress(email, email));
                mimeMessage.Subject = subject;
                mimeMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {

                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    // Note: only needed if the SMTP server requires authentication 
                    // Error 5.5.1 Authentication  
                    client.Authenticate("chiemployess@gmail.com", "TN#william5");
                    client.Send(mimeMessage);
                    _logger.LogInformation("----------------- The mail has been sent successfully !! ---------------" );
                    client.Disconnect(true);

                }
            }
            catch(Exception e)
            {
                _logger.LogError("------------------- Something went wrong trying send the email -------------" + e);
                throw e;
            }
        }

        public async Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("mateusbaruffi@chinet.org", "Mateus Baruffi"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            var response = await client.SendEmailAsync(msg);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
