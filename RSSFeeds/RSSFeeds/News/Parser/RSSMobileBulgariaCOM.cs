using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSMobileBulgariaCOM : IParser
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
            { return "mobilebulgaria.com"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.MobileBulgariaCOM; }
        }

        #endregion Properties

        public RSSMobileBulgariaCOM()
        {
            NewsSource nSource;

            nSource = new NewsSource();
            nSource.Title = "Mobile Bulgaria News";
            nSource.URL = "http://www.mobilebulgaria.com/rss.php";
            nSource.DefCategory = "News";
            _NSource.Add(nSource);

            nSource = new NewsSource();
            nSource.Title = "Mobile Bulgaria Reviews";
            nSource.URL = "http://www.mobilebulgaria.com/rss_reviews.php";
            nSource.DefCategory = "Reviews";
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
                     /*   string sGValue = aNode.Value.Trim();
                        int iFrom = sGValue.LastIndexOf('=')+1;
                        aNItem.Guid = Convert.ToInt32(sGValue.Substring(iFrom, sGValue.Length - iFrom)).ToString();*/
                        aNItem.Guid = PTools.GetMD5Hash(aNode.Value.Trim());
                    }
                    catch
                    { bRes = false; }
                    break;
                case "DESCRIPTION":
                    aNItem.Description = "";
                    aNItem.Body = PTools.GetWebText(PTools.RemoveWebTag(aNode.Value.Replace("MobileBulgaria.com: ", "").Trim()));
                    break;
                case "PICTURE":
                    aNItem.Enclosure.Add(new Enclosure(aNode.Value));
                    break;
                default:
                    throw new Exception("RSS Element: '" + aNode.Name.ToUpper() + "' !");
            }
            return bRes;
        }
    }
}
