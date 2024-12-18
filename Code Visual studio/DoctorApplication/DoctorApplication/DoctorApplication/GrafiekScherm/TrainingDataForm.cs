﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DoctorApplication {
    public partial class TrainingDataForm : Form {
        private List<TrainingData> trainingData;
        private ServerConnection serverConnection;

        public TrainingDataForm(List<TrainingData> data, ServerConnection connection) {
            InitializeComponent();
            trainingData = data;
            serverConnection = connection;
            InitializeChart();
        }

        private void BackToClientenFormButton_Click(object sender, EventArgs e) {
            ClientenForm clientenForm = new ClientenForm(serverConnection);
            this.Hide();
            clientenForm.Show();
        }

        private void InitializeChart() {
            if (trainingData.Count == 0) {
                MessageBox.Show("No data available to display.");
                return;
            }

            Series series = new Series("TrainingDataSeries") {
                ChartType = SeriesChartType.Line,
                XValueType = ChartValueType.DateTime
            };

            trainingChart.Series.Clear();
            trainingChart.Series.Add(series);

            foreach (var dataPoint in trainingData) {
                series.Points.AddXY(dataPoint.TimeStamp, dataPoint.Value);
            }

            trainingChart.ChartAreas[0].AxisX.Title = "Time";
            trainingChart.ChartAreas[0].AxisY.Title = "Value";

            trainingChart.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";
            trainingChart.ChartAreas[0].AxisX.Interval = 1;
            trainingChart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
        }
    }
}
