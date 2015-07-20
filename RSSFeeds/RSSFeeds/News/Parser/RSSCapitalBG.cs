using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public class RSSCapitalBG : IParser
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
            { return "capital.bg"; }
        }

        public Int32 WSaitID
        {
            get
            { return WebSaitID.CapitalBG; }
        }

        #endregion Properties

        public RSSCapitalBG()
        {
            NewsSource nSource;

            nSource = new NewsSource();
            nSource.Title = "www.capital.bg";
            nSource.URL = "http://www.capital.bg/rss/";
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
                    //  CATEGORY
                    int iCatStat = 0;
                    for (int i = 0; (i < aNItem.Link.Length) && (iCatStat < 5); i++)
                    {
                        char cCat = aNItem.Link[i];
                        if (cCat == '/')
                        { iCatStat++; }
                        //
                        if ((iCatStat == 3) && (cCat != '/'))
                        { aNItem.Category += cCat.ToString(); }
                        else if (iCatStat == 4)
                        {
                            if (cCat == '/')
                            { aNItem.Category += "_"; }
                            else
                            { aNItem.Category += cCat.ToString(); }
                        }
                    }
                    break;
                case "PUBDATE":
                    try
                    { aNItem.pubDate = Convert.ToDateTime(aNode.Value.Trim()); }
                    catch
                    { aNItem.pubDate = PTools.GetWebDateTime(DateTime.Now); }
                    break;
                case "DESCRIPTION":
                    string sValue = aNode.Value.Trim();
                    // DESCRIPTION
                    aNItem.Description = "";
                    aNItem.Body = PTools.GetWebText(PTools.RemoveWebTag(sValue));
                    // ENCLOSURE
                    string sIMG = PTools.GetEnclosure(sValue);
                    if (sIMG != "")
                    { aNItem.Enclosure.Add(new Enclosure(sIMG)); }
                    break;
                case "GUID":
                    try
                    { aNItem.Guid = Convert.ToInt32(aNode.Value.Trim()).ToString(); }
                    catch
                    { bRes = false; }
                    break;
                case "AUTHOR":
                    aNItem.Author = aNode.Value.Trim();
                    break;
                case "COMMENTS":
                    aNItem.Comments = aNode.Value.Trim();
                    break;
                default:
                    throw new Exception("RSS Element: '" + aNode.Name.ToUpper() + "' !");
            }
            return bRes;
        }
    }
}
