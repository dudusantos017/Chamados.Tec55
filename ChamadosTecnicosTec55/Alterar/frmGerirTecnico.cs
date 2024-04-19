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

    public partial class frmGerirTecnico : Form
    {
        public frmGerirTecnico()
        {
            InitializeComponent();
        }

        public void ListarTecnico()
        {
            //  Chama o Cliente DAO
            TecnicoDao tecnicoDao = new TecnicoDao(_conexao);

            // Captura o valor digitado na barra de texto TXB
            string busca = txbBuscar.Text.ToString();

            // Chama o Metodo BuscarCliente do objeto
            DataSet ds = new DataSet();
            ds = tecnicoDao.BuscaTecnico(busca);

            // Defini o DataSource do DataGridView
            dgvGerirTecnico.DataSource = ds;

            // Defini o membro do DataSet
            dgvGerirTecnico.DataMember = "Tecnicos";
        }

        string _conexao = ChamadosTecnicosTec55.Properties.Settings.Default.Conexao;

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            var frmtecnicoAdicionar = new frmTecnicoAdicionar();
            frmtecnicoAdicionar.Show();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            // Verifique se alguma linha está selecionada no DGV
            if (dgvGerirTecnico.SelectedRows.Count > 0)
            {
                int codigo = Convert.ToInt32(dgvGerirTecnico.CurrentRow.Cells[0].Value);

                var frmalterarTecnico = new frmAlterarTecnico(codigo);
                frmalterarTecnico.ShowDialog();

                //Listar Clientes apos fechar tela
                ListarTecnico();
            }

            else
            {
                MessageBox.Show("Selecione um registro para alteração");
            }
        }

        private void frmGerirTecnico_Load(object sender, EventArgs e)
        {
            ListarTecnico();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txbBuscar.Text != "")
            {
                ListarTecnico();
            }
            else
            {
                MessageBox.Show("Digite algo para buscar");
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvGerirTecnico.SelectedRows.Count > 0)
            {
                int codigo = Convert.ToInt32(dgvGerirTecnico.CurrentRow.Cells[0].Value);

                var resultado = MessageBox.Show("Deseja Excluir?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (resultado == DialogResult.Yes)
                {
                    TecnicoDao tecnicoDao = new TecnicoDao(_conexao);
                    tecnicoDao.ExcluirTecnico(codigo);
                    ListarTecnico();
                }
            }
            else
            {
            }
        }
    }
}
