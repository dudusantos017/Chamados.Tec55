using ChamadosTecnicosTec55.Adicionar;
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
    public partial class frmGerirClientes : Form
    {
        string _conexao = ChamadosTecnicosTec55.Properties.Settings.Default.Conexao;

        public frmGerirClientes()
        {
            InitializeComponent();
        }

        private void frmGerirClientes_Load(object sender, EventArgs e)
        {
            ListarCliente();
        }
        public void ListarCliente()
        {
            //  Chama o Cliente DAO
            ClienteDao clienteDao = new ClienteDao(_conexao);

            // Captura o valor digitado na barra de texto TXB
            string busca = txbBuscar.Text.ToString();

            // Chama o Metodo BuscarCliente do objeto
            DataSet ds = new DataSet();
            ds = clienteDao.BuscarCliente(busca);

            // Defini o DataSource do DataGridView
            dgvGerirCliente.DataSource = ds;

            // Defini o membro do DataSet
            dgvGerirCliente.DataMember = "Clientes";
        }

        public void btnBuscar_Click(object sender, EventArgs e)
        {
            ClienteDao clienteDao = new ClienteDao(_conexao);

            string pesquisa = txbBuscar.Text.ToString();

            DataSet ds = new DataSet();
            ds = clienteDao.BuscarCliente(pesquisa);

            dgvGerirCliente.DataSource = ds;

            dgvGerirCliente.DataMember = "Clientes";
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            var frmaddCliente = new frmAdicionarCliente();
            frmaddCliente.Show();
        }

        private void dgvGerirCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            // Verifique se alguma linha está selecionada no DGV
            if(dgvGerirCliente.SelectedRows.Count > 0)
            {
                // Obtém o código do cliente da linha selecionada
                int codigo = Convert.ToInt32(dgvGerirCliente.CurrentRow.Cells[0].Value);
            }
        }
    }
}
