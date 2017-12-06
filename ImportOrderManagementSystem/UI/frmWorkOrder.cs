﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using ImportOrderManagementSystem.DbGateway;
using ImportOrderManagementSystem.LoginUI;
using ImportOrderManagementSystem.Reports;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using Table = CrystalDecisions.CrystalReports.Engine.Table;
using TextBox = System.Windows.Forms.TextBox;

namespace ImportOrderManagementSystem.UI
{
    public partial class frmWorkOrder : Form
    {
        const int kSplashUpdateInterval_ms = 50;
        const int kMinAmountOfSplashTime_ms = 800;
        SqlConnection _con;
        SqlCommand _cmd;
        ConnectionString _cs = new ConnectionString();
        SqlDataReader rdr;
        public string SubmittedBy, FullName, BrandCode,ImpOrderNo,attentionid=null;
        public int SupplierId, Brandid, Sio, IncoId, CurrencyId,ProductId,ImpId;
        public decimal Price;
        public bool BrandSelected, SupplierSelected, IncoTermsSelected, CurrencySelected,Exists;
        private int i;
        public string currencyid= "";
        public frmWorkOrder()
        {
            InitializeComponent();
        }

       
        private void SaveSTatus()
        {
            try
            {
                //_con = new SqlConnection(_cs.DBConn);
                //_con.Open();
                //string cb2 = "Update ImportOrders set ImportOrders.OrderByUId=@d1, OrderStatus=@d2,OrderEntryDate=@d3 where  ImportOrders.IOId='" + SupplierId + "' ";
                //_cmd = new SqlCommand(cb2,_con);
                //_cmd.Parameters.AddWithValue("@d1", SubmittedBy);
                //_cmd.Parameters.AddWithValue("@d2", "OrderComplete");
                //_cmd.Parameters.AddWithValue("@d3", System.DateTime.UtcNow.ToLocalTime());
                //_cmd.ExecuteReader();
                //_con.Close();               
                //SupliercomboBox.SelectedIndex = -1;
                //FillWOrderCombo();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        
        private void submitButton_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Please add Product Item to Chart for Order", "Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProductId.Focus();
                return;

            }


          
             
                _con = new SqlConnection(_cs.DBConn);
                string cd1 = "INSERT INTO ImportOrders (BrandId,SupplierId,ImportDate,SIO,ImportOrderNo,IncoID,CurrencyId,AttnId,Ancillary,CFRFrieght,UserId,TotalItem,TotalQty,TotalPrice) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d9,@d10,@d11,@d12,@d13,@d14,@d15)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                _cmd = new SqlCommand(cd1, _con);
                _cmd.Parameters.AddWithValue("@d1", Brandid);
                _cmd.Parameters.AddWithValue("@d2", SupplierId);
                _cmd.Parameters.AddWithValue("@d3", importOrderDate.Value.ToLocalTime());
                _cmd.Parameters.AddWithValue("@d4", Sio);
                _cmd.Parameters.AddWithValue("@d5", ImpOrderNo);
                _cmd.Parameters.AddWithValue("@d6", IncoId);

                _cmd.Parameters.AddWithValue("@d7", textBox7.Text); 
               
                _cmd.Parameters.AddWithValue("@d9", attentionid);
                _cmd.Parameters.AddWithValue("@d10", string.IsNullOrWhiteSpace(textBox3.Text) ? (object)DBNull.Value : textBox3.Text);
                _cmd.Parameters.AddWithValue("@d11", string.IsNullOrWhiteSpace(textBox2.Text) ? (object)DBNull.Value : textBox2.Text);
                _cmd.Parameters.AddWithValue("@d12", LoginForm.uId2);
                _cmd.Parameters.AddWithValue("@d13", totalItemTextBox.Text);
                _cmd.Parameters.AddWithValue("@d14", totalQuantityTextBox.Text);
                _cmd.Parameters.AddWithValue("@d15", totalPriceTextBox.Text);
                string debugSQL = _cmd.CommandText;
           
            foreach (SqlParameter param in _cmd.Parameters)
            {
                debugSQL = debugSQL.Replace(param.ParameterName, param.Value.ToString());
            }
                _con.Open();
            ImpId = (int)_cmd.ExecuteScalar();
            
                _con.Close();

                //_con.Open();
                //_cmd.ExecuteNonQuery();
                //_con.Close();
                
         
            
            try
            {
                for (int i = 0; i <= listView1.Items.Count - 1; i++)
                {
                    _con = new SqlConnection(_cs.DBConn);
                    string cd = "INSERT INTO ImportOrderProduct (ImpId,Sl,OrderQty,Price,ExpectedDateOfArrival,BacklogQty,Remarks) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7)";
                     
                    _cmd = new SqlCommand(cd,_con);
                     
                    _cmd.Parameters.AddWithValue("@d1",ImpId);                  
                    _cmd.Parameters.AddWithValue("d2", listView1.Items[i].Text);                    
                    _cmd.Parameters.AddWithValue("d3", listView1.Items[i].SubItems[2].Text);
                    _cmd.Parameters.AddWithValue("d4", listView1.Items[i].SubItems[3].Text);
                    _cmd.Parameters.AddWithValue("d5", listView1.Items[i].SubItems[4].Text);
                    _cmd.Parameters.AddWithValue("d6", listView1.Items[i].SubItems[2].Text);
                    _cmd.Parameters.AddWithValue("d7", string.IsNullOrWhiteSpace(listView1.Items[i].SubItems[5].Text) ? (object)DBNull.Value : listView1.Items[i].SubItems[5].Text);
                    
                    _con.Open();
                    _cmd.ExecuteNonQuery();
                    _con.Close();

                    //_con = new SqlConnection(_cs.DBConn);
                    //string cdy = "INSERT INTO ImportOrders(CurrencyId) VALUES(@d8)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                    //_cmd = new SqlCommand(cdy, _con);
                    //_cmd.Parameters.AddWithValue("d8", listView1.Items[i].SubItems[7].Text);

                    //_con.Open();
                    //_cmd.ExecuteNonQuery();
                    //_con.Close();

                }
                SaveSTatus();
                MessageBox.Show("Successfully Submitted.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Report();
                listView1.Items.Clear();
                dataGridViewk.Enabled = false;
                clear_textbox();

                totalItemTextBox.Clear();
                totalQuantityTextBox.Clear();
                totalPriceTextBox.Clear();
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void Report()
        {
            ParameterField paramField1 = new ParameterField();


            //creating an object of ParameterFields class
            ParameterFields paramFields1 = new ParameterFields();

            //creating an object of ParameterDiscreteValue class
            ParameterDiscreteValue paramDiscreteValue1 = new ParameterDiscreteValue();

            //set the parameter field name
            paramField1.Name = "id";

            //set the parameter value
            paramDiscreteValue1.Value = ImpId;

            //add the parameter value in the ParameterField object
            paramField1.CurrentValues.Add(paramDiscreteValue1);

            //add the parameter in the ParameterFields object
            paramFields1.Add(paramField1);
            ReportViewer f2 = new ReportViewer();
            TableLogOnInfos reportLogonInfos = new TableLogOnInfos();
            TableLogOnInfo reportLogonInfo = new TableLogOnInfo();
            ConnectionInfo reportConInfo = new ConnectionInfo();
            Tables tables = default(Tables);
            //	Table table = default(Table);
            var with1 = reportConInfo;
            with1.ServerName = "tcp:KyotoServer,49172";
            with1.DatabaseName = "ProductNRelatedDB_newforSpecialPrice";
            with1.UserID = "sa";
            with1.Password = "SystemAdministrator";

            ImportOrder cr = new ImportOrder();

            tables = cr.Database.Tables;
            foreach (Table table in tables)
            {
                reportLogonInfo = table.LogOnInfo;
                reportLogonInfo.ConnectionInfo = reportConInfo;
                table.ApplyLogOnInfo(reportLogonInfo);
            }
            f2.crystalReportViewer1.ParameterFieldInfo = paramFields1;
            f2.crystalReportViewer1.ReportSource = cr;

            this.Visible = false;

            f2.ShowDialog();
            this.Visible = true;

        }

       
       public void GetData()
        {
           
        }
       
        private void frmWorkOrder_Load(object sender, EventArgs e)
        {
            totalItemTextBox.ReadOnly = true;
            totalQuantityTextBox.ReadOnly = true;
            totalPriceTextBox.ReadOnly = true;
          
           groupBox2.Enabled = false;
            SupliercomboBox.Focus();
            //FillWOrderCombo();
            GetBrand();
            GetSuplier();
            GetData();
            GetIncoTerms();
            GetCurrency();
            SubmittedBy = LoginForm.uId2.ToString();
            //GetAttention();
        }

        private void GetAttention()
        {

            try
            {
                _con = new SqlConnection(_cs.DBConn);
                _con.Open();
                string ctt = "SELECT AttentionDetails.Attention FROM AttentionDetails WHERE AttentionDetails.SupplierId ='" + SupplierId + "' Order by AttentionDetails.Attention asc";
                _cmd = new SqlCommand(ctt);
                _cmd.Connection = _con;
                rdr = _cmd.ExecuteReader();
                while (rdr.Read())
                {
                    AttentioncomboBox.Items.Add(rdr.GetValue(0).ToString());
                }
                AttentioncomboBox.Items.Add("Not In The List");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
           
        }

        public decimal totalPrice = 0, temp = 0;
        public int totalQuantity = 0;
        public int totalItem = 0;
        public int quantity;

        private void button1_Click(object sender, EventArgs e)
        {
            //SupliercomboBox.Enabled = false;

            if (txtProductId.Text == "")
            {
                MessageBox.Show("You must select a  product first.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProductId.Focus();
                return;
            }
            if (txtItemCode.Text == "")
            {
                MessageBox.Show("Please select Item Code", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtItemCode.Focus();
                return;
            }
            if (txtOrderAmount.Text == "")
            {
                MessageBox.Show("Please enter Order Quantity.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOrderAmount.Focus();
                return;
            }
            if (txtOrderPrice.Text == "")
            {
                MessageBox.Show("Please enter Order Price.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOrderPrice.Focus();
                return;
            }

           


            try
            {
                if (listView1.Items.Count == 0)
                {
                    ListViewItem lst = new ListViewItem();
                    lst.Text=txtProductId.Text;
                    lst.SubItems.Add(txtItemCode.Text);
                    lst.SubItems.Add(txtOrderAmount.Text);
                    lst.SubItems.Add(txtOrderPrice.Text);
                    lst.SubItems.Add(eDADateTimePicker.Value.ToLocalTime().Date.ToString());
                    lst.SubItems.Add(textBox1.Text);
                    lst.SubItems.Add(textBox4.Text);
                    lst.SubItems.Add(textBox5.Text);
                    listView1.Items.Add(lst);

                    //tawhidul
                    //total item calculation
                    totalItem = ++totalItem;
                    totalItemTextBox.Text = totalItem.ToString();

                    //total quantity calculation
                    totalQuantity = totalQuantity + Convert.ToInt32(txtOrderAmount.Text);
                    totalQuantityTextBox.Text = totalQuantity.ToString();

                    //total price calculation
                    quantity = Convert.ToInt32(txtOrderAmount.Text);
                    temp = Convert.ToDecimal(txtOrderPrice.Text) * quantity;
                    totalPrice = totalPrice + temp;
                    totalPriceTextBox.Text = totalPrice.ToString();

                    ClearProducts();
                }
                else
                {
                    if (GetValue())
                    {
                        ListViewItem lst1 = new ListViewItem();
                        lst1.Text=txtProductId.Text;
                        lst1.SubItems.Add(txtItemCode.Text);
                        lst1.SubItems.Add(txtOrderAmount.Text);
                        lst1.SubItems.Add(txtOrderPrice.Text);
                        lst1.SubItems.Add(eDADateTimePicker.Value.ToLocalTime().Date.ToString());
                        lst1.SubItems.Add(textBox1.Text);
                        lst1.SubItems.Add(textBox4.Text);
                        lst1.SubItems.Add(textBox5.Text);
                        listView1.Items.Add(lst1);

                        //tawhidul
                        //total item calculation
                        totalItem = ++totalItem;
                        totalItemTextBox.Text = totalItem.ToString();

                        //total quantity calculation
                        totalQuantity = totalQuantity + Convert.ToInt32(txtOrderAmount.Text);
                        totalQuantityTextBox.Text = totalQuantity.ToString();

                        //total price calculation
                        temp = Convert.ToDecimal(txtOrderPrice.Text) * quantity;
                        totalPrice = totalPrice + temp;
                        totalPriceTextBox.Text = totalPrice.ToString();

                        ClearProducts();

                    }
                    else
                    {
                        MessageBox.Show("You Can Not Add Same Item More than one times", "error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        ClearProducts();
                    }


                }

                textBox1.Clear();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }


        private bool GetValue()
        {
            bool x = true;
            foreach (ListViewItem z in listView1.Items)
            {
                if (z.Text == txtProductId.Text)
                {
                    x = false;
                    break;
                }
            }
            return x;
        }
        private void ClearProducts()
        {
            txtProductId.Text = "";
            txtItemCode.Text = "";
            txtOrderAmount.Text = "";
            txtOrderPrice.Text = "";
            eDADateTimePicker.Value = DateTime.Now;
            textBox5.Clear();
            textBox4.Clear();
        }

        private void txtProduct_TextChanged(object sender, EventArgs e)
        {
            switch (incoCombobox.Text)
            {
                case "EXW":
                   
                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        //String sql1 = "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, EXWPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN EXWPrice ON ProductListSummary.Sl = EXWPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = EXWPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = EXWPrice.IncoID where Brand.BrandName ='" +
                        //               BrandcomboBox.Text + "' order by ProductListSummary.Sl desc";
                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, EXWPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm, SpecialPrice.SPrice FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN EXWPrice ON ProductListSummary.Sl = EXWPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = EXWPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = EXWPrice.IncoID Left Outer Join SpecialPrice on ProductListSummary.Sl = SpecialPrice.Sl  where ItemCode like '" +
                            txtProduct.Text + "%'order by ProductListSummary.Sl desc ";

                        //String sql = "SELECT ('" + sql1 + "') where ItemCode like '" + txtProduct.Text +
                        //             "%'order by ProductListSummary.Sl desc";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                   
                    break;
                
               
                case "FCA":
                    
                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, FCAPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN FCAPrice ON ProductListSummary.Sl = FCAPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = FCAPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = FCAPrice.IncoID where ItemCode like '" +
                            txtProduct.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                   
                    break;

                case "FAS":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, FASPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN FASPrice ON ProductListSummary.Sl = FASPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = FASPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = FASPrice.IncoID where ItemCode like '" +
                            txtProduct.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case "FOB":
                   
                    
                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, FOBPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN FOBPrice ON ProductListSummary.Sl = FOBPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = FOBPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = FOBPrice.IncoID where ItemCode like '" +
                            txtProduct.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }



                    break;

                case "CPT":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, CPTPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN CPTPrice ON ProductListSummary.Sl = CPTPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = CPTPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = CPTPrice.IncoID where ItemCode like '" +
                            txtProduct.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case "CFR":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, CFRPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN CFRPrice ON ProductListSummary.Sl = CFRPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = CFRPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = CFRPrice.IncoID where ItemCode like '" +
                            txtProduct.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;


                case "CIF":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, CIFPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN CIFPrice ON ProductListSummary.Sl = CIFPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = CIFPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = CIFPrice.IncoID where ItemCode like '" +
                            txtProduct.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;


                case "CIP":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, CIPPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN CIPPrice ON ProductListSummary.Sl = CIPPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = CIPPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = CIPPrice.IncoID where ItemCode like '" +
                            txtProduct.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case "DAT":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, DATPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN DATPrice ON ProductListSummary.Sl = DATPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = DATPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = DATPrice.IncoID where ItemCode like '" +
                            txtProduct.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;


                case "DAP":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, DAPPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN DAPPrice ON ProductListSummary.Sl = DAPPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = DAPPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = DAPPrice.IncoID where ItemCode like '" +
                            txtProduct.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;


                case "DDP":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, DDPPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN DDPPrice ON ProductListSummary.Sl = DDPPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = DDPPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = DDPPrice.IncoID where ItemCode like '" +
                            txtProduct.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;




                default:
                    MessageBox.Show("ERROR");
                    break;
            }
        }
        
            
        
        private void button2_Click(object sender, EventArgs e)
        {
            //SaveSTatus();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
           //this.Hide();
           // frmWorkOrder  fwo=new frmWorkOrder();
           // fwo.Show();
        }

        private void txtOrderAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtOrderPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && txtOrderPrice.Text.Contains("."))
            {
                e.Handled = true;
            }


            // allows 0-9, backspace, and decimal
            //if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            //{
            //    e.Handled = true;
            //    return;
            //}
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1)
            {
                MessageBox.Show("Please Select a row from the list which you  want to remove", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                for (int i = listView1.Items.Count - 1; i >= 0; i--)
                {
                    if (listView1.Items[i].Selected)
                    {
                        //tawhidul
                        //total item calculation
                        totalItem = --totalItem;
                        totalItemTextBox.Text = totalItem.ToString();

                        //total quantity calculation
                        totalQuantity = totalQuantity - Convert.ToInt32(listView1.Items[i].SubItems[2].Text);
                        totalQuantityTextBox.Text = totalQuantity.ToString();

                        //total price calculation
                        temp = Convert.ToDecimal(listView1.Items[i].SubItems[3].Text) * quantity;
                        totalPrice = totalPrice - temp;
                        totalPriceTextBox.Text = totalPrice.ToString();

                        listView1.Items[i].Remove();
                    }
                }
            }




        }

        private void productNameTextBox_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    _con = new SqlConnection(_cs.DBConn);
            //    _con.Open();
            //    String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ProductGenericDescription like '" + productNameTextBox.Text + "%'order by ProductListSummary.Sl desc";
            //    _cmd = new SqlCommand(sql, _con);
            //    rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //    dataGridViewk.Rows.Clear();
            //    while (rdr.Read() == true)
            //    {
            //        dataGridViewk.Rows.Add(rdr[0], rdr[1],rdr[2],rdr[3]);
            //    }
            //    _con.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}


            switch (incoCombobox.Text)
            {
                case "EXW":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        //String sql1 = "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, EXWPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN EXWPrice ON ProductListSummary.Sl = EXWPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = EXWPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = EXWPrice.IncoID where Brand.BrandName ='" +
                        //               BrandcomboBox.Text + "' order by ProductListSummary.Sl desc";
                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, EXWPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm, SpecialPrice.SPrice FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN EXWPrice ON ProductListSummary.Sl = EXWPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = EXWPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = EXWPrice.IncoID Left Outer Join SpecialPrice on ProductListSummary.Sl = SpecialPrice.Sl where ProductGenericDescription like '" +
                            productNameTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        //String sql = "SELECT ('" + sql1 + "') where ItemCode like '" + txtProduct.Text +
                        //             "%'order by ProductListSummary.Sl desc";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;


                case "FCA":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, FCAPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN FCAPrice ON ProductListSummary.Sl = FCAPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = FCAPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = FCAPrice.IncoID where ProductGenericDescription like '" +
                            productNameTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case "FAS":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, FASPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN FASPrice ON ProductListSummary.Sl = FASPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = FASPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = FASPrice.IncoID where ProductGenericDescription like '" +
                            productNameTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case "FOB":


                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, FOBPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN FOBPrice ON ProductListSummary.Sl = FOBPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = FOBPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = FOBPrice.IncoID where ProductGenericDescription like '" +
                            productNameTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }



                    break;

                case "CPT":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, CPTPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN CPTPrice ON ProductListSummary.Sl = CPTPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = CPTPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = CPTPrice.IncoID where ProductGenericDescription like '" +
                            productNameTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case "CFR":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, CFRPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN CFRPrice ON ProductListSummary.Sl = CFRPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = CFRPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = CFRPrice.IncoID where ProductGenericDescription like '" +
                            productNameTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;


                case "CIF":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, CIFPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN CIFPrice ON ProductListSummary.Sl = CIFPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = CIFPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = CIFPrice.IncoID where ProductGenericDescription like '" +
                            productNameTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;


                case "CIP":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, CIPPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN CIPPrice ON ProductListSummary.Sl = CIPPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = CIPPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = CIPPrice.IncoID where ProductGenericDescription like '" +
                            productNameTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case "DAT":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, DATPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN DATPrice ON ProductListSummary.Sl = DATPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = DATPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = DATPrice.IncoID where ProductGenericDescription like '" +
                            productNameTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;


                case "DAP":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, DAPPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN DAPPrice ON ProductListSummary.Sl = DAPPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = DAPPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = DAPPrice.IncoID where ProductGenericDescription like '" +
                            productNameTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;


                case "DDP":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, DDPPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN DDPPrice ON ProductListSummary.Sl = DDPPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = DDPPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = DDPPrice.IncoID where ProductGenericDescription like '" +
                            productNameTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;




                default:
                    MessageBox.Show("ERROR");
                    break;
            }


        }

        private void itemDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    _con = new SqlConnection(_cs.DBConn);
            //    _con.Open();
            //   String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemDescription like '" + itemDescriptionTextBox.Text + "%'order by ProductListSummary.Sl desc";
              
            //    _cmd = new SqlCommand(sql, _con);
            //    rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //    dataGridViewk.Rows.Clear();
            //    while (rdr.Read() == true)
            //    {
            //        dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
            //    }
            //    _con.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            switch (incoCombobox.Text)
            {
                case "EXW":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        //String sql1 = "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, EXWPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN EXWPrice ON ProductListSummary.Sl = EXWPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = EXWPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = EXWPrice.IncoID where Brand.BrandName ='" +
                        //               BrandcomboBox.Text + "' order by ProductListSummary.Sl desc";
                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, EXWPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm, SpecialPrice.SPrice FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN EXWPrice ON ProductListSummary.Sl = EXWPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = EXWPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = EXWPrice.IncoID Left Outer Join SpecialPrice  on ProductListSummary.Sl = SpecialPrice.Sl   where ItemDescription like '" +
                            itemDescriptionTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        //String sql = "SELECT ('" + sql1 + "') where ItemCode like '" + txtProduct.Text +
                        //             "%'order by ProductListSummary.Sl desc";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;


                case "FCA":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, FCAPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN FCAPrice ON ProductListSummary.Sl = FCAPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = FCAPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = FCAPrice.IncoID where ItemDescription like '" +
                            itemDescriptionTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case "FAS":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, FASPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN FASPrice ON ProductListSummary.Sl = FASPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = FASPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = FASPrice.IncoID where ItemDescription like '" +
                            itemDescriptionTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case "FOB":


                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, FOBPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN FOBPrice ON ProductListSummary.Sl = FOBPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = FOBPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = FOBPrice.IncoID where ItemDescription like '" +
                            itemDescriptionTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }



                    break;

                case "CPT":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, CPTPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN CPTPrice ON ProductListSummary.Sl = CPTPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = CPTPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = CPTPrice.IncoID where ItemDescription like '" +
                            itemDescriptionTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case "CFR":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, CFRPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN CFRPrice ON ProductListSummary.Sl = CFRPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = CFRPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = CFRPrice.IncoID where ItemDescription like '" +
                            itemDescriptionTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;


                case "CIF":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, CIFPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN CIFPrice ON ProductListSummary.Sl = CIFPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = CIFPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = CIFPrice.IncoID where ItemDescription like '" +
                            itemDescriptionTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;


                case "CIP":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, CIPPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN CIPPrice ON ProductListSummary.Sl = CIPPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = CIPPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = CIPPrice.IncoID where ItemDescription like '" +
                            itemDescriptionTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case "DAT":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, DATPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN DATPrice ON ProductListSummary.Sl = DATPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = DATPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = DATPrice.IncoID where ItemDescription like '" +
                            itemDescriptionTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;


                case "DAP":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, DAPPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN DAPPrice ON ProductListSummary.Sl = DAPPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = DAPPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = DAPPrice.IncoID where ItemDescription like '" +
                            itemDescriptionTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;


                case "DDP":

                    try
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        _con.Open();

                        String sql =
                            "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, DDPPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN DDPPrice ON ProductListSummary.Sl = DDPPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = DDPPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = DDPPrice.IncoID where ItemDescription like '" +
                            itemDescriptionTextBox.Text + "%'order by ProductListSummary.Sl desc ";

                        // String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                        _cmd = new SqlCommand(sql, _con);
                        rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dataGridViewk.Rows.Clear();
                        while (rdr.Read() == true)
                        {
                            dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                        }
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;




                default:
                    MessageBox.Show("ERROR");
                    break;
            }


        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void cmbWorkOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AttentioncomboBox.Items.Clear();
            AttentioncomboBox.ResetText();
            if(SupliercomboBox.SelectedIndex!=-1)
            {
                try
            {
                _con = new SqlConnection(_cs.DBConn);
                _con.Open();
                string cty4 = "SELECT SupplierId FROM Supplier WHERE SupplierName ='" + SupliercomboBox.Text + "'";
                _cmd = new SqlCommand(cty4);
                _cmd.Connection = _con;
                rdr = _cmd.ExecuteReader();
                if (rdr.Read())
                {
                    SupplierId = (rdr.GetInt32(0));
                }
                _con.Close();
                _con = new SqlConnection(_cs.DBConn);
                _con.Open();
                //string q2 = "Select SQN From RefNumForQuotation where SClientId='" + sClientIdForRefNum + "'";
                //string qr2 = "SELECT MAX(RefNumForQuotation.SQN) FROM RefNumForQuotation where SClientId='" + sClientIdForRefNum + "'";
                string qr2 = "SELECT   MAX(SIO) FROM ImportOrders where BrandId=" + Brandid +
                             " and YEAR(ImportDate)=" + importOrderDate.Value.Year ;
                _cmd = new SqlCommand(qr2, _con);
                rdr = _cmd.ExecuteReader();
                while(rdr.Read())
                { if (!rdr.IsDBNull(0))
                {
                    Sio = (rdr.GetInt32(0));
                    Sio = Sio + 1;
                    ImpOrderNo = BrandCode + importOrderDate.Value.Year.ToString().Substring(2) + "-IO-" + Sio;



                }

                else
                {
                    Sio = 1;
                    ImpOrderNo = BrandCode + importOrderDate.Value.Year.ToString().Substring(2) + "-IO-" + Sio;

                }}
                txtImportOrderNo.Text = ImpOrderNo;
                SupplierSelected = true;


                //_con = new SqlConnection(_cs.DBConn);
                //_con.Open();
                //string cty5 = "SELECT AttentionDetails.Attention FROM AttentionDetails WHERE AttentionDetails.SupplierId ='" + SupplierId + "' Order by AttentionDetails.Attention asc";
                //_cmd = new SqlCommand(cty5);
                //_cmd.Connection = _con;
                //rdr = _cmd.ExecuteReader();
                //while (rdr.Read())
                //{
                //    AttentioncomboBox.Items.Add(rdr[0]);
                //}
                //AttentioncomboBox.Items.Add("Not In The List");
                //_con.Close();
                GetAttention();
             
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }

        }

        private void txtProductId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItemCode.Focus();
                e.Handled = true;
            }
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtOrderAmount.Focus();
                e.Handled = true;
            }
        }

        private void txtOrderAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtOrderPrice.Focus();
                e.Handled = true;
            }
        }

