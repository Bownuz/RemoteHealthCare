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
            this.BackToClientenFormButton.Location = new System.Drawing.Point(15, 16);
            this.BackToClientenFormButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BackToClientenFormButton.Name = "BackToClientenFormButton";
            this.BackToClientenFormButton.Size = new System.Drawing.Size(112, 32);
            this.BackToClientenFormButton.TabIndex = 0;
            this.BackToClientenFormButton.Text = "Terug naar Cliënten";
            this.BackToClientenFormButton.UseVisualStyleBackColor = true;
            this.BackToClientenFormButton.Click += new System.EventHandler(this.BackToClientenFormButton_Click);
            // 
            // trainingChart
            // 
            this.trainingChart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.trainingChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.trainingChart.Location = new System.Drawing.Point(27, 71);
            this.trainingChart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trainingChart.Name = "trainingChart";
            this.trainingChart.Size = new System.Drawing.Size(562, 284);
            this.trainingChart.TabIndex = 1;
            this.trainingChart.Text = "Training Data Grafiek";
            // 
            // TrainingDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.trainingChart);
            this.Controls.Add(this.BackToClientenFormButton);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "TrainingDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Training Data";
            ((System.ComponentModel.ISupportInitialize)(this.trainingChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
