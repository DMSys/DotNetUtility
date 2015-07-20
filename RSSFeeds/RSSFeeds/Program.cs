using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace RSSFeeds
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Входни параметри
            bool bAutoRun = false;
            foreach( string arg in args)
            {
                if (arg == "-autorun")
                { bAutoRun = true; }
            }
            //
            string sApplPath = Path.GetDirectoryName(Application.ExecutablePath);
            string sConnectionString = string.Format( "Version=3,uri=file:{0}"
                                                    , Path.Combine(sApplPath, "nadb.db3"));
            // Автоматично стартиране
            if (bAutoRun)
            {
                try
                {
                    using (TextWriter trLogFile = File.AppendText(Path.Combine(sApplPath, "RSSFeeds.log")))
                    {
                        try
                        {
                            AutoRun_RSSFeeds(trLogFile, sApplPath, sConnectionString);
                        }
                        catch (Exception ex)
                        {
                            trLogFile.WriteLine(ex.Message);
                        }
                    }
                }
                catch
                { }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                frmRss fRss = new frmRss();
                fRss.ApplPath = sApplPath;
                fRss.ConnectionString = sConnectionString;
                Application.Run(fRss);
            }
        }

        /// <summary>
        /// Автоматично зареждане на новини
        /// </summary>
        private static void AutoRun_RSSFeeds(TextWriter aLogFile, string aApplPath, string aConnectionString)
        {
            aLogFile.WriteLine(DateTime.Now.ToString("yyyy.MM.dd HH:mm") + " RSSFeeds");
            aLogFile.Flush();
            using (UCRSSFeeds ucRSSFeeds = new UCRSSFeeds())
            {
                ucRSSFeeds.ConnectionString = aConnectionString;
                ucRSSFeeds.LogFile = aLogFile;
                ucRSSFeeds.LoadNews();
            }
            aLogFile.WriteLine(DateTime.Now.ToString("yyyy.MM.dd HH:mm") + " UpdateData");
            aLogFile.Flush();
            using (UCUpdateData ucUpdateData = new UCUpdateData())
            {
                ucUpdateData.ConnectionString = aConnectionString;
                ucUpdateData.LogFile = aLogFile;
                ucUpdateData.ApplPath = aApplPath;
                ucUpdateData.UpdateData();
            }
        }
    }
}