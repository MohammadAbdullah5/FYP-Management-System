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
    public partial class EvaluationDelete : Form
    {
        public EvaluationDelete()
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
            SqlCommand cmd = new SqlCommand("Select Id, Name, TotalMarks, TotalWeightage from Evaluation", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("DELETE from Evaluation WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", int.Parse(dataGridView1.CurrentRow.Cells["Id"].Value.ToString()));
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Row Deleted");
                    showData();
                }
            }

            catch (Exception ex)
            {

            }
        }
    }
}
