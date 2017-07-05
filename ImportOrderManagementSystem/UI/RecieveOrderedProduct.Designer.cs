namespace ImportOrderManagementSystem.UI
{
    partial class RecieveOrderedProduct
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecieveOrderedProduct));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ShipmentOrderNoTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.DeliveryDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.ShippingDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.ShippingModeComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SupplierComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DoneButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ProductDesTextBox = new System.Windows.Forms.TextBox();
            this.ProductNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.ShipingQtyTextBox = new System.Windows.Forms.TextBox();
            this.ProductCodeTextBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddedProductGroupBox = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.iOCheckBox = new System.Windows.Forms.CheckBox();
            this.pDCheckBox = new System.Windows.Forms.CheckBox();
            this.pCCheckBox = new System.Windows.Forms.CheckBox();
            this.pNCheckBox = new System.Windows.Forms.CheckBox();
            this.iOSrchBx = new System.Windows.Forms.TextBox();
            this.itmDscrptnSrchBx = new System.Windows.Forms.TextBox();
            this.itmCdSrchBx = new System.Windows.Forms.TextBox();
            this.prNmSrchBx = new System.Windows.Forms.TextBox();
            this.totalItemLabel = new System.Windows.Forms.Label();
            this.totalItemTextBox = new System.Windows.Forms.TextBox();
            this.totalQuantityLabel = new System.Windows.Forms.Label();
            this.totalQuantityTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.AddedProductGroupBox.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.ShipmentOrderNoTextBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.DeliveryDateTimePicker);
            this.groupBox1.Controls.Add(this.ShippingDateTimePicker);
            this.groupBox1.Controls.Add(this.ShippingModeComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.SupplierComboBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Shipment Info";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(63, 116);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Shipment Order No";
            // 
            // ShipmentOrderNoTextBox
            // 
            this.ShipmentOrderNoTextBox.Location = new System.Drawing.Point(177, 116);
            this.ShipmentOrderNoTextBox.Name = "ShipmentOrderNoTextBox";
            this.ShipmentOrderNoTextBox.ReadOnly = true;
            this.ShipmentOrderNoTextBox.Size = new System.Drawing.Size(191, 20);
            this.ShipmentOrderNoTextBox.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(63, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Probable Time Of Delivery";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(74, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Probable Shipping Date";
            // 
            // DeliveryDateTimePicker
            // 
            this.DeliveryDateTimePicker.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.DeliveryDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DeliveryDateTimePicker.Location = new System.Drawing.Point(214, 90);
            this.DeliveryDateTimePicker.Name = "DeliveryDateTimePicker";
            this.DeliveryDateTimePicker.Size = new System.Drawing.Size(154, 20);
            this.DeliveryDateTimePicker.TabIndex = 4;
            // 
            // ShippingDateTimePicker
            // 
            this.ShippingDateTimePicker.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.ShippingDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ShippingDateTimePicker.Location = new System.Drawing.Point(214, 10);
            this.ShippingDateTimePicker.Name = "ShippingDateTimePicker";
            this.ShippingDateTimePicker.Size = new System.Drawing.Size(154, 20);
            this.ShippingDateTimePicker.TabIndex = 1;
            // 
            // ShippingModeComboBox
            // 
            this.ShippingModeComboBox.FormattingEnabled = true;
            this.ShippingModeComboBox.Location = new System.Drawing.Point(214, 63);
            this.ShippingModeComboBox.Name = "ShippingModeComboBox";
            this.ShippingModeComboBox.Size = new System.Drawing.Size(154, 21);
            this.ShippingModeComboBox.TabIndex = 3;
            this.ShippingModeComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select Shipping Mode";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Supplier";
            // 
            // SupplierComboBox
            // 
            this.SupplierComboBox.FormattingEnabled = true;
            this.SupplierComboBox.Location = new System.Drawing.Point(214, 36);
            this.SupplierComboBox.Name = "SupplierComboBox";
            this.SupplierComboBox.Size = new System.Drawing.Size(154, 21);
            this.SupplierComboBox.TabIndex = 2;
            this.SupplierComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DoneButton);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.ProductDesTextBox);
            this.groupBox2.Controls.Add(this.ProductNameTextBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.RemoveButton);
            this.groupBox2.Controls.Add(this.AddButton);
            this.groupBox2.Controls.Add(this.ShipingQtyTextBox);
            this.groupBox2.Controls.Add(this.ProductCodeTextBox);
            this.groupBox2.Location = new System.Drawing.Point(13, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 165);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Product Info";
            // 
            // DoneButton
            // 
            this.DoneButton.Location = new System.Drawing.Point(333, 123);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(75, 23);
            this.DoneButton.TabIndex = 7;
            this.DoneButton.Text = "Done";
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Product Description";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Product Name";
            // 
            // ProductDesTextBox
            // 
            this.ProductDesTextBox.Location = new System.Drawing.Point(108, 48);
            this.ProductDesTextBox.Name = "ProductDesTextBox";
            this.ProductDesTextBox.ReadOnly = true;
            this.ProductDesTextBox.Size = new System.Drawing.Size(191, 20);
            this.ProductDesTextBox.TabIndex = 2;
            // 
            // ProductNameTextBox
            // 
            this.ProductNameTextBox.Location = new System.Drawing.Point(108, 22);
            this.ProductNameTextBox.Name = "ProductNameTextBox";
            this.ProductNameTextBox.ReadOnly = true;
            this.ProductNameTextBox.Size = new System.Drawing.Size(191, 20);
            this.ProductNameTextBox.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Shiping Quantity";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Product Code";
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(333, 46);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveButton.TabIndex = 6;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(333, 20);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 5;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // ShipingQtyTextBox
            // 
            this.ShipingQtyTextBox.Location = new System.Drawing.Point(110, 111);
            this.ShipingQtyTextBox.Name = "ShipingQtyTextBox";
            this.ShipingQtyTextBox.Size = new System.Drawing.Size(100, 20);
            this.ShipingQtyTextBox.TabIndex = 4;
            this.ShipingQtyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // ProductCodeTextBox
            // 
            this.ProductCodeTextBox.Location = new System.Drawing.Point(108, 74);
            this.ProductCodeTextBox.Name = "ProductCodeTextBox";
            this.ProductCodeTextBox.ReadOnly = true;
            this.ProductCodeTextBox.Size = new System.Drawing.Size(191, 20);
            this.ProductCodeTextBox.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(457, 102);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(717, 219);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ordered Product";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8});
            this.dataGridView1.Location = new System.Drawing.Point(6, 15);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Size = new System.Drawing.Size(705, 199);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ImpId";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Product Name";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Product Code";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Description";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 140;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Order Qty";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 60;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Backlog Qty";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 60;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "IO No ";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            dataGridViewCellStyle1.NullValue = null;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column8.HeaderText = "EDA";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // AddedProductGroupBox
            // 
            this.AddedProductGroupBox.Controls.Add(this.listView1);
            this.AddedProductGroupBox.Location = new System.Drawing.Point(457, 324);
            this.AddedProductGroupBox.Name = "AddedProductGroupBox";
            this.AddedProductGroupBox.Size = new System.Drawing.Size(611, 220);
            this.AddedProductGroupBox.TabIndex = 3;
            this.AddedProductGroupBox.TabStop = false;
            this.AddedProductGroupBox.Text = "Added Product";
            this.AddedProductGroupBox.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(7, 20);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(597, 194);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ImpId";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Product Name";
            this.columnHeader2.Width = 160;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Product Code";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Description";
            this.columnHeader4.Width = 200;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Receieved Quantity";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader5.Width = 112;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.iOCheckBox);
            this.groupBox4.Controls.Add(this.pDCheckBox);
            this.groupBox4.Controls.Add(this.pCCheckBox);
            this.groupBox4.Controls.Add(this.pNCheckBox);
            this.groupBox4.Controls.Add(this.iOSrchBx);
            this.groupBox4.Controls.Add(this.itmDscrptnSrchBx);
            this.groupBox4.Controls.Add(this.itmCdSrchBx);
            this.groupBox4.Controls.Add(this.prNmSrchBx);
            this.groupBox4.Location = new System.Drawing.Point(447, 13);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(804, 83);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Search";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(461, 46);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "I O No";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Product Description";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(427, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Product Code";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Product Name";
            // 
            // iOCheckBox
            // 
            this.iOCheckBox.AutoSize = true;
            this.iOCheckBox.Location = new System.Drawing.Point(614, 45);
            this.iOCheckBox.Name = "iOCheckBox";
            this.iOCheckBox.Size = new System.Drawing.Size(169, 17);
            this.iOCheckBox.TabIndex = 11;
            this.iOCheckBox.Text = "Hold Data For Another Search";
            this.iOCheckBox.UseVisualStyleBackColor = true;
            this.iOCheckBox.CheckedChanged += new System.EventHandler(this.iOCheckBox_CheckedChanged);
            // 
            // pDCheckBox
            // 
            this.pDCheckBox.AutoSize = true;
            this.pDCheckBox.Location = new System.Drawing.Point(229, 45);
            this.pDCheckBox.Name = "pDCheckBox";
            this.pDCheckBox.Size = new System.Drawing.Size(169, 17);
            this.pDCheckBox.TabIndex = 10;
            this.pDCheckBox.Text = "Hold Data For Another Search";
            this.pDCheckBox.UseVisualStyleBackColor = true;
            this.pDCheckBox.CheckedChanged += new System.EventHandler(this.pDCheckBox_CheckedChanged);
            // 
            // pCCheckBox
            // 
            this.pCCheckBox.AutoSize = true;
            this.pCCheckBox.Location = new System.Drawing.Point(614, 16);
            this.pCCheckBox.Name = "pCCheckBox";
            this.pCCheckBox.Size = new System.Drawing.Size(169, 17);
            this.pCCheckBox.TabIndex = 9;
            this.pCCheckBox.Text = "Hold Data For Another Search";
            this.pCCheckBox.UseVisualStyleBackColor = true;
            this.pCCheckBox.CheckedChanged += new System.EventHandler(this.pCCheckBox_CheckedChanged);
            // 
            // pNCheckBox
            // 
            this.pNCheckBox.AutoSize = true;
            this.pNCheckBox.Location = new System.Drawing.Point(229, 16);
            this.pNCheckBox.Name = "pNCheckBox";
            this.pNCheckBox.Size = new System.Drawing.Size(169, 17);
            this.pNCheckBox.TabIndex = 8;
            this.pNCheckBox.Text = "Hold Data For Another Search";
            this.pNCheckBox.UseVisualStyleBackColor = true;
            this.pNCheckBox.CheckedChanged += new System.EventHandler(this.pNCheckBox_CheckedChanged);
            // 
            // iOSrchBx
            // 
            this.iOSrchBx.Location = new System.Drawing.Point(508, 42);
            this.iOSrchBx.Name = "iOSrchBx";
            this.iOSrchBx.Size = new System.Drawing.Size(100, 20);
            this.iOSrchBx.TabIndex = 6;
            this.iOSrchBx.TextChanged += new System.EventHandler(this.iOSrchBx_TextChanged);
            // 
            // itmDscrptnSrchBx
            // 
            this.itmDscrptnSrchBx.Location = new System.Drawing.Point(114, 40);
            this.itmDscrptnSrchBx.Name = "itmDscrptnSrchBx";
            this.itmDscrptnSrchBx.Size = new System.Drawing.Size(100, 20);
            this.itmDscrptnSrchBx.TabIndex = 7;
            this.itmDscrptnSrchBx.TextChanged += new System.EventHandler(this.itmDscrptnSrchBx_TextChanged);
            // 
            // itmCdSrchBx
            // 
            this.itmCdSrchBx.Location = new System.Drawing.Point(508, 16);
            this.itmCdSrchBx.Name = "itmCdSrchBx";
            this.itmCdSrchBx.Size = new System.Drawing.Size(100, 20);
            this.itmCdSrchBx.TabIndex = 6;
            this.itmCdSrchBx.TextChanged += new System.EventHandler(this.itmCdSrchBx_TextChanged);
            // 
            // prNmSrchBx
            // 
            this.prNmSrchBx.Location = new System.Drawing.Point(114, 14);
            this.prNmSrchBx.Name = "prNmSrchBx";
            this.prNmSrchBx.Size = new System.Drawing.Size(100, 20);
            this.prNmSrchBx.TabIndex = 5;
            this.prNmSrchBx.TextChanged += new System.EventHandler(this.prNmSrchBx_TextChanged);
            // 
            // totalItemLabel
            // 
            this.totalItemLabel.AutoSize = true;
            this.totalItemLabel.Location = new System.Drawing.Point(22, 334);
            this.totalItemLabel.Name = "totalItemLabel";
            this.totalItemLabel.Size = new System.Drawing.Size(54, 13);
            this.totalItemLabel.TabIndex = 5;
            this.totalItemLabel.Text = "Total Item";
            // 
            // totalItemTextBox
            // 
            this.totalItemTextBox.Location = new System.Drawing.Point(79, 331);
            this.totalItemTextBox.Name = "totalItemTextBox";
            this.totalItemTextBox.Size = new System.Drawing.Size(100, 20);
            this.totalItemTextBox.TabIndex = 6;
            // 
            // totalQuantityLabel
            // 
            this.totalQuantityLabel.AutoSize = true;
            this.totalQuantityLabel.Location = new System.Drawing.Point(214, 334);
            this.totalQuantityLabel.Name = "totalQuantityLabel";
            this.totalQuantityLabel.Size = new System.Drawing.Size(73, 13);
            this.totalQuantityLabel.TabIndex = 5;
            this.totalQuantityLabel.Text = "Total Quantity";
            // 
            // totalQuantityTextBox
            // 
            this.totalQuantityTextBox.Location = new System.Drawing.Point(304, 331);
            this.totalQuantityTextBox.Name = "totalQuantityTextBox";
            this.totalQuantityTextBox.Size = new System.Drawing.Size(100, 20);
            this.totalQuantityTextBox.TabIndex = 6;
            // 
            // RecieveOrderedProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 556);
            this.Controls.Add(this.totalQuantityTextBox);
            this.Controls.Add(this.totalQuantityLabel);
            this.Controls.Add(this.totalItemTextBox);
            this.Controls.Add(this.totalItemLabel);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.AddedProductGroupBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RecieveOrderedProduct";
            this.Text = "RecieveOrderedProduct";
            this.Load += new System.EventHandler(this.RecieveOrderedProduct_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.AddedProductGroupBox.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox AddedProductGroupBox;
        private System.Windows.Forms.ComboBox SupplierComboBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.ComboBox ShippingModeComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.TextBox ShipingQtyTextBox;
        private System.Windows.Forms.TextBox ProductCodeTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ProductDesTextBox;
        private System.Windows.Forms.TextBox ProductNameTextBox;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button DoneButton;
        private System.Windows.Forms.DateTimePicker DeliveryDateTimePicker;
        private System.Windows.Forms.DateTimePicker ShippingDateTimePicker;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox ShipmentOrderNoTextBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox itmCdSrchBx;
        private System.Windows.Forms.TextBox prNmSrchBx;
        private System.Windows.Forms.TextBox iOSrchBx;
        private System.Windows.Forms.TextBox itmDscrptnSrchBx;
        private System.Windows.Forms.CheckBox pDCheckBox;
        private System.Windows.Forms.CheckBox pCCheckBox;
        private System.Windows.Forms.CheckBox pNCheckBox;
        private System.Windows.Forms.CheckBox iOCheckBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label totalItemLabel;
        private System.Windows.Forms.TextBox totalItemTextBox;
        private System.Windows.Forms.Label totalQuantityLabel;
        private System.Windows.Forms.TextBox totalQuantityTextBox;
    }
}