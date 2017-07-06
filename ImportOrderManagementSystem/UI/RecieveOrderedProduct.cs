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

namespace ImportOrderManagementSystem.UI
{
    public partial class RecieveOrderedProduct : Form
    {
        private SqlCommand cmd;
        ConnectionString Cs=new ConnectionString();
        private SqlConnection con;
        private SqlDataReader rdr;
        private string impOd;
        private DataGridViewRow dr;
        private int checkvalue,smId;
        private int SupplierId;
        private int Sio;
        private string shipmentOrderNo;

        public RecieveOrderedProduct()
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
                    "SELECT ImportOrderProduct.ImportOrderProductId, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemCode, ProductListSummary.ItemDescription,ImportOrderProduct.OrderQty, ImportOrderProduct.BacklogQty, ImportOrders.ImportOrderNo, ImportOrderProduct.ExpectedDateOfArrival FROM ImportOrders INNER JOIN ImportOrderProduct ON ImportOrders.ImpId = ImportOrderProduct.ImpId INNER JOIN ProductListSummary ON ImportOrderProduct.Sl = ProductListSummary.Sl inner join Supplier on ImportOrders.SupplierId=Supplier.SupplierId WHERE BacklogQty>0 and Supplier.SupplierName='" +
                    SupplierComboBox.Text+"'";
                cmd = new SqlCommand(qry, con);
                con.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                }
                con.Close();
            }
            GetShimpentOredrNo();
        }

        private void GetShimpentOredrNo()
        {
            if (SupplierComboBox.SelectedIndex != -1)
            {
                try
                {
                    con = new SqlConnection(Cs.DBConn);
                    con.Open();
                    string cty4 = "SELECT SupplierId FROM Supplier WHERE SupplierName ='" + SupplierComboBox.Text + "'";
                    cmd = new SqlCommand(cty4);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        SupplierId = (rdr.GetInt32(0));
                    }
                    con.Close();
                    con = new SqlConnection(Cs.DBConn);
                    con.Open();
                    //string q2 = "Select SQN From RefNumForQuotation where SClientId='" + sClientIdForRefNum + "'";
                    //string qr2 = "SELECT MAX(RefNumForQuotation.SQN) FROM RefNumForQuotation where SClientId='" + sClientIdForRefNum + "'";
                    string qr2 = "SELECT   MAX(SSO) FROM ShipmentOrder where SupplierId=" + SupplierId +
                                 " and YEAR(ExpectedShipmentDate)=" + ShippingDateTimePicker.Value.Year;
                    cmd = new SqlCommand(qr2, con);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (!rdr.IsDBNull(0))
                        {
                            Sio = (rdr.GetInt32(0));
                            Sio = Sio + 1;
                            shipmentOrderNo = SupplierId +"-"+ ShippingDateTimePicker.Value.Year.ToString().Substring(2) + "-SO-" + Sio;



                        }

                        else
                        {
                            Sio = 1;
                            shipmentOrderNo = SupplierId + "-" + ShippingDateTimePicker.Value.Year.ToString().Substring(2) + "-SO-" + Sio;

                        }
                    }
                    ShipmentOrderNoTextBox.Text = shipmentOrderNo;
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RecieveOrderedProduct_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(Cs.DBConn);
            string qry =
                "SELECT SupplierName FROM Supplier";
            cmd = new SqlCommand(qry, con);
            con.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                SupplierComboBox.Items.Add(rdr[0]);
            }
            con.Close();
            string qry2 =
                "SELECT ShippingMood FROM ShipMood";
            cmd = new SqlCommand(qry2, con);
            con.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ShippingModeComboBox.Items.Add(rdr[0]);
            }
            con.Close();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
               dr = dataGridView1.SelectedRows[0];
                impOd = dr.Cells[0].Value.ToString();
                ProductCodeTextBox.Text = dr.Cells[2].Value.ToString();
                ShipingQtyTextBox.Text = dr.Cells[5].Value.ToString();
                ProductNameTextBox.Text = dr.Cells[1].Value.ToString();
                ProductDesTextBox.Text = dr.Cells[3].Value.ToString();
                checkvalue =Convert.ToInt32( dr.Cells[5].Value.ToString());
            }
            else
            {
                MessageBox.Show(@"Select Something first");
            }
        }

        public int totalItem = 0, totalQuantity=0;

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ShippingModeComboBox.Text))
            {
                if (string.IsNullOrEmpty(ProductCodeTextBox.Text))
                {
                    MessageBox.Show("Select A Product First");
                }
                else if (string.IsNullOrWhiteSpace(ShipingQtyTextBox.Text) || Convert.ToInt32(ShipingQtyTextBox.Text)<1)
                {
                    MessageBox.Show("Product With Zero , MInus or Empty Quantity Can Not Be Added");
                }
                else if (Convert.ToInt32(ShipingQtyTextBox.Text)>checkvalue)
                {
                    MessageBox.Show("Receive Amount Cannot Be greater Than the Backloq Quantity");
                }
                else
                {


                    if (listView1.Items.Count < 1)
                    {
                        ListViewItem l1 = new ListViewItem();
                        l1.Text = impOd;
                        l1.SubItems.Add(ProductNameTextBox.Text);
                        l1.SubItems.Add(ProductCodeTextBox.Text);
                        l1.SubItems.Add(ProductDesTextBox.Text);
                        l1.SubItems.Add(ShipingQtyTextBox.Text);
                        listView1.Items.Add(l1);

                        //tawhidul
                        //total item calculation
                        totalItem = ++totalItem;
                        totalItemTextBox.Text = totalItem.ToString();

                        //total quantity calculation
                        totalQuantity = totalQuantity + Convert.ToInt32(ShipingQtyTextBox.Text);
                        totalQuantityTextBox.Text = totalQuantity.ToString();

                        ClearselectedProduct();
                        groupBox1.Enabled = false;
                    }
                    else
                    {
                        if (GetValue())
                        {
                            ListViewItem l2 = new ListViewItem();
                            l2.Text = impOd;
                            l2.SubItems.Add(ProductNameTextBox.Text);
                            l2.SubItems.Add(ProductCodeTextBox.Text);
                            l2.SubItems.Add(ProductDesTextBox.Text);
                            l2.SubItems.Add(ShipingQtyTextBox.Text);
                            listView1.Items.Add(l2);

                            //tawhidul
                            //total item calculation
                            totalItem = ++totalItem;
                            totalItemTextBox.Text = totalItem.ToString();

                            //total quantity calculation
                            totalQuantity = totalQuantity + Convert.ToInt32(ShipingQtyTextBox.Text);
                            totalQuantityTextBox.Text = totalQuantity.ToString();

                            ClearselectedProduct();
                            //groupBox1.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("This Prduct already Added");
                            ClearselectedProduct();
                        }
                    }
                }
            }
            else 
            {
                MessageBox.Show("Select Shipment Method");
            }
        }
        private bool GetValue()
        {
            bool x = true;
            foreach (ListViewItem z in listView1.Items)
            {
                if (z.Text == impOd)
                {
                    x = false;
                    break;
                }
            }
            return x;
        }
     private void ClearselectedProduct()
        {
            impOd = null;
            ProductCodeTextBox.Clear();
            ShipingQtyTextBox.Clear();
            ProductDesTextBox.Clear();
            ProductNameTextBox.Clear();
        }

     private void ClearShipmentandgridsinfo() 
     {
         SupplierComboBox.SelectedIndex = -1;
         ShippingModeComboBox.SelectedIndex = -1;
         ShipmentOrderNoTextBox.Clear();

         SupplierComboBox.ResetText();
         ShippingModeComboBox.ResetText();

         listView1.Items.Clear();
         dataGridView1.Rows.Clear();
         dataGridView1.Refresh();
     } 

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ProductCodeTextBox.Text))
            {
                if (listView1.Items.Count>0)
                {

                    con = new SqlConnection(Cs.DBConn);
                    string q1 =
                        "INSERT INTO ShipmentOrder (ExpectedShipmentDate,ExpectedDateOfDelivery,SMId,UserId,EntryDate,ShipmentOrderNo,SSO,SupplierId,ACost,CFC) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                    cmd=new SqlCommand(q1,con);
                    cmd.Parameters.AddWithValue("@d1", ShippingDateTimePicker.Value);
                    cmd.Parameters.AddWithValue("@d2", DeliveryDateTimePicker.Value);
                    cmd.Parameters.AddWithValue("@d3",smId);
                    cmd.Parameters.AddWithValue("@d4", LoginForm.uId2);
                    cmd.Parameters.AddWithValue("@d5", DateTime.UtcNow.ToLocalTime());
                    cmd.Parameters.AddWithValue("@d6",shipmentOrderNo );
                    cmd.Parameters.AddWithValue("@d7", Sio);
                    cmd.Parameters.AddWithValue("@d8", SupplierId);
                    cmd.Parameters.AddWithValue("@d9", string.IsNullOrWhiteSpace(textBox1.Text)?(object)DBNull.Value:textBox1.Text);
                    cmd.Parameters.AddWithValue("@d10", string.IsNullOrWhiteSpace(textBox2.Text) ? (object)DBNull.Value : textBox2.Text);
                    con.Open();
                    string ShID=cmd.ExecuteScalar().ToString();
                    con.Close();
                    for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    {
                        string imprno = listView1.Items[i].Text;
                        string qty = listView1.Items[i].SubItems[4].Text;
                        string query =
                            "INSERT INTO ShipmentProduct(ImportOrderProductId,ShipmentProductQty,ShipmentId)Values(@d1,@d2,@d3)";
                        cmd=new SqlCommand(query,con);
                        cmd.Parameters.AddWithValue("@d1", imprno);
                        cmd.Parameters.AddWithValue("@d2",qty);
                        cmd.Parameters.AddWithValue("@d3", ShID);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        string query2 =
                            "UPDATE ImportOrderProduct SET BacklogQty = BacklogQty -@d1 WHERE (ImportOrderProductId = @d2)";
                        cmd = new SqlCommand(query2, con);
                        cmd.Parameters.AddWithValue("@d1", qty);
                        cmd.Parameters.AddWithValue("@d2", imprno);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    MessageBox.Show("Shipment Order Done");
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

            ClearselectedProduct();
            ClearShipmentandgridsinfo();
            groupBox1.Enabled = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            con=new SqlConnection(Cs.DBConn);
            string query = "SELECT SMId FROM ShipMood where ShippingMood='" + ShippingModeComboBox.Text + "'";
            cmd=new SqlCommand(query,con);
            con.Open();
            rdr=cmd.ExecuteReader();
            if (rdr.Read())
            {
                smId = Convert.ToInt32(rdr["SMId"]);
            }
            con.Close();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

       
        
         
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
          
        

        private void RemoveButton_Click(object sender, EventArgs e)
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
                        totalQuantity = totalQuantity - Convert.ToInt32(listView1.Items[i].SubItems[4].Text);
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
            string SearchModifier1="", SearchModifier2="", SearchModifier3="", SearchModifier4 = "";
            if (pNCheckBox.Checked)
            {
                SearchModifier1 = " and ProductListSummary.ProductGenericDescription like '"+prNmSrchBx.Text+"%' ";
            }
            else
            {
                SearchModifier1 = "";
            }
            if (pCCheckBox.Checked)
            {
                SearchModifier2 = " and ProductListSummary.ItemCode like '" +itmCdSrchBx.Text+ "%' ";
            }
            else
            {
                SearchModifier2 = "";
            }
            if (pDCheckBox.Checked )
            {
                SearchModifier3 = "  and ProductListSummary.ItemDescription like '" + itmDscrptnSrchBx.Text + "%' ";
            }
            else
            {
                SearchModifier3 = "";
            }
            if (iOCheckBox.Checked )
            {
                SearchModifier4 = " and  ImportOrders.ImportOrderNo like '"+iOSrchBx.Text+"%' ";
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
                    "SELECT ImportOrderProduct.ImportOrderProductId, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemCode, ProductListSummary.ItemDescription,ImportOrderProduct.OrderQty, ImportOrderProduct.BacklogQty, ImportOrders.ImportOrderNo, ImportOrderProduct.ExpectedDateOfArrival FROM ImportOrders INNER JOIN ImportOrderProduct ON ImportOrders.ImpId = ImportOrderProduct.ImpId INNER JOIN ProductListSummary ON ImportOrderProduct.Sl = ProductListSummary.Sl inner join Supplier on ImportOrders.SupplierId=Supplier.SupplierId WHERE BacklogQty>0 and Supplier.SupplierName='" +
                    SupplierComboBox.Text +"' "+ SearchModifier1+SearchModifier2+SearchModifier3+SearchModifier4;
                cmd = new SqlCommand(qry, con);
                con.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
                }
                con.Close();
            }
        }
        private void SearchSingle(string searchText, Control searchControl)
        {
            string SearchModifier=null;
            if (searchControl == prNmSrchBx)
            {
                SearchModifier = "ProductListSummary.ProductGenericDescription";
            }
            else if (searchControl == itmCdSrchBx)
            {
                SearchModifier = "ProductListSummary.ItemCode";
            }
            else if (searchControl ==itmDscrptnSrchBx)
            {
                SearchModifier = "ProductListSummary.ItemDescription";
            }
            else if (searchControl == iOSrchBx)
            {
                SearchModifier = "ImportOrders.ImportOrderNo";
            }
            string totalsearchtext = SearchModifier+" like '"+searchText+"%' Order by "+ SearchModifier;
            if (SupplierComboBox.SelectedIndex != -1)
            {
                dataGridView1.Rows.Clear();
                con = new SqlConnection(Cs.DBConn);
                string qry =
                    "SELECT ImportOrderProduct.ImportOrderProductId, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemCode, ProductListSummary.ItemDescription,ImportOrderProduct.OrderQty, ImportOrderProduct.BacklogQty, ImportOrders.ImportOrderNo, ImportOrderProduct.ExpectedDateOfArrival FROM ImportOrders INNER JOIN ImportOrderProduct ON ImportOrders.ImpId = ImportOrderProduct.ImpId INNER JOIN ProductListSummary ON ImportOrderProduct.Sl = ProductListSummary.Sl inner join Supplier on ImportOrders.SupplierId=Supplier.SupplierId WHERE BacklogQty>0 and Supplier.SupplierName='" +
                    SupplierComboBox.Text + "' and " + totalsearchtext;
                cmd = new SqlCommand(qry, con);
                con.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]);
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
            if (!string.IsNullOrWhiteSpace(prNmSrchBx.Text)&& !pNCheckBox.Checked)
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
    }
}
