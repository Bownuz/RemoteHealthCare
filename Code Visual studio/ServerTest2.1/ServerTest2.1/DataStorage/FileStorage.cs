

using Server.ObserverPattern;

namespace Server.DataStorage
{
    internal class FileStorage : Observer
    {
        HashSet<Person> patients = new HashSet<Person>(); 

        public static void SaveToFile()
        { 
        }

        public void LoadFromFile()
        {
        }

        public HashSet<Person> getPatients() { 
            return this.patients;
        }

        public void notify()
        {
            SaveToFile();
        }
    }
}
