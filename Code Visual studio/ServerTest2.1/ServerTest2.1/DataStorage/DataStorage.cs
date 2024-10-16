using System;
using System.Collections.Generic;
using System.IO;

namespace ServerTest2._1.DataStorage {
    internal class DataStorage {
        private HashSet<Person> patients = new HashSet<Person>();

        public static void SaveToFile(string message) {
            string path = "C:\\Users\\siemh\\IdeaProjects\\Fiets\\Code Visual studio\\TEST.txt";
            using(StreamWriter sw = File.CreateText(path)) {
                Console.WriteLine($"Saving data to: {path}");
                sw.WriteLine(message);
            }
        }

        public void LoadFromFile() {
            string path = "C:\\Users\\siemh\\IdeaProjects\\Fiets\\Code Visual studio\\TEST.txt";
            if(File.Exists(path)) {
                using(StreamReader sr = new StreamReader(path)) {
                    string line;
                    while((line = sr.ReadLine()) != null) {
                        
                        var dataParts = line.Split(','); 
                        if(dataParts.Length >= 4) {
                            string patientName = dataParts[0];
                            double bicycleSpeed = double.Parse(dataParts[1]);
                            int heartrate = int.Parse(dataParts[2]);
                            DateTime dateTime = DateTime.Parse(dataParts[3]);

                            patients.Add(new Person(patientName)); 
                        }
                    }
                }
            }
        }

        public HashSet<Person> GetPatients() {
            return this.patients;
        }
    }
}
