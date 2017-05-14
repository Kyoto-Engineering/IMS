namespace ImportOrderManagementSystem.UI
{
    partial class frmMainUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainUI));
            this.btnWorkOrder = new System.Windows.Forms.Button();
            this.lcButton = new System.Windows.Forms.Button();
            this.registerButton = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.TextBox();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnReceiveOrder = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.deliveryOrderButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.localStoreRoomButton = new System.Windows.Forms.Button();
            this.requisitionApproval = new System.Windows.Forms.Button();
            this.setPriceButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStripContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnWorkOrder
            // 
            this.btnWorkOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnWorkOrder.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWorkOrder.ForeColor = System.Drawing.Color.Blue;
            this.btnWorkOrder.Location = new System.Drawing.Point(17, 15);
            this.btnWorkOrder.Name = "btnWorkOrder";
            this.btnWorkOrder.Size = new System.Drawing.Size(132, 59);
            this.btnWorkOrder.TabIndex = 0;
            this.btnWorkOrder.Text = "Import Order";
            this.btnWorkOrder.UseVisualStyleBackColor = false;
            this.btnWorkOrder.Click += new System.EventHandler(this.btnWorkOrder_Click);
            // 
            // lcButton
            // 
            this.lcButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lcButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcButton.ForeColor = System.Drawing.Color.Blue;
            this.lcButton.Location = new System.Drawing.Point(17, 76);
            this.lcButton.Name = "lcButton";
            this.lcButton.Size = new System.Drawing.Size(132, 60);
            this.lcButton.TabIndex = 1;
            this.lcButton.Text = "Product Order";
            this.lcButton.UseVisualStyleBackColor = false;
            this.lcButton.Click += new System.EventHandler(this.lcButton_Click);
            // 
            // registerButton
            // 
            this.registerButton.BackColor = System.Drawing.Color.Olive;
            this.registerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerButton.Location = new System.Drawing.Point(10, 525);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(142, 60);
            this.registerButton.TabIndex = 6;
            this.registerButton.Text = "User Management";
            this.registerButton.UseVisualStyleBackColor = false;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // lblUser
            // 
            this.lblUser.Location = new System.Drawing.Point(89, 739);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(100, 20);
            this.lblUser.TabIndex = 13;
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(45, 10);
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(252, 749);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(45, 10);
            this.toolStripContainer1.TabIndex = 12;
            this.toolStripContainer1.Text = "toolStripContainer1";
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 746);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Loged in As:";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Blue;
            this.textBox1.Location = new System.Drawing.Point(446, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(397, 34);
            this.textBox1.TabIndex = 9;
            this.textBox1.Text = "Inventory  Management System";
            // 
            // btnReceiveOrder
            // 
            this.btnReceiveOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnReceiveOrder.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReceiveOrder.ForeColor = System.Drawing.Color.Blue;
            this.btnReceiveOrder.Location = new System.Drawing.Point(18, 142);
            this.btnReceiveOrder.Name = "btnReceiveOrder";
            this.btnReceiveOrder.Size = new System.Drawing.Size(132, 58);
            this.btnReceiveOrder.TabIndex = 2;
            this.btnReceiveOrder.Text = "Receive Order";
            this.btnReceiveOrder.UseVisualStyleBackColor = false;
            this.btnReceiveOrder.Click += new System.EventHandler(this.btnReceiveOrder_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.deliveryOrderButton);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.localStoreRoomButton);
            this.groupBox1.Controls.Add(this.requisitionApproval);
            this.groupBox1.Controls.Add(this.setPriceButton);
            this.groupBox1.Controls.Add(this.btnWorkOrder);
            this.groupBox1.Controls.Add(this.btnReceiveOrder);
            this.groupBox1.Controls.Add(this.lcButton);
            this.groupBox1.Controls.Add(this.registerButton);
            this.groupBox1.Location = new System.Drawing.Point(36, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(166, 646);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // deliveryOrderButton
            // 
            this.deliveryOrderButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.deliveryOrderButton.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deliveryOrderButton.Location = new System.Drawing.Point(17, 443);
            this.deliveryOrderButton.Name = "deliveryOrderButton";
            this.deliveryOrderButton.Size = new System.Drawing.Size(132, 62);
            this.deliveryOrderButton.TabIndex = 8;
            this.deliveryOrderButton.Text = "Delivery Order";
            this.deliveryOrderButton.UseVisualStyleBackColor = false;
            this.deliveryOrderButton.Click += new System.EventHandler(this.deliveryOrderButton_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(17, 376);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 64);
            this.button2.TabIndex = 7;
            this.button2.Text = "Create New Feeder Stock";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // localStoreRoomButton
            // 
            this.localStoreRoomButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.localStoreRoomButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.localStoreRoomButton.ForeColor = System.Drawing.Color.Blue;
            this.localStoreRoomButton.Location = new System.Drawing.Point(17, 203);
            this.localStoreRoomButton.Name = "localStoreRoomButton";
            this.localStoreRoomButton.Size = new System.Drawing.Size(131, 56);
            this.localStoreRoomButton.TabIndex = 3;
            this.localStoreRoomButton.Text = "Local Store Room";
            this.localStoreRoomButton.UseVisualStyleBackColor = false;
            this.localStoreRoomButton.Click += new System.EventHandler(this.localStoreRoomButton_Click);
            // 
            // requisitionApproval
            // 
            this.requisitionApproval.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            //this.requisitionApproval.BackgroundImage = global::ImportOrderManagementSystem.Properties.Resources.invertor_Management_System;
            this.requisitionApproval.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requisitionApproval.Location = new System.Drawing.Point(18, 315);
            this.requisitionApproval.Name = "requisitionApproval";
            this.requisitionApproval.Size = new System.Drawing.Size(134, 55);
            this.requisitionApproval.TabIndex = 5;
            this.requisitionApproval.Text = "Requisition  Approval";
            this.requisitionApproval.UseVisualStyleBackColor = false;
            this.requisitionApproval.Click += new System.EventHandler(this.btnStore_Click);
            // 
            // setPriceButton
            // 
            this.setPriceButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.setPriceButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setPriceButton.Location = new System.Drawing.Point(19, 264);
            this.setPriceButton.Name = "setPriceButton";
            this.setPriceButton.Size = new System.Drawing.Size(129, 45);
            this.setPriceButton.TabIndex = 4;
            this.setPriceButton.Text = "Requisition";
            this.setPriceButton.UseVisualStyleBackColor = false;
            this.setPriceButton.Click += new System.EventHandler(this.setPriceButton_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button1.Location = new System.Drawing.Point(1007, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 46);
            this.button1.TabIndex = 15;
            this.button1.Text = "Log Out";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            //this.BackgroundImage = global::ImportOrderManagementSystem.Properties.Resources.ims99;
            this.ClientSize = new System.Drawing.Size(1122, 733);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMainUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMainUI";
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWorkOrder;
        private System.Windows.Forms.Button lcButton;
        private System.Windows.Forms.Button registerButton;
        public System.Windows.Forms.TextBox lblUser;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnReceiveOrder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button setPriceButton;
        private System.Windows.Forms.Button requisitionApproval;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button localStoreRoomButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button deliveryOrderButton;
    }
}