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
using System.Configuration;
using System.IO;
using System.Diagnostics;
using BE_Servicios;
using BL_Servicios;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Images;
using DevExpress.XtraNavBar;

namespace UI_Servicios.Formularios.Sistema.Configuracion_del_Sistema
{
    public partial class frmOpcionesSistema : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        blSistema blSis = new blSistema();
        public eUsuario user = new eUsuario();
        public eGlobales eGlobal = new eGlobales();
        blEncrypta blEncryp = new blEncrypta();
        blGlobales blGlobal = new blGlobales();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;

        public frmOpcionesSistema()
        {
            InitializeComponent();
        }
        private void frmOpcionesSistema_Load(object sender, EventArgs e)
        {
            lblTitulo.ForeColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
            CargarOpcionesMenu();
            CargarListadoVentanas("ACTIVOS","SI");
            lblTitulo.Text = navBarControl1.SelectedLink.Group.Caption + ": " + "ACTIVOS";
            picTitulo.Image = navBarControl1.SelectedLink.Group.ImageOptions.LargeImage;
            navBarControl1.Groups[0].SelectedLinkIndex = 1;
        }

        private void CargarOpcionesMenu()
        {
            List<eVentana> ListVentana = new List<eVentana>();
            ListVentana = blSis.ListarOpcionesMenuVentana<eVentana>(2);
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

        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportarExcel();
        }

        private void ExportarExcel()
        {
            try
            {
                string carpeta = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\Ventanas" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                gvVentana.ExportToXlsx(archivo);
                if (MessageBox.Show("Excel exportado en la ruta " + archivo + Environment.NewLine + "¿Desea abrir el archivo?", "Exportar Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(archivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvVentana.ShowPrintPreview();
        }

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMantVentana frm = new frmMantVentana(this);
            NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
            frm.user = user;
            frm.eGlobal = eGlobal;
            frm.MiAccion = frmMantVentana.Ventana.Nuevo;
            frm.GrupoSeleccionado = navGrupo.Caption;
            frm.ItemSeleccionado = navGrupo.SelectedLink.Item.Tag.ToString();
            frm.ShowDialog();
            //CargarListadoVentanas(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());

        }

        internal void CargarListadoVentanas(string NombreGrupo,string Codigo) {
            string flg_activo = "SI";
            string solucion = eGlobal.dsc_solucion;
            switch (NombreGrupo)
            {
                case "Por Estado": flg_activo = Codigo; break;

            }
            List<eVentana> ListadoVentanas = new List<eVentana>();
            ListadoVentanas = blSis.ListarVentanas<eVentana>(1, flg_activo:flg_activo, solucion: solucion);
            bsVentana.DataSource = null; bsVentana.DataSource = ListadoVentanas;
        }

        private void gvVentana_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eVentana obj = gvVentana.GetFocusedRow() as eVentana;

                    frmMantVentana frm = new frmMantVentana(this);
                    frm.user = user;
                    frm.cod_ventana = obj.cod_ventana;
                    frm.MiAccion = frmMantVentana.Ventana.Editar;
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

        private void gvVentana_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
                CargarListadoVentanas(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
                SplashScreenManager.CloseForm();
            }
        }

        internal void frmOpcionesSistema_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
                CargarListadoVentanas(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
                SplashScreenManager.CloseForm();
            }
        }

        private void gvVentana_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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

        private void btnActivo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eVentana eVen = gvVentana.GetFocusedRow() as eVentana;
                if (eVen != null)
                {
                    DialogResult msgresult = MessageBox.Show("¿Está seguro de Activar esta ventana " + eVen.dsc_ventana + "?", "Activar Ventana", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msgresult == DialogResult.Yes)
                    {
                        eVen.flg_activo = "SI";
                        blSis.Guardar_Actualizar_Ventana<eVentana>(1, eVen, "Actualizar", user.cod_usuario);

                        NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
                        if (navGrupo.SelectedLink.Item.Tag.ToString() == "ALL")
                        {
                            gvVentana.SetRowCellValue(gvVentana.FocusedRowHandle, "flg_activo", "SI");
                        }
                        else {
                            CargarListadoVentanas(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
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

        private void btnInactivo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eVentana eVen = gvVentana.GetFocusedRow() as eVentana;
                if (eVen != null)
                {
                    DialogResult msgresult = MessageBox.Show("¿Está seguro de Inactivar esta ventana " + eVen.dsc_ventana + "?", "Activar Ventana", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msgresult == DialogResult.Yes)
                    {
                        eVen.flg_activo = "NO";
                        blSis.Guardar_Actualizar_Ventana<eVentana>(1, eVen, "Actualizar", user.cod_usuario);

                        NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
                        if (navGrupo.SelectedLink.Item.Tag.ToString() == "ALL")
                        {
                            gvVentana.SetRowCellValue(gvVentana.FocusedRowHandle, "flg_activo", "NO");
                        }
                        else
                        {
                            CargarListadoVentanas(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
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

        private void btnEliminar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eVentana eVen = gvVentana.GetFocusedRow() as eVentana;
                if (eVen != null)
                {
                    
                    DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar la ventana  " + eVen.dsc_ventana + "?", "Eliminar Ventana", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msgresult == DialogResult.Yes)
                    {
                        
                        eVen=blSis.Eliminar_Ventana<eVentana>(1, eVen.cod_ventana);
                        if (eVen.cod_ventana == 0) { MessageBox.Show("Acción no permitida. La ventana esta configurada como escritura y lectura en el formulario 'Asignar Permisos'", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                        else {
                        gvVentana.DeleteRow(gvVentana.FocusedRowHandle);
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

       
        private void navBarControl1_ActiveGroupChanged(object sender, NavBarGroupEventArgs e)
        {
            e.Group.SelectedLinkIndex = 0;
            if (e.Group.SelectedLink != null) ActiveGroupChanged(e.Group.Caption + ": " + e.Group.SelectedLink.Item.Caption, e.Group.ImageOptions.LargeImage);
            if (e.Group.SelectedLink != null)
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                CargarListadoVentanas(e.Group.Caption, e.Group.SelectedLink.Item.Tag.ToString());
                SplashScreenManager.CloseForm();
            }
        }

        private void gvVentana_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        void ActiveGroupChanged(string caption, Image imagen)
        {
            lblTitulo.Text = caption; picTitulo.Image = imagen;
        }
        private void NavDetalle_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            lblTitulo.Text = e.Link.Group.Caption + ": " + e.Link.Caption; picTitulo.Image = e.Link.Group.ImageOptions.LargeImage;
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
            CargarListadoVentanas(e.Link.Group.Caption, e.Link.Item.Tag.ToString()); 
            SplashScreenManager.CloseForm();
        }
        private void navBarControl1_SelectedLinkChanged(object sender, DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventArgs e)
        {
           // e.Group.SelectedLinkIndex = 0;
        }

       

       
    }
}