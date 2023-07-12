namespace UI_Servicios.Formularios.Sistema.Configuracion_del_Sistema
{
    partial class frmAsignacionPermiso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAsignacionPermiso));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.btnActivar = new DevExpress.XtraBars.BarButtonItem();
            this.btnInactivar = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminar = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportarExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnImprimir = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblTitulo = new DevExpress.XtraEditors.LabelControl();
            this.picTitulo = new DevExpress.XtraEditors.PictureEdit();
            this.pivotGridControl1 = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.bsPivot = new System.Windows.Forms.BindingSource(this.components);
            this.fieldValorE = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldValorL = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldVentana = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldModulo = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldPerfil = new DevExpress.XtraPivotGrid.PivotGridField();
            this.gcVentana = new DevExpress.XtraGrid.GridControl();
            this.bsVentana = new System.Windows.Forms.BindingSource(this.components);
            this.gvVentana = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.coldsc_ventana = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldcs_grupo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_escritura = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkEscritura = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colflg_lectura = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkLectura = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.gcPerfiles = new DevExpress.XtraGrid.GridControl();
            this.bsPerfiles = new System.Windows.Forms.BindingSource(this.components);
            this.gvPerfiles = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.coldsc_perfil1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutReporteExcel = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.pivotGridField1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.colcod_perfil = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTitulo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPivot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcVentana)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVentana)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVentana)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkEscritura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkLectura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPerfiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPerfiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPerfiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutReporteExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
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
            this.btnImprimir});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 7;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1059, 158);
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
            this.btnExportarExcel.Caption = "Exportar Excel";
            this.btnExportarExcel.Id = 5;
            this.btnExportarExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.ImageOptions.Image")));
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnExportarExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportarExcel_ItemClick);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Caption = "Imprimir";
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Id = 6;
            this.btnImprimir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.ImageOptions.Image")));
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Opciones de Permisos";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnNuevo);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnActivar);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnInactivar);
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
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblTitulo);
            this.layoutControl1.Controls.Add(this.picTitulo);
            this.layoutControl1.Controls.Add(this.pivotGridControl1);
            this.layoutControl1.Controls.Add(this.gcVentana);
            this.layoutControl1.Controls.Add(this.navBarControl1);
            this.layoutControl1.Controls.Add(this.gcPerfiles);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 158);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1059, 546);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(49)))), ((int)(((byte)(35)))));
            this.lblTitulo.Appearance.Options.UseFont = true;
            this.lblTitulo.Appearance.Options.UseForeColor = true;
            this.lblTitulo.Location = new System.Drawing.Point(134, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(913, 40);
            this.lblTitulo.StyleController = this.layoutControl1;
            this.lblTitulo.TabIndex = 10;
            this.lblTitulo.Text = "<<Titulo de grupo>>";
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
            this.picTitulo.Size = new System.Drawing.Size(59, 40);
            this.picTitulo.StyleController = this.layoutControl1;
            this.picTitulo.TabIndex = 11;
            // 
            // pivotGridControl1
            // 
            this.pivotGridControl1.Appearance.ColumnHeaderArea.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pivotGridControl1.Appearance.ColumnHeaderArea.Options.UseFont = true;
            this.pivotGridControl1.Appearance.ColumnHeaderArea.Options.UseTextOptions = true;
            this.pivotGridControl1.Appearance.ColumnHeaderArea.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridControl1.Appearance.ColumnHeaderArea.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.pivotGridControl1.DataSource = this.bsPivot;
            this.pivotGridControl1.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.fieldValorE,
            this.fieldValorL,
            this.fieldVentana,
            this.fieldModulo,
            this.fieldPerfil});
            this.pivotGridControl1.Location = new System.Drawing.Point(424, 400);
            this.pivotGridControl1.MenuManager = this.ribbon;
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.OptionsPrint.PrintColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.pivotGridControl1.OptionsPrint.PrintDataHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.pivotGridControl1.OptionsView.RowTreeWidth = 200;
            this.pivotGridControl1.OptionsView.ShowColumnGrandTotalHeader = false;
            this.pivotGridControl1.OptionsView.ShowColumnGrandTotals = false;
            this.pivotGridControl1.OptionsView.ShowRowGrandTotalHeader = false;
            this.pivotGridControl1.OptionsView.ShowRowGrandTotals = false;
            this.pivotGridControl1.OptionsView.ShowRowTotals = false;
            this.pivotGridControl1.Size = new System.Drawing.Size(623, 134);
            this.pivotGridControl1.TabIndex = 7;
            // 
            // bsPivot
            // 
            this.bsPivot.DataSource = typeof(BE_Servicios.eVentana);
            // 
            // fieldValorE
            // 
            this.fieldValorE.Appearance.Cell.Options.UseTextOptions = true;
            this.fieldValorE.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldValorE.Appearance.Header.Options.UseTextOptions = true;
            this.fieldValorE.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldValorE.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.fieldValorE.Appearance.Value.Options.UseFont = true;
            this.fieldValorE.Appearance.Value.Options.UseTextOptions = true;
            this.fieldValorE.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldValorE.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldValorE.AreaIndex = 0;
            this.fieldValorE.Caption = "E";
            this.fieldValorE.FieldName = "ValorE";
            this.fieldValorE.Name = "fieldValorE";
            this.fieldValorE.Width = 46;
            // 
            // fieldValorL
            // 
            this.fieldValorL.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.fieldValorL.Appearance.Header.Options.UseFont = true;
            this.fieldValorL.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.fieldValorL.Appearance.Value.Options.UseFont = true;
            this.fieldValorL.Appearance.Value.Options.UseTextOptions = true;
            this.fieldValorL.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldValorL.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldValorL.AreaIndex = 1;
            this.fieldValorL.Caption = "L";
            this.fieldValorL.FieldName = "ValorL";
            this.fieldValorL.Name = "fieldValorL";
            this.fieldValorL.Width = 46;
            // 
            // fieldVentana
            // 
            this.fieldVentana.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.fieldVentana.Appearance.Header.Options.UseFont = true;
            this.fieldVentana.Appearance.Header.Options.UseTextOptions = true;
            this.fieldVentana.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldVentana.Appearance.Header.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.fieldVentana.Appearance.Value.Options.UseTextOptions = true;
            this.fieldVentana.Appearance.Value.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.fieldVentana.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldVentana.AreaIndex = 1;
            this.fieldVentana.Caption = "Ventanas";
            this.fieldVentana.FieldName = "Ventana";
            this.fieldVentana.Name = "fieldVentana";
            this.fieldVentana.Width = 200;
            // 
            // fieldModulo
            // 
            this.fieldModulo.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.fieldModulo.Appearance.Header.Options.UseFont = true;
            this.fieldModulo.Appearance.Value.Options.UseTextOptions = true;
            this.fieldModulo.Appearance.Value.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.fieldModulo.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldModulo.AreaIndex = 0;
            this.fieldModulo.Caption = "Modulos";
            this.fieldModulo.FieldName = "Modulo";
            this.fieldModulo.Name = "fieldModulo";
            // 
            // fieldPerfil
            // 
            this.fieldPerfil.Appearance.Cell.Options.UseTextOptions = true;
            this.fieldPerfil.Appearance.Cell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.fieldPerfil.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.fieldPerfil.Appearance.Header.Options.UseFont = true;
            this.fieldPerfil.Appearance.Header.Options.UseTextOptions = true;
            this.fieldPerfil.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldPerfil.Appearance.Header.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.fieldPerfil.Appearance.Value.Options.UseTextOptions = true;
            this.fieldPerfil.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldPerfil.Appearance.Value.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.fieldPerfil.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldPerfil.AreaIndex = 0;
            this.fieldPerfil.Caption = "Perfiles";
            this.fieldPerfil.ColumnValueLineCount = 2;
            this.fieldPerfil.FieldName = "Perfil";
            this.fieldPerfil.Name = "fieldPerfil";
            this.fieldPerfil.RowValueLineCount = 2;
            this.fieldPerfil.Width = 170;
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
            this.gcVentana.Location = new System.Drawing.Point(424, 56);
            this.gcVentana.MainView = this.gvVentana;
            this.gcVentana.MenuManager = this.ribbon;
            this.gcVentana.Name = "gcVentana";
            this.gcVentana.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkLectura,
            this.rchkEscritura});
            this.gcVentana.Size = new System.Drawing.Size(623, 340);
            this.gcVentana.TabIndex = 5;
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
            this.gvVentana.ColumnPanelRowHeight = 35;
            this.gvVentana.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.coldsc_ventana,
            this.coldcs_grupo,
            this.colflg_escritura,
            this.colflg_lectura});
            this.gvVentana.GridControl = this.gcVentana;
            this.gvVentana.GroupCount = 1;
            this.gvVentana.Name = "gvVentana";
            this.gvVentana.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.gvVentana.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvVentana.OptionsPrint.ExpandAllDetails = true;
            this.gvVentana.OptionsView.EnableAppearanceEvenRow = true;
            this.gvVentana.OptionsView.ShowAutoFilterRow = true;
            this.gvVentana.OptionsView.ShowGroupPanel = false;
            this.gvVentana.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.coldcs_grupo, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvVentana.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvVentana_CustomDrawColumnHeader);
            this.gvVentana.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvVentana_RowStyle);
            this.gvVentana.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvVentana_CellValueChanged);
            // 
            // coldsc_ventana
            // 
            this.coldsc_ventana.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_ventana.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_ventana.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_ventana.Caption = "Ventana";
            this.coldsc_ventana.FieldName = "dsc_ventana";
            this.coldsc_ventana.Name = "coldsc_ventana";
            this.coldsc_ventana.OptionsColumn.AllowEdit = false;
            this.coldsc_ventana.Visible = true;
            this.coldsc_ventana.VisibleIndex = 0;
            this.coldsc_ventana.Width = 262;
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
            this.coldcs_grupo.VisibleIndex = 0;
            this.coldcs_grupo.Width = 129;
            // 
            // colflg_escritura
            // 
            this.colflg_escritura.AppearanceHeader.Options.UseTextOptions = true;
            this.colflg_escritura.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_escritura.Caption = "Escritura";
            this.colflg_escritura.ColumnEdit = this.rchkEscritura;
            this.colflg_escritura.FieldName = "flg_escritura";
            this.colflg_escritura.Name = "colflg_escritura";
            this.colflg_escritura.Visible = true;
            this.colflg_escritura.VisibleIndex = 1;
            this.colflg_escritura.Width = 114;
            // 
            // rchkEscritura
            // 
            this.rchkEscritura.AutoHeight = false;
            this.rchkEscritura.Name = "rchkEscritura";
            this.rchkEscritura.CheckedChanged += new System.EventHandler(this.rchkEscritura_CheckedChanged);
            // 
            // colflg_lectura
            // 
            this.colflg_lectura.AppearanceHeader.Options.UseTextOptions = true;
            this.colflg_lectura.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_lectura.Caption = "Lectura";
            this.colflg_lectura.ColumnEdit = this.rchkLectura;
            this.colflg_lectura.FieldName = "flg_lectura";
            this.colflg_lectura.Name = "colflg_lectura";
            this.colflg_lectura.Visible = true;
            this.colflg_lectura.VisibleIndex = 2;
            this.colflg_lectura.Width = 97;
            // 
            // rchkLectura
            // 
            this.rchkLectura.AutoHeight = false;
            this.rchkLectura.Name = "rchkLectura";
            this.rchkLectura.CheckedChanged += new System.EventHandler(this.rchkLectura_CheckedChanged);
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
            this.navBarControl1.Size = new System.Drawing.Size(55, 522);
            this.navBarControl1.TabIndex = 7;
            this.navBarControl1.Text = "navBarControl1";
            this.navBarControl1.ActiveGroupChanged += new DevExpress.XtraNavBar.NavBarGroupEventHandler(this.navBarControl1_ActiveGroupChanged);
            this.navBarControl1.Click += new System.EventHandler(this.navBarControl1_Click);
            // 
            // gcPerfiles
            // 
            this.gcPerfiles.DataSource = this.bsPerfiles;
            this.gcPerfiles.EmbeddedNavigator.Buttons.Append.Enabled = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.CancelEdit.Enabled = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.EndEdit.Enabled = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.First.Enabled = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.First.Visible = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.Last.Enabled = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.Last.Visible = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.Next.Enabled = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.Next.Visible = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.NextPage.Enabled = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.Prev.Enabled = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.Prev.Visible = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.PrevPage.Enabled = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.Remove.Enabled = false;
            this.gcPerfiles.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcPerfiles.Location = new System.Drawing.Point(71, 56);
            this.gcPerfiles.MainView = this.gvPerfiles;
            this.gcPerfiles.Name = "gcPerfiles";
            this.gcPerfiles.Size = new System.Drawing.Size(327, 478);
            this.gcPerfiles.TabIndex = 6;
            this.gcPerfiles.UseEmbeddedNavigator = true;
            this.gcPerfiles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPerfiles});
            // 
            // bsPerfiles
            // 
            this.bsPerfiles.DataSource = typeof(BE_Servicios.ePerfil);
            // 
            // gvPerfiles
            // 
            this.gvPerfiles.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvPerfiles.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvPerfiles.ColumnPanelRowHeight = 35;
            this.gvPerfiles.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_perfil,
            this.coldsc_perfil1});
            this.gvPerfiles.GridControl = this.gcPerfiles;
            this.gvPerfiles.Name = "gvPerfiles";
            this.gvPerfiles.OptionsBehavior.Editable = false;
            this.gvPerfiles.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            this.gvPerfiles.OptionsView.EnableAppearanceEvenRow = true;
            this.gvPerfiles.OptionsView.ShowAutoFilterRow = true;
            this.gvPerfiles.OptionsView.ShowGroupPanel = false;
            this.gvPerfiles.ViewCaption = "Perfiles disponibles";
            this.gvPerfiles.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvPerfiles_RowClick);
            this.gvPerfiles.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvPerfiles_CustomDrawColumnHeader);
            this.gvPerfiles.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvPerfiles_RowCellStyle);
            this.gvPerfiles.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvPerfiles_RowStyle);
            this.gvPerfiles.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvPerfiles_FocusedRowChanged);
            // 
            // coldsc_perfil1
            // 
            this.coldsc_perfil1.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_perfil1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_perfil1.Caption = "Perfiles Disponibles";
            this.coldsc_perfil1.FieldName = "dsc_perfil";
            this.coldsc_perfil1.Name = "coldsc_perfil1";
            this.coldsc_perfil1.OptionsColumn.AllowEdit = false;
            this.coldsc_perfil1.Visible = true;
            this.coldsc_perfil1.VisibleIndex = 1;
            this.coldsc_perfil1.Width = 262;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutReporteExcel,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem3});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1059, 546);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcPerfiles;
            this.layoutControlItem2.Location = new System.Drawing.Point(59, 44);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(331, 482);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcVentana;
            this.layoutControlItem3.Location = new System.Drawing.Point(412, 44);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(627, 344);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.navBarControl1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(59, 526);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutReporteExcel
            // 
            this.layoutReporteExcel.Control = this.pivotGridControl1;
            this.layoutReporteExcel.Location = new System.Drawing.Point(412, 388);
            this.layoutReporteExcel.Name = "layoutReporteExcel";
            this.layoutReporteExcel.Size = new System.Drawing.Size(627, 138);
            this.layoutReporteExcel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutReporteExcel.TextVisible = false;
            this.layoutReporteExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.picTitulo;
            this.layoutControlItem5.Location = new System.Drawing.Point(59, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(63, 44);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(63, 44);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(63, 44);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.lblTitulo;
            this.layoutControlItem6.Location = new System.Drawing.Point(122, 0);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(222, 28);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(917, 44);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(390, 44);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(22, 482);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // pivotGridField1
            // 
            this.pivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField1.AreaIndex = 1;
            this.pivotGridField1.FieldName = "ValorL";
            this.pivotGridField1.Name = "pivotGridField1";
            // 
            // colcod_perfil
            // 
            this.colcod_perfil.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_perfil.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_perfil.Caption = "N°";
            this.colcod_perfil.FieldName = "cod_perfil";
            this.colcod_perfil.Name = "colcod_perfil";
            this.colcod_perfil.OptionsColumn.FixedWidth = true;
            this.colcod_perfil.Visible = true;
            this.colcod_perfil.VisibleIndex = 0;
            this.colcod_perfil.Width = 40;
            // 
            // frmAsignacionPermiso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 704);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbon);
            this.Name = "frmAsignacionPermiso";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignación de Permisos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAsignacionPermiso_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAsignacionPermiso_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTitulo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPivot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcVentana)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVentana)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVentana)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkEscritura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkLectura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPerfiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPerfiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPerfiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutReporteExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem btnNuevo;
        private DevExpress.XtraBars.BarButtonItem btnActivar;
        private DevExpress.XtraBars.BarButtonItem btnInactivar;
        private DevExpress.XtraBars.BarButtonItem btnEliminar;
        private DevExpress.XtraBars.BarButtonItem btnExportarExcel;
        private DevExpress.XtraBars.BarButtonItem btnImprimir;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcPerfiles;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_perfil1;
        private DevExpress.XtraGrid.GridControl gcVentana;
        private DevExpress.XtraGrid.Views.Grid.GridView gvVentana;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_ventana;
        private DevExpress.XtraGrid.Columns.GridColumn coldcs_grupo;
        private System.Windows.Forms.BindingSource bsPerfiles;
        private System.Windows.Forms.BindingSource bsVentana;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_escritura;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_lectura;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkEscritura;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkLectura;
        internal DevExpress.XtraGrid.Views.Grid.GridView gvPerfiles;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraEditors.PictureEdit picTitulo;
        private DevExpress.XtraEditors.LabelControl lblTitulo;
        private System.Windows.Forms.BindingSource bsPivot;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldValorE;
        private DevExpress.XtraPivotGrid.PivotGridField fieldValorL;
        private DevExpress.XtraPivotGrid.PivotGridField fieldVentana;
        private DevExpress.XtraPivotGrid.PivotGridField fieldModulo;
        private DevExpress.XtraPivotGrid.PivotGridField fieldPerfil;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutReporteExcel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_perfil;
    }
}