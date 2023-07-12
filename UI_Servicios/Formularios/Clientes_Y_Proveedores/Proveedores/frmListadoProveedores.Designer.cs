namespace UI_Servicios.Formularios.Clientes_Y_Proveedores.Proveedores
{
    partial class frmListadoProveedores
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListadoProveedores));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.btnActivar = new DevExpress.XtraBars.BarButtonItem();
            this.btnInactivar = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminar = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportarExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnImprimir = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.btnFichaProveedor = new DevExpress.XtraBars.BarButtonItem();
            this.pageAccGenerales = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.grupoEdicion = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grupoReportes = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grupoAcciones = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.picTitulo = new DevExpress.XtraEditors.PictureEdit();
            this.lblTitulo = new DevExpress.XtraEditors.LabelControl();
            this.gcListaProveedores = new DevExpress.XtraGrid.GridControl();
            this.bsListaProveedores = new System.Windows.Forms.BindingSource(this.components);
            this.gvListaProveedores = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCantCriterios = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_proveedor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_proveedor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_razon_comercial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_tipo_documento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_documento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_direccion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_distrito = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_provincia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_departamento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_pais = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_modalidad_pago = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_activo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_mail_1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_mail_2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_fono_1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_juridico = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_agente_retencion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_agente_percepcion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_buen_contribuyente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_auto_detraccion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_afecto_cuarta = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_domiciliado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_no_habido = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_proveedor_ERP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_representante_legal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_registro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario_registro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_cambio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario_cambio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_empresas_vinculadas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rmmTexto = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.coldsc_servicios_vinculadas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colObservaciones = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colvalorRating = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rRatingCalificacion = new DevExpress.XtraEditors.Repository.RepositoryItemRatingControl();
            this.coldsc_formapago = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_banco = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_tipo_cuenta = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_cta_bancaria = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_cta_interbancaria = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_moneda = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_contactos = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_correos_contactos = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_telefonos_contactos = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_empresa_contactos = new DevExpress.XtraGrid.Columns.GridColumn();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.coldsc_frecuencia = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTitulo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaProveedores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListaProveedores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaProveedores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmmTexto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rRatingCalificacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.btnNuevo,
            this.btnActivar,
            this.btnInactivar,
            this.btnEliminar,
            this.btnExportarExcel,
            this.btnImprimir,
            this.barStaticItem1,
            this.btnFichaProveedor});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 9;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.pageAccGenerales});
            this.ribbon.Size = new System.Drawing.Size(1231, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Caption = "Nuevo";
            this.btnNuevo.Id = 1;
            this.btnNuevo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.ImageOptions.Image")));
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnNuevo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNuevo_ItemClick);
            // 
            // btnActivar
            // 
            this.btnActivar.Caption = "Activar";
            this.btnActivar.Id = 2;
            this.btnActivar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnActivar.ImageOptions.Image")));
            this.btnActivar.Name = "btnActivar";
            this.btnActivar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnActivar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnActivar_ItemClick);
            // 
            // btnInactivar
            // 
            this.btnInactivar.Caption = "Inactivar";
            this.btnInactivar.Id = 3;
            this.btnInactivar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnInactivar.ImageOptions.Image")));
            this.btnInactivar.Name = "btnInactivar";
            this.btnInactivar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnInactivar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnInactivar_ItemClick);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Caption = "Eliminar";
            this.btnEliminar.Id = 4;
            this.btnEliminar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.ImageOptions.Image")));
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnEliminar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEliminar_ItemClick);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Caption = "Exportar a Excel";
            this.btnExportarExcel.Id = 5;
            this.btnExportarExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.ImageOptions.Image")));
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnExportarExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportarExcel_ItemClick);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Caption = "Imprimir";
            this.btnImprimir.Id = 6;
            this.btnImprimir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.ImageOptions.Image")));
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnImprimir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImprimir_ItemClick);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Presione F5 para actualizar listado";
            this.barStaticItem1.Id = 7;
            this.barStaticItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.barStaticItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // btnFichaProveedor
            // 
            this.btnFichaProveedor.Caption = "Ficha del proveedor";
            this.btnFichaProveedor.Id = 8;
            this.btnFichaProveedor.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFichaProveedor.ImageOptions.Image")));
            this.btnFichaProveedor.Name = "btnFichaProveedor";
            this.btnFichaProveedor.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnFichaProveedor.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnFichaProveedor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFichaProveedor_ItemClick);
            // 
            // pageAccGenerales
            // 
            this.pageAccGenerales.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grupoEdicion,
            this.grupoReportes,
            this.grupoAcciones});
            this.pageAccGenerales.Name = "pageAccGenerales";
            this.pageAccGenerales.Text = "Opciones de Proveedores";
            // 
            // grupoEdicion
            // 
            this.grupoEdicion.ItemLinks.Add(this.btnNuevo);
            this.grupoEdicion.ItemLinks.Add(this.btnActivar);
            this.grupoEdicion.ItemLinks.Add(this.btnInactivar);
            this.grupoEdicion.ItemLinks.Add(this.btnEliminar);
            this.grupoEdicion.Name = "grupoEdicion";
            this.grupoEdicion.Text = "Edición";
            // 
            // grupoReportes
            // 
            this.grupoReportes.ItemLinks.Add(this.btnExportarExcel);
            this.grupoReportes.ItemLinks.Add(this.btnImprimir);
            this.grupoReportes.Name = "grupoReportes";
            this.grupoReportes.Text = "Reportes";
            // 
            // grupoAcciones
            // 
            this.grupoAcciones.ItemLinks.Add(this.btnFichaProveedor);
            this.grupoAcciones.Name = "grupoAcciones";
            this.grupoAcciones.Text = "Acciones";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItem1);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 659);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1231, 24);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.picTitulo);
            this.layoutControl1.Controls.Add(this.lblTitulo);
            this.layoutControl1.Controls.Add(this.gcListaProveedores);
            this.layoutControl1.Controls.Add(this.navBarControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 158);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1231, 501);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // picTitulo
            // 
            this.picTitulo.Location = new System.Drawing.Point(66, 7);
            this.picTitulo.MenuManager = this.ribbon;
            this.picTitulo.Name = "picTitulo";
            this.picTitulo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picTitulo.Properties.Appearance.Options.UseBackColor = true;
            this.picTitulo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picTitulo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picTitulo.Size = new System.Drawing.Size(59, 31);
            this.picTitulo.StyleController = this.layoutControl1;
            this.picTitulo.TabIndex = 1;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(49)))), ((int)(((byte)(35)))));
            this.lblTitulo.Appearance.Options.UseFont = true;
            this.lblTitulo.Appearance.Options.UseForeColor = true;
            this.lblTitulo.Location = new System.Drawing.Point(129, 7);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1095, 31);
            this.lblTitulo.StyleController = this.layoutControl1;
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "<<Titulo de grupo>>";
            // 
            // gcListaProveedores
            // 
            this.gcListaProveedores.DataSource = this.bsListaProveedores;
            this.gcListaProveedores.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListaProveedores.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListaProveedores.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListaProveedores.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListaProveedores.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListaProveedores.Location = new System.Drawing.Point(66, 42);
            this.gcListaProveedores.MainView = this.gvListaProveedores;
            this.gcListaProveedores.MenuManager = this.ribbon;
            this.gcListaProveedores.Name = "gcListaProveedores";
            this.gcListaProveedores.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rmmTexto,
            this.rRatingCalificacion});
            this.gcListaProveedores.Size = new System.Drawing.Size(1158, 452);
            this.gcListaProveedores.TabIndex = 0;
            this.gcListaProveedores.UseEmbeddedNavigator = true;
            this.gcListaProveedores.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListaProveedores});
            // 
            // bsListaProveedores
            // 
            this.bsListaProveedores.DataSource = typeof(BE_Servicios.eProveedor);
            // 
            // gvListaProveedores
            // 
            this.gvListaProveedores.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gvListaProveedores.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvListaProveedores.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaProveedores.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvListaProveedores.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListaProveedores.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListaProveedores.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListaProveedores.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListaProveedores.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaProveedores.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvListaProveedores.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaProveedores.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvListaProveedores.ColumnPanelRowHeight = 35;
            this.gvListaProveedores.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCantCriterios,
            this.colcod_proveedor,
            this.coldsc_proveedor,
            this.coldsc_razon_comercial,
            this.coldsc_tipo_documento,
            this.colnum_documento,
            this.coldsc_direccion,
            this.coldsc_distrito,
            this.coldsc_provincia,
            this.coldsc_departamento,
            this.coldsc_pais,
            this.coldsc_modalidad_pago,
            this.colflg_activo,
            this.coldsc_mail_1,
            this.coldsc_mail_2,
            this.coldsc_fono_1,
            this.colflg_juridico,
            this.colflg_agente_retencion,
            this.colflg_agente_percepcion,
            this.colflg_buen_contribuyente,
            this.colflg_auto_detraccion,
            this.colflg_afecto_cuarta,
            this.colflg_domiciliado,
            this.colflg_no_habido,
            this.colcod_proveedor_ERP,
            this.coldsc_representante_legal,
            this.colfch_registro,
            this.coldsc_usuario_registro,
            this.colfch_cambio,
            this.coldsc_usuario_cambio,
            this.coldsc_empresas_vinculadas,
            this.coldsc_servicios_vinculadas,
            this.colObservaciones,
            this.colvalorRating,
            this.coldsc_formapago,
            this.coldsc_banco,
            this.coldsc_tipo_cuenta,
            this.coldsc_cta_bancaria,
            this.coldsc_cta_interbancaria,
            this.colcod_moneda,
            this.coldsc_contactos,
            this.coldsc_correos_contactos,
            this.coldsc_telefonos_contactos,
            this.coldsc_empresa_contactos,
            this.coldsc_frecuencia});
            this.gvListaProveedores.GridControl = this.gcListaProveedores;
            this.gvListaProveedores.Name = "gvListaProveedores";
            this.gvListaProveedores.OptionsBehavior.Editable = false;
            this.gvListaProveedores.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gvListaProveedores.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListaProveedores.OptionsView.RowAutoHeight = true;
            this.gvListaProveedores.OptionsView.ShowAutoFilterRow = true;
            this.gvListaProveedores.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListaProveedores_RowClick);
            this.gvListaProveedores.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvListaProveedores_RowCellClick);
            this.gvListaProveedores.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListaProveedores_CustomDrawColumnHeader);
            this.gvListaProveedores.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvListaProveedores_CustomDrawCell);
            this.gvListaProveedores.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvListaProveedores_RowCellStyle);
            this.gvListaProveedores.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListaProveedores_RowStyle);
            // 
            // colCantCriterios
            // 
            this.colCantCriterios.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 2F);
            this.colCantCriterios.AppearanceCell.ForeColor = System.Drawing.Color.Transparent;
            this.colCantCriterios.AppearanceCell.Options.UseFont = true;
            this.colCantCriterios.AppearanceCell.Options.UseForeColor = true;
            this.colCantCriterios.AppearanceCell.Options.UseTextOptions = true;
            this.colCantCriterios.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCantCriterios.Caption = " ";
            this.colCantCriterios.FieldName = "CantCriterios";
            this.colCantCriterios.Name = "colCantCriterios";
            this.colCantCriterios.OptionsColumn.AllowEdit = false;
            this.colCantCriterios.OptionsColumn.AllowSize = false;
            this.colCantCriterios.OptionsColumn.FixedWidth = true;
            this.colCantCriterios.Visible = true;
            this.colCantCriterios.VisibleIndex = 0;
            this.colCantCriterios.Width = 30;
            // 
            // colcod_proveedor
            // 
            this.colcod_proveedor.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_proveedor.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_proveedor.Caption = "Código";
            this.colcod_proveedor.FieldName = "cod_proveedor";
            this.colcod_proveedor.Name = "colcod_proveedor";
            this.colcod_proveedor.OptionsColumn.FixedWidth = true;
            this.colcod_proveedor.Visible = true;
            this.colcod_proveedor.VisibleIndex = 1;
            this.colcod_proveedor.Width = 120;
            // 
            // coldsc_proveedor
            // 
            this.coldsc_proveedor.Caption = "Razón Social / Apellidos y Nombres";
            this.coldsc_proveedor.FieldName = "dsc_proveedor";
            this.coldsc_proveedor.Name = "coldsc_proveedor";
            this.coldsc_proveedor.OptionsColumn.FixedWidth = true;
            this.coldsc_proveedor.Visible = true;
            this.coldsc_proveedor.VisibleIndex = 2;
            this.coldsc_proveedor.Width = 300;
            // 
            // coldsc_razon_comercial
            // 
            this.coldsc_razon_comercial.Caption = "Nombre Comercial";
            this.coldsc_razon_comercial.FieldName = "dsc_razon_comercial";
            this.coldsc_razon_comercial.Name = "coldsc_razon_comercial";
            this.coldsc_razon_comercial.OptionsColumn.FixedWidth = true;
            this.coldsc_razon_comercial.Visible = true;
            this.coldsc_razon_comercial.VisibleIndex = 3;
            this.coldsc_razon_comercial.Width = 250;
            // 
            // coldsc_tipo_documento
            // 
            this.coldsc_tipo_documento.Caption = "Tipo documento";
            this.coldsc_tipo_documento.FieldName = "dsc_tipo_documento";
            this.coldsc_tipo_documento.Name = "coldsc_tipo_documento";
            this.coldsc_tipo_documento.OptionsColumn.FixedWidth = true;
            this.coldsc_tipo_documento.Visible = true;
            this.coldsc_tipo_documento.VisibleIndex = 4;
            this.coldsc_tipo_documento.Width = 80;
            // 
            // colnum_documento
            // 
            this.colnum_documento.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_documento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_documento.Caption = "N° documento";
            this.colnum_documento.FieldName = "num_documento";
            this.colnum_documento.Name = "colnum_documento";
            this.colnum_documento.OptionsColumn.FixedWidth = true;
            this.colnum_documento.Visible = true;
            this.colnum_documento.VisibleIndex = 5;
            this.colnum_documento.Width = 90;
            // 
            // coldsc_direccion
            // 
            this.coldsc_direccion.Caption = "Dirección";
            this.coldsc_direccion.FieldName = "dsc_direccion";
            this.coldsc_direccion.Name = "coldsc_direccion";
            this.coldsc_direccion.OptionsColumn.FixedWidth = true;
            this.coldsc_direccion.Visible = true;
            this.coldsc_direccion.VisibleIndex = 6;
            this.coldsc_direccion.Width = 200;
            // 
            // coldsc_distrito
            // 
            this.coldsc_distrito.Caption = "Distrito";
            this.coldsc_distrito.FieldName = "dsc_distrito";
            this.coldsc_distrito.Name = "coldsc_distrito";
            this.coldsc_distrito.OptionsColumn.FixedWidth = true;
            this.coldsc_distrito.Visible = true;
            this.coldsc_distrito.VisibleIndex = 7;
            this.coldsc_distrito.Width = 100;
            // 
            // coldsc_provincia
            // 
            this.coldsc_provincia.Caption = "Provincia";
            this.coldsc_provincia.FieldName = "dsc_provincia";
            this.coldsc_provincia.Name = "coldsc_provincia";
            this.coldsc_provincia.OptionsColumn.FixedWidth = true;
            this.coldsc_provincia.Visible = true;
            this.coldsc_provincia.VisibleIndex = 8;
            this.coldsc_provincia.Width = 100;
            // 
            // coldsc_departamento
            // 
            this.coldsc_departamento.Caption = "Departamento";
            this.coldsc_departamento.FieldName = "dsc_departamento";
            this.coldsc_departamento.Name = "coldsc_departamento";
            this.coldsc_departamento.OptionsColumn.FixedWidth = true;
            this.coldsc_departamento.Visible = true;
            this.coldsc_departamento.VisibleIndex = 9;
            this.coldsc_departamento.Width = 100;
            // 
            // coldsc_pais
            // 
            this.coldsc_pais.Caption = "País";
            this.coldsc_pais.FieldName = "dsc_pais";
            this.coldsc_pais.Name = "coldsc_pais";
            this.coldsc_pais.OptionsColumn.FixedWidth = true;
            this.coldsc_pais.Visible = true;
            this.coldsc_pais.VisibleIndex = 10;
            this.coldsc_pais.Width = 100;
            // 
            // coldsc_modalidad_pago
            // 
            this.coldsc_modalidad_pago.Caption = "Modalidad pago";
            this.coldsc_modalidad_pago.FieldName = "dsc_modalidad_pago";
            this.coldsc_modalidad_pago.Name = "coldsc_modalidad_pago";
            this.coldsc_modalidad_pago.OptionsColumn.FixedWidth = true;
            this.coldsc_modalidad_pago.Visible = true;
            this.coldsc_modalidad_pago.VisibleIndex = 11;
            this.coldsc_modalidad_pago.Width = 100;
            // 
            // colflg_activo
            // 
            this.colflg_activo.Caption = "Activo";
            this.colflg_activo.FieldName = "flg_activo";
            this.colflg_activo.Name = "colflg_activo";
            this.colflg_activo.OptionsColumn.FixedWidth = true;
            // 
            // coldsc_mail_1
            // 
            this.coldsc_mail_1.Caption = "Email 1";
            this.coldsc_mail_1.FieldName = "dsc_mail_1";
            this.coldsc_mail_1.Name = "coldsc_mail_1";
            this.coldsc_mail_1.OptionsColumn.FixedWidth = true;
            // 
            // coldsc_mail_2
            // 
            this.coldsc_mail_2.Caption = "Email 2";
            this.coldsc_mail_2.FieldName = "dsc_mail_2";
            this.coldsc_mail_2.Name = "coldsc_mail_2";
            this.coldsc_mail_2.OptionsColumn.FixedWidth = true;
            // 
            // coldsc_fono_1
            // 
            this.coldsc_fono_1.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_fono_1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_fono_1.Caption = "Fono 1";
            this.coldsc_fono_1.FieldName = "dsc_fono_1";
            this.coldsc_fono_1.Name = "coldsc_fono_1";
            this.coldsc_fono_1.OptionsColumn.FixedWidth = true;
            // 
            // colflg_juridico
            // 
            this.colflg_juridico.Caption = "Es Jurid.";
            this.colflg_juridico.FieldName = "flg_juridico";
            this.colflg_juridico.Name = "colflg_juridico";
            this.colflg_juridico.OptionsColumn.FixedWidth = true;
            this.colflg_juridico.ToolTip = "Es persona jurídica";
            // 
            // colflg_agente_retencion
            // 
            this.colflg_agente_retencion.Caption = "Agente retención";
            this.colflg_agente_retencion.FieldName = "flg_agente_retencion";
            this.colflg_agente_retencion.Name = "colflg_agente_retencion";
            this.colflg_agente_retencion.OptionsColumn.FixedWidth = true;
            // 
            // colflg_agente_percepcion
            // 
            this.colflg_agente_percepcion.Caption = "Agente percepción";
            this.colflg_agente_percepcion.FieldName = "flg_agente_percepcion";
            this.colflg_agente_percepcion.Name = "colflg_agente_percepcion";
            this.colflg_agente_percepcion.OptionsColumn.FixedWidth = true;
            // 
            // colflg_buen_contribuyente
            // 
            this.colflg_buen_contribuyente.Caption = "Buen contribuyente";
            this.colflg_buen_contribuyente.FieldName = "flg_buen_contribuyente";
            this.colflg_buen_contribuyente.Name = "colflg_buen_contribuyente";
            this.colflg_buen_contribuyente.OptionsColumn.FixedWidth = true;
            // 
            // colflg_auto_detraccion
            // 
            this.colflg_auto_detraccion.Caption = "Auto detracción";
            this.colflg_auto_detraccion.FieldName = "flg_auto_detraccion";
            this.colflg_auto_detraccion.Name = "colflg_auto_detraccion";
            this.colflg_auto_detraccion.OptionsColumn.FixedWidth = true;
            // 
            // colflg_afecto_cuarta
            // 
            this.colflg_afecto_cuarta.Caption = "Afecto cuarta";
            this.colflg_afecto_cuarta.FieldName = "flg_afecto_cuarta";
            this.colflg_afecto_cuarta.Name = "colflg_afecto_cuarta";
            this.colflg_afecto_cuarta.OptionsColumn.FixedWidth = true;
            // 
            // colflg_domiciliado
            // 
            this.colflg_domiciliado.Caption = "No domiciliado";
            this.colflg_domiciliado.FieldName = "flg_domiciliado";
            this.colflg_domiciliado.Name = "colflg_domiciliado";
            this.colflg_domiciliado.OptionsColumn.FixedWidth = true;
            // 
            // colflg_no_habido
            // 
            this.colflg_no_habido.Caption = "No habido";
            this.colflg_no_habido.FieldName = "flg_no_habido";
            this.colflg_no_habido.Name = "colflg_no_habido";
            this.colflg_no_habido.OptionsColumn.FixedWidth = true;
            // 
            // colcod_proveedor_ERP
            // 
            this.colcod_proveedor_ERP.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_proveedor_ERP.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_proveedor_ERP.Caption = "Código ERP";
            this.colcod_proveedor_ERP.FieldName = "cod_proveedor_ERP";
            this.colcod_proveedor_ERP.Name = "colcod_proveedor_ERP";
            this.colcod_proveedor_ERP.OptionsColumn.FixedWidth = true;
            this.colcod_proveedor_ERP.Width = 80;
            // 
            // coldsc_representante_legal
            // 
            this.coldsc_representante_legal.Caption = "Representante Legal";
            this.coldsc_representante_legal.FieldName = "dsc_representante_legal";
            this.coldsc_representante_legal.Name = "coldsc_representante_legal";
            this.coldsc_representante_legal.OptionsColumn.FixedWidth = true;
            this.coldsc_representante_legal.Width = 150;
            // 
            // colfch_registro
            // 
            this.colfch_registro.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_registro.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_registro.Caption = "Fecha registro";
            this.colfch_registro.FieldName = "fch_registro";
            this.colfch_registro.Name = "colfch_registro";
            this.colfch_registro.OptionsColumn.FixedWidth = true;
            this.colfch_registro.Width = 80;
            // 
            // coldsc_usuario_registro
            // 
            this.coldsc_usuario_registro.Caption = "Usuario registro";
            this.coldsc_usuario_registro.FieldName = "dsc_usuario_registro";
            this.coldsc_usuario_registro.Name = "coldsc_usuario_registro";
            this.coldsc_usuario_registro.OptionsColumn.FixedWidth = true;
            this.coldsc_usuario_registro.Width = 150;
            // 
            // colfch_cambio
            // 
            this.colfch_cambio.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_cambio.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_cambio.Caption = "Fecha modificación";
            this.colfch_cambio.FieldName = "fch_cambio";
            this.colfch_cambio.Name = "colfch_cambio";
            this.colfch_cambio.OptionsColumn.FixedWidth = true;
            this.colfch_cambio.Width = 80;
            // 
            // coldsc_usuario_cambio
            // 
            this.coldsc_usuario_cambio.Caption = "Usuario modificación";
            this.coldsc_usuario_cambio.FieldName = "dsc_usuario_cambio";
            this.coldsc_usuario_cambio.Name = "coldsc_usuario_cambio";
            this.coldsc_usuario_cambio.OptionsColumn.FixedWidth = true;
            this.coldsc_usuario_cambio.Width = 150;
            // 
            // coldsc_empresas_vinculadas
            // 
            this.coldsc_empresas_vinculadas.Caption = "Empresas Vinculadas";
            this.coldsc_empresas_vinculadas.ColumnEdit = this.rmmTexto;
            this.coldsc_empresas_vinculadas.FieldName = "dsc_empresas_vinculadas";
            this.coldsc_empresas_vinculadas.Name = "coldsc_empresas_vinculadas";
            this.coldsc_empresas_vinculadas.OptionsColumn.FixedWidth = true;
            this.coldsc_empresas_vinculadas.Width = 200;
            // 
            // rmmTexto
            // 
            this.rmmTexto.Name = "rmmTexto";
            // 
            // coldsc_servicios_vinculadas
            // 
            this.coldsc_servicios_vinculadas.Caption = "Servicios Vinculadas";
            this.coldsc_servicios_vinculadas.ColumnEdit = this.rmmTexto;
            this.coldsc_servicios_vinculadas.FieldName = "dsc_servicios_vinculadas";
            this.coldsc_servicios_vinculadas.Name = "coldsc_servicios_vinculadas";
            this.coldsc_servicios_vinculadas.OptionsColumn.FixedWidth = true;
            this.coldsc_servicios_vinculadas.Width = 200;
            // 
            // colObservaciones
            // 
            this.colObservaciones.ColumnEdit = this.rmmTexto;
            this.colObservaciones.FieldName = "Observaciones";
            this.colObservaciones.Name = "colObservaciones";
            this.colObservaciones.OptionsColumn.FixedWidth = true;
            this.colObservaciones.Width = 200;
            // 
            // colvalorRating
            // 
            this.colvalorRating.Caption = "Calificación";
            this.colvalorRating.ColumnEdit = this.rRatingCalificacion;
            this.colvalorRating.FieldName = "valorRating";
            this.colvalorRating.Name = "colvalorRating";
            this.colvalorRating.OptionsColumn.AllowEdit = false;
            this.colvalorRating.OptionsColumn.FixedWidth = true;
            this.colvalorRating.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.colvalorRating.Visible = true;
            this.colvalorRating.VisibleIndex = 12;
            this.colvalorRating.Width = 130;
            // 
            // rRatingCalificacion
            // 
            this.rRatingCalificacion.AllowFocused = false;
            this.rRatingCalificacion.AutoHeight = false;
            this.rRatingCalificacion.CheckedGlyph = global::UI_Servicios.Properties.Resources.estrella_azul;
            this.rRatingCalificacion.FillPrecision = DevExpress.XtraEditors.RatingItemFillPrecision.Exact;
            this.rRatingCalificacion.ItemCount = 4;
            this.rRatingCalificacion.ItemIndent = 10;
            this.rRatingCalificacion.Name = "rRatingCalificacion";
            // 
            // coldsc_formapago
            // 
            this.coldsc_formapago.Caption = "Forma Pago";
            this.coldsc_formapago.FieldName = "dsc_formapago";
            this.coldsc_formapago.Name = "coldsc_formapago";
            this.coldsc_formapago.OptionsColumn.FixedWidth = true;
            this.coldsc_formapago.Width = 60;
            // 
            // coldsc_banco
            // 
            this.coldsc_banco.Caption = "Banco";
            this.coldsc_banco.ColumnEdit = this.rmmTexto;
            this.coldsc_banco.FieldName = "dsc_banco";
            this.coldsc_banco.Name = "coldsc_banco";
            this.coldsc_banco.OptionsColumn.FixedWidth = true;
            this.coldsc_banco.Width = 70;
            // 
            // coldsc_tipo_cuenta
            // 
            this.coldsc_tipo_cuenta.Caption = "Tipo Cta. Bancaria";
            this.coldsc_tipo_cuenta.ColumnEdit = this.rmmTexto;
            this.coldsc_tipo_cuenta.FieldName = "dsc_tipo_cuenta";
            this.coldsc_tipo_cuenta.Name = "coldsc_tipo_cuenta";
            this.coldsc_tipo_cuenta.OptionsColumn.FixedWidth = true;
            this.coldsc_tipo_cuenta.Width = 70;
            // 
            // coldsc_cta_bancaria
            // 
            this.coldsc_cta_bancaria.Caption = "Cta. Bancaria";
            this.coldsc_cta_bancaria.ColumnEdit = this.rmmTexto;
            this.coldsc_cta_bancaria.FieldName = "dsc_cta_bancaria";
            this.coldsc_cta_bancaria.Name = "coldsc_cta_bancaria";
            this.coldsc_cta_bancaria.OptionsColumn.FixedWidth = true;
            this.coldsc_cta_bancaria.Width = 70;
            // 
            // coldsc_cta_interbancaria
            // 
            this.coldsc_cta_interbancaria.Caption = "Cta. Interbancaria";
            this.coldsc_cta_interbancaria.ColumnEdit = this.rmmTexto;
            this.coldsc_cta_interbancaria.FieldName = "dsc_cta_interbancaria";
            this.coldsc_cta_interbancaria.Name = "coldsc_cta_interbancaria";
            this.coldsc_cta_interbancaria.OptionsColumn.FixedWidth = true;
            this.coldsc_cta_interbancaria.Width = 70;
            // 
            // colcod_moneda
            // 
            this.colcod_moneda.Caption = "Moneda";
            this.colcod_moneda.ColumnEdit = this.rmmTexto;
            this.colcod_moneda.FieldName = "cod_moneda";
            this.colcod_moneda.Name = "colcod_moneda";
            this.colcod_moneda.OptionsColumn.FixedWidth = true;
            this.colcod_moneda.Width = 70;
            // 
            // coldsc_contactos
            // 
            this.coldsc_contactos.Caption = "Contactos";
            this.coldsc_contactos.ColumnEdit = this.rmmTexto;
            this.coldsc_contactos.FieldName = "dsc_contactos";
            this.coldsc_contactos.Name = "coldsc_contactos";
            this.coldsc_contactos.OptionsColumn.FixedWidth = true;
            this.coldsc_contactos.Width = 70;
            // 
            // coldsc_correos_contactos
            // 
            this.coldsc_correos_contactos.Caption = "Correo Contactos";
            this.coldsc_correos_contactos.ColumnEdit = this.rmmTexto;
            this.coldsc_correos_contactos.FieldName = "dsc_correos_contactos";
            this.coldsc_correos_contactos.Name = "coldsc_correos_contactos";
            this.coldsc_correos_contactos.OptionsColumn.FixedWidth = true;
            this.coldsc_correos_contactos.Width = 70;
            // 
            // coldsc_telefonos_contactos
            // 
            this.coldsc_telefonos_contactos.Caption = "Teléfonos Contactos";
            this.coldsc_telefonos_contactos.ColumnEdit = this.rmmTexto;
            this.coldsc_telefonos_contactos.FieldName = "dsc_telefonos_contactos";
            this.coldsc_telefonos_contactos.Name = "coldsc_telefonos_contactos";
            this.coldsc_telefonos_contactos.OptionsColumn.FixedWidth = true;
            this.coldsc_telefonos_contactos.Width = 70;
            // 
            // coldsc_empresa_contactos
            // 
            this.coldsc_empresa_contactos.Caption = "Empresas Contactos";
            this.coldsc_empresa_contactos.ColumnEdit = this.rmmTexto;
            this.coldsc_empresa_contactos.FieldName = "dsc_empresa_contactos";
            this.coldsc_empresa_contactos.Name = "coldsc_empresa_contactos";
            this.coldsc_empresa_contactos.OptionsColumn.FixedWidth = true;
            this.coldsc_empresa_contactos.Width = 70;
            // 
            // navBarControl1
            // 
            this.navBarControl1.BackColor = System.Drawing.Color.Transparent;
            this.navBarControl1.LinkSelectionMode = DevExpress.XtraNavBar.LinkSelectionModeType.OneInGroup;
            this.navBarControl1.Location = new System.Drawing.Point(7, 7);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.NavigationPaneGroupClientHeight = 160;
            this.navBarControl1.NavigationPaneMaxVisibleGroups = 5;
            this.navBarControl1.OptionsNavPane.CollapsedWidth = 55;
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 164;
            this.navBarControl1.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Collapsed;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControl1.Size = new System.Drawing.Size(55, 487);
            this.navBarControl1.TabIndex = 1;
            this.navBarControl1.Text = "navBarControl1";
            this.navBarControl1.View = new DevExpress.XtraNavBar.ViewInfo.StandardSkinNavigationPaneViewInfoRegistrator("DevExpress Style");
            this.navBarControl1.SelectedLinkChanged += new DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventHandler(this.navBarControl1_SelectedLinkChanged);
            this.navBarControl1.ActiveGroupChanged += new DevExpress.XtraNavBar.NavBarGroupEventHandler(this.navBarControl1_ActiveGroupChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem5,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1231, 501);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.navBarControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(59, 491);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.gcListaProveedores;
            this.layoutControlItem5.Location = new System.Drawing.Point(59, 35);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(1162, 456);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lblTitulo;
            this.layoutControlItem3.Location = new System.Drawing.Point(122, 0);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(106, 22);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1099, 35);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.picTitulo;
            this.layoutControlItem4.Location = new System.Drawing.Point(59, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(63, 35);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(63, 35);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(63, 35);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup3.Size = new System.Drawing.Size(229, 246);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup2.Size = new System.Drawing.Size(983, 246);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // coldsc_frecuencia
            // 
            this.coldsc_frecuencia.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_frecuencia.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_frecuencia.Caption = "Frecuencia";
            this.coldsc_frecuencia.FieldName = "dsc_frecuencia";
            this.coldsc_frecuencia.Name = "coldsc_frecuencia";
            this.coldsc_frecuencia.OptionsColumn.FixedWidth = true;
            this.coldsc_frecuencia.Width = 80;
            // 
            // frmListadoProveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1231, 683);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.KeyPreview = true;
            this.Name = "frmListadoProveedores";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Listado de Proveedores";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListadoProveedores_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmListadoProveedores_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTitulo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaProveedores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListaProveedores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaProveedores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmmTexto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rRatingCalificacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage pageAccGenerales;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoEdicion;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem btnNuevo;
        private DevExpress.XtraBars.BarButtonItem btnActivar;
        private DevExpress.XtraBars.BarButtonItem btnInactivar;
        private DevExpress.XtraBars.BarButtonItem btnEliminar;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoReportes;
        private DevExpress.XtraBars.BarButtonItem btnExportarExcel;
        private DevExpress.XtraBars.BarButtonItem btnImprimir;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraEditors.LabelControl lblTitulo;
        private DevExpress.XtraEditors.PictureEdit picTitulo;
        private DevExpress.XtraGrid.GridControl gcListaProveedores;
        private System.Windows.Forms.BindingSource bsListaProveedores;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_proveedor;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_proveedor;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_tipo_documento;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_documento;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_direccion;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_pais;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_departamento;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_provincia;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_distrito;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_modalidad_pago;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        internal DevExpress.XtraGrid.Views.Grid.GridView gvListaProveedores;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_activo;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_razon_comercial;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_mail_1;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_mail_2;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_juridico;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_agente_retencion;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_agente_percepcion;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_buen_contribuyente;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_auto_detraccion;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_afecto_cuarta;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_domiciliado;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_no_habido;
        private DevExpress.XtraBars.BarButtonItem btnFichaProveedor;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoAcciones;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_fono_1;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_proveedor_ERP;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_representante_legal;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_registro;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario_registro;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_cambio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario_cambio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_empresas_vinculadas;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_servicios_vinculadas;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit rmmTexto;
        private DevExpress.XtraGrid.Columns.GridColumn colObservaciones;
        private DevExpress.XtraGrid.Columns.GridColumn colvalorRating;
        private DevExpress.XtraEditors.Repository.RepositoryItemRatingControl rRatingCalificacion;
        private DevExpress.XtraGrid.Columns.GridColumn colCantCriterios;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_formapago;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_banco;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_tipo_cuenta;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_cta_bancaria;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_cta_interbancaria;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_moneda;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_contactos;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_correos_contactos;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_telefonos_contactos;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_empresa_contactos;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_frecuencia;
    }
}