using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ToolsRSS.News.Parser
{
    public interface IParser : IDisposable
    {
        List<NewsSource> NSource
        { get; }

        string WSait
        { get; }

        Int32 WSaitID
        { get; }

        /// <summary>
        /// Зарежда данните от новината
        /// </summary>
        bool ParseNews(NewsItem aNItem, XPathNavigator aNode);
    }
}
