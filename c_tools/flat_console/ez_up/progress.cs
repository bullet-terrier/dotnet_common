using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ez_up
{
    public partial class progress : Form
    {
        // I'm not going to worry about these, just make it publicly available.
        public void progress_bar(int num, int den, string message = "Processing...")
        {
            this.progressBar1.Maximum = den;
            this.progressBar1.Value = num;
            this.label1.Text = message;
        }

        public progress()
        {
            InitializeComponent();
            this.ResizeRedraw = false;
        }

        public void complete()
        {
            this.Visible = false;
        }

        public void show()
        {
            this.Visible = true;
            this.progressBar1.Value = 0;
            this.progressBar1.Maximum = 1;
        }

        private void onresize(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
