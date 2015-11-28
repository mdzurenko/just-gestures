using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Diagnostics;
using System.Web;
using System.Drawing;
using System.Threading;
using Microsoft.Win32;
using JustGestures.Properties;
using JustGestures.GestureParts;
using JustGestures.Features;

namespace JustGestures.TypeOfAction
{   
    [Serializable]
    class InternetOptions : BaseActionClass
    {
        public const string NAME = "internet_name";

        public const string INTERNET_PAGE_NEXT = "internet_page_next";
        public const string INTERNET_PAGE_BACK = "internet_page_back";
        public const string INTERNET_PAGE_HOME = "internet_page_home";
        public const string INTERNET_PAGE_RELOAD = "internet_page_reload";
        public const string INTERNET_PAGE_STOP = "internet_page_stop";
        public const string INTERNET_TAB_NEW = "internet_tab_new";
        public const string INTERNET_TAB_CLOSE = "internet_tab_close";
        public const string INTERNET_TAB_REOPEN = "internet_tab_reopen";
        public const string INTERNET_SEARCH_WEB = "internet_search_web";
        public const string INTERNET_OPEN_WEBSITE = "internet_open_web";
        public const string INTERNET_SEND_EMAIL = "internet_send_email";

        private const string INTERNET_BROWSER_OPERA = "OpWindow";
        private const string INTERNET_BROWSER_FIREFOX = "MozillaUIWindowClass";
        private const string INTERNET_BROWSER_IEXPLORER = "IEFrame";

        private Point m_location;
        
        public InternetOptions()
        {
            m_actions = new List<string>
                (new string[] { 
                    INTERNET_OPEN_WEBSITE,
                    INTERNET_SEND_EMAIL,
                    INTERNET_SEARCH_WEB,
                    INTERNET_PAGE_HOME,
                    INTERNET_PAGE_NEXT,
                    INTERNET_PAGE_BACK,
                    INTERNET_PAGE_RELOAD,
                    INTERNET_PAGE_STOP,
                    INTERNET_TAB_NEW,
                    INTERNET_TAB_CLOSE,
                    INTERNET_TAB_REOPEN                    
                });
        }

        public InternetOptions(InternetOptions action) : base(action) { }

        public InternetOptions(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
        }

        public override object Clone()
        {
            return new InternetOptions(this);
        }

