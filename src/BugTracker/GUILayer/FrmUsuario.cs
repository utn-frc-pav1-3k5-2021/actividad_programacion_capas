using BugTracker.BusinessLayer;
using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BugTracker.GUILayer
{
    public partial class FrmUsuario : Form
    {
        private UsuarioService usuarioService;

        public FrmUsuario()
        {
            InitializeComponent();
            InitializeDGV();
            usuarioService = new UsuarioService();
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            LlenarCombo(cmbPerfil, DataManager.GetInstance().ConsultaSQL("Select * from Perfiles"), "nombre", "id_perfil");
        }

        private void LlenarCombo(ComboBox cbo, Object source, string display, String value)
        {
            // Datasource: establece el origen de datos de este objeto.
            cbo.DataSource = source;
            // DisplayMember: establece la propiedad que se va a mostrar para este ListControl.
            cbo.DisplayMember = display;
            // ValueMember: establece la ruta de acceso de la propiedad que se utilizará como valor real para los elementos de ListControl.
            cbo.ValueMember = value;
            //SelectedIndex: establece el índice que especifica el elemento seleccionado actualmente.
            cbo.SelectedIndex = -1;
        }

        private void InitializeDGV()
        {
            // Cree un DataGridView no vinculado declarando un recuento de columnas.
            dgvUsuario.ColumnCount = 5;
            dgvUsuario.ColumnHeadersVisible = true;

            // Configuramos la AutoGenerateColumns en false para que no se autogeneren las columnas
            dgvUsuario.AutoGenerateColumns = false;

            // Cambia el estilo de la cabecera de la grilla.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            dgvUsuario.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Definimos el nombre de la columnas y el DataPropertyName que se asocia a DataSource
            dgvUsuario.Columns[0].Name = "ID";
            dgvUsuario.Columns[0].DataPropertyName = "IdUsuario";
            // Definimos el ancho de la columna.
            dgvUsuario.Columns[0].Width = 100;

            dgvUsuario.Columns[1].Name = "Nombre";
            dgvUsuario.Columns[1].DataPropertyName = "NombreUsuario";

            dgvUsuario.Columns[2].Name = "Perfil";
            dgvUsuario.Columns[2].DataPropertyName = "Perfil";

            dgvUsuario.Columns[3].Name = "Email";
            dgvUsuario.Columns[3].DataPropertyName = "Email";

            dgvUsuario.Columns[4].Name = "Baja";
            dgvUsuario.Columns[4].DataPropertyName = "Borrado";

            // Cambia el tamaño de la altura de los encabezados de columna.
            dgvUsuario.AutoResizeColumnHeadersHeight();

            // Cambia el tamaño de todas las alturas de fila para ajustar el contenido de todas las celdas que no sean de encabezado.
            dgvUsuario.AutoResizeRows(
                DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(txtUsuario.Text))
            {
                var nombreUsuario = txtUsuario.Text;
                parametros.Add("NombreUsuario", nombreUsuario);
            }

            if (!string.IsNullOrEmpty(cmbPerfil.Text))
            {
                parametros.Add("Perfil", cmbPerfil.SelectedValue.ToString());
            }

            if (ckbBorrados.Checked)
            {
                parametros.Add("Borrado", ckbBorrados.Checked);
            }

            IList<Usuario> usuarios = usuarioService.ConsultarUsuariosConFiltro(parametros);

            dgvUsuario.DataSource = usuarios;

            if (dgvUsuario.RowCount == 0)
            {
                MessageBox.Show("No se encontraron usuario/s", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvUsuario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnModificar.Enabled = true;
            btnBaja.Enabled = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmUsuarioABM frmUsuarioABM = new FrmUsuarioABM();
            frmUsuarioABM.ShowDialog();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            FrmUsuarioABM frmUsuarioABM = new FrmUsuarioABM();

            var usuarioSelected = (Usuario)dgvUsuario.CurrentRow.DataBoundItem;

            frmUsuarioABM.InicializarFormulario(FrmUsuarioABM.FormMode.modificar,usuarioSelected);

            frmUsuarioABM.ShowDialog();
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            FrmUsuarioABM frmUsuarioABM = new FrmUsuarioABM();

            var usuarioSelected = (Usuario)dgvUsuario.CurrentRow.DataBoundItem;

            frmUsuarioABM.InicializarFormulario(FrmUsuarioABM.FormMode.eliminar, usuarioSelected);

            frmUsuarioABM.ShowDialog();
        }

    }
}
