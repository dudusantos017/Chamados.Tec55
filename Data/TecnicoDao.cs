using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // ADO.net
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Security.AccessControl; // ADO para SQL SERVER

namespace Data
{
    public class TecnicoDao
    {
        private string _conexao;

        // Metodo Construtor => Inicializa Objeto buscando Conexao
        public TecnicoDao(string conexao)
        {
            // RECEBA Conexão 
            _conexao = conexao;
        }

        // Inserir Cliente Vulgo XUXAR
        public void IncluiTecnico(Tecnico tecnico)
        {
            using(SqlConnection conexaoBd = new SqlConnection(_conexao))
            {
                string sql = "insert into Tecnicos (nome,especialidade,email,obs,senha) values (@nome,@especialidade,@email,@obs,@senha)";

                using (SqlCommand comando = new SqlCommand(sql, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@nome", tecnico.Nome);
                    comando.Parameters.AddWithValue("@especialidade", tecnico.Especialidade);
                    comando.Parameters.AddWithValue("@email", tecnico.Email);
                    comando.Parameters.AddWithValue("@obs", tecnico.Obs);
                    comando.Parameters.AddWithValue("@senha", tecnico.Senha);

                    try
                    {
                        conexaoBd.Open();
                        comando.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao Incluir Cliente:" + ex.Message);
                    }
                }

            }
        }
    }
}
