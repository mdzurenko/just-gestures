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
    public partial class UC_whiteList : UC_blackWhiteList
    {
        public UC_whiteList()
        {
            InitializeComponent();
            m_name = new MyText("O_WL_name");
            m_caption = new MyText("O_WL_caption");
            I_infoText = new MyText("O_WL_info");
            this.Dock = DockStyle.Fill;
        }
    }
}
