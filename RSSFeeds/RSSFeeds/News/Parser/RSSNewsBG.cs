using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSFeedsBG : IParser
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
            { return "news.bg"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.NewsBG; }
        }

        #endregion Properties

        public RSSFeedsBG()
        {
            NewsSource nSource;

            nSource = new NewsSource();
            nSource.Title = "У нас";
            nSource.URL = "http://news.ibox.bg/rss_2";
            nSource.DefCategory = "2";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "У нас: Политика";
            nSource.URL = "http://news.ibox.bg/rss_9";
            nSource.DefCategory = "9";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "У нас: Криминално";
            nSource.URL = "http://news.ibox.bg/rss_10";
            nSource.DefCategory = "10";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "У нас: Съд и прокуратура";
            nSource.URL = "http://news.ibox.bg/rss_11";
            nSource.DefCategory = "11";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "У нас: Здравеопазване";
            nSource.URL = "http://news.ibox.bg/rss_12";
            nSource.DefCategory = "12";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "У нас: Образование и наука";
            nSource.URL = "http://news.ibox.bg/rss_13";
            nSource.DefCategory = "";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "У нас: Медии";
            nSource.URL = "http://news.ibox.bg/rss_14";
            nSource.DefCategory = "14";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "У нас: Екология";
            nSource.URL = "http://news.ibox.bg/rss_15";
            nSource.DefCategory = "15";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "У нас: Региони";
            nSource.URL = "http://news.ibox.bg/rss_16";
            nSource.DefCategory = "16";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "У нас: Религия";
            nSource.URL = "http://news.ibox.bg/rss_17";
            nSource.DefCategory = "17";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "По света";
            nSource.URL = "http://news.ibox.bg/rss_6";
            nSource.DefCategory = "6";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "По света: Политика";
            nSource.URL = "http://news.ibox.bg/rss_26";
            nSource.DefCategory = "26";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "По света: Икономика";
            nSource.URL = "http://news.ibox.bg/rss_27";
            nSource.DefCategory = "27";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "По света: Конфликти";
            nSource.URL = "http://news.ibox.bg/rss_29";
            nSource.DefCategory = "29";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "По света: Бедствия и аварии";
            nSource.URL = "http://news.ibox.bg/rss_30";
            nSource.DefCategory = "30";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "По света: Любопитно";
            nSource.URL = "http://news.ibox.bg/rss_31";
            nSource.DefCategory = "31";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Технологии";
            nSource.URL = "http://news.ibox.bg/rss_7";
            nSource.DefCategory = "7";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Технологии: Интернет";
            nSource.URL = "http://news.ibox.bg/rss_32";
            nSource.DefCategory = "32";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Технологии: Комуникации";
            nSource.URL = "http://news.ibox.bg/rss_33";
            nSource.DefCategory = "33";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Технологии: Открития";
            nSource.URL = "http://news.ibox.bg/rss_34";
            nSource.DefCategory = "34";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Технологии: Космос";
            nSource.URL = "http://news.ibox.bg/rss_35";
            nSource.DefCategory = "35";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Технологии: Автомобили";
            nSource.URL = "http://news.ibox.bg/rss_36";
            nSource.DefCategory = "36";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Икономика";
            nSource.URL = "http://news.ibox.bg/rss_4";
            nSource.DefCategory = "4";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Икономика: Финанси";
            nSource.URL = "http://news.ibox.bg/rss_5";
            nSource.DefCategory = "5";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Икономика: Индустрия";
            nSource.URL = "http://news.ibox.bg/rss_18";
            nSource.DefCategory = "18";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Икономика: Земеделие";
            nSource.URL = "http://news.ibox.bg/rss_19";
            nSource.DefCategory = "19";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Икономика: Търговия";
            nSource.URL = "http://news.ibox.bg/rss_21";
            nSource.DefCategory = "21";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Икономика: Транспорт";
            nSource.URL = "http://news.ibox.bg/rss_22";
            nSource.DefCategory = "22";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Икономика: Туризъм";
            nSource.URL = "http://news.ibox.bg/rss_23";
            nSource.DefCategory = "23";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Икономика: Телекомуникации";
            nSource.URL = "http://news.ibox.bg/rss_24";
            nSource.DefCategory = "24";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Икономика: Синдикати";
            nSource.URL = "http://news.ibox.bg/rss_25";
            nSource.DefCategory = "25";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Икономика: Бизнесмен на България";
            nSource.URL = "http://news.ibox.bg/rss_83";
            nSource.DefCategory = "83";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Култура";
            nSource.URL = "http://news.ibox.bg/rss_8";
            nSource.DefCategory = "8";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Култура: Кино";
            nSource.URL = "http://news.ibox.bg/rss_38";
            nSource.DefCategory = "38";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Култура: Музика";
            nSource.URL = "http://news.ibox.bg/rss_37";
            nSource.DefCategory = "37";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Култура: Книги";
            nSource.URL = "http://news.ibox.bg/rss_39";
            nSource.DefCategory = "39";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Култура: Сцена";
            nSource.URL = "http://news.ibox.bg/rss_40";
            nSource.DefCategory = "40";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Култура: Палитра";
            nSource.URL = "http://news.ibox.bg/rss_41";
            nSource.DefCategory = "41";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Култура: Любопитно";
            nSource.URL = "http://news.ibox.bg/rss_42";
            nSource.DefCategory = "42";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Времето";
            nSource.URL = "http://news.ibox.bg/rss_28";
            nSource.DefCategory = "28";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Начало";
            nSource.URL = "http://news.ibox.bg/rss_1";
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
                    aNItem.Guid = Convert.ToInt32(aNode.Value.Trim()).ToString();
                    break;
                case "CATEGORY":
                    aNItem.Category = aNode.Value.Trim();
                    break;
                case "DESCRIPTION":
                    aNItem.Description = PTools.GetWebText(PTools.RemoveWebTag(aNode.Value));
                    break;
                case "BODY":
                    aNItem.Body = PTools.GetWebText(PTools.RemoveWebTag(aNode.Value));
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
