using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace RSSFeeds
{
    public class HTMLDocNavigator:IDisposable
    {
        private string _InnerText = "";
        public string InnerText
        {
            get
            { return _InnerText; }
        }

        private HTMLDocNode _DNSelected;
        public string SelectedText
        {
            get
            { return _DNSelected.InnerText; }
        }

        private HTMLDocNode _DNChild;
        public string ChildText
        {
            get
            { return _DNChild.InnerText; }
        }

        public HTMLDocNavigator()
        { }

        public void Dispose()
        {
            _InnerText = "";
        }

        /// <summary>
        /// Зарежда страницата
        /// </summary>
        public void LoadUrl(string aUrl)
        {
            //
            _DNSelected.Clear();
            _DNChild.Clear();
            //
            StreamReader Reader = null;
            Stream WebStream = null;
            WebResponse Response = null;
            try
            {
                HttpWebRequest WebRequestObject = (HttpWebRequest)HttpWebRequest.Create(aUrl);

                WebRequestObject.UserAgent = "DMSys Web";
                WebRequestObject.Referer = "http://dmsys.co.cc/";

                Response = WebRequestObject.GetResponse();

                WebStream = Response.GetResponseStream();

                Reader = new StreamReader(WebStream);

                _InnerText = Reader.ReadToEnd();
            }
            finally
            {
                // Cleanup
                if (Reader != null)
                { Reader.Close(); }
                if (WebStream != null)
                { WebStream.Close(); }
                if (Response != null)
                { Response.Close(); }
            }
        }

        /// <summary>
        /// Зарежда страницата
        /// </summary>
        public void LoadFile(string aPath)
        {
            //
            _DNSelected.Clear();
            _DNChild.Clear();
            //
            StreamReader Reader = null;
            try
            {
                Reader = new StreamReader(aPath);
                
                _InnerText = Reader.ReadToEnd();
            }
            finally
            {
                // Cleanup
                if (Reader != null)
                { Reader.Close(); }
            }
        }

        /// <summary>
        /// Зарежда страницата
        /// </summary>
        public void LoadValue(string aValue)
        {
            //
            _DNSelected.Clear();
            _DNChild.Clear();
            //
            _InnerText = aValue;
        }

        public void Select(string aPath)
        {
            string[] mPath = aPath.Split('/');

            _DNSelected.InnerText = _InnerText.Trim();

            foreach (string sTag in mPath)
            {
                if (sTag != "")
                {
                    _DNSelected = GetTagValue(_DNSelected.InnerText, 0, sTag);
                }
            }
        }

        /// <summary>
        /// Дава съдържанието на тага
        /// </summary>
        private HTMLDocNode GetTagValue(string aPContent, int aFromIndex, string aTag)
        {
            bool bFBeginTag = false;
            bool bFBeginValue = false;
            bool bFEndValue = false;
            //
            HTMLDocNode dnPContent;
            dnPContent.InnerText = "";
            dnPContent.TagBegin = 0;
            dnPContent.TagEnd = 0;
            dnPContent.TagValueBegin = 0;
            dnPContent.TagValueEnd = 0;
            Int32 iSubTagCount = 0;
            //
            for (Int32 i = aFromIndex; (i < (aPContent.Length - aTag.Length - 2)) && (!bFEndValue); i++)
            {
                // Начло на тага
                if (!bFBeginTag)
                {
                    if (aPContent.Substring(i, aTag.Length + 1).Equals("<" + aTag))
                    {
                        bFBeginTag = true;
                        dnPContent.TagBegin = i; 
                        i += aTag.Length;
                    }
                }
                // Начало на съдържанието на тага
                else if (!bFBeginValue)
                {
                    if (aPContent[i].Equals('>'))
                    {
                        bFBeginValue = true;
                        dnPContent.TagValueBegin = i + 1;

                        // Проверява дали е край
                        if (aPContent[i - 1].Equals('/'))
                        {
                            bFEndValue = true;
                            dnPContent.TagValueEnd = i + 1;
                            dnPContent.TagEnd = i + 1;
                        }
                    }
                }
                // Края на тага
                else
                {
                    // Има вложен таг
                    if (aPContent.Substring(i, aTag.Length + 1).Equals("<" + aTag))
                    {
                        iSubTagCount += 1;
                    }
                    // Край на таг
                    else if (aPContent.Substring(i, aTag.Length + 2).Equals("</" + aTag))
                    {
                        if (iSubTagCount > 0)
                        {
                            iSubTagCount -= 1;
                        }
                        else
                        {
                            bFEndValue = true;
                            dnPContent.TagValueEnd = i;
                            dnPContent.TagEnd = i + aTag.Length + 3;
                        }
                    }
                }
            }
            //
            if (!bFBeginTag)
            {
                dnPContent.InnerText = "";
                dnPContent.TagValueBegin = 0;
                dnPContent.TagValueEnd = 0;
            }
            else
            {
                if (dnPContent.TagValueEnd < dnPContent.TagValueBegin)
                { dnPContent.TagValueEnd = aPContent.Length; }
                dnPContent.InnerText = aPContent.Substring(dnPContent.TagValueBegin, dnPContent.TagValueEnd - dnPContent.TagValueBegin).Trim();
            }
            //
            return dnPContent;
        }

        public bool MoveNext( string aValue )
        {
            _DNChild = GetTagValue(_DNSelected.InnerText, _DNChild.TagValueEnd, aValue);

            int iEnd = _DNChild.TagValueEnd + aValue.Length + 3;
            return ( iEnd < _InnerText.Length);
        }

        public bool MoveNext(string aValue, int iCount)
        {
            for (int i = 0; i < iCount; i++)
            {
                if (!MoveNext(aValue))
                { return false; }
            }
            return true;
        }
    }

    public struct HTMLDocNode
    {
        public string InnerText;
        public int TagBegin;
        public int TagEnd;
        public int TagValueBegin;
        public int TagValueEnd;

        public void Clear()
        { 
            InnerText = "";
            TagBegin = 0;
            TagEnd = 0;
            TagValueBegin = 0;
            TagValueEnd = 0;
        }
    }
}
