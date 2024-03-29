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
    public partial class AdvisorManage : Form
    {
        public AdvisorManage()
        {
            InitializeComponent();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            AdvisorAdd form = new AdvisorAdd();
            form.Show();
            this.Hide();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            AdvisorEdit form = new AdvisorEdit();
            form.Show();
            this.Hide();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            AdvisorDelete form = new AdvisorDelete();
            form.Show();
            this.Hide();
        }

        private void cmdView_Click(object sender, EventArgs e)
        {
            AdvisorView form = new AdvisorView();
            form.Show();
            this.Hide();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            MainMenu form = new MainMenu();
            form.Show();
            this.Hide();
        }
    }
}
