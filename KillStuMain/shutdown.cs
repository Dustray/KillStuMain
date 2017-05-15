using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillStuMain
{
    class shutdown
    {
        public string down;
        public shutdown(string pick)
        {
            this.down = pick;
        }
        public string getAndSet()
        {
            Process myPro2 = new Process();
            myPro2.StartInfo.FileName = "cmd.exe";//打开DOS控制平台 
            myPro2.StartInfo.UseShellExecute = false;
            myPro2.StartInfo.CreateNoWindow = false;//是否显示DOS窗口，true代表隐藏;
            myPro2.StartInfo.RedirectStandardInput = true;
            myPro2.StartInfo.RedirectStandardOutput = true;
            myPro2.StartInfo.RedirectStandardError = true;
            myPro2.Start();
            StreamWriter sIn2 = myPro2.StandardInput;//标准输入流 
            sIn2.AutoFlush = true;
            StreamReader sOut2 = myPro2.StandardOutput;//标准输出流 
            StreamReader sErr2 = myPro2.StandardError;//标准错误流 
            sIn2.Write(down + System.Environment.NewLine);//DOC命令
            sIn2.Write("exit" + System.Environment.NewLine);//第四条DOS命令，退出DOS窗口

            string s = sOut2.ReadToEnd();//读取执行DOS命令后输出信息 
            string er = sErr2.ReadToEnd();//读取执行DOS命令后错误信息 
            if (myPro2.HasExited == false)
            {
                myPro2.Kill();
                sIn2.Close();
                sOut2.Close();
                sErr2.Close();
                myPro2.Close();
                return er;
            }
            else
            {
                sIn2.Close();
                sOut2.Close();
                sErr2.Close();
                myPro2.Close();
                return s;
            }

        }
    }
}
