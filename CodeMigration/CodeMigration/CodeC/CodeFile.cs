using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CodeMigration.CodeC
{
    [Serializable]
    [XmlInclude(typeof(CodeComment))]
    public class CodeFile
    {
        public List<CodeObject> Objects = new List<CodeObject>();
    }
}
