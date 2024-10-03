namespace ServerTest2._1
{
    class Session
    {
        List<String> messagesSend = new List<string>();
        List<String> messagesRecieved = new List<string>();

        public void addMessagesSend(String message) { 
            messagesSend.Add(message);

        }

        public void addMessagesRecived(String message) { 
            messagesRecieved.Add(message);
        }

        public String getMessagesSend() {
            String s = "";
            foreach (String message in messagesSend) { 
                s+= message + "\n";
            }
            return s;
        }
        public String getMessagesRecived()
        {
            String s = "";
            foreach (String message in messagesRecieved)
            {
               s+= message + "\n";
            }
            return s;
        }







    }
}
