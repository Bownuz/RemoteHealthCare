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
        [STAThread]
        static async Task Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            StartGui();
        }

        public static void StartGui() {
            Form mainForm = new Form();
            SignInScreen userControl = new SignInScreen(mainForm);

            userControl.Dock = DockStyle.Fill;

            mainForm.WindowState = FormWindowState.Maximized;
            mainForm.Controls.Add(userControl);
            mainForm.Text = "Client Application";
            Application.Run(mainForm);
        }
    }
}
