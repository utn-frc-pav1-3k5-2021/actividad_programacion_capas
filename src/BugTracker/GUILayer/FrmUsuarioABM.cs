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
    public partial class FrmUsuarioABM : Form
    {
        private FormMode formMode  = FormMode.nuevo;

        private readonly UsuarioService oUsuarioService;
        private Usuario oUsuarioSelected;

        public FrmUsuarioABM()
        {
            InitializeComponent();
            oUsuarioService = new UsuarioService();
        }

        private void LlenarCombo(ComboBox cbo, Object source, string display, String value)
        {
            cbo.DataSource = source;
            cbo.DisplayMember = display;
            cbo.ValueMember = value;
            cbo.SelectedIndex = -1;
        }

        public enum FormMode {nuevo,modificar,eliminar}

        public void InicializarFormulario(FormMode op, Usuario usuarioSelected)
        {
            formMode = op;
            oUsuarioSelected = usuarioSelected;
        }

        private void FrmUsuarioABM_Load(object sender, EventArgs e)
        {
            LlenarCombo(cmbPerfil, DataManager.GetInstance().ConsultaSQL("Select * from Perfiles"), "nombre", "id_perfil");

            switch (formMode)
            {
                case FormMode.nuevo:
                    {
                        this.Text = "Nuevo Usuario";
                        break;
                    }
                case FormMode.modificar:
                    {
                        this.Text = "Actualizar Usuario";
                        //Recuperar usuario seleccionado en la grilla 
                        MostrarDatos();
                       
                        break;
                    }

                case FormMode.eliminar:
                    {
                        MostrarDatos();
                        this.Text = "Eliminar Usuario";
                        txtUsuario.Enabled = false;
                        txtEmail.Enabled = false;
                        cmbPerfil.Enabled = false;
                        txtContrasena.Enabled = false;
                        txtRC.Enabled = false;
                        break;
                    }
            }
        }

        private void MostrarDatos()
        {
            if (oUsuarioSelected != null)
            {
                lblId.Text = "ID: " + oUsuarioSelected.IdUsuario.ToString();
                lblId.Enabled = true;
                txtUsuario.Text = oUsuarioSelected.NombreUsuario;
                txtEmail.Text = oUsuarioSelected.Email;
                txtContrasena.Text = oUsuarioSelected.Contrasena;
                txtRC.Text = txtContrasena.Text;
                cmbPerfil.Text = oUsuarioSelected.Perfil.Nombre;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            switch (formMode)
            {
                case FormMode.nuevo:
                    {
                        if (!ExisteUsuario(txtUsuario.Text))
                        {
                            if (ValidarCampo())
                            {
                                var oUsuario = new Usuario();
                                oUsuario.NombreUsuario = txtUsuario.Text;
                                oUsuario.Contrasena = txtContrasena.Text;
                                oUsuario.Email = txtEmail.Text;
                                oUsuario.Perfil = new Perfil();
                                oUsuario.Perfil.IdPerfil = (int)cmbPerfil.SelectedValue;

                                if (oUsuarioService.CrearUsuario(oUsuario))
                                {
                                    MessageBox.Show("Usuario insertado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }
                            }
                        } else
                        {
                            MessageBox.Show("Nombre de usuario no valido o ya existe. Ingrese otro por favor.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    }

                case FormMode.modificar:
                    {
                        if (ValidarCampo())
                        {
                            oUsuarioSelected.NombreUsuario = txtUsuario.Text;
                            oUsuarioSelected.Contrasena = txtContrasena.Text;
                            oUsuarioSelected.Email = txtEmail.Text;
                            oUsuarioSelected.Perfil = new Perfil();
                            oUsuarioSelected.Perfil.IdPerfil = (int)cmbPerfil.SelectedValue;

                            if (oUsuarioService.ActualizarUsuario(oUsuarioSelected)) {
                                MessageBox.Show("Usuario actualizado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                                MessageBox.Show("Error al actualizar el usuario!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        } 
                        break;
                    }

                case FormMode.eliminar:
                    {
                         if (MessageBox.Show("Seguro que desea deshabilitar el usuario seleccionado?", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            if (oUsuarioService.EliminarUsuario(oUsuarioSelected))
                            {
                                MessageBox.Show("Usuario Deshabilitado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                                MessageBox.Show("Error al deshabilitar el usuario!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    }
            }
           
        }

        private bool ValidarCampo()
        {
            if (string.IsNullOrEmpty(txtUsuario.Text) || txtUsuario.Text.Length > 50) {
                txtUsuario.BackColor = Color.Red;
                txtUsuario.Focus();
                return false;
            } else
            {
                txtUsuario.BackColor = Color.White;
            }

            if (string.IsNullOrEmpty(txtContrasena.Text) || txtContrasena.Text.Length > 10)
            {
                txtContrasena.BackColor = Color.Red;
                txtContrasena.Focus();
                return false;
            }
            else
            {
                txtContrasena.BackColor = Color.White;
            }

            if (string.IsNullOrEmpty(txtRC.Text) || txtRC.Text.Length > 10)
            {
                txtRC.BackColor = Color.Red;
                txtRC.Focus();
                return false;
            }
            else
            {
                txtRC.BackColor = Color.White;
            }

            if (txtContrasena.Text != txtRC.Text)
            {
                txtRC.BackColor = Color.Red;
                txtRC.Focus();

                txtContrasena.BackColor = Color.Red;
                txtContrasena.Focus();
                return false;
            }
            else
            {
                txtRC.BackColor = Color.White;
                txtContrasena.BackColor = Color.White;
            }

            if (txtEmail.Text.Length > 50)
            {
                txtEmail.BackColor = Color.Red;
                txtEmail.Focus();
                return false;
            }
            else
            {
                txtEmail.BackColor = Color.White;
            }

            if (cmbPerfil.SelectedItem == null)
            {
                cmbPerfil.BackColor = Color.Red;
                cmbPerfil.Focus();
                return false;
            }   
            else
            {
                cmbPerfil.BackColor = Color.White;
            }

            return true;
        }

        private bool ExisteUsuario(String nombreUsuario)
        {
            return oUsuarioService.ExisteUsuario(nombreUsuario);
        }
    }
}
