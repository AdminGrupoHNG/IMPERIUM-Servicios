namespace UI_Servicios.Formularios.Cotizaciones
{
    partial class frmMantCargos
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtSalMin = new DevExpress.XtraEditors.TextEdit();
            this.txtSalMax = new DevExpress.XtraEditors.TextEdit();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.txtCargo = new DevExpress.XtraEditors.TextEdit();
            this.lkpArea = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpSedeEmpresa = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpEmpresa = new DevExpress.XtraEditors.LookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.controlArea = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlCargo = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlEmpresa = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlSedeEmpresa = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlGuardar = new DevExpress.XtraLayout.LayoutControlItem();
            this.espacioDos = new DevExpress.XtraLayout.EmptySpaceItem();
            this.espacioTres = new DevExpress.XtraLayout.EmptySpaceItem();
            this.controlSalMax = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlSalMin = new DevExpress.XtraLayout.LayoutControlItem();
            this.espacioUno = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCargo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSedeEmpresa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEmpresa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCargo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSedeEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlGuardar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.espacioDos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.espacioTres)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSalMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSalMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.espacioUno)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtSalMin);
            this.layoutControl1.Controls.Add(this.txtSalMax);
            this.layoutControl1.Controls.Add(this.btnGuardar);
            this.layoutControl1.Controls.Add(this.txtCargo);
            this.layoutControl1.Controls.Add(this.lkpArea);
            this.layoutControl1.Controls.Add(this.lkpSedeEmpresa);
            this.layoutControl1.Controls.Add(this.lkpEmpresa);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(-1574, -141, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(318, 193);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtSalMin
            // 
            this.txtSalMin.EditValue = "0";
            this.txtSalMin.Location = new System.Drawing.Point(112, 103);
            this.txtSalMin.Name = "txtSalMin";
            this.txtSalMin.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSalMin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtSalMin.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtSalMin.Properties.MaskSettings.Set("mask", "c");
            this.txtSalMin.Properties.UseMaskAsDisplayFormat = true;
            this.txtSalMin.Size = new System.Drawing.Size(199, 20);
            this.txtSalMin.StyleController = this.layoutControl1;
            this.txtSalMin.TabIndex = 8;
            // 
            // txtSalMax
            // 
            this.txtSalMax.EditValue = "0";
            this.txtSalMax.Location = new System.Drawing.Point(112, 127);
            this.txtSalMax.Name = "txtSalMax";
            this.txtSalMax.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSalMax.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtSalMax.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtSalMax.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtSalMax.Properties.MaskSettings.Set("mask", "c");
            this.txtSalMax.Properties.UseMaskAsDisplayFormat = true;
            this.txtSalMax.Size = new System.Drawing.Size(199, 20);
            this.txtSalMax.StyleController = this.layoutControl1;
            this.txtSalMax.TabIndex = 7;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(139)))), ((int)(((byte)(125)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.Appearance.Options.UseForeColor = true;
            this.btnGuardar.Location = new System.Drawing.Point(103, 164);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(112, 22);
            this.btnGuardar.StyleController = this.layoutControl1;
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtCargo
            // 
            this.txtCargo.Location = new System.Drawing.Point(112, 79);
            this.txtCargo.Name = "txtCargo";
            this.txtCargo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCargo.Size = new System.Drawing.Size(199, 20);
            this.txtCargo.StyleController = this.layoutControl1;
            this.txtCargo.TabIndex = 5;
            // 
            // lkpArea
            // 
            this.lkpArea.Enabled = false;
            this.lkpArea.Location = new System.Drawing.Point(112, 55);
            this.lkpArea.Name = "lkpArea";
            this.lkpArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpArea.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_area", "Descripción")});
            this.lkpArea.Properties.NullText = "";
            this.lkpArea.Size = new System.Drawing.Size(199, 20);
            this.lkpArea.StyleController = this.layoutControl1;
            this.lkpArea.TabIndex = 4;
            // 
            // lkpSedeEmpresa
            // 
            this.lkpSedeEmpresa.Location = new System.Drawing.Point(112, 31);
            this.lkpSedeEmpresa.Name = "lkpSedeEmpresa";
            this.lkpSedeEmpresa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpSedeEmpresa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_sede_empresa", "Descripción")});
            this.lkpSedeEmpresa.Properties.NullText = "";
            this.lkpSedeEmpresa.Size = new System.Drawing.Size(199, 20);
            this.lkpSedeEmpresa.StyleController = this.layoutControl1;
            this.lkpSedeEmpresa.TabIndex = 5;
            this.lkpSedeEmpresa.EditValueChanged += new System.EventHandler(this.lkpSedeEmpresa_EditValueChanged);
            // 
            // lkpEmpresa
            // 
            this.lkpEmpresa.Location = new System.Drawing.Point(112, 7);
            this.lkpEmpresa.Name = "lkpEmpresa";
            this.lkpEmpresa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpEmpresa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_empresa", "Descripción")});
            this.lkpEmpresa.Properties.NullText = "";
            this.lkpEmpresa.Size = new System.Drawing.Size(199, 20);
            this.lkpEmpresa.StyleController = this.layoutControl1;
            this.lkpEmpresa.TabIndex = 4;
            this.lkpEmpresa.EditValueChanged += new System.EventHandler(this.lkpEmpresa_EditValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.controlArea,
            this.controlCargo,
            this.controlEmpresa,
            this.controlSedeEmpresa,
            this.controlGuardar,
            this.espacioDos,
            this.espacioTres,
            this.controlSalMax,
            this.controlSalMin,
            this.espacioUno});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(318, 193);
            this.Root.TextVisible = false;
            // 
            // controlArea
            // 
            this.controlArea.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlArea.AppearanceItemCaption.Options.UseFont = true;
            this.controlArea.Control = this.lkpArea;
            this.controlArea.CustomizationFormText = "Area";
            this.controlArea.Location = new System.Drawing.Point(0, 48);
            this.controlArea.Name = "controlArea";
            this.controlArea.Size = new System.Drawing.Size(308, 24);
            this.controlArea.Text = "Area :";
            this.controlArea.TextSize = new System.Drawing.Size(93, 13);
            // 
            // controlCargo
            // 
            this.controlCargo.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlCargo.AppearanceItemCaption.Options.UseFont = true;
            this.controlCargo.Control = this.txtCargo;
            this.controlCargo.CustomizationFormText = "Cargo";
            this.controlCargo.Location = new System.Drawing.Point(0, 72);
            this.controlCargo.Name = "controlCargo";
            this.controlCargo.Size = new System.Drawing.Size(308, 24);
            this.controlCargo.Text = "Cargo :";
            this.controlCargo.TextSize = new System.Drawing.Size(93, 13);
            // 
            // controlEmpresa
            // 
            this.controlEmpresa.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.controlEmpresa.AppearanceItemCaption.Options.UseFont = true;
            this.controlEmpresa.Control = this.lkpEmpresa;
            this.controlEmpresa.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.controlEmpresa.CustomizationFormText = "Empresa";
            this.controlEmpresa.Location = new System.Drawing.Point(0, 0);
            this.controlEmpresa.Name = "controlEmpresa";
            this.controlEmpresa.Size = new System.Drawing.Size(308, 24);
            this.controlEmpresa.Text = "Empresa :";
            this.controlEmpresa.TextSize = new System.Drawing.Size(93, 13);
            // 
            // controlSedeEmpresa
            // 
            this.controlSedeEmpresa.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.controlSedeEmpresa.AppearanceItemCaption.Options.UseFont = true;
            this.controlSedeEmpresa.Control = this.lkpSedeEmpresa;
            this.controlSedeEmpresa.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.controlSedeEmpresa.CustomizationFormText = "Sede";
            this.controlSedeEmpresa.Location = new System.Drawing.Point(0, 24);
            this.controlSedeEmpresa.Name = "controlSedeEmpresa";
            this.controlSedeEmpresa.Size = new System.Drawing.Size(308, 24);
            this.controlSedeEmpresa.Text = "Sede :";
            this.controlSedeEmpresa.TextSize = new System.Drawing.Size(93, 13);
            // 
            // controlGuardar
            // 
            this.controlGuardar.Control = this.btnGuardar;
            this.controlGuardar.Location = new System.Drawing.Point(96, 157);
            this.controlGuardar.Name = "controlGuardar";
            this.controlGuardar.Size = new System.Drawing.Size(116, 26);
            this.controlGuardar.TextSize = new System.Drawing.Size(0, 0);
            this.controlGuardar.TextVisible = false;
            // 
            // espacioDos
            // 
            this.espacioDos.AllowHotTrack = false;
            this.espacioDos.Location = new System.Drawing.Point(0, 157);
            this.espacioDos.Name = "espacioDos";
            this.espacioDos.Size = new System.Drawing.Size(96, 26);
            this.espacioDos.TextSize = new System.Drawing.Size(0, 0);
            // 
            // espacioTres
            // 
            this.espacioTres.AllowHotTrack = false;
            this.espacioTres.Location = new System.Drawing.Point(212, 157);
            this.espacioTres.Name = "espacioTres";
            this.espacioTres.Size = new System.Drawing.Size(96, 26);
            this.espacioTres.TextSize = new System.Drawing.Size(0, 0);
            // 
            // controlSalMax
            // 
            this.controlSalMax.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.controlSalMax.AppearanceItemCaption.Options.UseFont = true;
            this.controlSalMax.Control = this.txtSalMax;
            this.controlSalMax.CustomizationFormText = "Salario Míáximo";
            this.controlSalMax.Location = new System.Drawing.Point(0, 120);
            this.controlSalMax.Name = "controlSalMax";
            this.controlSalMax.Size = new System.Drawing.Size(308, 24);
            this.controlSalMax.Text = "Salario Máximo :";
            this.controlSalMax.TextSize = new System.Drawing.Size(93, 13);
            // 
            // controlSalMin
            // 
            this.controlSalMin.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.controlSalMin.AppearanceItemCaption.Options.UseFont = true;
            this.controlSalMin.Control = this.txtSalMin;
            this.controlSalMin.CustomizationFormText = "Salario Mínimo";
            this.controlSalMin.Location = new System.Drawing.Point(0, 96);
            this.controlSalMin.Name = "controlSalMin";
            this.controlSalMin.Size = new System.Drawing.Size(308, 24);
            this.controlSalMin.Text = "Salario Mínimo :";
            this.controlSalMin.TextSize = new System.Drawing.Size(93, 13);
            // 
            // espacioUno
            // 
            this.espacioUno.AllowHotTrack = false;
            this.espacioUno.Location = new System.Drawing.Point(0, 144);
            this.espacioUno.Name = "espacioUno";
            this.espacioUno.Size = new System.Drawing.Size(308, 13);
            this.espacioUno.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmMantCargos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 193);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.MinimumSize = new System.Drawing.Size(300, 164);
            this.Name = "frmMantCargos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nuevo Cargo";
            this.Load += new System.EventHandler(this.frmMantCargos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSalMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCargo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSedeEmpresa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEmpresa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCargo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSedeEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlGuardar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.espacioDos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.espacioTres)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSalMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSalMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.espacioUno)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit txtCargo;
        private DevExpress.XtraEditors.LookUpEdit lkpArea;
        private DevExpress.XtraLayout.LayoutControlItem controlArea;
        private DevExpress.XtraLayout.LayoutControlItem controlCargo;
        private DevExpress.XtraEditors.LookUpEdit lkpSedeEmpresa;
        private DevExpress.XtraLayout.LayoutControlItem controlSedeEmpresa;
        private DevExpress.XtraEditors.LookUpEdit lkpEmpresa;
        private DevExpress.XtraLayout.LayoutControlItem controlEmpresa;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraLayout.LayoutControlItem controlGuardar;
        private DevExpress.XtraLayout.EmptySpaceItem espacioDos;
        private DevExpress.XtraLayout.EmptySpaceItem espacioTres;
        private DevExpress.XtraEditors.TextEdit txtSalMin;
        private DevExpress.XtraEditors.TextEdit txtSalMax;
        private DevExpress.XtraLayout.LayoutControlItem controlSalMax;
        private DevExpress.XtraLayout.LayoutControlItem controlSalMin;
        private DevExpress.XtraLayout.EmptySpaceItem espacioUno;
    }
}