        private void txtOrderPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
                e.Handled = true;
            }
        }

        private void frmWorkOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Visible = false;
            dynamic frm = new frmMainUI();
            frm.ShowDialog();
            this.Visible = true;  
        }


        private void dataGridViewk_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (AttentioncomboBox.SelectedIndex != -1)
            {

                if (dataGridViewk.SelectedRows.Count > 0)
                {
                    try
                    {
                        groupBox1.Enabled = false;
                        //cmbWorkOrderNo.Enabled = false;
                        DataGridViewRow dr = dataGridViewk.CurrentRow;
                        ProductId = Convert.ToInt32(dr.Cells[0].Value.ToString().Trim());
                        if (GetProductPrice())
                        {
                            txtProductId.Text = dr.Cells[0].Value.ToString();
                            txtItemCode.Text = dr.Cells[3].Value.ToString();
                            txtOrderPrice.Text = Price.ToString();
                            textBox4.Text = dr.Cells[5].Value.ToString();
                            textBox5.Text = dr.Cells[6].Value.ToString();
                            textBox6.Text = dr.Cells[5].Value.ToString();
                            textBox7.Text = dr.Cells[6].Value.ToString();
                            g.Text = k.Text;
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }

            else
            {
                MessageBox.Show("Select Attention");
            }
            
        }

        private bool GetProductPrice()
        {
            bool validate = false;
            _con = new SqlConnection(_cs.DBConn);
            _con.Open();
          
                string cty4 = "SELECT Price FROM " + incoCombobox.Text.Trim() + "Price WHERE Sl=" + ProductId+ "";
                _cmd = new SqlCommand(cty4);
                _cmd.Connection = _con;
                rdr = _cmd.ExecuteReader();
                if (rdr.Read())
                {

                    Price = Convert.ToDecimal(rdr["Price"]);
                    validate = true;
                }
                else
                {
                    MessageBox.Show(@"Product Price Is Not Given" + "\n" + @"Update product Price Using Product Management System");
                }
            _con.Close();
            return validate;
        }

        private bool PriceExists()
        {
            bool exists = false;

            var xd = new SqlConnection(_cs.DBConn);
            // ANSI SQL way.  Works in PostgreSQL, MSSQL, MySQL.  
            var cmx = new SqlCommand(
                "select case when exists((select * from information_schema.tables where table_name = '" +
                incoCombobox.Text.Trim() + "Price')) then 1 else 0 end");
            xd.Open();
            cmx.Connection = xd;

            exists = (int) cmx.ExecuteScalar() == 1;
            xd.Close();
            return exists;
        }

        private void GetIncoTerms()
        {
            try
            {
                _con = new SqlConnection(_cs.DBConn);
                _con.Open();
                string ctt = "SELECT Incoterm FROM IncoTerms";
                _cmd = new SqlCommand(ctt);
                _cmd.Connection = _con;
                rdr = _cmd.ExecuteReader();
                while (rdr.Read())
                {
                    incoCombobox.Items.Add(rdr.GetValue(0).ToString());
                }
                //cmbGender.Items.Add("Not In The List");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void GetCurrency()
        {
            try
            {
                _con = new SqlConnection(_cs.DBConn);
                _con.Open();
                string ctt = "SELECT CurrencyName FROM Currency";
                _cmd = new SqlCommand(ctt);
                _cmd.Connection = _con;
                rdr = _cmd.ExecuteReader();
                while (rdr.Read())
                {
                    currencyComboBox.Items.Add(rdr.GetValue(0).ToString());
                }
                //cmbGender.Items.Add("Not In The List");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void GetBrand()
        {
            try
            {
                _con = new SqlConnection(_cs.DBConn);
                _con.Open();
                string ctt = "select BrandName from Brand";
                _cmd = new SqlCommand(ctt);
                _cmd.Connection = _con;
                rdr = _cmd.ExecuteReader();
                while (rdr.Read())
                {
                    BrandcomboBox.Items.Add(rdr.GetValue(0).ToString());
                }
                //cmbGender.Items.Add("Not In The List");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GetSuplier()
        {
            try
            {
                _con = new SqlConnection(_cs.DBConn);
                _con.Open();
                string ctt = "SELECT SupplierName FROM Supplier";
                _cmd = new SqlCommand(ctt);
                _cmd.Connection = _con;
                rdr = _cmd.ExecuteReader();
                while (rdr.Read())
                {
                    SupliercomboBox.Items.Add(rdr.GetValue(0).ToString());
                }
                //cmbGender.Items.Add("Not In The List");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BrandcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BrandcomboBox.SelectedIndex != -1)
            {


                try
                {
                    _con = new SqlConnection(_cs.DBConn);
                    _con.Open();
                    string query1 = "Select BrandId,BrandCode From Brand where BrandName='" + BrandcomboBox.Text + "'";
                    _cmd = new SqlCommand(query1, _con);
                    rdr = _cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        Brandid = Convert.ToInt32(rdr["BrandId"]);
                        BrandCode = (rdr.GetValue(1).ToString());
                    }
                    _con.Close();

                    //_con = new SqlConnection(_cs.DBConn);
                    //_con.Open();
                    ////cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(ProductListSummary.ItemCode),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl desc", con);
                    //_cmd = new SqlCommand(
                    //    "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, EXWPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN EXWPrice ON ProductListSummary.Sl = EXWPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = EXWPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = EXWPrice.IncoID where Brand.BrandName ='" +
                    //    BrandcomboBox.Text + "' order by ProductListSummary.Sl desc", _con);
                    //rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    //dataGridViewk.Rows.Clear();
                    //while (rdr.Read() == true)
                    //{
                    //    dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                    //}

                    //_con.Close();
                    BrandSelected = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
        private void SaveReferenceNumForQuotation()
        {



        }
       
       

       

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void exwgrdload()
        {
            _con = new SqlConnection(_cs.DBConn);
            _con.Open();
            //cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(ProductListSummary.ItemCode),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl desc", con);
            _cmd = new SqlCommand(
                "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, EXWPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm,SpecialPrice.SPrice FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN EXWPrice ON ProductListSummary.Sl = EXWPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = EXWPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = EXWPrice.IncoID Left Outer Join SpecialPrice on ProductListSummary.Sl = SpecialPrice.Sl  where Brand.BrandName ='" +
                BrandcomboBox.Text + "' order by ProductListSummary.Sl desc", _con);
            rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dataGridViewk.Rows.Clear();
            while (rdr.Read() == true)
            {
                dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7],rdr[8]);
            }

            _con.Close();

        }

        private void fcagrdload()
        {
            _con = new SqlConnection(_cs.DBConn);
            _con.Open();
            //cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(ProductListSummary.ItemCode),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl desc", con);
            _cmd = new SqlCommand(
                "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, FCAPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN FCAPrice ON ProductListSummary.Sl = FCAPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = FCAPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = FCAPrice.IncoID where Brand.BrandName ='" +
                BrandcomboBox.Text + "' order by ProductListSummary.Sl desc", _con);
            rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dataGridViewk.Rows.Clear();
            while (rdr.Read() == true)
            {
                dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
            }

            _con.Close();

        }


        private void fasgrdload()
        {
            _con = new SqlConnection(_cs.DBConn);
            _con.Open();
            //cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(ProductListSummary.ItemCode),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl desc", con);
            _cmd = new SqlCommand(
                "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, FASPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN FASPrice ON ProductListSummary.Sl = FASPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = FASPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = FASPrice.IncoID where Brand.BrandName ='" +
                BrandcomboBox.Text + "' order by ProductListSummary.Sl desc", _con);
            rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dataGridViewk.Rows.Clear();
            while (rdr.Read() == true)
            {
                dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
            }

            _con.Close();

        }


        private void fobgrdload()
        {
            _con = new SqlConnection(_cs.DBConn);
            _con.Open();
            //cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(ProductListSummary.ItemCode),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl desc", con);
            _cmd = new SqlCommand(
                "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, FOBPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN FOBPrice ON ProductListSummary.Sl = FOBPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = FOBPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = FOBPrice.IncoID where Brand.BrandName ='" +
                BrandcomboBox.Text + "' order by ProductListSummary.Sl desc", _con);
            rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dataGridViewk.Rows.Clear();
            while (rdr.Read() == true)
            {
                dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
            }

            _con.Close();

        }


        private void cptgrdload()
        {
            _con = new SqlConnection(_cs.DBConn);
            _con.Open();
            //cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(ProductListSummary.ItemCode),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl desc", con);
            _cmd = new SqlCommand(
                "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, CPTPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN CPTPrice ON ProductListSummary.Sl = CPTPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = CPTPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = CPTPrice.IncoID where Brand.BrandName ='" +
                BrandcomboBox.Text + "' order by ProductListSummary.Sl desc", _con);
            rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dataGridViewk.Rows.Clear();
            while (rdr.Read() == true)
            {
                dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
            }

            _con.Close();

        }


        private void cfrgrdload()
        {
            _con = new SqlConnection(_cs.DBConn);
            _con.Open();
            //cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(ProductListSummary.ItemCode),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl desc", con);
            _cmd = new SqlCommand(
                "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, CFRPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN CFRPrice ON ProductListSummary.Sl = CFRPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = CFRPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = CFRPrice.IncoID where Brand.BrandName ='" +
                BrandcomboBox.Text + "' order by ProductListSummary.Sl desc", _con);
            rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dataGridViewk.Rows.Clear();
            while (rdr.Read() == true)
            {
                dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
            }

            _con.Close();

        }


        private void cifgrdload()
        {
            _con = new SqlConnection(_cs.DBConn);
            _con.Open();
            //cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(ProductListSummary.ItemCode),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl desc", con);
            _cmd = new SqlCommand(
                "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, CIFPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN CIFPrice ON ProductListSummary.Sl = CIFPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = CIFPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = CIFPrice.IncoID where Brand.BrandName ='" +
                BrandcomboBox.Text + "' order by ProductListSummary.Sl desc", _con);
            rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dataGridViewk.Rows.Clear();
            while (rdr.Read() == true)
            {
                dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
            }

            _con.Close();

        }

        private void cipgrdload()
        {
            _con = new SqlConnection(_cs.DBConn);
            _con.Open();
            //cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(ProductListSummary.ItemCode),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl desc", con);
            _cmd = new SqlCommand(
                "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, CIPPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN CIPPrice ON ProductListSummary.Sl = CIPPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = CIPPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = CIPPrice.IncoID where Brand.BrandName ='" +
                BrandcomboBox.Text + "' order by ProductListSummary.Sl desc", _con);
            rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dataGridViewk.Rows.Clear();
            while (rdr.Read() == true)
            {
                dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
            }

            _con.Close();

        }


        private void datgrdload()
        {
            _con = new SqlConnection(_cs.DBConn);
            _con.Open();
            //cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(ProductListSummary.ItemCode),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl desc", con);
            _cmd = new SqlCommand(
                "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, DATPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN DATPrice ON ProductListSummary.Sl = DATPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = DATPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = DATPrice.IncoID where Brand.BrandName ='" +
                BrandcomboBox.Text + "' order by ProductListSummary.Sl desc", _con);
            rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dataGridViewk.Rows.Clear();
            while (rdr.Read() == true)
            {
                dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
            }

            _con.Close();

        }

        private void dapgrdload()
        {
            _con = new SqlConnection(_cs.DBConn);
            _con.Open();
            //cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(ProductListSummary.ItemCode),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl desc", con);
            _cmd = new SqlCommand(
                "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, DAPPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN DAPPrice ON ProductListSummary.Sl = DAPPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = DAPPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = DAPPrice.IncoID where Brand.BrandName ='" +
                BrandcomboBox.Text + "' order by ProductListSummary.Sl desc", _con);
            rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dataGridViewk.Rows.Clear();
            while (rdr.Read() == true)
            {
                dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
            }

            _con.Close();

        }

        private void ddpgrdload()
        {
            _con = new SqlConnection(_cs.DBConn);
            _con.Open();
            //cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(ProductListSummary.ItemCode),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl desc", con);
            _cmd = new SqlCommand(
                "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, DDPPrice.Price, Currency.CurrencyName, Currency.CurrencyId, IncoTerms.Incoterm FROM  Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId  FULL OUTER JOIN DDPPrice ON ProductListSummary.Sl = DDPPrice.Sl FULL OUTER JOIN Currency ON Currency.CurrencyId = DDPPrice.CurrencyId FULL OUTER JOIN IncoTerms ON IncoTerms.IncoID = DDPPrice.IncoID where Brand.BrandName ='" +
                BrandcomboBox.Text + "' order by ProductListSummary.Sl desc", _con);
            rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dataGridViewk.Rows.Clear();
            while (rdr.Read() == true)
            {
                dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
            }

            _con.Close();

        }



        private void incoCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(incoCombobox.SelectedIndex!=-1)
            {
                _con = new SqlConnection(_cs.DBConn);
                _con.Open();
                string cty4 = "SELECT IncoID FROM IncoTerms WHERE Incoterm ='" + incoCombobox.Text + "'";
                _cmd = new SqlCommand(cty4);
                _cmd.Connection = _con;
                rdr = _cmd.ExecuteReader();
                if (rdr.Read())
                {

                    IncoId = (rdr.GetInt32(0));
                    IncoTermsSelected = true;
                   
                    if (PriceExists())
                    {
                        Exists = true;
                       
                    }
                   
                    else
                    {
                        MessageBox.Show(@"You Can Not Import in this Method Right Now" + "\n" +
                                        @"Please Contact With Developer");
                    }

                    if (BrandSelected && IncoTermsSelected && Exists)
                    {
                        groupBox2.Enabled = true;
                    }

                  
                }

                switch (incoCombobox.Text)
                {
                    case "EXW":
                        exwgrdload();
                        break;
                    case "FCA":
                        fcagrdload();
                        break;
                    case "FAS":
                        fasgrdload();
                        break;
                    case "FOB":
                        fobgrdload();
                        break;
                    case "CPT":
                        cptgrdload();
                        break;
                    case "CFR":
                        cfrgrdload();
                        break;
                    case "CIF":
                        cifgrdload();
                        break;
                    case "CIP":
                        cipgrdload();
                        break;
                    case "DAT":
                        datgrdload();
                        break;
                    case "DAP":
                        dapgrdload();
                        break;
                    case "DDP":
                        ddpgrdload();
                        break;
                    default:
                        MessageBox.Show("Error");
                        break;
                }
            }
        }

        private void currencyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (currencyComboBox.SelectedIndex != -1)
            {
                _con = new SqlConnection(_cs.DBConn);
                _con.Open();
                string cty4 = "SELECT CurrencyId FROM Currency where CurrencyName='" + currencyComboBox.Text + "'";
                _cmd = new SqlCommand(cty4);
                _cmd.Connection = _con;
                rdr = _cmd.ExecuteReader();
                if (rdr.Read())
                {
                    CurrencyId = (rdr.GetInt32(0));
                    CurrencySelected = true;
                    //if (BrandSelected && IncoTermsSelected && Exists)
                    //{
                    //    groupBox2.Enabled = true;
                    //}
                }

            }
        }

        private void AttentioncomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AttentioncomboBox.Text == "Not In The List")
            {
                using (var form = new Attention())
                {
                    this.Visible = false;
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string val = form.ReturnValue1; //values preserved after close
                        attentionid = form.ReturnValue2;
                        AttentioncomboBox.Items.Clear();        
                        GetAttention();
                        this.AttentioncomboBox.Text = val;
                    }
                    this.Visible = true;
                }
            }
            _con = new SqlConnection(_cs.DBConn);
            _con.Open();
            string cty4 = "SELECT AttnId FROM AttentionDetails WHERE Attention ='" + AttentioncomboBox.Text + "'";
            _cmd = new SqlCommand(cty4);
            _cmd.Connection = _con;
            rdr = _cmd.ExecuteReader();
            if (rdr.Read())
            {
                attentionid = (rdr.GetInt32(0).ToString());
            }
            _con.Close();
            
        }

        private void AttentioncomboBox_Leave(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(AttentioncomboBox.Text) && (!AttentioncomboBox.Items.Contains(AttentioncomboBox.Text)) || AttentioncomboBox.Text=="Not In The List"))
            {
                MessageBox.Show(@"Please Select Attention From List", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AttentioncomboBox.ResetText();
                //this.BeginInvoke(new ChangeFocusDelegate(changeFocus), cmbJobTitle);
                AttentioncomboBox.Focus();
            }

            if (string.IsNullOrWhiteSpace(AttentioncomboBox.Text))
            {
                attentionid = null;
            }
        }

        private void importOrderDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtImportOrderNo_TextChanged(object sender, EventArgs e)
        {

        }

       
        private void AttentioncomboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
                e.Handled = true;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                textBox3.Focus();
                e.Handled = true;
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                txtProductId.Focus();
                e.Handled = true;
            }
        }

        private void txtProductId_KeyDown_1(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                txtItemCode.Focus();
                e.Handled = true;
            }
        }

        private void txtItemCode_KeyDown_1(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                txtOrderPrice.Focus();
                e.Handled = true;
            }
        }

        private void txtOrderPrice_KeyDown_1(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                txtOrderAmount.Focus();
                e.Handled = true;
            }
        }

        private void txtOrderAmount_KeyDown_1(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                textBox1.Focus();
                e.Handled = true;
            }
        }

        private void txtOrderAmount_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if(!(char.IsDigit(e.KeyChar) || (e.KeyChar==(char)Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void txtOrderPrice_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            decimal x;
            if (ch == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && ch != '.' || !Decimal.TryParse(txtOrderPrice.Text + ch, out x))
            {
                e.Handled = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            decimal x;
            if (ch == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && ch != '.' || !Decimal.TryParse(textBox3.Text + ch, out x))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            decimal x;
            if (ch == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && ch != '.' || !Decimal.TryParse(textBox2.Text + ch, out x))
            {
                e.Handled = true;
            }
        }

        public void clear_textbox()
        {
            txtImportOrderNo.Clear();
            textBox2.Clear();
            textBox3.Clear();
            txtProductId.Clear();
            txtItemCode.Clear();
            txtOrderPrice.Clear();
            txtOrderAmount.Clear();
            textBox1.Clear();
            productNameTextBox.Clear();
            itemDescriptionTextBox.Clear();
            txtProduct.Clear();
            //BrandcomboBox.Items.Clear();
            BrandcomboBox.SelectedIndex = -1;
            //SupliercomboBox.Items.Clear();
            SupliercomboBox.SelectedIndex = -1;
            //incoCombobox.Items.Clear();
            incoCombobox.SelectedIndex = -1;
            //currencyComboBox.Items.Clear();
            currencyComboBox.SelectedIndex = -1;
            //AttentioncomboBox.Items.Clear();
            AttentioncomboBox.SelectedIndex = -1;
            importOrderDate.ResetText();
            eDADateTimePicker.ResetText();
            dataGridViewk.Rows.Clear();
            //dataGridViewk.Refresh();
            groupBox1.Enabled = true;
            groupBox2.Enabled = false;
            textBox6.Clear();
            textBox7.Clear();
        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void txtProductId_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewk_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void totalPriceTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void totalQuantitylabel_Click(object sender, EventArgs e)
        {

        }

        private void totalItemlabel_Click(object sender, EventArgs e)
        {

        }

        private void txtOrderAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {
            
        }


        public object TotalQty { get; set; }

        public object TotalItem { get; set; }

        public object TotalPrice { get; set; }

        private void txtOrderPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void eDADateTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
