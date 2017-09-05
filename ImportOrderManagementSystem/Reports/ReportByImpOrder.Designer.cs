﻿namespace ImportOrderManagementSystem.Reports
{
    partial class ReportByImpOrder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GetButton = new System.Windows.Forms.Button();
            this.ImpOrdNoComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GetButton
            // 
            this.GetButton.BackgroundImage = global::ImportOrderManagementSystem.Properties.Resources.WhiteButton;
            this.GetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GetButton.Location = new System.Drawing.Point(240, 185);
            this.GetButton.Name = "GetButton";
            this.GetButton.Size = new System.Drawing.Size(84, 36);
            this.GetButton.TabIndex = 0;
            this.GetButton.Text = "GET";
            this.GetButton.UseVisualStyleBackColor = true;
            this.GetButton.Click += new System.EventHandler(this.GetButton_Click);
            // 
            // ImpOrdNoComboBox
            // 
            this.ImpOrdNoComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImpOrdNoComboBox.FormattingEnabled = true;
            this.ImpOrdNoComboBox.Location = new System.Drawing.Point(217, 46);
            this.ImpOrdNoComboBox.Name = "ImpOrdNoComboBox";
            this.ImpOrdNoComboBox.Size = new System.Drawing.Size(149, 24);
            this.ImpOrdNoComboBox.TabIndex = 1;
            this.ImpOrdNoComboBox.SelectedIndexChanged += new System.EventHandler(this.ImpOrdNoComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Import Order No       :";
            // 
            // ReportByImpOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 273);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ImpOrdNoComboBox);
            this.Controls.Add(this.GetButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ReportByImpOrder";
            this.Text = "ReportByImpOrder";
            this.Load += new System.EventHandler(this.ReportByImpOrder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetButton;
        private System.Windows.Forms.ComboBox ImpOrdNoComboBox;
        private System.Windows.Forms.Label label1;
    }
}