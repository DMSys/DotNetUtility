using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using DMSys.Data.SQLiteClient;
using System.Data;

namespace RSSFeeds
{
    public static class dUtils
    {
        public static string UrlCombine(string part1, string part2)
        {
            if (part1.EndsWith("/"))
            { return part1 + part2; }
            else
            { return part1 + "/" + part2; }
        }

        public static string DownloadRss(string aUrl)
        {
            string pageContent = "";
            // Open a connection
            HttpWebRequest webRequestObject = (HttpWebRequest)HttpWebRequest.Create(aUrl);
            // You can also specify additional header values like 
            // the user agent or the referer:
            webRequestObject.UserAgent = "RSSFeeds";
            webRequestObject.Referer = "DMSys";

            // Request response:
            WebResponse response = webRequestObject.GetResponse();

            // Open data stream:
            using (Stream webStream = response.GetResponseStream())
            {
                // Create reader object:
                using (StreamReader reader = new StreamReader(webStream))
                {
                    // Read the entire stream content:
                    pageContent = reader.ReadToEnd();
                    // Cleanup
                    reader.Close();
                }
                // Cleanup
                webStream.Close();
            }
            // Cleanup
            response.Close();

            return pageContent.Trim();
        }

        public static SqliteConnection OpenConnection(string connectionString)
        {
            SqliteConnection connection = new SqliteConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            return connection;
        }

        public static int Fill(string commandText, SqliteConnection connection, DataTable dataTable)
        {
            int res = 0;
            using (SqliteCommand command = new SqliteCommand())
            {
                command.Connection = connection;
                command.CommandText = commandText;
                using (SqliteDataAdapter sqlDAdapter = new SqliteDataAdapter(command))
                {
                    res = sqlDAdapter.Fill(dataTable);
                }
            }
            return res;
        }

        public static int Fill(string commandText, string connectionString, DataTable dataTable)
        {
            int res = 0;
            using (SqliteConnection connection = dUtils.OpenConnection(connectionString))
            {
                using (SqliteCommand command = new SqliteCommand())
                {
                    command.Connection = connection;
                    command.CommandText = commandText;
                    using (SqliteDataAdapter sqlDAdapter = new SqliteDataAdapter(command))
                    {
                        res = sqlDAdapter.Fill(dataTable);
                    }
                }
                connection.Close();
            }
            return res;
        }
    }
}
