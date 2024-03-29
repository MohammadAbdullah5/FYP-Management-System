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
    public partial class StudentDelete : Form
    {
        public StudentDelete()
        {
            InitializeComponent();
            showData();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            StudentManage form = new StudentManage();
            form.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("SELECT Id FROM Student WHERE RegistrationNo = @RegistrationNo", con);
                    cmd.Parameters.AddWithValue("@RegistrationNo", dataGridView1.CurrentRow.Cells["RegistrationNo"].Value.ToString());
                    int studentId = (Int32)cmd.ExecuteScalar();

                    cmd = new SqlCommand("DELETE from GroupStudent WHERE StudentId = @StudentId", con);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("DELETE from Student WHERE CONVERT(nvarchar(MAX), RegistrationNo) = @RegistrationNo", con);
                    cmd.Parameters.AddWithValue("@RegistrationNo", dataGridView1.CurrentRow.Cells["RegistrationNo"].Value.ToString());
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("DELETE from Person WHERE CONVERT(nvarchar(MAX), Contact) = @Contact", con);
                    cmd.Parameters.AddWithValue("@Contact", dataGridView1.CurrentRow.Cells["Contact"].Value.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Row Deleted");
                    showData();
                }
            }

            catch(SqlException ex)
            {

            }
        }

        private void showData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select RegistrationNo, FirstName, LastName, Contact, Email, DateOfBirth, (SELECT Value FROM Lookup WHERE Id = Gender) AS Gender from Student AS S JOIN Person AS P ON P.Id = S.Id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
