using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MouseCore
{
    public enum ExtraMouseBtns
    {
        None = 0,
        Left = 1,
        Right = 2,
        Middle = 3,
        XButton1 = 4,
        XButton2 = 5,
        Wheel = 6
    }

    public enum ExtraMouseActions
    {
        None,
        Up,
        Down,
        Click,
        DoubleClick,
        Move,
        WheelUp,
        WheelDown
    }

    public class ExtraMouse
    {
        ExtraMouseBtns m_button;
        ExtraMouseActions m_action;
        Point m_point;
        int m_clicks;

        public ExtraMouseBtns Button { get { return m_button; } }
        public ExtraMouseActions Action { get { return m_action; } }
        public Point Position { get { return m_point; } }
        public int Cliks { get { return m_clicks; } }

        public ExtraMouse(ExtraMouseBtns button, ExtraMouseActions action, Point position)
        {   
            m_button = button;
            m_action = action;
            m_point = position;
            m_clicks = m_action == ExtraMouseActions.DoubleClick ? 2 : 1;
        }

        public ExtraMouse(IntPtr wParam, IntPtr lParam)
        {
            Win32.MouseLLHookStruct hookStruct = (Win32.MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(Win32.MouseLLHookStruct));
            m_button = ExtractButton(wParam, hookStruct);
            m_action = ExtractAction(wParam, hookStruct);
            m_point = new Point(hookStruct.pt.x, hookStruct.pt.y);
            m_clicks = m_action == ExtraMouseActions.DoubleClick ? 2 : 1;
        }

        public ExtraMouse(ExtraMouse mouse)
        {
            m_button = mouse.Button;
            m_action = mouse.Action;
            m_point = mouse.Position;
            m_clicks = mouse.Cliks;
        }

        public MouseEventArgs ToMouseEvent()
        {
            MouseButtons btn = ExtraBtnToMouseBtn(m_button);
            int delta = 0;
            if (m_button == ExtraMouseBtns.Wheel)
                delta = GetDwData(m_button, m_action);
            return new MouseEventArgs(btn, m_clicks, m_point.X, m_point.Y, delta);
        }

        //public static MouseEventArgs GetMouseEventArgs(ExtraMouseBtns button, ExtraMouseActions action, IntPtr lParam)
        //{
        //    Win32.MouseLLHookStruct hookStruct = (Win32.MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(Win32.MouseLLHookStruct));
        //    MouseButtons btn = ExtraBtnToMouseBtn(button);
        //    int delta = 0;
        //    int clicks = action == ExtraMouseActions.DoubleClick ? 2 : 1;
        //    if (button == ExtraMouseBtns.Wheel)
        //        delta = GetDwData(button, action);
        //    return new MouseEventArgs(btn, clicks, hookStruct.pt.x, hookStruct.pt.y, delta);
        //}

        //public static void SimateHiddenClick(ExtraMouseActions action, Point pos)
        //{
        //    int dwFlags = GetMouseEventValue(ExtraMouseBtns.Left, action);
        //    Win32.SetCursorPos(pos.X, pos.Y);
        //    Win32.mouse_event(dwFlags, pos.X, pos.Y, 0, IntPtr.Zero);
        //}

        public void SimulateMouseAction()
        {            
            int dwData = GetDwData(m_button, m_action);
            int dwFlags = GetMouseEventValue(m_button, m_action);
            lock (this)
            {
                Win32.SetCursorPos(m_point.X, m_point.Y);
                Win32.mouse_event(dwFlags, m_point.X, m_point.Y, dwData, IntPtr.Zero);
            }
        }

        public static void SimulateMouseAction(ExtraMouseBtns button, ExtraMouseActions action, IntPtr lParam)
        {
            Win32.MouseLLHookStruct hookStruct = (Win32.MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(Win32.MouseLLHookStruct));
            int dwData = GetDwData(button, action);
            int dwFlags = GetMouseEventValue(button, action);
            if (action == ExtraMouseActions.DoubleClick)
            {
                //Win32.SetCursorPos(hookStruct.pt.x, hookStruct.pt.y);
                Win32.mouse_event(dwFlags, hookStruct.pt.x, hookStruct.pt.y, dwData, IntPtr.Zero);
                System.Threading.Thread.Sleep(10);
                Win32.mouse_event(dwFlags, hookStruct.pt.x, hookStruct.pt.y, dwData, IntPtr.Zero);
            }
            else
            {
                //Win32.SetCursorPos(hookStruct.pt.x, hookStruct.pt.y);
                Win32.mouse_event(dwFlags, hookStruct.pt.x, hookStruct.pt.y, dwData, IntPtr.Zero);
            }
        }



        public static MouseButtons ExtraBtnToMouseBtn(ExtraMouseBtns button)
        {
            MouseButtons btn = MouseButtons.None;
            if (button != ExtraMouseBtns.Wheel)
                btn = (MouseButtons)Enum.Parse(typeof(MouseButtons), button.ToString());
            return btn;
            //switch (button)
            //{
            //    case ExtraMouseBtns.Left: btn = MouseButtons.Left; break;
            //    case ExtraMouseBtns.Right: btn = MouseButtons.Right; break;
            //    case ExtraMouseBtns.Middle: btn = MouseButtons.Middle; break;
            //    case ExtraMouseBtns.XButton1: btn = MouseButtons.XButton1; break;
            //    case ExtraMouseBtns.XButton2: btn = MouseButtons.XButton2; break;
            //}
            //return btn;
        }

        public static ExtraMouseBtns MouseBtnToExtraBtn(MouseButtons button)
        {
            ExtraMouseBtns btn = ExtraMouseBtns.None;
            btn = (ExtraMouseBtns)Enum.Parse(typeof(ExtraMouseBtns), button.ToString());
            return btn;
            //switch (button)
            //{
            //    case MouseButtons.Left: btn = ExtraMouseBtns.Left; break;
            //    case MouseButtons.Right: btn = ExtraMouseBtns.Right; break;
            //    case MouseButtons.Middle: btn = ExtraMouseBtns.Middle; break;
            //    case MouseButtons.XButton1: btn = ExtraMouseBtns.XButton1; break;
            //    case MouseButtons.XButton2: btn = ExtraMouseBtns.XButton2; break;
            //}
            //return btn;
        }

        public static int GetWmValue(ExtraMouseBtns button, ExtraMouseActions action)
        {
            int wm_message = 0;
            switch (button)
            {
                case ExtraMouseBtns.Left:
                    if (action == ExtraMouseActions.Down)
                        wm_message = Win32.WM_LBUTTONDOWN;
                    else
                        wm_message = Win32.WM_LBUTTONUP;
                    break;
                case ExtraMouseBtns.Right:
                    if (action == ExtraMouseActions.Down)
                        wm_message = Win32.WM_RBUTTONDOWN;
                    else
                        wm_message = Win32.WM_RBUTTONUP;
                    break;
                case ExtraMouseBtns.Middle:
                    if (action == ExtraMouseActions.Down)
                        wm_message = Win32.WM_MBUTTONDOWN;
                    else
                        wm_message = Win32.WM_MBUTTONUP;
                    break;
                case ExtraMouseBtns.XButton1:
                case ExtraMouseBtns.XButton2:
                    if (action == ExtraMouseActions.Down)
                        wm_message = Win32.WM_XBUTTONDOWN;
                    else
                        wm_message = Win32.WM_XBUTTONUP;
                    break;
                case ExtraMouseBtns.Wheel:
                    wm_message = Win32.WM_MOUSEWHEEL;
                    break;
            }
            return wm_message;
        }


        private static int GetMouseEventValue(ExtraMouseBtns button, ExtraMouseActions action)
        {
            int mouseEventValue = 0;
            bool swapedButtons = Win32.GetSystemMetrics(Win32.SM_SWAPBUTTON) == 0 ? false : true;
            
            switch (button)
            {
                case ExtraMouseBtns.Left:
                    if (swapedButtons)
                    {
                        swapedButtons = false;
                        goto case ExtraMouseBtns.Right;
                    }

                    if (action == ExtraMouseActions.Down)
                        mouseEventValue = Win32.MOUSEEVENTF_LEFTDOWN;
                    else if (action == ExtraMouseActions.Up)
                        mouseEventValue = Win32.MOUSEEVENTF_LEFTUP;
                    break;
                case ExtraMouseBtns.Right:
                    if (swapedButtons)
                    {
                        swapedButtons = false;
                        goto case ExtraMouseBtns.Left;
                    }

                    if (action == ExtraMouseActions.Down)
                        mouseEventValue = Win32.MOUSEEVENTF_RIGHTDOWN;
                    else if (action == ExtraMouseActions.Up)
                        mouseEventValue = Win32.MOUSEEVENTF_RIGHTUP;
                    break;
                case ExtraMouseBtns.Middle:
                    if (action == ExtraMouseActions.Down)
                        mouseEventValue = Win32.MOUSEEVENTF_MIDDLEDOWN;
                    else if (action == ExtraMouseActions.Up)
                        mouseEventValue = Win32.MOUSEEVENTF_MIDDLEUP;
                    break;
                case ExtraMouseBtns.XButton1:
                case ExtraMouseBtns.XButton2:
                    if (action == ExtraMouseActions.Down)
                        mouseEventValue = Win32.MOUSEEVENTF_XDOWN;
                    else if (action == ExtraMouseActions.Up)
                        mouseEventValue = Win32.MOUSEEVENTF_XUP;
                    break;
                case ExtraMouseBtns.Wheel:
                    mouseEventValue = Win32.MOUSEEVENTF_WHEEL;
                    break;
            }
            if (action == ExtraMouseActions.Click)
                mouseEventValue = GetMouseEventValue(button, ExtraMouseActions.Down) | GetMouseEventValue(button, ExtraMouseActions.Up);
            return mouseEventValue;
        }

        private static int GetDwData(ExtraMouseBtns button, ExtraMouseActions action)
        {
            int dwData = 0;
            switch (button)
            {
                case ExtraMouseBtns.XButton1:
                    dwData = Win32.XBUTTON1;
                    break;
                case ExtraMouseBtns.XButton2:
                    dwData = Win32.XBUTTON2;
                    break;
                case ExtraMouseBtns.Wheel:
                    if (action == ExtraMouseActions.WheelDown)
                        dwData = 120;
                    else if (action == ExtraMouseActions.WheelUp)
                        dwData = -120;
                    break;
                default:
                    dwData = 0;
                    break;
            }
            return dwData;
        }

        private ExtraMouseBtns ExtractButton(IntPtr wParam, Win32.MouseLLHookStruct hookStruct)
        {
            ExtraMouseBtns button = ExtraMouseBtns.None;
            switch (wParam.ToInt32())
            {
                //left button messages
                case Win32.WM_LBUTTONDBLCLK:
                case Win32.WM_NCLBUTTONDBLCLK:
                case Win32.WM_LBUTTONDOWN:
                case Win32.WM_LBUTTONUP:
                case Win32.WM_NCLBUTTONDOWN:
                case Win32.WM_NCLBUTTONUP:
                    button = ExtraMouseBtns.Left;
                    break;

                //right button messages
                case Win32.WM_RBUTTONDBLCLK:
                case Win32.WM_NCRBUTTONDBLCLK:
                case Win32.WM_RBUTTONDOWN:
                case Win32.WM_RBUTTONUP:
                case Win32.WM_NCRBUTTONDOWN:
                case Win32.WM_NCRBUTTONUP:
                    button = ExtraMouseBtns.Right;
                    break;

                //middle button messages
                case Win32.WM_MBUTTONDBLCLK:
                case Win32.WM_NCMBUTTONDBLCLK:
                case Win32.WM_MBUTTONDOWN:
                case Win32.WM_MBUTTONUP:
                case Win32.WM_NCMBUTTONDOWN:
                case Win32.WM_NCMBUTTONUP:
                    button = ExtraMouseBtns.Middle;
                    break;

                //x button messages
                case Win32.WM_XBUTTONDBLCLK:
                case Win32.WM_NCXBUTTONDBLCLK:
                case Win32.WM_XBUTTONDOWN:
                case Win32.WM_XBUTTONUP:
                case Win32.WM_NCXBUTTONDOWN:
                case Win32.WM_NCXBUTTONUP:
                    short xstate = (short)((hookStruct.mouseData >> 16) & 0xffff);
                    if (xstate == Win32.XBUTTON1) button = ExtraMouseBtns.XButton1;
                    else if (xstate == Win32.XBUTTON2) button = ExtraMouseBtns.XButton2;
                    else button = ExtraMouseBtns.None;
                    break;

                case Win32.WM_MOUSEWHEEL:
                    button = ExtraMouseBtns.Wheel;
                    break;
            }
            return button;
        }

        private ExtraMouseActions ExtractAction(IntPtr wParam, Win32.MouseLLHookStruct hookStruct)
        {
            ExtraMouseActions action = ExtraMouseActions.None;

            switch (wParam.ToInt32())
            {
                case Win32.WM_MOUSEMOVE:
                case Win32.WM_NCMOUSEMOVE:
                    action = ExtraMouseActions.Move;
                    break;

                case Win32.WM_LBUTTONDOWN:
                case Win32.WM_RBUTTONDOWN:
                case Win32.WM_MBUTTONDOWN:
                case Win32.WM_XBUTTONDOWN:
                case Win32.WM_NCLBUTTONDOWN:
                case Win32.WM_NCRBUTTONDOWN:
                case Win32.WM_NCMBUTTONDOWN:
                case Win32.WM_NCXBUTTONDOWN:
                    action = ExtraMouseActions.Down;
                    break;

                // button up messages
                case Win32.WM_LBUTTONUP:
                case Win32.WM_RBUTTONUP:
                case Win32.WM_MBUTTONUP:
                case Win32.WM_XBUTTONUP:
                case Win32.WM_NCLBUTTONUP:
                case Win32.WM_NCRBUTTONUP:
                case Win32.WM_NCMBUTTONUP:
                case Win32.WM_NCXBUTTONUP:
                    action = ExtraMouseActions.Up;
                    break;

                // double click messages
                case Win32.WM_LBUTTONDBLCLK:
                case Win32.WM_RBUTTONDBLCLK:
                case Win32.WM_MBUTTONDBLCLK:
                case Win32.WM_XBUTTONDBLCLK:
                case Win32.WM_NCLBUTTONDBLCLK:
                case Win32.WM_NCRBUTTONDBLCLK:
                case Win32.WM_NCMBUTTONDBLCLK:
                case Win32.WM_NCXBUTTONDBLCLK:
                    action = ExtraMouseActions.DoubleClick;
                    break;

                case Win32.WM_MOUSEWHEEL:
                    short mouseDelta = (short)((hookStruct.mouseData >> 16) & 0xffff);
                    if (mouseDelta == 120)
                        action = ExtraMouseActions.WheelDown;
                    else if (mouseDelta == -120)
                        action = ExtraMouseActions.WheelUp;
                    break;
            }
            return action;
        }
    }
}
