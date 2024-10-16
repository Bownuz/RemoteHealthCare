using System.Net.Http;
using System.Net.Sockets;
using Server.DataProtocol;
using Server.DataStorage;
using ServerTest2._1.Patterns.Observer;


namespace Server.ThreadHandlers
{
    internal class DoctorThread : Subject, Observer
    {
        FileStorage fileStorage;
        Person Person;
        TcpClient doctor;

        public DoctorThread(FileStorage fileStorage, TcpClient doctor)
        {
            this.fileStorage = fileStorage;
            this.doctor = doctor;
        }

        public void HandleThread()
        {
            Protocol protocol = new Protocol();
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