        public InternetOptions(string action)
            : base(action)
        {
            switch (this.Name)
            {
                case INTERNET_SEARCH_WEB:
                    this.KeyScript = new List<List<MyKey>>();
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_CONTROL_DOWN, AllKeys.KEY_C, AllKeys.KEY_CONTROL_UP });
                    break;
                case INTERNET_TAB_NEW:
                    this.KeyScript = new List<List<MyKey>>();
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_CONTROL_DOWN, AllKeys.KEY_T, AllKeys.KEY_CONTROL_UP });
                    break;
                case INTERNET_TAB_CLOSE:
                    this.KeyScript = new List<List<MyKey>>();
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_CONTROL_DOWN, AllKeys.KEY_W, AllKeys.KEY_CONTROL_UP });
                    break;
                case INTERNET_TAB_REOPEN:
                    this.KeyScript = new List<List<MyKey>>();
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_CONTROL_DOWN, AllKeys.KEY_SHIFT_DOWN, AllKeys.KEY_T, AllKeys.KEY_SHIFT_UP, AllKeys.KEY_CONTROL_UP });
                    break;
            }
            if (this.KeyScript != null)
            {
                this.KeyScript.Add(new List<MyKey>());
                this.KeyScript.Add(new List<MyKey>());
                this.KeyScript.Add(new List<MyKey>());
            }
        }

        public override void ExecuteAction(IntPtr activeWnd, Point location)
        {
            //AutomationElement aeBrowser = AutomationElement.FromHandle((IntPtr)ActiveWnd);                  

            //int procId;
            //Win32.GetWindowThreadProcessId(ActiveWnd, out procId);
            //System.Diagnostics.Process foregroundProc = System.Diagnostics.Process.GetProcessById(procId);
            //StringBuilder longPath = new StringBuilder(500);
            //Win32.GetLongPathName(foregroundProc.MainModule.FileName, longPath, 500);
            //string prgPath = longPath.ToString().ToLower();                
            //Win32.SetForegroundWindow(ActiveWnd);
            //Win32.SetActiveWindow(ActiveWnd);
            //Win32.SetFocus(ActiveWnd);                
            //Win32.SendMessage(ActiveWnd, Win32.WM_ACTIVATEAPP, Win32.WA_ACTIVE, 0);
            //Win32.SendMessage(ActiveWnd, Win32.WM_NCACTIVATE, Win32.WA_ACTIVE, 0);
            ////Win32.SendMessage(ActiveWnd, Win32.WM_MOUSEACTIVATE, ActiveWnd.ToInt32(), (Win32.HTCLIENT | (0x010000 * Win32.WM_LBUTTONDOWN)));
            //Win32.SendMessage(ActiveWnd, Win32.WM_ACTIVATE, Win32.WA_ACTIVE, 0);

            IntPtr hwnd = Win32.GetForegroundWindow();
            if (hwnd != activeWnd)
            {
                Win32.SetForegroundWindow(activeWnd);
                Win32.SetActiveWindow(activeWnd);
                System.Threading.Thread.Sleep(100);
            }
            Thread threadExecuteInSta;            
            string prefix = string.Empty;
            switch (this.Name)
            {
                case INTERNET_PAGE_NEXT:
                    //KeyInput.ExecuteKeyInput(AllKeys.KEY_MENU_DOWN);
                    //KeyInput.ExecuteKeyInput(AllKeys.KEY_RIGHT);
                    //KeyInput.ExecuteKeyInput(AllKeys.KEY_MENU_UP);
                    KeyInput.ExecuteKeyInput(AllKeys.KEY_BROWSER_FORWARD);
                    //SendKeys.SendWait("%{RIGHT}");
                    break;
                case INTERNET_PAGE_BACK:
                    //KeyInput.ExecuteKeyInput(AllKeys.KEY_MENU_DOWN);
                    //KeyInput.ExecuteKeyInput(AllKeys.KEY_LEFT);
                    //KeyInput.ExecuteKeyInput(AllKeys.KEY_MENU_UP);
                    KeyInput.ExecuteKeyInput(AllKeys.KEY_BROWSER_BACK);
                    //SendKeys.SendWait("%{LEFT}");
                    break;
                case INTERNET_PAGE_HOME:
                    //KeyInput.ExecuteKeyInput(AllKeys.KEY_MENU_DOWN);
                    //KeyInput.ExecuteKeyInput(AllKeys.KEY_HOME);
                    //KeyInput.ExecuteKeyInput(AllKeys.KEY_MENU_UP);
                    KeyInput.ExecuteKeyInput(AllKeys.KEY_BROWSER_HOME);
                    //SendKeys.SendWait("%{HOME}");
                    break;
                case INTERNET_PAGE_RELOAD:
                    //KeyInput.ExecuteKeyInput(AllKeys.KEY_F5);
                    KeyInput.ExecuteKeyInput(AllKeys.KEY_BROWSER_REFRESH);
                    //SendKeys.SendWait("{F5}");
                    break;
                case INTERNET_PAGE_STOP:
                    //KeyInput.ExecuteKeyInput(AllKeys.KEY_ESCAPE);
                    KeyInput.ExecuteKeyInput(AllKeys.KEY_BROWSER_STOP);
                    //SendKeys.SendWait("{ESCAPE}");
                    break;                
                case INTERNET_TAB_NEW:
                case INTERNET_TAB_REOPEN:
                case INTERNET_TAB_CLOSE:
                    ExecuteKeyList(MouseAction.ModifierClick, location);                    
                    break;                
                case INTERNET_SEND_EMAIL:
                    prefix = "mailto:";
                    goto case INTERNET_OPEN_WEBSITE;
                    //break;
                case INTERNET_OPEN_WEBSITE:
                    Process myProcess = new Process();
                    myProcess.StartInfo.FileName = prefix + this.Details;
                    myProcess.Start();
                    break;                
                case INTERNET_SEARCH_WEB:
                    m_location = location;
                    threadExecuteInSta = new Thread(new ThreadStart(SearchWeb));
                    threadExecuteInSta.SetApartmentState(ApartmentState.STA);
                    threadExecuteInSta.Start();
                    break;
            }
        }

        public override bool IsSameType(BaseActionClass action)
        {
            if (!base.IsSameType(action))
                return false;
            else
            {
                switch (this.Name)
                {
                    case INTERNET_OPEN_WEBSITE:
                    case INTERNET_SEARCH_WEB:
                    case INTERNET_SEND_EMAIL:
                        return (this.Name == action.Name);
                }
                switch (action.Name)
                {
                    case INTERNET_OPEN_WEBSITE:
                    case INTERNET_SEARCH_WEB:
                    case INTERNET_SEND_EMAIL:
                        return (this.Name == action.Name);
                }
                return true;
            }
        }

        public override Bitmap GetIcon(int size)
        {
            string path;
            Bitmap favicon;
            switch (this.Name)
            {
                case NAME:
                    return Resources.internet;
                    //break;
                case INTERNET_PAGE_BACK:
                    return Resources.browser_prev;
                    //break;
                case INTERNET_PAGE_NEXT:
                    return Resources.browser_next;
                    //break;
                case INTERNET_PAGE_HOME:
                    return Resources.browser_home;
                    //break;
                case INTERNET_PAGE_RELOAD:
                    return Resources.browser_refresh;
                    //break;
                case INTERNET_PAGE_STOP:
                    return Resources.browser_stop;
                    //break;
                case INTERNET_TAB_CLOSE:
                    return Resources.tab_close;
                    //break;
                case INTERNET_TAB_REOPEN:
                    return Resources.tab_undo;
                    //break;
                case INTERNET_TAB_NEW:
                    return Resources.tab_add;
                    //break;
                case INTERNET_OPEN_WEBSITE:
                    path = this.Details;
                    favicon = Features.Favicon.Load(path);
                    if (favicon != null)
                        return favicon;
                    else
                    {
                        path = GetDefaultBrowser();
                        if (System.IO.File.Exists(path))
                            return System.Drawing.Icon.ExtractAssociatedIcon(path).ToBitmap();
                        else
                            return Resources.internet;
                    }
                case INTERNET_SEND_EMAIL:
                    return Resources.mail;
                    //break;
                case INTERNET_SEARCH_WEB:
                    path = this.Details;
                    favicon = Features.Favicon.Load(path);
                    if (favicon != null)
                    {
                        //favicon = new Bitmap(favicon, new Size(24, 24));
                        //Graphics g = Graphics.FromImage(favicon);                        
                        //g.DrawImage(Resources.custom_search, new Point(0, 0));
                        //g.Dispose();
                        return favicon;
                    }
                    else
                    {
                        if (size == 20)
                            return Resources.web_search20;
                        else
                            return Resources.web_search32;
                    }
                    //break;
                default:
                    return Resources.internet;
                    //break;
            }      
        }

        public override string GetDescription()
        {
            string str = Languages.Translation.GetText(this.Name);

            switch (this.Name)
            {
                case INTERNET_OPEN_WEBSITE:
                case INTERNET_SEARCH_WEB:
                case INTERNET_SEND_EMAIL:
                    str += " " + this.Details;
                    break;
            }

            return str;
        }

      

        private void SearchWeb()
        {
            System.Collections.ObjectModel.ReadOnlyCollection<ClipData> clipData = ClipboardHandler.GetClipboard();
            ExecuteKeyList(MouseAction.ModifierClick, m_location);
            System.Threading.Thread.Sleep(250);            
            string text = Clipboard.GetText(); //ClipboardHandler.GetClipboardText();
            Process myProcess = new Process();            
            myProcess.StartInfo.FileName = this.Details.Replace("(*)", text);
            myProcess.Start();

            ClipboardHandler.SetClipboard(clipData);
        }

        //private void DoAction(IntPtr activeWnd, string cmd_ff, string cmd_o, string cmd_ie)
        //{
        //    StringBuilder Buff = new StringBuilder(256);
        //    Win32.GetClassName(activeWnd, Buff, 256);
        //    switch (Buff.ToString())
        //    {
        //        case INTERNET_BROWSER_FIREFOX:
        //            SendKeys.SendWait(cmd_ff);
        //            break;
        //        case INTERNET_BROWSER_OPERA:
        //            SendKeys.SendWait(cmd_o);
        //            break;
        //        case INTERNET_BROWSER_IEXPLORER:
        //            SendKeys.SendWait(cmd_ie);
        //            break;
        //    }
        //}

        public static string GetDefaultBrowser()
        {
            string path = string.Empty;
            // get path to default browser
            path = GetKeyValue(@"HTTP\shell\open\command");
            if (path == string.Empty)
            {
                // get the program associated to .html files
                path = GetKeyValue(@".html");
                if (path != null)
                {
                    // get the path to the program 
                    path = GetKeyValue(@path + @"\shell\open\command");
                }
            }
            if (path != string.Empty)
            {
                int start = path.IndexOf(":\\");
                int pos = path.ToLower().IndexOf(".exe");
                if (pos > 0 && start > 0 && start < pos)
                {
                    path = path.Substring(start - 1, pos + 5 - start);
                }
            }
            return path;
        }

        public static string GetKeyValue(string registryPath)
        {
            string keyValue = string.Empty;
            RegistryKey key = null;
            try
            {
                key = Registry.ClassesRoot.OpenSubKey(@registryPath, false);
                keyValue = key.GetValue(null).ToString();
            }
            catch (Exception ex) { }
            finally
            {
                if (key != null) key.Close();
            }
            return keyValue;
        }

        public static bool IsBrowser(IntPtr ActiveWnd)
        {
            StringBuilder Buff = new StringBuilder(256);
            Win32.GetClassName(ActiveWnd, Buff, 256);
            IntPtr mainWindow = Win32.GetAncestor(ActiveWnd, Win32.GA_ROOTOWNER);
            if (mainWindow != ActiveWnd)
                return false;
            if ((Buff.ToString() == INTERNET_BROWSER_IEXPLORER) 
                || (Buff.ToString() == INTERNET_BROWSER_FIREFOX)
                || (Buff.ToString() == INTERNET_BROWSER_OPERA))
                return true;
            else
                return false;
        }
    }
}
