using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoctorApplication;

namespace DoctorApplication {
    public partial class Form1 : Form {
        private SslStream sslStream;
        private TcpClient doctorClient;
        private DoctorState doctorState;
        private DoctorStateEnum currentState;

        public Form1() {
            InitializeComponent();
            currentState = DoctorStateEnum.Connecting;  // Start in de Connecting state
        }

        private async void LoginButton_Click(object sender, EventArgs e) {
            string doctorIdText = DoctorIDTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            if(string.IsNullOrEmpty(doctorIdText) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) {
                MessageBox.Show("Vul DoctorID, gebruikersnaam en wachtwoord in.");
                return;
            }

            if(!int.TryParse(doctorIdText, out int doctorId)) {
                MessageBox.Show("Voer een geldig DoctorID in.");
                return;
            }

            currentState = DoctorStateEnum.Login; // Update naar login state
            await LoginDoctor(doctorId, username, password);
        }

        private async Task LoginDoctor(int doctorId, string username, string password) {
            try {
                // Open een TCP-verbinding met de server
                doctorClient = new TcpClient("localhost", 4790);
                sslStream = new SslStream(doctorClient.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
                await sslStream.AuthenticateAsClientAsync("localhost");

                // Verzenden van login data
                var loginData = new {
                    DoctorID = doctorId,  // Ingevoerd DoctorID
                    DoctorName = username,
                    DoctorPassword = password
                };

                SendCommandToServer(loginData);

                using(var reader = new System.IO.StreamReader(sslStream)) {
                    string response = await reader.ReadLineAsync();
                    if(response.Contains("Login Successful")) {
                        MessageBox.Show("Inlog succesvol.");
                        currentState = DoctorStateEnum.CommandType;
                        InitializeDoctorState(); // Initializeer DoctorState na succesvolle login
                    }
                    else {
                        MessageBox.Show("Ongeldige inloggegevens.");
                        currentState = DoctorStateEnum.Login;  // Blijf in de login state bij fout
                    }
                }
            }
            catch(Exception ex) {
                MessageBox.Show("Kan geen verbinding maken: " + ex.Message);
            }
        }

        private void InitializeDoctorState() {
            // Initialiseer de DoctorState-klasse met de huidige form en serverconnection
            doctorState = new DoctorState(this, new ServerConnection(sslStream));
            doctorState.EnterCommandTypeState();
        }

        public string GetUserInput() {
            // Haal de invoer van de dokter op (bijv. RetrieveData, SendData, etc.)
            return UserInputTextBox.Text;
        }

        public string GetPatientName() {
            // Haal de patiëntnaam op
            return PatientNameTextBox.Text;
        }

        public DateTime GetSessionDate() {
            // Haal de sessiedatum op
            return SessionDatePicker.Value;
        }

        public string GetMessage() {
            // Haal het bericht op dat naar de patiënt moet worden verzonden
            return MessageTextBox.Text;
        }

        public void UpdateStatus(string statusMessage) {
            // Update het statusveld op het scherm
            StatusLabel.Text = statusMessage;
        }

        // Verzendt een commando naar de server
        private void SendCommandToServer(object command) {
            try {
                string jsonCommand = System.Text.Json.JsonSerializer.Serialize(command);
                using(var writer = new System.IO.StreamWriter(sslStream) { AutoFlush = true }) {
                    writer.WriteLine(jsonCommand);
                }
            }
            catch(Exception ex) {
                MessageBox.Show("Fout bij het verzenden van commando: " + ex.Message);
            }
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
            // TODO: Implement proper certificate validation
            return true;
        }

        private void Form1_Load(object sender, EventArgs e) {
            UpdateDateTimeLabel(null, null);
            timeUpdater.Interval = 1000;
            timeUpdater.Tick += new EventHandler(UpdateTimeLabel);
            timeUpdater.Start();
        }

        private void UpdateTimeLabel(object sender, EventArgs e) {
            DateTime now = DateTime.Now;
            TimeLabel.Text = $"Tijd: {now:HH:mm:ss}";
        }

        private void UpdateDateTimeLabel(object sender, EventArgs e) {
            DateTime now = DateTime.Now;
            DateLabel.Text = $"Vandaag is het: {now:dddd, dd MMMM yyyy}";
        }

        private void CloseButton_Click(object sender, EventArgs e) {
            Application.Exit();
        }
    }
}
