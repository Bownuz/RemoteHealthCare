namespace Server.DataStorage
{
    class Person
    {
        public readonly string Name;
        List<Session> sessions;
        public Session currentSession { get; set; }


        public Person(string name)
        {
            Name = name;
        }
    }
}
