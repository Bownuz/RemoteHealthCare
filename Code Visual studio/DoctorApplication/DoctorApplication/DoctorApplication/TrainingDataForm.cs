using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DoctorApplication {
    public partial class TrainingDataForm : Form {
        private List<TrainingData> trainingData;
        private Chart trainingChart;
        private SslStream sslStream;

        public TrainingDataForm(List<TrainingData> data, SslStream sslStream) {
            InitializeComponent();

            trainingData = data;
            this.sslStream = sslStream;
            InitializeChart();
        }

        private void BackToClientenFormButton_Click(object sender, EventArgs e) {
            ClientenForm clientenForm = new ClientenForm(sslStream);
            this.Hide();
            clientenForm.Show();
        }

        private void InitializeChart() {
            trainingChart = new Chart {
                Dock = DockStyle.Fill
            };
            ChartArea chartArea = new ChartArea("TrainingDataChartArea");
            trainingChart.ChartAreas.Add(chartArea);
            Series series = new Series("TrainingDataSeries") {
                ChartType = SeriesChartType.Line
            };
            trainingChart.Series.Add(series);
            foreach(var dataPoint in trainingData) {
                series.Points.AddXY(dataPoint.TimeStamp, dataPoint.Value);
            }
            Controls.Add(trainingChart);
        }
    }

    public class TrainingData {
        public DateTime TimeStamp { get; set; }
        public double Value { get; set; }
    }
}
