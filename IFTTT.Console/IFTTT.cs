using IFTTT.Console.Applets;
using IFTTT.Console.Interfaces;
using IFTTT.Console.Interfaces.Services;
using IFTTT.Console.Services;

namespace IFTTT.Console
{
    public class IFTTT : IIFTTT
    {
        public IFTTT()
        {
            var emailService = new EmailService(Config.Instance.SmtpHostName, Config.Instance.FromEmail, Config.Instance.FromEmailPassword, Config.Instance.ToEmail);

            this.EmailAppletService = new EmailAppletService(emailService);
        }

        public IEmailAppletService EmailAppletService { get; protected set; }
    }
}
