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
    public partial class ProjectAssign : Form
    {
        public ProjectAssign()
        {
            InitializeComponent();
            showGroupIDs();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            MainMenu form = new MainMenu();
            form.Show();
            this.Hide();
        }

        private void showGroupIDs()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM [Group]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBoxGroup.DataSource = dt; 
            comboBoxGroup.DisplayMember = "Id";
            comboBoxGroup.ValueMember = "Id";
        }

        private void comboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
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
                showProjects();
            }
        }

        private void showProjects()
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Title FROM [Project]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBoxProject.DataSource = dt;
            comboBoxProject.DisplayMember = "Title";
            comboBoxProject.ValueMember = "Title";

        }

        private void cmdAssign_Click(object sender, EventArgs e)
        {
            if (comboBoxProject.SelectedValue != null && comboBoxGroup.SelectedValue != null)
            {
                try
                {
                    List<int> studentIds = new List<int>();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string studentId = row.Cells["Id"].Value.ToString();
                            studentIds.Add(int.Parse(studentId));                            
                        }
                    }

                    var con = Configuration.getInstance().getConnection();
                    string title = comboBoxProject.SelectedValue.ToString();

                    SqlCommand cmd = new SqlCommand("SELECT Id FROM Project WHERE Title = @Title", con);
                    cmd.Parameters.AddWithValue("@Title", title);
                    int projectId = (Int32)cmd.ExecuteScalar();

                    int grpId = int.Parse(comboBoxGroup.SelectedValue.ToString());
                    
                    cmd = new SqlCommand("INSERT INTO GroupProject VALUES (@ProjectId, @GroupId, @AssignmentDate)", con);
                    cmd.Parameters.AddWithValue("@GroupId", grpId);
                    cmd.Parameters.AddWithValue("@ProjectId", projectId);
                    cmd.Parameters.AddWithValue("@AssignmentDate", assignmentDate.Value.Date);
                    cmd.ExecuteNonQuery();

                    for(int i = 0; i < studentIds.Count; i++)
                    {
                        cmd = new SqlCommand("UPDATE GroupStudent SET AssignmentDate = @AssignmentDate WHERE StudentId = @StudentId", con);
                        cmd.Parameters.AddWithValue("@StudentId", studentIds[i]);
                        cmd.Parameters.AddWithValue("@AssignmentDate", assignmentDate.Value.Date);
                        cmd.ExecuteNonQuery();
                    }

                }

                catch (Exception ex)
                {

                }
            }
        }
    }
}
