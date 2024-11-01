using System;
using System.Net.Sockets;
using System.Windows.Forms;

namespace DoctorApplication {
    internal static class Program {
        public static TcpClient client = new TcpClient("192.168.178.58", 4790);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            NetworkStream networkStream = client.GetStream();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(networkStream));
        }
    }
}
