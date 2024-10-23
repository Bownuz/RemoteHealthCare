using Server.Patterns.Observer;
using Server.ThreadHandlers;


namespace Server.DataStorage
{
	public class FileStorage : Observer
	{
		private readonly Dictionary<String, Patient> patients;

		private readonly Dictionary<int, Doctor> doctors;

        public FileStorage()
        {
            patients = new Dictionary<string, Patient>();
			doctors = new Dictionary<int, Doctor>();
            LoadFromFile();
        }

        public void SaveToFile()
		{
            Console.WriteLine("I should Save now!!");
            foreach (var patient in patients)
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
            return patients[patientName];
        }

		public void AddPatient(String patientName)
		{
            patients[patientName] = new Patient(patientName);
        }

		public Boolean PatientExists(String patientName)
		{
            return patients.ContainsKey(patientName);
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

