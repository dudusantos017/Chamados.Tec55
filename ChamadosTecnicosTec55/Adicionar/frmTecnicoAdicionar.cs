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
    public partial class frmTecnicoAdicionar : Form
    {
        // Chamaaa a conexão 
        string _conexao = ChamadosTecnicosTec55.Properties.Settings.Default.Conexao;

        public frmTecnicoAdicionar()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txbNome.Clear();
            txbEspecialidade.Clear();
            txbEmail.Clear();
            txbObs.Clear();
            txbSenha.Clear();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Tecnico tecnico = new Tecnico();
            TecnicoDao tecnicoDao = new TecnicoDao(_conexao);

            if (string.IsNullOrEmpty(txbNome.Text) || string.IsNullOrEmpty(txbEspecialidade.Text) || string.IsNullOrEmpty(txbEmail.Text) || string.IsNullOrEmpty(txbSenha.Text) || string.IsNullOrEmpty(txbObs.Text))
            {
                MessageBox.Show("CADE OS DADOSSS ??");
            }
            else
            {
                try
                {
                    tecnico.Nome = txbNome.Text;
                    tecnico.Especialidade = txbEspecialidade.Text;
                    tecnico.Email = txbEmail.Text;
                    tecnico.Senha = txbSenha.Text;
                    tecnico.Obs = txbObs.Text;

                    tecnicoDao.IncluiTecnico(tecnico);
                    MessageBox.Show("Cadastrado com sucesso!");
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao Cadastrar", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
