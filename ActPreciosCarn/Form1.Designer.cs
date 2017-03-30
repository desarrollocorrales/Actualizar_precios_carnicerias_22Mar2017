namespace ActPreciosCarn
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gcPreciosArt = new DevExpress.XtraGrid.GridControl();
            this.articulosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colseleccionado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colidArticulo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colclave = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colarticulo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprecLista = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprecMinimo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprecMayoreo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprecFilial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprecIMSS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colmedioMayoreo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tbBusqueda = new System.Windows.Forms.TextBox();
            this.btnCargaArti = new System.Windows.Forms.Button();
            this.gcArticActualizar = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colseleccionado1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colidArticulo1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colclave1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colarticulo1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprecLista1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprecMinimo1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprecMayoreo1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprecFilial1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprecIMSS1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colmedioMayoreo1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAgregarArt = new System.Windows.Forms.Button();
            this.btnQuitarArt = new System.Windows.Forms.Button();
            this.btnQuitarTodos = new System.Windows.Forms.Button();
            this.btnGuarda = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.altaUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambioDeClaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.salirToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descargaInformaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitacoraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gcPreciosArt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.articulosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcArticActualizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F);
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Actualizar Precios Sucursales";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label2.Location = new System.Drawing.Point(158, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Búsqueda";
            // 
            // gcPreciosArt
            // 
            this.gcPreciosArt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gcPreciosArt.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcPreciosArt.DataSource = this.articulosBindingSource;
            this.gcPreciosArt.Location = new System.Drawing.Point(12, 131);
            this.gcPreciosArt.MainView = this.gridView1;
            this.gcPreciosArt.Name = "gcPreciosArt";
            this.gcPreciosArt.Size = new System.Drawing.Size(633, 474);
            this.gcPreciosArt.TabIndex = 3;
            this.gcPreciosArt.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // articulosBindingSource
            // 
            this.articulosBindingSource.DataSource = typeof(ActPreciosCarn.Modelos.Articulos);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colseleccionado,
            this.colidArticulo,
            this.colclave,
            this.colarticulo,
            this.colprecLista,
            this.colprecMinimo,
            this.colprecMayoreo,
            this.colprecFilial,
            this.colprecIMSS,
            this.colmedioMayoreo});
            this.gridView1.GridControl = this.gcPreciosArt;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowViewCaption = true;
            this.gridView1.ViewCaption = "Precios Artículos";
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            // 
            // colseleccionado
            // 
            this.colseleccionado.Caption = " ";
            this.colseleccionado.FieldName = "seleccionado";
            this.colseleccionado.Name = "colseleccionado";
            this.colseleccionado.Visible = true;
            this.colseleccionado.VisibleIndex = 0;
            this.colseleccionado.Width = 30;
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
            this.colclave.OptionsColumn.AllowEdit = false;
            this.colclave.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colclave.Visible = true;
            this.colclave.VisibleIndex = 1;
            // 
            // colarticulo
            // 
            this.colarticulo.Caption = "Artículo";
            this.colarticulo.FieldName = "articulo";
            this.colarticulo.Name = "colarticulo";
            this.colarticulo.OptionsColumn.AllowEdit = false;
            this.colarticulo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colarticulo.Visible = true;
            this.colarticulo.VisibleIndex = 2;
            // 
            // colprecLista
            // 
            this.colprecLista.Caption = "Precio Lista";
            this.colprecLista.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colprecLista.FieldName = "precLista";
            this.colprecLista.Name = "colprecLista";
            this.colprecLista.OptionsColumn.AllowEdit = false;
            this.colprecLista.Visible = true;
            this.colprecLista.VisibleIndex = 3;
            // 
            // colprecMinimo
            // 
            this.colprecMinimo.Caption = "Precio Mínimo";
            this.colprecMinimo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colprecMinimo.FieldName = "precMinimo";
            this.colprecMinimo.Name = "colprecMinimo";
            this.colprecMinimo.OptionsColumn.AllowEdit = false;
            this.colprecMinimo.Visible = true;
            this.colprecMinimo.VisibleIndex = 4;
            // 
            // colprecMayoreo
            // 
            this.colprecMayoreo.Caption = "Precio Mayoreo";
            this.colprecMayoreo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colprecMayoreo.FieldName = "precMayoreo";
            this.colprecMayoreo.Name = "colprecMayoreo";
            this.colprecMayoreo.OptionsColumn.AllowEdit = false;
            this.colprecMayoreo.Visible = true;
            this.colprecMayoreo.VisibleIndex = 5;
            // 
            // colprecFilial
            // 
            this.colprecFilial.Caption = "Precio Filial";
            this.colprecFilial.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colprecFilial.FieldName = "precFilial";
            this.colprecFilial.Name = "colprecFilial";
            this.colprecFilial.OptionsColumn.AllowEdit = false;
            this.colprecFilial.Visible = true;
            this.colprecFilial.VisibleIndex = 6;
            // 
            // colprecIMSS
            // 
            this.colprecIMSS.Caption = "Precio IMSS";
            this.colprecIMSS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colprecIMSS.FieldName = "precIMSS";
            this.colprecIMSS.Name = "colprecIMSS";
            this.colprecIMSS.OptionsColumn.AllowEdit = false;
            this.colprecIMSS.Visible = true;
            this.colprecIMSS.VisibleIndex = 7;
            // 
            // colmedioMayoreo
            // 
            this.colmedioMayoreo.Caption = "Medio Mayoreo";
            this.colmedioMayoreo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colmedioMayoreo.FieldName = "medioMayoreo";
            this.colmedioMayoreo.Name = "colmedioMayoreo";
            this.colmedioMayoreo.OptionsColumn.AllowEdit = false;
            this.colmedioMayoreo.Visible = true;
            this.colmedioMayoreo.VisibleIndex = 8;
            // 
            // tbBusqueda
            // 
            this.tbBusqueda.Font = new System.Drawing.Font("Tahoma", 11F);
            this.tbBusqueda.Location = new System.Drawing.Point(236, 96);
            this.tbBusqueda.Name = "tbBusqueda";
            this.tbBusqueda.Size = new System.Drawing.Size(357, 25);
            this.tbBusqueda.TabIndex = 4;
            this.tbBusqueda.TextChanged += new System.EventHandler(this.tbBusqueda_TextChanged);
            // 
            // btnCargaArti
            // 
            this.btnCargaArti.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.btnCargaArti.Location = new System.Drawing.Point(12, 92);
            this.btnCargaArti.Name = "btnCargaArti";
            this.btnCargaArti.Size = new System.Drawing.Size(135, 33);
            this.btnCargaArti.TabIndex = 5;
            this.btnCargaArti.Text = "Carga Artículos";
            this.btnCargaArti.UseVisualStyleBackColor = true;
            this.btnCargaArti.Click += new System.EventHandler(this.btnCargaArti_Click);
            // 
            // gcArticActualizar
            // 
            this.gcArticActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gcArticActualizar.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcArticActualizar.DataSource = this.articulosBindingSource;
            this.gcArticActualizar.Location = new System.Drawing.Point(727, 131);
            this.gcArticActualizar.MainView = this.gridView2;
            this.gcArticActualizar.MinimumSize = new System.Drawing.Size(633, 365);
            this.gcArticActualizar.Name = "gcArticActualizar";
            this.gcArticActualizar.Size = new System.Drawing.Size(637, 474);
            this.gcArticActualizar.TabIndex = 6;
            this.gcArticActualizar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colseleccionado1,
            this.colidArticulo1,
            this.colclave1,
            this.colarticulo1,
            this.colprecLista1,
            this.colprecMinimo1,
            this.colprecMayoreo1,
            this.colprecFilial1,
            this.colprecIMSS1,
            this.colmedioMayoreo1});
            this.gridView2.GridControl = this.gcArticActualizar;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.MultiSelect = true;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowViewCaption = true;
            this.gridView2.ViewCaption = "Artículos a Actualizar";
            this.gridView2.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView2_RowCellStyle);
            // 
            // colseleccionado1
            // 
            this.colseleccionado1.Caption = " ";
            this.colseleccionado1.FieldName = "seleccionado";
            this.colseleccionado1.Name = "colseleccionado1";
            this.colseleccionado1.Visible = true;
            this.colseleccionado1.VisibleIndex = 0;
            this.colseleccionado1.Width = 30;
            // 
            // colidArticulo1
            // 
            this.colidArticulo1.FieldName = "idArticulo";
            this.colidArticulo1.Name = "colidArticulo1";
            // 
            // colclave1
            // 
            this.colclave1.Caption = "Clave";
            this.colclave1.FieldName = "clave";
            this.colclave1.Name = "colclave1";
            this.colclave1.OptionsColumn.AllowEdit = false;
            this.colclave1.Visible = true;
            this.colclave1.VisibleIndex = 1;
            // 
            // colarticulo1
            // 
            this.colarticulo1.Caption = "Artículo";
            this.colarticulo1.FieldName = "articulo";
            this.colarticulo1.Name = "colarticulo1";
            this.colarticulo1.OptionsColumn.AllowEdit = false;
            this.colarticulo1.Visible = true;
            this.colarticulo1.VisibleIndex = 2;
            // 
            // colprecLista1
            // 
            this.colprecLista1.Caption = "Precio Lista";
            this.colprecLista1.FieldName = "precLista";
            this.colprecLista1.Name = "colprecLista1";
            this.colprecLista1.Visible = true;
            this.colprecLista1.VisibleIndex = 3;
            // 
            // colprecMinimo1
            // 
            this.colprecMinimo1.Caption = "Precio Mínimo";
            this.colprecMinimo1.FieldName = "precMinimo";
            this.colprecMinimo1.Name = "colprecMinimo1";
            this.colprecMinimo1.Visible = true;
            this.colprecMinimo1.VisibleIndex = 4;
            // 
            // colprecMayoreo1
            // 
            this.colprecMayoreo1.Caption = "Precio Mayoreo";
            this.colprecMayoreo1.FieldName = "precMayoreo";
            this.colprecMayoreo1.Name = "colprecMayoreo1";
            this.colprecMayoreo1.Visible = true;
            this.colprecMayoreo1.VisibleIndex = 5;
            // 
            // colprecFilial1
            // 
            this.colprecFilial1.Caption = "Precio Filial";
            this.colprecFilial1.FieldName = "precFilial";
            this.colprecFilial1.Name = "colprecFilial1";
            this.colprecFilial1.Visible = true;
            this.colprecFilial1.VisibleIndex = 6;
            // 
            // colprecIMSS1
            // 
            this.colprecIMSS1.Caption = "Precio IMSS";
            this.colprecIMSS1.FieldName = "precIMSS";
            this.colprecIMSS1.Name = "colprecIMSS1";
            this.colprecIMSS1.Visible = true;
            this.colprecIMSS1.VisibleIndex = 7;
            // 
            // colmedioMayoreo1
            // 
            this.colmedioMayoreo1.Caption = "Medio Mayoreo";
            this.colmedioMayoreo1.FieldName = "medioMayoreo";
            this.colmedioMayoreo1.Name = "colmedioMayoreo1";
            this.colmedioMayoreo1.Visible = true;
            this.colmedioMayoreo1.VisibleIndex = 8;
            // 
            // btnAgregarArt
            // 
            this.btnAgregarArt.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarArt.Image")));
            this.btnAgregarArt.Location = new System.Drawing.Point(656, 236);
            this.btnAgregarArt.Name = "btnAgregarArt";
            this.btnAgregarArt.Size = new System.Drawing.Size(60, 60);
            this.btnAgregarArt.TabIndex = 7;
            this.btnAgregarArt.UseVisualStyleBackColor = true;
            this.btnAgregarArt.Click += new System.EventHandler(this.btnAgregarArt_Click);
            // 
            // btnQuitarArt
            // 
            this.btnQuitarArt.Image = ((System.Drawing.Image)(resources.GetObject("btnQuitarArt.Image")));
            this.btnQuitarArt.Location = new System.Drawing.Point(656, 308);
            this.btnQuitarArt.Name = "btnQuitarArt";
            this.btnQuitarArt.Size = new System.Drawing.Size(60, 60);
            this.btnQuitarArt.TabIndex = 8;
            this.btnQuitarArt.UseVisualStyleBackColor = true;
            this.btnQuitarArt.Click += new System.EventHandler(this.btnQuitarArt_Click);
            // 
            // btnQuitarTodos
            // 
            this.btnQuitarTodos.Image = ((System.Drawing.Image)(resources.GetObject("btnQuitarTodos.Image")));
            this.btnQuitarTodos.Location = new System.Drawing.Point(656, 380);
            this.btnQuitarTodos.Name = "btnQuitarTodos";
            this.btnQuitarTodos.Size = new System.Drawing.Size(60, 60);
            this.btnQuitarTodos.TabIndex = 9;
            this.btnQuitarTodos.UseVisualStyleBackColor = true;
            this.btnQuitarTodos.Click += new System.EventHandler(this.btnQuitarTodos_Click);
            // 
            // btnGuarda
            // 
            this.btnGuarda.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.btnGuarda.Location = new System.Drawing.Point(1226, 88);
            this.btnGuarda.Name = "btnGuarda";
            this.btnGuarda.Size = new System.Drawing.Size(135, 33);
            this.btnGuarda.TabIndex = 10;
            this.btnGuarda.Text = "Guardar Cambios";
            this.btnGuarda.UseVisualStyleBackColor = true;
            this.btnGuarda.Click += new System.EventHandler(this.btnGuarda_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Silver;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.reportesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1376, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.altaUsuarioToolStripMenuItem,
            this.cambioDeClaveToolStripMenuItem,
            this.salirToolStripMenuItem,
            this.salirToolStripMenuItem1});
            this.archivoToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // altaUsuarioToolStripMenuItem
            // 
            this.altaUsuarioToolStripMenuItem.Name = "altaUsuarioToolStripMenuItem";
            this.altaUsuarioToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.altaUsuarioToolStripMenuItem.Text = "Alta Usuario";
            this.altaUsuarioToolStripMenuItem.Click += new System.EventHandler(this.altaUsuarioToolStripMenuItem_Click);
            // 
            // cambioDeClaveToolStripMenuItem
            // 
            this.cambioDeClaveToolStripMenuItem.Name = "cambioDeClaveToolStripMenuItem";
            this.cambioDeClaveToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.cambioDeClaveToolStripMenuItem.Text = "Cambio de Clave";
            this.cambioDeClaveToolStripMenuItem.Click += new System.EventHandler(this.cambioDeClaveToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(161, 6);
            // 
            // salirToolStripMenuItem1
            // 
            this.salirToolStripMenuItem1.Name = "salirToolStripMenuItem1";
            this.salirToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.salirToolStripMenuItem1.Text = "Salir";
            this.salirToolStripMenuItem1.Click += new System.EventHandler(this.salirToolStripMenuItem1_Click);
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.descargaInformaciónToolStripMenuItem,
            this.bitacoraToolStripMenuItem});
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.reportesToolStripMenuItem.Text = "Reportes";
            // 
            // descargaInformaciónToolStripMenuItem
            // 
            this.descargaInformaciónToolStripMenuItem.Name = "descargaInformaciónToolStripMenuItem";
            this.descargaInformaciónToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.descargaInformaciónToolStripMenuItem.Text = "Descarga Información";
            this.descargaInformaciónToolStripMenuItem.Click += new System.EventHandler(this.descargaInformaciónToolStripMenuItem_Click);
            // 
            // bitacoraToolStripMenuItem
            // 
            this.bitacoraToolStripMenuItem.Name = "bitacoraToolStripMenuItem";
            this.bitacoraToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.bitacoraToolStripMenuItem.Text = "Bitácora";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1376, 617);
            this.Controls.Add(this.btnGuarda);
            this.Controls.Add(this.btnQuitarTodos);
            this.Controls.Add(this.btnQuitarArt);
            this.Controls.Add(this.btnAgregarArt);
            this.Controls.Add(this.gcArticActualizar);
            this.Controls.Add(this.gcPreciosArt);
            this.Controls.Add(this.btnCargaArti);
            this.Controls.Add(this.tbBusqueda);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actualizar Precios Sucursales";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcPreciosArt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.articulosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcArticActualizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.GridControl gcPreciosArt;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.TextBox tbBusqueda;
        private System.Windows.Forms.Button btnCargaArti;
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
        private DevExpress.XtraGrid.GridControl gcArticActualizar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colidArticulo1;
        private DevExpress.XtraGrid.Columns.GridColumn colclave1;
        private DevExpress.XtraGrid.Columns.GridColumn colarticulo1;
        private DevExpress.XtraGrid.Columns.GridColumn colprecLista1;
        private DevExpress.XtraGrid.Columns.GridColumn colprecMinimo1;
        private DevExpress.XtraGrid.Columns.GridColumn colprecMayoreo1;
        private DevExpress.XtraGrid.Columns.GridColumn colprecFilial1;
        private DevExpress.XtraGrid.Columns.GridColumn colprecIMSS1;
        private DevExpress.XtraGrid.Columns.GridColumn colmedioMayoreo1;
        private System.Windows.Forms.Button btnAgregarArt;
        private System.Windows.Forms.Button btnQuitarArt;
        private System.Windows.Forms.Button btnQuitarTodos;
        private DevExpress.XtraGrid.Columns.GridColumn colseleccionado;
        private DevExpress.XtraGrid.Columns.GridColumn colseleccionado1;
        private System.Windows.Forms.Button btnGuarda;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem altaUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambioDeClaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem descargaInformaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitacoraToolStripMenuItem;

    }
}

