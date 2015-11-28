using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml;
using System.Windows.Forms;
using JustGestures.Features;

namespace JustGestures
{
    class FileOptions
    {
        private const string GESTURES = "gestures.jg";
        private const string PROGRAMS = "programs.jg";
        private const string NETWORK = "network.jg";
        private const string TRAINING_SET = "training_set.jg";
        private const string FAVICONS = "favicons.jg";

        public static void SaveGestures(GesturesCollection gestures)
        {   
            Stream stream = null;
            try
            {
                stream = File.Open(Config.Default.FilesLocation + GESTURES, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(stream, gestures);                
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

        public static GesturesCollection LoadGestures()
        {
            GesturesCollection gestures = new GesturesCollection();
            Stream stream = null;
            try
            {
                stream = File.Open(Config.Default.FilesLocation + GESTURES, FileMode.Open);
                BinaryFormatter bFormatter = new BinaryFormatter();
                gestures = (GesturesCollection)bFormatter.Deserialize(stream);                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                gestures.Add(MyGesture.GlobalGroup);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return gestures;

        }

        public static void SaveLists(List<PrgNamePath> whiteList, List<PrgNamePath> blackList, List<PrgNamePath> finalList)
        {
            String[] elements = new string[] { "name", "path", "active" };
            XmlWriter writer = null;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = true;
            settings.ConformanceLevel = ConformanceLevel.Auto;
            try
            {
                writer = XmlWriter.Create(Config.Default.FilesLocation + PROGRAMS, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement("AutoBehave");                

                writer.WriteStartElement("WhiteList");
                for (int i = 0; i < whiteList.Count; i++)
                {
                    writer.WriteStartElement("program");
                    writer.WriteAttributeString(elements[0], whiteList[i].PrgName);
                    writer.WriteElementString(elements[1], whiteList[i].Path);
                    writer.WriteElementString(elements[2], whiteList[i].Active.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteStartElement("BlackList");
                for (int i = 0; i < blackList.Count; i++)
                {
                    writer.WriteStartElement("program");
                    writer.WriteAttributeString(elements[0], blackList[i].PrgName);
                    writer.WriteElementString(elements[1], blackList[i].Path);
                    writer.WriteElementString(elements[2], blackList[i].Active.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteStartElement("FinalList");
                for (int i = 0; i < finalList.Count; i++)
                {
                    writer.WriteStartElement("program");
                    writer.WriteAttributeString(elements[0], finalList[i].PrgName);
                    writer.WriteElementString(elements[1], finalList[i].Path);
                    writer.WriteElementString(elements[2], finalList[i].Active.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

        public static void LoadLists(out List<PrgNamePath> whiteList, out List<PrgNamePath> blackList, out List<PrgNamePath> finalList)
        {
            XmlDocument document = new XmlDocument();
            XmlTextReader reader = null;
            PrgNamePath prog = new PrgNamePath();
            whiteList = new List<PrgNamePath>();
            blackList = new List<PrgNamePath>();
            finalList = new List<PrgNamePath>();
            string reading = null;
            try
            {
                reader = new XmlTextReader(Config.Default.FilesLocation + PROGRAMS);
                while (reader.Read())
                {
                    if (reader.Name == "WhiteList")
                        reading = "WhiteList";
                    else if (reader.Name == "BlackList")
                        reading = "BlackList";
                    else if (reader.Name == "FinalList")
                        reading = "FinalList";
                    else if (reader.Name == "program")
                        prog.PrgName = reader["name"];
                    else if (reader.Name == "path")
                        prog.Path = reader.ReadString();
                    else if (reader.Name == "active")
                    {
                        bool result = false;
                        bool.TryParse(reader.ReadString(), out result);
                        prog.Active = result;
                        if (reading == "WhiteList")
                            whiteList.Add(new PrgNamePath(prog.PrgName, prog.Path, prog.Active));
                        else if (reading == "BlackList")
                            blackList.Add(new PrgNamePath(prog.PrgName, prog.Path, prog.Active));
                        else if (reading == "FinalList")
                        {
                            string state = prog.Active ? OptionItems.UC_autoBehaviour.STATE_ENABLED : OptionItems.UC_autoBehaviour.STATE_DISABLED;
                            finalList.Add(new PrgNamePath(state, prog.PrgName, prog.Path));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

        public static void SaveNeuralNetwork(NeuralNetwork.Network neuralNetwork)
        {
            Stream stream = null;
            try
            {
                stream = File.Open(Config.Default.FilesLocation + NETWORK, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                if (neuralNetwork != null)
                    bFormatter.Serialize(stream, neuralNetwork);
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

        public static NeuralNetwork.Network LoadNeuralNetwork()
        {
            NeuralNetwork.Network neuralNetwork = null;
            Stream stream = null;
            try
            {
                stream = File.Open(Config.Default.FilesLocation + NETWORK, FileMode.Open);
                BinaryFormatter bFormatter = new BinaryFormatter();
                neuralNetwork = (NeuralNetwork.Network)bFormatter.Deserialize(stream);                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null) stream.Close();
            }
            return neuralNetwork;
        }

        public static void SaveTrainingSet(Dictionary<string, MyCurve> trainingSet)
        {
            Stream stream = null;
            try
            {
                stream = File.Open(Config.Default.FilesLocation + TRAINING_SET, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(stream, trainingSet);                
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

        public static Dictionary<string, MyCurve> LoadTrainingSet()
        {
            Dictionary<string, MyCurve> trainingSet = new Dictionary<string, MyCurve>();
            Stream stream = null;
            try
            {
                stream = File.Open(Config.Default.FilesLocation + TRAINING_SET, FileMode.Open);
                BinaryFormatter bFormatter = new BinaryFormatter();
                trainingSet = (Dictionary<string, MyCurve>)bFormatter.Deserialize(stream);                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null) stream.Close();
            }
            return trainingSet;            
        }

        public static void SaveFavicons(Dictionary<string, System.Drawing.Bitmap> favicons)
        {
            Stream stream = null;
            try
            {
                stream = File.Open(Config.Default.FilesLocation + FAVICONS, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(stream, favicons);
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

        public static Dictionary<string, System.Drawing.Bitmap> LoadFavicons()
        {
            Dictionary<string, System.Drawing.Bitmap> favicons = new Dictionary<string, System.Drawing.Bitmap>();
            Stream stream = null;
            try
            {
                stream = File.Open(Config.Default.FilesLocation + FAVICONS, FileMode.Open);
                BinaryFormatter bFormatter = new BinaryFormatter();
                favicons = (Dictionary<string, System.Drawing.Bitmap>)bFormatter.Deserialize(stream);
                stream.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null) stream.Close();
            }
            return favicons;
        }
    }
}
