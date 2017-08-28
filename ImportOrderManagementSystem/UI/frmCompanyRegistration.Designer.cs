namespace ImportOrderManagementSystem.UI
{
    partial class frmCompanyRegistration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompanyRegistration));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AddressrichTextBox = new System.Windows.Forms.RichTextBox();
            this.CompanyNametextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(269, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(341, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Company Registration Form";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AddressrichTextBox);
            this.groupBox1.Controls.Add(this.CompanyNametextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(127, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(603, 217);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // AddressrichTextBox
            // 
            this.AddressrichTextBox.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressrichTextBox.Location = new System.Drawing.Point(175, 89);
            this.AddressrichTextBox.MaxLength = 300;
            this.AddressrichTextBox.Name = "AddressrichTextBox";
            this.AddressrichTextBox.Size = new System.Drawing.Size(399, 96);
            this.AddressrichTextBox.TabIndex = 2;
            this.AddressrichTextBox.Text = "";
            this.AddressrichTextBox.TextChanged += new System.EventHandler(this.AddressrichTextBox_TextChanged);
            this.AddressrichTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddressrichTextBox_KeyDown);
            // 
            // CompanyNametextBox
            // 
            this.CompanyNametextBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CompanyNametextBox.Location = new System.Drawing.Point(175, 46);
            this.CompanyNametextBox.MaxLength = 250;
            this.CompanyNametextBox.Name = "CompanyNametextBox";
            this.CompanyNametextBox.Size = new System.Drawing.Size(399, 29);
            this.CompanyNametextBox.TabIndex = 1;
            this.CompanyNametextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CompanyNametextBox_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Registration Address   :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Company Name           :";
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.saveButton.BackgroundImage = global::ImportOrderManagementSystem.Properties.Resources.green_button__1_;
            this.saveButton.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.ForeColor = System.Drawing.Color.Navy;
            this.saveButton.Location = new System.Drawing.Point(436, 333);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(118, 62);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Register";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // frmCompanyRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::ImportOrderManagementSystem.Properties.Resources.Import_Order_Management_System;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(854, 443);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCompanyRegistration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCompanyRegistration";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCompanyRegistration_FormClosed);
            this.Load += new System.EventHandler(this.frmCompanyRegistration_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CompanyNametextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.RichTextBox AddressrichTextBox;
    }
}