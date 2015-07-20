using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DMSys.Net.FTP;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.XPath;
using ToolsRSS.News;
using System.Configuration;
using DMSys.Data.SQLiteClient;

namespace RSSFeeds
{
    public partial class UCUpdateData : UserControl
    {
        public UCUpdateData()
        {
            InitializeComponent();
        }

        #region Properties

        private string _ConnectionString = "";
        public string ConnectionString
        {
            set
            { _ConnectionString = value; }
        }

        public TextWriter LogFile = null;

        public bool ShowMessage = true;

        private string _ApplPath_NewsXML = "";
        private string _ApplPath_NewsInfoXML = "";
        private string _ApplPath_EnclosureXML = "";
        private string _ApplPath = "";
        public string ApplPath
        {
            set
            {
                _ApplPath = value;
                _ApplPath_NewsXML = _ApplPath + "\\send_news\\news.xml";
                _ApplPath_NewsInfoXML = _ApplPath + "\\send_news\\news_info.xml";
                _ApplPath_EnclosureXML = _ApplPath + "\\send_news\\enclosure.xml";
            }
        }

        public string FTP_Hostname
        {
            get
            { return ConfigurationManager.AppSettings["FTP_HOSTNAME"].ToString(); }
        }
        public string FTP_Username
        {
            get
            { return ConfigurationManager.AppSettings["FTP_USERNAME"].ToString(); }
        }
        public string FTP_Password
        {
            get
            { return ConfigurationManager.AppSettings["FTP_PASSWORD"].ToString(); }
        }
        public string FTP_UpdateDir
        {
            get
            { return ConfigurationManager.AppSettings["FTP_UPDATE_DIR"].ToString(); }
        }

        private string _PageUrl_Password = "adshjkfhdkstryuewe";
        private string _PageUrl_Style = "xml";
        public string PageUrl_Update
        {
            get
            { return ConfigurationManager.AppSettings["PAGE_URL_UPDATE"].ToString(); }
         }
        public string PageUrl_UpdateNews
        {
            get
            { return dUtils.UrlCombine(this.PageUrl_Update, "load_news.php?stl=" + _PageUrl_Style + "&pswrd=" + _PageUrl_Password); }
        }
        public string PageUrl_UpdateCategories
        {
            get
            { return dUtils.UrlCombine(this.PageUrl_Update, "load_categories.php?stl=" + _PageUrl_Style + "&pswrd=" + _PageUrl_Password); }
        }
        public string PageUrl_UpdateCategoriesSub
        {
            get
            { return dUtils.UrlCombine(this.PageUrl_Update, "load_categories_sub.php?stl=" + _PageUrl_Style + "&pswrd=" + _PageUrl_Password); }
        }
        public string PageUrl_UpdateSource
        {
            get
            { return dUtils.UrlCombine(this.PageUrl_Update, "load_source.php?stl=" + _PageUrl_Style + "&pswrd=" + _PageUrl_Password); }
        }

        #endregion Properties

        #region Tools

        private void ClearMessage()
        {
            if (this.ShowMessage)
            {
                lbMsg.Items.Clear();
            }
        }

        private void SetMessage(LogMessageType aMessageType, string aMessage)
        {
            if (this.ShowMessage)
            {
                lbMsg.Items.Insert(0, aMessage);
                lbMsg.Refresh();
            }
            if ((this.LogFile != null) && (aMessageType == LogMessageType.Error))
            {
                this.LogFile.WriteLine(aMessage);
                this.LogFile.Flush();
            }
        }

        /// <summary>
        /// Качва файл на FTP /за един файл/
        /// </summary>
        private void UploadFile(string aFile)
        {
            using (FTPClient ftpClnt = new FTPClient())
            {
                ftpClnt.Hostname = this.FTP_Hostname;
                ftpClnt.Username = this.FTP_Username;
                ftpClnt.Password = this.FTP_Password;
                //
                string lFile = Path.Combine(Path.Combine(_ApplPath, "send_news"), aFile);
                string tFile = dUtils.UrlCombine(this.FTP_UpdateDir, aFile);
                ftpClnt.Upload(lFile, tFile);
            }
        }

