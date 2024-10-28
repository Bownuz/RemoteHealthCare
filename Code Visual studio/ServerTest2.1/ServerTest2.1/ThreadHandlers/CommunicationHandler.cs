using Server.DataStorage;
using Server.Patterns.Observer;
using System.Net.Security;
using System.Net.Sockets;

namespace Server.ThreadHandlers {
    public abstract class CommunicationHandler : Observer {

        public NetworkStream networkStream;

        public readonly FileStorage fileStorage;

        protected CommunicationType communicationType;

        public CommunicationHandler(FileStorage fileStorage, NetworkStream networkStream) {
            this.fileStorage = fileStorage;
            this.networkStream = networkStream;
        }

        public void HandleThread() {
            DataProtocol protocol = new DataProtocol(communicationType, this);
            while (networkStream.CanRead) {
                string receivedMessage;
                string response;

                try {
                    if ((receivedMessage = MessageCommunication.ReceiveMessage(networkStream)) == null) {
                        continue;
                    }

                    protocol.processInput(receivedMessage);

                } catch (IOException ex) {
                    Console.WriteLine(ex.Message);
                    networkStream.Close();
                }
            }
        }
        public abstract void Update(CommunicationType communicationOrigin, Session session);
    }
}
