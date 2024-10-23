using System;

namespace Server.DataStorage
{
	public class Doctor
	{
		public String DoctorID { get; set; }
		public String DoctorName {  get; set; }

		public String DoctorPassword {  get; set; }

        public Doctor(String DoctorID, string DoctorName, string DoctorPassword)
        {
            this.DoctorID = DoctorID;
            this.DoctorName = DoctorName;
            this.DoctorPassword = DoctorPassword;
        }
    }

}

