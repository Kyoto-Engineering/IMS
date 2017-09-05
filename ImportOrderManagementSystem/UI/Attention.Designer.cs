namespace ImportOrderManagementSystem.UI
{
    partial class Attention
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Attention));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EmailtextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.SupliercomboBox = new System.Windows.Forms.ComboBox();
            this.AttntextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Savebutton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.EmailtextBox);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.SupliercomboBox);
            this.groupBox1.Controls.Add(this.AttntextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 187);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // EmailtextBox
            // 
            this.EmailtextBox.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmailtextBox.Location = new System.Drawing.Point(203, 78);
            this.EmailtextBox.MaxLength = 90;
            this.EmailtextBox.Name = "EmailtextBox";
            this.EmailtextBox.Size = new System.Drawing.Size(274, 32);
            this.EmailtextBox.TabIndex = 2;
            this.EmailtextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EmailtextBox_KeyDown);
            this.EmailtextBox.Validating += new System.ComponentModel.CancelEventHandler(this.EmailtextBox_Validating);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(10, 137);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(193, 22);
            this.label15.TabIndex = 44;
            this.label15.Text = "Supplier                      :";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // SupliercomboBox
            // 
            this.SupliercomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SupliercomboBox.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SupliercomboBox.FormattingEnabled = true;
            this.SupliercomboBox.Location = new System.Drawing.Point(203, 132);
            this.SupliercomboBox.Name = "SupliercomboBox";
            this.SupliercomboBox.Size = new System.Drawing.Size(274, 32);
            this.SupliercomboBox.TabIndex = 3;
            this.SupliercomboBox.SelectedIndexChanged += new System.EventHandler(this.SupliercomboBox_SelectedIndexChanged_1);
            this.SupliercomboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SupliercomboBox_KeyDown);
            // 
            // AttntextBox
            // 
            this.AttntextBox.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttntextBox.Location = new System.Drawing.Point(203, 26);
            this.AttntextBox.MaxLength = 90;
            this.AttntextBox.Name = "AttntextBox";
            this.AttntextBox.Size = new System.Drawing.Size(274, 32);
            this.AttntextBox.TabIndex = 1;
            this.AttntextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AttntextBox_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(10, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 22);
            this.label2.TabIndex = 40;
            this.label2.Text = "Respondent Email      :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(10, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 22);
            this.label1.TabIndex = 39;
            this.label1.Text = "Attention                     :";
            // 
            // Savebutton
            // 
//            this.Savebutton.BackgroundImage = global::ImportOrderManagementSystem.Properties.Resources.green_button__1_;
            this.Savebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Savebutton.Location = new System.Drawing.Point(388, 271);
            this.Savebutton.Name = "Savebutton";
            this.Savebutton.Size = new System.Drawing.Size(105, 52);
            this.Savebutton.TabIndex = 1;
            this.Savebutton.Text = "Save";
            this.Savebutton.UseVisualStyleBackColor = true;
            this.Savebutton.Click += new System.EventHandler(this.Savebutton_Click);
            // 
            // Attention
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           // this.BackgroundImage = global::ImportOrderManagementSystem.Properties.Resources.Import_Order_Management_System;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(631, 422);
            this.Controls.Add(this.Savebutton);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Attention";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Attention";
            this.Load += new System.EventHandler(this.Attention_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox AttntextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox SupliercomboBox;
        private System.Windows.Forms.Button Savebutton;
        private System.Windows.Forms.TextBox EmailtextBox;
    }
}