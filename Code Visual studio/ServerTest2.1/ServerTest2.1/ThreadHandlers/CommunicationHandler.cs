using Server.DataStorage;
using Server.Patterns.Observer;
using System;
using System.Net.Sockets;

namespace Server.ThreadHandlers
{
	public abstract class CommunicationHandler(FileStorage fileStorage, TcpClient tcpClient) : Observer
    {
		protected TcpClient tcpClient = tcpClient;

		public readonly FileStorage fileStorage = fileStorage;

		protected CommunicationType communicationType;

        public void HandleThread()
		{
            DataProtocol protocol = new DataProtocol(communicationType, this);

            MessageCommunication.SendMessage(tcpClient, protocol.processInput(""));
            while (tcpClient.Connected)
            {
                string recievedMessage;
                String response;
                if ((recievedMessage = MessageCommunication.ReciveMessage(tcpClient)) != null)
                {
                    response = protocol.processInput(recievedMessage);
                    MessageCommunication.SendMessage(tcpClient, response);

                    if (response.Equals("Goodbye"))
                    {
                        tcpClient.Close();
                    }
                }
            }
        }

        public abstract void Update(CommunicationType communicationOrigin, Session session);
        
    }

}

