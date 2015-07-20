using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSDnesBG : IParser
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
            { return "dnes.bg"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.DnesBG; }
        }

        #endregion Properties

        public RSSDnesBG()
        {
            NewsSource nSource;

            nSource = new NewsSource();
            nSource.Title = "България";
            nSource.URL = "http://www.dnes.bg/rss.php?cat=1";
            nSource.DefCategory = "1";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Бизнес";
            nSource.URL = "http://www.dnes.bg/rss.php?cat=3";
            nSource.DefCategory = "3";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Свят";
            nSource.URL = "http://www.dnes.bg/rss.php?cat=2";
            nSource.DefCategory = "2";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Крими";
            nSource.URL = "http://www.dnes.bg/rss.php?cat=6";
            nSource.DefCategory = "6";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Спорт";
            nSource.URL = "http://www.dnes.bg/rss.php?cat=9";
            nSource.DefCategory = "9";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Наука и технологии";
            nSource.URL = "http://www.dnes.bg/rss.php?cat=4";
            nSource.DefCategory = "4";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Миш-маш";
            nSource.URL = "http://www.dnes.bg/rss.php?cat=7";
            nSource.DefCategory = "7";
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
                    string sValue = aNode.Value.Trim();
                    // Guid
                    int iFrom = sValue.LastIndexOf('.') + 1;
                    aNItem.Guid = Convert.ToInt32(sValue.Substring(iFrom, sValue.Length - iFrom)).ToString();
                    // Category
                    string[] sCategory = sValue.Split('/');
                    aNItem.Category = sCategory[3];
                    break;
                case "DESCRIPTION":
                    aNItem.Description = "";
                    aNItem.Body = PTools.GetWebText(PTools.RemoveWebTag(aNode.Value));
                    break;
                case "IMG":
                    aNItem.Enclosure.Add(new Enclosure(aNode.Value.Trim()));
                    break;
                case "DATE":
                case "DC:DATE":
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
