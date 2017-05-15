using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace KillStuMain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*--------------------------------------------------------------------------------------------
            string path1 = Assembly.GetExecutingAssembly().Location;
            path1 = path1.Replace("KillStuMain.exe", "ntsd.exe");
            textBox2.Text = path1;
            try
            {
                FileInfo fi = new FileInfo(path1);
                string path2 = @"C:\Windows\System32\ntsd.exe";
                fi.CopyTo(path2, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //------------------------------------------------------------------------------*/
            string getPID;
            if (textBox3.Text != "")
            {
                getPID = "taskkill /im "+textBox3.Text +" /f";
            }
            else
            {
                getPID = "taskkill /im StudentMain.exe /f";
            }

            Process myPro = new Process();
            myPro.StartInfo.FileName = "cmd.exe";//打开DOS控制平台 
            myPro.StartInfo.UseShellExecute = false;
            myPro.StartInfo.CreateNoWindow = false;//是否显示DOS窗口，true代表隐藏;
            myPro.StartInfo.RedirectStandardInput = true;
            myPro.StartInfo.RedirectStandardOutput = true;
            myPro.StartInfo.RedirectStandardError = true;
            myPro.Start();
            StreamWriter sIn = myPro.StandardInput;//标准输入流 
            sIn.AutoFlush = true;
            StreamReader sOut = myPro.StandardOutput;//标准输出流 
            StreamReader sErr = myPro.StandardError;//标准错误流 
            sIn.Write(getPID + System.Environment.NewLine);//DOC命令
            sIn.Write("exit" + System.Environment.NewLine);//第四条DOS命令，退出DOS窗口

            string s = sOut.ReadToEnd();//读取执行DOS命令后输出信息 
            string er = sErr.ReadToEnd();//读取执行DOS命令后错误信息 
            int position = s.IndexOf("PID 为 ");
            if (myPro.HasExited == false)
            {
                myPro.Kill();
                MessageBox.Show(er);
            }
            else
            {
                label1.Text = position.ToString() ;
                s = s.Remove(0,position+6);
                s = s.Substring(0, 6);
                s = Regex.Replace(s, @"[^\d]*", "");
                textBox1.Text = "PIN码是："+s;
            }
            //--------------------------------------------------------------------------------------------
            string path1 = Assembly.GetExecutingAssembly().Location;
            path1 = path1.Replace("KillStuMain.exe", "ntsd /p ");
            string pick = path1+s;
            textBox2.Text = pick+"\r\n";

            shutdown ds = new shutdown(pick);
            textBox2.Text = textBox2.Text + ds;
            sIn.Close();
            sOut.Close();
            sErr.Close();
            myPro.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
