using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSDoctorOnlineBG : IParser
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
            { return "doctoronline.bg"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.DoctorOnlineBG; }
        }

        #endregion Properties

        public RSSDoctorOnlineBG()
        {
            NewsSource nSource;

            nSource = new NewsSource();
            nSource.Title = "DoctorOnline.bg: сайт за здраве";
            nSource.URL = "http://doctoronline.bg/rss.php";
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
                    break;
                case "PUBDATE":
                    try
                    { aNItem.pubDate = Convert.ToDateTime(aNode.Value.Trim()); }
                    catch
                    { aNItem.pubDate = PTools.GetWebDateTime(DateTime.Now); }
                    break;
                case "GUID":
                    /*
                    try
                    {
                        string sGValue = aNode.Value.Trim();
                        int iFrom = sGValue.LastIndexOf('=')+1;
                        aNItem.Guid = Convert.ToInt32(sGValue.Substring(iFrom, sGValue.Length - iFrom)).ToString();
                    }
                    catch
                    { bRes = false; }*/
                    aNItem.Guid = PTools.GetMD5Hash(aNode.Value);
                    break;
                case "DESCRIPTION":
                    aNItem.Description = "";
                    string sValue = aNode.Value.Replace("<p>", "").Replace("</p>", "").Trim();
                    aNItem.Body = PTools.GetWebText(PTools.RemoveWebTag(sValue.Trim()));
                    //
                    if (sValue.Substring(0, 4).Equals("<img"))
                    {
                        int iIMGFrom = sValue.LastIndexOf("src=\"") + 5;
                        int iIMGTo = 0;
                        for (int i = iIMGFrom; i < sValue.Length; i++)
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
