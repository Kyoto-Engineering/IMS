using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportOrderManagementSystem.UI
{
    public partial class InsertPrice : Form
    {
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
    }
}
