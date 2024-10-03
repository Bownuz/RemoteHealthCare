using HardwareClientApplication;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectionImplemented {
    internal class ClientMain {

        static async Task Main(string[] args) {
            ListDisplay.ShowDeviceList();

            HeartRateMonitor heartRateMonitor = new HeartRateMonitor();
            Ergometer ergometer = new Ergometer();
            BleDevice[] bleDevices = { heartRateMonitor};

            DataHandler handler = new DataHandler(bleDevices);

            TcpClient client = new TcpClient("145.49.11.240", 4789);
            Thread connectionThread = new Thread(() => ServerConnection.HandleConnection(client, handler));
            connectionThread.Start();

            while (true) {
                await Task.Delay(1000);

                Console.WriteLine(handler.printAsJson());
            
            }
        }
    }
}
