using System.Net.Sockets;
using Server.DataProtocol;
using Server.DataStorage;
using Server.ObserverPattern;


namespace Server.ThreadHandlers
{
    public class DoctorThread : Observer
    {

        public void HandleThread(TcpClient doctor)
        {
            while (true)
            {
                string recived = Messages.ReciveMessage(doctor);
                Console.WriteLine($"Doctor: {recived}");


            }


        }

        public void Notify()
        {
            
        }
    }
}
