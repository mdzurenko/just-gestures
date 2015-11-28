using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using JustGestures.GestureParts;
using JustGestures.Languages;

namespace JustGestures
{
    [Serializable]
    public class GesturesCollection : List<MyGesture>, ITranslation
    {
        List<MyGesture> m_gestures = new List<MyGesture>();
        List<MyGesture> m_groups = new List<MyGesture>();
        Dictionary<string, List<MyGesture>> m_matchedGestures = new Dictionary<string, List<MyGesture>>();
        Dictionary<MyGesture, List<MyGesture>> m_hiddenActions = new Dictionary<MyGesture, List<MyGesture>>();
        
        public List<MyGesture> Gestures { get { return m_gestures; } }
        public List<MyGesture> Groups { get { return m_groups; } }

        #region ITranslation Members

        private void TranslateGestures()
        {
            foreach (MyGesture gest in this)
                gest.ChangeDescription();
        }

        public void Translate()
        {
            TranslateGestures();
        }

        #endregion


        public MyGesture this[string key]
        {
            get
            {
                foreach (MyGesture gest in this)
                    if (gest.ID == key) return gest;
                foreach (MyGesture group in m_hiddenActions.Keys)
                    foreach (MyGesture gest in m_hiddenActions[group])
                        if (gest.ID == key) return gest;
                return null;
            }
        }

        public new MyGesture this[int index]
        {
            set
            {
                if (value.IsGroup)
                {
                    int groupIndex = m_groups.IndexOf(base[index]);                    
                    base[index] = value;
                    UpdateGroupGestures(m_groups[groupIndex].ID, base[index]);
                    m_groups[groupIndex] = value;
                }
                else
                {
                    RemoveGesture(base[index]);
                    base[index] = value;
                    AddGesture(base[index]);
                }
            }
            get
            {
                return base[index];
            }
        }

        private void UpdateGroupGestures(string oldGroupID, MyGesture newGroup)
        {
            // update group for all gestures
            foreach (MyGesture gest in m_gestures)
            {
                if (gest.AppGroup.ID == oldGroupID)
                    gest.AppGroup = newGroup;
            }

            // check if the hidden group should be updated
            MyGesture hiddenGroup = null;
            foreach (MyGesture group in m_hiddenActions.Keys)
            {
                if (group.ID == oldGroupID)
                {
                    hiddenGroup = group;
                    break;
                }
            }

            // update gestures in hidden group
            if (hiddenGroup != null)
            {
                newGroup.IsExpanded = false;
                // move gestures to new group and update them
                m_hiddenActions.Add(newGroup, m_hiddenActions[hiddenGroup]);                
                foreach (MyGesture gest in m_hiddenActions[newGroup])
                    gest.AppGroup = newGroup;
                // remove old group
                m_hiddenActions.Remove(hiddenGroup);
            }
        }

        public List<MyGesture> MatchedGestures(string curveName)
        {
            if (m_matchedGestures.ContainsKey(curveName))
                return m_matchedGestures[curveName];
            else
                return new List<MyGesture>();
                //return null;
        }

        public MouseActivator GetCurve(string curveName)
        {
            if (m_matchedGestures.ContainsKey(curveName))
                return m_matchedGestures[curveName][0].Activator;
            else
                return null;
        }

        public Dictionary<string, ClassicCurve> GetCurves()
        {
            Dictionary<string, ClassicCurve> curves = new Dictionary<string, ClassicCurve>();
            if (m_matchedGestures != null)
            {
                foreach (string key in m_matchedGestures.Keys)
                    if (m_matchedGestures[key][0].Activator.Type == MouseActivator.Types.ClassicCurve)                    
                        curves.Add(key, m_matchedGestures[key][0].Activator);
            }
            return curves;
        }

        /// <summary>
        /// Get all gestures and actions (ignores collapsed groups)
        /// </summary>
        /// <returns></returns>
        public List<MyGesture> GetAll()
        {
            List<MyGesture> gestures = new List<MyGesture>();
            foreach (MyGesture gest in this)
            {
                gestures.Add(gest);
                if (gest.IsGroup && !gest.IsExpanded)
                {
                    if (m_hiddenActions != null && m_hiddenActions.ContainsKey(gest))
                    {
                        foreach (MyGesture hiddenGest in m_hiddenActions[gest])
                            gestures.Add(hiddenGest);
                    }
                    else
                    {
                        // even though group is marked es collapsed the hidden actions has not been populated yet
                        // will be done during GestureListView initialization
                    }
                }
            }
            return gestures;
        }

