using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImportOrderManagementSystem.UI;

namespace ImportOrderManagementSystem.Reports
{
    public partial class ReportsUI : Form
    {
        public int x;
        public ReportsUI()
        {
            InitializeComponent();
        }

        private void ShipAckButton_Click(object sender, EventArgs e)
        {
            ReportByShipAck f2 = new ReportByShipAck();
            this.Visible = false;
            f2.ShowDialog();
            this.Visible = true;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            //this.Hide();
            frmMainUI frm = new frmMainUI();
            this.Visible = false;
            frm.ShowDialog();
            this.Visible = true;
        }

        private void ReportsUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            frmMainUI frm = new frmMainUI();
            frm.Show();
        }

        private void ShipOrdButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReportByShipOrder f2 = new ReportByShipOrder();
            this.Visible = false;
            f2.ShowDialog();
            this.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReportByImpOrder f2 = new ReportByImpOrder();
            this.Visible = false;
            f2.ShowDialog();
            this.Visible = true;
        }
    }
}
