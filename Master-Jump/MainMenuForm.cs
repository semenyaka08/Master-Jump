using System;
using System.Windows.Forms;

namespace Master_Jump
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            Form1 gameForm = new Form1();
            gameForm.Show();
            Hide();
        }
    }
}