using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSActualnoCOM : IParser
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
            { return "actualno.com"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.ActualnoCom; }
        }

        #endregion Properties

        public RSSActualnoCOM()
        {
            NewsSource nSource;
            
            // Новини от България: "http://rss.actualno.com/bulgaria/"

            nSource = new NewsSource();
            nSource.Title = "Новини от България: Политика";
            nSource.URL = "http://rss.actualno.com/politics";
            nSource.DefCategory = "bulgaria";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Новини от България: Икономика";
            nSource.URL = "http://rss.actualno.com/business/";
            nSource.DefCategory = "business";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Новини от България: Общество";
            nSource.URL = "http://rss.actualno.com/society";
            nSource.DefCategory = "society";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Новини от България: Образование";
            nSource.URL = "http://rss.actualno.com/education";
            nSource.DefCategory = "education";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Новини от България: Крими";
            nSource.URL = "http://rss.actualno.com/crime";
            nSource.DefCategory = "crime";
            _NSource.Add(nSource);

            
            /*
            nSource = new NewsSource();
            nSource.Title = "Техно новини";
            nSource.URL = "http://rss.actualno.com/techno/";
            nSource.DefCategory = "techno";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Спортни новини";
            nSource.URL = "http://rss.actualno.com/sport/";
            nSource.DefCategory = "sport";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Новини от света";
            nSource.URL = "http://rss.actualno.com/world/";
            nSource.DefCategory = "world";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Lifestyle новини";
            nSource.URL = "http://rss.actualno.com/lifestyle/";
            nSource.DefCategory = "lifestyle";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Времето";
            nSource.URL = "http://rss.actualno.com/weather/";
            nSource.DefCategory = "weather";
            _NSource.Add(nSource);
             * */
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
                    // GUID
                    int iFrom = aNItem.Link.LastIndexOf('_') + 1;
                    int iTo = aNItem.Link.LastIndexOf('.');
                    string sGuid = aNItem.Link.Substring(iFrom, iTo - iFrom);
                    aNItem.Guid = Convert.ToInt32(sGuid).ToString();
                    break;
                case "PUBDATE":
                    aNItem.pubDate = Convert.ToDateTime(aNode.Value.Trim());
                    break;
                //case "GUID":
                    //aNItem.Guid = aNode.Value.Trim();
                    //break;
                case "DESCRIPTION":
                    aNItem.Description = "";
                    aNItem.Body = PTools.GetWebText(PTools.RemoveWebTag(aNode.Value));
                    break;
                case "ENCLOSURE":
                    aNItem.Enclosure.Add(new Enclosure(aNode.GetAttribute("url", ""), aNode.GetAttribute("type", "")));
                    break;
                case "CATEGORY":
                    aNItem.Category = aNode.Value.Trim();
                    break;
                case "AUTHOR":
                    aNItem.Author = aNode.Value.Trim();
                    break;
                default:
                    throw new Exception("RSS Element: '" + aNode.Name.ToUpper() + "' !");
            }
            return bRes;
        }
    }
}
