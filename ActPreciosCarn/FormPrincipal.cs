using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ActPreciosCarn.GUIs;
using System.IO;
using ActPreciosCarn.Negocio;
using DevExpress.XtraEditors.Repository;

namespace ActPreciosCarn
{
    public partial class FormPrincipal : Form
    {
        private IConsultasFBNegocio _consultasFBNegocio;
        private IConsultasMySQLNegocio _consultasMySQLNegocio;
        private List<Modelos.Articulos> _articulos = new List<Modelos.Articulos>();

        public FormPrincipal()
        {
            InitializeComponent();

            Rectangle resolution = Screen.PrimaryScreen.Bounds;

            if (resolution.Width > 1380)
            {
                this.gcArticActualizar.Anchor = (AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this._consultasFBNegocio = new ConsultasFBNegocio();
                this._consultasMySQLNegocio = new ConsultasMySQLNegocio();

                this.gcPreciosArt.DataSource = new List<Modelos.Articulos>();
                this.gcArticActualizar.DataSource = new List<Modelos.Articulos>();

                // Create the ToolTip and associate with the Form container.
                ToolTip toolTip1 = new ToolTip();

                // Set up the delays for the ToolTip.
                toolTip1.AutoPopDelay = 5000;
                toolTip1.InitialDelay = 1000;
                toolTip1.ReshowDelay = 500;

                // Force the ToolTip text to be displayed whether or not the form is active.
                toolTip1.ShowAlways = true;

                // Set up the ToolTip text for the Button and Checkbox.
                toolTip1.SetToolTip(this.btnCargaArti, "Carga los Artículos con sus precios desde Microsip");
                toolTip1.SetToolTip(this.btnAgregarArt, "Agregar los seleccionados a la lista de Artículos a Actualizar");
                toolTip1.SetToolTip(this.btnQuitarArt, "Quitar los seleccionados de la lista de Artículos a Actualizar");
                toolTip1.SetToolTip(this.btnQuitarTodos, "Limpiar la lista de Artículos a Actualizar");
                toolTip1.SetToolTip(this.btnGuarda, "Guarda todos los cambios realizados a los Artículos");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCargaArti_Click(object sender, EventArgs e)
        {
            try
            {
                // consulta articulos de mysql
                this._articulos = this._consultasMySQLNegocio.obtieneArticulos();

                this.gcPreciosArt.DataSource = this._articulos;

                this.gridView1.BestFitColumns();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void tbBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this._articulos.Count == 0)
                    return;

                List<Modelos.Articulos> seleccion = new List<Modelos.Articulos>();

                string texto = this.tbBusqueda.Text;

                seleccion = this._articulos.Where(w => w.articulo.ToUpper().Contains((texto.ToUpper())) || w.clave.Contains(texto)).ToList();

                this.gcPreciosArt.DataSource = null;
                this.gcPreciosArt.DataSource = seleccion;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#2E86C1");
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void gridView2_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#2E86C1");
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void btnAgregarArt_Click(object sender, EventArgs e)
        {
            try
            {
                // obtiene los articulos seleccionadas del grid de articulos
                List<Modelos.Articulos> seleccionados = ((List<Modelos.Articulos>)this.gridView1.DataSource).Where(w => w.seleccionado == true).Select(s => s).ToList();

                if (seleccionados.Count == 0) return;

                // obtiene los articulos agregados
                List<Modelos.Articulos> agregados = ((List<Modelos.Articulos>)this.gridView2.DataSource).ToList();

                foreach (Modelos.Articulos art in seleccionados)
                {
                    if (agregados.Where(w => w.idArticulo == art.idArticulo).ToList().Count == 0)
                    {
                        agregados.Add(new Modelos.Articulos 
                        {
                            articulo = art.articulo,
                            clave = art.clave,
                            idArticulo = art.idArticulo,
                            medioMayoreo = art.medioMayoreo,
                            precFilial = art.precFilial,
                            precIMSS = art.precIMSS,
                            precLista = art.precLista,
                            precMayoreo = art.precMayoreo,
                            precMinimo = art.precMinimo,
                            seleccionado = false
                        });
                    }
                }

                this.gcArticActualizar.DataSource = null;
                this.gcArticActualizar.DataSource = agregados;
                
                RepositoryItemTextEdit edit = new RepositoryItemTextEdit();
                edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                edit.Mask.EditMask = "#,###,##0.00####";
                this.gcArticActualizar.RepositoryItems.Add(edit);
                gridView2.Columns[4].ColumnEdit = edit;
                gridView2.Columns[5].ColumnEdit = edit;
                gridView2.Columns[6].ColumnEdit = edit;
                gridView2.Columns[7].ColumnEdit = edit;
                gridView2.Columns[8].ColumnEdit = edit;
                gridView2.Columns[9].ColumnEdit = edit;

                this.gridView2.BestFitColumns();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnQuitarArt_Click(object sender, EventArgs e)
        {
            try
            {
                // obtiene los articulos seleccionadas del grid de articulos
                List<Modelos.Articulos> seleccionados = ((List<Modelos.Articulos>)this.gridView2.DataSource).Where(w => w.seleccionado == false).Select(s => s).ToList();

                if (seleccionados.Count == 0) return;

                this.gcArticActualizar.DataSource = null;
                this.gcArticActualizar.DataSource = seleccionados;

                this.gridView2.BestFitColumns();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnQuitarTodos_Click(object sender, EventArgs e)
        {
            try
            {
                this.gcArticActualizar.DataSource = new List<Modelos.Articulos>();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuarda_Click(object sender, EventArgs e)
        {
            try
            {
                // obtiene los articulos seleccionadas del grid de articulos
                List<Modelos.Articulos> seleccionados = ((List<Modelos.Articulos>)this.gridView2.DataSource).Select(s => s).ToList();

                if (seleccionados.Count == 0) throw new Exception("No se han cargado los artículos a actualizar");

                string fecha1 = getFechaFireBird();

                int respuesta = this._consultasMySQLNegocio.guardaActualizacion(seleccionados, fecha1);

                if (respuesta < 0)
                    throw new Exception("Problemas al guardar los cambios");
                else
                {
                    // lista de articulos a actualizar
                    string lista = string.Empty;

                    foreach(Modelos.Articulos articulos in seleccionados)
                    {
                        lista += articulos.clave + " - " + articulos.articulo + "; ";
                    }

                    string fecha = getFechaFireBird();

                    // bitacora
                    long resultado = this._consultasMySQLNegocio.generaBitacora(
                        "Bloque de actualización creado '" + respuesta + "': " + lista.Trim(),
                        fecha);

                    // guarda bitacora detalle
                    // lista de precios anteriores
                    List<Modelos.Articulos> anteriores =
                        this._articulos.Where(w => seleccionados.Any(a => a.clave == w.clave)).ToList();

                    this._consultasMySQLNegocio.guardaBitacora(anteriores, seleccionados, resultado);

                    MessageBox.Show("Proceso concluido", "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.gcArticActualizar.DataSource = new List<Modelos.Articulos>();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                string fecha = getFechaFireBird();

                // bitacora
                this._consultasMySQLNegocio.generaBitacora(
                    "Sesión cerrada por el usuario '" + Modelos.Login.nombre + "'", fecha);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Desea salir de la aplicación?", "Actualizar Precios Carnicerías", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void altaUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmAtlaUsuario child = new frmAtlaUsuario();

                child.ShowDialog();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cambioDeClaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCambiarClave form = new frmCambiarClave(Modelos.Login.usuario, Modelos.Login.idUsuario);

                form.ShowDialog();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void descargaInformaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmDescargaInfo form = new frmDescargaInfo();

                form.ShowDialog();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCargaArtMicrosip_Click(object sender, EventArgs e)
        {
            try
            {
                // obtiene precios_empresas
                Dictionary<string, long> precEmpr = this._consultasFBNegocio.getPreciosEmpresas();

                // consulta articulos de microsip
                this._articulos = this._consultasFBNegocio.obtieneArticulos(precEmpr);

                this.gcPreciosArt.DataSource = this._articulos;

                this.gridView1.BestFitColumns();

                // insertar articulos en mysql
                this._consultasMySQLNegocio.insertaArticulos(this._articulos);

                string fecha = getFechaFireBird();

                // bitacora
                this._consultasMySQLNegocio.generaBitacora(
                    "Articulos cargados de microsip: servidor - " + Modelos.Login.servidor + " - ", fecha);
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
