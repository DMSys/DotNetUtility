using System;
using System.Collections.Generic;
using System.Text;

namespace ToolsRSS.News.Parser
{
    public static class PTools
    {
        public static string GetLinkURL(string aValue)
        {
            return aValue.Replace("&amp;", "&").Replace("&", "&amp;");
        }

        public static string GetWebText(string aValue)
        {
            return aValue
                .Replace("<br>", "")
                .Replace("\"", "&quot;")
                .Replace('\n', ' ')
                .Replace('\r', ' ')
                .Replace("  ", " ")
                .Replace("„", "&quot;")
                .Replace("”", "&quot;")
                .Replace("“", "&quot;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Trim();
        }

        public static string RemoveWebTag(string aValue)
        {
            string sValue = aValue.Trim();
            //
            string sRValue = "";
            bool bInTab = false;
            for (int i = 0; i < sValue.Length; i++)
            {
                if (bInTab)
                {
                    if (sValue[i].Equals('>'))
                    { bInTab = false; }
                }
                else
                {
                    if (sValue[i].Equals('<'))
                    { bInTab = true; }
                    else
                    { sRValue += sValue[i]; }
                }
            }
            //
            return sRValue.Trim();
        }

        public static DateTime GetWebDateTime(DateTime aValue)
        {
            return new DateTime(aValue.Year, aValue.Month, aValue.Day, aValue.Hour, aValue.Minute, aValue.Second);
        }

        public static string GetEnclosure(string aValue)
        {
            if (aValue.Length < 10)
            { return ""; }
            //
            bool bIsFind = false;
            // Начало на тага
            int iImgStart = 0;
            for (int i = 0; (i < aValue.Length - 4) && (!bIsFind); i++)
            {
                if (aValue.Substring(i, 4).Equals("<img"))
                {
                    iImgStart = i + 4;
                    bIsFind = true;
                }
            }
            if (!bIsFind)
            { return ""; }
            // начало на картинката
            bIsFind = false;
            int iIMGFrom = 0;
            for (int i = iImgStart; (i < aValue.Length - 5) && (!bIsFind); i++)
            {
                if (aValue.Substring(i, 5).Equals("src=\""))
                {
                    iIMGFrom = i + 5;
                    bIsFind = true;
                }
            }
            if (!bIsFind)
            { return ""; }
            // Край на картинката
            bIsFind = false;
            int iIMGTo = 0;
            for (int i = iIMGFrom; (i < aValue.Length) && (!bIsFind); i++)
            {
                if (aValue[i] == '"')
                {
                    iIMGTo = i;
                    bIsFind = true;
                }
            }
            if (!bIsFind)
            { return ""; }
            //
            string sIMG = "";
            if (iIMGTo > iIMGFrom)
            {
                sIMG = aValue.Substring(iIMGFrom, iIMGTo - iIMGFrom).Trim();
            }
            return sIMG;
        }

        public static Int32 DateTimeToInt32(DateTime aValue)
        {
            return aValue.Second +
                  (aValue.Minute * 60) +
                  (aValue.Hour * 60 * 60) +
                  (aValue.Day * 60 * 60 * 24) +
                  (aValue.Month * 60 * 60 * 24 * 31) +
                  (aValue.Year * 60 * 60 * 24 * 31 * 12);
        }

        public static string GetMD5Hash(string input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }
    }
}
