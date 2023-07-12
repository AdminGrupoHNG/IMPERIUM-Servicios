using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.Utils.Drawing;
using BE_Servicios;
using BL_Servicios;
using DevExpress.Images;
using DevExpress.XtraNavBar;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Grid;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraPrinting;
using DevExpress.Export;

namespace UI_Servicios.Formularios.Sistema.Configuracion_del_Sistema
{
    public partial class frmAsignacionPermiso : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        blSistema blSis = new blSistema();
        blGlobales blGlobal = new blGlobales();
        public eUsuario user = new eUsuario();
        public eGlobales eGlobal = new eGlobales();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;

        public frmAsignacionPermiso()
        {
            InitializeComponent();
        }
        
        private void gvPerfiles_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void frmAsignacionPermiso_Load(object sender, EventArgs e)
        {
            lblTitulo.ForeColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
            CargarOpcionesMenu();
            CargarPerfiles("ACTIVOS", "SI");
            lblTitulo.Text = navBarControl1.SelectedLink.Group.Caption + ": " + "ACTIVOS";
            picTitulo.Image = navBarControl1.SelectedLink.Group.ImageOptions.LargeImage;
            navBarControl1.Groups[0].SelectedLinkIndex = 1;
            //navBarControl1.Groups[0].Expanded = true;
        }
        private void CargarOpcionesMenu()
        {
            List<eVentana> ListVentana = new List<eVentana>();
            ListVentana = blSis.ListarOpcionesMenuPerfil<eVentana>(2);
            Image imgEstadoLarge = ImageResourceCache.Default.GetImage("images/programming/forcetesting_32x32.png");
            Image imgEstadoSmall = ImageResourceCache.Default.GetImage("images/programming/forcetesting_16x16.png");

            NavBarGroup NavEstado = navBarControl1.Groups.Add();
            NavEstado.Caption = "Por Estado"; NavEstado.Expanded = true; NavEstado.SelectedLinkIndex = 1;

            NavEstado.GroupCaptionUseImage = NavBarImage.Large; NavEstado.GroupStyle = NavBarGroupStyle.SmallIconsText;
            NavEstado.ImageOptions.LargeImage = imgEstadoLarge; NavEstado.ImageOptions.SmallImage = imgEstadoSmall;
            //NavTipoProv.ItemChanged += NavCabecera_LinkClicked;

            foreach (eVentana obj in ListVentana)
            {
                NavBarItem NavDetalle = navBarControl1.Items.Add();
                NavDetalle.Tag = obj.cod_menu; NavDetalle.Name = obj.cod_menu;
                NavDetalle.Caption = obj.dsc_menu; NavDetalle.LinkClicked += NavDetalle_LinkClicked;

                NavEstado.ItemLinks.Add(NavDetalle);
            }
        }
        
