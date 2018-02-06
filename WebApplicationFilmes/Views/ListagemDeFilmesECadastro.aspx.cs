using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Positivo.Dal.Interfaces;
using Positivo.Dal.Classes;

namespace WebApplicationFilmes.Views
{
    public partial class ListagemDeFilmesECadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (IConnection conexao = new Connection())
            {
                conexao.Open();

                IDAL<Filme> FilmeDAO = new FilmeDAO(conexao);

                Filme filme = new Filme();

                filme.nome = TextBox1.Text;
                filme.preco = Convert.ToDouble(TextBox2.Text);
                filme.ano = TextBox3.Text;

                FilmeDAO.insert(filme);

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            using (IConnection conexao = new Connection())
            {
                conexao.Open();

                IDAL<Filme> FilmeDAO = new FilmeDAO(conexao);

                Filme filme = new Filme();

                GridView1.DataSource = FilmeDAO.ListAll();
                GridView1.DataBind();

            }
        }

        protected void TextBox1_TextChanged1(object sender, EventArgs e)
        {

        }

    }
}