        public GesturesCollection()
            : base()
        {
            //this.Add(MyGesture.GlobalGroup);

        }

        public GesturesCollection(MyGesture[] gestures)
            : base()
        {
            if (gestures != null)
                foreach (MyGesture gest in gestures)
                    this.Add(gest);

        }

        

        public new void Insert(int index, MyGesture gesture)
        {
            base.Insert(index, gesture);
            AddGesture(gesture);
        }

        /// <summary>
        /// Inerts new gesture after the similar actions
        /// </summary>
        /// <param name="gesture"></param>
        /// <returns></returns>
        public int AutoInsert(MyGesture gesture)
        {
            int index = 0; 
            if (!gesture.IsGroup)
            {
                int groupId;
                for (groupId = 0; groupId < m_groups.Count; groupId++)
                    if (m_groups[groupId].ID == gesture.AppGroup.ID)
                    {
                        gesture.AppGroup = m_groups[groupId];
                        break;
                    }
                
                if (m_groups[groupId].IsExpanded)
                {
                    // insert directly into list if the group is expanded
                    index = InsertIntoExpandedGroup(gesture, groupId);
                }               
                else
                {
                    // insert into hidden collection because group is collapsed
                    index = InsertIntoCollapsedGroup(gesture, groupId);                    
                }
            }
            else
            {
                index = this.Count;
                base.Add(gesture);
                AddGesture(gesture);
            }
            return index;
        }

        private int InsertIntoExpandedGroup(MyGesture gestToInsert, int groupId)
        {
            int index = 0;
            // if there is no suitable group or no groupt at all
            if (groupId < m_groups.Count)
                index = base.IndexOf(m_groups[groupId]);

            groupId++;
            int lastIndex = groupId < m_groups.Count ? base.IndexOf(m_groups[groupId]) : this.Count;            

            if (index != lastIndex)
            {
                List<MyGesture> gestures = this.GetRange(index + 1, lastIndex - index - 1);
                int posInList = GetPositionToInsert(gestures, gestToInsert);
                index += posInList;
            }
            else
                index++;
        
            Insert(index, gestToInsert);
            return index;
        }

        private int InsertIntoCollapsedGroup(MyGesture gestToInsert, int groupId)
        {
            int index = 0;
            List<MyGesture> groupActions = m_hiddenActions[m_groups[groupId]];
            index = GetPositionToInsert(groupActions, gestToInsert);
            index--;
            groupActions.Insert(index, gestToInsert);
            AddGesture(gestToInsert);
            // return -1 which will indicates that action should not be added because group is collapsed
            return -1;
        }

        private static int GetPositionToInsert(List<MyGesture> gestures, MyGesture gestToInsert)
        {
            int index = 0;
            bool sameType = false;

            for (int i = 0; i < gestures.Count; i++)
            {
                index++;
                MyGesture gest = gestures[i];
                if (gest.Action.IsSameType(gestToInsert.Action))
                {
                    if (!sameType) sameType = true;
                }
                else
                {
                    if (sameType)
                    {
                        index--;
                        break;
                    }
                }
            }
            index++;
            return index;
        }
       
        public new void Add(MyGesture gesture)
        {
            //int index = this.Count;            
            //if (!gesture.IsGroup)
            //{
            //    int i;
            //    for (i = 0; i < m_groups.Count; i++)
            //        if (m_groups[i].ID == gesture.AppGroup.ID)
            //        {
            //            gesture.AppGroup = m_groups[i];
            //            break;
            //        }
            //    i++;
            //    if (i < m_groups.Count)
            //    {
            //        index = base.IndexOf(m_groups[i]);
            //        Insert(index, gesture);
            //    }
            //    else
            //    {
            //        base.Add(gesture);
            //        AddGesture(gesture);
            //    }
            //}
            //else
            {
                base.Add(gesture);
                AddGesture(gesture);
            }
            //return index;
        }

