using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using MouseCore;
using JustGestures.GestureParts;
using JustGestures.Properties;
using JustGestures.TypeOfAction;
using JustGestures.Features;
using JustGestures.Languages;

namespace JustGestures
{
    public class MyEngine : ITranslation
    {
        public enum AppState
        {
            Normal,
            Learning,
            Autobehave
        }
        GUI.Form_transparent form_top;                
        
        GesturesCollection learntGestures;
        static ExtraMouseHook m_mouse;
        MyCurve m_curve;
        MyGesture m_execute;
        MyNeuralNetwork m_network;
        ImageList m_imgListActions;
        
        System.Windows.Forms.Timer m_iconTimer;
        bool m_appIsActive = false;
        bool m_showTraining = false;
        AppState m_realState = AppState.Normal;

        MouseButtons m_toggleBtn;
        /// <summary>
        /// Indicates that toggle button was pushed and mouse move event should be passed to form_transparent
        /// </summary>
        bool m_toggleBtnPushed;
        /// <summary>
        /// Left mouse button was pushed on CmsMatchedGestures and up event should be processed
        /// </summary>
        bool m_leftMouseDownOnContextMenu = false;
        /// <summary>
        /// Right mouse button was pushed on CmsMatchedGestures and up event should be processed
        /// </summary>
        bool m_rightMouseDownOnContextMenu = false;
        
        bool m_wheelBtnUsed = false;
        IntPtr m_hwndForeWnd;
        IntPtr m_hwndWindowToUse;
        Point m_cursorPosition;
        AutoBehave autoBehave;

        GesturesCollection m_dblBtnGesturesToExecute = null;
        int m_threadID = -1;

        #region ITranslation Members
        MyText Unknown_gesture = new MyText("Unknown_gesture");

        public void Translate()
        {
            Unknown_gesture.Translate();
        }

        #endregion

        delegate void EmptyDelegate();
        
        public static ExtraMouseHook MouseEngine 
        { 
            get { return m_mouse; } 
        }

        public List<PrgNamePath> FinalList { set { autoBehave.FinalList = value; } }
        public GesturesCollection LearntGestures { set { learntGestures = value; } }
        public MyNeuralNetwork Network 
        { 
            get { return m_network; } 
            set 
            { 
                m_network = value;
                m_network.NetworkStartTraining += new MyNeuralNetwork.DlgEmpty(NetworkStartTraining);
                m_network.NetworkLearnt += new MyNeuralNetwork.DlgNetworkLearnt(NetworkLearnt);
                if (m_network.IsTraining)
                {
                    m_realState = AppState.Learning;
                    m_iconTimer.Start();
                }
            } 
        }

        public ImageList ImgListActions 
        { 
            get { return m_imgListActions; } 
            set { m_imgListActions = value; } 
        }

        public delegate void DlgAppStateChanged(bool active, AppState state, AppState realState);
        public DlgAppStateChanged AppStateChanged;

        private void OnAppStateChanged(bool active, AppState state, AppState realState)
        {
            if (AppStateChanged != null)
                AppStateChanged(active, state, realState);
        }
     
        public MyEngine()
        {
            m_iconTimer = new System.Windows.Forms.Timer();
            m_iconTimer.Tick += new EventHandler(m_iconTimer_Tick);
            m_iconTimer.Interval = 500;
        
            form_top = new GUI.Form_transparent();
            form_top.CmsMatchedGestures.ItemClicked += new ToolStripItemClickedEventHandler(cMS_MatchedGestures_ItemClicked);
            
            m_network = new MyNeuralNetwork();
            m_network.NetworkStartTraining += new MyNeuralNetwork.DlgEmpty(NetworkStartTraining);
            m_network.NetworkLearnt += new MyNeuralNetwork.DlgNetworkLearnt(NetworkLearnt);

            m_mouse = new ExtraMouseHook();
            m_mouse.MouseClick += new MouseEventHandler(MyMouse_MouseClick);
            m_mouse.MouseDown += new MouseEventHandler(MyMouse_MouseDown);
            m_mouse.MouseDownGesture += new MouseEventHandler(MyMouse_MouseDownGesture);
            m_mouse.MouseUp += new MouseEventHandler(MyMouse_MouseUp);
            m_mouse.MouseMove += new MouseEventHandler(MyMouse_MouseMove);
            m_mouse.MouseStoped += new EventHandler(MyMouse_MouseStoped);
            m_mouse.HookStateChanged += new ExtraMouseHook.DlgHookStateChanged(HookStateChanged);
            m_mouse.WheelBtnAction += new ExtraMouseHook.DlgWheelBtnAction(MyMouse_WheelBtnAction);
            m_mouse.DoubleBtnAction += new ExtraMouseHook.DlgDoubleBtnAction(MyMouse_DoubleBtnAction);
            //m_mouse.MouseBrowseWheel += new MouseHook.DlgMouseWheelMove(MyMouse_MouseBrowseWheel);

            autoBehave = new AutoBehave(form_top.Handle);
            autoBehave.DisableMouse += new AutoBehave.DlgDisableMouse(DisableMouse);
        }

