using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE_Servicios;
using BL_Servicios;
using DevExpress.XtraNavBar;
using DevExpress.XtraSplashScreen;
using DevExpress.Images;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraReports.UI;
using Excel = Microsoft.Office.Interop.Excel;

namespace UI_Servicios.Formularios.Logistica
{
    internal enum InventarioAlmacen
    {
        Nuevo = 0,
        Editar = 1
    }
    public partial class frmListaInventarioAlmacen : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public eUsuario user = new eUsuario();
        internal InventarioAlmacen MiAccion = InventarioAlmacen.Nuevo;
        blEncrypta blEncryp = new blEncrypta();
        blSistema blSist = new blSistema();
        blTrabajador blTrab = new blTrabajador();
        blProveedores blProv = new blProveedores();
        blLogistica blLogis = new blLogistica();
        blGlobales blGlobal = new blGlobales();
        blRequerimiento blReq = new blRequerimiento();
        blOrdenCompra_Servicio blOrd = new blOrdenCompra_Servicio();
        List<eProductos.eProductosProveedor> lista = new List<eProductos.eProductosProveedor>();
        List<eAlmacen.eEntrada_Cabecera> listaEntradas = new List<eAlmacen.eEntrada_Cabecera>();
        List<eAlmacen.eSalida_Cabecera> listaSalidas = new List<eAlmacen.eSalida_Cabecera>();
        List<eAlmacen.eGuiaRemision_Cabecera> listaGuias = new List<eAlmacen.eGuiaRemision_Cabecera>();
        List<eRequerimiento> listReqAprobados = new List<eRequerimiento>();
        List<eOrdenCompra_Servicio> listOrdenesEnviadas = new List<eOrdenCompra_Servicio>();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        Brush ConCriterios = Brushes.Green;
        Brush ParcialCriterios = Brushes.Orange;
        Brush SinCriterios = Brushes.Red;
        int markWidth = 16;
        string cod_empresa = "";
        bool Buscar = false;

        public frmListaInventarioAlmacen()
        {
            InitializeComponent();
        }

