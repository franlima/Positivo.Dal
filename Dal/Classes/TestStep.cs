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
    public class TestStep
    {
        public int ID { get; set; }
        public int ID_Test_Seq { get; set; }
        public string IdTp { get; set; }
        public string Description { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }
    }

    public class TestStepDAL : IDAL<TestStep>, IDisposable
    {
        private IConnection _connection;

        public TestStepDAL(IConnection Connection)
        {
            this._connection = Connection;
        }

        public TestStep insert(TestStep model)
        {
            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO TestStep (ID_Test_Seq, IdTp, Description," +
                                      "LowLimit, HighLimit, Unit) VALUES (@idtestseq, @idtp, @description" +
                                      "@lowlimit, @highlimit, @unit); SELECT @@IDENTITY";

                comando.Parameters.Add("@idtestseq", SqlDbType.Text).Value = model.ID_Test_Seq;
                comando.Parameters.Add("@idtp", SqlDbType.Text).Value = model.IdTp;
                comando.Parameters.Add("@description", SqlDbType.Text).Value = model.Description;
                comando.Parameters.Add("@lowlimit", SqlDbType.Text).Value = model.LowLimit;
                comando.Parameters.Add("@highlimit", SqlDbType.Text).Value = model.HighLimit;
                comando.Parameters.Add("@unit", SqlDbType.Text).Value = model.Unit;
                model.ID = int.Parse(comando.ExecuteScalar().ToString());
                //comando.ExecuteNonQuery();
            }

            return model;
        }

        public void update(TestStep model)
        {
            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "UPDATE TestStep SET Description=@description WHERE ID=@id";

                comando.Parameters.Add("@description", SqlDbType.Text).Value = model.Description;
                comando.Parameters.Add("@id", SqlDbType.Int).Value = model.ID;
                comando.ExecuteNonQuery();
            }
        }

        public bool remove(TestStep model)
        {
            bool retornar = false;

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "DELETE FROM TestStep WHERE ID=@id";

                comando.Parameters.Add("@id", SqlDbType.Int).Value = model.ID;
                if (comando.ExecuteNonQuery() > 0)
                {
                    retornar = true;
                }
            }

            return retornar;
        }

        public TestStep findPerCode(params object[] keys)
        {
            TestStep _model = new TestStep();

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT * FROM TestStep WHERE IdTp LIKE '%" + keys[0] + "%'";
                //comando.Parameters.Add("@idtp", SqlDbType.Text).Value = keys[0];

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        _model.ID = reader.GetInt32(0);
                        _model.ID_Test_Seq = reader.GetInt32(1);
                        _model.IdTp = reader.GetString(2);
                        _model.Description = reader.GetString(3);
                        _model.LowLimit = reader.GetDouble(4);
                        _model.HighLimit = reader.GetDouble(5);
                        _model.Unit = reader.GetString(6);
                    }
                }
            }
            return _model;
        }

        public Collection<TestStep> findPerTestSeqId(params object[] keys)
        {
            Collection<TestStep> colecao = new Collection<TestStep>();

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT * FROM TestSeq WHERE ID_Test_Seq LIKE '%" + keys[0] + "%' ORDER BY ID";

                using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                {
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);

                    foreach (DataRow row in tabela.Rows)
                    {
                        TestStep _model = new TestStep
                        {
                            ID = int.Parse(row["ID"].ToString()),
                            ID_Test_Seq = int.Parse(row["ID"].ToString()),
                            IdTp = row["Project"].ToString(),
                            Description = row["Project"].ToString(),
                            LowLimit = double.Parse(row["ID"].ToString()),
                            HighLimit = double.Parse(row["ID"].ToString()),
                            Unit = row["Project"].ToString()
                        };
                        colecao.Add(_model);
                    }
                }
            }
            return colecao;
        }

        public Collection<TestStep> ListAll()
        {
            Collection<TestStep> colecao = new Collection<TestStep>();

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT * FROM TestSeq ORDER BY ID";

                using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                {
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);

                    foreach (DataRow row in tabela.Rows)
                    {
                        TestStep _model = new TestStep
                        {
                            ID = int.Parse(row["ID"].ToString()),
                            ID_Test_Seq = int.Parse(row["ID"].ToString()),
                            IdTp = row["Project"].ToString(),
                            Description = row["Project"].ToString(),
                            LowLimit = double.Parse(row["ID"].ToString()),
                            HighLimit = double.Parse(row["ID"].ToString()),
                            Unit = row["Project"].ToString()
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
