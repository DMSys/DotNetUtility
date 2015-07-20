using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ToolsRSS.News;
using ToolsRSS.News.Parser;
using System.IO;

namespace RSSFeeds
{
    public partial class UCRSSFeeds : UserControl
    {
        #region Properties

        private string _ConnectionString = "";
        public string ConnectionString
        {
            set
            { _ConnectionString = value; }
        }

        public TextWriter LogFile = null;

        public bool ShowMessage = true;

        #endregion Properties

        public UCRSSFeeds()
        {
            InitializeComponent();
        }

        #region Button WebNews

        private void btnLoadNews_Click(object sender, EventArgs e)
        {
            LoadNews();
        }

        public void LoadNews()
        {
            btnActualnoCom_Click(null,null);
            btnDnesBG_Click(null,null);
            btnNewsBG_Click(null,null);
            btnBGFactorORG_Click(null,null);
            btnInvestorBG_Click(null,null);
            btnDarikNewsBG_Click(null,null);
            btnDirBG_Click(null, null);
            btnSPortalBG_Click(null,null);
            btnHotNewsBG_Click(null, null);
            btnHiCommBG_Click(null, null);
            btnMobileBulgariaCOM_Click(null, null);
            btnDoctorOnlineBG_Click(null, null);
            btnBtvBG_Click(null, null);
            bntCapitalBG_Click(null, null);
            btnIdgBG_Click(null, null);
            btnComputerWorldBG_Click(null, null);
            btnExpertBG_Click(null, null);
            btnGongBG_Click(null, null);
            btnSignalBG_Click(null, null);
            btnZarBG_Click(null, null);
        }

        private void btnActualnoCom_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSActualnoCOM());
        }

        private void btnDnesBG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSDnesBG());
        }

        private void btnNewsBG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSFeedsBG());
        }

        private void btnBGFactorORG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSBGFactorORG());
        }

        private void btnInvestorBG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSInvestorBG());
        }

        private void btnDarikNewsBG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSDarikNewsBG());
        }

        private void btnDirBG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSDirBG());
        }

        private void btnSPortalBG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSSPortalBG());
        }

        private void btnHotNewsBG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSHotNewsBG());
        }

        private void btnHiCommBG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSHiCommBG());
        }

        private void btnMobileBulgariaCOM_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSMobileBulgariaCOM());
        }

        private void btnDoctorOnlineBG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSDoctorOnlineBG());
        }

        private void btnBtvBG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSBtvBG());
        }

        private void bntCapitalBG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSCapitalBG());
        }

        private void btnIdgBG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSIdgBG());
        }

        private void btnComputerWorldBG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSComputerWorldBG());
        }

        private void btnExpertBG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSExpertBG());
        }

        private void btnGongBG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSGongBG());
        }

        private void btnSignalBG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSSignalBG());
        }

        private void btnZarBG_Click(object sender, EventArgs e)
        {
            LoadRSSFeeds(new RSSZarBG());
        }

        #endregion Button WebNews

        private void LogMsg(LogMessageType aMessageType, string aMessage)
        {
            SetMessage(aMessageType, aMessage);
        }

        private void SetMessage(LogMessageType aMessageType, string aMessage)
        {
            if (this.ShowMessage)
            {
                lbx_Msg.Items.Insert(0, aMessage);
                lbx_Msg.Refresh();
            }
            if ((this.LogFile != null) && (aMessageType == LogMessageType.Error))
            {
                this.LogFile.WriteLine(aMessage);
                this.LogFile.Flush();
            }
        }

        private void ClearMessage()
        {
            if (this.ShowMessage)
            {
                lbx_Msg.Items.Clear();
            }
        }

        private void LoadRSSFeeds(IParser aParser)
        {
            ClearMessage();
            //
            using (ENews eNews = new ENews(aParser))
            {
                eNews.ConnectionString = _ConnectionString;
                eNews.Log += new ToolsRSS.News.LogHandler(this.LogMsg);
                eNews.LoadNews();
            }
        }
    }
}
