using System.Threading.Tasks;

namespace IFTTT.Console.Interfaces.Services
{
    public interface IEmailAppletService
    {
        Task TriggerEventAsync(string hashtag);
    }
}
