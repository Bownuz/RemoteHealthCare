using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorApplication {
    public partial class ClientenForm : Form {
        private SslStream sslStream;

        public ClientenForm(SslStream stream) {
            InitializeComponent();
            sslStream = stream;
        }

        private void ClientenForm_Load(object sender, EventArgs e) {
            // Start listening for live data from the server
            _ = ListenForLiveData();
        }

        private async Task ListenForLiveData() {
            try {
                using(var reader = new StreamReader(sslStream)) {
                    while(true) {
                        string jsonData = await reader.ReadLineAsync();
                        if(!string.IsNullOrEmpty(jsonData)) {
                            var clientData = JsonSerializer.Deserialize<ClientData>(jsonData);
                            UpdateDataGridView(clientData); // Update de DataGridView met real-time gegevens
                        }
                    }
                }
            }
            catch(Exception ex) {
                MessageBox.Show("Fout bij het ontvangen van gegevens: " + ex.Message);
            }
        }

        private void SendMessageButton_Click(object sender, EventArgs e) {
            string message = MessageTextBox.Text;
            foreach(string client in GetSelectedClients()) {
                SendCommandToServer(new {
                    Action = "SendMessage",
                    TargetClient = client,
                    Message = message
                });
                MessageBox.Show($"Bericht verzonden naar {client}.");
            }
        }

        private void StartTrainingButton_Click(object sender, EventArgs e) {
            foreach(string client in GetSelectedClients()) {
                SendCommandToServer(new {
                    Action = "StartTraining",
                    TargetClient = client
                });
                MessageBox.Show($"Trainingssessie gestart voor {client}.");
            }
        }

        private void StopTrainingButton_Click(object sender, EventArgs e) {
            foreach(string client in GetSelectedClients()) {
                SendCommandToServer(new {
                    Action = "StopTraining",
                    TargetClient = client
                });
                MessageBox.Show($"Trainingssessie gestopt voor {client}.");
            }
        }

        private void NoodstopButton_Click(object sender, EventArgs e) {
            foreach(string client in GetSelectedClients()) {
                SendCommandToServer(new {
                    Action = "EmergencyStop",
                    TargetClient = client
                });
                MessageBox.Show($"Noodstop uitgevoerd voor {client}.");
            }
        }

        private void ViewPreviousDataButton_Click(object sender, EventArgs e) {
            foreach(string client in GetSelectedClients()) {
                SendCommandToServer(new {
                    Action = "GetTrainingData",
                    TargetClient = client
                });

                // Open het TrainingDataForm en geef de SSLStream mee voor verdere communicatie
                TrainingDataForm dataForm = new TrainingDataForm(sslStream);
                dataForm.Show();
                this.Hide();
            }
        }

        private void AdjustResistanceButton_Click(object sender, EventArgs e) {
            string resistanceValue = ResistanceTextBox.Text;
            if(int.TryParse(resistanceValue, out int resistance)) {
                foreach(string client in GetSelectedClients()) {
                    SendCommandToServer(new {
                        Action = "AdjustResistance",
                        TargetClient = client,
                        ResistanceLevel = resistance
                    });
                    MessageBox.Show($"Weerstand aangepast voor {client} naar {resistance}.");
                }
            }
            else {
                MessageBox.Show("Voer een geldige waarde in voor de weerstand.");
            }
        }

        private void SendCommandToServer(object command) {
            try {
                string jsonCommand = JsonSerializer.Serialize(command);
                using(var writer = new StreamWriter(sslStream) { AutoFlush = true }) {
                    writer.WriteLine(jsonCommand);
                }
            }
            catch(Exception ex) {
                MessageBox.Show("Fout bij het verzenden van commando: " + ex.Message);
            }
        }

        private string[] GetSelectedClients() {
            var selectedClients = new List<string>();
            foreach(DataGridViewRow row in ClientenGridView.Rows) {
                bool isSelected = Convert.ToBoolean(row.Cells["SelectClientColumn"].Value);
                if(isSelected) {
                    selectedClients.Add(row.Cells["PatientName"].Value.ToString());
                }
            }
            return selectedClients.ToArray();
        }

        private void UpdateDataGridView(ClientData clientData) {
            // Controleer of de cliënt al bestaat in de DataGridView
            foreach(DataGridViewRow row in ClientenGridView.Rows) {
                if(row.Cells["PatientName"].Value?.ToString() == clientData.PatientName) {
                    // Update bestaande cliëntgegevens
                    row.Cells["Resistance"].Value = clientData.Resistance;
                    row.Cells["HeartRate"].Value = clientData.HeartRate;
                    row.Cells["Speed"].Value = clientData.Speed;
                    row.Cells["LastUpdateColumn"].Value = clientData.LastUpdate;
                    return;
                }
            }

            // Als cliënt nog niet bestaat, voeg deze toe
            ClientenGridView.Rows.Add(
                clientData.PatientName,
                clientData.Resistance,
                clientData.LastUpdate,
                clientData.HeartRate,
                clientData.Speed
            );
        }

        private void ClientenGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if(e.ColumnIndex == ClientenGridView.Columns["SelectClientColumn"].Index && e.RowIndex >= 0) {
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)ClientenGridView.Rows[e.RowIndex].Cells["SelectClientColumn"];
                checkBoxCell.Value = !(bool)(checkBoxCell.Value ?? false);
            }
        }
    }
}
