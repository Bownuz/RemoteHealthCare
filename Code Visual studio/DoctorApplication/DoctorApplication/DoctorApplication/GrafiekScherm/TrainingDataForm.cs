using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DoctorApplication {
    public partial class TrainingDataForm : Form {
        private List<TrainingData> trainingData;
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
            Series series = new Series("TrainingDataSeries") {
                ChartType = SeriesChartType.Line,
                XValueType = ChartValueType.DateTime
            };

            trainingChart.Series.Clear();
            trainingChart.Series.Add(series);
            
            foreach(var dataPoint in trainingData) {
                series.Points.AddXY(dataPoint.TimeStamp, dataPoint.Value);
            }
        }
    }
    public class TrainingData {
        public DateTime TimeStamp { get; set; }
        public double Value { get; set; }
    }
}
