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
using System.Timers;
using System.Windows.Forms;
using ImportOrderManagementSystem.DbGateway;
using ImportOrderManagementSystem.UI;

namespace ImportOrderManagementSystem.LoginUI
{
    public partial class LoginForm : Form
    {
        ProgressBar ProgressBar1 = new ProgressBar();
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr,rdr1;
        ConnectionString cs = new ConnectionString();
        public static int uId2;
        public string readyPassword, dbUserName, dbPassword, userType;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Please enter user name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                return;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }
            try
            {

                string clearText = txtPassword.Text.Trim();
                string password = clearText;
                byte[] bytes = Encoding.Unicode.GetBytes(password);
                byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
                string readyPassword1 = Convert.ToBase64String(inArray);
                readyPassword = readyPassword1;


                con = new SqlConnection(cs.DBConn);
                con.Open();
                string qry = "SELECT UserName,Password FROM Registration WHERE UserName = '" + txtUserName.Text + "' AND Password = '" + readyPassword + "'";
                cmd = new SqlCommand(qry, con);
                rdr1 = cmd.ExecuteReader();
                if (rdr1.Read() == true)
                {
                    dbUserName = (rdr1.GetString(0));
                    dbPassword = (rdr1.GetString(1));


                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct = "select UserType,UserId from Registration where UserName='" + txtUserName.Text + "' and Password='" + readyPassword + "'";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        userType = (rdr.GetString(0));
                        uId2 = (rdr.GetInt32(1));
                    }
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    //if (dbUserName == txtUserName.Text && dbPassword == readyPassword && userType.Trim() == "SuperAdmin")
                    //{
                      

                    //}
                   
                    
                    if (dbUserName == txtUserName.Text && txtUserName.Text == "ceo@keal.com.bd" && dbPassword == readyPassword && userType.Trim() == "Admin")
                    {
                        this.Hide();
                        frmMainUI frm = new frmMainUI();
                        frm.Show();                        
                        frm.lblUser.Text = txtUserName.Text;
                        txtPassword.Clear();
                        txtUserName.Clear();

                    }
                     

                    //if (dbUserName == txtUserName.Text && dbPassword == readyPassword && userType.Trim() == "Admin")
                    //{
                    //    this.Hide();
                    //    frmMainUI frm = new frmMainUI();
                    //    frm.Show();
                    //    frm.lblUser.Text = txtUserName.Text;
                    //    txtPassword.Clear();
                    //    txtUserName.Clear();

                    //}
                    
                    else
                    {
                        MessageBox.Show("Invalid Credential");
                        txtUserName.Clear();
                        txtPassword.Clear();
                        txtUserName.Focus();
                    }
                    //if (dbUserName == txtUserName.Text && dbPassword == readyPassword && userType.Trim() == "User")
                    //{
                    //    this.Hide();
                    //    FiscalYear frm = new FiscalYear();
                    //    frm.Show();

                    //}

                }
                else
                {
                    MessageBox.Show("Login is Failed...Try again !", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.Clear();
                    txtPassword.Clear();
                    txtUserName.Focus();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //if (txtUserName.Text == "")
            //{
            //    MessageBox.Show("Please enter user name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtUserName.Focus();
            //    return;
            //}
            //if (txtPassword.Text == "")
            //{
            //    MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtPassword.Focus();
            //    return;
            //}
            //try
            //{
            //    SqlConnection myConnection = default(SqlConnection);
            //    myConnection = new SqlConnection(cs.DBConn);

            //    SqlCommand myCommand = default(SqlCommand);

            //    myCommand = new SqlCommand("SELECT Username,password FROM Registration WHERE Username = @username AND Password = @UserPassword", myConnection);
            //    SqlParameter uName = new SqlParameter("@username", SqlDbType.VarChar);
            //    SqlParameter uPassword = new SqlParameter("@UserPassword", SqlDbType.VarChar);
            //    uName.Value = txtUserName.Text;
            //    uPassword.Value = txtPassword.Text;
            //    myCommand.Parameters.Add(uName);
            //    myCommand.Parameters.Add(uPassword);

            //    myCommand.Connection.Open();

            //    SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            //    if (myReader.Read() == true)
            //    {
            //        int i;
            //        ProgressBar1.Visible = true;
            //        ProgressBar1.Maximum = 5000;
            //        ProgressBar1.Minimum = 0;
            //        ProgressBar1.Value = 4;
            //        ProgressBar1.Step = 1;

            //        for (i = 0; i <= 5000; i++)
            //        {
            //            ProgressBar1.PerformStep();
            //        }
            //        con = new SqlConnection(cs.DBConn);
            //        con.Open();
            //        string ct = "select usertype,UserId from Registration where Username='" + txtUserName.Text + "'and Password='" + txtPassword.Text + "'";
            //        cmd = new SqlCommand(ct);
            //        cmd.Connection = con;
            //        rdr = cmd.ExecuteReader();
            //        if (rdr.Read())
            //        {
            //            txtUserType.Text = (rdr.GetString(0));
            //            uId2 = (rdr.GetInt32(1));

            //        }
            //        if ((rdr != null))
            //        {
            //            rdr.Close();
            //        }

            //        if (txtUserType.Text.Trim() == "Admin")
            //        {
            //            this.Hide();
            //            frmMainUI frm = new frmMainUI();
            //            frm.Show();
            //            //this.Visible = false;
            //            //frm.ShowDialog();
            //            frm.lblUser.Text = txtUserName.Text;
            //            //this.Visible = true;
            //            txtPassword.Clear();
            //            txtUserName.Clear();
            //        }
            //        //if (txtUserType.Text.Trim() == "User")
            //        //{

            //        //    MasterPagesForUser frm = new MasterPagesForUser();
            //        //    this.Visible = false;
            //        //    frm.ShowDialog();
            //        //    this.Visible = true;

            //        //}

            //    }


            //    else
            //    {
            //        MessageBox.Show("Login is Failed...Try again !", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //        txtUserName.Clear();
            //        txtPassword.Clear();
            //        txtUserName.Focus();

            //    }
            //    if (myConnection.State == ConnectionState.Open)
            //    {
            //        myConnection.Dispose();
            //    }



            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
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
                btnSignIn_Click(this, new EventArgs());
            }
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
