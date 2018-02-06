using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportNsClass
{
    public class ReportNS
    {
    }
    public class FileNS
    {
        private int isloaded;
        private string ns;
        private string filelogpath;
        private string filename;

        /// <summary>
        /// Expose file is loaded correctly
        /// </summary>
        public int isLoaded
        {
            get
            {
                return isloaded;
            }
            set
            {
                isloaded = value;
            }
        }

        /// <summary>
        /// Serial Number loaded
        /// </summary>
        public string NS
        {
            get
            {
                return ns;
            }
            set
            {
                ns = value;
            }
        }

        public string fileLogPath
        {
            get
            {
                return fileLogPath;
            }
            set
            {
                filelogpath = value;
            }
        }

        public string fileName
        {
            get
            {
                return filename;
            }
            set
            {
                filename = value;
            }
        }
        /// <summary>
        /// Load log file from executable
        /// </summary>
        public int loadLogFile()
        {
            INI inilog = new INI();

            inilog.FileName = "@C:\\_Celular_Tablets\\App_ODM\\RTS_V6.0.20160630\\TestResult\\20161226\\4S1102T0N-20160815070953.txt";

            return 0;
        }
    }

}
