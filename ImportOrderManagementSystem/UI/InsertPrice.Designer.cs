namespace ImportOrderManagementSystem.UI
{
    partial class InsertPrice
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.test_textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.incoCombobox = new System.Windows.Forms.ComboBox();
            this.EXWbutton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.test_textBox1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.incoCombobox);
            this.groupBox1.Controls.Add(this.EXWbutton);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(86, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(433, 253);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Inco Term";
            // 
            // test_textBox1
            // 
            this.test_textBox1.Location = new System.Drawing.Point(25, 101);
            this.test_textBox1.Name = "test_textBox1";
            this.test_textBox1.Size = new System.Drawing.Size(100, 29);
            this.test_textBox1.TabIndex = 6;
            this.test_textBox1.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button1.BackgroundImage = global::ImportOrderManagementSystem.Properties.Resources.expencesbuttonpnghi;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(312, 209);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 38);
            this.button1.TabIndex = 5;
            this.button1.Text = "Go";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // incoCombobox
            // 
            this.incoCombobox.BackColor = System.Drawing.SystemColors.Window;
            this.incoCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.incoCombobox.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.incoCombobox.FormattingEnabled = true;
            this.incoCombobox.Location = new System.Drawing.Point(25, 41);
            this.incoCombobox.Name = "incoCombobox";
            this.incoCombobox.Size = new System.Drawing.Size(249, 32);
            this.incoCombobox.TabIndex = 4;
            this.incoCombobox.SelectedIndexChanged += new System.EventHandler(this.incoCombobox_SelectedIndexChanged);
            // 
            // EXWbutton
            // 
            this.EXWbutton.BackColor = System.Drawing.SystemColors.Control;
            this.EXWbutton.Location = new System.Drawing.Point(6, 191);
            this.EXWbutton.Name = "EXWbutton";
            this.EXWbutton.Size = new System.Drawing.Size(75, 56);
            this.EXWbutton.TabIndex = 0;
            this.EXWbutton.Text = "EXW Price";
            this.EXWbutton.UseVisualStyleBackColor = false;
            this.EXWbutton.Visible = false;
            this.EXWbutton.Click += new System.EventHandler(this.EXWbutton_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(181, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Import Price Management";
            // 
            // InsertPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 425);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "InsertPrice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InsertPrice";
            this.Load += new System.EventHandler(this.InsertPrice_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button EXWbutton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox incoCombobox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox test_textBox1;
    }
}