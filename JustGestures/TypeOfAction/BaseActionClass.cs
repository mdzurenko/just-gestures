using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.Serialization;
using JustGestures.GestureParts;

namespace JustGestures.TypeOfAction
{
    /// <summary>
    /// Parent for classes which implement the specific actions
    /// </summary>
    [Serializable]
    public class BaseActionClass : ISerializable, ICloneable
    {
        #region Members

        protected string m_name;

        public string Name { get { return m_name; } }

        protected string m_details;

        public string Details { get { return m_details; } set { m_details = value; } }

        protected List<List<MyKey>> m_keyScript;

        public List<List<MyKey>> KeyScript { get { return m_keyScript; } set { m_keyScript = value; } }

        public void CheckScriptForMouse()
        {
            m_scriptContainsMouse = false;
            
            if (m_keyScript == null)
                return;

            foreach (List<MyKey> scriptList in m_keyScript)
            {
                if (scriptList == null) continue;

                foreach (MyKey keyscrip in scriptList)
                    if (keyscrip != null
                        && keyscrip.KeyAction != MyKey.Action.KeyClick
                        && keyscrip.KeyAction != MyKey.Action.KeyDown
                        && keyscrip.KeyAction != MyKey.Action.KeyUp)
                    {
                        m_scriptContainsMouse = true;
                        return;
                    }
            }
        }

        private bool m_scriptContainsMouse = false;
        public bool ScriptContainsMouse { get { return m_scriptContainsMouse; } }

        protected List<string> m_actions;

        #endregion Members

        #region Constructors

        public BaseActionClass() 
            : this(string.Empty)
        {             
        }

        public BaseActionClass(string action) 
        {
            m_name = action;
            m_details = string.Empty;
            m_keyScript = new List<List<MyKey>>();
        }

        public BaseActionClass(BaseActionClass action)
            : this (action.Name)
        {
            this.Details = action.Details;
            this.KeyScript = new List<List<MyKey>>();
            foreach (List<MyKey> script in action.KeyScript)
                this.KeyScript.Add(new List<MyKey>(script.ToArray()));

            m_scriptContainsMouse = action.ScriptContainsMouse;
        }

        public virtual object Clone()
        {
            BaseActionClass action = new BaseActionClass(this);
            return action;
        }

        public virtual void ControlLostFocus()
        {

        }

        public BaseActionClass(SerializationInfo info, StreamingContext context)
        {
            try { m_name = info.GetString("ActionName"); }
            catch { m_name = string.Empty; }
            try { m_details = info.GetString("Details"); }
            catch { m_details = string.Empty; }
            try { m_keyScript = (List<List<MyKey>>)info.GetValue("KeyScript", typeof(List<List<MyKey>>)); }
            catch { m_keyScript = new List<List<MyKey>>(); }
        }

        #endregion Constructors

        public virtual void ExecuteAction(IntPtr activeWnd, Point location) { }

        public virtual string GetDescription()
        {
            return Languages.Translation.GetText(this.Name);
        }

        public virtual Bitmap GetIcon(int size)
        {
            return Properties.Resources.just_gestures.ToBitmap();
        }

        public string[] GetValues()
        {
            return m_actions.ToArray();
        }

        public bool ContainAction(string name)
        {
            foreach (string str in m_actions)
                if (str.Equals(name)) return true;
            return false;
        }

        public virtual bool IsExtras()
        {
            return false;
        }

        public virtual bool IsSameType(BaseActionClass action)
        {
            return this.GetType().Name == action.GetType().Name;
        }

        /// <summary>
        /// Is used whether the action might be exececuted when Form_transparent or CMS_matchedGestures is selected
        /// </summary>
        /// <returns></returns>
        public virtual bool IsSensitiveToMySystemWindows()
        {
            return true;
        }

        public virtual void ExecuteKeyScript(MouseAction mouse, IntPtr ActiveWnd, bool selectWnd, Point location)
        {
            if (selectWnd)
            {
                Win32.SetForegroundWindow(ActiveWnd);
                Win32.SetActiveWindow(ActiveWnd);
            }
            ExecuteKeyList(mouse, location);

            //Win32.INPUT[][] inputRange = KeyInput.CreateInput(KeystrokesControl.ExtractKeyList(action, mouse));
            //foreach (Win32.INPUT[] input in inputRange)
            //{
            //    Win32.SendInput((uint)input.Length, input, (int)System.Runtime.InteropServices.Marshal.SizeOf(input[0].GetType()));
            //}
        }

        public void ExecuteKeyList(MouseAction mouseAction, Point location)
        {
            Win32.INPUT[][] inputRange = Features.KeyInput.CreateInput(ExtractKeyList(mouseAction));
            Point currentLocation = new Point();
            foreach (Win32.INPUT[] input in inputRange)
            {
                foreach (Win32.INPUT one_input in input)
                {
                    Win32.INPUT[] one = new Win32.INPUT[1];
                    one[0] = one_input;
                    if (one_input.type == Win32.INPUT_MOUSE)
                    {
                        currentLocation = Cursor.Position;
                        Win32.SetCursorPos(location.X, location.Y);
                    }
                    Win32.SendInput((uint)one.Length, one, (int)System.Runtime.InteropServices.Marshal.SizeOf(one[0].GetType()));
                    System.Threading.Thread.Sleep(1);
                    if (one_input.type == Win32.INPUT_MOUSE)
                    {
                        Win32.SetCursorPos(currentLocation.X, currentLocation.Y);
                    }
                }
            }

            //Win32.INPUT[][] inputRange = KeyInput.CreateInput(ExtractKeyList(action, MouseAction.ModifierClick));
            //foreach (Win32.INPUT[] input in inputRange)
            //{               
            //    Win32.SendInput((uint)input.Length, input, (int)System.Runtime.InteropServices.Marshal.SizeOf(input[0].GetType()));                        
            //}
        }

        public enum MouseAction
        {
            TriggerDown,
            TriggerUp,
            WheelDown,
            WheelUp,
            ModifierClick
        }

        public List<MyKey> ExtractKeyList(MouseAction mouse)
        {
            List<MyKey> keyScript = new List<MyKey>();
            if (this.KeyScript != null && this.KeyScript.Count == 4) //ModifierClick
                keyScript = this.KeyScript[0];
            else
                return keyScript;

            switch (mouse)
            {
                case MouseAction.TriggerDown:
                    keyScript = this.KeyScript[0];
                    break;
                case MouseAction.WheelDown:
                    keyScript = this.KeyScript[1];
                    break;
                case MouseAction.WheelUp:
                    keyScript = this.KeyScript[2];
                    break;
                case MouseAction.TriggerUp:
                    keyScript = this.KeyScript[3];
                    break;
            }
            return keyScript;
        }

        #region ISerializable Members

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ActionName", m_name);
            info.AddValue("Details", m_details);
            info.AddValue("KeyScript", m_keyScript, typeof(List<List<MyKey>>));
        }

        #endregion
    }
}
