using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSHiCommBG : IParser
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
            { return "hicomm.bg"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.HiCommBG; }
        }

        #endregion Properties

        public RSSHiCommBG()
        {
            NewsSource nSource;

            nSource = new NewsSource();
            nSource.Title = "HiCommBG";
            nSource.URL = "http://hicomm.bg/bg/rss/";
            nSource.DefCategory = "HiCommBG";
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
                    aNItem.Title = PTools.GetWebText(PTools.RemoveWebTag(aNode.Value));
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
                    aNItem.Guid = aNode.Value.Trim();
                    break;
                case "CATEGORY":
                    aNItem.Category = aNode.Value.Trim();
                    if (aNItem.Category.Equals(""))
                    { bRes = false; }
                    break;
                case "DESCRIPTION":
                    aNItem.Description = PTools.GetWebText(PTools.RemoveWebTag(aNode.Value.Trim()));
                    break;
                case "BODY":
                    aNItem.Body = PTools.GetWebText(PTools.RemoveWebTag(aNode.Value.Trim()));
                    break;
                case "AUTHOR":
                    aNItem.Author = aNode.Value.Trim();
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
