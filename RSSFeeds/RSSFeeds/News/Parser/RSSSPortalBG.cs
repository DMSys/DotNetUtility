using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSSPortalBG : IParser
    {
        #region Properties

        private List<NewsSource> _NSource = new List<NewsSource>();
        public List<NewsSource> NSource
        {
            get
            { return _NSource; }
        }

        public string WSait
        {
            get
            { return "sportal.bg"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.SPortalBG; }
        }

        #endregion Properties

        public RSSSPortalBG()
        {
            NewsSource nSource;

            nSource = new NewsSource();
            nSource.Title = "Формула 1";
            nSource.URL = "http://www.sportal.bg/uploads/rss_category_8.xml";
            nSource.DefCategory = "8";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Волейбол";
            nSource.URL = "http://www.sportal.bg/uploads/rss_category_5.xml";
            nSource.DefCategory = "5";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "БГ Футбол";
            nSource.URL = "http://www.sportal.bg/uploads/rss_category_1.xml";
            nSource.DefCategory = "1";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Европейски футбол";
            nSource.URL = "http://www.sportal.bg/uploads/rss_category_2.xml";
            nSource.DefCategory = "2";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Световен футбол";
            nSource.URL = "http://www.sportal.bg/uploads/rss_category_120.xml";
            nSource.DefCategory = "120";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Баскетбол";
            nSource.URL = "http://www.sportal.bg/uploads/rss_category_3.xml";
            nSource.DefCategory = "3";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Тенис";
            nSource.URL = "http://www.sportal.bg/uploads/rss_category_6.xml";
            nSource.DefCategory = "6";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Моторни спортове";
            nSource.URL = "http://www.sportal.bg/uploads/rss_category_73.xml";
            nSource.DefCategory = "73";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Булевард";
            nSource.URL = "http://www.sportal.bg/uploads/rss_category_35.xml";
            nSource.DefCategory = "35";
            _NSource.Add(nSource);
        }

        public void Dispose()
        {
            _NSource.Clear();
        }

        public bool ParseNews(NewsItem aNItem, XPathNavigator aNode)
        {
            bool bRes = true;
            switch (aNode.Name.ToUpper())
            {
                case "TITLE":
                    aNItem.Title = PTools.GetWebText(PTools.RemoveWebTag(aNode.Value));
                    if (aNItem.Title.Equals(""))
                    { bRes = false; }
                    break;
                case "LINK":
                    aNItem.Link = aNode.Value.Trim();
                    if (aNItem.Link.Equals(""))
                    { bRes = false; }
                    // GUID
                    try
                    {
                        string sGValue = aNode.Value.Trim();
                        int iFrom = sGValue.LastIndexOf('=') + 1;
                        aNItem.Guid = Convert.ToInt32(sGValue.Substring(iFrom, sGValue.Length - iFrom)).ToString();
                    }
                    catch
                    { bRes = false; }
                    break;
                case "PUBDATE":
                    string sDT = aNode.Value.Trim();
                    if (sDT.Length == 14)
                    {
                        int iY = Convert.ToInt32(sDT.Substring(0, 4));
                        int iM = Convert.ToInt32(sDT.Substring(4, 2));
                        int iD = Convert.ToInt32(sDT.Substring(6, 2));
                        int iH = Convert.ToInt32(sDT.Substring(8, 2));
                        int iMin = Convert.ToInt32(sDT.Substring(10, 2));
                        int iSec = Convert.ToInt32(sDT.Substring(12, 2));
                        aNItem.pubDate = new DateTime(iY, iM, iD, iH, iMin, iSec);
                    }
                    else
                    {
                        aNItem.pubDate = DateTime.Now;
                    }
                    break;
                case "GUID":
                    break;
                case "CATEGORY":
                    aNItem.Category = aNode.Value.Trim();
                    if (aNItem.Category.Equals(""))
                    { bRes = false; }
                    break;
                case "DESCRIPTION":
                    aNItem.Description = "";
                    aNItem.Body = PTools.GetWebText(PTools.RemoveWebTag(aNode.Value.Trim()));
                    break;
                case "ENCLOSURE":
                    aNItem.Enclosure.Add(new Enclosure(aNode.GetAttribute("url", ""), aNode.GetAttribute("type", "")));
                    break;
                default:
                    throw new Exception("RSS Element: '" + aNode.Name.ToUpper() + "' !");
            }
            return bRes;
        }
    }
}
