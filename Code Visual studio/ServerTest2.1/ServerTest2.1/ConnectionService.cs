using Server.DataStorage;
using Server.ThreadHandlers;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace Server {
    public class ConnectionService {
        static void Main(string[] args) {
            List<Thread> threads = new List<Thread>();
            TcpListener PatientListner = new TcpListener(IPAddress.Any, 4789);
            TcpListener DoctorListner = new TcpListener(IPAddress.Any, 4790);
            PatientListner.Start();
            DoctorListner.Start();

            FileStorage fileStorage = new FileStorage();

            Console.WriteLine("Starting up server and waiting for connections.....");

            while (true) {
                if (PatientListner.Pending()) {
                    TcpClient client = PatientListner.AcceptTcpClient();
                    Thread clientThread = new Thread(() => HandleClient(client, fileStorage));
                    threads.Add(clientThread);
                    clientThread.Start();
                }
                if (DoctorListner.Pending()) {
                    TcpClient doctor = DoctorListner.AcceptTcpClient();
                    Thread doctorThread = new Thread(() => HandleDoctor(doctor, fileStorage));
                    threads.Add(doctorThread);
                    doctorThread.Start();
                }
            }
        }

        static void HandleClient(TcpClient client, FileStorage fileStorage) {
            // this should convert tcpclient to an ssl stream


            PatientHandler clientThread = new PatientHandler(fileStorage, sslStream);
            clientThread.HandleThread();

        }

        static void HandleDoctor(TcpClient doctor, FileStorage fileStorage) {
            try {
                SslStream sslStream = new SslStream(doctor.GetStream(), false);

                X509Certificate2 serverCertificate = new X509Certificate2("C:\\Users\\mlahl\\source\\repos\\Project\\Code Visual studio\\certificate", "groepa4");

                sslStream.AuthenticateAsServer(serverCertificate, clientCertificateRequired: false, checkCertificateRevocation: false);

                DoctorHandler doctorThread = new DoctorHandler(fileStorage, sslStream);
                doctorThread.HandleThread();
            } catch (Exception ex) {
                Console.WriteLine("Error establishing SSL connection: " + ex.Message);
            }
        }
    }
}
