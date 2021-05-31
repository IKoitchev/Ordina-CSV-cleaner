using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CSVcleaner
{
    class Cleaner
    {
        private StreamReader sr;
        private string fileName;
        private string path;
        private StreamWriter outputFile;

        public Cleaner(string path, string fileName)
        {
            this.fileName = fileName;
            this.path = path;
       
            LoadFile(path);
        }

        public void LoadFile(string path)
        {
            sr = new StreamReader(path);
            outputFile = new StreamWriter(Path.Combine(path.Replace(this.fileName, ""),  fileName.Substring(0, fileName.Length - 4) + ".xml"), true);

        }
        public void CleanFile()
        {
            
            int lineCount = 0;
            while (!sr.EndOfStream) // trim the lines 
            {
                string line = sr.ReadLine();        
                string[] elements = line.Split(',');

                if(lineCount == 0)
                    outputFile.WriteLine("<root>");

                if (!string.IsNullOrEmpty(elements[1]))
                {
                    string cleanLine = RemoveFirstTag(elements[1]);
                    cleanLine = RemoveQuotes(cleanLine);
                    outputFile.WriteLine(cleanLine);
                    lineCount++;
                }
                    
                else break;
                // breaking has to be done manually because empty lines are read as ",," and the reading of the file will continue


                if (lineCount % 60000 == 0 && lineCount != 0)
                    MessageBox.Show(lineCount.ToString() + " lines cleaned");



            }


            outputFile.WriteLine("</root>");
            SaveXMLFile();

        }

        private void SaveXMLFile()
        {
            sr.Close();
            outputFile.Close();
            MessageBox.Show("Cleaning Finished");
        }

        public string RemoveQuotes(string line)
        {
            string cleanLine = null;

            //quotes at the end
            string endQuotePattern = "\"$";
            cleanLine = Regex.Replace(line, endQuotePattern, "");

            //double quotes
            string doubleQuotePattern = "\"\"";
            cleanLine = Regex.Replace(cleanLine, doubleQuotePattern, "\"");                 


            return cleanLine;
        }
        public string RemoveFirstTag(string line)
        {
            string cleanLine = null;
            string firstTagPattern = "^(\"<\\?)(.+)(\\?>)";

            cleanLine = Regex.Replace(line, firstTagPattern, "");


            return cleanLine;
        } 
    }
}
