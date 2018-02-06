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
    public class TestSeq
    {
        public int ID { get; set; }
        public string SeqName { get; set; }
    }

    public class TestSeqDAL : IDAL<TestSeq>, IDisposable
    {
        private IConnection _connection;

        public TestSeqDAL(IConnection Connection)
        {
            this._connection = Connection;
        }

        public TestSeq insert(TestSeq model)
        {
            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO TestSeq (SeqName) VALUES (@seqname); SELECT @@IDENTITY";

                comando.Parameters.Add("@seqname", SqlDbType.Text).Value = model.SeqName;
                model.ID = int.Parse(comando.ExecuteScalar().ToString());
                //comando.ExecuteNonQuery();
            }

            return model;
        }

        public void update(TestSeq model)
        {
            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "UPDATE TestSeq SET SeqName=@seqname WHERE id=@id";

                comando.Parameters.Add("@seqname", SqlDbType.Text).Value = model.SeqName;
                comando.Parameters.Add("@id", SqlDbType.Int).Value = model.ID;
                comando.ExecuteNonQuery();
            }
        }

        public bool remove(TestSeq model)
        {
            bool retornar = false;

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "DELETE FROM SeqName WHERE id.@id";

                comando.Parameters.Add("@id", SqlDbType.Int).Value = model.ID;
                if (comando.ExecuteNonQuery() > 0)
                {
                    retornar = true;
                }
            }

            return retornar;
        }

        public TestSeq findPerCode(params object[] keys)
        {
            TestSeq _projectseq = null;

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT id, SeqName FROM TestSeq WHERE id=@id";
                comando.Parameters.Add("@id", SqlDbType.Int).Value = keys[0];

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    _projectseq = new TestSeq();
                    reader.Read();
                    _projectseq.ID = reader.GetInt32(0);
                    _projectseq.SeqName = reader.GetString(1);
                }
            }
            return _projectseq;
        }

        public System.Collections.ObjectModel.Collection<TestSeq> ListAll()
        {
            Collection<TestSeq> colecao = new Collection<TestSeq>();

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT id, SeqName FROM TestSeq ORDER BY SeqName";

                using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                {
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);

                    foreach (DataRow row in tabela.Rows)
                    {
                        TestSeq _projectseq = new TestSeq
                        {
                            ID = int.Parse(row["ID"].ToString()),
                            SeqName = row["Project"].ToString(),
                        };
                        colecao.Add(_projectseq);
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
