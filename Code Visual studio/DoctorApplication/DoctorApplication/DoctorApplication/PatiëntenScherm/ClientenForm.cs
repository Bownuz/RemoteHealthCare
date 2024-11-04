using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorApplication
{
    public partial class ClientenForm : UserControl
    {
        private ServerConnection serverConnection;

        public ClientenForm(ServerConnection serverConnection)
        {
            InitializeComponent();
            this.serverConnection = serverConnection;
            Task.Run(async () => serverConnection.RunConnection());
        }

        private void ClientenForm_Load(object sender, EventArgs e)
        {
            _ = ListenForLiveData();
        }

        private async Task ListenForLiveData()
        {
            try
            {
                // Hier moet nog logica komen voor 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout bij het ontvangen van gegevens: " + ex.Message);
            }
        }

        private void SendMessageButton_Click(object sender, EventArgs e)
        {
            string message = MessageTextBox.Text;
            foreach (string client in GetSelectedClients())
            {
                //doctorState.sendData.SendData(client, message);
                MessageBox.Show($"Bericht verzonden naar {client}.");
            }
        }

        private void StartTrainingButton_Click(object sender, EventArgs e)
        {
            serverConnection.protocol.doctorState.PerformAction(ValidMessages.d_startSession);
            string[] selectedPatients = GetSelectedClients();

            if (selectedPatients.Length == 0)
            {
                selectedPatients = new string[1] { "null" };
            }

            foreach (string clientName in selectedPatients)
            {
                serverConnection.protocol.doctorState.PerformAction(clientName);
                MessageBox.Show($"Session gestart voor {clientName}.");
            }
        }

        private void EndTrainingButton_Click(object sender, EventArgs e)
        {
            serverConnection.protocol.doctorState.PerformAction(ValidMessages.d_endSession);
            string[] selectedPatients = GetSelectedClients();

            if (selectedPatients.Length == 0)
            {
                selectedPatients = new string[1] { "null" };
            }

            foreach (string clientName in selectedPatients)
            {
                serverConnection.protocol.doctorState.PerformAction(clientName);
                MessageBox.Show($"Session gestopt voor {clientName}.");
            }
        }

        private void NoodstopButton_Click(object sender, EventArgs e)
        {
            foreach (string client in GetSelectedClients())
            {
                //doctorState.emergencyStop.EmergencyStop(client);
                MessageBox.Show($"Noodstop uitgevoerd voor {client}.");
            }
        }

        private async void ViewPreviousDataButton_Click(object sender, EventArgs e)
        {
            foreach (string client in GetSelectedClients())
            {
                List<TrainingData> clientTrainingData = await FetchTrainingDataForClient(client);

                TrainingDataForm dataForm = new TrainingDataForm(clientTrainingData, serverConnection);
                dataForm.Show();
                this.Hide();
            }
        }

        private async Task<List<TrainingData>> FetchTrainingDataForClient(string client)
        {
            // Voeg hier logica toe om de trainingsdata op te halen
            MessageBox.Show($"Fout bij het ophalen van trainingsdata: {client}");
            return new List<TrainingData>();
        }

        private void SubscribeButton_Click(object sender, EventArgs e)
        {
            serverConnection.protocol.doctorState.PerformAction(ValidMessages.d_subscribe);
            string[] selectedPatients = GetSelectedClients();

            if (selectedPatients.Length == 0)
            {
                selectedPatients = new string[1] { "null" };
            }

            foreach (string clientName in selectedPatients)
            {
                serverConnection.protocol.doctorState.PerformAction(clientName);
                MessageBox.Show($"Geabonneerd op {clientName}.");
            }
        }

        private void UnsubscribeButton_Click(object sender, EventArgs e)
        {
            serverConnection.protocol.doctorState.PerformAction(ValidMessages.d_unsubscribe);
            string[] selectedPatients = GetSelectedClients();

            if (selectedPatients.Length == 0)
            {
                selectedPatients = new string[1] { "null" };
            }

            foreach (string clientName in selectedPatients)
            {
                serverConnection.protocol.doctorState.PerformAction(clientName);
                MessageBox.Show($"Afgemeld van {clientName}.");
            }
        }

        private string[] GetSelectedClients()
        {
            var selectedClients = new List<string>();
            foreach (DataGridViewRow row in ClientenGridView.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["SelectClientColumn"].Value);
                if (isSelected)
                {
                    selectedClients.Add(row.Cells["PatientName"].Value.ToString());
                }
            }
            return selectedClients.ToArray();
        }

        private void UpdateDataGridView(ClientData clientData)
        {
            foreach (DataGridViewRow row in ClientenGridView.Rows)
            {
                if (row.Cells["PatientName"].Value?.ToString() == clientData.PatientName)
                {
                    row.Cells["HeartRate"].Value = clientData.HeartRate;
                    row.Cells["Speed"].Value = clientData.Speed;
                    row.Cells["LastUpdateColumn"].Value = clientData.LastUpdate;
                    return;
                }
            }

            ClientenGridView.Rows.Add(
                clientData.PatientName,
                clientData.LastUpdate,
                clientData.HeartRate,
                clientData.Speed
            );
        }

        public void UpdateStatus(string statusMessage)
        {
            MessageBox.Show(statusMessage);
        }

        private void ClientenGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ClientenGridView.Columns["SelectClientColumn"].Index && e.RowIndex >= 0)
            {
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)ClientenGridView.Rows[e.RowIndex].Cells["SelectClientColumn"];
                checkBoxCell.Value = !(bool)(checkBoxCell.Value ?? false);
            }
        }

        private void ResistanceTextBox_TextChanged(object sender, EventArgs e) {

        }

        private void ClientenGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e) {

        }

        private void label2_Click(object sender, EventArgs e) {

        }
        private void AdjustResistanceButton_Click(object sender, EventArgs e)
        {
            // Logica om de weerstand aan te passen
        }

    }
}
