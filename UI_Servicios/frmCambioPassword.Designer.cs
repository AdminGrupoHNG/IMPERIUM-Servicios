namespace UI_Servicios
{
    partial class frmCambioPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCambioPassword));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtPasswordAntiguo = new DevExpress.XtraEditors.TextEdit();
            this.txtPasswordNuevo = new DevExpress.XtraEditors.TextEdit();
            this.txtPasswordNuevoReconfirmar = new DevExpress.XtraEditors.TextEdit();
            this.btnGuardarPassword = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordAntiguo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordNuevo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordNuevoReconfirmar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtPasswordAntiguo);
            this.layoutControl1.Controls.Add(this.txtPasswordNuevo);
            this.layoutControl1.Controls.Add(this.txtPasswordNuevoReconfirmar);
            this.layoutControl1.Controls.Add(this.btnGuardarPassword);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(374, 158);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtPasswordAntiguo
            // 
            this.txtPasswordAntiguo.Location = new System.Drawing.Point(136, 34);
            this.txtPasswordAntiguo.Name = "txtPasswordAntiguo";
            this.txtPasswordAntiguo.Properties.PasswordChar = '*';
            this.txtPasswordAntiguo.Size = new System.Drawing.Size(232, 20);
            this.txtPasswordAntiguo.StyleController = this.layoutControl1;
            this.txtPasswordAntiguo.TabIndex = 4;
            // 
            // txtPasswordNuevo
            // 
            this.txtPasswordNuevo.Location = new System.Drawing.Point(136, 58);
            this.txtPasswordNuevo.Name = "txtPasswordNuevo";
            this.txtPasswordNuevo.Properties.PasswordChar = '*';
            this.txtPasswordNuevo.Size = new System.Drawing.Size(232, 20);
            this.txtPasswordNuevo.StyleController = this.layoutControl1;
            this.txtPasswordNuevo.TabIndex = 5;
            // 
            // txtPasswordNuevoReconfirmar
            // 
            this.txtPasswordNuevoReconfirmar.Location = new System.Drawing.Point(136, 82);
            this.txtPasswordNuevoReconfirmar.Name = "txtPasswordNuevoReconfirmar";
            this.txtPasswordNuevoReconfirmar.Properties.PasswordChar = '*';
            this.txtPasswordNuevoReconfirmar.Size = new System.Drawing.Size(232, 20);
            this.txtPasswordNuevoReconfirmar.StyleController = this.layoutControl1;
            this.txtPasswordNuevoReconfirmar.TabIndex = 6;
            // 
            // btnGuardarPassword
            // 
            this.btnGuardarPassword.Appearance.BackColor = System.Drawing.Color.Gray;
            this.btnGuardarPassword.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnGuardarPassword.Appearance.Options.UseBackColor = true;
            this.btnGuardarPassword.Appearance.Options.UseFont = true;
            this.btnGuardarPassword.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarPassword.ImageOptions.Image")));
            this.btnGuardarPassword.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnGuardarPassword.ImageOptions.ImageToTextIndent = 10;
            this.btnGuardarPassword.Location = new System.Drawing.Point(104, 112);
            this.btnGuardarPassword.Name = "btnGuardarPassword";
            this.btnGuardarPassword.Size = new System.Drawing.Size(166, 34);
            this.btnGuardarPassword.StyleController = this.layoutControl1;
            this.btnGuardarPassword.TabIndex = 7;
            this.btnGuardarPassword.Text = "Guardar";
            this.btnGuardarPassword.Click += new System.EventHandler(this.btnGuardarPassword_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.simpleLabelItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.Root.Size = new System.Drawing.Size(374, 158);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtPasswordAntiguo;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(366, 24);
            this.layoutControlItem1.Text = "Contraseña antigua : ";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(125, 20);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(130)))), ((int)(((byte)(53)))));
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(366, 28);
            this.simpleLabelItem1.Text = "Cambiar contraseña";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(202, 24);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtPasswordNuevo;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(366, 24);
            this.layoutControlItem2.Text = "Contraseña nueva : ";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(125, 20);
            this.layoutControlItem2.TextToControlDistance = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtPasswordNuevoReconfirmar;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 76);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(366, 24);
            this.layoutControlItem3.Text = "Reconfirmar contraseña : ";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(125, 20);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnGuardarPassword;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 100);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(234, 42);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(100, 100, 8, 8);
            this.layoutControlItem4.Size = new System.Drawing.Size(366, 50);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // frmCambioPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 158);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.Image = global::UI_Servicios.Properties.Resources.Logo_HNG;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCambioPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cambiar contraseña";
            this.Load += new System.EventHandler(this.frmCambioPassword_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordAntiguo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordNuevo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordNuevoReconfirmar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit txtPasswordAntiguo;
        private DevExpress.XtraEditors.TextEdit txtPasswordNuevo;
        private DevExpress.XtraEditors.TextEdit txtPasswordNuevoReconfirmar;
        private DevExpress.XtraEditors.SimpleButton btnGuardarPassword;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}