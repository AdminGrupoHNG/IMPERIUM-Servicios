
namespace UI_Servicios.Formularios.Logistica
{
    partial class frmListaProductosSunat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaProductosSunat));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcListadoProductos = new DevExpress.XtraGrid.GridControl();
            this.bsListadoProductos = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoProductos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_segmento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_segmento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_familia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_familia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_clase = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_clase = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_producto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_producto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_activo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnMostrarProductos = new DevExpress.XtraBars.BarButtonItem();
            this.btnLimpiar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.lkpSegmento = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpFamilia = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpClase = new DevExpress.XtraEditors.LookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem3 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSegmento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpFamilia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpClase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcListadoProductos);
            this.layoutControl1.Controls.Add(this.lkpSegmento);
            this.layoutControl1.Controls.Add(this.lkpFamilia);
            this.layoutControl1.Controls.Add(this.lkpClase);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1058, 462);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcListadoProductos
            // 
            this.gcListadoProductos.DataSource = this.bsListadoProductos;
            this.gcListadoProductos.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListadoProductos.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListadoProductos.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListadoProductos.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListadoProductos.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListadoProductos.Location = new System.Drawing.Point(7, 50);
            this.gcListadoProductos.MainView = this.gvListadoProductos;
            this.gcListadoProductos.MenuManager = this.barManager1;
            this.gcListadoProductos.Name = "gcListadoProductos";
            this.gcListadoProductos.Size = new System.Drawing.Size(1044, 405);
            this.gcListadoProductos.TabIndex = 4;
            this.gcListadoProductos.UseEmbeddedNavigator = true;
            this.gcListadoProductos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListadoProductos});
            // 
            // bsListadoProductos
            // 
            this.bsListadoProductos.DataSource = typeof(BE_Servicios.eProductos_SUNAT);
            // 
            // gvListadoProductos
            // 
            this.gvListadoProductos.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListadoProductos.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListadoProductos.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListadoProductos.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListadoProductos.ColumnPanelRowHeight = 35;
            this.gvListadoProductos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_segmento,
            this.coldsc_segmento,
            this.colcod_familia,
            this.coldsc_familia,
            this.colcod_clase,
            this.coldsc_clase,
            this.colcod_producto,
            this.coldsc_producto,
            this.colflg_activo});
            this.gvListadoProductos.GridControl = this.gcListadoProductos;
            this.gvListadoProductos.Name = "gvListadoProductos";
            this.gvListadoProductos.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvListadoProductos.OptionsBehavior.Editable = false;
            this.gvListadoProductos.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListadoProductos.OptionsView.ShowAutoFilterRow = true;
            this.gvListadoProductos.OptionsView.ShowGroupPanelColumnsAsSingleRow = true;
            this.gvListadoProductos.OptionsView.ShowIndicator = false;
            this.gvListadoProductos.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListadoProductos_RowClick);
            this.gvListadoProductos.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListadoProductos_CustomDrawColumnHeader);
            this.gvListadoProductos.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListadoProductos_RowStyle);
            this.gvListadoProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvListadoProductos_KeyDown);
            // 
            // colcod_segmento
            // 
            this.colcod_segmento.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_segmento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_segmento.Caption = "Cod. Segmento";
            this.colcod_segmento.FieldName = "cod_segmento";
            this.colcod_segmento.Name = "colcod_segmento";
            this.colcod_segmento.OptionsColumn.FixedWidth = true;
            this.colcod_segmento.Width = 80;
            // 
            // coldsc_segmento
            // 
            this.coldsc_segmento.Caption = "Segmento";
            this.coldsc_segmento.FieldName = "dsc_segmento";
            this.coldsc_segmento.Name = "coldsc_segmento";
            this.coldsc_segmento.OptionsColumn.FixedWidth = true;
            this.coldsc_segmento.Width = 300;
            // 
            // colcod_familia
            // 
            this.colcod_familia.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_familia.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_familia.Caption = "Cod. Familia";
            this.colcod_familia.FieldName = "cod_familia";
            this.colcod_familia.Name = "colcod_familia";
            this.colcod_familia.OptionsColumn.FixedWidth = true;
            this.colcod_familia.Width = 80;
            // 
            // coldsc_familia
            // 
            this.coldsc_familia.Caption = "Familia";
            this.coldsc_familia.FieldName = "dsc_familia";
            this.coldsc_familia.Name = "coldsc_familia";
            this.coldsc_familia.OptionsColumn.FixedWidth = true;
            this.coldsc_familia.Width = 300;
            // 
            // colcod_clase
            // 
            this.colcod_clase.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_clase.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_clase.Caption = "Cod. Clase";
            this.colcod_clase.FieldName = "cod_clase";
            this.colcod_clase.Name = "colcod_clase";
            this.colcod_clase.OptionsColumn.FixedWidth = true;
            this.colcod_clase.Width = 80;
            // 
            // coldsc_clase
            // 
            this.coldsc_clase.Caption = "Clase";
            this.coldsc_clase.FieldName = "dsc_clase";
            this.coldsc_clase.Name = "coldsc_clase";
            this.coldsc_clase.OptionsColumn.FixedWidth = true;
            this.coldsc_clase.Width = 300;
            // 
            // colcod_producto
            // 
            this.colcod_producto.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_producto.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_producto.Caption = "Cod. Producto";
            this.colcod_producto.FieldName = "cod_producto";
            this.colcod_producto.Name = "colcod_producto";
            this.colcod_producto.OptionsColumn.FixedWidth = true;
            this.colcod_producto.Visible = true;
            this.colcod_producto.VisibleIndex = 0;
            this.colcod_producto.Width = 80;
            // 
            // coldsc_producto
            // 
            this.coldsc_producto.Caption = "Producto";
            this.coldsc_producto.FieldName = "dsc_producto";
            this.coldsc_producto.Name = "coldsc_producto";
            this.coldsc_producto.OptionsColumn.FixedWidth = true;
            this.coldsc_producto.Visible = true;
            this.coldsc_producto.VisibleIndex = 1;
            this.coldsc_producto.Width = 450;
            // 
            // colflg_activo
            // 
            this.colflg_activo.AppearanceCell.Options.UseTextOptions = true;
            this.colflg_activo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_activo.Caption = "Activo";
            this.colflg_activo.FieldName = "flg_activo";
            this.colflg_activo.Name = "colflg_activo";
            this.colflg_activo.OptionsColumn.FixedWidth = true;
            this.colflg_activo.Visible = true;
            this.colflg_activo.VisibleIndex = 2;
            this.colflg_activo.Width = 70;
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
            this.btnMostrarProductos,
            this.btnLimpiar});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMostrarProductos),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLimpiar)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Menú principal";
            // 
            // btnMostrarProductos
            // 
            this.btnMostrarProductos.Caption = "Mostrar todos los Productos";
            this.btnMostrarProductos.Id = 0;
            this.btnMostrarProductos.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMostrarProductos.ImageOptions.Image")));
            this.btnMostrarProductos.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnMostrarProductos.ItemAppearance.Normal.Options.UseFont = true;
            this.btnMostrarProductos.Name = "btnMostrarProductos";
            this.btnMostrarProductos.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnMostrarProductos.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMostrarProductos_ItemClick);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Caption = "Limpiar";
            this.btnLimpiar.Id = 1;
            this.btnLimpiar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpiar.ImageOptions.Image")));
            this.btnLimpiar.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.ItemAppearance.Normal.Options.UseFont = true;
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnLimpiar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLimpiar_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1058, 40);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 502);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1058, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 40);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 462);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1058, 40);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 462);
            // 
            // lkpSegmento
            // 
            this.lkpSegmento.Location = new System.Drawing.Point(7, 26);
            this.lkpSegmento.MenuManager = this.barManager1;
            this.lkpSegmento.Name = "lkpSegmento";
            this.lkpSegmento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpSegmento.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_segmento", "Descripción")});
            this.lkpSegmento.Properties.DropDownRows = 10;
            this.lkpSegmento.Properties.NullText = "";
            this.lkpSegmento.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.lkpSegmento.Size = new System.Drawing.Size(339, 20);
            this.lkpSegmento.StyleController = this.layoutControl1;
            this.lkpSegmento.TabIndex = 5;
            this.lkpSegmento.EditValueChanged += new System.EventHandler(this.lkpSegmento_EditValueChanged);
            this.lkpSegmento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lkpSegmento_KeyDown);
            // 
            // lkpFamilia
            // 
            this.lkpFamilia.Location = new System.Drawing.Point(350, 26);
            this.lkpFamilia.Name = "lkpFamilia";
            this.lkpFamilia.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpFamilia.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_familia", "Descripción")});
            this.lkpFamilia.Properties.DropDownRows = 10;
            this.lkpFamilia.Properties.NullText = "";
            this.lkpFamilia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.lkpFamilia.Size = new System.Drawing.Size(344, 20);
            this.lkpFamilia.StyleController = this.layoutControl1;
            this.lkpFamilia.TabIndex = 5;
            this.lkpFamilia.EditValueChanged += new System.EventHandler(this.lkpFamilia_EditValueChanged);
            this.lkpFamilia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lkpFamilia_KeyDown);
            // 
            // lkpClase
            // 
            this.lkpClase.Location = new System.Drawing.Point(698, 26);
            this.lkpClase.Name = "lkpClase";
            this.lkpClase.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpClase.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_clase", "Descripción")});
            this.lkpClase.Properties.DropDownRows = 10;
            this.lkpClase.Properties.NullText = "";
            this.lkpClase.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.lkpClase.Size = new System.Drawing.Size(353, 20);
            this.lkpClase.StyleController = this.layoutControl1;
            this.lkpClase.TabIndex = 5;
            this.lkpClase.EditValueChanged += new System.EventHandler(this.lkpClase_EditValueChanged);
            this.lkpClase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lkpClase_KeyDown);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.simpleLabelItem1,
            this.simpleLabelItem2,
            this.simpleLabelItem3,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(1058, 462);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListadoProductos;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 43);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1048, 409);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(167)))));
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.White;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseBackColor = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.simpleLabelItem1.Size = new System.Drawing.Size(343, 19);
            this.simpleLabelItem1.Text = " Segmento : ";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(103, 19);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(193)))), ((int)(((byte)(80)))));
            this.simpleLabelItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.White;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseBackColor = true;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem2.Location = new System.Drawing.Point(343, 0);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.simpleLabelItem2.Size = new System.Drawing.Size(348, 19);
            this.simpleLabelItem2.Text = " Familia : ";
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(103, 19);
            // 
            // simpleLabelItem3
            // 
            this.simpleLabelItem3.AllowHotTrack = false;
            this.simpleLabelItem3.AppearanceItemCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.simpleLabelItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem3.AppearanceItemCaption.ForeColor = System.Drawing.Color.White;
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseBackColor = true;
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem3.Location = new System.Drawing.Point(691, 0);
            this.simpleLabelItem3.Name = "simpleLabelItem3";
            this.simpleLabelItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.simpleLabelItem3.Size = new System.Drawing.Size(357, 19);
            this.simpleLabelItem3.Text = " Clase : ";
            this.simpleLabelItem3.TextSize = new System.Drawing.Size(103, 19);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem2.Control = this.lkpSegmento;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 19);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(343, 24);
            this.layoutControlItem2.Text = "Segmento : ";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem3.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem3.Control = this.lkpFamilia;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem3.Location = new System.Drawing.Point(343, 19);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(348, 24);
            this.layoutControlItem3.Text = "Familia : ";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem4.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem4.Control = this.lkpClase;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem4.Location = new System.Drawing.Point(691, 19);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(357, 24);
            this.layoutControlItem4.Text = "Clase : ";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // frmListaProductosSunat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 502);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListaProductosSunat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Consulta Productos SUNAT";
            this.Load += new System.EventHandler(this.frmProductosSunat_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmListaProductosSunat_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSegmento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpFamilia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpClase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem btnMostrarProductos;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gcListadoProductos;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoProductos;
        private DevExpress.XtraEditors.LookUpEdit lkpSegmento;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.BindingSource bsListadoProductos;
        private DevExpress.XtraEditors.LookUpEdit lkpFamilia;
        private DevExpress.XtraEditors.LookUpEdit lkpClase;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_segmento;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_segmento;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_familia;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_familia;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_clase;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_clase;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_producto;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_producto;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_activo;
        private DevExpress.XtraBars.BarButtonItem btnLimpiar;
    }
}