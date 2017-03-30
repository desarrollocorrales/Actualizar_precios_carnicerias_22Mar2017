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
    public partial class frmDescargaInfo : Form
    {
        private IConsultasMySQLNegocio _consultasMySQLNegocio;

        public frmDescargaInfo()
        {
            InitializeComponent();
            this._consultasMySQLNegocio = new ConsultasMySQLNegocio();
        }

        private void frmDescargaInfo_Load(object sender, EventArgs e)
        {
            try
            {
                // validaciones
                if (string.IsNullOrEmpty(this.dtpFechaInicio.Text))
                    throw new Exception("Defina una fecha de inicio");

                if (string.IsNullOrEmpty(this.dtpFechaFin.Text))
                    throw new Exception("Defina una fecha final");

                if (this.cbMostrarBloques.Checked)
                    if (this.cmbBloques.SelectedIndex == -1)
                        throw new Exception("Seleccione un bloque");

                if (this.dtpFechaInicio.Value > this.dtpFechaFin.Value)
                    throw new Exception("La fecha de inicio no puede ser mayor a la fecha final");

                string fechaIni = this.dtpFechaInicio.Value.ToString("yyyy-MM-dd");
                string fechaFin = this.dtpFechaFin.Value.ToString("yyyy-MM-dd");

                int bloque = 0;
                if (this.cbMostrarBloques.Checked)
                    bloque = (int)this.cmbBloques.SelectedItem;

                bool pendiente = this.cbPendientes.Checked;
                bool realizado = this.cbRealizados.Checked;

                List<Modelos.Actualizacion> resultado = this._consultasMySQLNegocio.obtieneInformacion(fechaIni, fechaFin, bloque, pendiente, realizado);

                if (resultado.Count == 0)
                    throw new Exception("Sin resultados");

                this.gcActualizaciones.DataSource = null;
                this.gcActualizaciones.DataSource = resultado;

                this.gridView1.BestFitColumns();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Descarga Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}

