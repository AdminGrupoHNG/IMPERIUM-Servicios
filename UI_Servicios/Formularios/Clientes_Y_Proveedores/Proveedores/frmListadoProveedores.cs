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
using BE_Servicios;
using BL_Servicios;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.Images;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraReports.UI;

namespace UI_Servicios.Formularios.Clientes_Y_Proveedores.Proveedores
{
    public partial class frmListadoProveedores : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public eUsuario user = new eUsuario();
        blEncrypta blEncryp = new blEncrypta();
        blProveedores blProv = new blProveedores();
        blGlobales blGlobal = new blGlobales();
        bool Buscar = false;
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        Brush ConCriterios = Brushes.Green;
        Brush SinCriterios = Brushes.Red;
        int markWidth = 16;

        public frmListadoProveedores()
        {
            InitializeComponent();
        }

        private void frmListadoProveedores_Load(object sender, EventArgs e)
        {
            HabilitarBotones();
            Inicializar();
        }
        private void Inicializar()
        {
            CargarOpcionesMenu();
            CargarListado("TODOS", "");
            lblTitulo.ForeColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
            lblTitulo.Text = navBarControl1.SelectedLink.Group.Caption + ": " + navBarControl1.SelectedLink.Item.Caption;
            picTitulo.Image = navBarControl1.SelectedLink.Group.ImageOptions.LargeImage;
            navBarControl1.Groups[0].SelectedLinkIndex = 0;
            //navBarControl1.Groups["Por_Empresa"].SelectedLinkIndex = 0;
            Buscar = true;
        }

        private void HabilitarBotones()
        {
            blSistema blSist = new blSistema();
            List<eVentana> listPermisos = blSist.ListarMenuxUsuario<eVentana>(user.cod_usuario, this.Name);

            if (listPermisos.Count > 0)
            {
                grupoEdicion.Enabled = listPermisos[0].flg_escritura;
                grupoAcciones.Enabled = listPermisos[0].flg_escritura;
            }
        }

        internal void CargarOpcionesMenu()
        {
            List<eProveedor> ListProveedor = new List<eProveedor>();

            ListProveedor = blProv.ListarOpcionesMenu<eProveedor>(3);
            Image imgModoPagoLarge = ImageResourceCache.Default.GetImage("images/business%20objects/bosaleitem_32x32.png");
            Image imgModoPagoSmall = ImageResourceCache.Default.GetImage("images/business%20objects/bosaleitem_16x16.png");

            NavBarGroup NavModoPago = navBarControl1.Groups.Add();
            NavModoPago.Caption = "Por Modalidad de Pago"; NavModoPago.Expanded = true; NavModoPago.SelectedLinkIndex = 0;
            NavModoPago.GroupCaptionUseImage = NavBarImage.Large; NavModoPago.GroupStyle = NavBarGroupStyle.SmallIconsText;
            NavModoPago.ImageOptions.LargeImage = imgModoPagoLarge; NavModoPago.ImageOptions.SmallImage = imgModoPagoSmall;

            foreach (eProveedor obj in ListProveedor)
            {
                NavBarItem NavDetalle = navBarControl1.Items.Add();
                NavDetalle.Tag = obj.cod_modalidad_pago; NavDetalle.Name = obj.cod_modalidad_pago;
                NavDetalle.Caption = obj.dsc_modalidad_pago; NavDetalle.LinkClicked += NavDetalle_LinkClicked;

                NavModoPago.ItemLinks.Add(NavDetalle);
            }
            

            List<eProveedor_Servicios> ListProvServ = blProv.ListarOpcionesMenu<eProveedor_Servicios>(16);
            Image imgServicioLarge = ImageResourceCache.Default.GetImage("images/business%20objects/bodepartment_32x32.png");
            Image imgServicioSmall = ImageResourceCache.Default.GetImage("images/business%20objects/bodepartment_16x16.png");

            NavBarGroup NavServicio = navBarControl1.Groups.Add();
            NavServicio.Caption = "Por Servicio"; NavServicio.Expanded = true; NavServicio.SelectedLinkIndex = 0;
            NavServicio.GroupCaptionUseImage = NavBarImage.Large; NavServicio.GroupStyle = NavBarGroupStyle.SmallIconsText;
            NavServicio.ImageOptions.LargeImage = imgServicioLarge; NavServicio.ImageOptions.SmallImage = imgServicioSmall;

            foreach (eProveedor_Servicios obj in ListProvServ)
            {
                NavBarItem NavDetalle = navBarControl1.Items.Add();
                NavDetalle.Tag = obj.cod_tipo_servicio; NavDetalle.Name = obj.cod_tipo_servicio;
                NavDetalle.Caption = obj.dsc_tipo_servicio; NavDetalle.LinkClicked += NavDetalle_LinkClicked;

                NavServicio.ItemLinks.Add(NavDetalle);
            }

            List<eProveedor_Empresas> ListProvEmp = blProv.ListarOpcionesMenu<eProveedor_Empresas>(12);
            Image imgEmpresaLarge = ImageResourceCache.Default.GetImage("images/navigation/home_32x32.png");
            Image imgEmpresaSmall = ImageResourceCache.Default.GetImage("images/navigation/home_16x16.png");

            NavBarGroup NavEmpresa = navBarControl1.Groups.Add();
            NavEmpresa.Name = "Por Empresa";
            NavEmpresa.Caption = "Por Empresa"; NavEmpresa.Expanded = true; NavEmpresa.SelectedLinkIndex = 0;
            NavEmpresa.GroupCaptionUseImage = NavBarImage.Large; NavEmpresa.GroupStyle = NavBarGroupStyle.SmallIconsText;
            NavEmpresa.ImageOptions.LargeImage = imgEmpresaLarge; NavEmpresa.ImageOptions.SmallImage = imgEmpresaSmall;

            foreach (eProveedor_Empresas obj in ListProvEmp)
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
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
            CargarListado(e.Link.Group.Caption, e.Link.Item.Tag.ToString());
            SplashScreenManager.CloseForm();
        }