        #endregion Tools

        #region XMLNews

        #region CreateXMLNews

        /// <summary>
        /// XML: Новините
        /// </summary>
        private void WriteXML_News(SqliteConnection aSqlCnnctn, Int64 aNewsID)
        {
            using (DataTable dtNews = new DataTable())
            {
                using (SqliteCommand sqlCmmnd = new SqliteCommand())
                {
                    sqlCmmnd.Connection = aSqlCnnctn;
                    sqlCmmnd.CommandText = "SELECT * "
                                         + " FROM news sws "
                                         + "    , x_news_nweb snn "
                                         + " WHERE sws.news_id > " + aNewsID.ToString()
                                         + "   AND sws.news_category_id = snn.news_category_id "
                                         + "   AND snn.nweb_category_id > 0 "
                                         + " ORDER BY sws.news_id; ";
                    using (SqliteDataAdapter sqlDAdapter = new SqliteDataAdapter(sqlCmmnd))
                    {
                        dtNews.Clear();
                        sqlDAdapter.Fill(dtNews);
                    }
                }
                //
                using (StreamWriter swNews = new StreamWriter(_ApplPath_NewsXML, false, Encoding.UTF8))
                {
                    swNews.WriteLine("<rss version=\"2.0\">");
                    swNews.WriteLine("<channel>");
                    swNews.WriteLine("<title>presata.co.cc</title>");
                    swNews.WriteLine("<link>http://presata.co.cc</link>");
                    swNews.WriteLine("<description>Новини - presata.co.cc</description>");
                    swNews.WriteLine("<copyright>http://presata.co.cc/</copyright>");
                    swNews.WriteLine("<lastBuildDate>" + DateTime.Now.ToString() + "</lastBuildDate>");
                    swNews.WriteLine("<newsid>" + aNewsID.ToString() + "</newsid>");
                    //
                    foreach (DataRow dr in dtNews.Rows)
                    {
                        swNews.WriteLine("<item>");
                        swNews.WriteLine("<guid>" + dr["news_id"].ToString() + "</guid>");
                        swNews.WriteLine("<title>" + ConvertToXMLText(dr["news_title"].ToString()) + "</title>");
                        swNews.WriteLine("<description>" + ConvertToXMLText(dr["news_description"].ToString()) + "</description>");
                        swNews.WriteLine("<body>" + ConvertToXMLText(dr["news_body"].ToString()) + "</body>");
                        swNews.WriteLine("<pubDate>" + Convert.ToDateTime(dr["news_pubdate"]).ToString("yyyy-MM-dd HH:mm:ss") + "</pubDate>");
                        swNews.WriteLine("<author>" + dr["news_site_id"].ToString() + "</author>");
                        swNews.WriteLine("<category>" + dr["nweb_category_id"].ToString() + "</category>");
                        swNews.WriteLine("</item>");
                    }
                    swNews.WriteLine("</channel>");
                    swNews.WriteLine("</rss>");
                    swNews.Close();
                }
            }
        }

        /// <summary>
        /// XML: Информация за новините
        /// </summary>
        private void WriteXML_NewsInfo(SqliteConnection aSqlCnnctn, Int64 aNewsID)
        {
            using (DataTable dtNews = new DataTable())
            {
                using (SqliteCommand sqlCmmnd = new SqliteCommand())
                {
                    // 
                    sqlCmmnd.Connection = aSqlCnnctn;
                    sqlCmmnd.CommandText = "SELECT * "
                                         + " FROM news "
                                         + " WHERE news.news_id > " + aNewsID.ToString()
                                         + " ORDER BY news.news_id; ";
                    using (SqliteDataAdapter sqlDAdapter = new SqliteDataAdapter(sqlCmmnd))
                    {
                        dtNews.Clear();
                        sqlDAdapter.Fill(dtNews);
                    }
                }
                //
                using (StreamWriter swNews = new StreamWriter(_ApplPath_NewsInfoXML, false, Encoding.UTF8))
                {
                    swNews.WriteLine("<rss version=\"2.0\">");
                    swNews.WriteLine("<channel>");
                    swNews.WriteLine("<title>presata.co.cc</title>");
                    swNews.WriteLine("<link>http://presata.co.cc</link>");
                    swNews.WriteLine("<description>Новини - presata.co.cc</description>");
                    swNews.WriteLine("<copyright>http://presata.co.cc/</copyright>");
                    swNews.WriteLine("<lastBuildDate>" + DateTime.Now.ToString() + "</lastBuildDate>");
                    swNews.WriteLine("<newsid>" + aNewsID.ToString() + "</newsid>");
                    //
                    foreach (DataRow dr in dtNews.Rows)
                    {
                        swNews.WriteLine("<item>");
                        swNews.WriteLine("<guid>" + dr["news_id"].ToString() + "</guid>");
                        swNews.WriteLine("<link>" + dr["news_link"].ToString() + "</link>");
                        swNews.WriteLine("</item>");
                    }
                    swNews.WriteLine("</channel>");
                    swNews.WriteLine("</rss>");
                    swNews.Close();
                }
            }
        }

