using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NK_Wi_Fi_Hotspot_V1_1
{
    public partial class Form2 : Form
    {
        int timeLeft;
        public Form2()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Visible = false;

            progressBar1.Increment(3);
            if (progressBar1.Value == 100)
            {
                timer1.Stop();
                this.Close();
            }
        }
    }
}
