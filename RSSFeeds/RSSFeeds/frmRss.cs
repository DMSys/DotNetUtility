using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RSSFeeds
{
    public partial class frmRss : Form
    {
        private string _ApplPath = "";
        public string ApplPath
        {
            set
            {
                _ApplPath = value;
                //
                ucUpdateD.ApplPath = _ApplPath;
            }
        }

        private string _ConnectionString = "";
        public string ConnectionString
        {
            set
            {
                _ConnectionString = value; 
                //
                ucNews.ConnectionString = _ConnectionString;
                //
                ucCat.ConnectionString = _ConnectionString;
                //
                ucTest1.ConnectionString = _ConnectionString;
                //
                ucTools1.ConnectionString = _ConnectionString;
                //
                ucUpdateD.ConnectionString = _ConnectionString;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public frmRss()
        {
            InitializeComponent();
        }
    }
}
