﻿using ClientApplication.State;
using ConnectionImplemented;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApplication {
    internal partial class ClientInfoScreen : UserControl {
        private Ergometer ergometer;
        private HeartRateMonitor heartRateMonitor;
        private DataHandler dataHandler;
        private Form mainForm;
        private Timer updateTimer;
        private NetworkHandler handler;
        public ClientInfoScreen(DataHandler dataHandler, Form mainForm, Ergometer ergometer, HeartRateMonitor heartRateMonitor, NetworkHandler handler) {
            InitializeComponent();
            this.mainForm = mainForm;
            this.ergometer = ergometer;
            this.heartRateMonitor = heartRateMonitor;
            this.dataHandler = dataHandler;
            this.handler = handler;

            this.handler.NewDoctorMessage += UpdateMessage;
            Task.Run(() => this.handler.HandleNetworkThread());
            InitializeTimer();
        }

        private void UpdateMessage(string message) {
            listBox1.Invoke((MethodInvoker)delegate {
                listBox1.Items.Add(message);
                listBox1.TopIndex = listBox1.Items.Count - 1;
            });
        }


        private void SpeedPatientLabel(object sender, EventArgs e) {
        }

        private void HeartRatePatientLabel(object sender, EventArgs e) {
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
        }
    }
}
