namespace UI_Servicios.Formularios.Cotizaciones
{
    partial class frmSeleccionPuestos
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
            this.controlGeneral = new DevExpress.XtraLayout.LayoutControl();
            this.chkSeleccionarTodos = new DevExpress.XtraEditors.CheckEdit();
            this.btnConfirmar = new DevExpress.XtraEditors.SimpleButton();
            this.gcPuestos = new DevExpress.XtraGrid.GridControl();
            this.bsPuestos = new System.Windows.Forms.BindingSource(this.components);
            this.gvPuestos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colsel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_cargo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_rango_horario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.controlPuestos = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlConfirmar = new DevExpress.XtraLayout.LayoutControlItem();
            this.espacioUno = new DevExpress.XtraLayout.EmptySpaceItem();
            this.controlSeleccionarTodos = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.controlGeneral)).BeginInit();
            this.controlGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSeleccionarTodos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPuestos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPuestos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPuestos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlPuestos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlConfirmar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.espacioUno)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSeleccionarTodos)).BeginInit();
            this.SuspendLayout();
            // 
            // controlGeneral
            // 
            this.controlGeneral.Controls.Add(this.chkSeleccionarTodos);
            this.controlGeneral.Controls.Add(this.btnConfirmar);
            this.controlGeneral.Controls.Add(this.gcPuestos);
            this.controlGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlGeneral.Location = new System.Drawing.Point(0, 0);
            this.controlGeneral.Name = "controlGeneral";
            this.controlGeneral.Root = this.Root;
            this.controlGeneral.Size = new System.Drawing.Size(448, 318);
            this.controlGeneral.TabIndex = 0;
            this.controlGeneral.Text = "layoutControl1";
            // 
            // chkSeleccionarTodos
            // 
            this.chkSeleccionarTodos.Location = new System.Drawing.Point(12, 12);
            this.chkSeleccionarTodos.Name = "chkSeleccionarTodos";
            this.chkSeleccionarTodos.Properties.Caption = "Seleccionar Todos";
            this.chkSeleccionarTodos.Size = new System.Drawing.Size(424, 20);
            this.chkSeleccionarTodos.StyleController = this.controlGeneral;
            this.chkSeleccionarTodos.TabIndex = 6;
            this.chkSeleccionarTodos.CheckedChanged += new System.EventHandler(this.chkSeleccionarTodos_CheckedChanged);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(139)))), ((int)(((byte)(125)))));
            this.btnConfirmar.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar.Appearance.Options.UseBackColor = true;
            this.btnConfirmar.Appearance.Options.UseFont = true;
            this.btnConfirmar.Appearance.Options.UseForeColor = true;
            this.btnConfirmar.Location = new System.Drawing.Point(335, 284);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(101, 22);
            this.btnConfirmar.StyleController = this.controlGeneral;
            this.btnConfirmar.TabIndex = 5;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // gcPuestos
            // 
            this.gcPuestos.DataSource = this.bsPuestos;
            this.gcPuestos.Location = new System.Drawing.Point(12, 36);
            this.gcPuestos.MainView = this.gvPuestos;
            this.gcPuestos.Name = "gcPuestos";
            this.gcPuestos.Size = new System.Drawing.Size(424, 244);
            this.gcPuestos.TabIndex = 4;
            this.gcPuestos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPuestos});
            // 
            // bsPuestos
            // 
            this.bsPuestos.DataSource = typeof(BE_Servicios.eAnalisis.eAnalisis_Personal);
            // 
            // gvPuestos
            // 
            this.gvPuestos.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvPuestos.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvPuestos.ColumnPanelRowHeight = 35;
            this.gvPuestos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colsel,
            this.coldsc_cargo,
            this.coldsc_rango_horario});
            this.gvPuestos.GridControl = this.gcPuestos;
            this.gvPuestos.Name = "gvPuestos";
            this.gvPuestos.OptionsView.ShowGroupPanel = false;
            this.gvPuestos.OptionsView.ShowIndicator = false;
            this.gvPuestos.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvPuestos_CustomDrawColumnHeader);
            this.gvPuestos.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvPuestos_RowStyle);
            this.gvPuestos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvPuestos_KeyDown);
            // 
            // colsel
            // 
            this.colsel.Caption = " ";
            this.colsel.FieldName = "sel";
            this.colsel.Name = "colsel";
            this.colsel.OptionsColumn.FixedWidth = true;
            this.colsel.Visible = true;
            this.colsel.VisibleIndex = 0;
            this.colsel.Width = 20;
            // 
            // coldsc_cargo
            // 
            this.coldsc_cargo.Caption = "Cargo";
            this.coldsc_cargo.FieldName = "dsc_cargo";
            this.coldsc_cargo.Name = "coldsc_cargo";
            this.coldsc_cargo.OptionsColumn.AllowEdit = false;
            this.coldsc_cargo.OptionsColumn.FixedWidth = true;
            this.coldsc_cargo.Visible = true;
            this.coldsc_cargo.VisibleIndex = 1;
            this.coldsc_cargo.Width = 200;
            // 
            // coldsc_rango_horario
            // 
            this.coldsc_rango_horario.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_rango_horario.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_rango_horario.Caption = "Horario";
            this.coldsc_rango_horario.FieldName = "dsc_rango_horario";
            this.coldsc_rango_horario.Name = "coldsc_rango_horario";
            this.coldsc_rango_horario.OptionsColumn.AllowEdit = false;
            this.coldsc_rango_horario.OptionsColumn.FixedWidth = true;
            this.coldsc_rango_horario.Visible = true;
            this.coldsc_rango_horario.VisibleIndex = 2;
            this.coldsc_rango_horario.Width = 150;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.controlPuestos,
            this.controlConfirmar,
            this.espacioUno,
            this.controlSeleccionarTodos});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(448, 318);
            this.Root.TextVisible = false;
            // 
            // controlPuestos
            // 
            this.controlPuestos.Control = this.gcPuestos;
            this.controlPuestos.Location = new System.Drawing.Point(0, 24);
            this.controlPuestos.Name = "controlPuestos";
            this.controlPuestos.Size = new System.Drawing.Size(428, 248);
            this.controlPuestos.TextSize = new System.Drawing.Size(0, 0);
            this.controlPuestos.TextVisible = false;
            // 
            // controlConfirmar
            // 
            this.controlConfirmar.Control = this.btnConfirmar;
            this.controlConfirmar.Location = new System.Drawing.Point(323, 272);
            this.controlConfirmar.Name = "controlConfirmar";
            this.controlConfirmar.Size = new System.Drawing.Size(105, 26);
            this.controlConfirmar.TextSize = new System.Drawing.Size(0, 0);
            this.controlConfirmar.TextVisible = false;
            // 
            // espacioUno
            // 
            this.espacioUno.AllowHotTrack = false;
            this.espacioUno.Location = new System.Drawing.Point(0, 272);
            this.espacioUno.Name = "espacioUno";
            this.espacioUno.Size = new System.Drawing.Size(323, 26);
            this.espacioUno.TextSize = new System.Drawing.Size(0, 0);
            // 
            // controlSeleccionarTodos
            // 
            this.controlSeleccionarTodos.Control = this.chkSeleccionarTodos;
            this.controlSeleccionarTodos.Location = new System.Drawing.Point(0, 0);
            this.controlSeleccionarTodos.Name = "controlSeleccionarTodos";
            this.controlSeleccionarTodos.Size = new System.Drawing.Size(428, 24);
            this.controlSeleccionarTodos.TextSize = new System.Drawing.Size(0, 0);
            this.controlSeleccionarTodos.TextVisible = false;
            // 
            // frmSeleccionPuestos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 318);
            this.Controls.Add(this.controlGeneral);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmSeleccionPuestos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Seleccionar Puestos";
            this.Load += new System.EventHandler(this.frmSeleccionPuestos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.controlGeneral)).EndInit();
            this.controlGeneral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkSeleccionarTodos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPuestos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPuestos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPuestos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlPuestos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlConfirmar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.espacioUno)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSeleccionarTodos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl controlGeneral;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton btnConfirmar;
        private DevExpress.XtraGrid.GridControl gcPuestos;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPuestos;
        private DevExpress.XtraLayout.LayoutControlItem controlPuestos;
        private DevExpress.XtraLayout.LayoutControlItem controlConfirmar;
        private DevExpress.XtraLayout.EmptySpaceItem espacioUno;
        private DevExpress.XtraEditors.CheckEdit chkSeleccionarTodos;
        private DevExpress.XtraLayout.LayoutControlItem controlSeleccionarTodos;
        private System.Windows.Forms.BindingSource bsPuestos;
        private DevExpress.XtraGrid.Columns.GridColumn colsel;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_cargo;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_rango_horario;
    }
}