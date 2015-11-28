using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using JustGestures.OptionItems;

namespace JustGestures
{
    [XmlRoot("Config")]
    public class Config : IXmlSerializable
    {
        #region Singleton 

        const string CONFIGURATION = "config.jg";

        static Config m_user;
        static Config m_default;

        public static Config User
        {
            get
            {
                if (m_user == null)
                {
                    m_user = new Config();
                    m_user.Load();
                    m_user.CheckValues();
                }
                return m_user;
            }
        }

        public static Config Default
        {
            get
            {
                if (m_default == null)
                    m_default = new Config();
                return m_default;
            }
        }

        #endregion Singleton

#if DEBUG
        public string FilesLocation = Path.GetDirectoryName(Application.ExecutablePath) + "/";
#else 
        public string FilesLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Just Gestures/";
#endif

        public bool AutoStart = true;
        public bool MinToTray = true;
        public bool CloseToTray = true;
        public bool StartMinimized = false;        
        public bool HandleWndUnderCursor = true;

        public Color PenColor = Color.Red;
        public int PenWidth = 4;
        public int MhToolTipDelay = 500;
        public bool MhShowToolTip = true;
        public bool DisplayGesture = true;
        public MouseButtons BtnToggle = MouseButtons.Right;

        public bool AutoBehaviour = false;
        public int StateFullScreen = 0;
        public int StateDefault = 0;
        public int StateAuto1 = 0;
        public int StateAuto2 = 0;
        public int CheckWndLoop = 1000;

        public string NnVersion = "1.0";
        public int NnInputSize = 18;
        public int NnOutputSize = 40;
        public int NnTrainingSetSize = 50;
        public double NnLearningRate = 0.5;
        public double NnMomentum = 0.3;
        public double NnGoal = 0.0001;
        public int NnHidenLayerSize = 30;        

        public int LW_viewMode = 0;
        public bool UsingClassicCurve = true;
        public bool UsingDoubleBtn = true;
        public bool UsingWheelBtn = true;
        public int SensitiveZone = 3;
        public int DeactivationTimeout = 450;
        public bool CheckForUpdate = true;
        public string Language = "English";
        public bool FirstTimeRun = true;

        #region Values Checking

        private void CheckValues()
        {
            if (!Enum.IsDefined(typeof(UC_visualisation.eColors), PenColor.Name))
                PenColor = Default.PenColor;
            PenWidth = Math.Min(Math.Max(PenWidth, 1), UC_visualisation.WIDTH_COUNT);
            MhToolTipDelay = Math.Min(Math.Max(MhToolTipDelay, UC_visualisation.TOOL_TIP_DELAY_MIN), UC_visualisation.TOOL_TIP_DELAY_MAX); 
            CheckWndLoop = Math.Min(Math.Max(CheckWndLoop, UC_autoBehaviour.AUTO_CHECK_MIN), UC_autoBehaviour.AUTO_CHECK_MAX);
            if (BtnToggle == MouseButtons.None) BtnToggle = Default.BtnToggle;

            if (StateFullScreen < 0 || StateFullScreen > 2) StateFullScreen = Default.StateFullScreen;
            if (StateDefault < 0 || StateDefault > 2) StateDefault = Default.StateDefault;
            if (StateAuto1 < 0 || StateAuto1 > 2) StateAuto1 = Default.StateAuto1;
            if (StateAuto2 < 0 || StateAuto2 > 2) StateAuto2 = Default.StateAuto2;

            if (LW_viewMode != 0 && LW_viewMode != 1) LW_viewMode = Default.LW_viewMode;
            SensitiveZone = Math.Min(Math.Max(SensitiveZone, 0), UC_gestureOptions.SENSITIVE_ZONE_RANGE);
            DeactivationTimeout = Math.Min(Math.Max(DeactivationTimeout, UC_gestureOptions.DEACTIVATION_TIME_MIN), UC_gestureOptions.DEACTIVATION_TIME_MAX);
        }

        #endregion Values Checking

        #region Init

        public Config()
        {
        }

