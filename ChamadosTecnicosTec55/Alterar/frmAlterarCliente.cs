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

namespace ChamadosTecnicosTec55.Alterar
{
    public partial class frmAlterarCliente : Form
    {
        string _conexao = ChamadosTecnicosTec55.Properties.Settings.Default.Conexao;

        public frmAlterarCliente(int codigo)
        {
            InitializeComponent();
            // Verificar se o código maior que zero
            if (codigo > 0)
            {
                // Criar uma instância do objeto Cliente
                Cliente cliente = new Cliente();
                ClienteDao clientedao = new ClienteDao(_conexao);

                cliente = clientedao.ObtemCliente(codigo);

                //Se o cliente não foi encontrado
                if (cliente == null)
                {
                    MessageBox.Show("Cliente não encontrado");
                    this.Close();
                }

                txbCodigo.Text = cliente.CodigoCliente.ToString();
                txbNome.Text = cliente.Nome.ToString();
                txbProfissao.Text = cliente.Profissao.ToString();
                txbSetor.Text = cliente.Setor.ToString();
                txbObs.Text = cliente.Obs.ToString();
            }
        }
    }
}
