using ConnectionImplemented;
using ClientApplication;
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
        private static SignInScreen userControl;

        [STAThread]
        static async Task Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            StartGui();
        }

        public static void StartGui() {
            Form mainForm = new Form();
            userControl = new SignInScreen(mainForm);

            userControl.Dock = DockStyle.Fill;

            mainForm.WindowState = FormWindowState.Maximized;
            mainForm.Controls.Add(userControl);
            mainForm.Text = "Client Application";
            mainForm.FormClosed += (sender, e) => Shutdown();

            Application.Run(mainForm);
        }

        private static void Shutdown() {
            userControl.CloseNetworkConnection();
            Task.Delay(100);
            Application.Exit();
        }
    }
}
