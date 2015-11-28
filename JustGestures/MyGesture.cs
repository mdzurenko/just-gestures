using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using JustGestures.GestureParts;
using JustGestures.TypeOfAction;

namespace JustGestures
{
    [Serializable]
    public class MyGesture : ListItemPart, ISerializable
    {        
        BaseActionClass m_action = null;
        MouseActivator m_activator = null;
        MyGesture m_group = null;

        public string ID { get { return base.m_id; } }

        public BaseActionClass Action
        {
            get { return m_action; }
            set
            {
                if (value == null) return;
                m_action = value;
                base.m_description = m_action.GetDescription();               
            }
        }

        public MouseActivator Activator { get { return m_activator; } set { m_activator = value; } }

        public MyGesture AppGroup { get { return m_group; } set { m_group = value; } }

        public bool IsGroup { get { return m_group == null; } }

        public bool IsActive(IntPtr hWnd)
        {
            return base.m_active && m_group.Active && ((AppGroupOptions)m_group.Action).IsAppGroupActive(hWnd);
                //TypeOfAction.AppGroupControl.IsAppGroupActive(hWnd, m_group.Action);
        }

        public bool IsWorking
        {
            get
            {
                switch (m_activator.Type)
                {
                    case BaseActivator.Types.ClassicCurve: return Config.User.UsingClassicCurve; //break;
                    case BaseActivator.Types.DoubleButton: return Config.User.UsingDoubleBtn; //break;
                    case BaseActivator.Types.WheelButton: return Config.User.UsingWheelBtn; //break;
                    default: return true; //break;
                }
            }
        }

        public bool IsImplicitOnly
        {
            get
            {
                if (m_activator.Type == BaseActivator.Types.WheelButton)
                    return true;
                else
                    return false;
                //return ScriptContainsMouse;
            }
        }

        public bool ScriptContainsMouse
        {
            get
            {
                if (m_action == null)
                    return false;
                else
                    return m_action.ScriptContainsMouse;
            }
        }

        public static MyGesture GlobalGroup = new MyGesture(true);

        public MyGesture(string id)
            : base(id)
        {
            
        }        

        public MyGesture(MyGesture newGesture)
            : base(newGesture.ID)
        {
            m_action = (BaseActionClass)newGesture.Action.Clone();     
            m_activator = new MouseActivator(newGesture.m_activator);
            m_group = newGesture.AppGroup;
            base.SetItem(newGesture);
        }

        public MyGesture(string id, MyGesture newGesture, MyGesture group)
            : base (id)
        {
            m_action = (BaseActionClass)newGesture.Action.Clone();
            m_activator = new MouseActivator(newGesture.m_activator);
            m_group = group;
            base.SetItem(newGesture);
        }

        private MyGesture(bool isGlobalGroup)
            : base(TypeOfAction.AppGroupOptions.APP_GROUP_GLOBAL)
        {
            m_caption = Languages.Translation.GetText(TypeOfAction.AppGroupOptions.APP_GROUP_GLOBAL);
            m_action = new AppGroupOptions(AppGroupOptions.APP_GROUP_GLOBAL);
            m_action.Details = TypeOfAction.AppGroupOptions.APP_GROUP_GLOBAL;
            m_activator = new MouseActivator(string.Empty, MouseActivator.Types.Undefined);
            m_group = null;
            base.SetMainListItem();
        }

        public void ChangeDescription()
        {
            base.Description = m_action.GetDescription();
            if (m_action.Details == TypeOfAction.AppGroupOptions.APP_GROUP_GLOBAL)
            {
                m_caption = Languages.Translation.GetText(TypeOfAction.AppGroupOptions.APP_GROUP_GLOBAL);
                base.Text = m_caption;
            }
            
        }

        public ListViewItem ToSameListItem()
        {
            return base.ToSameListItem(m_group.ID, m_group.Active);
        }
    
        public void SetActionIcon(ImageList imgList)
        {
            SetIcon(base.m_id, m_action.GetIcon(imgList.ImageSize.Width), imgList);
        }

        public void SetGestureIcon(ImageList imgList)
        {
            if (IsGroup)
                SetIcon(base.m_id, m_action.GetIcon(imgList.ImageSize.Width), imgList);
            else
                SetIcon(m_activator.ID, m_activator.GetIcon(imgList.ImageSize), imgList);
        }

        private void SetIcon(string id, Bitmap bmp, ImageList imgList)
        {            
            if (imgList.Images.ContainsKey(id))
            {
                imgList.Images.RemoveByKey(id);
                imgList.Images.Add(id, bmp);
            }
            else
                imgList.Images.Add(id, bmp);
        }

        public static string CreateUniqueId(MyGesture gest, GesturesCollection gestures)
        {
            string name = gest.Action.Name;
            int postfix = 1;
            while (gestures[name + postfix.ToString()] != null) postfix++;
            return name + postfix.ToString();
        }

        public MyGesture(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            try { m_action = (BaseActionClass)info.GetValue("Action", typeof(BaseActionClass)); }
            catch { m_action = new BaseActionClass(); }
            try { m_activator = (MouseActivator)info.GetValue("Activator", typeof(MouseActivator)); }
            catch { m_activator = null; }
            try { m_group = (MyGesture)info.GetValue("Group", typeof(MyGesture)); }
            catch { m_group = null; }
            base.SetMainListItem();
            m_action.CheckScriptForMouse();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Action", m_action, typeof(BaseActionClass));
            info.AddValue("Activator", m_activator, typeof(MouseActivator));
            info.AddValue("Group", m_group, typeof(MyGesture));
        }
    }
}
