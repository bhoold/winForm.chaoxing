using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;

namespace WindowsForms.chaoxing
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            /* //单例: 常规方式
            if (System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("已经启动了一个相同程序！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
            */

            /*
            //单例: 互斥方式
            Boolean createdNew; //返回是否赋予了使用线程的互斥体初始所属权
            System.Threading.Mutex instance = new System.Threading.Mutex(true, "WinForm.chaoxing", out createdNew); //同步基元变量
            if (createdNew) //赋予了线程初始所属权，也就是首次使用互斥体
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
                instance.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("您已经打开了一个相同程序！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            */

            //单例
            Process instance = RunningInstance();
            if(null == instance)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
            else
            {
                MessageBox.Show("您已经打开了一个相同程序！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
                HandleRunningInstance(instance);
            }
        }





        #region 防止重复运行
        public static Process RunningInstance()
        {

            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }
        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);
            SetForegroundWindow(instance.MainWindowHandle);
        }
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;
        #endregion





    }
}
