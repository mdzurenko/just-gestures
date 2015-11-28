using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

namespace JustGestures.Languages
{
    public class Translation
    {
        #region Singleton

        static Translation m_current;

        public static Translation Current
        {
            get
            {
                if (m_current == null)
                {
                    m_current = new Translation();
                }
                return m_current;
            }
        }

        #endregion Singleton

        Dictionary<string, string> m_originalTexts = new Dictionary<string, string>();

        Dictionary<string, string> m_translatedTexts = new Dictionary<string, string>();

        #region Global names

        public static string Name_CurveGesture { get { return GetText("Name_CurveGesture"); } }
        public static string Name_WheelGesture { get { return GetText("Name_WheelGesture"); } }
        public static string Name_RockerGesture { get { return GetText("Name_RockerGesture"); } }

        public static string Name_CurveHoldDownBtn { get { return GetText("Name_CurveHoldDownBtn"); } }
        public static string Name_RockerHoldDownBtn { get { return GetText("Name_RockerHoldDownBtn"); } }
        public static string Name_RockerExecuteBtn { get { return GetText("Name_RockerExecuteBtn"); } }
        public static string Name_WheelHoldDownBtn { get { return GetText("Name_WheelHoldDownBtn"); } }

        public static string Name_LeftButton { get { return GetText("Name_leftButton"); } }
        public static string Name_RightButton { get { return GetText("Name_rightButton"); } }
        public static string Name_MiddleButton { get { return GetText("Name_middleButton"); } }
        public static string Name_X1Button { get { return GetText("Name_x1Button"); } }
        public static string Name_X2Button { get { return GetText("Name_x2Button"); } }
        public static string Name_MouseWheel { get { return GetText("Name_mouseWheel"); } }

        public static string Name_lowPriority { get { return GetText("Name_lowPriority"); } }
        public static string Name_normalPriority { get { return GetText("Name_normalPriority"); } }
        public static string Name_highPriority { get { return GetText("Name_highPriority"); } }

        public static string Text_info { get { return GetText("I_infoCaption"); } }
        public static string Text_warning { get { return GetText("I_warningCaption"); } }
        public static string Text_error { get { return GetText("I_errorCaption"); } }

        public static string Btn_ok { get { return GetText("Btn_ok"); } }
        public static string Btn_add { get { return GetText("Btn_add"); } }
        public static string Btn_addNew { get { return GetText("Btn_addNew"); } }
        public static string Btn_apply { get { return GetText("Btn_apply"); } }
        public static string Btn_back { get { return GetText("Btn_back"); } }
        public static string Btn_cancel { get { return GetText("Btn_cancel"); } }
        public static string Btn_finish { get { return GetText("Btn_finish"); } }
        public static string Btn_next { get { return GetText("Btn_next"); } }
        
        #endregion Global names

        public static string GetMouseBtnText(MouseButtons btn)
        {
            switch (btn)
            {
                case MouseButtons.Left: return Name_LeftButton;
                case MouseButtons.Right: return Name_RightButton;
                case MouseButtons.Middle: return Name_MiddleButton;
                case MouseButtons.XButton1: return Name_X1Button;
                case MouseButtons.XButton2: return Name_X2Button;
            }
            return string.Empty;
        }
        public static string GetPriorityText(ExecuteType priority)
        {            
            switch (priority)
            {
                case ExecuteType.Implicit: return Name_highPriority; 
                case ExecuteType.ImplicitIfUnique: return Name_normalPriority; 
                case ExecuteType.Explicit: return Name_lowPriority;
            }
            return string.Empty;
        }
        
        public Translation()
        {
            this.LoadOriginalTexts();
            Dictionary<string, string> languages = new Dictionary<string, string>();
            try
            {
                languages = GetAllLanguages();
            }
            catch { } 
            string file = string.Empty;
            if (languages.ContainsKey(Config.User.Language))
                file = languages[Config.User.Language];
            else
                Config.User.Language = Config.Default.Language;
            this.LoadLanguage(file);
        }

