using ClientApplication.Vr;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApplication {
    internal static class ClientMain {
        private static SignInScreen userControl;

        [STAThread]
        static async Task Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            VrInit.StandardInit("127.0.0.1");
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
