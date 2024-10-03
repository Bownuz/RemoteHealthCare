namespace DataStorage
{
    internal class DataStorage
    {
        public static void SaveToFile(String messagge) {
            
                using (StreamWriter sw = File.CreateText("C:\\Users\\siemh\\IdeaProjects\\Fiets\\Code Visual studio" + "/TEST.txt"))
                {
                    Console.WriteLine(System.Environment.CurrentDirectory + "/TEST.txt");
                    sw.WriteLine(messagge);
                }
            
        }

        public void LoadFromFile() { 
        
        }






    }
}
