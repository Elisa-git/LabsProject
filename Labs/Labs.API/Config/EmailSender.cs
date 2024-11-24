using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Labs.API.Config
{
    public class EmailSender : IEmailSender
    {
        private readonly ISendGridClient sendGridClient;
        private readonly ILogger<EmailSender> logger;

        public EmailSender(ISendGridClient sendGridClient, ILogger<EmailSender> logger)
        {
            this.sendGridClient = sendGridClient;
            this.logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("eulisamesquita@gmail.com", "Labs"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            var response = await sendGridClient.SendEmailAsync(msg);
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation("Email queued successfully");
            }
            else
            {
                logger.LogError("Failed to send email");
            }

        }
    }
}
