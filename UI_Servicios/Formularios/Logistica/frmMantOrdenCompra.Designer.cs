namespace UI_Servicios.Formularios.Logistica
{
    partial class frmMantOrdenCompra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMantOrdenCompra));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtObservaciones = new DevExpress.XtraEditors.TextEdit();
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.barOpciones = new DevExpress.XtraBars.Bar();
            this.btnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.btnNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.btnVerProveedor = new DevExpress.XtraEditors.SimpleButton();
            this.txtGG = new DevExpress.XtraEditors.TextEdit();
            this.txtOPER = new DevExpress.XtraEditors.TextEdit();
            this.txtLI = new DevExpress.XtraEditors.TextEdit();
            this.txtGV = new DevExpress.XtraEditors.TextEdit();
            this.txtCB = new DevExpress.XtraEditors.TextEdit();
            this.txtADM = new DevExpress.XtraEditors.TextEdit();
            this.txtCV = new DevExpress.XtraEditors.TextEdit();
            this.txtIGV = new DevExpress.XtraEditors.TextEdit();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            this.txtSubTotal = new DevExpress.XtraEditors.TextEdit();
            this.txtTermCond = new DevExpress.XtraEditors.TextEdit();
            this.txtDireccion = new DevExpress.XtraEditors.TextEdit();
            this.txtRUC = new DevExpress.XtraEditors.TextEdit();
            this.txtCotizacion = new DevExpress.XtraEditors.TextEdit();
            this.txtOC = new DevExpress.XtraEditors.TextEdit();
            this.gcDetalleOC = new DevExpress.XtraGrid.GridControl();
            this.bsDetalleOC = new System.Windows.Forms.BindingSource(this.components);
            this.gvDetalleOC = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.coldsc_producto1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_simbolo1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_cantidad1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_unitario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_total_det = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_proveedor_det = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_ruc_det = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_requerimiento1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_proveedor_det = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_producto1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_tipo_servicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_subtipo_servicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colbtnbuscar_proveedor_det = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnBuscarProveedorDet = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.txtTotalLetras = new DevExpress.XtraEditors.TextEdit();
            this.txtProveedor = new DevExpress.XtraEditors.TextEdit();
            this.dtpFechaEmision = new DevExpress.XtraEditors.DateEdit();
            this.lkpSede = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpEmpresa = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpAlmacen = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpModPago = new DevExpress.XtraEditors.LookUpEdit();
            this.dtpFechaDespacho = new DevExpress.XtraEditors.DateEdit();
            this.lkpMoneda = new DevExpress.XtraEditors.LookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblDatos = new DevExpress.XtraLayout.SimpleLabelItem();
            this.controlEmpresa = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlAlmacen = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlSede = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlOrdenCompra = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlProveedor = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlRucEmpresa = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblImportes = new DevExpress.XtraLayout.SimpleLabelItem();
            this.controlSubTotal = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlTotal = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlIGV = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlTotalLetras = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDistribucion = new DevExpress.XtraLayout.SimpleLabelItem();
            this.controlCV = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlADM = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlCB = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlGV = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlLI = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlOPER = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlGG = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlCotizacion = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlFechaEmision = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlTermCond = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlModPago = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlDireccion = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlFechaDespacho = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlBuscarProveedor = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlObservaciones = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblAstUno = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblAstDos = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblAstCuatro = new DevExpress.XtraLayout.SimpleLabelItem();
            this.controlMoneda = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblAstTres = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblAstCinco = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblAstSeis = new DevExpress.XtraLayout.SimpleLabelItem();
            this.bsProductosReq = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservaciones.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGG.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOPER.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLI.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtADM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIGV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTermCond.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDireccion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRUC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCotizacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDetalleOC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDetalleOC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalleOC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnBuscarProveedorDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalLetras.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProveedor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaEmision.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaEmision.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSede.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEmpresa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpAlmacen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpModPago.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaDespacho.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaDespacho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMoneda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlAlmacen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSede)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlOrdenCompra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlProveedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlRucEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblImportes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSubTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlIGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlTotalLetras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDistribucion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlADM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlLI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlOPER)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlGG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCotizacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlFechaEmision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlTermCond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlModPago)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlDireccion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlFechaDespacho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBuscarProveedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlObservaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAstUno)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAstDos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAstCuatro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlMoneda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAstTres)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAstCinco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAstSeis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProductosReq)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtObservaciones);
            this.layoutControl1.Controls.Add(this.btnVerProveedor);
            this.layoutControl1.Controls.Add(this.txtGG);
            this.layoutControl1.Controls.Add(this.txtOPER);
            this.layoutControl1.Controls.Add(this.txtLI);
            this.layoutControl1.Controls.Add(this.txtGV);
            this.layoutControl1.Controls.Add(this.txtCB);
            this.layoutControl1.Controls.Add(this.txtADM);
            this.layoutControl1.Controls.Add(this.txtCV);
            this.layoutControl1.Controls.Add(this.txtIGV);
            this.layoutControl1.Controls.Add(this.txtTotal);
            this.layoutControl1.Controls.Add(this.txtSubTotal);
            this.layoutControl1.Controls.Add(this.txtTermCond);
            this.layoutControl1.Controls.Add(this.txtDireccion);
            this.layoutControl1.Controls.Add(this.txtRUC);
            this.layoutControl1.Controls.Add(this.txtCotizacion);
            this.layoutControl1.Controls.Add(this.txtOC);
            this.layoutControl1.Controls.Add(this.gcDetalleOC);
            this.layoutControl1.Controls.Add(this.txtTotalLetras);
            this.layoutControl1.Controls.Add(this.txtProveedor);
            this.layoutControl1.Controls.Add(this.dtpFechaEmision);
            this.layoutControl1.Controls.Add(this.lkpSede);
            this.layoutControl1.Controls.Add(this.lkpEmpresa);
            this.layoutControl1.Controls.Add(this.lkpAlmacen);
            this.layoutControl1.Controls.Add(this.lkpModPago);
            this.layoutControl1.Controls.Add(this.dtpFechaDespacho);
            this.layoutControl1.Controls.Add(this.lkpMoneda);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(-650, 204, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1250, 653);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(147, 130);
            this.txtObservaciones.MenuManager = this.barManager2;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservaciones.Size = new System.Drawing.Size(1091, 20);
            this.txtObservaciones.StyleController = this.layoutControl1;
            this.txtObservaciones.TabIndex = 41;
            // 
            // barManager2
            // 
            this.barManager2.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barOpciones});
            this.barManager2.DockControls.Add(this.barDockControl1);
            this.barManager2.DockControls.Add(this.barDockControl2);
            this.barManager2.DockControls.Add(this.barDockControl3);
            this.barManager2.DockControls.Add(this.barDockControl4);
            this.barManager2.Form = this;
            this.barManager2.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnNuevo,
            this.btnGuardar});
            this.barManager2.MainMenu = this.barOpciones;
            this.barManager2.MaxItemId = 2;
            // 
            // barOpciones
            // 
            this.barOpciones.BarName = "Menú principal";
            this.barOpciones.DockCol = 0;
            this.barOpciones.DockRow = 0;
            this.barOpciones.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barOpciones.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnGuardar)});
            this.barOpciones.OptionsBar.DrawDragBorder = false;
            this.barOpciones.OptionsBar.MultiLine = true;
            this.barOpciones.OptionsBar.UseWholeRow = true;
            this.barOpciones.Text = "Menú principal";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Caption = "Guardar";
            this.btnGuardar.Id = 1;
            this.btnGuardar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.ImageOptions.Image")));
            this.btnGuardar.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ItemAppearance.Normal.Options.UseFont = true;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGuardar_ItemClick);
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Manager = this.barManager2;
            this.barDockControl1.Size = new System.Drawing.Size(1250, 40);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 693);
            this.barDockControl2.Manager = this.barManager2;
            this.barDockControl2.Size = new System.Drawing.Size(1250, 0);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 40);
            this.barDockControl3.Manager = this.barManager2;
            this.barDockControl3.Size = new System.Drawing.Size(0, 653);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(1250, 40);
            this.barDockControl4.Manager = this.barManager2;
            this.barDockControl4.Size = new System.Drawing.Size(0, 653);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Caption = "Nuevo";
            this.btnNuevo.Id = 0;
            this.btnNuevo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.ImageOptions.Image")));
            this.btnNuevo.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ItemAppearance.Normal.Options.UseFont = true;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnNuevo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNuevo_ItemClick);
            // 
            // btnVerProveedor
            // 
            this.btnVerProveedor.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnVerProveedor.ImageOptions.Image")));
            this.btnVerProveedor.Location = new System.Drawing.Point(301, 82);
            this.btnVerProveedor.Name = "btnVerProveedor";
            this.btnVerProveedor.Size = new System.Drawing.Size(20, 20);
            this.btnVerProveedor.StyleController = this.layoutControl1;
            this.btnVerProveedor.TabIndex = 40;
            this.btnVerProveedor.Click += new System.EventHandler(this.btnVerProveedor_Click);
            // 
            // txtGG
            // 
            this.txtGG.EditValue = "0";
            this.txtGG.Location = new System.Drawing.Point(592, 222);
            this.txtGG.MenuManager = this.barManager2;
            this.txtGG.Name = "txtGG";
            this.txtGG.Properties.Appearance.Options.UseTextOptions = true;
            this.txtGG.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtGG.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtGG.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtGG.Properties.MaskSettings.Set("mask", "p");
            this.txtGG.Properties.UseMaskAsDisplayFormat = true;
            this.txtGG.Size = new System.Drawing.Size(116, 20);
            this.txtGG.StyleController = this.layoutControl1;
            this.txtGG.TabIndex = 37;
            // 
            // txtOPER
            // 
            this.txtOPER.EditValue = "0";
            this.txtOPER.Location = new System.Drawing.Point(942, 222);
            this.txtOPER.MenuManager = this.barManager2;
            this.txtOPER.Name = "txtOPER";
            this.txtOPER.Properties.Appearance.Options.UseTextOptions = true;
            this.txtOPER.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtOPER.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtOPER.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtOPER.Properties.MaskSettings.Set("mask", "p");
            this.txtOPER.Properties.UseMaskAsDisplayFormat = true;
            this.txtOPER.Size = new System.Drawing.Size(116, 20);
            this.txtOPER.StyleController = this.layoutControl1;
            this.txtOPER.TabIndex = 36;
            // 
            // txtLI
            // 
            this.txtLI.EditValue = "0";
            this.txtLI.Location = new System.Drawing.Point(242, 222);
            this.txtLI.MenuManager = this.barManager2;
            this.txtLI.Name = "txtLI";
            this.txtLI.Properties.Appearance.Options.UseTextOptions = true;
            this.txtLI.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtLI.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtLI.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtLI.Properties.MaskSettings.Set("mask", "p");
            this.txtLI.Properties.UseMaskAsDisplayFormat = true;
            this.txtLI.Size = new System.Drawing.Size(116, 20);
            this.txtLI.StyleController = this.layoutControl1;
            this.txtLI.TabIndex = 35;
            // 
            // txtGV
            // 
            this.txtGV.EditValue = "0";
            this.txtGV.Location = new System.Drawing.Point(1117, 222);
            this.txtGV.MenuManager = this.barManager2;
            this.txtGV.Name = "txtGV";
            this.txtGV.Properties.Appearance.Options.UseTextOptions = true;
            this.txtGV.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtGV.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtGV.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtGV.Properties.MaskSettings.Set("mask", "p");
            this.txtGV.Properties.UseMaskAsDisplayFormat = true;
            this.txtGV.Size = new System.Drawing.Size(121, 20);
            this.txtGV.StyleController = this.layoutControl1;
            this.txtGV.TabIndex = 34;
            // 
            // txtCB
            // 
            this.txtCB.EditValue = "0";
            this.txtCB.Location = new System.Drawing.Point(417, 222);
            this.txtCB.MenuManager = this.barManager2;
            this.txtCB.Name = "txtCB";
            this.txtCB.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCB.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtCB.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtCB.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtCB.Properties.MaskSettings.Set("mask", "p");
            this.txtCB.Properties.UseMaskAsDisplayFormat = true;
            this.txtCB.Size = new System.Drawing.Size(116, 20);
            this.txtCB.StyleController = this.layoutControl1;
            this.txtCB.TabIndex = 33;
            // 
            // txtADM
            // 
            this.txtADM.EditValue = "0";
            this.txtADM.Location = new System.Drawing.Point(767, 222);
            this.txtADM.MenuManager = this.barManager2;
            this.txtADM.Name = "txtADM";
            this.txtADM.Properties.Appearance.Options.UseTextOptions = true;
            this.txtADM.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtADM.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtADM.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtADM.Properties.MaskSettings.Set("mask", "p");
            this.txtADM.Properties.UseMaskAsDisplayFormat = true;
            this.txtADM.Size = new System.Drawing.Size(116, 20);
            this.txtADM.StyleController = this.layoutControl1;
            this.txtADM.TabIndex = 32;
            // 
            // txtCV
            // 
            this.txtCV.EditValue = "0";
            this.txtCV.Location = new System.Drawing.Point(67, 222);
            this.txtCV.MenuManager = this.barManager2;
            this.txtCV.Name = "txtCV";
            this.txtCV.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCV.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtCV.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtCV.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtCV.Properties.MaskSettings.Set("mask", "p");
            this.txtCV.Properties.UseMaskAsDisplayFormat = true;
            this.txtCV.Size = new System.Drawing.Size(116, 20);
            this.txtCV.StyleController = this.layoutControl1;
            this.txtCV.TabIndex = 31;
            // 
            // txtIGV
            // 
            this.txtIGV.EditValue = "0";
            this.txtIGV.Location = new System.Drawing.Point(171, 176);
            this.txtIGV.MenuManager = this.barManager2;
            this.txtIGV.Name = "txtIGV";
            this.txtIGV.Properties.Appearance.Options.UseTextOptions = true;
            this.txtIGV.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtIGV.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtIGV.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtIGV.Properties.MaskSettings.Set("mask", "c");
            this.txtIGV.Properties.MaskSettings.Set("culture", "es-PE");
            this.txtIGV.Properties.UseMaskAsDisplayFormat = true;
            this.txtIGV.Size = new System.Drawing.Size(77, 20);
            this.txtIGV.StyleController = this.layoutControl1;
            this.txtIGV.TabIndex = 30;
            this.txtIGV.TextChanged += new System.EventHandler(this.txtIGV_TextChanged);
            // 
            // txtTotal
            // 
            this.txtTotal.EditValue = "0";
            this.txtTotal.Location = new System.Drawing.Point(287, 176);
            this.txtTotal.MenuManager = this.barManager2;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotal.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtTotal.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtTotal.Properties.MaskSettings.Set("culture", "es-PE");
            this.txtTotal.Properties.MaskSettings.Set("mask", "c");
            this.txtTotal.Properties.UseMaskAsDisplayFormat = true;
            this.txtTotal.Size = new System.Drawing.Size(73, 20);
            this.txtTotal.StyleController = this.layoutControl1;
            this.txtTotal.TabIndex = 29;
            this.txtTotal.TextChanged += new System.EventHandler(this.txtTotal_TextChanged);
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.EditValue = "0";
            this.txtSubTotal.Location = new System.Drawing.Point(67, 176);
            this.txtSubTotal.MenuManager = this.barManager2;
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSubTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtSubTotal.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtSubTotal.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtSubTotal.Properties.MaskSettings.Set("mask", "c");
            this.txtSubTotal.Properties.MaskSettings.Set("culture", "es-PE");
            this.txtSubTotal.Properties.UseMaskAsDisplayFormat = true;
            this.txtSubTotal.Size = new System.Drawing.Size(70, 20);
            this.txtSubTotal.StyleController = this.layoutControl1;
            this.txtSubTotal.TabIndex = 28;
            this.txtSubTotal.TextChanged += new System.EventHandler(this.txtSubTotal_TextChanged);
            // 
            // txtTermCond
            // 
            this.txtTermCond.Location = new System.Drawing.Point(147, 106);
            this.txtTermCond.MenuManager = this.barManager2;
            this.txtTermCond.Name = "txtTermCond";
            this.txtTermCond.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTermCond.Size = new System.Drawing.Size(1091, 20);
            this.txtTermCond.StyleController = this.layoutControl1;
            this.txtTermCond.TabIndex = 27;
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(715, 58);
            this.txtDireccion.MenuManager = this.barManager2;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDireccion.Size = new System.Drawing.Size(185, 20);
            this.txtDireccion.StyleController = this.layoutControl1;
            this.txtDireccion.TabIndex = 24;
            // 
            // txtRUC
            // 
            this.txtRUC.Location = new System.Drawing.Point(390, 82);
            this.txtRUC.MenuManager = this.barManager2;
            this.txtRUC.Name = "txtRUC";
            this.txtRUC.Size = new System.Drawing.Size(236, 20);
            this.txtRUC.StyleController = this.layoutControl1;
            this.txtRUC.TabIndex = 22;
            // 
            // txtCotizacion
            // 
            this.txtCotizacion.EditValue = "N/A";
            this.txtCotizacion.Location = new System.Drawing.Point(390, 58);
            this.txtCotizacion.MenuManager = this.barManager2;
            this.txtCotizacion.Name = "txtCotizacion";
            this.txtCotizacion.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCotizacion.Size = new System.Drawing.Size(236, 20);
            this.txtCotizacion.StyleController = this.layoutControl1;
            this.txtCotizacion.TabIndex = 21;
            // 
            // txtOC
            // 
            this.txtOC.Enabled = false;
            this.txtOC.Location = new System.Drawing.Point(82, 58);
            this.txtOC.MenuManager = this.barManager2;
            this.txtOC.Name = "txtOC";
            this.txtOC.Size = new System.Drawing.Size(239, 20);
            this.txtOC.StyleController = this.layoutControl1;
            this.txtOC.TabIndex = 19;
            // 
            // gcDetalleOC
            // 
            this.gcDetalleOC.DataSource = this.bsDetalleOC;
            this.gcDetalleOC.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcDetalleOC.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcDetalleOC.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcDetalleOC.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcDetalleOC.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcDetalleOC.Location = new System.Drawing.Point(12, 246);
            this.gcDetalleOC.MainView = this.gvDetalleOC;
            this.gcDetalleOC.MenuManager = this.barManager2;
            this.gcDetalleOC.Name = "gcDetalleOC";
            this.gcDetalleOC.Padding = new System.Windows.Forms.Padding(6);
            this.gcDetalleOC.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rbtnBuscarProveedorDet});
            this.gcDetalleOC.Size = new System.Drawing.Size(1226, 395);
            this.gcDetalleOC.TabIndex = 15;
            this.gcDetalleOC.UseEmbeddedNavigator = true;
            this.gcDetalleOC.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetalleOC});
            // 
            // bsDetalleOC
            // 
            this.bsDetalleOC.DataSource = typeof(BE_Servicios.eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle);
            // 
            // gvDetalleOC
            // 
            this.gvDetalleOC.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Blue;
            this.gvDetalleOC.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvDetalleOC.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvDetalleOC.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvDetalleOC.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvDetalleOC.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvDetalleOC.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvDetalleOC.ColumnPanelRowHeight = 30;
            this.gvDetalleOC.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.coldsc_producto1,
            this.coldsc_simbolo1,
            this.colnum_cantidad1,
            this.colimp_unitario,
            this.colimp_total_det,
            this.coldsc_proveedor_det,
            this.coldsc_ruc_det,
            this.colcod_requerimiento1,
            this.colcod_proveedor_det,
            this.colcod_producto1,
            this.coldsc_tipo_servicio,
            this.coldsc_subtipo_servicio,
            this.colbtnbuscar_proveedor_det});
            this.gvDetalleOC.GridControl = this.gcDetalleOC;
            this.gvDetalleOC.Name = "gvDetalleOC";
            this.gvDetalleOC.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvDetalleOC.OptionsSelection.MultiSelect = true;
            this.gvDetalleOC.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvDetalleOC.OptionsView.EnableAppearanceEvenRow = true;
            this.gvDetalleOC.OptionsView.ShowAutoFilterRow = true;
            this.gvDetalleOC.OptionsView.ShowFooter = true;
            this.gvDetalleOC.OptionsView.ShowGroupPanel = false;
            this.gvDetalleOC.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvDetalleReq_CustomDrawColumnHeader);
            this.gvDetalleOC.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvDetalleReq_CustomDrawCell);
            this.gvDetalleOC.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvDetalleReq_RowCellStyle);
            this.gvDetalleOC.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvDetalleReq_RowStyle);
            this.gvDetalleOC.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvDetalleOC_CellValueChanged);
            // 
            // coldsc_producto1
            // 
            this.coldsc_producto1.Caption = "Producto";
            this.coldsc_producto1.FieldName = "dsc_producto";
            this.coldsc_producto1.Name = "coldsc_producto1";
            this.coldsc_producto1.OptionsColumn.AllowEdit = false;
            this.coldsc_producto1.OptionsColumn.FixedWidth = true;
            this.coldsc_producto1.Visible = true;
            this.coldsc_producto1.VisibleIndex = 1;
            this.coldsc_producto1.Width = 200;
            // 
            // coldsc_simbolo1
            // 
            this.coldsc_simbolo1.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_simbolo1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_simbolo1.Caption = "Und. Med.";
            this.coldsc_simbolo1.FieldName = "dsc_simbolo";
            this.coldsc_simbolo1.Name = "coldsc_simbolo1";
            this.coldsc_simbolo1.OptionsColumn.AllowEdit = false;
            this.coldsc_simbolo1.OptionsColumn.FixedWidth = true;
            this.coldsc_simbolo1.Visible = true;
            this.coldsc_simbolo1.VisibleIndex = 2;
            this.coldsc_simbolo1.Width = 50;
            // 
            // colnum_cantidad1
            // 
            this.colnum_cantidad1.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_cantidad1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_cantidad1.Caption = "Cant.";
            this.colnum_cantidad1.FieldName = "num_cantidad";
            this.colnum_cantidad1.Name = "colnum_cantidad1";
            this.colnum_cantidad1.OptionsColumn.FixedWidth = true;
            this.colnum_cantidad1.Visible = true;
            this.colnum_cantidad1.VisibleIndex = 3;
            this.colnum_cantidad1.Width = 40;
            // 
            // colimp_unitario
            // 
            this.colimp_unitario.Caption = "Ult. Precio";
            this.colimp_unitario.FieldName = "imp_unitario";
            this.colimp_unitario.Name = "colimp_unitario";
            this.colimp_unitario.OptionsColumn.FixedWidth = true;
            this.colimp_unitario.Visible = true;
            this.colimp_unitario.VisibleIndex = 4;
            this.colimp_unitario.Width = 40;
            // 
            // colimp_total_det
            // 
            this.colimp_total_det.Caption = "Imp. Total";
            this.colimp_total_det.FieldName = "imp_total_det";
            this.colimp_total_det.Name = "colimp_total_det";
            this.colimp_total_det.OptionsColumn.AllowEdit = false;
            this.colimp_total_det.OptionsColumn.FixedWidth = true;
            this.colimp_total_det.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, DevExpress.Data.SummaryMode.Mixed, "imp_total_det", "{0:#,#.##}")});
            this.colimp_total_det.Visible = true;
            this.colimp_total_det.VisibleIndex = 5;
            this.colimp_total_det.Width = 40;
            // 
            // coldsc_proveedor_det
            // 
            this.coldsc_proveedor_det.Caption = "Proveedor";
            this.coldsc_proveedor_det.FieldName = "dsc_proveedor_det";
            this.coldsc_proveedor_det.Name = "coldsc_proveedor_det";
            this.coldsc_proveedor_det.OptionsColumn.AllowEdit = false;
            this.coldsc_proveedor_det.OptionsColumn.FixedWidth = true;
            this.coldsc_proveedor_det.Visible = true;
            this.coldsc_proveedor_det.VisibleIndex = 6;
            this.coldsc_proveedor_det.Width = 120;
            // 
            // coldsc_ruc_det
            // 
            this.coldsc_ruc_det.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_ruc_det.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_ruc_det.Caption = "RUC";
            this.coldsc_ruc_det.FieldName = "dsc_ruc_det";
            this.coldsc_ruc_det.Name = "coldsc_ruc_det";
            this.coldsc_ruc_det.OptionsColumn.AllowEdit = false;
            this.coldsc_ruc_det.OptionsColumn.FixedWidth = true;
            this.coldsc_ruc_det.Visible = true;
            this.coldsc_ruc_det.VisibleIndex = 7;
            this.coldsc_ruc_det.Width = 60;
            // 
            // colcod_requerimiento1
            // 
            this.colcod_requerimiento1.FieldName = "cod_requerimiento";
            this.colcod_requerimiento1.Name = "colcod_requerimiento1";
            this.colcod_requerimiento1.OptionsColumn.FixedWidth = true;
            // 
            // colcod_proveedor_det
            // 
            this.colcod_proveedor_det.FieldName = "cod_proveedor_det";
            this.colcod_proveedor_det.Name = "colcod_proveedor_det";
            // 
            // colcod_producto1
            // 
            this.colcod_producto1.Caption = "Cod. Producto";
            this.colcod_producto1.FieldName = "cod_producto";
            this.colcod_producto1.Name = "colcod_producto1";
            this.colcod_producto1.OptionsColumn.AllowEdit = false;
            this.colcod_producto1.OptionsColumn.FixedWidth = true;
            // 
            // coldsc_tipo_servicio
            // 
            this.coldsc_tipo_servicio.Caption = "Tipo";
            this.coldsc_tipo_servicio.FieldName = "dsc_tipo_servicio";
            this.coldsc_tipo_servicio.Name = "coldsc_tipo_servicio";
            this.coldsc_tipo_servicio.OptionsColumn.FixedWidth = true;
            // 
            // coldsc_subtipo_servicio
            // 
            this.coldsc_subtipo_servicio.Caption = "SubTipo";
            this.coldsc_subtipo_servicio.FieldName = "dsc_subtipo_servicio";
            this.coldsc_subtipo_servicio.Name = "coldsc_subtipo_servicio";
            this.coldsc_subtipo_servicio.OptionsColumn.FixedWidth = true;
            // 
            // colbtnbuscar_proveedor_det
            // 
            this.colbtnbuscar_proveedor_det.Caption = " ";
            this.colbtnbuscar_proveedor_det.ColumnEdit = this.rbtnBuscarProveedorDet;
            this.colbtnbuscar_proveedor_det.FieldName = "colbtnbuscar_proveedor_det";
            this.colbtnbuscar_proveedor_det.Name = "colbtnbuscar_proveedor_det";
            this.colbtnbuscar_proveedor_det.OptionsColumn.FixedWidth = true;
            this.colbtnbuscar_proveedor_det.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colbtnbuscar_proveedor_det.Visible = true;
            this.colbtnbuscar_proveedor_det.VisibleIndex = 8;
            this.colbtnbuscar_proveedor_det.Width = 20;
            // 
            // rbtnBuscarProveedorDet
            // 
            this.rbtnBuscarProveedorDet.AutoHeight = false;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.rbtnBuscarProveedorDet.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.rbtnBuscarProveedorDet.Name = "rbtnBuscarProveedorDet";
            this.rbtnBuscarProveedorDet.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.rbtnBuscarProveedorDet.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.rbtnBuscarProveedorDet_ButtonPressed);
            // 
            // txtTotalLetras
            // 
            this.txtTotalLetras.Location = new System.Drawing.Point(449, 176);
            this.txtTotalLetras.Name = "txtTotalLetras";
            this.txtTotalLetras.Size = new System.Drawing.Size(789, 20);
            this.txtTotalLetras.StyleController = this.layoutControl1;
            this.txtTotalLetras.TabIndex = 5;
            // 
            // txtProveedor
            // 
            this.txtProveedor.Enabled = false;
            this.txtProveedor.Location = new System.Drawing.Point(82, 82);
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.Size = new System.Drawing.Size(215, 20);
            this.txtProveedor.StyleController = this.layoutControl1;
            this.txtProveedor.TabIndex = 4;
            this.txtProveedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProveedor_KeyPress);
            // 
            // dtpFechaEmision
            // 
            this.dtpFechaEmision.EditValue = null;
            this.dtpFechaEmision.Location = new System.Drawing.Point(715, 82);
            this.dtpFechaEmision.Name = "dtpFechaEmision";
            this.dtpFechaEmision.Properties.Appearance.Options.UseTextOptions = true;
            this.dtpFechaEmision.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dtpFechaEmision.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFechaEmision.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFechaEmision.Size = new System.Drawing.Size(185, 20);
            this.dtpFechaEmision.StyleController = this.layoutControl1;
            this.dtpFechaEmision.TabIndex = 13;
            this.dtpFechaEmision.EditValueChanged += new System.EventHandler(this.dtpFechaEmision_EditValueChanged);
            // 
            // lkpSede
            // 
            this.lkpSede.Location = new System.Drawing.Point(390, 34);
            this.lkpSede.MenuManager = this.barManager2;
            this.lkpSede.Name = "lkpSede";
            this.lkpSede.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpSede.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_sede_empresa", "Descripción")});
            this.lkpSede.Properties.NullText = "";
            this.lkpSede.Size = new System.Drawing.Size(236, 20);
            this.lkpSede.StyleController = this.layoutControl1;
            this.lkpSede.TabIndex = 12;
            // 
            // lkpEmpresa
            // 
            this.lkpEmpresa.Location = new System.Drawing.Point(82, 34);
            this.lkpEmpresa.Name = "lkpEmpresa";
            this.lkpEmpresa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpEmpresa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_empresa", "Descripción")});
            this.lkpEmpresa.Properties.NullText = "";
            this.lkpEmpresa.Size = new System.Drawing.Size(239, 20);
            this.lkpEmpresa.StyleController = this.layoutControl1;
            this.lkpEmpresa.TabIndex = 3;
            // 
            // lkpAlmacen
            // 
            this.lkpAlmacen.Location = new System.Drawing.Point(715, 34);
            this.lkpAlmacen.MenuManager = this.barManager2;
            this.lkpAlmacen.Name = "lkpAlmacen";
            this.lkpAlmacen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpAlmacen.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_almacen", "Descripción")});
            this.lkpAlmacen.Properties.NullText = "";
            this.lkpAlmacen.Size = new System.Drawing.Size(185, 20);
            this.lkpAlmacen.StyleController = this.layoutControl1;
            this.lkpAlmacen.TabIndex = 23;
            this.lkpAlmacen.EditValueChanged += new System.EventHandler(this.lkpAlmacen_EditValueChanged);
            // 
            // lkpModPago
            // 
            this.lkpModPago.Location = new System.Drawing.Point(1010, 34);
            this.lkpModPago.MenuManager = this.barManager2;
            this.lkpModPago.Name = "lkpModPago";
            this.lkpModPago.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpModPago.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_modalidad_pago", "Descripción")});
            this.lkpModPago.Properties.NullText = "";
            this.lkpModPago.Size = new System.Drawing.Size(217, 20);
            this.lkpModPago.StyleController = this.layoutControl1;
            this.lkpModPago.TabIndex = 25;
            // 
            // dtpFechaDespacho
            // 
            this.dtpFechaDespacho.EditValue = null;
            this.dtpFechaDespacho.Location = new System.Drawing.Point(1010, 82);
            this.dtpFechaDespacho.Name = "dtpFechaDespacho";
            this.dtpFechaDespacho.Properties.Appearance.Options.UseTextOptions = true;
            this.dtpFechaDespacho.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dtpFechaDespacho.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFechaDespacho.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFechaDespacho.Size = new System.Drawing.Size(217, 20);
            this.dtpFechaDespacho.StyleController = this.layoutControl1;
            this.dtpFechaDespacho.TabIndex = 13;
            // 
            // lkpMoneda
            // 
            this.lkpMoneda.Location = new System.Drawing.Point(1010, 58);
            this.lkpMoneda.MenuManager = this.barManager2;
            this.lkpMoneda.Name = "lkpMoneda";
            this.lkpMoneda.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpMoneda.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_moneda", "Descripción")});
            this.lkpMoneda.Properties.NullText = "";
            this.lkpMoneda.Size = new System.Drawing.Size(217, 20);
            this.lkpMoneda.StyleController = this.layoutControl1;
            this.lkpMoneda.TabIndex = 42;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblDatos,
            this.controlEmpresa,
            this.layoutControlItem7,
            this.controlAlmacen,
            this.controlSede,
            this.controlOrdenCompra,
            this.controlProveedor,
            this.controlRucEmpresa,
            this.lblImportes,
            this.controlSubTotal,
            this.controlTotal,
            this.controlIGV,
            this.controlTotalLetras,
            this.lblDistribucion,
            this.controlCV,
            this.controlADM,
            this.controlCB,
            this.controlGV,
            this.controlLI,
            this.controlOPER,
            this.controlGG,
            this.controlCotizacion,
            this.controlFechaEmision,
            this.controlTermCond,
            this.controlModPago,
            this.controlDireccion,
            this.controlFechaDespacho,
            this.controlBuscarProveedor,
            this.controlObservaciones,
            this.lblAstUno,
            this.lblAstDos,
            this.lblAstCuatro,
            this.controlMoneda,
            this.lblAstTres,
            this.lblAstCinco,
            this.lblAstSeis});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1250, 653);
            this.Root.TextVisible = false;
            // 
            // lblDatos
            // 
            this.lblDatos.AllowHotTrack = false;
            this.lblDatos.AppearanceItemCaption.BackColor = System.Drawing.Color.LightGray;
            this.lblDatos.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblDatos.AppearanceItemCaption.Options.UseBackColor = true;
            this.lblDatos.AppearanceItemCaption.Options.UseFont = true;
            this.lblDatos.CustomizationFormText = "Datos Generales";
            this.lblDatos.Location = new System.Drawing.Point(0, 0);
            this.lblDatos.Name = "lblDatos";
            this.lblDatos.Size = new System.Drawing.Size(1230, 22);
            this.lblDatos.Text = "Datos Generales";
            this.lblDatos.TextSize = new System.Drawing.Size(123, 18);
            // 
            // controlEmpresa
            // 
            this.controlEmpresa.Control = this.lkpEmpresa;
            this.controlEmpresa.CustomizationFormText = "Empresa :";
            this.controlEmpresa.Location = new System.Drawing.Point(0, 22);
            this.controlEmpresa.Name = "controlEmpresa";
            this.controlEmpresa.Size = new System.Drawing.Size(313, 24);
            this.controlEmpresa.Text = "Empresa :";
            this.controlEmpresa.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlEmpresa.TextSize = new System.Drawing.Size(65, 13);
            this.controlEmpresa.TextToControlDistance = 5;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.gcDetalleOC;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 234);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(1230, 399);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // controlAlmacen
            // 
            this.controlAlmacen.Control = this.lkpAlmacen;
            this.controlAlmacen.CustomizationFormText = "Almacen";
            this.controlAlmacen.Location = new System.Drawing.Point(618, 22);
            this.controlAlmacen.Name = "controlAlmacen";
            this.controlAlmacen.Size = new System.Drawing.Size(274, 24);
            this.controlAlmacen.Text = "Almacen :";
            this.controlAlmacen.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlAlmacen.TextSize = new System.Drawing.Size(80, 13);
            this.controlAlmacen.TextToControlDistance = 5;
            // 
            // controlSede
            // 
            this.controlSede.Control = this.lkpSede;
            this.controlSede.CustomizationFormText = "Sede";
            this.controlSede.Location = new System.Drawing.Point(313, 22);
            this.controlSede.Name = "controlSede";
            this.controlSede.Size = new System.Drawing.Size(305, 24);
            this.controlSede.Text = "Sede :";
            this.controlSede.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlSede.TextSize = new System.Drawing.Size(60, 20);
            this.controlSede.TextToControlDistance = 5;
            // 
            // controlOrdenCompra
            // 
            this.controlOrdenCompra.Control = this.txtOC;
            this.controlOrdenCompra.CustomizationFormText = "Num. OC";
            this.controlOrdenCompra.Location = new System.Drawing.Point(0, 46);
            this.controlOrdenCompra.Name = "controlOrdenCompra";
            this.controlOrdenCompra.Size = new System.Drawing.Size(313, 24);
            this.controlOrdenCompra.Text = "Num. OC :";
            this.controlOrdenCompra.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlOrdenCompra.TextSize = new System.Drawing.Size(65, 20);
            this.controlOrdenCompra.TextToControlDistance = 5;
            // 
            // controlProveedor
            // 
            this.controlProveedor.Control = this.txtProveedor;
            this.controlProveedor.CustomizationFormText = "Proveedor";
            this.controlProveedor.Location = new System.Drawing.Point(0, 70);
            this.controlProveedor.Name = "controlProveedor";
            this.controlProveedor.Size = new System.Drawing.Size(289, 24);
            this.controlProveedor.Text = "Proveedor :";
            this.controlProveedor.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlProveedor.TextSize = new System.Drawing.Size(65, 13);
            this.controlProveedor.TextToControlDistance = 5;
            // 
            // controlRucEmpresa
            // 
            this.controlRucEmpresa.Control = this.txtRUC;
            this.controlRucEmpresa.CustomizationFormText = "RUC";
            this.controlRucEmpresa.Location = new System.Drawing.Point(313, 70);
            this.controlRucEmpresa.Name = "controlRucEmpresa";
            this.controlRucEmpresa.Size = new System.Drawing.Size(305, 24);
            this.controlRucEmpresa.Text = "RUC :";
            this.controlRucEmpresa.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlRucEmpresa.TextSize = new System.Drawing.Size(60, 20);
            this.controlRucEmpresa.TextToControlDistance = 5;
            // 
            // lblImportes
            // 
            this.lblImportes.AllowHotTrack = false;
            this.lblImportes.AppearanceItemCaption.BackColor = System.Drawing.Color.LightGray;
            this.lblImportes.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblImportes.AppearanceItemCaption.Options.UseBackColor = true;
            this.lblImportes.AppearanceItemCaption.Options.UseFont = true;
            this.lblImportes.CustomizationFormText = "Importes";
            this.lblImportes.Location = new System.Drawing.Point(0, 142);
            this.lblImportes.Name = "lblImportes";
            this.lblImportes.Size = new System.Drawing.Size(1230, 22);
            this.lblImportes.Text = "Importes";
            this.lblImportes.TextSize = new System.Drawing.Size(123, 18);
            // 
            // controlSubTotal
            // 
            this.controlSubTotal.Control = this.txtSubTotal;
            this.controlSubTotal.CustomizationFormText = "SubTotal";
            this.controlSubTotal.Location = new System.Drawing.Point(0, 164);
            this.controlSubTotal.Name = "controlSubTotal";
            this.controlSubTotal.Size = new System.Drawing.Size(129, 24);
            this.controlSubTotal.Text = "SubTotal :";
            this.controlSubTotal.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlSubTotal.TextSize = new System.Drawing.Size(50, 20);
            this.controlSubTotal.TextToControlDistance = 5;
            // 
            // controlTotal
            // 
            this.controlTotal.Control = this.txtTotal;
            this.controlTotal.CustomizationFormText = "Total";
            this.controlTotal.Location = new System.Drawing.Point(240, 164);
            this.controlTotal.Name = "controlTotal";
            this.controlTotal.Size = new System.Drawing.Size(112, 24);
            this.controlTotal.Text = "Total :";
            this.controlTotal.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlTotal.TextSize = new System.Drawing.Size(30, 20);
            this.controlTotal.TextToControlDistance = 5;
            // 
            // controlIGV
            // 
            this.controlIGV.Control = this.txtIGV;
            this.controlIGV.CustomizationFormText = "IGV";
            this.controlIGV.Location = new System.Drawing.Point(129, 164);
            this.controlIGV.Name = "controlIGV";
            this.controlIGV.Size = new System.Drawing.Size(111, 24);
            this.controlIGV.Text = "IGV :";
            this.controlIGV.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlIGV.TextSize = new System.Drawing.Size(25, 20);
            this.controlIGV.TextToControlDistance = 5;
            // 
            // controlTotalLetras
            // 
            this.controlTotalLetras.Control = this.txtTotalLetras;
            this.controlTotalLetras.CustomizationFormText = "Importe Total";
            this.controlTotalLetras.Location = new System.Drawing.Point(352, 164);
            this.controlTotalLetras.Name = "controlTotalLetras";
            this.controlTotalLetras.Size = new System.Drawing.Size(878, 24);
            this.controlTotalLetras.Text = "Importe Total :";
            this.controlTotalLetras.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlTotalLetras.TextSize = new System.Drawing.Size(80, 13);
            this.controlTotalLetras.TextToControlDistance = 5;
            // 
            // lblDistribucion
            // 
            this.lblDistribucion.AllowHotTrack = false;
            this.lblDistribucion.AppearanceItemCaption.BackColor = System.Drawing.Color.LightGray;
            this.lblDistribucion.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblDistribucion.AppearanceItemCaption.Options.UseBackColor = true;
            this.lblDistribucion.AppearanceItemCaption.Options.UseFont = true;
            this.lblDistribucion.CustomizationFormText = "Distribucion";
            this.lblDistribucion.Location = new System.Drawing.Point(0, 188);
            this.lblDistribucion.Name = "lblDistribucion";
            this.lblDistribucion.Size = new System.Drawing.Size(1230, 22);
            this.lblDistribucion.Text = "Distribución";
            this.lblDistribucion.TextSize = new System.Drawing.Size(123, 18);
            this.lblDistribucion.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // controlCV
            // 
            this.controlCV.Control = this.txtCV;
            this.controlCV.CustomizationFormText = "CV";
            this.controlCV.Location = new System.Drawing.Point(0, 210);
            this.controlCV.MaxSize = new System.Drawing.Size(175, 24);
            this.controlCV.MinSize = new System.Drawing.Size(175, 24);
            this.controlCV.Name = "controlCV";
            this.controlCV.Size = new System.Drawing.Size(175, 24);
            this.controlCV.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlCV.Text = "CV :";
            this.controlCV.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlCV.TextSize = new System.Drawing.Size(50, 20);
            this.controlCV.TextToControlDistance = 5;
            this.controlCV.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // controlADM
            // 
            this.controlADM.Control = this.txtADM;
            this.controlADM.CustomizationFormText = "ADM";
            this.controlADM.Location = new System.Drawing.Point(700, 210);
            this.controlADM.MaxSize = new System.Drawing.Size(175, 24);
            this.controlADM.MinSize = new System.Drawing.Size(175, 24);
            this.controlADM.Name = "controlADM";
            this.controlADM.Size = new System.Drawing.Size(175, 24);
            this.controlADM.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlADM.Text = "ADM :";
            this.controlADM.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlADM.TextSize = new System.Drawing.Size(50, 20);
            this.controlADM.TextToControlDistance = 5;
            this.controlADM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // controlCB
            // 
            this.controlCB.Control = this.txtCB;
            this.controlCB.CustomizationFormText = "CB";
            this.controlCB.Location = new System.Drawing.Point(350, 210);
            this.controlCB.MaxSize = new System.Drawing.Size(175, 24);
            this.controlCB.MinSize = new System.Drawing.Size(175, 24);
            this.controlCB.Name = "controlCB";
            this.controlCB.Size = new System.Drawing.Size(175, 24);
            this.controlCB.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlCB.Text = "CB :";
            this.controlCB.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlCB.TextSize = new System.Drawing.Size(50, 20);
            this.controlCB.TextToControlDistance = 5;
            this.controlCB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // controlGV
            // 
            this.controlGV.Control = this.txtGV;
            this.controlGV.CustomizationFormText = "GV";
            this.controlGV.Location = new System.Drawing.Point(1050, 210);
            this.controlGV.MaxSize = new System.Drawing.Size(180, 24);
            this.controlGV.MinSize = new System.Drawing.Size(180, 24);
            this.controlGV.Name = "controlGV";
            this.controlGV.Size = new System.Drawing.Size(180, 24);
            this.controlGV.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlGV.Text = "GV :";
            this.controlGV.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlGV.TextSize = new System.Drawing.Size(50, 20);
            this.controlGV.TextToControlDistance = 5;
            this.controlGV.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // controlLI
            // 
            this.controlLI.Control = this.txtLI;
            this.controlLI.CustomizationFormText = "LI";
            this.controlLI.Location = new System.Drawing.Point(175, 210);
            this.controlLI.MaxSize = new System.Drawing.Size(175, 24);
            this.controlLI.MinSize = new System.Drawing.Size(175, 24);
            this.controlLI.Name = "controlLI";
            this.controlLI.Size = new System.Drawing.Size(175, 24);
            this.controlLI.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlLI.Text = "LI :";
            this.controlLI.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlLI.TextSize = new System.Drawing.Size(50, 20);
            this.controlLI.TextToControlDistance = 5;
            this.controlLI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // controlOPER
            // 
            this.controlOPER.Control = this.txtOPER;
            this.controlOPER.CustomizationFormText = "OPER";
            this.controlOPER.Location = new System.Drawing.Point(875, 210);
            this.controlOPER.MaxSize = new System.Drawing.Size(175, 24);
            this.controlOPER.MinSize = new System.Drawing.Size(175, 24);
            this.controlOPER.Name = "controlOPER";
            this.controlOPER.Size = new System.Drawing.Size(175, 24);
            this.controlOPER.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlOPER.Text = "OPER :";
            this.controlOPER.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlOPER.TextSize = new System.Drawing.Size(50, 20);
            this.controlOPER.TextToControlDistance = 5;
            this.controlOPER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // controlGG
            // 
            this.controlGG.Control = this.txtGG;
            this.controlGG.CustomizationFormText = "GG";
            this.controlGG.Location = new System.Drawing.Point(525, 210);
            this.controlGG.MaxSize = new System.Drawing.Size(175, 24);
            this.controlGG.MinSize = new System.Drawing.Size(175, 24);
            this.controlGG.Name = "controlGG";
            this.controlGG.Size = new System.Drawing.Size(175, 24);
            this.controlGG.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlGG.Text = "GG :";
            this.controlGG.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlGG.TextSize = new System.Drawing.Size(50, 20);
            this.controlGG.TextToControlDistance = 5;
            this.controlGG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // controlCotizacion
            // 
            this.controlCotizacion.Control = this.txtCotizacion;
            this.controlCotizacion.CustomizationFormText = "Cotizacion";
            this.controlCotizacion.Location = new System.Drawing.Point(313, 46);
            this.controlCotizacion.Name = "controlCotizacion";
            this.controlCotizacion.Size = new System.Drawing.Size(305, 24);
            this.controlCotizacion.Text = "Cotización :";
            this.controlCotizacion.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlCotizacion.TextSize = new System.Drawing.Size(60, 20);
            this.controlCotizacion.TextToControlDistance = 5;
            // 
            // controlFechaEmision
            // 
            this.controlFechaEmision.Control = this.dtpFechaEmision;
            this.controlFechaEmision.CustomizationFormText = "Fecha Emisión";
            this.controlFechaEmision.Location = new System.Drawing.Point(618, 70);
            this.controlFechaEmision.Name = "controlFechaEmision";
            this.controlFechaEmision.Size = new System.Drawing.Size(274, 24);
            this.controlFechaEmision.Text = "Fecha Emisión :";
            this.controlFechaEmision.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlFechaEmision.TextSize = new System.Drawing.Size(80, 13);
            this.controlFechaEmision.TextToControlDistance = 5;
            // 
            // controlTermCond
            // 
            this.controlTermCond.Control = this.txtTermCond;
            this.controlTermCond.CustomizationFormText = "Terminos y Condiciones";
            this.controlTermCond.Location = new System.Drawing.Point(0, 94);
            this.controlTermCond.MaxSize = new System.Drawing.Size(0, 24);
            this.controlTermCond.MinSize = new System.Drawing.Size(189, 24);
            this.controlTermCond.Name = "controlTermCond";
            this.controlTermCond.Size = new System.Drawing.Size(1230, 24);
            this.controlTermCond.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlTermCond.Text = "Terminos y Condiciones :";
            this.controlTermCond.TextSize = new System.Drawing.Size(123, 13);
            // 
            // controlModPago
            // 
            this.controlModPago.Control = this.lkpModPago;
            this.controlModPago.CustomizationFormText = "Modalidad Pago";
            this.controlModPago.Location = new System.Drawing.Point(903, 22);
            this.controlModPago.Name = "controlModPago";
            this.controlModPago.Size = new System.Drawing.Size(316, 24);
            this.controlModPago.Text = "Mod. Pago :";
            this.controlModPago.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlModPago.TextSize = new System.Drawing.Size(90, 13);
            this.controlModPago.TextToControlDistance = 5;
            // 
            // controlDireccion
            // 
            this.controlDireccion.Control = this.txtDireccion;
            this.controlDireccion.CustomizationFormText = "Direccion";
            this.controlDireccion.Location = new System.Drawing.Point(618, 46);
            this.controlDireccion.Name = "controlDireccion";
            this.controlDireccion.Size = new System.Drawing.Size(274, 24);
            this.controlDireccion.Text = "Dirección :";
            this.controlDireccion.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlDireccion.TextSize = new System.Drawing.Size(80, 13);
            this.controlDireccion.TextToControlDistance = 5;
            // 
            // controlFechaDespacho
            // 
            this.controlFechaDespacho.Control = this.dtpFechaDespacho;
            this.controlFechaDespacho.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.controlFechaDespacho.CustomizationFormText = "Fecha despacho";
            this.controlFechaDespacho.Location = new System.Drawing.Point(903, 70);
            this.controlFechaDespacho.Name = "controlFechaDespacho";
            this.controlFechaDespacho.Size = new System.Drawing.Size(316, 24);
            this.controlFechaDespacho.Text = "Fecha Despacho :";
            this.controlFechaDespacho.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlFechaDespacho.TextSize = new System.Drawing.Size(90, 13);
            this.controlFechaDespacho.TextToControlDistance = 5;
            // 
            // controlBuscarProveedor
            // 
            this.controlBuscarProveedor.Control = this.btnVerProveedor;
            this.controlBuscarProveedor.CustomizationFormText = "Buscar Proveedor";
            this.controlBuscarProveedor.Location = new System.Drawing.Point(289, 70);
            this.controlBuscarProveedor.MinSize = new System.Drawing.Size(24, 24);
            this.controlBuscarProveedor.Name = "controlBuscarProveedor";
            this.controlBuscarProveedor.Size = new System.Drawing.Size(24, 24);
            this.controlBuscarProveedor.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlBuscarProveedor.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlBuscarProveedor.TextSize = new System.Drawing.Size(0, 0);
            this.controlBuscarProveedor.TextToControlDistance = 0;
            this.controlBuscarProveedor.TextVisible = false;
            // 
            // controlObservaciones
            // 
            this.controlObservaciones.Control = this.txtObservaciones;
            this.controlObservaciones.CustomizationFormText = "Observaciones";
            this.controlObservaciones.Location = new System.Drawing.Point(0, 118);
            this.controlObservaciones.Name = "controlObservaciones";
            this.controlObservaciones.Size = new System.Drawing.Size(1230, 24);
            this.controlObservaciones.Text = "Observaciones :";
            this.controlObservaciones.TextSize = new System.Drawing.Size(123, 13);
            // 
            // lblAstUno
            // 
            this.lblAstUno.AllowHotTrack = false;
            this.lblAstUno.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.lblAstUno.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblAstUno.Location = new System.Drawing.Point(892, 22);
            this.lblAstUno.MinSize = new System.Drawing.Size(11, 17);
            this.lblAstUno.Name = "lblAstUno";
            this.lblAstUno.Size = new System.Drawing.Size(11, 24);
            this.lblAstUno.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblAstUno.Text = "*";
            this.lblAstUno.TextSize = new System.Drawing.Size(123, 13);
            // 
            // lblAstDos
            // 
            this.lblAstDos.AllowHotTrack = false;
            this.lblAstDos.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.lblAstDos.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblAstDos.Location = new System.Drawing.Point(1219, 22);
            this.lblAstDos.MinSize = new System.Drawing.Size(11, 17);
            this.lblAstDos.Name = "lblAstDos";
            this.lblAstDos.Size = new System.Drawing.Size(11, 24);
            this.lblAstDos.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblAstDos.Text = "*";
            this.lblAstDos.TextSize = new System.Drawing.Size(123, 13);
            // 
            // lblAstCuatro
            // 
            this.lblAstCuatro.AllowHotTrack = false;
            this.lblAstCuatro.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.lblAstCuatro.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblAstCuatro.Location = new System.Drawing.Point(892, 70);
            this.lblAstCuatro.MinSize = new System.Drawing.Size(11, 17);
            this.lblAstCuatro.Name = "lblAstCuatro";
            this.lblAstCuatro.Size = new System.Drawing.Size(11, 24);
            this.lblAstCuatro.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblAstCuatro.Text = "*";
            this.lblAstCuatro.TextSize = new System.Drawing.Size(123, 13);
            // 
            // controlMoneda
            // 
            this.controlMoneda.Control = this.lkpMoneda;
            this.controlMoneda.CustomizationFormText = "Moneda";
            this.controlMoneda.Location = new System.Drawing.Point(903, 46);
            this.controlMoneda.Name = "controlMoneda";
            this.controlMoneda.Size = new System.Drawing.Size(316, 24);
            this.controlMoneda.Text = "Moneda :";
            this.controlMoneda.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.controlMoneda.TextSize = new System.Drawing.Size(90, 20);
            this.controlMoneda.TextToControlDistance = 5;
            // 
            // lblAstTres
            // 
            this.lblAstTres.AllowHotTrack = false;
            this.lblAstTres.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.lblAstTres.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblAstTres.Location = new System.Drawing.Point(892, 46);
            this.lblAstTres.MinSize = new System.Drawing.Size(11, 17);
            this.lblAstTres.Name = "lblAstTres";
            this.lblAstTres.Size = new System.Drawing.Size(11, 24);
            this.lblAstTres.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblAstTres.Text = "*";
            this.lblAstTres.TextSize = new System.Drawing.Size(123, 13);
            // 
            // lblAstCinco
            // 
            this.lblAstCinco.AllowHotTrack = false;
            this.lblAstCinco.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.lblAstCinco.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblAstCinco.Location = new System.Drawing.Point(1219, 70);
            this.lblAstCinco.MinSize = new System.Drawing.Size(11, 17);
            this.lblAstCinco.Name = "lblAstCinco";
            this.lblAstCinco.Size = new System.Drawing.Size(11, 24);
            this.lblAstCinco.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblAstCinco.Text = "*";
            this.lblAstCinco.TextSize = new System.Drawing.Size(123, 13);
            // 
            // lblAstSeis
            // 
            this.lblAstSeis.AllowHotTrack = false;
            this.lblAstSeis.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.lblAstSeis.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblAstSeis.CustomizationFormText = "*";
            this.lblAstSeis.Location = new System.Drawing.Point(1219, 46);
            this.lblAstSeis.MinSize = new System.Drawing.Size(11, 17);
            this.lblAstSeis.Name = "lblAstSeis";
            this.lblAstSeis.Size = new System.Drawing.Size(11, 24);
            this.lblAstSeis.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblAstSeis.Text = "*";
            this.lblAstSeis.TextSize = new System.Drawing.Size(123, 13);
            // 
            // bsProductosReq
            // 
            this.bsProductosReq.DataSource = typeof(BE_Servicios.eRequerimiento.eRequerimiento_Detalle);
            // 
            // frmMantOrdenCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 693);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmMantOrdenCompra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Orden de Compra";
            this.Load += new System.EventHandler(this.frmMantRequerimientos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtObservaciones.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGG.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOPER.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLI.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtADM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIGV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTermCond.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDireccion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRUC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCotizacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDetalleOC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDetalleOC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalleOC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnBuscarProveedorDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalLetras.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProveedor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaEmision.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaEmision.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSede.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEmpresa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpAlmacen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpModPago.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaDespacho.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaDespacho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMoneda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlAlmacen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSede)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlOrdenCompra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlProveedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlRucEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblImportes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSubTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlIGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlTotalLetras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDistribucion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlADM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlLI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlOPER)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlGG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCotizacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlFechaEmision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlTermCond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlModPago)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlDireccion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlFechaDespacho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBuscarProveedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlObservaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAstUno)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAstDos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAstCuatro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlMoneda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAstTres)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAstCinco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAstSeis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProductosReq)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.SimpleLabelItem lblDatos;
        private DevExpress.XtraEditors.TextEdit txtTotalLetras;
        private DevExpress.XtraEditors.TextEdit txtProveedor;
        private DevExpress.XtraLayout.LayoutControlItem controlProveedor;
        private DevExpress.XtraLayout.LayoutControlItem controlTotalLetras;
        private DevExpress.XtraLayout.LayoutControlItem controlEmpresa;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraBars.Bar barOpciones;
        private DevExpress.XtraBars.BarButtonItem btnNuevo;
        private DevExpress.XtraBars.BarButtonItem btnGuardar;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraGrid.GridControl gcDetalleOC;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetalleOC;
        private DevExpress.XtraLayout.LayoutControlItem controlSede;
        private DevExpress.XtraLayout.LayoutControlItem controlFechaEmision;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.DateEdit dtpFechaEmision;
        private DevExpress.XtraEditors.LookUpEdit lkpSede;
        private DevExpress.XtraEditors.LookUpEdit lkpEmpresa;
        private System.Windows.Forms.BindingSource bsProductosReq;
        private System.Windows.Forms.BindingSource bsDetalleOC;
        private DevExpress.XtraEditors.TextEdit txtCotizacion;
        private DevExpress.XtraEditors.TextEdit txtOC;
        private DevExpress.XtraLayout.LayoutControlItem controlOrdenCompra;
        private DevExpress.XtraLayout.LayoutControlItem controlCotizacion;
        private DevExpress.XtraEditors.TextEdit txtRUC;
        private DevExpress.XtraLayout.LayoutControlItem controlRucEmpresa;
        private DevExpress.XtraEditors.TextEdit txtTermCond;
        private DevExpress.XtraEditors.TextEdit txtDireccion;
        private DevExpress.XtraLayout.LayoutControlItem controlAlmacen;
        private DevExpress.XtraLayout.LayoutControlItem controlDireccion;
        private DevExpress.XtraLayout.LayoutControlItem controlModPago;
        private DevExpress.XtraLayout.LayoutControlItem controlTermCond;
        private DevExpress.XtraEditors.LookUpEdit lkpAlmacen;
        private DevExpress.XtraEditors.LookUpEdit lkpModPago;
        private DevExpress.XtraLayout.SimpleLabelItem lblImportes;
        private DevExpress.XtraEditors.TextEdit txtIGV;
        private DevExpress.XtraEditors.TextEdit txtTotal;
        private DevExpress.XtraEditors.TextEdit txtSubTotal;
        private DevExpress.XtraLayout.LayoutControlItem controlSubTotal;
        private DevExpress.XtraLayout.LayoutControlItem controlTotal;
        private DevExpress.XtraLayout.LayoutControlItem controlIGV;
        private DevExpress.XtraLayout.SimpleLabelItem lblDistribucion;
        private DevExpress.XtraEditors.TextEdit txtGG;
        private DevExpress.XtraEditors.TextEdit txtOPER;
        private DevExpress.XtraEditors.TextEdit txtLI;
        private DevExpress.XtraEditors.TextEdit txtGV;
        private DevExpress.XtraEditors.TextEdit txtCB;
        private DevExpress.XtraEditors.TextEdit txtADM;
        private DevExpress.XtraEditors.TextEdit txtCV;
        private DevExpress.XtraLayout.LayoutControlItem controlCV;
        private DevExpress.XtraLayout.LayoutControlItem controlADM;
        private DevExpress.XtraLayout.LayoutControlItem controlCB;
        private DevExpress.XtraLayout.LayoutControlItem controlGV;
        private DevExpress.XtraLayout.LayoutControlItem controlLI;
        private DevExpress.XtraLayout.LayoutControlItem controlOPER;
        private DevExpress.XtraLayout.LayoutControlItem controlGG;
        private DevExpress.XtraEditors.DateEdit dtpFechaDespacho;
        private DevExpress.XtraLayout.LayoutControlItem controlFechaDespacho;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_producto1;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_producto1;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_cantidad1;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_unitario;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_total_det;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_requerimiento1;
        private DevExpress.XtraEditors.SimpleButton btnVerProveedor;
        private DevExpress.XtraLayout.LayoutControlItem controlBuscarProveedor;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_tipo_servicio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_subtipo_servicio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_simbolo1;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_proveedor_det;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_ruc_det;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_proveedor_det;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnBuscarProveedorDet;
        private DevExpress.XtraGrid.Columns.GridColumn colbtnbuscar_proveedor_det;
        private DevExpress.XtraEditors.TextEdit txtObservaciones;
        private DevExpress.XtraLayout.LayoutControlItem controlObservaciones;
        private DevExpress.XtraLayout.SimpleLabelItem lblAstUno;
        private DevExpress.XtraLayout.SimpleLabelItem lblAstDos;
        private DevExpress.XtraLayout.SimpleLabelItem lblAstTres;
        private DevExpress.XtraLayout.SimpleLabelItem lblAstCuatro;
        private DevExpress.XtraLayout.SimpleLabelItem lblAstCinco;
        private DevExpress.XtraLayout.LayoutControlItem controlMoneda;
        private DevExpress.XtraLayout.SimpleLabelItem lblAstSeis;
        private DevExpress.XtraEditors.LookUpEdit lkpMoneda;
    }
}