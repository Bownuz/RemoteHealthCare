using System;
using System.Windows.Forms;

namespace DoctorApplication {
    public partial class ClientenForm : Form {
        private ServerConnection serverConnection;

        public ClientenForm() {
            InitializeComponent();
            serverConnection = new ServerConnection();
            serverConnection.OnDataReceived += UpdateDataGridView;
        }

        private async void ClientenForm_Load(object sender, EventArgs e) {
            try {
                await serverConnection.ConnectToServer("localhost", 4790);
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void SendMessageButton_Click(object sender, EventArgs e) {
            string message = MessageTextBox.Text;
            string selectedClient = null;

            // Zoek de geselecteerde client
            foreach(DataGridViewRow row in ClientenGridView.Rows) {
                bool isSelected = Convert.ToBoolean(row.Cells["SelectClientColumn"].Value);
                if(isSelected) {
                    selectedClient = row.Cells["PatientName"].Value.ToString();
                    break;
                }
            }

            if(selectedClient != null) {
                try {
                    // Stuur het bericht naar de server, die het vervolgens naar de cliënt stuurt
                    serverConnection.SendMessageToServer(message, selectedClient);
                    MessageBox.Show($"Bericht verzonden naar {selectedClient} via de server.");
                }
                catch(Exception ex) {
                    MessageBox.Show("Fout bij het verzenden van het bericht: " + ex.Message);
                }
            }
            else {
                MessageBox.Show("Selecteer een cliënt om een bericht naar te sturen.");
            }
        }

        private void ClientenGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            // Controleer of de cel een checkbox is en update de selectie
            if(e.ColumnIndex == ClientenGridView.Columns["SelectClientColumn"].Index && e.RowIndex >= 0) {
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)ClientenGridView.Rows[e.RowIndex].Cells["SelectClientColumn"];
                checkBoxCell.Value = !(checkBoxCell.Value != null && (bool)checkBoxCell.Value);
            }
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
    }
}
