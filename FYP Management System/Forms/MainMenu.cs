using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FYP_Management_System.Forms;

namespace FYP_Management_System
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void cmdStudentManage_Click(object sender, EventArgs e)
        {
            StudentManage form = new StudentManage();
            form.Show();
            this.Hide();
        }

        private void cmdAdvisorManage_Click(object sender, EventArgs e)
        {
            AdvisorManage form = new AdvisorManage();
            form.Show();
            this.Hide();
        }

        private void cmdProjectManage_Click(object sender, EventArgs e)
        {
            ProjectManage form = new ProjectManage();
            form.Show();
            this.Hide();
        }

        private void cmdFormGrp_Click(object sender, EventArgs e)
        {
            GroupFormation form = new GroupFormation();
            form.Show();
            this.Hide();
        }

        private void cmdAssignProject_Click(object sender, EventArgs e)
        {
            ProjectAssign form = new ProjectAssign();
            form.Show();
            this.Hide();
        }

        private void cmdAssignAdvisor_Click(object sender, EventArgs e)
        {
            AdvisorAssign form = new AdvisorAssign();
            form.Show();
            this.Hide();
        }

        private void cmdManageEvaluations_Click(object sender, EventArgs e)
        {
            EvaluationManage form = new EvaluationManage();
            form.Show();
            this.Hide();
        }

        private void cmdMarkEvaluation_Click(object sender, EventArgs e)
        {
            EvaluationMark form = new EvaluationMark();
            form.Show();
            this.Hide();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cmdReports_Click(object sender, EventArgs e)
        {
            Reports form = new Reports();
            form.Show();
            this.Hide();
        }
    }
}
