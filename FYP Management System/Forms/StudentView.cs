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
    public partial class StudentView : Form
    {
        public StudentView()
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

        private void showData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT RegistrationNo, FirstName, LastName, Contact, Email, DateOfBirth, (SELECT Value FROM Lookup WHERE Id = Gender) AS Gender FROM Student AS S JOIN Person AS P ON P.Id = S.Id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
