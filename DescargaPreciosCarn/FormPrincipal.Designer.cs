namespace DescargaPreciosCarn
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.lbLeyenda = new System.Windows.Forms.Label();
            this.btnVerifPendiente = new System.Windows.Forms.Button();
            this.btnDescargarInfo = new System.Windows.Forms.Button();
            this.gcArticulos = new DevExpress.XtraGrid.GridControl();
            this.articulosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colidArticulo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colclave = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colarticulo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprecLista = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprecMinimo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprecMayoreo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprecFilial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprecIMSS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colmedioMayoreo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lbPendientes = new System.Windows.Forms.Label();
            this.btnActPendientes = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gcArticulos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.articulosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbLeyenda
            // 
            this.lbLeyenda.AutoSize = true;
            this.lbLeyenda.Font = new System.Drawing.Font("Tahoma", 18F);
            this.lbLeyenda.Location = new System.Drawing.Point(12, 9);
            this.lbLeyenda.Name = "lbLeyenda";
            this.lbLeyenda.Size = new System.Drawing.Size(300, 29);
            this.lbLeyenda.TabIndex = 1;
            this.lbLeyenda.Text = "Descargar Precios Sucursal";
            // 
            // btnVerifPendiente
            // 
            this.btnVerifPendiente.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.btnVerifPendiente.Location = new System.Drawing.Point(12, 58);
            this.btnVerifPendiente.Name = "btnVerifPendiente";
            this.btnVerifPendiente.Size = new System.Drawing.Size(161, 33);
            this.btnVerifPendiente.TabIndex = 13;
            this.btnVerifPendiente.Text = "Cargar Información";
            this.btnVerifPendiente.UseVisualStyleBackColor = true;
            this.btnVerifPendiente.Click += new System.EventHandler(this.btnVerifPendiente_Click);
            // 
            // btnDescargarInfo
            // 
            this.btnDescargarInfo.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.btnDescargarInfo.Location = new System.Drawing.Point(587, 58);
            this.btnDescargarInfo.Name = "btnDescargarInfo";
            this.btnDescargarInfo.Size = new System.Drawing.Size(194, 33);
            this.btnDescargarInfo.TabIndex = 14;
            this.btnDescargarInfo.Text = "Actualizar Precios";
            this.btnDescargarInfo.UseVisualStyleBackColor = true;
            this.btnDescargarInfo.Click += new System.EventHandler(this.btnDescargarInfo_Click);
            // 
            // gcArticulos
            // 
            this.gcArticulos.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcArticulos.DataSource = this.articulosBindingSource;
            this.gcArticulos.Location = new System.Drawing.Point(12, 97);
            this.gcArticulos.MainView = this.gridView1;
            this.gcArticulos.Name = "gcArticulos";
            this.gcArticulos.Size = new System.Drawing.Size(769, 325);
            this.gcArticulos.TabIndex = 15;
            this.gcArticulos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // articulosBindingSource
            // 
            this.articulosBindingSource.DataSource = typeof(DescargaPreciosCarn.Modelos.Articulos);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colidArticulo,
            this.colclave,
            this.colarticulo,
            this.colprecLista,
            this.colprecMinimo,
            this.colprecMayoreo,
            this.colprecFilial,
            this.colprecIMSS,
            this.colmedioMayoreo});
            this.gridView1.GridControl = this.gcArticulos;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowViewCaption = true;
            this.gridView1.ViewCaption = "Artículos";
            // 
            // colidArticulo
            // 
            this.colidArticulo.FieldName = "idArticulo";
            this.colidArticulo.Name = "colidArticulo";
            // 
            // colclave
            // 
            this.colclave.Caption = "Clave";
            this.colclave.FieldName = "clave";
            this.colclave.Name = "colclave";
            this.colclave.Visible = true;
            this.colclave.VisibleIndex = 0;
            // 
            // colarticulo
            // 
            this.colarticulo.Caption = "Artículo";
            this.colarticulo.FieldName = "articulo";
            this.colarticulo.Name = "colarticulo";
            this.colarticulo.Visible = true;
            this.colarticulo.VisibleIndex = 1;
            // 
            // colprecLista
            // 
            this.colprecLista.Caption = "Precio Lista";
            this.colprecLista.FieldName = "precLista";
            this.colprecLista.Name = "colprecLista";
            this.colprecLista.Visible = true;
            this.colprecLista.VisibleIndex = 2;
            // 
            // colprecMinimo
            // 
            this.colprecMinimo.Caption = "Precio Miínimo";
            this.colprecMinimo.FieldName = "precMinimo";
            this.colprecMinimo.Name = "colprecMinimo";
            this.colprecMinimo.Visible = true;
            this.colprecMinimo.VisibleIndex = 3;
            // 
            // colprecMayoreo
            // 
            this.colprecMayoreo.Caption = "Precio Mayoreo";
            this.colprecMayoreo.FieldName = "precMayoreo";
            this.colprecMayoreo.Name = "colprecMayoreo";
            this.colprecMayoreo.Visible = true;
            this.colprecMayoreo.VisibleIndex = 4;
            // 
            // colprecFilial
            // 
            this.colprecFilial.Caption = "Precio Filial";
            this.colprecFilial.FieldName = "precFilial";
            this.colprecFilial.Name = "colprecFilial";
            this.colprecFilial.Visible = true;
            this.colprecFilial.VisibleIndex = 5;
            // 
            // colprecIMSS
            // 
            this.colprecIMSS.Caption = "Precio IMSS";
            this.colprecIMSS.FieldName = "precIMSS";
            this.colprecIMSS.Name = "colprecIMSS";
            this.colprecIMSS.Visible = true;
            this.colprecIMSS.VisibleIndex = 6;
            // 
            // colmedioMayoreo
            // 
            this.colmedioMayoreo.Caption = "Medio Mayoreo";
            this.colmedioMayoreo.FieldName = "medioMayoreo";
            this.colmedioMayoreo.Name = "colmedioMayoreo";
            this.colmedioMayoreo.Visible = true;
            this.colmedioMayoreo.VisibleIndex = 7;
            // 
            // lbPendientes
            // 
            this.lbPendientes.BackColor = System.Drawing.Color.Transparent;
            this.lbPendientes.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lbPendientes.Location = new System.Drawing.Point(388, 20);
            this.lbPendientes.Name = "lbPendientes";
            this.lbPendientes.Size = new System.Drawing.Size(347, 20);
            this.lbPendientes.TabIndex = 16;
            this.lbPendientes.Text = "ACTUALIZADO";
            this.lbPendientes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnActPendientes
            // 
            this.btnActPendientes.Image = ((System.Drawing.Image)(resources.GetObject("btnActPendientes.Image")));
            this.btnActPendientes.Location = new System.Drawing.Point(741, 12);
            this.btnActPendientes.Name = "btnActPendientes";
            this.btnActPendientes.Size = new System.Drawing.Size(40, 40);
            this.btnActPendientes.TabIndex = 17;
            this.btnActPendientes.UseVisualStyleBackColor = true;
            this.btnActPendientes.Click += new System.EventHandler(this.btnActPendientes_Click);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 434);
            this.Controls.Add(this.btnActPendientes);
            this.Controls.Add(this.gcArticulos);
            this.Controls.Add(this.btnDescargarInfo);
            this.Controls.Add(this.btnVerifPendiente);
            this.Controls.Add(this.lbLeyenda);
            this.Controls.Add(this.lbPendientes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Descarga Información de Precios";
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcArticulos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.articulosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbLeyenda;
        private System.Windows.Forms.Button btnVerifPendiente;
        private System.Windows.Forms.Button btnDescargarInfo;
        private DevExpress.XtraGrid.GridControl gcArticulos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label lbPendientes;
        private System.Windows.Forms.BindingSource articulosBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colidArticulo;
        private DevExpress.XtraGrid.Columns.GridColumn colclave;
        private DevExpress.XtraGrid.Columns.GridColumn colarticulo;
        private DevExpress.XtraGrid.Columns.GridColumn colprecLista;
        private DevExpress.XtraGrid.Columns.GridColumn colprecMinimo;
        private DevExpress.XtraGrid.Columns.GridColumn colprecMayoreo;
        private DevExpress.XtraGrid.Columns.GridColumn colprecFilial;
        private DevExpress.XtraGrid.Columns.GridColumn colprecIMSS;
        private DevExpress.XtraGrid.Columns.GridColumn colmedioMayoreo;
        private System.Windows.Forms.Button btnActPendientes;
    }
}

