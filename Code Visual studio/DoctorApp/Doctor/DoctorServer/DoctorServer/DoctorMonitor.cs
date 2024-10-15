using System;
using System.Net.Sockets;
using System.Text;

namespace DoctorApp {
    class DoctorMonitor {
        public void StartMonitoring(TcpClient doctorClient) {
            try {
                while(true) {      
                    var stream = doctorClient.GetStream();
                    byte[] data = new byte[1024];
                    int bytes = stream.Read(data, 0, data.Length);
                    string message = Encoding.ASCII.GetString(data, 0, bytes);

                    if(!string.IsNullOrEmpty(message)) {
                  
                        Console.WriteLine("Cliëntgegevens ontvangen: " + message);

                        // grafiek ??
                    }
                }
            }
            catch(Exception ex) {
                Console.WriteLine("Fout tijdens het monitoren: " + ex.Message);
            }
        }
    }
}
