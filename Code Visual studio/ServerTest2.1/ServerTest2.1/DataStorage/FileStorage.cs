using Server.Patterns.Observer;
using Server.ThreadHandlers;


namespace Server.DataStorage
{
    public class FileStorage : Observer
    {
        public readonly Dictionary<String, Patient> Patients;

        public readonly Dictionary<int, Doctor> doctors;

        public FileStorage()
        {
            Patients = new Dictionary<string, Patient>();
            doctors = new Dictionary<int, Doctor>();
            LoadFromFile();
        }

        public void SaveToFile()
        {
            Console.WriteLine("I should Save now!!");
            foreach (var patient in Patients)
            {
                Console.WriteLine(patient.Key);
            }
        }

        public void LoadFromFile()
        {
            Console.WriteLine("I should load now");
        }

        public Patient GetPatient(String patientName)
        {
            return Patients[patientName];
        }

        public void AddPatient(String patientName)
        {
            Patients[patientName] = new Patient(patientName);
        }

        public Boolean PatientExists(String patientName)
        {
            return Patients.ContainsKey(patientName);
        }

        public String[] PatientNamesToArray() {
            String[] nameArray = new String[Patients.Count];
            int counter = 0;
            foreach (var patient in Patients)
            {
                nameArray[counter] = patient.Key;
                counter++;
            }
            return nameArray;
        }

		public Doctor getDoctor(int doctorId)
		{
            return doctors[doctorId];
		}

		public void addDoctor(int doctorId, String DoctorName, String DoctorPassword)
		{
            doctors[doctorId] = new Doctor(doctorId,DoctorName,DoctorPassword);
        }

        public void Update(CommunicationType communicationOrigin, Session session)
        {
            SaveToFile();
        }
    }

}

