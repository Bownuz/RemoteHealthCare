using HardwareClientApplication;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectionImplemented
{
    internal class ClientMain
    {
        static async Task Main(string[] args)
        {
            Thread connectionThread = new Thread(ServerConnection.HandleConnection);

            
            //ListDisplay.ShowDeviceList();

            //HeartRateMonitor heartRateMonitor = new HeartRateMonitor();
            //Ergometer ergometer = new Ergometer();

            //Console.Write("enter heart rate device: ");
            //var hrm = Console.ReadLine();
            //Console.Write("enter ergometer device:0 ");
            //var erg = Console.ReadLine();
            //await heartRateMonitor.ConnectToBLE_Device(hrm);
            //await ergometer.ConnectToBLE_Device(erg);
            //Console.ReadLine();
        }
    }
}
