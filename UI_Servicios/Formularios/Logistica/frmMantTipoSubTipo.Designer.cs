namespace UI_Servicios.Formularios.Logistica
{
    partial class frmMantTipoSubTipo
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
            this.controlPrincipal = new DevExpress.XtraLayout.LayoutControl();
            this.gcEmpresas = new DevExpress.XtraGrid.GridControl();
            this.bsEmpresa = new System.Windows.Forms.BindingSource(this.components);
            this.gvEmpresas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_empresa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_empresa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSubTipoServicio = new DevExpress.XtraGrid.GridControl();
            this.bsSubTipoServicio = new System.Windows.Forms.BindingSource(this.components);
            this.gvSubTipoServicio = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_subtipo_servicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_subtipo_servicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTipoServicio = new DevExpress.XtraGrid.GridControl();
            this.bsTipoServicio = new System.Windows.Forms.BindingSource(this.components);
            this.gvTipoServicio = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_tipo_servicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_tipo_servicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.collkpCaracteristica = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rlkpCaracteristica = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.controlTipoServicio = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlSubTipoServicio = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlEmpresas = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.controlPrincipal)).BeginInit();
            this.controlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcEmpresas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpresas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSubTipoServicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSubTipoServicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSubTipoServicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTipoServicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTipoServicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTipoServicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlkpCaracteristica)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlTipoServicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSubTipoServicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlEmpresas)).BeginInit();
            this.SuspendLayout();
            // 
            // controlPrincipal
            // 
            this.controlPrincipal.Controls.Add(this.gcEmpresas);
            this.controlPrincipal.Controls.Add(this.gcSubTipoServicio);
            this.controlPrincipal.Controls.Add(this.gcTipoServicio);
            this.controlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlPrincipal.Location = new System.Drawing.Point(0, 0);
            this.controlPrincipal.Name = "controlPrincipal";
            this.controlPrincipal.Root = this.Root;
            this.controlPrincipal.Size = new System.Drawing.Size(822, 584);
            this.controlPrincipal.TabIndex = 4;
            this.controlPrincipal.Text = "layoutControl1";
            // 
            // gcEmpresas
            // 
            this.gcEmpresas.DataSource = this.bsEmpresa;
            this.gcEmpresas.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcEmpresas.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcEmpresas.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcEmpresas.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcEmpresas.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcEmpresas.Location = new System.Drawing.Point(8, 303);
            this.gcEmpresas.MainView = this.gvEmpresas;
            this.gcEmpresas.Name = "gcEmpresas";
            this.gcEmpresas.Size = new System.Drawing.Size(806, 273);
            this.gcEmpresas.TabIndex = 6;
            this.gcEmpresas.UseEmbeddedNavigator = true;
            this.gcEmpresas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvEmpresas});
            // 
            // bsEmpresa
            // 
            this.bsEmpresa.DataSource = typeof(BE_Servicios.eEmpresa);
            // 
            // gvEmpresas
            // 
            this.gvEmpresas.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvEmpresas.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvEmpresas.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvEmpresas.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvEmpresas.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvEmpresas.ColumnPanelRowHeight = 30;
            this.gvEmpresas.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_empresa,
            this.coldsc_empresa});
            this.gvEmpresas.GridControl = this.gcEmpresas;
            this.gvEmpresas.Name = "gvEmpresas";
            this.gvEmpresas.OptionsBehavior.Editable = false;
            this.gvEmpresas.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvEmpresas.OptionsSelection.MultiSelect = true;
            this.gvEmpresas.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvEmpresas.OptionsView.EnableAppearanceEvenRow = true;
            this.gvEmpresas.OptionsView.ShowGroupPanel = false;
            this.gvEmpresas.OptionsView.ShowIndicator = false;
            this.gvEmpresas.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvEmpresas_CustomDrawColumnHeader);
            this.gvEmpresas.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvEmpresas_CustomDrawCell);
            this.gvEmpresas.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvEmpresas_RowCellStyle);
            this.gvEmpresas.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvEmpresas_RowStyle);
            this.gvEmpresas.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gvEmpresas_SelectionChanged);
            // 
            // colcod_empresa
            // 
            this.colcod_empresa.Caption = "Código";
            this.colcod_empresa.FieldName = "cod_empresa";
            this.colcod_empresa.Name = "colcod_empresa";
            this.colcod_empresa.OptionsColumn.FixedWidth = true;
            this.colcod_empresa.Width = 70;
            // 
            // coldsc_empresa
            // 
            this.coldsc_empresa.Caption = "Empresa";
            this.coldsc_empresa.FieldName = "dsc_empresa";
            this.coldsc_empresa.Name = "coldsc_empresa";
            this.coldsc_empresa.OptionsColumn.FixedWidth = true;
            this.coldsc_empresa.Visible = true;
            this.coldsc_empresa.VisibleIndex = 1;
            this.coldsc_empresa.Width = 350;
            // 
            // gcSubTipoServicio
            // 
            this.gcSubTipoServicio.DataSource = this.bsSubTipoServicio;
            this.gcSubTipoServicio.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcSubTipoServicio.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcSubTipoServicio.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcSubTipoServicio.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcSubTipoServicio.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcSubTipoServicio.Location = new System.Drawing.Point(413, 8);
            this.gcSubTipoServicio.MainView = this.gvSubTipoServicio;
            this.gcSubTipoServicio.Name = "gcSubTipoServicio";
            this.gcSubTipoServicio.Size = new System.Drawing.Size(401, 291);
            this.gcSubTipoServicio.TabIndex = 5;
            this.gcSubTipoServicio.UseEmbeddedNavigator = true;
            this.gcSubTipoServicio.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSubTipoServicio});
            // 
            // bsSubTipoServicio
            // 
            this.bsSubTipoServicio.DataSource = typeof(BE_Servicios.eProyecto.eProyecto_SubTipo_Servicio);
            // 
            // gvSubTipoServicio
            // 
            this.gvSubTipoServicio.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvSubTipoServicio.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvSubTipoServicio.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvSubTipoServicio.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvSubTipoServicio.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvSubTipoServicio.ColumnPanelRowHeight = 30;
            this.gvSubTipoServicio.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_subtipo_servicio,
            this.coldsc_subtipo_servicio});
            this.gvSubTipoServicio.GridControl = this.gcSubTipoServicio;
            this.gvSubTipoServicio.Name = "gvSubTipoServicio";
            this.gvSubTipoServicio.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvSubTipoServicio.OptionsView.EnableAppearanceEvenRow = true;
            this.gvSubTipoServicio.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gvSubTipoServicio.OptionsView.ShowGroupPanel = false;
            this.gvSubTipoServicio.OptionsView.ShowIndicator = false;
            this.gvSubTipoServicio.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvSubTipoServicio_CustomDrawColumnHeader);
            this.gvSubTipoServicio.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvSubTipoServicio_CustomDrawCell);
            this.gvSubTipoServicio.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvSubTipoServicio_RowCellStyle);
            this.gvSubTipoServicio.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvSubTipoServicio_RowStyle);
            this.gvSubTipoServicio.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvSubTipoServicio_ShowingEditor);
            this.gvSubTipoServicio.HiddenEditor += new System.EventHandler(this.gvSubTipoServicio_HiddenEditor);
            // 
            // colcod_subtipo_servicio
            // 
            this.colcod_subtipo_servicio.Caption = "Código";
            this.colcod_subtipo_servicio.FieldName = "cod_subtipo_servicio";
            this.colcod_subtipo_servicio.Name = "colcod_subtipo_servicio";
            this.colcod_subtipo_servicio.OptionsColumn.FixedWidth = true;
            this.colcod_subtipo_servicio.Width = 70;
            // 
            // coldsc_subtipo_servicio
            // 
            this.coldsc_subtipo_servicio.Caption = "SubTipo Servicio";
            this.coldsc_subtipo_servicio.FieldName = "dsc_subtipo_servicio";
            this.coldsc_subtipo_servicio.Name = "coldsc_subtipo_servicio";
            this.coldsc_subtipo_servicio.OptionsColumn.FixedWidth = true;
            this.coldsc_subtipo_servicio.Visible = true;
            this.coldsc_subtipo_servicio.VisibleIndex = 0;
            this.coldsc_subtipo_servicio.Width = 180;
            // 
            // gcTipoServicio
            // 
            this.gcTipoServicio.DataSource = this.bsTipoServicio;
            this.gcTipoServicio.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcTipoServicio.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcTipoServicio.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcTipoServicio.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcTipoServicio.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcTipoServicio.Location = new System.Drawing.Point(8, 8);
            this.gcTipoServicio.MainView = this.gvTipoServicio;
            this.gcTipoServicio.Name = "gcTipoServicio";
            this.gcTipoServicio.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rlkpCaracteristica});
            this.gcTipoServicio.Size = new System.Drawing.Size(401, 291);
            this.gcTipoServicio.TabIndex = 4;
            this.gcTipoServicio.UseEmbeddedNavigator = true;
            this.gcTipoServicio.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTipoServicio});
            // 
            // bsTipoServicio
            // 
            this.bsTipoServicio.DataSource = typeof(BE_Servicios.eProyecto.eProyecto_Tipo_Servicio);
            // 
            // gvTipoServicio
            // 
            this.gvTipoServicio.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvTipoServicio.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvTipoServicio.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvTipoServicio.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvTipoServicio.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvTipoServicio.ColumnPanelRowHeight = 30;
            this.gvTipoServicio.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_tipo_servicio,
            this.coldsc_tipo_servicio,
            this.collkpCaracteristica});
            this.gvTipoServicio.GridControl = this.gcTipoServicio;
            this.gvTipoServicio.Name = "gvTipoServicio";
            this.gvTipoServicio.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvTipoServicio.OptionsView.EnableAppearanceEvenRow = true;
            this.gvTipoServicio.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gvTipoServicio.OptionsView.ShowGroupPanel = false;
            this.gvTipoServicio.OptionsView.ShowIndicator = false;
            this.gvTipoServicio.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvTipoServicio_RowClick);
            this.gvTipoServicio.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvTipoServicio_CustomDrawColumnHeader);
            this.gvTipoServicio.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvTipoServicio_CustomDrawCell);
            this.gvTipoServicio.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvTipoServicio_RowCellStyle);
            this.gvTipoServicio.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvTipoServicio_RowStyle);
            this.gvTipoServicio.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvTipoServicio_ShowingEditor);
            this.gvTipoServicio.HiddenEditor += new System.EventHandler(this.gvTipoServicio_HiddenEditor);
            // 
            // colcod_tipo_servicio
            // 
            this.colcod_tipo_servicio.Caption = "Código";
            this.colcod_tipo_servicio.FieldName = "cod_tipo_servicio";
            this.colcod_tipo_servicio.Name = "colcod_tipo_servicio";
            this.colcod_tipo_servicio.OptionsColumn.FixedWidth = true;
            this.colcod_tipo_servicio.Width = 70;
            // 
            // coldsc_tipo_servicio
            // 
            this.coldsc_tipo_servicio.Caption = "Tipo Servicio";
            this.coldsc_tipo_servicio.FieldName = "dsc_tipo_servicio";
            this.coldsc_tipo_servicio.Name = "coldsc_tipo_servicio";
            this.coldsc_tipo_servicio.OptionsColumn.FixedWidth = true;
            this.coldsc_tipo_servicio.Visible = true;
            this.coldsc_tipo_servicio.VisibleIndex = 0;
            this.coldsc_tipo_servicio.Width = 150;
            // 
            // collkpCaracteristica
            // 
            this.collkpCaracteristica.Caption = "Característica";
            this.collkpCaracteristica.ColumnEdit = this.rlkpCaracteristica;
            this.collkpCaracteristica.FieldName = "cod_caracteristica";
            this.collkpCaracteristica.Name = "collkpCaracteristica";
            this.collkpCaracteristica.OptionsColumn.FixedWidth = true;
            this.collkpCaracteristica.Visible = true;
            this.collkpCaracteristica.VisibleIndex = 1;
            this.collkpCaracteristica.Width = 150;
            // 
            // rlkpCaracteristica
            // 
            this.rlkpCaracteristica.AutoHeight = false;
            this.rlkpCaracteristica.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rlkpCaracteristica.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_caracteristica", "Descripción")});
            this.rlkpCaracteristica.DisplayMember = "dsc_caracteristica";
            this.rlkpCaracteristica.Name = "rlkpCaracteristica";
            this.rlkpCaracteristica.ValueMember = "cod_caracteristica";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.controlTipoServicio,
            this.controlSubTipoServicio,
            this.controlEmpresas});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.Root.Size = new System.Drawing.Size(822, 584);
            this.Root.TextVisible = false;
            // 
            // controlTipoServicio
            // 
            this.controlTipoServicio.Control = this.gcTipoServicio;
            this.controlTipoServicio.Location = new System.Drawing.Point(0, 0);
            this.controlTipoServicio.Name = "controlTipoServicio";
            this.controlTipoServicio.Size = new System.Drawing.Size(405, 295);
            this.controlTipoServicio.TextSize = new System.Drawing.Size(0, 0);
            this.controlTipoServicio.TextVisible = false;
            // 
            // controlSubTipoServicio
            // 
            this.controlSubTipoServicio.Control = this.gcSubTipoServicio;
            this.controlSubTipoServicio.Location = new System.Drawing.Point(405, 0);
            this.controlSubTipoServicio.Name = "controlSubTipoServicio";
            this.controlSubTipoServicio.Size = new System.Drawing.Size(405, 295);
            this.controlSubTipoServicio.TextSize = new System.Drawing.Size(0, 0);
            this.controlSubTipoServicio.TextVisible = false;
            // 
            // controlEmpresas
            // 
            this.controlEmpresas.Control = this.gcEmpresas;
            this.controlEmpresas.Location = new System.Drawing.Point(0, 295);
            this.controlEmpresas.Name = "controlEmpresas";
            this.controlEmpresas.Size = new System.Drawing.Size(810, 277);
            this.controlEmpresas.TextSize = new System.Drawing.Size(0, 0);
            this.controlEmpresas.TextVisible = false;
            // 
            // frmMantTipoSubTipo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 584);
            this.Controls.Add(this.controlPrincipal);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmMantTipoSubTipo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tipo / SubTipo";
            this.Load += new System.EventHandler(this.frmMantTipoSubTipo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.controlPrincipal)).EndInit();
            this.controlPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcEmpresas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpresas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSubTipoServicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSubTipoServicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSubTipoServicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTipoServicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTipoServicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTipoServicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlkpCaracteristica)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlTipoServicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSubTipoServicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlEmpresas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl controlPrincipal;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcEmpresas;
        private DevExpress.XtraGrid.Views.Grid.GridView gvEmpresas;
        private DevExpress.XtraGrid.GridControl gcSubTipoServicio;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSubTipoServicio;
        private DevExpress.XtraGrid.GridControl gcTipoServicio;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTipoServicio;
        private DevExpress.XtraLayout.LayoutControlItem controlTipoServicio;
        private DevExpress.XtraLayout.LayoutControlItem controlSubTipoServicio;
        private DevExpress.XtraLayout.LayoutControlItem controlEmpresas;
        private System.Windows.Forms.BindingSource bsTipoServicio;
        private System.Windows.Forms.BindingSource bsSubTipoServicio;
        private System.Windows.Forms.BindingSource bsEmpresa;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_tipo_servicio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_tipo_servicio;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_subtipo_servicio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_subtipo_servicio;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_empresa;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_empresa;
        private DevExpress.XtraGrid.Columns.GridColumn collkpCaracteristica;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rlkpCaracteristica;
    }
}