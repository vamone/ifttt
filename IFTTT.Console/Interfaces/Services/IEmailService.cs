using System.Net.Mail;
using System.Threading.Tasks;

namespace IFTTT.Console.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendAsync(MailMessage message);
    }
}
