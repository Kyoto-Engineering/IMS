using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImportOrderManagementSystem.DAL;
using ImportOrderManagementSystem.DbGateway;
using ImportOrderManagementSystem.Manager;

namespace ImportOrderManagementSystem.LoginUI
{
    public partial class frmRegistration : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        ConnectionString cs = new ConnectionString();
        private SqlDataReader rdr;
        public string readyPassword;
        public frmRegistration()
        {
            InitializeComponent();
        }
        private void Reset()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            cmbUserType.Text = "";
            txtFullName.Text = "";
            designationTextBox.Text = "";
            departmentTextBox.Text = "";
            txtContact_no.Text = "";
            userButton.Enabled = true;
        }
        private void frmRegistration_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        public string EncodePasswordToBase64(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            string readyPassword1 = Convert.ToBase64String(inArray);
            readyPassword = readyPassword1;
            return readyPassword1;
        }
        private void userButton_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Please enter user name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus();
                return;
            }
            if (cmbUserType.Text == "")
            {
                MessageBox.Show("Please Select User type", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbUserType.Focus();
                return;
            }
            if (txtFullName.Text == "")
            {
                MessageBox.Show("You Must Type Your Full Name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFullName.Focus();
                return;
            }
            if (txtFullName.Text == "")
            {
                MessageBox.Show("You Must Type Your Full Name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFullName.Focus();
                return;
            }
          
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select UserName from Registration where UserName=@find";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NChar, 30, "UserName"));
                cmd.Parameters["@find"].Value = txtUsername.Text;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("Username Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Text = "";
                    txtUsername.Focus();


                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                //con = new SqlConnection(cs.DBConn);
                //con.Open();
                //string ct1 = "select Email from registration where Email='" + txtEmail_Address.Text + "'";

                //cmd = new SqlCommand(ct1);
                //cmd.Connection = con;
                //rdr = cmd.ExecuteReader();

                //if (rdr.Read())
                //{
                //    MessageBox.Show("Email ID Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    txtEmail_Address.Text = "";
                //    txtEmail_Address.Focus();


                //    if ((rdr != null))
                //    {
                //        rdr.Close();
                //    }
                //    return;
                //}
                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "insert into Registration(UserName,UserType,Password,Name,Designation,Department,ContactNo) VALUES ('" + txtUsername.Text + "','" + cmbUserType.Text + "','" + txtPassword.Text + "','" + txtFullName.Text + "','" + designationTextBox.Text + "','" + departmentTextBox.Text + "','" + txtContact_no.Text + "')";

                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Successfully Registered", "User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
                userButton.Enabled = false;
               
            }
            catch (FormatException formatException)
            {
                MessageBox.Show("Please Enter Input in Correct Format", formatException.Message);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Please enter username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus();
                return;
            }
            if (cmbUserType.Text == "")
            {
                MessageBox.Show("Please select user type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbUserType.Focus();
                return;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }
            if (txtFullName.Text == "")
            {
                MessageBox.Show("Please enter name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFullName.Focus();
                return;
            }
            if (txtContact_no.Text == "")
            {
                MessageBox.Show("Please enter contact no.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContact_no.Focus();
                return;
            }
            //if (txtEmail_Address.Text == "")
            //{
            //    MessageBox.Show("Please enter email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtEmail_Address.Focus();
            //    return;
            //}
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "Update Registration set UserType='" + cmbUserType.Text + "', Password='" + txtPassword.Text + "',Name='" + txtFullName.Text + "',ContactNo='" + txtContact_no.Text + "' where UserName='" + txtUsername.Text + "'";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();

                MessageBox.Show("Successfully updated", "User Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtUsername.Text = txtUsername.Text.TrimEnd();
                con = new SqlConnection(cs.DBConn);

                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT UserType,Password,Name,ContactNo,email FROM registration WHERE username = '" + txtUsername.Text.Trim() + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    cmbUserType.Text = (rdr.GetString(0).Trim());
                    txtPassword.Text = (rdr.GetString(1).Trim());
                    txtFullName.Text = (rdr.GetString(2).Trim());
                    txtContact_no.Text = (rdr.GetString(3).Trim());
                    // txtEmail_Address.Text = (rdr.GetString(4).Trim());
                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
                e.Handled = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbUserType.Focus();
                e.Handled = true;
            }
        }

        private void cmbUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFullName.Focus();
        }

        private void txtFullName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                designationTextBox.Focus();
                e.Handled = true;
            }
        }

        private void designationTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                departmentTextBox.Focus();
                e.Handled = true;
            }
        }

        private void departmentTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtContact_no.Focus();
                e.Handled = true;
            }
        }

        private void txtContact_no_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                userButton_Click(this, new EventArgs());
            }
        }
    }
}
