using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;


namespace DoctorApplication {
    partial class TrainingDataForm {
        private System.ComponentModel.IContainer components = null;
        private Button BackToClientenFormButton;
        private Chart trainingChart;

        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent() {
            this.BackToClientenFormButton = new System.Windows.Forms.Button();
            this.trainingChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.trainingChart)).BeginInit();
            this.SuspendLayout();

            // 
            // BackToClientenFormButton
            // 
            this.BackToClientenFormButton.BackColor = System.Drawing.Color.LightBlue;
            this.BackToClientenFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackToClientenFormButton.Location = new System.Drawing.Point(20, 20);
            this.BackToClientenFormButton.Name = "BackToClientenFormButton";
            this.BackToClientenFormButton.Size = new System.Drawing.Size(150, 40);
            this.BackToClientenFormButton.TabIndex = 0;
            this.BackToClientenFormButton.Text = "Terug naar Cliënten";
            this.BackToClientenFormButton.UseVisualStyleBackColor = true;
            this.BackToClientenFormButton.Click += new System.EventHandler(this.BackToClientenFormButton_Click);

            // 
            // trainingChart
            // 
            this.trainingChart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.trainingChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.trainingChart.Location = new System.Drawing.Point(20, 80);
            this.trainingChart.Name = "trainingChart";
            this.trainingChart.Size = new System.Drawing.Size(750, 350);
            this.trainingChart.TabIndex = 1;
            this.trainingChart.Text = "Training Data Grafiek";

            // 
            // TrainingDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.trainingChart);
            this.Controls.Add(this.BackToClientenFormButton);
            this.BackColor = System.Drawing.Color.WhiteSmoke; 
            this.Name = "TrainingDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Training Data";
            ((System.ComponentModel.ISupportInitialize)(this.trainingChart)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