        /// <summary>
        /// XML: Снимки за новините
        /// Само първата снимка
        /// </summary>
        private void WriteXML_Enclosure(SqliteConnection aSqlCnnctn, Int64 aNewsID)
        {
            using (DataTable dtNews = new DataTable())
            {
                using (SqliteCommand sqlCmmnd = new SqliteCommand())
                {
                    sqlCmmnd.Connection = aSqlCnnctn;
                    sqlCmmnd.CommandText = "SELECT * "
                                         + " FROM enclosure "
                                         + " WHERE news_id > " + aNewsID.ToString()
                                         + " ORDER BY news_id, encl_link; ";
                    using (SqliteDataAdapter sqlDAdapter = new SqliteDataAdapter(sqlCmmnd))
                    {
                        dtNews.Clear();
                        sqlDAdapter.Fill(dtNews);
                    }
                }
                //
                using (StreamWriter swNews = new StreamWriter(_ApplPath_EnclosureXML, false, Encoding.UTF8))
                {
                    swNews.WriteLine("<rss version=\"2.0\">");
                    swNews.WriteLine("<channel>");
                    swNews.WriteLine("<title>presata.co.cc</title>");
                    swNews.WriteLine("<link>http://presata.co.cc</link>");
                    swNews.WriteLine("<description>Новини - presata.co.cc</description>");
                    swNews.WriteLine("<copyright>http://presata.co.cc/</copyright>");
                    swNews.WriteLine("<lastBuildDate>" + DateTime.Now.ToString() + "</lastBuildDate>");
                    swNews.WriteLine("<newsid>" + aNewsID.ToString() + "</newsid>");
                    //
                    string sNewsID = "";
                    foreach (DataRow dr in dtNews.Rows)
                    {
                        if (!sNewsID.Equals(dr["news_id"].ToString()))
                        {
                            sNewsID = dr["news_id"].ToString();

                            swNews.WriteLine("<item>");
                            swNews.WriteLine("<guid>" + dr["news_id"].ToString() + "</guid>");
                            swNews.WriteLine("<url>" + dr["encl_link"].ToString() + "</url>");
                            swNews.WriteLine("<type>" + dr["encl_id"].ToString() + "</type>");
                            swNews.WriteLine("</item>");
                        }
                    }
                    swNews.WriteLine("</channel>");
                    swNews.WriteLine("</rss>");
                    swNews.Close();
                }
            }
        }

        private string ConvertToXMLText(string aValue)
        {
            return aValue.Replace("&amp;", "&").Replace("&", "&amp;").Replace("\"", "&quot;");
        }

        private Int64 GetLastSendNews(SqliteConnection aSqlCnnctn)
        {
            Int64 iLastSendNews = 0;
            using (SqliteCommand sqlCmmnd = new SqliteCommand())
            {
                sqlCmmnd.Connection = aSqlCnnctn;
                //
                sqlCmmnd.CommandText = "SELECT news_setting.prm_value_int "
                                     + " FROM news_setting "
                                     + " WHERE news_setting.prm_name = 'last_send_news' ";
                using (SqliteDataReader sqlDReader = sqlCmmnd.ExecuteReader())
                {
                    if (sqlDReader.HasRows && sqlDReader.Read())
                    {
                        iLastSendNews = Convert.ToInt64(sqlDReader["prm_value_int"]);
                    }
                    sqlDReader.Close();
                }
            }
            return iLastSendNews;
        }

