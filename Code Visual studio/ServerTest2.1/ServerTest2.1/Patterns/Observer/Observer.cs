using Server.DataStorage;
using Server.ThreadHandlers;

namespace Server.Patterns.Observer {
    public interface Observer {
        void Update(CommunicationType communicationOrigin, Session session);
    }
}