        public void Load()
        {
            // we have to call this method first so the the form will be shown
            form_top.Show();
        }

        public void ResizeTopWindow()
        {
            form_top.ResizeWindow();
        }


        #region Settings and moduls connection

        public void SetImageList()
        {
            m_imgListActions = new ImageList();
            m_imgListActions.ColorDepth = ColorDepth.Depth32Bit;
            m_imgListActions.ImageSize = new Size(20, 20);
            m_imgListActions.TransparentColor = Color.Transparent;
            foreach (MyGesture gesture in learntGestures.GetAll())
                gesture.SetActionIcon(m_imgListActions);
        }

        public void ApplySettings()
        {
            SetMouseHookValues();
            m_toggleBtn = Config.User.BtnToggle;
            form_top.ApplySettings();
            //autoBehave.FinalList = finalList;
            if (autoBehave.IsRunning) autoBehave.Start();
        }

        private void SetMouseHookValues()
        {
            bool classicCurve = Config.User.UsingClassicCurve;
            bool doubleBtn = Config.User.UsingDoubleBtn;
            bool wheelBtn = Config.User.UsingWheelBtn;
            int sensibleZone = Config.User.SensitiveZone;
            int timeout = Config.User.DeactivationTimeout;
            bool showTooltip = Config.User.MhShowToolTip;
            int toolTipInt = Config.User.MhToolTipDelay;
            MouseButtons toggleBtn = Config.User.BtnToggle;

            m_mouse.SetHookValues(toggleBtn, classicCurve, doubleBtn, wheelBtn, sensibleZone,
                timeout, showTooltip, toolTipInt);
        }

        private bool MouseInstalled { get { return m_mouse.IsInstalled; } }
        private void StartMouse() { if (!autoBehave.IsRunning) m_mouse.Install(); }
        private void StopMouse() { m_mouse.Uninstall(); }
        
        public void ManualInstall() 
        { 
            m_mouse.ForceInstall();
            form_top.ShowForm();
        }
        public void ManualUninstall() 
        {
            form_top.HideForm();
            m_mouse.ForceUninstall();
        }
        public bool IsRunningAutoBehave { get { return autoBehave.IsRunning; } }

        public void StartAutoBehave() 
        { 
            autoBehave.Start();
            if (m_realState != AppState.Learning) m_realState = AppState.Autobehave;
            SetIconState(); 
        }

        public void StopAutoBehave() 
        { 
            autoBehave.Stop();
            if (m_realState != AppState.Learning) m_realState = AppState.Normal;
            SetIconState(); 
        }

        private void NetworkStartTraining()
        {         
            m_iconTimer.Stop();
            m_iconTimer.Start();
            m_showTraining = false;
            m_realState = AppState.Learning;
        }

        private void NetworkLearnt(double error, int epochs)
        {            
            m_iconTimer.Stop();
            if (epochs == MyNeuralNetwork.TRAINING_INTERRUPTED) return; //disable more icon change notifycations
            m_showTraining = false;
            m_realState = autoBehave.IsRunning ? AppState.Autobehave : AppState.Normal;
            SetIconState();
        }

        private void m_iconTimer_Tick(object sender, EventArgs e)
        {
            m_showTraining = !m_showTraining;
            SetIconState();
        }

        private void HookStateChanged(bool active)
        {
            Debug.WriteLine("HookStateChanged " + active.ToString());
            m_appIsActive = active;
            SetIconState();
        }

