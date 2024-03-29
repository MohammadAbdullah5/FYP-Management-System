﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FYP_Management_System.Forms
{
    public partial class AdvisorEdit : Form
    {
        public AdvisorEdit()
        {
            InitializeComponent();
            showData();
        }

        private void showData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT FirstName, LastName, Contact, DateOfBirth, Email, Salary FROM Advisor AS A JOIN Person AS P ON P.Id = A.Id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            DataGridViewColumn col = dataGridView1.Columns["Gender"];
            col.DisplayIndex = 6;

            DataGridViewColumn colu = dataGridView1.Columns["Designation"];
            col.DisplayIndex = 3;
        }
        
        private void cmdExit_Click(object sender, EventArgs e)
        {
            AdvisorManage form = new AdvisorManage();
            form.Show();
            this.Hide();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Salary")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Advisor SET Salary = @Salary FROM Advisor JOIN Person ON Advisor.Id = Person.Id WHERE CONVERT(nvarchar(MAX), Contact) = @Contact", con);
                    cmd.Parameters.AddWithValue("@Salary", dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    cmd.Parameters.AddWithValue("@Contact", dataGridView1.Rows[e.RowIndex].Cells["Contact"].Value.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Edited");
                }

                else if (dataGridView1.Columns[e.ColumnIndex].Name == "FirstName")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Person SET FirstName = @FirstName WHERE CONVERT(nvarchar(MAX), Contact) = @Contact", con);
                    cmd.Parameters.AddWithValue("@FirstName", dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    cmd.Parameters.AddWithValue("@Contact", dataGridView1.Rows[e.RowIndex].Cells["Contact"].Value.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Edited");
                }

                else if (dataGridView1.Columns[e.ColumnIndex].Name == "LastName")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Person SET LastName = @LastName WHERE CONVERT(nvarchar(MAX), Contact) = @Contact", con);
                    cmd.Parameters.AddWithValue("@LastName", dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    cmd.Parameters.AddWithValue("@Contact", dataGridView1.Rows[e.RowIndex].Cells["Contact"].Value.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Edited");
                }

                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Contact")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Person SET Contact = @Contact WHERE CONVERT(nvarchar(MAX), Email) = @Email", con);
                    cmd.Parameters.AddWithValue("@Contact", dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    cmd.Parameters.AddWithValue("@Email", dataGridView1.Rows[e.RowIndex].Cells["Email"].Value.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Edited");
                }

                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Email")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Person SET Email = @Email WHERE CONVERT(nvarchar(MAX), Contact) = @Contact", con);
                    cmd.Parameters.AddWithValue("@Email", dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    cmd.Parameters.AddWithValue("@Contact", dataGridView1.Rows[e.RowIndex].Cells["Contact"].Value.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Edited");
                }

                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Gender")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Person SET Gender = @Gender WHERE CONVERT(nvarchar(MAX), Contact) = @Contact", con);
                    cmd.Parameters.AddWithValue("@Gender", getId(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), "GENDER"));
                    cmd.Parameters.AddWithValue("@Contact", dataGridView1.Rows[e.RowIndex].Cells["Contact"].Value.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Edited");
                }

                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Designation")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Advisor SET Designation = @Designation FROM Advisor JOIN Person ON Advisor.Id = Person.Id WHERE CONVERT(nvarchar(MAX), Contact) = @Contact", con);
                    cmd.Parameters.AddWithValue("@Designation", getId(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), "DESIGNATION"));
                    cmd.Parameters.AddWithValue("@Contact", dataGridView1.Rows[e.RowIndex].Cells["Contact"].Value.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Edited");
                }

                else if (dataGridView1.Columns[e.ColumnIndex].Name == "DateOfBirth")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Person SET DateOfBirth = @DateOfBirth WHERE CONVERT(nvarchar(MAX), Contact) = @Contact", con);
                    string date = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    if (IsValidDate(date))
                    {
                        cmd.Parameters.AddWithValue("@DateOfBirth", date);
                        cmd.Parameters.AddWithValue("@Contact", dataGridView1.Rows[e.RowIndex].Cells["Contact"].Value.ToString());
                        cmd.ExecuteNonQuery();
                    }

                    else
                    {
                        MessageBox.Show("Wrong Format. The format should be (dd-MM-yyyy). The previous date is not edited.");
                    }
                }
            }

            catch (FormatException ex)
            {
                MessageBox.Show("Wrong Input");
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

        static bool IsValidDate(string input)
        {
            string expectedFormat = "dd/MM/yyyy";
            char[] possibleSeparators = { '/', '-' };

            if (DateTime.TryParseExact(input, expectedFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                if (parsedDate.Day <= DateTime.DaysInMonth(parsedDate.Year, parsedDate.Month))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
