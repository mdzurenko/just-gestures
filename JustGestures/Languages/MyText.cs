using System;
using System.Collections.Generic;
using System.Text;

namespace JustGestures.Languages
{
    public class MyText : ITranslation
    {
        string m_name;
        public string Name { get { return m_name; } }

        string m_text = string.Empty;
        private string Text { get { return m_text; } }

        public MyText(string name)
        {
            m_name = name;
            Translate();
        }
        public static implicit operator String(MyText phrase)
        {
            return phrase.m_text;
        }
        

        #region ITranslation Members

        public void Translate()
        {
            m_text = Translation.GetText(m_name).Replace("{n}", Environment.NewLine);
        }

        #endregion
    }
}
