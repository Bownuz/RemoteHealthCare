using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorApplication {
    internal static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            StartGui();
        }
        public static void StartGui() {
            Form mainForm = new Form();
            Form1 initialWindow = new Form1(mainForm);

            initialWindow.Dock = DockStyle.Fill;

            mainForm.WindowState = FormWindowState.Maximized;
            mainForm.Controls.Add(initialWindow);
            mainForm.Text = "DoctorApplication";
            Application.Run(mainForm);
        }
    }
}
