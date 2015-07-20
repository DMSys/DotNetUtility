using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSInvestorBG : IParser
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
            { return "investor.bg"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.InvestorBG; }
        }

        #endregion Properties

        public RSSInvestorBG()
        {
            NewsSource nSource;

            nSource = new NewsSource();
            nSource.Title = "България";
            nSource.URL = "http://rss.investor.bg/c/33288/f/564912/index.rss";
            nSource.DefCategory = "България";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Свят";
            nSource.URL = "http://rss.investor.bg/c/33288/f/564913/index.rss";
            nSource.DefCategory = "World";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Имоти";
            nSource.URL = "http://rss.investor.bg/c/33288/f/564914/index.rss";
            nSource.DefCategory = "RealEstate";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Борса";
            nSource.URL = "http://rss.investor.bg/c/33288/f/564915/index.rss";
            nSource.DefCategory = "Exchange";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Forex";
            nSource.URL = "http://rss.investor.bg/c/33288/f/564916/index.rss";
            nSource.DefCategory = "Forex";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Комуникации";
            nSource.URL = "http://rss.investor.bg/c/33288/f/564917/index.rss";
            nSource.DefCategory = "Communications";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Обучение";
            nSource.URL = "http://rss.investor.bg/c/33288/f/564918/index.rss";
            nSource.DefCategory = "Training";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Style";
            nSource.URL = "http://rss.investor.bg/c/33288/f/564919/index.rss";
            nSource.DefCategory = "Style";
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
                    aNItem.Link = PTools.GetLinkURL(aNode.Value.Trim());
                    if (aNItem.Link.Equals(""))
                    { bRes = false; }
                    break;
                case "GUID":
                    aNItem.Guid = aNode.Value.Trim();
                    break;
                case "DESCRIPTION":
                    aNItem.Description = "";
                    aNItem.Body = PTools.GetWebText(PTools.RemoveWebTag(aNode.Value));
                    break;
                case "PUBDATE":
                    try
                    { aNItem.pubDate = Convert.ToDateTime(aNode.Value.Trim()); }
                    catch
                    { aNItem.pubDate = PTools.GetWebDateTime(DateTime.Now); }
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
