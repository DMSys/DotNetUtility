using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DMSys.Data.SQLiteClient;

namespace RSSFeeds
{
    public partial class UCTools : UserControl
    {
        private string _ConnectionString = "";
        public string ConnectionString
        {
            set
            { _ConnectionString = value; }
        }

        public UCTools()
        {
            InitializeComponent();
        }

        private void SetMessage(string aMessage)
        {
            listBox1.Items.Insert(0, aMessage);
            listBox1.Refresh();
        }

        /// <summary>
        /// Изтрива невалидни записи
        /// </summary>
        private void btnDeleteInvalidEntries_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                SetMessage("Старт: Изтрива невалидни записи");

                using (SqliteConnection sqlCnnctn = new SqliteConnection())
                {
                    sqlCnnctn.ConnectionString = _ConnectionString;
                    sqlCnnctn.Open();

                    using (SqliteCommand sqlCmmnd = new SqliteCommand())
                    {
                        sqlCmmnd.Connection = sqlCnnctn;

                        // Новини без картинки
                        sqlCmmnd.CommandText =
                            "DELETE FROM news " +
                            " WHERE news_id NOT IN (SELECT DISTINCT news_id FROM enclosure ) ";
                        Int32 iRows = sqlCmmnd.ExecuteNonQuery();
                        SetMessage("Изтити новини без картинки: " + iRows.ToString());

                        // Картинки без новини
                        sqlCmmnd.CommandText =
                            "DELETE FROM enclosure " +
                            " WHERE news_id NOT IN (SELECT news_id FROM news) ";
                        iRows = sqlCmmnd.ExecuteNonQuery();

                        SetMessage("Изтити картинки без новини: " + iRows.ToString());
                    }
                    sqlCnnctn.Close();
                }
                SetMessage("Край");
            }
            catch (Exception ex)
            {
                SetMessage(ex.Message);
            }
        }

        /// <summary>
        /// Изтрива старите новини
        /// </summary>
        private void btnDeleteOldNews_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                SetMessage("Старт: Изтрива старите новини");

                using (SqliteConnection sqlCnnctn = new SqliteConnection())
                {
                    sqlCnnctn.ConnectionString = _ConnectionString;
                    sqlCnnctn.Open();

                    using (SqliteCommand sqlCmmnd = new SqliteCommand())
                    {
                        sqlCmmnd.Connection = sqlCnnctn;
                        sqlCmmnd.CommandText =
                           "DELETE FROM news " +
                           " WHERE news_pubdate < @FromDate ";
                        sqlCmmnd.Parameters.Clear();
                        sqlCmmnd.Parameters.Add("@FromDate", DbType.Date).Value = DateTime.Now.AddMonths(-1).AddDays(-1);
                        int iDelNews = sqlCmmnd.ExecuteNonQuery();

                        SetMessage("Delete News: " + iDelNews.ToString());
                    }
                    sqlCnnctn.Close();
                }
                SetMessage("Край");
            }
            catch (Exception ex)
            {
                SetMessage(ex.Message);
            }
        }

        /// <summary>
        /// Оптимиьира базата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Optimization_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                SetMessage("Старт: Оптимизация");

                using (SqliteConnection sqlCnnctn = new SqliteConnection())
                {
                    sqlCnnctn.ConnectionString = _ConnectionString;
                    sqlCnnctn.Open();

                    using (SqliteCommand sqlCmmnd = new SqliteCommand())
                    {
                        sqlCmmnd.Connection = sqlCnnctn;

                        SetMessage("Старт: ReIndex");
                        sqlCmmnd.CommandText = "REINDEX;";
                        sqlCmmnd.ExecuteNonQuery();
                        SetMessage("Край: ReIndex");

                        SetMessage("Старт: Compact the database");
                        sqlCmmnd.CommandText = "VACUUM;";
                        sqlCmmnd.ExecuteNonQuery();
                        SetMessage("Край: Compact the database");
                    }
                    sqlCnnctn.Close();
                }
                SetMessage("Край: Оптимизация");
            }
            catch (Exception ex)
            {
                SetMessage(ex.Message);
            }
        }
    }
}
