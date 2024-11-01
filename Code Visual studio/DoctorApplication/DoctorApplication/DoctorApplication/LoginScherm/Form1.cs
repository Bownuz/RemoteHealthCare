using System;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorApplication {
    public partial class Form1 : Form {
        private ServerConnection serverConnection;
        NetworkStream networkStream;

        public Form1(NetworkStream networkStream) {
            InitializeComponent();
            this.networkStream = networkStream;
        }

        private async void LoginButton_Click(object sender, EventArgs e) {
            string doctorIdText = DoctorIDTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            if (string.IsNullOrEmpty(doctorIdText) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) {
                MessageBox.Show("Vul DoctorID, gebruikersnaam en wachtwoord in.");
                return;
            }

            await LoginDoctor(doctorIdText, username, password);
        }

        private async Task LoginDoctor(String doctorId, string username, string password) {
            try {
                var loginData = new {
                    DoctorID = doctorId,
                    DoctorName = username,
                    DoctorPassword = password
                };
                String help = MessageCommunication.ReceiveMessage(networkStream);

                String loginDataJson = JsonSerializer.Serialize(loginData);
                MessageCommunication.SendMessage(networkStream, loginDataJson);

                String response = MessageCommunication.ReceiveMessage(networkStream);

                if (response != null && response.Contains("Login Successful")) {
                    MessageBox.Show("Inlog succesvol.");
                    OpenClientenForm();
                } else {
                    MessageBox.Show("Ongeldige inloggegevens.");
                }

            } catch (SocketException ex) {
                MessageBox.Show("Kan geen verbinding maken met de server: " + ex.Message);
            }
        }

        private void SendCommandToServer(object command) {
            try {
                string jsonCommand = System.Text.Json.JsonSerializer.Serialize(command);
                using (var writer = new System.IO.StreamWriter(networkStream) { AutoFlush = true }) {
                    writer.WriteLine(jsonCommand);
                }
                MessageBox.Show("Commando succesvol verzonden.");
            } catch (Exception ex) {
                MessageBox.Show("Fout bij het verzenden van commando: " + ex.Message);
            }
        }

        private void OpenClientenForm() {
            ClientenForm clientenForm = new ClientenForm(serverConnection);
            clientenForm.Show();
            this.Hide();
        }

        private void CloseButton_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        protected override void OnFormClosed(FormClosedEventArgs e) {
            base.OnFormClosed(e);
            serverConnection?.Disconnect();
        }
    }
}
