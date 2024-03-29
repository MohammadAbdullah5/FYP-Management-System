using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FYP_Management_System.Forms
{
    public partial class ProjectManage : Form
    {
        public ProjectManage()
        {
            InitializeComponent();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            MainMenu form = new MainMenu();
            form.Show();
            this.Hide();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            ProjectAdd form = new ProjectAdd();
            form.Show();
            this.Hide();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            ProjectEdit form = new ProjectEdit();
            form.Show();
            this.Hide();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            ProjectDelete form = new ProjectDelete();
            form.Show();
            this.Hide();
        }

        private void cmdView_Click(object sender, EventArgs e)
        {
            ProjectView form = new ProjectView();
            form.Show();
            this.Hide();
        }
    }
}
