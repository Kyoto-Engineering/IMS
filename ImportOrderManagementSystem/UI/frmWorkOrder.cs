using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImportOrderManagementSystem.DbGateway;
using ImportOrderManagementSystem.LoginUI;

namespace ImportOrderManagementSystem.UI
{
    public partial class frmWorkOrder : Form
    {
        const int kSplashUpdateInterval_ms = 50;
        const int kMinAmountOfSplashTime_ms = 800;
        SqlConnection con;
        SqlCommand cmd;
        ConnectionString cs = new ConnectionString();
        SqlDataReader rdr;
        public string submittedBy, fullName, brandCode,impOrderNo;
        public int SupplierId, brandid, SIO, IncoId;
        public frmWorkOrder()
        {
            InitializeComponent();
        }

       
        private void SaveSTatus()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb2 = "Update ImportOrder set ImportOrder.OrderByUId=@d1, OrderStatus=@d2,OrderEntryDate=@d3 where  ImportOrder.IOId='" + SupplierId + "' ";
                cmd = new SqlCommand(cb2,con);
                cmd.Parameters.AddWithValue("@d1", submittedBy);
                cmd.Parameters.AddWithValue("@d2", "OrderComplete");
                cmd.Parameters.AddWithValue("@d3", System.DateTime.UtcNow.ToLocalTime());
                cmd.ExecuteReader();
                con.Close();               
                SupliercomboBox.SelectedIndex = -1;
                FillWOrderCombo();

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

            try
            {
                for (int i = 0; i <= listView1.Items.Count - 1; i++)
                {
                    con = new SqlConnection(cs.DBConn);
                    string cd = "insert Into OrderListProduct(IOId,Sl,OrderAmount,OrderPrice,ProductStatus) VALUES (@d1,@d2,@d3,@d4,@d5)";
                    cmd = new SqlCommand(cd,con);                   
                    cmd.Parameters.AddWithValue("d1", SupplierId);                  
                    cmd.Parameters.AddWithValue("d2", listView1.Items[i].SubItems[1].Text);                    
                    cmd.Parameters.AddWithValue("d3", listView1.Items[i].SubItems[3].Text);
                    cmd.Parameters.AddWithValue("d4", listView1.Items[i].SubItems[4].Text);                    
                    cmd.Parameters.AddWithValue("d5", "NotReceived");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                   
                }
                SaveSTatus();
                MessageBox.Show("Successfully Submitted.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listView1.Items.Clear();
                dataGridViewk.Enabled = false;               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void GetData()
        {
           
        }
        public void FillWOrderCombo()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(ImportOrder.ImportOrderNo) from ImportOrder where ImportOrder.OrderStatus!='OrderComplete' order by ImportOrderNo";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    SupliercomboBox.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //static Splash splash = null;

        /// <summary>
        /// Starts the splash screen on a separate thread
        /// </summary>
        //static public void StartSplash()
        //{
        //    // Instance a splash form given the image names
        //    splash = new Splash(kSplashUpdateInterval_ms);

        //    // Run the form
        //    Application.Run(splash);
        //}

        //private void CloseSplash()
        //{
        //    if (splash == null)
        //        return;

        //    // Shut down the splash screen
        //    splash.Invoke(new EventHandler(splash.KillMe));
        //    splash.Dispose();
        //    splash = null;
        //}

        private void temp()
        {
           
        }
        private void frmWorkOrder_Load(object sender, EventArgs e)
        {
            groupBox2.Enabled = false;
            //Application.UseWaitCursor = true;
            //Thread splashThread = new Thread(new ThreadStart(StartSplash));
            //splashThread.Start();

            // Pretend that our application is doing a bunch of loading and
            // initialization
            //Thread.Sleep(kMinAmountOfSplashTime_ms / 8);

            SupliercomboBox.Focus();
            FillWOrderCombo();
            GetBrand();
            GetSuplier();
            GetData();
            GetIncoTerms();
            submittedBy = LoginForm.uId2.ToString();
            //CloseSplash();
            //Application.UseWaitCursor = false;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SupliercomboBox.Enabled = false;

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
                    lst.SubItems.Add(txtProductId.Text);
                    lst.SubItems.Add(txtItemCode.Text);
                    lst.SubItems.Add(txtOrderAmount.Text);
                    lst.SubItems.Add(txtOrderPrice.Text);
                    listView1.Items.Add(lst);                   
                    txtProductId.Text = "";
                    txtItemCode.Text = "";
                    txtOrderAmount.Text = "";
                    txtOrderPrice.Text = "";
                    return;
                }
                  String csVal = txtProductId.Text;

                if (listView1.FindItemWithText(csVal) == null)
                    {
                    ListViewItem lst1 = new ListViewItem();
                    lst1.SubItems.Add(txtProductId.Text);
                    lst1.SubItems.Add(txtItemCode.Text);
                    lst1.SubItems.Add(txtOrderAmount.Text);
                    lst1.SubItems.Add(txtOrderPrice.Text);
                    listView1.Items.Add(lst1);
                    txtProductId.Text = "";
                    txtItemCode.Text = "";
                    txtOrderAmount.Text = "";
                    txtOrderPrice.Text = "";
                    return;
                    }
                  else
                   {
                   MessageBox.Show("You Can Not Add Same Item More than one times", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  return;

                   }


              
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtProduct_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemCode like '" + txtProduct.Text + "%'order by ProductListSummary.Sl desc";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridViewk.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridViewk.Rows.Add(rdr[0], rdr[1],rdr[2],rdr[3]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        listView1.Items[i].Remove();
                    }
                }
            }
            


           
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void productNameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ProductGenericDescription like '" + productNameTextBox.Text + "%'order by ProductListSummary.Sl desc";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridViewk.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridViewk.Rows.Add(rdr[0], rdr[1],rdr[2],rdr[3]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void itemDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "SELECT ProductListSummary.Sl,ProductListSummary.ProductGenericDescription,ProductListSummary.ItemDescription,ProductListSummary.ItemCode from ProductListSummary where ItemDescription like '" + itemDescriptionTextBox.Text + "%'order by ProductListSummary.Sl desc";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridViewk.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridViewk.Rows.Add(rdr[0], rdr[1],rdr[2],rdr[3]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void cmbWorkOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cty4 = "SELECT SupplierId FROM Supplier WHERE SupplierName ='" + SupliercomboBox.Text + "'";
                cmd = new SqlCommand(cty4);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    SupplierId = (rdr.GetInt32(0));
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                //string q2 = "Select SQN From RefNumForQuotation where SClientId='" + sClientIdForRefNum + "'";
                //string qr2 = "SELECT MAX(RefNumForQuotation.SQN) FROM RefNumForQuotation where SClientId='" + sClientIdForRefNum + "'";
                string qr2 = "SELECT   MAX(SIO) FROM ImportOrders where BrandId='" + brandid +
                             " datepart(YEAR, ImportDate)'" + importOrderDate.Value.Year + "'";
                cmd = new SqlCommand(qr2, con);
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    SIO = (rdr.GetInt32(0));
                    SIO = SIO + 1;
                    impOrderNo = brandCode + importOrderDate.Value.Year.ToString("yy") + "-IO-" + SIO;



                }

