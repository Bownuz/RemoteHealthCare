using Server.DataStorage;

namespace Server.Patterns.Observer
{
    public interface Observer
    {
        void Update(ClientType messageType);
    }
}
