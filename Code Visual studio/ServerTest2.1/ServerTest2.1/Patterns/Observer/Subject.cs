using Server.DataStorage;

namespace Server.Patterns.Observer
{
    public class Subject
    {
        public readonly List<Observer> Observers;

        public Subject()
        {
            Observers = new List<Observer>();
        }

        public void AddObserver(Observer newObserver)
        {
            Observers.Add(newObserver);
        }

        public void RemoveObserver(Observer newObserver)
        {
            Observers.Remove(newObserver);
        }

        public void UpdateAll(ClientType clientType)
        {
            foreach (Observer observer in Observers)
            {
                observer.Update(clientType);
            }
        }
    }

}
