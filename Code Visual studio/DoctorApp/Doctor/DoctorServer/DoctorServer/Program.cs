using System;
using System.Net.Sockets; 
using DoctorApp; 

class Program {
    static void Main(string[] args) {
        TcpClient doctorClient = new TcpClient("127.0.0.1", 4790); 
        DoctorLogin login = new DoctorLogin();

        if(login.Login(doctorClient)) {
            Console.WriteLine("Inloggen succesvol! Beginnen met monitoren...");

            DoctorMonitor monitor = new DoctorMonitor();
            monitor.StartMonitoring(doctorClient);
        }
        else {
            Console.WriteLine("Inloggen mislukt!");
        }
    }
}
