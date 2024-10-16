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

            //HeartRateMonitor heartRateMonitor = new HeartRateMonitor();
            Ergometer ergometer = new Ergometer();
            BleDevice[] bleDevices = { ergometer };

            DataHandler handler = new DataHandler(bleDevices);

            //TcpClient client = new TcpClient("192.168.178.101", 4789);
            TcpClient client = new TcpClient("localhost", 4789);
            Thread connectionThread = new Thread(() => ServerConnection.HandleConnection(client, handler, ergometer));
            connectionThread.Start();
        }
    }
}
