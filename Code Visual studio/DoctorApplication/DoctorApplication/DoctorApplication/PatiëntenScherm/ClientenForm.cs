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
        private DoctorState doctorState;

        public ClientenForm(SslStream stream) {
            InitializeComponent();
            sslStream = stream;
            doctorState = new DoctorState(this, new ServerConnection(sslStream));
        }

        private void ClientenForm_Load(object sender, EventArgs e) {
            _ = ListenForLiveData();
        }

        private async Task ListenForLiveData() {
            try {
                using(var reader = new StreamReader(sslStream)) {
                    while(true) {
                        string jsonData = await reader.ReadLineAsync();
                        if(!string.IsNullOrEmpty(jsonData)) {
                            var clientData = JsonSerializer.Deserialize<ClientData>(jsonData);
                            UpdateDataGridView(clientData);
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
                doctorState.SendDataToClient(client, message);
                MessageBox.Show($"Bericht verzonden naar {client}.");
            }
        }

        private void StartTrainingButton_Click(object sender, EventArgs e) {
            foreach(string client in GetSelectedClients()) {
                doctorState.StartTrainingForClient(client);
                MessageBox.Show($"Trainingssessie gestart voor {client}.");
            }
        }

        private void StopTrainingButton_Click(object sender, EventArgs e) {
            foreach(string client in GetSelectedClients()) {
                doctorState.StopTrainingForClient(client);
                MessageBox.Show($"Trainingssessie gestopt voor {client}.");
            }
        }

        private void NoodstopButton_Click(object sender, EventArgs e) {
            foreach(string client in GetSelectedClients()) {
                doctorState.EmergencyStopForClient(client);
                MessageBox.Show($"Noodstop uitgevoerd voor {client}.");
            }
        }

        private async void ViewPreviousDataButton_Click(object sender, EventArgs e) {
            foreach(string client in GetSelectedClients()) {
                List<TrainingData> clientTrainingData = await FetchTrainingDataForClient(client);

                TrainingDataForm dataForm = new TrainingDataForm(clientTrainingData, sslStream);
                dataForm.Show();
                this.Hide();
            }
        }
        private async Task<List<TrainingData>> FetchTrainingDataForClient(string client) {
            try { 
                var command = new {
                    Action = "FetchTrainingData",
                    TargetClient = client
                };

                string jsonCommand = JsonSerializer.Serialize(command);
                using(var writer = new StreamWriter(sslStream) { AutoFlush = true }) {
                    await writer.WriteLineAsync(jsonCommand);
                }

                
                using(var reader = new StreamReader(sslStream)) {
                    string response = await reader.ReadLineAsync();

                    if(!string.IsNullOrEmpty(response)) {
                        
                        return JsonSerializer.Deserialize<List<TrainingData>>(response);
                    }
                    else {
                        MessageBox.Show($"Geen trainingsdata gevonden voor {client}.");
                        return new List<TrainingData>(); 
                    }
                }
            }
            catch(Exception ex) {
                MessageBox.Show($"Fout bij het ophalen van trainingsdata: {ex.Message}");
                return new List<TrainingData>(); 
            }
        }



        private void AdjustResistanceButton_Click(object sender, EventArgs e) {
            string resistanceValue = ResistanceTextBox.Text;
            if(int.TryParse(resistanceValue, out int resistance)) {
                foreach(string client in GetSelectedClients()) {
                    doctorState.AdjustResistanceForClient(client, resistance);
                    MessageBox.Show($"Weerstand aangepast voor {client} naar {resistance}.");
                }
            }
            else {
                MessageBox.Show("Voer een geldige waarde in voor de weerstand.");
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
            foreach(DataGridViewRow row in ClientenGridView.Rows) {
                if(row.Cells["PatientName"].Value?.ToString() == clientData.PatientName) {
                    row.Cells["Resistance"].Value = clientData.Resistance;
                    row.Cells["HeartRate"].Value = clientData.HeartRate;
                    row.Cells["Speed"].Value = clientData.Speed;
                    row.Cells["LastUpdateColumn"].Value = clientData.LastUpdate;
                    return;
                }
            }

            ClientenGridView.Rows.Add(
                clientData.PatientName,
                clientData.Resistance,
                clientData.LastUpdate,
                clientData.HeartRate,
                clientData.Speed
            );
        }

        public void UpdateStatus(string statusMessage) {
            MessageBox.Show(statusMessage);
        }

        private void ClientenGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if(e.ColumnIndex == ClientenGridView.Columns["SelectClientColumn"].Index && e.RowIndex >= 0) {
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)ClientenGridView.Rows[e.RowIndex].Cells["SelectClientColumn"];
                checkBoxCell.Value = !(bool)(checkBoxCell.Value ?? false);
            }
        }
    }
}
