using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using DMSys.Data.SQLiteClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RSSFeeds
{
    public partial class FCategory_Edit : Form
    {
        #region Properties

        private Int32 _CatID = 0;

        private string _ConnectionString = "";
        public string ConnectionString
        {
            set
            { _ConnectionString = value; }
        }

        #endregion Properties

        public FCategory_Edit()
        {
            InitializeComponent();
        }

        public void ShowForm( Int32 aID )
        {
            try
            {
                _CatID = aID;
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            //
            ShowDialog();
        }

        private void LoadData()
        {
            lbl_ID.Text = _CatID.ToString();

            using (SqliteConnection sqlCnnctn = new SqliteConnection())
            {
                sqlCnnctn.ConnectionString = _ConnectionString;
                sqlCnnctn.Open();
                using (SqliteCommand sqlCmmnd = new SqliteCommand())
                {
                    sqlCmmnd.Connection = sqlCnnctn;
                    sqlCmmnd.CommandText =
                        "SELECT nct.news_category " +
                        "     , ifnull(nct.n_dscr, '') AS n_dscr " +
                        "     , src.n_source " +
                        " FROM t_news_category nct " +
                        "    , t_news_sources src " +
                        " WHERE nct.news_category_id = " + _CatID.ToString() +
                        "   AND src.n_source_id = nct.n_source_id ";
                    using (SqliteDataReader sqlDReader = sqlCmmnd.ExecuteReader())
                    {
                        if (sqlDReader.HasRows && sqlDReader.Read())
                        {
                            lbl_NCategory.Text = sqlDReader["news_category"].ToString();
                            lbl_NSource.Text = sqlDReader["n_source"].ToString();
                            tbx_NDscr.Text = sqlDReader["n_dscr"].ToString();
                        }
                        sqlDReader.Close();
                    }
                }
                sqlCnnctn.Close();
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqliteConnection sqlCnnctn = new SqliteConnection())
                {
                    sqlCnnctn.ConnectionString = _ConnectionString;
                    sqlCnnctn.Open();
                    using (SqliteCommand sqlCmmnd = new SqliteCommand())
                    {
                        sqlCmmnd.Connection = sqlCnnctn;
                        sqlCmmnd.CommandText =
                            "UPDATE t_news_category " +
                            "    SET n_dscr = @n_dscr " +
                            " WHERE news_category_id = " + _CatID.ToString();
                        sqlCmmnd.Parameters.Add("@n_dscr", DbType.String).Value = tbx_NDscr.Text.Trim();
                        using (SqliteDataReader sqlDReader = sqlCmmnd.ExecuteReader())
                        {
                            if (sqlDReader.HasRows && sqlDReader.Read())
                            {
                                lbl_NCategory.Text = sqlDReader["news_category"].ToString();
                                lbl_NSource.Text = sqlDReader["n_source"].ToString();
                                tbx_NDscr.Text = sqlDReader["n_dscr"].ToString();
                            }
                            sqlDReader.Close();
                        }
                    }
                    sqlCnnctn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            Close();
        }
    }
}
