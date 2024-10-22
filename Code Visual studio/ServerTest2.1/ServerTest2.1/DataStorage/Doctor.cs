using System;

namespace Server.DataStorage
{
	public class Doctor
	{
		private int DoctorID;

		private String Name;

		private String Password;

        public Doctor(int doctorID, string name, string password)
        {
            DoctorID = doctorID;
            Name = name;
            Password = password;
        }
    }

}

