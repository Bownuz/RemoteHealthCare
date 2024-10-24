using Server.DataStorage;
using Server.Patterns.Observer;
using System;
using System.IO;
using System.Net.Security;

namespace Server.ThreadHandlers {
    public abstract class CommunicationHandler : Observer {
      
        protected readonly SslStream sslStream;

        protected readonly FileStorage fileStorage;

        protected CommunicationType communicationType;

        public CommunicationHandler(FileStorage fileStorage, SslStream sslStream) {
            this.fileStorage = fileStorage;
            this.sslStream = sslStream;
        }

        public void HandleThread() {
            DataProtocol protocol = new DataProtocol(communicationType, this);

            MessageCommunication.SendMessage(sslStream, protocol.processInput(""));

            while(sslStream.CanRead) {
                string receivedMessage;
                string response;

                try {
                    if((receivedMessage = MessageCommunication.ReceiveMessage(sslStream)) == null) {
                        continue;
                    }

                    response = protocol.processInput(receivedMessage);
                    MessageCommunication.SendMessage(sslStream, response);

                    if(response.Equals("Goodbye")) {
                        sslStream.Close();
                    }
                }
                catch(IOException ex) {
                    Console.WriteLine(ex.Message);
                    sslStream.Close();
                }
            }
        }
        public abstract void Update(CommunicationType communicationOrigin, Session session);
    }
}
