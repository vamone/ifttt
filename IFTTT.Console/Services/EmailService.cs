using IFTTT.Console.Interfaces.Services;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IFTTT.Console.Services
{
    public class EmailService : IEmailService
    {
        public EmailService(string smtpHostName, string fromEmail, string fromPassword, string toEmail)
        {
            this.SmtpHostName = smtpHostName;
            this.FromEmail = fromEmail;
            this.FromPassword = fromPassword;
            this.ToEmail = toEmail;
        }

        private string SmtpHostName { get; set; }

        private string FromEmail { get; set; }

        private string FromPassword { get; set; }

        private string ToEmail { get; set; }

        public async Task SendAsync(MailMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.From = new MailAddress(this.FromEmail);
            message.To.Add(this.ToEmail);

            var client = new SmtpClient(this.SmtpHostName);
            client.Port = 587;
            client.Credentials = new NetworkCredential(this.FromEmail, this.FromPassword);
            client.EnableSsl = true;

            await client.SendMailAsync(message);
        }
    }
}