        private void SetLastSendNews(SqliteConnection aSqlCnnctn)
        {
            using (SqliteCommand sqlCmmnd = new SqliteCommand())
            {
                sqlCmmnd.Connection = aSqlCnnctn;
                Int64 iLastSendNews = 0;
                sqlCmmnd.CommandText = 
                    "SELECT MAX(news.news_id) AS max_news_id FROM news ";
                using (SqliteDataReader sqlDReader = sqlCmmnd.ExecuteReader())
                {
                    if (sqlDReader.HasRows && sqlDReader.Read())
                    {
                        iLastSendNews = Convert.ToInt64(sqlDReader["max_news_id"]);
                    }
                    sqlDReader.Close();
                }
                //
                sqlCmmnd.CommandText = "UPDATE news_setting "
                                     + "    SET prm_value_int = " + iLastSendNews.ToString()
                                     + " WHERE prm_name = 'last_send_news' ";
                sqlCmmnd.ExecuteNonQuery();
            }
        }

        #endregion CreateXMLNews

        #region LoadXMLNews

        private string DownloadWebPage(string aUrl)
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

        private void WriteWebResponse(string aAction, string aValue)
        {
            string sFileName = _ApplPath + "\\web_response\\" + aAction + DateTime.Now.ToString("_yyyy.MM.dd.HH.mm") + ".xml";
            using (StreamWriter sr = new StreamWriter(sFileName))
            {
                sr.Write(aValue);
                sr.Close();
            }
        }

