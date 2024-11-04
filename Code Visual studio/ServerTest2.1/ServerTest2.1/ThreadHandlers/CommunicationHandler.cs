using Server.DataStorage;
using Server.Patterns.Observer;
using System.Net.Sockets;

namespace Server.ThreadHandlers {
    public abstract class CommunicationHandler : Observer {
        public TcpClient client;
        public NetworkStream networkStream;
        public readonly FileStorage fileStorage;

        protected CommunicationType communicationType;

        public CommunicationHandler(FileStorage fileStorage, NetworkStream networkStream) {
            this.fileStorage = fileStorage;
            this.networkStream = networkStream;
        }

        public async Task HandleThread() {
            DataProtocol protocol = new DataProtocol(communicationType, this);
            protocol.processInput("");
            while (client.Connected) {
                string receivedMessage;
                string response;


                if ((receivedMessage = MessageCommunication.ReceiveMessage(networkStream)) == null) {
                    client.Close();
                    continue;
                }

                Console.WriteLine(receivedMessage);


                await protocol.processInput(receivedMessage);
            }
        }
        public abstract void Update(CommunicationType communicationOrigin, Session session);
    }
}
