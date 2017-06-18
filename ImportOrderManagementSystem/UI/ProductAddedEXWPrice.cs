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
            try
            {
                
                    _con = new SqlConnection(_cs.DBConn);
                    string cd = "INSERT INTO EXWPrice (Price,Sl) VALUES (@d1,@d2)";
                    _cmd = new SqlCommand(cd, _con);
                    _cmd.Parameters.AddWithValue("@d1", PricetextBox.Text);
                    _cmd.Parameters.AddWithValue("d2", dataGridViewk.Rows[0].Cells[0].Value);                   
                    _con.Open();
                    _cmd.ExecuteNonQuery();
                    _con.Close();

                
                
                MessageBox.Show("Saved Successfully.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                dataGridViewk.Enabled = false;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
