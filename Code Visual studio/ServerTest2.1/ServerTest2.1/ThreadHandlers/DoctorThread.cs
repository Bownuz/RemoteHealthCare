using System.Net.Http;
using System.Net.Sockets;
using Server.DataProtocol;
using Server.DataStorage;
using ServerTest2._1.Patterns.Observer;


namespace Server.ThreadHandlers
{
    internal class DoctorThread(FileStorage fileStorage, TcpClient doctor) : Subject, Observer
    {
        FileStorage fileStorage = fileStorage;
        Person Person;
        TcpClient doctor = doctor;

        public void HandleThread()
        {
            Protocol protocol = new Protocol();
            AddObserver(fileStorage);
            
            while (true)
            {
                if (doctor.Connected)
                {
                    string recievedMessage;
                    if ((recievedMessage = MessageCommunication.ReciveMessage(doctor)) != null)
                    {
                        protocol.processMessage(recievedMessage);
                        NotifyAll();
                    }
                }
            }


        }

        public void Update()
        {
            MessageCommunication.SendMessage(doctor, Person.currentSession.getLatestMessage(ClientType.DOCTOR));
        }
    }
}
