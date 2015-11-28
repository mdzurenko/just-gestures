using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JustGestures.Features;
using JustGestures.Languages;

namespace JustGestures.OptionItems
{
    using Properties;

    public partial class UC_blackWhiteList : BaseOptionControl
    {
        protected List<PrgNamePath> programs;
        bool onLoad;

        public override void Translate()
        {
            base.Translate();
            cH_name.Text = Translation.GetText("O_BW_cH_name");
            cH_path.Text = Translation.GetText("O_BW_cH_path");
            btn_add.Text = Translation.GetText("O_BW_btn_add");
            btn_remove.Text = Translation.GetText("O_BW_btn_remove");
        }

        public UC_blackWhiteList()
        {
            InitializeComponent();            
            programs = new List<PrgNamePath>();
            onLoad = false;     
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Programs (*.exe)|*.exe|All Files (*.*)|*.*";
            file.Multiselect = false;
            file.RestoreDirectory = true;
            if (file.ShowDialog() == DialogResult.OK)
            {
                PrgNamePath prg;
                int index = (file.FileName.LastIndexOf("\\"));
                if (index < 0) return;
                string name = file.FileName.Substring(index + 1);
                prg = new PrgNamePath(name, file.FileName, true);
                FindProgram findProgram = new FindProgram(prg);
                if (programs.Find(findProgram.FindItem) == null)
                {
                    programs.Add(prg);
                    lV_list.Items.Add(prg);
                }
                OnEnableApply(true);
            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {   
            ListView.SelectedIndexCollection items = lV_list.SelectedIndices;
            while (items.Count > 0)
            {
                programs.RemoveAt(items[items.Count - 1]);
                lV_list.Items.RemoveAt(items[items.Count - 1]);
            }
        }

        public List<PrgNamePath> Programs
        {
            get { return programs; }
            set { programs = value; }
        }

        private void UC_blackWhiteList_Load(object sender, EventArgs e)
        {
            onLoad = true;            
            foreach (PrgNamePath item in programs)
                lV_list.Items.Add((ListViewItem)item.Clone());
            onLoad = false;
        }

        private void lV_list_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            programs[e.Item.Index].Active = e.Item.Checked;
            if (!onLoad) OnEnableApply(true);
        }
    }    

   

}
