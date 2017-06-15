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
    public partial class RecieveOrderedProduct : Form
    {
        private SqlCommand cmd;
        ConnectionString Cs=new ConnectionString();
        private SqlConnection con;
        private SqlDataReader rdr;
        private string impOd;
        private DataGridViewRow dr;
        public RecieveOrderedProduct()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                con = new SqlConnection(Cs.DBConn);
                string qry =
                    "SELECT ImportOrders.ImpId, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemCode, ProductListSummary.ItemDescription,ImportOrderProduct.OrderQty, ImportOrderProduct.BacklogQty, ImportOrders.ImportOrderNo, ImportOrderProduct.ExpectedDateOfArrival FROM ImportOrders INNER JOIN ImportOrderProduct ON ImportOrders.ImpId = ImportOrderProduct.ImpId INNER JOIN ProductListSummary ON ImportOrderProduct.Sl = ProductListSummary.Sl inner join Supplier on ImportOrders.SupplierId=Supplier.SupplierId WHERE BacklogQty>0 and Supplier.SupplierName='" +
                    comboBox1.Text+"'";
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
                comboBox1.Items.Add(rdr[0]);
            }
            con.Close();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
               dr = dataGridView1.SelectedRows[0];
                impOd = dr.Cells[0].Value.ToString();
                textBox1.Text = dr.Cells[2].Value.ToString();
                textBox2.Text = dr.Cells[5].Value.ToString();
            }
            else
            {
                MessageBox.Show(@"Select Something first");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(comboBox2.Text))
            {
                if (listView1.Items.Count < 1)
                {
                    ListViewItem l1=new ListViewItem();
                    l1.Text = impOd;
                    l1.SubItems.Add(textBox3.Text);
                    l1.SubItems.Add(textBox1.Text);
                    l1.SubItems.Add(textBox4.Text);
                    l1.SubItems.Add(textBox4.Text);
                    listView1.Items.Add(l1);
                }
                else
                {
                    if (listView1.FindItemWithText(impOd) == null)
                    {
                        ListViewItem l2 = new ListViewItem();
                        l2.Text = impOd;
                        l2.SubItems.Add(textBox3.Text);
                        l2.SubItems.Add(textBox1.Text);
                        l2.SubItems.Add(textBox4.Text);
                        l2.SubItems.Add(textBox4.Text);
                        listView1.Items.Add(l2);
                    }
                    else
                        MessageBox.Show("This Prduct already Added");

                }
            }
        }
    }
}
