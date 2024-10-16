using Avans.TI.BLE;
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
            BleDevice[] bleDevices = { heartRateMonitor };

            DataHandler handler = new DataHandler(bleDevices);

            TcpClient client = new TcpClient("192.168.178.58", 4789); 
            Thread connectionThread = new Thread(() => ServerConnection.HandleConnection(client, handler));
            connectionThread.Start();
        }
    }
}

