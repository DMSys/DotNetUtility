using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSDirBG : IParser
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
            { return "dir.bg"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.DirBG; }
        }

        #endregion Properties

        public RSSDirBG()
        {
            NewsSource nSource;

            nSource = new NewsSource();
            nSource.Title = "life.dir.bg";
            nSource.URL = "http://life.dir.bg/rss20.xml";
            nSource.DefCategory = "life";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "dnes.dir.bg";
            nSource.URL = "http://dnes.dir.bg/rss20.xml";
            nSource.DefCategory = "dnes";
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
                    aNItem.Guid = PTools.GetMD5Hash(aNode.Value.Trim());
                    break;
                case "CATEGORY":
                    aNItem.Category = aNode.Value.Trim();
                    break;
                case "DESCRIPTION":
                    aNItem.Description = "";
                    aNItem.Body = PTools.GetWebText(aNode.Value);
                    break;
                case "ENCLOSURE":
                    aNItem.Enclosure.Add(new Enclosure(aNode.GetAttribute("url", ""), aNode.GetAttribute("type", "")));
                    break;
                case "AUTHOR":
                    break;
                default:
                    throw new Exception("RSS Element: '" + aNode.Name.ToUpper() + "' !");
            }
            return bRes;
        }
    }
}
