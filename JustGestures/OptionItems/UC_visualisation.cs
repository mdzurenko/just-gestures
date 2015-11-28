using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JustGestures.Properties;
using System.Diagnostics;
using System.Reflection;
using JustGestures.Languages;

namespace JustGestures.OptionItems
{
    public partial class UC_visualisation : BaseOptionControl
    {
        public const int WIDTH_COUNT = 17;
        public const int TOOL_TIP_DELAY_MAX = 2000;
        public const int TOOL_TIP_DELAY_MIN = 50;

        public enum eColors
        {
            Yellow = 0,
            Orange = 1,
            Red = 2,
            Purple = 3,
            Green = 4,
            Blue = 5,
            Brown = 6,
            //Black
        }

        public override void Translate()
        {
            base.Translate();
            gB_toolTipForGestures.Text = Translation.GetText("O_V_gB_toolTipForGestures");
            checkB_showToolTip.Text = Translation.GetText("O_V_cB_showToolTip");
            lbl_toolTipDelay.Text = Translation.GetText("O_V_lbl_toolTipDelay");
            gB_graphics.Text = Translation.GetText("O_V_gB_graphics");
            checkB_showGesture.Text = Translation.GetText("O_V_cB_showGesture");
            lbl_color.Text = Translation.GetText("O_V_lbl_color");
            lbl_width.Text = Translation.GetText("O_V_lbl_width");            
        }

        public UC_visualisation()
        {
            InitializeComponent();
            m_name = new MyText("O_V_name");
            m_caption = new MyText("O_V_caption");
            I_infoText = new MyText("O_V_info");

            nUD_toolTipDelay.Minimum = TOOL_TIP_DELAY_MIN;
            nUD_toolTipDelay.Maximum = TOOL_TIP_DELAY_MAX;

            typeof(ComboBox).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, cB_gestureColor, new object[] { true });
            typeof(ComboBox).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, cB_gestureWidth, new object[] { true });

            
            this.Dock = DockStyle.Fill;

            for (int i = 0; i < Enum.GetValues(typeof(eColors)).Length; i++)
                cB_gestureColor.Items.Add("");

            for (int i = 0; i < WIDTH_COUNT; i++)
                cB_gestureWidth.Items.Add("");

            checkB_showToolTip.Checked = Config.User.MhShowToolTip;
            nUD_toolTipDelay.Value = Math.Min(Math.Max(Config.User.MhToolTipDelay, nUD_toolTipDelay.Minimum), nUD_toolTipDelay.Maximum); 
            checkB_showGesture.Checked = Config.User.DisplayGesture;
            cB_gestureColor.SelectedIndex = (int)Enum.Parse(typeof(eColors), Config.User.PenColor.Name);
            cB_gestureWidth.SelectedIndex = Config.User.PenWidth - 1;
            OnEnableApply(false);
        }

        public override void SaveSettings()
        {
            Config.User.MhShowToolTip = checkB_showToolTip.Checked;
            Config.User.MhToolTipDelay = (int)nUD_toolTipDelay.Value;
            Config.User.DisplayGesture = checkB_showGesture.Checked;
            Config.User.PenColor = Color.FromName((string)Enum.GetName(typeof(eColors), cB_gestureColor.SelectedIndex));
            Config.User.PenWidth = cB_gestureWidth.SelectedIndex + 1;            
        }

        private void checkB_showGesture_CheckedChanged(object sender, EventArgs e)
        {
            OnEnableApply(true);
        }

        private void comboBox_selectedIndexChanged(object sender, EventArgs e)
        {
            OnEnableApply(true);
        }

        private void cB_gestureColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            Rectangle r = e.Bounds;

            if (e.Index >= 0)
            {
                e.Graphics.FillRectangle(new SolidBrush(cB_gestureColor.BackColor), e.Bounds);
                Rectangle rColor =  new Rectangle(r.Left, r.Top, r.Width, r.Height);
                rColor.Width = 100;

                Rectangle rName = new Rectangle(rColor.Right, r.Top, r.Width - rColor.Width, r.Height);

                // Get the brush object, at the specifid index in the colorArray
                SolidBrush brush = new SolidBrush(Color.FromName(Enum.GetName(typeof(eColors), e.Index)));
                // Fill a portion of the rectangle with the selected brush
                e.Graphics.FillRectangle(brush, rColor);

                //// Set the string format options
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                // Draw the rectangle
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 2), rColor);
                e.Graphics.DrawString(brush.Color.Name, SystemFonts.DefaultFont, new SolidBrush(Color.Black), rName, sf);
                Debug.Write("cB_gestureColor_DrawItem " + e.Index.ToString() + " ");
                Debug.WriteLine(e.State);
                if (e.State == (DrawItemState.NoAccelerator) ||
                    e.State == (DrawItemState.NoAccelerator | DrawItemState.NoFocusRect) ||
                    e.State == (DrawItemState.NoAccelerator | DrawItemState.NoFocusRect | DrawItemState.ComboBoxEdit) ||
                    e.State == (DrawItemState.NoAccelerator | DrawItemState.NoFocusRect | DrawItemState.ComboBoxEdit | DrawItemState.Disabled))
                {
                    Debug.WriteLine("_INACTIVE_ PART");
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 2), r);
                    e.DrawFocusRectangle();
                }
                else
                {
                    Debug.WriteLine("#ACTIVE# PART");
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Blue), 2), r);
                    e.DrawFocusRectangle();
                }

              
            }
        }

        private void cB_gestureWidth_DrawItem(object sender, DrawItemEventArgs e)
        {
            Rectangle r = e.Bounds;

            if (e.Index >= 0)
            {
                e.Graphics.FillRectangle(new SolidBrush(cB_gestureWidth.BackColor), e.Bounds);
                e.Graphics.DrawRectangle(new Pen(cB_gestureWidth.BackColor, 2), r);

                int gestureWidth = e.Index + 1;
                int top = r.Top + (r.Height - gestureWidth) / 2;
                Rectangle rWidth = new Rectangle(r.Left + 2, top, 100, gestureWidth);
                Rectangle rName = new Rectangle(rWidth.Right, r.Top, r.Width - rWidth.Width, r.Height);

                e.Graphics.FillRectangle(Brushes.Black, rWidth);

                // Set the string format options
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                e.Graphics.DrawString(string.Format("{0} px", gestureWidth), SystemFonts.DefaultFont, new SolidBrush(Color.Black), rName, sf);


                if (e.State != (DrawItemState.NoAccelerator) &&
                    e.State != (DrawItemState.NoAccelerator | DrawItemState.NoFocusRect) &&
                    e.State != (DrawItemState.NoAccelerator | DrawItemState.NoFocusRect | DrawItemState.ComboBoxEdit) &&
                    e.State != (DrawItemState.NoAccelerator | DrawItemState.NoFocusRect | DrawItemState.ComboBoxEdit | DrawItemState.Disabled))
                {
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Blue), 2), r);
                    e.DrawFocusRectangle();
                }
            }
        }

        private void checkB_showToolTip_CheckedChanged(object sender, EventArgs e)
        {
            OnEnableApply(true);
        }

        private void nUD_toolTipDelay_ValueChanged(object sender, EventArgs e)
        {
            OnEnableApply(true);
        }       

    }
}
