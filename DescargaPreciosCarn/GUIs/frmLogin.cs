using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DescargaPreciosCarn.Negocio;

namespace DescargaPreciosCarn.GUIs
{
    public partial class frmLogin : Form
    {
        private bool _defConfig;
        private IConsultasMySQLNegocio _consultasMySQLNegocio;

        public frmLogin()
        {
            InitializeComponent();
            this.ActiveControl = this.tbUsuario;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                // valida si ya tiene alguna clave guardada para el archivo
                string cveActual = Properties.Settings.Default.accesoConfig;

                if (string.IsNullOrEmpty(cveActual))
                {
                    string acceso = Modelos.Utilerias.Transform("p4ssw0rd");

                    Properties.Settings.Default.accesoConfig = acceso;
                    Properties.Settings.Default.Save();
                }

                string fileName = "config.dat";
                string pathConfigFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DescPrecCarn\";

                // si no existe el directorio, lo crea
                bool exists = System.IO.Directory.Exists(pathConfigFile);

                if (!exists) System.IO.Directory.CreateDirectory(pathConfigFile);

                // busca en el directorio si exite el archivo con el nombre dado
                var file = Directory.GetFiles(pathConfigFile, fileName, SearchOption.AllDirectories)
                        .FirstOrDefault();

                if (file == null)
                {
                    // no existe
                    // abrir el formulario para llenar la configuracion de conexion 
                    frmConfiguracion form = new frmConfiguracion();
                    var resultado = form.ShowDialog();

                    if (resultado != System.Windows.Forms.DialogResult.OK)
                    {
                        this._defConfig = false;
                        throw new Exception("No se ha definido la configuración");
                    }
                }

                file = Directory.GetFiles(pathConfigFile, fileName, SearchOption.AllDirectories)
                        .FirstOrDefault();

                // si existe
                // obtener la cadena de conexion del archivo
                FEncrypt.Respuesta result = FEncrypt.EncryptDncrypt.DecryptFile(file, "milagros");

                if (result.status == FEncrypt.Estatus.ERROR)
                    throw new Exception(result.error);

                if (result.status == FEncrypt.Estatus.OK)
                {
                    string[] list = result.resultado.Split(new string[] { "||" }, StringSplitOptions.None);

                    // FIREBIRD
                    string servidorM = list[0].Substring(2);    // servidor microsip
                    string usuarioM = list[1].Substring(2);     // usuario microsip
                    string contraM = list[2].Substring(2);      // contraseña microsip
                    string puertoM = list[3].Substring(2);      // puerto microsip
                    string baseDatosM = list[4].Substring(2);   // base de datos microsip

                    // MySQL
                    string servidorMs = list[5].Substring(2);   // servidor mysql
                    string usuarioMs = list[6].Substring(2);    // usuario mysql
                    string contraMs = list[7].Substring(2);     // contraseña
                    string baseDatosMs = list[8].Substring(2);  // base de datos

                    string sucursal = list[9].Substring(2);     // sucursal

                    Modelos.ConectionString.connFB = string.Format(
                                "User={0};Password={1};Database={2};DataSource={3};Port={4}",
                                usuarioM,
                                contraM,
                                baseDatosM,
                                servidorM,
                                puertoM);

                    Modelos.ConectionString.connMySQL = string.Format(
                                "Data Source={0};database={1};User Id={2};password={3};",
                                servidorMs,
                                baseDatosMs,
                                usuarioMs,
                                contraMs);

                    Modelos.Login.sucursal = sucursal;
                }

                this._defConfig = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            frmAccesso formA = new frmAccesso();

            var respuesta = formA.ShowDialog();

            if (respuesta == System.Windows.Forms.DialogResult.OK)
            {
                frmConfiguracion form = new frmConfiguracion();
                var resultado = form.ShowDialog();

                if (resultado == System.Windows.Forms.DialogResult.OK)
                    this.frmLogin_Load(null, null);
            }
        }

        private void tbUsuario_Enter(object sender, EventArgs e)
        {
            this.tbUsuario.SelectAll();
        }

        private void tbUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.ActiveControl = this.tbPass;
                this.tbPass.SelectAll();
            }
        }

        private void tbPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.btnAcceder_Click(null, null);
            }
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            try
            {
                this._consultasMySQLNegocio = new ConsultasMySQLNegocio();

                // validaciones
                if (string.IsNullOrEmpty(this.tbUsuario.Text))
                    throw new Exception("Llene el campo Usuario");

                if (string.IsNullOrEmpty(this.tbPass.Text))
                    throw new Exception("Llene el campo Clave");

                Modelos.Response resp = this._consultasMySQLNegocio.validaAcceso(this.tbUsuario.Text, this.tbPass.Text);

                if (resp.status == Modelos.Estatus.OK)
                {
                    // almacenar credeniales
                    Modelos.Login.idUsuario = resp.usuario.idUsuario;
                    Modelos.Login.nombre = resp.usuario.nombreCompleto;
                    Modelos.Login.usuario = resp.usuario.usuario;

                    // bitacora
                    this._consultasMySQLNegocio.generaBitacora(
                        "Nuevo Acceso a usuario '" + Modelos.Login.nombre + "' en '" + Modelos.Login.sucursal + "'");

                    this.Hide();
                    new FormPrincipal().ShowDialog();
                    this.Close();
                }
                else throw new Exception(resp.error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
