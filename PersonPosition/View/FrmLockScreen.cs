using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PersonPosition.Common;

namespace PersonPosition.View
{
    public partial class FrmLockScreen : Form
    {
        public FrmLockScreen()
        {
            InitializeComponent();
            this.Text = Application.ProductName + (Global.IsTempVersion ? "(演示版)" : "");
            this.label3.Text = Application.ProductName + (Global.IsTempVersion ? "(演示版)" : "");
        }

        private void FrmLockScreen_Load(object sender, EventArgs e)
        {
            if (Global.TouMingLock)
            {
                this.Opacity = 0.05;
            }
            else
            {
                this.Opacity = 1.0;
            }
            this.timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLockScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.Black), new Point(0, label1.Top + label1.Height / 2), new Point(9999, label1.Top + label1.Height / 2));
        }

        private void linkLabel_UnLock_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (textBox1.Text.Trim() == Global.LockPassword)
            {
                this.Close();
            }
            else
            {
                textBox1.Text = "";
                label4.Visible = true;
            }
        }

        private void FrmLockScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 || e.KeyCode == Keys.Alt || e.Alt == true)
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label4.Visible = false;
        }

        private void FrmLockScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (Global.TouMingLock)
            {
                if (e.Y >= label1.Top)
                {
                    this.Opacity = 1.0;
                }
                else
                {
                    this.Opacity = 0.05;
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                linkLabel_UnLock_LinkClicked(sender, null);
            }
        }
    }
}