        private void SetIconState()
        {
            AppState state = AppState.Normal;
            if (m_network.IsTraining && m_showTraining) state = AppState.Learning;
            else if (autoBehave.IsRunning) state = AppState.Autobehave;
            OnAppStateChanged(m_appIsActive, state, m_realState);
        }
      

        private void DisableMouse(bool disable)
        {
            if (disable)
            {
                if (m_mouse.IsInstalled)
                {
                    bool tooglePushed = m_toggleBtnPushed;
                    m_toggleBtnPushed = false;
                    m_mouse.Uninstall();
                    if (tooglePushed && form_top.Points.Count != 0)
                    {
                        form_top.RefreshAndStopDrawing();
                    }
                }
            }
            else
            {
                if (!m_mouse.IsInstalled) m_mouse.Install();
            }
        }

        #endregion Settings and moduls connection

        #region Mouse Events

        //void MyMouse_MouseBrowseWheel(ExtraMouseHook.WheelAction wheelAction)
        //{
        //    switch (wheelAction)
        //    {
        //        case WheelAction.Down:
        //            m_mouse.SimulateMouseAction(MouseAction.LeftDoubleClick);
        //            break;
        //        case WheelAction.Up:
        //            KeyInput.ExecuteKeyInput(AllKeys.KEY_BACK);
        //            break;
        //    }
        //}

        void MyMouse_DoubleBtnAction(MouseEventArgs trigger, MouseEventArgs modifier, bool notifyOnly)
        {            
            Debug.WriteLine(string.Format("MyEngine: DoubleBtnAction {0}/{1} NotifyOnly: {2}", trigger.Button, modifier.Button, notifyOnly));
            form_top.CloseContextMenu();
            m_cursorPosition = modifier.Location;
            m_hwndForeWnd = Win32.GetForegroundWindow();
            //m_hwndWndToUse = GetWindowToUse(modifier.Location);            

            string name = DoubleButton.GetName(trigger.Button, modifier.Button);
 
            if (notifyOnly)
                ShowToolTip(name);
            else
            {
                //form_top.CloseContextMenu();
                GesturesCollection recognizedGestures = GetRecognizedGestures(name);                
                if (recognizedGestures.Count == 0) return;

                if (recognizedGestures.Count == 1 && recognizedGestures[0].IsImplicitOnly)
                {
                    MyGesture gestToExecute = recognizedGestures[0];
                    ExecuteImplicitOnlyAction(gestToExecute.Action, KeystrokesOptions.MouseAction.ModifierClick, true);
                }
                else
                {
                    m_dblBtnGesturesToExecute = recognizedGestures;                    
                    //ExecuteDblBtnGestures();
                    Thread threadExecuteDblBtnGestures = new Thread(new ThreadStart(ExecuteDblBtnGestures));
                    threadExecuteDblBtnGestures.Start();
                }
            }
        }

        private void ExecuteDblBtnGestures()
        {
            m_threadID = Thread.CurrentThread.ManagedThreadId;
            ExecuteRecognizedGestures(m_dblBtnGesturesToExecute);
        }


        void MyMouse_WheelBtnAction(MouseEventArgs trigger, ExtraMouseActions action)
        {
            Debug.WriteLine(string.Format("MyEngine: WheelBtnAction Trigger: {0} Action: {1}", trigger.Button, action));
            if (!m_wheelBtnUsed)
                form_top.CloseContextMenu();
            m_cursorPosition = trigger.Location;
            m_hwndForeWnd = Win32.GetForegroundWindow();
            //m_hwndWndToUse = GetWindowToUse(trigger.Location);            

            string name = WheelButton.GetName(trigger.Button);

            GesturesCollection recognizedGestures = GetRecognizedGestures(name);
            if (recognizedGestures.Count == 0) return;
            MyGesture gestToExecute = recognizedGestures[0];

            if (!m_wheelBtnUsed)
            {
                m_wheelBtnUsed = true;
                ExecuteImplicitOnlyAction(gestToExecute.Action, KeystrokesOptions.MouseAction.TriggerDown, true);
            }
            switch (action)
            {
                case ExtraMouseActions.WheelDown:
                    ExecuteImplicitOnlyAction(gestToExecute.Action, KeystrokesOptions.MouseAction.WheelDown, false);                   
                    break;
                case ExtraMouseActions.WheelUp:
                    ExecuteImplicitOnlyAction(gestToExecute.Action, KeystrokesOptions.MouseAction.WheelUp, false);
                    break;
                case ExtraMouseActions.None:
                    ExecuteImplicitOnlyAction(gestToExecute.Action, KeystrokesOptions.MouseAction.TriggerUp, false);
                    m_wheelBtnUsed = false;
                    break;
            }

        }

