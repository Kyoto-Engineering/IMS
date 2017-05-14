using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportOrderManagementSystem.LoginUI
{
    public partial class UserManagementUI : Form
    {
        public UserManagementUI()
        {
            InitializeComponent();
        }

        private void buttonCreateNewUser_Click(object sender, EventArgs e)
        {
            this.Visible = false;
           dynamic  afrm=new frmRegistration();
            afrm.ShowDialog();
            this.Visible = true;
        }

        private void buttonResetPassword_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            dynamic afrm = new ResetPassword();
            afrm.ShowDialog();
            this.Visible = true;
        }
    }
}
