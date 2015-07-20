using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeMigration.CodeC
{
    [Serializable]
    public class CodeObject
    {
    }

    [Serializable]
    public class CodeComment : CodeObject
    {
        public string Value = "";
    }

    [Serializable]
    public class CodePreprocessorIf : CodeObject
    {
        public List<CodeObject> Expression = new List<CodeObject>();
    }
}
