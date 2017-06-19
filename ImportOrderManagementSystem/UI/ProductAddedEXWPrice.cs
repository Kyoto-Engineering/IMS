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
    public partial class ProductAddedEXWPrice : Form
    {
        SqlConnection _con;
        SqlCommand _cmd;
        ConnectionString _cs = new ConnectionString();
        SqlDataReader rdr;
        public ProductAddedEXWPrice()
        {
            InitializeComponent();
        }

        private void ProductAddedEXWPrice_Load(object sender, EventArgs e)
        {
            GridLoad();
        }

        private void GridLoad()
        {
            try
            {

                _con = new SqlConnection(_cs.DBConn);
                _con.Open();
                //cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(ProductListSummary.ItemCode),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl desc", con);
                _cmd = new SqlCommand(
                    "SELECT ProductListSummary.Sl, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemDescription, ProductListSummary.ItemCode FROM ProductListSummary LEFT JOIN EXWPrice ON ProductListSummary.Sl = EXWPrice.Sl where ProductListSummary.Sl not in(select EXWPrice.Sl from EXWPrice)", _con);
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
                        _con = new SqlConnection(_cs.DBConn);
                        string cd = "INSERT INTO EXWPrice (Price,Sl) VALUES (@d1,@d2)";
                        _cmd = new SqlCommand(cd, _con);
                        _cmd.Parameters.AddWithValue("@d1", listView1.Items[i].SubItems[1].Text);
                        _cmd.Parameters.AddWithValue("@d2", listView1.Items[i].SubItems[0].Text);
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

        private void dataGridViewk_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridViewk.SelectedRows[0];
            IdtextBox.Text = dr.Cells[0].Value.ToString();
            GenerictextBox.Text = dr.Cells[1].Value.ToString();
            ItemDestextBox.Text = dr.Cells[2].Value.ToString();
            CodetextBox.Text = dr.Cells[3].Value.ToString();
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
            else
            {
                try
                {
                    if (listView1.Items.Count == 0)
                    {
                        ListViewItem lst = new ListViewItem();
                        //lst.SubItems.Add(IdtextBox.Text);
                        lst.Text = IdtextBox.Text;
                        lst.SubItems.Add(PricetextBox.Text);
                        listView1.Items.Add(lst);
                       
                        IdtextBox.Clear();
                        GenerictextBox.Clear();
                        ItemDestextBox.Clear();
                        CodetextBox.Clear();
                        PricetextBox.Clear();
                        return;
                    }
                    String Val = IdtextBox.Text;
                    if (listView1.FindItemWithText(Val) == null)
                    {
                        ListViewItem lst1 = new ListViewItem();
                        //lst1.SubItems.Add(IdtextBox.Text);
                        lst1.Text = IdtextBox.Text;
                        lst1.SubItems.Add(PricetextBox.Text);
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
    }
}
