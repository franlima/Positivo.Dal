using System;
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
    public class TestResult
    {
        public int ID { get; set; }
        public int ID_Header { get; set; }
        public int ID_TestStep { get; set; }
        public string IdTp { get; set; }
        public double Result { get; set; }
        public double Elapse_Time { get; set; }
    }

    public class TestResultDAL : IDAL<TestResult>, IDisposable
    {
        private IConnection _connection;

        public TestResultDAL(IConnection Connection)
        {
            this._connection = Connection;
        }

        public TestResult insert(TestResult model)
        {
            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO TestResult (ID_Header, ID_TestStep, IdTp," +
                                      "Result, Elapse_Time) VALUES (@id_header, @id_teststep," +
                                      "@idtp, @result, @elapsetime); SELECT @@IDENTITY";

                comando.Parameters.Add("@id_header", SqlDbType.Int).Value = model.ID_Header;
                comando.Parameters.Add("@id_teststep", SqlDbType.Int).Value = model.ID_TestStep;
                comando.Parameters.Add("@idtp", SqlDbType.Text).Value = model.IdTp;
                comando.Parameters.Add("@result", SqlDbType.Real).Value = model.Result;
                comando.Parameters.Add("@elapsetime", SqlDbType.Real).Value = model.Elapse_Time;
                model.ID = int.Parse(comando.ExecuteScalar().ToString());
                //comando.ExecuteNonQuery();
            }

            return model;
        }

        public void update(TestResult model)
        {
            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "UPDATE TestResult SET Result=@result WHERE ID=@id";

                comando.Parameters.Add("@result", SqlDbType.Text).Value = model.Result;
                comando.Parameters.Add("@id", SqlDbType.Int).Value = model.ID;
                comando.ExecuteNonQuery();
            }
        }

        public bool remove(TestResult model)
        {
            bool retornar = false;

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "DELETE FROM TestResult WHERE ID=@id";

                comando.Parameters.Add("@id", SqlDbType.Int).Value = model.ID;
                if (comando.ExecuteNonQuery() > 0)
                {
                    retornar = true;
                }
            }

            return retornar;
        }

        public TestResult findPerCode(params object[] keys)
        {
            TestResult _model = null;

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT * FROM TestResult WHERE IdTp LIKE '%@idtp%'";
                comando.Parameters.Add("@idtp", SqlDbType.Text).Value = keys[0];

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    _model = new TestResult();
                    reader.Read();
                    _model.ID = reader.GetInt32(0);
                    _model.ID_Header = reader.GetInt32(1);
                    _model.ID_TestStep = reader.GetInt32(2);
                    _model.IdTp = reader.GetString(3);
                    _model.Result = reader.GetDouble(4);
                    _model.Elapse_Time = reader.GetDouble(5);
                }
            }
            return _model;
        }

        public System.Collections.ObjectModel.Collection<TestResult> ListAll()
        {
            Collection<TestResult> colecao = new Collection<TestResult>();

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT * FROM TestResult ORDER BY ID";

                using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                {
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);

                    foreach (DataRow row in tabela.Rows)
                    {
                        TestResult _model = new TestResult
                        {
                            ID = int.Parse(row["ID"].ToString()),
                            ID_Header = int.Parse(row["ID"].ToString()),
                            ID_TestStep = int.Parse(row["ID"].ToString()),
                            IdTp = row["Project"].ToString(),
                            Result = double.Parse(row["ID"].ToString()),
                            Elapse_Time = double.Parse(row["ID"].ToString())
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
