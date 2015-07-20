using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSDarikNewsBG : IParser
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
            { return "dariknews.bg"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.DarikNewsBG; }
        }

        #endregion Properties

        public RSSDarikNewsBG()
        {
            NewsSource nSource;

            nSource = new NewsSource();
            nSource.Title = "България";
            nSource.URL = "http://dariknews.bg/rss.php?cat=1";
            nSource.DefCategory = "1";
            _NSource.Add(nSource);
            
            nSource = new NewsSource();
            nSource.Title = "Свят";
            nSource.URL = "http://dariknews.bg/rss.php?cat=4";
            nSource.DefCategory = "4";
            _NSource.Add(nSource);
            
            nSource = new NewsSource();
            nSource.Title = "Спорт";
            nSource.URL = "http://dariknews.bg/rss.php?cat=3";
            nSource.DefCategory = "3";
            _NSource.Add(nSource);
            
            nSource = new NewsSource();
            nSource.Title = "Бизнес";
            nSource.URL = "http://dariknews.bg/rss.php?cat=2";
            nSource.DefCategory = "2";
            _NSource.Add(nSource);
            
            nSource = new NewsSource();
            nSource.Title = "Общество";
            nSource.URL = "http://dariknews.bg/rss.php?cat=5";
            nSource.DefCategory = "5";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Технологии";
            nSource.URL = "http://dariknews.bg/rss.php?cat=9";
            nSource.DefCategory = "9";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Любопитно";
            nSource.URL = "http://dariknews.bg/rss.php?cat=8";
            nSource.DefCategory = "8";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "София";
            nSource.URL = "http://dariknews.bg/rss.php?cat=36";
            nSource.DefCategory = "36";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Варна";
            nSource.URL = "http://dariknews.bg/rss.php?cat=18";
            nSource.DefCategory = "18";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Пловдив";
            nSource.URL = "http://dariknews.bg/rss.php?cat=19";
            nSource.DefCategory = "19";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Благоевград";
            nSource.URL = "http://dariknews.bg/rss.php?cat=20";
            nSource.DefCategory = "20";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Сливен";
            nSource.URL = "http://dariknews.bg/rss.php?cat=21";
            nSource.DefCategory = "21";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Хасково";
            nSource.URL = "http://dariknews.bg/rss.php?cat=22";
            nSource.DefCategory = "22";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Русе";
            nSource.URL = "http://dariknews.bg/rss.php?cat=23";
            nSource.DefCategory = "23";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Бургас";
            nSource.URL = "http://dariknews.bg/rss.php?cat=24";
            nSource.DefCategory = "24";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Габрово";
            nSource.URL = "http://dariknews.bg/rss.php?cat=25";
            nSource.DefCategory = "25";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Добрич";
            nSource.URL = "http://dariknews.bg/rss.php?cat=26";
            nSource.DefCategory = "26";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Враца";
            nSource.URL = "http://dariknews.bg/rss.php?cat=28";
            nSource.DefCategory = "28";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Кюстендил";
            nSource.URL = "http://dariknews.bg/rss.php?cat=29";
            nSource.DefCategory = "29";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Плевен";
            nSource.URL = "http://dariknews.bg/rss.php?cat=30";
            nSource.DefCategory = "30";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Велико Търново";
            nSource.URL = "http://dariknews.bg/rss.php?cat=31";
            nSource.DefCategory = "31";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Стара Загора";
            nSource.URL = "http://dariknews.bg/rss.php?cat=32";
            nSource.DefCategory = "32";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Кърджали";
            nSource.URL = "http://dariknews.bg/rss.php?cat=3";
            nSource.DefCategory = "3";
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
