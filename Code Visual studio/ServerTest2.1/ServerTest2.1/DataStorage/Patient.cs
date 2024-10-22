using System;
using System.Collections.Generic;

namespace Server.DataStorage
{
	public class Patient(string name)
    {
        public readonly string Name = name;
        public readonly List<Session> sessions = new List<Session>();
        public Session currentSession { get; set; }

        public void addSession(Session sessionToAdd)
        {
            sessions.Add(sessionToAdd);
        }
    }

}

