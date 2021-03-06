﻿using System;
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
    public partial class frmConfiguracion : Form
    {
        private string _path = string.Empty;
        private IConsultasFBNegocio _consultasFBNegocio;
        private IConsultasMySQLNegocio _consultasMySQLNegocio;

        public frmConfiguracion()
        {
            InitializeComponent();
        }

        private void frmConfiguracion_Load(object sender, EventArgs e)
        {
            try
            {
                string fileName = "config.dat";
                string pathConfigFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DescPrecCarn\";

                // si no existe el directorio, lo crea
                bool exists = System.IO.Directory.Exists(pathConfigFile);

                if (!exists) System.IO.Directory.CreateDirectory(pathConfigFile);

                // busca en el directorio si exite el archivo con el nombre dado
                var file = Directory.GetFiles(pathConfigFile, fileName, SearchOption.AllDirectories)
                        .FirstOrDefault();

                this._path = pathConfigFile + fileName;

                if (file != null)
                {
                    // si existe
                    // cargar los datos en los campos
                    FEncrypt.Respuesta result = FEncrypt.EncryptDncrypt.DecryptFile(this._path, "milagros");

                    if (result.status == FEncrypt.Estatus.ERROR)
                        throw new Exception(result.error);

                    if (result.status == FEncrypt.Estatus.OK)
                    {
                        string[] list = result.resultado.Split(new string[] { "||" }, StringSplitOptions.None);

                        if (list.Count() < 9)
                        {
                            foreach (Control x in this.Controls)
                            {
                                if (x is TextBox)
                                {
                                    ((TextBox)x).Text = string.Empty;
                                }
                            }
                        }
                        else
                        {
                            // MICROSIP
                            this.tbServidorM.Text = list[0].Substring(2);       // IP
                            this.tbUsuarioM.Text = list[1].Substring(2);        // PUERTO
                            this.tbContraseniaM.Text = list[2].Substring(2);    // servidor
                            this.tbPuertoM.Text = list[3].Substring(2);         // usuario
                            this.tbBaseDatosM.Text = list[4].Substring(2);      // contraseña

                            // MYSQL
                            this.tbServidorMs.Text = list[5].Substring(2);      // base de datos
                            this.tbUsuarioMs.Text = list[6].Substring(2);       // nombre del checador
                            this.tbContraseniaMs.Text = list[7].Substring(2);   // tipo del checador
                            this.tbBaseDeDatosMs.Text = list[8].Substring(2);   // numero del checador

                            // SUCURSAL
                            this.cmbSucursal.SelectedItem = list[9].Substring(2); // sucursal
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnProbarConnMicrosip_Click(object sender, EventArgs e)
        {
            try
            {
                Modelos.ConectionString.connFB = string.Format(
                            "User={0};Password={1};Database={2};DataSource={3};Port={4}",
                            this.tbUsuarioM.Text,
                            this.tbContraseniaM.Text,
                            this.tbBaseDatosM.Text,
                            this.tbServidorM.Text,
                            this.tbPuertoM.Text);

                this._consultasFBNegocio = new ConsultasFBNegocio();

                bool pruebaConn = this._consultasFBNegocio.pruebaConn();

                if (pruebaConn)
                    MessageBox.Show("Conexión Exitosa!!!", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    throw new Exception("Falló la conexión a la base de datos del Microsip");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnProbarConMysql_Click(object sender, EventArgs e)
        {
            try
            {

                // validaciones
                foreach (Control x in this.groupBox2.Controls)
                {
                    if (x is TextBox)
                    {
                        if (string.IsNullOrEmpty(((TextBox)x).Text))
                            throw new Exception("Campos incompletos, Por favor verifique");
                    }
                }

                Modelos.ConectionString.connMySQL = string.Format(
                            "Data Source={0};database={1};User Id={2};password={3};",
                            this.tbServidorMs.Text,
                            this.tbBaseDeDatosMs.Text,
                            this.tbUsuarioMs.Text,
                            this.tbContraseniaMs.Text);

                this._consultasMySQLNegocio = new ConsultasMySQLNegocio();

                bool pruebaConn = this._consultasMySQLNegocio.pruebaConn();

                if (pruebaConn)
                    MessageBox.Show("Conexión Exitosa!!!", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    throw new Exception("Falló la conexión a la base de datos del Microsip");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // validaciones
                foreach (Control x in this.groupBox1.Controls)
                {
                    if (x is TextBox)
                    {
                        if (string.IsNullOrEmpty(((TextBox)x).Text))
                            throw new Exception("Campos incompletos, Por favor verifique");
                    }
                }

                // validaciones
                foreach (Control x in this.groupBox2.Controls)
                {
                    if (x is TextBox)
                    {
                        if (string.IsNullOrEmpty(((TextBox)x).Text))
                            throw new Exception("Campos incompletos, Por favor verifique");
                    }
                }

                // validacion sucursal
                if (this.cmbSucursal.SelectedIndex == -1)
                    throw new Exception("Seleccione la Sucursal a la que pertenece");

                // define texto del archivo
                string cadena = string.Empty;

                // MICROSIP
                cadena += "S_" + this.tbServidorM.Text + "||";
                cadena += "U_" + this.tbUsuarioM.Text + "||";
                cadena += "C_" + this.tbContraseniaM.Text + "||";
                cadena += "P_" + this.tbPuertoM.Text + "||";
                cadena += "B_" + this.tbBaseDatosM.Text + "||";

                // MYSQL
                cadena += "S_" + this.tbServidorMs.Text + "||";
                cadena += "U_" + this.tbUsuarioMs.Text + "||";
                cadena += "C_" + this.tbContraseniaMs.Text + "||";
                cadena += "B_" + this.tbBaseDeDatosMs.Text + "||";

                // SUCURSAL
                cadena += "S_" + this.cmbSucursal.SelectedItem + "||";

                // prosigue con la creación del archivo
                FEncrypt.Respuesta result = FEncrypt.EncryptDncrypt.EncryptFile("milagros", cadena, this._path);

                if (result.status == FEncrypt.Estatus.ERROR)
                    throw new Exception(result.error);

                if (result.status == FEncrypt.Estatus.OK)
                {
                    // firebird
                    Modelos.ConectionString.connFB = string.Format(
                                "User={0};Password={1};Database={2};DataSource={3};Port={4}",
                                this.tbUsuarioM.Text,
                                this.tbContraseniaM.Text,
                                this.tbBaseDatosM.Text,
                                this.tbServidorM.Text,
                                this.tbPuertoM.Text);

                    Modelos.Login.servidor = this.tbServidorM.Text;

                    // mysql
                    Modelos.ConectionString.connMySQL = string.Format(
                                "Data Source={0};database={1};User Id={2};password={3};",
                                this.tbServidorMs.Text,
                                this.tbBaseDeDatosMs.Text,
                                this.tbUsuarioMs.Text,
                                this.tbContraseniaMs.Text);

                    // sucursal
                    Modelos.Login.sucursal = (string)this.cmbSucursal.SelectedItem;

                    MessageBox.Show("Se cargó correctamente la información", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;

                    this.Close();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
