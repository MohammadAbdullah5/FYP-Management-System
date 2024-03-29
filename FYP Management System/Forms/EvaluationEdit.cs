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
    public partial class EvaluationEdit : Form
    {
        public EvaluationEdit()
        {
            InitializeComponent();
            showData();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            EvaluationManage form = new EvaluationManage();
            form.Show();
            this.Hide();
        }

        private void showData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id, Name, TotalMarks, TotalWeightage FROM Evaluation", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Id"].Visible = false;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Name")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Evaluation SET Name = @Name WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Name", dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    cmd.Parameters.AddWithValue("@Id", dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Edited");
                }

                else if (dataGridView1.Columns[e.ColumnIndex].Name == "TotalMarks")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Evaluation SET TotalMarks = @TotalMarks WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@TotalMarks", dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    cmd.Parameters.AddWithValue("@Id", dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Edited");
                }

                else if (dataGridView1.Columns[e.ColumnIndex].Name == "TotalWeightage")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Evaluation SET TotalWeightage = @TotalWeightage WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@TotalWeightage", dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    cmd.Parameters.AddWithValue("@Id", dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Edited");
                }
            }

            catch (FormatException ex)
            {
                MessageBox.Show("Wrong input");
            }
        }
    }
}
