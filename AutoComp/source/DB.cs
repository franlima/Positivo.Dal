#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using Positivo.AutoComp;

#endregion

namespace Positivo.AutoComp
{
    public class DB
    {
        #region
        //private SQLiteConnection sql_con;
        //private SQLiteDataAdapter sql_da;
        //private string strconn = @"Data Source=C:C:\Users\58035\Downloads\SQLite\dbLocal.db";
        //private string strconn = @"C:\Users\58035\Downloads\SQLite\dbLocal.xml";
     
        #endregion


        /// <summary>
        /// Open connection with DB
        /// </summary>
        private bool openConn()
        {
            //setDbConnection();
            //sql_con.Open();
            return true;
        }

        /// <summary>
        /// Close connection with DB
        /// </summary>
        private void closeConn()
        {
            //sql_con.Close();
        }

        /// <summary>
        /// Load data from local
        /// </summary>
        public DataTable loadDataFromLocal()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Write data to local
        /// </summary>
        public DataTable saveDataToLocal()
        {
            throw new System.NotImplementedException();
        }
    }

    public class TB
    {
        private DataTable dtTestData;
        private string _defaultlog;

        public TB()
        {
            dtTestData = new DataTable();
            dtTestData.TableName = "TestData";
            loadDataFromLocal();
        }
        public void GetData(string logFile)
        {
            _defaultLog = logFile;
            createDataColumns();
        }
        public DataTable dtData
        {
            get
            {
                return dtTestData;
            }
        }

        /// <summary>
        /// Load data from local
        /// </summary>
        private void loadDataFromLocal()
        {
            try
            {
                dtTestData.ReadXmlSchema(@"C:\Users\58035\Documents\Visual Studio 2012\Projects\ReportNS\ReportData\bin\Debug\db.xsd");
                dtTestData.ReadXml(@"C:\Users\58035\Documents\Visual Studio 2012\Projects\ReportNS\ReportData\bin\Debug\db.xml");
            }
            catch { }
            finally
            {
                File.Delete(@"C:\Users\58035\Documents\Visual Studio 2012\Projects\ReportNS\ReportData\bin\Debug\db.xml");
            }
        }

        /// <summary>
        /// Save data to local
        /// </summary>
        public void saveDataToLocal()
        {
            try
            {
                dtTestData.WriteXml(@"C:\Users\58035\Documents\Visual Studio 2012\Projects\ReportNS\ReportData\bin\Debug\db.xml");
                dtTestData.WriteXmlSchema(@"C:\Users\58035\Documents\Visual Studio 2012\Projects\ReportNS\ReportData\bin\Debug\db.xsd");
            }
            catch { }
        }
        
        /// <summary>
        /// To retain log file path
        /// </summary>
        private string _defaultLog
        {
            get
            {
                return _defaultlog;
            }
            set
            {
                if (File.Exists(value))
                {
                    _defaultlog = value;
                }
                else
                {
                    throw new ArgumentException(value + " not exists!!!");
                }
                    
            }
        }

        /// <summary>
        /// Creates columns to DataTable
        /// </summary>
        private void createDataColumns()
        {
            RtsLog filelog = new RtsLog(_defaultlog);

            try
            {
                if (!dtTestData.Columns.Contains("ID"))
                {
                    dtTestData.Columns.Add("ID", typeof(int));
                    dtTestData.Columns["ID"].AllowDBNull = false;
                    dtTestData.Columns["ID"].AutoIncrement = true;
                }
            }
            finally
            { }

            DataRow dr = dtTestData.NewRow();

            foreach (KeyValuePair<string, string> kvp in filelog.FileContents["HEAD"])
            {
                if (!dtTestData.Columns.Contains(kvp.Key))
                {
                    dtTestData.Columns.Add(kvp.Key, typeof(String));
                }
                dr[kvp.Key] = kvp.Value;
            }
            foreach (KeyValuePair<string, string> kvp in filelog.FileContents["TEST_RESULT"])
            {
                if (!dtTestData.Columns.Contains(kvp.Key))
                {
                    dtTestData.Columns.Add(kvp.Key, typeof(String));
                }
                dr[kvp.Key] = kvp.Value;
            }
            foreach (KeyValuePair<string, string> kvp in filelog.FileContents["TEST_DATA"])
            {
                String tempcolname = "DC" + kvp.Key;
                object variavel = "";

                if (!dtTestData.Columns.Contains(tempcolname))
                {

                    try
                    {
                        string temp = kvp.Value.Replace(".", ",");
                        variavel = Convert.ChangeType(temp, typeof(Double));
                        dtTestData.Columns.Add(tempcolname, typeof(Double));
                    }
                    catch
                    {
                        variavel = Convert.ChangeType(kvp.Value, typeof(String));
                        dtTestData.Columns.Add(tempcolname, typeof(String));
                    }
                }
                else
                {
                    try
                    {
                        string temp = kvp.Value.Replace(".", ",");
                        var tipo = dtTestData.Columns[tempcolname].DataType;
                        variavel = Convert.ChangeType(temp, tipo);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine(e);
                        var tipo = dtTestData.Columns[tempcolname].DataType;
                        variavel = Convert.ChangeType("0", tipo);
                    }
                }

                dr[tempcolname] = variavel;
            }

            dtTestData.Rows.Add(dr);
        }
    }

}