        public void CargarListado(string NombreGrupo, string Codigo)
        {
            try
            {
                string cod_tipo_documento = "", cod_tipo_proveedor = "", cod_modalidad_pago = "", cod_empresa = "", cod_tipo_servicio = "";
                switch (NombreGrupo)
                {
                    case "Por Tipo Documento": cod_tipo_documento = Codigo; break;
                    case "Por Tipo Proveedor": cod_tipo_proveedor = Codigo; break;
                    case "Por Modalidad de Pago": cod_modalidad_pago = Codigo; break;
                    case "Por Empresa": cod_empresa = Codigo; break;
                    case "Por Servicio": cod_tipo_servicio = Codigo; break;
                }

                //List<eProveedor_Empresas> listEmpresasUsuario = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(11, "", user.cod_usuario);
                //eProveedor_Empresas objUser = new eProveedor_Empresas();
                //if (listEmpresasUsuario.Count > 0) objUser.cod_empresa = listEmpresasUsuario[0].cod_empresa;
                List<eProveedor> ListProveedor = new List<eProveedor>();
                ListProveedor = blProv.ListarProveedores<eProveedor>(1, cod_tipo_documento, cod_tipo_proveedor, cod_modalidad_pago, cod_empresa, cod_tipo_servicio, user.cod_usuario);
                /*bsListaProveedores.DataSource = null;*/ bsListaProveedores.DataSource = ListProveedor;
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
            if(!Buscar) e.Group.SelectedLinkIndex = 0;
            if (e.Group.SelectedLink != null && Buscar)
            {
                ActiveGroupChanged(e.Group.Caption + ": " + e.Group.SelectedLink.Item.Caption, e.Group.ImageOptions.LargeImage);
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                CargarListado(e.Group.Caption, e.Group.SelectedLink.Item.Tag.ToString());
                SplashScreenManager.CloseForm();
            }
        }
        internal void frmListadoProveedores_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
                CargarListado(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
                SplashScreenManager.CloseForm();
            }
        }
        
