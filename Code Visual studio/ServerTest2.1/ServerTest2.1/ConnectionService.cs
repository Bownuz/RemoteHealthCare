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
            NetworkStream networkStream = client.GetStream();   
            PatientHandler clientThread = new PatientHandler(fileStorage, networkStream);
            clientThread.HandleThread();

        }

        static void HandleDoctor(TcpClient doctor, FileStorage fileStorage) 
            {
            try {
                NetworkStream networkStream = doctor.GetStream();
                DoctorHandler doctorThread = new DoctorHandler(fileStorage, networkStream);
                doctorThread.HandleThread();
            } catch (Exception ex) {
                Console.WriteLine("Error establishing SSL connection: " + ex.Message);
            }
        }
    }
}
