using System;
using System.Collections.Generic;
using System.Text;
using ToolsRSS.News.Parser;
using System.Xml;
using System.Xml.XPath;
using System.Data;
using DMSys.Data.SQLiteClient;

namespace ToolsRSS.News
{
    public class ENews : IDisposable
    {
        #region Properties

        private string _Msg1_ExemptionТmpТables = ".1. Освобождата tmp таблиците";
        private string _Msg2_DownloadedAndFilledNews = ".2. Изтегля и зарежда новините";
        private string _Msg3_NewsChargedHandle = ".3. Обработва заредените новини";
        private string _Msg4_LoadingNews = ".4. Зарежда новините";
        private string _Msg5_Done = ".5. Done";

        private NewsItems _News = new NewsItems();

        private IParser _PNews = null;

        private string _ConnectionString = "";
        public string ConnectionString
        {
            set
            { _ConnectionString = value; }
        }

        #endregion Properties

        public event LogHandler Log;

        public ENews(IParser aParser)
        {
            // Парсер на новини
            if (_PNews != null)
            {
                _PNews.Dispose();
                _PNews = null;
            }
            _PNews = aParser;
        }

        public void Dispose()
        {
            // Парсер на новини
            if (_PNews != null)
            {
                _PNews.Dispose();
                _PNews = null;
            }
            //
            _News.Dispose();
        }

        protected void OnLog(LogMessageType aMessageType, string aMessage)
        {
            if (Log != null)
            { Log(aMessageType, aMessage); }
        }

