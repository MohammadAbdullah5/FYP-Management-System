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
    public partial class EvaluationManage : Form
    {
        public EvaluationManage()
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
            EvaluationAdd form = new EvaluationAdd();
            form.Show();
            this.Hide();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            EvaluationEdit form = new EvaluationEdit();
            form.Show();
            this.Hide();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            EvaluationDelete form = new EvaluationDelete();
            form.Show();
            this.Hide();
        }

        private void cmdView_Click(object sender, EventArgs e)
        {
            EvaluationView form = new EvaluationView();
            form.Show();
            this.Hide();
        }
    }
}
