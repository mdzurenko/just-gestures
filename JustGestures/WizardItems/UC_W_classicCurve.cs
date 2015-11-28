using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JustGestures.Languages;

namespace JustGestures.WizardItems
{
    public partial class UC_W_classicCurve : UC_W_baseGesture
    {
        public UC_W_classicCurve()
        {
            InitializeComponent();

            m_identifier = Page.ClassicCurve;
            m_next = Page.DoubleBtn;
            m_previous = Page.Welcome;

            List<PointF> points = new List<PointF>();
            //Curve G
            points.AddRange(new PointF[] { new PointF(253, 100), new PointF(252, 99), new PointF(249, 98), new PointF(247, 98), new PointF(243, 98), 
                new PointF(241, 98), new PointF(238, 98), new PointF(235, 98), new PointF(230, 98), new PointF(227, 99), new PointF(223, 100), new PointF(218, 101), 
                new PointF(215, 101), new PointF(215, 101), new PointF(212, 102), new PointF(208, 103), new PointF(205, 103), new PointF(200, 104), new PointF(196, 106), 
                new PointF(191, 108), new PointF(187, 110), new PointF(182, 111), new PointF(177, 113), new PointF(171, 115), new PointF(165, 119), new PointF(159, 123), 
                new PointF(156, 125), new PointF(149, 129), new PointF(145, 132), new PointF(141, 135), new PointF(136, 140), new PointF(132, 144), new PointF(130, 147), 
                new PointF(126, 152), new PointF(123, 157), new PointF(121, 161), new PointF(119, 166), new PointF(117, 171), new PointF(115, 177), new PointF(114, 182), 
                new PointF(114, 187), new PointF(113, 194), new PointF(113, 198), new PointF(113, 203), new PointF(113, 209), new PointF(113, 215), new PointF(114, 219), 
                new PointF(116, 225), new PointF(118, 230), new PointF(120, 236), new PointF(122, 240), new PointF(125, 245), new PointF(128, 250), new PointF(132, 255), 
                new PointF(134, 258), new PointF(139, 263), new PointF(144, 267), new PointF(148, 270), new PointF(157, 275), new PointF(163, 280), new PointF(170, 283), 
                new PointF(177, 286), new PointF(183, 288), new PointF(191, 291), new PointF(199, 292), new PointF(206, 293), new PointF(214, 293), new PointF(221, 293), 
                new PointF(229, 293), new PointF(236, 293), new PointF(243, 293), new PointF(250, 293), new PointF(257, 292), new PointF(263, 291), new PointF(270, 289), 
                new PointF(275, 287), new PointF(281, 284), new PointF(284, 282), new PointF(290, 278), new PointF(295, 275), new PointF(300, 271), new PointF(304, 268), 
                new PointF(308, 263), new PointF(311, 259), new PointF(315, 253), new PointF(318, 249), new PointF(321, 245), new PointF(322, 241), new PointF(323, 238), 
                new PointF(323, 235), new PointF(323, 232), new PointF(323, 229), new PointF(323, 226), new PointF(323, 223), new PointF(322, 220), new PointF(320, 217), 
                new PointF(319, 213), new PointF(317, 210), new PointF(315, 207), new PointF(312, 204), new PointF(310, 201), new PointF(306, 199), new PointF(303, 196), 
                new PointF(299, 194), new PointF(296, 193), new PointF(290, 191), new PointF(285, 190), new PointF(280, 189), new PointF(275, 188), new PointF(268, 188), 
                new PointF(261, 188), new PointF(255, 188), new PointF(249, 188), new PointF(243, 188), new PointF(239, 188), new PointF(235, 188), new PointF(231, 188), 
                new PointF(228, 188), new PointF(224, 188), new PointF(220, 190), new PointF(218, 191), new PointF(216, 192), new PointF(215, 193), new PointF(213, 195), 
                new PointF(212, 197), new PointF(210, 199), new PointF(210, 201), new PointF(209, 203), new PointF(209, 205), new PointF(208, 208), new PointF(208, 210), 
                new PointF(207, 213), new PointF(207, 216), new PointF(207, 218), new PointF(207, 220)});

            m_gesture = new MyGesture("ClassicCurve");
            m_gesture.Activator = new GestureParts.ClassicCurve(points, m_gesturesCollection);

            cB_gesture.Checked = Config.User.UsingClassicCurve;

            for (int i = 0; i < ConvertValue.MouseButtonsCount; i++)
                cB_toggleBtn.Items.Add(Translation.GetMouseBtnText(ConvertValue.IndexToMouseBtn(i)));

            cB_toggleBtn.SelectedIndex = ConvertValue.MouseBtnToIndex(Config.User.BtnToggle);

            cB_toggleExample.Items.Add(Translation.Name_RightButton);
            cB_toggleExample.SelectedIndex = 0;

            m_caption = Translation.GetText("W_CC_caption");
            I_infoText = Translation.GetText("W_CC_info");
            cB_gesture.Text = Translation.Name_CurveGesture;
            lbl_toggleBtn.Text = lbl_toggleBtn2.Text = Translation.Name_CurveHoldDownBtn;
            rTB_instructions.AppendText(Translation.GetText("W_CC_instructions"));
            lbl_and.Text = Translation.GetText("W_G_lbl_and");
            lbl_mouseMovement.Text = Translation.GetText("W_G_lbl_mouseMovement");
            rTB_description.AppendText("\n" + Translation.GetText("W_CC_description") + "\n");
        }

        public override void SaveSettings()
        {
            Config.User.UsingClassicCurve = cB_gesture.Checked;
            Config.User.BtnToggle = ConvertValue.IndexToMouseBtn(cB_toggleBtn.SelectedIndex);
        }
    }
}
