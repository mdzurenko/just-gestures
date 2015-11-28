using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace JustGestures
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);            
            Mutex mt = null;
            try
            {
                mt = Mutex.OpenExisting("JustGestures");
                string[] args = Environment.GetCommandLineArgs();
                
            }
            catch (WaitHandleCannotBeOpenedException)
            {

            }
            if (mt == null)
            {
                
                mt = new Mutex(true, "JustGestures");
                Application.Run(Form_engine.Instance);
                GC.KeepAlive(mt);
                mt.ReleaseMutex();
            }
            else
            {                
                mt.Close();
                MessageBox.Show("Application is already running", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }            
        }
    }
}