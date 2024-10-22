namespace Server.DataStorage
{
    class Person
    {
        public readonly string Name;
        public readonly List<Session> sessions;
        public Session currentSession { get; set; }


        public Person(string name)
        {
            Name = name;
            sessions = new List<Session>();
        }

        public void addSession(Session sessionToAdd) { 
            sessions.Add(sessionToAdd);
        }
    }
}
