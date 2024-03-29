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
    public partial class EvaluationMark : Form
    {
        public EvaluationMark()
        {
            InitializeComponent();
            showGroupIDs();
            showEvaluations();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            MainMenu form = new MainMenu();
            form.Show();
            this.Hide();
        }

        private void comboBoxGroup_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxGroup.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                int idx = int.Parse(comboBoxGroup.SelectedValue.ToString());
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT RegistrationNo, FirstName, LastName, Contact, Email, Gender, S.Id As [Id] FROM [Group] AS G JOIN GroupStudent AS GS ON GS.GroupID = G.Id JOIN Student AS S ON S.Id = GS.StudentId JOIN Person AS P ON P.Id = S.Id WHERE G.Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", idx);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["Id"].Visible = false;

                cmd = new SqlCommand("SELECT ProjectId FROM GroupProject WHERE GroupId = @GroupId", con);
                cmd.Parameters.AddWithValue("@GroupId", int.Parse(comboBoxGroup.SelectedValue.ToString()));
                int projId = (Int32)cmd.ExecuteScalar();

                cmd = new SqlCommand("SELECT Title FROM Project WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", projId);
                txtProject.Text = cmd.ExecuteScalar().ToString();
            }
        }

        private void showGroupIDs()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT GroupId FROM [GroupProject]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBoxGroup.DataSource = dt;
            comboBoxGroup.DisplayMember = "GroupId";
            comboBoxGroup.ValueMember = "GroupId";
        }

        private void showEvaluations()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT E.Name FROM Evaluation E LEFT JOIN GroupEvaluation GE ON E.Id = GE.EvaluationId WHERE GE.GroupId IS NULL; ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBoxEvaluation.DataSource = dt;
            comboBoxEvaluation.DisplayMember = "Name";
            comboBoxEvaluation.ValueMember = "Name";
        }

        private void comboBoxEvaluation_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxEvaluation.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                string evalName = comboBoxEvaluation.SelectedValue.ToString();
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT TotalMarks FROM [Evaluation] WHERE Name = @Name", con);
                cmd.Parameters.AddWithValue("@Name", evalName);
                txtTotal.Text = cmd.ExecuteScalar().ToString();                
                numObtained.Maximum = int.Parse(txtTotal.Text);    
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (txtProject.Text != "")
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("INSERT INTO [GroupEvaluation] VALUES (@GroupId, (SELECT Id FROM Evaluation WHERE Name = @Name), @ObtainedMarks, @EvaluationDate)", con);
                    cmd.Parameters.AddWithValue("@GroupId", int.Parse(comboBoxGroup.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("@Name", comboBoxEvaluation.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@ObtainedMarks", numObtained.Value);
                    cmd.Parameters.AddWithValue("@EvaluationDate", DateTime.Now.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Added");
                }

                catch (Exception ex)
                {

                }
            }
        }
    }
}
