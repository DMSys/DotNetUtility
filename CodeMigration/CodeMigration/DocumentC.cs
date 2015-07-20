using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CodeMigration
{
    public class DocumentC: IDisposable
    {
        private StreamReader _SRFile = null;

        /// <summary>
        /// Следващия символ / стойност
        /// </summary>
        private char? _NextChar_Value = null;

        /// <summary>
        /// Следващия символ / разделител?
        /// </summary>
        private bool _NextChar_IsSeparator = false;

        public DocumentC(string fileName)
        {
            _SRFile = new StreamReader(fileName);
        }

        public void Dispose()
        {
            if (_SRFile != null)
            {
                _SRFile.Close();
                _SRFile.Dispose();
                _SRFile = null;
            }
        }

        public bool EndOfFile
        {
            get
            { return _SRFile.EndOfStream; }
        }

        public string NextElement()
        {
            string elementText = "";
            // Дава разделителя, ако има
            if (_NextChar_IsSeparator && (_NextChar_Value != null))
            {
                elementText = _NextChar_Value.ToString();
                switch(_NextChar_Value)
                {
                    case '+':
                    case '=':
                    case '-':
                        if (!_SRFile.EndOfStream)
                        {
                            char ch = (char)_SRFile.Read();
                            if (ch == _NextChar_Value)
                            {
                                elementText += ch.ToString();
                                _NextChar_Value = null;
                                _NextChar_IsSeparator = false;
                            }
                            else
                            {
                                _NextChar_Value = ch;
                                _NextChar_IsSeparator = false;
                            }
                        }
                        break;
                    default:
                        _NextChar_Value = null;
                        _NextChar_IsSeparator = false;
                        break;
                }
                return elementText;
            }
            else
            {
                _NextChar_IsSeparator = false;
            }
            // Чете до следващия разделител
            while ((!_SRFile.EndOfStream) && (!_NextChar_IsSeparator))
            {
                char ch = ' ';
                if (_NextChar_Value != null)
                {
                    ch = (char)_NextChar_Value;
                    _NextChar_Value = null;
                }
                else
                {
                    ch = (char)_SRFile.Read();
                }
                switch(ch)
                {
                    case ' ':
                    case '\n':
                        _NextChar_Value = null;
                        _NextChar_IsSeparator = true;
                        break;
                    case '{':
                    case '}':
                    case '(':
                    case ')':
                    case '!':
                    case ';':
                    case '-':
                    case '+':
                    case '=':
                    case ',':
                    case '>':
                    case '<':
                        _NextChar_Value = ch;
                        _NextChar_IsSeparator = true;
                        break;
                    default:
                        elementText += ch;
                        break;
                }
                // Ако е начало на блок
                if (elementText.StartsWith("/*"))
                {
                    elementText = ParseCommentBlock();
                    _NextChar_Value = null;
                    _NextChar_IsSeparator = true;
                }
                else if (elementText.StartsWith("\""))
                {
                    elementText = ParseString('"');
                    _NextChar_Value = null;
                    _NextChar_IsSeparator = true;
                }
                else if (elementText.StartsWith("\'"))
                {
                    elementText = ParseString('\'');
                    _NextChar_Value = null;
                    _NextChar_IsSeparator = true;
                }
                else if (elementText.StartsWith("#"))
                {
                    elementText = ParsePreprocessor();
                    if (elementText == "#include")
                    { elementText += " " + ReadToRowEnd().Trim(); }
                    _NextChar_Value = null;
                    _NextChar_IsSeparator = true;
                }
            }
            return elementText;
        }

        /// <summary>
        /// Прочита реда до края
        /// </summary>
        private string ReadToRowEnd()
        {
            StringBuilder sbElement = new StringBuilder();
            bool endOfElement = false;
            char ch1 = ' ';
            while ((!_SRFile.EndOfStream) && (!endOfElement))
            {
                ch1 = (char)_SRFile.Read();
                endOfElement = (ch1 == '\n');
                if (!endOfElement)
                { sbElement.Append(ch1); }
            }
            return sbElement.ToString();
        }

        private string ParseCommentBlock()
        {
            StringBuilder sbElement = new StringBuilder();
            bool endOfElement = false;
            char ch1 = '/';
            char ch2 = '*';
            while ((!_SRFile.EndOfStream) && (!endOfElement))
            {
                sbElement.Append(ch1);
                ch1 = ch2;
                ch2 = (char)_SRFile.Read();
                endOfElement = ((ch1 == '*') && (ch2 == '/'));
            }
            sbElement.Append(ch1);
            sbElement.Append(ch2);
            return sbElement.ToString();
        }

        private string ParseString(char separator = '"')
        {
            StringBuilder sbElement = new StringBuilder();
            bool endOfElement = false;
            char ch1 = separator;
            char ch_pr = ' ';
            while ((!_SRFile.EndOfStream) && (!endOfElement))
            {
                sbElement.Append(ch1);
                ch_pr = ch1;
                ch1 = (char)_SRFile.Read();
                endOfElement = ((ch1 == separator) && (ch_pr != '\\'));
            }
            sbElement.Append(ch1);
            return sbElement.ToString();
        }

        private string ParsePreprocessor()
        {
            StringBuilder sbElement = new StringBuilder();
            bool endOfElement = false;
            bool startWord = false;
            char ch1 = '#';
            while ((!_SRFile.EndOfStream) && (!endOfElement))
            {
                if (ch1 != ' ')
                { sbElement.Append(ch1); }
                ch1 = (char)_SRFile.Read();

                if (startWord)
                { endOfElement = (ch1 == ' '); }
                else
                { startWord = (ch1 != ' '); }
                if (ch1 == '\n')
                { endOfElement = true; }
            }
            return sbElement.ToString();
        }
    }
}