using Server.Patterns.Observer;
using Server.ThreadHandlers;
using System.Text.Json;


namespace Server.DataStorage {
    public class FileStorage : Observer {
        public readonly Dictionary<String, Patient> Patients;
        public Dictionary<String, Doctor> doctors { get; private set; }

        public FileStorage() {
            Patients = new Dictionary<string, Patient>();
            doctors = new Dictionary<String, Doctor>();

            LoadFromFile();
        }

        public void SaveToFile() {
            String PatientsPath = Environment.CurrentDirectory + "/PatientData";
            String DoctorsPath = Environment.CurrentDirectory + "/DoctorData";

            Console.WriteLine("I should Save now!!");
            foreach (var patient in Patients) {
                if (!Directory.Exists(PatientsPath)) {
                    Directory.CreateDirectory(PatientsPath);
                }

                if (File.Exists(PatientsPath + "/" + patient.Key + ".txt")) {
                    File.Delete(PatientsPath + "/" + patient.Key + ".txt");
                }

                using (StreamWriter sw = File.CreateText((PatientsPath + "/" + patient.Key + ".txt"))) {
                    String patientString = JsonSerializer.Serialize<Patient>(patient.Value);
                    sw.WriteLine(patientString);
                }
            }

            foreach (var doctor in doctors) {
                if (!Directory.Exists(DoctorsPath)) {
                    Directory.CreateDirectory(DoctorsPath);
                }

                if (File.Exists(DoctorsPath + "/" + doctor.Key + ".txt")) {
                    File.Delete(DoctorsPath + "/" + doctor.Key + ".txt");
                }

                using (StreamWriter sw = File.CreateText((DoctorsPath + "/" + doctor.Key + ".txt"))) {
                    String patientString = JsonSerializer.Serialize<Doctor>(doctor.Value);
                    sw.WriteLine(patientString);
                }
            }

        }

        public void LoadFromFile() {
            String PatientsPath = Environment.CurrentDirectory + "/PatientData";
            String DoctorsPath = Environment.CurrentDirectory + "/DoctorData";


            foreach (var file in
            Directory.EnumerateFiles(PatientsPath, "*.txt")) {
                String fileContent = "";
                using (StreamReader sr = new StreamReader(file)) {
                    while (!sr.EndOfStream) {
                        fileContent += sr.ReadLine();
                    }
                }
                String Name = Path.GetFileNameWithoutExtension(file);
                Patient patient = JsonSerializer.Deserialize<Patient>(fileContent);
                Patients.Add(Name, patient);
            }

            foreach (var file in
            Directory.EnumerateFiles(DoctorsPath, "*.txt")) {
                String fileContent = "";
                using (StreamReader sr = new StreamReader(file)) {
                    while (!sr.EndOfStream) {
                        fileContent += sr.ReadLine();
                    }
                }
                String Name = Path.GetFileNameWithoutExtension(file);
                Doctor doctor = JsonSerializer.Deserialize<Doctor>(fileContent);
                doctors.Add(Name, doctor);
            }
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

