using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JustGestures.Languages;

namespace JustGestures.OptionItems
{
    public partial class UC_blackList : UC_blackWhiteList
    {
        public UC_blackList()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            m_name = new MyText("O_BL_name");
            m_caption = new MyText("O_BL_caption");
            I_infoText = new MyText("O_BL_info");
        }
    }
}
