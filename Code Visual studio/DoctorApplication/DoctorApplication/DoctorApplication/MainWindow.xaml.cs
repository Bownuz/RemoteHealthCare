using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows;

namespace DoctorApplication {
    public partial class MainWindow : Window {
        private SslStream sslStream;
        private TcpClient doctorClient;

        public MainWindow() {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e) {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;

            if(username == "doctor" && password == "password123") {
                // Verbind met de server via SSL
                ConnectToServer();
            }
            else {
                MessageBox.Show("Foutieve gebruikersnaam of wachtwoord.");
            }
        }

        private void ConnectToServer() {
            try {
                // Maak een verbinding met de server
                doctorClient = new TcpClient("127.0.0.1", 4790); // Vervang door de juiste server-IP en poort

                // Gebruik SSL-stream voor beveiligde communicatie
                sslStream = new SslStream(doctorClient.GetStream(), false,
                    new RemoteCertificateValidationCallback(ValidateServerCertificate), null);

                // Start de SSL-authenticatie
                sslStream.AuthenticateAsClient("ServerName"); // Vervang "ServerName" door de naam van je server of je certificaat

                MessageBox.Show("Beveiligde verbinding gemaakt met de server!");

                // Start het monitoren van cliëntgegevens
                StartMonitoring();
            }
            catch(Exception ex) {
                MessageBox.Show("Kan geen verbinding maken met de server: " + ex.Message);
            }
        }

        // Controleer het servercertificaat
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
            if(sslPolicyErrors == SslPolicyErrors.None) {
                return true; // Het certificaat is geldig
            }

            // Voor testdoeleinden: accepteer elk certificaat (niet gebruiken in productie)
            MessageBox.Show("Fout in SSL-certificaat: " + sslPolicyErrors.ToString());
            return false;
        }

        private async void StartMonitoring() {
            while(true) {
                string receivedData = await ReceiveDataFromServer();
                if(!string.IsNullOrEmpty(receivedData)) {
                    // Verwerk de ontvangen data
                    ProcessClientData(receivedData);
                }
            }
        }

        private async Task<string> ReceiveDataFromServer() {
            try {
                if(doctorClient.Connected) {
                    // Lezen van de SSL-stream
                    using(StreamReader reader = new StreamReader(sslStream)) {
                        return await reader.ReadLineAsync();
                    }
                }
            }
            catch(Exception ex) {
                MessageBox.Show("Fout bij het ontvangen van gegevens: " + ex.Message);
            }

            return null;
        }

        private void ProcessClientData(string receivedData) {
            // Deserialiseer de ontvangen data en werk de GUI bij
            MessageBox.Show($"Gegevens ontvangen: {receivedData}");
        }
    }
}
