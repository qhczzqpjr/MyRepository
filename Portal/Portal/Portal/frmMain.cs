using System;
using System.Windows.Forms;

namespace Portal
{
    public partial class FrmMain : Form
    {
        //public static FrmMain Newfrm = new FrmMain();
        public FrmMain()
        {
            InitializeComponent();
        }


        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnRequirement_Click(object sender, EventArgs e)
        {
            Form newForm = new FrmRequirement();
            newForm.Show();
            //this.Hide();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            GlobalVoice.SayHello();
        }
    }
}