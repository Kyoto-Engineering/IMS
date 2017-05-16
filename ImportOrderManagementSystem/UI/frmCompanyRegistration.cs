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
    public partial class frmCompanyRegistration : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs = new ConnectionString();
        public string user_id;
        private bool companyCreated;
        public int companyId, currentCompanyId;
        public frmCompanyRegistration()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (companyCreated)
            {
                MessageBox.Show(@"Already a Company Created", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CompanyNametextBox.Clear();
                AddressrichTextBox.Clear();
                return;
            }

            if (string.IsNullOrEmpty(CompanyNametextBox.Text))
            {
                MessageBox.Show(@"Please Enter Company Name", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CompanyNametextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(AddressrichTextBox.Text))
            {
                MessageBox.Show(@"Please Enter Company Address", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CompanyNametextBox.Focus();

            }
            else
            {
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string query =
                        "INSERT INTO Companies (CompanyName,CompanyAddress,UserId) VALUES (@d1,@d2,@d3)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";


                    cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@d1", CompanyNametextBox.Text);
                    cmd.Parameters.AddWithValue("@d2", AddressrichTextBox.Text);
                    cmd.Parameters.AddWithValue("@d3", user_id);
                    currentCompanyId = (int)(cmd.ExecuteScalar());
                    con.Close();
                    MessageBox.Show(@"Company Registered successfully", @"Record", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    CompanyNametextBox.Clear();
                    AddressrichTextBox.Clear();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        private void frmCompanyRegistration_Load(object sender, EventArgs e)
        {
            companyCreated = LoadCompany();
            user_id = LoginForm.uId2.ToString();
        }



        private bool LoadCompany()
        {
            bool x = false;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string query = "SELECT CompanyId FROM Companies";
            cmd = new SqlCommand(query, con);
            rdr = cmd.ExecuteReader();
            if (rdr.Read() && !rdr.IsDBNull(0))
            {
                companyId = Convert.ToInt32(rdr["CompanyId"]);
                x = true;
            }
            return x;
        }

        private void frmCompanyRegistration_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            frmMainUI frm = new frmMainUI();
            frm.Show();
        }
    }
}
