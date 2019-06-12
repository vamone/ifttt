using System.Configuration;

namespace IFTTT.Console
{
    public class Config
    {
        public static Config _instance;

        public string SmtpHostName { get; protected set; }

        public string FromEmail { get; protected set; }

        public string FromEmailPassword { get; protected set; }

        public string ToEmail { get; protected set; }

        private Config()
        {
            this.SmtpHostName = ConfigurationManager.AppSettings.Get("SMTPHostName");
            this.FromEmail = ConfigurationManager.AppSettings.Get("FromEmail");
            this.FromEmailPassword = ConfigurationManager.AppSettings.Get("FromEmailPassword");
            this.ToEmail = ConfigurationManager.AppSettings.Get("ToEmail");
        }

        public static Config Instance
        {
            get
            {
                return _instance ?? (_instance = new Config());
            }
        }
    }
}
