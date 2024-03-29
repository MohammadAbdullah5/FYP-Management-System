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
    public partial class AdvisorDelete : Form
    {
        public AdvisorDelete()
        {
            InitializeComponent();
            showData();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            AdvisorManage form = new AdvisorManage();
            form.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE from Advisor WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", dataGridView1.CurrentRow.Cells["Id"].Value.ToString());
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("DELETE from Person WHERE CONVERT(nvarchar(MAX), Contact) = @Contact", con);
                cmd.Parameters.AddWithValue("@Contact", dataGridView1.CurrentRow.Cells["Contact"].Value.ToString());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Row Deleted");
                showData();
            }
        }

        private void showData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select A.Id, FirstName, LastName, Contact, Email, DateOfBirth, (SELECT Value FROM Lookup WHERE Lookup.Id = Gender) AS Gender from Advisor AS A JOIN Person AS P ON P.Id = A.Id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
