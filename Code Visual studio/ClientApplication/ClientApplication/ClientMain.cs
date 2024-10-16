using ConnectionImplemented;
using HardwareClientApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace ClientApplication {
    internal static class ClientMain {
        [STAThread]
        static async Task Main(string[] args) {
            HeartRateMonitor heartRateMonitor = new HeartRateMonitor();
            Ergometer ergometer = new Ergometer();
            BleDevice[] bleDevices = { ergometer, heartRateMonitor };

            DataHandler handler = new DataHandler(bleDevices);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            StartGui(handler);

            //TcpClient client = new TcpClient("192.168.178.101", 4789);
            TcpClient client = new TcpClient("localhost", 4789);
            Thread connectionThread = new Thread(() => ServerConnection.HandleConnection(client, handler, ergometer));
            connectionThread.Start();
        }

        public static void StartGui(DataHandler handler) {
            Form mainForm = new Form();
            SignInScreen userControl = new SignInScreen(handler, mainForm);
            // Zorgt ervoor dat de UserControl het hele form vult
            userControl.Dock = DockStyle.Fill;

            mainForm.WindowState = FormWindowState.Maximized;
            mainForm.Controls.Add(userControl);
            mainForm.Text = "Client Application";
            Application.Run(mainForm);
        }
    }
}
