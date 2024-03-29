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
    public partial class StudentManage : Form
    {
        public StudentManage()
        {
            InitializeComponent();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            StudentAdd form = new StudentAdd();
            form.Show();
            this.Hide();
        }

        private void cmdView_Click(object sender, EventArgs e)
        {
            StudentView form = new StudentView();
            form.Show();
            this.Hide();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            StudentEdit form = new StudentEdit();
            form.Show();
            this.Hide();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            MainMenu form = new MainMenu();
            form.Show();
            this.Hide();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            StudentDelete form = new StudentDelete();
            form.Show();
            this.Hide();
        }
    }
}
