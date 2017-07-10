﻿namespace ImportOrderManagementSystem.Reports
{
    partial class ReportByShipAck
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
            this.ShipmentIdComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GetButton
            // 
            this.GetButton.Location = new System.Drawing.Point(227, 194);
            this.GetButton.Name = "GetButton";
            this.GetButton.Size = new System.Drawing.Size(75, 23);
            this.GetButton.TabIndex = 0;
            this.GetButton.Text = "GET";
            this.GetButton.UseVisualStyleBackColor = true;
            this.GetButton.Click += new System.EventHandler(this.GetButton_Click);
            // 
            // ShipmentIdComboBox
            // 
            this.ShipmentIdComboBox.FormattingEnabled = true;
            this.ShipmentIdComboBox.Location = new System.Drawing.Point(236, 43);
            this.ShipmentIdComboBox.Name = "ShipmentIdComboBox";
            this.ShipmentIdComboBox.Size = new System.Drawing.Size(121, 21);
            this.ShipmentIdComboBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Shipment Id     :";
            // 
            // ReportByShipAck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 308);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ShipmentIdComboBox);
            this.Controls.Add(this.GetButton);
            this.Name = "ReportByShipAck";
            this.Text = "ReportByShipmentAcknowledgement";
            this.Load += new System.EventHandler(this.ReportByShipAck_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetButton;
        private System.Windows.Forms.ComboBox ShipmentIdComboBox;
        private System.Windows.Forms.Label label1;
    }
}