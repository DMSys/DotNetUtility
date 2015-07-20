using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSZarBG : IParser
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
            { return "zar.bg"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.ZarBG; }
        }

        #endregion Properties

        public RSSZarBG()
        {
            NewsSource nSource;

            nSource = new NewsSource();
            nSource.Title = "ZarBG";
            nSource.URL = "http://zar.bg/rss.php";
            nSource.DefCategory = "1";
            _NSource.Add(nSource);
        }

        public void Dispose()
        {
            _NSource.Clear();
        }

        public bool ParseNews(NewsItem aNItem, XPathNavigator aNode)
        {
            bool bRes = true;
            //
            switch (aNode.Name.ToUpper())
            {
                case "TITLE":
                    aNItem.Title = PTools.GetWebText(aNode.Value);
                    if (aNItem.Title.Equals(""))
                    { bRes = false; }
                    break;
                case "LINK":
                    aNItem.Link = aNode.Value.Trim();
                    if (aNItem.Link.Equals(""))
                    { bRes = false; }
                    break;
                case "PUBDATE":
                    aNItem.pubDate = Convert.ToDateTime(aNode.Value.Trim());
                    break;
                case "GUID":
                    string sGValue = aNode.Value.Trim();
                    int iFrom = sGValue.LastIndexOf('=') + 1;
                    aNItem.Guid = Convert.ToInt32(sGValue.Substring(iFrom, sGValue.Length - iFrom)).ToString();
                    break;
                case "CATEGORY":
                    aNItem.Category = aNode.Value.Trim();
                    break;
                case "DESCRIPTION":
                    aNItem.Description = "";
                    string sValue = aNode.Value.Trim();
                    int iTextFrom = sValue.LastIndexOf(">") + 1;
                    //aNItem.Body = PTools.GetWebText(PTools.RemoveWebTag(sValue.Substring(iTextFrom, sValue.Length - iTextFrom).Trim()));
                    aNItem.Body = PTools.GetWebText(PTools.RemoveWebTag(sValue));
                    //
                    if (sValue.Substring(0, 10).Equals("<img src=\""))
                    {
                        int iIMGFrom = 10;
                        int iIMGTo = 0;
                        for (int i = iIMGFrom; i < iTextFrom; i++)
                        {
                            if ((iIMGTo == 0) && sValue.Substring(i, 1).Equals("\""))
                            { iIMGTo = i; }
                        }
                        if (iIMGTo > iIMGFrom)
                        {
                            aNItem.Enclosure.Add(new Enclosure(sValue.Substring(iIMGFrom, iIMGTo - iIMGFrom).Trim()));
                        }
                    }
                    break;
                default:
                    throw new Exception("RSS Element: '" + aNode.Name.ToUpper() + "' !");
            }
            return bRes;
        }
    }
}
