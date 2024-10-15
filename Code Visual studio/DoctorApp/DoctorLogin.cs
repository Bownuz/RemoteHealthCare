using System;
using System.Net.Sockets;
using System.Text;

namespace DoctorApp {
    class DoctorLogin {
        public bool Login(TcpClient doctorClient) {
            try {
                
                Console.WriteLine("Voer gebruikersnaam in:");
                string username = Console.ReadLine();

                Console.WriteLine("Voer wachtwoord in:");
                string password = Console.ReadLine();

                string credentials = $"{username}:{password}";
                byte[] data = Encoding.ASCII.GetBytes(credentials);

                var stream = doctorClient.GetStream();
                stream.Write(data, 0, data.Length);

                byte[] response = new byte[256];
                int bytes = stream.Read(response, 0, response.Length);
                string responseData = Encoding.ASCII.GetString(response, 0, bytes);

                if(responseData == "Success") {
                    Console.WriteLine("Inloggen succesvol!");
                    return true;
                }
                else {
                    Console.WriteLine("Inloggen mislukt. Probeer opnieuw.");
                    return false;
                }
            }
            catch(Exception ex) {
                Console.WriteLine($"Fout bij het inloggen: {ex.Message}");
                return false;
            }
        }
    }
}