        public void LoadNews()
        {
            try
            {
                using (SqliteConnection sqlCnnctn = new SqliteConnection())
                {
                    sqlCnnctn.ConnectionString = _ConnectionString;
                    sqlCnnctn.Open();
                    //
                    using (SqliteCommand sqlCmmndNews = new SqliteCommand())
                    {
                        using (SqliteCommand sqlCmmndEncl = new SqliteCommand())
                        {
                            sqlCmmndNews.Connection = sqlCnnctn;
                            sqlCmmndEncl.Connection = sqlCnnctn;

                            // сваля и зарежда новините
                            foreach (NewsSource ns in _PNews.NSource)
                            {
                                try
                                {
                                    OnLog(LogMessageType.Event, ns.Title);
                                    //
                                    LoadNewsRSS(sqlCmmndNews, sqlCmmndEncl, ns);
                                    //
                                    OnLog(LogMessageType.Event, _PNews.WSaitID.ToString() + _Msg3_NewsChargedHandle);
                                    TmpNewsRepair(sqlCmmndNews);
                                    //
                                    OnLog(LogMessageType.Event, _PNews.WSaitID.ToString() + _Msg4_LoadingNews);
                                    TmpNewsTransfer(sqlCmmndNews, sqlCmmndEncl);
                                    //
                                    OnLog(LogMessageType.Event, _PNews.WSaitID.ToString() + _Msg5_Done);
                                }
                                catch (Exception ex)
                                {
                                    OnLog(LogMessageType.Error, "Exception /Load:" + ns.URL + "/: " + ex.Message);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                OnLog(LogMessageType.Error, "Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// Сваля новините и ги зарежда в tmp таблици
        /// </summary>
        private void LoadNewsRSS(SqliteCommand aCmmndNews, SqliteCommand aCmmndEncl, NewsSource aNSource)
        {
            // Освобождата tmp таблиците
            OnLog(LogMessageType.Event, _PNews.WSaitID.ToString() + _Msg1_ExemptionТmpТables);
            // tmp_news
            aCmmndNews.CommandText = "DELETE FROM tmp_news;";
            aCmmndNews.ExecuteNonQuery();

            // tmp_enclosure
            aCmmndEncl.CommandText = "DELETE FROM tmp_enclosure;";
            aCmmndEncl.ExecuteNonQuery();

            // Зарежда tmp таблиците
            // tmp_news
            aCmmndNews.CommandText = 
                "INSERT INTO tmp_news " +
                "        (  news_site_id,  news_id,  news_guid,  news_title,  news_link,  news_description,  news_body, news_comments,  news_pubdate,  news_category ) " +
                " VALUES ( @news_site_id, @news_id, @news_guid, @news_title, @news_link, @news_description, @news_body, @news_comments, @news_pubdate, @news_category ); ";
            aCmmndNews.Parameters.Clear();
            aCmmndNews.Parameters.Add("@news_site_id", DbType.Int32).Value = _PNews.WSaitID;
            aCmmndNews.Parameters.Add("@news_id", DbType.Int32);
            aCmmndNews.Parameters.Add("@news_guid", DbType.String);
            aCmmndNews.Parameters.Add("@news_title", DbType.String);
            aCmmndNews.Parameters.Add("@news_link", DbType.String);
            aCmmndNews.Parameters.Add("@news_description", DbType.String);
            aCmmndNews.Parameters.Add("@news_body", DbType.String);
            aCmmndNews.Parameters.Add("@news_comments", DbType.String);
            aCmmndNews.Parameters.Add("@news_pubdate", DbType.DateTime);
            aCmmndNews.Parameters.Add("@news_category", DbType.String);

            // tmp_enclosure
            aCmmndEncl.CommandText = 
                "INSERT INTO tmp_enclosure " +
                "        (  news_id,  encl_link,  encl_type ) " +
                " VALUES ( @news_id, @encl_link, @encl_type ); ";
            aCmmndEncl.Parameters.Clear();
            aCmmndEncl.Parameters.Add("@news_id", DbType.Int32);
            aCmmndEncl.Parameters.Add("@encl_link", DbType.String);
            aCmmndEncl.Parameters.Add("@encl_type", DbType.String);

            //
            OnLog(LogMessageType.Event, _PNews.WSaitID.ToString() + _Msg2_DownloadedAndFilledNews);
            // create a new xml doc
            XmlDocument doc = new XmlDocument();
            doc.XmlResolver = null;

            // load the xml doc
            // doc.Load(aNSource.URL);
            string rssPage = RSSFeeds.dUtils.DownloadRss(aNSource.URL);
            int indexEnd = rssPage.IndexOf("</rss>");
            if (indexEnd > 0)
            { 
                indexEnd += 6;
                int endCount = rssPage.Length - indexEnd;
                rssPage = rssPage.Remove(indexEnd, endCount);
            }
            doc.LoadXml(rssPage);

            // get an xpath navigator   
            XPathNavigator navigator = doc.CreateNavigator();
            NewsItem NData = new NewsItem();

            // Get the links from the RSS feed
            XPathNodeIterator nodesLink = navigator.Select("//rss/channel/item");
            int iNewsID = 1;
            while (nodesLink.MoveNext())
            {
                NData.Clear();
                // clean up the link
                XPathNodeIterator RNode = nodesLink.Current.SelectChildren(XPathNodeType.Element);
                //
                bool bRes = true;
                while (RNode.MoveNext() && bRes)
                {
                    bRes = _PNews.ParseNews(NData, RNode.Current);
                }
                // Ако е успешно заредена новината
                if (bRes)
                {
                    // tmp_news
                    aCmmndNews.Parameters["@news_id"].Value = iNewsID;
                    aCmmndNews.Parameters["@news_guid"].Value = NData.Guid;
                    aCmmndNews.Parameters["@news_title"].Value = NData.Title;
                    aCmmndNews.Parameters["@news_link"].Value = NData.Link;
                    aCmmndNews.Parameters["@news_description"].Value = NData.Description;
                    aCmmndNews.Parameters["@news_body"].Value = NData.Body;
                    aCmmndNews.Parameters["@news_comments"].Value = NData.Comments;
                    aCmmndNews.Parameters["@news_pubdate"].Value = NData.pubDate;
                    aCmmndNews.Parameters["@news_category"].Value = (NData.Category.Equals("") ? aNSource.DefCategory : NData.Category).ToLower().Trim();
                    
                    aCmmndNews.ExecuteNonQuery();

                    // tmp_enclosure
                    aCmmndEncl.Parameters["@news_id"].Value = iNewsID;
                    foreach (Enclosure encl in NData.Enclosure)
                    {
                        aCmmndEncl.Parameters["@encl_link"].Value = encl.Link;
                        aCmmndEncl.Parameters["@encl_type"].Value = encl.Type;

                        aCmmndEncl.ExecuteNonQuery();
                    }
                    //
                    iNewsID++;
                }
                NData.Clear();
            }
        }

        /// <summary>
        /// Обработва новните във временните таблици
        /// </summary>
        public void TmpNewsRepair(SqliteCommand aCmmndNews)
        {
            // Изтрива старите новини
            aCmmndNews.CommandText =
               "DELETE FROM tmp_news " +
               " WHERE news_pubdate < @FromDate ";
            aCmmndNews.Parameters.Clear();
            aCmmndNews.Parameters.Add("@FromDate", DbType.Date).Value = DateTime.Now.AddMonths(-1);
            aCmmndNews.ExecuteNonQuery();

            // Изтрива новини с бъдеща дата
            aCmmndNews.CommandText =
               "DELETE FROM tmp_news " +
               " WHERE news_pubdate > @FromDate ";
            aCmmndNews.Parameters.Clear();
            aCmmndNews.Parameters.Add("@FromDate", DbType.Date).Value = DateTime.Now.AddDays(1);
            aCmmndNews.ExecuteNonQuery();
            //
            aCmmndNews.Parameters.Clear();
            // Изтрива дублиращите се новини
            aCmmndNews.CommandText =
                "DELETE FROM tmp_news " +
                " WHERE tmp_news.news_id in (SELECT tmp_news.news_id " +
                                           " FROM news " +
                                           " INNER JOIN tmp_news " +
                                           "    ON (news.news_guid = tmp_news.news_guid) " +
                                           "   AND (news.news_site_id = tmp_news.news_site_id)); ";
            aCmmndNews.ExecuteNonQuery();

            // Изтрива новините без картинки
            aCmmndNews.CommandText =
                "DELETE FROM tmp_news " +
                " WHERE news_id NOT IN ( SELECT news_id FROM tmp_enclosure ) ";
            OnLog(LogMessageType.Event, "- Изтрити новините без картинки: " + aCmmndNews.ExecuteNonQuery().ToString());

            // Новини за добавяне
            using (DataTable dtNews = new DataTable())
            {
                aCmmndNews.CommandText = 
                    "SELECT tmp_news.news_id " +
                    " FROM tmp_news ";
                using (SqliteDataAdapter sqlDAdapter = new SqliteDataAdapter(aCmmndNews))
                {
                    dtNews.Clear();
                    sqlDAdapter.Fill(dtNews);
                }
                // Ако няма новини - КРАЙ
                if (dtNews.Rows.Count == 0)
                { return; }
            }
            // Определя типа на Enclosure
            aCmmndNews.CommandText = 
                "UPDATE tmp_enclosure  " +
                "   SET encl_id = (SELECT encl.encl_id " +
                                 " FROM t_enclosure encl " +
                                 " WHERE tmp_enclosure.encl_type = encl.encl_type); ";
            aCmmndNews.ExecuteNonQuery();

            // Проверка за непознат тип на Enclosure
            aCmmndNews.CommandText = "SELECT tmp_enclosure.encl_type "
                                   + " FROM tmp_enclosure "
                                   + " WHERE ( tmp_enclosure.encl_id = 0 ); ";
            using (SqliteDataReader dr = aCmmndNews.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    string sMsg = "Неразпознат тип на 'Enclosure' !";
                    if (dr.Read())
                    {
                        sMsg += dr["encl_type"].ToString();
                    }
                    throw new Exception(sMsg);
                }
                dr.Close();
            }

            // Категории на новините
            /* прецаква кодовата таблица
            // Стандартизира news_category
            aCmmndNews.CommandText =
                    "UPDATE tmp_news " +
                    " SET news_category = lower(trim(news_category)); ";
            aCmmndNews.ExecuteNonQuery();
            */

            // Определя категориите на новините
            SetNewsCategory(aCmmndNews);
            while (HasNewCategory(aCmmndNews))
            {
                AddNewCategory(aCmmndNews);
                SetNewsCategory(aCmmndNews);
            }
            // Връзка м/у таблициците с категории
            aCmmndNews.CommandText =
                "INSERT INTO x_news_nweb " +
                " ( news_category_id, nweb_category_id ) " +
                " SELECT nct.news_category_id " +
                "      , 0 " +
                " FROM t_news_category nct " +
                " LEFT JOIN x_news_nweb xnn " +
                "   ON nct.news_category_id = xnn.news_category_id " +
                " WHERE ifnull(xnn.news_category_id, 0) <= 0 " +
                " ORDER BY nct.news_category_id; ";

            aCmmndNews.ExecuteNonQuery();
        }

        /// <summary>
        /// Определя категорията на новината
        /// </summary>
        private void SetNewsCategory(SqliteCommand aSQLCmmnd)
        {
            aSQLCmmnd.CommandText =
                "UPDATE tmp_news " +
                "   SET news_category_id = (SELECT nctg.news_category_id " +
										   " FROM t_news_category nctg " +
										   " WHERE tmp_news.news_category = nctg.news_category " +
                                           "   AND tmp_news.news_site_id = nctg.news_site_id); ";

            aSQLCmmnd.ExecuteNonQuery();
        }

        /// <summary>
        /// Проверява за нови категории
        /// </summary>
        private bool HasNewCategory(SqliteCommand aSQLCmmnd)
        {
            bool bRes = false;
            //
            aSQLCmmnd.CommandText =
                "SELECT news_category " +
                " FROM tmp_news " +
                " WHERE ( ifnull(news_category_id, 0) = 0 ); ";
            using (SqliteDataReader dr = aSQLCmmnd.ExecuteReader())
            {
                bRes = dr.HasRows;
                dr.Close();
            }
            return bRes;
        }

        /// <summary>
        /// Добавя новите категории
        /// </summary>
        private void AddNewCategory(SqliteCommand aSQLCmmnd)
        {
            aSQLCmmnd.CommandText =
                "INSERT INTO t_news_category " +
                " ( news_category_id, news_category, news_site_id ) " +
                " SELECT (SELECT ifnull(MAX(news_category_id),0) +1 FROM t_news_category) " +
                "      , news_category " +
                "      , news_site_id " +
                " FROM tmp_news nws " +
                " WHERE ifnull(nws.news_category_id, 0) = 0 " +
                " LIMIT 1 ";

            aSQLCmmnd.ExecuteNonQuery();
        }

        /// <summary>
        /// Зарежда новите новини
        /// </summary>
        public void TmpNewsTransfer(SqliteCommand aCmmndNews, SqliteCommand aCmmndEncl)
        {
            DataTable dtNews = new DataTable();

            aCmmndNews.CommandText =
                "SELECT tmp_news.news_id FROM tmp_news ";
            using (SqliteDataAdapter sqlDAdapter = new SqliteDataAdapter(aCmmndNews))
            {
                dtNews.Clear();
                sqlDAdapter.Fill(dtNews);
            }

            // Ако няма новини - КРАЙ
            if (dtNews.Rows.Count == 0)
            { return; }

            Int32 iNewsID = 0;
            // ID на последната новина
            aCmmndNews.CommandText = 
                "SELECT ifnull(max(news.news_id),0) AS news_id " +
                " FROM news ";
            using (SqliteDataReader dr = aCmmndNews.ExecuteReader())
            {
                if (dr.HasRows && dr.Read())
                {
                    if (dr["news_id"].ToString().Equals(""))
                    {
                        iNewsID = 1;
                    }
                    else
                    {
                        iNewsID = Convert.ToInt32(dr["news_id"]) + 1;
                    }
                }
                else
                {
                    throw new Exception("Exception: TmpNewsTransfer");
                }
                dr.Close();
            }

            // Новини
            aCmmndNews.CommandText = "INSERT INTO news ( news_id, news_site_id, news_guid, news_title, news_link, news_description, news_body, news_comments, news_pubdate, news_category_id ) "
                                   + " SELECT @news_id "
                                   + "      , tmp_news.news_site_id "
                                   + "      , tmp_news.news_guid "
                                   + "      , tmp_news.news_title "
                                   + "      , tmp_news.news_link "
                                   + "      , tmp_news.news_description "
                                   + "      , tmp_news.news_body "
                                   + "      , tmp_news.news_comments "
                                   + "      , tmp_news.news_pubdate "
                                   + "      , tmp_news.news_category_id "
                                   + " FROM tmp_news "
                                   + " WHERE ( tmp_news.news_id = @tmp_news_id ); ";
            aCmmndNews.Parameters.Clear();
            aCmmndNews.Parameters.Add("@news_id", DbType.Int32);
            aCmmndNews.Parameters.Add("@tmp_news_id", DbType.Int32);

            // Новини - Картинки
            aCmmndEncl.CommandText = "INSERT INTO enclosure ( news_id, encl_link, encl_id ) "
                                   + " SELECT @news_id "
                                   + "      , tmp_enclosure.encl_link "
                                   + "      , tmp_enclosure.encl_id "
                                   + " FROM tmp_enclosure "
                                   + " WHERE ( tmp_enclosure.news_id = @tmp_news_id ); ";
            aCmmndEncl.Parameters.Clear();
            aCmmndEncl.Parameters.Add("@news_id", DbType.Int32);
            aCmmndEncl.Parameters.Add("@tmp_news_id", DbType.Int32);

            foreach (DataRow dr in dtNews.Rows)
            {
                //
                aCmmndNews.Parameters["@news_id"].Value = iNewsID;
                aCmmndNews.Parameters["@tmp_news_id"].Value = dr["news_id"];

                aCmmndNews.ExecuteNonQuery();

                aCmmndEncl.Parameters["@news_id"].Value = iNewsID;
                aCmmndEncl.Parameters["@tmp_news_id"].Value = dr["news_id"];

                aCmmndEncl.ExecuteNonQuery();

                iNewsID++;
            }
        }
    }
}
