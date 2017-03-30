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
    public partial class frmCambiarClave : Form
    {
        private string _usuario;
        private int _idUsuario;
        IConsultasMySQLNegocio _consultasMySQLNegocio;

        public frmCambiarClave(string usuario, int idUsuario)
        {
            InitializeComponent();

            this._usuario = usuario;
            this._idUsuario = idUsuario;
            this._consultasMySQLNegocio = new ConsultasMySQLNegocio();
        }

        private void frmCambiarClave_Load(object sender, EventArgs e)
        {
            this.tbUsuario.Text = this._usuario;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // validaciones
                if (string.IsNullOrEmpty(this.tbClaveActual.Text))
                    throw new Exception("Ingrese la clave que tiene actualmente");

                if (string.IsNullOrEmpty(this.tbClaveNueva.Text))
                    throw new Exception("Ingrese la nueva clave");

                if (string.IsNullOrEmpty(this.tbConfirmClave.Text))
                    throw new Exception("Ingrese la clave de confirmación");

                bool resp = this._consultasMySQLNegocio.validaClave(this.tbClaveActual.Text, _idUsuario);

                if (!resp)
                    throw new Exception("La clave no es correcta");

                if (!this.tbConfirmClave.Text.Equals(this.tbClaveNueva.Text))
                    throw new Exception("La claves no coinciden");

                // cambia la clave
                bool respuesta = this._consultasMySQLNegocio.actualizaClave(this.tbClaveNueva.Text, this._idUsuario, this._usuario);

                if (respuesta)
                {
                    // bitacora
                    this._consultasMySQLNegocio.generaBitacora(
                        "Se modificó la clave del usuario " + this._usuario + " por " + Modelos.Utilerias.Base64Encode(this.tbClaveNueva.Text));

                    this.tbClaveActual.Text = string.Empty;
                    this.tbClaveNueva.Text = string.Empty;
                    this.tbConfirmClave.Text = string.Empty;

                    MessageBox.Show("Se ha cambiado la clave con exito", "Cambio de clave", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Cambio de Clave", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void tbConfirmClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.btnGuardar_Click(null, null);
            }
        }
    }
}
