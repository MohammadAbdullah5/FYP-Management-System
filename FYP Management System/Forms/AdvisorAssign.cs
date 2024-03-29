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
    public partial class AdvisorAssign : Form
    {
        public AdvisorAssign()
        {
            InitializeComponent();
            showGroupIDs();
            showCoAdvisorIDs();
            showIndustryAdvisorIDs();
            showMainAdvisorIDs();
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

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if(comboBoxGroup.SelectedValue != null && cmbBoxCoAdv.SelectedValue != null && cmbBoxIndustryAdv.SelectedValue != null && cmbBoxMainAdv.SelectedValue != null)
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();

                    DateTime date;
                    SqlCommand cmd = new SqlCommand("SELECT AssignmentDate FROM GroupProject WHERE ProjectId = (SELECT Id FROM Project WHERE Title = @Title)", con);
                    cmd.Parameters.AddWithValue("@Title", txtProject.Text);
                    date = (DateTime)cmd.ExecuteScalar();


                    cmd = new SqlCommand("INSERT INTO [ProjectAdvisor] VALUES (@AdvisorId, (SELECT Id FROM Project WHERE Title = @Title), @AdvisorRole, @AssignmentDate)", con);
                    cmd.Parameters.AddWithValue("@AdvisorId", int.Parse(cmbBoxMainAdv.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("@Title", txtProject.Text);
                    cmd.Parameters.AddWithValue("@AdvisorRole", getId("Main Advisor", "ADVISOR_ROLE"));
                    cmd.Parameters.AddWithValue("@AssignmentDate", date);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("INSERT INTO [ProjectAdvisor] VALUES (@AdvisorId, (SELECT Id FROM Project WHERE Title = @Title), @AdvisorRole, @AssignmentDate)", con);
                    cmd.Parameters.AddWithValue("@AdvisorId", int.Parse(cmbBoxCoAdv.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("@Title", txtProject.Text);
                    cmd.Parameters.AddWithValue("@AdvisorRole", getId("Co-Advisror", "ADVISOR_ROLE"));
                    cmd.Parameters.AddWithValue("@AssignmentDate", date);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("INSERT INTO [ProjectAdvisor] VALUES (@AdvisorId, (SELECT Id FROM Project WHERE Title = @Title), @AdvisorRole, @AssignmentDate)", con);
                    cmd.Parameters.AddWithValue("@AdvisorId", int.Parse(cmbBoxIndustryAdv.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("@Title", txtProject.Text);
                    cmd.Parameters.AddWithValue("@AdvisorRole", getId("Industry Advisor", "ADVISOR_ROLE"));
                    cmd.Parameters.AddWithValue("@AssignmentDate", date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Added");
                }

                catch(Exception ex)
                {

                }
            }
        }

        private void showGroupIDs()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT GroupId FROM [GroupProject] ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBoxGroup.DataSource = dt;
            comboBoxGroup.DisplayMember = "GroupId";
            comboBoxGroup.ValueMember = "GroupId";
        }

        private void showMainAdvisorIDs()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM [Advisor] WHERE Designation = 6 OR Designation = 7", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbBoxMainAdv.DataSource = dt;
            cmbBoxMainAdv.DisplayMember = "Id";
            cmbBoxMainAdv.ValueMember = "Id";
        }

        private void showCoAdvisorIDs()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM [Advisor] WHERE Designation = 8 OR Designation = 9", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbBoxCoAdv.DataSource = dt;
            cmbBoxCoAdv.DisplayMember = "Id";
            cmbBoxCoAdv.ValueMember = "Id";
        }

        private void showIndustryAdvisorIDs()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM [Advisor] WHERE Designation = 10", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbBoxIndustryAdv.DataSource = dt;
            cmbBoxIndustryAdv.DisplayMember = "Id";
            cmbBoxIndustryAdv.ValueMember = "Id";
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            MainMenu form = new MainMenu();
            form.Show();
            this.Hide();
        }

        private void cmbBoxMainAdv_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbBoxMainAdv.SelectedValue != null && cmbBoxMainAdv.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                int idx = int.Parse(cmbBoxMainAdv.SelectedValue.ToString());
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT FirstName + ' ' + LastName AS [Name] FROM Advisor AS A JOIN Person AS P ON P.Id = A.Id WHERE A.Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", idx);
                string txt = cmd.ExecuteScalar().ToString();
                txtMainAdv.Text = txt;
            }
        }

        private void cmbBoxCoAdv_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbBoxCoAdv.SelectedValue != null && cmbBoxCoAdv.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                int idx = int.Parse(cmbBoxCoAdv.SelectedValue.ToString());
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT FirstName + ' ' + LastName AS [Name] FROM Advisor AS A JOIN Person AS P ON P.Id = A.Id WHERE A.Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", idx);
                string txt = cmd.ExecuteScalar().ToString();
                txtCoAdv.Text = txt;
            }
        }

        private void cmbBoxIndustryAdv_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbBoxIndustryAdv.SelectedValue != null && cmbBoxIndustryAdv.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                int idx = int.Parse(cmbBoxIndustryAdv.SelectedValue.ToString());
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT FirstName + ' ' + LastName AS [Name] FROM Advisor AS A JOIN Person AS P ON P.Id = A.Id WHERE A.Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", idx);
                string txt = cmd.ExecuteScalar().ToString();
                txtIndAdv.Text = txt;
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
    }
}
