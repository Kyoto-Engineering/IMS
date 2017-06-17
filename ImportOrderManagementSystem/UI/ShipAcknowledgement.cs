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
using ImportOrderManagementSystem.LoginUI;

namespace ImportOrderManagementSystem.UI
{
    public partial class ShipAcknowledgement : Form
    {
        private SqlCommand cmd;
        ConnectionString Cs=new ConnectionString();
        private SqlConnection con;
        private SqlDataReader rdr;
        private string impOd;
        private DataGridViewRow dr;
        private int checkvalue,smId;
        public ShipAcknowledgement()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                con = new SqlConnection(Cs.DBConn);
                string qry =
                    "SELECT ImportOrderProduct.ImportOrderProductId, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemCode, ProductListSummary.ItemDescription,ImportOrderProduct.OrderQty, ImportOrderProduct.BacklogQty, ImportOrders.ImportOrderNo, ImportOrderProduct.ExpectedDateOfArrival FROM ImportOrders INNER JOIN ImportOrderProduct ON ImportOrders.ImpId = ImportOrderProduct.ImpId INNER JOIN ProductListSummary ON ImportOrderProduct.Sl = ProductListSummary.Sl inner join Supplier on ImportOrders.SupplierId=Supplier.SupplierId WHERE BacklogQty>0 and Supplier.SupplierName='" +
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

        private void ShipAcknowledgement_Load(object sender, EventArgs e)
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
                textBox4.Text = dr.Cells[1].Value.ToString();
                textBox3.Text = dr.Cells[3].Value.ToString();
                checkvalue =Convert.ToInt32( dr.Cells[5].Value.ToString());
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
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Select A Product First");
                }
                else if (string.IsNullOrWhiteSpace(textBox2.Text) || Convert.ToInt32(textBox2.Text)<1)
                {
                    MessageBox.Show("Product With Zero , MInus or Empty Quantity Can Not Be Added");
                }
                else if (Convert.ToInt32(textBox2.Text)>checkvalue)
                {
                    MessageBox.Show("Receive Amount Cannot Be greater Than the Backloq Quantity");
                }
                else
                {


                    if (listView1.Items.Count < 1)
                    {
                        ListViewItem l1 = new ListViewItem();
                        l1.Text = impOd;
                        l1.SubItems.Add(textBox4.Text);
                        l1.SubItems.Add(textBox1.Text);
                        l1.SubItems.Add(textBox3.Text);
                        l1.SubItems.Add(textBox2.Text);
                        listView1.Items.Add(l1);
                        ClearselectedProduct();
                    }
                    else
                    {
                        if (listView1.FindItemWithText(impOd) == null)
                        {
                            ListViewItem l2 = new ListViewItem();
                            l2.Text = impOd;
                            l2.SubItems.Add(textBox4.Text);
                            l2.SubItems.Add(textBox1.Text);
                            l2.SubItems.Add(textBox3.Text);
                            l2.SubItems.Add(textBox2.Text);
                            listView1.Items.Add(l2);
                            ClearselectedProduct();
                        }
                        else
                        {
                            MessageBox.Show("This Prduct already Added");
                            ClearselectedProduct();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Shipment Method");
            }
        }

        private void ClearselectedProduct()
        {
            impOd = null;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                if (listView1.Items.Count>1)
                {

                    con = new SqlConnection(Cs.DBConn);
                    string q1 =
                        "INSERT INTO ShipmentOrder (ExpectedShipmentDate,ExpectedDateOfDelivery,SMId,UserId,EntryDate) VALUES (@d1,@d2,@d3,@d4,@d5)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                    cmd=new SqlCommand(q1,con);
                    cmd.Parameters.AddWithValue("@d1", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@d2", dateTimePicker2.Value);
                    cmd.Parameters.AddWithValue("@d3",smId);
                    cmd.Parameters.AddWithValue("@d4", LoginForm.uId2);
                    cmd.Parameters.AddWithValue("@d5", DateTime.UtcNow.ToLocalTime());
                    con.Open();
                    string ShID=cmd.ExecuteScalar().ToString();
                    con.Close();
                    for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    {
                        string imprno = listView1.Items[i].Text;
                        string qty = listView1.Items[i].SubItems[4].Text;
                        string query =
                            "INSERT INTO ShipmentProduct(ImportOrderProductId,ShipmentProductQty,ShipmentId)Values(@d1,@d2,@d3)";
                        cmd=new SqlCommand(query,con);
                        cmd.Parameters.AddWithValue("@d1", imprno);
                        cmd.Parameters.AddWithValue("@d2",qty);
                        cmd.Parameters.AddWithValue("@d3", ShID);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        string query2 =
                            "UPDATE ImportOrderProduct SET BacklogQty = BacklogQty -@d1 WHERE (ImportOrderProductId = @d2)";
                        cmd = new SqlCommand(query2, con);
                        cmd.Parameters.AddWithValue("@d1", qty);
                        cmd.Parameters.AddWithValue("@d2", imprno);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    MessageBox.Show("Shipment Order Done");
                }
                else
                {
                    MessageBox.Show("No Pruduct Added");
                }
                
            }
            else
            {
                MessageBox.Show("May be You forgot to add Last Selected Product\r\n Add The Product");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            con=new SqlConnection(Cs.DBConn);
            string query = "SELECT SMId FROM ShipMood where ShippingMood='" + comboBox2.Text + "'";
            cmd=new SqlCommand(query,con);
            con.Open();
            rdr=cmd.ExecuteReader();
            if (rdr.Read())
            {
                smId = Convert.ToInt32(rdr["SMId"]);
            }
            con.Close();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

  }
}
