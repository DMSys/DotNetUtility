using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSComputerWorldBG : IParser
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
            { return "computerworld.bg"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.ComputerWorldBG; }
        }

        #endregion Properties

        public RSSComputerWorldBG()
        {
            NewsSource nSource;

            nSource = new NewsSource();
            nSource.Title = "ИТ новини";
            nSource.URL = "http://computerworld.bg/rss/cwnews.xml";
            nSource.DefCategory = "cwnews";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Вести";
            nSource.URL = "http://computerworld.bg/rss/cwvesti.xml";
            nSource.DefCategory = "cwvesti";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Бизнес новини";
            nSource.URL = "http://computerworld.bg/rss/cwbiznes.xml";
            nSource.DefCategory = "cwbiznes";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Хардуер";
            nSource.URL = "http://computerworld.bg/rss/cwharduer.xml";
            nSource.DefCategory = "cwharduer";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Софтуер";
            nSource.URL = "http://computerworld.bg/rss/cwsoftuer.xml";
            nSource.DefCategory = "cwsoftuer";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Телекомуникации";
            nSource.URL = "http://computerworld.bg/rss/cwtelekomunikacii.xml";
            nSource.DefCategory = "cwtelekomunikacii";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Комуникации";
            nSource.URL = "http://computerworld.bg/rss/cwkomunikacii.xml";
            nSource.DefCategory = "cwkomunikacii";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "ИТ кариера";
            nSource.URL = "http://computerworld.bg/rss/cwit_karieri.xml";
            nSource.DefCategory = "cwit_karieri";
            _NSource.Add(nSource);

            
            nSource = new NewsSource();
            nSource.Title = "е-Администрация";
            nSource.URL = "http://computerworld.bg/rss/cwe-administracia.xml";
            nSource.DefCategory = "cwe-administracia";
            _NSource.Add(nSource);

            
            nSource = new NewsSource();
            nSource.Title = "Наука и техника";
            nSource.URL = "http://computerworld.bg/rss/cwnauka_i_tehnika.xml";
            nSource.DefCategory = "cwnauka_i_tehnika";
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
                case "LINK":
                    break;
                case "FEEDBURNER:ORIGLINK":
                    aNItem.Link = aNode.Value.Trim();
                    if (aNItem.Link.Equals(""))
                    { bRes = false; }
                    break;
                case "PUBDATE":
                    try
                    { aNItem.pubDate = Convert.ToDateTime(aNode.Value.Trim()); }
                    catch
                    { aNItem.pubDate = PTools.GetWebDateTime(DateTime.Now); }
                    break;
                case "GUID":
                    try
                    {
                        string sGValue = aNode.Value.Replace("newsid", "").Trim();
                        aNItem.Guid = Convert.ToInt32(sGValue).ToString();
                    }
                    catch
                    { bRes = false; }
                    break;
                case "DESCRIPTION":
                    aNItem.Description = "";
                    aNItem.Body = PTools.GetWebText(PTools.RemoveWebTag(aNode.Value.Trim()));
                    //
                    string sIMG = PTools.GetEnclosure(aNode.Value.Trim());
                    if ( !sIMG.Equals(sIMG.Replace("http://feeds.feedburner.com/~ff/", "")) )
                    { sIMG = ""; }
                    if (sIMG != "")
                    { aNItem.Enclosure.Add(new Enclosure(sIMG)); }
                    break;
                default:
                    throw new Exception("RSS Element: '" + aNode.Name.ToUpper() + "' !");
            }
            return bRes;
        }
    }
}
