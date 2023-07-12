namespace UI_Servicios.Formularios.Clientes_Y_Proveedores.Clientes
{
    partial class frmBusquedaVendedor
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcListadoVendedor = new DevExpress.XtraGrid.GridControl();
            this.bsListadoVendedor = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoVendedor = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_trabajador = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_nombre_completo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_cargo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_cargo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoVendedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoVendedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoVendedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcListadoVendedor);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(597, 514);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcListadoVendedor
            // 
            this.gcListadoVendedor.DataSource = this.bsListadoVendedor;
            this.gcListadoVendedor.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListadoVendedor.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListadoVendedor.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListadoVendedor.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListadoVendedor.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListadoVendedor.Location = new System.Drawing.Point(6, 6);
            this.gcListadoVendedor.MainView = this.gvListadoVendedor;
            this.gcListadoVendedor.Name = "gcListadoVendedor";
            this.gcListadoVendedor.Size = new System.Drawing.Size(585, 502);
            this.gcListadoVendedor.TabIndex = 4;
            this.gcListadoVendedor.UseEmbeddedNavigator = true;
            this.gcListadoVendedor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListadoVendedor});
            // 
            // bsListadoVendedor
            // 
            this.bsListadoVendedor.DataSource = typeof(BE_Servicios.eTrabajador);
            // 
            // gvListadoVendedor
            // 
            this.gvListadoVendedor.Appearance.EvenRow.BackColor = System.Drawing.Color.Gainsboro;
            this.gvListadoVendedor.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvListadoVendedor.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.gvListadoVendedor.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvListadoVendedor.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.gvListadoVendedor.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvListadoVendedor.Appearance.FocusedCell.Options.UseFont = true;
            this.gvListadoVendedor.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvListadoVendedor.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.gvListadoVendedor.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvListadoVendedor.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gvListadoVendedor.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvListadoVendedor.Appearance.FocusedRow.Options.UseFont = true;
            this.gvListadoVendedor.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvListadoVendedor.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListadoVendedor.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListadoVendedor.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.gvListadoVendedor.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvListadoVendedor.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.gvListadoVendedor.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gvListadoVendedor.Appearance.HideSelectionRow.Options.UseFont = true;
            this.gvListadoVendedor.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvListadoVendedor.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.gvListadoVendedor.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvListadoVendedor.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gvListadoVendedor.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gvListadoVendedor.Appearance.SelectedRow.Options.UseFont = true;
            this.gvListadoVendedor.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvListadoVendedor.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_trabajador,
            this.coldsc_nombre_completo,
            this.colcod_cargo,
            this.coldsc_cargo});
            this.gvListadoVendedor.GridControl = this.gcListadoVendedor;
            this.gvListadoVendedor.Name = "gvListadoVendedor";
            this.gvListadoVendedor.OptionsBehavior.Editable = false;
            this.gvListadoVendedor.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListadoVendedor.OptionsView.ShowAutoFilterRow = true;
            this.gvListadoVendedor.OptionsView.ShowGroupPanel = false;
            this.gvListadoVendedor.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListadoVendedor_RowClick);
            this.gvListadoVendedor.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListadoVendedor_CustomDrawColumnHeader);
            this.gvListadoVendedor.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListadoVendedor_RowStyle);
            this.gvListadoVendedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvListadoVendedor_KeyDown);
            // 
            // colcod_trabajador
            // 
            this.colcod_trabajador.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_trabajador.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_trabajador.Caption = "Código";
            this.colcod_trabajador.FieldName = "cod_trabajador";
            this.colcod_trabajador.Name = "colcod_trabajador";
            this.colcod_trabajador.OptionsColumn.FixedWidth = true;
            this.colcod_trabajador.Visible = true;
            this.colcod_trabajador.VisibleIndex = 0;
            this.colcod_trabajador.Width = 70;
            // 
            // coldsc_nombre_completo
            // 
            this.coldsc_nombre_completo.Caption = "Nombres y Apellidos";
            this.coldsc_nombre_completo.FieldName = "dsc_nombre_completo";
            this.coldsc_nombre_completo.Name = "coldsc_nombre_completo";
            this.coldsc_nombre_completo.OptionsColumn.FixedWidth = true;
            this.coldsc_nombre_completo.Visible = true;
            this.coldsc_nombre_completo.VisibleIndex = 1;
            this.coldsc_nombre_completo.Width = 250;
            // 
            // colcod_cargo
            // 
            this.colcod_cargo.FieldName = "cod_cargo";
            this.colcod_cargo.Name = "colcod_cargo";
            this.colcod_cargo.OptionsColumn.FixedWidth = true;
            // 
            // coldsc_cargo
            // 
            this.coldsc_cargo.Caption = "Cargo";
            this.coldsc_cargo.FieldName = "dsc_cargo";
            this.coldsc_cargo.Name = "coldsc_cargo";
            this.coldsc_cargo.OptionsColumn.FixedWidth = true;
            this.coldsc_cargo.Visible = true;
            this.coldsc_cargo.VisibleIndex = 2;
            this.coldsc_cargo.Width = 150;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.layoutControlGroup1.Size = new System.Drawing.Size(597, 514);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListadoVendedor;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(589, 506);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmBusquedaVendedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 514);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.Name = "frmBusquedaVendedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Búsqueda vendedor";
            this.Load += new System.EventHandler(this.frmBusquedaVendedor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBusquedaVendedor_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoVendedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoVendedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoVendedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcListadoVendedor;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoVendedor;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource bsListadoVendedor;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_trabajador;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_nombre_completo;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_cargo;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_cargo;
    }
}