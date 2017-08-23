using ImportOrderManagementSystem.DbGateway;
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

 
namespace ImportOrderManagementSystem.UI
{
    public partial class InsertPrice : Form
    {

        SqlConnection _con;
        SqlCommand _cmd;
        ConnectionString _cs = new ConnectionString();
        SqlDataReader rdr;
        public int IncoId;
        public bool IncoTermsSelected;
        public string strvl;

        public InsertPrice()
        {
            InitializeComponent();
        }

        private void EXWbutton_Click(object sender, EventArgs e)
        {
            ProductAddedEXWPrice paexw=new ProductAddedEXWPrice();
            this.Visible = false;
            paexw.ShowDialog();
            this.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (incoCombobox.SelectedIndex != -1)
            {
                //if (test_textBox1.Text == "1")
                //{

                    ProductAddedEXWPrice paexw1 = new ProductAddedEXWPrice();
                    paexw1.textBox2.Text = incoCombobox.Text;
                    paexw1.textBox3.Text = test_textBox1.Text;
                    this.Visible = false;
                    paexw1.ShowDialog();
                    this.Visible = true;

                incoCombobox.Items.Clear();
                GetIncoTerms();
                test_textBox1.Clear();

                //}

                //else if (test_textBox1.Text == "6")
                //{
                //    CFR_Price paexw2 = new CFR_Price();
                //    this.Visible = false;
                //    paexw2.ShowDialog();
                //    this.Visible = true;
                
                
                //}

                //else { MessageBox.Show("Please Select EXW or CFR Terms"); }

            }
            

            else
            {
                MessageBox.Show("Please Select Inco Terms");
            
            }


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
        private void InsertPrice_Load(object sender, EventArgs e)
        {
            GetIncoTerms();
        }

        private void incoCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (incoCombobox.SelectedIndex != -1)
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
                    test_textBox1.Text = IncoId.ToString();
                    //strvl = textBox1.Text;
                    //testboooox.Text = strvl;
                    //if (PriceExists())
                    //{
                    //    Exists = true;

                    //}

                    //else
                    //{
                    //    MessageBox.Show(@"You Can Not Import in this Method Right Now" + "\n" +
                    //                    @"Please Contact With Developer");
                    //}

                    //if (BrandSelected && IncoTermsSelected && Exists)
                    //{
                    //    groupBox2.Enabled = true;
                    //}


                }

            }
        }
    }
}
