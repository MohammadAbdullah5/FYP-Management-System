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
    public partial class AdvisorView : Form
    {
        public AdvisorView()
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

        private void showData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT FirstName, LastName, Contact, Email, DateOfBirth, Designation, Salary , (SELECT Value FROM Lookup WHERE Id = Gender) AS Gender  FROM Advisor AS A JOIN Person AS P ON P.Id = A.Id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
