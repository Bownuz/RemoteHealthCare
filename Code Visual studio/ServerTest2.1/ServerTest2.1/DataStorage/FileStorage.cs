using Server.Patterns.Observer;
using Server.ThreadHandlers;
using System.Text.Json;


namespace Server.DataStorage {
    public class FileStorage : Observer {
        public readonly Dictionary<String, Patient> Patients;
        public Dictionary<String, Doctor> doctors { get; private set; }

        public static readonly String DoctorDirectoryPath = Environment.CurrentDirectory + "/DoctorData";
        public static readonly String PatientDirectoryPath = Environment.CurrentDirectory + "/PatientData";

        public FileStorage() {
            Patients = new Dictionary<string, Patient>();
            doctors = new Dictionary<String, Doctor>();
            LoadFromFile();
        }

        public void SaveToFile() {
            foreach (KeyValuePair<String, Patient> patientEntry in Patients) {
                Patient patient = patientEntry.Value;
                String patientName = patientEntry.Key;
                String filePath = String.Format($"{PatientDirectoryPath}/{patientName}.txt");

                FileCheckAndDelete(filePath);
                WritePatientToFile(patientName, patient);
            }

            foreach (KeyValuePair<String, Doctor> doctorEntry in doctors) {
                Doctor doctor = doctorEntry.Value;
                String doctorName = doctorEntry.Key;
                String filePath = String.Format($"{DoctorDirectoryPath}/{doctorName}.txt");

                FileCheckAndDelete(filePath);
                WriteDoctorToFile(doctorName, doctor);
            }

        }

        public void LoadFromFile() {
            DirectoryCheckAndCreate(PatientDirectoryPath);
            DirectoryCheckAndCreate(DoctorDirectoryPath);

            LoadAllObjectsFromDirectory(PatientDirectoryPath, CommunicationType.PATIENT);
            LoadAllObjectsFromDirectory(DoctorDirectoryPath, CommunicationType.DOCTOR);
        }

        private void LoadAllObjectsFromDirectory(string DirectoryPath, CommunicationType communicationType) {
            foreach (String filePath in Directory.EnumerateFiles(DirectoryPath, "*.txt")) {
                String fileContent = ReadFileContent(filePath);
                String Name = Path.GetFileNameWithoutExtension(filePath);

                switch (communicationType) {
                    case CommunicationType.PATIENT:
                        Patient patient = JsonSerializer.Deserialize<Patient>(fileContent);
                        Patients.Add(Name, patient);
                        break;
                    case CommunicationType.DOCTOR:
                        DoctorInitialiseMessage doctorData = JsonSerializer.Deserialize<DoctorInitialiseMessage>(fileContent);
                        Doctor doctor = new Doctor(doctorData.DoctorID, doctorData.DoctorName, doctorData.DoctorPassword);
                        doctors.Add(Name, doctor);
                        break;
                }
            }
        }

        public static void DirectoryCheckAndCreate(String DirectoryPath) {
            if (!Directory.Exists(DirectoryPath)) {
                Directory.CreateDirectory(DirectoryPath);
            }
        }

        public static void FileCheckAndDelete(String FilePath) {
            if (File.Exists(FilePath)) {
                File.Delete(FilePath);
            }
        }

        private static void WritePatientToFile(String patientName, Patient patient) {
            String FilePath = String.Format($"{PatientDirectoryPath}/{patientName}.txt");

            using (StreamWriter sw = File.CreateText(FilePath)) {
                String patientString = JsonSerializer.Serialize<Patient>(patient);
                sw.WriteLine(patientString);
            }
        }

        private static void WriteDoctorToFile(String doctorName, Doctor doctor) {
            String FilePath = String.Format($"{DoctorDirectoryPath}/{doctorName}.txt");

            using (StreamWriter sw = File.CreateText(FilePath)) {
                DoctorInitialiseMessage doctorData = new DoctorInitialiseMessage(doctor.DoctorID, doctor.DoctorName, doctor.DoctorPassword);
                String patientString = JsonSerializer.Serialize<DoctorInitialiseMessage>(doctorData);
                sw.WriteLine(patientString);
            }
        }

        public static string ReadFileContent(String filePath) {
            String fileContent = "";
            using (StreamReader sr = new StreamReader(filePath)) {
                while (!sr.EndOfStream) {
                    fileContent += sr.ReadLine();
                }
            }
            return fileContent;
        }

        public Patient GetPatient(String patientName) {
            return Patients[patientName];
        }

        public void AddPatient(String patientName) {
            Patients[patientName] = new Patient(patientName);
        }

        public Boolean PatientExists(String patientName) {
            return Patients.ContainsKey(patientName);
        }

        public String[] PatientNamesToArray() {
            String[] nameArray = new String[Patients.Count];
            int counter = 0;
            foreach (var patient in Patients) {
                nameArray[counter] = patient.Key;
                counter++;
            }
            return nameArray;
        }

        public Doctor getDoctor(String doctorId) {
            return doctors[doctorId];
        }

        public Boolean DoctorExists(String DoctorId) {
            return doctors.ContainsKey(DoctorId);
        }

        public void addDoctor(String doctorId, String DoctorName, String DoctorPassword) {
            doctors[doctorId] = new Doctor(doctorId, DoctorName, DoctorPassword);
        }

        public void Update(CommunicationType communicationOrigin, Session session) {
            SaveToFile();
        }



    }

    struct patientData() {
        public String patientName { get; set; }
        public Session Session { get; set; }
    }

}

