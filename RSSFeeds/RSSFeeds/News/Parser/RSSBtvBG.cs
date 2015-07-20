using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSBtvBG : IParser
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
            { return "btv.bg"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.BTvBG; }
        }

        #endregion Properties

        public RSSBtvBG()
        {
            NewsSource nSource;

            nSource = new NewsSource();
            nSource.Title = "btv.bg/action";
            nSource.URL = "http://www.btv.bg/rss__action";
            nSource.DefCategory = "News";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "ladyzone.bg";
            nSource.URL = "http://www.ladyzone.bg/rss_all";
            nSource.DefCategory = "ladyzone";
            _NSource.Add(nSource);
        
            nSource = new NewsSource();
            nSource.Title = "btv.bg";
            nSource.URL = "http://www.btv.bg/rss_all";
            nSource.DefCategory = "News";
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
                    aNItem.Link = aNode.Value.Trim();
                    if (aNItem.Link.Equals(""))
                    { bRes = false; }
                    else
                    {
                        int iLink = aNItem.Link.IndexOf('"');
                        if (iLink > 0)
                        { aNItem.Link = aNItem.Link.Substring(0, iLink); }
                    }
                    break;
                case "PUBDATE":
                    try
                    { aNItem.pubDate = Convert.ToDateTime(aNode.Value.Trim()); }
                    catch
                    { aNItem.pubDate = PTools.GetWebDateTime(DateTime.Now); }
                    break;
                case "GUID":
                    aNItem.Guid = PTools.GetMD5Hash(aNode.Value.Trim());
                    if (aNItem.Guid.Equals(""))
                    { bRes = false; }
                    break;
                case "BODY":
                    aNItem.Body = PTools.GetWebText(aNode.Value);
                    break;
                case "DESCRIPTION":
                    aNItem.Description = PTools.GetWebText(PTools.RemoveWebTag(aNode.Value));
                    // ENCLOSURE
                    string sIMG = PTools.GetEnclosure(aNode.Value);
                    if (sIMG != "")
                    {
                        int iLink = sIMG.IndexOf('"');
                        if (iLink > 0)
                        { sIMG = sIMG.Substring(0, iLink); }
                        aNItem.Enclosure.Add(new Enclosure(sIMG)); 
                    }
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
