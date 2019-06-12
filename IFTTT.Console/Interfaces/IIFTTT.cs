using IFTTT.Console.Interfaces.Services;

namespace IFTTT.Console.Interfaces
{
    public interface IIFTTT
    {
        IEmailAppletService EmailAppletService { get; }
    }
}