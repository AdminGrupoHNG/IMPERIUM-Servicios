namespace UI_Servicios.Formularios.Shared
{
    partial class frmBusquedas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBusquedas));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnNuevoProveedor = new DevExpress.XtraEditors.SimpleButton();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.gcAyuda = new DevExpress.XtraGrid.GridControl();
            this.gvAyuda = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutAgregar = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutEspacioAgregar = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAyuda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAyuda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAgregar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutEspacioAgregar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnNuevoProveedor);
            this.layoutControl1.Controls.Add(this.btnAgregar);
            this.layoutControl1.Controls.Add(this.gcAyuda);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(717, 615);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnNuevoProveedor
            // 
            this.btnNuevoProveedor.Appearance.Options.UseTextOptions = true;
            this.btnNuevoProveedor.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnNuevoProveedor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoProveedor.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoProveedor.ImageOptions.Image")));
            this.btnNuevoProveedor.Location = new System.Drawing.Point(450, 12);
            this.btnNuevoProveedor.Name = "btnNuevoProveedor";
            this.btnNuevoProveedor.Size = new System.Drawing.Size(126, 36);
            this.btnNuevoProveedor.StyleController = this.layoutControl1;
            this.btnNuevoProveedor.TabIndex = 6;
            this.btnNuevoProveedor.Text = "Nuevo Proveedor";
            this.btnNuevoProveedor.Click += new System.EventHandler(this.btnNuevoProveedor_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.ImageOptions.Image")));
            this.btnAgregar.Location = new System.Drawing.Point(580, 12);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(125, 36);
            this.btnAgregar.StyleController = this.layoutControl1;
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // gcAyuda
            // 
            this.gcAyuda.Location = new System.Drawing.Point(12, 52);
            this.gcAyuda.MainView = this.gvAyuda;
            this.gcAyuda.Name = "gcAyuda";
            this.gcAyuda.Size = new System.Drawing.Size(693, 551);
            this.gcAyuda.TabIndex = 4;
            this.gcAyuda.UseEmbeddedNavigator = true;
            this.gcAyuda.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAyuda});
            // 
            // gvAyuda
            // 
            this.gvAyuda.Appearance.EvenRow.BackColor = System.Drawing.Color.LightGray;
            this.gvAyuda.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvAyuda.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(88)))));
            this.gvAyuda.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvAyuda.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvAyuda.Appearance.FocusedCell.Options.UseFont = true;
            this.gvAyuda.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(88)))));
            this.gvAyuda.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvAyuda.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvAyuda.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAyuda.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvAyuda.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvAyuda.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvAyuda.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvAyuda.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(88)))));
            this.gvAyuda.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvAyuda.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gvAyuda.Appearance.HideSelectionRow.Options.UseFont = true;
            this.gvAyuda.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(88)))));
            this.gvAyuda.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvAyuda.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gvAyuda.Appearance.SelectedRow.Options.UseFont = true;
            this.gvAyuda.ColumnPanelRowHeight = 35;
            this.gvAyuda.GridControl = this.gcAyuda;
            this.gvAyuda.Name = "gvAyuda";
            this.gvAyuda.OptionsBehavior.Editable = false;
            this.gvAyuda.OptionsView.EnableAppearanceEvenRow = true;
            this.gvAyuda.OptionsView.ShowAutoFilterRow = true;
            this.gvAyuda.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvAyuda_RowClick);
            this.gvAyuda.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvAyuda_CustomDrawColumnHeader);
            this.gvAyuda.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvAyuda_RowStyle);
            this.gvAyuda.ShownEditor += new System.EventHandler(this.gvAyuda_ShownEditor);
            this.gvAyuda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvAyuda_KeyDown);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutAgregar,
            this.layoutEspacioAgregar,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(717, 615);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcAyuda;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(697, 555);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutAgregar
            // 
            this.layoutAgregar.Control = this.btnAgregar;
            this.layoutAgregar.Location = new System.Drawing.Point(568, 0);
            this.layoutAgregar.MaxSize = new System.Drawing.Size(129, 40);
            this.layoutAgregar.MinSize = new System.Drawing.Size(129, 40);
            this.layoutAgregar.Name = "layoutAgregar";
            this.layoutAgregar.Size = new System.Drawing.Size(129, 40);
            this.layoutAgregar.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutAgregar.TextSize = new System.Drawing.Size(0, 0);
            this.layoutAgregar.TextVisible = false;
            this.layoutAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutEspacioAgregar
            // 
            this.layoutEspacioAgregar.AllowHotTrack = false;
            this.layoutEspacioAgregar.Location = new System.Drawing.Point(0, 0);
            this.layoutEspacioAgregar.Name = "layoutEspacioAgregar";
            this.layoutEspacioAgregar.Size = new System.Drawing.Size(438, 40);
            this.layoutEspacioAgregar.TextSize = new System.Drawing.Size(0, 0);
            this.layoutEspacioAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnNuevoProveedor;
            this.layoutControlItem2.Location = new System.Drawing.Point(438, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(130, 40);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(130, 40);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(130, 40);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            this.layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // frmBusquedas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 615);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBusquedas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmBusquedas";
            this.Load += new System.EventHandler(this.frmBusquedas_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBusquedas_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAyuda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAyuda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAgregar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutEspacioAgregar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcAyuda;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAyuda;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnAgregar;
        private DevExpress.XtraLayout.LayoutControlItem layoutAgregar;
        private DevExpress.XtraLayout.EmptySpaceItem layoutEspacioAgregar;
        private DevExpress.XtraEditors.SimpleButton btnNuevoProveedor;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}