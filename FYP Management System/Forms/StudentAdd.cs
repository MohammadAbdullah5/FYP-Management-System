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
    public partial class StudentAdd : Form
    {
        public StudentAdd()
        {
            InitializeComponent();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            StudentManage form = new StudentManage();
            form.Show();
            this.Hide();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text != "" && txtEmail.Text != "" && txtRollNo.Text != "" && cmbBoxGender.SelectedItem != null)
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

                    cmd = new SqlCommand("INSERT INTO Student (Id, RegistrationNo) VALUES ((SELECT Id FROM Person WHERE Contact = @Contact AND Email = @Email), @RegistrationNo) ", con);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@RegistrationNo", txtRollNo.Text);
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved");
                }

                catch (FormatException ex)
                {

                }
            }
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
