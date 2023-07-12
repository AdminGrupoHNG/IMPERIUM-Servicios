namespace UI_Servicios.Formularios.Sistema.Configuracion_del_Sistema
{
    partial class frmOpcionesSistema
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOpcionesSistema));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.btnActivo = new DevExpress.XtraBars.BarButtonItem();
            this.btnInactivo = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminar = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportarExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnImprimir = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.picTitulo = new DevExpress.XtraEditors.PictureEdit();
            this.lblTitulo = new DevExpress.XtraEditors.LabelControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.gcVentana = new DevExpress.XtraGrid.GridControl();
            this.bsVentana = new System.Windows.Forms.BindingSource(this.components);
            this.gvVentana = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colnum_orden = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_ventana = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldcs_grupo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_menu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_formulario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_activa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTitulo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcVentana)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVentana)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVentana)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.btnNuevo,
            this.btnActivo,
            this.btnInactivo,
            this.btnEliminar,
            this.btnExportarExcel,
            this.btnImprimir});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 7;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(867, 158);
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
            // btnActivo
            // 
            this.btnActivo.Caption = "Activo";
            this.btnActivo.Id = 2;
            this.btnActivo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnActivo.ImageOptions.Image")));
            this.btnActivo.Name = "btnActivo";
            this.btnActivo.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnActivo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnActivo_ItemClick);
            // 
            // btnInactivo
            // 
            this.btnInactivo.Caption = "Inactivo";
            this.btnInactivo.Id = 3;
            this.btnInactivo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnInactivo.ImageOptions.Image")));
            this.btnInactivo.Name = "btnInactivo";
            this.btnInactivo.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnInactivo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnInactivo_ItemClick);
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
            this.btnExportarExcel.Caption = "Exportar en Excel";
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
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Opciones de Sistema";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnNuevo);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnActivo);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnInactivo);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnEliminar);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Edición";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btnExportarExcel);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnImprimir);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Reportes";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 622);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(867, 24);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.picTitulo);
            this.layoutControl1.Controls.Add(this.lblTitulo);
            this.layoutControl1.Controls.Add(this.navBarControl1);
            this.layoutControl1.Controls.Add(this.gcVentana);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 158);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(867, 464);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // picTitulo
            // 
            this.picTitulo.Location = new System.Drawing.Point(71, 12);
            this.picTitulo.MenuManager = this.ribbon;
            this.picTitulo.Name = "picTitulo";
            this.picTitulo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picTitulo.Properties.Appearance.Options.UseBackColor = true;
            this.picTitulo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picTitulo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picTitulo.Size = new System.Drawing.Size(57, 40);
            this.picTitulo.StyleController = this.layoutControl1;
            this.picTitulo.TabIndex = 9;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(49)))), ((int)(((byte)(35)))));
            this.lblTitulo.Appearance.Options.UseFont = true;
            this.lblTitulo.Appearance.Options.UseForeColor = true;
            this.lblTitulo.Location = new System.Drawing.Point(132, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(723, 40);
            this.lblTitulo.StyleController = this.layoutControl1;
            this.lblTitulo.TabIndex = 8;
            this.lblTitulo.Text = "<<Titulo de grupo>>";
            // 
            // navBarControl1
            // 
            this.navBarControl1.BackColor = System.Drawing.Color.Transparent;
            this.navBarControl1.LinkSelectionMode = DevExpress.XtraNavBar.LinkSelectionModeType.OneInGroup;
            this.navBarControl1.Location = new System.Drawing.Point(12, 12);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.NavigationPaneGroupClientHeight = 160;
            this.navBarControl1.NavigationPaneMaxVisibleGroups = 5;
            this.navBarControl1.OptionsNavPane.CollapsedWidth = 55;
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 164;
            this.navBarControl1.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Collapsed;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControl1.Size = new System.Drawing.Size(55, 440);
            this.navBarControl1.TabIndex = 5;
            this.navBarControl1.Text = "navBarControl1";
            this.navBarControl1.SelectedLinkChanged += new DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventHandler(this.navBarControl1_SelectedLinkChanged);
            this.navBarControl1.ActiveGroupChanged += new DevExpress.XtraNavBar.NavBarGroupEventHandler(this.navBarControl1_ActiveGroupChanged);
            // 
            // gcVentana
            // 
            this.gcVentana.DataSource = this.bsVentana;
            this.gcVentana.EmbeddedNavigator.Buttons.Append.Enabled = false;
            this.gcVentana.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcVentana.EmbeddedNavigator.Buttons.CancelEdit.Enabled = false;
            this.gcVentana.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcVentana.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.gcVentana.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcVentana.EmbeddedNavigator.Buttons.EndEdit.Enabled = false;
            this.gcVentana.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcVentana.EmbeddedNavigator.Buttons.First.Enabled = false;
            this.gcVentana.EmbeddedNavigator.Buttons.First.Visible = false;
            this.gcVentana.EmbeddedNavigator.Buttons.Last.Enabled = false;
            this.gcVentana.EmbeddedNavigator.Buttons.Last.Visible = false;
            this.gcVentana.EmbeddedNavigator.Buttons.Next.Enabled = false;
            this.gcVentana.EmbeddedNavigator.Buttons.Next.Visible = false;
            this.gcVentana.EmbeddedNavigator.Buttons.NextPage.Enabled = false;
            this.gcVentana.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.gcVentana.EmbeddedNavigator.Buttons.Prev.Enabled = false;
            this.gcVentana.EmbeddedNavigator.Buttons.Prev.Visible = false;
            this.gcVentana.EmbeddedNavigator.Buttons.PrevPage.Enabled = false;
            this.gcVentana.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.gcVentana.EmbeddedNavigator.Buttons.Remove.Enabled = false;
            this.gcVentana.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcVentana.Location = new System.Drawing.Point(71, 56);
            this.gcVentana.MainView = this.gvVentana;
            this.gcVentana.MenuManager = this.ribbon;
            this.gcVentana.Name = "gcVentana";
            this.gcVentana.Size = new System.Drawing.Size(784, 396);
            this.gcVentana.TabIndex = 4;
            this.gcVentana.UseEmbeddedNavigator = true;
            this.gcVentana.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvVentana});
            // 
            // bsVentana
            // 
            this.bsVentana.DataSource = typeof(BE_Servicios.eVentana);
            // 
            // gvVentana
            // 
            this.gvVentana.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.gvVentana.Appearance.GroupRow.ForeColor = System.Drawing.Color.DodgerBlue;
            this.gvVentana.Appearance.GroupRow.Options.UseFont = true;
            this.gvVentana.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvVentana.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvVentana.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvVentana.AppearancePrint.GroupFooter.BackColor = System.Drawing.Color.Orange;
            this.gvVentana.AppearancePrint.GroupFooter.Options.UseBackColor = true;
            this.gvVentana.AppearancePrint.GroupRow.BackColor = System.Drawing.Color.Orange;
            this.gvVentana.AppearancePrint.GroupRow.Options.UseBackColor = true;
            this.gvVentana.ColumnPanelRowHeight = 35;
            this.gvVentana.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colnum_orden,
            this.coldsc_ventana,
            this.coldcs_grupo,
            this.coldsc_menu,
            this.coldsc_formulario,
            this.colflg_activa});
            this.gvVentana.GridControl = this.gcVentana;
            this.gvVentana.GroupCount = 1;
            this.gvVentana.Name = "gvVentana";
            this.gvVentana.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.gvVentana.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvVentana.OptionsBehavior.Editable = false;
            this.gvVentana.OptionsPrint.ExpandAllDetails = true;
            this.gvVentana.OptionsView.EnableAppearanceEvenRow = true;
            this.gvVentana.OptionsView.ShowGroupPanel = false;
            this.gvVentana.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.coldcs_grupo, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvVentana.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvVentana_RowClick);
            this.gvVentana.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvVentana_CustomDrawColumnHeader);
            this.gvVentana.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvVentana_RowCellStyle);
            this.gvVentana.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvVentana_RowStyle);
            this.gvVentana.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvVentana_KeyDown);
            // 
            // colnum_orden
            // 
            this.colnum_orden.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_orden.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_orden.AppearanceHeader.Options.UseTextOptions = true;
            this.colnum_orden.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_orden.Caption = "N. Orden";
            this.colnum_orden.FieldName = "num_orden";
            this.colnum_orden.Name = "colnum_orden";
            this.colnum_orden.Visible = true;
            this.colnum_orden.VisibleIndex = 0;
            this.colnum_orden.Width = 69;
            // 
            // coldsc_ventana
            // 
            this.coldsc_ventana.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_ventana.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_ventana.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_ventana.Caption = "Ventana";
            this.coldsc_ventana.FieldName = "dsc_ventana";
            this.coldsc_ventana.Name = "coldsc_ventana";
            this.coldsc_ventana.Visible = true;
            this.coldsc_ventana.VisibleIndex = 1;
            this.coldsc_ventana.Width = 162;
            // 
            // coldcs_grupo
            // 
            this.coldcs_grupo.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.coldcs_grupo.AppearanceHeader.Options.UseFont = true;
            this.coldcs_grupo.AppearanceHeader.Options.UseTextOptions = true;
            this.coldcs_grupo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldcs_grupo.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldcs_grupo.Caption = "Modulo";
            this.coldcs_grupo.FieldName = "dsc_grupo";
            this.coldcs_grupo.Name = "coldcs_grupo";
            this.coldcs_grupo.OptionsColumn.AllowEdit = false;
            this.coldcs_grupo.Visible = true;
            this.coldcs_grupo.VisibleIndex = 1;
            this.coldcs_grupo.Width = 129;
            // 
            // coldsc_menu
            // 
            this.coldsc_menu.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_menu.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_menu.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_menu.Caption = "Opción del Menú";
            this.coldsc_menu.FieldName = "dsc_menu";
            this.coldsc_menu.Name = "coldsc_menu";
            this.coldsc_menu.Visible = true;
            this.coldsc_menu.VisibleIndex = 2;
            this.coldsc_menu.Width = 171;
            // 
            // coldsc_formulario
            // 
            this.coldsc_formulario.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_formulario.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_formulario.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_formulario.Caption = "Formulario";
            this.coldsc_formulario.FieldName = "dsc_formulario";
            this.coldsc_formulario.Name = "coldsc_formulario";
            this.coldsc_formulario.Visible = true;
            this.coldsc_formulario.VisibleIndex = 3;
            this.coldsc_formulario.Width = 167;
            // 
            // colflg_activa
            // 
            this.colflg_activa.AppearanceCell.Options.UseTextOptions = true;
            this.colflg_activa.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_activa.AppearanceHeader.Options.UseTextOptions = true;
            this.colflg_activa.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_activa.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colflg_activa.Caption = "Activo";
            this.colflg_activa.FieldName = "flg_activo";
            this.colflg_activa.Name = "colflg_activa";
            this.colflg_activa.Visible = true;
            this.colflg_activa.VisibleIndex = 4;
            this.colflg_activa.Width = 97;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem3});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(867, 464);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcVentana;
            this.layoutControlItem1.Location = new System.Drawing.Point(59, 44);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(788, 400);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.navBarControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(59, 444);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.picTitulo;
            this.layoutControlItem4.Location = new System.Drawing.Point(59, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(61, 44);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(61, 44);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(61, 44);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lblTitulo;
            this.layoutControlItem3.Location = new System.Drawing.Point(120, 0);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(222, 28);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(727, 44);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmOpcionesSistema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 646);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.KeyPreview = true;
            this.Name = "frmOpcionesSistema";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Opciones de Sistemas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmOpcionesSistema_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOpcionesSistema_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTitulo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcVentana)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVentana)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVentana)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem btnNuevo;
        private DevExpress.XtraBars.BarButtonItem btnActivo;
        private DevExpress.XtraBars.BarButtonItem btnInactivo;
        private DevExpress.XtraBars.BarButtonItem btnEliminar;
        private DevExpress.XtraBars.BarButtonItem btnExportarExcel;
        private DevExpress.XtraBars.BarButtonItem btnImprimir;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource bsVentana;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_ventana;
        private DevExpress.XtraGrid.Columns.GridColumn coldcs_grupo;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_menu;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_formulario;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_activa;
        internal DevExpress.XtraGrid.Views.Grid.GridView gvVentana;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_orden;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LabelControl lblTitulo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.PictureEdit picTitulo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        public DevExpress.XtraGrid.GridControl gcVentana;
    }
}