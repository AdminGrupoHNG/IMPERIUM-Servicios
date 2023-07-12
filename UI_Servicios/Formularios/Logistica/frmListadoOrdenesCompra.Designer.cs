namespace UI_Servicios.Formularios.Logistica
{
    partial class frmListadoOrdenesCompra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListadoOrdenesCompra));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNuevaOrdCom = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.btnExportarExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnImprimir = new DevExpress.XtraBars.BarButtonItem();
            this.btnAnularOrdCom = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminarOrdCom = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.btnLiquidar = new DevExpress.XtraBars.BarButtonItem();
            this.btnAtender = new DevExpress.XtraBars.BarButtonItem();
            this.btnReporteOrdenCompra = new DevExpress.XtraBars.BarButtonItem();
            this.btnAprobar = new DevExpress.XtraBars.BarButtonItem();
            this.btnDesaprobar = new DevExpress.XtraBars.BarButtonItem();
            this.btnEnviar = new DevExpress.XtraBars.BarButtonItem();
            this.paginaOpciones = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.grupoEdicion = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grupoReportes = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grupoAcciones = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.controlListado = new DevExpress.XtraLayout.LayoutControl();
            this.tcOrdenCompra = new DevExpress.XtraTab.XtraTabControl();
            this.tpOrdenesGeneradas = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcOrdGeneradas = new DevExpress.XtraGrid.GridControl();
            this.bsListadoOrdGeneradas = new System.Windows.Forms.BindingSource(this.components);
            this.gvOrdGeneradas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.coldsc_empresa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_orden_compra = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_proveedor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_modalidad_pago = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_emision = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_total = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_almacen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_sede_empresa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_cotizacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_ruc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_direccion_despacho = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_despacho = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_registro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_cambio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario_cam = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lcOrdenesGeneradas = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tpOrdenesAprobadas = new DevExpress.XtraTab.XtraTabPage();
            this.lcOrdAprob = new DevExpress.XtraLayout.LayoutControl();
            this.gcOrdAprobadas = new DevExpress.XtraGrid.GridControl();
            this.bsListadoOrdAprobadas = new System.Windows.Forms.BindingSource(this.components);
            this.gvOrdAprobadas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_modalidad_pago1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_aprobacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario_aprobacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tpOrdenesEnviadas = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.gcOrdEnviadas = new DevExpress.XtraGrid.GridControl();
            this.bsListadoOrdEnviadas = new System.Windows.Forms.BindingSource(this.components);
            this.gvOrdEnviadas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn47 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn48 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn50 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn51 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn52 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn53 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn46 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn49 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn54 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn55 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn56 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn57 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn58 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn59 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tpOrdenesAtendidas = new DevExpress.XtraTab.XtraTabPage();
            this.lcOrdAten = new DevExpress.XtraLayout.LayoutControl();
            this.gcOrdAtendidas = new DevExpress.XtraGrid.GridControl();
            this.bsListadoOrdAtendidas = new System.Windows.Forms.BindingSource(this.components);
            this.gvOrdAtendidas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_modalidad_pago2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_estado_orden = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_atencion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario_atencion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tpOrdenesLiquidadas = new DevExpress.XtraTab.XtraTabPage();
            this.lcOrdLiq = new DevExpress.XtraLayout.LayoutControl();
            this.gcOrdLiquidadas = new DevExpress.XtraGrid.GridControl();
            this.bsListadoOrdLiquidadas = new System.Windows.Forms.BindingSource(this.components);
            this.gvOrdLiquidadas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_modalidad_pago3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_liquidacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario_liquidacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn42 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn43 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn45 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tpOrdenesAnuladas = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.gcOrdAnuladas = new DevExpress.XtraGrid.GridControl();
            this.bsListadoOrdAnuladas = new System.Windows.Forms.BindingSource(this.components);
            this.gvOrdAnuladas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn60 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn61 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn62 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn63 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn64 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn65 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn66 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn67 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn68 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn69 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn70 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn71 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn72 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn73 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn74 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn75 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn76 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup8 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnBuscarProveedor = new DevExpress.XtraEditors.SimpleButton();
            this.lkpEmpresa = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpSede = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpTipoFecha = new DevExpress.XtraEditors.LookUpEdit();
            this.dtpDesde = new DevExpress.XtraEditors.DateEdit();
            this.dtpHasta = new DevExpress.XtraEditors.DateEdit();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.txtProveedor = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.controlProveedor = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlBuscarProveedor = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.controlTipoFecha = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlEmpresa = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblFiltros = new DevExpress.XtraLayout.SimpleLabelItem();
            this.controlSede = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.controlDesdeFecha = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlFechaHasta = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlBuscar = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcGrillas = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlListado)).BeginInit();
            this.controlListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcOrdenCompra)).BeginInit();
            this.tcOrdenCompra.SuspendLayout();
            this.tpOrdenesGeneradas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOrdGeneradas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoOrdGeneradas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrdGeneradas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrdenesGeneradas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.tpOrdenesAprobadas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrdAprob)).BeginInit();
            this.lcOrdAprob.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOrdAprobadas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoOrdAprobadas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrdAprobadas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.tpOrdenesEnviadas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOrdEnviadas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoOrdEnviadas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrdEnviadas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.tpOrdenesAtendidas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrdAten)).BeginInit();
            this.lcOrdAten.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOrdAtendidas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoOrdAtendidas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrdAtendidas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.tpOrdenesLiquidadas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrdLiq)).BeginInit();
            this.lcOrdLiq.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOrdLiquidadas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoOrdLiquidadas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrdLiquidadas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.tpOrdenesAnuladas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOrdAnuladas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoOrdAnuladas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrdAnuladas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEmpresa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSede.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoFecha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDesde.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDesde.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpHasta.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpHasta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProveedor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlProveedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBuscarProveedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlTipoFecha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFiltros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSede)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlDesdeFecha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlFechaHasta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBuscar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
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
            this.btnNuevaOrdCom,
            this.barStaticItem1,
            this.btnExportarExcel,
            this.btnImprimir,
            this.btnAnularOrdCom,
            this.btnEliminarOrdCom,
            this.barButtonItem1,
            this.btnLiquidar,
            this.btnAtender,
            this.btnReporteOrdenCompra,
            this.btnAprobar,
            this.btnDesaprobar,
            this.btnEnviar});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 15;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.paginaOpciones});
            this.ribbon.Size = new System.Drawing.Size(1376, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnNuevaOrdCom
            // 
            this.btnNuevaOrdCom.Caption = "Nueva Orden de Compra";
            this.btnNuevaOrdCom.Id = 1;
            this.btnNuevaOrdCom.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevaOrdCom.ImageOptions.Image")));
            this.btnNuevaOrdCom.Name = "btnNuevaOrdCom";
            this.btnNuevaOrdCom.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnNuevaOrdCom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNuevaOrdCom_ItemClick);
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
            // btnAnularOrdCom
            // 
            this.btnAnularOrdCom.Caption = "Anular Orden de Compra";
            this.btnAnularOrdCom.Enabled = false;
            this.btnAnularOrdCom.Id = 6;
            this.btnAnularOrdCom.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAnularOrdCom.ImageOptions.Image")));
            this.btnAnularOrdCom.Name = "btnAnularOrdCom";
            this.btnAnularOrdCom.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnAnularOrdCom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAnularOrdCom_ItemClick);
            // 
            // btnEliminarOrdCom
            // 
            this.btnEliminarOrdCom.Caption = "Eliminar Orden de Compra";
            this.btnEliminarOrdCom.Enabled = false;
            this.btnEliminarOrdCom.Id = 7;
            this.btnEliminarOrdCom.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarOrdCom.ImageOptions.Image")));
            this.btnEliminarOrdCom.Name = "btnEliminarOrdCom";
            this.btnEliminarOrdCom.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnEliminarOrdCom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEliminarOrdCom_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 8;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnLiquidar
            // 
            this.btnLiquidar.Caption = "Liquidar Orden de Compra";
            this.btnLiquidar.Enabled = false;
            this.btnLiquidar.Id = 9;
            this.btnLiquidar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLiquidar.ImageOptions.Image")));
            this.btnLiquidar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnLiquidar.ImageOptions.LargeImage")));
            this.btnLiquidar.Name = "btnLiquidar";
            this.btnLiquidar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnLiquidar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLiquidar_ItemClick);
            // 
            // btnAtender
            // 
            this.btnAtender.Caption = "Atender Orden de Compra";
            this.btnAtender.Enabled = false;
            this.btnAtender.Id = 10;
            this.btnAtender.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAtender.ImageOptions.Image")));
            this.btnAtender.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAtender.ImageOptions.LargeImage")));
            this.btnAtender.Name = "btnAtender";
            this.btnAtender.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnAtender.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAtender_ItemClick);
            // 
            // btnReporteOrdenCompra
            // 
            this.btnReporteOrdenCompra.Caption = "Generar Orden de Compra";
            this.btnReporteOrdenCompra.Enabled = false;
            this.btnReporteOrdenCompra.Id = 11;
            this.btnReporteOrdenCompra.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnReporteOrdenCompra.ImageOptions.Image")));
            this.btnReporteOrdenCompra.Name = "btnReporteOrdenCompra";
            this.btnReporteOrdenCompra.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnReporteOrdenCompra.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnReporteOrdenCompra_ItemClick);
            // 
            // btnAprobar
            // 
            this.btnAprobar.Caption = "Aprobar Orden de Compra";
            this.btnAprobar.Id = 12;
            this.btnAprobar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAprobar.ImageOptions.Image")));
            this.btnAprobar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAprobar.ImageOptions.LargeImage")));
            this.btnAprobar.Name = "btnAprobar";
            this.btnAprobar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnAprobar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAprobar_ItemClick);
            // 
            // btnDesaprobar
            // 
            this.btnDesaprobar.Caption = "Desaprobar Orden de Compra";
            this.btnDesaprobar.Enabled = false;
            this.btnDesaprobar.Id = 13;
            this.btnDesaprobar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDesaprobar.ImageOptions.Image")));
            this.btnDesaprobar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDesaprobar.ImageOptions.LargeImage")));
            this.btnDesaprobar.Name = "btnDesaprobar";
            this.btnDesaprobar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnDesaprobar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDesaprobar_ItemClick);
            // 
            // btnEnviar
            // 
            this.btnEnviar.Caption = "Enviar Orden de Compra";
            this.btnEnviar.Enabled = false;
            this.btnEnviar.Id = 14;
            this.btnEnviar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEnviar.ImageOptions.Image")));
            this.btnEnviar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnEnviar.ImageOptions.LargeImage")));
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnEnviar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEnviar_ItemClick);
            // 
            // paginaOpciones
            // 
            this.paginaOpciones.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grupoEdicion,
            this.grupoReportes,
            this.grupoAcciones});
            this.paginaOpciones.Name = "paginaOpciones";
            this.paginaOpciones.Text = "Opciones Ordenes Compra";
            // 
            // grupoEdicion
            // 
            this.grupoEdicion.ItemLinks.Add(this.btnNuevaOrdCom);
            this.grupoEdicion.ItemLinks.Add(this.btnAnularOrdCom);
            this.grupoEdicion.ItemLinks.Add(this.btnEliminarOrdCom);
            this.grupoEdicion.Name = "grupoEdicion";
            this.grupoEdicion.Text = "Edición";
            // 
            // grupoReportes
            // 
            this.grupoReportes.ItemLinks.Add(this.btnExportarExcel);
            this.grupoReportes.ItemLinks.Add(this.btnImprimir);
            this.grupoReportes.ItemLinks.Add(this.btnReporteOrdenCompra);
            this.grupoReportes.Name = "grupoReportes";
            this.grupoReportes.Text = "Reportes";
            // 
            // grupoAcciones
            // 
            this.grupoAcciones.ItemLinks.Add(this.btnAprobar);
            this.grupoAcciones.ItemLinks.Add(this.btnDesaprobar);
            this.grupoAcciones.ItemLinks.Add(this.btnEnviar);
            this.grupoAcciones.ItemLinks.Add(this.btnAtender);
            this.grupoAcciones.ItemLinks.Add(this.btnLiquidar);
            this.grupoAcciones.Name = "grupoAcciones";
            this.grupoAcciones.Text = "Acciones";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItem1);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 763);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1376, 24);
            // 
            // controlListado
            // 
            this.controlListado.Controls.Add(this.tcOrdenCompra);
            this.controlListado.Controls.Add(this.btnBuscarProveedor);
            this.controlListado.Controls.Add(this.lkpEmpresa);
            this.controlListado.Controls.Add(this.lkpSede);
            this.controlListado.Controls.Add(this.lkpTipoFecha);
            this.controlListado.Controls.Add(this.dtpDesde);
            this.controlListado.Controls.Add(this.dtpHasta);
            this.controlListado.Controls.Add(this.btnBuscar);
            this.controlListado.Controls.Add(this.txtProveedor);
            this.controlListado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlListado.Location = new System.Drawing.Point(0, 158);
            this.controlListado.Name = "controlListado";
            this.controlListado.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2636, 239, 650, 400);
            this.controlListado.Root = this.Root;
            this.controlListado.Size = new System.Drawing.Size(1376, 605);
            this.controlListado.TabIndex = 2;
            this.controlListado.Text = "layoutControl1";
            // 
            // tcOrdenCompra
            // 
            this.tcOrdenCompra.Location = new System.Drawing.Point(8, 78);
            this.tcOrdenCompra.Name = "tcOrdenCompra";
            this.tcOrdenCompra.SelectedTabPage = this.tpOrdenesGeneradas;
            this.tcOrdenCompra.Size = new System.Drawing.Size(1360, 519);
            this.tcOrdenCompra.TabIndex = 10;
            this.tcOrdenCompra.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpOrdenesGeneradas,
            this.tpOrdenesAprobadas,
            this.tpOrdenesEnviadas,
            this.tpOrdenesAtendidas,
            this.tpOrdenesLiquidadas,
            this.tpOrdenesAnuladas});
            this.tcOrdenCompra.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tcOrdenCompra_SelectedPageChanged);
            // 
            // tpOrdenesGeneradas
            // 
            this.tpOrdenesGeneradas.Controls.Add(this.layoutControl1);
            this.tpOrdenesGeneradas.Name = "tpOrdenesGeneradas";
            this.tpOrdenesGeneradas.Size = new System.Drawing.Size(1358, 494);
            this.tpOrdenesGeneradas.Text = "Ordenes Generadas";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcOrdGeneradas);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.lcOrdenesGeneradas;
            this.layoutControl1.Size = new System.Drawing.Size(1358, 494);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcOrdGeneradas
            // 
            this.gcOrdGeneradas.DataSource = this.bsListadoOrdGeneradas;
            this.gcOrdGeneradas.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcOrdGeneradas.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcOrdGeneradas.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcOrdGeneradas.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcOrdGeneradas.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcOrdGeneradas.Location = new System.Drawing.Point(12, 12);
            this.gcOrdGeneradas.MainView = this.gvOrdGeneradas;
            this.gcOrdGeneradas.MenuManager = this.ribbon;
            this.gcOrdGeneradas.Name = "gcOrdGeneradas";
            this.gcOrdGeneradas.Padding = new System.Windows.Forms.Padding(6);
            this.gcOrdGeneradas.Size = new System.Drawing.Size(1334, 470);
            this.gcOrdGeneradas.TabIndex = 4;
            this.gcOrdGeneradas.UseEmbeddedNavigator = true;
            this.gcOrdGeneradas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOrdGeneradas});
            // 
            // bsListadoOrdGeneradas
            // 
            this.bsListadoOrdGeneradas.DataSource = typeof(BE_Servicios.eOrdenCompra_Servicio);
            // 
            // gvOrdGeneradas
            // 
            this.gvOrdGeneradas.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Blue;
            this.gvOrdGeneradas.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvOrdGeneradas.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvOrdGeneradas.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvOrdGeneradas.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvOrdGeneradas.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvOrdGeneradas.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvOrdGeneradas.ColumnPanelRowHeight = 35;
            this.gvOrdGeneradas.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.coldsc_empresa,
            this.colcod_orden_compra,
            this.coldsc_proveedor,
            this.coldsc_modalidad_pago,
            this.colfch_emision,
            this.colimp_total,
            this.coldsc_almacen,
            this.coldsc_sede_empresa,
            this.colnum_cotizacion,
            this.coldsc_ruc,
            this.coldsc_direccion_despacho,
            this.colfch_despacho,
            this.colfch_registro,
            this.coldsc_usuario,
            this.colfch_cambio,
            this.coldsc_usuario_cam});
            this.gvOrdGeneradas.GridControl = this.gcOrdGeneradas;
            this.gvOrdGeneradas.Name = "gvOrdGeneradas";
            this.gvOrdGeneradas.OptionsBehavior.Editable = false;
            this.gvOrdGeneradas.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvOrdGeneradas.OptionsSelection.MultiSelect = true;
            this.gvOrdGeneradas.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvOrdGeneradas.OptionsView.EnableAppearanceEvenRow = true;
            this.gvOrdGeneradas.OptionsView.ShowAutoFilterRow = true;
            this.gvOrdGeneradas.OptionsView.ShowFooter = true;
            this.gvOrdGeneradas.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvOrdGeneradas_RowClick);
            this.gvOrdGeneradas.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvOrdGeneradas_CustomDrawColumnHeader);
            this.gvOrdGeneradas.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvOrdGeneradas_CustomDrawCell);
            this.gvOrdGeneradas.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvOrdGeneradas_RowCellStyle);
            this.gvOrdGeneradas.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvOrdGeneradas_RowStyle);
            this.gvOrdGeneradas.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gvOrdenesCompra_SelectionChanged);
            // 
            // coldsc_empresa
            // 
            this.coldsc_empresa.Caption = "Empresa";
            this.coldsc_empresa.FieldName = "dsc_empresa";
            this.coldsc_empresa.Name = "coldsc_empresa";
            this.coldsc_empresa.OptionsColumn.FixedWidth = true;
            this.coldsc_empresa.Visible = true;
            this.coldsc_empresa.VisibleIndex = 1;
            this.coldsc_empresa.Width = 180;
            // 
            // colcod_orden_compra
            // 
            this.colcod_orden_compra.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_orden_compra.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_orden_compra.Caption = "Ord. Compra";
            this.colcod_orden_compra.FieldName = "cod_orden_compra_servicio";
            this.colcod_orden_compra.Name = "colcod_orden_compra";
            this.colcod_orden_compra.OptionsColumn.FixedWidth = true;
            this.colcod_orden_compra.Visible = true;
            this.colcod_orden_compra.VisibleIndex = 2;
            this.colcod_orden_compra.Width = 80;
            // 
            // coldsc_proveedor
            // 
            this.coldsc_proveedor.Caption = "Proveedor";
            this.coldsc_proveedor.FieldName = "dsc_proveedor";
            this.coldsc_proveedor.Name = "coldsc_proveedor";
            this.coldsc_proveedor.OptionsColumn.FixedWidth = true;
            this.coldsc_proveedor.Visible = true;
            this.coldsc_proveedor.VisibleIndex = 3;
            this.coldsc_proveedor.Width = 180;
            // 
            // coldsc_modalidad_pago
            // 
            this.coldsc_modalidad_pago.Caption = "Modalidad de Pago";
            this.coldsc_modalidad_pago.FieldName = "dsc_modalidad_pago";
            this.coldsc_modalidad_pago.Name = "coldsc_modalidad_pago";
            this.coldsc_modalidad_pago.OptionsColumn.FixedWidth = true;
            this.coldsc_modalidad_pago.Visible = true;
            this.coldsc_modalidad_pago.VisibleIndex = 4;
            this.coldsc_modalidad_pago.Width = 100;
            // 
            // colfch_emision
            // 
            this.colfch_emision.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_emision.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_emision.Caption = "Fecha Emision";
            this.colfch_emision.FieldName = "fch_emision";
            this.colfch_emision.Name = "colfch_emision";
            this.colfch_emision.OptionsColumn.FixedWidth = true;
            this.colfch_emision.Visible = true;
            this.colfch_emision.VisibleIndex = 5;
            this.colfch_emision.Width = 80;
            // 
            // colimp_total
            // 
            this.colimp_total.Caption = "Imp. Total";
            this.colimp_total.FieldName = "imp_total";
            this.colimp_total.Name = "colimp_total";
            this.colimp_total.OptionsColumn.FixedWidth = true;
            this.colimp_total.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, DevExpress.Data.SummaryMode.Mixed, "imp_total", "{0:#,#.##}")});
            this.colimp_total.Visible = true;
            this.colimp_total.VisibleIndex = 6;
            this.colimp_total.Width = 70;
            // 
            // coldsc_almacen
            // 
            this.coldsc_almacen.Caption = "Almacen";
            this.coldsc_almacen.FieldName = "dsc_almacen";
            this.coldsc_almacen.Name = "coldsc_almacen";
            this.coldsc_almacen.OptionsColumn.FixedWidth = true;
            this.coldsc_almacen.Visible = true;
            this.coldsc_almacen.VisibleIndex = 7;
            this.coldsc_almacen.Width = 180;
            // 
            // coldsc_sede_empresa
            // 
            this.coldsc_sede_empresa.Caption = "Sede";
            this.coldsc_sede_empresa.FieldName = "dsc_sede_empresa";
            this.coldsc_sede_empresa.Name = "coldsc_sede_empresa";
            this.coldsc_sede_empresa.OptionsColumn.FixedWidth = true;
            this.coldsc_sede_empresa.Width = 100;
            // 
            // colnum_cotizacion
            // 
            this.colnum_cotizacion.Caption = "Cotizacion";
            this.colnum_cotizacion.FieldName = "num_cotizacion";
            this.colnum_cotizacion.Name = "colnum_cotizacion";
            this.colnum_cotizacion.OptionsColumn.FixedWidth = true;
            this.colnum_cotizacion.Width = 60;
            // 
            // coldsc_ruc
            // 
            this.coldsc_ruc.Caption = "RUC";
            this.coldsc_ruc.FieldName = "dsc_ruc";
            this.coldsc_ruc.Name = "coldsc_ruc";
            this.coldsc_ruc.OptionsColumn.FixedWidth = true;
            // 
            // coldsc_direccion_despacho
            // 
            this.coldsc_direccion_despacho.Caption = "Despacho";
            this.coldsc_direccion_despacho.FieldName = "dsc_direccion_despacho";
            this.coldsc_direccion_despacho.Name = "coldsc_direccion_despacho";
            this.coldsc_direccion_despacho.OptionsColumn.FixedWidth = true;
            // 
            // colfch_despacho
            // 
            this.colfch_despacho.Caption = "Fecha Despacho";
            this.colfch_despacho.FieldName = "fch_despacho";
            this.colfch_despacho.Name = "colfch_despacho";
            this.colfch_despacho.OptionsColumn.FixedWidth = true;
            // 
            // colfch_registro
            // 
            this.colfch_registro.Caption = "Fecha registro";
            this.colfch_registro.FieldName = "fch_registro";
            this.colfch_registro.Name = "colfch_registro";
            this.colfch_registro.OptionsColumn.FixedWidth = true;
            // 
            // coldsc_usuario
            // 
            this.coldsc_usuario.Caption = "Usuario Registro";
            this.coldsc_usuario.FieldName = "dsc_usuario";
            this.coldsc_usuario.Name = "coldsc_usuario";
            this.coldsc_usuario.OptionsColumn.FixedWidth = true;
            // 
            // colfch_cambio
            // 
            this.colfch_cambio.Caption = "Fecha Cambio";
            this.colfch_cambio.FieldName = "fch_cambio";
            this.colfch_cambio.Name = "colfch_cambio";
            this.colfch_cambio.OptionsColumn.FixedWidth = true;
            // 
            // coldsc_usuario_cam
            // 
            this.coldsc_usuario_cam.Caption = "Usuario Cambio";
            this.coldsc_usuario_cam.FieldName = "dsc_usuario_cam";
            this.coldsc_usuario_cam.Name = "coldsc_usuario_cam";
            this.coldsc_usuario_cam.OptionsColumn.FixedWidth = true;
            // 
            // lcOrdenesGeneradas
            // 
            this.lcOrdenesGeneradas.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcOrdenesGeneradas.GroupBordersVisible = false;
            this.lcOrdenesGeneradas.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.lcOrdenesGeneradas.Name = "lcOrdenesGeneradas";
            this.lcOrdenesGeneradas.Size = new System.Drawing.Size(1358, 494);
            this.lcOrdenesGeneradas.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcOrdGeneradas;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1338, 474);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // tpOrdenesAprobadas
            // 
            this.tpOrdenesAprobadas.Controls.Add(this.lcOrdAprob);
            this.tpOrdenesAprobadas.Name = "tpOrdenesAprobadas";
            this.tpOrdenesAprobadas.Size = new System.Drawing.Size(1358, 494);
            this.tpOrdenesAprobadas.Text = "Ordenes Aprobadas";
            // 
            // lcOrdAprob
            // 
            this.lcOrdAprob.Controls.Add(this.gcOrdAprobadas);
            this.lcOrdAprob.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcOrdAprob.Location = new System.Drawing.Point(0, 0);
            this.lcOrdAprob.Name = "lcOrdAprob";
            this.lcOrdAprob.Root = this.layoutControlGroup4;
            this.lcOrdAprob.Size = new System.Drawing.Size(1358, 494);
            this.lcOrdAprob.TabIndex = 0;
            this.lcOrdAprob.Text = "layoutControl2";
            // 
            // gcOrdAprobadas
            // 
            this.gcOrdAprobadas.DataSource = this.bsListadoOrdAprobadas;
            this.gcOrdAprobadas.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcOrdAprobadas.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcOrdAprobadas.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcOrdAprobadas.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcOrdAprobadas.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcOrdAprobadas.Location = new System.Drawing.Point(12, 12);
            this.gcOrdAprobadas.MainView = this.gvOrdAprobadas;
            this.gcOrdAprobadas.MenuManager = this.ribbon;
            this.gcOrdAprobadas.Name = "gcOrdAprobadas";
            this.gcOrdAprobadas.Padding = new System.Windows.Forms.Padding(6);
            this.gcOrdAprobadas.Size = new System.Drawing.Size(1334, 470);
            this.gcOrdAprobadas.TabIndex = 5;
            this.gcOrdAprobadas.UseEmbeddedNavigator = true;
            this.gcOrdAprobadas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOrdAprobadas});
            // 
            // bsListadoOrdAprobadas
            // 
            this.bsListadoOrdAprobadas.DataSource = typeof(BE_Servicios.eOrdenCompra_Servicio);
            // 
            // gvOrdAprobadas
            // 
            this.gvOrdAprobadas.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Blue;
            this.gvOrdAprobadas.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvOrdAprobadas.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvOrdAprobadas.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvOrdAprobadas.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvOrdAprobadas.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvOrdAprobadas.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvOrdAprobadas.ColumnPanelRowHeight = 30;
            this.gvOrdAprobadas.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn5,
            this.coldsc_modalidad_pago1,
            this.gridColumn7,
            this.gridColumn9,
            this.colfch_aprobacion,
            this.coldsc_usuario_aprobacion,
            this.gridColumn2,
            this.gridColumn4,
            this.gridColumn6,
            this.gridColumn11,
            this.gridColumn10,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15});
            this.gvOrdAprobadas.GridControl = this.gcOrdAprobadas;
            this.gvOrdAprobadas.Name = "gvOrdAprobadas";
            this.gvOrdAprobadas.OptionsBehavior.Editable = false;
            this.gvOrdAprobadas.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvOrdAprobadas.OptionsSelection.MultiSelect = true;
            this.gvOrdAprobadas.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvOrdAprobadas.OptionsView.EnableAppearanceEvenRow = true;
            this.gvOrdAprobadas.OptionsView.ShowAutoFilterRow = true;
            this.gvOrdAprobadas.OptionsView.ShowFooter = true;
            this.gvOrdAprobadas.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvOrdAprobadas_RowClick);
            this.gvOrdAprobadas.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvOrdAprobadas_CustomDrawColumnHeader);
            this.gvOrdAprobadas.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvOrdAprobadas_CustomDrawCell);
            this.gvOrdAprobadas.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvOrdAprobadas_RowCellStyle);
            this.gvOrdAprobadas.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvOrdAprobadas_RowStyle);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Empresa";
            this.gridColumn1.FieldName = "dsc_empresa";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 180;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Ord. Compra";
            this.gridColumn3.FieldName = "cod_orden_compra_servicio";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 80;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Proveedor";
            this.gridColumn5.FieldName = "dsc_proveedor";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.FixedWidth = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 180;
            // 
            // coldsc_modalidad_pago1
            // 
            this.coldsc_modalidad_pago1.Caption = "Modalidad de Pago";
            this.coldsc_modalidad_pago1.FieldName = "dsc_modalidad_pago";
            this.coldsc_modalidad_pago1.Name = "coldsc_modalidad_pago1";
            this.coldsc_modalidad_pago1.OptionsColumn.FixedWidth = true;
            this.coldsc_modalidad_pago1.Visible = true;
            this.coldsc_modalidad_pago1.VisibleIndex = 4;
            this.coldsc_modalidad_pago1.Width = 100;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "Fecha Emision";
            this.gridColumn7.FieldName = "fch_emision";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.FixedWidth = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            this.gridColumn7.Width = 80;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Imp. Total";
            this.gridColumn9.FieldName = "imp_total";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.FixedWidth = true;
            this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, DevExpress.Data.SummaryMode.Mixed, "imp_total", "{0:#,#.##}")});
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 6;
            this.gridColumn9.Width = 70;
            // 
            // colfch_aprobacion
            // 
            this.colfch_aprobacion.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_aprobacion.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_aprobacion.Caption = "Fecha Aprobacion";
            this.colfch_aprobacion.FieldName = "fch_aprobacion";
            this.colfch_aprobacion.Name = "colfch_aprobacion";
            this.colfch_aprobacion.OptionsColumn.FixedWidth = true;
            this.colfch_aprobacion.Visible = true;
            this.colfch_aprobacion.VisibleIndex = 8;
            this.colfch_aprobacion.Width = 80;
            // 
            // coldsc_usuario_aprobacion
            // 
            this.coldsc_usuario_aprobacion.Caption = "Usuario Aprobacion";
            this.coldsc_usuario_aprobacion.FieldName = "dsc_usuario_aprobacion";
            this.coldsc_usuario_aprobacion.Name = "coldsc_usuario_aprobacion";
            this.coldsc_usuario_aprobacion.OptionsColumn.FixedWidth = true;
            this.coldsc_usuario_aprobacion.Visible = true;
            this.coldsc_usuario_aprobacion.VisibleIndex = 7;
            this.coldsc_usuario_aprobacion.Width = 120;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Sede";
            this.gridColumn2.FieldName = "dsc_sede_empresa";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Width = 100;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Cotizacion";
            this.gridColumn4.FieldName = "num_cotizacion";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.FixedWidth = true;
            this.gridColumn4.Width = 60;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "RUC";
            this.gridColumn6.FieldName = "dsc_ruc";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Fecha Despacho";
            this.gridColumn11.FieldName = "fch_despacho";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Despacho";
            this.gridColumn10.FieldName = "dsc_direccion_despacho";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Fecha registro";
            this.gridColumn12.FieldName = "fch_registro";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Usuario Registro";
            this.gridColumn13.FieldName = "dsc_usuario";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Fecha Cambio";
            this.gridColumn14.FieldName = "fch_cambio";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Usuario Cambio";
            this.gridColumn15.FieldName = "dsc_usuario_cam";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.FixedWidth = true;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup4.GroupBordersVisible = false;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(1358, 494);
            this.layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcOrdAprobadas;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1338, 474);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // tpOrdenesEnviadas
            // 
            this.tpOrdenesEnviadas.Controls.Add(this.layoutControl2);
            this.tpOrdenesEnviadas.Name = "tpOrdenesEnviadas";
            this.tpOrdenesEnviadas.Size = new System.Drawing.Size(1358, 494);
            this.tpOrdenesEnviadas.Text = "Ordenes Enviadas";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.gcOrdEnviadas);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup7;
            this.layoutControl2.Size = new System.Drawing.Size(1358, 494);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // gcOrdEnviadas
            // 
            this.gcOrdEnviadas.DataSource = this.bsListadoOrdEnviadas;
            this.gcOrdEnviadas.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcOrdEnviadas.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcOrdEnviadas.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcOrdEnviadas.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcOrdEnviadas.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcOrdEnviadas.Location = new System.Drawing.Point(12, 12);
            this.gcOrdEnviadas.MainView = this.gvOrdEnviadas;
            this.gcOrdEnviadas.MenuManager = this.ribbon;
            this.gcOrdEnviadas.Name = "gcOrdEnviadas";
            this.gcOrdEnviadas.Padding = new System.Windows.Forms.Padding(6);
            this.gcOrdEnviadas.Size = new System.Drawing.Size(1334, 470);
            this.gcOrdEnviadas.TabIndex = 6;
            this.gcOrdEnviadas.UseEmbeddedNavigator = true;
            this.gcOrdEnviadas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOrdEnviadas});
            // 
            // bsListadoOrdEnviadas
            // 
            this.bsListadoOrdEnviadas.DataSource = typeof(BE_Servicios.eOrdenCompra_Servicio);
            // 
            // gvOrdEnviadas
            // 
            this.gvOrdEnviadas.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Blue;
            this.gvOrdEnviadas.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvOrdEnviadas.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvOrdEnviadas.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvOrdEnviadas.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvOrdEnviadas.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvOrdEnviadas.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvOrdEnviadas.ColumnPanelRowHeight = 30;
            this.gvOrdEnviadas.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn8,
            this.gridColumn38,
            this.gridColumn47,
            this.gridColumn48,
            this.gridColumn50,
            this.gridColumn51,
            this.gridColumn52,
            this.gridColumn53,
            this.gridColumn23,
            this.gridColumn46,
            this.gridColumn49,
            this.gridColumn54,
            this.gridColumn55,
            this.gridColumn56,
            this.gridColumn57,
            this.gridColumn58,
            this.gridColumn59});
            this.gvOrdEnviadas.GridControl = this.gcOrdEnviadas;
            this.gvOrdEnviadas.Name = "gvOrdEnviadas";
            this.gvOrdEnviadas.OptionsBehavior.Editable = false;
            this.gvOrdEnviadas.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvOrdEnviadas.OptionsSelection.MultiSelect = true;
            this.gvOrdEnviadas.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvOrdEnviadas.OptionsView.EnableAppearanceEvenRow = true;
            this.gvOrdEnviadas.OptionsView.ShowAutoFilterRow = true;
            this.gvOrdEnviadas.OptionsView.ShowFooter = true;
            this.gvOrdEnviadas.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvOrdEnviadas_RowClick);
            this.gvOrdEnviadas.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvOrdEnviadas_CustomDrawColumnHeader);
            this.gvOrdEnviadas.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvOrdEnviadas_CustomDrawCell);
            this.gvOrdEnviadas.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvOrdEnviadas_RowCellStyle);
            this.gvOrdEnviadas.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvOrdEnviadas_RowStyle);
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Empresa";
            this.gridColumn8.FieldName = "dsc_empresa";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.FixedWidth = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 1;
            this.gridColumn8.Width = 180;
            // 
            // gridColumn38
            // 
            this.gridColumn38.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn38.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn38.Caption = "Ord. Compra";
            this.gridColumn38.FieldName = "cod_orden_compra_servicio";
            this.gridColumn38.Name = "gridColumn38";
            this.gridColumn38.OptionsColumn.FixedWidth = true;
            this.gridColumn38.Visible = true;
            this.gridColumn38.VisibleIndex = 2;
            this.gridColumn38.Width = 80;
            // 
            // gridColumn47
            // 
            this.gridColumn47.Caption = "Proveedor";
            this.gridColumn47.FieldName = "dsc_proveedor";
            this.gridColumn47.Name = "gridColumn47";
            this.gridColumn47.OptionsColumn.FixedWidth = true;
            this.gridColumn47.Visible = true;
            this.gridColumn47.VisibleIndex = 3;
            this.gridColumn47.Width = 180;
            // 
            // gridColumn48
            // 
            this.gridColumn48.Caption = "Modalidad de Pago";
            this.gridColumn48.FieldName = "dsc_modalidad_pago";
            this.gridColumn48.Name = "gridColumn48";
            this.gridColumn48.OptionsColumn.FixedWidth = true;
            this.gridColumn48.Visible = true;
            this.gridColumn48.VisibleIndex = 4;
            this.gridColumn48.Width = 100;
            // 
            // gridColumn50
            // 
            this.gridColumn50.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn50.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn50.Caption = "Fecha Emision";
            this.gridColumn50.FieldName = "fch_emision";
            this.gridColumn50.Name = "gridColumn50";
            this.gridColumn50.OptionsColumn.FixedWidth = true;
            this.gridColumn50.Visible = true;
            this.gridColumn50.VisibleIndex = 5;
            this.gridColumn50.Width = 80;
            // 
            // gridColumn51
            // 
            this.gridColumn51.Caption = "Imp. Total";
            this.gridColumn51.FieldName = "imp_total";
            this.gridColumn51.Name = "gridColumn51";
            this.gridColumn51.OptionsColumn.FixedWidth = true;
            this.gridColumn51.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, DevExpress.Data.SummaryMode.Mixed, "imp_total", "{0:#,#.##}")});
            this.gridColumn51.Visible = true;
            this.gridColumn51.VisibleIndex = 6;
            this.gridColumn51.Width = 70;
            // 
            // gridColumn52
            // 
            this.gridColumn52.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn52.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn52.Caption = "Fecha Envio";
            this.gridColumn52.FieldName = "fch_envio";
            this.gridColumn52.Name = "gridColumn52";
            this.gridColumn52.OptionsColumn.FixedWidth = true;
            this.gridColumn52.Visible = true;
            this.gridColumn52.VisibleIndex = 8;
            this.gridColumn52.Width = 80;
            // 
            // gridColumn53
            // 
            this.gridColumn53.Caption = "Usuario Envio";
            this.gridColumn53.FieldName = "dsc_usuario_envio";
            this.gridColumn53.Name = "gridColumn53";
            this.gridColumn53.OptionsColumn.FixedWidth = true;
            this.gridColumn53.Visible = true;
            this.gridColumn53.VisibleIndex = 7;
            this.gridColumn53.Width = 120;
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "Sede";
            this.gridColumn23.FieldName = "dsc_sede_empresa";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.OptionsColumn.FixedWidth = true;
            this.gridColumn23.Width = 100;
            // 
            // gridColumn46
            // 
            this.gridColumn46.Caption = "Cotizacion";
            this.gridColumn46.FieldName = "num_cotizacion";
            this.gridColumn46.Name = "gridColumn46";
            this.gridColumn46.OptionsColumn.FixedWidth = true;
            this.gridColumn46.Width = 60;
            // 
            // gridColumn49
            // 
            this.gridColumn49.Caption = "RUC";
            this.gridColumn49.FieldName = "dsc_ruc";
            this.gridColumn49.Name = "gridColumn49";
            this.gridColumn49.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn54
            // 
            this.gridColumn54.Caption = "Fecha Despacho";
            this.gridColumn54.FieldName = "fch_despacho";
            this.gridColumn54.Name = "gridColumn54";
            this.gridColumn54.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn55
            // 
            this.gridColumn55.Caption = "Despacho";
            this.gridColumn55.FieldName = "dsc_direccion_despacho";
            this.gridColumn55.Name = "gridColumn55";
            this.gridColumn55.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn56
            // 
            this.gridColumn56.Caption = "Fecha registro";
            this.gridColumn56.FieldName = "fch_registro";
            this.gridColumn56.Name = "gridColumn56";
            this.gridColumn56.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn57
            // 
            this.gridColumn57.Caption = "Usuario Registro";
            this.gridColumn57.FieldName = "dsc_usuario";
            this.gridColumn57.Name = "gridColumn57";
            this.gridColumn57.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn58
            // 
            this.gridColumn58.Caption = "Fecha Cambio";
            this.gridColumn58.FieldName = "fch_cambio";
            this.gridColumn58.Name = "gridColumn58";
            this.gridColumn58.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn59
            // 
            this.gridColumn59.Caption = "Usuario Cambio";
            this.gridColumn59.FieldName = "dsc_usuario_cam";
            this.gridColumn59.Name = "gridColumn59";
            this.gridColumn59.OptionsColumn.FixedWidth = true;
            // 
            // layoutControlGroup7
            // 
            this.layoutControlGroup7.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup7.GroupBordersVisible = false;
            this.layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.Size = new System.Drawing.Size(1358, 494);
            this.layoutControlGroup7.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.gcOrdEnviadas;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(1338, 474);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // tpOrdenesAtendidas
            // 
            this.tpOrdenesAtendidas.Controls.Add(this.lcOrdAten);
            this.tpOrdenesAtendidas.Name = "tpOrdenesAtendidas";
            this.tpOrdenesAtendidas.Size = new System.Drawing.Size(1358, 494);
            this.tpOrdenesAtendidas.Text = "Ordenes Atendidas";
            // 
            // lcOrdAten
            // 
            this.lcOrdAten.Controls.Add(this.gcOrdAtendidas);
            this.lcOrdAten.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcOrdAten.Location = new System.Drawing.Point(0, 0);
            this.lcOrdAten.Name = "lcOrdAten";
            this.lcOrdAten.Root = this.layoutControlGroup5;
            this.lcOrdAten.Size = new System.Drawing.Size(1358, 494);
            this.lcOrdAten.TabIndex = 0;
            this.lcOrdAten.Text = "layoutControl2";
            // 
            // gcOrdAtendidas
            // 
            this.gcOrdAtendidas.DataSource = this.bsListadoOrdAtendidas;
            this.gcOrdAtendidas.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcOrdAtendidas.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcOrdAtendidas.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcOrdAtendidas.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcOrdAtendidas.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcOrdAtendidas.Location = new System.Drawing.Point(12, 12);
            this.gcOrdAtendidas.MainView = this.gvOrdAtendidas;
            this.gcOrdAtendidas.MenuManager = this.ribbon;
            this.gcOrdAtendidas.Name = "gcOrdAtendidas";
            this.gcOrdAtendidas.Padding = new System.Windows.Forms.Padding(6);
            this.gcOrdAtendidas.Size = new System.Drawing.Size(1334, 470);
            this.gcOrdAtendidas.TabIndex = 5;
            this.gcOrdAtendidas.UseEmbeddedNavigator = true;
            this.gcOrdAtendidas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOrdAtendidas});
            // 
            // bsListadoOrdAtendidas
            // 
            this.bsListadoOrdAtendidas.DataSource = typeof(BE_Servicios.eOrdenCompra_Servicio);
            // 
            // gvOrdAtendidas
            // 
            this.gvOrdAtendidas.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Blue;
            this.gvOrdAtendidas.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvOrdAtendidas.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvOrdAtendidas.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvOrdAtendidas.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvOrdAtendidas.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvOrdAtendidas.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvOrdAtendidas.ColumnPanelRowHeight = 30;
            this.gvOrdAtendidas.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn16,
            this.gridColumn18,
            this.gridColumn20,
            this.coldsc_modalidad_pago2,
            this.colcod_estado_orden,
            this.gridColumn22,
            this.gridColumn24,
            this.colfch_atencion,
            this.coldsc_usuario_atencion,
            this.gridColumn17,
            this.gridColumn19,
            this.gridColumn21,
            this.gridColumn26,
            this.gridColumn25,
            this.gridColumn27,
            this.gridColumn28,
            this.gridColumn29,
            this.gridColumn30});
            this.gvOrdAtendidas.GridControl = this.gcOrdAtendidas;
            this.gvOrdAtendidas.Name = "gvOrdAtendidas";
            this.gvOrdAtendidas.OptionsBehavior.Editable = false;
            this.gvOrdAtendidas.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvOrdAtendidas.OptionsSelection.MultiSelect = true;
            this.gvOrdAtendidas.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvOrdAtendidas.OptionsView.EnableAppearanceEvenRow = true;
            this.gvOrdAtendidas.OptionsView.ShowAutoFilterRow = true;
            this.gvOrdAtendidas.OptionsView.ShowFooter = true;
            this.gvOrdAtendidas.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvOrdAtendidas_RowClick);
            this.gvOrdAtendidas.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvOrdAtendidas_CustomDrawColumnHeader);
            this.gvOrdAtendidas.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvOrdAtendidas_CustomDrawCell);
            this.gvOrdAtendidas.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvOrdAtendidas_RowCellStyle);
            this.gvOrdAtendidas.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvOrdAtendidas_RowStyle);
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Empresa";
            this.gridColumn16.FieldName = "dsc_empresa";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.FixedWidth = true;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 1;
            this.gridColumn16.Width = 180;
            // 
            // gridColumn18
            // 
            this.gridColumn18.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn18.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn18.Caption = "Ord. Compra";
            this.gridColumn18.FieldName = "cod_orden_compra_servicio";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.OptionsColumn.FixedWidth = true;
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 2;
            this.gridColumn18.Width = 80;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "Proveedor";
            this.gridColumn20.FieldName = "dsc_proveedor";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.OptionsColumn.FixedWidth = true;
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 3;
            this.gridColumn20.Width = 180;
            // 
            // coldsc_modalidad_pago2
            // 
            this.coldsc_modalidad_pago2.Caption = "Modalidad de Pago";
            this.coldsc_modalidad_pago2.FieldName = "dsc_modalidad_pago";
            this.coldsc_modalidad_pago2.Name = "coldsc_modalidad_pago2";
            this.coldsc_modalidad_pago2.OptionsColumn.FixedWidth = true;
            this.coldsc_modalidad_pago2.Visible = true;
            this.coldsc_modalidad_pago2.VisibleIndex = 4;
            this.coldsc_modalidad_pago2.Width = 100;
            // 
            // colcod_estado_orden
            // 
            this.colcod_estado_orden.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colcod_estado_orden.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.colcod_estado_orden.AppearanceCell.Options.UseFont = true;
            this.colcod_estado_orden.AppearanceCell.Options.UseForeColor = true;
            this.colcod_estado_orden.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_estado_orden.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_estado_orden.Caption = "Estado";
            this.colcod_estado_orden.FieldName = "cod_estado_orden";
            this.colcod_estado_orden.Name = "colcod_estado_orden";
            this.colcod_estado_orden.OptionsColumn.FixedWidth = true;
            this.colcod_estado_orden.Visible = true;
            this.colcod_estado_orden.VisibleIndex = 7;
            this.colcod_estado_orden.Width = 80;
            // 
            // gridColumn22
            // 
            this.gridColumn22.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn22.Caption = "Fecha Emision";
            this.gridColumn22.FieldName = "fch_emision";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.OptionsColumn.FixedWidth = true;
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 5;
            this.gridColumn22.Width = 80;
            // 
            // gridColumn24
            // 
            this.gridColumn24.Caption = "Imp. Total";
            this.gridColumn24.FieldName = "imp_total";
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.OptionsColumn.FixedWidth = true;
            this.gridColumn24.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, DevExpress.Data.SummaryMode.Mixed, "imp_total", "{0:#,#.##}")});
            this.gridColumn24.Visible = true;
            this.gridColumn24.VisibleIndex = 6;
            this.gridColumn24.Width = 70;
            // 
            // colfch_atencion
            // 
            this.colfch_atencion.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_atencion.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_atencion.Caption = "Fecha Atencion";
            this.colfch_atencion.FieldName = "fch_atencion";
            this.colfch_atencion.Name = "colfch_atencion";
            this.colfch_atencion.OptionsColumn.FixedWidth = true;
            this.colfch_atencion.Visible = true;
            this.colfch_atencion.VisibleIndex = 8;
            this.colfch_atencion.Width = 80;
            // 
            // coldsc_usuario_atencion
            // 
            this.coldsc_usuario_atencion.Caption = "Usuario Atencion";
            this.coldsc_usuario_atencion.FieldName = "dsc_usuario_atencion";
            this.coldsc_usuario_atencion.Name = "coldsc_usuario_atencion";
            this.coldsc_usuario_atencion.OptionsColumn.FixedWidth = true;
            this.coldsc_usuario_atencion.Visible = true;
            this.coldsc_usuario_atencion.VisibleIndex = 9;
            this.coldsc_usuario_atencion.Width = 120;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Sede";
            this.gridColumn17.FieldName = "dsc_sede_empresa";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsColumn.FixedWidth = true;
            this.gridColumn17.Width = 100;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "Cotizacion";
            this.gridColumn19.FieldName = "num_cotizacion";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.OptionsColumn.FixedWidth = true;
            this.gridColumn19.Width = 60;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "RUC";
            this.gridColumn21.FieldName = "dsc_ruc";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn26
            // 
            this.gridColumn26.Caption = "Fecha Despacho";
            this.gridColumn26.FieldName = "fch_despacho";
            this.gridColumn26.Name = "gridColumn26";
            this.gridColumn26.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn25
            // 
            this.gridColumn25.Caption = "Despacho";
            this.gridColumn25.FieldName = "dsc_direccion_despacho";
            this.gridColumn25.Name = "gridColumn25";
            this.gridColumn25.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn27
            // 
            this.gridColumn27.Caption = "Fecha registro";
            this.gridColumn27.FieldName = "fch_registro";
            this.gridColumn27.Name = "gridColumn27";
            this.gridColumn27.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn28
            // 
            this.gridColumn28.Caption = "Usuario Registro";
            this.gridColumn28.FieldName = "dsc_usuario";
            this.gridColumn28.Name = "gridColumn28";
            this.gridColumn28.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn29
            // 
            this.gridColumn29.Caption = "Fecha Cambio";
            this.gridColumn29.FieldName = "fch_cambio";
            this.gridColumn29.Name = "gridColumn29";
            this.gridColumn29.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn30
            // 
            this.gridColumn30.Caption = "Usuario Cambio";
            this.gridColumn30.FieldName = "dsc_usuario_cam";
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.OptionsColumn.FixedWidth = true;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup5.GroupBordersVisible = false;
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Size = new System.Drawing.Size(1358, 494);
            this.layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcOrdAtendidas;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1338, 474);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // tpOrdenesLiquidadas
            // 
            this.tpOrdenesLiquidadas.Controls.Add(this.lcOrdLiq);
            this.tpOrdenesLiquidadas.Name = "tpOrdenesLiquidadas";
            this.tpOrdenesLiquidadas.Size = new System.Drawing.Size(1358, 494);
            this.tpOrdenesLiquidadas.Text = "Ordenes Liquidadas";
            // 
            // lcOrdLiq
            // 
            this.lcOrdLiq.Controls.Add(this.gcOrdLiquidadas);
            this.lcOrdLiq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcOrdLiq.Location = new System.Drawing.Point(0, 0);
            this.lcOrdLiq.Name = "lcOrdLiq";
            this.lcOrdLiq.Root = this.layoutControlGroup6;
            this.lcOrdLiq.Size = new System.Drawing.Size(1358, 494);
            this.lcOrdLiq.TabIndex = 0;
            this.lcOrdLiq.Text = "layoutControl2";
            // 
            // gcOrdLiquidadas
            // 
            this.gcOrdLiquidadas.DataSource = this.bsListadoOrdLiquidadas;
            this.gcOrdLiquidadas.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcOrdLiquidadas.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcOrdLiquidadas.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcOrdLiquidadas.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcOrdLiquidadas.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcOrdLiquidadas.Location = new System.Drawing.Point(12, 12);
            this.gcOrdLiquidadas.MainView = this.gvOrdLiquidadas;
            this.gcOrdLiquidadas.MenuManager = this.ribbon;
            this.gcOrdLiquidadas.Name = "gcOrdLiquidadas";
            this.gcOrdLiquidadas.Padding = new System.Windows.Forms.Padding(6);
            this.gcOrdLiquidadas.Size = new System.Drawing.Size(1334, 470);
            this.gcOrdLiquidadas.TabIndex = 5;
            this.gcOrdLiquidadas.UseEmbeddedNavigator = true;
            this.gcOrdLiquidadas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOrdLiquidadas});
            // 
            // bsListadoOrdLiquidadas
            // 
            this.bsListadoOrdLiquidadas.DataSource = typeof(BE_Servicios.eOrdenCompra_Servicio);
            // 
            // gvOrdLiquidadas
            // 
            this.gvOrdLiquidadas.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Blue;
            this.gvOrdLiquidadas.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvOrdLiquidadas.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvOrdLiquidadas.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvOrdLiquidadas.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvOrdLiquidadas.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvOrdLiquidadas.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvOrdLiquidadas.ColumnPanelRowHeight = 30;
            this.gvOrdLiquidadas.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn31,
            this.gridColumn33,
            this.gridColumn35,
            this.coldsc_modalidad_pago3,
            this.gridColumn37,
            this.gridColumn39,
            this.colfch_liquidacion,
            this.coldsc_usuario_liquidacion,
            this.gridColumn32,
            this.gridColumn34,
            this.gridColumn36,
            this.gridColumn41,
            this.gridColumn40,
            this.gridColumn42,
            this.gridColumn43,
            this.gridColumn44,
            this.gridColumn45});
            this.gvOrdLiquidadas.GridControl = this.gcOrdLiquidadas;
            this.gvOrdLiquidadas.Name = "gvOrdLiquidadas";
            this.gvOrdLiquidadas.OptionsBehavior.Editable = false;
            this.gvOrdLiquidadas.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvOrdLiquidadas.OptionsSelection.MultiSelect = true;
            this.gvOrdLiquidadas.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvOrdLiquidadas.OptionsView.EnableAppearanceEvenRow = true;
            this.gvOrdLiquidadas.OptionsView.ShowAutoFilterRow = true;
            this.gvOrdLiquidadas.OptionsView.ShowFooter = true;
            this.gvOrdLiquidadas.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvOrdLiquidadas_RowClick);
            this.gvOrdLiquidadas.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvOrdLiquidadas_CustomDrawColumnHeader);
            this.gvOrdLiquidadas.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvOrdLiquidadas_CustomDrawCell);
            this.gvOrdLiquidadas.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvOrdLiquidadas_RowCellStyle);
            this.gvOrdLiquidadas.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvOrdLiquidadas_RowStyle);
            // 
            // gridColumn31
            // 
            this.gridColumn31.Caption = "Empresa";
            this.gridColumn31.FieldName = "dsc_empresa";
            this.gridColumn31.Name = "gridColumn31";
            this.gridColumn31.OptionsColumn.FixedWidth = true;
            this.gridColumn31.Visible = true;
            this.gridColumn31.VisibleIndex = 1;
            this.gridColumn31.Width = 180;
            // 
            // gridColumn33
            // 
            this.gridColumn33.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn33.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn33.Caption = "Ord. Compra";
            this.gridColumn33.FieldName = "cod_orden_compra_servicio";
            this.gridColumn33.Name = "gridColumn33";
            this.gridColumn33.OptionsColumn.FixedWidth = true;
            this.gridColumn33.Visible = true;
            this.gridColumn33.VisibleIndex = 2;
            this.gridColumn33.Width = 80;
            // 
            // gridColumn35
            // 
            this.gridColumn35.Caption = "Proveedor";
            this.gridColumn35.FieldName = "dsc_proveedor";
            this.gridColumn35.Name = "gridColumn35";
            this.gridColumn35.OptionsColumn.FixedWidth = true;
            this.gridColumn35.Visible = true;
            this.gridColumn35.VisibleIndex = 3;
            this.gridColumn35.Width = 180;
            // 
            // coldsc_modalidad_pago3
            // 
            this.coldsc_modalidad_pago3.Caption = "Modalidad de Pago";
            this.coldsc_modalidad_pago3.FieldName = "dsc_modalidad_pago";
            this.coldsc_modalidad_pago3.Name = "coldsc_modalidad_pago3";
            this.coldsc_modalidad_pago3.OptionsColumn.FixedWidth = true;
            this.coldsc_modalidad_pago3.Visible = true;
            this.coldsc_modalidad_pago3.VisibleIndex = 4;
            this.coldsc_modalidad_pago3.Width = 100;
            // 
            // gridColumn37
            // 
            this.gridColumn37.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn37.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn37.Caption = "Fecha Emision";
            this.gridColumn37.FieldName = "fch_emision";
            this.gridColumn37.Name = "gridColumn37";
            this.gridColumn37.OptionsColumn.FixedWidth = true;
            this.gridColumn37.Visible = true;
            this.gridColumn37.VisibleIndex = 5;
            this.gridColumn37.Width = 80;
            // 
            // gridColumn39
            // 
            this.gridColumn39.Caption = "Imp. Total";
            this.gridColumn39.FieldName = "imp_total";
            this.gridColumn39.Name = "gridColumn39";
            this.gridColumn39.OptionsColumn.FixedWidth = true;
            this.gridColumn39.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, DevExpress.Data.SummaryMode.Mixed, "imp_total", "{0:#,#.##}")});
            this.gridColumn39.Visible = true;
            this.gridColumn39.VisibleIndex = 6;
            this.gridColumn39.Width = 70;
            // 
            // colfch_liquidacion
            // 
            this.colfch_liquidacion.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_liquidacion.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_liquidacion.Caption = "Fecha Liquidacion";
            this.colfch_liquidacion.FieldName = "fch_liquidacion";
            this.colfch_liquidacion.Name = "colfch_liquidacion";
            this.colfch_liquidacion.OptionsColumn.FixedWidth = true;
            this.colfch_liquidacion.Visible = true;
            this.colfch_liquidacion.VisibleIndex = 7;
            this.colfch_liquidacion.Width = 80;
            // 
            // coldsc_usuario_liquidacion
            // 
            this.coldsc_usuario_liquidacion.Caption = "Usuario Liquidacion";
            this.coldsc_usuario_liquidacion.FieldName = "dsc_usuario_liquidacion";
            this.coldsc_usuario_liquidacion.Name = "coldsc_usuario_liquidacion";
            this.coldsc_usuario_liquidacion.OptionsColumn.FixedWidth = true;
            this.coldsc_usuario_liquidacion.Visible = true;
            this.coldsc_usuario_liquidacion.VisibleIndex = 8;
            this.coldsc_usuario_liquidacion.Width = 120;
            // 
            // gridColumn32
            // 
            this.gridColumn32.Caption = "Sede";
            this.gridColumn32.FieldName = "dsc_sede_empresa";
            this.gridColumn32.Name = "gridColumn32";
            this.gridColumn32.OptionsColumn.FixedWidth = true;
            this.gridColumn32.Width = 100;
            // 
            // gridColumn34
            // 
            this.gridColumn34.Caption = "Cotizacion";
            this.gridColumn34.FieldName = "num_cotizacion";
            this.gridColumn34.Name = "gridColumn34";
            this.gridColumn34.OptionsColumn.FixedWidth = true;
            this.gridColumn34.Width = 60;
            // 
            // gridColumn36
            // 
            this.gridColumn36.Caption = "RUC";
            this.gridColumn36.FieldName = "dsc_ruc";
            this.gridColumn36.Name = "gridColumn36";
            this.gridColumn36.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn41
            // 
            this.gridColumn41.Caption = "Fecha Despacho";
            this.gridColumn41.FieldName = "fch_despacho";
            this.gridColumn41.Name = "gridColumn41";
            this.gridColumn41.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn40
            // 
            this.gridColumn40.Caption = "Despacho";
            this.gridColumn40.FieldName = "dsc_direccion_despacho";
            this.gridColumn40.Name = "gridColumn40";
            this.gridColumn40.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn42
            // 
            this.gridColumn42.Caption = "Fecha registro";
            this.gridColumn42.FieldName = "fch_registro";
            this.gridColumn42.Name = "gridColumn42";
            this.gridColumn42.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn43
            // 
            this.gridColumn43.Caption = "Usuario Registro";
            this.gridColumn43.FieldName = "dsc_usuario";
            this.gridColumn43.Name = "gridColumn43";
            this.gridColumn43.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn44
            // 
            this.gridColumn44.Caption = "Fecha Cambio";
            this.gridColumn44.FieldName = "fch_cambio";
            this.gridColumn44.Name = "gridColumn44";
            this.gridColumn44.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn45
            // 
            this.gridColumn45.Caption = "Usuario Cambio";
            this.gridColumn45.FieldName = "dsc_usuario_cam";
            this.gridColumn45.Name = "gridColumn45";
            this.gridColumn45.OptionsColumn.FixedWidth = true;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup6.GroupBordersVisible = false;
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Size = new System.Drawing.Size(1358, 494);
            this.layoutControlGroup6.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gcOrdLiquidadas;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1338, 474);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // tpOrdenesAnuladas
            // 
            this.tpOrdenesAnuladas.Controls.Add(this.layoutControl3);
            this.tpOrdenesAnuladas.Name = "tpOrdenesAnuladas";
            this.tpOrdenesAnuladas.Size = new System.Drawing.Size(1358, 494);
            this.tpOrdenesAnuladas.Text = "Ordenes Anuladas";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.gcOrdAnuladas);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup8;
            this.layoutControl3.Size = new System.Drawing.Size(1358, 494);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // gcOrdAnuladas
            // 
            this.gcOrdAnuladas.DataSource = this.bsListadoOrdAnuladas;
            this.gcOrdAnuladas.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcOrdAnuladas.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcOrdAnuladas.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcOrdAnuladas.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcOrdAnuladas.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcOrdAnuladas.Location = new System.Drawing.Point(12, 12);
            this.gcOrdAnuladas.MainView = this.gvOrdAnuladas;
            this.gcOrdAnuladas.MenuManager = this.ribbon;
            this.gcOrdAnuladas.Name = "gcOrdAnuladas";
            this.gcOrdAnuladas.Padding = new System.Windows.Forms.Padding(6);
            this.gcOrdAnuladas.Size = new System.Drawing.Size(1334, 470);
            this.gcOrdAnuladas.TabIndex = 7;
            this.gcOrdAnuladas.UseEmbeddedNavigator = true;
            this.gcOrdAnuladas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOrdAnuladas});
            // 
            // gvOrdAnuladas
            // 
            this.gvOrdAnuladas.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Blue;
            this.gvOrdAnuladas.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvOrdAnuladas.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvOrdAnuladas.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvOrdAnuladas.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvOrdAnuladas.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvOrdAnuladas.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvOrdAnuladas.ColumnPanelRowHeight = 30;
            this.gvOrdAnuladas.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn60,
            this.gridColumn61,
            this.gridColumn62,
            this.gridColumn63,
            this.gridColumn64,
            this.gridColumn65,
            this.gridColumn66,
            this.gridColumn67,
            this.gridColumn68,
            this.gridColumn69,
            this.gridColumn70,
            this.gridColumn71,
            this.gridColumn72,
            this.gridColumn73,
            this.gridColumn74,
            this.gridColumn75,
            this.gridColumn76});
            this.gvOrdAnuladas.GridControl = this.gcOrdAnuladas;
            this.gvOrdAnuladas.Name = "gvOrdAnuladas";
            this.gvOrdAnuladas.OptionsBehavior.Editable = false;
            this.gvOrdAnuladas.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvOrdAnuladas.OptionsSelection.MultiSelect = true;
            this.gvOrdAnuladas.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvOrdAnuladas.OptionsView.EnableAppearanceEvenRow = true;
            this.gvOrdAnuladas.OptionsView.ShowAutoFilterRow = true;
            this.gvOrdAnuladas.OptionsView.ShowFooter = true;
            this.gvOrdAnuladas.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvOrdAnuladas_RowClick);
            this.gvOrdAnuladas.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvOrdAnuladas_CustomDrawColumnHeader);
            this.gvOrdAnuladas.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvOrdAnuladas_CustomDrawCell);
            this.gvOrdAnuladas.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvOrdAnuladas_RowCellStyle);
            this.gvOrdAnuladas.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvOrdAnuladas_RowStyle);
            // 
            // gridColumn60
            // 
            this.gridColumn60.Caption = "Empresa";
            this.gridColumn60.FieldName = "dsc_empresa";
            this.gridColumn60.Name = "gridColumn60";
            this.gridColumn60.OptionsColumn.FixedWidth = true;
            this.gridColumn60.Visible = true;
            this.gridColumn60.VisibleIndex = 1;
            this.gridColumn60.Width = 180;
            // 
            // gridColumn61
            // 
            this.gridColumn61.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn61.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn61.Caption = "Ord. Servicio";
            this.gridColumn61.FieldName = "cod_orden_compra_servicio";
            this.gridColumn61.Name = "gridColumn61";
            this.gridColumn61.OptionsColumn.FixedWidth = true;
            this.gridColumn61.Visible = true;
            this.gridColumn61.VisibleIndex = 2;
            this.gridColumn61.Width = 80;
            // 
            // gridColumn62
            // 
            this.gridColumn62.Caption = "Proveedor";
            this.gridColumn62.FieldName = "dsc_proveedor";
            this.gridColumn62.Name = "gridColumn62";
            this.gridColumn62.OptionsColumn.FixedWidth = true;
            this.gridColumn62.Visible = true;
            this.gridColumn62.VisibleIndex = 3;
            this.gridColumn62.Width = 180;
            // 
            // gridColumn63
            // 
            this.gridColumn63.Caption = "Modalidad de Pago";
            this.gridColumn63.FieldName = "dsc_modalidad_pago";
            this.gridColumn63.Name = "gridColumn63";
            this.gridColumn63.OptionsColumn.FixedWidth = true;
            this.gridColumn63.Visible = true;
            this.gridColumn63.VisibleIndex = 4;
            this.gridColumn63.Width = 100;
            // 
            // gridColumn64
            // 
            this.gridColumn64.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn64.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn64.Caption = "Fecha Emision";
            this.gridColumn64.FieldName = "fch_emision";
            this.gridColumn64.Name = "gridColumn64";
            this.gridColumn64.OptionsColumn.FixedWidth = true;
            this.gridColumn64.Visible = true;
            this.gridColumn64.VisibleIndex = 5;
            this.gridColumn64.Width = 80;
            // 
            // gridColumn65
            // 
            this.gridColumn65.Caption = "Imp. Total";
            this.gridColumn65.FieldName = "imp_total";
            this.gridColumn65.Name = "gridColumn65";
            this.gridColumn65.OptionsColumn.FixedWidth = true;
            this.gridColumn65.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, DevExpress.Data.SummaryMode.Mixed, "imp_total", "{0:#,#.##}")});
            this.gridColumn65.Visible = true;
            this.gridColumn65.VisibleIndex = 6;
            this.gridColumn65.Width = 70;
            // 
            // gridColumn66
            // 
            this.gridColumn66.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn66.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn66.Caption = "Fecha Anulación";
            this.gridColumn66.FieldName = "fch_anulacion";
            this.gridColumn66.Name = "gridColumn66";
            this.gridColumn66.OptionsColumn.FixedWidth = true;
            this.gridColumn66.Visible = true;
            this.gridColumn66.VisibleIndex = 7;
            this.gridColumn66.Width = 80;
            // 
            // gridColumn67
            // 
            this.gridColumn67.Caption = "Usuario Anulación";
            this.gridColumn67.FieldName = "dsc_usuario_anulacion";
            this.gridColumn67.Name = "gridColumn67";
            this.gridColumn67.OptionsColumn.FixedWidth = true;
            this.gridColumn67.Visible = true;
            this.gridColumn67.VisibleIndex = 8;
            this.gridColumn67.Width = 120;
            // 
            // gridColumn68
            // 
            this.gridColumn68.Caption = "Sede";
            this.gridColumn68.FieldName = "dsc_sede_empresa";
            this.gridColumn68.Name = "gridColumn68";
            this.gridColumn68.OptionsColumn.FixedWidth = true;
            this.gridColumn68.Width = 100;
            // 
            // gridColumn69
            // 
            this.gridColumn69.Caption = "Cotizacion";
            this.gridColumn69.FieldName = "num_cotizacion";
            this.gridColumn69.Name = "gridColumn69";
            this.gridColumn69.OptionsColumn.FixedWidth = true;
            this.gridColumn69.Width = 60;
            // 
            // gridColumn70
            // 
            this.gridColumn70.Caption = "RUC";
            this.gridColumn70.FieldName = "dsc_ruc";
            this.gridColumn70.Name = "gridColumn70";
            this.gridColumn70.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn71
            // 
            this.gridColumn71.Caption = "Fecha Despacho";
            this.gridColumn71.FieldName = "fch_despacho";
            this.gridColumn71.Name = "gridColumn71";
            this.gridColumn71.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn72
            // 
            this.gridColumn72.Caption = "Despacho";
            this.gridColumn72.FieldName = "dsc_direccion_despacho";
            this.gridColumn72.Name = "gridColumn72";
            this.gridColumn72.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn73
            // 
            this.gridColumn73.Caption = "Fecha registro";
            this.gridColumn73.FieldName = "fch_registro";
            this.gridColumn73.Name = "gridColumn73";
            this.gridColumn73.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn74
            // 
            this.gridColumn74.Caption = "Usuario Registro";
            this.gridColumn74.FieldName = "dsc_usuario";
            this.gridColumn74.Name = "gridColumn74";
            this.gridColumn74.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn75
            // 
            this.gridColumn75.Caption = "Fecha Cambio";
            this.gridColumn75.FieldName = "fch_cambio";
            this.gridColumn75.Name = "gridColumn75";
            this.gridColumn75.OptionsColumn.FixedWidth = true;
            // 
            // gridColumn76
            // 
            this.gridColumn76.Caption = "Usuario Cambio";
            this.gridColumn76.FieldName = "dsc_usuario_cam";
            this.gridColumn76.Name = "gridColumn76";
            this.gridColumn76.OptionsColumn.FixedWidth = true;
            // 
            // layoutControlGroup8
            // 
            this.layoutControlGroup8.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup8.GroupBordersVisible = false;
            this.layoutControlGroup8.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6});
            this.layoutControlGroup8.Name = "layoutControlGroup8";
            this.layoutControlGroup8.Size = new System.Drawing.Size(1358, 494);
            this.layoutControlGroup8.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gcOrdAnuladas;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1338, 474);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // btnBuscarProveedor
            // 
            this.btnBuscarProveedor.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarProveedor.ImageOptions.Image")));
            this.btnBuscarProveedor.Location = new System.Drawing.Point(794, 30);
            this.btnBuscarProveedor.Name = "btnBuscarProveedor";
            this.btnBuscarProveedor.Size = new System.Drawing.Size(22, 20);
            this.btnBuscarProveedor.StyleController = this.controlListado;
            this.btnBuscarProveedor.TabIndex = 9;
            this.btnBuscarProveedor.Click += new System.EventHandler(this.btnBuscarProveedor_Click);
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
            this.lkpEmpresa.Size = new System.Drawing.Size(314, 20);
            this.lkpEmpresa.StyleController = this.controlListado;
            this.lkpEmpresa.TabIndex = 4;
            this.lkpEmpresa.EditValueChanged += new System.EventHandler(this.lkpEmpresa_EditValueChanged);
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
            this.lkpSede.Size = new System.Drawing.Size(314, 20);
            this.lkpSede.StyleController = this.controlListado;
            this.lkpSede.TabIndex = 6;
            // 
            // lkpTipoFecha
            // 
            this.lkpTipoFecha.Location = new System.Drawing.Point(900, 30);
            this.lkpTipoFecha.Name = "lkpTipoFecha";
            this.lkpTipoFecha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipoFecha.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_tipo_fecha", "Descripción")});
            this.lkpTipoFecha.Properties.NullText = "";
            this.lkpTipoFecha.Size = new System.Drawing.Size(247, 20);
            this.lkpTipoFecha.StyleController = this.controlListado;
            this.lkpTipoFecha.TabIndex = 5;
            // 
            // dtpDesde
            // 
            this.dtpDesde.EditValue = null;
            this.dtpDesde.Location = new System.Drawing.Point(900, 54);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Properties.Appearance.Options.UseTextOptions = true;
            this.dtpDesde.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dtpDesde.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDesde.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDesde.Size = new System.Drawing.Size(102, 20);
            this.dtpDesde.StyleController = this.controlListado;
            this.dtpDesde.TabIndex = 7;
            // 
            // dtpHasta
            // 
            this.dtpHasta.EditValue = null;
            this.dtpHasta.Location = new System.Drawing.Point(1046, 54);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Properties.Appearance.Options.UseTextOptions = true;
            this.dtpHasta.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dtpHasta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpHasta.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpHasta.Size = new System.Drawing.Size(101, 20);
            this.dtpHasta.StyleController = this.controlListado;
            this.dtpHasta.TabIndex = 7;
            // 
            // btnBuscar
            // 
            this.btnBuscar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.ImageOptions.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(1183, 33);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(102, 38);
            this.btnBuscar.StyleController = this.controlListado;
            this.btnBuscar.TabIndex = 8;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtProveedor
            // 
            this.txtProveedor.Location = new System.Drawing.Point(453, 30);
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.Size = new System.Drawing.Size(337, 20);
            this.txtProveedor.StyleController = this.controlListado;
            this.txtProveedor.TabIndex = 4;
            this.txtProveedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProveedor_KeyPress);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.lcGrillas});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.Root.Size = new System.Drawing.Size(1376, 605);
            this.Root.TextVisible = false;
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
            this.controlDesdeFecha,
            this.controlFechaHasta,
            this.controlBuscar,
            this.emptySpaceItem1,
            this.emptySpaceItem5});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 3;
            this.layoutControlGroup2.Size = new System.Drawing.Size(1364, 70);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.controlProveedor,
            this.controlBuscarProveedor,
            this.emptySpaceItem3,
            this.emptySpaceItem6});
            this.layoutControlGroup3.Location = new System.Drawing.Point(383, 22);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsItemText.TextToControlDistance = 3;
            this.layoutControlGroup3.Size = new System.Drawing.Size(429, 48);
            this.layoutControlGroup3.Text = "layoutControlGroup2";
            // 
            // controlProveedor
            // 
            this.controlProveedor.Control = this.txtProveedor;
            this.controlProveedor.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.controlProveedor.CustomizationFormText = "Proveedor";
            this.controlProveedor.Location = new System.Drawing.Point(0, 0);
            this.controlProveedor.MaxSize = new System.Drawing.Size(403, 25);
            this.controlProveedor.MinSize = new System.Drawing.Size(403, 25);
            this.controlProveedor.Name = "controlProveedor";
            this.controlProveedor.Size = new System.Drawing.Size(403, 25);
            this.controlProveedor.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlProveedor.Text = "Proveedor :";
            this.controlProveedor.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlProveedor.TextLocation = DevExpress.Utils.Locations.Left;
            this.controlProveedor.TextSize = new System.Drawing.Size(50, 13);
            this.controlProveedor.TextToControlDistance = 12;
            // 
            // controlBuscarProveedor
            // 
            this.controlBuscarProveedor.Control = this.btnBuscarProveedor;
            this.controlBuscarProveedor.CustomizationFormText = "Buscar Proveedor";
            this.controlBuscarProveedor.Location = new System.Drawing.Point(403, 0);
            this.controlBuscarProveedor.MaxSize = new System.Drawing.Size(26, 24);
            this.controlBuscarProveedor.MinSize = new System.Drawing.Size(26, 24);
            this.controlBuscarProveedor.Name = "controlBuscarProveedor";
            this.controlBuscarProveedor.Size = new System.Drawing.Size(26, 24);
            this.controlBuscarProveedor.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlBuscarProveedor.Text = " ";
            this.controlBuscarProveedor.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlBuscarProveedor.TextSize = new System.Drawing.Size(0, 0);
            this.controlBuscarProveedor.TextToControlDistance = 0;
            this.controlBuscarProveedor.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 25);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(403, 23);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.Location = new System.Drawing.Point(403, 24);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(26, 24);
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // controlTipoFecha
            // 
            this.controlTipoFecha.Control = this.lkpTipoFecha;
            this.controlTipoFecha.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.controlTipoFecha.CustomizationFormText = "TipoFecha";
            this.controlTipoFecha.Location = new System.Drawing.Point(822, 22);
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
            this.controlEmpresa.MaxSize = new System.Drawing.Size(373, 24);
            this.controlEmpresa.MinSize = new System.Drawing.Size(373, 24);
            this.controlEmpresa.Name = "controlEmpresa";
            this.controlEmpresa.Size = new System.Drawing.Size(373, 24);
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
            this.emptySpaceItem2.Location = new System.Drawing.Point(373, 22);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(10, 0);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(10, 10);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(10, 48);
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
            this.lblFiltros.Size = new System.Drawing.Size(1364, 22);
            this.lblFiltros.Text = "Filtros de Búsqueda";
            this.lblFiltros.TextSize = new System.Drawing.Size(147, 18);
            // 
            // controlSede
            // 
            this.controlSede.Control = this.lkpSede;
            this.controlSede.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.controlSede.CustomizationFormText = "Estado";
            this.controlSede.Location = new System.Drawing.Point(0, 46);
            this.controlSede.MaxSize = new System.Drawing.Size(373, 24);
            this.controlSede.MinSize = new System.Drawing.Size(373, 24);
            this.controlSede.Name = "controlSede";
            this.controlSede.Size = new System.Drawing.Size(373, 24);
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
            this.emptySpaceItem4.Location = new System.Drawing.Point(812, 22);
            this.emptySpaceItem4.MaxSize = new System.Drawing.Size(10, 0);
            this.emptySpaceItem4.MinSize = new System.Drawing.Size(10, 10);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(10, 48);
            this.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // controlDesdeFecha
            // 
            this.controlDesdeFecha.Control = this.dtpDesde;
            this.controlDesdeFecha.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.controlDesdeFecha.CustomizationFormText = "Desde";
            this.controlDesdeFecha.Location = new System.Drawing.Point(822, 46);
            this.controlDesdeFecha.MaxSize = new System.Drawing.Size(176, 24);
            this.controlDesdeFecha.MinSize = new System.Drawing.Size(176, 24);
            this.controlDesdeFecha.Name = "controlDesdeFecha";
            this.controlDesdeFecha.Size = new System.Drawing.Size(176, 24);
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
            this.controlFechaHasta.Location = new System.Drawing.Point(998, 46);
            this.controlFechaHasta.MaxSize = new System.Drawing.Size(145, 24);
            this.controlFechaHasta.MinSize = new System.Drawing.Size(145, 24);
            this.controlFechaHasta.Name = "controlFechaHasta";
            this.controlFechaHasta.Size = new System.Drawing.Size(145, 24);
            this.controlFechaHasta.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlFechaHasta.Text = "Hasta :";
            this.controlFechaHasta.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlFechaHasta.TextLocation = DevExpress.Utils.Locations.Left;
            this.controlFechaHasta.TextSize = new System.Drawing.Size(35, 13);
            this.controlFechaHasta.TextToControlDistance = 5;
            // 
            // controlBuscar
            // 
            this.controlBuscar.Control = this.btnBuscar;
            this.controlBuscar.Location = new System.Drawing.Point(1172, 22);
            this.controlBuscar.MaxSize = new System.Drawing.Size(112, 48);
            this.controlBuscar.MinSize = new System.Drawing.Size(112, 48);
            this.controlBuscar.Name = "controlBuscar";
            this.controlBuscar.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.controlBuscar.Size = new System.Drawing.Size(112, 48);
            this.controlBuscar.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlBuscar.TextSize = new System.Drawing.Size(0, 0);
            this.controlBuscar.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(1284, 22);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(80, 48);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(1143, 22);
            this.emptySpaceItem5.MaxSize = new System.Drawing.Size(29, 0);
            this.emptySpaceItem5.MinSize = new System.Drawing.Size(29, 10);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(29, 48);
            this.emptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcGrillas
            // 
            this.lcGrillas.Control = this.tcOrdenCompra;
            this.lcGrillas.Location = new System.Drawing.Point(0, 70);
            this.lcGrillas.Name = "lcGrillas";
            this.lcGrillas.Size = new System.Drawing.Size(1364, 523);
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
            // frmListadoOrdenesCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1376, 787);
            this.Controls.Add(this.controlListado);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.KeyPreview = true;
            this.Name = "frmListadoOrdenesCompra";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Mantenimiento Ordenes Compra";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListadoOrdenesCompra_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmListadoOrdenesCompra_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlListado)).EndInit();
            this.controlListado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tcOrdenCompra)).EndInit();
            this.tcOrdenCompra.ResumeLayout(false);
            this.tpOrdenesGeneradas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcOrdGeneradas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoOrdGeneradas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrdGeneradas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrdenesGeneradas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.tpOrdenesAprobadas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcOrdAprob)).EndInit();
            this.lcOrdAprob.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcOrdAprobadas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoOrdAprobadas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrdAprobadas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.tpOrdenesEnviadas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcOrdEnviadas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoOrdEnviadas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrdEnviadas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.tpOrdenesAtendidas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcOrdAten)).EndInit();
            this.lcOrdAten.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcOrdAtendidas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoOrdAtendidas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrdAtendidas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.tpOrdenesLiquidadas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcOrdLiq)).EndInit();
            this.lcOrdLiq.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcOrdLiquidadas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoOrdLiquidadas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrdLiquidadas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.tpOrdenesAnuladas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcOrdAnuladas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoOrdAnuladas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrdAnuladas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEmpresa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSede.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoFecha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDesde.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDesde.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpHasta.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpHasta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProveedor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlProveedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBuscarProveedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlTipoFecha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFiltros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSede)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlDesdeFecha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlFechaHasta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBuscar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
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
        private DevExpress.XtraLayout.LayoutControl controlListado;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraBars.BarButtonItem btnNuevaOrdCom;
        private DevExpress.XtraGrid.GridControl gcOrdGeneradas;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOrdGeneradas;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem controlEmpresa;
        private DevExpress.XtraLayout.LayoutControlItem controlSede;
        private DevExpress.XtraLayout.LayoutControlItem controlTipoFecha;
        private DevExpress.XtraLayout.LayoutControlItem controlDesdeFecha;
        private DevExpress.XtraEditors.LookUpEdit lkpEmpresa;
        private DevExpress.XtraEditors.LookUpEdit lkpSede;
        private DevExpress.XtraLayout.LayoutControlItem controlFechaHasta;
        private DevExpress.XtraEditors.LookUpEdit lkpTipoFecha;
        private DevExpress.XtraEditors.DateEdit dtpDesde;
        private DevExpress.XtraEditors.DateEdit dtpHasta;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem controlProveedor;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraLayout.LayoutControlItem controlBuscar;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblFiltros;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private System.Windows.Forms.BindingSource bsListadoOrdGeneradas;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoReportes;
        private DevExpress.XtraBars.BarButtonItem btnExportarExcel;
        private DevExpress.XtraBars.BarButtonItem btnImprimir;
        private DevExpress.XtraBars.BarButtonItem btnAnularOrdCom;
        private DevExpress.XtraBars.BarButtonItem btnEliminarOrdCom;
        private DevExpress.XtraEditors.SimpleButton btnBuscarProveedor;
        private DevExpress.XtraLayout.LayoutControlItem controlBuscarProveedor;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_empresa;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_sede_empresa;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_orden_compra;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_cotizacion;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_ruc;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_emision;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_total;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_direccion_despacho;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_registro;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_cambio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario_cam;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_proveedor;
        private DevExpress.XtraEditors.TextEdit txtProveedor;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem btnLiquidar;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoAcciones;
        private DevExpress.XtraBars.BarButtonItem btnAtender;
        private DevExpress.XtraBars.BarButtonItem btnReporteOrdenCompra;
        private DevExpress.XtraTab.XtraTabControl tcOrdenCompra;
        private DevExpress.XtraTab.XtraTabPage tpOrdenesGeneradas;
        private DevExpress.XtraTab.XtraTabPage tpOrdenesAprobadas;
        private DevExpress.XtraLayout.LayoutControlItem lcGrillas;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup lcOrdenesGeneradas;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraBars.BarButtonItem btnAprobar;
        private DevExpress.XtraTab.XtraTabPage tpOrdenesAtendidas;
        private DevExpress.XtraTab.XtraTabPage tpOrdenesLiquidadas;
        private DevExpress.XtraLayout.LayoutControl lcOrdAprob;
        private DevExpress.XtraGrid.GridControl gcOrdAprobadas;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOrdAprobadas;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControl lcOrdAten;
        private DevExpress.XtraGrid.GridControl gcOrdAtendidas;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOrdAtendidas;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn26;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn27;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn28;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn29;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControl lcOrdLiq;
        private DevExpress.XtraGrid.GridControl gcOrdLiquidadas;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOrdLiquidadas;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn31;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn32;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn33;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn34;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn35;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn36;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn37;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn39;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn40;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn41;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn42;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn43;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn44;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn45;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private System.Windows.Forms.BindingSource bsListadoOrdAprobadas;
        private System.Windows.Forms.BindingSource bsListadoOrdAtendidas;
        private System.Windows.Forms.BindingSource bsListadoOrdLiquidadas;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_almacen;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_despacho;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_aprobacion;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario_aprobacion;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_estado_orden;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_atencion;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario_atencion;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_liquidacion;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario_liquidacion;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraTab.XtraTabPage tpOrdenesEnviadas;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_modalidad_pago;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_modalidad_pago1;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_modalidad_pago2;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_modalidad_pago3;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraGrid.GridControl gcOrdEnviadas;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOrdEnviadas;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn38;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn46;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn47;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn48;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn49;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn50;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn51;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn52;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn53;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn54;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn55;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn56;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn57;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn58;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn59;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraBars.BarButtonItem btnDesaprobar;
        private DevExpress.XtraBars.BarButtonItem btnEnviar;
        private System.Windows.Forms.BindingSource bsListadoOrdEnviadas;
        private DevExpress.XtraTab.XtraTabPage tpOrdenesAnuladas;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup8;
        private System.Windows.Forms.BindingSource bsListadoOrdAnuladas;
        private DevExpress.XtraGrid.GridControl gcOrdAnuladas;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOrdAnuladas;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn60;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn61;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn62;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn63;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn64;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn65;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn66;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn67;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn68;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn69;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn70;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn71;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn72;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn73;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn74;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn75;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn76;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}