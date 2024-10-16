using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace DoctorApplication {
    public partial class MainWindow : Window {
        private SslStream sslStream;
        private TcpClient doctorClient;
        private ObservableCollection<ClientRecieveData> clients = new ObservableCollection<ClientRecieveData>();

        public MainWindow() {
            InitializeComponent();
            ClientsListView.ItemsSource = clients;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e) {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;

            if(username == "doctor" && password == "password123") {
                ConnectToServer();
            }
            else {
                MessageBox.Show("Foutieve gebruikersnaam of wachtwoord.");
            }
        }

        private void ConnectToServer() {
            try {
                doctorClient = new TcpClient("127.0.0.1", 4790);
                sslStream = new SslStream(doctorClient.GetStream(), false, ValidateServerCertificate, null);
                sslStream.AuthenticateAsClient("ServerName");

                MessageBox.Show("Beveiligde verbinding gemaakt met de server!");
                StartMonitoring();
            }
            catch(Exception ex) {
                MessageBox.Show("Kan geen verbinding maken met de server: " + ex.Message);
            }
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
            if(sslPolicyErrors == SslPolicyErrors.None) return true;
            return false;
        }

        private async void StartMonitoring() {
            while(true) {
                string receivedData = await ReceiveDataFromServer();
                if(!string.IsNullOrEmpty(receivedData)) {
                    ProcessClientData(receivedData);
                }
            }
        }

        private async Task<string> ReceiveDataFromServer() {
            try {
                if(doctorClient.Connected) {
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
            var clientData = JsonSerializer.Deserialize<ClientRecieveData>(receivedData);

            // Update the ListView
            var existingClient = clients.FirstOrDefault(c => c.PatientName == clientData.PatientName);
            if(existingClient != null) {
                existingClient.BicycleSpeed = clientData.BicycleSpeed;
                existingClient.Heartrate = clientData.Heartrate;
                existingClient.DateTime = clientData.DateTime;
            }
            else {
                clients.Add(clientData);
            }
        }

        private void StartSession_Click(object sender, RoutedEventArgs e) {
            var sessionCommand = new {
                CommandType = "START_SESSION",
                ClientId = GetSelectedClientId(),
                StartTime = DateTime.Now
            };

            string commandJson = JsonSerializer.Serialize(sessionCommand);
            SendCommandToServer(commandJson);
        }

        private void StopSession_Click(object sender, RoutedEventArgs e) {
            var sessionCommand = new {
                CommandType = "STOP_SESSION",
                ClientId = GetSelectedClientId(),
                EndTime = DateTime.Now
            };

            string commandJson = JsonSerializer.Serialize(sessionCommand);
            SendCommandToServer(commandJson);
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e) {
            string message = MessageTextBox.Text;

            var messageCommand = new {
                CommandType = "SEND_MESSAGE",
                ClientId = GetSelectedClientId(),
                Message = message
            };

            string commandJson = JsonSerializer.Serialize(messageCommand);
            SendCommandToServer(commandJson);
        }

        private void AdjustResistance_Click(object sender, RoutedEventArgs e) {
            int resistanceValue = 50;

            var resistanceCommand = new {
                CommandType = "ADJUST_RESISTANCE",
                ClientId = GetSelectedClientId(),
                Resistance = resistanceValue
            };

            string commandJson = JsonSerializer.Serialize(resistanceCommand);
            SendCommandToServer(commandJson);
        }

        private void EmergencyStop_Click(object sender, RoutedEventArgs e) {
            var stopCommand = new {
                CommandType = "EMERGENCY_STOP",
                ClientId = GetSelectedClientId()
            };

            string commandJson = JsonSerializer.Serialize(stopCommand);
            SendCommandToServer(commandJson);
        }

        private string GetSelectedClientId() {
            if(ClientsListView.SelectedItem is ClientRecieveData selectedClient) {
                return selectedClient.PatientName;
            }
            return null;
        }

        private void SendCommandToServer(string command) {
            try {
                if(doctorClient.Connected) {
                    using(StreamWriter writer = new StreamWriter(sslStream)) {
                        writer.WriteLine(command);
                        writer.Flush();
                    }
                }
            }
            catch(Exception ex) {
                MessageBox.Show("Fout bij het versturen van gegevens: " + ex.Message);
            }
        }
    }

    public class ClientRecieveData {
        public string PatientName { get; set; }
        public double BicycleSpeed { get; set; }
        public int Heartrate { get; set; }
        public DateTime DateTime { get; set; }
    }
}
