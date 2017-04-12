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
        private IConsultasFBNegocio _consultasFBNegocio;

        public frmDescargaInfo()
        {
            InitializeComponent();
            this._consultasMySQLNegocio = new ConsultasMySQLNegocio();
            this._consultasFBNegocio = new ConsultasFBNegocio();
        }

        private void frmDescargaInfo_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Descarga Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
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
                string fechaFin = this.dtpFechaFin.Value.AddHours(23).AddMinutes(59).AddSeconds(59).ToString("yyyy-MM-dd");

                int bloque = 0;
                if (this.cbMostrarBloques.Checked)
                    bloque = (int)this.cmbBloques.SelectedItem;

                bool pendiente = this.cbPendientes.Checked;
                bool realizado = this.cbRealizados.Checked;

                List<Modelos.Actualizacion> resultado = this._consultasMySQLNegocio.obtieneInformacion(fechaIni, fechaFin, bloque, pendiente, realizado);

                if (!pendiente && !realizado)
                    resultado = new List<Modelos.Actualizacion>();

                if (resultado.Count == 0)
                {
                    this.gcActualizaciones.DataSource = new List<Modelos.Actualizacion>();

                    throw new Exception("Sin resultados");
                }

                this.gcActualizaciones.DataSource = null;
                this.gcActualizaciones.DataSource = resultado;

                string fecha = getFechaFireBird();

                // bitacora
                this._consultasMySQLNegocio.generaBitacora((
                    "Consulta de descarga de información:" 
                        + "FI:'" + fechaIni 
                        + "' FF:'" + fechaFin 
                        + "' Bloque:'" + bloque 
                        + "' " 
                        + (pendiente ? "PENDIENTE" : string.Empty) + " "
                        + (realizado ? "RELIZADO" : string.Empty)).Replace("  ", ""), fecha);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Descarga Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cbMostrarBloques_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.btnCargarBloques.Enabled = this.cbMostrarBloques.Checked;
                this.label4.Enabled = this.cbMostrarBloques.Checked;
                this.cmbBloques.Enabled = this.cbMostrarBloques.Checked;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Descarga Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCargarBloques_Click(object sender, EventArgs e)
        {
            try
            {
                // obtiene bloques del rango de fechas
                /*
                string fechaIni = this.dtpFechaInicio.Value.ToString("yyyy-MM-dd");
                string fechaFin = this.dtpFechaFin.Value.ToString("yyyy-MM-dd");
                */

                string fechaIni = this.dtpFechaInicio.Value.ToString("yyyy-MM-dd");
                string fechaFin = this.dtpFechaFin.Value.AddHours(23).AddMinutes(59).AddSeconds(59).ToString("yyyy-MM-dd");

                List<int> resultado = this._consultasMySQLNegocio.obtieneBloques(fechaIni, fechaFin);

                if (resultado.Count == 0)
                    throw new Exception("Sin resultados");

                this.cmbBloques.DataSource = resultado;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Descarga Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gcActualizaciones_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Modelos.Actualizacion entAct = new Modelos.Actualizacion();

                foreach (int i in this.gridView1.GetSelectedRows())
                {
                    var dr1 = this.gridView1.GetRow(i);

                    entAct = (Modelos.Actualizacion)dr1;
                }

                Modelos.ActualizacionDet res = this._consultasMySQLNegocio.obtieneDetalle(entAct.idActualizacion);

                frmVerPrecios form = new frmVerPrecios(entAct.nombreArticulo, res.precLista, res.precMinimo, res.precMayoreo, res.precFilial, res.precIMSS, res.medioMayoreo);

                form.ShowDialog();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Descarga Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //  F I D E L
            if (e.Column.FieldName == "fidel")
            {
                var data = gridView1.GetRow(e.RowHandle) as Modelos.Actualizacion;
                if (data == null)
                    return;

                if (data.fidel.Equals("PENDIENTE"))
                {
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }

                if (data.fidel.Equals("REALIZADO"))
                {
                    e.Appearance.ForeColor = Color.Green;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }

            // L I B E R T A D
            if (e.Column.FieldName == "libertad")
            {
                var data = gridView1.GetRow(e.RowHandle) as Modelos.Actualizacion;
                if (data == null)
                    return;

                if (data.libertad.Equals("PENDIENTE"))
                {
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }

                if (data.libertad.Equals("REALIZADO"))
                {
                    e.Appearance.ForeColor = Color.Green;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }

            // H E R O I C O
            if (e.Column.FieldName == "heroico")
            {
                var data = gridView1.GetRow(e.RowHandle) as Modelos.Actualizacion;
                if (data == null)
                    return;

                if (data.heroico.Equals("PENDIENTE"))
                {
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }

                if (data.heroico.Equals("REALIZADO"))
                {
                    e.Appearance.ForeColor = Color.Green;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }

            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#2E86C1");
                e.Appearance.ForeColor = Color.White;
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