        private void gvVentana_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }
       
        public void CargarPerfiles(string NombreGrupo, string Codigo)
        {
            string flg_activo = "SI";
            switch (NombreGrupo)
            {
                case "Por Estado": flg_activo = Codigo; break;

            }
            List<ePerfil> ListadoPerfilesAsignados = new List<ePerfil>();
            ListadoPerfilesAsignados = blSis.ListarPerfiles<ePerfil>(5, "", flg_activo:flg_activo);
            bsPerfiles.DataSource = null; bsPerfiles.DataSource = ListadoPerfilesAsignados;
           
        }
        public void CargarListadoVentanas()
        {
            string solucion = eGlobal.dsc_solucion;
            ePerfil obj= gvPerfiles.GetFocusedRow() as ePerfil;
            if(obj!=null)
            {
                var r = gvVentana.FocusedRowHandle;
                List<eVentana> ListadoVentanas = new List<eVentana>();
                ListadoVentanas = blSis.ListarVentanas<eVentana>(3, obj.cod_perfil, solucion: solucion);
                bsVentana.DataSource = null; bsVentana.DataSource = ListadoVentanas;
                gvVentana.FocusedRowHandle = r;
            }
            
        }
        private void gvPerfiles_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CargarListadoVentanas();
           
        }

        private void gvVentana_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            eVentana obj = gvVentana.GetRow(gvVentana.FocusedRowHandle) as eVentana;
            obj.flg_escritura = gvVentana.GetRowCellValue(gvVentana.FocusedRowHandle, "flg_escritura") == null ? false : Convert.ToBoolean(gvVentana.GetRowCellValue(gvVentana.FocusedRowHandle, "flg_escritura"));
            obj.flg_lectura = gvVentana.GetRowCellValue(gvVentana.FocusedRowHandle, "flg_lectura") == null ? false : Convert.ToBoolean(gvVentana.GetRowCellValue(gvVentana.FocusedRowHandle, "flg_lectura"));
        }

        private void rchkLectura_CheckedChanged(object sender, EventArgs e)
        {
            gvVentana.PostEditor();
            eVentana obj = gvVentana.GetRow(gvVentana.FocusedRowHandle) as eVentana;
            if (!Convert.ToBoolean(obj.flg_lectura)) {
                gvVentana.SetRowCellValue(gvVentana.FocusedRowHandle, "flg_escritura", false);
            }

            ePerfil obj1 = gvPerfiles.GetFocusedRow() as ePerfil;

            obj.cod_perfil = obj1.cod_perfil;
            blSis.Guardar_Perfil_Ventana(obj, user.cod_usuario);

        }

        private void rchkEscritura_CheckedChanged(object sender, EventArgs e)
        {
            gvVentana.PostEditor();
            eVentana obj = gvVentana.GetRow(gvVentana.FocusedRowHandle) as eVentana;
            if (Convert.ToBoolean(obj.flg_escritura))
            {
                gvVentana.SetRowCellValue(gvVentana.FocusedRowHandle, "flg_lectura", true);
            }

            ePerfil obj1 = gvPerfiles.GetFocusedRow() as ePerfil;

            obj.cod_perfil = obj1.cod_perfil;
            blSis.Guardar_Perfil_Ventana(obj, user.cod_usuario);
        }

        private void gvPerfiles_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    ePerfil obj = gvPerfiles.GetFocusedRow() as ePerfil;

                    frmMantPerfil frm = new frmMantPerfil(this);
                    frm.user = user;
                    frm.cod_perfil = obj.cod_perfil;
                    frm.MiAccion = frmMantPerfil.Perfil.Editar;
                    NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
                    frm.GrupoSeleccionado = navGrupo.Caption;
                    frm.ItemSeleccionado = navGrupo.SelectedLink.Item.Tag.ToString();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMantPerfil frm = new frmMantPerfil(this);
            NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
            frm.user = user;
            frm.eGlobal = eGlobal;
            frm.MiAccion = frmMantPerfil.Perfil.Nuevo;
            frm.GrupoSeleccionado = navGrupo.Caption;
            frm.ItemSeleccionado = navGrupo.SelectedLink.Item.Tag.ToString();
            frm.ShowDialog();
        }

        private void navBarControl1_ActiveGroupChanged(object sender, NavBarGroupEventArgs e)
        {
            e.Group.SelectedLinkIndex = 0;
            if (e.Group.SelectedLink != null) ActiveGroupChanged(e.Group.Caption + ": " + e.Group.SelectedLink.Item.Caption, e.Group.ImageOptions.LargeImage);
            if (e.Group.SelectedLink != null)
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                CargarPerfiles(e.Group.Caption, e.Group.SelectedLink.Item.Tag.ToString());
                SplashScreenManager.CloseForm();
            }
        }

        void ActiveGroupChanged(string caption, Image imagen)
        {
            lblTitulo.Text = caption; picTitulo.Image = imagen;
        }


        private void NavDetalle_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            lblTitulo.Text = e.Link.Group.Caption + ": " + e.Link.Caption; picTitulo.Image = e.Link.Group.ImageOptions.LargeImage;
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
            CargarPerfiles(e.Link.Group.Caption, e.Link.Item.Tag.ToString());
            SplashScreenManager.CloseForm();
        }

        internal void frmAsignacionPermiso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
                CargarPerfiles(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
                SplashScreenManager.CloseForm();
            }
        }

        private void gvPerfiles_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView view = sender as GridView;
                    string campo = e.Column.FieldName;
                    if (view.GetRowCellValue(e.RowHandle, "flg_activo").ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnActivar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                ePerfil ePer = gvPerfiles.GetFocusedRow() as ePerfil;
                if (ePer != null)
                {
                    DialogResult msgresult = MessageBox.Show("¿Está seguro de Activar el perfil " + ePer.dsc_perfil + "?", "Activar Perfil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msgresult == DialogResult.Yes)
                    {
                        ePer.flg_activo = "SI";
                        blSis.Guardar_Actualizar_Perfil<ePerfil>(1, ePer, "Actualizar", user.cod_usuario);

                        NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
                        if (navGrupo.SelectedLink.Item.Tag.ToString() == "ALL")
                        {
                            gvPerfiles.SetRowCellValue(gvVentana.FocusedRowHandle, "flg_activo", "SI");
                        }
                        else
                        {
                            CargarPerfiles(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
                        }




                    }
                }
                else
                {
                    MessageBox.Show("No hay perfil seleccionado", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnInactivar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                ePerfil ePer = gvPerfiles.GetFocusedRow() as ePerfil;
                if (ePer != null)
                {
                    DialogResult msgresult = MessageBox.Show("¿Está seguro de Inactivar el perfil " + ePer.dsc_perfil + "?", "Activar Perfil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msgresult == DialogResult.Yes)
                    {
                        ePer.flg_activo = "NO";
                        blSis.Guardar_Actualizar_Perfil<ePerfil>(1, ePer, "Actualizar", user.cod_usuario);

                        NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
                        if (navGrupo.SelectedLink.Item.Tag.ToString() == "ALL")
                        {
                            gvPerfiles.SetRowCellValue(gvVentana.FocusedRowHandle, "flg_activo", "NO");
                        }
                        else
                        {
                            CargarPerfiles(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
                        }




                    }
                }
                else
                {
                    MessageBox.Show("No hay perfil seleccionado", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvVentana_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvPerfiles_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void btnEliminar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                ePerfil ePer = gvPerfiles.GetFocusedRow() as ePerfil;
                if (ePer != null)
                {

                    DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar el perfil " + ePer.dsc_perfil + "?", "Eliminar Perfil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msgresult == DialogResult.Yes)
                    {

                        ePer = blSis.Eliminar_Perfil<ePerfil>(1, ePer.cod_perfil);
                        if (ePer.cod_perfil == 0) { MessageBox.Show("Acción no permitida. El perfil esta asignada a algunos usuarios en 'Mantenimiento de Usuarios'", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                        else if (ePer.cod_scfvi_perfil ==0) { MessageBox.Show("Acción no permitida. El perfil esta configurada como escritura y lectura en el formulario 'Asignar Permisos'", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                        else
                        {
                            gvPerfiles.DeleteRow(gvPerfiles.FocusedRowHandle);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No hay ventana seleccionada", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<eVentana> ListadoVentanas = new List<eVentana>();
            ListadoVentanas = blSis.ListarVentanas<eVentana>(4, 0);
            bsPivot.DataSource = null; bsPivot.DataSource = ListadoVentanas;

            try
            {
               
                //GridView view = gcPaxs.MainView as GridView;    
                string ruta = ConfigurationManager.AppSettings["RutaArchivosLocalExportar"].ToString() + "\\Permisos" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(ConfigurationManager.AppSettings["RutaArchivosLocalExportar"].ToString())) Directory.CreateDirectory(ConfigurationManager.AppSettings["RutaArchivosLocalExportar"].ToString());


                //view.ExportToXlsx(ruta);

                //pivotGridControl1.OptionsPrint.AutoWidth = AutoSize;
                //pivotGridControl1.OptionsPrint.ShowPrintExportProgress = true;
                //pivotGridControl1.OptionsPrint.AllowCancelPrintExport = true;

                //this.layoutReporteExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                XlsxExportOptions options = new XlsxExportOptions();
                options.TextExportMode = TextExportMode.Text;
                options.ExportMode = XlsxExportMode.SingleFile;





                ExportSettings.DefaultExportType = ExportType.WYSIWYG;
                pivotGridControl1.ExportToXlsx(ruta);

                this.layoutReporteExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (MessageBox.Show("Excel exportado en la ruta " + ruta + Environment.NewLine + "¿Desea abrir el archivo?", "Exportar Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(ruta);
                }


               


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void navBarControl1_Click(object sender, EventArgs e)
        {

        }
    }

}