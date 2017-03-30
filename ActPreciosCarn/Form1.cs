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
    public partial class Form1 : Form
    {
        private IConsultasFBNegocio _consultasFBNegocio;
        private IConsultasMySQLNegocio _consultasMySQLNegocio;
        private List<Modelos.Articulos> _articulos = new List<Modelos.Articulos>();

        public Form1()
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
                // obtiene precios_empresas
                Dictionary<string, long> precEmpr = this._consultasFBNegocio.getPreciosEmpresas();

                // consulta articulos de microsip
                this._articulos = this._consultasFBNegocio.obtieneArticulos(precEmpr);

                this.gcPreciosArt.DataSource = this._articulos;

                RepositoryItemTextEdit edit = new RepositoryItemTextEdit();
                edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                edit.Mask.EditMask = "#,###,##0.00####";
                this.gcPreciosArt.RepositoryItems.Add(edit);
                gridView1.Columns[3].ColumnEdit = edit;
                gridView1.Columns[4].ColumnEdit = edit;
                gridView1.Columns[5].ColumnEdit = edit;
                gridView1.Columns[6].ColumnEdit = edit;
                gridView1.Columns[7].ColumnEdit = edit;
                gridView1.Columns[8].ColumnEdit = edit;

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

                int respuesta = this._consultasMySQLNegocio.guardaActualizacion(seleccionados);

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

                    // bitacora
                    this._consultasMySQLNegocio.generaBitacora(
                        "Bloque de actualización creado '" + respuesta + "': " + lista.Trim());

                    MessageBox.Show("Proceso concluido");
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
                // bitacora
                this._consultasMySQLNegocio.generaBitacora(
                    "Sesión cerrada por el usuario '" + Modelos.Login.nombre + "'");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Activos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
