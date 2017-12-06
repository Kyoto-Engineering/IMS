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

namespace ImportOrderManagementSystem.UI
{
    public partial class SpecialPriceUI : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        ConnectionString cs = new ConnectionString();
        SqlDataReader rdr;
        public int CurrencyId;
        public bool CurrencySelected;

        public SpecialPriceUI()
        {
            InitializeComponent();
        }

        private void dataGridViewk_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        
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

        private void SpecialPriceUI_Load(object sender, EventArgs e)
        {
            gridload();
            GetCurrency();
        }

        private void gridload()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string qq2 =
                "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode, SpecialPrice.SPrice FROM ProductListSummary Left Outer Join SpecialPrice on ProductListSummary.Sl = SpecialPrice.Sl  ";
            cmd = new SqlCommand(qq2, con);
            dataGridViewk.Rows.Clear();
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                dataGridViewk.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4]);
            }
            con.Close();
        }

        private void currencyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currencyComboBox.SelectedIndex != -1)
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cty4 = "SELECT CurrencyId FROM Currency where CurrencyName='" + currencyComboBox.Text + "'";
                cmd = new SqlCommand(cty4);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
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

        private bool GetValue()
        {
            bool x = true;
            foreach (ListViewItem z in listView1.Items)
            {
                if (z.Text == IdtextBox.Text)
                {
                    x = false;
                    break;
                }
            }
            return x;
        }
        private void Addbutton_Click(object sender, EventArgs e)
        {
            if (dataGridViewk.SelectedRows.Count == 0)
            {
                MessageBox.Show(@"Please Select Row!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(PricetextBox.Text))
            {
                MessageBox.Show(@"Please Enter Price!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(currencyComboBox.Text))
            {
                MessageBox.Show(@"Please Select Currency!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                        //lst.SubItems.Add(textBox2.Text);
                        //lst.SubItems.Add(textBox3.Text);
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
                    //String Val = IdtextBox.Text;
                    //if (listView1.FindItemWithText(Val) == null)
                    if(GetValue())
                    {
                        ListViewItem lst1 = new ListViewItem();
                        //lst1.SubItems.Add(IdtextBox.Text);

                        lst1.SubItems.Add(PricetextBox.Text);
                        lst1.Text = IdtextBox.Text;
                        lst1.SubItems.Add(currencyComboBox.SelectedItem.ToString());
                        lst1.SubItems.Add(textBox1.Text);
                        //lst1.SubItems.Add(textBox2.Text);
                        //lst1.SubItems.Add(textBox3.Text);
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

        private void dataGridViewk_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridViewk.SelectedRows[0];
            IdtextBox.Text = dr.Cells[0].Value.ToString();
            GenerictextBox.Text = dr.Cells[1].Value.ToString();
            ItemDestextBox.Text = dr.Cells[2].Value.ToString();
            CodetextBox.Text = dr.Cells[3].Value.ToString();
        }

        private void Removebutton_Click(object sender, EventArgs e)
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

        private void Savebutton_Click(object sender, EventArgs e)
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
                        con = new SqlConnection(cs.DBConn);
                        string cd = "INSERT INTO SpecialPrice (Sl,SPrice,CurrencyId) VALUES (@d1,@d2,@d3)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                        cmd = new SqlCommand(cd, con);
                        cmd.Parameters.AddWithValue("@d1", listView1.Items[i].SubItems[0].Text);
                        cmd.Parameters.AddWithValue("@d2", listView1.Items[i].SubItems[1].Text);
                        cmd.Parameters.AddWithValue("@d3", listView1.Items[i].SubItems[3].Text);
                        //cmd.Parameters.AddWithValue("@d4", listView1.Items[i].SubItems[5].Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

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
