using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace ReportNsClass
{
    class DBLocal
    {
        #region
        private SQLiteConnection sql_con;
        private SQLiteDataAdapter sql_da;
        private DataTable data_dt = new DataTable();
        private string strconn = @"Data Source=C:C:\Users\58035\Downloads\SQLite\dbLocal.db";
        #endregion

        private void setDbConnection()
        {           
            sql_con = new SQLiteConnection (strconn);
        }

        private bool openConn()
        {
            setDbConnection();
            sql_con.Open();
            return true;
        }

        private void closeConn()
        {
            sql_con.Close();
        }

        private DataTable loadData(string CommandText)
        {
            openConn();
            sql_da = new SQLiteDataAdapter(CommandText, sql_con);
            sql_da.Fill(data_dt);
            closeConn();

            return data_dt;
        }
 


    }
}
