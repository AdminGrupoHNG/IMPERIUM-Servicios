namespace UI_Servicios.Formularios.Cotizaciones
{
    partial class frmVersionesAnalisis
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
            this.gcVersiones = new DevExpress.XtraGrid.GridControl();
            this.bsVersiones = new System.Windows.Forms.BindingSource(this.components);
            this.gvVersiones = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.coldsc_version = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rmeObservaciones = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colfch_registro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_obs_version = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcVersiones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVersiones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVersiones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmeObservaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcVersiones);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(398, 268);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcVersiones
            // 
            this.gcVersiones.DataSource = this.bsVersiones;
            this.gcVersiones.Location = new System.Drawing.Point(12, 12);
            this.gcVersiones.MainView = this.gvVersiones;
            this.gcVersiones.Name = "gcVersiones";
            this.gcVersiones.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rmeObservaciones});
            this.gcVersiones.Size = new System.Drawing.Size(374, 244);
            this.gcVersiones.TabIndex = 4;
            this.gcVersiones.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvVersiones});
            // 
            // bsVersiones
            // 
            this.bsVersiones.DataSource = typeof(BE_Servicios.eAnalisis.eAnalisis_Sedes_Prestacion);
            // 
            // gvVersiones
            // 
            this.gvVersiones.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvVersiones.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvVersiones.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvVersiones.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvVersiones.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvVersiones.ColumnPanelRowHeight = 30;
            this.gvVersiones.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.coldsc_version,
            this.colfch_registro,
            this.coldsc_obs_version});
            this.gvVersiones.GridControl = this.gcVersiones;
            this.gvVersiones.Name = "gvVersiones";
            this.gvVersiones.OptionsBehavior.Editable = false;
            this.gvVersiones.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gvVersiones.OptionsView.RowAutoHeight = true;
            this.gvVersiones.OptionsView.ShowGroupPanel = false;
            this.gvVersiones.OptionsView.ShowIndicator = false;
            this.gvVersiones.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvVersiones_RowClick);
            this.gvVersiones.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvVersiones_CustomDrawColumnHeader);
            this.gvVersiones.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvVersiones_RowStyle);
            // 
            // coldsc_version
            // 
            this.coldsc_version.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_version.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_version.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.coldsc_version.Caption = "Versión";
            this.coldsc_version.ColumnEdit = this.rmeObservaciones;
            this.coldsc_version.FieldName = "dsc_version";
            this.coldsc_version.Name = "coldsc_version";
            this.coldsc_version.OptionsColumn.FixedWidth = true;
            this.coldsc_version.Visible = true;
            this.coldsc_version.VisibleIndex = 0;
            this.coldsc_version.Width = 50;
            // 
            // rmeObservaciones
            // 
            this.rmeObservaciones.Name = "rmeObservaciones";
            // 
            // colfch_registro
            // 
            this.colfch_registro.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_registro.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_registro.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colfch_registro.Caption = "Fecha";
            this.colfch_registro.FieldName = "fch_registro";
            this.colfch_registro.Name = "colfch_registro";
            this.colfch_registro.OptionsColumn.FixedWidth = true;
            this.colfch_registro.Visible = true;
            this.colfch_registro.VisibleIndex = 1;
            this.colfch_registro.Width = 80;
            // 
            // coldsc_obs_version
            // 
            this.coldsc_obs_version.Caption = "Observaciones";
            this.coldsc_obs_version.ColumnEdit = this.rmeObservaciones;
            this.coldsc_obs_version.FieldName = "dsc_obs_version";
            this.coldsc_obs_version.Name = "coldsc_obs_version";
            this.coldsc_obs_version.OptionsColumn.FixedWidth = true;
            this.coldsc_obs_version.Visible = true;
            this.coldsc_obs_version.VisibleIndex = 2;
            this.coldsc_obs_version.Width = 100;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(398, 268);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcVersiones;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(378, 248);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmVersionesAnalisis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 268);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmVersionesAnalisis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Historial de Versiones";
            this.Load += new System.EventHandler(this.frmHistorialVersiones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcVersiones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVersiones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVersiones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmeObservaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcVersiones;
        private DevExpress.XtraGrid.Views.Grid.GridView gvVersiones;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource bsVersiones;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_registro;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_version;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_obs_version;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit rmeObservaciones;
    }
}