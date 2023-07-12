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
using Microsoft.Identity.Client;
using DevExpress.XtraSplashScreen;
using System.Configuration;
using System.Security;
using System.IO;
using System.Net.Http.Headers;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
using DevExpress.XtraEditors;
using Excel = Microsoft.Office.Interop.Excel;

namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    public partial class frmResumenMovCajaChica : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public eUsuario user = new eUsuario();
        blCajaChica blCaja = new blCajaChica();
        blTrabajador blTrab = new blTrabajador();
        blFactura blFact = new blFactura();
        blGlobales blGlobal = new blGlobales();
        blEncrypta blEncryp = new blEncrypta();
        blProveedores blProv = new blProveedores();
        List<eCajaChica.eMovimiento_CajaChica> listPreRendicion = new List<eCajaChica.eMovimiento_CajaChica>();
        List<eCajaChica.eDetalleMov_CajaChica> listPostRendicion = new List<eCajaChica.eDetalleMov_CajaChica>();
        List<eCajaChica.eDetalleMov_CajaChica> listCajaRendida = new List<eCajaChica.eDetalleMov_CajaChica>();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        Image ImgPDF = DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttopdf_16x16.png");
        Brush RENDIDO = Brushes.Green;
        Brush PENDIENTE = Brushes.Red;
        Brush APERTURA = Brushes.Purple;
        int markWidth = 16;

        //OneDrive
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        string varPathOrigen = "";
        string varNombreArchivo = "";

        public frmResumenMovCajaChica()
        {
            InitializeComponent();
        }

        private void frmResumenMovCajaChica_Load(object sender, EventArgs e)
        {
            timer1.Stop(); 
            Inicializar();
            bgvListadoPostRendicion.Appearance.VertLine.BackColor = Color.Transparent;
            bgvListadoPostRendicion.Appearance.HorzLine.BackColor = Color.Transparent;
            bgvListaCajaRendida.Appearance.VertLine.BackColor = Color.Transparent;
            bgvListaCajaRendida.Appearance.HorzLine.BackColor = Color.Transparent;
            if (lkpTipoCaja.EditValue != null) btnBuscar_Click(btnBuscar, new EventArgs());
        }

        private void Inicializar()
        {
            CargarLookUpEdit();
            List<eFacturaProveedor> list = blProv.ListarEmpresasProveedor<eFacturaProveedor>(11, "", user.cod_usuario);
            if (list.Count >= 1) lkpEmpresa.EditValue = list[0].cod_empresa;
            //List<eTrabajador.eInfoLaboral_Trabajador> ListInfoLaboral = blTrab.ListarTrabajadores<eTrabajador.eInfoLaboral_Trabajador>(4, cod_trabajador, lkpEmpresa.EditValue.ToString());

            blSistema blSist = new blSistema();
            List<eVentana> listPerfil = blSist.ListarPerfilesUsuario<eVentana>(4, user.cod_usuario);
            eVentana oPerfil = listPerfil.Find(x => x.cod_perfil == 4 || x.cod_perfil == 18 || x.cod_perfil == 15 || x.cod_perfil == 5);
            lkpEmpresa.ReadOnly = oPerfil == null ? true : false; 
            //lkpSedeEmpresa.ReadOnly = oPerfil == null ? true : false; 
            lkpResponsable.ReadOnly = oPerfil == null ? true : false;

            //if (user.cod_usuario != "ADMINISTRADOR" )
            //{
            //    lkpEmpresa.ReadOnly = true; lkpSedeEmpresa.ReadOnly = true; lkpResponsable.ReadOnly = true;
            //}
        }

        private void CargarLookUpEdit()
        {
            blFact.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: user.cod_usuario);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lkpTipoCaja.EditValue == null) { MessageBox.Show("Debe seleccionar una caja", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                ObtenerLista_PreRendicion(lkpTipoCaja.EditValue.ToString());
                ObtenerLista_PostRendicion(lkpTipoCaja.EditValue.ToString());
                ObtenerLista_CajaRendida(lkpTipoCaja.EditValue.ToString());

                txtImporteCaja.EditValue = 0; txtSaldoCaja.EditValue = 0; dtFecUltimaReposicion.EditValue = null;
                eCajaChica obj = blCaja.ObtenerDatos_CajaChica<eCajaChica>(10, lkpTipoCaja.EditValue.ToString());
                if (obj == null) return;
                txtImporteCaja.EditValue = obj.imp_monto; txtSaldoCaja.EditValue = 0;
                eCajaChica obj2 = blCaja.ObtenerDatos_CajaChica<eCajaChica>(11, lkpTipoCaja.EditValue.ToString());
                if (obj2 != null) dtFecUltimaReposicion.EditValue = obj2.fch_creacion;
                ObtenerMovimientos(obj.imp_alertar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ObtenerLista_PreRendicion(string cod_caja)
        {
            listPreRendicion = blCaja.ListarDatos_CajaChica<eCajaChica.eMovimiento_CajaChica>(6, cod_caja, "");
            bsListaPreRendicion.DataSource = listPreRendicion; gvListadoPreRendicion.RefreshData();
        }

        private void ObtenerLista_PostRendicion(string cod_caja)
        {
            listPostRendicion = blCaja.ListarDatos_CajaChica<eCajaChica.eDetalleMov_CajaChica>(7, cod_caja, "");
            bsListaPostRendicion.DataSource = listPostRendicion; bgvListadoPostRendicion.RefreshData();
        }

        private void ObtenerLista_CajaRendida(string cod_caja)
        {
            listCajaRendida = blCaja.ListarDatos_CajaChica<eCajaChica.eDetalleMov_CajaChica>(8, cod_caja, "");
            bsCajaRendida.DataSource = listCajaRendida; bgvListaCajaRendida.RefreshData();
        }

        private void gvFacturasProveedor_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoPreRendicion_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eCajaChica.eMovimiento_CajaChica obj = gvListadoPreRendicion.GetRow(e.RowHandle) as eCajaChica.eMovimiento_CajaChica;

                    if (e.Column.FieldName == "flg_PDF" && obj.flg_PDF == "SI")
                    {
                        e.Handled = true;
                        e.Graphics.DrawImage(ImgPDF, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "abv_estado") e.DisplayText = "";
                    if (e.Column.FieldName == "dsc_ajuste" && obj.dsc_ajuste != null && obj.dsc_ajuste.Trim() != "")
                    {
                        if (obj.dsc_ajuste.Trim().Substring(0, 1) == "R") { e.Appearance.ForeColor = Color.Purple; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                        if (obj.dsc_ajuste.Trim().Substring(0, 1) == "D") { e.Appearance.ForeColor = Color.DarkGreen; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                    }
                    if (obj.cod_movimiento != null && obj.cod_movimiento.Trim().Substring(0, 2) == "RP") { e.Appearance.BackColor = Color.DarkGray; e.Appearance.ForeColor = Color.DarkBlue; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                    e.DefaultDraw();
                    if (e.Column.FieldName == "abv_estado")
                    {
                        Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        string cellValue = e.CellValue.ToString();
                        if (cellValue == "A" || cellValue == "R") { b = RENDIDO; }  else { b = PENDIENTE; }
                        e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        static void Appl()
        {
            _clientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithAuthority($"{Instance}{TenantId}")
                .WithDefaultRedirectUri()
                .Build();
            TokenCacheHelper.EnableSerialization(_clientApp.UserTokenCache);
        }
        private static string ClientId = "";
        private static string TenantId = "";
        private static string Instance = "https://login.microsoftonline.com/";
        public static IPublicClientApplication _clientApp;
        public static IPublicClientApplication PublicClientApp { get { return _clientApp; } }

        
        private async void bgvListadoPostRendicion_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                eCajaChica.eDetalleMov_CajaChica obj = new eCajaChica.eDetalleMov_CajaChica();
                if (e.Clicks == 1 && e.Column.FieldName == "dsc_documento")
                {
                    obj = bgvListadoPostRendicion.GetFocusedRow() as eCajaChica.eDetalleMov_CajaChica;
                    if (obj == null) return;

                    if (obj.tipo_documento != null && obj.tipo_documento != "TC045")
                    {
                        frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                        frmModif.MiAccion = Factura.Vista;
                        frmModif.colorVerde = colorVerde;
                        frmModif.colorPlomo = colorPlomo;
                        frmModif.colorFocus = colorFocus;
                        frmModif.colorEventRow = colorEventRow;
                        frmModif.RUC = obj.dsc_ruc;
                        frmModif.tipo_documento = obj.tipo_documento;
                        frmModif.serie_documento = obj.serie_documento;
                        frmModif.numero_documento = obj.numero_documento;
                        frmModif.cod_proveedor = obj.cod_proveedor;
                        frmModif.user = user;
                        frmModif.ShowDialog();
                    }
                    else if (obj.dsc_documento.Substring(0, 2) == "ER")
                    {
                        frmDetalleEntregaRendir frm = new frmDetalleEntregaRendir();
                        frm.MiAccion = DetEntregaRendir.Vista;
                        frm.user = user;
                        frm.colorVerde = colorVerde;
                        frm.colorPlomo = colorPlomo;
                        frm.colorEventRow = colorEventRow;
                        frm.colorFocus = colorFocus;
                        frm.cod_entregarendir = obj.dsc_documento;
                        frm.cod_empresa = lkpEmpresa.EditValue.ToString();
                        frm.ShowDialog();
                    }
                    else
                    {
                        frmMantDocumentoInterno frmModif = new frmMantDocumentoInterno();
                        frmModif.MiAccion = DocInterno.Vista;
                        frmModif.tipo_documento = obj.tipo_documento;
                        frmModif.serie_documento = obj.serie_documento;
                        frmModif.numero_documento = obj.numero_documento;
                        frmModif.cod_proveedor = obj.cod_proveedor;
                        frmModif.cod_empresa = lkpEmpresa.EditValue.ToString();
                        frmModif.CajaChica = "SI";
                        frmModif.user = user;
                        frmModif.ShowDialog();
                    }
                }
                if (e.Clicks == 2 && (e.Column.FieldName != "dsc_documento" && e.Column.FieldName != "flg_PDF" && e.Column.FieldName != "flg_XML"))
                {
                    obj = bgvListadoPostRendicion.GetFocusedRow() as eCajaChica.eDetalleMov_CajaChica;
                    if (obj == null || obj.dsc_tipo == "APERTURA") return;
                    frmDetalleMovimiento frm = new frmDetalleMovimiento();
                    frm.MiAccion = DetMovimiento.Vista;
                    frm.user = user;
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorEventRow = colorEventRow;
                    frm.colorFocus = colorFocus;
                    frm.cod_caja = obj.cod_caja;
                    frm.cod_movimiento = obj.cod_movimiento;
                    frm.cod_empresa = obj.cod_empresa;
                    //frm.eMovCaja = obj;
                    frm.ShowDialog();
                    if (frm.ActualizarListado == "SI") btnBuscar_Click(btnBuscar, new EventArgs());
                }
                if (e.Clicks == 2 && (e.Column.FieldName == "flg_PDF" || e.Column.FieldName == "flg_XML"))
                {
                    obj = bgvListadoPostRendicion.GetFocusedRow() as eCajaChica.eDetalleMov_CajaChica;
                    if (obj == null) { return; }

                    eFacturaProveedor eFact = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(24, obj.tipo_documento, obj.serie_documento, obj.numero_documento, obj.cod_proveedor);

                    if (e.Column.FieldName == "flg_PDF" && (eFact.idPDF == null || eFact.idPDF == ""))
                    {
                        MessageBox.Show("No se cargado ningún PDF", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (e.Column.FieldName == "flg_XML" && (eFact.idXML == null || eFact.idXML == ""))
                    {
                        MessageBox.Show("No se cargado ningún XML", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    //else
                    //{
                    eEmpresa eEmp = blFact.ObtenerDatosEmpresa<eEmpresa>(12, lkpEmpresa.EditValue.ToString());
                    if (eEmp.ClientIdOnedrive == null || eEmp.ClientIdOnedrive == "")
                    { MessageBox.Show("Debe configurar los datos del Onedrive de la empresa asignada", "Onedrive", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    //var app = App.PublicClientApp;
                    ClientId = eEmp.ClientIdOnedrive;
                    TenantId = eEmp.TenantOnedrive;
                    Appl();
                    var app = PublicClientApp;

                    try
                    {
                        blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                        //eEmpresa eEmp = blFact.ObtenerDatosEmpresa<eEmpresa>(12, obj.cod_empresa);
                        string correo = eEmp.UsuarioOnedrive;
                        string password = eEmp.ClaveOnedrive;

                        var securePassword = new SecureString();
                        foreach (char c in password)
                            securePassword.AppendChar(c);

                        authResult = await app.AcquireTokenByUsernamePassword(scopes, correo, securePassword).ExecuteAsync();

                        GraphClient = new Microsoft.Graph.GraphServiceClient(
                        new Microsoft.Graph.DelegateAuthenticationProvider((requestMessage) =>
                        {
                            requestMessage
                                .Headers
                                .Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);
                            return Task.FromResult(0);
                        }));

                        string IdPDF = eFact.idPDF;
                        string IdXML = eFact.idXML;
                        string IdOneDriveDoc = e.Column.FieldName == "flg_PDF" ? IdPDF : IdXML;
                        string Extension = e.Column.FieldName == "flg_PDF" ? ".pdf" : ".xml";

                        var fileContent = await GraphClient.Me.Drive.Items[IdOneDriveDoc].Content.Request().GetAsync();
                        string ruta = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()) + @"\" + (eFact.NombreArchivo + Extension);
                        if (!System.IO.File.Exists(ruta))
                        {
                            using (var fileStream = new FileStream(ruta, FileMode.Create, System.IO.FileAccess.Write))
                                fileContent.CopyTo(fileStream);
                        }

                        if (!System.IO.Directory.Exists(blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()))) System.IO.Directory.CreateDirectory(blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()));
                        System.Diagnostics.Process.Start(ruta);
                        SplashScreenManager.CloseForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hubieron problemas al autenticar las credenciales", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //lblResultado.Text = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
                        return;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvListadoPreRendicion_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2)
                {
                    eCajaChica.eMovimiento_CajaChica obj = new eCajaChica.eMovimiento_CajaChica();
                    obj = gvListadoPreRendicion.GetFocusedRow() as eCajaChica.eMovimiento_CajaChica;
                    if (obj == null || obj.abv_estado == "A") return; 
                    frmDetalleMovimiento frm = new frmDetalleMovimiento();
                    frm.MiAccion = DetMovimiento.Editar;
                    frm.user = user;
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorEventRow = colorEventRow;
                    frm.colorFocus = colorFocus;
                    frm.cod_caja = obj.cod_caja;
                    frm.cod_movimiento = obj.cod_movimiento;
                    frm.cod_empresa = obj.cod_empresa;
                    //frm.eMovCaja = obj;
                    frm.ShowDialog();
                    if (frm.ActualizarListado == "SI") btnBuscar_Click(btnBuscar, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmResumenMovCajaChica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) btnBuscar_Click(btnBuscar, new EventArgs());
        }

        private void btnAperturaCajaChica_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmAperturaCajaChica"] != null)
            {
                Application.OpenForms["frmAperturaCajaChica"].Activate();
            }
            else
            {
                frmAperturaCajaChica frm = new frmAperturaCajaChica();
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.cod_empresa = lkpEmpresa.EditValue.ToString();
                frm.lkpEmpresa.Enabled = user.cod_usuario == "ADMINISTRADOR" ? true : false;
                frm.ShowDialog();
                if (frm.ActualizarListado == "SI")
                {
                    string valorSede = lkpSedeEmpresa.EditValue.ToString();
                    lkpSedeEmpresa.EditValue = null;
                    lkpSedeEmpresa.EditValue = valorSede;
                    if (lkpResponsable.EditValue != null && lkpTipoCaja.EditValue != null) btnBuscar_Click(btnBuscar, new EventArgs());
                }
            }
        }

        private void btnMovCajaChica_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lkpTipoCaja.EditValue == null) { MessageBox.Show("Debe seleccionar una caja", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (Application.OpenForms["frmMovimientosCajaChica"] != null)
            {
                Application.OpenForms["frmMovimientosCajaChica"].Activate();
            }
            else
            {
                frmMovimientosCajaChica frm = new frmMovimientosCajaChica();
                frm.MiAccion = Movimiento.Editar;
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.cod_empresa = lkpEmpresa.EditValue.ToString();
                frm.cod_sede_empresa = lkpSedeEmpresa.EditValue.ToString();
                frm.cod_caja = lkpTipoCaja.EditValue.ToString();
                frm.lkpEmpresa.Enabled = false;
                frm.lkpSedeEmpresaInfoLaboral.Enabled = false;
                frm.lkpTipoCaja.Enabled = false;
                frm.ShowDialog();
                if (frm.ActualizarListado == "SI") btnBuscar_Click(btnBuscar, new EventArgs());
            }
        }

        private void bgvListadoPostRendicion_CustomDrawBandHeader(object sender, DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnasBandHeader(e);
        }

        private void bgvListadoPostRendicion_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eCajaChica.eDetalleMov_CajaChica obj = bgvListadoPostRendicion.GetRow(e.RowHandle) as eCajaChica.eDetalleMov_CajaChica;
                    if (obj == null) return;
                    if (e.Column.FieldName == "flg_PDF" && obj.flg_PDF == "SI")
                    {
                        e.Handled = true;
                        e.Graphics.DrawImage(ImgPDF, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "abv_estado") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_PDF") e.DisplayText = "";
                    if (e.Column.FieldName == "imp_entregado" && obj.imp_entregado == 0) e.DisplayText = "";
                    if (e.Column.FieldName == "imp_monto" && obj.imp_monto == 0) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_documento" && obj.fch_documento.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (obj.fch_documento.ToString().Contains("1/01/0001")) e.Appearance.FontStyleDelta = FontStyle.Bold;
                    if (obj.cod_rendicion == "MAS DE 1" || obj.cod_rendicion == "SOLO 1") e.Appearance.BackColor = Color.LightGray;
                    if (obj.dsc_tipo == "APERTURA") e.Appearance.BackColor = Color.FromArgb(221, 235, 247);
                    //if (obj.dsc_tipo != "SALIDA") { e.Appearance.ForeColor = Color.DarkGoldenrod; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                    if (obj.dsc_tipo != null && obj.dsc_tipo.Trim() != "")
                    {
                        if (obj.dsc_tipo.Trim().Substring(0, 1) == "R") { e.Appearance.ForeColor = Color.Purple; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                        if (obj.dsc_tipo.Trim().Substring(0, 1) == "D") { e.Appearance.ForeColor = Color.DarkGreen; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                    }
                    if (obj.cod_movimiento != null && obj.cod_movimiento.Trim().Substring(0, 2) == "RP") { e.Appearance.BackColor = Color.DarkGray; e.Appearance.ForeColor = Color.DarkBlue; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                    e.DefaultDraw();
                    if (e.Column.FieldName == "abv_estado" && obj.fch_documento.ToString().Contains("1/01/0001") && (obj.dsc_tipo == "SALIDA" || obj.dsc_tipo == "APERTURA"))
                    { 
                        Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        string cellValue = e.CellValue.ToString();
                        if (cellValue == "P") { b = PENDIENTE; } else if (cellValue == "R") { b = RENDIDO; } else { b = APERTURA; }
                        e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void bgvListaCajaRendida_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void bgvListaCajaRendida_CustomDrawBandHeader(object sender, DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnasBandHeader(e);
        }

        private void bgvListaCajaRendida_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eCajaChica.eDetalleMov_CajaChica obj = bgvListaCajaRendida.GetRow(e.RowHandle) as eCajaChica.eDetalleMov_CajaChica;
                    if (obj == null) return;
                    if (e.Column.FieldName == "flg_PDF" && obj.flg_PDF == "SI")
                    {
                        e.Handled = true;
                        e.Graphics.DrawImage(ImgPDF, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "abv_estado") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_PDF") e.DisplayText = "";
                    if (e.Column.FieldName == "imp_entregado" && obj.imp_entregado == 0) e.DisplayText = "";
                    if (e.Column.FieldName == "imp_monto" && obj.imp_monto == 0) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_documento" && obj.fch_documento.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (obj.fch_documento.ToString().Contains("1/01/0001")) e.Appearance.FontStyleDelta = FontStyle.Bold;
                    if (obj.cod_rendicion == "MAS DE 1" || obj.cod_rendicion == "SOLO 1") e.Appearance.BackColor = Color.LightGray;
                    if (obj.dsc_tipo == "APERTURA") { e.Appearance.BackColor = Color.FromArgb(221, 235, 247); e.Appearance.ForeColor = Color.DarkGoldenrod; }
                    if (obj.periodo_tributario != null && obj.tipo_documento != null) e.Appearance.ForeColor = Color.Blue;
                    //if (obj.cod_estado_registro == "RVS") { e.Appearance.ForeColor = Color.Purple; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    //if (obj.cod_estado_registro == "REV") { e.Appearance.ForeColor = Color.Green; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    //if (obj.dsc_tipo == "REPOSICION") e.Appearance.BackColor = Color.DarkGray;
                    //if (obj.dsc_tipo != "SALIDA") { e.Appearance.ForeColor = Color.DarkGoldenrod; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                    if (obj.dsc_tipo != null && obj.dsc_tipo.Trim() != "")
                    {
                        if (obj.dsc_tipo.Trim().Substring(0, 1) == "R") { e.Appearance.ForeColor = Color.Purple; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                        if (obj.dsc_tipo.Trim().Substring(0, 1) == "D") { e.Appearance.ForeColor = Color.DarkGreen; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                    }
                    if (obj.cod_movimiento != null && obj.cod_movimiento.Trim().Substring(0, 2) == "RP") { e.Appearance.BackColor = Color.DarkGray; e.Appearance.ForeColor = Color.DarkBlue; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                    e.DefaultDraw();
                    if (e.Column.FieldName == "abv_estado" && obj.fch_documento.ToString().Contains("1/01/0001") && (obj.dsc_tipo == "SALIDA" || obj.dsc_tipo == "APERTURA"))
                    {
                        Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        string cellValue = e.CellValue.ToString();
                        //if (cellValue == "P") { b = PENDIENTE; } else if (cellValue == "R") { b = RENDIDO; } else { b = APERTURA; }
                        b = APERTURA;
                        e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bgvListaCajaRendida_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void btnRendirCajaChica_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lkpTipoCaja.EditValue == null) { MessageBox.Show("Debe seleccionar una caja", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (Application.OpenForms["frmMovimientosCajaChica"] != null)
            {
                Application.OpenForms["frmMovimientosCajaChica"].Activate();
            }
            else
            {
                frmMovimientosCajaChica frm = new frmMovimientosCajaChica();
                frm.MiAccion = Movimiento.Rendir;
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.cod_empresa = lkpEmpresa.EditValue.ToString();
                frm.cod_sede_empresa = lkpSedeEmpresa.EditValue.ToString();
                frm.cod_caja = lkpTipoCaja.EditValue.ToString();
                frm.imp_saldo = Convert.ToDecimal(txtSaldoCaja.EditValue);
                frm.lkpEmpresa.Enabled = false;
                frm.lkpSedeEmpresaInfoLaboral.Enabled = false;
                frm.lkpTipoCaja.Enabled = false;
                frm.lkpTipoMoneda.Enabled = false;
                frm.ShowDialog();
                if (frm.ActualizarListado == "SI") btnBuscar_Click(btnBuscar, new EventArgs());
            }
        }

        private async void bgvListaCajaRendida_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                eCajaChica.eDetalleMov_CajaChica obj = new eCajaChica.eDetalleMov_CajaChica();
                obj = bgvListaCajaRendida.GetFocusedRow() as eCajaChica.eDetalleMov_CajaChica;
                if (obj == null) return;
                if (e.Clicks == 1 && e.Column.FieldName == "dsc_documento")
                {
                    if (obj.tipo_documento != null && obj.tipo_documento != "TC045")
                    {
                        frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                        frmModif.MiAccion = Factura.Vista;
                        frmModif.colorVerde = colorVerde;
                        frmModif.colorPlomo = colorPlomo;
                        frmModif.colorFocus = colorFocus;
                        frmModif.colorEventRow = colorEventRow;
                        frmModif.RUC = obj.dsc_ruc;
                        frmModif.tipo_documento = obj.tipo_documento;
                        frmModif.serie_documento = obj.serie_documento;
                        frmModif.numero_documento = obj.numero_documento;
                        frmModif.cod_proveedor = obj.cod_proveedor;
                        frmModif.user = user;
                        frmModif.ShowDialog();
                    }
                    else if (obj.dsc_documento.Substring(0, 2) == "ER")
                    {
                        frmDetalleEntregaRendir frm = new frmDetalleEntregaRendir();
                        frm.MiAccion = DetEntregaRendir.Vista;
                        frm.user = user;
                        frm.colorVerde = colorVerde;
                        frm.colorPlomo = colorPlomo;
                        frm.colorEventRow = colorEventRow;
                        frm.colorFocus = colorFocus;
                        frm.cod_entregarendir = obj.dsc_documento;
                        frm.cod_empresa = lkpEmpresa.EditValue.ToString();
                        frm.ShowDialog();
                    }
                    else
                    {
                        frmMantDocumentoInterno frmModif = new frmMantDocumentoInterno();
                        frmModif.MiAccion = DocInterno.Vista;
                        frmModif.tipo_documento = obj.tipo_documento;
                        frmModif.serie_documento = obj.serie_documento;
                        frmModif.numero_documento = obj.numero_documento;
                        frmModif.cod_proveedor = obj.cod_proveedor;
                        frmModif.cod_empresa = lkpEmpresa.EditValue.ToString();
                        frmModif.CajaChica = "SI";
                        frmModif.user = user;
                        frmModif.ShowDialog();
                    }
                }
                if (e.Clicks == 1 && e.Column.FieldName == "Sel")
                {
                    if (obj.tipo_documento != null && obj.tipo_documento != "" && obj.tipo_documento != "TC045") obj.Sel = obj.Sel ? false : true;
                    bgvListaCajaRendida.RefreshData();
                }
                if (e.Clicks == 2 && (e.Column.FieldName != "dsc_documento" && e.Column.FieldName != "flg_PDF" && e.Column.FieldName != "flg_XML" && e.Column.FieldName != "Sel"))
                {
                    if (obj == null || obj.dsc_tipo == "APERTURA") return;
                    frmDetalleMovimiento frm = new frmDetalleMovimiento();
                    frm.MiAccion = DetMovimiento.Vista;
                    frm.user = user;
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorEventRow = colorEventRow;
                    frm.colorFocus = colorFocus;
                    frm.cod_caja = obj.cod_caja;
                    frm.cod_movimiento = obj.cod_movimiento;
                    frm.cod_empresa = obj.cod_empresa;
                    frm.btnNuevo.Enabled = false;
                    //frm.eMovCaja = obj;
                    frm.ShowDialog();
                    //if (frm.ActualizarListado == "SI") btnBuscar_Click(btnBuscar, new EventArgs());
                }
                if (e.Clicks == 2 && (e.Column.FieldName == "flg_PDF" || e.Column.FieldName == "flg_XML"))
                {
                    obj = bgvListaCajaRendida.GetFocusedRow() as eCajaChica.eDetalleMov_CajaChica;
                    if (obj == null) { return; }

                    eFacturaProveedor eFact = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(24, obj.tipo_documento, obj.serie_documento, obj.numero_documento, obj.cod_proveedor);

                    if (e.Column.FieldName == "flg_PDF" && (eFact.idPDF == null || eFact.idPDF == ""))
                    {
                        MessageBox.Show("No se cargado ningún PDF", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (e.Column.FieldName == "flg_XML" && (eFact.idXML == null || eFact.idXML == ""))
                    {
                        MessageBox.Show("No se cargado ningún XML", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    //else
                    //{
                    eEmpresa eEmp = blFact.ObtenerDatosEmpresa<eEmpresa>(12, lkpEmpresa.EditValue.ToString());
                    if (eEmp.ClientIdOnedrive == null || eEmp.ClientIdOnedrive == "")
                    { MessageBox.Show("Debe configurar los datos del Onedrive de la empresa asignada", "Onedrive", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    //var app = App.PublicClientApp;
                    ClientId = eEmp.ClientIdOnedrive;
                    TenantId = eEmp.TenantOnedrive;
                    Appl();
                    var app = PublicClientApp;

                    try
                    {
                        blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                        //eEmpresa eEmp = blFact.ObtenerDatosEmpresa<eEmpresa>(12, obj.cod_empresa);
                        string correo = eEmp.UsuarioOnedrive;
                        string password = eEmp.ClaveOnedrive;

                        var securePassword = new SecureString();
                        foreach (char c in password)
                            securePassword.AppendChar(c);

                        authResult = await app.AcquireTokenByUsernamePassword(scopes, correo, securePassword).ExecuteAsync();

                        GraphClient = new Microsoft.Graph.GraphServiceClient(
                        new Microsoft.Graph.DelegateAuthenticationProvider((requestMessage) =>
                        {
                            requestMessage
                                .Headers
                                .Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);
                            return Task.FromResult(0);
                        }));

                        string IdPDF = eFact.idPDF;
                        string IdXML = eFact.idXML;
                        string IdOneDriveDoc = e.Column.FieldName == "flg_PDF" ? IdPDF : IdXML;
                        string Extension = e.Column.FieldName == "flg_PDF" ? ".pdf" : ".xml";

                        var fileContent = await GraphClient.Me.Drive.Items[IdOneDriveDoc].Content.Request().GetAsync();
                        string ruta = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()) + @"\" + (eFact.NombreArchivo + Extension);
                        if (!System.IO.File.Exists(ruta))
                        {
                            using (var fileStream = new FileStream(ruta, FileMode.Create, System.IO.FileAccess.Write))
                                fileContent.CopyTo(fileStream);
                        }

                        if (!System.IO.Directory.Exists(blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()))) System.IO.Directory.CreateDirectory(blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()));
                        System.Diagnostics.Process.Start(ruta);
                        SplashScreenManager.CloseForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hubieron problemas al autenticar las credenciales", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //lblResultado.Text = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
                        return;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void bgvListaCajaRendida_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Sel") return;
            if (e.RowHandle != 0) return;
            eCajaChica.eDetalleMov_CajaChica obj = bgvListaCajaRendida.GetFocusedRow() as eCajaChica.eDetalleMov_CajaChica;
            if (obj == null) return;
            if (e.Column.FieldName == "cod_correlativoSISPAG")
            {
                string mes = "", correlativo = ""; int num_correlativo = 0;
                mes = obj.cod_correlativoSISPAG.Substring(0, 2);
                correlativo = obj.cod_correlativoSISPAG.Substring(2, 4);
                num_correlativo = Convert.ToInt32(correlativo);
                for (int x = 1; x <= bgvListaCajaRendida.RowCount; x++)
                {
                    eCajaChica.eDetalleMov_CajaChica obj2 = bgvListaCajaRendida.GetRow(x) as eCajaChica.eDetalleMov_CajaChica;
                    if (obj2 == null) continue;
                    num_correlativo += 1;
                    obj2.cod_correlativoSISPAG = mes + $"{num_correlativo:0000}";
                }
                bgvListaCajaRendida.RefreshData();
            }
            //eCajaChica.eDetalleMov_CajaChica obj = bgvListaCajaRendida.GetFocusedRow() as eCajaChica.eDetalleMov_CajaChica;
            //if (e.Column.FieldName == "Sel")
            //{
            //    if (obj.tipo_documento != null && obj.tipo_documento != "" && obj.tipo_documento != "TC045") obj.Sel = obj.Sel ? false : true;
            //    bgvListaCajaRendida.RefreshData();
            //}
        }

        private void bgvListaCajaRendida_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                //eCajaChica.eDetalleMov_CajaChica obj = bgvListaCajaRendida.GetFocusedRow() as eCajaChica.eDetalleMov_CajaChica;
                //if (obj == null) return;
                if (bgvListaCajaRendida.FocusedColumn != null && bgvListaCajaRendida.FocusedColumn.FieldName != "Sel") e.Cancel = true;
                //if (bgvListaCajaRendida.FocusedColumn.FieldName == "Sel")
                //{
                //    if (obj.tipo_documento != null && obj.tipo_documento != "" && obj.tipo_documento != "TC045") obj.Sel = obj.Sel ? false : true;
                //    bgvListaCajaRendida.RefreshData();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            blTrab.CargaCombosLookUp("SedesEmpresa", lkpSedeEmpresa, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, lkpEmpresa.EditValue.ToString());
            lkpSedeEmpresa.EditValue = null; lkpResponsable.EditValue = null; lkpTipoCaja.EditValue = null;
            List<eTrabajador.eInfoLaboral_Trabajador> lista = blTrab.ListarOpcionesTrabajador<eTrabajador.eInfoLaboral_Trabajador>(6, lkpEmpresa.EditValue.ToString());
            if (lista.Count == 1) lkpSedeEmpresa.EditValue = lista[0].cod_sede_empresa;
        }

        private void lkpSedeEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpSedeEmpresa.EditValue != null)
            {
                blCaja.CargaCombosLookUp("Responsable", lkpResponsable, "cod_responsable", "dsc_responsable", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString(), cod_sede_empresa: lkpSedeEmpresa.EditValue.ToString());
                lkpResponsable.EditValue = null; lkpTipoCaja.EditValue = null;
                List<eTrabajador> lista = new List<eTrabajador>();
                lista = blCaja.ListarDatos_CajaChica<eTrabajador>(12, "", "", cod_empresa: lkpEmpresa.EditValue.ToString(), cod_sede_empresa: lkpSedeEmpresa.EditValue.ToString());
                if (lista.Count == 1) lkpResponsable.EditValue = lista[0].cod_trabajador;
                eTrabajador obj = lista.Find(x => x.cod_trabajador == user.cod_trabajador);
                if (obj != null)
                {
                    lkpResponsable.EditValue = user.cod_trabajador;
                }
                else
                {
                    if (lista.Count == 1) lkpResponsable.EditValue = lista[0].cod_trabajador;
                }
            }
        }

        private void lkpTipoCaja_EditValueChanged(object sender, EventArgs e)
        {
            txtImporteCaja.EditValue = 0; txtSaldoCaja.EditValue = 0; dtFecUltimaReposicion.EditValue = null;
            layoutControlItem8.AppearanceItemCaption.ForeColor = Color.DarkGreen;
            txtSaldoCaja.ForeColor = Color.DarkGreen;
        }
        private void ObtenerMovimientos(decimal imp_alertar)
        {
            List<eCajaChica.eMovimiento_CajaChica> listMovimientos = blCaja.ListarDatos_CajaChica<eCajaChica.eMovimiento_CajaChica>(3, lkpTipoCaja.EditValue.ToString(), "");

            listPreRendicion = blCaja.ListarDatos_CajaChica<eCajaChica.eMovimiento_CajaChica>(6, lkpTipoCaja.EditValue.ToString(), "");
            DateTime fch_registro = new DateTime();
            fch_registro = Convert.ToDateTime((from tabla in listPreRendicion
                                              where tabla.cod_tipo == "RP" || tabla.cod_tipo == "AP" //&& tabla.cod_estado == "REN"
                                              select tabla.fch_registro).First());

            decimal imp_salida_REN = (from tabla in listMovimientos
                                      where tabla.cod_tipo == "SA" //&& tabla.fch_registro >= fch_registro //&& tabla.cod_estado == "REN"
                                      select tabla.imp_entregado).Sum();
            decimal imp_devolucion_REN = (from tabla in listMovimientos
                                        where tabla.cod_tipo == "DV" //&& tabla.fch_registro >= fch_registro //&& tabla.cod_estado == "REN"
                                          select tabla.imp_entregado).Sum();
            decimal imp_reembolso_REN = (from tabla in listMovimientos
                                        where tabla.cod_tipo == "RB" //&& tabla.fch_registro >= fch_registro //&& tabla.cod_estado == "REN"
                                         select tabla.imp_entregado).Sum();
            decimal imp_saldo = Convert.ToDecimal(txtImporteCaja.EditValue) - imp_salida_REN + imp_devolucion_REN - imp_reembolso_REN;
            txtSaldoCaja.EditValue = imp_saldo;
            layoutControlItem8.AppearanceItemCaption.ForeColor = imp_saldo < 0 ? Color.DarkRed : Color.DarkGreen;
            txtSaldoCaja.ForeColor = imp_saldo < 0 ? Color.DarkRed : Color.DarkGreen;
            conteo = 51; 
            MostrarAlerta(imp_saldo, imp_alertar); 
        }

        int conteo = 0;
        private void MostrarAlerta(decimal imp_saldo, decimal imp_alertar)
        {
            lblAlertaReposicion.Visibility = LayoutVisibility.Always;
            timer1.Interval = 200;
            conteo = 0;
            timer1.Start();
            if (imp_alertar < imp_saldo) { timer1.Stop(); lblAlertaReposicion.Visibility = LayoutVisibility.Never; }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            conteo = conteo + 1;
            lblAlertaReposicion.Visibility = lblAlertaReposicion.Visibility == LayoutVisibility.Always ? LayoutVisibility.Never : LayoutVisibility.Always;
            if (conteo > 50) { timer1.Stop(); lblAlertaReposicion.Visibility = LayoutVisibility.Always; }
        }

        private void btnNuevoMovimiento_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (lkpTipoCaja.EditValue == null) { MessageBox.Show("Debe seleccionar una caja", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                frmDetalleMovimiento frm = new frmDetalleMovimiento();
                frm.MiAccion = DetMovimiento.Nuevo;
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.cod_caja = lkpTipoCaja.EditValue.ToString();
                frm.cod_empresa = lkpEmpresa.EditValue.ToString();
                frm.ShowDialog();
                if (frm.ActualizarListado == "SI") btnBuscar_Click(btnBuscar, new EventArgs());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lkpResponsable_EditValueChanged(object sender, EventArgs e)
        {
            //blCaja.CargaCombosLookUp("TipoCajaEmpresa", lkpTipoCaja, "cod_caja", "dsc_caja", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString(), cod_sede_empresa: lkpSedeEmpresa.EditValue.ToString());
            if (lkpResponsable.EditValue != null)
            {
                blCaja.CargaCombosLookUp("TipoCajaResponsable", lkpTipoCaja, "cod_caja", "dsc_caja", "", valorDefecto: true, cod_responsable: lkpResponsable.EditValue.ToString(), cod_empresa: lkpEmpresa.EditValue.ToString());
                lkpTipoCaja.EditValue = null;
                List<eCajaChica> lista = new List<eCajaChica>();
                //lista = blCaja.ListarDatos_CajaChica<eCajaChica>(9, "", "", cod_empresa: lkpEmpresa.EditValue.ToString(), cod_sede_empresa: lkpSedeEmpresa.EditValue.ToString());
                lista = blCaja.ListarDatos_CajaChica<eCajaChica>(13, "", "", cod_responsable: lkpResponsable.EditValue.ToString(), cod_empresa: lkpEmpresa.EditValue.ToString());
                if (lista.Count == 1) lkpTipoCaja.EditValue = lista[0].cod_caja;
            }
        }

        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            GridView view = new GridView();
            switch (xtraTabControl1.SelectedTabPage.Name)
            {
                case "xtabPreRendicion": view = gvListadoPreRendicion; break;
                case "xtabPostRendicion": view = bgvListadoPostRendicion; break;
                case "xtabRendidos": view = bgvListaCajaRendida; break;
            }
            ExportarExcel(view);
        }
        private void ExportarExcel(GridView view)
        {
            try
            {
                string carpeta = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\CajaChica" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

                view.ExportToXlsx(archivo);
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
            GridView view = new GridView();
            switch (xtraTabControl1.SelectedTabPage.Name)
            {
                case "xtabPreRendicion": view = gvListadoPreRendicion; break;
                case "xtabPostRendicion": view = bgvListadoPostRendicion; break;
                case "xtabRendidos": view = bgvListaCajaRendida; break;
            }
            view.ShowPrintPreview();
        }

        private async void btnContabilizarDocumento_ItemClick(object sender, ItemClickEventArgs e)
        {
            bgvListaCajaRendida.RefreshData(); bgvListaCajaRendida.PostEditor();
            List<eCajaChica.eDetalleMov_CajaChica> lista = new List<eCajaChica.eDetalleMov_CajaChica>();
            lista = listCajaRendida.FindAll(x => x.Sel && (x.tipo_documento != null && x.tipo_documento != "TC045"));

            if (lista.Count == 0) { MessageBox.Show("Debe seleccionar un registro.", "Contabilizar documentos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (lista.Count == 1)
            {
                eCajaChica.eDetalleMov_CajaChica obj = lista[0];
                if (obj.cod_estado_contabilizado == "CON")
                {
                    MessageBox.Show("El documento ya se encuentra contabilizado.", "Contabilizar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Ingrese el periodo tributario";
            DateEdit dtFecha = new DateEdit(); dtFecha.Width = 100; args.DefaultResponse = DateTime.Today;
            dtFecha.Properties.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.MonthView;
            dtFecha.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
            dtFecha.Properties.Mask.EditMask = "MMMM-yyyy";
            dtFecha.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            dtFecha.Properties.Mask.UseMaskAsDisplayFormat = true;
            args.Editor = dtFecha;
            var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);

            if ((res == DialogResult.OK || res == DialogResult.Yes) && dtFecha.EditValue != null)
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Contabilizando documentos", "Cargando...");
                foreach (eCajaChica.eDetalleMov_CajaChica objCJ in lista)
                {
                    eFacturaProveedor objTrib = blFact.Obtener_PeriodoTributario<eFacturaProveedor>(50, Convert.ToDateTime(dtFecha.EditValue).ToString("MM-yyyy"), lkpEmpresa.EditValue.ToString());
                    if (objTrib != null && objTrib.flg_cerrado == "SI") { MessageBox.Show("El periodo elegido ya se encuentra CERRADO", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                    eFacturaProveedor objF = new eFacturaProveedor();
                    if (objCJ.cod_estado_contabilizado == "CON" || objCJ.cod_estado_contabilizado == "PEN") continue;
                    objF.tipo_documento = objCJ.tipo_documento; objF.serie_documento = objCJ.serie_documento;
                    objF.numero_documento = objCJ.numero_documento; objF.cod_proveedor = objCJ.cod_proveedor; objF.cod_empresa = objCJ.cod_empresa;
                    objF.cod_estado_registro = "CON"; objF.cod_usuario_registro = user.cod_usuario; objF.cod_usuario_contabilizado = user.cod_usuario;
                    objF.periodo_tributario = Convert.ToDateTime(dtFecha.EditValue).ToString("MM-yyyy"); //Convert.ToDateTime(dtFecha.EditValue);
                    string result = blFact.Actualiar_EstadoRegistroFactura(objF);
                    if (result != "OK") { MessageBox.Show("Error al contabilizar documento", "Contabilizar documentos", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    //if (objF.imp_percepcion > 0)
                    //{
                    //    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objProg = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                    //    if (objF.imp_saldo != 0)
                    //    {
                    //        objProg.tipo_documento = objF.tipo_documento; objProg.serie_documento = objF.serie_documento;
                    //        objProg.numero_documento = objF.numero_documento; objProg.cod_proveedor = objF.cod_proveedor;
                    //        objProg.num_linea = 0;
                    //        objProg.imp_pago = objF.imp_saldo; objProg.cod_tipo_prog = objF.tipo_documento == "TC006" ? "NOTACRED" : "REGULAR";
                    //        objProg.cod_formapago = objF.tipo_documento == "TC006" ? "NOTACRED" : "TRANF";
                    //        objProg.fch_pago = objF.fch_pago_programado; objProg.dsc_observacion = null;
                    //        objProg.cod_estado = objF.tipo_documento == "TC006" ? "EJE" : objF.cod_estado_pago == "PAG" ? "EJE" : "PRO"; objProg.cod_pagar_a = "PROV";
                    //        objProg.fch_ejecucion = objF.cod_estado_pago == "PAG" ? objF.fch_pago_ejecutado : new DateTime(); objProg.cod_usuario_ejecucion = null; objProg.cod_usuario_registro = user.cod_usuario;
                    //        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                    //        eProgFact = blFact.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(objProg);
                    //        if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    }
                    //}

                    ///// MOVEMOS LOS ARCHIVOS A LA CARPETA DEL PERIODO TRIBUTARIO EN EL ONEDRIVE
                    if (objF.idPDF != null || objF.idPDF != "" || objF.idXML != null || objF.idXML != "") await MoverArchivoOneDrive(objF, Convert.ToDateTime(dtFecha.EditValue), objF.idPDF != null && objF.idPDF != "" ? true : false, objF.idXML != null && objF.idXML != "" ? true : false);
                }
                SplashScreenManager.CloseForm();
                XtraMessageBox.Show("Se contabilizaron los documentos de manera satisfactoria", "Contabilizar documentos", MessageBoxButtons.OK);
                btnBuscar_Click(btnBuscar, new EventArgs());
            }
            else
            {
                MessageBox.Show("Debe ingresar el periodo tributario para contabilizar los documentos", "Contabilizar documentos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task MoverArchivoOneDrive(eFacturaProveedor obj, DateTime FechaPeriodo, bool PDF, bool XML)
        {
            try
            {
                //eFacturaProveedor obj = bgvListaCajaRendida.GetRow(nRow) as eFacturaProveedor;
                obj.periodo_tributario = FechaPeriodo.ToString("MM-yyyy");
                if (bgvListaCajaRendida.SelectedRowsCount == 1 && (obj.periodo_tributario == null || obj.periodo_tributario == "")) { MessageBox.Show("Debe asignar un periodo tributario para mover los archivos adjuntos", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                if (obj.periodo_tributario == null || obj.periodo_tributario == "") return;
                string dsc_Carpeta = "Caja Chica";
                int Anho = Convert.ToInt32(obj.periodo_tributario.Substring(3, 4)); int Mes = Convert.ToInt32(obj.periodo_tributario.Substring(0, 2)); string NombreMes = Convert.ToDateTime(obj.periodo_tributario).ToString("MMMM");
                string IdArchivoAnho = "", IdArchivoMes = "";
                varNombreArchivo = obj.NombreArchivo;

                eEmpresa eEmp = blFact.ObtenerDatosEmpresa<eEmpresa>(12, obj.cod_empresa);
                if (eEmp.ClientIdOnedrive == null || eEmp.ClientIdOnedrive == "")
                { MessageBox.Show("Debe configurar los datos del Onedrive de la empresa asignada", "Onedrive", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                ClientId = eEmp.ClientIdOnedrive;
                TenantId = eEmp.TenantOnedrive;
                Appl();
                var app = PublicClientApp;
                ////var app = App.PublicClientApp;
                string correo = eEmp.UsuarioOnedrive;
                string password = eEmp.ClaveOnedrive;

                var securePassword = new SecureString();
                foreach (char c in password)
                    securePassword.AppendChar(c);

                authResult = await app.AcquireTokenByUsernamePassword(scopes, correo, securePassword).ExecuteAsync();

                GraphClient = new Microsoft.Graph.GraphServiceClient(
                  new Microsoft.Graph.DelegateAuthenticationProvider((requestMessage) =>
                  {
                      requestMessage
                          .Headers
                          .Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);
                      return Task.FromResult(0);
                  }));

                //var targetItemFolderId = eEmp.idCarpetaFacturasOnedrive;
                eEmpresa.eOnedrive_Empresa eDatos = new eEmpresa.eOnedrive_Empresa();
                eDatos = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(26, obj.cod_empresa, Convert.ToInt32(obj.periodo_tributario.Substring(3, 4)), dsc_Carpeta: dsc_Carpeta);
                var targetItemFolderId = eDatos.idCarpeta;

                //eFacturaProveedor IdCarpetaAnho = blFact.ObtenerDatosOneDrive<eFacturaProveedor>(13, lkpEmpresaProveedor.EditValue.ToString(), Convert.ToDateTime(dtFechaRegistro.EditValue).Year);
                eEmpresa.eOnedrive_Empresa IdCarpetaAnho = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(13, obj.cod_empresa, Convert.ToInt32(obj.periodo_tributario.Substring(3, 4)), dsc_Carpeta: dsc_Carpeta);
                if (IdCarpetaAnho == null) //Si no existe folder lo crea
                {
                    var driveItem = new Microsoft.Graph.DriveItem
                    {
                        Name = Anho.ToString(),
                        Folder = new Microsoft.Graph.Folder
                        {
                        },
                        AdditionalData = new Dictionary<string, object>()
                                {
                                {"@microsoft.graph.conflictBehavior", "rename"}
                                }
                    };

                    var driveItemInfo = await GraphClient.Me.Drive.Items[targetItemFolderId].Children.Request().AddAsync(driveItem);
                    IdArchivoAnho = driveItemInfo.Id;
                }
                else //Si existe folder obtener id
                {
                    IdArchivoAnho = IdCarpetaAnho.idCarpetaAnho;
                }
                var targetItemFolderIdAnho = IdArchivoAnho;

                //eFacturaProveedor IdCarpetaMes = blFact.ObtenerDatosOneDrive<eFacturaProveedor>(14, lkpEmpresaProveedor.EditValue.ToString(), Mes: Convert.ToDateTime(dtFechaRegistro.EditValue).Month);
                eEmpresa.eOnedrive_Empresa IdCarpetaMes = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(14, obj.cod_empresa, Convert.ToInt32(obj.periodo_tributario.Substring(3, 4)), Convert.ToInt32(obj.periodo_tributario.Substring(0, 2)), dsc_Carpeta);
                if (IdCarpetaMes == null)
                {
                    var driveItem = new Microsoft.Graph.DriveItem
                    {
                        //Name = Mes.ToString() + ". " + NombreMes.ToUpper(),
                        Name = $"{Mes:00}" + ". " + NombreMes.ToUpper(),
                        Folder = new Microsoft.Graph.Folder
                        {
                        },
                        AdditionalData = new Dictionary<string, object>()
                                {
                                {"@microsoft.graph.conflictBehavior", "rename"}
                                }
                    };

                    var driveItemInfo = await GraphClient.Me.Drive.Items[targetItemFolderIdAnho].Children.Request().AddAsync(driveItem);
                    IdArchivoMes = driveItemInfo.Id;
                }
                else //Si existe folder obtener id
                {
                    IdArchivoMes = IdCarpetaMes.idCarpetaMes;
                }


                for (int x = 0; x < 2; x++)
                {
                    if (x == 0 && !PDF) continue;
                    if (x == 1 && !XML) continue;
                    //MOVER ARCHIVO A OTRA CARPETA DEL ONEDRIVE
                    var DriveItem = new Microsoft.Graph.DriveItem
                    {
                        ParentReference = new Microsoft.Graph.ItemReference
                        {
                            Id = IdArchivoMes
                        },
                        //Name = varNombreArchivo + (x == 0 ? ".pdf" : ".xml") //Se comenta para que siga MANTENIENDO EL NOMBRE ASIGNADO
                    };

                    await GraphClient.Me.Drive.Items[x == 0 ? obj.idPDF : obj.idXML].Request().UpdateAsync(DriveItem);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void chkSel_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void btnExportarFormatoSISPAG_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportarReporteSISPAG();
        }

        private void ExportarReporteSISPAG()
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
            string procedure = "";

            ListSeparator = ConfigurationManager.AppSettings["ListSeparator"];
            Excel.Application objExcel = new Excel.Application();
            objExcel.Workbooks.Add();
            //objExcel.Visible = true;
            var workbook = objExcel.ActiveWorkbook;
            var sheet = workbook.Sheets["Hoja1"];

            try
            {
                //////////////////////////////////////////////////////////////////////////////////HOJA REPORTE DE FILES//////////////////////////////////////////////////////////////////////////////////////
                objExcel.Sheets.Add();
                var worksheet = workbook.ActiveSheet;
                worksheet.Name = "Importacion_SISPAG";
                objExcel.ActiveWindow.DisplayGridlines = false;
                //procedure = "usp_Reporte_ResumenConcar @cod_empresa = '" + (lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString()) +
                //                                    "', @tipo_documento = '', @cod_estado_registro = '', @cod_estado_pago = '" +
                //                                    "', @cod_tipo_fecha = '01', @FechaInicio = '20210101" + 
                //                                    "', @FechaFin = '" + DateTime.Today.ToString("yyyyMMdd") +
                //                                    "', @flg_CajaChica = 'SI', @flg_EntregasRendir = 'NO'";
                //blFact.pDatosAExcel(cnxl, objExcel, procedure, "Consulta", "A" + 1, true);

                int fila = 0;
                for (int x = 0; x <= bgvListaCajaRendida.RowCount; x++)
                {
                    eCajaChica.eDetalleMov_CajaChica obj = bgvListaCajaRendida.GetRow(x) as eCajaChica.eDetalleMov_CajaChica;
                    if (obj == null) continue;
                    fila = fila + 1;
                    procedure = "usp_Reporte_ResumenConcar @cod_proveedor = '" + obj.cod_proveedor +
                                                    "', @tipo_documento = '" + obj.tipo_documento +
                                                    "', @serie_documento = '" + obj.serie_documento +
                                                    "', @numero_documento = '" + obj.numero_documento +
                                                    "', @cod_correlativoSISPAG = '" + obj.cod_correlativoSISPAG + "'";
                    blFact.pDatosAExcel(cnxl, objExcel, procedure, "Consulta", "A" + fila, true);
                    if (fila > 1) objExcel.Rows[fila].Delete();
                    fila = objExcel.Cells.Find("*", System.Reflection.Missing.Value,
                    System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
                }

                objExcel.Range["A:A"].Delete();
                objExcel.Range["A1"].Select();
                fila = objExcel.Cells.Find("*", System.Reflection.Missing.Value,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
                worksheet.Rows(2).Insert();
                worksheet.Rows(2).Insert();
                fila = fila + 2;

                objExcel.Range["A1:AR1"].Select();
                objExcel.Selection.Borders.Color = System.Drawing.Color.FromArgb(0, 0, 0);
                objExcel.Selection.Font.Bold = true;
                objExcel.Selection.Font.Color = System.Drawing.Color.Black;
                objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#FFC000");
                objExcel.Range["A1:AR" + fila].Font.Name = "Century Gothic";
                objExcel.Range["A1:AR" + fila].Font.Size = 10;

                objExcel.Range["A1:AR" + fila].Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AR" + fila].Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AR" + fila].Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AR" + (fila + 1)].Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AR" + fila].Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AR" + fila].Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AR1"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                objExcel.Range["A1:AR1"].Borders.Color = System.Drawing.Color.FromArgb(0, 0, 0);

                objExcel.Range["A1"].RowHeight = 70;
                objExcel.Range["A1:AR" + fila].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                objExcel.Range["A1:AR" + fila].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                objExcel.Range["A1:AR" + fila].WrapText = true;
                objExcel.Range["A1:AR1"].AutoFilter(1, Type.Missing, Excel.XlAutoFilterOperator.xlAnd, Type.Missing, true);
                objExcel.Range["A1"].Select();

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
                MessageBox.Show(ex.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEliminarMovimiento_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Esta seguro de eliminar el movimiento?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    eCajaChica.eMovimiento_CajaChica obj = gvListadoPreRendicion.GetFocusedRow() as eCajaChica.eMovimiento_CajaChica;
                    if (obj == null) return;
                    List<eFacturaProveedor> listFacturas = blCaja.ListarDatos_CajaChica<eFacturaProveedor>(5, obj.cod_caja, obj.cod_movimiento);
                    listFacturas.RemoveAll(x => x.dsc_documento.Substring(0, 2) == "RB" || x.dsc_documento.Substring(0, 2) == "DV");
                    if (listFacturas.Count > 0) { MessageBox.Show("No se puede eliminar un movimiento con documentos vinculados", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                    if (obj.cod_tipo == "RP") { MessageBox.Show("No se puede eliminar una reposición", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                    string result = blCaja.Eliminar_MovimientoCajaChica(1, obj);
                    if (result != "OK") MessageBox.Show("Error al eliminar movimiento", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (result == "OK") MessageBox.Show("Se eliminó el movimiento de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBuscar_Click(btnBuscar, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvFacturasProveedor_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void bgvListadoPostRendicion_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void bgvListadoPostRendicion_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

    }
}