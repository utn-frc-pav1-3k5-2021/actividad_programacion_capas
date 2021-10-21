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
    public partial class frmProducto : Form
    {
        private ProductoService productoService;
        public frmProducto()
        {
            InitializeComponent();
            InitializeDataGridView();
            productoService = new ProductoService();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeDataGridView()
        {
            // Cree un DataGridView no vinculado declarando un recuento de columnas.
            dgvProductos.ColumnCount = 2;
            dgvProductos.ColumnHeadersVisible = true;

            // Configuramos la AutoGenerateColumns en false para que no se autogeneren las columnas
            dgvProductos.AutoGenerateColumns = false;

            // Cambia el estilo de la cabecera de la grilla.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            dgvProductos.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Definimos el nombre de la columnas y el DataPropertyName que se asocia a DataSource
            dgvProductos.Columns[0].Name = "ID";
            dgvProductos.Columns[0].DataPropertyName = "idProducto";
            // Definimos el ancho de la columna.
            dgvProductos.Columns[0].Width = 100;

            dgvProductos.Columns[1].Name = "Nombre";
            dgvProductos.Columns[1].DataPropertyName = "Nombre";
            dgvProductos.Columns[1].Width = 250;

            // Cambia el tamaño de la altura de los encabezados de columna.
            dgvProductos.AutoResizeColumnHeadersHeight();

            // Cambia el tamaño de todas las alturas de fila para ajustar el contenido de todas las celdas que no sean de encabezado.
            dgvProductos.AutoResizeRows(
                DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            // Dictionary: Representa una colección de claves y valores.
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(txtId.Text)) {
                parametros.Add("idProducto", txtId.Text);
            }

            if (!string.IsNullOrEmpty(txtNom.Text))
            {
                parametros.Add("nombreProducto", txtNom.Text);
            }

            IList<Producto> productos = productoService.ConsultarProductosConFiltro(parametros);

            dgvProductos.DataSource = productos;

            if (dgvProductos.RowCount == 0)
            {
                MessageBox.Show("No se encontraron productos.","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }

        }
    }
}
