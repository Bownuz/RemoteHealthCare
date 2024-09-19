using System;
using System.Threading.Tasks;

namespace ConnectionImplemented
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ListDisplay.ShowDeviceList();

            HeartRateMonitor heartRateMonitor = new HeartRateMonitor();
            Ergometer ergometer = new Ergometer();


            await heartRateMonitor.ConnectToBLE_Device();
            await ergometer.ConnectToBLE_Device();

        }
    }
}
