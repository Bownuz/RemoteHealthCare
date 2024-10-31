namespace Server.DataStorage {
    public class Patient(string name) {
        public string Name { get; set; } = name;
        public List<Session> sessions { get; set; } = new List<Session>();
        public Session currentSession { get; set; }
        public String lastConnectedErgometer { get; set; }
        public String lastConnectedHeartrateMonitor {  get; set; }

        public void addSession(Session sessionToAdd) {
            sessions.Add(sessionToAdd);
        }

        public Session GetSession(DateTime beginDate) {
            foreach (Session session in sessions) {
                if (session.sessionStart.Date.Equals(beginDate.Date)) {
                    return session;
                }
            }
            throw new Exception("This session was not found");
        }
    }

}

