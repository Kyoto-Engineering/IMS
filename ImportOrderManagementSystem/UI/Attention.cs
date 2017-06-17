﻿using System;
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
using ImportOrderManagementSystem.Models;

namespace ImportOrderManagementSystem.UI
{
    public partial class Attention : Form
    {
        SqlConnection _con;
        SqlCommand _cmd;
        ConnectionString _cs = new ConnectionString();
        SqlDataReader rdr;
        public int SupplierId;      
        public Attention()
        {
            InitializeComponent();
        }

        private void Attention_Load(object sender, EventArgs e)
        {
            GetSuplier();
        }

        private void GetSuplier()
        {
            try
            {
                _con = new SqlConnection(_cs.DBConn);
                _con.Open();
                string ctt = "SELECT SupplierName FROM Supplier";
                _cmd = new SqlCommand(ctt);
                _cmd.Connection = _con;
                rdr = _cmd.ExecuteReader();
                while (rdr.Read())
                {
                    SupliercomboBox.Items.Add(rdr.GetValue(0).ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SupliercomboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                _con = new SqlConnection(_cs.DBConn);
                _con.Open();
                string cty4 = "SELECT SupplierId FROM Supplier WHERE SupplierName ='" + SupliercomboBox.Text + "'";
                _cmd = new SqlCommand(cty4);
                _cmd.Connection = _con;
                rdr = _cmd.ExecuteReader();
                if (rdr.Read())
                {
                    SupplierId = (rdr.GetInt32(0));
                }
                _con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(AttntextBox.Text))
            {
                MessageBox.Show(@"Please Enter Attention", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                AttntextBox.Focus();
            }

            else if (string.IsNullOrWhiteSpace(EmailtextBox.Text))
            {
                MessageBox.Show(@"Please Enter Email", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                EmailtextBox.Focus();
            }
            else if (!string.IsNullOrWhiteSpace(SupliercomboBox.Text))
            {

                MessageBox.Show(@"Please Select Supplier", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                SupliercomboBox.Focus();
            }
            else
            {
                try
                {

                    _con = new SqlConnection(_cs.DBConn);
                    _con.Open();
                    string query1 = "insert into AttentionDetails(Attention,Email,SupplierId) values (@d1,@d2,@d3)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                    _cmd = new SqlCommand(query1, _con);
                    _cmd.Parameters.AddWithValue("@d1", AttntextBox.Text);
                    _cmd.Parameters.AddWithValue("@d2", EmailtextBox.Text);
                    _cmd.Parameters.AddWithValue("@d3", SupplierId);
                    _cmd.ExecuteNonQuery();
                    _con.Close();
                    MessageBox.Show("Saved successfully", "Record", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Clear()
        {
            AttntextBox.Clear();
            EmailtextBox.Clear();
            SupliercomboBox.SelectedIndex = -1;
        }
    }

}

