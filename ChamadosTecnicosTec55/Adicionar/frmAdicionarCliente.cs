using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChamadosTecnicosTec55.Adicionar
{
    public partial class frmAdicionarCliente : Form
    {
        // Chamaaa a conexão 
        string _conexao = ChamadosTecnicosTec55.Properties.Settings.Default.Conexao;

        public frmAdicionarCliente()
        {
            InitializeComponent();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txbNome.Clear();
            txbObs.Clear();
            txbProfissao.Clear();
            txbSetor.Clear();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Chama o objeto Cliente
            Cliente cliente = new Cliente();
            ClienteDao clienteDao = new ClienteDao(_conexao);

            if (string.IsNullOrEmpty(txbNome.Text) || string.IsNullOrEmpty(txbObs.Text) || string.IsNullOrEmpty(txbProfissao.Text) || string.IsNullOrEmpty(txbSetor.Text))
            {
                MessageBox.Show("CADE OS DADOSSS ??");
            }
            else 
            {
                // TODA QUE MEXER COM BD USAR TRY
                try
                {
                    //Preenche o Objeto Cliente
                    cliente.Nome = txbNome.Text;
                    cliente.Profissao = txbProfissao.Text;
                    cliente.Obs = txbObs.Text;
                    cliente.Setor = txbSetor.Text;

                    //CHAMA O DAO PARA INCLUIR O CLIENTE
                    clienteDao.IncluiCliente(cliente);
                    MessageBox.Show("Cadastrado com sucesso!");
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao Cadastrar","Atenção",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void txbNome_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
