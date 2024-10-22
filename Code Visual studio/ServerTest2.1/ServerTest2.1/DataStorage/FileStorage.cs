using Server.Patterns.Observer;

namespace Server.DataStorage
{
    internal class FileStorage
    {
        Dictionary<String, Person> patients;

        public FileStorage()
        {
            patients = new Dictionary<string, Person>();
            LoadFromFile();
        }



        //TODO: add saving functionality
        public void SaveToFile()
        {
            Console.WriteLine("I should Save now!!");
            foreach (var patient in patients)
            {
                Console.WriteLine(patient.Key);
            }
        }

        //TODO: add loading functionality  
        public void LoadFromFile()
        {
            Console.WriteLine("I should load now");
        }

        public Person GetPatient(String name)
        {
            return patients[name];
        }

        public Boolean PatientExists(String name)
        {
            return patients.ContainsKey(name);
        }

        public void AddPatient(String name)
        {
            patients[name] = new Person(name);
        }

        public void Update(ClientType clientType)
        {
            SaveToFile();
        }
    }
}
