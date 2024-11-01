using Server.DataStorage;
using Server.ThreadHandlers;

namespace Server.Patterns.Observer {
    public class Subject {
        public readonly List<Observer> observers;

        public Subject() {
            this.observers = new List<Observer>();
        }

        public void AddObserver(Observer observerToAdd) {
            observers.Add(observerToAdd);
        }

        public void RemoveObserver(Observer observerToRemove) {
            observers.Remove(observerToRemove);
        }

        public void UpdateObservers(CommunicationType communicationOrigin, Session session) {
            foreach (Observer observer in observers) {
                observer.Update(communicationOrigin, session);
            }
        }

    }

}

