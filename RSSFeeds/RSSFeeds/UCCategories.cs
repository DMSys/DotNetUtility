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
    public partial class UCCategories : UserControl
    {
        #region Properties

        private string _ConnectionString = "";
        public string ConnectionString
        {
            set
            { _ConnectionString = value; }
        }

        #endregion Properties

        public UCCategories()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Зарежда категории
        /// </summary>
        private void UCCategories_Load(object sender, EventArgs e)
        {
            if (_ConnectionString.Equals(""))
            { return; }
            try
            {
                string commandText =
                    "SELECT n_category_id " +
                    "     , n_category " +
                    " FROM tn_categories " +
                    " ORDER BY n_category ";
                dtTNCategory.Rows.Clear();
                dUtils.Fill(commandText, _ConnectionString, dtTNCategory);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cbTNCategories_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// Промяна на категория
        /// Зарежда подкатегории
        /// </summary>
        private void cbTNCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cbTNCategories.SelectedValue == null)
              || cbTNCategories.SelectedValue.ToString().Equals(""))
            { return; }
            try
            {
                int category_id = 0;
                Int32.TryParse(cbTNCategories.SelectedValue.ToString(), out category_id);

                string commandText =
                    "SELECT n_category_sub_id " +
                    "     , n_category_sub " +
                    " FROM tn_categories_sub " +
                    " WHERE n_category_id = " + category_id.ToString() +
                    " ORDER BY n_category_sub ";
                dtTNCategoriesSub.Rows.Clear();
                dUtils.Fill(commandText, _ConnectionString, dtTNCategoriesSub);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cbTNCategoriesSub_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// Промяна на подкатегории
        /// Зарежда категориите на новините
        /// </summary>
        private void cbTNCategoriesSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtCatIN.Rows.Clear();
            dtCatOUT.Rows.Clear();

            if ( (cbTNCategoriesSub.SelectedValue == null)
              || cbTNCategoriesSub.SelectedValue.ToString().Equals(""))
            { return; }
            try
            {
                int sub_category_id = 0;
                Int32.TryParse(cbTNCategoriesSub.SelectedValue.ToString(), out sub_category_id);

                using (SqliteConnection connection = dUtils.OpenConnection(_ConnectionString))
                {
                    // от категорията
                    string commandTextCatIN =
                        "SELECT nct.news_category_id " +
                        "      , ifnull(nct.news_category,'') || ifnull(' - ' || nct.news_dscr,'') AS news_category " +
                        "      , src.n_source " +
                        " FROM t_news_category nct " +
                        "    , x_news_nweb xnn " +
                        "    , t_news_sources src " +
                        " WHERE nct.news_category_id = xnn.news_category_id " +
                        "   AND src.n_source_id = nct.n_source_id " +
                        "   AND xnn.nweb_category_id = " + sub_category_id.ToString() +
                        " ORDER BY nct.news_category ";
                    dUtils.Fill(commandTextCatIN, connection, dtCatIN);

                    // без категория
                    string commandTextCatOUT =
                            "SELECT nct.news_category_id " +
                            "      , ifnull(nct.news_category,'') || ifnull(' - ' || nct.news_dscr,'') AS news_category " +
                            "      , src.n_source " +
                            " FROM t_news_sources src " +
                            "     , t_news_category nct " +
                            " LEFT JOIN x_news_nweb xnn " +
                            "   ON nct.news_category_id = xnn.news_category_id " +
                            " WHERE src.n_source_id = nct.n_source_id " +
                            "   AND ifnull(xnn.nweb_category_id, 0) <= 0 " +
                            " ORDER BY nct.news_category ";
                    dUtils.Fill(commandTextCatOUT, connection, dtCatOUT);

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Премахва категория
        /// </summary>
        private void btnOUT_Click(object sender, EventArgs e)
        {
            if (dgvCatIN.CurrentRow == null)
            { return; }
            Int32 iCategoryID = Convert.ToInt32(dgvCatIN.CurrentRow.Cells["gcCategoryID_IN"].Value);
            try
            {
                using (SqliteConnection sqlCnnctn = new SqliteConnection())
                {
                    sqlCnnctn.ConnectionString = _ConnectionString;
                    sqlCnnctn.Open();

                    using (SqliteCommand sqlCmmnd = new SqliteCommand())
                    {
                        sqlCmmnd.Connection = sqlCnnctn;
                        //
                        sqlCmmnd.CommandText =
                            "UPDATE x_news_nweb " +
                            "   SET nweb_category_id = -1 " +
                            " WHERE news_category_id = " + iCategoryID.ToString();
                        sqlCmmnd.ExecuteNonQuery();
                    }
                    sqlCnnctn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cbTNCategoriesSub_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// Добавя категория
        /// </summary>
        private void btnIN_Click(object sender, EventArgs e)
        {
            if ((cbTNCategoriesSub.SelectedValue == null)
              || cbTNCategoriesSub.SelectedValue.ToString().Equals(""))
            { return; }
            if (dgvCatOUT.CurrentRow == null)
            { return; }
            Int32 iCategoryID = Convert.ToInt32(dgvCatOUT.CurrentRow.Cells["gcCategoryID_OUT"].Value);
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
                            "UPDATE x_news_nweb " +
                            "    SET nweb_category_id = " + cbTNCategoriesSub.SelectedValue.ToString() +
                            " WHERE news_category_id = " + iCategoryID.ToString();
                        sqlCmmnd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cbTNCategoriesSub_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// Рекакция на категория
        /// </summary>
        private void dgvCatOUT_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                using (FCategory_Edit fCat = new FCategory_Edit())
                {
                    fCat.ConnectionString = _ConnectionString;
                    fCat.ShowForm(Convert.ToInt32(dgvCatOUT.CurrentRow.Cells["gcCategoryID_OUT"].Value));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            cbTNCategoriesSub_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// Рекакция на категория
        /// </summary>
        private void dgvCatIN_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                using (FCategory_Edit fCat = new FCategory_Edit())
                {
                    fCat.ConnectionString = _ConnectionString;
                    fCat.ShowForm(Convert.ToInt32(dgvCatIN.CurrentRow.Cells["gcCategoryID_IN"].Value));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            cbTNCategoriesSub_SelectedIndexChanged(null, null);
        }
    }
}
