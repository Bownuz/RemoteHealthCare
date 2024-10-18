using System;

namespace DoctorApplication {
    partial class TrainingDataForm {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent() {
            this.BackToClientenFormButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BackToClientenFormButton
            // 
            this.BackToClientenFormButton.Location = new System.Drawing.Point(12, 12);
            this.BackToClientenFormButton.Name = "BackToClientenFormButton";
            this.BackToClientenFormButton.Size = new System.Drawing.Size(120, 23);
            this.BackToClientenFormButton.TabIndex = 0;
            this.BackToClientenFormButton.Text = "Terug naar Cliënten";
            this.BackToClientenFormButton.UseVisualStyleBackColor = true;
            this.BackToClientenFormButton.Click += new System.EventHandler(this.BackToClientenFormButton_Click);
            // 
            // TrainingDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BackToClientenFormButton);
            this.Name = "TrainingDataForm";
            this.Text = "TrainingDataForm";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button BackToClientenFormButton;
    }
}