        private void ExecuteImplicitOnlyAction(BaseActionClass action, KeystrokesOptions.MouseAction mouse, bool selectWnd)
        {
            Debug.WriteLine(string.Format("ExecuteImplicitOnlyAction - Action: {0}, Mouse: {1}, SelectWindow: {2} Handle WndUnderCursor: {3}", action.Name, mouse, selectWnd, Config.User.HandleWndUnderCursor));
            IntPtr hwnd = m_hwndWindowToUse;//GetWindowToUse(m_cursorPosition);
            //if (Config.User.handleWndUnderCursor)
            //    hwnd = m_mouse.ActiveWindow;
            //else
            //    hwnd = m_hwndWndToUse;
            Point location = new Point(m_cursorPosition.X, m_cursorPosition.Y);
            lock (this)
            {
                m_mouse.StartSimulation();
                action.ExecuteKeyScript(mouse, hwnd, selectWnd, location);
                m_mouse.StopSimulation();
            }
        }

        void MyMouse_MouseClick(object sender, MouseEventArgs e)
        {
            Debug.WriteLine(string.Format("MyEngine: -{0}- Mouse button -CLICK- at X: {1} , Y: {2}", e.Button.ToString().ToUpper(), e.X, e.Y));
            IntPtr hwnd = Win32.WindowFromPoint(Cursor.Position);
            if (form_top.IsContextMenu(hwnd))
            {
                // click on context menu only if it is Left or Right button
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
                {
                    form_top.PerformClickOnContextMenu();
                    ((ExtraMouseHook)sender).CancelEvent = true;
                }
                return;
            }
            form_top.CloseContextMenuAsync();
            
        }

        /// <summary>
        /// Method called when mouse is down and moved within the timeout => curve gesture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MyMouse_MouseDownGesture(object sender, MouseEventArgs e)
        {
            // currently this method is called only for right mouse button

            Debug.WriteLine(string.Format("MyEngine: -{0}- Mouse button -GESTURE DOWN- at X: {1} , Y: {2}", e.Button.ToString().ToUpper(), e.X, e.Y));
            
            if (e.Button == m_toggleBtn && Config.User.UsingClassicCurve)
            {
                form_top.CloseContextMenuAsync();

                m_cursorPosition = e.Location;
                m_hwndForeWnd = Win32.GetForegroundWindow();

                //m_hwndWndToUse = GetWindowToUse(e.Location);
                m_toggleBtnPushed = true;
                form_top.Mouse_ToggleButtonDown(sender, e);
                m_curve = new MyCurve();
            }
        }

        /// <summary>
        /// Method called when mouse is down and timeout is reached => normal mouse behaviour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MyMouse_MouseDown(object sender, MouseEventArgs e)
        {
            Debug.WriteLine(string.Format("MyEngine: -{0}- Mouse button -DOWN- at X: {1} , Y: {2}", e.Button.ToString().ToUpper(), e.X, e.Y));

            IntPtr hwnd = Win32.WindowFromPoint(Cursor.Position);
            if (form_top.IsContextMenu(hwnd))
            {
                // check only for left and right button
                if (e.Button == MouseButtons.Left)
                {
                    m_leftMouseDownOnContextMenu = true;
                    ((ExtraMouseHook)sender).CancelEvent = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    m_rightMouseDownOnContextMenu = true;
                    ((ExtraMouseHook)sender).CancelEvent = true;
                }
                return;
            }
            form_top.CloseContextMenuAsync();

        }

