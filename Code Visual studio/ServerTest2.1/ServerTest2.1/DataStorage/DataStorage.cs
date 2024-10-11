using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServerTest2._1.DataStorage
{
    internal class DataStorage
    {
        HashSet<Person> patients = new HashSet<Person>(); 

        public static void SaveToFile(string messagge)
        {

            using (StreamWriter sw = File.CreateText("C:\\Users\\siemh\\IdeaProjects\\Fiets\\Code Visual studio" + "/TEST.txt"))
            {
                Console.WriteLine(Environment.CurrentDirectory + "/TEST.txt");
                sw.WriteLine(messagge);
                
            }

            

        }

        public void LoadFromFile()
        {
        }

        public HashSet<Person> getPatients() { 
            return this.patients;
        }




    }
}