        private void AddGesture(MyGesture gesture)
        {
            if (!gesture.IsGroup)
            {
                m_gestures.Add(gesture);
                if (m_matchedGestures.ContainsKey(gesture.Activator.ID))
                {
                    int i = 0;
                    //if (gesture.ItemPos != -1)
                    //{
                    //    while (i < m_matchedGestures[gesture.Activator.ID].Count
                    //        && gesture.ItemPos > m_matchedGestures[gesture.Activator.ID][i].ItemPos)
                    //        i++;
                    //}
                    //else
                    {
                        i = m_matchedGestures[gesture.Activator.ID].Count;
                        gesture.ItemPos = i;
                    }
                    m_matchedGestures[gesture.Activator.ID].Insert(i, gesture);
                }
                else
                {
                    gesture.ItemPos = 0;
                    m_matchedGestures.Add(gesture.Activator.ID, new List<MyGesture>() { gesture });
                }
            }
            else
                m_groups.Add(gesture);
        }

        public new string RemoveAt(int index)
        {
            if (index < base.Count)
            {
                MyGesture gesture = base[index];
                return this.Remove(gesture);
            }
            else
                return string.Empty;
        }

        public new string Remove(MyGesture gesture)
        {
            string curve = string.Empty;
            if (!MyGesture.Equals(gesture, MyGesture.GlobalGroup))
            {
                curve = RemoveGesture(gesture);
                base.Remove(gesture);
            }
            return curve;
        }

        private string RemoveGesture(MyGesture gesture)
        {
            string curve = string.Empty;
            if (!gesture.IsGroup)
            {
                m_gestures.Remove(gesture);
                if (m_matchedGestures[gesture.Activator.ID].Count > 1)
                {
                    int index = m_matchedGestures[gesture.Activator.ID].IndexOf(gesture);
                    m_matchedGestures[gesture.Activator.ID].Remove(gesture);
                    while (index < m_matchedGestures[gesture.Activator.ID].Count)
                    {
                        m_matchedGestures[gesture.Activator.ID][index].ItemPos--;
                        index++;
                    }
                }
                else
                {
                    curve = gesture.Activator.ID;
                    m_matchedGestures.Remove(gesture.Activator.ID);
                }
            }
            else
                m_groups.Remove(gesture);
            return curve;
        }

        public void SortMatchedGestures(string curveName)
        {
            if (m_matchedGestures.ContainsKey(curveName))
                m_matchedGestures[curveName].Sort(ComperByIndexes);
        }

        private static int ComperByIndexes(MyGesture x, MyGesture y)
        {
            return x.ItemPos.CompareTo(y.ItemPos);
        }

        public List<MyGesture> GetGroupActions(MyGesture group)
        {
            if (m_hiddenActions != null && m_hiddenActions.ContainsKey(group))
                return m_hiddenActions[group];
            else
            {
                int end = this.Count;
                int start = group.Index;
                MyGesture groupGest = this[start];
                int next = m_groups.IndexOf(groupGest);
                next++;
                if (next < m_groups.Count)
                    end = m_groups[next].Index;
                start++;

                if (start != end)
                    return this.GetRange(start, end - start);
                else
                    return new List<MyGesture>();
            }
        }

        /// <summary>
        /// Return true if group doesn't have any acctions
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool IsEmptyGroup(MyGesture group)
        {
            return GetGroupActions(group).Count == 0;
        }

        /// <summary>
        /// Collapse all groups that are set as collapsed.
        /// </summary>
        public void UpdateCollapsedGroups()
        {
            foreach (MyGesture group in m_groups)
                if (!group.IsExpanded)
                    CollapseGroup(group);
        }

        public void CollapseGroup(MyGesture group)
        {            
            if (m_hiddenActions == null)
                m_hiddenActions = new Dictionary<MyGesture, List<MyGesture>>();
            if (!m_hiddenActions.ContainsKey(group))
            {
                List<MyGesture> groupActions = GetGroupActions(group);
                m_hiddenActions.Add(group, groupActions);
                foreach (MyGesture gesture in groupActions)
                {
                    base.Remove(gesture);
                }
            }
        }

        public List<MyGesture> ExpandGroup(MyGesture group)
        {
            if (m_hiddenActions == null || !m_hiddenActions.ContainsKey(group))
                return new List<MyGesture>();
            else
            {
                List<MyGesture> actions = m_hiddenActions[group];
                base.InsertRange(group.Index + 1, actions);
                m_hiddenActions.Remove(group);
                return actions;
            }
        }
    }
}
