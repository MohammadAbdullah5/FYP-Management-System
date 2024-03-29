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

namespace FYP_Management_System.Forms
{
    public partial class AdvisorAdd : Form
    {
        public AdvisorAdd()
        {
            InitializeComponent();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (txtSalary.Text != "" && txtFirstName.Text != "" && txtEmail.Text != "" && cmbBoxGender.SelectedItem != null && cmboBoxDesignation.SelectedItem != null)
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd;

                    cmd = new SqlCommand("Insert Into Person values (@FirstName, @LastName, @Contact, @Email, @DateOfBirth, @Gender)", con);
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", DOB.Value.Date);
                    cmd.Parameters.AddWithValue("@Gender", getId(cmbBoxGender.SelectedItem.ToString(), "GENDER"));

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("INSERT INTO Advisor VALUES((SELECT Id FROM Person WHERE Contact = @Contact and Email = @Email) , @Designation, @Salary);", con);
                    cmd.Parameters.AddWithValue("@Designation", getId(cmboBoxDesignation.SelectedItem.ToString(), "DESIGNATION"));
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                    cmd.Parameters.AddWithValue("@Salary", int.Parse(txtSalary.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Added");
                }

                catch (FormatException ex)
                {
                    MessageBox.Show("Wrong Input in Salary");
                }
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            AdvisorManage form = new AdvisorManage();
            form.Show();
            this.Hide();
        }

        private int getId(string value, string category)
        {
            SqlConnection con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM Lookup WHERE value = @VALUE and Category = @CATEGORY", con);
            cmd.Parameters.AddWithValue("@VALUE", value);
            cmd.Parameters.AddWithValue("@CATEGORY", category);
            return (Int32)cmd.ExecuteScalar();
        }
    }
}
