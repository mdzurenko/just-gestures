using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing;
using JustGestures.GestureParts;
using JustGestures.Properties;
using JustGestures.Features;

namespace JustGestures.TypeOfAction
{
    [Serializable]
    class ExtrasOptions : BaseActionClass
    {
        public const string NAME = "extras_name";

        public const string EXTRAS_TRANSPARENCY = "extras_transparency";
        public const string EXTRAS_VOLUME = "extras_volume";
        public const string EXTRAS_TASK_SWITCHER = "extras_task_switch";
        public const string EXTRAS_TAB_SWITCHER = "extras_tab_switch";
        public const string EXTRAS_CUSTOM_WHEEL_BTN = "extras_custom_wheel_btn";
        public const string EXTRAS_ZOOM = "extras_zoom";
        //public const string EXPLORER_WHEEL = "Explorer Wheel";
        //public const string BROWSER_WHEEL = "Browser Wheel";
        //public const string CUSTOM_WHEEL = "Custom Wheel";
        

        public ExtrasOptions()
        {
            m_actions = new List<string>
                (new string[] { 
                    EXTRAS_TRANSPARENCY,
                    EXTRAS_VOLUME,
                    EXTRAS_TASK_SWITCHER,
                    EXTRAS_TAB_SWITCHER,
                    EXTRAS_ZOOM,
                    EXTRAS_CUSTOM_WHEEL_BTN
                });
        }

        public ExtrasOptions(ExtrasOptions action) : base(action) { }

        public ExtrasOptions(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
        }

        public override object Clone()
        {
            return new ExtrasOptions(this);
        }

        public ExtrasOptions(string action)
            : base(action)
        {
            switch (this.Name)
            {
                case EXTRAS_TASK_SWITCHER:
                    this.KeyScript = new List<List<MyKey>>();
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_MENU_DOWN });
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_SHIFT_DOWN, AllKeys.KEY_TAB, AllKeys.KEY_SHIFT_UP });
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_TAB });
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_MENU_UP });
                    break;
                case EXTRAS_TAB_SWITCHER:
                    this.KeyScript = new List<List<MyKey>>();
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_CONTROL_DOWN });
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_SHIFT_DOWN, AllKeys.KEY_TAB, AllKeys.KEY_SHIFT_UP });
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_TAB });
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_CONTROL_UP });
                    break;
                case EXTRAS_ZOOM:
                    this.KeyScript = new List<List<MyKey>>();
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_CONTROL_DOWN });
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.MOUSE_WHEEL_DOWN });
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.MOUSE_WHEEL_UP });
                    this.KeyScript.Add(new List<MyKey>() { AllKeys.KEY_CONTROL_UP });
                    break;
            }            
        }

        public override void ExecuteAction(IntPtr activeWnd, Point location)
        {
            //switch (this.Action)
            //{
            //    case SPECIAL_ZOOM:                   
            //        break;               
            //}
        }

        public override bool IsExtras()
        {
            return true;
        }

        public override bool IsSensitiveToMySystemWindows()
        {
            switch (m_name)
            {
                case EXTRAS_TAB_SWITCHER:
                case EXTRAS_VOLUME:
                    return false;
                    break;
                default:
                    return true;
                    break;
            }            
        }

        public override void ExecuteKeyScript(MouseAction mouse, IntPtr ActiveWnd, bool selectWnd, Point location)
        {
            switch (this.Name)
            {
                case EXTRAS_VOLUME:
                    if (mouse == MouseAction.WheelUp)
                        VolumeOptions.TurnVolume(VolumeOptions.Volume.Up, 0);
                    else if (mouse == MouseAction.WheelDown)
                        VolumeOptions.TurnVolume(VolumeOptions.Volume.Down, 0);
                    break;
                case EXTRAS_TRANSPARENCY:
                    //Win32.SetForegroundWindow(ActiveWnd);
                    //Win32.SetActiveWindow(ActiveWnd);
                    if (mouse == MouseAction.WheelUp)
                        WindowOptions.ChangeWndTransparency(ActiveWnd, true);
                    else if (mouse == MouseAction.WheelDown)
                        WindowOptions.ChangeWndTransparency(ActiveWnd, false);
                    break;
                case EXTRAS_ZOOM:
                case EXTRAS_TASK_SWITCHER:
                case EXTRAS_TAB_SWITCHER:
                case EXTRAS_CUSTOM_WHEEL_BTN:
                default: //actions from other categories 
                    base.ExecuteKeyScript(mouse, ActiveWnd, selectWnd, location);                    
                    break;
            }
        }

        //public override bool IsSameType(BaseActionClass action)
        //{
        //    if (!base.IsSameType(action))
        //        return false;
        //    else
        //    {
        //        if (this.Name == EXTRAS_CUSTOM_WHEEL_BTN ||
        //            action.Name == EXTRAS_CUSTOM_WHEEL_BTN)
        //        {
        //            return (this.Name == action.Name);
        //        }
        //        else
        //            return true;
        //    }
        //}

        public override System.Drawing.Bitmap GetIcon(int size)
        {
            switch (this.Name)
            {
                case NAME:
                    return Resources.favourites;
                    //break;
                case EXTRAS_TRANSPARENCY:
                    return Resources.window_transparent;
                    //break;
                case EXTRAS_VOLUME:
                    return Resources.volume_control2;
                    //break;
                case EXTRAS_TASK_SWITCHER:
                    return Resources.window_switcher;
                    //break;
                case EXTRAS_TAB_SWITCHER:
                    return Resources.tab_switcher;
                    //break;
                case EXTRAS_CUSTOM_WHEEL_BTN:
                    return Resources.keystrokes;
                    //break;
                case EXTRAS_ZOOM:
                    return Resources.zoom;
                    //break;
                default:
                    return Resources.favourites;
                    //break;
            }
        }

        public override string GetDescription()
        {
            string str = Languages.Translation.GetText(this.Name);
            switch (this.Name)
            {
                case EXTRAS_TASK_SWITCHER:
                case EXTRAS_TAB_SWITCHER:
                case EXTRAS_CUSTOM_WHEEL_BTN:
                    str = string.Format("{0} - {1}", Languages.Translation.GetText("C_CK_gB_keyScript"), KeysFromDetails(this.Details));
                    break;
            }
            return str;
        }

        public static string KeysFromDetails(string _details)
        {
            return _details;
        }
    }
}
 