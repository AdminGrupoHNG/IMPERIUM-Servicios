namespace UI_Servicios.Formularios.Sistema.Configuracion_del_Sistema
{
    partial class frmListadoUsuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListadoUsuario));
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.btnActivar = new DevExpress.XtraBars.BarButtonItem();
            this.btnInactivar = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminar = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportarExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnImprimir = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblTitulo = new DevExpress.XtraEditors.LabelControl();
            this.picTitulo = new DevExpress.XtraEditors.PictureEdit();
            this.gcListaUsuario = new DevExpress.XtraGrid.GridControl();
            this.bsListadoUsuario = new System.Windows.Forms.BindingSource(this.components);
            this.gvListaUsuario = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_usuario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_trabajador = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_trabajador = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_correo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_activo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_perfil = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario_cambio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario_registro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_dias_cambio_contraseña = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_audita_login = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_noexpira = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_administrador = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_registro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rptFecha = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colfch_cambioclave = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_cambio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTitulo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptFecha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 659);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1271, 24);
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.btnNuevo,
            this.btnActivar,
            this.btnInactivar,
            this.btnEliminar,
            this.btnExportarExcel,
            this.btnImprimir,
            this.barStaticItem1});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 8;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1271, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Caption = "Nuevo";
            this.btnNuevo.Id = 1;
            this.btnNuevo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.ImageOptions.Image")));
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnNuevo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNuevo_ItemClick);
            // 
            // btnActivar
            // 
            this.btnActivar.Caption = "Activar";
            this.btnActivar.Id = 2;
            this.btnActivar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnActivar.ImageOptions.Image")));
            this.btnActivar.Name = "btnActivar";
            this.btnActivar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnActivar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnActivar_ItemClick);
            // 
            // btnInactivar
            // 
            this.btnInactivar.Caption = "Inactivar";
            this.btnInactivar.Id = 3;
            this.btnInactivar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnInactivar.ImageOptions.Image")));
            this.btnInactivar.Name = "btnInactivar";
            this.btnInactivar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnInactivar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnInactivar_ItemClick);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Caption = "Eliminar";
            this.btnEliminar.Enabled = false;
            this.btnEliminar.Id = 4;
            this.btnEliminar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.ImageOptions.Image")));
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnEliminar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEliminar_ItemClick);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Caption = "Exportar a Excel";
            this.btnExportarExcel.Id = 5;
            this.btnExportarExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.ImageOptions.Image")));
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnExportarExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportarExcel_ItemClick);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Caption = "Imprimir";
            this.btnImprimir.Id = 6;
            this.btnImprimir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.ImageOptions.Image")));
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnImprimir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImprimir_ItemClick);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Presione F5 para actualizar listado";
            this.barStaticItem1.Id = 7;
            this.barStaticItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.barStaticItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Opciones de Usuarios";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnNuevo);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnActivar);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnInactivar);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnEliminar);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Edición";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btnExportarExcel);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnImprimir);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = " Reportes";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblTitulo);
            this.layoutControl1.Controls.Add(this.picTitulo);
            this.layoutControl1.Controls.Add(this.gcListaUsuario);
            this.layoutControl1.Controls.Add(this.navBarControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 158);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Padding = new System.Windows.Forms.Padding(5);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1271, 501);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(130)))), ((int)(((byte)(53)))));
            this.lblTitulo.Appearance.Options.UseFont = true;
            this.lblTitulo.Appearance.Options.UseForeColor = true;
            this.lblTitulo.Location = new System.Drawing.Point(131, 7);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1133, 38);
            this.lblTitulo.StyleController = this.layoutControl1;
            this.lblTitulo.TabIndex = 7;
            this.lblTitulo.Text = "<<Titulo de grupo>>";
            // 
            // picTitulo
            // 
            this.picTitulo.Location = new System.Drawing.Point(66, 7);
            this.picTitulo.MenuManager = this.ribbon;
            this.picTitulo.Name = "picTitulo";
            this.picTitulo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picTitulo.Properties.Appearance.Options.UseBackColor = true;
            this.picTitulo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picTitulo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picTitulo.Size = new System.Drawing.Size(61, 38);
            this.picTitulo.StyleController = this.layoutControl1;
            this.picTitulo.TabIndex = 6;
            // 
            // gcListaUsuario
            // 
            this.gcListaUsuario.DataSource = this.bsListadoUsuario;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.Append.Enabled = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.CancelEdit.Enabled = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.EndEdit.Enabled = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.First.Enabled = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.First.Visible = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.Last.Enabled = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.Last.Visible = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.Next.Enabled = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.Next.Visible = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.NextPage.Enabled = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.Prev.Enabled = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.Prev.Visible = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.PrevPage.Enabled = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.Remove.Enabled = false;
            this.gcListaUsuario.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListaUsuario.Location = new System.Drawing.Point(66, 49);
            this.gcListaUsuario.MainView = this.gvListaUsuario;
            this.gcListaUsuario.MenuManager = this.ribbon;
            this.gcListaUsuario.Name = "gcListaUsuario";
            this.gcListaUsuario.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rptFecha});
            this.gcListaUsuario.Size = new System.Drawing.Size(1198, 445);
            this.gcListaUsuario.TabIndex = 5;
            this.gcListaUsuario.UseEmbeddedNavigator = true;
            this.gcListaUsuario.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListaUsuario});
            // 
            // bsListadoUsuario
            // 
            this.bsListadoUsuario.DataSource = typeof(BE_Servicios.eUsuario);
            // 
            // gvListaUsuario
            // 
            this.gvListaUsuario.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListaUsuario.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListaUsuario.ColumnPanelRowHeight = 35;
            this.gvListaUsuario.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_usuario,
            this.coldsc_usuario,
            this.colcod_trabajador,
            this.colflg_trabajador,
            this.coldsc_correo,
            this.colflg_activo,
            this.coldsc_perfil,
            this.coldsc_usuario_cambio,
            this.coldsc_usuario_registro,
            this.colnum_dias_cambio_contraseña,
            this.colflg_audita_login,
            this.colflg_noexpira,
            this.colflg_administrador,
            this.colfch_registro,
            this.colfch_cambioclave,
            this.colfch_cambio});
            this.gvListaUsuario.CustomizationFormBounds = new System.Drawing.Rectangle(758, 69, 260, 352);
            this.gvListaUsuario.GridControl = this.gcListaUsuario;
            this.gvListaUsuario.Name = "gvListaUsuario";
            this.gvListaUsuario.OptionsBehavior.Editable = false;
            this.gvListaUsuario.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gvListaUsuario.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListaUsuario.OptionsView.ShowAutoFilterRow = true;
            this.gvListaUsuario.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListaUsuario_RowClick);
            this.gvListaUsuario.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListaUsuario_CustomDrawColumnHeader);
            this.gvListaUsuario.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvListaUsuario_RowCellStyle);
            this.gvListaUsuario.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListaUsuario_RowStyle);
            // 
            // colcod_usuario
            // 
            this.colcod_usuario.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.colcod_usuario.AppearanceHeader.Options.UseForeColor = true;
            this.colcod_usuario.AppearanceHeader.Options.UseTextOptions = true;
            this.colcod_usuario.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_usuario.Caption = "Código";
            this.colcod_usuario.FieldName = "cod_usuario";
            this.colcod_usuario.Name = "colcod_usuario";
            this.colcod_usuario.Visible = true;
            this.colcod_usuario.VisibleIndex = 0;
            this.colcod_usuario.Width = 89;
            // 
            // coldsc_usuario
            // 
            this.coldsc_usuario.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.coldsc_usuario.AppearanceHeader.Options.UseForeColor = true;
            this.coldsc_usuario.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_usuario.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_usuario.Caption = "Usuario";
            this.coldsc_usuario.FieldName = "dsc_usuario";
            this.coldsc_usuario.Name = "coldsc_usuario";
            this.coldsc_usuario.Visible = true;
            this.coldsc_usuario.VisibleIndex = 1;
            this.coldsc_usuario.Width = 290;
            // 
            // colcod_trabajador
            // 
            this.colcod_trabajador.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_trabajador.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_trabajador.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.colcod_trabajador.AppearanceHeader.Options.UseForeColor = true;
            this.colcod_trabajador.AppearanceHeader.Options.UseTextOptions = true;
            this.colcod_trabajador.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_trabajador.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colcod_trabajador.Caption = "Código Planilla";
            this.colcod_trabajador.FieldName = "cod_trabajador";
            this.colcod_trabajador.Name = "colcod_trabajador";
            this.colcod_trabajador.Visible = true;
            this.colcod_trabajador.VisibleIndex = 3;
            this.colcod_trabajador.Width = 83;
            // 
            // colflg_trabajador
            // 
            this.colflg_trabajador.AppearanceCell.Options.UseTextOptions = true;
            this.colflg_trabajador.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_trabajador.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.colflg_trabajador.AppearanceHeader.Options.UseForeColor = true;
            this.colflg_trabajador.AppearanceHeader.Options.UseTextOptions = true;
            this.colflg_trabajador.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_trabajador.Caption = "Trabajador";
            this.colflg_trabajador.FieldName = "flg_trabajador";
            this.colflg_trabajador.Name = "colflg_trabajador";
            this.colflg_trabajador.Visible = true;
            this.colflg_trabajador.VisibleIndex = 4;
            this.colflg_trabajador.Width = 67;
            // 
            // coldsc_correo
            // 
            this.coldsc_correo.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.coldsc_correo.AppearanceHeader.Options.UseForeColor = true;
            this.coldsc_correo.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_correo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_correo.Caption = "Correo";
            this.coldsc_correo.FieldName = "dsc_correo";
            this.coldsc_correo.Name = "coldsc_correo";
            this.coldsc_correo.Visible = true;
            this.coldsc_correo.VisibleIndex = 2;
            this.coldsc_correo.Width = 287;
            // 
            // colflg_activo
            // 
            this.colflg_activo.AppearanceCell.Options.UseTextOptions = true;
            this.colflg_activo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_activo.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.colflg_activo.AppearanceHeader.Options.UseForeColor = true;
            this.colflg_activo.AppearanceHeader.Options.UseTextOptions = true;
            this.colflg_activo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_activo.Caption = "Activo";
            this.colflg_activo.FieldName = "flg_activo";
            this.colflg_activo.Name = "colflg_activo";
            this.colflg_activo.Visible = true;
            this.colflg_activo.VisibleIndex = 5;
            this.colflg_activo.Width = 65;
            // 
            // coldsc_perfil
            // 
            this.coldsc_perfil.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.coldsc_perfil.AppearanceHeader.Options.UseForeColor = true;
            this.coldsc_perfil.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_perfil.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_perfil.Caption = "Perfil";
            this.coldsc_perfil.FieldName = "dsc_perfil";
            this.coldsc_perfil.Name = "coldsc_perfil";
            this.coldsc_perfil.Width = 299;
            // 
            // coldsc_usuario_cambio
            // 
            this.coldsc_usuario_cambio.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.coldsc_usuario_cambio.AppearanceHeader.Options.UseForeColor = true;
            this.coldsc_usuario_cambio.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_usuario_cambio.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_usuario_cambio.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_usuario_cambio.Caption = "Usuario Modificación";
            this.coldsc_usuario_cambio.FieldName = "dsc_usuario_cambio";
            this.coldsc_usuario_cambio.Name = "coldsc_usuario_cambio";
            // 
            // coldsc_usuario_registro
            // 
            this.coldsc_usuario_registro.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.coldsc_usuario_registro.AppearanceHeader.Options.UseForeColor = true;
            this.coldsc_usuario_registro.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_usuario_registro.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_usuario_registro.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_usuario_registro.Caption = "Usuario Registro";
            this.coldsc_usuario_registro.FieldName = "dsc_usuario_registro";
            this.coldsc_usuario_registro.Name = "coldsc_usuario_registro";
            // 
            // colnum_dias_cambio_contraseña
            // 
            this.colnum_dias_cambio_contraseña.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_dias_cambio_contraseña.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_dias_cambio_contraseña.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.colnum_dias_cambio_contraseña.AppearanceHeader.Options.UseForeColor = true;
            this.colnum_dias_cambio_contraseña.AppearanceHeader.Options.UseTextOptions = true;
            this.colnum_dias_cambio_contraseña.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_dias_cambio_contraseña.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colnum_dias_cambio_contraseña.Caption = "Dias a Caducar";
            this.colnum_dias_cambio_contraseña.FieldName = "num_dias_cambio_contraseña";
            this.colnum_dias_cambio_contraseña.Name = "colnum_dias_cambio_contraseña";
            // 
            // colflg_audita_login
            // 
            this.colflg_audita_login.AppearanceCell.Options.UseTextOptions = true;
            this.colflg_audita_login.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_audita_login.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.colflg_audita_login.AppearanceHeader.Options.UseForeColor = true;
            this.colflg_audita_login.AppearanceHeader.Options.UseTextOptions = true;
            this.colflg_audita_login.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_audita_login.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colflg_audita_login.Caption = "Auditoria Login";
            this.colflg_audita_login.FieldName = "flg_audita_login";
            this.colflg_audita_login.Name = "colflg_audita_login";
            // 
            // colflg_noexpira
            // 
            this.colflg_noexpira.AppearanceCell.Options.UseTextOptions = true;
            this.colflg_noexpira.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_noexpira.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.colflg_noexpira.AppearanceHeader.Options.UseForeColor = true;
            this.colflg_noexpira.AppearanceHeader.Options.UseTextOptions = true;
            this.colflg_noexpira.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_noexpira.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colflg_noexpira.Caption = "No Expira";
            this.colflg_noexpira.FieldName = "flg_noexpira";
            this.colflg_noexpira.Name = "colflg_noexpira";
            // 
            // colflg_administrador
            // 
            this.colflg_administrador.AppearanceCell.Options.UseTextOptions = true;
            this.colflg_administrador.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_administrador.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.colflg_administrador.AppearanceHeader.Options.UseForeColor = true;
            this.colflg_administrador.AppearanceHeader.Options.UseTextOptions = true;
            this.colflg_administrador.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_administrador.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colflg_administrador.Caption = "Administrador";
            this.colflg_administrador.FieldName = "flg_administrador";
            this.colflg_administrador.Name = "colflg_administrador";
            // 
            // colfch_registro
            // 
            this.colfch_registro.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_registro.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_registro.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.colfch_registro.AppearanceHeader.Options.UseForeColor = true;
            this.colfch_registro.AppearanceHeader.Options.UseTextOptions = true;
            this.colfch_registro.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_registro.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colfch_registro.Caption = "Fecha Registro";
            this.colfch_registro.ColumnEdit = this.rptFecha;
            this.colfch_registro.FieldName = "fch_registro";
            this.colfch_registro.Name = "colfch_registro";
            // 
            // rptFecha
            // 
            this.rptFecha.AutoHeight = false;
            this.rptFecha.Name = "rptFecha";
            this.rptFecha.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(this.rptFecha_CustomDisplayText);
            // 
            // colfch_cambioclave
            // 
            this.colfch_cambioclave.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_cambioclave.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_cambioclave.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.colfch_cambioclave.AppearanceHeader.Options.UseForeColor = true;
            this.colfch_cambioclave.AppearanceHeader.Options.UseTextOptions = true;
            this.colfch_cambioclave.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_cambioclave.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colfch_cambioclave.Caption = "Fecha Cambio Contraseña";
            this.colfch_cambioclave.ColumnEdit = this.rptFecha;
            this.colfch_cambioclave.FieldName = "fch_cambioclave";
            this.colfch_cambioclave.Name = "colfch_cambioclave";
            // 
            // colfch_cambio
            // 
            this.colfch_cambio.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_cambio.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_cambio.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.colfch_cambio.AppearanceHeader.Options.UseForeColor = true;
            this.colfch_cambio.AppearanceHeader.Options.UseTextOptions = true;
            this.colfch_cambio.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_cambio.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colfch_cambio.Caption = "Fecha Modificación";
            this.colfch_cambio.ColumnEdit = this.rptFecha;
            this.colfch_cambio.FieldName = "fch_cambio";
            this.colfch_cambio.Name = "colfch_cambio";
            // 
            // navBarControl1
            // 
            this.navBarControl1.BackColor = System.Drawing.Color.Transparent;
            this.navBarControl1.LinkSelectionMode = DevExpress.XtraNavBar.LinkSelectionModeType.OneInGroup;
            this.navBarControl1.Location = new System.Drawing.Point(7, 7);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.NavigationPaneGroupClientHeight = 160;
            this.navBarControl1.NavigationPaneMaxVisibleGroups = 5;
            this.navBarControl1.OptionsNavPane.CollapsedWidth = 55;
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 164;
            this.navBarControl1.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Collapsed;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControl1.Size = new System.Drawing.Size(55, 487);
            this.navBarControl1.TabIndex = 1;
            this.navBarControl1.Text = "navBarControl1";
            this.navBarControl1.SelectedLinkChanged += new DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventHandler(this.navBarControl1_SelectedLinkChanged);
            this.navBarControl1.ActiveGroupChanged += new DevExpress.XtraNavBar.NavBarGroupEventHandler(this.navBarControl1_ActiveGroupChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1271, 501);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.navBarControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(59, 491);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcListaUsuario;
            this.layoutControlItem2.Location = new System.Drawing.Point(59, 42);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1202, 449);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.picTitulo;
            this.layoutControlItem3.Location = new System.Drawing.Point(59, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(65, 42);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(65, 42);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(65, 42);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblTitulo;
            this.layoutControlItem4.Location = new System.Drawing.Point(124, 0);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(222, 28);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1137, 42);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // frmListadoUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 683);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.KeyPreview = true;
            this.Name = "frmListadoUsuario";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Listado de Usuarios";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListadoUsuario_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmListadoUsuario_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTitulo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptFecha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.BindingSource bsListadoUsuario;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_usuario;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_trabajador;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_correo;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_activo;
        private DevExpress.XtraEditors.PictureEdit picTitulo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl lblTitulo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        internal DevExpress.XtraGrid.Views.Grid.GridView gvListaUsuario;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_trabajador;
        public DevExpress.XtraGrid.GridControl gcListaUsuario;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_perfil;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario_cambio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario_registro;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_dias_cambio_contraseña;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_audita_login;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_noexpira;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_administrador;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_registro;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_cambioclave;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_cambio;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.BarButtonItem btnNuevo;
        private DevExpress.XtraBars.BarButtonItem btnActivar;
        private DevExpress.XtraBars.BarButtonItem btnInactivar;
        private DevExpress.XtraBars.BarButtonItem btnEliminar;
        private DevExpress.XtraBars.BarButtonItem btnExportarExcel;
        private DevExpress.XtraBars.BarButtonItem btnImprimir;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rptFecha;
    }
}