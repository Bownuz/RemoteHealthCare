

using Server.ObserverPattern;

namespace Server.DataStorage
{
    class Session : Observer
    {
        List<string> messagesSend = new List<string>();
        List<string> messagesRecieved = new List<string>();

        public void addMessagesSend(string message)
        {
            messagesSend.Add(message);

        }

        public void addMessagesRecived(string message)
        {
            messagesRecieved.Add(message);
        }

        public string getMessagesSend()
        {
            string s = "";
            foreach (string message in messagesSend)
            {
                s += message + "\n";
            }
            return s;
        }
        public string getMessagesRecived()
        {
            string s = "";
            foreach (string message in messagesRecieved)
            {
                s += message + "\n";
            }
            return s;
        }

        public void notify()
        {
            throw new NotImplementedException();
        }
    }
}
