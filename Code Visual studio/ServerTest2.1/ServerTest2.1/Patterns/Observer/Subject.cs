namespace Server.Patterns.Observer
{
    public class Subject
    {
        private List<Observer> observers;

        public Subject()
        {
            observers = new List<Observer>();
        }

        public void AddObserver(Observer newObserver)
        {
            observers.Add(newObserver);
        }

        public void RemoveObserver(Observer newObserver)
        {
            observers.Remove(newObserver);
        }

        public void NotifyAll()
        {
            foreach (Observer observer in observers)
            {
                observer.Update();
            }
        }


    }
}
