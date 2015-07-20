using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSBGFactorORG : IParser
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
            { return "bgfactor.org"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.BGFactorORG; }
        }

        #endregion Properties

        public RSSBGFactorORG()
        {
            NewsSource nSource;

            nSource = new NewsSource();
            nSource.Title = "Водещите материали";
            nSource.URL = "http://www.bgfactor.org/rss/leading.xml";
            nSource.DefCategory = "leading";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Наука и техника";
            nSource.URL = "http://www.bgfactor.org/rss/sci_tech.xml";
            nSource.DefCategory = "sci_tech";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Любопитно";
            nSource.URL = "http://www.bgfactor.org/rss/znaete_li.xml";
            nSource.DefCategory = "znaete_li";
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
                    aNItem.Title = aNode.Value.Trim();
                    if (aNItem.Title.Equals(""))
                    { bRes = false; }
                    break;
                case "LINK":
                    aNItem.Link = aNode.Value.Trim().Replace("&", "&amp;");
                    if (aNItem.Link.Equals(""))
                    { bRes = false; }
                    int iFrom = aNItem.Link.LastIndexOf('=') + 1;
                    string sGuid = aNItem.Link.Substring(iFrom, aNItem.Link.Length - iFrom);
                    aNItem.Guid = sGuid;
                    break;
                case "DATE":
                    aNItem.pubDate = Convert.ToDateTime(aNode.Value.Trim());
                    break;
                case "DESCRIPTION":
                    aNItem.Description = "";
                    aNItem.Body = PTools.GetWebText(aNode.Value);
                    break;
                case "IMAGE":
                    aNItem.Enclosure.Add(new Enclosure(aNode.Value.Trim()));
                    break;
                case "CATEGORY":
                    aNItem.Category = aNode.Value.Trim();
                    break;
                default:
                    throw new Exception("RSS Element: '" + aNode.Name.ToUpper() + "' !");
            }
            return bRes;
        }
    }
}
