using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSHotNewsBG : IParser
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
            { return "hotnews.bg"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.HotNewsBG; }
        }

        #endregion Properties

        public RSSHotNewsBG()
        {
            NewsSource nSource;

            nSource = new NewsSource();
            nSource.Title = "Звездите";
            nSource.URL = "http://hotnews.bg/rss.php?type=1";
            nSource.DefCategory = "1";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "На микрофона";
            nSource.URL = "http://hotnews.bg/rss.php?type=11";
            nSource.DefCategory = "11";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Hollywood";
            nSource.URL = "http://hotnews.bg/rss.php?type=7";
            nSource.DefCategory = "7";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Фолк";
            nSource.URL = "http://hotnews.bg/rss.php?type=6";
            nSource.DefCategory = "6";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Риалити";
            nSource.URL = "http://hotnews.bg/rss.php?type=13";
            nSource.DefCategory = "13";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Събития";
            nSource.URL = "http://hotnews.bg/rss.php?type=14";
            nSource.DefCategory = "14";
            _NSource.Add(nSource);
            
            nSource = new NewsSource();
            nSource.Title = "Издънки";
            nSource.URL = "http://hotnews.bg/rss.php?type=15";
            nSource.DefCategory = "15";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Top stories";
            nSource.URL = "http://hotnews.bg/rss.php?type=8";
            nSource.DefCategory = "8";
            _NSource.Add(nSource);
            
            nSource = new NewsSource();
            nSource.Title = "Fashionista";
            nSource.URL = "http://hotnews.bg/rss.php?type=16";
            nSource.DefCategory = "16";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Любопитно";
            nSource.URL = "http://hotnews.bg/rss.php?type=2";
            nSource.DefCategory = "2";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Hot";
            nSource.URL = "http://hotnews.bg/rss.php?type=18";
            nSource.DefCategory = "18";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Спорт";
            nSource.URL = "http://hotnews.bg/rss.php?type=19";
            nSource.DefCategory = "19";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Криминално";
            nSource.URL = "http://hotnews.bg/rss.php?type=20";
            nSource.DefCategory = "20";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Интимно";
            nSource.URL = "http://hotnews.bg/rss.php?type=4";
            nSource.DefCategory = "4";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Съдби";
            nSource.URL = "http://hotnews.bg/rss.php?type=5";
            nSource.DefCategory = "5";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Красота";
            nSource.URL = "http://hotnews.bg/rss.php?type=10";
            nSource.DefCategory = "10";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Култура";
            nSource.URL = "http://hotnews.bg/rss.php?type=12";
            nSource.DefCategory = "12";
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
                    // GUID
                    try
                    {
                        string sGValue = aNode.Value.Trim();
                        int iFrom = sGValue.LastIndexOf('.');
                        sGValue = sGValue.Substring(0, iFrom);
                        iFrom = sGValue.LastIndexOf('.') + 1;
                        aNItem.Guid = Convert.ToInt32(sGValue.Substring(iFrom, sGValue.Length - iFrom)).ToString();
                    }
                    catch
                    { bRes = false; }
                    break;
                case "PUBDATE":
                    aNItem.pubDate = Convert.ToDateTime(aNode.Value.Trim());
                    break;
                case "GUID":
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
