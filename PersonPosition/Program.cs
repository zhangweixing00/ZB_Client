using System;
using System.Collections.Generic;
using System.Windows.Forms;

using PersonPosition.View;

namespace PersonPosition
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //不对控件的多线程访问安全进行控制
            Control.CheckForIllegalCrossThreadCalls = false;

            bool loginSucceed = false;

            using (FrmLogin loginform = new FrmLogin())
            {
                loginform.ShowDialog();
                loginSucceed = Convert.ToBoolean(loginform.Tag);
            }
            if (loginSucceed)
            {
                Application.Run(new MainForm());
            }
        }
    }
}