        public static string GetText(string key)
        {
            string translation = string.Empty;
            
            if (Translation.Current.m_translatedTexts.ContainsKey(key))
                translation = Translation.Current.m_translatedTexts[key];
#if !DEBUG
            if (translation == string.Empty)
            {
                if (Translation.Current.m_originalTexts.ContainsKey(key))
                    translation = Translation.Current.m_originalTexts[key];
                else
                    translation = string.Empty;
            }
#endif

            if (translation != string.Empty)
                return translation;                
            else
                return "[To Translate]";
        }

        

        #region Language Files' Options

        /// <summary>
        /// Loads translated texts from file 
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadLanguage(string filePath)
        {
            // Load original texts as a default english is chosen
            if (filePath == string.Empty)
            {
                m_translatedTexts = m_originalTexts;
                return;
            }

            Stream stream = null;
            XmlReader reader = null;
            try
            {
                stream = File.Open(filePath, FileMode.Open);
                reader = XmlReader.Create(stream);
                m_translatedTexts = LoadTranslation(reader);
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null) stream.Close();
                if (reader != null) reader.Close();
            }
        }

        /// <summary>
        /// Returns all available languages from folder Languages
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetAllLanguages()
        {
            Dictionary<string, string> languages = new Dictionary<string, string>();
            languages.Add("English", "");
            string path = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "/";
            path += "Languages/";
            if (!Directory.Exists(path))
            {
                try { Directory.CreateDirectory(path); }
                catch { return languages; } 
            }
            Stream stream = null;
            XmlReader reader = null;
            foreach (string file in Directory.GetFiles(path, "*.lng"))
            {
                try
                {
                    stream = File.Open(file, FileMode.Open);
                        reader = XmlReader.Create(stream);
                        string lng = GetLanguage(reader);
                    if (!languages.ContainsKey(lng))
                        languages.Add(lng, file);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    if (stream != null) stream.Close();
                    if (reader != null) reader.Close();
                }

            }
            return languages;
        }

        /// <summary>
        /// Gets the language from xml file 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static string GetLanguage(XmlReader reader)
        {
            string language = string.Empty;
            while (reader.Read())
            {
                if (reader.Name == "Language")
                {
                    language = reader.ReadString();
                    break;
                }
            }
            return language;
        }
        
        /// <summary>
        /// Loads original texts from embedded resources
        /// </summary>
        private void LoadOriginalTexts()
        {
            Stream stream = null;
            XmlReader reader = null;
            try
            {
                Assembly a = Assembly.GetEntryAssembly();
                stream = a.GetManifestResourceStream("JustGestures.Languages.OriginalTexts.xml");
                reader = XmlReader.Create(stream);
                m_originalTexts = LoadTranslation(reader);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                try
                {
                    stream = File.Open("D:\\SCHOOL\\_JUST_GESTURES_\\JustGesturesNN\\Project\\JustGestures\\Languages\\OriginalTexts.xml", FileMode.Open);
                    reader = XmlReader.Create(stream);
                    m_originalTexts = LoadTranslation(reader);
                }
                catch { }
                finally
                {
                    if (stream != null) stream.Close();
                    if (reader != null) reader.Close();
                }
            }
            finally
            {
                if (stream != null) stream.Close();
                if (reader != null) reader.Close();
            }
        }

        /// <summary>
        /// Loads texts from xml file
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Dictionary<string, string> LoadTranslation(XmlReader reader)
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();
            string key = string.Empty;
            string value = string.Empty;
            while (reader.Read())
            {
                if (reader.Name == "TranslatedText")
                {
                    while (reader.Read())
                    {
                        // reads only keys and values all other are not required
                        if (reader.Name == "Text")
                        {
                            if (reader.IsStartElement())
                                key = reader["Key"];
                            else
                            {
                                if (key != string.Empty && !translation.ContainsKey(key))
                                    translation.Add(key, value);
                            }
                        }
                        else if (reader.Name == "Value")
                            value = reader.ReadString();
                    }
                }
            }
            return translation;
        }

        #endregion Language Files' Options

    }
}
