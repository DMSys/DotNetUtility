using System;
using System.Collections.Generic;
using System.Text;

namespace RSSFeeds
{
    public class NewsData
    {
        public string Title = "";
        public string Link = "";
        public string Description = "";
        public string Body = "";
        public string Author = "";
        public string Comments = "";
        public Int32 Guid = 0;
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
            Guid = 0;
            pubDate = DateTime.Now;
            Category = "";
            Enclosure.Clear();
        }
    }

    public class Enclosure
    {
        public Enclosure(string aLink, string aType)
        {
            Link = aLink;
            Type = aType;
        }
        public Enclosure(string aLink)
        {
            Link = aLink;
            Type = "image";
        }
        public string Link = "";
        public string Type = "image";
    }
}
