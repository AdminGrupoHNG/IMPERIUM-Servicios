
namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    partial class frmResumenPresupuestoEjecucion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResumenPresupuestoEjecucion));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnExportarExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnImprimir = new DevExpress.XtraBars.BarButtonItem();
            this.btnListarProyectos = new DevExpress.XtraBars.BarButtonItem();
            this.btnTarifarioServicioProducto = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.grupoReportes = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grupoAcciones = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcListaEjecutado = new DevExpress.XtraGrid.GridControl();
            this.bsResumen = new System.Windows.Forms.BindingSource(this.components);
            this.gvListaEjecutado = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colimp_venta_ejecutado2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtImporte2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colimp_costo_ejecutado2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_utilidad_ejecutado2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprc_utilidad_ejecutado2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtPorcentaje2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gcListaPresupuesto = new DevExpress.XtraGrid.GridControl();
            this.gvListaPresupuesto = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colimp_venta_presupuesto1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtImporte = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colimp_costo_presupuesto1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_utilidad_presupuesto1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprc_utilidad_presupuesto1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtPorcentaje = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gcListaTipoProducto = new DevExpress.XtraGrid.GridControl();
            this.gvListaTipoProducto = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_tipo_servicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_tipo_servicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.picBuscarProyecto = new DevExpress.XtraEditors.PictureEdit();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.lkpEmpresa = new DevExpress.XtraEditors.LookUpEdit();
            this.grdbTipoMoneda = new DevExpress.XtraEditors.RadioGroup();
            this.txtDescripcion = new DevExpress.XtraEditors.TextEdit();
            this.txtPorcentaje = new DevExpress.XtraEditors.TextEdit();
            this.dtFechaActualizacion = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem10 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem11 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem13 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem12 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleLabelItem3 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem4 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem5 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem15 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaEjecutado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsResumen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaEjecutado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentaje2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaPresupuesto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaPresupuesto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentaje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaTipoProducto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaTipoProducto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBuscarProyecto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEmpresa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdbTipoMoneda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPorcentaje.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaActualizacion.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaActualizacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem15)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.btnExportarExcel,
            this.btnImprimir,
            this.btnListarProyectos,
            this.btnTarifarioServicioProducto});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 6;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1385, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Caption = "Exportar Excel";
            this.btnExportarExcel.Id = 1;
            this.btnExportarExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.ImageOptions.Image")));
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Caption = "Imprimir";
            this.btnImprimir.Id = 2;
            this.btnImprimir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.ImageOptions.Image")));
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnListarProyectos
            // 
            this.btnListarProyectos.Caption = "Listar Proyectos";
            this.btnListarProyectos.Id = 3;
            this.btnListarProyectos.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnListarProyectos.ImageOptions.Image")));
            this.btnListarProyectos.Name = "btnListarProyectos";
            this.btnListarProyectos.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnListarProyectos.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnListarProyectos_ItemClick);
            // 
            // btnTarifarioServicioProducto
            // 
            this.btnTarifarioServicioProducto.Caption = "Tarifario Servicio/Producto";
            this.btnTarifarioServicioProducto.Id = 4;
            this.btnTarifarioServicioProducto.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTarifarioServicioProducto.ImageOptions.Image")));
            this.btnTarifarioServicioProducto.Name = "btnTarifarioServicioProducto";
            this.btnTarifarioServicioProducto.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnTarifarioServicioProducto.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnTarifarioServicioProducto_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grupoReportes,
            this.grupoAcciones});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Opciones Generales";
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
            this.grupoAcciones.ItemLinks.Add(this.btnListarProyectos);
            this.grupoAcciones.ItemLinks.Add(this.btnTarifarioServicioProducto);
            this.grupoAcciones.Name = "grupoAcciones";
            this.grupoAcciones.Text = "Acciones";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 878);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1385, 24);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcListaEjecutado);
            this.layoutControl1.Controls.Add(this.gcListaPresupuesto);
            this.layoutControl1.Controls.Add(this.gcListaTipoProducto);
            this.layoutControl1.Controls.Add(this.picBuscarProyecto);
            this.layoutControl1.Controls.Add(this.btnBuscar);
            this.layoutControl1.Controls.Add(this.lkpEmpresa);
            this.layoutControl1.Controls.Add(this.grdbTipoMoneda);
            this.layoutControl1.Controls.Add(this.txtDescripcion);
            this.layoutControl1.Controls.Add(this.txtPorcentaje);
            this.layoutControl1.Controls.Add(this.dtFechaActualizacion);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 158);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1385, 720);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcListaEjecutado
            // 
            this.gcListaEjecutado.DataSource = this.bsResumen;
            this.gcListaEjecutado.Location = new System.Drawing.Point(883, 216);
            this.gcListaEjecutado.MainView = this.gvListaEjecutado;
            this.gcListaEjecutado.MenuManager = this.ribbon;
            this.gcListaEjecutado.Name = "gcListaEjecutado";
            this.gcListaEjecutado.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtImporte2,
            this.rtxtPorcentaje2});
            this.gcListaEjecutado.Size = new System.Drawing.Size(492, 327);
            this.gcListaEjecutado.TabIndex = 11;
            this.gcListaEjecutado.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListaEjecutado});
            // 
            // bsResumen
            // 
            this.bsResumen.DataSource = typeof(BE_Servicios.eProyecto.eProyeto_Presupuesto_Ejecucion);
            // 
            // gvListaEjecutado
            // 
            this.gvListaEjecutado.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(203)))), ((int)(((byte)(173)))));
            this.gvListaEjecutado.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.gvListaEjecutado.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gvListaEjecutado.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvListaEjecutado.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvListaEjecutado.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListaEjecutado.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListaEjecutado.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListaEjecutado.Appearance.ViewCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(60)))), ((int)(((byte)(12)))));
            this.gvListaEjecutado.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.gvListaEjecutado.Appearance.ViewCaption.ForeColor = System.Drawing.Color.White;
            this.gvListaEjecutado.Appearance.ViewCaption.Options.UseBackColor = true;
            this.gvListaEjecutado.Appearance.ViewCaption.Options.UseFont = true;
            this.gvListaEjecutado.Appearance.ViewCaption.Options.UseForeColor = true;
            this.gvListaEjecutado.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colimp_venta_ejecutado2,
            this.colimp_costo_ejecutado2,
            this.colimp_utilidad_ejecutado2,
            this.colprc_utilidad_ejecutado2});
            this.gvListaEjecutado.GridControl = this.gcListaEjecutado;
            this.gvListaEjecutado.Name = "gvListaEjecutado";
            this.gvListaEjecutado.OptionsBehavior.Editable = false;
            this.gvListaEjecutado.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvListaEjecutado.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvListaEjecutado.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvListaEjecutado.OptionsView.ShowGroupPanel = false;
            this.gvListaEjecutado.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvListaEjecutado.OptionsView.ShowIndicator = false;
            this.gvListaEjecutado.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvListaEjecutado.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvListaEjecutado.ViewCaption = "EJECUTADO";
            this.gvListaEjecutado.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListaEjecutado_CustomDrawColumnHeader);
            this.gvListaEjecutado.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvListaEjecutado_CustomDrawCell);
            // 
            // colimp_venta_ejecutado2
            // 
            this.colimp_venta_ejecutado2.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colimp_venta_ejecutado2.AppearanceCell.Options.UseForeColor = true;
            this.colimp_venta_ejecutado2.Caption = "P. VENTA";
            this.colimp_venta_ejecutado2.ColumnEdit = this.rtxtImporte2;
            this.colimp_venta_ejecutado2.FieldName = "imp_venta_ejecutado";
            this.colimp_venta_ejecutado2.Name = "colimp_venta_ejecutado2";
            this.colimp_venta_ejecutado2.Visible = true;
            this.colimp_venta_ejecutado2.VisibleIndex = 0;
            // 
            // rtxtImporte2
            // 
            this.rtxtImporte2.AutoHeight = false;
            this.rtxtImporte2.Mask.EditMask = "c2";
            this.rtxtImporte2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtImporte2.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtImporte2.Name = "rtxtImporte2";
            // 
            // colimp_costo_ejecutado2
            // 
            this.colimp_costo_ejecutado2.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colimp_costo_ejecutado2.AppearanceCell.Options.UseForeColor = true;
            this.colimp_costo_ejecutado2.Caption = "COSTO";
            this.colimp_costo_ejecutado2.ColumnEdit = this.rtxtImporte2;
            this.colimp_costo_ejecutado2.FieldName = "imp_costo_ejecutado";
            this.colimp_costo_ejecutado2.Name = "colimp_costo_ejecutado2";
            this.colimp_costo_ejecutado2.Visible = true;
            this.colimp_costo_ejecutado2.VisibleIndex = 1;
            // 
            // colimp_utilidad_ejecutado2
            // 
            this.colimp_utilidad_ejecutado2.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(203)))), ((int)(((byte)(173)))));
            this.colimp_utilidad_ejecutado2.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colimp_utilidad_ejecutado2.AppearanceCell.Options.UseBackColor = true;
            this.colimp_utilidad_ejecutado2.AppearanceCell.Options.UseForeColor = true;
            this.colimp_utilidad_ejecutado2.Caption = "UTILIDAD";
            this.colimp_utilidad_ejecutado2.ColumnEdit = this.rtxtImporte2;
            this.colimp_utilidad_ejecutado2.FieldName = "imp_utilidad_ejecutado";
            this.colimp_utilidad_ejecutado2.Name = "colimp_utilidad_ejecutado2";
            this.colimp_utilidad_ejecutado2.Visible = true;
            this.colimp_utilidad_ejecutado2.VisibleIndex = 2;
            // 
            // colprc_utilidad_ejecutado2
            // 
            this.colprc_utilidad_ejecutado2.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(203)))), ((int)(((byte)(173)))));
            this.colprc_utilidad_ejecutado2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colprc_utilidad_ejecutado2.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colprc_utilidad_ejecutado2.AppearanceCell.Options.UseBackColor = true;
            this.colprc_utilidad_ejecutado2.AppearanceCell.Options.UseFont = true;
            this.colprc_utilidad_ejecutado2.AppearanceCell.Options.UseForeColor = true;
            this.colprc_utilidad_ejecutado2.Caption = "% UTILIDAD";
            this.colprc_utilidad_ejecutado2.ColumnEdit = this.rtxtPorcentaje2;
            this.colprc_utilidad_ejecutado2.FieldName = "prc_utilidad_ejecutado";
            this.colprc_utilidad_ejecutado2.Name = "colprc_utilidad_ejecutado2";
            this.colprc_utilidad_ejecutado2.Visible = true;
            this.colprc_utilidad_ejecutado2.VisibleIndex = 3;
            // 
            // rtxtPorcentaje2
            // 
            this.rtxtPorcentaje2.AutoHeight = false;
            this.rtxtPorcentaje2.Mask.EditMask = "p2";
            this.rtxtPorcentaje2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtPorcentaje2.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtPorcentaje2.Name = "rtxtPorcentaje2";
            // 
            // gcListaPresupuesto
            // 
            this.gcListaPresupuesto.DataSource = this.bsResumen;
            this.gcListaPresupuesto.Location = new System.Drawing.Point(381, 216);
            this.gcListaPresupuesto.MainView = this.gvListaPresupuesto;
            this.gcListaPresupuesto.MenuManager = this.ribbon;
            this.gcListaPresupuesto.Name = "gcListaPresupuesto";
            this.gcListaPresupuesto.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtImporte,
            this.rtxtPorcentaje});
            this.gcListaPresupuesto.Size = new System.Drawing.Size(492, 327);
            this.gcListaPresupuesto.TabIndex = 10;
            this.gcListaPresupuesto.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListaPresupuesto});
            // 
            // gvListaPresupuesto
            // 
            this.gvListaPresupuesto.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(224)))), ((int)(((byte)(180)))));
            this.gvListaPresupuesto.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.gvListaPresupuesto.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gvListaPresupuesto.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvListaPresupuesto.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvListaPresupuesto.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListaPresupuesto.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListaPresupuesto.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListaPresupuesto.Appearance.ViewCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(86)))), ((int)(((byte)(35)))));
            this.gvListaPresupuesto.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.gvListaPresupuesto.Appearance.ViewCaption.ForeColor = System.Drawing.Color.White;
            this.gvListaPresupuesto.Appearance.ViewCaption.Options.UseBackColor = true;
            this.gvListaPresupuesto.Appearance.ViewCaption.Options.UseFont = true;
            this.gvListaPresupuesto.Appearance.ViewCaption.Options.UseForeColor = true;
            this.gvListaPresupuesto.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colimp_venta_presupuesto1,
            this.colimp_costo_presupuesto1,
            this.colimp_utilidad_presupuesto1,
            this.colprc_utilidad_presupuesto1});
            this.gvListaPresupuesto.GridControl = this.gcListaPresupuesto;
            this.gvListaPresupuesto.Name = "gvListaPresupuesto";
            this.gvListaPresupuesto.OptionsBehavior.Editable = false;
            this.gvListaPresupuesto.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvListaPresupuesto.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvListaPresupuesto.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvListaPresupuesto.OptionsView.ShowGroupPanel = false;
            this.gvListaPresupuesto.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvListaPresupuesto.OptionsView.ShowIndicator = false;
            this.gvListaPresupuesto.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvListaPresupuesto.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvListaPresupuesto.ViewCaption = "PRESUPUESTO";
            this.gvListaPresupuesto.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListaPresupuesto_CustomDrawColumnHeader);
            this.gvListaPresupuesto.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvListaPresupuesto_CustomDrawCell);
            // 
            // colimp_venta_presupuesto1
            // 
            this.colimp_venta_presupuesto1.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colimp_venta_presupuesto1.AppearanceCell.Options.UseForeColor = true;
            this.colimp_venta_presupuesto1.Caption = "P. VENTA";
            this.colimp_venta_presupuesto1.ColumnEdit = this.rtxtImporte;
            this.colimp_venta_presupuesto1.FieldName = "imp_venta_presupuesto";
            this.colimp_venta_presupuesto1.Name = "colimp_venta_presupuesto1";
            this.colimp_venta_presupuesto1.Visible = true;
            this.colimp_venta_presupuesto1.VisibleIndex = 0;
            // 
            // rtxtImporte
            // 
            this.rtxtImporte.AutoHeight = false;
            this.rtxtImporte.Mask.EditMask = "c2";
            this.rtxtImporte.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtImporte.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtImporte.Name = "rtxtImporte";
            // 
            // colimp_costo_presupuesto1
            // 
            this.colimp_costo_presupuesto1.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colimp_costo_presupuesto1.AppearanceCell.Options.UseForeColor = true;
            this.colimp_costo_presupuesto1.Caption = "COSTO";
            this.colimp_costo_presupuesto1.ColumnEdit = this.rtxtImporte;
            this.colimp_costo_presupuesto1.FieldName = "imp_costo_presupuesto";
            this.colimp_costo_presupuesto1.Name = "colimp_costo_presupuesto1";
            this.colimp_costo_presupuesto1.Visible = true;
            this.colimp_costo_presupuesto1.VisibleIndex = 1;
            // 
            // colimp_utilidad_presupuesto1
            // 
            this.colimp_utilidad_presupuesto1.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(224)))), ((int)(((byte)(180)))));
            this.colimp_utilidad_presupuesto1.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colimp_utilidad_presupuesto1.AppearanceCell.Options.UseBackColor = true;
            this.colimp_utilidad_presupuesto1.AppearanceCell.Options.UseForeColor = true;
            this.colimp_utilidad_presupuesto1.Caption = "UTILIDAD";
            this.colimp_utilidad_presupuesto1.ColumnEdit = this.rtxtImporte;
            this.colimp_utilidad_presupuesto1.FieldName = "imp_utilidad_presupuesto";
            this.colimp_utilidad_presupuesto1.Name = "colimp_utilidad_presupuesto1";
            this.colimp_utilidad_presupuesto1.Visible = true;
            this.colimp_utilidad_presupuesto1.VisibleIndex = 2;
            // 
            // colprc_utilidad_presupuesto1
            // 
            this.colprc_utilidad_presupuesto1.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(224)))), ((int)(((byte)(180)))));
            this.colprc_utilidad_presupuesto1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colprc_utilidad_presupuesto1.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colprc_utilidad_presupuesto1.AppearanceCell.Options.UseBackColor = true;
            this.colprc_utilidad_presupuesto1.AppearanceCell.Options.UseFont = true;
            this.colprc_utilidad_presupuesto1.AppearanceCell.Options.UseForeColor = true;
            this.colprc_utilidad_presupuesto1.Caption = "% UTILIDAD";
            this.colprc_utilidad_presupuesto1.ColumnEdit = this.rtxtPorcentaje;
            this.colprc_utilidad_presupuesto1.FieldName = "prc_utilidad_presupuesto";
            this.colprc_utilidad_presupuesto1.Name = "colprc_utilidad_presupuesto1";
            this.colprc_utilidad_presupuesto1.Visible = true;
            this.colprc_utilidad_presupuesto1.VisibleIndex = 3;
            // 
            // rtxtPorcentaje
            // 
            this.rtxtPorcentaje.AutoHeight = false;
            this.rtxtPorcentaje.Mask.EditMask = "p2";
            this.rtxtPorcentaje.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtPorcentaje.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtPorcentaje.Name = "rtxtPorcentaje";
            // 
            // gcListaTipoProducto
            // 
            this.gcListaTipoProducto.DataSource = this.bsResumen;
            this.gcListaTipoProducto.Location = new System.Drawing.Point(12, 192);
            this.gcListaTipoProducto.MainView = this.gvListaTipoProducto;
            this.gcListaTipoProducto.MenuManager = this.ribbon;
            this.gcListaTipoProducto.Name = "gcListaTipoProducto";
            this.gcListaTipoProducto.Size = new System.Drawing.Size(332, 349);
            this.gcListaTipoProducto.TabIndex = 9;
            this.gcListaTipoProducto.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListaTipoProducto});
            // 
            // gvListaTipoProducto
            // 
            this.gvListaTipoProducto.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(84)))), ((int)(((byte)(150)))));
            this.gvListaTipoProducto.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.gvListaTipoProducto.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListaTipoProducto.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvListaTipoProducto.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvListaTipoProducto.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListaTipoProducto.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListaTipoProducto.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListaTipoProducto.ColumnPanelRowHeight = 50;
            this.gvListaTipoProducto.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_tipo_servicio,
            this.coldsc_tipo_servicio});
            this.gvListaTipoProducto.GridControl = this.gcListaTipoProducto;
            this.gvListaTipoProducto.Name = "gvListaTipoProducto";
            this.gvListaTipoProducto.OptionsBehavior.Editable = false;
            this.gvListaTipoProducto.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvListaTipoProducto.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvListaTipoProducto.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvListaTipoProducto.OptionsView.ShowGroupPanel = false;
            this.gvListaTipoProducto.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvListaTipoProducto.OptionsView.ShowIndicator = false;
            this.gvListaTipoProducto.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvListaTipoProducto.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvListaTipoProducto.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListaTipoProducto_CustomDrawColumnHeader);
            this.gvListaTipoProducto.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvListaTipoProducto_CustomDrawCell);
            // 
            // colcod_tipo_servicio
            // 
            this.colcod_tipo_servicio.FieldName = "cod_tipo_servicio";
            this.colcod_tipo_servicio.Name = "colcod_tipo_servicio";
            // 
            // coldsc_tipo_servicio
            // 
            this.coldsc_tipo_servicio.Caption = "TIPO DE PRODUCTO";
            this.coldsc_tipo_servicio.FieldName = "dsc_tipo_servicio";
            this.coldsc_tipo_servicio.Name = "coldsc_tipo_servicio";
            this.coldsc_tipo_servicio.Visible = true;
            this.coldsc_tipo_servicio.VisibleIndex = 0;
            // 
            // picBuscarProyecto
            // 
            this.picBuscarProyecto.Cursor = System.Windows.Forms.Cursors.Default;
            this.picBuscarProyecto.EditValue = ((object)(resources.GetObject("picBuscarProyecto.EditValue")));
            this.picBuscarProyecto.Location = new System.Drawing.Point(984, 68);
            this.picBuscarProyecto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picBuscarProyecto.Name = "picBuscarProyecto";
            this.picBuscarProyecto.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picBuscarProyecto.Size = new System.Drawing.Size(20, 26);
            this.picBuscarProyecto.StyleController = this.layoutControl1;
            this.picBuscarProyecto.TabIndex = 8;
            this.picBuscarProyecto.Click += new System.EventHandler(this.picBuscarProyecto_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.ImageOptions.Image")));
            this.btnBuscar.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnBuscar.ImageOptions.ImageToTextIndent = 10;
            this.btnBuscar.Location = new System.Drawing.Point(1070, 16);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(169, 44);
            this.btnBuscar.StyleController = this.layoutControl1;
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Refrescar datos";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lkpEmpresa
            // 
            this.lkpEmpresa.Location = new System.Drawing.Point(137, 25);
            this.lkpEmpresa.Name = "lkpEmpresa";
            this.lkpEmpresa.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.lkpEmpresa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lkpEmpresa.Properties.Appearance.Options.UseFont = true;
            this.lkpEmpresa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpEmpresa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_empresa", "Descripción")});
            this.lkpEmpresa.Properties.NullText = "TODOS";
            this.lkpEmpresa.Properties.PopupSizeable = false;
            this.lkpEmpresa.Size = new System.Drawing.Size(319, 26);
            this.lkpEmpresa.StyleController = this.layoutControl1;
            this.lkpEmpresa.TabIndex = 0;
            // 
            // grdbTipoMoneda
            // 
            this.grdbTipoMoneda.Location = new System.Drawing.Point(788, 22);
            this.grdbTipoMoneda.Name = "grdbTipoMoneda";
            this.grdbTipoMoneda.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.grdbTipoMoneda.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.grdbTipoMoneda.Properties.Appearance.Options.UseBackColor = true;
            this.grdbTipoMoneda.Properties.Appearance.Options.UseFont = true;
            this.grdbTipoMoneda.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.grdbTipoMoneda.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "SOLES"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "DOLARES")});
            this.grdbTipoMoneda.Size = new System.Drawing.Size(259, 32);
            this.grdbTipoMoneda.StyleController = this.layoutControl1;
            this.grdbTipoMoneda.TabIndex = 2;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(177, 68);
            this.txtDescripcion.MenuManager = this.ribbon;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtDescripcion.Properties.Appearance.Options.UseFont = true;
            this.txtDescripcion.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Size = new System.Drawing.Size(803, 26);
            this.txtDescripcion.StyleController = this.layoutControl1;
            this.txtDescripcion.TabIndex = 5;
            this.txtDescripcion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescripcion_KeyDown);
            this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion_KeyPress);
            // 
            // txtPorcentaje
            // 
            this.txtPorcentaje.EditValue = "0";
            this.txtPorcentaje.Location = new System.Drawing.Point(177, 98);
            this.txtPorcentaje.MenuManager = this.ribbon;
            this.txtPorcentaje.Name = "txtPorcentaje";
            this.txtPorcentaje.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPorcentaje.Properties.Appearance.Options.UseFont = true;
            this.txtPorcentaje.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPorcentaje.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPorcentaje.Properties.Mask.EditMask = "p2";
            this.txtPorcentaje.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPorcentaje.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPorcentaje.Size = new System.Drawing.Size(135, 26);
            this.txtPorcentaje.StyleController = this.layoutControl1;
            this.txtPorcentaje.TabIndex = 6;
            // 
            // dtFechaActualizacion
            // 
            this.dtFechaActualizacion.EditValue = null;
            this.dtFechaActualizacion.Location = new System.Drawing.Point(517, 98);
            this.dtFechaActualizacion.MenuManager = this.ribbon;
            this.dtFechaActualizacion.Name = "dtFechaActualizacion";
            this.dtFechaActualizacion.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.dtFechaActualizacion.Properties.Appearance.Options.UseFont = true;
            this.dtFechaActualizacion.Properties.Appearance.Options.UseTextOptions = true;
            this.dtFechaActualizacion.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dtFechaActualizacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaActualizacion.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaActualizacion.Size = new System.Drawing.Size(147, 26);
            this.dtFechaActualizacion.StyleController = this.layoutControl1;
            this.dtFechaActualizacion.TabIndex = 7;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem19,
            this.layoutControlItem2,
            this.emptySpaceItem4,
            this.emptySpaceItem5,
            this.emptySpaceItem6,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem7,
            this.emptySpaceItem9,
            this.layoutControlItem6,
            this.emptySpaceItem10,
            this.emptySpaceItem8,
            this.simpleLabelItem1,
            this.emptySpaceItem11,
            this.emptySpaceItem13,
            this.simpleLabelItem2,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.emptySpaceItem12,
            this.simpleLabelItem3,
            this.simpleLabelItem4,
            this.simpleLabelItem5,
            this.emptySpaceItem15});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1385, 720);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 602);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(1365, 98);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem19.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(139)))), ((int)(((byte)(125)))));
            this.layoutControlItem19.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem19.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem19.Control = this.lkpEmpresa;
            this.layoutControlItem19.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem19.CustomizationFormText = "Empresa :";
            this.layoutControlItem19.Location = new System.Drawing.Point(0, 10);
            this.layoutControlItem19.MaxSize = new System.Drawing.Size(448, 36);
            this.layoutControlItem19.MinSize = new System.Drawing.Size(448, 36);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 2);
            this.layoutControlItem19.Size = new System.Drawing.Size(448, 36);
            this.layoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem19.Text = "Empresa :";
            this.layoutControlItem19.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem19.TextSize = new System.Drawing.Size(120, 20);
            this.layoutControlItem19.TextToControlDistance = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(139)))), ((int)(((byte)(125)))));
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem2.Control = this.grdbTipoMoneda;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "Moneda de visualización :";
            this.layoutControlItem2.Location = new System.Drawing.Point(458, 10);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(581, 36);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(581, 36);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(581, 36);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Moneda de visualización :";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(298, 28);
            this.layoutControlItem2.TextToControlDistance = 20;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(448, 10);
            this.emptySpaceItem4.MaxSize = new System.Drawing.Size(10, 36);
            this.emptySpaceItem4.MinSize = new System.Drawing.Size(10, 36);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(10, 36);
            this.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(1039, 10);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.Location = new System.Drawing.Point(0, 46);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(1039, 10);
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(1235, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(130, 56);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(1039, 0);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(15, 56);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(15, 56);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(15, 56);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnBuscar;
            this.layoutControlItem1.Location = new System.Drawing.Point(1054, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(181, 56);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(181, 56);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem1.Size = new System.Drawing.Size(181, 56);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F);
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.Control = this.txtDescripcion;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 56);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(972, 30);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(972, 30);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(972, 30);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "Descripción : ";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(160, 20);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F);
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.Control = this.txtPorcentaje;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 86);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(304, 30);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(304, 30);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(304, 30);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "Porcentaje de avance :";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(160, 20);
            this.layoutControlItem4.TextToControlDistance = 5;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F);
            this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem5.Control = this.dtFechaActualizacion;
            this.layoutControlItem5.Location = new System.Drawing.Point(350, 86);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(306, 30);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(306, 30);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(306, 30);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "Fecha actualización : ";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(150, 20);
            this.layoutControlItem5.TextToControlDistance = 5;
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            this.emptySpaceItem7.Location = new System.Drawing.Point(304, 86);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(46, 30);
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem9
            // 
            this.emptySpaceItem9.AllowHotTrack = false;
            this.emptySpaceItem9.Location = new System.Drawing.Point(656, 86);
            this.emptySpaceItem9.Name = "emptySpaceItem9";
            this.emptySpaceItem9.Size = new System.Drawing.Size(709, 30);
            this.emptySpaceItem9.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.picBuscarProyecto;
            this.layoutControlItem6.Location = new System.Drawing.Point(972, 56);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(24, 30);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(24, 30);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(24, 30);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem10
            // 
            this.emptySpaceItem10.AllowHotTrack = false;
            this.emptySpaceItem10.Location = new System.Drawing.Point(0, 169);
            this.emptySpaceItem10.MaxSize = new System.Drawing.Size(0, 11);
            this.emptySpaceItem10.MinSize = new System.Drawing.Size(10, 11);
            this.emptySpaceItem10.Name = "emptySpaceItem10";
            this.emptySpaceItem10.Size = new System.Drawing.Size(1365, 11);
            this.emptySpaceItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem10.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem8
            // 
            this.emptySpaceItem8.AllowHotTrack = false;
            this.emptySpaceItem8.Location = new System.Drawing.Point(996, 56);
            this.emptySpaceItem8.Name = "emptySpaceItem8";
            this.emptySpaceItem8.Size = new System.Drawing.Size(369, 30);
            this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(84)))), ((int)(((byte)(150)))));
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.White;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseBackColor = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 141);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(1365, 28);
            this.simpleLabelItem1.Text = "RESUMEN DE PRESUPUESTO Y EJECUCION EN SOLES";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(530, 24);
            // 
            // emptySpaceItem11
            // 
            this.emptySpaceItem11.AllowHotTrack = false;
            this.emptySpaceItem11.Location = new System.Drawing.Point(0, 116);
            this.emptySpaceItem11.Name = "emptySpaceItem11";
            this.emptySpaceItem11.Size = new System.Drawing.Size(1365, 25);
            this.emptySpaceItem11.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem13
            // 
            this.emptySpaceItem13.AllowHotTrack = false;
            this.emptySpaceItem13.Location = new System.Drawing.Point(0, 553);
            this.emptySpaceItem13.MaxSize = new System.Drawing.Size(0, 21);
            this.emptySpaceItem13.MinSize = new System.Drawing.Size(10, 21);
            this.emptySpaceItem13.Name = "emptySpaceItem13";
            this.emptySpaceItem13.Size = new System.Drawing.Size(1365, 21);
            this.emptySpaceItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem13.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(84)))), ((int)(((byte)(150)))));
            this.simpleLabelItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.White;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseBackColor = true;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleLabelItem2.Location = new System.Drawing.Point(0, 574);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(1365, 28);
            this.simpleLabelItem2.Text = "INFORMACION GENERAL";
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(530, 24);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.gcListaTipoProducto;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 180);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(336, 353);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.gcListaPresupuesto;
            this.layoutControlItem8.Location = new System.Drawing.Point(371, 206);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(492, 0);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(492, 24);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem8.Size = new System.Drawing.Size(492, 327);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.gcListaEjecutado;
            this.layoutControlItem9.Location = new System.Drawing.Point(873, 206);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(492, 0);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(492, 24);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem9.Size = new System.Drawing.Size(492, 327);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // emptySpaceItem12
            // 
            this.emptySpaceItem12.AllowHotTrack = false;
            this.emptySpaceItem12.Location = new System.Drawing.Point(863, 180);
            this.emptySpaceItem12.MaxSize = new System.Drawing.Size(10, 0);
            this.emptySpaceItem12.MinSize = new System.Drawing.Size(10, 10);
            this.emptySpaceItem12.Name = "emptySpaceItem12";
            this.emptySpaceItem12.Size = new System.Drawing.Size(10, 353);
            this.emptySpaceItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem12.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem3
            // 
            this.simpleLabelItem3.AllowHotTrack = false;
            this.simpleLabelItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem3.Location = new System.Drawing.Point(0, 533);
            this.simpleLabelItem3.MaxSize = new System.Drawing.Size(0, 20);
            this.simpleLabelItem3.MinSize = new System.Drawing.Size(534, 20);
            this.simpleLabelItem3.Name = "simpleLabelItem3";
            this.simpleLabelItem3.Size = new System.Drawing.Size(1365, 20);
            this.simpleLabelItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem3.Text = "No se incluye el IGV";
            this.simpleLabelItem3.TextSize = new System.Drawing.Size(530, 16);
            // 
            // simpleLabelItem4
            // 
            this.simpleLabelItem4.AllowHotTrack = false;
            this.simpleLabelItem4.AppearanceItemCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(86)))), ((int)(((byte)(35)))));
            this.simpleLabelItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem4.AppearanceItemCaption.ForeColor = System.Drawing.Color.White;
            this.simpleLabelItem4.AppearanceItemCaption.Options.UseBackColor = true;
            this.simpleLabelItem4.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem4.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem4.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItem4.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleLabelItem4.Location = new System.Drawing.Point(371, 180);
            this.simpleLabelItem4.MaxSize = new System.Drawing.Size(0, 26);
            this.simpleLabelItem4.MinSize = new System.Drawing.Size(100, 20);
            this.simpleLabelItem4.Name = "simpleLabelItem4";
            this.simpleLabelItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.simpleLabelItem4.Size = new System.Drawing.Size(492, 26);
            this.simpleLabelItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem4.Text = "PRESUPUESTO";
            this.simpleLabelItem4.TextSize = new System.Drawing.Size(530, 18);
            // 
            // simpleLabelItem5
            // 
            this.simpleLabelItem5.AllowHotTrack = false;
            this.simpleLabelItem5.AppearanceItemCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(60)))), ((int)(((byte)(12)))));
            this.simpleLabelItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem5.AppearanceItemCaption.ForeColor = System.Drawing.Color.White;
            this.simpleLabelItem5.AppearanceItemCaption.Options.UseBackColor = true;
            this.simpleLabelItem5.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem5.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem5.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItem5.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleLabelItem5.Location = new System.Drawing.Point(873, 180);
            this.simpleLabelItem5.MaxSize = new System.Drawing.Size(0, 26);
            this.simpleLabelItem5.MinSize = new System.Drawing.Size(100, 26);
            this.simpleLabelItem5.Name = "simpleLabelItem5";
            this.simpleLabelItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.simpleLabelItem5.Size = new System.Drawing.Size(492, 26);
            this.simpleLabelItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem5.Text = "EJECUTADO";
            this.simpleLabelItem5.TextSize = new System.Drawing.Size(530, 18);
            // 
            // emptySpaceItem15
            // 
            this.emptySpaceItem15.AllowHotTrack = false;
            this.emptySpaceItem15.Location = new System.Drawing.Point(336, 180);
            this.emptySpaceItem15.MaxSize = new System.Drawing.Size(35, 0);
            this.emptySpaceItem15.MinSize = new System.Drawing.Size(35, 10);
            this.emptySpaceItem15.Name = "emptySpaceItem15";
            this.emptySpaceItem15.Size = new System.Drawing.Size(35, 353);
            this.emptySpaceItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem15.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmResumenPresupuestoEjecucion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1385, 902);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "frmResumenPresupuestoEjecucion";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Resumen de presupues y ejecución";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmResumenPresupuestoEjecucion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListaEjecutado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsResumen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaEjecutado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentaje2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaPresupuesto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaPresupuesto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentaje)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaTipoProducto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaTipoProducto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBuscarProyecto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEmpresa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdbTipoMoneda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPorcentaje.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaActualizacion.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaActualizacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem15)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoReportes;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.LookUpEdit lkpEmpresa;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraEditors.RadioGroup grdbTipoMoneda;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraEditors.TextEdit txtDescripcion;
        private DevExpress.XtraEditors.TextEdit txtPorcentaje;
        private DevExpress.XtraEditors.DateEdit dtFechaActualizacion;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem9;
        private DevExpress.XtraEditors.PictureEdit picBuscarProyecto;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem10;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraGrid.GridControl gcListaEjecutado;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListaEjecutado;
        private DevExpress.XtraGrid.GridControl gcListaPresupuesto;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListaPresupuesto;
        private DevExpress.XtraGrid.GridControl gcListaTipoProducto;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListaTipoProducto;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem11;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem13;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem12;
        private System.Windows.Forms.BindingSource bsResumen;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_venta_ejecutado2;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_costo_ejecutado2;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_utilidad_ejecutado2;
        private DevExpress.XtraGrid.Columns.GridColumn colprc_utilidad_ejecutado2;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_venta_presupuesto1;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_costo_presupuesto1;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_utilidad_presupuesto1;
        private DevExpress.XtraGrid.Columns.GridColumn colprc_utilidad_presupuesto1;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_tipo_servicio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_tipo_servicio;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem3;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem4;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem5;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtImporte2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtPorcentaje2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtImporte;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtPorcentaje;
        private DevExpress.XtraBars.BarButtonItem btnExportarExcel;
        private DevExpress.XtraBars.BarButtonItem btnImprimir;
        private DevExpress.XtraBars.BarButtonItem btnListarProyectos;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoAcciones;
        private DevExpress.XtraBars.BarButtonItem btnTarifarioServicioProducto;
    }
}