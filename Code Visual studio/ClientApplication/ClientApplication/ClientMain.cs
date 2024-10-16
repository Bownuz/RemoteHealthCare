using ConnectionImplemented;
using HardwareClientApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApplication {
    internal static class ClientMain {
        [STAThread]
        static async Task Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new screen());

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
