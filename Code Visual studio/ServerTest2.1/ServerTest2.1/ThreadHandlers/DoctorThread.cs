using System.Net.Sockets;
using Server.DataProtocol;
using Server.DataStorage;
using Server.Patterns.Observer;


namespace Server.ThreadHandlers
{
    internal class DoctorThread(FileStorage fileStorage, TcpClient doctor) : Subject, Observer
    {
        FileStorage fileStorage = fileStorage;
        Person Person;
        TcpClient doctor = doctor;
        DataProtocol.DataProtocol protocol;

        public void HandleThread()
        {
            protocol = new DataProtocol.DataProtocol(ClientType.DOCTOR);
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
