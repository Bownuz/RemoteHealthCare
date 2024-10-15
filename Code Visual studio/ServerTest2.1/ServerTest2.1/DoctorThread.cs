using System.Net.Sockets;
using Server.DataProtocol;
using Server.DataStorage;
using Server.ObserverPattern;


namespace Server.Threads
{
    public class DoctorThread : Observer
    {
        public static void HandleDoctorThread(TcpClient doctor)
        {
            while (true)
            {
                String recived = DataProtocol.Messages.ReciveMessage(doctor);
                Console.WriteLine($"Doctor: {recived}");


            }


        }

        public void notify()
        {
            throw new NotImplementedException();
        }
    }
}
