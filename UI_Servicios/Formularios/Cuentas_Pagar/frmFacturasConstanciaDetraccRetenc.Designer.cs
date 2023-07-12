namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    partial class frmFacturasConstanciaDetraccRetenc
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFacturasConstanciaDetraccRetenc));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcFacturasProveedor = new DevExpress.XtraGrid.GridControl();
            this.bsFacturasProveedor = new System.Windows.Forms.BindingSource(this.components);
            this.gvFacturasProveedor = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDescTipoDoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNroFactura = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rLinkDocumento = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colDescripcion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescTipoMoneda = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_tipocambio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtImporte = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.coldsc_observacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_subtotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMontoFactura = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dtFecha = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colnum_constancia_detraccion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnObservaciones = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rbtnVerPDF = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rmmDistribucionCECO = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcFacturasProveedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFacturasProveedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFacturasProveedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rLinkDocumento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFecha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFecha.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnObservaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnVerPDF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmmDistribucionCECO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcFacturasProveedor);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1215, 455);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcFacturasProveedor
            // 
            this.gcFacturasProveedor.DataSource = this.bsFacturasProveedor;
            this.gcFacturasProveedor.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcFacturasProveedor.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcFacturasProveedor.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcFacturasProveedor.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcFacturasProveedor.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcFacturasProveedor.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcFacturasProveedor.Location = new System.Drawing.Point(5, 5);
            this.gcFacturasProveedor.MainView = this.gvFacturasProveedor;
            this.gcFacturasProveedor.Name = "gcFacturasProveedor";
            this.gcFacturasProveedor.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtImporte,
            this.rbtnObservaciones,
            this.rbtnVerPDF,
            this.rmmDistribucionCECO,
            this.rLinkDocumento,
            this.dtFecha});
            this.gcFacturasProveedor.Size = new System.Drawing.Size(1205, 445);
            this.gcFacturasProveedor.TabIndex = 0;
            this.gcFacturasProveedor.UseEmbeddedNavigator = true;
            this.gcFacturasProveedor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFacturasProveedor});
            // 
            // bsFacturasProveedor
            // 
            this.bsFacturasProveedor.DataSource = typeof(BE_Servicios.eFacturaProveedor.eFaturaProveedor_ProgramacionPagos);
            // 
            // gvFacturasProveedor
            // 
            this.gvFacturasProveedor.Appearance.EvenRow.BackColor = System.Drawing.Color.Azure;
            this.gvFacturasProveedor.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvFacturasProveedor.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(88)))));
            this.gvFacturasProveedor.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvFacturasProveedor.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvFacturasProveedor.Appearance.FocusedCell.Options.UseFont = true;
            this.gvFacturasProveedor.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(88)))));
            this.gvFacturasProveedor.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvFacturasProveedor.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvFacturasProveedor.Appearance.FocusedRow.Options.UseFont = true;
            this.gvFacturasProveedor.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.gvFacturasProveedor.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(139)))), ((int)(((byte)(125)))));
            this.gvFacturasProveedor.Appearance.FooterPanel.Options.UseFont = true;
            this.gvFacturasProveedor.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvFacturasProveedor.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.gvFacturasProveedor.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gvFacturasProveedor.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvFacturasProveedor.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvFacturasProveedor.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvFacturasProveedor.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvFacturasProveedor.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvFacturasProveedor.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(88)))));
            this.gvFacturasProveedor.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gvFacturasProveedor.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(88)))));
            this.gvFacturasProveedor.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvFacturasProveedor.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gvFacturasProveedor.Appearance.SelectedRow.Options.UseFont = true;
            this.gvFacturasProveedor.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.gvFacturasProveedor.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gvFacturasProveedor.ColumnPanelRowHeight = 35;
            this.gvFacturasProveedor.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDescTipoDoc,
            this.colNroFactura,
            this.colDescripcion,
            this.colDescTipoMoneda,
            this.colimp_tipocambio,
            this.coldsc_observacion,
            this.colimp_subtotal,
            this.colMontoFactura,
            this.colnum_constancia_detraccion});
            this.gvFacturasProveedor.DetailHeight = 284;
            this.gvFacturasProveedor.GridControl = this.gcFacturasProveedor;
            this.gvFacturasProveedor.Name = "gvFacturasProveedor";
            this.gvFacturasProveedor.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.True;
            this.gvFacturasProveedor.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gvFacturasProveedor.OptionsView.EnableAppearanceEvenRow = true;
            this.gvFacturasProveedor.OptionsView.RowAutoHeight = true;
            this.gvFacturasProveedor.OptionsView.ShowAutoFilterRow = true;
            this.gvFacturasProveedor.OptionsView.ShowFooter = true;
            this.gvFacturasProveedor.OptionsView.ShowIndicator = false;
            this.gvFacturasProveedor.ViewCaption = " ";
            this.gvFacturasProveedor.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvFacturasProveedor_RowCellClick);
            this.gvFacturasProveedor.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvFacturasProveedor_CustomDrawColumnHeader);
            this.gvFacturasProveedor.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvFacturasProveedor_RowStyle);
            // 
            // colDescTipoDoc
            // 
            this.colDescTipoDoc.Caption = "Tipo Documento";
            this.colDescTipoDoc.FieldName = "dsc_tipo_documento";
            this.colDescTipoDoc.Name = "colDescTipoDoc";
            this.colDescTipoDoc.OptionsColumn.AllowEdit = false;
            this.colDescTipoDoc.OptionsColumn.FixedWidth = true;
            this.colDescTipoDoc.Visible = true;
            this.colDescTipoDoc.VisibleIndex = 0;
            this.colDescTipoDoc.Width = 120;
            // 
            // colNroFactura
            // 
            this.colNroFactura.AppearanceCell.Options.UseTextOptions = true;
            this.colNroFactura.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNroFactura.Caption = "Documento";
            this.colNroFactura.ColumnEdit = this.rLinkDocumento;
            this.colNroFactura.FieldName = "dsc_documento";
            this.colNroFactura.Name = "colNroFactura";
            this.colNroFactura.OptionsColumn.AllowEdit = false;
            this.colNroFactura.OptionsColumn.FixedWidth = true;
            this.colNroFactura.Visible = true;
            this.colNroFactura.VisibleIndex = 1;
            this.colNroFactura.Width = 100;
            // 
            // rLinkDocumento
            // 
            this.rLinkDocumento.AutoHeight = false;
            this.rLinkDocumento.Name = "rLinkDocumento";
            // 
            // colDescripcion
            // 
            this.colDescripcion.Caption = "Glosa";
            this.colDescripcion.FieldName = "dsc_glosa";
            this.colDescripcion.Name = "colDescripcion";
            this.colDescripcion.OptionsColumn.AllowEdit = false;
            this.colDescripcion.OptionsColumn.FixedWidth = true;
            this.colDescripcion.Visible = true;
            this.colDescripcion.VisibleIndex = 2;
            this.colDescripcion.Width = 250;
            // 
            // colDescTipoMoneda
            // 
            this.colDescTipoMoneda.AppearanceCell.Options.UseTextOptions = true;
            this.colDescTipoMoneda.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDescTipoMoneda.Caption = "Moneda";
            this.colDescTipoMoneda.FieldName = "cod_moneda";
            this.colDescTipoMoneda.Name = "colDescTipoMoneda";
            this.colDescTipoMoneda.OptionsColumn.AllowEdit = false;
            this.colDescTipoMoneda.OptionsColumn.FixedWidth = true;
            this.colDescTipoMoneda.Visible = true;
            this.colDescTipoMoneda.VisibleIndex = 3;
            this.colDescTipoMoneda.Width = 60;
            // 
            // colimp_tipocambio
            // 
            this.colimp_tipocambio.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_tipocambio.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colimp_tipocambio.Caption = "Monto TC";
            this.colimp_tipocambio.ColumnEdit = this.rtxtImporte;
            this.colimp_tipocambio.FieldName = "imp_tipocambio";
            this.colimp_tipocambio.Name = "colimp_tipocambio";
            this.colimp_tipocambio.OptionsColumn.AllowEdit = false;
            this.colimp_tipocambio.OptionsColumn.FixedWidth = true;
            this.colimp_tipocambio.Width = 60;
            // 
            // rtxtImporte
            // 
            this.rtxtImporte.AutoHeight = false;
            this.rtxtImporte.Mask.EditMask = "n2";
            this.rtxtImporte.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtImporte.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtImporte.Name = "rtxtImporte";
            // 
            // coldsc_observacion
            // 
            this.coldsc_observacion.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_observacion.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_observacion.Caption = "Observación";
            this.coldsc_observacion.FieldName = "dsc_observacion";
            this.coldsc_observacion.Name = "coldsc_observacion";
            this.coldsc_observacion.OptionsColumn.AllowEdit = false;
            this.coldsc_observacion.OptionsColumn.FixedWidth = true;
            this.coldsc_observacion.Visible = true;
            this.coldsc_observacion.VisibleIndex = 4;
            this.coldsc_observacion.Width = 90;
            // 
            // colimp_subtotal
            // 
            this.colimp_subtotal.Caption = "Monto";
            this.colimp_subtotal.ColumnEdit = this.rtxtImporte;
            this.colimp_subtotal.FieldName = "imp_pago";
            this.colimp_subtotal.Name = "colimp_subtotal";
            this.colimp_subtotal.OptionsColumn.AllowEdit = false;
            this.colimp_subtotal.OptionsColumn.FixedWidth = true;
            this.colimp_subtotal.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "imp_subtotal", "{0:#,#.##}")});
            this.colimp_subtotal.Visible = true;
            this.colimp_subtotal.VisibleIndex = 5;
            this.colimp_subtotal.Width = 80;
            // 
            // colMontoFactura
            // 
            this.colMontoFactura.AppearanceCell.Options.UseTextOptions = true;
            this.colMontoFactura.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMontoFactura.Caption = "Fecha Ejecución";
            this.colMontoFactura.ColumnEdit = this.dtFecha;
            this.colMontoFactura.FieldName = "fch_ejecucion";
            this.colMontoFactura.Name = "colMontoFactura";
            this.colMontoFactura.OptionsColumn.AllowEdit = false;
            this.colMontoFactura.OptionsColumn.FixedWidth = true;
            this.colMontoFactura.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "imp_total", "{0:#,#.##}")});
            this.colMontoFactura.Visible = true;
            this.colMontoFactura.VisibleIndex = 6;
            this.colMontoFactura.Width = 65;
            // 
            // dtFecha
            // 
            this.dtFecha.AutoHeight = false;
            this.dtFecha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFecha.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFecha.Name = "dtFecha";
            // 
            // colnum_constancia_detraccion
            // 
            this.colnum_constancia_detraccion.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.colnum_constancia_detraccion.AppearanceCell.Options.UseBackColor = true;
            this.colnum_constancia_detraccion.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_constancia_detraccion.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_constancia_detraccion.Caption = "Num. Constancia";
            this.colnum_constancia_detraccion.FieldName = "num_constancia_detraccion";
            this.colnum_constancia_detraccion.Name = "colnum_constancia_detraccion";
            this.colnum_constancia_detraccion.OptionsColumn.FixedWidth = true;
            this.colnum_constancia_detraccion.Visible = true;
            this.colnum_constancia_detraccion.VisibleIndex = 7;
            this.colnum_constancia_detraccion.Width = 70;
            // 
            // rbtnObservaciones
            // 
            this.rbtnObservaciones.AutoHeight = false;
            this.rbtnObservaciones.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.rbtnObservaciones.Name = "rbtnObservaciones";
            this.rbtnObservaciones.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // rbtnVerPDF
            // 
            this.rbtnVerPDF.AutoHeight = false;
            this.rbtnVerPDF.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.rbtnVerPDF.Name = "rbtnVerPDF";
            this.rbtnVerPDF.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // rmmDistribucionCECO
            // 
            this.rmmDistribucionCECO.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.rmmDistribucionCECO.Name = "rmmDistribucionCECO";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.Root.Size = new System.Drawing.Size(1215, 455);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcFacturasProveedor;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1209, 449);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
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
            this.btnGuardar});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 1;
            // 
            // bar2
            // 
            this.bar2.BarName = "Menú principal";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnGuardar)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Menú principal";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Caption = "Guardar";
            this.btnGuardar.Id = 0;
            this.btnGuardar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.ImageOptions.Image")));
            this.btnGuardar.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ItemAppearance.Normal.Options.UseFont = true;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGuardar_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1215, 40);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 495);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1215, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 40);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 455);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1215, 40);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 455);
            // 
            // frmFacturasConstanciaDetraccRetenc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 495);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFacturasConstanciaDetraccRetenc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Listado Facturas";
            this.Load += new System.EventHandler(this.frmFacturasConstanciaDetracc_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFacturasConstanciaDetracc_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcFacturasProveedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFacturasProveedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFacturasProveedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rLinkDocumento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFecha.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFecha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnObservaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnVerPDF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmmDistribucionCECO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcFacturasProveedor;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFacturasProveedor;
        private DevExpress.XtraGrid.Columns.GridColumn colNroFactura;
        private DevExpress.XtraGrid.Columns.GridColumn colMontoFactura;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtImporte;
        private DevExpress.XtraGrid.Columns.GridColumn colDescTipoMoneda;
        private DevExpress.XtraGrid.Columns.GridColumn colDescripcion;
        private DevExpress.XtraGrid.Columns.GridColumn colDescTipoDoc;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_subtotal;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_tipocambio;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit rmmDistribucionCECO;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnObservaciones;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnVerPDF;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource bsFacturasProveedor;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit rLinkDocumento;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnGuardar;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_observacion;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dtFecha;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_constancia_detraccion;
    }
}