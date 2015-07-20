using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Net;
using System.IO;

namespace RSSFeeds
{
    public partial class UCTest : UserControl
    {
        private string _ConnectionString = "";
        public string ConnectionString
        {
            set
            { _ConnectionString = value; }
        }

        public UCTest()
        {
            InitializeComponent();
        }

        private void SetMessage(string aMessage)
        {
            listBox1.Items.Insert(0, aMessage);
            listBox1.Refresh();
        }

        private void btnTestOpen_Click(object sender, EventArgs e)
        {
            string sURL = tbRSSFeedsURL.Text.Trim();
            if (sURL.Equals(""))
            {
                listBox1.Items.Clear();
                SetMessage("Въведи адрес !");
                return;
            }
            try
            {
                listBox1.Items.Clear();
                SetMessage("TEST Start: " + sURL);
                //
                XmlDocument doc = new XmlDocument();

                doc.XmlResolver = null;
                doc.Load(sURL);
                SetMessage("TEST Успешно зарежда сайта");
                //
                XPathNavigator navigator = doc.CreateNavigator();
                XPathNodeIterator nodesLink = navigator.Select("//rss/channel/item");
                SetMessage("TEST Брой елементи: " + nodesLink.Count.ToString());
                //
                SetMessage("TEST Done");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btnDownLoadURL_Click(object sender, EventArgs e)
        {/*
            StreamWriter sr = new StreamWriter(@"D:\Projects\MSVS9\Programs\RSSFeeds.Bin\dddddddddddddddd.txt");
            sr.Write(DownloadWebPage("http://presata.xtreemhost.com/update/load_news.php"));
            sr.Close();*/

            XmlDocument doc = new XmlDocument();
            doc.XmlResolver = null;
            // load the xml doc
            doc.Load(@"D:\Projects\MSVS9\Programs\RSSFeeds.Bin\dddddddddddddddd.txt");

            // get an xpath navigator   
            XPathNavigator navigator = doc.CreateNavigator();
            NewsData NData = new NewsData();

            // Get the links from the RSS feed
            XPathNodeIterator nodesLink = navigator.Select("//root");

            while (nodesLink.MoveNext())
            {
                // clean up the link
                XPathNodeIterator RNode = nodesLink.Current.SelectChildren(XPathNodeType.Element);
                //
                while (RNode.MoveNext())
                {
                    SetMessage(RNode.Current.Name +": "+ RNode.Current.Value);
                }
            }

        }

        public string DownloadWebPage( string aUrl )
        {
            // Open a connection
            HttpWebRequest WebRequestObject = (HttpWebRequest)HttpWebRequest.Create(aUrl);

            // You can also specify additional header values like 
            // the user agent or the referer:
            WebRequestObject.UserAgent = "DMSys Web";
            WebRequestObject.Referer = "http://dmsys.co.cc/";

            // Request response:
            WebResponse Response = WebRequestObject.GetResponse();

            // Open data stream:
            Stream WebStream = Response.GetResponseStream();

            // Create reader object:
            StreamReader Reader = new StreamReader(WebStream);

            // Read the entire stream content:
            string PageContent = Reader.ReadToEnd();

            // Cleanup
            Reader.Close();
            WebStream.Close();
            Response.Close();

            return PageContent;
        }

        private void btnParseHTML_Click(object sender, EventArgs e)
        {
            using (HTMLDocNavigator nav = new HTMLDocNavigator())
            {
                nav.LoadUrl("http://politics.actualno.com/news_295515.html");

                //nav.LoadFile(@"C:\Documents and Settings\user\Desktop\proba1.html");

                nav.Select("//html/body/div");
                nav.MoveNext("div", 4);
                //
                nav.LoadValue(nav.ChildText);
                nav.Select("//");
                if (nav.MoveNext("div", 4))
                {
                    string sValue = nav.ChildText;
                    MessageBox.Show(sValue);
                }
            }
        }
    }
}
