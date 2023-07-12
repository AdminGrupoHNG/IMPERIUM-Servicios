
namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    partial class frmListarProyectos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListarProyectos));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.gcListadoProyecto = new DevExpress.XtraGrid.GridControl();
            this.bsListadoProyecto = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoProyecto = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_proyecto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_proyecto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_activo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnCrearProyecto = new DevExpress.XtraBars.BarButtonItem();
            this.btnAgregarProducto = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.gcListadoProyectoServicio = new DevExpress.XtraGrid.GridControl();
            this.bsListadoProyectoServicio = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoProyectoServicio = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.coldsc_tipo_servicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_subtipo_servicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_producto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colctd_requerida = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblNombreEmpresa = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoProyecto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoProyecto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoProyecto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoProyectoServicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoProyectoServicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoProyectoServicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNombreEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.splitContainerControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1127, 644);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Location = new System.Drawing.Point(7, 35);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.layoutControl3);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.layoutControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1113, 602);
            this.splitContainerControl1.SplitterPosition = 397;
            this.splitContainerControl1.TabIndex = 6;
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.gcListadoProyecto);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup2;
            this.layoutControl3.Size = new System.Drawing.Size(397, 602);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // gcListadoProyecto
            // 
            this.gcListadoProyecto.DataSource = this.bsListadoProyecto;
            this.gcListadoProyecto.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListadoProyecto.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListadoProyecto.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListadoProyecto.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListadoProyecto.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListadoProyecto.Location = new System.Drawing.Point(4, 4);
            this.gcListadoProyecto.MainView = this.gvListadoProyecto;
            this.gcListadoProyecto.MenuManager = this.barManager1;
            this.gcListadoProyecto.Name = "gcListadoProyecto";
            this.gcListadoProyecto.Size = new System.Drawing.Size(389, 594);
            this.gcListadoProyecto.TabIndex = 4;
            this.gcListadoProyecto.UseEmbeddedNavigator = true;
            this.gcListadoProyecto.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListadoProyecto});
            // 
            // bsListadoProyecto
            // 
            this.bsListadoProyecto.DataSource = typeof(BE_Servicios.eProyecto);
            // 
            // gvListadoProyecto
            // 
            this.gvListadoProyecto.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListadoProyecto.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListadoProyecto.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListadoProyecto.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListadoProyecto.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvListadoProyecto.ColumnPanelRowHeight = 35;
            this.gvListadoProyecto.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_proyecto,
            this.coldsc_proyecto,
            this.colflg_activo});
            this.gvListadoProyecto.GridControl = this.gcListadoProyecto;
            this.gvListadoProyecto.Name = "gvListadoProyecto";
            this.gvListadoProyecto.OptionsBehavior.Editable = false;
            this.gvListadoProyecto.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListadoProyecto.OptionsView.ShowGroupPanel = false;
            this.gvListadoProyecto.OptionsView.ShowIndicator = false;
            this.gvListadoProyecto.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListadoProyecto_RowClick);
            this.gvListadoProyecto.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListadoProyecto_CustomDrawColumnHeader);
            this.gvListadoProyecto.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvListadoProyecto_CustomDrawCell);
            this.gvListadoProyecto.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListadoProyecto_RowStyle);
            this.gvListadoProyecto.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvListadoProyecto_FocusedRowChanged);
            // 
            // colcod_proyecto
            // 
            this.colcod_proyecto.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_proyecto.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_proyecto.Caption = "N°";
            this.colcod_proyecto.FieldName = "cod_proyecto";
            this.colcod_proyecto.Name = "colcod_proyecto";
            this.colcod_proyecto.OptionsColumn.FixedWidth = true;
            this.colcod_proyecto.Visible = true;
            this.colcod_proyecto.VisibleIndex = 0;
            this.colcod_proyecto.Width = 100;
            // 
            // coldsc_proyecto
            // 
            this.coldsc_proyecto.Caption = "Descripción";
            this.coldsc_proyecto.FieldName = "dsc_proyecto";
            this.coldsc_proyecto.Name = "coldsc_proyecto";
            this.coldsc_proyecto.Visible = true;
            this.coldsc_proyecto.VisibleIndex = 1;
            this.coldsc_proyecto.Width = 260;
            // 
            // colflg_activo
            // 
            this.colflg_activo.AppearanceCell.Options.UseTextOptions = true;
            this.colflg_activo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_activo.Caption = "Flag Activo";
            this.colflg_activo.FieldName = "flg_activo";
            this.colflg_activo.Name = "colflg_activo";
            this.colflg_activo.OptionsColumn.FixedWidth = true;
            this.colflg_activo.Visible = true;
            this.colflg_activo.VisibleIndex = 2;
            this.colflg_activo.Width = 50;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnCrearProyecto,
            this.btnAgregarProducto});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 2;
            // 
            // bar2
            // 
            this.bar2.BarName = "Menú principal";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCrearProyecto),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAgregarProducto)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Menú principal";
            // 
            // btnCrearProyecto
            // 
            this.btnCrearProyecto.Caption = "Crear Proyecto";
            this.btnCrearProyecto.Id = 0;
            this.btnCrearProyecto.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCrearProyecto.ImageOptions.Image")));
            this.btnCrearProyecto.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCrearProyecto.ItemAppearance.Normal.Options.UseFont = true;
            this.btnCrearProyecto.Name = "btnCrearProyecto";
            this.btnCrearProyecto.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnCrearProyecto.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCrearProyecto_ItemClick);
            // 
            // btnAgregarProducto
            // 
            this.btnAgregarProducto.Caption = "Agregar Producto";
            this.btnAgregarProducto.Id = 1;
            this.btnAgregarProducto.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarProducto.ImageOptions.Image")));
            this.btnAgregarProducto.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAgregarProducto.ItemAppearance.Normal.Options.UseFont = true;
            this.btnAgregarProducto.Name = "btnAgregarProducto";
            this.btnAgregarProducto.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAgregarProducto.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAgregarProducto_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1127, 40);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 684);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1127, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 40);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 644);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1127, 40);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 644);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup2.Size = new System.Drawing.Size(397, 602);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListadoProyecto;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(393, 598);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.gcListadoProyectoServicio);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(706, 602);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // gcListadoProyectoServicio
            // 
            this.gcListadoProyectoServicio.DataSource = this.bsListadoProyectoServicio;
            this.gcListadoProyectoServicio.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListadoProyectoServicio.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListadoProyectoServicio.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListadoProyectoServicio.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListadoProyectoServicio.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListadoProyectoServicio.Location = new System.Drawing.Point(4, 4);
            this.gcListadoProyectoServicio.MainView = this.gvListadoProyectoServicio;
            this.gcListadoProyectoServicio.MenuManager = this.barManager1;
            this.gcListadoProyectoServicio.Name = "gcListadoProyectoServicio";
            this.gcListadoProyectoServicio.Size = new System.Drawing.Size(698, 594);
            this.gcListadoProyectoServicio.TabIndex = 5;
            this.gcListadoProyectoServicio.UseEmbeddedNavigator = true;
            this.gcListadoProyectoServicio.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListadoProyectoServicio});
            // 
            // bsListadoProyectoServicio
            // 
            this.bsListadoProyectoServicio.DataSource = typeof(BE_Servicios.eProyecto.eProyecto_Producto_Receta);
            // 
            // gvListadoProyectoServicio
            // 
            this.gvListadoProyectoServicio.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListadoProyectoServicio.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListadoProyectoServicio.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListadoProyectoServicio.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListadoProyectoServicio.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvListadoProyectoServicio.ColumnPanelRowHeight = 35;
            this.gvListadoProyectoServicio.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.coldsc_tipo_servicio,
            this.coldsc_subtipo_servicio,
            this.coldsc_producto,
            this.colctd_requerida});
            this.gvListadoProyectoServicio.GridControl = this.gcListadoProyectoServicio;
            this.gvListadoProyectoServicio.Name = "gvListadoProyectoServicio";
            this.gvListadoProyectoServicio.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListadoProyectoServicio.OptionsView.ShowGroupPanel = false;
            this.gvListadoProyectoServicio.OptionsView.ShowIndicator = false;
            this.gvListadoProyectoServicio.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListadoProyectoServicio_CustomDrawColumnHeader);
            this.gvListadoProyectoServicio.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvListadoProyectoServicio_CustomDrawCell);
            this.gvListadoProyectoServicio.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListadoProyectoServicio_RowStyle);
            this.gvListadoProyectoServicio.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvListadoProyectoServicio_CellValueChanged);
            // 
            // coldsc_tipo_servicio
            // 
            this.coldsc_tipo_servicio.Caption = "Tipo Servicio";
            this.coldsc_tipo_servicio.FieldName = "dsc_tipo_servicio";
            this.coldsc_tipo_servicio.Name = "coldsc_tipo_servicio";
            this.coldsc_tipo_servicio.OptionsColumn.AllowEdit = false;
            this.coldsc_tipo_servicio.OptionsColumn.FixedWidth = true;
            this.coldsc_tipo_servicio.Visible = true;
            this.coldsc_tipo_servicio.VisibleIndex = 0;
            this.coldsc_tipo_servicio.Width = 150;
            // 
            // coldsc_subtipo_servicio
            // 
            this.coldsc_subtipo_servicio.Caption = "SubTipo Servicio";
            this.coldsc_subtipo_servicio.FieldName = "dsc_subtipo_servicio";
            this.coldsc_subtipo_servicio.Name = "coldsc_subtipo_servicio";
            this.coldsc_subtipo_servicio.OptionsColumn.AllowEdit = false;
            this.coldsc_subtipo_servicio.OptionsColumn.FixedWidth = true;
            this.coldsc_subtipo_servicio.Visible = true;
            this.coldsc_subtipo_servicio.VisibleIndex = 1;
            this.coldsc_subtipo_servicio.Width = 150;
            // 
            // coldsc_producto
            // 
            this.coldsc_producto.Caption = "Producto";
            this.coldsc_producto.FieldName = "dsc_producto";
            this.coldsc_producto.Name = "coldsc_producto";
            this.coldsc_producto.OptionsColumn.AllowEdit = false;
            this.coldsc_producto.OptionsColumn.FixedWidth = true;
            this.coldsc_producto.Visible = true;
            this.coldsc_producto.VisibleIndex = 2;
            this.coldsc_producto.Width = 230;
            // 
            // colctd_requerida
            // 
            this.colctd_requerida.AppearanceCell.Options.UseTextOptions = true;
            this.colctd_requerida.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colctd_requerida.Caption = "Cantidad";
            this.colctd_requerida.FieldName = "ctd_requerida";
            this.colctd_requerida.Name = "colctd_requerida";
            this.colctd_requerida.OptionsColumn.FixedWidth = true;
            this.colctd_requerida.Visible = true;
            this.colctd_requerida.VisibleIndex = 3;
            this.colctd_requerida.Width = 60;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup1.Size = new System.Drawing.Size(706, 602);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcListadoProyectoServicio;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(702, 598);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.simpleLabelItem1,
            this.lblNombreEmpresa,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(1127, 644);
            this.Root.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.Green;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem1.MaxSize = new System.Drawing.Size(119, 28);
            this.simpleLabelItem1.MinSize = new System.Drawing.Size(119, 28);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(119, 28);
            this.simpleLabelItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem1.Text = "EMPRESA:";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(256, 24);
            // 
            // lblNombreEmpresa
            // 
            this.lblNombreEmpresa.AllowHotTrack = false;
            this.lblNombreEmpresa.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblNombreEmpresa.AppearanceItemCaption.ForeColor = System.Drawing.Color.Green;
            this.lblNombreEmpresa.AppearanceItemCaption.Options.UseFont = true;
            this.lblNombreEmpresa.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblNombreEmpresa.Location = new System.Drawing.Point(119, 0);
            this.lblNombreEmpresa.Name = "lblNombreEmpresa";
            this.lblNombreEmpresa.Size = new System.Drawing.Size(998, 28);
            this.lblNombreEmpresa.Text = "<<NOMBRE EMPRESA>>";
            this.lblNombreEmpresa.TextSize = new System.Drawing.Size(256, 24);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.splitContainerControl1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1117, 606);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmListarProyectos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1127, 684);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.ShowIcon = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListarProyectos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Crear Proyecto";
            this.Load += new System.EventHandler(this.frmListarProyectos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoProyecto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoProyecto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoProyecto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoProyectoServicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoProyectoServicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoProyectoServicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNombreEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem btnCrearProyecto;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gcListadoProyecto;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoProyecto;
        private System.Windows.Forms.BindingSource bsListadoProyecto;
        private System.Windows.Forms.BindingSource bsListadoProyectoServicio;
        private DevExpress.XtraGrid.GridControl gcListadoProyectoServicio;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoProyectoServicio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_tipo_servicio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_subtipo_servicio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_producto;
        private DevExpress.XtraGrid.Columns.GridColumn colctd_requerida;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_proyecto;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_proyecto;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_activo;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblNombreEmpresa;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraBars.BarButtonItem btnAgregarProducto;
    }
}