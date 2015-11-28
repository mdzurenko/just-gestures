using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace JustGestures
{
    /// <summary>
    /// Conversions between values and indexes
    /// </summary>
    public class ConvertValue
    {
        public static int MouseButtonsCount = 5; 

        public static int MouseBtnToIndex(MouseButtons btn)
        {
            switch (btn)
            {
                case MouseButtons.Left: return 0;
                case MouseButtons.Right: return 1;
                case MouseButtons.Middle: return 2;
                case MouseButtons.XButton1: return 3;
                case MouseButtons.XButton2: return 4;
                default: return 1;
            }
        }

        public static MouseButtons IndexToMouseBtn(int index)
        {
            switch (index)
            {
                case 0: return MouseButtons.Left;
                case 1: return MouseButtons.Right;
                case 2: return MouseButtons.Middle;
                case 3: return MouseButtons.XButton1;
                case 4: return MouseButtons.XButton2;
                default: return MouseButtons.Right;
            }
        }

        public static int PrioritiesCount = 3;

        public static ExecuteType IndexToPriority(int index)
        {
            switch (index)
            {
                case 0: return ExecuteType.Implicit;
                case 1: return ExecuteType.ImplicitIfUnique;
                case 2: return ExecuteType.Explicit;
                default: return ExecuteType.Implicit;
            }
        }

        public static int PriorityToIndex(ExecuteType priority)
        {
            switch (priority)
            {
                case ExecuteType.Implicit: return 0;
                case ExecuteType.ImplicitIfUnique: return 1;
                case ExecuteType.Explicit: return 2;
                default: return 0;
            }
        }

        public static int WndStylesCount = 3;

        public static string IndexToWndStyle(int index)
        {
            switch (index)
            {
                case 0: return TypeOfAction.WindowsShell.WND_STYLE_NORMAL;
                case 1: return TypeOfAction.WindowsShell.WND_STYLE_MINIMIZED;
                case 2: return TypeOfAction.WindowsShell.WND_STYLE_MAXIMIZED;
                default: return TypeOfAction.WindowsShell.WND_STYLE_NORMAL;
            }
        }

        public static int WndStyleToIndex(string wndStyle)
        {
            switch (wndStyle)
            {
                case TypeOfAction.WindowsShell.WND_STYLE_NORMAL: return 0;
                case TypeOfAction.WindowsShell.WND_STYLE_MINIMIZED: return 1;
                case TypeOfAction.WindowsShell.WND_STYLE_MAXIMIZED: return 2;
                default: return 0;
            }
        }
     



    }
}
