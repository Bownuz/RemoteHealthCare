﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTest2._1.DataStorage
{
    class Session
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







    }
}