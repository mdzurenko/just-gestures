using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using JustGestures.Properties;
using JustGestures.GestureParts;
using JustGestures.Features;

namespace JustGestures.TypeOfAction
{
    [Serializable]
    class KeystrokesOptions : BaseActionClass
    {
        public const string NAME = "keystrokes_name";
        public const string KEYSTROKES_CUSTOM = "keystrokes_custom";
        public const string KEYSTROKES_PRIVATE_COPY_TEXT = "keystrokes_private_copy_text";
        public const string KEYSTROKES_PRIVATE_PASTE_TEXT = "keystrokes_private_paste_text";
        public const string KEYSTROKES_SYSTEM_COPY = "keystrokes_system_copy";
        public const string KEYSTROKES_SYSTEM_CUT = "keystrokes_system_cut";
        public const string KEYSTROKES_SYSTEM_PASTE = "keystrokes_system_paste";
        public const string KEYSTROKES_PLAIN_TEXT = "keystrokes_plain_text";
        public const string KEYSTROKES_ZOOM_IN = "keystrokes_zoom_in";
        public const string KEYSTROKES_ZOOM_OUT = "keystrokes_zoom_out";

        const string KEYSTROKES_INSERT_NUMBER = "keystrokes_inser_number_to_icon";

        private Point m_location;

        public KeystrokesOptions()
        {
            m_actions = new List<string>
                (new string[] { 
                    KEYSTROKES_SYSTEM_COPY,
                    KEYSTROKES_SYSTEM_CUT,
                    KEYSTROKES_SYSTEM_PASTE,
                    KEYSTROKES_PRIVATE_COPY_TEXT,
                    KEYSTROKES_PRIVATE_PASTE_TEXT,
                    KEYSTROKES_ZOOM_IN,
                    KEYSTROKES_ZOOM_OUT,
                    KEYSTROKES_PLAIN_TEXT,
                    KEYSTROKES_CUSTOM
                });
        }

        public KeystrokesOptions(KeystrokesOptions action) : base(action) { }

        public KeystrokesOptions(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
        }

        public override object Clone()
        {
            return new KeystrokesOptions(this);
        }

