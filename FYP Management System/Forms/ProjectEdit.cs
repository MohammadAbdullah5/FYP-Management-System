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
    public partial class ProjectEdit : Form
    {
        public ProjectEdit()
        {
            InitializeComponent();
            showData();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            ProjectManage form = new ProjectManage();
            form.Show();
            this.Hide();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Description")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("UPDATE Project SET Description = @Description WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Description", dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                cmd.Parameters.AddWithValue("@Id", int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Edited");
            }

            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Title")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("UPDATE Project SET Title = @Title WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Title", dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                cmd.Parameters.AddWithValue("@Id",int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Edited");
            }
        }

        private void showData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id, Description, Title FROM Project", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Id"].ReadOnly = true;
        }
    }
}
