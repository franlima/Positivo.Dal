using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using Positivo.Dal.Interfaces;
using Positivo.Dal.Classes;

namespace Positivo.Dal.Classes
{
    public class GenericTable
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class GenericTableDAL : IDAL<GenericTable>, IDisposable
    {
        private IConnection _connection;

        public GenericTableDAL(IConnection Connection)
        {
            this._connection = Connection;
        }

        public GenericTable insert(GenericTable model)
        {
            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = @"INSERT INTO " + model.GetType().Name + "(Name, Description)" +
                                       "VALUES (@name, @description); SELECT @@IDENTITY";

                comando.Parameters.Add("@name", SqlDbType.Text).Value = model.Name;
                comando.Parameters.Add("@description", SqlDbType.Text).Value = model.Description;
                model.ID = int.Parse(comando.ExecuteScalar().ToString());
            }

            return model;
        }

        public void update(GenericTable model)
        {
            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = @"UPDATE " + model.GetType().Name + " SET Name=@name" +
                                       "Description=@description WHERE ID=@id";

                comando.Parameters.Add("@name", SqlDbType.Text).Value = model.Name;
                comando.Parameters.Add("@description", SqlDbType.Text).Value = model.Description;
                comando.Parameters.Add("@id", SqlDbType.Int).Value = model.ID;
                comando.ExecuteNonQuery();
            }
        }

        public bool remove(GenericTable model)
        {
            bool retornar = false;

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = @"DELETE FROM " + model.GetType().Name + " WHERE ID=@id";

                comando.Parameters.Add("@id", SqlDbType.Int).Value = model.ID;
                if (comando.ExecuteNonQuery() > 0)
                {
                    retornar = true;
                }
            }

            return retornar;
        }

        public GenericTable findPerCode(params object[] keys)
        {
            GenericTable _model = new GenericTable(); 

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                string value = keys[0].GetType().GetProperty("Name").GetValue(keys[0], null).ToString();

                comando.CommandText = @"SELECT * FROM " + keys[0].GetType().Name + " WHERE Name LIKE '%" + value + "%'";

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        _model.ID = reader.GetInt32(0);
                        _model.Name = reader.GetString(1);
                        _model.Description = reader.GetString(2);
                    }
                }
            }
            return _model;
        }

        public System.Collections.ObjectModel.Collection<GenericTable> ListAll()
        {
            throw new NotImplementedException();
        }

        public System.Collections.ObjectModel.Collection<GenericTable> ListAll(GenericTable model)
        {
            Collection<GenericTable> colecao = new Collection<GenericTable>();

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = @"SELECT * FROM " + model.GetType().Name + " ORDER BY ID";

                //comando.Parameters.Add("@table", SqlDbType.Text).Value = this.GetType().Name;

                using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                {
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);

                    foreach (DataRow row in tabela.Rows)
                    {
                        GenericTable _model = new GenericTable
                        {
                            ID = int.Parse(row["ID"].ToString()),
                            Name = row["Name"].ToString(),
                            Description = row["Description"].ToString()
                        };
                        colecao.Add(_model);
                    }
                }
            }
            return colecao;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

}
