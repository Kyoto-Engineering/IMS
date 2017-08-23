using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImportOrderManagementSystem.DbGateway;

namespace ImportOrderManagementSystem.UI
{
    public partial class CFR_Price : Form
    {
        SqlConnection _con;
        SqlCommand _cmd;
        ConnectionString _cs = new ConnectionString();
        SqlDataReader rdr;
        // InsertPrice _ip;
        //string strvvvvl;

        public int CurrencyId;
        public bool CurrencySelected;
        public CFR_Price()
        {
            InitializeComponent();
        }

        private void GetIncoTerms()
        {
            try
            {
                _con = new SqlConnection(_cs.DBConn);
                _con.Open();
                string ctt = "SELECT Incoterm FROM IncoTerms where IncoID = 6";
                //string ctt1 = "SELECT IncoID FROM IncoTerms where IncoID = 1"
                _cmd = new SqlCommand(ctt);
                _cmd.Connection = _con;
                rdr = _cmd.ExecuteReader();
                while (rdr.Read())
                {
                    textBox2.Text = (rdr.GetString(0));
                }
                //cmbGender.Items.Add("Not In The List");
                _con.Close();

                _con.Open();
                //string ctt = "SELECT Incoterm FROM IncoTerms where IncoID = 1";
                string ctt1 = "SELECT IncoID FROM IncoTerms where IncoID = 6";
                _cmd = new SqlCommand(ctt1);
                _cmd.Connection = _con;
                rdr = _cmd.ExecuteReader();
                while (rdr.Read())
                {
                    textBox3.Text = (rdr.GetInt32(0).ToString());
                }
                //cmbGender.Items.Add("Not In The List");
                _con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CFR_Price_Load(object sender, EventArgs e)
        {
            GridLoad();
            GetCurrency();
            GetIncoTerms();
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
                    textBox1.Text = CurrencyId.ToString();
                    CurrencySelected = true;
                    //if (BrandSelected && IncoTermsSelected && Exists)
                    //{
                    //    groupBox2.Enabled = true;
                    //}
                }

            }

        }

        private void GridLoad()
        {
            try
            {

                _con = new SqlConnection(_cs.DBConn);
                _con.Open();
                //cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(ProductListSummary.ItemCode),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl desc", con);
                _cmd = new SqlCommand(
                    "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode FROM ProductListSummary LEFT JOIN CFRPrice ON ProductListSummary.Sl = CFRPrice.Sl where ProductListSummary.Sl not in(select CFRPrice.Sl from CFRPrice)", _con);
                rdr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridViewk.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                }

                _con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        //private void Savebutton_Click(object sender, EventArgs e)
        //{
        //    if (listView1.Items.Count == 0)
        //    {
        //        MessageBox.Show("Please add to Chart first", "Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            for (int i = 0; i <= listView1.Items.Count - 1; i++)
        //            {
        //                _con = new SqlConnection(_cs.DBConn);
        //                string cd = "INSERT INTO CFRPrice (Price,Sl,CurrencyId) VALUES (@d1,@d2,@d3)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
        //                _cmd = new SqlCommand(cd, _con);
        //                _cmd.Parameters.AddWithValue("@d1", listView1.Items[i].SubItems[1].Text);
        //                _cmd.Parameters.AddWithValue("@d2", listView1.Items[i].SubItems[0].Text);
        //                _cmd.Parameters.AddWithValue("@d3", listView1.Items[i].SubItems[3].Text);
        //                _con.Open();
        //                _cmd.ExecuteNonQuery();
        //                _con.Close();

        //            }

        //            MessageBox.Show("Saved Successfully.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            listView1.Items.Clear();
        //            this.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}


        //private void dataGridViewk_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    DataGridViewRow dr = dataGridViewk.SelectedRows[0];
        //    IdtextBox.Text = dr.Cells[0].Value.ToString();
        //    GenerictextBox.Text = dr.Cells[1].Value.ToString();
        //    ItemDestextBox.Text = dr.Cells[2].Value.ToString();
        //    CodetextBox.Text = dr.Cells[3].Value.ToString();
        //}

        //private void Addbutton_Click(object sender, EventArgs e)
        //{
        //    if (dataGridViewk.SelectedRows.Count == 0)
        //    {
        //        MessageBox.Show(@"Please Select Row!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    else if (string.IsNullOrWhiteSpace(PricetextBox.Text))
        //    {
        //        MessageBox.Show(@"Please Enter Price!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    else
        //    {
        //        try
        //        {
        //            if (listView1.Items.Count == 0)
        //            {
        //                ListViewItem lst = new ListViewItem();
        //                //lst.SubItems.Add(IdtextBox.Text);

        //                lst.SubItems.Add(PricetextBox.Text);
        //                lst.Text = IdtextBox.Text;
        //                lst.SubItems.Add(currencyComboBox.SelectedItem.ToString());
        //                lst.SubItems.Add(textBox1.Text);
        //                listView1.Items.Add(lst);

        //                IdtextBox.Clear();
        //                GenerictextBox.Clear();
        //                ItemDestextBox.Clear();
        //                CodetextBox.Clear();
        //                PricetextBox.Clear();
        //                currencyComboBox.Items.Clear();
        //                GetCurrency();
        //                return;
        //            }
        //            String Val = IdtextBox.Text;
        //            if (listView1.FindItemWithText(Val) == null)
        //            {
        //                ListViewItem lst1 = new ListViewItem();
        //                //lst1.SubItems.Add(IdtextBox.Text);

        //                lst1.SubItems.Add(PricetextBox.Text);
        //                lst1.Text = IdtextBox.Text;
        //                lst1.SubItems.Add(currencyComboBox.SelectedItem.ToString());
        //                lst1.SubItems.Add(textBox1.Text);
        //                listView1.Items.Add(lst1);

        //                IdtextBox.Clear();
        //                GenerictextBox.Clear();
        //                ItemDestextBox.Clear();
        //                CodetextBox.Clear();
        //                PricetextBox.Clear();
        //                return;
        //            }
        //            else
        //            {
        //                MessageBox.Show("You Can Not Add Same Product Id More than one times", "error",
        //                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                //IdtextBox.Clear();
        //                //GenerictextBox.Clear();
        //                //ItemDestextBox.Clear();
        //                //CodetextBox.Clear();
        //                //PricetextBox.Clear();
        //                return;
        //            }


        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        //private void Removebutton_Click(object sender, EventArgs e)
        //{
        //    if (listView1.SelectedItems.Count < 1)
        //    {
        //        MessageBox.Show("Please Select a row from the list which you  want to remove", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }
        //    else
        //    {
        //        for (int i = listView1.Items.Count - 1; i >= 0; i--)
        //        {
        //            if (listView1.Items[i].Selected)
        //            {
        //                listView1.Items[i].Remove();
        //            }
        //        }
        //    }
        //}

        private void PricetextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Addbutton_Click_1(this, new EventArgs());
            }
        }

