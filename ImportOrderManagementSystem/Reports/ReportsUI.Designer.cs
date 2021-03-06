﻿namespace ImportOrderManagementSystem.Reports
{
    partial class ReportsUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsUI));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ShipAckButton = new System.Windows.Forms.Button();
            this.ShipOrdButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ShipAckButton);
            this.groupBox1.Controls.Add(this.ShipOrdButton);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 341);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // ShipAckButton
            // 
            this.ShipAckButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ShipAckButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ShipAckButton.BackgroundImage")));
            this.ShipAckButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShipAckButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ShipAckButton.Location = new System.Drawing.Point(341, 30);
            this.ShipAckButton.Name = "ShipAckButton";
            this.ShipAckButton.Size = new System.Drawing.Size(166, 52);
            this.ShipAckButton.TabIndex = 2;
            this.ShipAckButton.Text = "Shipment Acknowledgement";
            this.ShipAckButton.UseVisualStyleBackColor = false;
            this.ShipAckButton.Click += new System.EventHandler(this.ShipAckButton_Click);
            // 
            // ShipOrdButton
            // 
            this.ShipOrdButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ShipOrdButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ShipOrdButton.BackgroundImage")));
            this.ShipOrdButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShipOrdButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ShipOrdButton.Location = new System.Drawing.Point(172, 30);
            this.ShipOrdButton.Name = "ShipOrdButton";
            this.ShipOrdButton.Size = new System.Drawing.Size(151, 52);
            this.ShipOrdButton.TabIndex = 1;
            this.ShipOrdButton.Text = "Shipment Order";
            this.ShipOrdButton.UseVisualStyleBackColor = false;
            this.ShipOrdButton.Click += new System.EventHandler(this.ShipOrdButton_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.Location = new System.Drawing.Point(26, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 52);
            this.button2.TabIndex = 0;
            this.button2.Text = "Import Order";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.LightSteelBlue;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.Crimson;
            this.CloseButton.Location = new System.Drawing.Point(509, 12);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 36);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ReportsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(598, 407);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "ReportsUI";
            this.Text = "ReportsUI";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ReportsUI_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ShipAckButton;
        private System.Windows.Forms.Button ShipOrdButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button CloseButton;
    }
}