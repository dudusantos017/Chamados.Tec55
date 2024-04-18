using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // ADD.net
using System.Data.SqlClient; // ADD para SQL SERVER


namespace Data
{
    public class ClienteDao
    {
        private string _conexao;


        // Metodo Construtor => Inicializa Objeto buscando Conexao


        public ClienteDao(string conexao)
        {
            // RECEBA Conexão
            _conexao = conexao;
        }


        // Inserir Clinte Vulgo XUXAR
        public void IncluiCliente(Cliente cliente)
        {
            using (SqlConnection conexaoBd = new SqlConnection(_conexao))
            {
                string sql = "insert into Clientes (nome,profissao,setor,obs) values (@nome,@profissao,@setor,@obs)";


                using (SqlCommand comando = new SqlCommand(sql, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@profissao", cliente.Profissao);
                    comando.Parameters.AddWithValue("@setor", cliente.Setor);
                    comando.Parameters.AddWithValue("@obs", cliente.Obs);


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

        public DataSet BuscaCliente(String pesquisa = "")
        {
            const string query = "Select * From Clientes Where Nome like @pesquisa";

            try
            {
                using (var conexao = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexao))
                using (var adaptador = new SqlDataAdapter(comando))
                {
                    string parametroPesquisa = $"%{pesquisa}%";
                    comando.Parameters.AddWithValue("@pesquisa", parametroPesquisa);
                    conexao.Open();
                    var dsClientes = new DataSet();
                    adaptador.Fill(dsClientes, "Clientes");
                    return dsClientes;
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar Clientes:{ex.Message}");

            }
        }

        //xuxar aquió
        public Cliente ObtemCliente(int codigoCliente)
        {
            // Definir o sql para obter o cliente
            const string query = @"select * from Clientes where
                                   CodigoCliente = @CodigoCliente";
            Cliente cliente = null;

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@CodigoCliente", codigoCliente);
                    conexaoBd.Open();
                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                CodigoCliente = Convert.ToInt32(reader["CodigoCliente"]),
                                Nome = reader["Nome"].ToString(),
                                Profissao = reader["Profissao"].ToString(),
                                Setor = reader["Setor"].ToString(),
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
            return cliente;
        }
    }
}





