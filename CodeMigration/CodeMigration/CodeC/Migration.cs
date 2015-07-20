using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CodeMigration.CodeC
{
    public class Migration
    {
        private CodeFile _CFile = new CodeFile();

        public void Parse(string fileH, string fileC)
        {
            _CFile.Objects.Clear();
            if ((fileH != "") && (!File.Exists(fileH)))
            {
                throw new Exception("The file '" + fileH + "' does not exist.");
            }
            if ((fileC != "") && (!File.Exists(fileC)))
            {
                throw new Exception("The file '" + fileC + "' does not exist.");
            }
            
            using (StreamReader sr = new StreamReader(fileC))
            {
                string codeText = "";
                while (!sr.EndOfStream)
                {
                    char ch = (char)sr.Read();
                    if (ch == '\n')
                    { continue; }
                    codeText += ch;

                    CodeObject cObject = ParseObject(sr, codeText);
                    if (cObject != null)
                    {
                        _CFile.Objects.Add(cObject);
                        codeText = "";
                    }
                }
                sr.Close();
            }
        }

        #region Code Parse

        private CodeObject ParseObject(StreamReader sr, string codeText)
        {
            switch (codeText)
            { 
                case "/*":
                    return ParseComment(sr);
                case "#if":
                    return ParsePreprocessorIf(sr);
                default :
                    return null;
            }
        }

        private CodeObject ParseComment(StreamReader sr)
        { 
            char ch1 = ' ';
            if (!sr.EndOfStream)
            { ch1 = (char)sr.Read(); }
            char ch2 = ' ';
            if (!sr.EndOfStream)
            { ch2 = (char)sr.Read(); }
            bool endOfComment = ((ch1 == '*') && (ch2 == '/'));

            StringBuilder sbComment = new StringBuilder();
            while ((!sr.EndOfStream) && (!endOfComment))
            {
                sbComment.Append(ch1);
                ch1 = ch2;
                ch2 = (char)sr.Read();
                endOfComment = ((ch1 == '*') && (ch2 == '/'));
            }
            CodeComment cComment = new CodeComment();
            cComment.Value = sbComment.ToString().Trim();
            return cComment;
        }

        private CodeObject ParsePreprocessorIf(StreamReader sr)
        {
            char ch1 = ' ';
            if (!sr.EndOfStream)
            { ch1 = (char)sr.Read(); }
            char ch2 = ' ';
            if (!sr.EndOfStream)
            { ch2 = (char)sr.Read(); }
            bool endOfComment = ((ch1 == '*') && (ch2 == '/'));

            char[] ddd = { ' ', '\n' };

            StringBuilder sbComment = new StringBuilder();
            while ((!sr.EndOfStream) && (ddd.Where(d => d.Equals(ch1)).Count() == 0))
            {
                sbComment.Append(ch1);
                ch1 = ch2;
                ch2 = (char)sr.Read();
                endOfComment = ((ch1 == '*') && (ch2 == '/'));
            }
            CodeComment cComment = new CodeComment();
            cComment.Value = sbComment.ToString().Trim();
            return cComment;
        }

        #endregion Code Parse

        public void SaveXML(string fileXML)
        {
            using (var writer = new System.IO.StreamWriter(fileXML))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(_CFile.GetType());
                serializer.Serialize(writer, _CFile);
                writer.Flush();
            }
        }
    }
}
