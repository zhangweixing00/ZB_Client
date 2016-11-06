using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PersonPosition.View
{
    public partial class FrmPro : Form
    {
        private int ProValue = 0;

        public int MaxValue
        {
            get 
            {
                return this.progressBar1.Maximum;
            }
        }

        public FrmPro()
        {
            InitializeComponent();
        }

        private void FrmPro_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = ProValue;
        }

        public void Add(int step)
        {
            if (step == 0)
            {
                step = 1;
            }
            int result = ProValue + step;
            if (result > this.progressBar1.Maximum)
            {
                result = this.progressBar1.Maximum;
            }
            ProValue = result;
        }
    }
}