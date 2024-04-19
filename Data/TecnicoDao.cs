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
                string sql = "insert into Tecnicos (nome,especialidade,email,senha,obs) values (@nome,@especialidade,@email,@senha,@obs)";

                using (SqlCommand comando = new SqlCommand(sql, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@nome", tecnico.Nome);
                    comando.Parameters.AddWithValue("@especialidade", tecnico.Especialidade);
                    comando.Parameters.AddWithValue("@email", tecnico.Email);
                    comando.Parameters.AddWithValue("@Senha", tecnico.Senha);
                    comando.Parameters.AddWithValue("@Obs", tecnico.Obs);

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
        public DataSet BuscaTecnico(String pesquisa = "")
        {
            const string query = "Select * From Tecnicos Where Nome like @pesquisa";

            try
            {
                using (var conexao = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexao))
                using (var adaptador = new SqlDataAdapter(comando))
                {
                    string parametroPesquisa = $"%{pesquisa}%";
                    comando.Parameters.AddWithValue("@pesquisa", parametroPesquisa);
                    conexao.Open();
                    var dsTecnicos = new DataSet();
                    adaptador.Fill(dsTecnicos, "Tecnicos");
                    return dsTecnicos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar Tecnico:{ex.Message}");
            }
        }
        public Tecnico ObtemTecnico(int codigoTecnico)
        {
            // Definir o sql para obter o cliente
            const string query = @"select * from Tecnicos where
                                   CodigoTecnico = @CodigoTecnico";
            Tecnico tecnico = null;

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@CodigoTecnico", codigoTecnico);
                    conexaoBd.Open();
                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tecnico = new Tecnico
                            {
                                CodigoTecnico = Convert.ToInt32(reader["CodigoTecnico"]),
                                Nome = reader["Nome"].ToString(),
                                Especialidade = reader["Especialidade"].ToString(),
                                Email = reader["Email"].ToString(),
                                Senha = reader["Senha"].ToString(),
                                Obs = reader["Obs"].ToString(),
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter o cliente {ex.Message}", ex);
            }
            return tecnico;
        }
        public void AlterarTecnico(Tecnico tecnico)
        {
            const string query = @"update Tecnicos set nome=@Nome, 
                                    Especialidade = @Especialidade, Email = @Email, 
                                    Senha = @Senha, Obs = @Obs 
                                    where CodigoTecnico = @CodTecnico";
            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@CodTecnico",tecnico.CodigoTecnico);
                    comando.Parameters.AddWithValue("@Nome", tecnico.Nome);
                    comando.Parameters.AddWithValue("@Especialidade", tecnico.Especialidade);
                    comando.Parameters.AddWithValue("@Email", tecnico.Email);
                    comando.Parameters.AddWithValue("@Senha", tecnico.Senha);
                    comando.Parameters.AddWithValue("@Obs", tecnico.Obs);
                    conexaoBd.Open();
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro{ex}");
            }
        }
        public void ExcluirTecnico(int codigoTecnico)
        {
            const string query = @"delete from tecnicos where codigotecnico = @codigoTecnico";
            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@codigoTecnico", codigoTecnico);
                    conexaoBd.Open();
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir{ex.Message}", ex);
            }
        }
    }
}
