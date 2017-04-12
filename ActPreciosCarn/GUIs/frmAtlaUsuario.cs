using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ActPreciosCarn.Negocio;

namespace ActPreciosCarn.GUIs
{
    public partial class frmAtlaUsuario : Form
    {
        private IConsultasMySQLNegocio _consultasMySQLNegocio;
        private IConsultasFBNegocio _consultasFBNegocio;

        public frmAtlaUsuario()
        {
            InitializeComponent();

            this.ActiveControl = this.tbNombre;
            this._consultasMySQLNegocio = new ConsultasMySQLNegocio();
            this._consultasFBNegocio = new ConsultasFBNegocio();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                // validaciones 
                if (string.IsNullOrEmpty(this.tbNombre.Text))
                    throw new Exception("Defina un nombre");

                if (string.IsNullOrEmpty(this.tbUsuario.Text))
                    throw new Exception("Defina un Usuario");

                if (string.IsNullOrEmpty(this.tbClave.Text))
                    throw new Exception("Defina una clave para la cuenta");

                if (string.IsNullOrEmpty(this.tbCorreo.Text))
                    throw new Exception("Defina un Correo para el usuario");

                if (!this.tbConfirmClave.Text.Equals(this.tbClave.Text))
                    throw new Exception("La claves no coinciden");

                string nombreCompleto = this.tbNombre.Text;
                string correo = this.tbCorreo.Text;
                string usuario = this.tbUsuario.Text;
                string clave = this.tbClave.Text;

                string fecha = getFechaFireBird();

                // guarda el usuario
                Modelos.Response resp = this._consultasMySQLNegocio.creaUsuario(nombreCompleto, correo, usuario, clave, fecha);

                if (resp.status == Modelos.Estatus.OK)
                {
                    // bitacora
                    this._consultasMySQLNegocio.generaBitacora(
                        "Usuario creado '" + usuario + "'", fecha);

                    MessageBox.Show("El usuario ha sido creado correctamente", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.tbNombre.Text = string.Empty;

                    this.tbCorreo.Text = string.Empty;
                    this.tbUsuario.Text = string.Empty;
                    this.tbClave.Text = string.Empty;
                    this.tbConfirmClave.Text = string.Empty;
                }
                else
                    throw new Exception(resp.error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public string getFechaFireBird()
        {
            string result = string.Empty;

            try
            {
                result = this._consultasFBNegocio.getFecha();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }
    }
}