        private void gvListaProveedores_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListaProveedores_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListaProveedores_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                if(e.RowHandle >= 0)
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

        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportarExcel();
        }
        private void ExportarExcel()
        {
            try
            {
                string carpeta = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\Proveedores" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                gvListaProveedores.ExportToXlsx(archivo);
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
            gvListaProveedores.ShowPrintPreview();
        }

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmMantProveedor frm = new frmMantProveedor(this);
                frm.MiAccion = Proveedor.Nuevo;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.user = user;
                frm.ShowDialog();
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
                DialogResult msgresult = MessageBox.Show("¿Está seguro de activar esta proveedor?", "Activar proveedor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eProveedor eProv = gvListaProveedores.GetFocusedRow() as eProveedor;
                    string result = blProv.Activar_Inactivar_Proveedor(eProv.cod_proveedor, "SI");
                    blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    int nRow = gvListaProveedores.FocusedRowHandle;
                    CargarListado(navBarControl1.SelectedLink.Group.Caption, navBarControl1.SelectedLink.Item.Tag.ToString());
                    gvListaProveedores.FocusedRowHandle = nRow;
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnFichaProveedor_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                eProveedor eProv = gvListaProveedores.GetFocusedRow() as eProveedor;
                if (eProv == null) { MessageBox.Show("Debe seleccionar un proveedor.", "Ficha del proveedor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                rptFichaProveedor report = new rptFichaProveedor();
                ReportPrintTool printTool = new ReportPrintTool(report);
                report.RequestParameters = false;
                printTool.AutoShowParametersPanel = false;
                report.Parameters["cod_proveedor"].Value = eProv.cod_proveedor;
                report.Parameters["cod_usuario"].Value = user.cod_usuario;
                printTool.ShowPreviewDialog();
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaProveedores_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            e.DefaultDraw();
            if (e.Column.FieldName == "CantCriterios")
            {
                Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                int cellValue = Convert.ToInt32(e.CellValue);
                if (cellValue < 2) { b = SinCriterios; } else { b = ConCriterios; }
                e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth));
            }
        }

        private void gvListaProveedores_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2)
                {
                    eFacturaProveedor objj = gvListaProveedores.GetRow(e.RowHandle) as eFacturaProveedor;
                    if (objj != null) return; 
                    if (e.Column.FieldName == "CantCriterios")
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnInactivar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de inactivar esta proveedor?", "Inactivar proveedor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eProveedor eProv = gvListaProveedores.GetFocusedRow() as eProveedor;
                    string result = blProv.Activar_Inactivar_Proveedor(eProv.cod_proveedor, "NO");
                    blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    int nRow = gvListaProveedores.FocusedRowHandle;
                    CargarListado(navBarControl1.SelectedLink.Group.Caption, navBarControl1.SelectedLink.Item.Tag.ToString());
                    gvListaProveedores.FocusedRowHandle = nRow;
                    SplashScreenManager.CloseForm();
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
                eProveedor eProv = gvListaProveedores.GetFocusedRow() as eProveedor;

                List<eProveedor_CuentasBancarias> ListCuentasBancarias = blProv.ListarCuentasBancariasProveedor<eProveedor_CuentasBancarias>(3, eProv.cod_proveedor);
                if (ListCuentasBancarias.Count > 0) { MessageBox.Show("No se puede eliminar el proveedor ya que tiene registrado cuentas bancarias.", "Eliminar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                List<eProveedor_Contactos> ListContactosProveedor = blProv.ListarContactosProveedor<eProveedor_Contactos>(5, eProv.cod_proveedor);
                if (ListContactosProveedor.Count > 0) { MessageBox.Show("No se puede eliminar el proveedor ya que tiene registrado contactos.", "Eliminar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                List<eProveedor_Tecnicos> ListTecnicosProveedor = blProv.ListarTecnicosProveedor<eProveedor_Tecnicos>(7, eProv.cod_proveedor);
                if (ListTecnicosProveedor.Count > 0) { MessageBox.Show("No se puede eliminar el proveedor ya que tiene registrado técnicos.", "Eliminar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                List<eProveedor> ListProv = blProv.ListarFacturas<eProveedor>(11, eProv.cod_proveedor);
                if (ListProv.Count > 0) { MessageBox.Show("No se puede eliminar el proveedor ya que tiene facturas asociadas.", "Eliminar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este proveedor?", "Eliminar proveedor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {

                    string result = blProv.Eliminar_Proveedor(eProv.cod_proveedor);
                    blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    CargarListado(navBarControl1.SelectedLink.Group.Caption, navBarControl1.SelectedLink.Item.Tag.ToString());
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvListaProveedores_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eProveedor obj = gvListaProveedores.GetFocusedRow() as eProveedor;

                    frmMantProveedor frm = new frmMantProveedor(this);
                    frm.cod_proveedor = obj.cod_proveedor;
                    frm.MiAccion = Proveedor.Editar;
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorEventRow = colorEventRow;
                    frm.colorFocus = colorFocus;
                    frm.cod_empresa = navBarControl1.SelectedLink.Item.Tag.ToString();
                    frm.user = user;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}