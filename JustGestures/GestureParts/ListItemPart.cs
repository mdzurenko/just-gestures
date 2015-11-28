using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace JustGestures.GestureParts
{
    [Serializable]
    public class ListItemPart : ListViewItem, ISerializable
    {
        private const string SUBITEM_DESCRIPTION = "description";

        protected string m_id = string.Empty;
        protected string m_caption = string.Empty;
        protected string m_description = string.Empty;
        protected bool m_active = true;
        protected ExecuteType m_executionType = ExecuteType.ImplicitIfUnique; 
        protected int m_itemPos = -1;
        protected bool m_isExpanded = true;

        public string Caption 
        { 
            get { return m_caption; } 
            set 
            {
                if (value.Length <= 100)
                    m_caption = value;
                else
                    m_caption = value.Substring(0, 100);
            } 
        }

        public string Description 
        { 
            get { return m_description; } 
            set 
            { 
                m_description = value;
                if (base.SubItems.ContainsKey(SUBITEM_DESCRIPTION))
                    base.SubItems[SUBITEM_DESCRIPTION].Text = m_description;
            } 
        }
        public bool Active { get { return m_active; } set { m_active = value; } }
        public ExecuteType ExecutionType { get { return m_executionType; } set { m_executionType = value; } }
        public string PriorityName { get { return Languages.Translation.GetPriorityText(m_executionType); } }
        public int ItemPos { get { return m_itemPos; } set { m_itemPos = value; } }
        public bool IsExpanded { get { return m_isExpanded; } set { m_isExpanded = value; } }

        protected ListItemPart(string id)
            : base()
        {
            m_id = id;
            base.Name = m_id;
        }

        public void SetItem(MyGesture gesture)
        {
            m_active = gesture.Active;
            m_caption = gesture.Caption;
            m_description = gesture.Description;
            m_executionType = gesture.ExecutionType;
            m_itemPos = gesture.ItemPos;
            SetMainListItem();
        }

        protected void SetMainListItem()
        {
            base.Checked = m_active;
            base.Text = m_caption;
            ListViewSubItem subItem = new ListViewSubItem();
            subItem.Name = SUBITEM_DESCRIPTION;
            subItem.Text = m_description;
            base.SubItems.Add(subItem);
            base.ImageKey = m_id;
        }

        protected ListViewItem ToSameListItem(string groupID, bool groupActive)
        {
            ListViewItem item = new ListViewItem();
            item.Checked = m_active && groupActive;            
            item.Name = m_id;
            item.Text = m_caption;            
            item.SubItems.Add(Languages.Translation.GetPriorityText(m_executionType));
            item.ImageKey = groupID;
            item.Tag = m_id;
            return item;
        }

        public ToolStripMenuItem ToToolStripMenuItem(ImageList imgList)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Image = imgList.Images[m_id];
            item.Name = m_id;
            item.Text = m_caption;
            return item;
        }


        public ListItemPart(SerializationInfo info, StreamingContext context)
        {
            try { m_id = info.GetString("ID"); }
            catch { m_id = string.Empty; }
            try { m_caption = info.GetString("Caption"); }
            catch { m_caption = string.Empty; }    
            try { m_description = info.GetString("Description"); }
            catch { m_description = string.Empty; }
            try { m_active = info.GetBoolean("Active"); }
            catch { m_active = true; }
            try { m_isExpanded = info.GetBoolean("IsExpanded"); }
            catch { m_isExpanded = true; }
            try { m_itemPos = info.GetInt32("ItemPos"); }
            catch { m_itemPos = 0; }
            try { m_executionType = (ExecuteType)info.GetValue("Execution", typeof(ExecuteType)); }
            catch { m_executionType = ExecuteType.ImplicitIfUnique; }
            base.Name = m_id;
            SetMainListItem();
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", m_id);
            info.AddValue("Caption", m_caption);
            info.AddValue("Description", m_description);
            info.AddValue("Active", m_active);
            info.AddValue("IsExpanded", m_isExpanded);
            info.AddValue("ItemPos", m_itemPos);
            info.AddValue("Execution", m_executionType);
        }
    }
}
