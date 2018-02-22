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
    public class ProjectSeq
    {
        public int ID { get; set; }
        public int ID_TestSeq { get; set; }
        public string Name { get; set; }
    }

    public class ProjectSeqDAL : IDAL<ProjectSeq>, IDisposable
    {
        private IConnection _connection;

        public ProjectSeqDAL(IConnection Connection)
        {
            this._connection = Connection;
        }

        public ProjectSeq insert(ProjectSeq model)
        {
            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO ProjectSeq (ID_TestSeq, Name) VALUES (@id,@project); SELECT @@IDENTITY";

                comando.Parameters.Add("@id", SqlDbType.Int).Value = model.ID_TestSeq;
                comando.Parameters.Add("@project", SqlDbType.Text).Value = model.Name;
                //comando.ExecuteNonQuery();
                model.ID = int.Parse(comando.ExecuteScalar().ToString());
            }

            return model;
        }

        public void update(ProjectSeq model)
        {
            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "UPDATE ProjectSeq SET project=@project WHERE id=@id";

                comando.Parameters.Add("@project", SqlDbType.Text).Value = model.Name;
                comando.Parameters.Add("@id", SqlDbType.Int).Value = model.ID_TestSeq;
                comando.ExecuteNonQuery();
            }
        }

        public bool remove(ProjectSeq model)
        {
            bool retornar = false;

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "DELETE FROM ProjectSeq WHERE id=@id";

                comando.Parameters.Add("@id", SqlDbType.Int).Value = model.ID_TestSeq;
                if (comando.ExecuteNonQuery() > 0)
                {
                    retornar = true;
                }
            }

            return retornar;
        }

        public ProjectSeq findPerCode(params object[] keys)
        {
            ProjectSeq _projectseq = new ProjectSeq(); ;

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT * FROM ProjectSeq WHERE Name LIKE '%" + keys[0] + "%'";
                //comando.Parameters.Add("@name", SqlDbType.Text).Value = keys[0];

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.HasRows)
                    {                       
                        reader.Read();
                        _projectseq.ID = reader.GetInt32(0);
                        _projectseq.ID_TestSeq = reader.GetInt32(1);
                        _projectseq.Name = reader.GetString(2);
                    }
                }
            }
            return _projectseq;
        }

        public System.Collections.ObjectModel.Collection<ProjectSeq> ListAll()
        {
            Collection<ProjectSeq> colecao = new Collection<ProjectSeq>();

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT * FROM ProjectSeq ORDER BY Name";
                            
                using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                {
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);

                    foreach (DataRow row in tabela.Rows)
                    {
                        ProjectSeq _projectseq = new ProjectSeq {
                            ID = int.Parse(row["ID"].ToString()),
                            ID_TestSeq = int.Parse(row["ID_TestSeq"].ToString()),
                            Name = row["Name"].ToString()
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
