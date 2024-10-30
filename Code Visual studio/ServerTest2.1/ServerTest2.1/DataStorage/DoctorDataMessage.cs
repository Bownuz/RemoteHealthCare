namespace Server.DataStorage {
    public struct DoctorDataMessage {
        public string Action { get; set; }
        public string PatientName { get; set; }
        public string Message { get; set; }
        public int newResistance { get; set; }

        public DoctorDataMessage(string action, string patientName, string message, int newResistance) {
            Action = action;
            PatientName = patientName;
            Message = message;
            this.newResistance = newResistance;
        }
    }
}
