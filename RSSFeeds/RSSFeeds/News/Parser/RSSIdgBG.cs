using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSIdgBG : IParser
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
            { return "idg.bg"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.IdgBG; }
        }

        #endregion Properties

        public RSSIdgBG()
        {
            NewsSource nSource;

            // ÕŒ¬»Õ»
            nSource = new NewsSource();
            nSource.Title = "»Õ“≈–Õ≈“";
            nSource.URL = "http://idg.bg/online/idg_internet";
            nSource.DefCategory = "idg_internet";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "—»√”–ÕŒ—“";
            nSource.URL = "http://idg.bg/online/idg_security";
            nSource.DefCategory = "idg_security";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "ÕŒ¬» œ–Œƒ” “» » “≈’ÕŒÀŒ√»»";
            nSource.URL = "http://idg.bg/online/idg_newtech";
            nSource.DefCategory = "idg_newtech";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Ã–≈∆» »  ŒÃ”Õ» ¿÷»»";
            nSource.URL = "http://idg.bg/online/idg_comunications";
            nSource.DefCategory = "idg_comunications";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "–¿«¬À≈◊≈Õ»≈";
            nSource.URL = "http://idg.bg/online/idg_entertainment";
            nSource.DefCategory = "idg_entertainment";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "’¿–ƒ”≈–";
            nSource.URL = "http://idg.bg/online/idg_hardware";
            nSource.DefCategory = "idg_hardware";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "—Œ‘“”≈–";
            nSource.URL = "http://idg.bg/online/idg_software";
            nSource.DefCategory = "idg_software";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "¡»«Õ≈—";
            nSource.URL = "http://idg.bg/online/idg_business";
            nSource.DefCategory = "idg_business";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "ÕŒ¬»Õ»";
            nSource.URL = "http://idg.bg/online/idgbgonline";
            nSource.DefCategory = "idgbgonline";
            _NSource.Add(nSource);

            //
            nSource = new NewsSource();
            nSource.Title = "IDG.bg - ÔÓ„‡ÏË";
            nSource.URL = "http://idg.bg/online/idg_download";
            nSource.DefCategory = "idg_download";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "IDG.bg - Ò‡ÈÚÓ‚Â";
            nSource.URL = "http://idg.bg/online/idg_links";
            nSource.DefCategory = "idg_links";
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
                    // Enclosure
                    string sIMG = PTools.GetEnclosure(aNode.Value);
                    if ( !sIMG.Equals(sIMG.Replace("http://feeds.feedburner.com/~", "")) )
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
