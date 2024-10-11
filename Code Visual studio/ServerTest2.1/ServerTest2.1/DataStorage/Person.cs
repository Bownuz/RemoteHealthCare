using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServerTest2._1.DataStorage
{
    class Person
    {
        string name { get; }
        List<Session> sessions = new List<Session>();
        public Person(string name)
        {
            this.name = name;

        }

        public void addSessions(Session s)
        {
            sessions.Add(s);
        }

        public string printSessions()
        {
            string Returnstring = "";
            foreach (Session s in sessions)
            {
                Returnstring = Returnstring + s.getMessagesSend() + " " + s.getMessagesRecived();
            }
            return Returnstring;

        }


        public override string ToString()
        {
            return "name: " + name + " Sessions: " + printSessions();
        }



    }
}
