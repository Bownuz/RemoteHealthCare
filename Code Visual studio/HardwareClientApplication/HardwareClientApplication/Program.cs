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

            Console.Write("enter heart rate device: ");
            var hrm = Console.ReadLine();
            Console.Write("enter ergometer device: ");
            var erg = Console.ReadLine();
            await heartRateMonitor.ConnectToBLE_Device(hrm);
            await ergometer.ConnectToBLE_Device(erg);
            Console.ReadLine();
        }
    }
}
