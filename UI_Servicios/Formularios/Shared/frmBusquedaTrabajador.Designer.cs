namespace UI_Servicios.Formularios.Shared
{
    partial class frmBusquedaTrabajador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBusquedaTrabajador));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.btnAgregarTrabajador = new DevExpress.XtraEditors.SimpleButton();
            this.gcListadoTrabajadores = new DevExpress.XtraGrid.GridControl();
            this.bsListadoTrabajadores = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoTrabajadores = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_trabajador = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_nombre_completo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutBoton = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutTipoSeleccion = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoTrabajadores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoTrabajadores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoTrabajadores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBoton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTipoSeleccion)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Controls.Add(this.btnAgregarTrabajador);
            this.layoutControl1.Controls.Add(this.gcListadoTrabajadores);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(568, 423);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioGroup1.Location = new System.Drawing.Point(12, 12);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Eliminar y agregar como nuevo"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Insertar")});
            this.radioGroup1.Size = new System.Drawing.Size(385, 36);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 6;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // btnAgregarTrabajador
            // 
            this.btnAgregarTrabajador.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarTrabajador.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarTrabajador.ImageOptions.Image")));
            this.btnAgregarTrabajador.Location = new System.Drawing.Point(419, 12);
            this.btnAgregarTrabajador.Name = "btnAgregarTrabajador";
            this.btnAgregarTrabajador.Size = new System.Drawing.Size(137, 36);
            this.btnAgregarTrabajador.StyleController = this.layoutControl1;
            this.btnAgregarTrabajador.TabIndex = 5;
            this.btnAgregarTrabajador.Text = "Agregar Trabajador";
            this.btnAgregarTrabajador.Click += new System.EventHandler(this.btnAgregarTrabajador_Click);
            // 
            // gcListadoTrabajadores
            // 
            this.gcListadoTrabajadores.DataSource = this.bsListadoTrabajadores;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.Append.Enabled = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.CancelEdit.Enabled = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.EndEdit.Enabled = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.First.Enabled = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.First.Visible = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.Last.Enabled = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.Last.Visible = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.Next.Enabled = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.Next.Visible = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.NextPage.Enabled = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.Prev.Enabled = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.Prev.Visible = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.PrevPage.Enabled = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.Remove.Enabled = false;
            this.gcListadoTrabajadores.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListadoTrabajadores.Location = new System.Drawing.Point(12, 52);
            this.gcListadoTrabajadores.MainView = this.gvListadoTrabajadores;
            this.gcListadoTrabajadores.Name = "gcListadoTrabajadores";
            this.gcListadoTrabajadores.Size = new System.Drawing.Size(544, 359);
            this.gcListadoTrabajadores.TabIndex = 4;
            this.gcListadoTrabajadores.UseEmbeddedNavigator = true;
            this.gcListadoTrabajadores.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListadoTrabajadores});
            this.gcListadoTrabajadores.Click += new System.EventHandler(this.gcListadoTrabajadores_Click);
            // 
            // bsListadoTrabajadores
            // 
            this.bsListadoTrabajadores.DataSource = typeof(BE_Servicios.eTrabajador);
            // 
            // gvListadoTrabajadores
            // 
            this.gvListadoTrabajadores.Appearance.EvenRow.BackColor = System.Drawing.Color.Gainsboro;
            this.gvListadoTrabajadores.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvListadoTrabajadores.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(88)))));
            this.gvListadoTrabajadores.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvListadoTrabajadores.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvListadoTrabajadores.Appearance.FocusedCell.Options.UseFont = true;
            this.gvListadoTrabajadores.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(88)))));
            this.gvListadoTrabajadores.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvListadoTrabajadores.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvListadoTrabajadores.Appearance.FocusedRow.Options.UseFont = true;
            this.gvListadoTrabajadores.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(88)))));
            this.gvListadoTrabajadores.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvListadoTrabajadores.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gvListadoTrabajadores.Appearance.HideSelectionRow.Options.UseFont = true;
            this.gvListadoTrabajadores.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(88)))));
            this.gvListadoTrabajadores.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvListadoTrabajadores.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gvListadoTrabajadores.Appearance.SelectedRow.Options.UseFont = true;
            this.gvListadoTrabajadores.ColumnPanelRowHeight = 35;
            this.gvListadoTrabajadores.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_trabajador,
            this.coldsc_nombre_completo});
            this.gvListadoTrabajadores.GridControl = this.gcListadoTrabajadores;
            this.gvListadoTrabajadores.Name = "gvListadoTrabajadores";
            this.gvListadoTrabajadores.OptionsBehavior.Editable = false;
            this.gvListadoTrabajadores.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            this.gvListadoTrabajadores.OptionsSelection.CheckBoxSelectorField = "seleccionado";
            this.gvListadoTrabajadores.OptionsSelection.MultiSelect = true;
            this.gvListadoTrabajadores.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvListadoTrabajadores.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListadoTrabajadores.OptionsView.ShowAutoFilterRow = true;
            this.gvListadoTrabajadores.OptionsView.ShowGroupPanel = false;
            this.gvListadoTrabajadores.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListadoTrabajadores_RowClick);
            this.gvListadoTrabajadores.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListadoTrabajadores_CustomDrawColumnHeader);
            this.gvListadoTrabajadores.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListadoTrabajadores_RowStyle);
            // 
            // colcod_trabajador
            // 
            this.colcod_trabajador.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_trabajador.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_trabajador.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.colcod_trabajador.AppearanceHeader.Options.UseForeColor = true;
            this.colcod_trabajador.AppearanceHeader.Options.UseTextOptions = true;
            this.colcod_trabajador.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_trabajador.Caption = "Código";
            this.colcod_trabajador.FieldName = "cod_trabajador";
            this.colcod_trabajador.Name = "colcod_trabajador";
            this.colcod_trabajador.Visible = true;
            this.colcod_trabajador.VisibleIndex = 1;
            this.colcod_trabajador.Width = 106;
            // 
            // coldsc_nombre_completo
            // 
            this.coldsc_nombre_completo.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.coldsc_nombre_completo.AppearanceHeader.Options.UseForeColor = true;
            this.coldsc_nombre_completo.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_nombre_completo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_nombre_completo.Caption = "Trabajador";
            this.coldsc_nombre_completo.FieldName = "dsc_nombre_completo";
            this.coldsc_nombre_completo.Name = "coldsc_nombre_completo";
            this.coldsc_nombre_completo.Visible = true;
            this.coldsc_nombre_completo.VisibleIndex = 2;
            this.coldsc_nombre_completo.Width = 456;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutBoton,
            this.emptySpaceItem1,
            this.layoutTipoSeleccion});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(568, 423);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListadoTrabajadores;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(548, 363);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutBoton
            // 
            this.layoutBoton.Control = this.btnAgregarTrabajador;
            this.layoutBoton.Location = new System.Drawing.Point(407, 0);
            this.layoutBoton.MaxSize = new System.Drawing.Size(141, 40);
            this.layoutBoton.MinSize = new System.Drawing.Size(141, 40);
            this.layoutBoton.Name = "layoutBoton";
            this.layoutBoton.Size = new System.Drawing.Size(141, 40);
            this.layoutBoton.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutBoton.TextSize = new System.Drawing.Size(0, 0);
            this.layoutBoton.TextVisible = false;
            this.layoutBoton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(389, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(18, 40);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            this.emptySpaceItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutTipoSeleccion
            // 
            this.layoutTipoSeleccion.Control = this.radioGroup1;
            this.layoutTipoSeleccion.Location = new System.Drawing.Point(0, 0);
            this.layoutTipoSeleccion.Name = "layoutTipoSeleccion";
            this.layoutTipoSeleccion.Size = new System.Drawing.Size(389, 40);
            this.layoutTipoSeleccion.TextSize = new System.Drawing.Size(0, 0);
            this.layoutTipoSeleccion.TextVisible = false;
            this.layoutTipoSeleccion.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // frmBusquedaTrabajador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 423);
            this.Controls.Add(this.layoutControl1);
            this.KeyPreview = true;
            this.Name = "frmBusquedaTrabajador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Busqueda Trabajadores";
            this.Load += new System.EventHandler(this.frmBusquedaTrabajador_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBusquedaTrabajador_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoTrabajadores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoTrabajadores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoTrabajadores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBoton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTipoSeleccion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcListadoTrabajadores;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoTrabajadores;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource bsListadoTrabajadores;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_trabajador;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_nombre_completo;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.SimpleButton btnAgregarTrabajador;
        private DevExpress.XtraLayout.LayoutControlItem layoutBoton;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutTipoSeleccion;
    }
}