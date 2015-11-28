using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Drawing;
using System.Threading;

namespace JustGestures.Features
{
    public class Favicon
    {
        Thread m_threadIcon = null;
        string m_url = string.Empty;

        public string Url { set { m_url = value; } }

        public delegate void DlgIconObtained(Bitmap bmp);
        public DlgIconObtained IconObatined;

        private void OnIconObtained(Bitmap bmp)
        {
            if (IconObatined != null)
                IconObatined(bmp);
        }

        public Favicon()
        {
        }

        public static void AddNew(string url, Bitmap bmp)
        {
            Uri uri = new Uri(url);
            string key = uri.Scheme + "://" + uri.Host;
            Dictionary<string, Bitmap> favicons = FileOptions.LoadFavicons();
            if (!favicons.ContainsKey(key))
                favicons.Add(key, bmp);
            FileOptions.SaveFavicons(favicons);
        }

        public static Bitmap Load(string url)
        {
            if (url == string.Empty || url.Remove(0, url.IndexOf("://") + 3) == string.Empty)
                return null;
            Uri uri = null;
            try
            {
                uri = new Uri(url);
            }
            catch (Exception ex)
            {
                return null;
            }
            string key = uri.Scheme + "://" + uri.Host;
            Dictionary<string, Bitmap> favicons = FileOptions.LoadFavicons();
            if (favicons.ContainsKey(key))
                return favicons[key];
            else
                return null;
        }


        #region Favicon Extractor

        public void ObtainIcon()
        {            
            if (m_threadIcon != null && m_threadIcon.IsAlive)
                m_threadIcon.Abort();
            m_threadIcon = new Thread(new ThreadStart(ThreadObtainIcon));
            m_threadIcon.Start();
        }

        public void StopThread()
        {
            if (m_threadIcon != null && m_threadIcon.IsAlive)
                m_threadIcon.Abort();
        }

        private void ThreadObtainIcon()
        {
            Uri uri = null;
            WebResponse wb = null;
            try
            {
                uri = new Uri(m_url);
                wb = WebRequest.Create(uri.Scheme + "://" + uri.Host).GetResponse();
            }
            catch (Exception ex)
            {
                OnIconObtained(null);
                return;
            }

            Bitmap bmp = null;
            Stream myStream = null;
            myStream = GetFaviconFromSource(wb.ResponseUri.ToString());
            if (myStream == null)
                myStream = GetDefaultFavIcon(wb.ResponseUri.ToString());
            if (myStream != null)
            {
                try
                {
                    bmp = new Bitmap(myStream);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    bmp = null;
                }
            }
            if (bmp != null)
                AddNew(m_url, bmp);
            OnIconObtained(bmp);
        }


        private Stream GetDefaultFavIcon(string url)
        {
            if (url == string.Empty) return null;
            string iconPath = string.Empty;
            Uri uri = new Uri(url);
            WebRequest requestImg = WebRequest.Create(uri.Scheme + "://" + uri.Host + "/favicon.ico");
            try
            {
                WebResponse response = requestImg.GetResponse();
                if (response.ContentLength > 0)
                    return response.GetResponseStream();
                else
                    return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        private Stream GetFaviconFromSource(string url)
        {
            if (url == string.Empty) return null;
            string iconPath = string.Empty;
            Uri uri = new Uri(url);
            Stream strm;
            try
            {
                strm = WebRequest.Create(uri.Scheme + "://" + uri.Host).GetResponse().GetResponseStream();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
            string webSource = new StreamReader(strm).ReadToEnd();
            string pattern = "<[lL][iI][nN][kK][^>]*>";
            MatchCollection links = Regex.Matches(webSource, pattern);
            foreach (Match link in links)
            {
                Match rel = Regex.Match(link.Value, "[rR][eE][lL]=\"[^\"]*\"");
                Match relName = Regex.Match(rel.Value, "\".*\"");
                string name = relName.Value.Replace("\"", "");
                if (name.ToLower() == "shortcut icon" || name.ToLower() == "icon")
                {
                    Match href = Regex.Match(link.Value, "[hH][rR][eE][fF]=\"[^\"]*\"");
                    Match path = Regex.Match(href.Value, "\".*\"");
                    iconPath = path.Value.Replace("\"", "");
                    break;
                }
            }
            if (iconPath == string.Empty)
                return null;

            WebRequest requestImg;
            if (iconPath.StartsWith("http://") || iconPath.StartsWith("https://") || iconPath.StartsWith("ftp://"))
                requestImg = WebRequest.Create(iconPath);
            else
            {
                string backslash = "";
                if (!iconPath.StartsWith("/"))
                    backslash = "/";
                requestImg = WebRequest.Create(uri.Scheme + "://" + uri.Host + backslash + iconPath);
            }

            try
            {
                return requestImg.GetResponse().GetResponseStream();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        #endregion Favicon Extractor

            
    }
}
