using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CodeMigration
{
    public partial class Form1 : Form
    {
        private string _AppPath = "";

        public Form1()
        {
            InitializeComponent();
            _AppPath = Path.GetDirectoryName(Application.ExecutablePath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string fileC = @"D:\DProjects\DMSys.Google\Debug\CodeMigration\SCode\shell.c";
                string fileH = "";
                string fileXML = @"D:\DProjects\DMSys.Google\Debug\CodeMigration\BCode\shell.xml";

                CodeC.Migration mgrtn = new CodeC.Migration();
                mgrtn.Parse(fileH, fileC);
                mgrtn.SaveXML(fileXML);

                MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Documet_Click(object sender, EventArgs e)
        {
            try
            {
                string fileC = Path.Combine(_AppPath, @"SCode\shell.c");
                string fileXML = Path.Combine(_AppPath, @"BCode\shell.xml");

                using (StreamWriter sw = new StreamWriter(fileXML, false))
                {
                    using (DocumentC doc = new DocumentC(fileC))
                    {
                        while (!doc.EndOfFile)
                        {
                            string elementText = doc.NextElement().Trim();
                            if (elementText != "")
                            {
                                sw.WriteLine(elementText);
                                sw.WriteLine();
                                sw.Flush();
                            }
                        }
                    }
                    sw.Close();
                }
                MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
