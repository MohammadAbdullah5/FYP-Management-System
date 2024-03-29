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
    public partial class EvaluationAdd : Form
    {
        public EvaluationAdd()
        {
            InitializeComponent();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            EvaluationManage form = new EvaluationManage();
            form.Show();
            this.Hide();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd;

                    cmd = new SqlCommand("Insert Into Evaluation values (@Name, @TotalMarks, @TotalWeightage)", con);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@TotalMarks", numTotalMarks.Value);
                    cmd.Parameters.AddWithValue("@TotalWeightage", numTotalWeightage.Value);
                    
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved");
                }

                catch (FormatException ex)
                {

                }
            }
        }
    }
}
