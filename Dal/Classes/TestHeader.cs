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
    public class TestHeader
    {
        public int ID { get; set; }
        public int ID_ProjectSeq { get; set; }
        public int ID_Line { get; set; }
        public int ID_Station { get; set; }
        public int ID_Phase { get; set; }
        public string Version { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SN { get; set; }
        public string Test_Result { get; set; }
        public float Elapse_Time { get; set; }
    }

    public class TestHeaderDAL : IDAL<TestHeader>, IDisposable
    {
        private IConnection _connection;

        public TestHeaderDAL(IConnection Connection)
        {
            this._connection = Connection;
        }

        public TestHeader insert(TestHeader model)
        {
            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = @"INSERT INTO TestHeader
                                        (ID_ProjectSeq, ID_Line,
                                        ID_Station, ID_Phase, Version,
                                        StartTime, EndTime, SN,
                                        Test_Result, Elapse_Time)
                                        VALUES (@ID_ProjectSeq, @ID_Line,
                                        @ID_Station,@ID_Phase, @Version,
                                        @StartTime, @EndTime, @SN,
                                        @Test_Result, @Elapse_Time);
                                        SELECT @@IDENTITY";

                comando.Parameters.Add("@ID_ProjectSeq", SqlDbType.Int).Value = model.ID_ProjectSeq;
                comando.Parameters.Add("@ID_Line", SqlDbType.Int).Value = model.ID_Line;
                comando.Parameters.Add("@ID_Station", SqlDbType.Int).Value = model.ID_Station;
                comando.Parameters.Add("@ID_Phase", SqlDbType.Int).Value = model.ID_Phase;
                comando.Parameters.Add("@Version", SqlDbType.Text).Value = model.Version;
                comando.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = model.StartTime;
                comando.Parameters.Add("@EndTime", SqlDbType.DateTime).Value = model.EndTime;
                comando.Parameters.Add("@SN", SqlDbType.Text).Value = model.SN;
                comando.Parameters.Add("@Test_Result", SqlDbType.Text).Value = model.Test_Result;
                comando.Parameters.Add("@Elapse_Time", SqlDbType.Float).Value = model.Elapse_Time;
                model.ID = int.Parse(comando.ExecuteScalar().ToString());
                //comando.ExecuteNonQuery();
            }

            return model;
        }

        public void update(TestHeader model)
        {
                using (SqlCommand comando = _connection.Find().CreateCommand())
                {
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = @"UPDATE TestHeader SET ID_ProjectSeq=@ID_ProjectSeq, ID_Line=@ID_Line,
                                            ID_Station=@ID_Station, ID_Phase=@ID_phase, Version=@Version,
                                            StartTime=@StartTime, EndTime=@EndTime, SN=@SN,
                                            Test_Result=@Test_Result, Elapse_Time@Elapse_time
                                            WHERE ID=@id";

                    comando.Parameters.Add("@ID_ProjectSeq", SqlDbType.Int).Value = model.ID_ProjectSeq;
                    comando.Parameters.Add("@ID_Line", SqlDbType.Int).Value = model.ID_Line;
                    comando.Parameters.Add("@ID_Station", SqlDbType.Int).Value = model.ID_Station;
                    comando.Parameters.Add("@ID_Phase", SqlDbType.Int).Value = model.ID_Phase;
                    comando.Parameters.Add("@Version", SqlDbType.Text).Value = model.Version;
                    comando.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = model.StartTime;
                    comando.Parameters.Add("@EndTime", SqlDbType.DateTime).Value = model.EndTime;
                    comando.Parameters.Add("@SN", SqlDbType.Text).Value = model.SN;
                    comando.Parameters.Add("@Test_Result", SqlDbType.Text).Value = model.Test_Result;
                    comando.Parameters.Add("@Elapse_Time", SqlDbType.Text).Value = model.Elapse_Time;
                    comando.ExecuteNonQuery();
                }
        }

        public bool remove(TestHeader model)
        {
            bool retornar = false;

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = @"DELETE FROM TestHeader WHERE ID=@id";

                comando.Parameters.Add("@id", SqlDbType.Int).Value = model.ID;
                if (comando.ExecuteNonQuery() > 0)
                {
                    retornar = true;
                }
            }

            return retornar;
        }

        public TestHeader findPerCode(params object[] keys)
        {
            TestHeader _model = null;

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = @"SELECT * FROM TestHeader WHERE ID=@id";
                comando.Parameters.Add("@id", SqlDbType.Int).Value = keys[0];

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    _model = new TestHeader();
                    reader.Read();
                    _model.ID = reader.GetInt32(0);
                    _model.ID_ProjectSeq = reader.GetInt32(1);
                    _model.ID_Line = reader.GetInt32(2);
                    _model.ID_Station = reader.GetInt32(3);
                    _model.ID_Phase = reader.GetInt32(4);
                    _model.Version = reader.GetString(5);
                    _model.StartTime = reader.GetDateTime(6);
                    _model.EndTime = reader.GetDateTime(7);
                    _model.SN = reader.GetString(8);
                    _model.Test_Result = reader.GetString(9);
                    _model.Elapse_Time = reader.GetFloat(10);
                }
            }
            return _model;
        }

        public System.Collections.ObjectModel.Collection<TestHeader> ListAll()
        {
            Collection<TestHeader> colecao = new Collection<TestHeader>();

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = @"SELECT * FROM TestHeader ORDER BY ID";

                using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                {
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);

                    foreach (DataRow row in tabela.Rows)
                    {
                        TestHeader _model = new TestHeader
                        {
                            ID = int.Parse(row["ID"].ToString()),
                            ID_ProjectSeq = int.Parse(row["ID"].ToString()),
                            ID_Line = int.Parse(row["ID"].ToString()),
                            ID_Station = int.Parse(row["ID"].ToString()),
                            ID_Phase = int.Parse(row["ID"].ToString()),
                            Version = row["Project"].ToString(),
                            StartTime = DateTime.Parse(row["Project"].ToString()),
                            EndTime = DateTime.Parse(row["Project"].ToString()),
                            SN = row["Project"].ToString(),
                            Test_Result = row["Project"].ToString(),
                            Elapse_Time = float.Parse( row["Project"].ToString())
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
