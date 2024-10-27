﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DoctorApplication {
    public partial class TrainingDataForm : Form {
        private List<TrainingData> trainingData;
        private NetworkStream networkStream;

        public TrainingDataForm(List<TrainingData> data, NetworkStream stream) {
            InitializeComponent();
            trainingData = data;
            networkStream = stream;
            InitializeChart();
        }

        private void BackToClientenFormButton_Click(object sender, EventArgs e) {
            ClientenForm clientenForm = new ClientenForm(networkStream);
            this.Hide();
            clientenForm.Show();
        }

        private void InitializeChart() {
            
            Series series = new Series("TrainingDataSeries") {
                ChartType = SeriesChartType.Line,
                XValueType = ChartValueType.DateTime
            };

            trainingChart.Series.Clear();
            trainingChart.Series.Add(series);

            foreach(var dataPoint in trainingData) {
                series.Points.AddXY(dataPoint.TimeStamp, dataPoint.Value);
            }

            trainingChart.ChartAreas[0].AxisX.Title = "Time";
            trainingChart.ChartAreas[0].AxisY.Title = "Value";
        }
    }

    public class TrainingData {
        public DateTime TimeStamp { get; set; }
        public double Value { get; set; }
    }
}
