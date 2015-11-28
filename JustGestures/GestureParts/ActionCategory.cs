using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using JustGestures.TypeOfAction;

namespace JustGestures.GestureParts
{
    public class ActionCategory
    {

        private static readonly List<string> m_categories = new List<string>
            (new string[] {   
                WindowOptions.NAME,
                WindowsShell.NAME,
                //WinampControl.NAME,
                VolumeOptions.NAME,
                MediaControl.NAME,
                InternetOptions.NAME,
                KeystrokesOptions.NAME,
                ExtrasOptions.NAME
                //SpecialControl.NAME                
            });

        public static TreeNode GetCategories(ImageList imgList)
        {
            imgList.Images.Add("justgestures", Properties.Resources.just_gestures);
            TreeNode node = new TreeNode("Just Gestures");            
            node.SelectedImageKey = "justgestures";
            node.ImageKey = "justgestures";
            node.Expand();
            TreeNode[] nodes = new TreeNode[m_categories.Count];            
            for (int i = 0; i < m_categories.Count; i++)
            {
                nodes[i] = new TreeNode();
                nodes[i].Text = Languages.Translation.GetText(m_categories[i]);
                nodes[i].Name = m_categories[i];
                imgList.Images.Add(m_categories[i], InitializeAction(m_categories[i], m_categories[i]).GetIcon(imgList.ImageSize.Width));
                //imgList.Images.Add(m_categories[i], GetIcon(new Action(m_categories[i], m_categories[i])));
            }
            node.Nodes.AddRange(ArrayToNodes(m_categories.ToArray()));
            return node;
        }

        public static TreeNode GetActions(string category, ImageList imgList)
        {
            TreeNode node = new TreeNode();
            node.Text = Languages.Translation.GetText(category);
            node.Name = category;
            node.SelectedImageKey = category;
            node.ImageKey = category;
            node.Expand();
            string[] actions = InitializeAction(category, category).GetValues();
            foreach (string actionName in actions)
            {
                BaseActionClass action = InitializeAction(category, actionName);
                imgList.Images.Add(actionName, action.GetIcon(imgList.ImageSize.Width));
                node.Nodes.Add(GetNode(action));
                //imgList.Images.Add(action, GetIcon(new Action(action, category)));
            }
            //node.Nodes.AddRange(ArrayToNodes(actions));
            return node;
        }

        private static TreeNode GetNode(BaseActionClass action)
        {
            TreeNode node = new TreeNode();
            node.Text = Languages.Translation.GetText(action.Name);
            node.Name = action.Name;
            node.SelectedImageKey = action.Name;
            node.ImageKey = action.Name;
            node.Tag = action;
            return node;
        }

        private static TreeNode[] ArrayToNodes(string[] actions)
        {
            TreeNode[] nodes = new TreeNode[actions.Length];
            for (int i = 0; i < actions.Length; i++)
            {
                nodes[i] = new TreeNode();
                nodes[i].Text = Languages.Translation.GetText(actions[i]);
                nodes[i].Name = actions[i];
                nodes[i].SelectedImageKey = actions[i];
                nodes[i].ImageKey = actions[i];
                
            }
            return nodes;
        }
        private static BaseActionClass InitializeAction(string category, string actionName)
        {
            BaseActionClass action = null;
            switch (category)
            {
                case InternetOptions.NAME:
                    if (category != actionName)
                        action = new InternetOptions(actionName);
                    else
                        action = new InternetOptions();
                    break;
                case WinampOptions.NAME:
                    if (category != actionName)
                        action = new WinampOptions(actionName);
                    else
                        action = new WinampOptions();
                    break;
                case MediaControl.NAME:
                    if (category != actionName)
                        action = new MediaControl(actionName);
                    else
                        action = new MediaControl();
                    break;
                case WindowOptions.NAME:
                    if (category != actionName)
                        action = new WindowOptions(actionName);
                    else
                        action = new WindowOptions();
                    break;
                case WindowsShell.NAME:
                    if (category != actionName)
                        action = new WindowsShell(actionName);
                    else
                        action = new WindowsShell();
                    break;
                case KeystrokesOptions.NAME:
                    if (category != actionName)
                        action = new KeystrokesOptions(actionName);
                    else
                        action = new KeystrokesOptions();
                    break;
                case SpecialOptions.NAME:
                    if (category != actionName)
                        action = new SpecialOptions(actionName);
                    else
                        action = new SpecialOptions();
                    break;
                case AppGroupOptions.NAME:
                    if (category != actionName)
                        action = new AppGroupOptions(actionName);
                    else
                        action = new AppGroupOptions();
                    break;
                case ExtrasOptions.NAME:
                    if (category != actionName)
                        action = new ExtrasOptions(actionName);
                    else
                        action = new ExtrasOptions();
                    break;
                case VolumeOptions.NAME:
                    if (category != actionName)
                        action = new VolumeOptions(actionName);
                    else
                        action = new VolumeOptions();
                    break;
                default:
                    //if (category != actionName)
                    //    action = new BaseActionClass(actionName);
                    //else
                        action = new BaseActionClass();
                    break;
            }
            return action;  
        }
    }
}
