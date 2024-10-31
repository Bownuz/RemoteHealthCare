namespace Server.DataStorage {
    public class Doctor {
        public readonly String DoctorID;
        public readonly String DoctorName;
        public readonly String DoctorPassword;

        public Doctor(String DoctorID, string DoctorName, string DoctorPassword) {
            this.DoctorID = DoctorID;
            this.DoctorName = DoctorName;
            this.DoctorPassword = DoctorPassword;
        }
    }

}

