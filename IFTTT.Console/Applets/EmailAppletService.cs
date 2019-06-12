using IFTTT.Console.Interfaces.Services;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IFTTT.Console.Applets
{
    public class EmailAppletService : IEmailAppletService
    {
        private readonly IEmailService _emailService;

        public EmailAppletService(IEmailService emailService)
        {
            this._emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public async Task TriggerEventAsync(string hashtag)
        {
            if (string.IsNullOrWhiteSpace(hashtag))
            {
                throw new ArgumentNullException(nameof(hashtag));
            }

            var message = new MailMessage
            {
                Subject = hashtag,
                Body = string.Empty
            };

            await this._emailService.SendAsync(message);
        }
    }
}