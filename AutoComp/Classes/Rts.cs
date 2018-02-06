using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace Positivo.AutoComp
{
    /// <summary>
    /// Class for helping with INI files
    /// </summary>
    public class RtsLog
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RtsLog()
        {
            LoadFile();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="FileName">Name of the file</param>
        public RtsLog(string FileName)
        {
            this.FileName = FileName;
            LoadFile();
        }
        #endregion

        #region Public Functions

        /// <summary>
        /// Returns an XML representation of the INI file
        /// </summary>
        /// <returns>An XML representation of the INI file</returns>
        public string ToXML()
        {
            if (string.IsNullOrEmpty(this.FileName))
                return "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<INI>\r\n</INI>";
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
            Builder.Append("<INI>\r\n");
            foreach (string Header in _FileContents.Keys)
            {
                Builder.Append("<section name=\"" + Header + "\">\r\n");
                foreach (string Key in _FileContents[Header].Keys)
                {
                    Builder.Append("<key name=\"" + Key + "\">" + _FileContents[Header][Key] + "</key>\r\n");
                }
                Builder.Append("</section>\r\n");
            }
            Builder.Append("</INI>");
            return Builder.ToString();
        }

        /// <summary>
        /// Returns an string formar representation of the INI file
        /// </summary>
        /// <returns>An string representation of the INI file</returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.FileName))
                return _FileName;
            StringBuilder Builder = new StringBuilder();
            foreach (string Header in _FileContents.Keys)
            {
                Builder.Append("[" + Header + "]\r\n");
                foreach (string Key in _FileContents[Header].Keys)
                {
                    Builder.Append(Key + "=" + _FileContents[Header][Key] + "\r\n");
                }
            }
            return Builder.ToString();
        }
        #endregion

        #region Private Functions

        /// <summary>
        /// Loads an INI file
        /// </summary>
        private void LoadFile()
        {
            _FileContents = new Dictionary<string, Dictionary<string, string>>();
            if (string.IsNullOrEmpty(this.FileName))
                return;

            string Content = File.ReadAllText(FileName);
            string Contents = Content.Replace("\t", "");
            Regex Section = new Regex("[" + Regex.Escape(" ") + "\t]*" + Regex.Escape("[") + ".*" + Regex.Escape("]\r\n"));
            string[] Sections = Section.Split(Contents);
            MatchCollection SectionHeaders = Section.Matches(Contents);
            int Counter = 1;
            foreach (Match SectionHeader in SectionHeaders)
            {
                string[] Splitter = { "\r\n" };
                string[] Splitter2 = { "=" };
                string[] Items = Sections[Counter].Split(Splitter, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, string> SectionValues = new Dictionary<string, string>();
                foreach (string Item in Items)
                {
                    SectionValues.Add(Item.Split(Splitter2, StringSplitOptions.None)[0], Item.Split(Splitter2, StringSplitOptions.None)[1]);
                }
                _FileContents.Add(SectionHeader.Value.Replace("[", "").Replace("]\r\n", ""), SectionValues);
                ++Counter;
            }

            var badKeys = _FileContents["TEST_DATA"].Where(pair => (pair.Value == "") | (pair.Value == "--"))
                        .Select(pair => pair.Key)
                        .ToList();

            foreach (var badKey in badKeys)
            {
                _FileContents["TEST_DATA"].Remove(badKey);
            }

        }

        #endregion

        #region Properties
        /// <summary>
        /// Name of the file
        /// </summary>
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; LoadFile(); }
        }

        public Dictionary<string, Dictionary<string, string>> FileContents
        {
            get { return _FileContents; }
            set { _FileContents = value; }
        }
        private string _FileName = string.Empty;
        private Dictionary<string, Dictionary<string, string>> _FileContents = null;
        #endregion
    }

    public class RtsXml
    {
           
        private string _xmlfile = "C:\\Users\\58035\\Documents\\Visual Studio 2012\\Projects\\ReportNS\\S520_4G_Antenna.xml";
        private XDocument xmlDoc;
        String txt = null;
        private string _compensationBand = null;
        private string _valuedb = null;
        private double _compensationdB;
        
        private bool Save()
        {
            bool i = false;

            try
            {
                xmlDoc.Save(_xmlfile);
                txt = File.ReadAllText(_xmlfile).Replace("version=\"1.0\"", "version=\"1.1\"");
                txt = txt.Replace("encoding=\"utf-8\"", "encoding=\"UTF16\"");
                FileStream fStr = new FileStream(_xmlfile, FileMode.Create, FileAccess.Write);
                using (var sr = new StreamWriter(fStr))
                {
                    sr.Write(txt.ToString());
                    sr.Flush();
                    sr.Close();
                }
                fStr.Close();
                i = true;
            }
            catch
            {
                i = false;
            }
            
            return i;
        }

        private bool Load()
        {
            bool i = false;

            try
            {
                txt = File.ReadAllText(_xmlfile).Replace("version=\"1.1\"", "version=\"1.0\"");
                xmlDoc = XDocument.Parse(txt, LoadOptions.PreserveWhitespace);
                i = true;
            }
            catch
            {
                i = false;
            }

            return i;
        }



        public RtsXml(string XmlFile, string CompensationBand, double CompensationdB)
        {

            _xmlfile = XmlFile;
            _compensationBand = CompensationBand;
            _compensationdB = CompensationdB;

            if (this.Load())
            {
                try
                {
                    var items1 = from item in xmlDoc.Descendants("Edit")
                                 where item.Attribute("name").Value == _compensationBand
                                 select item.Attribute("value").Value;

                    string temp = null;
                    foreach (var itemElement in items1)
                    {
                        temp = itemElement.ToString();
                    }

                    string[] temp2 = temp.Split(',');
                    temp2[1] = temp2[1].Replace(".", ",");
                    double temp3 = Convert.ToDouble(temp2[1]);

                    CompensationdB += temp3;
                    _valuedb = CompensationdB.ToString().Replace(",", ".");

                    var items = from item in xmlDoc.Descendants("Edit")
                                where item.Attribute("name").Value == _compensationBand
                                select item;

                    foreach (XElement itemElement in items)
                    {
                        itemElement.SetAttributeValue("value", temp2[0] + "," + _valuedb + "," + temp2[2]);
                    }
                    this.Save();
                }
                catch { }
            }
        }
    }
}
