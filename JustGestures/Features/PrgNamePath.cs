using System;
using System.Collections.Generic;
using System.Text;

namespace JustGestures.Features
{
    /// <summary>
    /// Class for saving the information about programs in black/white list
    /// </summary>
    public class PrgNamePath : System.Windows.Forms.ListViewItem
    {
        private string prgName;
        private string path;
        private bool active;

        public PrgNamePath()
        {
        }

        public PrgNamePath(string _state, string _name, string _path)
        {
            prgName = _name;
            path = _path;
            active = (_state == OptionItems.UC_autoBehaviour.STATE_ENABLED ? true : false);
            base.Text = _state;
            base.SubItems.Add(_name);
            base.SubItems.Add(_path);
        }

        public PrgNamePath(string _name, string _path, bool _active)
        {
            prgName = _name;
            path = _path;
            active = _active;
            base.Text = prgName;
            base.SubItems.Add(path);
            base.Checked = active;
        }

        public string PrgName
        {
            get { return prgName; }
            set { prgName = value; }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
    }

    public class FindProgram
    {
        PrgNamePath finding;

        public FindProgram(PrgNamePath _finding)
        {
            finding = _finding;
        }

        public bool FindItem(PrgNamePath pattern)
        {
            return finding.Path == pattern.Path;
        }
    }
}