                else
                {
                    SIO = 1;
                    impOrderNo = brandCode + importOrderDate.Value.Year.ToString("yy") + "-IO-" + SIO;

                }
                groupBox2.Enabled = true;
            }
            catch (Exception ex)
            {
                groupBox2.Enabled = false;
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        }


        private void dataGridViewk_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //cmbWorkOrderNo.Enabled = false;
                DataGridViewRow dr = dataGridViewk.CurrentRow;
                txtProductId.Text = dr.Cells[0].Value.ToString();
                txtItemCode.Text = dr.Cells[3].Value.ToString();
                g.Text = k.Text;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GetIncoTerms()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctt = "SELECT Incoterm FROM IncoTerms";
                cmd = new SqlCommand(ctt);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
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
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctt = "SELECT CurrencyName FROM Currency";
                cmd = new SqlCommand(ctt);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
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
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctt = "select BrandName from Brand";
                cmd = new SqlCommand(ctt);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
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
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctt = "SELECT SupplierName FROM Supplier";
                cmd = new SqlCommand(ctt);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
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
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string query1 = "Select BrandId,BrandCode From Brand where BrandName='" + BrandcomboBox.Text + "'";
                cmd = new SqlCommand(query1, con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    brandid = Convert.ToInt32(rdr["BrandId"]);
                    brandCode = (rdr.GetValue(1).ToString());
                }
                con.Close();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                //cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(ProductListSummary.ItemCode),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl desc", con);
                cmd = new SqlCommand("SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode FROM Brand INNER JOIN ProductListSummary ON Brand.BrandId = ProductListSummary.BrandId where Brand.BrandName='" + BrandcomboBox.Text + "' order by ProductListSummary.Sl desc", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridViewk.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                }
               
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
        }
        private void SaveReferenceNumForQuotation()
        {



        }

        private void SaveImportOrder()
        {
            if (Convert.ToInt32(txtClientId.Text) == sClientIdForRefNum)
            {
                con = new SqlConnection(cs.DBConn);
                string cb =
                    "insert into RefNumForQuotation(BrandCode,SClientId,SQN,QuotationId,ReferenceNo) VALUES (@d1,@d2,@d3,@d4,@d5)";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("d1", brandCode);
                cmd.Parameters.AddWithValue("d2", sClientIdForRefNum);
                cmd.Parameters.AddWithValue("d3", SIO);
                cmd.Parameters.AddWithValue("d4", quotationId);
                cmd.Parameters.AddWithValue("d5", referenceNo);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                con = new SqlConnection(cs.DBConn);
                string cb =
                    "insert into RefNumForQuotation(BrandCode,SClientId,SQN,QuotationId,ReferenceNo) VALUES (@d1,@d2,@d3,@d4,@d5)";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("d1", brandCode);
                cmd.Parameters.AddWithValue("d2", txtClientId.Text);
                cmd.Parameters.AddWithValue("d3", SIO);
                cmd.Parameters.AddWithValue("d4", quotationId);
                cmd.Parameters.AddWithValue("d5", referenceNo);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void incoCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string cty4 = "SELECT IncoID FROM IncoTerms WHERE Incoterm ='" + incoCombobox.Text + "'";
            cmd = new SqlCommand(cty4);
            cmd.Connection = con;
            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                IncoId = (rdr.GetInt32(0));
            }
        }
    }
}
