
namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    partial class frmOpcionesReembolso
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
            this.btnAceptar = new DevExpress.XtraEditors.SimpleButton();
            this.grdbOpcionesReembolso = new DevExpress.XtraEditors.RadioGroup();
            this.lkpResponsable = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpTipoCaja = new DevExpress.XtraEditors.LookUpEdit();
            this.txtMontoSaldo = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdbOpcionesReembolso.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpResponsable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoCaja.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontoSaldo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnAceptar);
            this.layoutControl1.Controls.Add(this.grdbOpcionesReembolso);
            this.layoutControl1.Controls.Add(this.lkpResponsable);
            this.layoutControl1.Controls.Add(this.lkpTipoCaja);
            this.layoutControl1.Controls.Add(this.txtMontoSaldo);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(521, 150);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(139)))), ((int)(((byte)(125)))));
            this.btnAceptar.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.btnAceptar.Appearance.Options.UseBackColor = true;
            this.btnAceptar.Appearance.Options.UseFont = true;
            this.btnAceptar.Location = new System.Drawing.Point(167, 109);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(187, 30);
            this.btnAceptar.StyleController = this.layoutControl1;
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // grdbOpcionesReembolso
            // 
            this.grdbOpcionesReembolso.Location = new System.Drawing.Point(89, 12);
            this.grdbOpcionesReembolso.Name = "grdbOpcionesReembolso";
            this.grdbOpcionesReembolso.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.grdbOpcionesReembolso.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.grdbOpcionesReembolso.Properties.Appearance.Options.UseBackColor = true;
            this.grdbOpcionesReembolso.Properties.Appearance.Options.UseFont = true;
            this.grdbOpcionesReembolso.Properties.Columns = 2;
            this.grdbOpcionesReembolso.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Caja chica"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Entrega a rendir")});
            this.grdbOpcionesReembolso.Size = new System.Drawing.Size(343, 36);
            this.grdbOpcionesReembolso.StyleController = this.layoutControl1;
            this.grdbOpcionesReembolso.TabIndex = 4;
            this.grdbOpcionesReembolso.SelectedIndexChanged += new System.EventHandler(this.grdbOpcionesReembolso_SelectedIndexChanged);
            // 
            // lkpResponsable
            // 
            this.lkpResponsable.Location = new System.Drawing.Point(87, 52);
            this.lkpResponsable.Name = "lkpResponsable";
            this.lkpResponsable.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lkpResponsable.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lkpResponsable.Properties.Appearance.Options.UseFont = true;
            this.lkpResponsable.Properties.Appearance.Options.UseForeColor = true;
            this.lkpResponsable.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpResponsable.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_responsable", "Descripción")});
            this.lkpResponsable.Properties.NullText = "";
            this.lkpResponsable.Properties.PopupSizeable = false;
            this.lkpResponsable.Size = new System.Drawing.Size(422, 22);
            this.lkpResponsable.StyleController = this.layoutControl1;
            this.lkpResponsable.TabIndex = 0;
            this.lkpResponsable.EditValueChanged += new System.EventHandler(this.lkpResponsable_EditValueChanged);
            // 
            // lkpTipoCaja
            // 
            this.lkpTipoCaja.Location = new System.Drawing.Point(87, 78);
            this.lkpTipoCaja.Name = "lkpTipoCaja";
            this.lkpTipoCaja.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lkpTipoCaja.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lkpTipoCaja.Properties.Appearance.Options.UseFont = true;
            this.lkpTipoCaja.Properties.Appearance.Options.UseForeColor = true;
            this.lkpTipoCaja.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipoCaja.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_caja", "Descripción")});
            this.lkpTipoCaja.Properties.NullText = "";
            this.lkpTipoCaja.Properties.PopupSizeable = false;
            this.lkpTipoCaja.Size = new System.Drawing.Size(286, 22);
            this.lkpTipoCaja.StyleController = this.layoutControl1;
            this.lkpTipoCaja.TabIndex = 0;
            this.lkpTipoCaja.EditValueChanged += new System.EventHandler(this.lkpTipoCaja_EditValueChanged);
            // 
            // txtMontoSaldo
            // 
            this.txtMontoSaldo.EditValue = "0";
            this.txtMontoSaldo.Location = new System.Drawing.Point(417, 78);
            this.txtMontoSaldo.Name = "txtMontoSaldo";
            this.txtMontoSaldo.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtMontoSaldo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtMontoSaldo.Properties.Appearance.ForeColor = System.Drawing.Color.DarkGreen;
            this.txtMontoSaldo.Properties.Appearance.Options.UseBackColor = true;
            this.txtMontoSaldo.Properties.Appearance.Options.UseFont = true;
            this.txtMontoSaldo.Properties.Appearance.Options.UseForeColor = true;
            this.txtMontoSaldo.Properties.Appearance.Options.UseTextOptions = true;
            this.txtMontoSaldo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtMontoSaldo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtMontoSaldo.Properties.Mask.EditMask = "n2";
            this.txtMontoSaldo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMontoSaldo.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMontoSaldo.Properties.ReadOnly = true;
            this.txtMontoSaldo.Properties.UseReadOnlyAppearance = false;
            this.txtMontoSaldo.Size = new System.Drawing.Size(92, 18);
            this.txtMontoSaldo.StyleController = this.layoutControl1;
            this.txtMontoSaldo.TabIndex = 20;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.emptySpaceItem3,
            this.emptySpaceItem4,
            this.layoutControlItem12,
            this.layoutControlItem13,
            this.layoutControlItem9});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 4);
            this.Root.Size = new System.Drawing.Size(521, 150);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdbOpcionesReembolso;
            this.layoutControlItem1.Location = new System.Drawing.Point(77, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(347, 40);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 92);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(150, 0);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(150, 10);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(150, 44);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(351, 92);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(150, 0);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(150, 10);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(150, 44);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnAceptar;
            this.layoutControlItem2.Location = new System.Drawing.Point(150, 92);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(201, 44);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(201, 44);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 7, 7);
            this.layoutControlItem2.Size = new System.Drawing.Size(201, 44);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(77, 0);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(77, 10);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(77, 40);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(424, 0);
            this.emptySpaceItem4.MaxSize = new System.Drawing.Size(77, 0);
            this.emptySpaceItem4.MinSize = new System.Drawing.Size(77, 10);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(77, 40);
            this.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
            this.layoutControlItem12.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem12.Control = this.lkpResponsable;
            this.layoutControlItem12.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem12.CustomizationFormText = "Empresa :";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(501, 26);
            this.layoutControlItem12.Text = "Responsable : ";
            this.layoutControlItem12.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem12.TextSize = new System.Drawing.Size(70, 20);
            this.layoutControlItem12.TextToControlDistance = 5;
            this.layoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
            this.layoutControlItem13.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem13.Control = this.lkpTipoCaja;
            this.layoutControlItem13.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem13.CustomizationFormText = "Empresa :";
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 66);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(365, 26);
            this.layoutControlItem13.Text = "Caja chica : ";
            this.layoutControlItem13.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem13.TextSize = new System.Drawing.Size(70, 20);
            this.layoutControlItem13.TextToControlDistance = 5;
            this.layoutControlItem13.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
            this.layoutControlItem9.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem9.Control = this.txtMontoSaldo;
            this.layoutControlItem9.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem9.CustomizationFormText = "Saldo caja :";
            this.layoutControlItem9.Location = new System.Drawing.Point(365, 66);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(136, 26);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(136, 26);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(136, 26);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "Saldo : ";
            this.layoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(35, 20);
            this.layoutControlItem9.TextToControlDistance = 5;
            // 
            // frmOpcionesReembolso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 150);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOpcionesReembolso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Opciones reembolso";
            this.Load += new System.EventHandler(this.frmOpcionesReembolso_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdbOpcionesReembolso.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpResponsable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoCaja.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontoSaldo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton btnAceptar;
        private DevExpress.XtraEditors.RadioGroup grdbOpcionesReembolso;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.LookUpEdit lkpResponsable;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraEditors.LookUpEdit lkpTipoCaja;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraEditors.TextEdit txtMontoSaldo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
    }
}