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
    public class FilmeDAO : IDAL<Filme>, IDisposable
    {
        private IConnection _connection;

        public FilmeDAO(IConnection Connection)
        {
            this._connection = Connection;
        }

        public Filme insert(Filme model)
        {
            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO filme (nome, preco, ano) VALUES (@nome, @preco, @ano); SELECT @Identity";

                comando.Parameters.Add("@nome", SqlDbType.Text).Value = model.nome;
                comando.Parameters.Add("@preco", SqlDbType.Decimal).Value = model.preco;
                comando.Parameters.Add("@nome", SqlDbType.Text).Value = model.ano;
                model.id = int.Parse(comando.ExecuteScalar().ToString());
            }

            return model;
        }

        public void update(Filme model)
        {
            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "UPDATE filme SET nome=@nome, preco=@preco, ano=@ano WHERE id.@id";

                comando.Parameters.Add("@nome", SqlDbType.Text).Value = model.nome;
                comando.Parameters.Add("@preco", SqlDbType.Decimal).Value = model.preco;
                comando.Parameters.Add("@nome", SqlDbType.Text).Value = model.ano;
                comando.Parameters.Add("@id", SqlDbType.Int).Value = model.id;
                comando.ExecuteNonQuery();
            }
        }

        public bool remove(Filme model)
        {
            bool retornar = false;

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "DELETE FROM filme WHERE id.@id";

                comando.Parameters.Add("@id", SqlDbType.Int).Value = model.id;
                if (comando.ExecuteNonQuery() > 0)
                {
                    retornar = true;
                }
            }

            return retornar;
        }

        public Filme findPerCode(params object[] keys)
        {
            Filme filme = null;

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT id, nome, preco, ano FROM filme WHERE id=@id";
                comando.Parameters.Add("@id", SqlDbType.Int).Value = keys[0];

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    filme = new Filme();
                    reader.Read();
                    filme.id = reader.GetInt32(0);
                    filme.nome = reader.GetString(1);
                    filme.preco = reader.GetDouble(2);
                    filme.ano = reader.GetString(3);
                }
            }
            return filme;
        }

        public System.Collections.ObjectModel.Collection<Filme> ListAll()
        {
            Collection<Filme> colecao = new Collection<Filme>();

            using (SqlCommand comando = _connection.Find().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT id, nome, preco, ano FROM filme ORDER BY nome";
                            
                using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                {
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);

                    foreach (DataRow row in tabela.Rows)
                    {
                        Filme filme = new Filme {
                            id=int.Parse(row["id"].ToString()),
                            nome=row["nome"].ToString(),
                            preco=Convert.ToDouble(row["preco"].ToString()),
                            ano=row["ano"].ToString()
                        };
                        colecao.Add(filme);
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