        private void PrintWebResponse(string aAction, string aValue)
        {
            XmlDocument doc = new XmlDocument();
            doc.XmlResolver = null;
            // load the xml doc
            doc.LoadXml(aValue);

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
                    SetMessage(LogMessageType.Event, RNode.Current.Name + ": " + RNode.Current.Value);
                }
            }
        }

        #endregion LoadXMLNews

        /// <summary>
        /// Създава XML файл за новите новини
        /// </summary>
        private void CreateXMLNews()
        {
            SetMessage(LogMessageType.Event, "CreateXMLNews: Start");
            if (File.Exists(_ApplPath_NewsXML))
            {
                SetMessage(LogMessageType.Event, "CreateXMLNews: Има незаредени новини");
            }
            else
            {
                using (SqliteConnection sqlCnnctn = new SqliteConnection())
                {
                    sqlCnnctn.ConnectionString = _ConnectionString;
                    sqlCnnctn.Open();
                    //
                    Int64 iLastSendNews = GetLastSendNews(sqlCnnctn);
                    //
                    WriteXML_News(sqlCnnctn, iLastSendNews);
                    WriteXML_NewsInfo(sqlCnnctn, iLastSendNews);
                    WriteXML_Enclosure(sqlCnnctn, iLastSendNews);
                    //
                    SetLastSendNews(sqlCnnctn);
                    //
                    sqlCnnctn.Close();
                }
            }
            //
            SetMessage(LogMessageType.Event, "CreateXMLNews: Done");
        }

        /// <summary>
        /// Качва новините на сайта
        /// </summary>
        private void UpdateXMLNews()
        {
            SetMessage(LogMessageType.Event, "UpdateXMLNews: Start");

            using (FTPClient ftpClnt = new FTPClient())
            {  
                ftpClnt.Hostname = this.FTP_Hostname;
                ftpClnt.Username = this.FTP_Username;
                ftpClnt.Password = this.FTP_Password;

                SetMessage(LogMessageType.Event, "UpdateXMLNews: news.xml");
                ftpClnt.Upload(_ApplPath_NewsXML, dUtils.UrlCombine(this.FTP_UpdateDir, "news.xml"));
                SetMessage(LogMessageType.Event, "UpdateXMLNews: news_info.xml");
                ftpClnt.Upload(_ApplPath_NewsInfoXML, dUtils.UrlCombine(this.FTP_UpdateDir, "news_info.xml"));
                SetMessage(LogMessageType.Event, "UpdateXMLNews: enclosure.xml");
                ftpClnt.Upload(_ApplPath_EnclosureXML, dUtils.UrlCombine(this.FTP_UpdateDir, "enclosure.xml"));
            }

            SetMessage(LogMessageType.Event, "UpdateXMLNews: Done");
        }

        /// <summary>
        /// Стартира зареждането на новините
        /// </summary>
        private void LoadXMLNews()
        {
            SetMessage(LogMessageType.Event, "LoadXMLNews: Start");

            string sResponse = DownloadWebPage(this.PageUrl_UpdateNews);
            WriteWebResponse("LoadXMLNews", sResponse);
            PrintWebResponse("LoadXMLNews", sResponse);

            File.Delete(_ApplPath_NewsXML);
            File.Delete(_ApplPath_NewsInfoXML);
            File.Delete(_ApplPath_EnclosureXML);

            SetMessage(LogMessageType.Event, "LoadXMLNews: Done");
        }

        private void btnCreateXMLNews_Click(object sender, EventArgs e)
        {
            try
            {
                ClearMessage();
                CreateXMLNews();
            }
            catch (Exception ex)
            {
                SetMessage(LogMessageType.Error, "CreateXMLNews: Exception: " + ex.Message);
            }
        }

        private void btnUpdateXMLNews_Click(object sender, EventArgs e)
        {
            try
            {
                ClearMessage();
                UpdateXMLNews();
            }
            catch (Exception ex)
            {
                SetMessage(LogMessageType.Error, "UpdateXMLNews: Exception: " + ex.Message);
            }
        }

        private void btnLoadXMLNews_Click(object sender, EventArgs e)
        {
            try
            {
                ClearMessage();
                LoadXMLNews();
            }
            catch (Exception ex)
            {
                SetMessage(LogMessageType.Error, "LoadXMLNews: Exception: " + ex.Message);
            }
        }

        private void btnNewsXML_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        public void UpdateData()
        {
            try
            {
                ClearMessage();
                CreateXMLNews();
                UpdateXMLNews();
                LoadXMLNews();
            }
            catch (Exception ex)
            {
                SetMessage(LogMessageType.Error, "CreateXMLNews: Exception: " + ex.Message);
            }
        }

        #endregion XMLNews

        #region XMLCategories

        /// <summary>
        /// XML: Категории
        /// </summary>
        private void WriteXML_Categories(SqliteConnection aSqlCnnctn)
        {
            using (DataTable dtNews = new DataTable())
            {
                using (SqliteCommand sqlCmmnd = new SqliteCommand())
                {
                    sqlCmmnd.Connection = aSqlCnnctn;
                    sqlCmmnd.CommandText = 
                        "SELECT nct.* " +
                        " FROM tn_categories nct ";
                    using (SqliteDataAdapter sqlDAdapter = new SqliteDataAdapter(sqlCmmnd))
                    {
                        dtNews.Clear();
                        sqlDAdapter.Fill(dtNews);
                    }
                }
                //
                using (StreamWriter swNews = new StreamWriter(_ApplPath + "\\send_news\\tn_categories.xml", false, Encoding.UTF8))
                {
                    swNews.WriteLine("<rss version=\"2.0\">");
                    swNews.WriteLine("<channel>");
                    swNews.WriteLine("<title>presata.co.cc</title>");
                    swNews.WriteLine("<link>http://presata.co.cc</link>");
                    swNews.WriteLine("<description>Новини - presata.co.cc</description>");
                    swNews.WriteLine("<copyright>http://presata.co.cc/</copyright>");
                    swNews.WriteLine("<lastBuildDate>" + DateTime.Now.ToString() + "</lastBuildDate>");
                    //
                    foreach (DataRow dr in dtNews.Rows)
                    {
                        swNews.WriteLine("<item>");
                        swNews.WriteLine("<n_category_id>" + dr["n_category_id"].ToString() + "</n_category_id>");
                        swNews.WriteLine("<n_category>" + dr["n_category"].ToString() + "</n_category>");
                        swNews.WriteLine("<f_sort>" + dr["f_sort"].ToString() + "</f_sort>");
                        swNews.WriteLine("<is_valid>" + dr["is_valid"].ToString() + "</is_valid>");
                        swNews.WriteLine("<table_name>" + dr["table_name"].ToString() + "</table_name>");
                        swNews.WriteLine("</item>");
                    }
                    swNews.WriteLine("</channel>");
                    swNews.WriteLine("</rss>");
                    swNews.Close();
                }
            }
        }

        private void CreateXMLCategories()
        {
            SetMessage(LogMessageType.Event, "CreateXMLCategories: Start");

            using (SqliteConnection sqlCnnctn = new SqliteConnection())
            {
                sqlCnnctn.ConnectionString = _ConnectionString;
                sqlCnnctn.Open();

                WriteXML_Categories(sqlCnnctn);

                sqlCnnctn.Close();
            }
            SetMessage(LogMessageType.Event, "CreateXMLCategories: Done");
        }

        private void UploadXMLCategories()
        {
            SetMessage(LogMessageType.Event, "UploadXMLCategories: Start");
            UploadFile("tn_categories.xml");
            SetMessage(LogMessageType.Event, "UploadXMLCategories: Done");
        }

        private void LoadXMLCategories()
        {
            SetMessage(LogMessageType.Event, "LoadXMLCategories: Start");

            string sResponse = DownloadWebPage(this.PageUrl_UpdateCategories);
            WriteWebResponse("LoadXMLCategories", sResponse);
            PrintWebResponse("LoadXMLCategories", sResponse);

            SetMessage(LogMessageType.Event, "LoadXMLCategories: Done");
        }

        private void btn_CreateXMLCategories_Click(object sender, EventArgs e)
        {
            try
            {
                ClearMessage();
                CreateXMLCategories();
            }
            catch (Exception ex)
            {
                SetMessage(LogMessageType.Error, "CreateXMLCategories: Exception: " + ex.Message);
            }
        }

        private void btn_UploadXMLCategories_Click(object sender, EventArgs e)
        {
            try
            {
                ClearMessage();
                UploadXMLCategories();
            }
            catch (Exception ex)
            {
                SetMessage(LogMessageType.Error, "UploadXMLCategorie: Exception: " + ex.Message);
            }
        }

        private void btn_LoadXMLCategories_Click(object sender, EventArgs e)
        {
            try
            {
                ClearMessage();
                LoadXMLCategories();
            }
            catch (Exception ex)
            {
                SetMessage(LogMessageType.Error, "LoadXMLCategories: Exception: " + ex.Message);
            }
        }

        private void btn_XMLCategories_Click(object sender, EventArgs e)
        {
            try
            {
                ClearMessage();
                CreateXMLCategories();
                UploadXMLCategories();
                LoadXMLCategories();
            }
            catch (Exception ex)
            {
                SetMessage(LogMessageType.Error, "XMLCategories: Exception: " + ex.Message);
            }
        }

        #endregion XMLCategories

        #region XMLSubCategories

        /// <summary>
        /// XML: Подкатегории
        /// </summary>
        private void WriteXML_Subcategories(SqliteConnection aSqlCnnctn)
        {
            using (DataTable dtNews = new DataTable())
            {
                using (SqliteCommand sqlCmmnd = new SqliteCommand())
                {
                    sqlCmmnd.Connection = aSqlCnnctn;
                    sqlCmmnd.CommandText =
                        "SELECT nct.* " +
                        " FROM tn_categories_sub nct ";
                    using (SqliteDataAdapter sqlDAdapter = new SqliteDataAdapter(sqlCmmnd))
                    {
                        dtNews.Clear();
                        sqlDAdapter.Fill(dtNews);
                    }
                }
                //
                using (StreamWriter swNews = new StreamWriter(_ApplPath + "\\send_news\\tn_categories_sub.xml", false, Encoding.UTF8))
                {
                    swNews.WriteLine("<rss version=\"2.0\">");
                    swNews.WriteLine("<channel>");
                    swNews.WriteLine("<title>presata.co.cc</title>");
                    swNews.WriteLine("<link>http://presata.co.cc</link>");
                    swNews.WriteLine("<description>Новини - presata.co.cc</description>");
                    swNews.WriteLine("<copyright>http://presata.co.cc/</copyright>");
                    swNews.WriteLine("<lastBuildDate>" + DateTime.Now.ToString() + "</lastBuildDate>");
                    //
                    foreach (DataRow dr in dtNews.Rows)
                    {
                        swNews.WriteLine("<item>");
                        swNews.WriteLine("<n_category_sub_id>" + dr["n_category_sub_id"].ToString() + "</n_category_sub_id>");
                        swNews.WriteLine("<n_category_id>" + dr["n_category_id"].ToString() + "</n_category_id>");
                        swNews.WriteLine("<n_category_sub>" + dr["n_category_sub"].ToString() + "</n_category_sub>");
                        swNews.WriteLine("<f_sort>" + dr["f_sort"].ToString() + "</f_sort>");
                        swNews.WriteLine("<is_valid>" + dr["is_valid"].ToString() + "</is_valid>");
                        swNews.WriteLine("</item>");
                    }
                    swNews.WriteLine("</channel>");
                    swNews.WriteLine("</rss>");
                    swNews.Close();
                }
            }
        }

        private void CreateXMLSubCategories()
        {
            SetMessage(LogMessageType.Event, "CreateXMLSubCategories: Start");

            using (SqliteConnection sqlCnnctn = new SqliteConnection())
            {
                sqlCnnctn.ConnectionString = _ConnectionString;
                sqlCnnctn.Open();

                WriteXML_Subcategories(sqlCnnctn);
                
                sqlCnnctn.Close();
            }
            SetMessage(LogMessageType.Event, "CreateXMLSubCategories: Done");
        }

        private void UploadXMLSubCategories()
        {
            SetMessage(LogMessageType.Event, "UploadXMLSubCategories: Start");
            UploadFile("tn_categories_sub.xml");
            SetMessage(LogMessageType.Event, "UploadXMLSubCategories: Done");
        }

        private void LoadXMLSubCategories()
        {
            SetMessage(LogMessageType.Event, "LoadXMLSubCategories: Start");

            string sResponse = DownloadWebPage(this.PageUrl_UpdateCategoriesSub);
            WriteWebResponse("LoadXMLSubCategories", sResponse);
            PrintWebResponse("LoadXMLSubCategories", sResponse);

            SetMessage(LogMessageType.Event, "LoadXMLSubCategories: Done");
        }

        private void btn_CreateXMLSubCategories_Click(object sender, EventArgs e)
        {
            try
            {
                ClearMessage();
                CreateXMLSubCategories();
            }
            catch (Exception ex)
            {
                SetMessage(LogMessageType.Error, "CreateXMLSubCategories: Exception: " + ex.Message);
            }
        }

        private void btn_UploadXMLSubCategories_Click(object sender, EventArgs e)
        {
            try
            {
                ClearMessage();
                UploadXMLSubCategories();
            }
            catch (Exception ex)
            {
                SetMessage(LogMessageType.Error, "UploadXMLSubCategories: Exception: " + ex.Message);
            }
        }

        private void btn_LoadXMLSubCategories_Click(object sender, EventArgs e)
        {
            try
            {
                ClearMessage();
                LoadXMLSubCategories();
            }
            catch (Exception ex)
            {
                SetMessage(LogMessageType.Error, "LoadXMLSubCategories: Exception: " + ex.Message);
            }
        }

        private void btn_XMLSubCategories_Click(object sender, EventArgs e)
        {
            try
            {
                ClearMessage();
                CreateXMLSubCategories();
                UploadXMLSubCategories();
                LoadXMLSubCategories();
            }
            catch (Exception ex)
            {
                SetMessage(LogMessageType.Error, "XMLSubCategories: Exception: " + ex.Message);
            }
        }

        #endregion XMSubCategories

        #region XMLSource

        /// <summary>
        /// XML: Подкатегории
        /// </summary>
        private void WriteXML_Source(SqliteConnection aSqlCnnctn)
        {
            using (DataTable dtNews = new DataTable())
            {
                using (SqliteCommand sqlCmmnd = new SqliteCommand())
                {
                    sqlCmmnd.Connection = aSqlCnnctn;
                    sqlCmmnd.CommandText =
                        "SELECT nsr.* " +
                        " FROM t_news_sources nsr ";
                    using (SqliteDataAdapter sqlDAdapter = new SqliteDataAdapter(sqlCmmnd))
                    {
                        dtNews.Clear();
                        sqlDAdapter.Fill(dtNews);
                    }
                }
                //
                using (StreamWriter swNews = new StreamWriter(_ApplPath + "\\send_news\\tn_sources.xml", false, Encoding.UTF8))
                {
                    swNews.WriteLine("<rss version=\"2.0\">");
                    swNews.WriteLine("<channel>");
                    swNews.WriteLine("<title>presata.co.cc</title>");
                    swNews.WriteLine("<link>http://presata.co.cc</link>");
                    swNews.WriteLine("<description>Новини - presata.co.cc</description>");
                    swNews.WriteLine("<copyright>http://presata.co.cc/</copyright>");
                    swNews.WriteLine("<lastBuildDate>" + DateTime.Now.ToString() + "</lastBuildDate>");
                    //
                    foreach (DataRow dr in dtNews.Rows)
                    {
                        swNews.WriteLine("<item>");
                        swNews.WriteLine("<n_source_id>" + dr["n_source_id"].ToString() + "</n_source_id>");
                        swNews.WriteLine("<n_source>" + dr["n_source"].ToString() + "</n_source>");
                        swNews.WriteLine("</item>");
                    }
                    swNews.WriteLine("</channel>");
                    swNews.WriteLine("</rss>");
                    swNews.Close();
                }
            }
        }

        private void CreateXMLSource()
        {
            SetMessage(LogMessageType.Event, "CreateXMLSource: Start");

            using (SqliteConnection sqlCnnctn = new SqliteConnection())
            {
                sqlCnnctn.ConnectionString = _ConnectionString;
                sqlCnnctn.Open();

                WriteXML_Source(sqlCnnctn);

                sqlCnnctn.Close();
            }
            SetMessage(LogMessageType.Event, "CreateXMLSource: Done");
        }

        private void UploadXMLSource()
        {
            SetMessage(LogMessageType.Event, "UploadXMLSource: Start");
            UploadFile("tn_sources.xml");
            SetMessage(LogMessageType.Event, "UploadXMLSource: Done");
        }

        private void LoadXMLSource()
        {
            SetMessage(LogMessageType.Event, "LoadXMLSource: Start");

            string sResponse = DownloadWebPage(this.PageUrl_UpdateSource);
            WriteWebResponse("LoadXMLSource", sResponse);
            PrintWebResponse("LoadXMLSource", sResponse);

            SetMessage(LogMessageType.Event, "LoadXMLSource: Done");
        }

        private void btn_CreateXMLSource_Click(object sender, EventArgs e)
        {
            try
            {
                ClearMessage();
                CreateXMLSource();
            }
            catch (Exception ex)
            {
                SetMessage(LogMessageType.Error, "CreateXMLSource: Exception: " + ex.Message);
            }
        }

        private void btn_UploadXMLSource_Click(object sender, EventArgs e)
        {
            try
            {
                ClearMessage();
                UploadXMLSource();
            }
            catch (Exception ex)
            {
                SetMessage(LogMessageType.Error, "UploadXMLSource: Exception: " + ex.Message);
            }
        }

        private void btn_LoadXMLSource_Click(object sender, EventArgs e)
        {
            try
            {
                ClearMessage();
                LoadXMLSource();
            }
            catch (Exception ex)
            {
                SetMessage(LogMessageType.Error, "LoadXMLSource: Exception: " + ex.Message);
            }
        }

        private void btn_XMLSource_Click(object sender, EventArgs e)
        {
            try
            {
                ClearMessage();
                CreateXMLSource();
                UploadXMLSource();
                LoadXMLSource();
            }
            catch (Exception ex)
            {
                SetMessage(LogMessageType.Error, "XMLSource: Exception: " + ex.Message);
            }
        }

        #endregion XMLSource
    }
}
