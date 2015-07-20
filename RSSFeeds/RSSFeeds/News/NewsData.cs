using System;
using System.Collections.Generic;
using System.Text;

namespace ToolsRSS.News
{
    public class NewsItems : IDisposable
    {
        private List<NewsItem> _Items = new List<NewsItem>();

        public NewsItem this[ int index ]
        {
            get 
            { return _Items[index]; }
            set 
            { _Items[index] = value; }
        }

        public void Add(NewsItem value)
        {
            _Items.Add(value);
        }

        public void Clear()
        {
            _Items.Clear();
        }

        public void Dispose()
        {
            this.Clear();
            _Items = null;
        }
    }

    public class NewsItem : IDisposable
    {
        public string Title = "";
        public string Link = "";
        public string Description = "";
        public string Body = "";
        public string Author = "";
        public string Comments = "";
        public string Guid = "";
        public DateTime pubDate = DateTime.Now;
        public string Category = "";
        public List<Enclosure> Enclosure = new List<Enclosure>();

        public void Clear()
        { 
            Title = "";
            Link = "";
            Description = "";
            Author = "";
            Comments = "";
            Guid = "";
            pubDate = DateTime.Now;
            Category = "";
            Enclosure.Clear();
        }

        public void Dispose()
        {
            this.Clear();
        }
    }

    public class Enclosure
    {
        public Enclosure(string aLink, string aType)
        {
            this.Link = aLink;
            if (aType == "")
            { this.Type = "image"; }
            else
            { this.Type = aType; }
        }
        public Enclosure(string aLink)
        {
            this.Link = aLink;
            this.Type = "image";
        }
        public string Link = "";
        public string Type = "image";
    }

    public class NewsSource
    {
        public string Title = "";
        public string URL = "";
        public string DefCategory = "";
    }

    public delegate void LogHandler(LogMessageType messageType, string message);

    public enum LogMessageType { Event, Error }

    public class WebSaitID
    {
        public const Int32 ActualnoCom = 1;
        public const Int32 DnesBG = 2;
        public const Int32 NewsBG = 3;
        public const Int32 BGFactorORG = 4;
        public const Int32 InvestorBG = 5;
        public const Int32 DarikNewsBG = 6;
        public const Int32 DirBG = 7;
        public const Int32 SPortalBG = 8;
        public const Int32 HotNewsBG = 9;
        public const Int32 HiCommBG = 10;
        public const Int32 MobileBulgariaCOM = 11;
        public const Int32 DoctorOnlineBG = 12;
        public const Int32 BTvBG = 13;
        public const Int32 CapitalBG = 14;
        public const Int32 IdgBG = 15;
        public const Int32 ComputerWorldBG = 16;
        public const Int32 ExpertBG = 17;
        public const Int32 GongBG = 18;
        public const Int32 SignalBG = 19;
        public const Int32 ZarBG = 20;
    }
}
