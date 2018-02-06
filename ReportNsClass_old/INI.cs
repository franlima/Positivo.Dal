/*
Copyright (c) 2010 <a href="http://www.gutgames.com">James Craig</a>
 
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.
 
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/
  
#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

#endregion

namespace ReportNsClass
{
     /// <summary>
     /// Class for helping with INI files
     /// </summary>
     public class INI
     {
         #region Constructor
         /// <summary>
         /// Constructor
         /// </summary>
         public INI()
         {
             LoadFile();
         }
  
         /// <summary>
         /// Constructor
         /// </summary>
         /// <param name="FileName">Name of the file</param>
         public INI(string FileName)
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
  
             string Contents = File.ReadAllText(FileName);
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
  
         private Dictionary<string, Dictionary<string, string>> FileContents
         {
             get { return _FileContents; }
             set { _FileContents = value; }
         }
         private string _FileName = string.Empty;
         private Dictionary<string, Dictionary<string, string>> _FileContents = null;
         #endregion
     }
 }