        private void frmListaInventarioAlmacen_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            CargarOpcionesMenu();
            //CargarListado("TODOS", "");
            lblTitulo.ForeColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
            lblTitulo.Text = navBarControl1.SelectedLink.Group.Caption + ": " + navBarControl1.SelectedLink.Item.Caption;
            picTitulo.Image = navBarControl1.SelectedLink.Group.ImageOptions.LargeImage;
            navBarControl1.Groups[0].SelectedLinkIndex = 0;
            Buscar = true;
            navBarControl1.SelectedLink = navBarControl1.Groups[0].ItemLinks[0];
            NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
            CargarListado(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
            //Fecha
            DateTime date = DateTime.Now;
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            dtFechaInicio.EditValue = new DateTime(date.Year, 1, 1);
            dtFechaFin.EditValue = oUltimoDiaDelMes;
            HabilitarBotones();

            switch (MiAccion)
            {
                case InventarioAlmacen.Nuevo:
                    break;
                case InventarioAlmacen.Editar:
                    break;
            }
        }

        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = blSist.ListarMenuxUsuario<eVentana>(user.cod_usuario, this.Name);
            if (listPermisos.Count > 0)
            {
                grupoAcciones.Enabled = listPermisos[0].flg_escritura;
            }
        }

        internal void CargarOpcionesMenu()
        {
            List<eProveedor_Empresas> listEmpresas = blProv.ListarOpcionesMenu<eProveedor_Empresas>(12);
            Image imgEmpresaLarge = ImageResourceCache.Default.GetImage("images/navigation/home_32x32.png");
            Image imgEmpresaSmall = ImageResourceCache.Default.GetImage("images/navigation/home_16x16.png");

            NavBarGroup NavEmpresa = navBarControl1.Groups.Add();
            NavEmpresa.Name = "Por Empresa";
            NavEmpresa.Caption = "Por Empresa"; NavEmpresa.Expanded = true; NavEmpresa.SelectedLinkIndex = 0;
            NavEmpresa.GroupCaptionUseImage = NavBarImage.Large; NavEmpresa.GroupStyle = NavBarGroupStyle.SmallIconsText;
            NavEmpresa.ImageOptions.LargeImage = imgEmpresaLarge; NavEmpresa.ImageOptions.SmallImage = imgEmpresaSmall;

            List<eProveedor_Empresas> listEmpresasUsuario = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(11, "", user.cod_usuario);
            if (listEmpresasUsuario.Count == 0) { MessageBox.Show("Debe tener una empresa asignada para visualizar los datos", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            List<eProveedor_Empresas> listadoEmp = new List<eProveedor_Empresas>();
            eProveedor_Empresas objEmp = new eProveedor_Empresas();
            //objEmp = listEmpresas.Find(x => x.cod_empresa == "ALL");
            //listadoEmp.Add(objEmp);
            if (listEmpresas.Count > 0)
            {
                foreach (eProveedor_Empresas obj2 in listEmpresasUsuario)
                {
                    objEmp = listEmpresas.Find(x => x.cod_empresa == obj2.cod_empresa);
                    if (objEmp != null) listadoEmp.Add(objEmp);
                }
            }

            foreach (eProveedor_Empresas obj in listadoEmp)
            {
                NavBarItem NavDetalle = navBarControl1.Items.Add();
                NavDetalle.Tag = obj.cod_empresa; NavDetalle.Name = obj.cod_empresa;
                NavDetalle.Caption = obj.dsc_empresa; NavDetalle.LinkClicked += NavDetalle_LinkClicked;

                NavEmpresa.ItemLinks.Add(NavDetalle);
            }
        }

        private void NavDetalle_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            lblTitulo.Text = e.Link.Group.Caption + ": " + e.Link.Caption; picTitulo.Image = e.Link.Group.ImageOptions.LargeImage;
            CargarListado(e.Link.Group.Caption, e.Link.Item.Tag.ToString());
        }

        public void CargarListado(string NombreGrupo, string Codigo)
        {
            try
            {
                switch (NombreGrupo)
                {
                    case "Por Empresa": cod_empresa = Codigo; break;
                }

                blTrab.CargaCombosLookUp("SedesEmpresa", lkpSedeEmpresa, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa);
                lkpSedeEmpresa.EditValue = null; lkpAlmacen.EditValue = null;
                List<eTrabajador.eInfoLaboral_Trabajador> lista = blTrab.ListarOpcionesTrabajador<eTrabajador.eInfoLaboral_Trabajador>(6, cod_empresa);
                if (lista.Count >= 1) lkpSedeEmpresa.EditValue = lista[0].cod_sede_empresa;

                btnBuscar_Click(btnBuscar, new EventArgs());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
                
        private void navBarControl1_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            e.Group.SelectedLinkIndex = 0;
            navBarControl1_SelectedLinkChanged(navBarControl1, new DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventArgs(e.Group, e.Group.SelectedLink));
        }

        void ActiveGroupChanged(string caption, Image imagen)
        {
            lblTitulo.Text = caption; picTitulo.Image = imagen;
        }

        private void navBarControl1_SelectedLinkChanged(object sender, DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventArgs e)
        {
            //e.Group.SelectedLinkIndex = 0;
            if (!Buscar) e.Group.SelectedLinkIndex = 0;
            if (e.Group.SelectedLink != null && Buscar)
            {
                ActiveGroupChanged(e.Group.Caption + ": " + e.Group.SelectedLink.Item.Caption, e.Group.ImageOptions.LargeImage);
                //blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                CargarListado(e.Group.Caption, e.Group.SelectedLink.Item.Tag.ToString());
                //SplashScreenManager.CloseForm();
            }
        }

        private void btnCrearAlmacen_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMantAlmacen frm = new frmMantAlmacen();
            frm.user = user;
            frm.cod_empresa = cod_empresa;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.ShowDialog();
            if (frm.ActualizarListado) lkpSedeEmpresa_EditValueChanged(lkpSedeEmpresa, new EventArgs());
        }

        private void btnRegistrarEntrada_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
                if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
                eOrdenCompra_Servicio obj = null;
                if (xtcInventarioAlmacen.SelectedTabPage == xtabOrdenesCompra) obj = gvOrdEnviadas.GetFocusedRow() as eOrdenCompra_Servicio;
                if(obj != null && obj.ctd_Atencion == 2)
                {
                    MessageBox.Show("La OC ya se encuentra ATENDIDA", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); lkpAlmacen.Focus(); return;
                }

                frmRegistrarEntradaAlmacen frm = new frmRegistrarEntradaAlmacen();
                frm.MiAccion = IngresoAlmacen.Nuevo;
                frm.user = user;
                frm.cod_empresa = cod_empresa;
                frm.cod_sede_empresa = lkpSedeEmpresa.EditValue.ToString();
                frm.cod_almacen = lkpAlmacen.EditValue.ToString();
                frm.cod_orden_compra_servicio = obj == null ? "" : obj.cod_orden_compra_servicio;
                frm.flg_solicitud = obj == null ? "" : obj.flg_solicitud;
                frm.dsc_anho = obj == null ? "" : obj.dsc_anho.ToString();
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.ShowDialog();
                if (frm.ActualizarListado)
                {
                    blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    btnBuscar_Click(btnBuscar, new EventArgs());
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRegistrarSalida_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
                if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
                eRequerimiento obj = null;
                if (xtcInventarioAlmacen.SelectedTabPage == xtabRequerimientos) obj = gvReqAprobados.GetFocusedRow() as eRequerimiento;

                frmRegistrarSalidaAlmacen frm = new frmRegistrarSalidaAlmacen();
                frm.user = user;
                frm.cod_empresa = cod_empresa;
                frm.cod_sede_empresa = lkpSedeEmpresa.EditValue.ToString();
                frm.cod_almacen = lkpAlmacen.EditValue.ToString();
                frm.cod_empresa = cod_empresa;
                frm.cod_requerimiento = obj == null ? "" : obj.cod_requerimiento;
                frm.flg_solicitud = obj == null ? "" : obj.flg_solicitud;
                frm.dsc_anho = obj == null ? "" : obj.dsc_anho.ToString();
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.ShowDialog();
                if (frm.ActualizarListado)
                {
                    blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    btnBuscar_Click(btnBuscar, new EventArgs());
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRegistrarGuiaRemision_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
                if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
                eRequerimiento obj = null;
                if (xtcInventarioAlmacen.SelectedTabPage == xtabRequerimientos) obj = gvReqAprobados.GetFocusedRow() as eRequerimiento;

                frmRegistrarGuiaRemisionAlmacen frm = new frmRegistrarGuiaRemisionAlmacen();
                frm.user = user;
                frm.cod_empresa = cod_empresa;
                frm.cod_sede_empresa = lkpSedeEmpresa.EditValue.ToString();
                frm.cod_almacen = lkpAlmacen.EditValue.ToString();
                frm.cod_empresa = cod_empresa;
                frm.cod_requerimiento = obj == null ? "" : obj.cod_requerimiento;
                frm.flg_solicitud = obj == null ? "" : obj.flg_solicitud;
                frm.dsc_anho = obj == null ? "" : obj.dsc_anho.ToString();
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.ShowDialog();
                if (frm.ActualizarListado)
                {
                    blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    btnBuscar_Click(btnBuscar, new EventArgs());
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lkpSedeEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpSedeEmpresa.EditValue != null)
            {
                blLogis.CargaCombosLookUp("Almacen", lkpAlmacen, "cod_almacen", "dsc_almacen", "", valorDefecto: true, cod_empresa: cod_empresa, cod_sede_empresa: lkpSedeEmpresa.EditValue.ToString());
                List<eAlmacen> lista = blLogis.Obtener_ListaVariasLogistica<eAlmacen>(13, cod_empresa: cod_empresa, cod_sede_empresa: lkpSedeEmpresa.EditValue.ToString());
                if (lista.Count >= 1) lkpAlmacen.EditValue = lista[0].cod_almacen;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
                if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }

                lista = blLogis.Obtener_ListaLogistica<eProductos.eProductosProveedor>(15, lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(),
                                                                                      FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                                      FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));
                bsListadoProductos.DataSource = lista; gvListadoProductos.RefreshData();

                listaEntradas = blLogis.Obtener_ListaLogistica<eAlmacen.eEntrada_Cabecera>(19, lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(),
                                                                                      FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                                      FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));
                bsListadoEntradas.DataSource = listaEntradas; gvListadoEntradas.RefreshData();

                listaSalidas = blLogis.Obtener_ListaLogistica<eAlmacen.eSalida_Cabecera>(22, lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(),
                                                                                      FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                                      FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));
                bsListadoSalidas.DataSource = listaSalidas; gvListadoSalidas.RefreshData();

                listaGuias = blLogis.Obtener_ListaLogistica<eAlmacen.eGuiaRemision_Cabecera>(24, lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(),
                                                                                      FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                                      FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));
                bsListadoSalidasGuiaRemision.DataSource = listaGuias; gvListadoSalidasGuiaRemision.RefreshData();

                listReqAprobados = blReq.ListarRequerimiento<eRequerimiento>(3, cod_empresa, lkpSedeEmpresa.EditValue == null ? "" : lkpSedeEmpresa.EditValue.ToString(),
                                                                            "", "", "01", Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                            Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));
                bsListadoReqAprobados.DataSource = listReqAprobados;

                //listOrdenesEnviadas = blOrd.ListarOrdenesCompra<eOrdenCompra>(7, cod_empresa, lkpSedeEmpresa.EditValue == null ? "" : lkpSedeEmpresa.EditValue.ToString(),
                //                                                            "", "01", Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                //                                                            Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));
                listOrdenesEnviadas = blLogis.Obtener_ListaLogistica<eOrdenCompra_Servicio>(30, lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(),
                                                                                FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                                FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));

                bsListadoOrdEnviadas.DataSource = listOrdenesEnviadas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoEntradas_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2)
                {
                    eAlmacen.eEntrada_Cabecera obj = gvListadoEntradas.GetFocusedRow() as eAlmacen.eEntrada_Cabecera;

                    frmRegistrarEntradaAlmacen frm = new frmRegistrarEntradaAlmacen();
                    frm.MiAccion = IngresoAlmacen.Editar;
                    frm.user = user;
                    frm.cod_empresa = cod_empresa;
                    frm.cod_sede_empresa = lkpSedeEmpresa.EditValue.ToString();
                    frm.cod_almacen = lkpAlmacen.EditValue.ToString();
                    frm.cod_entrada = obj.cod_entrada;
                    frm.dsc_anho = "";
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorEventRow = colorEventRow;
                    frm.colorFocus = colorFocus;
                    frm.ShowDialog();
                    if (frm.ActualizarListado)
                    {
                        blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                        btnBuscar_Click(btnBuscar, new EventArgs());
                        SplashScreenManager.CloseForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoSalidas_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2)
                {
                    eAlmacen.eSalida_Cabecera obj = gvListadoSalidas.GetFocusedRow() as eAlmacen.eSalida_Cabecera;

                    frmRegistrarSalidaAlmacen frm = new frmRegistrarSalidaAlmacen();
                    frm.MiAccion = SalidaAlmacen.Vista;
                    frm.user = user;
                    frm.cod_empresa = cod_empresa;
                    frm.cod_sede_empresa = lkpSedeEmpresa.EditValue.ToString();
                    frm.cod_almacen = lkpAlmacen.EditValue.ToString();
                    frm.cod_salida = obj.cod_salida;
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorEventRow = colorEventRow;
                    frm.colorFocus = colorFocus;
                    frm.ShowDialog();
                    if (frm.ActualizarListado)
                    {
                        blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                        btnBuscar_Click(btnBuscar, new EventArgs());
                        SplashScreenManager.CloseForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoSalidasGuiaRemision_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2)
                {
                    eAlmacen.eGuiaRemision_Cabecera obj = gvListadoSalidasGuiaRemision.GetFocusedRow() as eAlmacen.eGuiaRemision_Cabecera;

                    frmRegistrarGuiaRemisionAlmacen frm = new frmRegistrarGuiaRemisionAlmacen();
                    frm.MiAccion = GuiaRemision.Vista;
                    frm.user = user;
                    frm.cod_empresa = cod_empresa;
                    frm.cod_sede_empresa = lkpSedeEmpresa.EditValue.ToString();
                    frm.cod_almacen = lkpAlmacen.EditValue.ToString();
                    frm.cod_guiaremision = obj.cod_guiaremision;
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorEventRow = colorEventRow;
                    frm.colorFocus = colorFocus;
                    frm.ShowDialog();
                    if (frm.ActualizarListado)
                    {
                        blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                        btnBuscar_Click(btnBuscar, new EventArgs());
                        SplashScreenManager.CloseForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmListaInventarioAlmacen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                btnBuscar_Click(btnBuscar, new EventArgs());
                SplashScreenManager.CloseForm();
            }
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
                string archivo = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\InventarioAlmacen" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                if (xtcInventarioAlmacen.SelectedTabPage == xtabListadoProductos) gvListadoProductos.ExportToXlsx(archivo);
                if (xtcInventarioAlmacen.SelectedTabPage == xtabListadoEntradas) gvListadoEntradas.ExportToXlsx(archivo);
                if (xtcInventarioAlmacen.SelectedTabPage == xtabListadoSalidas) gvListadoSalidas.ExportToXlsx(archivo);
                if (xtcInventarioAlmacen.SelectedTabPage == xtabListadoSalidasGuiaRemision) gvListadoSalidasGuiaRemision.ExportToXlsx(archivo);
                gvListadoProductos.ExportToXlsx(archivo);
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

        private void gvListadoProductos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoProductos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoEntradas_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoEntradas_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoSalidas_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void btnGenerarNotaIngreso_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (xtcInventarioAlmacen.SelectedTabPage != xtabListadoEntradas) return;
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                eAlmacen.eEntrada_Cabecera eProv = gvListadoEntradas.GetFocusedRow() as eAlmacen.eEntrada_Cabecera;
                if (eProv == null) { MessageBox.Show("Debe seleccionar un registro de ingreso.", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                rptNotaIngreso report = new rptNotaIngreso();
                ReportPrintTool printTool = new ReportPrintTool(report);
                report.RequestParameters = false;
                printTool.AutoShowParametersPanel = false;
                report.Parameters["cod_entrada"].Value = eProv.cod_entrada;
                report.Parameters["cod_almacen"].Value = eProv.cod_almacen;
                report.Parameters["cod_empresa"].Value = eProv.cod_empresa;
                report.Parameters["cod_sede_empresa"].Value = eProv.cod_sede_empresa;
                report.Parameters["cod_proveedor"].Value = eProv.cod_proveedor;

                if (eProv.cod_empresa == "00001") { report.xpb_logo.Image = Properties.Resources.Logo_HNG1; report.lblref.BackColor = Color.FromArgb(63, 63, 65); report.tblcuadro.BackColor = Color.FromArgb(63, 63, 65); report.lblglosa.BackColor = Color.FromArgb(63, 63, 65); }
                if (eProv.cod_empresa == "00002") { report.xpb_logo.Image = Properties.Resources.logo_facilita; report.lblref.BackColor = Color.FromArgb(12, 63, 104); report.tblcuadro.BackColor = Color.FromArgb(12, 63, 104); report.lblglosa.BackColor = Color.FromArgb(12, 63, 104); }
                if (eProv.cod_empresa == "00003") { report.xpb_logo.Image = Properties.Resources.Logo_HNG1; }
                if (eProv.cod_empresa == "00004") { report.xpb_logo.Image = Properties.Resources.logo_k2; report.lblref.BackColor = Color.FromArgb(0, 157, 150); report.tblcuadro.BackColor = Color.FromArgb(0, 157, 150); report.lblglosa.BackColor = Color.FromArgb(0, 157, 150); }
                if (eProv.cod_empresa == "00005") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00006") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00007") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00008") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00009") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00010") { report.xpb_logo.Image = Properties.Resources.add_32x32; }

                printTool.ShowPreviewDialog();
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGenerarNotaSalida_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (xtcInventarioAlmacen.SelectedTabPage != xtabListadoSalidas) return;
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                eAlmacen.eSalida_Cabecera eProv = gvListadoSalidas.GetFocusedRow() as eAlmacen.eSalida_Cabecera;
                if (eProv == null) { MessageBox.Show("Debe seleccionar un registro de saldia.", "Nota de Salida", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                rptNotaSalida report = new rptNotaSalida();
                ReportPrintTool printTool = new ReportPrintTool(report);
                report.RequestParameters = false;
                printTool.AutoShowParametersPanel = false;
                report.Parameters["cod_salida"].Value = eProv.cod_salida;
                report.Parameters["cod_almacen"].Value = eProv.cod_almacen;
                report.Parameters["cod_empresa"].Value = eProv.cod_empresa;
                report.Parameters["cod_sede_empresa"].Value = eProv.cod_sede_empresa;
                //report.Parameters["cod_proveedor"].Value = eProv.cod_proveedor;

                if (eProv.cod_empresa == "00001") { report.xpb_logo.Image = Properties.Resources.Logo_HNG1; report.lblref.BackColor = Color.FromArgb(63, 63, 65); report.tblcuadro.BackColor = Color.FromArgb(63, 63, 65); report.lblglosa.BackColor = Color.FromArgb(63, 63, 65); }
                if (eProv.cod_empresa == "00002") { report.xpb_logo.Image = Properties.Resources.logo_facilita; report.lblref.BackColor = Color.FromArgb(12, 63, 104); report.tblcuadro.BackColor = Color.FromArgb(12, 63, 104); report.lblglosa.BackColor = Color.FromArgb(12, 63, 104); }
                if (eProv.cod_empresa == "00003") { report.xpb_logo.Image = Properties.Resources.Logo_HNG1; }
                if (eProv.cod_empresa == "00004") { report.xpb_logo.Image = Properties.Resources.logo_k2; report.lblref.BackColor = Color.FromArgb(0, 157, 150); report.tblcuadro.BackColor = Color.FromArgb(0, 157, 150); report.lblglosa.BackColor = Color.FromArgb(0, 157, 150); }
                if (eProv.cod_empresa == "00005") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00006") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00007") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00008") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00009") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00010") { report.xpb_logo.Image = Properties.Resources.add_32x32; }

                printTool.ShowPreviewDialog();
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGenerarGuiaRemision_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (xtcInventarioAlmacen.SelectedTabPage != xtabListadoSalidasGuiaRemision) return;
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                eAlmacen.eGuiaRemision_Cabecera eProv = gvListadoSalidasGuiaRemision.GetFocusedRow() as eAlmacen.eGuiaRemision_Cabecera;
                if (eProv == null) { MessageBox.Show("Debe seleccionar un registro de salida con guía de remisión.", "Guía de Remisión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                rptGuiaRemision report = new rptGuiaRemision();
                ReportPrintTool printTool = new ReportPrintTool(report);
                report.RequestParameters = false;
                printTool.AutoShowParametersPanel = false;
                report.Parameters["cod_guiaremision"].Value = eProv.cod_guiaremision;
                report.Parameters["cod_empresa"].Value = eProv.cod_empresa;
                report.Parameters["cod_sede_empresa"].Value = eProv.cod_sede_empresa;

                if (eProv.cod_empresa == "00001")
                {
                    report.xpb_logo.Image = Properties.Resources.Logo_HNG;
                    report.lbldomicilio.BackColor = Color.FromArgb(63, 63, 65); report.lblllegada.BackColor = Color.FromArgb(63, 63, 65); report.lbldestinatario.BackColor = Color.FromArgb(63, 63, 65); report.lbltransporte.BackColor = Color.FromArgb(63, 63, 65);
                    report.tblcuadro.BackColor = Color.FromArgb(63, 63, 65); report.lbltransportista.BackColor = Color.FromArgb(63, 63, 65); report.lbltipo.BackColor = Color.FromArgb(63, 63, 65); report.lblremitente.BackColor = Color.FromArgb(63, 63, 65);
                }
                if (eProv.cod_empresa == "00002")
                {
                    report.xpb_logo.Image = Properties.Resources.logo_facilita;
                    report.lbldomicilio.BackColor = Color.FromArgb(12, 63, 104); report.lblllegada.BackColor = Color.FromArgb(12, 63, 104); report.lbldestinatario.BackColor = Color.FromArgb(12, 63, 104); report.lbltransporte.BackColor = Color.FromArgb(12, 63, 104);
                    report.tblcuadro.BackColor = Color.FromArgb(12, 63, 104); report.lbltransportista.BackColor = Color.FromArgb(12, 63, 104); report.lbltipo.BackColor = Color.FromArgb(12, 63, 104); report.lblremitente.BackColor = Color.FromArgb(12, 63, 104);
                }
                if (eProv.cod_empresa == "00003") { report.xpb_logo.Image = Properties.Resources.Logo_HNG; }
                if (eProv.cod_empresa == "00004")
                {
                    report.xpb_logo.Image = Properties.Resources.logo_k2;
                    report.lbldomicilio.BackColor = Color.FromArgb(0, 157, 150); report.lblllegada.BackColor = Color.FromArgb(0, 157, 150); report.lbldestinatario.BackColor = Color.FromArgb(0, 157, 150); report.lbltransporte.BackColor = Color.FromArgb(0, 157, 150);
                    report.tblcuadro.BackColor = Color.FromArgb(0, 157, 150); report.lbltransportista.BackColor = Color.FromArgb(0, 157, 150); report.lbltipo.BackColor = Color.FromArgb(0, 157, 150); report.lblremitente.BackColor = Color.FromArgb(0, 157, 150);
                }
                if (eProv.cod_empresa == "00005") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00006") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00007") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00008") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00009") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00010") { report.xpb_logo.Image = Properties.Resources.add_32x32; }

                printTool.ShowPreviewDialog();
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void gvOrdEnviadas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eOrdenCompra_Servicio obj = gvListadoEntradas.GetRow(e.RowHandle) as eOrdenCompra_Servicio;
                    if (e.Column.FieldName == "ctd_Atencion") e.Appearance.ForeColor = Color.Transparent;

                    e.DefaultDraw();
                    if (e.Column.FieldName == "ctd_Atencion")
                    {
                        Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        int cellValue = Convert.ToInt32(e.CellValue);
                        b = cellValue == 0 ? SinCriterios : cellValue == 1 ? ParcialCriterios : ConCriterios;
                        e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void gvListadoEntradas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eAlmacen.eEntrada_Cabecera obj = gvListadoEntradas.GetRow(e.RowHandle) as eAlmacen.eEntrada_Cabecera;
                    if (e.Column.FieldName == "ctd_DocVinculado") e.Appearance.ForeColor = Color.Transparent;

                    e.DefaultDraw();
                    if (e.Column.FieldName == "ctd_DocVinculado")
                    {
                        Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        string cellValue = e.CellValue.ToString();
                        b = cellValue == "NO" ? SinCriterios : ConCriterios;
                        e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportarLibro12_1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
            if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
            FrmRangoFecha frm = new FrmRangoFecha();
            frm.ShowDialog();
            if (frm.fechaInicio.ToString().Contains("1/01/0001")) return;
            ExportarLibro12_1(frm.fechaInicio.ToString("yyyyMMdd"), frm.fechaFin.ToString("yyyyMMdd"));
        }


        private void ExportarLibro12_1(string FechaInicio, string FechaFin)
        {
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Exportando Reporte", "Cargando...");
            string ListSeparator = "";

            string entorno = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("conexion")].ToString());
            string server = blEncryp.Desencrypta(entorno == "LOCAL" ? ConfigurationManager.AppSettings[blEncryp.Encrypta("ServidorLOCAL")].ToString() : ConfigurationManager.AppSettings[blEncryp.Encrypta("ServidorREMOTO")].ToString());
            string bd = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("BBDD")].ToString());
            string user = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("UserID")].ToString());
            string pass = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("Password")].ToString());
            string AppName = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("AppName")].ToString());

            string cnxl = "ODBC;DRIVER=SQL Server;SERVER=" + server + ";UID=" + user + ";PWD=" + pass + ";APP=SGI_Excel;DATABASE=" + bd + "";
            //string procedure = "";

            ListSeparator = ConfigurationManager.AppSettings["ListSeparator"];
            Excel.Application objExcel = new Excel.Application();
            objExcel.Workbooks.Add();
            //objExcel.Visible = true;
            var workbook = objExcel.ActiveWorkbook;
            var sheet = workbook.Sheets["Hoja1"];
            try
            {
                objExcel.Sheets.Add();
                var worksheet = workbook.ActiveSheet;
                worksheet.Name = "Libro 12.1";
                objExcel.ActiveWindow.DisplayGridlines = false;
                objExcel.Range["A1:ZZ10000"].Font.Name = "Calibri"; objExcel.Range["A2:ZZ10000"].Font.Size = 10;
                objExcel.Range["A1:A7"].Font.Bold = true; objExcel.Range["A9:P11"].Font.Bold = true;
                objExcel.Range["A1:ZZ10000"].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                objExcel.Range["A9:P11"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                objExcel.Range["A1"].Font.Size = 16;
                //objExcel.Range["A1"].Font.Color = System.Drawing.ColorTranslator.FromHtml("#E8194F");
                objExcel.Range["A1"].Value = "Formato 12.1 - Registro de Inventario Permanente en unidades fisicas - Detalle del Inventario permanente en unidades fisicas";
                objExcel.Range["A2"].Value = "Periodo :";
                objExcel.Range["A3"].Value = "RUC :";
                objExcel.Range["A4"].Value = "Razón Social :";
                objExcel.Range["B4"].Value = navBarControl1.SelectedLink.Item.Caption.ToUpper();
                objExcel.Range["A5"].Value = "Establecimiento :";
                //  objExcel.Range["A7"].Value = "Método de Valuación :"; objExcel.Range["B7"].Value = "PROMEDIO PONDERADO DIARIO";

                objExcel.Range["A9"].Value = "DOCUMENTO TRASLADO, COMPROBANTE PAGO";
                objExcel.Range["A10"].Value = "DOCUMENTO INTERNO O SIMILAR";
                objExcel.Range["A11"].Value = "FECHA"; objExcel.Range["B11"].Value = "TIPO"; objExcel.Range["C11"].Value = "SERIE";
                objExcel.Range["D11"].Value = "NUMERO"; objExcel.Range["E11"].Value = "TIPO DE OPERACION"; objExcel.Range["F11"].Value = "ALMACEN";
                worksheet.Range["A9:G9"].MergeCells = true; worksheet.Range["A10:G10"].MergeCells = true; worksheet.Range["F11:G11"].MergeCells = true;
                objExcel.Range["H9"].Value = "ENTRADAS";
                worksheet.Range["H9:H11"].MergeCells = true;
                objExcel.Range["I9"].Value = "SALIDAS";
                worksheet.Range["I9:I11"].MergeCells = true;
                objExcel.Range["J9"].Value = "SALDO FINAL";
                worksheet.Range["J9:J11"].MergeCells = true;

                objExcel.Range["A9:J11"].Font.Color = Color.White;
                objExcel.Range["A9:J11"].Select();
                objExcel.Selection.Borders.Color = Color.FromArgb(0, 0, 0);
                objExcel.Selection.Interior.Color = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);


                objExcel.Range["A:A"].ColumnWidth = 17; objExcel.Range["B:B"].ColumnWidth = 7; objExcel.Range["C:C"].ColumnWidth = 7;
                objExcel.Range["D:D"].ColumnWidth = 15; objExcel.Range["E:E"].ColumnWidth = 23; objExcel.Range["F:F"].ColumnWidth = 6;
                objExcel.Range["G:G"].ColumnWidth = 25; objExcel.Range["H:I"].ColumnWidth = 11; objExcel.Range["J:J"].ColumnWidth = 15;
                objExcel.Range["K:L"].ColumnWidth = 11; objExcel.Range["M:M"].ColumnWidth = 15; objExcel.Range["N:O"].ColumnWidth = 11;
                objExcel.Range["P:P"].ColumnWidth = 15;
                objExcel.Range["B:G"].NumberFormat = "@";

                int fila = 13;

                List<eAlmacen.eReporteInventario> eLista = new List<eAlmacen.eReporteInventario>();
                //eLista = blLogis.Reporte_InventariounidadesFisicas<eAlmacen.eReporteInventario>(lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(), FechaInicio, FechaFin);
                eLista = blLogis.Obtener_ReporteLogistica_InventarioPermanenteValorizado<eAlmacen.eReporteInventario>(lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(), FechaInicio, FechaFin);
                List<eAlmacen.eReporteInventario> eListaAnt = new List<eAlmacen.eReporteInventario>(); //SALDO ANTERIOR
                eListaAnt = blLogis.Obtener_ListaLogistica<eAlmacen.eReporteInventario>(28, lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(), FechaInicio: FechaInicio);
                int ctd_entrada = 0, ctd_salida = 0, ctd_entradaT = 0, ctd_salidaT = 0, ctd_finalT = 0, ctd_finalS = 0;
                decimal total_entrada = 0, total_salida = 0, total_final = 0, total_entradaT = 0, total_salidaT = 0, total_finalT = 0;
                if (eLista.Count > 0)
                {
                    string producto = eLista[0].cod_producto;
                    foreach (eAlmacen.eReporteInventario eObj in eLista)
                    {
                        eAlmacen.eReporteInventario eObjAnt = new eAlmacen.eReporteInventario();
                        if (fila == 13)
                        {
                            objExcel.Range["A" + fila].Value = "CODIGO EXISTENCIA: " + eObj.cod_producto_SUNAT; objExcel.Range["A" + fila].Font.Bold = true;
                            objExcel.Range["E" + fila].Value = "DESCRIPCION: " + eObj.dsc_producto; objExcel.Range["E" + fila].Font.Bold = true;
                            fila = fila + 1;
                            objExcel.Range["A" + fila].Value = "U. MED.: " + eObj.dsc_simbolo; objExcel.Range["A" + fila].Font.Bold = true;
                            objExcel.Range["E" + fila].Value = "TIPO EXISTENCIA: " + eObj.dsc_tipo_servicio; objExcel.Range["E" + fila].Font.Bold = true;
                            fila = fila + 1;
                            objExcel.Range["C" + fila].Value = "SALDO ANTERIOR:";
                            objExcel.Rows[fila].Font.Bold = true;
                            objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                            eObjAnt = eListaAnt.Find(x => x.cod_tipo_servicio == eObj.cod_tipo_servicio && x.cod_subtipo_servicio == eObj.cod_subtipo_servicio && x.cod_producto == eObj.cod_producto);
                            objExcel.Range["J" + fila].Value = eObjAnt == null ? 0 : eObjAnt.cantidad_final;
                            ctd_finalS = ctd_finalS + (eObjAnt == null ? 0 : eObjAnt.cantidad_final);
                        }
                        if (eObj.cod_producto != producto)
                        {
                            fila = fila + 1;
                            objExcel.Range["B" + fila].Value = "Total Movimiento :";
                            objExcel.Rows[fila].Font.Bold = true;
                            objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#0070C0");
                            objExcel.Range["H" + fila].Value = ctd_entrada;
                            objExcel.Range["I" + fila].Value = ctd_salida;

                            ctd_entrada = 0; ctd_salida = 0; total_entrada = 0; total_salida = 0; total_final = 0;
                            fila = fila + 2;
                            objExcel.Range["A" + fila].Value = "CODIGO EXISTENCIA: " + eObj.cod_producto_SUNAT; objExcel.Range["A" + fila].Font.Bold = true;
                            objExcel.Range["E" + fila].Value = "DESCRIPCION: " + eObj.dsc_producto; objExcel.Range["E" + fila].Font.Bold = true;
                            fila = fila + 1;
                            objExcel.Range["A" + fila].Value = "U. MED.: " + eObj.dsc_simbolo; objExcel.Range["A" + fila].Font.Bold = true;
                            objExcel.Range["E" + fila].Value = "TIPO EXISTENCIA: " + eObj.dsc_tipo_servicio; objExcel.Range["E" + fila].Font.Bold = true;
                            fila = fila + 1;
                            objExcel.Range["C" + fila].Value = "SALDO ANTERIOR:";
                            objExcel.Rows[fila].Font.Bold = true;
                            objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                            eObjAnt = eListaAnt.Find(x => x.cod_tipo_servicio == eObj.cod_tipo_servicio && x.cod_subtipo_servicio == eObj.cod_subtipo_servicio && x.cod_producto == eObj.cod_producto);
                            objExcel.Range["J" + fila].Value = eObjAnt == null ? 0 : eObjAnt.cantidad_final;
                            ctd_finalS = ctd_finalS + (eObjAnt == null ? 0 : eObjAnt.cantidad_final);
                        }

                        fila = fila + 1;
                        objExcel.Range["A" + fila].Value = eObj.fch_documento;
                        objExcel.Range["B" + fila].Value = eObj.tipo;
                        objExcel.Range["C" + fila].Value = eObj.serie;
                        objExcel.Range["D" + fila].Value = eObj.numero;
                        objExcel.Range["E" + fila].Value = eObj.dsc_tipo_movimiento;
                        objExcel.Range["F" + fila].Value = eObj.cod_almacen;
                        objExcel.Range["G" + fila].Value = eObj.dsc_almacen;
                        if (eObj.cantidad_entrada > 0) objExcel.Range["H" + fila].Value = eObj.cantidad_entrada;
                        if (eObj.cantidad_salida > 0) objExcel.Range["I" + fila].Value = eObj.cantidad_salida;
                        objExcel.Range["J" + fila].Value = eObj.cantidad_final;
                        ctd_entrada = ctd_entrada + eObj.cantidad_entrada;
                        ctd_salida = ctd_salida + eObj.cantidad_salida;
                        ctd_entradaT = ctd_entradaT + eObj.cantidad_entrada;
                        ctd_salidaT = ctd_salidaT + eObj.cantidad_salida;
                        ctd_finalT = ctd_finalT + eObj.cantidad_final;
                        total_entrada = total_entrada + eObj.total_entrada;
                        total_salida = total_salida + eObj.total_salida;
                        total_final = total_final + eObj.total_final;
                        total_entradaT = total_entradaT + eObj.total_entrada;
                        total_salidaT = total_salidaT + eObj.total_salida;
                        total_finalT = total_finalT + eObj.total_final;
                        producto = eObj.cod_producto;
                    }
                }
                fila = fila + 1;
                objExcel.Range["B" + fila].Value = "Total Movimiento :";
                objExcel.Rows[fila].Font.Bold = true;
                objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#0070C0");
                objExcel.Range["H" + fila].Value = ctd_entrada;
                objExcel.Range["I" + fila].Value = ctd_salida;
                fila = fila + 3;
                objExcel.Range["B" + fila].Value = "SALDO INICIAL:"; objExcel.Range["B" + fila + ":P" + fila].Font.Bold = true;
                objExcel.Rows[fila].Font.Bold = true;
                objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                objExcel.Range["J" + fila].Value = ctd_finalS;
                fila = fila + 1;
                objExcel.Range["B" + fila].Value = "TOTALES:"; objExcel.Range["B" + fila + ":P" + fila].Font.Bold = true;
                objExcel.Range["H" + fila].Value = ctd_entradaT; objExcel.Range["J" + fila].Value = total_entradaT;

                objExcel.Range["H13:P" + fila].NumberFormat = "#,##0.0000";
                //objExcel.Rows[13].Delete();

                sheet.Delete();
                objExcel.WindowState = Excel.XlWindowState.xlMaximized;
                objExcel.Visible = true;
                objExcel = null/* TODO Change to default(_) if this is not a reference type */;
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                System.Threading.Thread.CurrentThread.Abort();
                objExcel.ActiveWorkbook.Saved = true;
                objExcel.ActiveWorkbook.Close();
                objExcel = null/* TODO Change to default(_) if this is not a reference type */;
                objExcel.Quit();
                SplashScreenManager.CloseForm();
                MessageBox.Show(ex.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void ExportDataSetToExcel(DataSet ds, string strPath)
        {
            int inHeaderLength = 3, inColumn = 0, inRow = 0;
            System.Reflection.Missing Default = System.Reflection.Missing.Value;
            strPath += @"\Excel" + DateTime.Now.ToString().Replace(':', '-') + ".xlx";
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelworkbook = excelApp.Workbooks.Add(1);
            foreach (DataTable dt in ds.Tables)
            {
                Excel.Worksheet excelworkSheet = excelworkbook.Sheets.Add(Default, excelworkbook.Sheets[excelworkbook.Sheets.Count], 1, Default);
                excelworkSheet.Name = dt.TableName;

                for (int i = 0; i < dt.Rows.Count; i++)
                    excelworkSheet.Cells[inHeaderLength + 1, i + 1] = dt.Columns[i].ColumnName.ToUpper();
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    for (int n = 0; n < dt.Columns.Count; n++)
                    {
                        inColumn = n + 1;
                        inRow = inHeaderLength + 2 + m;
                        excelworkSheet.Cells[inRow, inColumn] = dt.Rows[m].ItemArray[n].ToString();
                        if (m % 2 == 0)
                            excelworkSheet.get_Range("A" + inRow.ToString(), "G" + inRow.ToString()).Interior.Color = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

                    }
                }

                Excel.Range cellRang = excelworkSheet.get_Range("A1", "P3");
                cellRang.Merge(false);
                cellRang.Interior.Color = System.Drawing.Color.White;
                cellRang.Font.Color = System.Drawing.Color.Gray;
                cellRang.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                cellRang.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                cellRang.Font.Size = 12;
                excelworkSheet.Cells[1, 1] = "Formato 12.1 - Registro de Inventario Permanente en unidades fisicas - Detalle del Inventario permanente en unidades fisicas";

                cellRang = excelworkSheet.get_Range("A4", "G4");
                cellRang.Font.Bold = true;
                cellRang.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                cellRang.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#ED7D31");
                excelworkSheet.get_Range("F4").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                excelworkSheet.get_Range("F5").EntireColumn.NumberFormat = "0.00";
                excelworkSheet.Columns.AutoFit();
            }
            excelApp.DisplayAlerts = false;
            Microsoft.Office.Interop.Excel.Worksheet lastWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkbook.Worksheets[1];
            lastWorkSheet.Delete();
            excelApp.DisplayAlerts = true;

            (excelworkbook.Sheets[1] as Excel._Worksheet).Activate();
            //excelworkbook.SaveAs(strPath, Default, Default, Default, false, Default, officeExcel.XlSaveAsAccessMode.xlNoChange, Default, Default, Default, Default, Default);
            excelworkbook.Close();
            excelApp.Quit();
            MessageBox.Show("EXCEL GENERADO " + strPath);
        }

        private void btnExportarLibro13_1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
            if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
            FrmRangoFecha frm = new FrmRangoFecha();
            frm.ShowDialog();
            if (frm.fechaInicio.ToString().Contains("1/01/0001")) return;
            ExportarLibro13_1(frm.fechaInicio, frm.fechaFin);
        }

        private void ExportarLibro13_1(DateTime FechaInicio, DateTime FechaFin)
        {
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Exportando Reporte", "Cargando...");
            string ListSeparator = "";

            string entorno = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("conexion")].ToString());
            string server = blEncryp.Desencrypta(entorno == "LOCAL" ? ConfigurationManager.AppSettings[blEncryp.Encrypta("ServidorLOCAL")].ToString() : ConfigurationManager.AppSettings[blEncryp.Encrypta("ServidorREMOTO")].ToString());
            string bd = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("BBDD")].ToString());
            string user = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("UserID")].ToString());
            string pass = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("Password")].ToString());
            string AppName = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("AppName")].ToString());

            string cnxl = "ODBC;DRIVER=SQL Server;SERVER=" + server + ";UID=" + user + ";PWD=" + pass + ";APP=SGI_Excel;DATABASE=" + bd + "";
            //string procedure = "";

            ListSeparator = ConfigurationManager.AppSettings["ListSeparator"];
            Excel.Application objExcel = new Excel.Application();
            objExcel.Workbooks.Add();
            //objExcel.Visible = true;
            var workbook = objExcel.ActiveWorkbook;
            var sheet = workbook.Sheets["Hoja1"];
            try
            {
                objExcel.Sheets.Add();
                var worksheet = workbook.ActiveSheet;
                worksheet.Name = "Libro 13.1";
                objExcel.ActiveWindow.DisplayGridlines = false;
                objExcel.Range["A1:ZZ10000"].Font.Name = "Calibri";objExcel.Range["A2:ZZ10000"].Font.Size = 10;
                objExcel.Range["A1:A7"].Font.Bold = true; objExcel.Range["A9:P11"].Font.Bold = true;
                objExcel.Range["A1:ZZ10000"].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                objExcel.Range["A9:P11"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                objExcel.Range["A1"].Font.Size = 16;
                //objExcel.Range["A1"].Font.Color = System.Drawing.ColorTranslator.FromHtml("#E8194F");

                eEmpresa eEmp = blLogis.Obtener_DatosLogistica<eEmpresa>(29, "", cod_empresa);
                objExcel.Range["A1"].Value = "Formato 13.1 - Registro de Inventario Permanente Valorizado - Detalle del Inventario Valorizado";
                objExcel.Range["A2"].Value = "Periodo :"; objExcel.Range["B2"].Value = FechaInicio.ToString("dd/MM/yyyy") + " al " + FechaFin.ToString("dd/MM/yyyy"); 
                objExcel.Range["A3"].Value = "RUC :"; objExcel.Range["B3"].NumberFormat = "@"; objExcel.Range["B3"].Value = eEmp.dsc_ruc;
                objExcel.Range["A4"].Value = "Razón Social :"; objExcel.Range["B4"].Value = eEmp.dsc_empresa.ToUpper();
                objExcel.Range["A5"].Value = "Expresado en :"; objExcel.Range["B5"].Value = "SOLES"; objExcel.Range["A6"].Value = "Establecimiento :"; 
                objExcel.Range["A7"].Value = "Método de Valuación :"; objExcel.Range["B7"].Value = "PROMEDIO PONDERADO DIARIO";

                objExcel.Range["A9"].Value = "DOCUMENTO TRASLADO, COMPROBANTE PAGO";
                objExcel.Range["A10"].Value = "DOCUMENTO INTERNO O SIMILAR";
                objExcel.Range["A11"].Value = "FECHA"; objExcel.Range["B11"].Value = "TIPO"; objExcel.Range["C11"].Value = "SERIE";
                objExcel.Range["D11"].Value = "NUMERO"; objExcel.Range["E11"].Value = "TIPO DE OPERACION"; objExcel.Range["F11"].Value = "ALMACEN";
                worksheet.Range["A9:G9"].MergeCells = true; worksheet.Range["A10:G10"].MergeCells = true; worksheet.Range["F11:G11"].MergeCells = true;
                objExcel.Range["H9"].Value = "ENTRADAS"; objExcel.Range["H10"].Value = "CANTIDAD"; objExcel.Range["I10"].Value = "COSTO UNITARIO";
                objExcel.Range["J10"].Value = "COSTO TOTAL";
                worksheet.Range["H9:J9"].MergeCells = true; worksheet.Range["H10:H11"].MergeCells = true; 
                worksheet.Range["I10:I11"].MergeCells = true; worksheet.Range["J10:J11"].MergeCells = true;
                objExcel.Range["K9"].Value = "SALIDAS"; objExcel.Range["K10"].Value = "CANTIDAD"; objExcel.Range["L10"].Value = "COSTO UNITARIO";
                objExcel.Range["M10"].Value = "COSTO TOTAL";
                worksheet.Range["K9:M9"].MergeCells = true; worksheet.Range["K10:K11"].MergeCells = true;
                worksheet.Range["L10:L11"].MergeCells = true; worksheet.Range["M10:M11"].MergeCells = true;
                objExcel.Range["N9"].Value = "SALDO FINAL"; objExcel.Range["N10"].Value = "CANTIDAD"; objExcel.Range["O10"].Value = "COSTO UNITARIO";
                objExcel.Range["P10"].Value = "COSTO TOTAL";
                worksheet.Range["N9:P9"].MergeCells = true; worksheet.Range["N10:N11"].MergeCells = true;
                worksheet.Range["O10:O11"].MergeCells = true; worksheet.Range["P10:P11"].MergeCells = true;
                worksheet.Range["I10:I11"].WrapText = true; worksheet.Range["L10:L11"].WrapText = true; worksheet.Range["O10:O11"].WrapText = true;

                objExcel.Range["A9:P11"].Font.Color = Color.White;
                objExcel.Range["A9:P11"].Select();
                objExcel.Selection.Borders.Color = Color.FromArgb(0, 0, 0);
                objExcel.Selection.Interior.Color = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);

                objExcel.Range["A:A"].ColumnWidth = 17; objExcel.Range["B:B"].ColumnWidth = 7; objExcel.Range["C:C"].ColumnWidth = 7;
                objExcel.Range["D:D"].ColumnWidth = 15; objExcel.Range["E:E"].ColumnWidth = 23; objExcel.Range["F:F"].ColumnWidth = 6;
                objExcel.Range["G:G"].ColumnWidth = 25; objExcel.Range["H:I"].ColumnWidth = 11; objExcel.Range["J:J"].ColumnWidth = 15;
                objExcel.Range["K:L"].ColumnWidth = 11; objExcel.Range["M:M"].ColumnWidth = 15; objExcel.Range["N:O"].ColumnWidth = 11;
                objExcel.Range["P:P"].ColumnWidth = 15; objExcel.Range["B:G"].NumberFormat = "@";

                int fila = 13;
                //procedure = "usp_Reporte_Logistica_InventarioPermanenteValorizado";
                ////procedure = "usp_Reporte_Logistica_InventarioPermanenteValorizado @cod_almacen = '" + lkpAlmacen.EditValue.ToString() +
                ////                                    "', @cod_empresa = '" + cod_empresa +
                ////                                    "', @cod_sede_empresa = '" + lkpSedeEmpresa.EditValue.ToString() +
                ////                                    "', @FechaInicio = '" + FechaInicio + //Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd") +
                ////                                    "', @FechaFin = '" + FechaFin + "'";
                //blLogis.pDatosAExcel(cnxl, objExcel, procedure, "Consulta", "A" + fila, true);
                //fila = objExcel.Cells.Find("*", System.Reflection.Missing.Value,
                //    System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;

                List<eAlmacen.eReporteInventario> eLista = new List<eAlmacen.eReporteInventario>(); //LISTA PRODUCTO
                eLista = blLogis.Obtener_ReporteLogistica_InventarioPermanenteValorizado<eAlmacen.eReporteInventario>(lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(), FechaInicio.ToString("yyyyMMdd"), FechaFin.ToString("yyyyMMdd"));
                List<eAlmacen.eReporteInventario> eListaAnt = new List<eAlmacen.eReporteInventario>(); //SALDO ANTERIOR
                eListaAnt = blLogis.Obtener_ListaLogistica<eAlmacen.eReporteInventario>(28, lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(), FechaInicio: FechaInicio.ToString("yyyyMMdd"));
                int ctd_entrada = 0, ctd_salida = 0, ctd_entradaT = 0, ctd_salidaT = 0, ctd_finalT = 0, ctd_finalS = 0;
                decimal total_entrada = 0, total_salida = 0, total_final = 0, total_entradaT = 0, total_salidaT = 0, total_finalT = 0, total_finalS = 0;
                if (eLista.Count > 0)
                {
                    string producto = eLista[0].cod_producto;
                    foreach (eAlmacen.eReporteInventario eObj in eLista)
                    {
                        eAlmacen.eReporteInventario eObjAnt = new eAlmacen.eReporteInventario();
                        if (fila == 13)
                        {
                            objExcel.Range["A" + fila].Value = "CODIGO EXISTENCIA: " + eObj.cod_producto_SUNAT; objExcel.Range["A" + fila].Font.Bold = true;
                            objExcel.Range["E" + fila].Value = "DESCRIPCION: " + eObj.dsc_producto; objExcel.Range["E" + fila].Font.Bold = true;
                            fila = fila + 1;
                            objExcel.Range["A" + fila].Value = "U. MED.: " + eObj.dsc_simbolo; objExcel.Range["A" + fila].Font.Bold = true;
                            objExcel.Range["E" + fila].Value = "TIPO EXISTENCIA: " + eObj.dsc_tipo_servicio; objExcel.Range["E" + fila].Font.Bold = true;
                            fila = fila + 1;
                            objExcel.Range["C" + fila].Value = "SALDO ANTERIOR:";
                            objExcel.Rows[fila].Font.Bold = true;
                            objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                            eObjAnt = eListaAnt.Find(x => x.cod_tipo_servicio == eObj.cod_tipo_servicio && x.cod_subtipo_servicio == eObj.cod_subtipo_servicio && x.cod_producto == eObj.cod_producto);
                            objExcel.Range["N" + fila].Value = eObjAnt == null ? 0 : eObjAnt.cantidad_final;
                            objExcel.Range["O" + fila].Value = eObjAnt == null ? 0 : eObjAnt.costo_ponderado;
                            objExcel.Range["P" + fila].Value = eObjAnt == null ? 0 : eObjAnt.total_final;
                            ctd_finalS = ctd_finalS + (eObjAnt == null ? 0 : eObjAnt.cantidad_final);
                            total_finalS = total_finalS + (eObjAnt == null ? 0 : eObjAnt.total_final);
                        }
                        if (eObj.cod_producto != producto)
                        {
                            fila = fila + 1;
                            objExcel.Range["B" + fila].Value = "Total Movimiento :";
                            objExcel.Rows[fila].Font.Bold = true; 
                            objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#0070C0");
                            objExcel.Range["H" + fila].Value = ctd_entrada; objExcel.Range["J" + fila].Value = total_entrada;
                            objExcel.Range["K" + fila].Value = ctd_salida; objExcel.Range["M" + fila].Value = total_salida;

                            ctd_entrada = 0; ctd_salida = 0; total_entrada = 0; total_salida = 0; total_final = 0;
                            fila = fila + 2;
                            objExcel.Range["A" + fila].Value = "CODIGO EXISTENCIA: " + eObj.cod_producto_SUNAT; objExcel.Range["A" + fila].Font.Bold = true;
                            objExcel.Range["E" + fila].Value = "DESCRIPCION: " + eObj.dsc_producto; objExcel.Range["E" + fila].Font.Bold = true;
                            fila = fila + 1;
                            objExcel.Range["A" + fila].Value = "U. MED.: " + eObj.dsc_simbolo; objExcel.Range["A" + fila].Font.Bold = true;
                            objExcel.Range["E" + fila].Value = "TIPO EXISTENCIA: " + eObj.dsc_tipo_servicio; objExcel.Range["E" + fila].Font.Bold = true;
                            fila = fila + 1;
                            objExcel.Range["C" + fila].Value = "SALDO ANTERIOR:";
                            objExcel.Rows[fila].Font.Bold = true;
                            objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                            eObjAnt = eListaAnt.Find(x => x.cod_tipo_servicio == eObj.cod_tipo_servicio && x.cod_subtipo_servicio == eObj.cod_subtipo_servicio && x.cod_producto == eObj.cod_producto);
                            objExcel.Range["N" + fila].Value = eObjAnt == null ? 0 : eObjAnt.cantidad_final;
                            objExcel.Range["O" + fila].Value = eObjAnt == null ? 0 : eObjAnt.costo_ponderado;
                            objExcel.Range["P" + fila].Value = eObjAnt == null ? 0 : eObjAnt.total_final;
                            ctd_finalS = ctd_finalS + (eObjAnt == null ? 0 : eObjAnt.cantidad_final);
                            total_finalS = total_finalS + (eObjAnt == null ? 0 : eObjAnt.total_final);
                        }
                        
                        fila = fila + 1;
                        objExcel.Range["A" + fila].Value = eObj.fch_documento;
                        objExcel.Range["B" + fila].Value = eObj.tipo;
                        objExcel.Range["C" + fila].Value = eObj.serie;
                        objExcel.Range["D" + fila].Value = eObj.numero;
                        objExcel.Range["E" + fila].Value = eObj.dsc_tipo_movimiento;
                        objExcel.Range["F" + fila].Value = eObj.cod_almacen;
                        objExcel.Range["G" + fila].Value = eObj.dsc_almacen;
                        if (eObj.cantidad_entrada > 0) objExcel.Range["H" + fila].Value = eObj.cantidad_entrada;
                        if (eObj.costo_entrada > 0) objExcel.Range["I" + fila].Value = eObj.costo_entrada;
                        if (eObj.total_entrada > 0) objExcel.Range["J" + fila].Value = eObj.total_entrada;
                        if (eObj.cantidad_salida > 0) objExcel.Range["K" + fila].Value = eObj.cantidad_salida;
                        if (eObj.costo_salida > 0) objExcel.Range["L" + fila].Value = eObj.costo_salida;
                        if (eObj.total_salida > 0) objExcel.Range["M" + fila].Value = eObj.total_salida;
                        objExcel.Range["N" + fila].Value = eObj.cantidad_final;
                        objExcel.Range["O" + fila].Value = eObj.costo_ponderado;
                        objExcel.Range["P" + fila].Value = eObj.total_final;
                        ctd_entrada = ctd_entrada + eObj.cantidad_entrada; 
                        ctd_salida = ctd_salida + eObj.cantidad_salida; 
                        ctd_entradaT = ctd_entradaT + eObj.cantidad_entrada; 
                        ctd_salidaT = ctd_salidaT + eObj.cantidad_salida; 
                        ctd_finalT = ctd_finalT + eObj.cantidad_final;
                        total_entrada = total_entrada + eObj.total_entrada; 
                        total_salida = total_salida + eObj.total_salida;
                        total_final = total_final + eObj.total_final; 
                        total_entradaT = total_entradaT + eObj.total_entrada; 
                        total_salidaT = total_salidaT + eObj.total_salida;
                        total_finalT = total_finalT + eObj.total_final;
                        producto = eObj.cod_producto;
                    }
                }
                fila = fila + 1;
                objExcel.Range["B" + fila].Value = "Total Movimiento :";
                objExcel.Rows[fila].Font.Bold = true;
                objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#0070C0");
                objExcel.Range["H" + fila].Value = ctd_entrada; objExcel.Range["J" + fila].Value = total_entrada;
                objExcel.Range["K" + fila].Value = ctd_salida; objExcel.Range["M" + fila].Value = total_salida;
                fila = fila + 3;
                objExcel.Range["B" + fila].Value = "SALDO INICIAL:"; 
                objExcel.Rows[fila].Font.Bold = true;
                objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                objExcel.Range["N" + fila].Value = ctd_finalS; objExcel.Range["P" + fila].Value = total_finalS;
                fila = fila + 1;
                objExcel.Range["B" + fila].Value = "TOTALES:"; objExcel.Range["B" + fila + ":P" + fila].Font.Bold = true;
                objExcel.Range["H" + fila].Value = ctd_entradaT; objExcel.Range["J" + fila].Value = total_entradaT;
                objExcel.Range["K" + fila].Value = ctd_salidaT; objExcel.Range["M" + fila].Value = total_salidaT;
                objExcel.Range["N" + fila].Value = ctd_finalT; objExcel.Range["P" + fila].Value = total_finalT;
                objExcel.Range["H13:P" + fila].NumberFormat = "#,##0.0000";
                //objExcel.Rows[13].Delete();

                sheet.Delete();
                objExcel.WindowState = Excel.XlWindowState.xlMaximized;
                objExcel.Visible = true;
                objExcel = null/* TODO Change to default(_) if this is not a reference type */;
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                System.Threading.Thread.CurrentThread.Abort();
                objExcel.ActiveWorkbook.Saved = true;
                objExcel.ActiveWorkbook.Close();
                objExcel = null/* TODO Change to default(_) if this is not a reference type */;
                objExcel.Quit();
                SplashScreenManager.CloseForm();
                MessageBox.Show(ex.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnExportarKardexValorizado_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
            if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
            FrmRangoFecha frm = new FrmRangoFecha();
            frm.ShowDialog();
            if (frm.fechaInicio.ToString().Contains("1/01/0001")) return;

            DataTable dtGeneral = blLogis.ReporteKardex(navBarControl1.SelectedLink.Item.Name.ToString(), lkpSedeEmpresa.EditValue.ToString(), lkpAlmacen.EditValue.ToString(), frm.fechaInicio.ToString("yyyyMMdd"), frm.fechaFin.ToString("yyyyMMdd"));
            DataTable dtSaldo = blLogis.ReporteKardex_Saldo(navBarControl1.SelectedLink.Item.Name.ToString(), lkpSedeEmpresa.EditValue.ToString(), lkpAlmacen.EditValue.ToString(), frm.fechaInicio.ToString("yyyyMMdd"));

            dtGeneral.Merge(dtSaldo);

            DataView dvKardex = dtGeneral.DefaultView;
            dvKardex.Sort = "cod_producto, Fecha, fch_registro ASC";
            DataTable dtKardex = dvKardex.ToTable();

            GenerarReporteKardex(dtKardex, frm.fechaInicio, frm.fechaFin);
        }

        private void GenerarReporteKardex(DataTable dtKardex, DateTime fechaInicio, DateTime fechaFin)
        {
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Exportando Reporte", "Cargando...");

            Excel.Application objExcel = new Excel.Application();
            objExcel.Workbooks.Add();

            var workbook = objExcel.ActiveWorkbook;
            var sheet = workbook.Sheets["Hoja1"];
            //objExcel.Visible = true;

            try
            {
                objExcel.Sheets.Add();
                var worksheet = workbook.ActiveSheet;
                worksheet.Name = "Kardex";

                objExcel.ActiveWindow.DisplayGridlines = false;
                objExcel.Range["B:G"].NumberFormat = "@";
                objExcel.Range["G:G"].ColumnWidth = 34;
                objExcel.Range["B:B"].ColumnWidth = 6;
                objExcel.Range["C:C"].ColumnWidth = 6;
                objExcel.Range["D:D"].ColumnWidth = 8;
                objExcel.Range["E:E"].ColumnWidth = 8;
                objExcel.Range["A4:O5"].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                objExcel.Range["A4:O5"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                objExcel.Cells[1, 1] = navBarControl1.SelectedLink.Item.Caption.ToString();
                objExcel.Cells[2, 1] = "ALPROM23";

                string mesInicio = mesEnLetras(fechaInicio);
                string mesFin = mesEnLetras(fechaFin);

                objExcel.Cells[2, 4] = "MOVIMIENTO DE EXISTENCIAS POR ARTICULO - DE " + mesInicio + " DEL " + fechaInicio.Year.ToString() + " A " + mesFin + " DEL " + fechaFin.Year.ToString();
                objExcel.Cells[3, 6] = "MONEDA :";
                objExcel.Cells[3, 7] = "MONEDA NACIONAL";

                worksheet.Range["A4:A5"].MergeCells = true; objExcel.Cells[4, 1] = "Fecha";
                worksheet.Range["B4:B5"].MergeCells = true; objExcel.Cells[4, 2] = "Tipo Doc."; worksheet.Range["B4:B5"].WrapText = true;
                worksheet.Range["C4:C5"].MergeCells = true; objExcel.Cells[4, 3] = "Tipo Mov."; worksheet.Range["C4:C5"].WrapText = true;
                worksheet.Range["D4:D5"].MergeCells = true; objExcel.Cells[4, 4] = "Almacen";
                worksheet.Range["E4:E5"].MergeCells = true; objExcel.Cells[4, 5] = "Nro. Doc.";
                worksheet.Range["F4:F5"].MergeCells = true; objExcel.Cells[4, 6] = "Doc. Ref";
                worksheet.Range["G4:G5"].MergeCells = true; objExcel.Cells[4, 7] = "Proveedor/Cliente";
                worksheet.Range["H4:H5"].MergeCells = true; objExcel.Cells[4, 8] = "Precio Unitario"; worksheet.Range["H4:H5"].WrapText = true;
                worksheet.Range["I4:J4"].MergeCells = true; objExcel.Cells[4, 9] = "*****E N T R A D A******";
                worksheet.Range["K4:L4"].MergeCells = true; objExcel.Cells[4, 11] = "******S A L I D A*******";
                worksheet.Range["M4:N4"].MergeCells = true; objExcel.Cells[4, 13] = "*******S A L D O********";
                worksheet.Range["O4:O5"].MergeCells = true; objExcel.Cells[4, 15] = "Glosa";

                objExcel.Cells[5, 9] = "Cantidad";
                objExcel.Cells[5, 10] = "P.T.Doc.";
                objExcel.Cells[5, 11] = "Cantidad";
                objExcel.Cells[5, 12] = "P.T.Doc.";
                objExcel.Cells[5, 13] = "Cantidad";
                objExcel.Cells[5, 14] = "Importe";

                objExcel.Cells[1, 12] = DateTime.Now.ToString("dd/MM/yyyy");
                objExcel.Cells[2, 12] = DateTime.Now.ToString("hh:mm:ss t");

                int fila = 6;
                int cantidad = 0, ctd_entradas = 0, ctd_salidas = 0, ctd_saldos = 0, ctd_entradas_tot = 0, ctd_salidas_tot = 0;
                decimal importe = 0, imp_ponderado = 0, costo_salida = 0, total_entradas = 0, total_salidas = 0, total_saldos = 0, total_entradas_tot = 0, total_salidas_tot = 0;
                Boolean cambiar = true;

                for (int i = 0; i < dtKardex.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        objExcel.Range["A" + fila].NumberFormat = "@";
                        objExcel.Cells[fila, 1] = dtKardex.Rows[i][9].ToString();
                        objExcel.Cells[fila, 2] = dtKardex.Rows[i][10];

                        objExcel.Range["A" + fila + ":B" + fila].Select();
                        objExcel.Selection.Font.Bold = true;

                        fila = fila + 1;

                        objExcel.Cells[fila, 2] = "SALDO ANTERIOR";
                        objExcel.Rows[fila].Font.Bold = true;
                        objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                        objExcel.Cells[fila, 13] = cantidad;
                        objExcel.Cells[fila, 14] = importe;

                        if (dtKardex.Rows[i][8].ToString() == "Saldo")
                        {
                            ctd_saldos = (ctd_saldos + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                            total_saldos = (total_saldos + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());

                            cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                            importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                            imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                            objExcel.Cells[fila, 13] = cantidad;
                            objExcel.Cells[fila, 14] = importe;
                            objExcel.Rows[fila].Font.Bold = true;
                            objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                        }
                        else
                        {
                            objExcel.Cells[fila, 1] = "Saldo Inicial:";
                            objExcel.Rows[fila].Font.Bold = true;
                            objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                            objExcel.Cells[fila, 2] = "";

                            fila = fila + 1;

                            objExcel.Cells[fila, 1] = dtKardex.Rows[i][2];
                            objExcel.Cells[fila, 2] = dtKardex.Rows[i][3];
                            objExcel.Cells[fila, 3] = dtKardex.Rows[i][4];
                            objExcel.Cells[fila, 4] = dtKardex.Rows[i][5];
                            objExcel.Cells[fila, 5] = dtKardex.Rows[i][6];
                            objExcel.Cells[fila, 6] = dtKardex.Rows[i][7];
                            objExcel.Cells[fila, 7] = dtKardex.Rows[i][8];
                            objExcel.Cells[fila, 8] = dtKardex.Rows[i][11];
                            objExcel.Cells[fila, 9] = dtKardex.Rows[i][12];
                            objExcel.Cells[fila, 10] = dtKardex.Rows[i][13];
                            objExcel.Cells[fila, 11] = dtKardex.Rows[i][14];
                            objExcel.Cells[fila, 12] = dtKardex.Rows[i][15];

                            cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                            if (Convert.ToInt32(dtKardex.Rows[i][14]) == 0) importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                            if (Convert.ToInt32(dtKardex.Rows[i][14]) > 0)
                            {
                                objExcel.Cells[fila, 8] = "";
                                costo_salida = imp_ponderado;
                                dtKardex.Rows[i][15] = costo_salida * Convert.ToInt32(dtKardex.Rows[i][14]);
                                objExcel.Cells[fila, 12] = dtKardex.Rows[i][15];
                                importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                            }
                            imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                            ctd_entradas = ctd_entradas + Convert.ToInt32(dtKardex.Rows[i][12]); ctd_entradas_tot = ctd_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][12]);
                            total_entradas = total_entradas + Convert.ToInt32(dtKardex.Rows[i][13]); total_entradas_tot = total_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][13]);
                            ctd_salidas = ctd_salidas + Convert.ToInt32(dtKardex.Rows[i][14]); ctd_salidas_tot = ctd_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][14]);
                            total_salidas = total_salidas + Convert.ToInt32(dtKardex.Rows[i][15]); total_salidas_tot = total_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][15]);

                            objExcel.Cells[fila, 13] = cantidad;
                            objExcel.Cells[fila, 14] = importe;
                            objExcel.Cells[fila, 15] = dtKardex.Rows[i][18];
                        }
                    }
                    else
                    {
                        if (dtKardex.Rows[i][9].ToString() == dtKardex.Rows[i - 1][9].ToString())
                        {
                            if (dtKardex.Rows[i][8].ToString() == "Saldo")
                            {
                                ctd_saldos = (ctd_saldos + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                total_saldos = (total_saldos + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());

                                cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                                imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                                objExcel.Cells[fila, 13] = cantidad;
                                objExcel.Cells[fila, 14] = importe;
                                objExcel.Rows[fila].Font.Bold = true;
                                objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                            }
                            else
                            {
                                if (cambiar)
                                {
                                    if (dtKardex.Rows[i - 1][8].ToString() == "Saldo") { fila = fila + 1; }

                                    objExcel.Cells[fila - 1, 1] = "Saldo Inicial:";
                                    objExcel.Rows[fila - 1].Font.Bold = true;
                                    objExcel.Rows[fila - 1].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                                    objExcel.Cells[fila - 1, 2] = "";

                                    if (dtKardex.Rows[i - 1][8].ToString() == "Saldo") { fila = fila - 1; }
                                }

                                fila = fila + 1;

                                objExcel.Cells[fila, 1] = dtKardex.Rows[i][2];
                                objExcel.Cells[fila, 2] = dtKardex.Rows[i][3];
                                objExcel.Cells[fila, 3] = dtKardex.Rows[i][4];
                                objExcel.Cells[fila, 4] = dtKardex.Rows[i][5];
                                objExcel.Cells[fila, 5] = dtKardex.Rows[i][6];
                                objExcel.Cells[fila, 6] = dtKardex.Rows[i][7];
                                objExcel.Cells[fila, 7] = dtKardex.Rows[i][8];
                                objExcel.Cells[fila, 8] = dtKardex.Rows[i][11];
                                objExcel.Cells[fila, 9] = dtKardex.Rows[i][12];
                                objExcel.Cells[fila, 10] = dtKardex.Rows[i][13];
                                objExcel.Cells[fila, 11] = dtKardex.Rows[i][14];
                                objExcel.Cells[fila, 12] = dtKardex.Rows[i][15];

                                cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                if (Convert.ToInt32(dtKardex.Rows[i][14]) == 0) importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());

                                if (Convert.ToInt32(dtKardex.Rows[i][14]) > 0)
                                {
                                    objExcel.Cells[fila, 8] = "";
                                    costo_salida = imp_ponderado;
                                    dtKardex.Rows[i][15] = costo_salida * Convert.ToInt32(dtKardex.Rows[i][14]);
                                    objExcel.Cells[fila, 12] = dtKardex.Rows[i][15];

                                    importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                                }
                                imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                                ctd_entradas = ctd_entradas + Convert.ToInt32(dtKardex.Rows[i][12]); ctd_entradas_tot = ctd_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][12]);
                                total_entradas = total_entradas + Convert.ToInt32(dtKardex.Rows[i][13]); total_entradas_tot = total_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][13]);
                                ctd_salidas = ctd_salidas + Convert.ToInt32(dtKardex.Rows[i][14]); ctd_salidas_tot = ctd_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][14]);
                                total_salidas = total_salidas + Convert.ToInt32(dtKardex.Rows[i][15]); total_salidas_tot = total_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][15]);

                                objExcel.Cells[fila, 13] = cantidad;
                                objExcel.Cells[fila, 14] = importe;
                                objExcel.Cells[fila, 15] = dtKardex.Rows[i][18];

                                cambiar = false;
                            }
                        }
                        else
                        {
                            if (dtKardex.Rows[i - 1][8].ToString() == "Saldo")
                            {
                                objExcel.Cells[fila, 1] = "";
                                objExcel.Cells[fila, 2] = "SALDO ANTERIOR";
                                objExcel.Rows[fila].Font.Bold = true;
                                objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                            }
                            else
                            {
                                fila = fila + 1;

                                objExcel.Cells[fila, 2] = "TOTAL DE MOVIMIENTO :";
                                objExcel.Cells[fila, 9] = ctd_entradas;
                                objExcel.Cells[fila, 10] = total_entradas;
                                objExcel.Cells[fila, 11] = ctd_salidas;
                                objExcel.Cells[fila, 12] = total_salidas;
                            }

                            fila = fila + 1;

                            cantidad = 0; importe = 0;
                            imp_ponderado = 0; costo_salida = 0;
                            ctd_entradas = 0; total_entradas = 0;
                            ctd_salidas = 0; total_salidas = 0;

                            objExcel.Range["A" + fila].NumberFormat = "@";
                            objExcel.Cells[fila, 1] = dtKardex.Rows[i][9].ToString();
                            objExcel.Cells[fila, 2] = dtKardex.Rows[i][10];

                            objExcel.Range["A" + fila + ":B" + fila].Select();
                            objExcel.Selection.Font.Bold = true;

                            fila = fila + 1;

                            objExcel.Cells[fila, 2] = "SALDO ANTERIOR";
                            objExcel.Cells[fila, 13] = cantidad;
                            objExcel.Cells[fila, 14] = importe;
                            objExcel.Rows[fila].Font.Bold = true;
                            objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                            cambiar = true;

                            if (dtKardex.Rows[i][8].ToString() == "Saldo")
                            {
                                ctd_saldos = (ctd_saldos + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                total_saldos = (total_saldos + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());

                                cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                if (Convert.ToInt32(dtKardex.Rows[i][14]) == 0) importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());

                                if (Convert.ToInt32(dtKardex.Rows[i][14]) > 0)
                                {
                                    objExcel.Cells[fila, 8] = "";
                                    costo_salida = imp_ponderado;
                                    dtKardex.Rows[i][15] = costo_salida * Convert.ToInt32(dtKardex.Rows[i][14]);
                                    objExcel.Cells[fila, 12] = dtKardex.Rows[i][15];
                                    importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                                }
                                imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                                objExcel.Cells[fila, 13] = cantidad;
                                objExcel.Cells[fila, 14] = importe;
                                objExcel.Rows[fila].Font.Bold = true;
                                objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                            }
                            else
                            {
                                objExcel.Cells[fila, 1] = "Saldo Inicial:";
                                objExcel.Rows[fila].Font.Bold = true;
                                objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                                objExcel.Cells[fila, 2] = "";

                                fila = fila + 1;

                                objExcel.Cells[fila, 1] = dtKardex.Rows[i][2];
                                objExcel.Cells[fila, 2] = dtKardex.Rows[i][3];
                                objExcel.Cells[fila, 3] = dtKardex.Rows[i][4];
                                objExcel.Cells[fila, 4] = dtKardex.Rows[i][5];
                                objExcel.Cells[fila, 5] = dtKardex.Rows[i][6];
                                objExcel.Cells[fila, 6] = dtKardex.Rows[i][7];
                                objExcel.Cells[fila, 7] = dtKardex.Rows[i][8];
                                objExcel.Cells[fila, 8] = dtKardex.Rows[i][11];
                                objExcel.Cells[fila, 9] = dtKardex.Rows[i][12];
                                objExcel.Cells[fila, 10] = dtKardex.Rows[i][13];
                                objExcel.Cells[fila, 11] = dtKardex.Rows[i][14];
                                objExcel.Cells[fila, 12] = dtKardex.Rows[i][15];

                                cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                if (Convert.ToInt32(dtKardex.Rows[i][14]) == 0) importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                                if (Convert.ToInt32(dtKardex.Rows[i][14]) > 0)
                                {
                                    objExcel.Cells[fila, 8] = "";
                                    costo_salida = imp_ponderado;
                                    dtKardex.Rows[i][15] = costo_salida * Convert.ToInt32(dtKardex.Rows[i][14]);
                                    objExcel.Cells[fila, 12] = dtKardex.Rows[i][15];
                                    importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                                }
                                imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                                ctd_entradas = ctd_entradas + Convert.ToInt32(dtKardex.Rows[i][12]); ctd_entradas_tot = ctd_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][12]);
                                total_entradas = total_entradas + Convert.ToInt32(dtKardex.Rows[i][13]); total_entradas_tot = total_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][13]);
                                ctd_salidas = ctd_salidas + Convert.ToInt32(dtKardex.Rows[i][14]); ctd_salidas_tot = ctd_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][14]);
                                total_salidas = total_salidas + Convert.ToInt32(dtKardex.Rows[i][15]); total_salidas_tot = total_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][15]);

                                objExcel.Cells[fila, 13] = cantidad;
                                objExcel.Cells[fila, 14] = importe;
                                objExcel.Cells[fila, 15] = dtKardex.Rows[i][18];
                            }
                        }
                    }
                }

                if (dtKardex.DataSet != null && dtKardex.Rows[dtKardex.Rows.Count - 1][8].ToString() == "Saldo")
                {
                    objExcel.Cells[fila, 1] = "";
                    objExcel.Cells[fila, 2] = "SALDO ANTERIOR";
                    objExcel.Rows[fila].Font.Bold = true;
                    objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                }
                else
                {
                    fila = fila + 1;

                    objExcel.Cells[fila, 2] = "TOTAL DE MOVIMIENTO :";
                    objExcel.Cells[fila, 9] = ctd_entradas;
                    objExcel.Cells[fila, 10] = total_entradas;
                    objExcel.Cells[fila, 11] = ctd_salidas;
                    objExcel.Cells[fila, 12] = total_salidas;
                }

                fila = fila + 2;
                objExcel.Cells[fila, 2] = "SALDO INICIAL";
                objExcel.Cells[fila, 13] = ctd_saldos;
                objExcel.Cells[fila, 14] = total_saldos;

                fila = fila + 1;
                objExcel.Cells[fila, 2] = "TOTALES :";
                objExcel.Cells[fila, 9] = ctd_entradas_tot;
                objExcel.Cells[fila, 10] = total_entradas_tot;
                objExcel.Cells[fila, 11] = ctd_salidas_tot;
                objExcel.Cells[fila, 12] = total_salidas_tot;

                fila = fila + 1;
                objExcel.Cells[fila, 2] = "STOCK FINAL A : " + fechaFin.ToString("dd/MM/yyyy");
                objExcel.Cells[fila, 13] = (ctd_entradas_tot - ctd_salidas_tot) + ctd_saldos;
                objExcel.Cells[fila, 14] = (total_entradas_tot - total_salidas_tot) + total_saldos;

                objExcel.Range["A1:O5"].Select();
                objExcel.Selection.Font.Bold = true;

                objExcel.Range["B" + (fila - 2) + ":N" + fila].Select();
                objExcel.Selection.Font.Bold = true;

                objExcel.Range["A4:H5"].Select();
                objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#C0FFC0");

                objExcel.Range["I4:J5"].Select();
                objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#C0FFFF");

                objExcel.Range["K4:L5"].Select();
                objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#FFFFC0");

                objExcel.Range["M4:N5"].Select();
                objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#C0FFC0");

                objExcel.Range["A4:O5"].Select();
                objExcel.Selection.Borders.Color = Color.FromArgb(0, 0, 0);

                objExcel.Range["A1"].Select();

                objExcel.Range["H6:N" + fila].NumberFormat = "#,##0.0000";

                sheet.Delete();
                objExcel.WindowState = Excel.XlWindowState.xlMaximized;
                objExcel.Visible = true;
                objExcel = null;

                SplashScreenManager.CloseForm();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Generar Reporte.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string mesEnLetras(DateTime fecha)
        {
            string mes = "";

            switch (fecha.Month)
            {
                case 1: mes = "Enero"; break;
                case 2: mes = "FEBRERO"; break;
                case 3: mes = "MARZO"; break;
                case 4: mes = "ABRIL"; break;
                case 5: mes = "MAYO"; break;
                case 6: mes = "JUNIO"; break;
                case 7: mes = "JULIO"; break;
                case 8: mes = "AGOSTO"; break;
                case 9: mes = "SETIEMBRE"; break;
                case 10: mes = "OCTUBRE"; break;
                case 11: mes = "NOVIEMBRE"; break;
                case 12: mes = "DICIEMBRE"; break;
            }

            return mes;
        }

        private void gvOrdEnviadas_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvOrdEnviadas_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvReqAprobados_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvReqAprobados_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void btnAtenderRequerimiento_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (xtcInventarioAlmacen.SelectedTabPage == xtabRequerimientos)
            {
                if (MessageBox.Show("¿Esta seguro de atender los requerimientos?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    eRequerimiento obj = gvReqAprobados.GetFocusedRow() as eRequerimiento;
                    string result = blReq.Atender_Requerimiento(cod_empresa, lkpSedeEmpresa.EditValue.ToString(), obj.cod_requerimiento, obj.flg_solicitud, obj.dsc_anho, user.cod_usuario);
                    if (result != "OK") { MessageBox.Show("Error al atender requerimientos", "Requerimientos Aprobados", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    listReqAprobados = blReq.ListarRequerimiento<eRequerimiento>(3, cod_empresa, lkpSedeEmpresa.EditValue == null ? "" : lkpSedeEmpresa.EditValue.ToString(),
                                                                            "", "", "01", Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                            Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));
                    bsListadoReqAprobados.DataSource = listReqAprobados;
                    MessageBox.Show("Se atendieron los requerimientos de manera satisfactoria", "Requerimientos No Aprobados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void gvOrdEnviadas_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarOC();
        }

        private void MostrarOC()
        {
            eOrdenCompra_Servicio obj = new eOrdenCompra_Servicio();
            obj = gvOrdEnviadas.GetFocusedRow() as eOrdenCompra_Servicio;
            frmMantOrdenCompra frm = new frmMantOrdenCompra();
            frm.accion = OrdenCompra.Vista;
            frm.empresa = obj.cod_empresa;
            frm.sede = obj.cod_sede_empresa;
            frm.ordenCompraServicio = obj.cod_orden_compra_servicio;
            frm.solicitud = obj.flg_solicitud;
            frm.anho = obj.dsc_anho;
            frm.WindowState = FormWindowState.Maximized;
            frm.user = user;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.ShowDialog();
        }

        private void gvReqAprobados_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarReq();
        }

        private void MostrarReq()
        {
            eRequerimiento obj = new eRequerimiento();
            frmMantRequerimientosCompra frm = new frmMantRequerimientosCompra();
            obj = gvReqAprobados.GetFocusedRow() as eRequerimiento;
            frm.accion = RequerimientoCompra.Vista;
            frm.empresa = obj.cod_empresa;
            frm.sede = obj.cod_sede_empresa;
            frm.requerimiento = obj.cod_requerimiento;
            frm.solicitud = obj.flg_solicitud;
            frm.anho = obj.dsc_anho;
            frm.WindowState = FormWindowState.Maximized;
            frm.user = user;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.ShowDialog();
        }

        private void xtcInventarioAlmacen_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtcInventarioAlmacen.SelectedTabPage == xtabListadoEntradas)
            {
                btnGenerarNotaIngreso.Enabled = true;
                btnGenerarNotaSalida.Enabled = false;
                btnGenerarGuiaRemision.Enabled = false;
            }
            else if (xtcInventarioAlmacen.SelectedTabPage == xtabListadoSalidas)
            {
                btnGenerarNotaSalida.Enabled = true;
                btnGenerarNotaIngreso.Enabled = false;
                btnGenerarGuiaRemision.Enabled = false;
            }
            else if (xtcInventarioAlmacen.SelectedTabPage == xtabListadoSalidasGuiaRemision)
            {
                btnGenerarGuiaRemision.Enabled = true;
                btnGenerarNotaIngreso.Enabled = false;
                btnGenerarNotaSalida.Enabled = false;
            }
            else
            {
                btnGenerarGuiaRemision.Enabled = false;
                btnGenerarNotaIngreso.Enabled = false;
                btnGenerarNotaSalida.Enabled = false;
            }
        }

        private void btnKardexValorizadoDetallado_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
            if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
            FrmRangoFecha frm = new FrmRangoFecha();
            frm.ShowDialog();
            if (frm.fechaInicio.ToString().Contains("1/01/0001")) return;

            DataTable dtGeneral = blLogis.ReporteKardex(navBarControl1.SelectedLink.Item.Name.ToString(), lkpSedeEmpresa.EditValue.ToString(), lkpAlmacen.EditValue.ToString(), frm.fechaInicio.ToString("yyyyMMdd"), frm.fechaFin.ToString("yyyyMMdd"));
            DataTable dtSaldo = blLogis.ReporteKardex_Saldo(navBarControl1.SelectedLink.Item.Name.ToString(), lkpSedeEmpresa.EditValue.ToString(), lkpAlmacen.EditValue.ToString(), frm.fechaInicio.ToString("yyyyMMdd"));

            dtGeneral.Merge(dtSaldo);

            DataView dvKardex = dtGeneral.DefaultView;
            dvKardex.Sort = "cod_producto, Fecha, fch_registro ASC";
            DataTable dtKardex = dvKardex.ToTable();

            GenerarReporteKardexDetallado(dtKardex, frm.fechaInicio, frm.fechaFin);
        }

        private void GenerarReporteKardexDetallado(DataTable dtKardex, DateTime fechaInicio, DateTime fechaFin)
        {
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Exportando Reporte", "Cargando...");

            Excel.Application objExcel = new Excel.Application();
            objExcel.Workbooks.Add();

            var workbook = objExcel.ActiveWorkbook;
            var sheet = workbook.Sheets["Hoja1"];
            //objExcel.Visible = true;

            try
            {
                objExcel.Sheets.Add();
                var worksheet = workbook.ActiveSheet;
                worksheet.Name = "Kardex";

                objExcel.ActiveWindow.DisplayGridlines = false;
                objExcel.Range["F:K"].NumberFormat = "@";
                objExcel.Range["K:K"].ColumnWidth = 34;
                objExcel.Range["F:F"].ColumnWidth = 6;
                objExcel.Range["G:G"].ColumnWidth = 6;
                objExcel.Range["H:H"].ColumnWidth = 8;
                objExcel.Range["I:I"].ColumnWidth = 8;
                objExcel.Range["A4:S5"].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                objExcel.Range["A4:S5"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                objExcel.Cells[1, 1] = navBarControl1.SelectedLink.Item.Caption.ToString();
                objExcel.Cells[2, 1] = "ALPROM23";

                string mesInicio = mesEnLetras(fechaInicio);
                string mesFin = mesEnLetras(fechaFin);

                objExcel.Cells[2, 4] = "MOVIMIENTO DE EXISTENCIAS POR ARTICULO - DE " + mesInicio + " DEL " + fechaInicio.Year.ToString() + " A " + mesFin + " DEL " + fechaFin.Year.ToString();
                objExcel.Cells[3, 6] = "MONEDA :";
                objExcel.Cells[3, 7] = "MONEDA NACIONAL";

                worksheet.Range["A4:A5"].MergeCells = true; objExcel.Cells[4, 1] = "Codigo";
                worksheet.Range["B4:B5"].MergeCells = true; objExcel.Cells[4, 2] = "Tipo";
                worksheet.Range["C4:C5"].MergeCells = true; objExcel.Cells[4, 3] = "Sub Tipo";
                worksheet.Range["D4:D5"].MergeCells = true; objExcel.Cells[4, 4] = "Producto";
                worksheet.Range["E4:E5"].MergeCells = true; objExcel.Cells[4, 5] = "Fecha";
                worksheet.Range["F4:F5"].MergeCells = true; objExcel.Cells[4, 6] = "Tipo Doc."; worksheet.Range["F4:F5"].WrapText = true;
                worksheet.Range["G4:G5"].MergeCells = true; objExcel.Cells[4, 7] = "Tipo Mov."; worksheet.Range["G4:G5"].WrapText = true;
                worksheet.Range["H4:H5"].MergeCells = true; objExcel.Cells[4, 8] = "Almacen";
                worksheet.Range["I4:I5"].MergeCells = true; objExcel.Cells[4, 9] = "Nro. Doc.";
                worksheet.Range["J4:J5"].MergeCells = true; objExcel.Cells[4, 10] = "Doc. Ref";
                worksheet.Range["K4:K5"].MergeCells = true; objExcel.Cells[4, 11] = "Proveedor/Cliente";
                worksheet.Range["L4:L5"].MergeCells = true; objExcel.Cells[4, 12] = "Precio Unitario"; worksheet.Range["L4:L5"].WrapText = true;
                worksheet.Range["M4:N4"].MergeCells = true; objExcel.Cells[4, 13] = "*****E N T R A D A******";
                worksheet.Range["O4:P4"].MergeCells = true; objExcel.Cells[4, 15] = "******S A L I D A*******";
                worksheet.Range["Q4:R4"].MergeCells = true; objExcel.Cells[4, 17] = "*******S A L D O********";
                worksheet.Range["S4:S5"].MergeCells = true; objExcel.Cells[4, 19] = "Glosa";

                objExcel.Cells[5, 13] = "Cantidad";
                objExcel.Cells[5, 14] = "P.T.Doc.";
                objExcel.Cells[5, 15] = "Cantidad";
                objExcel.Cells[5, 16] = "P.T.Doc.";
                objExcel.Cells[5, 17] = "Cantidad";
                objExcel.Cells[5, 18] = "Importe";

                objExcel.Cells[1, 12] = DateTime.Now.ToString("dd/MM/yyyy");
                objExcel.Cells[2, 12] = DateTime.Now.ToString("hh:mm:ss t");

                int fila = 6;
                int cantidad = 0, ctd_entradas = 0, ctd_salidas = 0, ctd_saldos = 0, ctd_entradas_tot = 0, ctd_salidas_tot = 0;
                decimal importe = 0, imp_ponderado = 0, costo_salida = 0, total_entradas = 0, total_salidas = 0, total_saldos = 0, total_entradas_tot = 0, total_salidas_tot = 0;

                for (int i = 0; i < dtKardex.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        objExcel.Range["A" + fila].NumberFormat = "@";
                        objExcel.Cells[fila, 1] = dtKardex.Rows[i][9].ToString();
                        objExcel.Range["B" + fila].NumberFormat = "@";
                        objExcel.Cells[fila, 2] = dtKardex.Rows[i][19].ToString();
                        objExcel.Range["C" + fila].NumberFormat = "@";
                        objExcel.Cells[fila, 3] = dtKardex.Rows[i][20].ToString();
                        objExcel.Cells[fila, 4] = dtKardex.Rows[i][10];

                        objExcel.Cells[fila, 5] = "SALDO ANTERIOR";
                        objExcel.Cells[fila, 17] = cantidad;
                        objExcel.Cells[fila, 18] = importe;

                        if (dtKardex.Rows[i][8].ToString() == "Saldo")
                        {
                            ctd_saldos = (ctd_saldos + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                            total_saldos = (total_saldos + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());

                            cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                            importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                            imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                            objExcel.Cells[fila, 17] = cantidad;
                            objExcel.Cells[fila, 18] = importe;
                        }
                        else
                        {
                            objExcel.Cells[fila, 5] = dtKardex.Rows[i][2];
                            objExcel.Cells[fila, 6] = dtKardex.Rows[i][3];
                            objExcel.Cells[fila, 7] = dtKardex.Rows[i][4];
                            objExcel.Cells[fila, 8] = dtKardex.Rows[i][5];
                            objExcel.Cells[fila, 9] = dtKardex.Rows[i][6];
                            objExcel.Cells[fila, 10] = dtKardex.Rows[i][7];
                            objExcel.Cells[fila, 11] = dtKardex.Rows[i][8];
                            objExcel.Cells[fila, 12] = dtKardex.Rows[i][11];
                            objExcel.Cells[fila, 13] = dtKardex.Rows[i][12];
                            objExcel.Cells[fila, 14] = dtKardex.Rows[i][13];
                            objExcel.Cells[fila, 15] = dtKardex.Rows[i][14];
                            objExcel.Cells[fila, 16] = dtKardex.Rows[i][15];

                            cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                            if (Convert.ToInt32(dtKardex.Rows[i][14]) == 0) importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                            if (Convert.ToInt32(dtKardex.Rows[i][14]) > 0)
                            {
                                objExcel.Cells[fila, 8] = "";
                                costo_salida = imp_ponderado;
                                dtKardex.Rows[i][15] = costo_salida * Convert.ToInt32(dtKardex.Rows[i][14]);
                                objExcel.Cells[fila, 16] = dtKardex.Rows[i][15];
                                importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                            }
                            imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                            ctd_entradas = ctd_entradas + Convert.ToInt32(dtKardex.Rows[i][12]); ctd_entradas_tot = ctd_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][12]);
                            total_entradas = total_entradas + Convert.ToInt32(dtKardex.Rows[i][13]); total_entradas_tot = total_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][13]);
                            ctd_salidas = ctd_salidas + Convert.ToInt32(dtKardex.Rows[i][14]); ctd_salidas_tot = ctd_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][14]);
                            total_salidas = total_salidas + Convert.ToInt32(dtKardex.Rows[i][15]); total_salidas_tot = total_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][15]);

                            objExcel.Cells[fila, 17] = cantidad;
                            objExcel.Cells[fila, 18] = importe;
                            objExcel.Cells[fila, 19] = dtKardex.Rows[i][18];
                        }
                    }
                    else
                    {
                        if (dtKardex.Rows[i][9].ToString() == dtKardex.Rows[i - 1][9].ToString())
                        {
                            if (dtKardex.Rows[i][8].ToString() == "Saldo")
                            {
                                ctd_saldos = (ctd_saldos + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                total_saldos = (total_saldos + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());

                                cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                                imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                                objExcel.Cells[fila, 17] = cantidad;
                                objExcel.Cells[fila, 18] = importe;
                            }
                            else
                            {
                                if (dtKardex.Rows[i - 1][8].ToString() == "Saldo")
                                {
                                    objExcel.Cells[fila, 5] = "Saldo Inicial:";
                                }

                                fila = fila + 1;

                                objExcel.Range["A" + fila].NumberFormat = "@";
                                objExcel.Cells[fila, 1] = dtKardex.Rows[i][9].ToString();
                                objExcel.Range["B" + fila].NumberFormat = "@";
                                objExcel.Cells[fila, 2] = dtKardex.Rows[i][19].ToString();
                                objExcel.Range["C" + fila].NumberFormat = "@";
                                objExcel.Cells[fila, 3] = dtKardex.Rows[i][20].ToString();
                                objExcel.Cells[fila, 4] = dtKardex.Rows[i][10];

                                objExcel.Cells[fila, 5] = dtKardex.Rows[i][2];
                                objExcel.Cells[fila, 6] = dtKardex.Rows[i][3];
                                objExcel.Cells[fila, 7] = dtKardex.Rows[i][4];
                                objExcel.Cells[fila, 8] = dtKardex.Rows[i][5];
                                objExcel.Cells[fila, 9] = dtKardex.Rows[i][6];
                                objExcel.Cells[fila, 10] = dtKardex.Rows[i][7];
                                objExcel.Cells[fila, 11] = dtKardex.Rows[i][8];
                                objExcel.Cells[fila, 12] = dtKardex.Rows[i][11];
                                objExcel.Cells[fila, 13] = dtKardex.Rows[i][12];
                                objExcel.Cells[fila, 14] = dtKardex.Rows[i][13];
                                objExcel.Cells[fila, 15] = dtKardex.Rows[i][14];
                                objExcel.Cells[fila, 16] = dtKardex.Rows[i][15];

                                cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                if (Convert.ToInt32(dtKardex.Rows[i][14]) == 0) importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());

                                if (Convert.ToInt32(dtKardex.Rows[i][14]) > 0)
                                {
                                    objExcel.Cells[fila, 8] = "";
                                    costo_salida = imp_ponderado;
                                    dtKardex.Rows[i][15] = costo_salida * Convert.ToInt32(dtKardex.Rows[i][14]);
                                    objExcel.Cells[fila, 16] = dtKardex.Rows[i][15];

                                    importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                                }
                                imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                                ctd_entradas = ctd_entradas + Convert.ToInt32(dtKardex.Rows[i][12]); ctd_entradas_tot = ctd_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][12]);
                                total_entradas = total_entradas + Convert.ToInt32(dtKardex.Rows[i][13]); total_entradas_tot = total_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][13]);
                                ctd_salidas = ctd_salidas + Convert.ToInt32(dtKardex.Rows[i][14]); ctd_salidas_tot = ctd_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][14]);
                                total_salidas = total_salidas + Convert.ToInt32(dtKardex.Rows[i][15]); total_salidas_tot = total_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][15]);

                                objExcel.Cells[fila, 17] = cantidad;
                                objExcel.Cells[fila, 18] = importe;
                                objExcel.Cells[fila, 19] = dtKardex.Rows[i][18];
                            }
                        }
                        else
                        {
                            if (dtKardex.Rows[i - 1][8].ToString() == "Saldo")
                            {
                                objExcel.Cells[fila, 5] = "SALDO ANTERIOR";
                            }
                            else
                            {
                                fila = fila + 1;

                                objExcel.Range["A" + fila].NumberFormat = "@";
                                objExcel.Cells[fila, 1] = dtKardex.Rows[i - 1][9].ToString();
                                objExcel.Range["B" + fila].NumberFormat = "@";
                                objExcel.Cells[fila, 2] = dtKardex.Rows[i - 1][19].ToString();
                                objExcel.Range["C" + fila].NumberFormat = "@";
                                objExcel.Cells[fila, 3] = dtKardex.Rows[i - 1][20].ToString();
                                objExcel.Cells[fila, 4] = dtKardex.Rows[i - 1][10];

                                objExcel.Cells[fila, 5] = "TOTAL DE MOVIMIENTO :";
                                objExcel.Cells[fila, 13] = ctd_entradas;
                                objExcel.Cells[fila, 14] = total_entradas;
                                objExcel.Cells[fila, 15] = ctd_salidas;
                                objExcel.Cells[fila, 16] = total_salidas;
                            }

                            fila = fila + 1;

                            cantidad = 0; importe = 0;
                            imp_ponderado = 0; costo_salida = 0;
                            ctd_entradas = 0; total_entradas = 0;
                            ctd_salidas = 0; total_salidas = 0;

                            objExcel.Range["A" + fila].NumberFormat = "@";
                            objExcel.Cells[fila, 1] = dtKardex.Rows[i][9].ToString();
                            objExcel.Range["B" + fila].NumberFormat = "@";
                            objExcel.Cells[fila, 2] = dtKardex.Rows[i][19].ToString();
                            objExcel.Range["C" + fila].NumberFormat = "@";
                            objExcel.Cells[fila, 3] = dtKardex.Rows[i][20].ToString();
                            objExcel.Cells[fila, 4] = dtKardex.Rows[i][10];

                            objExcel.Cells[fila, 5] = "SALDO ANTERIOR";
                            objExcel.Cells[fila, 13] = cantidad;
                            objExcel.Cells[fila, 14] = importe;

                            if (dtKardex.Rows[i][8].ToString() == "Saldo")
                            {
                                ctd_saldos = (ctd_saldos + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                total_saldos = (total_saldos + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());

                                cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                if (Convert.ToInt32(dtKardex.Rows[i][14]) == 0) importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());

                                if (Convert.ToInt32(dtKardex.Rows[i][14]) > 0)
                                {
                                    objExcel.Cells[fila, 8] = "";
                                    costo_salida = imp_ponderado;
                                    dtKardex.Rows[i][15] = costo_salida * Convert.ToInt32(dtKardex.Rows[i][14]);
                                    objExcel.Cells[fila, 16] = dtKardex.Rows[i][15];
                                    importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                                }
                                imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                                objExcel.Cells[fila, 17] = cantidad;
                                objExcel.Cells[fila, 18] = importe;
                            }
                            else
                            {
                                objExcel.Cells[fila, 5] = dtKardex.Rows[i][2];
                                objExcel.Cells[fila, 6] = dtKardex.Rows[i][3];
                                objExcel.Cells[fila, 7] = dtKardex.Rows[i][4];
                                objExcel.Cells[fila, 8] = dtKardex.Rows[i][5];
                                objExcel.Cells[fila, 9] = dtKardex.Rows[i][6];
                                objExcel.Cells[fila, 10] = dtKardex.Rows[i][7];
                                objExcel.Cells[fila, 11] = dtKardex.Rows[i][8];
                                objExcel.Cells[fila, 12] = dtKardex.Rows[i][11];
                                objExcel.Cells[fila, 13] = dtKardex.Rows[i][12];
                                objExcel.Cells[fila, 14] = dtKardex.Rows[i][13];
                                objExcel.Cells[fila, 15] = dtKardex.Rows[i][14];
                                objExcel.Cells[fila, 16] = dtKardex.Rows[i][15];

                                cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                if (Convert.ToInt32(dtKardex.Rows[i][14]) == 0) importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                                if (Convert.ToInt32(dtKardex.Rows[i][14]) > 0)
                                {
                                    objExcel.Cells[fila, 8] = "";
                                    costo_salida = imp_ponderado;
                                    dtKardex.Rows[i][15] = costo_salida * Convert.ToInt32(dtKardex.Rows[i][14]);
                                    objExcel.Cells[fila, 16] = dtKardex.Rows[i][15];
                                    importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                                }
                                imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                                ctd_entradas = ctd_entradas + Convert.ToInt32(dtKardex.Rows[i][12]); ctd_entradas_tot = ctd_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][12]);
                                total_entradas = total_entradas + Convert.ToInt32(dtKardex.Rows[i][13]); total_entradas_tot = total_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][13]);
                                ctd_salidas = ctd_salidas + Convert.ToInt32(dtKardex.Rows[i][14]); ctd_salidas_tot = ctd_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][14]);
                                total_salidas = total_salidas + Convert.ToInt32(dtKardex.Rows[i][15]); total_salidas_tot = total_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][15]);

                                objExcel.Cells[fila, 17] = cantidad;
                                objExcel.Cells[fila, 18] = importe;
                                objExcel.Cells[fila, 19] = dtKardex.Rows[i][18];
                            }
                        }
                    }
                }

                if (dtKardex != null)
                {
                    if (dtKardex.Rows[dtKardex.Rows.Count - 1][8].ToString() == "Saldo")
                    {
                        objExcel.Cells[fila, 5] = "SALDO ANTERIOR";
                    }
                    else
                    {
                        fila = fila + 1;

                        objExcel.Range["A" + fila].NumberFormat = "@";
                        objExcel.Cells[fila, 1] = dtKardex.Rows[dtKardex.Rows.Count - 1][9].ToString();
                        objExcel.Range["B" + fila].NumberFormat = "@";
                        objExcel.Cells[fila, 2] = dtKardex.Rows[dtKardex.Rows.Count - 1][19].ToString();
                        objExcel.Range["C" + fila].NumberFormat = "@";
                        objExcel.Cells[fila, 3] = dtKardex.Rows[dtKardex.Rows.Count - 1][20].ToString();
                        objExcel.Cells[fila, 4] = dtKardex.Rows[dtKardex.Rows.Count - 1][10];

                        objExcel.Cells[fila, 5] = "TOTAL DE MOVIMIENTO :";
                        objExcel.Cells[fila, 13] = ctd_entradas;
                        objExcel.Cells[fila, 14] = total_entradas;
                        objExcel.Cells[fila, 15] = ctd_salidas;
                        objExcel.Cells[fila, 16] = total_salidas;
                    }
                }

                fila = fila + 2;
                objExcel.Cells[fila, 2] = "SALDO INICIAL";
                objExcel.Cells[fila, 17] = ctd_saldos;
                objExcel.Cells[fila, 18] = total_saldos;

                fila = fila + 1;
                objExcel.Cells[fila, 2] = "TOTALES :";
                objExcel.Cells[fila, 13] = ctd_entradas_tot;
                objExcel.Cells[fila, 14] = total_entradas_tot;
                objExcel.Cells[fila, 15] = ctd_salidas_tot;
                objExcel.Cells[fila, 16] = total_salidas_tot;

                fila = fila + 1;
                objExcel.Cells[fila, 2] = "STOCK FINAL A : " + fechaFin.ToString("dd/MM/yyyy");
                objExcel.Cells[fila, 17] = (ctd_entradas_tot - ctd_salidas_tot) + ctd_saldos;
                objExcel.Cells[fila, 18] = (total_entradas_tot - total_salidas_tot) + total_saldos;

                objExcel.Range["A1:S5"].Select();
                objExcel.Selection.Font.Bold = true;

                objExcel.Range["B" + (fila - 2) + ":R" + fila].Select();
                objExcel.Selection.Font.Bold = true;

                objExcel.Range["A4:L5"].Select();
                objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#C0FFC0");

                objExcel.Range["M4:N5"].Select();
                objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#C0FFFF");

                objExcel.Range["O4:P5"].Select();
                objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#FFFFC0");

                objExcel.Range["Q4:R5"].Select();
                objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#C0FFC0");

                objExcel.Range["A4:S5"].Select();
                objExcel.Selection.Borders.Color = Color.FromArgb(0, 0, 0);

                objExcel.Range["A1"].Select();

                objExcel.Range["L6:R" + fila].NumberFormat = "#,##0.0000";

                sheet.Delete();
                objExcel.WindowState = Excel.XlWindowState.xlMaximized;
                objExcel.Visible = true;
                objExcel = null;

                SplashScreenManager.CloseForm();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Generar Reporte.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRepOCxProv_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
            if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
            FrmRangoFecha frm = new FrmRangoFecha();
            frm.ShowDialog();
            if (frm.fechaInicio.ToString().Contains("1/01/0001")) return;

            DataTable dtOrdenes = blLogis.ReporteOrdenesCompra(8, navBarControl1.SelectedLink.Item.Name.ToString(), lkpSedeEmpresa.EditValue.ToString(), lkpAlmacen.EditValue.ToString(), frm.fechaInicio.ToString("yyyyMMdd"), frm.fechaFin.ToString("yyyyMMdd"));

            GenerarReporteOrdenCompra(dtOrdenes, frm.fechaInicio, frm.fechaFin);
        }

        private void GenerarReporteOrdenCompra(DataTable dtOrdenes, DateTime fechaInicio, DateTime fechaFin)
        {
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Exportando Reporte", "Cargando...");

            Excel.Application objExcel = new Excel.Application();
            objExcel.Workbooks.Add();

            var workbook = objExcel.ActiveWorkbook;
            var sheet = workbook.Sheets["Hoja1"];
            //objExcel.Visible = true;

            try
            {
                objExcel.Sheets.Add();
                var worksheet = workbook.ActiveSheet;
                worksheet.Name = "Reporte OC";

                //objExcel.ActiveWindow.DisplayGridlines = false;
                objExcel.Range["H:H"].NumberFormat = "@";
                objExcel.Range["A:A"].ColumnWidth = 18;
                objExcel.Range["B:B"].ColumnWidth = 50;
                objExcel.Range["C:I"].ColumnWidth = 18;
                objExcel.Range["J:J"].ColumnWidth = 60;
                objExcel.Range["K:AG"].ColumnWidth = 18;

                objExcel.Range["A1:A1"].Select();
                objExcel.Selection.Font.Bold = true;
                objExcel.Cells[1, 1] = navBarControl1.SelectedLink.Item.Caption.ToString();
                objExcel.Cells[2, 1] = "COMOVI11";

                string mesInicio = mesEnLetras(fechaInicio);
                string mesFin = mesEnLetras(fechaFin);

                objExcel.Range["B3:B5"].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                objExcel.Range["B3:B5"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                worksheet.Range["B3:H3"].MergeCells = true; objExcel.Cells[3, 2] = "REPORTE DE ORDENES DE COMPRA POR PROVEEDOR";
                worksheet.Range["B4:H4"].MergeCells = true; objExcel.Cells[4, 2] = "TIPO DE MONEDA: MN - MONEDA NACIONAL";
                worksheet.Range["B5:H5"].MergeCells = true; objExcel.Cells[5, 2] = "DEL PROVEEDOR: " + dtOrdenes.Rows[0][0].ToString() + " AL PROVEEDOR: " + dtOrdenes.Rows[dtOrdenes.Rows.Count - 1][0].ToString();

                objExcel.Range["B3:B5"].Select();
                objExcel.Selection.Font.Bold = true;

                objExcel.Cells[8, 1] = "COD.PROVEE"; objExcel.Cells[8, 2] = "PROVEEDOR"; objExcel.Cells[8, 3] = "NRO.ORDEN"; objExcel.Cells[8, 4] = "M.O";
                objExcel.Cells[8, 5] = "FEC.ORDEN"; objExcel.Cells[8, 6] = "FORMA DE PAGO"; objExcel.Cells[8, 7] = "FEC.ENTREGA"; objExcel.Cells[8, 8] = "COD.ARTICULO";
                objExcel.Cells[8, 9] = "COD.REFERENCIA"; objExcel.Cells[8, 10] = "ARTICULO"; objExcel.Cells[8, 11] = "CANT.ORDENADA"; objExcel.Cells[8, 12] = "PR.UNIT. s/IGV";
                objExcel.Cells[8, 13] = "PR.TOTAL s/IGV"; objExcel.Cells[8, 14] = "PR.UNIT c/IGV"; objExcel.Cells[8, 15] = "PR.TOTAL c/IGV"; objExcel.Cells[8, 16] = "U.M.";
                objExcel.Cells[8, 17] = "CANT.RECIBIDA"; objExcel.Cells[8, 18] = "CANT.PENDIENTE"; objExcel.Cells[8, 19] = "SITUACION"; objExcel.Cells[8, 20] = "TD NUMDOC";
                objExcel.Cells[8, 21] = "P.ARANCELARIA"; objExcel.Cells[8, 22] = "COD.C.COSTOS"; objExcel.Cells[8, 23] = "CENTRO DE COSTOS"; objExcel.Cells[8, 24] = "COD.GRUPO";
                objExcel.Cells[8, 25] = "GRUPO"; objExcel.Cells[8, 26] = "COD.FAMILIA"; objExcel.Cells[8, 27] = "FAMILIA"; objExcel.Cells[8, 28] = "COD.MODELO";
                objExcel.Cells[8, 29] = "MODELO"; objExcel.Cells[8, 30] = "COD.MARCA"; objExcel.Cells[8, 31] = "MARCA"; objExcel.Cells[8, 32] = "COD.LINEA";
                objExcel.Cells[8, 33] = "LINEA";

                objExcel.Range["A8:AG8"].Select();
                objExcel.Selection.Borders.Color = Color.FromArgb(0, 0, 0);

                objExcel.Range["AG1:AG2"].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                objExcel.Cells[1, 33] = DateTime.Now.ToString("dd/MM/yyyy");
                objExcel.Cells[2, 33] = DateTime.Now.ToString("hh:mm:ss");

                int fila = 9;

                for (int i = 0; i < dtOrdenes.Rows.Count; i++)
                {
                    for (int x = 0; x < 33; x++)
                    {
                        objExcel.Cells[fila, x + 1] = dtOrdenes.Rows[i][x];
                    }

                    fila = fila + 1;
                }

                objExcel.Range["L6:O" + fila].NumberFormat = "#,##0.0000";

                objExcel.Range["A1:A1"].Select();

                sheet.Delete();
                objExcel.WindowState = Excel.XlWindowState.xlMaximized;
                objExcel.Visible = true;
                objExcel = null;

                SplashScreenManager.CloseForm();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Generar Reporte.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                SplashScreenManager.CloseForm();
            }
        }

        private void gvListadoSalidas_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoSalidasGuiaRemision_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoSalidasGuiaRemision_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

    }
}