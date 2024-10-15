using ServerTest2._1.Patterns.Observer;

namespace Server.DataStorage
{
    internal class FileStorage : Observer
    {
        Dictionary<String, Person> patients;

        //TODO: add saving functionality
        public void SaveToFile()
        {
        }

        //TODO: add loading functionality  
        public void LoadFromFile()
        {
        }

        public Person GetPatient(String Name)
        {
            return patients[Name];
        }

        public Boolean PatientExists(String name)
        {
            return patients.ContainsKey(name);
        }

        public void AddPatient(String name)
        {
            patients[name] = new Person(name);
        }

        public void Update()
        {
            SaveToFile();
        }
    }
}
