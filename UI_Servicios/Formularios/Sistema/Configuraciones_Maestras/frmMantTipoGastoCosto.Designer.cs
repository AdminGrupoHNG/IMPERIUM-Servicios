namespace UI_Servicios.Formularios.Sistema.Configuraciones_Maestras
{
    partial class frmMantTipoGastoCosto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMantTipoGastoCosto));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcTipoGastoCosto = new DevExpress.XtraGrid.GridControl();
            this.bsTipoGastoCosto = new System.Windows.Forms.BindingSource(this.components);
            this.gvTipoGastoCosto = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_und_negocio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_und_negocio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_pref_ceco = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtPrefCECO = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnEliminar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTipoGastoCosto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTipoGastoCosto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTipoGastoCosto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPrefCECO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnEliminar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcTipoGastoCosto);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(709, 389);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcTipoGastoCosto
            // 
            this.gcTipoGastoCosto.DataSource = this.bsTipoGastoCosto;
            this.gcTipoGastoCosto.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcTipoGastoCosto.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcTipoGastoCosto.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcTipoGastoCosto.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcTipoGastoCosto.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcTipoGastoCosto.Location = new System.Drawing.Point(5, 5);
            this.gcTipoGastoCosto.MainView = this.gvTipoGastoCosto;
            this.gcTipoGastoCosto.Name = "gcTipoGastoCosto";
            this.gcTipoGastoCosto.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtPrefCECO,
            this.rbtnEliminar});
            this.gcTipoGastoCosto.Size = new System.Drawing.Size(699, 379);
            this.gcTipoGastoCosto.TabIndex = 5;
            this.gcTipoGastoCosto.UseEmbeddedNavigator = true;
            this.gcTipoGastoCosto.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTipoGastoCosto});
            // 
            // bsTipoGastoCosto
            // 
            this.bsTipoGastoCosto.DataSource = typeof(BE_Servicios.eTipoGastoCosto);
            // 
            // gvTipoGastoCosto
            // 
            this.gvTipoGastoCosto.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvTipoGastoCosto.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvTipoGastoCosto.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvTipoGastoCosto.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvTipoGastoCosto.ColumnPanelRowHeight = 35;
            this.gvTipoGastoCosto.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_und_negocio,
            this.coldsc_und_negocio,
            this.coldsc_pref_ceco,
            this.gridColumn1});
            this.gvTipoGastoCosto.GridControl = this.gcTipoGastoCosto;
            this.gvTipoGastoCosto.Name = "gvTipoGastoCosto";
            this.gvTipoGastoCosto.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvTipoGastoCosto.OptionsView.EnableAppearanceEvenRow = true;
            this.gvTipoGastoCosto.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvTipoGastoCosto.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvTipoGastoCosto_CustomDrawColumnHeader);
            this.gvTipoGastoCosto.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvTipoGastoCosto_RowStyle);
            this.gvTipoGastoCosto.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvTipoGastoCosto_ValidateRow);
            // 
            // colcod_und_negocio
            // 
            this.colcod_und_negocio.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_und_negocio.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_und_negocio.Caption = "Código";
            this.colcod_und_negocio.FieldName = "cod_tipo_gasto";
            this.colcod_und_negocio.Name = "colcod_und_negocio";
            this.colcod_und_negocio.OptionsColumn.AllowEdit = false;
            this.colcod_und_negocio.OptionsColumn.FixedWidth = true;
            this.colcod_und_negocio.Visible = true;
            this.colcod_und_negocio.VisibleIndex = 0;
            this.colcod_und_negocio.Width = 70;
            // 
            // coldsc_und_negocio
            // 
            this.coldsc_und_negocio.Caption = "Tipo Gasto-Costo";
            this.coldsc_und_negocio.FieldName = "dsc_tipo_gasto";
            this.coldsc_und_negocio.Name = "coldsc_und_negocio";
            this.coldsc_und_negocio.OptionsColumn.FixedWidth = true;
            this.coldsc_und_negocio.Visible = true;
            this.coldsc_und_negocio.VisibleIndex = 1;
            this.coldsc_und_negocio.Width = 200;
            // 
            // coldsc_pref_ceco
            // 
            this.coldsc_pref_ceco.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_pref_ceco.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_pref_ceco.Caption = "Pref. CECO";
            this.coldsc_pref_ceco.ColumnEdit = this.rtxtPrefCECO;
            this.coldsc_pref_ceco.FieldName = "dsc_pref_ceco";
            this.coldsc_pref_ceco.Name = "coldsc_pref_ceco";
            this.coldsc_pref_ceco.OptionsColumn.FixedWidth = true;
            this.coldsc_pref_ceco.Visible = true;
            this.coldsc_pref_ceco.VisibleIndex = 2;
            this.coldsc_pref_ceco.Width = 80;
            // 
            // rtxtPrefCECO
            // 
            this.rtxtPrefCECO.AutoHeight = false;
            this.rtxtPrefCECO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.rtxtPrefCECO.MaxLength = 3;
            this.rtxtPrefCECO.Name = "rtxtPrefCECO";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = " ";
            this.gridColumn1.ColumnEdit = this.rbtnEliminar;
            this.gridColumn1.FieldName = "gridColumn1";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridColumn1.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 20;
            // 
            // rbtnEliminar
            // 
            this.rbtnEliminar.AutoHeight = false;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.rbtnEliminar.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.rbtnEliminar.Name = "rbtnEliminar";
            this.rbtnEliminar.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.rbtnEliminar.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.rbtnEliminar_ButtonClick);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.Root.Size = new System.Drawing.Size(709, 389);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcTipoGastoCosto;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(703, 383);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmMantTipoGastoCosto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 389);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMantTipoGastoCosto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tipo Gasto Costo";
            this.Load += new System.EventHandler(this.frmMantGastoCosto_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMantTipoGastoCosto_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTipoGastoCosto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTipoGastoCosto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTipoGastoCosto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPrefCECO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnEliminar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcTipoGastoCosto;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTipoGastoCosto;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_und_negocio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_und_negocio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_pref_ceco;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtPrefCECO;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource bsTipoGastoCosto;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnEliminar;
    }
}