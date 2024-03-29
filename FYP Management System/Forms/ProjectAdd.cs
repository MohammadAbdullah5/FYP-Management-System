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
    public partial class ProjectAdd : Form
    {
        public ProjectAdd()
        {
            InitializeComponent();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            ProjectManage form = new ProjectManage();
            form.Show();
            this.Hide();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text != "")
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd;

                    cmd = new SqlCommand("Insert Into Project values (@Description, @Title)", con);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Added");
                }

                catch (FormatException ex)
                {
                    MessageBox.Show("Wrong Input");
                }
            }
        }
    }
}
