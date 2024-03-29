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
    public partial class GroupFormation : Form
    {
        public GroupFormation()
        {
            InitializeComponent();
            showData();
            setDGV();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            MainMenu form = new MainMenu();
            form.Show();
            this.Hide();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            bool isGroup = false;
            List<int> studentIds = new List<int>();
            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell checkboxCell = row.Cells["SelectStudent"] as DataGridViewCheckBoxCell;

                if (checkboxCell != null && Convert.ToBoolean(checkboxCell.Value))
                {
                    isGroup = true;
                    count++;
                    studentIds.Add(int.Parse(row.Cells["Id"].Value.ToString()));
                }
            }

            if(isGroup)
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd;
                    DateTime date = DateTime.Now.Date;
                    cmd = new SqlCommand("Insert Into [Group] values (@Created_On)", con);
                    cmd.Parameters.AddWithValue("@Created_On", creationDate.Value.Date);
                    cmd.ExecuteNonQuery();

                    for (int i = 0; i < count; i++)
                    {
                        cmd = new SqlCommand("SELECT MAX(Id) FROM [Group]", con);
                        int grpId = (Int32)cmd.ExecuteScalar();

                        cmd = new SqlCommand("INSERT INTO GroupStudent values (@GroupID, @StudentID, @Status, @AssignmentDate)", con);
                        cmd.Parameters.AddWithValue("@GroupID", grpId);
                        cmd.Parameters.AddWithValue("@StudentId", studentIds[i]);
                        cmd.Parameters.AddWithValue("@Status", getId("Active", "STATUS"));
                        cmd.Parameters.AddWithValue("@AssignmentDate", date);

                        cmd.ExecuteNonQuery();
                    }
                    
                    MessageBox.Show("Successfully saved");
                    showData();
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

        private void showData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT RegistrationNo, FirstName, LastName, Contact, Email, DateOfBirth, S.Id AS [Id], (SELECT Value FROM Lookup WHERE Id = Gender) AS Gender FROM Student AS S JOIN Person AS P ON P.Id = S.Id LEFT JOIN GroupStudent AS GS ON GS.StudentId = S.Id WHERE GS.GroupId IS NULL", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void setDGV()
        {
            dataGridView1.Columns["SelectStudent"].ReadOnly = false;
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["FirstName"].ReadOnly = true;
            dataGridView1.Columns["LastName"].ReadOnly = true;
            dataGridView1.Columns["Gender"].ReadOnly = true;
            dataGridView1.Columns["DateOfBirth"].ReadOnly = true;
            dataGridView1.Columns["Contact"].ReadOnly = true;
            dataGridView1.Columns["Email"].ReadOnly = true;
            dataGridView1.Columns["RegistrationNo"].ReadOnly = true;
        }
    }
}
