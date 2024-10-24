using ConnectionImplemented;
using ClientApplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApplication.State;

namespace ClientApplication {
    internal partial class ClientInfoScreen : UserControl {
        private Ergometer ergometer;
        private HeartRateMonitor heartRateMonitor;
        private DataHandler dataHandler;
        private Form mainForm;
        private Timer updateTimer;
        private Handler handler;
        public ClientInfoScreen(DataHandler dataHandler, Form mainForm, Ergometer ergometer, HeartRateMonitor heartRateMonitor, Handler handler) {
            InitializeComponent();
            this.mainForm = mainForm;
            this.ergometer = ergometer;
            this.heartRateMonitor = heartRateMonitor;
            this.dataHandler = dataHandler;
            this.handler = handler;

            //handler.NewDoctorMessage += UpdateMessage;

            InitializeTimer();
        }

        private void SpeedPatientLabel(object sender, EventArgs e) {
        }

        private void HeartRatePatientLabel(object sender, EventArgs e) {
        }

        private void CloseButton_Click(object sender, EventArgs e) {
            DialogResult dialog = MessageBox.Show("Do you want to close this window", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes) {
                this.ParentForm?.Close();
            }
        }

        private void InitializeTimer() {
            updateTimer = new Timer();
            updateTimer.Interval = 1000;
            updateTimer.Tick += new EventHandler(UpdateLabels);
            updateTimer.Start();
        }

        private void UpdateLabels(object sender, EventArgs e) {
            UpdateSpeed();
            UpdateHeartRate();
            UpdateResistanceValue();
            UpdateLocalTime();
            UpdateLocalDate();
        }
        private void UpdateSpeed() {
            SpeedPatient.Text = $"{ergometer.getCurrentSpeed()} km/h";
        }

        private void UpdateHeartRate() {
            HeartRatePatient.Text = $"{heartRateMonitor.getCurrentHeartRate()} BPM";
        }

        private void UpdateResistanceValue() {
            Resistance.Text = $"{ergometer.getResistanceValue()} %";
        }

        private void UpdateLocalTime() {
            Time.Text = DateTime.Now.ToLongTimeString();
        }
        private void UpdateLocalDate() {
            Date.Text = DateTime.Now.ToShortDateString();
        }

        public void UpdateMessage(string message) {
            listBox1.Items.Add(message);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            //string doctorMessage = ServerConnection.getDocterMessage();
            //if (doctorMessage != null) {
            //    listBox1.Items.Add(doctorMessage);
            //}
        }
    }
}
