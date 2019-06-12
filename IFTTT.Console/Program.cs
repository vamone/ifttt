using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IFTTT.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ifttt = new IFTTT();

            RunPowerSocketMonitoringTask(ifttt);

            System.Console.ReadLine();
        }

        private static int ConvertToMiliseconds(int seconds)
        {
            if(seconds <= 0)
            {
                return 0;
            }

            return (int)TimeSpan.FromSeconds(seconds).TotalMilliseconds;
        }

        private static void RunPowerSocketMonitoringTask(IFTTT ifttt)
        {
            var task = Task.Run(async () =>
            {
                try
                {
                    var powerStatus = SystemInformation.PowerStatus;

                    System.Console.WriteLine($"Battery {Math.Round(powerStatus.BatteryLifePercent * 100, 2)}% remaining. Powerline status: {powerStatus.PowerLineStatus}. Timestamp: {DateTime.UtcNow}");

                    bool isCharging = powerStatus.PowerLineStatus == PowerLineStatus.Online;
                    bool isHighPeak = powerStatus.BatteryLifePercent >= 0.99;
                    bool isLowPeak = powerStatus.BatteryLifePercent <= 0.06;

                    if (isHighPeak && isCharging)
                    {
                        await ifttt.EmailAppletService.TriggerEventAsync("#powerfull");
                        System.Console.WriteLine("Turned off charging.");
                    }

                    if (isLowPeak && !isCharging)
                    {
                        await ifttt.EmailAppletService.TriggerEventAsync("#powerlow");
                        System.Console.WriteLine("Turned on charging.");
                    }

                    int sleep = powerStatus.BatteryLifePercent > 0.10 ? ConvertToMiliseconds(60 * 5) : ConvertToMiliseconds(3);
                    await Task.Delay(sleep);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }
            });

            task.Wait();

            if (task.IsCompleted)
            {
                RunPowerSocketMonitoringTask(ifttt);
            }
        }
    }
}