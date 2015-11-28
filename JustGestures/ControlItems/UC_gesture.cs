using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Threading;
using System.Diagnostics;
using NeuralNetwork;
using NeuralNetwork.ActivationFunctions;
using NeuralNetwork.Learning;
using JustGestures.Properties;
using JustGestures.GestureParts;
using JustGestures.Languages;

namespace JustGestures.ControlItems
{
    public partial class UC_gesture : BaseActionControl
    {
        UC_TP_baseActivator m_activator;
        MyNeuralNetwork m_networkBackup;
        
        public MyNeuralNetwork MyNNetwork 
        {
            get 
            {
                if (tabControl_invokeAction.SelectedTab == tP_classicCurves)
                    return m_tpClassicCurve.MyNNetwork;
                else
                    return m_networkBackup; 
            }
            set 
            {
                m_networkBackup = new MyNeuralNetwork(value);
                m_tpClassicCurve.MyNNetwork = value; 
            } 
        }

        public MyNeuralNetwork MyNNetworkOriginal
        {
            set { m_tpClassicCurve.MyNNetworkOriginal = value; }
        }
        
        public UC_gesture()
        {
            InitializeComponent();
            m_identifier = Page.Gesture;
            m_previous = Page.Action; //default
            m_next = Page.Name;

            tP_classicCurves.Text = Translation.GetText("C_Gestures_tP_curveG");
            tP_rockerGesture.Text = Translation.GetText("C_Gestures_tP_rockerG");
            tP_wheelGesture.Text = Translation.GetText("C_Gestures_tP_wheelG");
        }

     
        private void UserControl_gesture_Load(object sender, EventArgs e)
        {
            List<UC_TP_baseActivator> activatorItems = new List<UC_TP_baseActivator>();
            activatorItems.Add(m_tpClassicCurve);
            activatorItems.Add(m_tPdoubleBtn);
            activatorItems.Add(m_tpWheelButton);
            tabControl_invokeAction.TabPages.Clear();            
           
            foreach (UC_TP_baseActivator activator in activatorItems)
            {
                activator.CanContinue += new DlgCanContinue(CanContinue);
                if (ChangeAboutText != null)
                    activator.ChangeAboutText += new DlgChangeAboutText(ChangeAboutText);
                activator.ChangeInfoText += new DlgChangeInfoText(ChangeInfoText);
                activator.PB_Display = pb_Display;
                activator.Gestures = m_gesturesCollection;
                activator.TempGesture = m_tempGesture;
                activator.Initialize();
            }
            SetTabPages();
            if (m_tempGesture.Activator != null)
            {
                if (m_tempGesture.Activator.Type == MouseActivator.Types.ClassicCurve)
                {
                    m_activator = m_tpClassicCurve;
                    tabControl_invokeAction.SelectedTab = tP_classicCurves;
                }
                else if (m_tempGesture.Activator.Type == MouseActivator.Types.DoubleButton)
                {
                    m_activator = m_tPdoubleBtn;
                    tabControl_invokeAction.SelectedTab = tP_rockerGesture;
                }
                else if (m_tempGesture.Activator.Type == BaseActivator.Types.WheelButton)
                {
                    m_activator = m_tpWheelButton;
                    tabControl_invokeAction.SelectedTab = tP_wheelGesture;
                }
            }
            else
            {
                if (!m_tempGesture.Action.IsExtras())//.Category != TypeOfAction.ExtrasControl.NAME)
                {
                    m_activator = m_tpClassicCurve;
                    tabControl_invokeAction.SelectedTab = tP_classicCurves;
                }
                else
                {
                    m_activator = m_tpWheelButton;
                    tabControl_invokeAction.SelectedTab = tP_wheelGesture;
                }
            }
            OnCanContinue(false);
        }
    
        private void panel_recognizedGesture_MouseDown(object sender, MouseEventArgs e)
        {
            if (pb_Display.Image == null)
            {
                ((UC_TP_baseActivator)m_tpClassicCurve).InitializePictureBox();
                ((UC_TP_baseActivator)m_tPdoubleBtn).InitializePictureBox();
                ((UC_TP_baseActivator)m_tpWheelButton).InitializePictureBox();
            }
            m_activator.Display_MouseDown(sender, e);
        }

        private void panel_recognizedGesture_MouseMove(object sender, MouseEventArgs e)
        {
            m_activator.Display_MouseMove(sender, e);
        }

        private void panel_recognizedGesture_MouseUp(object sender, MouseEventArgs e)
        {
            m_activator.Display_MouseUp(sender, e);
        }

        private void panel_recognizedGesture_MouseClick(object sender, MouseEventArgs e)
        {
            m_activator.Display_MouseClick(sender, e);
        }

        private void SetTabPages()
        {
            if (!m_tempGesture.Action.IsExtras()) //.Category != TypeOfAction.ExtrasControl.NAME)
            {
                if (!tabControl_invokeAction.TabPages.Contains(tP_classicCurves))
                    tabControl_invokeAction.TabPages.Add(tP_classicCurves);
                if (!tabControl_invokeAction.TabPages.Contains(tP_rockerGesture))
                    tabControl_invokeAction.TabPages.Add(tP_rockerGesture);
                if (tabControl_invokeAction.TabPages.Contains(tP_wheelGesture))
                    tabControl_invokeAction.TabPages.Remove(tP_wheelGesture);
            }
            else
            {
                if (!tabControl_invokeAction.TabPages.Contains(tP_wheelGesture))
                    tabControl_invokeAction.TabPages.Add(tP_wheelGesture);
                if (tabControl_invokeAction.TabPages.Contains(tP_classicCurves))
                    tabControl_invokeAction.TabPages.Remove(tP_classicCurves);
                if (tabControl_invokeAction.TabPages.Contains(tP_rockerGesture))
                    tabControl_invokeAction.TabPages.Remove(tP_rockerGesture);
            }

        }
     
        private void UC_gesture_VisibleChanged(object sender, EventArgs e)
        {
            if (m_tempGesture != null && m_tempGesture.Activator != null)
                m_tempGesture.Activator.AbortAnimating();
            if (((UC_gesture)sender).Visible)
            {                
                SetTabPages();
                m_activator.SetValues();
            }
        }

        public void OnClosingForm(DialogResult result)
        {
            if (m_tempGesture != null && m_tempGesture.Activator != null)
                m_tempGesture.Activator.AbortAnimating();
            if (result != DialogResult.OK && m_tpClassicCurve.MyNNetwork != null)
                m_tpClassicCurve.MyNNetwork.StopLearning();
        }
           
        private void tabControl_invokeAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_tempGesture != null && m_tempGesture.Activator != null)
                m_tempGesture.Activator.AbortAnimating();
            if (tabControl_invokeAction.SelectedTab == tP_classicCurves)
                m_activator = m_tpClassicCurve;
            else if (tabControl_invokeAction.SelectedTab == tP_rockerGesture)
                m_activator = m_tPdoubleBtn;
            else if (tabControl_invokeAction.SelectedTab == tP_wheelGesture)
                m_activator = m_tpWheelButton;
            if (m_activator != null)
                m_activator.SetValues();
        }

        private void pb_Display_SizeChanged(object sender, EventArgs e)
        {
            if (m_activator != null)
                m_activator.RedrawDisplay();
        }

        private void tabControl_invokeAction_DrawItem(object sender, DrawItemEventArgs e)
        {
            int i = 0;
         
        }

       
    }
}