        public KeystrokesOptions(string action)
            : base(action)
        {            
            switch (this.Name)
            {
                case KEYSTROKES_PRIVATE_COPY_TEXT:
                case KEYSTROKES_SYSTEM_COPY:
                    this.KeyScript = new List<List<MyKey>>();
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_CONTROL_DOWN, AllKeys.KEY_C, AllKeys.KEY_CONTROL_UP });
                    break;
                case KEYSTROKES_PLAIN_TEXT:
                case KEYSTROKES_PRIVATE_PASTE_TEXT:
                case KEYSTROKES_SYSTEM_PASTE:
                    this.KeyScript = new List<List<MyKey>>();
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_CONTROL_DOWN, AllKeys.KEY_V, AllKeys.KEY_CONTROL_UP });
                    break;
                case KEYSTROKES_SYSTEM_CUT:
                    this.KeyScript = new List<List<MyKey>>();
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_CONTROL_DOWN, AllKeys.KEY_X, AllKeys.KEY_CONTROL_UP });
                    break;
                case KEYSTROKES_ZOOM_IN:
                    this.KeyScript = new List<List<MyKey>>();
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_CONTROL_DOWN, AllKeys.MOUSE_WHEEL_DOWN, AllKeys.KEY_CONTROL_UP });
                    break;
                case KEYSTROKES_ZOOM_OUT:
                    this.KeyScript = new List<List<MyKey>>();
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_CONTROL_DOWN, AllKeys.MOUSE_WHEEL_UP, AllKeys.KEY_CONTROL_UP });
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
            IntPtr hwnd = Win32.GetForegroundWindow();
            if (hwnd != activeWnd)
            {
                Win32.SetForegroundWindow(activeWnd);
                Win32.SetActiveWindow(activeWnd);
                System.Threading.Thread.Sleep(100);
            }
            m_location = location;
            Thread threadExecuteInSta;
            switch (this.Name)
            {
                case KEYSTROKES_PRIVATE_COPY_TEXT:
                    threadExecuteInSta = new Thread(new ThreadStart(CopyToClip));
                    threadExecuteInSta.SetApartmentState(ApartmentState.STA);
                    threadExecuteInSta.Start();
                    //CopyToClip();
                    break;
                case KEYSTROKES_PRIVATE_PASTE_TEXT:
                    threadExecuteInSta = new Thread(new ThreadStart(PasteFromClip));
                    threadExecuteInSta.SetApartmentState(ApartmentState.STA);
                    threadExecuteInSta.Start();
                    //PasteFromClip();
                    break;
                case KEYSTROKES_PLAIN_TEXT:
                    threadExecuteInSta = new Thread(new ThreadStart(InsertText));
                    threadExecuteInSta.SetApartmentState(ApartmentState.STA);
                    threadExecuteInSta.Start();
                    //InsertText();
                    break;
                case KEYSTROKES_ZOOM_IN:
                case KEYSTROKES_ZOOM_OUT:
                case KEYSTROKES_SYSTEM_COPY:
                case KEYSTROKES_SYSTEM_CUT:
                case KEYSTROKES_SYSTEM_PASTE:
                case KEYSTROKES_CUSTOM:
                    ExecuteKeyList(MouseAction.ModifierClick, location);                    
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
                    case KEYSTROKES_CUSTOM:
                    case KEYSTROKES_PLAIN_TEXT:
                        return (this.Name == action.Name);
                    case KEYSTROKES_PRIVATE_COPY_TEXT:
                    case KEYSTROKES_PRIVATE_PASTE_TEXT:
                        if (action.Name == KEYSTROKES_PRIVATE_COPY_TEXT ||
                            action.Name == KEYSTROKES_PRIVATE_PASTE_TEXT)
                            return true;
                        else
                            return false;
                }
                switch (action.Name)
                {
                    case KEYSTROKES_CUSTOM:
                    case KEYSTROKES_PLAIN_TEXT:
                        return (this.Name == action.Name);
                    case KEYSTROKES_PRIVATE_COPY_TEXT:
                    case KEYSTROKES_PRIVATE_PASTE_TEXT:
                        if (this.Name == KEYSTROKES_PRIVATE_COPY_TEXT ||
                            this.Name == KEYSTROKES_PRIVATE_PASTE_TEXT)
                            return true;
                        else
                            return false;
                }
                return true;

            }
            
        }
        
        public override string GetDescription()
        {
            string str = this.Details;
            switch (this.Name)
            {
                case KEYSTROKES_ZOOM_IN:
                case KEYSTROKES_ZOOM_OUT:
                case KEYSTROKES_SYSTEM_COPY:
                case KEYSTROKES_SYSTEM_PASTE:
                case KEYSTROKES_SYSTEM_CUT:
                case KEYSTROKES_CUSTOM:
                    str = string.Format("{0} - {1}",Languages.Translation.GetText("C_CK_gB_keyScript"), KeysFromDetails(this.Details));
                    break;
                case KEYSTROKES_PRIVATE_PASTE_TEXT:
                case KEYSTROKES_PRIVATE_COPY_TEXT:
                    if (this.Details != string.Empty)
                        str = string.Format("{0} {1}", Languages.Translation.GetText(this.Name), int.Parse(this.Details) + 1);
                    break;
                case KEYSTROKES_PLAIN_TEXT:
                    str = string.Format("{0}: {1}", Languages.Translation.GetText(this.Name), str);
                    break;
            }

            return str;
        }

        public override Bitmap GetIcon(int size)
        {
            Bitmap bmp = null;
            switch (this.Name)
            {
                case NAME:
                    return Resources.keystrokes;
                    //break;
                case KEYSTROKES_ZOOM_IN:
                    return Resources.zoom_in;
                    //break;
                case KEYSTROKES_ZOOM_OUT:
                    return Resources.zoom_out;
                    //break;
                case KEYSTROKES_CUSTOM:
                    return Resources.keystrokes;
                    //break;
                case KEYSTROKES_SYSTEM_COPY:
                    return Resources.edit_copy;
                    //break;
                case KEYSTROKES_SYSTEM_PASTE:
                    return Resources.edit_paste;
                    //break;
                case KEYSTROKES_SYSTEM_CUT:
                    return Resources.edit_cut;
                    //break;
                case KEYSTROKES_PRIVATE_COPY_TEXT:
                    bmp = Resources.edit_copy;
                    goto case KEYSTROKES_INSERT_NUMBER;
                    //break;
                case KEYSTROKES_PRIVATE_PASTE_TEXT:
                    bmp = Resources.edit_paste;
                    goto case KEYSTROKES_INSERT_NUMBER;
                    //break;
                case KEYSTROKES_INSERT_NUMBER:
                    int height = Convert.ToInt16(bmp.Height / 1.75);
                    Graphics g = Graphics.FromImage(bmp);                    
                    string number = this.Details == string.Empty ? "x" : (int.Parse(this.Details) + 1).ToString();
                    StringFormat format = new StringFormat();
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    Font font = new Font(FontFamily.GenericSansSerif, height, FontStyle.Regular);
                    g.DrawString(number, font, Brushes.Black, new RectangleF(bmp.Width - height, bmp.Height - height, height, height), format);
                    g.Dispose();
                    return bmp;
                    //break;                    
                case KEYSTROKES_PLAIN_TEXT:
                    return Resources.text;
                    //break;
                default:
                    return Resources.keystrokes;
                    //break;
            }            
        }

        delegate void emptydel();

        private void InsertText()
        {
            ReadOnlyCollection<ClipData> clipData = ClipboardHandler.GetClipboard();
            Thread.Sleep(50);
            Clipboard.SetText(this.Details);
            Thread.Sleep(50);
            ExecuteKeyList(MouseAction.ModifierClick, m_location);
            Thread.Sleep(200);
            ClipboardHandler.SetClipboard(clipData);
        }

        private void CopyToClip()
        {
            //ReadOnlyCollection<ClipData> backup = ClipboardHandler.GetClipboard();
            //ClipboardHandler.EmptyClipboard();
            //KeyInput.ExecuteKeyInput(AllKeys.KEY_CONTROL_DOWN);
            //KeyInput.ExecuteKeyInput(AllKeys.KEY_C);
            //KeyInput.ExecuteKeyInput(AllKeys.KEY_CONTROL_UP);
            ExecuteKeyList(MouseAction.ModifierClick, m_location);
            System.Threading.Thread.Sleep(250);
            //SendKeys.SendWait("^c");
            //Win32.SendMessage(hwnd, Win32.WM_COPY, 0, 0);
            int index = int.Parse(this.Details);

            Form_engine.PrivateTextClipboards[index] = Clipboard.GetText();
            //Form_engine.PrivateClipboards[index] = ClipboardHandler.GetClipboard();
            
            
            //ClipboardHandler.EmptyClipboard();
            //ClipboardHandler.SetClipboard(backup);            

        }

        private void PasteFromClip()
        {
            //ReadOnlyCollection<ClipData> backup = ClipboardHandler.GetClipboard();
            int index = int.Parse(this.Details);

            Clipboard.SetText(Form_engine.PrivateTextClipboards[index]);
            System.Threading.Thread.Sleep(50);
            //ClipboardHandler.SetClipboard(Form_engine.PrivateClipboards[index]);
            
            //KeyInput.ExecuteKeyInput(AllKeys.KEY_CONTROL_DOWN);
            //KeyInput.ExecuteKeyInput(AllKeys.KEY_V);
            //KeyInput.ExecuteKeyInput(AllKeys.KEY_CONTROL_UP);
            ExecuteKeyList(MouseAction.ModifierClick, m_location);
            
            //SendKeys.SendWait("^v");
            //Win32.SendMessage(hwnd, Win32.WM_PASTE, 0, 0);
            //ClipboardHandler.SetClipboard(backup);
        }

        public static string KeysFromDetails(string _details)
        {
            return _details;
        }

      
    }
}
