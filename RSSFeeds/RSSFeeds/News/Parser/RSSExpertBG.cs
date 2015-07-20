using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSExpertBG : IParser
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
            { return "expert.bg"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.ExpertBG; }
        }

        #endregion Properties

        public RSSExpertBG()
        {
            NewsSource nSource;

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - Анализи";
            nSource.URL = "http://xml.webground.bg/expert/analisys/";
            nSource.DefCategory = "analisys";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - България";
            nSource.URL = "http://xml.webground.bg/expert/bulgaria/";
            nSource.DefCategory = "bulgaria";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - Енергетика";
            nSource.URL = "http://xml.webground.bg/expert/energetics/";
            nSource.DefCategory = "energetics";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - ЕС";
            nSource.URL = "http://xml.webground.bg/expert/eu/";
            nSource.DefCategory = "eu";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - Здравеопазване";
            nSource.URL = "http://xml.webground.bg/expert/health_services/";
            nSource.DefCategory = "health_services";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - Земеделие";
            nSource.URL = "http://xml.webground.bg/expert/agriculture/";
            nSource.DefCategory = "agriculture";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - Автомобили";
            nSource.URL = "http://xml.webground.bg/expert/automobiles/";
            nSource.DefCategory = "automobiles";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - Потребителски стоки и услуги";
            nSource.URL = "http://xml.webground.bg/expert/user_goods/";
            nSource.DefCategory = "user_goods";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - Лица";
            nSource.URL = "http://xml.webground.bg/expert/faces/";
            nSource.DefCategory = "faces";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - Международни";
            nSource.URL = "http://xml.webground.bg/expert/internationals/";
            nSource.DefCategory = "internationals";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - Младият бизнес";
            nSource.URL = "http://xml.webground.bg/expert/young_business/";
            nSource.DefCategory = "young_business";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - Строителство и имоти";
            nSource.URL = "http://xml.webground.bg/expert/building_properties/";
            nSource.DefCategory = "building_properties";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - Събития";
            nSource.URL = "http://xml.webground.bg/expert/events/";
            nSource.DefCategory = "events";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - Технологии";
            nSource.URL = "http://xml.webground.bg/expert/technologies/";
            nSource.DefCategory = "technologies";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - Транспорт и спедиция";
            nSource.URL = "http://xml.webground.bg/expert/transport/";
            nSource.DefCategory = "transport";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - Труд и социална политика";
            nSource.URL = "http://xml.webground.bg/expert/labour_social_politics/";
            nSource.DefCategory = "labour_social_politics";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - Туризъм";
            nSource.URL = "http://xml.webground.bg/expert/tourism/";
            nSource.DefCategory = "tourism";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Expert.bg - Финанси и застраховане";
            nSource.URL = "http://xml.webground.bg/expert/finances_insurance/";
            nSource.DefCategory = "finances_insurance";
            _NSource.Add(nSource);

            //
            nSource = new NewsSource();
            nSource.Title = "Последни новини";
            nSource.URL = "http://xml.webground.bg/expert/last_news/";
            nSource.DefCategory = "news";
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
                case "GUID":
                    break;
                case "LINK":
                    aNItem.Link = aNode.Value.Trim();
                    if (aNItem.Link.Equals(""))
                    { bRes = false; }
                    break;
                case "PUBDATE":
                    try
                    { aNItem.pubDate = Convert.ToDateTime(aNode.Value.Trim()); }
                    catch
                    { aNItem.pubDate = PTools.GetWebDateTime(DateTime.Now); }
                    // GUID
                    aNItem.Guid = PTools.DateTimeToInt32(aNItem.pubDate).ToString();
                    break;
                case "DESCRIPTION":
                    aNItem.Description = "";
                    aNItem.Body = PTools.GetWebText(PTools.RemoveWebTag(aNode.Value.Trim()));
                    break;
                case "ENCLOSURE":
                    aNItem.Enclosure.Add(new Enclosure(aNode.GetAttribute("url", ""), aNode.GetAttribute("type", "")));
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
