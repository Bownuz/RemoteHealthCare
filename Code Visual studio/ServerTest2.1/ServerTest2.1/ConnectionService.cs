using Server.DataStorage;
using Server.ThreadHandlers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
	public class ConnectionService
	{
        static void Main(string[] args)
        {
            //server
            List<Thread> threads = new List<Thread>();
            TcpListener Clientlistener = new TcpListener(IPAddress.Any, 4789);
            TcpListener DoctorListener = new TcpListener(IPAddress.Any, 4790);
            Clientlistener.Start();
            DoctorListener.Start();

            FileStorage fileStorage = new FileStorage();

            Console.WriteLine("Starting up server and waiting for connections.....");

            while (true)
            {
                if (Clientlistener.Pending())
                {
                    TcpClient client = Clientlistener.AcceptTcpClient();
                    Thread clientThread = new Thread(() => HandleClient(client, fileStorage));
                    threads.Add(clientThread);
                    clientThread.Start();
                }
                if (DoctorListener.Pending())
                {
                    TcpClient doctor = DoctorListener.AcceptTcpClient();
                    Thread doctorThread = new Thread(() => HandleDoctor(doctor, fileStorage));
                    threads.Add(doctorThread);
                    doctorThread.Start();
                }
            }
        }

        static void HandleClient(TcpClient client, FileStorage fileStorage)
        {
            PatientHandler clientThread = new PatientHandler(fileStorage, client);
            clientThread.HandleThread();
        }

        static void HandleDoctor(TcpClient doctor, FileStorage fileStorage)
        {
            DoctorHandler doctorThread = new DoctorHandler(fileStorage, doctor);
            doctorThread.HandleThread();
        }


    }
}

