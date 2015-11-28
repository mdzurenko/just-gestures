using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace JustGestures
{
    [Serializable]
    public class MyKey : ListViewItem, ISerializable
    {
        public enum Action
        {
            KeyDown,
            KeyUp,
            KeyClick,
            MouseClick,
            MouseDblClick,
            MouseX1Click,
            MouseX1DblClick,
            MouseX2Click,
            MouseX2DblClick,
            MouseWheelUp,
            MouseWheelDown
        }

        private ushort m_value;
        private string m_keyName;
        private Action m_action;

        public MyKey()
        {
            m_value = 0;
            m_keyName = string.Empty;
            m_action = Action.KeyClick;
            base.Text = string.Empty;
            base.SubItems.Add("0");
            base.SubItems.Add("2");
        }

        public MyKey(ushort value, string name)
        {            
            m_value = value;
            m_keyName = name;
            m_action = Action.KeyClick;
            base.Text = name;
            base.SubItems.Add(value.ToString());
            base.SubItems.Add("2");
        }

        public MyKey(ushort value, string name, Action action)
        {
            m_value = value;
            m_keyName = name;
            m_action = action;
            base.Text = name;
            base.SubItems.Add(value.ToString());
            base.SubItems.Add(((int)action).ToString());
        }

        public static MyKey ListViewItemToKey(ListViewItem item)
        {
            string keyName = item.Text;
            ushort keyValue = ushort.Parse(item.SubItems[1].Text);
            Action keyAction = (Action)Enum.Parse(typeof(Action), item.SubItems[2].Text);
            return new MyKey(keyValue, keyName, keyAction);
        }
              
        public ushort Value
        {
            get { return m_value; }
        }

        public string KeyName
        {
            get { return m_keyName; }
        }

        public Action KeyAction
        {
            get { return m_action; }
            set { m_action = value; } 
        }

        public MyKey(SerializationInfo info, StreamingContext context)
        {
            try { m_value = info.GetUInt16("Value"); }
            catch { m_value = 0; }
            try { m_keyName = info.GetString("Key"); }
            catch { m_keyName = string.Empty; }
            try { m_action = (Action)info.GetValue("KeyAction", typeof(Action)); }
            catch { m_action = Action.KeyClick; }
            base.Text = m_keyName;
            base.SubItems.Add(m_value.ToString());
            base.SubItems.Add(((int)m_action).ToString());
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", m_value);
            info.AddValue("Key", m_keyName);
            info.AddValue("KeyAction", m_action, typeof(Action));
        }


    }
}
