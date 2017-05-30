using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImportOrderManagementSystem.DbGateway;
using ImportOrderManagementSystem.LoginUI;

namespace ImportOrderManagementSystem.UI
{
    public partial class frmSupplierRegistration : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs = new ConnectionString();
        public string user_id, countryid, divisionIdCA, districtIdCA, thanaIdCA, postofficeIdCA;
        public int CAdistrictid, currentSupplierId, suppliertypeid, companyId;
        private int affectedRows1, affectedRows3;
        private bool companyCreated;
        public frmSupplierRegistration()
        {
            InitializeComponent();
        }

        private void frmSupplierRegistration_Load(object sender, EventArgs e)
        {
            companyCreated = LoadCompany();
            user_id = LoginForm.uId2.ToString();
            
            FillCountry();
            CountrycomboBox.SelectedItem = "Bangladesh";
            FillSupplierType();
            FillDivisionCombo();
            //DisableAll();
        }

        public void FillCountry()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select Countries.CountryName from Countries  order by Countries.CountryId";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    CountrycomboBox.Items.Add(rdr[0]);
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctt = "select CountryId from Countries  where  Countries.CountryName='" +
                             CountrycomboBox.Text + "' ";
                cmd = new SqlCommand(ctt);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    countryid = (rdr.GetString(0));
                }          
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void FillSupplierType()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select SupplierType.SupplierTypeName from SupplierType  order by SupplierType.SupplierTypeId";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    SupplierTypecomboBox.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        public void FillDivisionCombo()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Divisions.Division) from Divisions  order by Divisions.Division asc ";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cDivisionCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cDivisionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cDistCombo.Enabled = true;
            cDistCombo.Items.Clear();
            cDistCombo.ResetText();
            cThanaCombo.Items.Clear();
            cThanaCombo.ResetText();
            cPostOfficeCombo.Items.Clear();
            cPostOfficeCombo.ResetText();

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Divisions.Division_ID)  from Divisions WHERE Divisions.Division=@find";

                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "Division"));
                cmd.Parameters["@find"].Value = cDivisionCombo.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    divisionIdCA = (rdr.GetString(0));

                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                cDivisionCombo.Text = cDivisionCombo.Text.Trim();
                cDistCombo.Items.Clear();
                cDistCombo.ResetText();
                cThanaCombo.Items.Clear();
                cThanaCombo.ResetText();
                cThanaCombo.SelectedIndex = -1;
                cPostOfficeCombo.Items.Clear();
                cPostOfficeCombo.ResetText();
                cPostOfficeCombo.SelectedIndex = -1;
                cPostCodeTextBox.Clear();
                cDistCombo.Enabled = true;
                cDistCombo.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Districts.District) from Districts  Where Districts.Division_ID = '" +
                            divisionIdCA + "' order by Districts.District asc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cDistCombo.Items.Add(rdr[0]);
                }
                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cThanaCombo.Enabled = false;
            cPostOfficeCombo.Enabled = false;
        }

        private void cDistCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Districts.D_ID)  from Districts WHERE Districts.District=@find";

                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "District"));
                cmd.Parameters["@find"].Value = cDistCombo.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    districtIdCA = (rdr.GetString(0));

                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                cDistCombo.Text = cDistCombo.Text.Trim();
                cThanaCombo.Items.Clear();
                cThanaCombo.ResetText();
                cPostOfficeCombo.Items.Clear();
                cPostOfficeCombo.ResetText();
                cPostOfficeCombo.SelectedIndex = -1;
                cPostOfficeCombo.Enabled = false;
                cPostCodeTextBox.Clear();
                cThanaCombo.Enabled = true;
                cThanaCombo.Focus();


                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Thanas.Thana) from Thanas  Where Thanas.D_ID = '" + districtIdCA +
                            "' order by Thanas.Thana asc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cThanaCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cThanaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            cmd = con.CreateCommand();

            cmd.CommandText = "select D_ID from Districts WHERE District= '" + cDistCombo.Text + "'";

            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                CAdistrictid = rdr.GetInt32(0);
            }
            if ((rdr != null))
            {
                rdr.Close();
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Thanas.T_ID)  from Thanas WHERE Thanas.Thana=@find AND Thanas.D_ID='" +
                             CAdistrictid + "'";
                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "Thana"));
                cmd.Parameters["@find"].Value = cThanaCombo.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    thanaIdCA = (rdr.GetString(0));

                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                cThanaCombo.Text = cThanaCombo.Text.Trim();
                cPostOfficeCombo.Items.Clear();
                cPostOfficeCombo.ResetText();
                cPostCodeTextBox.Clear();
                cPostOfficeCombo.Enabled = true;
                cPostOfficeCombo.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(PostOffice.PostOfficeName) from PostOffice  Where PostOffice.T_ID = '" +
                            thanaIdCA + "' order by PostOffice.PostOfficeName asc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cPostOfficeCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cPostOfficeCombo.SelectedIndex = -1;
        }

        private void cPostOfficeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk =
                    "SELECT  RTRIM(PostOffice.PostOfficeId),RTRIM(PostOffice.PostCode) from PostOffice WHERE PostOffice.PostOfficeName=@find";
                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "PostOfficeName"));
                cmd.Parameters["@find"].Value = cPostOfficeCombo.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    postofficeIdCA = (rdr.GetString(0));
                    cPostCodeTextBox.Text = (rdr.GetString(1));
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

        private void CountrycomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CountrycomboBox.Text == "Bangladesh")
            {
                enableAll();
                groupBox6.Hide();
                groupBox2.Show();
                saveButton.Location = new Point(922, 276);
            }
            else
            {
                enableAll();
                groupBox2.Hide();
                groupBox6.Show();
                groupBox6.Location = new Point(470, 9);
                saveButton.Location = new Point(650, 149);
                
            }
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(CountryId) from Countries  where  Countries.CountryName='" +
                            CountrycomboBox.Text + "' ";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    countryid = (rdr.GetString(0));
                    
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void enableAll()
        {
            SupplierTypecomboBox.Enabled = true;
            SupplierNametextBox.Enabled = true;
            PhonetextBox.Enabled = true;
            FaxtextBox.Enabled = true;
            EmailAddresstextBox.Enabled = true;
            WebSiteUrltextBox.Enabled = true;
            CustomerCodetextBox.Enabled = true;
            
        }
        private void DisableAll()
        {
            SupplierTypecomboBox.Enabled = false;
            SupplierNametextBox.Enabled = false;
            PhonetextBox.Enabled = false;
            FaxtextBox.Enabled = false;
            EmailAddresstextBox.Enabled = false;
            WebSiteUrltextBox.Enabled = false;
            CustomerCodetextBox.Enabled = false;

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
        private bool ValidateControlls()
        {
            bool validate = true;

            if (!companyCreated)
            {
                MessageBox.Show(@"Company is not created Yet" + "\n" + @"Please Create Company First", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
                validate = false;
            }

          
            else  if (string.IsNullOrEmpty(SupplierTypecomboBox.Text))
            {
                MessageBox.Show(@"Please select Supplier Type", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                validate = false;
                SupplierTypecomboBox.Focus();
            }

            else if (string.IsNullOrEmpty(SupplierNametextBox.Text))
            {
                MessageBox.Show(@"Please enter Supplier  name", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                validate = false;
                SupplierNametextBox.Focus();
            }

            if (CountrycomboBox.Text == "Bangladesh")
            {

                if (string.IsNullOrEmpty(cDivisionCombo.Text))
                {
                    MessageBox.Show(@"Please select division", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    validate = false;
                    cDivisionCombo.Focus();
                }
                else if (string.IsNullOrWhiteSpace(cDistCombo.Text))
                {
                    MessageBox.Show(@"Please Select district", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    validate = false;
                    cDistCombo.Focus();
                }
                else if (string.IsNullOrWhiteSpace(cThanaCombo.Text))
                {
                    MessageBox.Show(@"Please select Thana", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    validate = false;
                    cThanaCombo.Focus();
                }

                else if (string.IsNullOrWhiteSpace(cPostOfficeCombo.Text))
                {
                    MessageBox.Show(@"Please Select Post Office", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    validate = false;
                    cPostOfficeCombo.Focus();
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(StreettextBox.Text) &&
                    string.IsNullOrWhiteSpace(StatetextBox.Text) &&
                    string.IsNullOrWhiteSpace(PostalCodetextBox.Text))
                {
                    MessageBox.Show("Please enter Addresses!", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    validate = false;
                    StreettextBox.Focus();

                }
            }

            if (ValidateSupplier())
            {
                validate = false;
            }

            return validate;
        }

        private bool ValidateSupplier()
        {
            List<Supplier> suppliers = new List<Supplier>();

            con = new SqlConnection(cs.DBConn);
            con.Open();
            string ct3 =
                "SELECT SupplierName, Phone, Fax, Email, WebSiteUrl, Code FROM Supplier where  Supplier.SupplierName='" +
                SupplierNametextBox.Text + "'";
            cmd = new SqlCommand(ct3, con);
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.HasRows)
                {
                    Supplier x = new Supplier();
                    x.Name = rdr.GetString(0);

                    if (!DBNull.Value.Equals(rdr["Phone"]))
                    {
                        x.Phone = rdr.GetString(1);
                    }
                    else
                    {
                        x.Phone = null;
                    }

                    if (!DBNull.Value.Equals(rdr["Fax"]))
                    {
                        x.Fax = rdr.GetString(2);
                    }
                    else
                    {
                        x.Fax = null;
                    }

                    if (!DBNull.Value.Equals(rdr["Email"]))
                    {
                        x.Email = rdr.GetString(3);
                    }
                    else
                    {
                        x.Email = null;
                    }


                    if (!DBNull.Value.Equals(rdr["WebSiteUrl"]))
                    {
                        x.WebSiteUrl = rdr.GetString(4);
                    }
                    else
                    {
                        x.WebSiteUrl = null;
                    }


                    if (!DBNull.Value.Equals(rdr["Code"]))
                    {
                        x.Code = rdr.GetString(5);
                    }
                    else
                    {
                        x.Code = null;
                    }

                    suppliers.Add(x);
                }
            }
            foreach (Supplier p in suppliers)
            {
                if (p.Name == SupplierNametextBox.Text && p.Phone == PhonetextBox.Text)
                {
                    MessageBox.Show(@"This Person Exists,Please Input another one" + "\n" + @"Or Use another Phone",
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SupplierNametextBox.Clear();
                    PhonetextBox.Clear();
                    //WebServiceUrltextBox.Clear();
                    //EmailAddresstextBox.Clear();
                    //VendorNametextBox.Focus();
                    con.Close();
                    return true;
                }



                if (p.Name == SupplierNametextBox.Text && p.Fax == FaxtextBox.Text)
                {
                    MessageBox.Show(@"This Person Exists,Please Input another one" + "\n" + @"Or Use another Fax",
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SupplierNametextBox.Clear();
                    FaxtextBox.Clear();
                    //WebServiceUrltextBox.Clear();
                    //EmailAddresstextBox.Clear();
                    //VendorNametextBox.Focus();
                    con.Close();
                    return true;
                }
                if (p.Name == SupplierNametextBox.Text && p.Email == EmailAddresstextBox.Text)
                {
                    MessageBox.Show(@"This Person Exists,Please Input another one" + "\n" + @"Or Use another Email",
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SupplierNametextBox.Clear();
                    //PhonetextBox.Clear();
                    //WebServiceUrltextBox.Clear();
                    EmailAddresstextBox.Clear();
                    con.Close();
                    return true;
                }

                if (p.Name == SupplierNametextBox.Text && p.WebSiteUrl == WebSiteUrltextBox.Text)
                {
                    MessageBox.Show(
                        @"This Person Exists,Please Input another one" + "\n" + @"Or Use another Web Service URL",
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SupplierNametextBox.Clear();
                    //PhonetextBox.Clear();
                    WebSiteUrltextBox.Clear();
                    //EmailAddresstextBox.Clear();

                    con.Close();
                    return true;
                }

                if (p.Name == SupplierNametextBox.Text && p.Code == CustomerCodetextBox.Text)
                {
                    MessageBox.Show(
                        @"This Person Exists,Please Input another one" + "\n" + @"Or Use another Customer Code",
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SupplierNametextBox.Clear();
                    //PhonetextBox.Clear();
                    CustomerCodetextBox.Clear();
                    //EmailAddresstextBox.Clear();

                    con.Close();
                    return true;
                }

            }
            return false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SupplierNametextBox.Text) && string.IsNullOrEmpty(PhonetextBox.Text) && string.IsNullOrEmpty(FaxtextBox.Text) && string.IsNullOrEmpty(EmailAddresstextBox.Text) &&
                string.IsNullOrEmpty(WebSiteUrltextBox.Text) && string.IsNullOrEmpty(CustomerCodetextBox.Text))
            {
                MessageBox.Show(@"Please insert Phone or Fax or Email or Website", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ValidateControlls())
            {

                try
                {
                    if (CountrycomboBox.Text == "Bangladesh")
                    {
                        SaveAddress("SupplierCorporateAddresses");
                        SaveSupplier();
                        MessageBox.Show(@"Registered successfully", @"Record", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        Reset1();
                        CountrycomboBox.SelectedItem = "Bangladesh";
                    }
                    else
                    {
                        SaveAddress("SupplierForeignAddresses");
                        SaveSupplier();
                        MessageBox.Show(@"Registered successfully", @"Record", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        Reset2();
                        CountrycomboBox.SelectedItem = "Bangladesh";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public void Reset2()
        {
            SupplierTypecomboBox.SelectedIndex = -1;
            SupplierNametextBox.Clear();
            PhonetextBox.Clear();
            FaxtextBox.Clear();
            EmailAddresstextBox.Clear();
            WebSiteUrltextBox.Clear();
            CustomerCodetextBox.Clear();
            ResetForeignAddress();

        }

        public void Reset1()
        {
            SupplierTypecomboBox.SelectedIndex = -1;
            SupplierNametextBox.Clear();
            PhonetextBox.Clear();
            FaxtextBox.Clear();
            EmailAddresstextBox.Clear();
            WebSiteUrltextBox.Clear();
            CustomerCodetextBox.Clear();
            ResetCorporateAddress();
        }

        public void ResetCorporateAddress()
        {
            cFlatNoTextBox.Clear();
            cHouseNoTextBox.Clear();
            cRoadNoTextBox.Clear();
            blocktextBox.Clear();
            cAreaTextBox.Clear();
            cContactNoTextBox.Clear();
            LandmarktextBox.Clear();
            cPostCodeTextBox.Clear();
            cPostOfficeCombo.SelectedIndex = -1;
            cThanaCombo.SelectedIndex = -1;
            cDistCombo.SelectedIndex = -1;
            cDivisionCombo.SelectedIndex = -1;
        }

        public void ResetForeignAddress()
        {
            StreettextBox.Clear();
            StatetextBox.Clear();
            PostalCodetextBox.Clear();
        }

        private void SaveSupplier()
        {
            string newname = SupplierNametextBox.Text;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string query =
                "INSERT INTO Supplier (SupplierName, Phone, Fax, Email, WebSiteUrl, Code, SupplierTypeId, CADId, FAddressId, CountryId, CompanyId, UserId) VALUES(@nname,@nphone,@nfax, @nemail, @nweburl, @ncode, @d1, @d2, @d3, @d4, @d5, @d6)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@nname", newname);
           
            cmd.Parameters.Add(new SqlParameter("@nphone",
                string.IsNullOrEmpty(PhonetextBox.Text) ? (object)DBNull.Value : PhonetextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@nfax",
                string.IsNullOrEmpty(FaxtextBox.Text) ? (object)DBNull.Value : FaxtextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@nemail",
                string.IsNullOrEmpty(EmailAddresstextBox.Text) ? (object)DBNull.Value : EmailAddresstextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@nweburl",
                string.IsNullOrEmpty(WebSiteUrltextBox.Text) ? (object)DBNull.Value : WebSiteUrltextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@ncode",
                string.IsNullOrEmpty(CustomerCodetextBox.Text) ? (object)DBNull.Value : CustomerCodetextBox.Text));
            cmd.Parameters.AddWithValue("@d1", suppliertypeid);
            cmd.Parameters.AddWithValue("@d2", (object)affectedRows1 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@d3", (object)affectedRows3 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@d4", countryid);
            cmd.Parameters.AddWithValue("@d5", companyId);
            cmd.Parameters.AddWithValue("@d6", user_id);
            currentSupplierId = (int)(cmd.ExecuteScalar());
            con.Close();
        }

        private void SaveAddress(string tblName1)
        {
            string tableName = tblName1;
            if (tableName == "SupplierCorporateAddresses")
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string insertQ = "insert into " + tableName +
                                 "(PostOfficeId,CFlatNo,CHouseNo,CRoadNo,CBlock,CArea,CLandmark,CContactNo) Values(@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11)" +
                                 "SELECT CONVERT(int, SCOPE_IDENTITY())";
                cmd = new SqlCommand(insertQ);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@d4",
                    string.IsNullOrEmpty(postofficeIdCA) ? (object) DBNull.Value : postofficeIdCA));
                cmd.Parameters.Add(new SqlParameter("@d5",
                    string.IsNullOrEmpty(cFlatNoTextBox.Text) ? (object) DBNull.Value : cFlatNoTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d6",
                    string.IsNullOrEmpty(cHouseNoTextBox.Text) ? (object) DBNull.Value : cHouseNoTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d7",
                    string.IsNullOrEmpty(cRoadNoTextBox.Text) ? (object) DBNull.Value : cRoadNoTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d8",
                    string.IsNullOrEmpty(blocktextBox.Text) ? (object) DBNull.Value : blocktextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d9",
                    string.IsNullOrEmpty(cAreaTextBox.Text) ? (object) DBNull.Value : cAreaTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d10",
                    string.IsNullOrEmpty(LandmarktextBox.Text) ? (object) DBNull.Value : LandmarktextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d11",
                    string.IsNullOrEmpty(cContactNoTextBox.Text) ? (object) DBNull.Value : cContactNoTextBox.Text));
                affectedRows1 = (int) cmd.ExecuteScalar();
                con.Close();
            }
            else if (tableName == "SupplierForeignAddresses")
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string Qury = "insert into " + tableName +
                              "(Street,State,PostalCode) Values(@d2,@d3,@d4)" +
                              "SELECT CONVERT(int, SCOPE_IDENTITY())";
                cmd = new SqlCommand(Qury);
                cmd.Connection = con;
                
                cmd.Parameters.Add(new SqlParameter("@d2",
                    string.IsNullOrEmpty(StreettextBox.Text) ? (object) DBNull.Value : StreettextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d3",
                    string.IsNullOrEmpty(StatetextBox.Text) ? (object) DBNull.Value : StatetextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d4",
                    string.IsNullOrEmpty(PostalCodetextBox.Text) ? (object) DBNull.Value : PostalCodetextBox.Text));
                affectedRows3 = (int) cmd.ExecuteScalar();
                con.Close();
            }
        }

        private void SupplierTypecomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select SupplierTypeId from SupplierType  where  SupplierType.SupplierTypeName='" + SupplierTypecomboBox.Text + "' ";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    //jobTitleId = (rdr.GetString(0));
                    //jobTitleId = rdr.GetInt32(0);
                    suppliertypeid = rdr.GetInt32(0);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSupplierRegistration_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            frmMainUI frm = new frmMainUI();
            frm.Show();
        }

        private void SupplierNametextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PhonetextBox.Focus();
                e.Handled = true;
            }
        }

        private void SupplierNametextBox_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SupplierNametextBox.Text))
            {
                string suppliername = SupplierNametextBox.Text.Trim();
                Regex mRegxExpression;
                int Minlen = 3;

                mRegxExpression = new Regex(@"^[A-Za-z]+[\s][A-Za-z]+[.][A-Za-z]+$");

                if ((!mRegxExpression.IsMatch(suppliername)) && (!(SupplierNametextBox.Text.Length >= Minlen)))
                {

                    MessageBox.Show("Name at least Three letters!", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SupplierNametextBox.Clear();
                    SupplierNametextBox.Focus();

                }
            }

        }

        private void PhonetextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FaxtextBox.Focus();
                e.Handled = true;
            }
        }

        private void PhonetextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }

        private void PhonetextBox_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PhonetextBox.Text))
            {
                string PhnNo = PhonetextBox.Text.Trim();
                int Minlen = 8;



                if (!(PhonetextBox.Text.Length >= Minlen))
                {

                    MessageBox.Show("Please type valid Phone No.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PhonetextBox.Clear();
                    PhonetextBox.Focus();

                }
            }
        }

        private void FaxtextBox_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(FaxtextBox.Text))
            {
                string faxno = FaxtextBox.Text.Trim();
                Regex mmRegxExpression;

                mmRegxExpression = new Regex(@"\+[0-9]{1,3}\([0-9]{3}\)[0-9]{7}");

                if (!mmRegxExpression.IsMatch(faxno))
                {

                    MessageBox.Show("Please type valid Fax Number.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FaxtextBox.Clear();
                    FaxtextBox.Focus();

                }
            }
        }

        private void FaxtextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EmailAddresstextBox.Focus();
                e.Handled = true;
            }
        }

        private void EmailAddresstextBox_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(EmailAddresstextBox.Text))
            {
                string emailId = EmailAddresstextBox.Text.Trim();
                Regex mRegxExpression;

                mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

                if (!mRegxExpression.IsMatch(emailId))
                {

                    MessageBox.Show("Please type Valid email Address.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    EmailAddresstextBox.Clear();
                    EmailAddresstextBox.Focus();
                }
            }
        }

        private void EmailAddresstextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                WebSiteUrltextBox.Focus();
                e.Handled = true;
            }
        }

        private void WebSiteUrltextBox_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(WebSiteUrltextBox.Text))
            {
                string urlAddress = WebSiteUrltextBox.Text.Trim();
                Regex mRegxExpression;
                Regex mRegxExpression1;

                mRegxExpression = new Regex(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$");
                mRegxExpression1 = new Regex(@"^(www.)[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$");

                if ((!mRegxExpression.IsMatch(urlAddress)) && (!mRegxExpression1.IsMatch(urlAddress)))
                {

                    MessageBox.Show("Please type Valid Website!", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    WebSiteUrltextBox.Clear();
                    WebSiteUrltextBox.Focus();
                }
            }
        }

        private void WebSiteUrltextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CustomerCodetextBox.Focus();
                e.Handled = true;
            }
        }

        private void CustomerCodetextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cFlatNoTextBox.Focus();
                e.Handled = true;
            }
        }

        private void cFlatNoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cHouseNoTextBox.Focus();
                e.Handled = true;
            }
        }

        private void cHouseNoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cRoadNoTextBox.Focus();
                e.Handled = true;
            }
        }

        private void cRoadNoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                blocktextBox.Focus();
                e.Handled = true;
            }
        }

        private void blocktextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                cAreaTextBox.Focus();
                e.Handled = true;
            }
        }

        private void cAreaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LandmarktextBox.Focus();
                e.Handled = true;
            }
        }

        private void LandmarktextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                cContactNoTextBox.Focus();
                e.Handled = true;
            }
        }

        private void cContactNoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void cContactNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }

        private void StreettextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                StatetextBox.Focus();
                e.Handled = true;
            }
        }

        private void StatetextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                PostalCodetextBox.Focus();
                e.Handled = true;
            }
        }

        private void PostalCodetextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                saveButton_Click(this, new EventArgs());
            }
        }
    }
}
