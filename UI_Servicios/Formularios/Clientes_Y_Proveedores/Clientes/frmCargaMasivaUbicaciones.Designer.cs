namespace UI_Servicios.Formularios.Clientes_Y_Proveedores.Clientes
{
    partial class frmCargaMasivaUbicaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCargaMasivaUbicaciones));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcUbicaciones = new DevExpress.XtraGrid.GridControl();
            this.bsUbicaciones = new System.Windows.Forms.BindingSource(this.components);
            this.gvUbicaciones = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.coldsc_ubicacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_nivel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_observacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_larga_ubicacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barManager2 = new DevExpress.XtraBars.Bar();
            this.btnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcUbicaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUbicaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUbicaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcUbicaciones);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(763, 330);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcUbicaciones
            // 
            this.gcUbicaciones.DataSource = this.bsUbicaciones;
            this.gcUbicaciones.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcUbicaciones.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcUbicaciones.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcUbicaciones.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcUbicaciones.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcUbicaciones.Location = new System.Drawing.Point(8, 8);
            this.gcUbicaciones.MainView = this.gvUbicaciones;
            this.gcUbicaciones.MenuManager = this.barManager1;
            this.gcUbicaciones.Name = "gcUbicaciones";
            this.gcUbicaciones.Size = new System.Drawing.Size(747, 314);
            this.gcUbicaciones.TabIndex = 4;
            this.gcUbicaciones.UseEmbeddedNavigator = true;
            this.gcUbicaciones.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUbicaciones});
            // 
            // bsUbicaciones
            // 
            this.bsUbicaciones.DataSource = typeof(BE_Servicios.eCliente_Ubicacion);
            // 
            // gvUbicaciones
            // 
            this.gvUbicaciones.Appearance.EvenRow.BackColor = System.Drawing.Color.Gainsboro;
            this.gvUbicaciones.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvUbicaciones.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvUbicaciones.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvUbicaciones.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvUbicaciones.ColumnPanelRowHeight = 35;
            this.gvUbicaciones.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.coldsc_ubicacion,
            this.colcod_nivel,
            this.coldsc_observacion,
            this.coldsc_larga_ubicacion});
            this.gvUbicaciones.GridControl = this.gcUbicaciones;
            this.gvUbicaciones.Name = "gvUbicaciones";
            this.gvUbicaciones.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvUbicaciones.OptionsView.EnableAppearanceEvenRow = true;
            this.gvUbicaciones.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gvUbicaciones.OptionsView.ShowGroupPanel = false;
            this.gvUbicaciones.OptionsView.ShowIndicator = false;
            this.gvUbicaciones.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvUbicaciones_CustomDrawColumnHeader);
            this.gvUbicaciones.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvUbicaciones_RowStyle);
            this.gvUbicaciones.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvUbicaciones_InitNewRow);
            // 
            // coldsc_ubicacion
            // 
            this.coldsc_ubicacion.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(49)))), ((int)(((byte)(35)))));
            this.coldsc_ubicacion.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.coldsc_ubicacion.AppearanceHeader.Options.UseBackColor = true;
            this.coldsc_ubicacion.AppearanceHeader.Options.UseForeColor = true;
            this.coldsc_ubicacion.Caption = "Descripción";
            this.coldsc_ubicacion.FieldName = "dsc_ubicacion";
            this.coldsc_ubicacion.Name = "coldsc_ubicacion";
            this.coldsc_ubicacion.OptionsColumn.FixedWidth = true;
            this.coldsc_ubicacion.Visible = true;
            this.coldsc_ubicacion.VisibleIndex = 0;
            this.coldsc_ubicacion.Width = 100;
            // 
            // colcod_nivel
            // 
            this.colcod_nivel.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(49)))), ((int)(((byte)(35)))));
            this.colcod_nivel.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.colcod_nivel.AppearanceHeader.Options.UseBackColor = true;
            this.colcod_nivel.AppearanceHeader.Options.UseForeColor = true;
            this.colcod_nivel.Caption = "Nivel";
            this.colcod_nivel.FieldName = "cod_nivel";
            this.colcod_nivel.Name = "colcod_nivel";
            this.colcod_nivel.OptionsColumn.AllowEdit = false;
            this.colcod_nivel.OptionsColumn.FixedWidth = true;
            this.colcod_nivel.Visible = true;
            this.colcod_nivel.VisibleIndex = 1;
            this.colcod_nivel.Width = 30;
            // 
            // coldsc_observacion
            // 
            this.coldsc_observacion.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(49)))), ((int)(((byte)(35)))));
            this.coldsc_observacion.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.coldsc_observacion.AppearanceHeader.Options.UseBackColor = true;
            this.coldsc_observacion.AppearanceHeader.Options.UseForeColor = true;
            this.coldsc_observacion.Caption = "Observación";
            this.coldsc_observacion.FieldName = "dsc_observacion";
            this.coldsc_observacion.Name = "coldsc_observacion";
            this.coldsc_observacion.OptionsColumn.FixedWidth = true;
            this.coldsc_observacion.Visible = true;
            this.coldsc_observacion.VisibleIndex = 2;
            this.coldsc_observacion.Width = 100;
            // 
            // coldsc_larga_ubicacion
            // 
            this.coldsc_larga_ubicacion.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(49)))), ((int)(((byte)(35)))));
            this.coldsc_larga_ubicacion.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.coldsc_larga_ubicacion.AppearanceHeader.Options.UseBackColor = true;
            this.coldsc_larga_ubicacion.AppearanceHeader.Options.UseForeColor = true;
            this.coldsc_larga_ubicacion.Caption = "Nivel Superior";
            this.coldsc_larga_ubicacion.FieldName = "dsc_larga_ubicacion";
            this.coldsc_larga_ubicacion.Name = "coldsc_larga_ubicacion";
            this.coldsc_larga_ubicacion.OptionsColumn.AllowEdit = false;
            this.coldsc_larga_ubicacion.OptionsColumn.FixedWidth = true;
            this.coldsc_larga_ubicacion.Visible = true;
            this.coldsc_larga_ubicacion.VisibleIndex = 3;
            this.coldsc_larga_ubicacion.Width = 50;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barManager2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnGuardar});
            this.barManager1.MainMenu = this.barManager2;
            this.barManager1.MaxItemId = 1;
            // 
            // barManager2
            // 
            this.barManager2.BarName = "Menú principal";
            this.barManager2.DockCol = 0;
            this.barManager2.DockRow = 0;
            this.barManager2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barManager2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnGuardar)});
            this.barManager2.OptionsBar.MultiLine = true;
            this.barManager2.OptionsBar.UseWholeRow = true;
            this.barManager2.Text = "Menú principal";
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
            this.barDockControlTop.Size = new System.Drawing.Size(763, 40);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 370);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(763, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 40);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 330);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(763, 40);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 330);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.Root.Size = new System.Drawing.Size(763, 330);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcUbicaciones;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(751, 318);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmCargaMasivaUbicaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 370);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.ShowIcon = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCargaMasivaUbicaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Carga masiva de ubicaciones";
            this.Load += new System.EventHandler(this.frmCargaMasivaUbicaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcUbicaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUbicaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUbicaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar barManager2;
        private DevExpress.XtraBars.BarButtonItem btnGuardar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gcUbicaciones;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUbicaciones;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource bsUbicaciones;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_ubicacion;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_nivel;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_observacion;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_larga_ubicacion;
    }
}