using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Positivo.Dal.Interfaces;
using Positivo.Dal.Classes;

namespace Positivo.Dal.Classes
{
    public class Connection : IConnection, IDisposable
    {
        private SqlConnection _connection;

        /// <summary>
        /// Constructor
        /// </summary>
        public Connection()
        {
            //string str = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();
            //_connection = new SqlConnection("Data Source=(LocalDB)\v11.0;AttachDbFilename='C:\\Users\\58035\\Documents\\Visual Studio 2012\\Projects\\ReportNS\\WebApplicationFilmes\\App_Data\\Filmes.mdf';Integrated Security=True");
            string str = Properties.Settings.Default.ConnectionStringStd;
            _connection = new SqlConnection(str);
            
        }

        /// <summary>
        /// Open DB connection
        /// </summary>
        public SqlConnection Open()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            return _connection;
        }

        /// <summary>
        /// Check pointer to Open connection
        /// </summary>
        public SqlConnection Find()
        {
            return this.Open();
        }

        /// <summary>
        /// Close DB connection
        /// </summary>
        public void Close()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        /// <summary>
        /// Disposible method
        /// </summary>
        public void Dispose()
        {
            this.Close();
            GC.SuppressFinalize(this);
        }
    }
}