        public void Save()
        {
            Stream stream = null;
            try
            {
                stream = File.Open(Config.Default.FilesLocation + CONFIGURATION, FileMode.Create);                
                XmlSerializer xs = new XmlSerializer(typeof(Config));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, Encoding.UTF8);
                xmlTextWriter.Formatting = Formatting.Indented;
                xs.Serialize(xmlTextWriter, m_user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }

        public void Load()
        {            
            Stream stream = null;
            try
            {
                stream = File.Open(Config.Default.FilesLocation + CONFIGURATION, FileMode.Open);
                XmlSerializer xs = new XmlSerializer(typeof(Config));
                m_user = (Config)xs.Deserialize(stream);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }
       
        #endregion Init

        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            string str;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    try { str = reader.ReadString(); }
                    catch { str = string.Empty; }
                    try
                    {
                        switch (reader.Name)
                        {
                            case "AutoStart": AutoStart = bool.Parse(str); break;
                            case "MinToTray": MinToTray = bool.Parse(str); break;
                            case "CloseToTray": CloseToTray = bool.Parse(str); break;
                            case "StartMinimized": StartMinimized = bool.Parse(str); break;
                            case "HandleWndUnderCursor": HandleWndUnderCursor = bool.Parse(str); break;
                            case "PenColor": PenColor = Color.FromName(str); break;
                            case "PenWidth": PenWidth = int.Parse(str); break;
                            case "MhToolTipDelay": MhToolTipDelay = int.Parse(str); break;
                            case "MhShowToolTip": MhShowToolTip = bool.Parse(str); break;
                            case "CheckWndLoop": CheckWndLoop = int.Parse(str); break;
                            case "DisplayGesture": DisplayGesture = bool.Parse(str); break;
                            case "BtnToggle":
                                switch (str)
                                {
                                    case "Left": BtnToggle = MouseButtons.Left; break;
                                    case "Right": BtnToggle = MouseButtons.Right; break;
                                    case "Middle": BtnToggle = MouseButtons.Middle; break;
                                    case "XButton1": BtnToggle = MouseButtons.XButton1; break;
                                    case "XButton2": BtnToggle = MouseButtons.XButton2; break;
                                }
                                break;
                            case "AutoBehaviour": AutoBehaviour = bool.Parse(str); break;
                            case "StateFullScreen": StateFullScreen = int.Parse(str); break;
                            case "StateDefault": StateDefault = int.Parse(str); break;
                            case "StateAuto1": StateAuto1 = int.Parse(str); break;
                            case "StateAuto2": StateAuto2 = int.Parse(str); break;
                            case "NnVersion": NnVersion = str; break;
                            case "NnInputSize": NnInputSize = int.Parse(str); break;
                            case "NnOutputSize": NnOutputSize = int.Parse(str); break;
                            case "NnTrainingSetSize": NnTrainingSetSize = int.Parse(str); break;
                            case "NnLearningRate": NnLearningRate = double.Parse(str); break;
                            case "NnMomentum": NnMomentum = double.Parse(str); break;
                            case "NnGoal": NnGoal = double.Parse(str); break;
                            case "NnHidenLayerSize": NnHidenLayerSize = int.Parse(str); break;
                            case "LW_viewMode": LW_viewMode = int.Parse(str); break;
                            case "UsingClassicCurve": UsingClassicCurve = bool.Parse(str); break;
                            case "UsingDoubleBtn": UsingDoubleBtn = bool.Parse(str); break;
                            case "UsingWheelBtn": UsingWheelBtn = bool.Parse(str); break;
                            case "SensitiveZone": SensitiveZone = int.Parse(str); break;
                            case "DeactivationTimeout": DeactivationTimeout = int.Parse(str); break;
                            case "CheckForUpdate": CheckForUpdate = bool.Parse(str); break;
                            case "Language": Language = str; break;
                            case "FirstTimeRun": FirstTimeRun = bool.Parse(str); break;

                        }
                    }
                    catch { }
                }
            }          
        }

        private static string GetColorName(Color color)
        {
            if (color.IsNamedColor)
            {
                string name = color.Name.Replace("Color", "").Replace("[", "").Replace("]", "");
                return name;
            }
            else
                return color.Name;
        }

        public void WriteXml(XmlWriter writer)
        {                        
            writer.WriteElementString("AutoStart", AutoStart.ToString());
            writer.WriteElementString("MinToTray", MinToTray.ToString());
            writer.WriteElementString("CloseToTray", CloseToTray.ToString());
            writer.WriteElementString("StartMinimized", StartMinimized.ToString());
            writer.WriteElementString("HandleWndUnderCursor", HandleWndUnderCursor.ToString());
            writer.WriteElementString("PenColor", GetColorName(PenColor));
            writer.WriteElementString("PenWidth", PenWidth.ToString());
            writer.WriteElementString("MhToolTipDelay", MhToolTipDelay.ToString());
            writer.WriteElementString("MhShowToolTip", MhShowToolTip.ToString());
            writer.WriteElementString("CheckWndLoop", CheckWndLoop.ToString());
            writer.WriteElementString("DisplayGesture", DisplayGesture.ToString());
            writer.WriteElementString("BtnToggle", BtnToggle.ToString());
            writer.WriteElementString("AutoBehaviour", AutoBehaviour.ToString());
            writer.WriteElementString("StateFullScreen", StateFullScreen.ToString());
            writer.WriteElementString("StateDefault", StateDefault.ToString());
            writer.WriteElementString("StateAuto1", StateAuto1.ToString());
            writer.WriteElementString("StateAuto2", StateAuto2.ToString());
            writer.WriteElementString("NnVersion", NnVersion.ToString());
            writer.WriteElementString("NnInputSize", NnInputSize.ToString());
            writer.WriteElementString("NnOutputSize", NnOutputSize.ToString());
            writer.WriteElementString("NnTrainingSetSize", NnTrainingSetSize.ToString());
            writer.WriteElementString("NnLearningRate", NnLearningRate.ToString());
            writer.WriteElementString("NnMomentum", NnMomentum.ToString());
            writer.WriteElementString("NnGoal", NnGoal.ToString());
            writer.WriteElementString("NnHidenLayerSize", NnHidenLayerSize.ToString());
            writer.WriteElementString("LW_viewMode", LW_viewMode.ToString());
            writer.WriteElementString("UsingClassicCurve", UsingClassicCurve.ToString());
            writer.WriteElementString("UsingDoubleBtn", UsingDoubleBtn.ToString());
            writer.WriteElementString("UsingWheelBtn", UsingWheelBtn.ToString());
            writer.WriteElementString("SensitiveZone", SensitiveZone.ToString());
            writer.WriteElementString("DeactivationTimeout", DeactivationTimeout.ToString());
            writer.WriteElementString("CheckForUpdate", CheckForUpdate.ToString());
            writer.WriteElementString("Language", Language.ToString());
            writer.WriteElementString("FirstTimeRun", FirstTimeRun.ToString());
        }
        #endregion
    }
}