        private void dataGridViewk_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridViewk.SelectedRows[0];
            IdtextBox.Text = dr.Cells[0].Value.ToString();
            GenerictextBox.Text = dr.Cells[1].Value.ToString();
            ItemDestextBox.Text = dr.Cells[2].Value.ToString();
            CodetextBox.Text = dr.Cells[3].Value.ToString();
        }

        private void Addbutton_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewk.SelectedRows.Count == 0)
            {
                MessageBox.Show(@"Please Select Row!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(PricetextBox.Text))
            {
                MessageBox.Show(@"Please Enter Price!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (listView1.Items.Count == 0)
                    {
                        ListViewItem lst = new ListViewItem();
                        //lst.SubItems.Add(IdtextBox.Text);

                        lst.SubItems.Add(PricetextBox.Text);
                        lst.Text = IdtextBox.Text;
                        lst.SubItems.Add(currencyComboBox.SelectedItem.ToString());
                        lst.SubItems.Add(textBox1.Text);
                        listView1.Items.Add(lst);

                        IdtextBox.Clear();
                        GenerictextBox.Clear();
                        ItemDestextBox.Clear();
                        CodetextBox.Clear();
                        PricetextBox.Clear();
                        currencyComboBox.Items.Clear();
                        GetCurrency();
                        return;
                    }
                    String Val = IdtextBox.Text;
                    if (listView1.FindItemWithText(Val) == null)
                    {
                        ListViewItem lst1 = new ListViewItem();
                        //lst1.SubItems.Add(IdtextBox.Text);

                        lst1.SubItems.Add(PricetextBox.Text);
                        lst1.Text = IdtextBox.Text;
                        lst1.SubItems.Add(currencyComboBox.SelectedItem.ToString());
                        lst1.SubItems.Add(textBox1.Text);
                        listView1.Items.Add(lst1);

                        IdtextBox.Clear();
                        GenerictextBox.Clear();
                        ItemDestextBox.Clear();
                        CodetextBox.Clear();
                        PricetextBox.Clear();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("You Can Not Add Same Product Id More than one times", "error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //IdtextBox.Clear();
                        //GenerictextBox.Clear();
                        //ItemDestextBox.Clear();
                        //CodetextBox.Clear();
                        //PricetextBox.Clear();
                        return;
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Removebutton_Click_1(object sender, EventArgs e)
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

        private void Savebutton_Click_1(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Please add to Chart first", "Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                try
                {
                    for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    {
                        _con = new SqlConnection(_cs.DBConn);
                        string cd = "INSERT INTO CFRPrice(Price,Sl,CurrencyId) VALUES (@d1,@d2,@d3)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                        _cmd = new SqlCommand(cd, _con);
                        _cmd.Parameters.AddWithValue("@d1", listView1.Items[i].SubItems[1].Text);
                        _cmd.Parameters.AddWithValue("@d2", listView1.Items[i].SubItems[0].Text);
                        _cmd.Parameters.AddWithValue("@d3", listView1.Items[i].SubItems[3].Text);
                        _con.Open();
                        _cmd.ExecuteNonQuery();
                        _con.Close();

                    }

                    MessageBox.Show("Saved Successfully.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listView1.Items.Clear();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }





    }
}
