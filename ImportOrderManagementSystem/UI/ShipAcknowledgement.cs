using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImportOrderManagementSystem.DbGateway;
using ImportOrderManagementSystem.LoginUI;
using CrystalDecisions.Shared;
using ImportOrderManagementSystem.Reports;
using CrystalDecisions.CrystalReports.Engine;

namespace ImportOrderManagementSystem.UI
{
    public partial class ShipAcknowledgement : Form
    {
        private SqlCommand cmd;
        ConnectionString Cs=new ConnectionString();
        private SqlConnection con;
        private SqlDataReader rdr;
        private string ShipmentProductId;
        private DataGridViewRow dr;
        private string checkvalue;
        private int smId;
        private string ImportOrderProductId;
        private string Sl;
        private string backlogQty;

        public ShipAcknowledgement()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SupplierComboBox.SelectedIndex != -1)
            {
                dataGridView1.Rows.Clear();
                con = new SqlConnection(Cs.DBConn);
                string qry =
                    "SELECT ShipmentProduct.ShipmentProductId, ImportOrderProduct.ImportOrderProductId, ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemCode,ProductListSummary.ItemDescription, ShipmentProduct.ShipmentProductQty, ImportOrderProduct.BacklogQty, ImportOrders.ImportOrderNo FROM ShipmentProduct INNER JOIN ImportOrderProduct ON ShipmentProduct.ImportOrderProductId = ImportOrderProduct.ImportOrderProductId INNER JOIN ProductListSummary ON ImportOrderProduct.Sl = ProductListSummary.Sl INNER JOIN ImportOrders ON ImportOrderProduct.ImpId = ImportOrders.ImpId INNER JOIN ShipmentOrder ON ShipmentProduct.ShipmentId = ShipmentOrder.ShipmentId where ShipmentOrder.ShipmentOrderNo='" +
                    SupplierComboBox.Text+"'";
                cmd = new SqlCommand(qry, con);
                con.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7],rdr[8]);
                }
                con.Close();
                
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                groupBox5.Enabled = true;
                groupBox6.Enabled = true;
                
            }
        }

        private void ShipAcknowledgement_Load(object sender, EventArgs e)
        {
            totalItemTextBox.ReadOnly = true;
            totalQuantityTextBox.ReadOnly = true;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox5.Enabled = false;
            groupBox6.Enabled = false;
            
            LoadSupplyOrder();
        }

        private void LoadSupplyOrder()
        {
            con = new SqlConnection(Cs.DBConn);
            string qry =
                "SELECT ShipmentOrder.ShipmentOrderNo FROM ShipmentOrder where ShipmentId not in (select ShipmentId from ShipmentAcknowledgement)";
            cmd = new SqlCommand(qry, con);
            con.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                SupplierComboBox.Items.Add(rdr[0]);
            }
            con.Close();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
               dr = dataGridView1.SelectedRows[0];
                ShipmentProductId = dr.Cells[0].Value.ToString();
                Sl = dr.Cells[2].Value.ToString();
                ProductDesTextBox.Text = dr.Cells[5].Value.ToString();
                ImportOrderProductId = dr.Cells[1].Value.ToString();
                ProductNameTextBox.Text = dr.Cells[3].Value.ToString();
                ProductCodeTextBox.Text = dr.Cells[4].Value.ToString();
                ShipingQtyTextBox.Text = dr.Cells[6].Value.ToString();
                checkvalue =dr.Cells[6].Value.ToString();
                backlogQty = dr.Cells[7].Value.ToString();
            }
            else
            {
                MessageBox.Show(@"Select Something first");
            }
        }

        public int totalQuantity = 0, totalItem=0;
        private int _shId;

        private void button1_Click(object sender, EventArgs e)
        {
           if (string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text) && string.IsNullOrEmpty(textBox3.Text) && string.IsNullOrEmpty(textBox4.Text) && string.IsNullOrEmpty(textBox5.Text))
           {
                MessageBox.Show("Fill Up at least one of Above Information");
            }
            else
            { 
                if (string.IsNullOrEmpty(ProductCodeTextBox.Text))
                {
                    MessageBox.Show("Select A Product First");
                }
                else if (string.IsNullOrWhiteSpace(ShipingQtyTextBox.Text) || Convert.ToInt32(ShipingQtyTextBox.Text) < 1)
                {
                    MessageBox.Show("Product With Zero , MInus or Empty Quantity Can Not Be Added");
                }
                else if (Convert.ToInt32(ShipingQtyTextBox.Text) > Convert.ToInt32(checkvalue))
                {
                    MessageBox.Show("Receive Amount Cannot Be greater Than the Order Quantity");
                }
                else
                {


                    if (listView1.Items.Count < 1)
                    {
                        ListViewItem l1 = new ListViewItem();
                        l1.Text = ShipmentProductId;
                        l1.SubItems.Add(ImportOrderProductId);
                        l1.SubItems.Add(Sl);
                        l1.SubItems.Add(ProductNameTextBox.Text);
                        l1.SubItems.Add(ProductCodeTextBox.Text);
                        l1.SubItems.Add(ProductDesTextBox.Text);
                        l1.SubItems.Add(ShipingQtyTextBox.Text);
                        l1.SubItems.Add(backlogQty);
                        l1.SubItems.Add(checkvalue);
                        listView1.Items.Add(l1);

                        //tawhidul
                        //total item calculation
                        totalItem = ++totalItem;
                        totalItemTextBox.Text = totalItem.ToString();

                        //total quantity calculation
                        totalQuantity = Convert.ToInt32(totalQuantity + ShipingQtyTextBox.Text);
                        totalQuantityTextBox.Text = totalQuantity.ToString();

                        groupBox1.Enabled = false;
                        ClearselectedProduct();
                    }
                    else
                    {

                        if (GetValue())
                        {
                            ListViewItem l2 = new ListViewItem();
                            l2.Text = ShipmentProductId;
                            l2.SubItems.Add(ImportOrderProductId);
                            l2.SubItems.Add(Sl);
                            l2.SubItems.Add(ProductNameTextBox.Text);
                            l2.SubItems.Add(ProductCodeTextBox.Text);
                            l2.SubItems.Add(ProductDesTextBox.Text);
                            l2.SubItems.Add(ShipingQtyTextBox.Text);
                            l2.SubItems.Add(backlogQty);
                            l2.SubItems.Add(checkvalue);
                            listView1.Items.Add(l2);

                            //tawhidul
                            //total item calculation
                            totalItem = ++totalItem;
                            totalItemTextBox.Text = totalItem.ToString();

                            //total quantity calculation
                            totalQuantity = totalQuantity + Convert.ToInt32(ShipingQtyTextBox.Text);
                            totalQuantityTextBox.Text = totalQuantity.ToString();

                            ClearselectedProduct();
                        }
                        else
                        {
                            MessageBox.Show("This Prduct already Added");
                            ClearselectedProduct();
                        }
                    }
                }
           
            }
                
        }

        private bool GetValue()
        {
            bool x = true;
            foreach (ListViewItem z in listView1.Items)
            {
                if (z.Text == ShipmentProductId)
                {
                    x = false;
                    break;
                }
            }
            return x;
        }

        private void ClearselectedProduct()
        {
            ShipmentProductId = null;
            ImportOrderProductId = null;
            Sl = null;
            checkvalue = null;
            backlogQty = null;
            ProductCodeTextBox.Clear();
            ShipingQtyTextBox.Clear();
            ProductDesTextBox.Clear();
            ProductNameTextBox.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ProductCodeTextBox.Text))
            {
                if (listView1.Items.Count > 0)
                {
                    string Sid=null;
                    con = new SqlConnection(Cs.DBConn);
                    string qry =
                        "SELECT  ShipmentId  FROM ShipmentOrder where ShipmentOrderNo='"+SupplierComboBox.Text+"'";
                    cmd = new SqlCommand(qry, con);
                    con.Open();
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        Sid = rdr["ShipmentId"].ToString();

                    }
                    con.Close();
                    con = new SqlConnection(Cs.DBConn);
                    string q1 =
                        "INSERT INTO ShipmentAcknowledgement (ShipmentId,UserId,EntryDate,AirWayBill,BillofLadding,TruckChallan,CIN,PL) VALUES(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                    cmd = new SqlCommand(q1, con);
                    cmd.Parameters.AddWithValue("@d1", Sid);
                    cmd.Parameters.AddWithValue("@d2", LoginForm.uId2);
                    cmd.Parameters.AddWithValue("@d3", DateTime.UtcNow.ToLocalTime());
                    cmd.Parameters.AddWithValue("@d4", string.IsNullOrWhiteSpace(textBox1.Text)?(object)DBNull.Value:textBox1.Text);
                    cmd.Parameters.AddWithValue("@d5", string.IsNullOrWhiteSpace(textBox2.Text) ? (object)DBNull.Value : textBox2.Text);
                    cmd.Parameters.AddWithValue("@d6", string.IsNullOrWhiteSpace(textBox3.Text) ? (object)DBNull.Value : textBox3.Text);
                    cmd.Parameters.AddWithValue("@d7", string.IsNullOrWhiteSpace(textBox4.Text) ? (object)DBNull.Value : textBox4.Text);
                    cmd.Parameters.AddWithValue("@d8", string.IsNullOrWhiteSpace(textBox5.Text) ? (object)DBNull.Value : textBox5.Text);
                    con.Open();
                    _shId = (int) cmd.ExecuteScalar();
                    con.Close();
                    for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    {
                        string shiprno = listView1.Items[i].Text;
                       int qty =Convert.ToInt32(listView1.Items[i].SubItems[6].Text);
                        int orderedqty=Convert.ToInt32(listView1.Items[i].SubItems[8].Text);
                        string Pno = listView1.Items[i].SubItems[2].Text;
                        string query =
                            "INSERT INTO ReceivedProduct (ShipmentProductId,ShipmentAId,RecievedQuantity) Values(@d1,@d2,@d3)";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@d1", shiprno);
                        cmd.Parameters.AddWithValue("@d2", _shId );
                        cmd.Parameters.AddWithValue("@d3", qty);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        if (orderedqty>qty)
                        {
                            string imprid = listView1.Items[i].SubItems[1].Text;
                            string query2 =
                                "UPDATE ImportOrderProduct SET BacklogQty = BacklogQty + @d1 WHERE (ImportOrderProductId = @d2)";
                            cmd = new SqlCommand(query2, con);
                            cmd.Parameters.AddWithValue("@d1", (orderedqty-qty));
                            cmd.Parameters.AddWithValue("@d2", imprid);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        con = new SqlConnection(Cs.DBConn);
                        string Sqry =
                            "SELECT Sl FROM MasterStocks1 where Sl='" + Pno + "'";
                        cmd = new SqlCommand(Sqry, con);
                        con.Open();
                        rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            con.Close();
                            string query2 =
                                "UPDATE MasterStocks1 SET MQuantity = MQuantity+@d1 WHERE (Sl = @d2)";
                            cmd = new SqlCommand(query2, con);
                            cmd.Parameters.AddWithValue("@d1",  qty);
                            cmd.Parameters.AddWithValue("@d2", Pno);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                        }
                        else
                        {
                            con.Close();
                            string inquery =
                                "INSERT INTO MasterStocks1 (MQuantity, Sl) VALUES (@d1,@d2)";
                            cmd = new SqlCommand(inquery, con);
                            cmd.Parameters.AddWithValue("@d1", qty);
                            cmd.Parameters.AddWithValue("@d2", Pno );
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                       
                    }
                    con = new SqlConnection(Cs.DBConn);
                    string Snqry =
                        "SELECT ImportOrderProductId, ShipmentProductQty FROM ShipmentProduct WHERE (ShipmentId =" + Sid + " ) and ShipmentProductId not in ( select ShipmentProductId from ReceivedProduct)";
                    cmd = new SqlCommand(Snqry, con);
                    con.Open();
                    rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        Dictionary<int, int> xDictionary = new Dictionary<int, int>();
                        //con.Close();
                        //string Stqry =
                        //    "SELECT ImportOrderProductId, ShipmentProductQty FROM ShipmentProduct WHERE (ShipmentId = 1) and ShipmentProductId not in ( select ShipmentProductId from ReceivedProduct)";
                        //cmd = new SqlCommand(Stqry, con);
                        //con.Open();
                        //rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            xDictionary.Add(Convert.ToInt32(rdr["ImportOrderProductId"]),
                                Convert.ToInt32(rdr["ShipmentProductQty"]));
                        }
                        con.Close();
                        foreach (KeyValuePair<int,int> xPair in xDictionary)
                        {

                            
                            string query2 =
                                "UPDATE ImportOrderProduct SET BacklogQty = BacklogQty + @d1 WHERE (ImportOrderProductId = @d2)";
                            cmd = new SqlCommand(query2, con);
                            cmd.Parameters.AddWithValue("@d1", xPair.Value);
                            cmd.Parameters.AddWithValue("@d2", xPair.Key);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        
                    }
                    MessageBox.Show("Shipment Taking Done");
                    Report();
                    ////////////
                    totalItemTextBox.Clear();
                    totalQuantityTextBox.Clear();
                    ClearTextbox();
                    LoadSupplyOrder();
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = false;
                    groupBox6.Enabled = false;
                    groupBox5.Enabled = false;
                   // dataGridView1.Refresh();
                    groupBox3.Enabled = false;
                    groupBox4.Enabled = false;

                }
                else
                {
                    MessageBox.Show("No Pruduct Added");
                }

            }
            else
            {
                MessageBox.Show("May be You forgot to add Last Selected Product\r\n Add The Product");
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
            paramField1.Name = "ShipmentId";

            //set the parameter value
            paramDiscreteValue1.Value = _shId;

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

            ShipmentAcknowledgement cr = new ShipmentAcknowledgement();

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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //con=new SqlConnection(Cs.DBConn);
            //string query = "SELECT SMId FROM ShipMood where ShippingMood='" + comboBox2.Text + "'";
            //cmd=new SqlCommand(query,con);
            //con.Open();
            //rdr=cmd.ExecuteReader();
            //if (rdr.Read())
            //{
            //    smId = Convert.ToInt32(rdr["SMId"]);
            //}
            //con.Close();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        private void ShipingQtyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void ShipingQtyTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ShipingQtyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar)) || e.KeyChar==(char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void RemoveButton_Click_1(object sender, EventArgs e)
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
                        totalQuantity = totalQuantity - Convert.ToInt32(listView1.Items[i].SubItems[6].Text);
                        totalQuantityTextBox.Text = totalQuantity.ToString();

                        listView1.Items[i].Remove();
                    }
                }
            }
        }
        private void prNmSrchBx_TextChanged(object sender, EventArgs e)
        {
            if (!iOCheckBox.Checked && !pCCheckBox.Checked && !pDCheckBox.Checked)
            {
                iOSrchBx.TextChanged -= iOSrchBx_TextChanged;
                iOSrchBx.Clear();
                iOSrchBx.TextChanged += iOSrchBx_TextChanged;

                itmDscrptnSrchBx.TextChanged -= itmDscrptnSrchBx_TextChanged;
                itmDscrptnSrchBx.Clear();
                itmDscrptnSrchBx.TextChanged += itmDscrptnSrchBx_TextChanged;

                itmCdSrchBx.TextChanged -= itmCdSrchBx_TextChanged;
                itmCdSrchBx.Clear();
                itmCdSrchBx.TextChanged += itmCdSrchBx_TextChanged;

                SearchSingle(prNmSrchBx.Text, prNmSrchBx);
            }
            else
            {
                SearchMulti();
            }

        }

        private void SearchMulti()
        {
            string SearchModifier1 = "", SearchModifier2 = "", SearchModifier3 = "", SearchModifier4 = "";
            if (pNCheckBox.Checked)
            {
                SearchModifier1 = " and ProductListSummary.ProductGenericDescription like '" + prNmSrchBx.Text + "%' ";
            }
            else
            {
                SearchModifier1 = "";
            }
            if (pCCheckBox.Checked)
            {
                SearchModifier2 = " and ProductListSummary.ItemCode like '" + itmCdSrchBx.Text + "%' ";
            }
            else
            {
                SearchModifier2 = "";
            }
            if (pDCheckBox.Checked)
            {
                SearchModifier3 = "  and ProductListSummary.ItemDescription like '" + itmDscrptnSrchBx.Text + "%' ";
            }
            else
            {
                SearchModifier3 = "";
            }
            if (iOCheckBox.Checked)
            {
                SearchModifier4 = " and  ImportOrders.ImportOrderNo like '" + iOSrchBx.Text + "%' ";
            }
            else
            {
                SearchModifier4 = "";
            }
            if (SupplierComboBox.SelectedIndex != -1)
            {
                dataGridView1.Rows.Clear();
                con = new SqlConnection(Cs.DBConn);
                string qry =
                    "SELECT ShipmentProduct.ShipmentProductId, ImportOrderProduct.ImportOrderProductId, ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemCode,ProductListSummary.ItemDescription, ShipmentProduct.ShipmentProductQty, ImportOrderProduct.BacklogQty, ImportOrders.ImportOrderNo FROM ShipmentProduct INNER JOIN ImportOrderProduct ON ShipmentProduct.ImportOrderProductId = ImportOrderProduct.ImportOrderProductId INNER JOIN ProductListSummary ON ImportOrderProduct.Sl = ProductListSummary.Sl INNER JOIN ImportOrders ON ImportOrderProduct.ImpId = ImportOrders.ImpId INNER JOIN ShipmentOrder ON ShipmentProduct.ShipmentId = ShipmentOrder.ShipmentId where ShipmentOrder.ShipmentOrderNo='" +
                    SupplierComboBox.Text + "' " + SearchModifier1 + SearchModifier2 + SearchModifier3 + SearchModifier4;
                cmd = new SqlCommand(qry, con);
                con.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7], rdr[8]);
                }
                con.Close();
            }
        }
        private void SearchSingle(string searchText, Control searchControl)
        {
            string SearchModifier = null;
            if (searchControl == prNmSrchBx)
            {
                SearchModifier = "ProductListSummary.ProductGenericDescription";
            }
            else if (searchControl == itmCdSrchBx)
            {
                SearchModifier = "ProductListSummary.ItemCode";
            }
            else if (searchControl == itmDscrptnSrchBx)
            {
                SearchModifier = "ProductListSummary.ItemDescription";
            }
            else if (searchControl == iOSrchBx)
            {
                SearchModifier = "ImportOrders.ImportOrderNo";
            }
            string totalsearchtext = SearchModifier + " like '" + searchText + "%' Order by " + SearchModifier;
            if (SupplierComboBox.SelectedIndex != -1)
            {
                dataGridView1.Rows.Clear();
                con = new SqlConnection(Cs.DBConn);
                string qry = "SELECT ShipmentProduct.ShipmentProductId, ImportOrderProduct.ImportOrderProductId, ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemCode,ProductListSummary.ItemDescription, ShipmentProduct.ShipmentProductQty, ImportOrderProduct.BacklogQty, ImportOrders.ImportOrderNo FROM ShipmentProduct INNER JOIN ImportOrderProduct ON ShipmentProduct.ImportOrderProductId = ImportOrderProduct.ImportOrderProductId INNER JOIN ProductListSummary ON ImportOrderProduct.Sl = ProductListSummary.Sl INNER JOIN ImportOrders ON ImportOrderProduct.ImpId = ImportOrders.ImpId INNER JOIN ShipmentOrder ON ShipmentProduct.ShipmentId = ShipmentOrder.ShipmentId where ShipmentOrder.ShipmentOrderNo='" + 
                    SupplierComboBox.Text + "' and " + totalsearchtext;
                cmd = new SqlCommand(qry, con);
                con.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7], rdr[8]);
                }
                con.Close();
            }
        }

        private void itmCdSrchBx_TextChanged(object sender, EventArgs e)
        {
            if (!iOCheckBox.Checked && !pNCheckBox.Checked && !pDCheckBox.Checked)
            {
                iOSrchBx.TextChanged -= iOSrchBx_TextChanged;
                iOSrchBx.Clear();
                iOSrchBx.TextChanged += iOSrchBx_TextChanged;

                itmDscrptnSrchBx.TextChanged -= itmDscrptnSrchBx_TextChanged;
                itmDscrptnSrchBx.Clear();
                itmDscrptnSrchBx.TextChanged += itmDscrptnSrchBx_TextChanged;

                prNmSrchBx.TextChanged -= prNmSrchBx_TextChanged;
                prNmSrchBx.Clear();
                prNmSrchBx.TextChanged += prNmSrchBx_TextChanged;

                SearchSingle(itmCdSrchBx.Text, itmCdSrchBx);
            }
            else
            {
                SearchMulti();
            }
        }

        private void itmDscrptnSrchBx_TextChanged(object sender, EventArgs e)
        {

            if (!iOCheckBox.Checked && !pNCheckBox.Checked && !pCCheckBox.Checked)
            {
                iOSrchBx.TextChanged -= iOSrchBx_TextChanged;
                iOSrchBx.Clear();
                iOSrchBx.TextChanged += iOSrchBx_TextChanged;

                itmCdSrchBx.TextChanged -= itmCdSrchBx_TextChanged;
                itmCdSrchBx.Clear();
                itmCdSrchBx.TextChanged += itmCdSrchBx_TextChanged;

                prNmSrchBx.TextChanged -= prNmSrchBx_TextChanged;
                prNmSrchBx.Clear();
                prNmSrchBx.TextChanged += prNmSrchBx_TextChanged;

                SearchSingle(itmDscrptnSrchBx.Text, itmDscrptnSrchBx);
            }
            else
            {
                SearchMulti();
            }

        }

        private void iOSrchBx_TextChanged(object sender, EventArgs e)
        {
            if (!pDCheckBox.Checked && !pNCheckBox.Checked && !pCCheckBox.Checked)
            {
                itmDscrptnSrchBx.TextChanged -= itmDscrptnSrchBx_TextChanged;
                itmDscrptnSrchBx.Clear();
                itmDscrptnSrchBx.TextChanged += itmDscrptnSrchBx_TextChanged;

                itmCdSrchBx.TextChanged -= itmCdSrchBx_TextChanged;
                itmCdSrchBx.Clear();
                itmCdSrchBx.TextChanged += itmCdSrchBx_TextChanged;

                prNmSrchBx.TextChanged -= prNmSrchBx_TextChanged;
                prNmSrchBx.Clear();
                prNmSrchBx.TextChanged += prNmSrchBx_TextChanged;

                SearchSingle(iOSrchBx.Text, iOSrchBx);
            }
            else
            {
                SearchMulti();
            }

        }

        private void pNCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(prNmSrchBx.Text) && !pNCheckBox.Checked)
            {
                prNmSrchBx.Clear();
            }
            if (string.IsNullOrWhiteSpace(prNmSrchBx.Text) && pNCheckBox.Checked)
            {
                MessageBox.Show("Nothing To Hold");
                pNCheckBox.Checked = false;
            }
        }

        private void pCCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(itmCdSrchBx.Text) && !pCCheckBox.Checked)
            {
                itmCdSrchBx.Clear();
            }
            if (string.IsNullOrWhiteSpace(itmCdSrchBx.Text) && pCCheckBox.Checked)
            {
                MessageBox.Show("Nothing To Hold");
                pCCheckBox.Checked = false;
            }
        }

        private void pDCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(itmDscrptnSrchBx.Text) && !pDCheckBox.Checked)
            {
                itmDscrptnSrchBx.Clear();
            }
            if (string.IsNullOrWhiteSpace(itmDscrptnSrchBx.Text) && pDCheckBox.Checked)
            {
                MessageBox.Show("Nothing To Hold");
                pDCheckBox.Checked = false;
            }
        }

        private void iOCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(iOSrchBx.Text) && !iOCheckBox.Checked)
            {
                iOSrchBx.Clear();
            }
            if (string.IsNullOrWhiteSpace(iOSrchBx.Text) && iOCheckBox.Checked)
            {
                MessageBox.Show("Nothing To Hold");
                iOCheckBox.Checked = false;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
                e.Handled = true;
            }
        }

        private void textBox2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.Focus();
                e.Handled = true;
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox4.Focus();
                e.Handled = true;
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox5.Focus();
                e.Handled = true;
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProductNameTextBox.Focus();
                e.Handled = true;
            }
        }

        private void ProductNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProductDesTextBox.Focus();
                e.Handled = true;
            }
        }

        private void ProductDesTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProductCodeTextBox.Focus();
                e.Handled = true;
            }
        }

        private void ProductCodeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShipingQtyTextBox.Focus();
                e.Handled = true;
            }
        }

        public void ClearTextbox()
        {
            SupplierComboBox.SelectedIndex = -1;
            SupplierComboBox.Items.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            ProductNameTextBox.Clear();
            ProductDesTextBox.Clear();
            ProductCodeTextBox.Clear();
            ShipingQtyTextBox.Clear();
            totalItemTextBox.Clear();
            totalQuantityTextBox.Clear();
            prNmSrchBx.Clear();
            itmDscrptnSrchBx.Clear();
            pNCheckBox.Checked = false;
            pDCheckBox.Checked = false;
            itmCdSrchBx.Clear();
            iOSrchBx.Clear();
            pCCheckBox.Checked = false;
            iOCheckBox.Checked = false;
            listView1.Items.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            totalQuantity = 0;
            totalItem=0;
        }



        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
