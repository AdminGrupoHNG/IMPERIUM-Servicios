namespace UI_Servicios.Formularios.Logistica
{
    partial class frmListadoRequerimientos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListadoRequerimientos));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNuevoReqCompra = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.btnExportarExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnImprimir = new DevExpress.XtraBars.BarButtonItem();
            this.btnAnularRequerimiento = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminarRequerimiento = new DevExpress.XtraBars.BarButtonItem();
            this.btnAprobar = new DevExpress.XtraBars.BarButtonItem();
            this.btnGenerarOC = new DevExpress.XtraBars.BarButtonItem();
            this.btnDesaprobar = new DevExpress.XtraBars.BarButtonItem();
            this.btnNuevoReqServicio = new DevExpress.XtraBars.BarButtonItem();
            this.btnGenerarOS = new DevExpress.XtraBars.BarButtonItem();
            this.paginaOpciones = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.grupoEdicion = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grupoReportes = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grupoAcciones = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.controlGeneral = new DevExpress.XtraLayout.LayoutControl();
            this.tcRequerimientos = new DevExpress.XtraTab.XtraTabControl();
            this.tpReqSolicitados = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.gcReqSolicitados = new DevExpress.XtraGrid.GridControl();
            this.bsListadoReqSolicitados = new System.Windows.Forms.BindingSource(this.components);
            this.gvReqSolicitados = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_requerimiento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_solicitud = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_razon_social = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_area = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_tipo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_requerimiento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_justificacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_observaciones = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lcRequerimientos = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tpReqAprobados = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.gcReqAprobados = new DevExpress.XtraGrid.GridControl();
            this.bsListadoReqAprobados = new System.Windows.Forms.BindingSource(this.components);
            this.gvReqAprobados = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_requerimiento1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_solicitud1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_razon_social1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_area1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_tipo1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_estado_requerimiento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_requerimiento1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_aprobacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_usuario_aprobacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_justificacion1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tpReqAtendidos = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.gcReqAtendidos = new DevExpress.XtraGrid.GridControl();
            this.bsListadoReqAtendidos = new System.Windows.Forms.BindingSource(this.components);
            this.gvReqAtendidos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_solicitud2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_requerimiento2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_atendido = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario_atendido = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_justificacion2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tpReqAnulados = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcReqAnulados = new DevExpress.XtraGrid.GridControl();
            this.bsListadoReqAnulados = new System.Windows.Forms.BindingSource(this.components);
            this.gvReqAnulados = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnBuscarCliente = new DevExpress.XtraEditors.SimpleButton();
            this.lkpEmpresa = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpTipoFecha = new DevExpress.XtraEditors.LookUpEdit();
            this.dtpDesde = new DevExpress.XtraEditors.DateEdit();
            this.dtpHasta = new DevExpress.XtraEditors.DateEdit();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.txtCliente = new DevExpress.XtraEditors.TextEdit();
            this.lkpSede = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpArea = new DevExpress.XtraEditors.LookUpEdit();
            this.lcGeneral = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.controlCliente = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlBuscarCliente = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlArea = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlTipoFecha = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlEmpresa = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblFiltros = new DevExpress.XtraLayout.SimpleLabelItem();
            this.controlSede = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.controlBuscar = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.controlDesdeFecha = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlFechaHasta = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcGrillas = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlGeneral)).BeginInit();
            this.controlGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcRequerimientos)).BeginInit();
            this.tcRequerimientos.SuspendLayout();
            this.tpReqSolicitados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcReqSolicitados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoReqSolicitados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReqSolicitados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRequerimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.tpReqAprobados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcReqAprobados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoReqAprobados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReqAprobados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.tpReqAtendidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcReqAtendidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoReqAtendidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReqAtendidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.tpReqAnulados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcReqAnulados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoReqAnulados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReqAnulados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEmpresa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoFecha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDesde.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDesde.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpHasta.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpHasta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCliente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSede.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGeneral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBuscarCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlTipoFecha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFiltros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSede)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBuscar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlDesdeFecha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlFechaHasta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrillas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.btnNuevoReqCompra,
            this.barStaticItem1,
            this.btnExportarExcel,
            this.btnImprimir,
            this.btnAnularRequerimiento,
            this.btnEliminarRequerimiento,
            this.btnAprobar,
            this.btnGenerarOC,
            this.btnDesaprobar,
            this.btnNuevoReqServicio,
            this.btnGenerarOS});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 13;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.paginaOpciones});
            this.ribbon.Size = new System.Drawing.Size(1374, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnNuevoReqCompra
            // 
            this.btnNuevoReqCompra.Caption = "Requerimiento de Compra";
            this.btnNuevoReqCompra.Id = 1;
            this.btnNuevoReqCompra.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoReqCompra.ImageOptions.Image")));
            this.btnNuevoReqCompra.Name = "btnNuevoReqCompra";
            this.btnNuevoReqCompra.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnNuevoReqCompra.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNuevoRequerimiento_ItemClick);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Presione F5 para actualizar listado";
            this.barStaticItem1.Id = 3;
            this.barStaticItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.barStaticItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Caption = "Exportar Excel";
            this.btnExportarExcel.Id = 4;
            this.btnExportarExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.ImageOptions.Image")));
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnExportarExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportarExcel_ItemClick);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Caption = "Imprimir";
            this.btnImprimir.Id = 5;
            this.btnImprimir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.ImageOptions.Image")));
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnImprimir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImprimir_ItemClick);
            // 
            // btnAnularRequerimiento
            // 
            this.btnAnularRequerimiento.Caption = "Anular Requerimiento";
            this.btnAnularRequerimiento.Enabled = false;
            this.btnAnularRequerimiento.Id = 6;
            this.btnAnularRequerimiento.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAnularRequerimiento.ImageOptions.Image")));
            this.btnAnularRequerimiento.Name = "btnAnularRequerimiento";
            this.btnAnularRequerimiento.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnAnularRequerimiento.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAnularRequerimiento_ItemClick);
            // 
            // btnEliminarRequerimiento
            // 
            this.btnEliminarRequerimiento.Caption = "Eliminar Requerimiento";
            this.btnEliminarRequerimiento.Enabled = false;
            this.btnEliminarRequerimiento.Id = 7;
            this.btnEliminarRequerimiento.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarRequerimiento.ImageOptions.Image")));
            this.btnEliminarRequerimiento.Name = "btnEliminarRequerimiento";
            this.btnEliminarRequerimiento.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnEliminarRequerimiento.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEliminarRequerimiento_ItemClick);
            // 
            // btnAprobar
            // 
            this.btnAprobar.Caption = "Aprobar Requerimiento";
            this.btnAprobar.Id = 8;
            this.btnAprobar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAprobar.ImageOptions.Image")));
            this.btnAprobar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAprobar.ImageOptions.LargeImage")));
            this.btnAprobar.Name = "btnAprobar";
            this.btnAprobar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnAprobar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAprobar_ItemClick);
            // 
            // btnGenerarOC
            // 
            this.btnGenerarOC.Caption = "Crear Orden de Compra";
            this.btnGenerarOC.Enabled = false;
            this.btnGenerarOC.Id = 9;
            this.btnGenerarOC.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarOC.ImageOptions.Image")));
            this.btnGenerarOC.Name = "btnGenerarOC";
            this.btnGenerarOC.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnGenerarOC.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGenerarOC_ItemClick);
            // 
            // btnDesaprobar
            // 
            this.btnDesaprobar.Caption = "Desaprobar Requerimiento";
            this.btnDesaprobar.Enabled = false;
            this.btnDesaprobar.Id = 10;
            this.btnDesaprobar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDesaprobar.ImageOptions.Image")));
            this.btnDesaprobar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDesaprobar.ImageOptions.LargeImage")));
            this.btnDesaprobar.Name = "btnDesaprobar";
            this.btnDesaprobar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnDesaprobar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDesaprobar_ItemClick);
            // 
            // btnNuevoReqServicio
            // 
            this.btnNuevoReqServicio.Caption = "Requerimiento de Servicio";
            this.btnNuevoReqServicio.Id = 11;
            this.btnNuevoReqServicio.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoReqServicio.ImageOptions.Image")));
            this.btnNuevoReqServicio.Name = "btnNuevoReqServicio";
            this.btnNuevoReqServicio.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnNuevoReqServicio.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNuevoReqServicio_ItemClick);
            // 
            // btnGenerarOS
            // 
            this.btnGenerarOS.Caption = "Crear Orden de Servicio";
            this.btnGenerarOS.Enabled = false;
            this.btnGenerarOS.Id = 12;
            this.btnGenerarOS.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarOS.ImageOptions.Image")));
            this.btnGenerarOS.Name = "btnGenerarOS";
            this.btnGenerarOS.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnGenerarOS.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGenerarOS_ItemClick);
            // 
            // paginaOpciones
            // 
            this.paginaOpciones.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grupoEdicion,
            this.grupoReportes,
            this.grupoAcciones});
            this.paginaOpciones.Name = "paginaOpciones";
            this.paginaOpciones.Text = "Opciones Requerimiento";
            // 
            // grupoEdicion
            // 
            this.grupoEdicion.ItemLinks.Add(this.btnNuevoReqCompra);
            this.grupoEdicion.ItemLinks.Add(this.btnNuevoReqServicio);
            this.grupoEdicion.ItemLinks.Add(this.btnAnularRequerimiento);
            this.grupoEdicion.ItemLinks.Add(this.btnEliminarRequerimiento);
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
            this.grupoAcciones.ItemLinks.Add(this.btnAprobar);
            this.grupoAcciones.ItemLinks.Add(this.btnDesaprobar);
            this.grupoAcciones.ItemLinks.Add(this.btnGenerarOC);
            this.grupoAcciones.ItemLinks.Add(this.btnGenerarOS);
            this.grupoAcciones.Name = "grupoAcciones";
            this.grupoAcciones.Text = "Acciones";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItem1);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 738);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1374, 24);
            // 
            // controlGeneral
            // 
            this.controlGeneral.Controls.Add(this.tcRequerimientos);
            this.controlGeneral.Controls.Add(this.btnBuscarCliente);
            this.controlGeneral.Controls.Add(this.lkpEmpresa);
            this.controlGeneral.Controls.Add(this.lkpTipoFecha);
            this.controlGeneral.Controls.Add(this.dtpDesde);
            this.controlGeneral.Controls.Add(this.dtpHasta);
            this.controlGeneral.Controls.Add(this.btnBuscar);
            this.controlGeneral.Controls.Add(this.txtCliente);
            this.controlGeneral.Controls.Add(this.lkpSede);
            this.controlGeneral.Controls.Add(this.lkpArea);
            this.controlGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlGeneral.Location = new System.Drawing.Point(0, 158);
            this.controlGeneral.Name = "controlGeneral";
            this.controlGeneral.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2636, 239, 650, 400);
            this.controlGeneral.Root = this.lcGeneral;
            this.controlGeneral.Size = new System.Drawing.Size(1374, 580);
            this.controlGeneral.TabIndex = 2;
            this.controlGeneral.Text = "layoutControl1";
            // 
            // tcRequerimientos
            // 
            this.tcRequerimientos.Location = new System.Drawing.Point(8, 79);
            this.tcRequerimientos.Name = "tcRequerimientos";
            this.tcRequerimientos.SelectedTabPage = this.tpReqSolicitados;
            this.tcRequerimientos.Size = new System.Drawing.Size(1358, 493);
            this.tcRequerimientos.TabIndex = 10;
            this.tcRequerimientos.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpReqSolicitados,
            this.tpReqAprobados,
            this.tpReqAtendidos,
            this.tpReqAnulados});
            this.tcRequerimientos.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tcRequerimientos_SelectedPageChanged);
            // 
            // tpReqSolicitados
            // 
            this.tpReqSolicitados.Controls.Add(this.layoutControl2);
            this.tpReqSolicitados.Name = "tpReqSolicitados";
            this.tpReqSolicitados.Size = new System.Drawing.Size(1356, 468);
            this.tpReqSolicitados.Text = "Requerimientos Solicitados";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.gcReqSolicitados);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.lcRequerimientos;
            this.layoutControl2.Size = new System.Drawing.Size(1356, 468);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // gcReqSolicitados
            // 
            this.gcReqSolicitados.DataSource = this.bsListadoReqSolicitados;
            this.gcReqSolicitados.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcReqSolicitados.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcReqSolicitados.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcReqSolicitados.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcReqSolicitados.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcReqSolicitados.Location = new System.Drawing.Point(12, 12);
            this.gcReqSolicitados.MainView = this.gvReqSolicitados;
            this.gcReqSolicitados.MenuManager = this.ribbon;
            this.gcReqSolicitados.Name = "gcReqSolicitados";
            this.gcReqSolicitados.Padding = new System.Windows.Forms.Padding(6);
            this.gcReqSolicitados.Size = new System.Drawing.Size(1332, 444);
            this.gcReqSolicitados.TabIndex = 4;
            this.gcReqSolicitados.UseEmbeddedNavigator = true;
            this.gcReqSolicitados.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvReqSolicitados});
            // 
            // bsListadoReqSolicitados
            // 
            this.bsListadoReqSolicitados.DataSource = typeof(BE_Servicios.eRequerimiento);
            // 
            // gvReqSolicitados
            // 
            this.gvReqSolicitados.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvReqSolicitados.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvReqSolicitados.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvReqSolicitados.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvReqSolicitados.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvReqSolicitados.ColumnPanelRowHeight = 35;
            this.gvReqSolicitados.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_requerimiento,
            this.colflg_solicitud,
            this.coldsc_razon_social,
            this.coldsc_area,
            this.colcod_tipo,
            this.coldsc_usuario,
            this.colfch_requerimiento,
            this.coldsc_justificacion,
            this.coldsc_observaciones});
            this.gvReqSolicitados.GridControl = this.gcReqSolicitados;
            this.gvReqSolicitados.Name = "gvReqSolicitados";
            this.gvReqSolicitados.OptionsBehavior.Editable = false;
            this.gvReqSolicitados.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvReqSolicitados.OptionsSelection.MultiSelect = true;
            this.gvReqSolicitados.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvReqSolicitados.OptionsView.EnableAppearanceEvenRow = true;
            this.gvReqSolicitados.OptionsView.ShowAutoFilterRow = true;
            this.gvReqSolicitados.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvReqSolicitados_RowClick);
            this.gvReqSolicitados.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvReqSolicitados_CustomDrawColumnHeader);
            this.gvReqSolicitados.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvReqSolicitados_CustomDrawCell);
            this.gvReqSolicitados.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvReqSolicitados_RowCellStyle);
            this.gvReqSolicitados.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvReqSolicitados_RowStyle);
            // 
            // colcod_requerimiento
            // 
            this.colcod_requerimiento.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_requerimiento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_requerimiento.Caption = "Código";
            this.colcod_requerimiento.FieldName = "cod_requerimiento";
            this.colcod_requerimiento.Name = "colcod_requerimiento";
            this.colcod_requerimiento.OptionsColumn.FixedWidth = true;
            this.colcod_requerimiento.Visible = true;
            this.colcod_requerimiento.VisibleIndex = 1;
            this.colcod_requerimiento.Width = 80;
            // 
            // colflg_solicitud
            // 
            this.colflg_solicitud.Caption = "Solicitud";
            this.colflg_solicitud.FieldName = "flg_solicitud";
            this.colflg_solicitud.Name = "colflg_solicitud";
            this.colflg_solicitud.OptionsColumn.FixedWidth = true;
            this.colflg_solicitud.Visible = true;
            this.colflg_solicitud.VisibleIndex = 2;
            this.colflg_solicitud.Width = 70;
            // 
            // coldsc_razon_social
            // 
            this.coldsc_razon_social.Caption = "Cliente";
            this.coldsc_razon_social.FieldName = "dsc_razon_social";
            this.coldsc_razon_social.Name = "coldsc_razon_social";
            this.coldsc_razon_social.OptionsColumn.FixedWidth = true;
            this.coldsc_razon_social.Visible = true;
            this.coldsc_razon_social.VisibleIndex = 3;
            this.coldsc_razon_social.Width = 200;
            // 
            // coldsc_area
            // 
            this.coldsc_area.Caption = "Area";
            this.coldsc_area.FieldName = "dsc_area";
            this.coldsc_area.Name = "coldsc_area";
            this.coldsc_area.OptionsColumn.FixedWidth = true;
            this.coldsc_area.Visible = true;
            this.coldsc_area.VisibleIndex = 4;
            this.coldsc_area.Width = 120;
            // 
            // colcod_tipo
            // 
            this.colcod_tipo.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_tipo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_tipo.Caption = "Tipo";
            this.colcod_tipo.FieldName = "cod_tipo";
            this.colcod_tipo.Name = "colcod_tipo";
            this.colcod_tipo.OptionsColumn.FixedWidth = true;
            this.colcod_tipo.Visible = true;
            this.colcod_tipo.VisibleIndex = 5;
            this.colcod_tipo.Width = 80;
            // 
            // coldsc_usuario
            // 
            this.coldsc_usuario.Caption = "Solicitante";
            this.coldsc_usuario.FieldName = "dsc_usuario";
            this.coldsc_usuario.Name = "coldsc_usuario";
            this.coldsc_usuario.OptionsColumn.FixedWidth = true;
            this.coldsc_usuario.Visible = true;
            this.coldsc_usuario.VisibleIndex = 6;
            this.coldsc_usuario.Width = 120;
            // 
            // colfch_requerimiento
            // 
            this.colfch_requerimiento.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_requerimiento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_requerimiento.Caption = "Fecha Requerimiento";
            this.colfch_requerimiento.FieldName = "fch_requerimiento";
            this.colfch_requerimiento.Name = "colfch_requerimiento";
            this.colfch_requerimiento.OptionsColumn.FixedWidth = true;
            this.colfch_requerimiento.Visible = true;
            this.colfch_requerimiento.VisibleIndex = 7;
            this.colfch_requerimiento.Width = 80;
            // 
            // coldsc_justificacion
            // 
            this.coldsc_justificacion.Caption = "Justificación";
            this.coldsc_justificacion.FieldName = "dsc_justificacion";
            this.coldsc_justificacion.Name = "coldsc_justificacion";
            this.coldsc_justificacion.OptionsColumn.FixedWidth = true;
            this.coldsc_justificacion.Visible = true;
            this.coldsc_justificacion.VisibleIndex = 8;
            this.coldsc_justificacion.Width = 150;
            // 
            // coldsc_observaciones
            // 
            this.coldsc_observaciones.Caption = "Observaciones";
            this.coldsc_observaciones.FieldName = "dsc_observaciones";
            this.coldsc_observaciones.Name = "coldsc_observaciones";
            this.coldsc_observaciones.OptionsColumn.FixedWidth = true;
            this.coldsc_observaciones.Width = 120;
            // 
            // lcRequerimientos
            // 
            this.lcRequerimientos.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcRequerimientos.GroupBordersVisible = false;
            this.lcRequerimientos.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.lcRequerimientos.Name = "lcRequerimientos";
            this.lcRequerimientos.Size = new System.Drawing.Size(1356, 468);
            this.lcRequerimientos.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcReqSolicitados;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1336, 448);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // tpReqAprobados
            // 
            this.tpReqAprobados.Controls.Add(this.layoutControl3);
            this.tpReqAprobados.Name = "tpReqAprobados";
            this.tpReqAprobados.Size = new System.Drawing.Size(1356, 468);
            this.tpReqAprobados.Text = "Requerimientos Aprobados";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.gcReqAprobados);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.Root;
            this.layoutControl3.Size = new System.Drawing.Size(1356, 468);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // gcReqAprobados
            // 
            this.gcReqAprobados.DataSource = this.bsListadoReqAprobados;
            this.gcReqAprobados.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcReqAprobados.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcReqAprobados.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcReqAprobados.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcReqAprobados.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcReqAprobados.Location = new System.Drawing.Point(12, 12);
            this.gcReqAprobados.MainView = this.gvReqAprobados;
            this.gcReqAprobados.MenuManager = this.ribbon;
            this.gcReqAprobados.Name = "gcReqAprobados";
            this.gcReqAprobados.Padding = new System.Windows.Forms.Padding(6);
            this.gcReqAprobados.Size = new System.Drawing.Size(1332, 444);
            this.gcReqAprobados.TabIndex = 5;
            this.gcReqAprobados.UseEmbeddedNavigator = true;
            this.gcReqAprobados.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvReqAprobados});
            // 
            // bsListadoReqAprobados
            // 
            this.bsListadoReqAprobados.DataSource = typeof(BE_Servicios.eRequerimiento);
            // 
            // gvReqAprobados
            // 
            this.gvReqAprobados.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvReqAprobados.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvReqAprobados.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvReqAprobados.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvReqAprobados.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvReqAprobados.ColumnPanelRowHeight = 30;
            this.gvReqAprobados.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_requerimiento1,
            this.colflg_solicitud1,
            this.coldsc_razon_social1,
            this.coldsc_area1,
            this.colcod_tipo1,
            this.colcod_estado_requerimiento,
            this.colfch_requerimiento1,
            this.colfch_aprobacion,
            this.colcod_usuario_aprobacion,
            this.coldsc_justificacion1});
            this.gvReqAprobados.GridControl = this.gcReqAprobados;
            this.gvReqAprobados.Name = "gvReqAprobados";
            this.gvReqAprobados.OptionsBehavior.Editable = false;
            this.gvReqAprobados.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvReqAprobados.OptionsSelection.MultiSelect = true;
            this.gvReqAprobados.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvReqAprobados.OptionsView.EnableAppearanceEvenRow = true;
            this.gvReqAprobados.OptionsView.ShowAutoFilterRow = true;
            this.gvReqAprobados.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvReqAprobados_RowClick);
            this.gvReqAprobados.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvReqAprobados_CustomDrawColumnHeader);
            this.gvReqAprobados.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvReqAprobados_CustomDrawCell);
            this.gvReqAprobados.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvReqAprobados_RowCellStyle);
            this.gvReqAprobados.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvReqAprobados_RowStyle);
            this.gvReqAprobados.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gvReqAprobados_SelectionChanged);
            // 
            // colcod_requerimiento1
            // 
            this.colcod_requerimiento1.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_requerimiento1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_requerimiento1.Caption = "Código";
            this.colcod_requerimiento1.FieldName = "cod_requerimiento";
            this.colcod_requerimiento1.Name = "colcod_requerimiento1";
            this.colcod_requerimiento1.OptionsColumn.FixedWidth = true;
            this.colcod_requerimiento1.Visible = true;
            this.colcod_requerimiento1.VisibleIndex = 1;
            this.colcod_requerimiento1.Width = 80;
            // 
            // colflg_solicitud1
            // 
            this.colflg_solicitud1.Caption = "Solicitud";
            this.colflg_solicitud1.FieldName = "flg_solicitud";
            this.colflg_solicitud1.Name = "colflg_solicitud1";
            this.colflg_solicitud1.OptionsColumn.FixedWidth = true;
            this.colflg_solicitud1.Visible = true;
            this.colflg_solicitud1.VisibleIndex = 2;
            this.colflg_solicitud1.Width = 70;
            // 
            // coldsc_razon_social1
            // 
            this.coldsc_razon_social1.Caption = "Cliente";
            this.coldsc_razon_social1.FieldName = "dsc_razon_social";
            this.coldsc_razon_social1.Name = "coldsc_razon_social1";
            this.coldsc_razon_social1.OptionsColumn.FixedWidth = true;
            this.coldsc_razon_social1.Visible = true;
            this.coldsc_razon_social1.VisibleIndex = 3;
            this.coldsc_razon_social1.Width = 200;
            // 
            // coldsc_area1
            // 
            this.coldsc_area1.Caption = "Área";
            this.coldsc_area1.FieldName = "dsc_area";
            this.coldsc_area1.Name = "coldsc_area1";
            this.coldsc_area1.OptionsColumn.FixedWidth = true;
            this.coldsc_area1.Visible = true;
            this.coldsc_area1.VisibleIndex = 4;
            this.coldsc_area1.Width = 120;
            // 
            // colcod_tipo1
            // 
            this.colcod_tipo1.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_tipo1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_tipo1.Caption = "Tipo";
            this.colcod_tipo1.FieldName = "cod_tipo";
            this.colcod_tipo1.Name = "colcod_tipo1";
            this.colcod_tipo1.OptionsColumn.FixedWidth = true;
            this.colcod_tipo1.Visible = true;
            this.colcod_tipo1.VisibleIndex = 5;
            this.colcod_tipo1.Width = 80;
            // 
            // colcod_estado_requerimiento
            // 
            this.colcod_estado_requerimiento.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colcod_estado_requerimiento.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.colcod_estado_requerimiento.AppearanceCell.Options.UseFont = true;
            this.colcod_estado_requerimiento.AppearanceCell.Options.UseForeColor = true;
            this.colcod_estado_requerimiento.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_estado_requerimiento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_estado_requerimiento.Caption = "Estado";
            this.colcod_estado_requerimiento.FieldName = "cod_estado_requerimiento";
            this.colcod_estado_requerimiento.Name = "colcod_estado_requerimiento";
            this.colcod_estado_requerimiento.OptionsColumn.FixedWidth = true;
            this.colcod_estado_requerimiento.Visible = true;
            this.colcod_estado_requerimiento.VisibleIndex = 6;
            this.colcod_estado_requerimiento.Width = 80;
            // 
            // colfch_requerimiento1
            // 
            this.colfch_requerimiento1.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_requerimiento1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_requerimiento1.Caption = "Fecha Emision";
            this.colfch_requerimiento1.FieldName = "fch_requerimiento";
            this.colfch_requerimiento1.Name = "colfch_requerimiento1";
            this.colfch_requerimiento1.OptionsColumn.FixedWidth = true;
            this.colfch_requerimiento1.Visible = true;
            this.colfch_requerimiento1.VisibleIndex = 7;
            this.colfch_requerimiento1.Width = 80;
            // 
            // colfch_aprobacion
            // 
            this.colfch_aprobacion.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_aprobacion.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_aprobacion.Caption = "Fecha Aprobación";
            this.colfch_aprobacion.FieldName = "fch_aprobacion";
            this.colfch_aprobacion.Name = "colfch_aprobacion";
            this.colfch_aprobacion.OptionsColumn.FixedWidth = true;
            this.colfch_aprobacion.Visible = true;
            this.colfch_aprobacion.VisibleIndex = 8;
            this.colfch_aprobacion.Width = 80;
            // 
            // colcod_usuario_aprobacion
            // 
            this.colcod_usuario_aprobacion.Caption = "Usuario Aprobación";
            this.colcod_usuario_aprobacion.FieldName = "cod_usuario_aprobacion";
            this.colcod_usuario_aprobacion.Name = "colcod_usuario_aprobacion";
            this.colcod_usuario_aprobacion.OptionsColumn.FixedWidth = true;
            this.colcod_usuario_aprobacion.Visible = true;
            this.colcod_usuario_aprobacion.VisibleIndex = 9;
            this.colcod_usuario_aprobacion.Width = 120;
            // 
            // coldsc_justificacion1
            // 
            this.coldsc_justificacion1.Caption = "Justificación";
            this.coldsc_justificacion1.FieldName = "dsc_justificacion";
            this.coldsc_justificacion1.Name = "coldsc_justificacion1";
            this.coldsc_justificacion1.OptionsColumn.FixedWidth = true;
            this.coldsc_justificacion1.Visible = true;
            this.coldsc_justificacion1.VisibleIndex = 10;
            this.coldsc_justificacion1.Width = 150;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1356, 468);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcReqAprobados;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1336, 448);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // tpReqAtendidos
            // 
            this.tpReqAtendidos.Controls.Add(this.layoutControl4);
            this.tpReqAtendidos.Name = "tpReqAtendidos";
            this.tpReqAtendidos.Size = new System.Drawing.Size(1356, 468);
            this.tpReqAtendidos.Text = "Requerimientos Atendidos";
            // 
            // layoutControl4
            // 
            this.layoutControl4.Controls.Add(this.gcReqAtendidos);
            this.layoutControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl4.Location = new System.Drawing.Point(0, 0);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.Root = this.layoutControlGroup4;
            this.layoutControl4.Size = new System.Drawing.Size(1356, 468);
            this.layoutControl4.TabIndex = 0;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // gcReqAtendidos
            // 
            this.gcReqAtendidos.DataSource = this.bsListadoReqAtendidos;
            this.gcReqAtendidos.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcReqAtendidos.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcReqAtendidos.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcReqAtendidos.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcReqAtendidos.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcReqAtendidos.Location = new System.Drawing.Point(12, 12);
            this.gcReqAtendidos.MainView = this.gvReqAtendidos;
            this.gcReqAtendidos.MenuManager = this.ribbon;
            this.gcReqAtendidos.Name = "gcReqAtendidos";
            this.gcReqAtendidos.Padding = new System.Windows.Forms.Padding(6);
            this.gcReqAtendidos.Size = new System.Drawing.Size(1332, 444);
            this.gcReqAtendidos.TabIndex = 6;
            this.gcReqAtendidos.UseEmbeddedNavigator = true;
            this.gcReqAtendidos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvReqAtendidos});
            // 
            // bsListadoReqAtendidos
            // 
            this.bsListadoReqAtendidos.DataSource = typeof(BE_Servicios.eRequerimiento);
            // 
            // gvReqAtendidos
            // 
            this.gvReqAtendidos.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvReqAtendidos.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvReqAtendidos.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvReqAtendidos.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvReqAtendidos.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvReqAtendidos.ColumnPanelRowHeight = 30;
            this.gvReqAtendidos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.colflg_solicitud2,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn7,
            this.gridColumn4,
            this.colfch_requerimiento2,
            this.colfch_atendido,
            this.coldsc_usuario_atendido,
            this.coldsc_justificacion2});
            this.gvReqAtendidos.GridControl = this.gcReqAtendidos;
            this.gvReqAtendidos.Name = "gvReqAtendidos";
            this.gvReqAtendidos.OptionsBehavior.Editable = false;
            this.gvReqAtendidos.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvReqAtendidos.OptionsSelection.MultiSelect = true;
            this.gvReqAtendidos.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvReqAtendidos.OptionsView.EnableAppearanceEvenRow = true;
            this.gvReqAtendidos.OptionsView.ShowAutoFilterRow = true;
            this.gvReqAtendidos.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvReqAtendidos_RowClick);
            this.gvReqAtendidos.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvReqAtendidos_CustomDrawColumnHeader);
            this.gvReqAtendidos.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvReqAtendidos_CustomDrawCell);
            this.gvReqAtendidos.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvReqAtendidos_RowCellStyle);
            this.gvReqAtendidos.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvReqAtendidos_RowStyle);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Código";
            this.gridColumn1.FieldName = "cod_requerimiento";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 80;
            // 
            // colflg_solicitud2
            // 
            this.colflg_solicitud2.Caption = "Solicitud";
            this.colflg_solicitud2.FieldName = "flg_solicitud";
            this.colflg_solicitud2.Name = "colflg_solicitud2";
            this.colflg_solicitud2.OptionsColumn.FixedWidth = true;
            this.colflg_solicitud2.Visible = true;
            this.colflg_solicitud2.VisibleIndex = 2;
            this.colflg_solicitud2.Width = 70;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Cliente";
            this.gridColumn2.FieldName = "dsc_razon_social";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 200;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Área";
            this.gridColumn3.FieldName = "dsc_area";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            this.gridColumn3.Width = 120;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "Tipo";
            this.gridColumn7.FieldName = "cod_tipo";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.FixedWidth = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            this.gridColumn7.Width = 80;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn4.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Estado";
            this.gridColumn4.FieldName = "cod_estado_requerimiento";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.FixedWidth = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 6;
            this.gridColumn4.Width = 80;
            // 
            // colfch_requerimiento2
            // 
            this.colfch_requerimiento2.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_requerimiento2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_requerimiento2.Caption = "Fecha Emision";
            this.colfch_requerimiento2.FieldName = "fch_requerimiento";
            this.colfch_requerimiento2.Name = "colfch_requerimiento2";
            this.colfch_requerimiento2.OptionsColumn.FixedWidth = true;
            this.colfch_requerimiento2.Visible = true;
            this.colfch_requerimiento2.VisibleIndex = 7;
            this.colfch_requerimiento2.Width = 80;
            // 
            // colfch_atendido
            // 
            this.colfch_atendido.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_atendido.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_atendido.Caption = "Fecha de Atención";
            this.colfch_atendido.FieldName = "fch_atendido";
            this.colfch_atendido.Name = "colfch_atendido";
            this.colfch_atendido.OptionsColumn.FixedWidth = true;
            this.colfch_atendido.Visible = true;
            this.colfch_atendido.VisibleIndex = 8;
            this.colfch_atendido.Width = 80;
            // 
            // coldsc_usuario_atendido
            // 
            this.coldsc_usuario_atendido.Caption = "Atendido Por";
            this.coldsc_usuario_atendido.FieldName = "dsc_usuario_atendido";
            this.coldsc_usuario_atendido.Name = "coldsc_usuario_atendido";
            this.coldsc_usuario_atendido.OptionsColumn.FixedWidth = true;
            this.coldsc_usuario_atendido.Visible = true;
            this.coldsc_usuario_atendido.VisibleIndex = 9;
            this.coldsc_usuario_atendido.Width = 120;
            // 
            // coldsc_justificacion2
            // 
            this.coldsc_justificacion2.Caption = "Justificación";
            this.coldsc_justificacion2.FieldName = "dsc_justificacion";
            this.coldsc_justificacion2.Name = "coldsc_justificacion2";
            this.coldsc_justificacion2.OptionsColumn.FixedWidth = true;
            this.coldsc_justificacion2.Visible = true;
            this.coldsc_justificacion2.VisibleIndex = 10;
            this.coldsc_justificacion2.Width = 150;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup4.GroupBordersVisible = false;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup4.Name = "Root";
            this.layoutControlGroup4.Size = new System.Drawing.Size(1356, 468);
            this.layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gcReqAtendidos;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1336, 448);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // tpReqAnulados
            // 
            this.tpReqAnulados.Controls.Add(this.layoutControl1);
            this.tpReqAnulados.Name = "tpReqAnulados";
            this.tpReqAnulados.Size = new System.Drawing.Size(1356, 468);
            this.tpReqAnulados.Text = "Requerimientos Anulados";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcReqAnulados);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup5;
            this.layoutControl1.Size = new System.Drawing.Size(1356, 468);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcReqAnulados
            // 
            this.gcReqAnulados.DataSource = this.bsListadoReqAnulados;
            this.gcReqAnulados.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcReqAnulados.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcReqAnulados.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcReqAnulados.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcReqAnulados.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcReqAnulados.Location = new System.Drawing.Point(12, 12);
            this.gcReqAnulados.MainView = this.gvReqAnulados;
            this.gcReqAnulados.MenuManager = this.ribbon;
            this.gcReqAnulados.Name = "gcReqAnulados";
            this.gcReqAnulados.Padding = new System.Windows.Forms.Padding(6);
            this.gcReqAnulados.Size = new System.Drawing.Size(1332, 444);
            this.gcReqAnulados.TabIndex = 7;
            this.gcReqAnulados.UseEmbeddedNavigator = true;
            this.gcReqAnulados.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvReqAnulados});
            // 
            // bsListadoReqAnulados
            // 
            this.bsListadoReqAnulados.DataSource = typeof(BE_Servicios.eRequerimiento);
            // 
            // gvReqAnulados
            // 
            this.gvReqAnulados.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvReqAnulados.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvReqAnulados.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvReqAnulados.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvReqAnulados.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvReqAnulados.ColumnPanelRowHeight = 30;
            this.gvReqAnulados.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15});
            this.gvReqAnulados.GridControl = this.gcReqAnulados;
            this.gvReqAnulados.Name = "gvReqAnulados";
            this.gvReqAnulados.OptionsBehavior.Editable = false;
            this.gvReqAnulados.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvReqAnulados.OptionsSelection.MultiSelect = true;
            this.gvReqAnulados.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvReqAnulados.OptionsView.EnableAppearanceEvenRow = true;
            this.gvReqAnulados.OptionsView.ShowAutoFilterRow = true;
            this.gvReqAnulados.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvReqAnulados_RowClick);
            this.gvReqAnulados.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvReqAnulados_CustomDrawColumnHeader);
            this.gvReqAnulados.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvReqAnulados_CustomDrawCell);
            this.gvReqAnulados.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvReqAnulados_RowCellStyle);
            this.gvReqAnulados.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvReqAnulados_RowStyle);
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "Código";
            this.gridColumn5.FieldName = "cod_requerimiento";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.FixedWidth = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 80;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Solicitud";
            this.gridColumn6.FieldName = "flg_solicitud";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.FixedWidth = true;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 70;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Cliente";
            this.gridColumn8.FieldName = "dsc_razon_social";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.FixedWidth = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            this.gridColumn8.Width = 200;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Área";
            this.gridColumn9.FieldName = "dsc_area";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.FixedWidth = true;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 4;
            this.gridColumn9.Width = 120;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.Caption = "Tipo";
            this.gridColumn10.FieldName = "cod_tipo";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.FixedWidth = true;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 5;
            this.gridColumn10.Width = 80;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.Caption = "Fecha Emision";
            this.gridColumn12.FieldName = "fch_requerimiento";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.FixedWidth = true;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 6;
            this.gridColumn12.Width = 80;
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn13.Caption = "Fecha Anulación";
            this.gridColumn13.FieldName = "fch_anulacion";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.FixedWidth = true;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 7;
            this.gridColumn13.Width = 80;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Anulado Por";
            this.gridColumn14.FieldName = "dsc_usuario_anulacion";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.FixedWidth = true;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 8;
            this.gridColumn14.Width = 120;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Justificación";
            this.gridColumn15.FieldName = "dsc_justificacion";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.FixedWidth = true;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 9;
            this.gridColumn15.Width = 150;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup5.GroupBordersVisible = false;
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Size = new System.Drawing.Size(1356, 468);
            this.layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcReqAnulados;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1336, 448);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarCliente.ImageOptions.Image")));
            this.btnBuscarCliente.Location = new System.Drawing.Point(833, 30);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(22, 20);
            this.btnBuscarCliente.StyleController = this.controlGeneral;
            this.btnBuscarCliente.TabIndex = 9;
            this.btnBuscarCliente.Click += new System.EventHandler(this.btnBuscarCliente_Click);
            // 
            // lkpEmpresa
            // 
            this.lkpEmpresa.Location = new System.Drawing.Point(63, 30);
            this.lkpEmpresa.Name = "lkpEmpresa";
            this.lkpEmpresa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpEmpresa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_empresa", "Descripción")});
            this.lkpEmpresa.Properties.NullText = "";
            this.lkpEmpresa.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.lkpEmpresa.Size = new System.Drawing.Size(320, 20);
            this.lkpEmpresa.StyleController = this.controlGeneral;
            this.lkpEmpresa.TabIndex = 4;
            this.lkpEmpresa.EditValueChanged += new System.EventHandler(this.lkpEmpresa_EditValueChanged);
            // 
            // lkpTipoFecha
            // 
            this.lkpTipoFecha.Location = new System.Drawing.Point(939, 30);
            this.lkpTipoFecha.Name = "lkpTipoFecha";
            this.lkpTipoFecha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipoFecha.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_tipo_fecha", "Descripción")});
            this.lkpTipoFecha.Properties.NullText = "";
            this.lkpTipoFecha.Size = new System.Drawing.Size(247, 20);
            this.lkpTipoFecha.StyleController = this.controlGeneral;
            this.lkpTipoFecha.TabIndex = 5;
            // 
            // dtpDesde
            // 
            this.dtpDesde.EditValue = null;
            this.dtpDesde.Location = new System.Drawing.Point(939, 54);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Properties.Appearance.Options.UseTextOptions = true;
            this.dtpDesde.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dtpDesde.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDesde.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDesde.Size = new System.Drawing.Size(102, 20);
            this.dtpDesde.StyleController = this.controlGeneral;
            this.dtpDesde.TabIndex = 7;
            // 
            // dtpHasta
            // 
            this.dtpHasta.EditValue = null;
            this.dtpHasta.Location = new System.Drawing.Point(1085, 54);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Properties.Appearance.Options.UseTextOptions = true;
            this.dtpHasta.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dtpHasta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpHasta.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpHasta.Size = new System.Drawing.Size(101, 20);
            this.dtpHasta.StyleController = this.controlGeneral;
            this.dtpHasta.TabIndex = 7;
            // 
            // btnBuscar
            // 
            this.btnBuscar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.ImageOptions.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(1222, 33);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(111, 39);
            this.btnBuscar.StyleController = this.controlGeneral;
            this.btnBuscar.TabIndex = 8;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(459, 30);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(370, 20);
            this.txtCliente.StyleController = this.controlGeneral;
            this.txtCliente.TabIndex = 4;
            this.txtCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCliente_KeyPress);
            // 
            // lkpSede
            // 
            this.lkpSede.Location = new System.Drawing.Point(63, 54);
            this.lkpSede.Name = "lkpSede";
            this.lkpSede.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpSede.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_sede_empresa", "Descripción")});
            this.lkpSede.Properties.NullText = "";
            this.lkpSede.Size = new System.Drawing.Size(320, 20);
            this.lkpSede.StyleController = this.controlGeneral;
            this.lkpSede.TabIndex = 6;
            this.lkpSede.EditValueChanged += new System.EventHandler(this.lkpSede_EditValueChanged);
            // 
            // lkpArea
            // 
            this.lkpArea.Location = new System.Drawing.Point(459, 55);
            this.lkpArea.MenuManager = this.ribbon;
            this.lkpArea.Name = "lkpArea";
            this.lkpArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpArea.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_area", "Descripción")});
            this.lkpArea.Properties.NullText = "";
            this.lkpArea.Size = new System.Drawing.Size(370, 20);
            this.lkpArea.StyleController = this.controlGeneral;
            this.lkpArea.TabIndex = 11;
            // 
            // lcGeneral
            // 
            this.lcGeneral.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcGeneral.GroupBordersVisible = false;
            this.lcGeneral.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.lcGrillas});
            this.lcGeneral.Name = "lcGeneral";
            this.lcGeneral.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.lcGeneral.Size = new System.Drawing.Size(1374, 580);
            this.lcGeneral.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.controlTipoFecha,
            this.controlEmpresa,
            this.emptySpaceItem2,
            this.lblFiltros,
            this.controlSede,
            this.emptySpaceItem4,
            this.controlBuscar,
            this.emptySpaceItem5,
            this.controlDesdeFecha,
            this.controlFechaHasta,
            this.emptySpaceItem3});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 3;
            this.layoutControlGroup2.Size = new System.Drawing.Size(1362, 71);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.controlCliente,
            this.controlBuscarCliente,
            this.controlArea});
            this.layoutControlGroup3.Location = new System.Drawing.Point(389, 22);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsItemText.TextToControlDistance = 3;
            this.layoutControlGroup3.Size = new System.Drawing.Size(462, 49);
            this.layoutControlGroup3.Text = "layoutControlGroup2";
            // 
            // controlCliente
            // 
            this.controlCliente.Control = this.txtCliente;
            this.controlCliente.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.controlCliente.CustomizationFormText = "Cliente";
            this.controlCliente.Location = new System.Drawing.Point(0, 0);
            this.controlCliente.MaxSize = new System.Drawing.Size(436, 25);
            this.controlCliente.MinSize = new System.Drawing.Size(436, 25);
            this.controlCliente.Name = "controlCliente";
            this.controlCliente.Size = new System.Drawing.Size(436, 25);
            this.controlCliente.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlCliente.Text = "Cliente :";
            this.controlCliente.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlCliente.TextLocation = DevExpress.Utils.Locations.Left;
            this.controlCliente.TextSize = new System.Drawing.Size(50, 13);
            this.controlCliente.TextToControlDistance = 12;
            // 
            // controlBuscarCliente
            // 
            this.controlBuscarCliente.Control = this.btnBuscarCliente;
            this.controlBuscarCliente.CustomizationFormText = "Buscar Cliente";
            this.controlBuscarCliente.Location = new System.Drawing.Point(436, 0);
            this.controlBuscarCliente.MaxSize = new System.Drawing.Size(26, 24);
            this.controlBuscarCliente.MinSize = new System.Drawing.Size(26, 24);
            this.controlBuscarCliente.Name = "controlBuscarCliente";
            this.controlBuscarCliente.Size = new System.Drawing.Size(26, 49);
            this.controlBuscarCliente.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlBuscarCliente.Text = " ";
            this.controlBuscarCliente.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlBuscarCliente.TextSize = new System.Drawing.Size(0, 0);
            this.controlBuscarCliente.TextToControlDistance = 0;
            this.controlBuscarCliente.TextVisible = false;
            // 
            // controlArea
            // 
            this.controlArea.Control = this.lkpArea;
            this.controlArea.CustomizationFormText = "Area";
            this.controlArea.Location = new System.Drawing.Point(0, 25);
            this.controlArea.MaxSize = new System.Drawing.Size(436, 24);
            this.controlArea.MinSize = new System.Drawing.Size(436, 24);
            this.controlArea.Name = "controlArea";
            this.controlArea.Size = new System.Drawing.Size(436, 24);
            this.controlArea.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlArea.Text = "Area :";
            this.controlArea.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlArea.TextSize = new System.Drawing.Size(50, 20);
            this.controlArea.TextToControlDistance = 12;
            // 
            // controlTipoFecha
            // 
            this.controlTipoFecha.Control = this.lkpTipoFecha;
            this.controlTipoFecha.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.controlTipoFecha.CustomizationFormText = "TipoFecha";
            this.controlTipoFecha.Location = new System.Drawing.Point(861, 22);
            this.controlTipoFecha.MaxSize = new System.Drawing.Size(321, 24);
            this.controlTipoFecha.MinSize = new System.Drawing.Size(321, 24);
            this.controlTipoFecha.Name = "controlTipoFecha";
            this.controlTipoFecha.Size = new System.Drawing.Size(321, 24);
            this.controlTipoFecha.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlTipoFecha.Text = "Tipo Fecha :";
            this.controlTipoFecha.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlTipoFecha.TextLocation = DevExpress.Utils.Locations.Left;
            this.controlTipoFecha.TextSize = new System.Drawing.Size(70, 13);
            this.controlTipoFecha.TextToControlDistance = 0;
            // 
            // controlEmpresa
            // 
            this.controlEmpresa.Control = this.lkpEmpresa;
            this.controlEmpresa.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.controlEmpresa.CustomizationFormText = "Empresa";
            this.controlEmpresa.Location = new System.Drawing.Point(0, 22);
            this.controlEmpresa.MaxSize = new System.Drawing.Size(379, 24);
            this.controlEmpresa.MinSize = new System.Drawing.Size(379, 24);
            this.controlEmpresa.Name = "controlEmpresa";
            this.controlEmpresa.Size = new System.Drawing.Size(379, 24);
            this.controlEmpresa.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlEmpresa.Text = "Empresa :";
            this.controlEmpresa.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlEmpresa.TextLocation = DevExpress.Utils.Locations.Left;
            this.controlEmpresa.TextSize = new System.Drawing.Size(50, 13);
            this.controlEmpresa.TextToControlDistance = 5;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(379, 22);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(10, 0);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(10, 10);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(10, 49);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblFiltros
            // 
            this.lblFiltros.AllowHotTrack = false;
            this.lblFiltros.AppearanceItemCaption.BackColor = System.Drawing.Color.LightGray;
            this.lblFiltros.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblFiltros.AppearanceItemCaption.Options.UseBackColor = true;
            this.lblFiltros.AppearanceItemCaption.Options.UseFont = true;
            this.lblFiltros.Location = new System.Drawing.Point(0, 0);
            this.lblFiltros.Name = "lblFiltros";
            this.lblFiltros.Size = new System.Drawing.Size(1362, 22);
            this.lblFiltros.Text = "Filtros de Búsqueda";
            this.lblFiltros.TextSize = new System.Drawing.Size(147, 18);
            // 
            // controlSede
            // 
            this.controlSede.Control = this.lkpSede;
            this.controlSede.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.controlSede.CustomizationFormText = "Estado";
            this.controlSede.Location = new System.Drawing.Point(0, 46);
            this.controlSede.MaxSize = new System.Drawing.Size(379, 25);
            this.controlSede.MinSize = new System.Drawing.Size(379, 25);
            this.controlSede.Name = "controlSede";
            this.controlSede.Size = new System.Drawing.Size(379, 25);
            this.controlSede.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlSede.Text = "Sede :";
            this.controlSede.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlSede.TextLocation = DevExpress.Utils.Locations.Left;
            this.controlSede.TextSize = new System.Drawing.Size(50, 13);
            this.controlSede.TextToControlDistance = 5;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(851, 22);
            this.emptySpaceItem4.MaxSize = new System.Drawing.Size(10, 0);
            this.emptySpaceItem4.MinSize = new System.Drawing.Size(10, 10);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(10, 49);
            this.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // controlBuscar
            // 
            this.controlBuscar.Control = this.btnBuscar;
            this.controlBuscar.Location = new System.Drawing.Point(1211, 22);
            this.controlBuscar.MaxSize = new System.Drawing.Size(121, 49);
            this.controlBuscar.MinSize = new System.Drawing.Size(121, 49);
            this.controlBuscar.Name = "controlBuscar";
            this.controlBuscar.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.controlBuscar.Size = new System.Drawing.Size(121, 49);
            this.controlBuscar.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlBuscar.TextSize = new System.Drawing.Size(0, 0);
            this.controlBuscar.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(1182, 22);
            this.emptySpaceItem5.MaxSize = new System.Drawing.Size(29, 0);
            this.emptySpaceItem5.MinSize = new System.Drawing.Size(29, 10);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(29, 49);
            this.emptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // controlDesdeFecha
            // 
            this.controlDesdeFecha.Control = this.dtpDesde;
            this.controlDesdeFecha.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.controlDesdeFecha.CustomizationFormText = "Desde";
            this.controlDesdeFecha.Location = new System.Drawing.Point(861, 46);
            this.controlDesdeFecha.MaxSize = new System.Drawing.Size(176, 24);
            this.controlDesdeFecha.MinSize = new System.Drawing.Size(176, 24);
            this.controlDesdeFecha.Name = "controlDesdeFecha";
            this.controlDesdeFecha.Size = new System.Drawing.Size(176, 25);
            this.controlDesdeFecha.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlDesdeFecha.Text = "Desde :";
            this.controlDesdeFecha.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlDesdeFecha.TextLocation = DevExpress.Utils.Locations.Left;
            this.controlDesdeFecha.TextSize = new System.Drawing.Size(65, 13);
            this.controlDesdeFecha.TextToControlDistance = 5;
            // 
            // controlFechaHasta
            // 
            this.controlFechaHasta.Control = this.dtpHasta;
            this.controlFechaHasta.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.controlFechaHasta.CustomizationFormText = "Hasta";
            this.controlFechaHasta.Location = new System.Drawing.Point(1037, 46);
            this.controlFechaHasta.MaxSize = new System.Drawing.Size(145, 24);
            this.controlFechaHasta.MinSize = new System.Drawing.Size(145, 24);
            this.controlFechaHasta.Name = "controlFechaHasta";
            this.controlFechaHasta.Size = new System.Drawing.Size(145, 25);
            this.controlFechaHasta.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlFechaHasta.Text = "Hasta :";
            this.controlFechaHasta.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlFechaHasta.TextLocation = DevExpress.Utils.Locations.Left;
            this.controlFechaHasta.TextSize = new System.Drawing.Size(35, 13);
            this.controlFechaHasta.TextToControlDistance = 5;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(1332, 22);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(30, 49);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcGrillas
            // 
            this.lcGrillas.Control = this.tcRequerimientos;
            this.lcGrillas.Location = new System.Drawing.Point(0, 71);
            this.lcGrillas.Name = "lcGrillas";
            this.lcGrillas.Size = new System.Drawing.Size(1362, 497);
            this.lcGrillas.TextSize = new System.Drawing.Size(0, 0);
            this.lcGrillas.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1165, 88);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // frmListadoRequerimientos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1374, 762);
            this.Controls.Add(this.controlGeneral);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.KeyPreview = true;
            this.Name = "frmListadoRequerimientos";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Mantenimiento Requerimientos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListadoRequerimientos_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmListadoRequerimientos_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlGeneral)).EndInit();
            this.controlGeneral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tcRequerimientos)).EndInit();
            this.tcRequerimientos.ResumeLayout(false);
            this.tpReqSolicitados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcReqSolicitados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoReqSolicitados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReqSolicitados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRequerimientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.tpReqAprobados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcReqAprobados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoReqAprobados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReqAprobados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.tpReqAtendidos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcReqAtendidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoReqAtendidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReqAtendidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.tpReqAnulados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcReqAnulados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoReqAnulados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReqAnulados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEmpresa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoFecha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDesde.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDesde.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpHasta.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpHasta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCliente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSede.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGeneral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBuscarCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlTipoFecha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFiltros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSede)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBuscar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlDesdeFecha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlFechaHasta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrillas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage paginaOpciones;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoEdicion;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraLayout.LayoutControl controlGeneral;
        private DevExpress.XtraLayout.LayoutControlGroup lcGeneral;
        private DevExpress.XtraBars.BarButtonItem btnNuevoReqCompra;
        private DevExpress.XtraGrid.GridControl gcReqSolicitados;
        private DevExpress.XtraGrid.Views.Grid.GridView gvReqSolicitados;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem controlEmpresa;
        private DevExpress.XtraLayout.LayoutControlItem controlSede;
        private DevExpress.XtraLayout.LayoutControlItem controlTipoFecha;
        private DevExpress.XtraLayout.LayoutControlItem controlDesdeFecha;
        private DevExpress.XtraEditors.LookUpEdit lkpEmpresa;
        private DevExpress.XtraLayout.LayoutControlItem controlFechaHasta;
        private DevExpress.XtraEditors.LookUpEdit lkpTipoFecha;
        private DevExpress.XtraEditors.DateEdit dtpDesde;
        private DevExpress.XtraEditors.DateEdit dtpHasta;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem controlCliente;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraLayout.LayoutControlItem controlBuscar;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblFiltros;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private System.Windows.Forms.BindingSource bsListadoReqSolicitados;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_requerimiento;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_observaciones;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_justificacion;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_tipo;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoReportes;
        private DevExpress.XtraBars.BarButtonItem btnExportarExcel;
        private DevExpress.XtraBars.BarButtonItem btnImprimir;
        private DevExpress.XtraBars.BarButtonItem btnAnularRequerimiento;
        private DevExpress.XtraBars.BarButtonItem btnEliminarRequerimiento;
        private DevExpress.XtraEditors.SimpleButton btnBuscarCliente;
        private DevExpress.XtraLayout.LayoutControlItem controlBuscarCliente;
        private DevExpress.XtraEditors.TextEdit txtCliente;
        private DevExpress.XtraBars.BarButtonItem btnAprobar;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoAcciones;
        private DevExpress.XtraTab.XtraTabControl tcRequerimientos;
        private DevExpress.XtraTab.XtraTabPage tpReqSolicitados;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup lcRequerimientos;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraTab.XtraTabPage tpReqAprobados;
        private DevExpress.XtraLayout.LayoutControlItem lcGrillas;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraGrid.GridControl gcReqAprobados;
        private DevExpress.XtraGrid.Views.Grid.GridView gvReqAprobados;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraBars.BarButtonItem btnGenerarOC;
        private DevExpress.XtraEditors.LookUpEdit lkpSede;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_razon_social;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_area;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_requerimiento;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_requerimiento1;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_razon_social1;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_area1;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_tipo1;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_aprobacion;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_usuario_aprobacion;
        private System.Windows.Forms.BindingSource bsListadoReqAprobados;
        private DevExpress.XtraLayout.LayoutControlItem controlArea;
        private DevExpress.XtraEditors.LookUpEdit lkpArea;
        private DevExpress.XtraTab.XtraTabPage tpReqAtendidos;
        private DevExpress.XtraLayout.LayoutControl layoutControl4;
        private DevExpress.XtraGrid.GridControl gcReqAtendidos;
        private DevExpress.XtraGrid.Views.Grid.GridView gvReqAtendidos;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private System.Windows.Forms.BindingSource bsListadoReqAtendidos;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario_atendido;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_atendido;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_requerimiento1;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_requerimiento2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_estado_requerimiento;
        private DevExpress.XtraBars.BarButtonItem btnDesaprobar;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_justificacion1;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_justificacion2;
        private DevExpress.XtraBars.BarButtonItem btnNuevoReqServicio;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_solicitud;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_solicitud1;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_solicitud2;
        private DevExpress.XtraBars.BarButtonItem btnGenerarOS;
        private DevExpress.XtraTab.XtraTabPage tpReqAnulados;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcReqAnulados;
        private DevExpress.XtraGrid.Views.Grid.GridView gvReqAnulados;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.BindingSource bsListadoReqAnulados;
    }
}