        void MyMouse_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_toggleBtnPushed)
            {
                form_top.Mouse_Move(sender, e);
            }
        }

        void MyMouse_MouseUp(object sender, MouseEventArgs e)
        {
            Debug.WriteLine(string.Format("MyEngine: -{0}- Mouse button -UP- at X: {1} , Y: {2}", e.Button.ToString().ToUpper(), e.X, e.Y));
            if (e.Button == m_toggleBtn && m_toggleBtnPushed)
            {
                m_toggleBtnPushed = false;
                form_top.Mouse_ToggleButtonUp(sender, e);
                if (form_top.Points.Count <= 1) return;
                m_curve = new MyCurve(form_top.Points, false);
                
                Thread threadAnalyzeGesture = new Thread(new ThreadStart(StartAnalyze));
                threadAnalyzeGesture.Name = "Analyze Gesture";
                threadAnalyzeGesture.Start();
                //StartAnalyze();
            }
            else
            {
                IntPtr hwnd = Win32.WindowFromPoint(Cursor.Position);
                
                // click on context menu only if it is Left or Right button
                if (m_leftMouseDownOnContextMenu && e.Button == MouseButtons.Left)
                {
                    if (form_top.IsContextMenu(hwnd))
                        form_top.PerformClickOnContextMenu();
                    m_leftMouseDownOnContextMenu = false;
                    ((ExtraMouseHook)sender).CancelEvent = true;
                } 
                else if (m_rightMouseDownOnContextMenu && e.Button == MouseButtons.Right)
                {
                    if (form_top.IsContextMenu(hwnd))
                        form_top.PerformClickOnContextMenu();
                    m_rightMouseDownOnContextMenu = false;
                    ((ExtraMouseHook)sender).CancelEvent = true;
                }
            }
        }

        void MyMouse_MouseStoped(object sender, EventArgs e)
        {
            if (form_top.Points.Count <= 1) return;
            m_curve = new MyCurve(form_top.Points, false);
            int approxPoints = Config.User.NnInputSize / 2 + 1;
            double[] nnInput = m_curve.GetScaledInput();
            if (nnInput == null)
            {
                form_top.ShowToolTip(Unknown_gesture);
                return;
            }
            double divergence;
            string nn_name = m_network.RecognizeCurve(nnInput, out divergence);
            ShowToolTip(nn_name);
        }

        private void ShowToolTip(string name)
        {
            GesturesCollection recognizedGestures = GetRecognizedGestures(name);

            string msg = string.Empty;
            foreach (MyGesture ga in recognizedGestures)
            {
                //if (ga.ExecutionType == ExecuteType.Implicit)
                //{  
                //    msg = ga.Caption + "\n";
                //    break;
                //}
                msg += (ga.Caption + "\n");
            }
            if (msg == string.Empty) msg = Unknown_gesture;
            else msg = msg.Substring(0, msg.Length - 1);
            form_top.ShowToolTip(msg);
        }

        #endregion Mouse Events

        #region Analyze & Execute

        /// <summary>
        /// Analyze the drawn curve and find associated learnt one
        /// </summary>
        private void StartAnalyze()
        {
            //form_top.MakeNonTopMost();
            //if (cMS_MatchedGestures.Visible)
            //    form_top.CloseAndClearContextMenu(cMS_MatchedGestures);
     
            Debug.WriteLine("Thread StartAnalyze started");
            int approxPoints = Config.User.NnInputSize / 2 + 1;
            double[] nnInput = null;
            if (m_curve == null) return;
            else
            {
                //Get scaled input cannot last to long otherwise might be modified with new curve
                try { nnInput = m_curve.GetScaledInput(); }
                catch (Exception ex) { nnInput = null; } 
            }
            m_curve = null;
            if (nnInput == null) return;

            double divergence;
            string nn_name = m_network.RecognizeCurve(nnInput, out divergence);

            m_threadID = Thread.CurrentThread.ManagedThreadId;
            GesturesCollection recognizedGestures = GetRecognizedGestures(nn_name);
            ExecuteRecognizedGestures(recognizedGestures);           
        }
   
        private void ExecuteRecognizedGestures(GesturesCollection recognizedGestures)
        {
            //form_top.ClearContextMenu();
            int threadID = Thread.CurrentThread.ManagedThreadId;
            int index = 0;
            MyGesture gest;

            switch (recognizedGestures.Count)
            {
                case 0:
                    // if curve wasnt recognize then hide form_transparent
                    //form_top.MakeNonTopMost();
                    break;
                case 1:
                    gest = recognizedGestures[index];
                    if (gest.ExecutionType == ExecuteType.Implicit || gest.ExecutionType == ExecuteType.ImplicitIfUnique)
                    {
                        Debug.WriteLine("Execute : " + gest.ID);
                        switch (gest.Action.Name)
                        {
                            case TypeOfAction.SpecialOptions.SPECIAL_ZOOM:
                                //int area_width = Math.Abs(area_right - area_left);
                                //int area_height = Math.Abs(area_bottom - area_top);
                                //recognizedGestures[0].gAction.Details = string.Format("{0}:{1}:{2}:{3}", area_left, area_top, area_width, area_height);
                                gest.Action.Details = string.Format("{0}:{1}:{2}:{3}", 10, 10, 100, 100);
                                goto default;
                            //break;
                            case TypeOfAction.WindowOptions.WND_TRANSPARENT:
                            case TypeOfAction.VolumeOptions.VOLUME_SET:
                                ToolStripMenuItem[] items = tSMI_arrayMenu(gest);
                                
                                if (threadID == m_threadID)
                                    lock (this) 
                                    {  
                                        form_top.ClearContextMenu();
                                    }
                                else break;

                                for (int i = 0; i < items.Length; i++)
                                {
                                    if (threadID == m_threadID)
                                        lock (this) 
                                        { 
                                            form_top.AddItemToCms(items[i]);
                                        }
                                    else break;
                                }
                                //foreach (ToolStripMenuItem item in items)
                                //    form_top.AddItemToCms(cMS_MatchedGestures, item);
                                if (threadID == m_threadID)
                                    lock (this) 
                                    { 
                                        form_top.ShowContextMenu();
                                    }
                                break;
                            default:
                                // gesture was recognized therefore hide the form_transparent
                                //form_top.MakeNonTopMost();

                                m_execute = gest;
                                //Thread threadExecuteAction = new Thread(new ThreadStart(ExecuteAction));
                                //threadExecuteAction.Name = "Execute Action";
                                //threadExecuteAction.Start();
                                
                                // no need to do it in thread as it is already in one
                                ExecuteAction();
                                break;
                        }
                        break;
                    }
                    else goto default;
                //break;
                default:
                    if (threadID == m_threadID)
                        lock (this) 
                        {
                            form_top.ClearContextMenu();
                        }

                    for (int i = 0; i < recognizedGestures.Count; i++)
                    {
                        gest = recognizedGestures[i];
                        if (gest.ExecutionType == ExecuteType.Implicit)
                        {
                            index = i;
                            goto case 1;
                        }
                        if (threadID == m_threadID)
                            lock (this)
                            {
                                switch (gest.Action.Name)
                                {
                                    case TypeOfAction.WindowOptions.WND_TRANSPARENT:
                                    case TypeOfAction.VolumeOptions.VOLUME_SET:
                                        //case TypeOfAction.SpecialControl.SPECIAL_ZOOM:
                                        //form_top.AddItemToCms(cMS_MatchedGestures, tSMI_dropDownMenu(gest));
                                        form_top.AddItemToCms(tSMI_dropDownMenu(gest));
                                        break;
                                    default:
                                        //ToolStripMenuItem item = new ToolStripMenuItem();
                                        //item.Image = gest.ImageList.Images[gest.ID];
                                        //item.Name = gest.ID;
                                        //item.Text = gest.Caption;
                                        //form_top.AddItemToCms(cMS_SameGestures, item);
                                        //form_top.AddItemToCms(cMS_MatchedGestures, gest.ToToolStripMenuItem(m_imgListActions));
                                        form_top.AddItemToCms(gest.ToToolStripMenuItem(m_imgListActions));
                                        break;
                                }
                            }
                    }
                    if (threadID == m_threadID)
                        lock (this) 
                        { 
                            form_top.ShowContextMenu();
                        }

                    break;
            }
        }

        private GesturesCollection GetRecognizedGestures(string name)
        {
            GesturesCollection recognizedGestures = new GesturesCollection();            
            m_hwndWindowToUse = GetWindowToUse(m_cursorPosition);            
            IntPtr hwnd = m_hwndWindowToUse;// GetWindowToUse(m_cursorPosition); //m_hwndWndToUse;
            //if (Config.User.handleWndUnderCursor)
            //    hwnd = m_mouse.ActiveWindow;
            //else
            //    hwnd = m_hwndWndToUse;
            foreach (MyGesture gesture in learntGestures.MatchedGestures(name))
            {    
                if (gesture.IsActive(hwnd))
                {
                    recognizedGestures.Add(gesture);
                    if (gesture.ExecutionType == ExecuteType.Implicit || gesture.IsImplicitOnly)
                    {
                        recognizedGestures.Clear();
                        recognizedGestures.Add(gesture);
                        break;
                    }
                }
            }
            return recognizedGestures;
        }

        private void ExecuteAction()
        {
            Debug.WriteLine("Thread ExecuteAction started. Handle WndUnderCursor " + Config.User.HandleWndUnderCursor.ToString());
            IntPtr hwnd = m_hwndWindowToUse;//GetWindowToUse(m_cursorPosition); //m_hwndWndToUse;
            //if (Config.User.handleWndUnderCursor)
            //    hwnd = m_mouse.ActiveWindow;
            //else
            //    hwnd = m_hwndWndToUse;
            Point position = new Point(m_cursorPosition.X, m_cursorPosition.Y);

            if (!m_execute.Action.IsSensitiveToMySystemWindows()
                || (!form_top.IsTopForm(hwnd) && !form_top.IsContextMenu(hwnd)))
            {
                if (!m_execute.ScriptContainsMouse)
                    m_execute.Action.ExecuteAction(hwnd, position);
                else
                {
                    m_mouse.StartSimulation();
                    m_execute.Action.ExecuteAction(hwnd, position);
                    m_mouse.StopSimulation();
                }
            }
            else
            {
                Debug.WriteLine("Action was not executed!");
            }

            //Win32.SetForegroundWindow(form_top.GetNextWindow());
            //IntPtr foreWnd = Win32.GetForegroundWindow();
            //if (form_top.IsTopForm(foreWnd))
            //{
            //    Debug.WriteLine("FORM_TOP IS SELECTED");
                
            //    form_top.MakeNonTopMost();                
            //    foreWnd = Win32.GetForegroundWindow();
            //    if (form_top.IsTopForm(foreWnd))
            //        Debug.WriteLine("FORM_TOP IS STILL SELECTED!!!!!");
            //    //IntPtr lHwnd = Win32.FindWindow("Shell_TrayWnd", null);
            //    //Win32.SetForegroundWindow(lHwnd);
            //    //Win32.SetActiveWindow(lHwnd);
            //    //IntPtr nextWnd = Win32.GetWindow(foreWnd, Win32.GW_HWNDNEXT);
            //    //Win32.ShowWindow(nextWnd, 1);
            //    //Win32.SetWindowPos(nextWnd, Win32.HWND_TOPMOST, 0, 0, 0, 0, Win32.SWP_SHOWWINDOW);
            //    //Win32.SetActiveWindow(nextWnd);
            //    //Win32.SetForegroundWindow(m_hwndWndToUse);
            //}
             
        }

        private IntPtr GetWindowToUse(Point position)
        {
            IntPtr hwnd;
            if (Config.User.HandleWndUnderCursor)
                hwnd = GetWindowUnderCursor(position);
            else
                hwnd = m_hwndForeWnd;
            return hwnd;
        }

        

        private IntPtr GetWindowUnderCursor(Point position)
        {
            StringBuilder wndText = new StringBuilder(256);
            Random rnd = new Random();            
            int countOfTry = 10;
            IntPtr originalHandle = IntPtr.Zero;
            IntPtr mainWindow = IntPtr.Zero;
            IntPtr window = IntPtr.Zero;

            originalHandle = Win32.WindowFromPoint(position);

            for (int i = 0; i < countOfTry; i++)
            {
                window = Win32.WindowFromPoint(position);
                mainWindow = Win32.GetAncestor(window, Win32.GA_ROOT);
                Win32.GetWindowText(mainWindow, wndText, 256);
                if (wndText.Length != 0)
                    break;
                int xDeviation = StaticRandom.RandomInteger(2, 5);
                int yDeviation = StaticRandom.RandomInteger(2, 5);
                position = new Point(position.X + xDeviation, position.Y + yDeviation);
            }

            if (mainWindow != IntPtr.Zero)
                return mainWindow;
            else
                return originalHandle;
        }

        #endregion Analyze & Execute

        #region ContextMenuStrip actions

        private ToolStripMenuItem tSMI_dropDownMenu(MyGesture gesture)
        {
            //ToolStripMenuItem tSMI_menu = new ToolStripMenuItem();
            //tSMI_menu.Image = gesture.ImageList.Images[gesture.ID];
            //tSMI_menu.Text = gesture.ListItem.Caption;
            //tSMI_menu.Name = gesture.ID;
            ToolStripMenuItem tSMI_menu = gesture.ToToolStripMenuItem(m_imgListActions);

            tSMI_menu.DropDown = new ContextMenuStrip();
            ((ContextMenuStrip)tSMI_menu.DropDown).Text = form_top.CmsMatchedGestures.Text;//cMS_MatchedGestures.Text;
            ((ContextMenuStrip)tSMI_menu.DropDown).ShowImageMargin = false;
            ((ContextMenuStrip)tSMI_menu.DropDown).ShowCheckMargin = false;
            ((ContextMenuStrip)tSMI_menu.DropDown).ItemClicked += new ToolStripItemClickedEventHandler(cMS_MatchedGestures_ItemClicked);
            ToolStripMenuItem[] items = tSMI_arrayMenu(gesture);
            foreach (ToolStripMenuItem item in items)
            {
                //form_top.AddItemToCms((ContextMenuStrip)tSMI_menu.DropDown, item);
                //form_top.AddItemToCms(item);
                tSMI_menu.DropDownItems.Add(item);
            }
            return tSMI_menu;
        }


        void cMS_MatchedGestures_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {            
            ToolStripMenuItem item = (ToolStripMenuItem)e.ClickedItem;
            // do not close context menu when it contains dropdown menu - it has to open the context menu
            if (item.DropDown.Items.Count == 0)
                form_top.CloseContextMenuAsync();    
            m_execute = learntGestures[item.Name];
            switch (m_execute.Action.Name)
            {
                case TypeOfAction.SpecialOptions.SPECIAL_ZOOM:
                    //int area_width = Math.Abs(area_right - area_left);
                    //int area_height = Math.Abs(area_bottom - area_top);
                    //ga.gAction.Details = string.Format("{0}:{1}:{2}:{3}", area_left, area_top, area_width, area_height);
                    m_execute.Action.Details = string.Format("{0}:{1}:{2}:{3}", 10, 10, 100, 100);
                    break;
                case TypeOfAction.WindowOptions.WND_TRANSPARENT:
                    if (item.Tag != null)
                        m_execute.Action.Details = item.Tag.ToString();
                    else return;
                    break;
                case TypeOfAction.VolumeOptions.VOLUME_SET:
                    if (item.Tag != null)
                        m_execute.Action.Details = item.Tag.ToString();
                    else return;
                    break;
            }
            Thread threadExecuteAction = new Thread(new ThreadStart(ExecuteAction));
            threadExecuteAction.Name = "Execute Action";
            threadExecuteAction.Start();
            //ExecuteAction();
        }

        private ToolStripMenuItem[] tSMI_arrayMenu(MyGesture gesture)
        {
            double[] lvl = new double[] { };
            string postfix = string.Empty;
            switch (gesture.Action.Name)
            {
                case TypeOfAction.WindowOptions.WND_TRANSPARENT:
                    lvl = TypeOfAction.WindowOptions.TransparencyLvl;
                    postfix = "%";
                    break;
                case TypeOfAction.VolumeOptions.VOLUME_SET:
                    lvl = TypeOfAction.VolumeOptions.VolumeLvl;
                    postfix = "%";
                    break;
                //case TypeOfAction.SpecialControl.SPECIAL_ZOOM:
                //    lvl = TypeOfAction.SpecialControl.ZoomLvl;
                //    postfix = "x";
                //    break;
            }

            ToolStripMenuItem[] items = new ToolStripMenuItem[lvl.Length];
            for (int i = 0; i < lvl.Length; i++)
            {
                items[i] = new ToolStripMenuItem();
                items[i].Text = String.Format("{0} {1}", lvl[i], postfix);
                items[i].Name = gesture.ID;
                items[i].Tag = lvl[i].ToString();
            }
            return items;
        }

        #endregion ContextMenuStrip actions

     
    }
}
