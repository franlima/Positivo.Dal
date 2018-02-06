#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace Positivo.AutoComp
{
     public class LOG
     {
         #region Constructor

         /// <summary>
         /// Constructor
         /// </summary>
         /// <remarks>FileName string</remarks>
         /// <returns>NULL</returns>
         public LOG(string FileName)
         {
         }
         
         #endregion

         #region Properties
         /// <summary>
         /// String with file name and path
         /// </summary>
         public string FileName
         {
             get { return _FileName; }
             set { _FileName = value; }
         }

         /// <summary>
         /// Handler for log contents
         /// </summary>
         public Dictionary<string, Dictionary<string, string>> FileContents
         {
             get { return _FileContents; }
             set { _FileContents = value; }
         }
         private string _FileName = string.Empty;
         private Dictionary<string, Dictionary<string, string>> _FileContents = null;
         #endregion
     }

}