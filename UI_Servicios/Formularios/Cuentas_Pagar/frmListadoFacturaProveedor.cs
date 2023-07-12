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
using DevExpress.XtraSplashScreen;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using System.Security;
using System.Net.Http.Headers;
using DevExpress.XtraEditors;
using System.Globalization;
using UI_Servicios.Formularios.Clientes_Y_Proveedores.Proveedores;
using UI_Servicios.Formularios.Cuentas_Pagar;
using Microsoft.Identity.Client;
using DevExpress.Utils;
using Excel = Microsoft.Office.Interop.Excel;
using FileHelpers;

namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    public partial class frmListadoFacturaProveedor : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        blEncrypta blEncryp = new blEncrypta();
        blSistema blSist = new blSistema();
        TaskScheduler scheduler;
        Timer oTimerLoadMtto;
        public blFactura blFact = new blFactura();
        blProveedores blProv = new blProveedores();
        Image ImgPDF = DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttopdf_16x16.png");
        Image ImgXML = DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttoxml_16x16.png");
        Image ImgPago = DevExpress.Images.ImageResourceCache.Default.GetImage("images/miscellaneous/currency_16x16.png");        
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        Brush ConCriterios = Brushes.Green;
        Brush SinCriterios = Brushes.Red;
        Brush NAplCriterio = Brushes.Orange;
        Brush Mensaje = Brushes.Transparent;
        int markWidth = 16;

        //OneDrive
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        string varPathOrigen = "";
        string varNombreArchivo = "";

        public frmListadoFacturaProveedor()
        {
            InitializeComponent();
            oTimerLoadMtto = new Timer();
            oTimerLoadMtto.Interval = 500;
            oTimerLoadMtto.Tick += oTimerLoadMtto_Tick;
        }

        private void oTimerLoadMtto_Tick(object sender, EventArgs e)
        {
            try
            {
                oTimerLoadMtto.Stop();
                HabilitarBotones();
                Inicializar();
                barButtonItem3.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Inicializar()
        {
            try
            {
                CargarLookUpEdit();
                //Fecha
                DateTime date = DateTime.Now;
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
                dtFechaInicio.EditValue = oPrimerDiaDelMes;
                dtFechaFin.EditValue = oUltimoDiaDelMes;
                chkcbTipoDocumento.CheckAll();
                chkcbEstadoRegistro.CheckAll();
                chkcbEstadoPago.CheckAll();
                BuscarFacturas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = blSist.ListarMenuxUsuario<eVentana>(user.cod_usuario, this.Name);

            if (listPermisos.Count > 0)
            {
                grupoEdicion.Enabled = listPermisos[0].flg_escritura;
                //grupoAcciones.Enabled = listPermisos[0].flg_escritura;
                //btnAprobarDocumento.Enabled = listPermisos[0].flg_escritura;
            }
            List<eVentana> listPerfil = blSist.ListarPerfilesUsuario<eVentana>(4, user.cod_usuario);
            eVentana oPerfil = listPerfil.Find(x => x.cod_perfil == 4 || x.cod_perfil == 18 || x.cod_perfil == 5);
            btnContabilizarDocumento.Enabled = oPerfil != null ? true : false;
            btnCerrarPeriodoContable.Enabled = oPerfil != null ? true : false;
            btnExportarFormatoSISPAG.Enabled = oPerfil != null ? true : false;
            eVentana oPerfilApro = listPerfil.Find(x => x.cod_perfil == 1 || x.cod_perfil == 5);
            btnAprobarDocumento.Enabled = oPerfilApro != null ? true : false;
        }
        private void CargarLookUpEdit()
        {
            try
            {
                blFact.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: user.cod_usuario);
                //CargarCombosGridLookup("TipoComprobante", glkpTipoDocumento, "cod_tipo_comprobante", "dsc_tipo_comprobante", "", valorDefecto: true);
                blFact.CargaCombosChecked("TipoDocumento", chkcbTipoDocumento, "cod_tipo_comprobante", "dsc_tipo_comprobante", "");
                blFact.CargaCombosLookUp("TipoFecha", lkpTipoFecha, "cod_tipo_fecha", "dsc_tipo_fecha", "", valorDefecto: true);
                blFact.CargaCombosChecked("EstadoRegistro", chkcbEstadoRegistro, "cod_estado_registro", "dsc_estado_registro", "");
                blFact.CargaCombosChecked("EstadoPago", chkcbEstadoPago, "cod_estado_pago", "dsc_estado_pago", "");

                List<eFacturaProveedor> list = blProv.ListarEmpresasProveedor<eFacturaProveedor>(11, "", user.cod_usuario);
                if (list.Count >= 1) lkpEmpresa.EditValue = list[0].cod_empresa;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargarCombosGridLookup(string nCombo, GridLookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", string cod_condicion = "", bool valorDefecto = false)
        {
            DataTable tabla = new DataTable();
            tabla = blFact.ObtenerListadoGridLookup(nCombo, cod_condicion);

            combo.Properties.DataSource = tabla;
            combo.Properties.ValueMember = campoValueMember;
            combo.Properties.DisplayMember = campoDispleyMember;
            if (campoSelectedValue == "") { combo.EditValue = null; } else { combo.EditValue = campoSelectedValue; }
            if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
        }

        private void frmListadoFacturaProveedor_Load(object sender, EventArgs e)
        {
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            oTimerLoadMtto.Start();
        }

        private void btnNuevaFactura_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
            if (Application.OpenForms["frmMantFacturaProveedor"] != null)
            {
                Application.OpenForms["frmMantFacturaProveedor"].Activate();
            }
            else
            {
                frmModif.MiAccion = Factura.Nuevo;
                frmModif.colorVerde = colorVerde;
                frmModif.colorPlomo = colorPlomo;
                frmModif.colorFocus = colorFocus;
                frmModif.colorEventRow = colorEventRow;
                frmModif.user = user;
                frmModif.ShowDialog();
                if (frmModif.ActualizarListado) BuscarFacturas();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                BuscarFacturas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void BuscarFacturas()
        {
            try
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo documentos", "Cargando...");
                List<eFacturaProveedor> lista = blFact.FiltroFactura<eFacturaProveedor>(1, lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(),
                                                                                        chkcbTipoDocumento.EditValue == null ? "" : chkcbTipoDocumento.EditValue.ToString(),
                                                                                        chkcbEstadoRegistro.EditValue == null ? "" : chkcbEstadoRegistro.EditValue.ToString(),
                                                                                        chkcbEstadoPago.EditValue == null ? "" : chkcbEstadoPago.EditValue.ToString(),
                                                                                        lkpTipoFecha.EditValue == null ? "" : lkpTipoFecha.EditValue.ToString(),
                                                                                        Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                                        Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"),
                                                                                        aplicar_conversion: chkAplicarConversion.CheckState == CheckState.Checked ? "SI" : "NO",
                                                                                        cod_moneda: grdbConversionMoneda.SelectedIndex == 0 ? "SOL" : "DOL");
                bsFacturasProveedor.DataSource = lista;

                List<eFacturaProveedor> listaCompleta = blFact.FiltroFactura<eFacturaProveedor>(1, lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(),
                                                                                        chkcbTipoDocumento.EditValue == null ? "" : chkcbTipoDocumento.EditValue.ToString(),
                                                                                        chkcbEstadoRegistro.EditValue == null ? "" : chkcbEstadoRegistro.EditValue.ToString(),
                                                                                        chkcbEstadoPago.EditValue == null ? "" : chkcbEstadoPago.EditValue.ToString(),
                                                                                        lkpTipoFecha.EditValue == null ? "" : lkpTipoFecha.EditValue.ToString(),
                                                                                        Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                                        Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"),
                                                                                        aplicar_conversion: chkAplicarConversion.CheckState == CheckState.Checked ? "SI" : "NO",
                                                                                        cod_moneda: grdbConversionMoneda.SelectedIndex == 0 ? "SOL" : "DOL",
                                                                                        flg_CajaChica: "", flg_EntregasRendir: "");
                bsFacturasProveedor.DataSource = lista; bsListadoCompleto.DataSource = listaCompleta;
                LlenarResumen();
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void LlenarResumen()
        {
            List<eFacturaProveedor> listaGrafico = blFact.FiltroFactura<eFacturaProveedor>(5, lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(),
                                                                                        chkcbTipoDocumento.EditValue == null ? "" : chkcbTipoDocumento.EditValue.ToString(),
                                                                                        chkcbEstadoRegistro.EditValue == null ? "" : chkcbEstadoRegistro.EditValue.ToString(),
                                                                                        chkcbEstadoPago.EditValue == null ? "" : chkcbEstadoPago.EditValue.ToString(),
                                                                                        lkpTipoFecha.EditValue == null ? "" : lkpTipoFecha.EditValue.ToString(),
                                                                                        Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                                        Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));
            bsGraficoResumen.DataSource = listaGrafico;

            List<eFacturaProveedor> listaMontosDocs = blFact.FiltroFactura<eFacturaProveedor>(6, lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(),
                                                                                        chkcbTipoDocumento.EditValue == null ? "" : chkcbTipoDocumento.EditValue.ToString(),
                                                                                        chkcbEstadoRegistro.EditValue == null ? "" : chkcbEstadoRegistro.EditValue.ToString(),
                                                                                        chkcbEstadoPago.EditValue == null ? "" : chkcbEstadoPago.EditValue.ToString(),
                                                                                        lkpTipoFecha.EditValue == null ? "" : lkpTipoFecha.EditValue.ToString(),
                                                                                        Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                                        Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));
            eFacturaProveedor objSoles = listaMontosDocs.Find(x => x.cod_moneda == "SOL" && x.nOrden == 0);
            tlbiCantDocumentoSoles.Elements[1].Text = objSoles == null ? "0" : objSoles.cant_documentos.ToString();
            eFacturaProveedor objSoles1 = listaMontosDocs.Find(x => x.cod_moneda == "SOL" && x.nOrden == 1);
            tlbiMontoPendienteSoles.Elements[1].Text = objSoles1 == null ? $"{0: 0.00}" : $"{objSoles1.imp_no_pagado.ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}";
            eFacturaProveedor objSoles2 = listaMontosDocs.Find(x => x.cod_moneda == "SOL" && x.nOrden == 2);
            tlbiMontoPagadoSoles.Elements[1].Text = objSoles2 == null ? $"{0: 0.00}" : $"{objSoles2.imp_pagado.ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}";

            eFacturaProveedor objDolares = listaMontosDocs.Find(x => x.cod_moneda == "DOL" && x.nOrden == 3);
            tlbiCantDocumentoDolares.Elements[1].Text = objDolares == null ? "0" : objDolares.cant_documentos.ToString();
            eFacturaProveedor objDolares1 = listaMontosDocs.Find(x => x.cod_moneda == "DOL" && x.nOrden == 4);
            tlbiMontoPendienteDolares.Elements[1].Text = objDolares1 == null ? $"{0: 0.00}" : $"{objDolares1.imp_no_pagado.ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}";
            eFacturaProveedor objDolares2 = listaMontosDocs.Find(x => x.cod_moneda == "DOL" && x.nOrden == 5);
            tlbiMontoPagadoDolares.Elements[1].Text = objDolares2 == null ? $"{0: 0.00}" : $"{objDolares2.imp_pagado.ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}";

            List<eFacturaProveedor> listaDocumentos = blFact.FiltroFactura<eFacturaProveedor>(7, lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(),
                                                                                        chkcbTipoDocumento.EditValue == null ? "" : chkcbTipoDocumento.EditValue.ToString(),
                                                                                        chkcbEstadoRegistro.EditValue == null ? "" : chkcbEstadoRegistro.EditValue.ToString(),
                                                                                        chkcbEstadoPago.EditValue == null ? "" : chkcbEstadoPago.EditValue.ToString(),
                                                                                        lkpTipoFecha.EditValue == null ? "" : lkpTipoFecha.EditValue.ToString(),
                                                                                        Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                                        Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));
            bsDocumentos.DataSource = listaDocumentos;
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
        private async void gvFacturasProveedor_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                eFacturaProveedor obj = new eFacturaProveedor();
                if (e.Clicks == 2 && (e.Column.FieldName != "flg_PDF" && e.Column.FieldName != "flg_XML"))
                {
                    obj = gvFacturasProveedor.GetFocusedRow() as eFacturaProveedor;
                    if (obj == null) { return; }

                    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor(this);
                    if (Application.OpenForms["frmMantFacturaProveedor"] != null)
                    {
                        Application.OpenForms["frmMantFacturaProveedor"].Activate();
                    }
                    else
                    {
                        frmModif.MiAccion = Factura.Editar;
                        frmModif.colorVerde = colorVerde;
                        frmModif.colorPlomo = colorPlomo;
                        frmModif.colorFocus = colorFocus;
                        frmModif.colorEventRow = colorEventRow;
                        frmModif.RUC = obj.dsc_ruc;
                        frmModif.tipo_documento = obj.tipo_documento;
                        frmModif.serie_documento = obj.serie_documento;
                        frmModif.numero_documento = obj.numero_documento;
                        frmModif.cod_proveedor = obj.cod_proveedor;
                        frmModif.orden_servicio = obj.num_OrdenCompraServ;
                        frmModif.user = user;
                        frmModif.ShowDialog();
                        if (frmModif.ActualizarListado) BuscarFacturas();
                    }
                }
                if (e.Clicks == 2 && (e.Column.FieldName == "flg_PDF" || e.Column.FieldName == "flg_XML"))
                {
                    obj = gvFacturasProveedor.GetFocusedRow() as eFacturaProveedor;
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
                    if(e.Clicks == 2 && (e.Column.FieldName == "dsc_estado_pago"))
                     {

                    }

                    //else
                    //{
                    eEmpresa eEmp = blFact.ObtenerDatosEmpresa<eEmpresa>(12, obj.cod_empresa);
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

        

        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportarExcel();
        }
        private void ExportarExcel()
        {
            try
            {
                string carpeta = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\Documentos" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                if (xtraTabControl1.SelectedTabPage == xtraTabPage1) gvFacturasProveedor.ExportToXlsx(archivo);
                if (xtraTabControl1.SelectedTabPage == xtraTabPage3) gvListadoCompleto.ExportToXlsx(archivo);
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

        private void gvFacturasProveedor_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eFacturaProveedor obj = gvFacturasProveedor.GetRow(e.RowHandle) as eFacturaProveedor;
                    //if (gvFacturasProveedor.IsRowSelected(e.RowHandle) && obj.dsc_estado_documento == "Anulado") e.Appearance.ForeColor = Color.Red;
                    //if (gvFacturasProveedor.IsRowSelected(e.RowHandle) && obj.imp_saldo == 0) e.Appearance.ForeColor = Color.Blue;
                    if (e.Column.FieldName == "flg_detraccion" && !obj.fch_pago_ejecutado_detraccion.ToString().Contains("1/01/0001")) e.Appearance.BackColor = Color.Green;
                    if (obj.dsc_estado_pago == "PAGADO") e.Appearance.ForeColor = Color.Blue;
                    if (obj.dsc_estado_documento == "Anulado") e.Appearance.ForeColor = Color.Red;
                    //if (obj.imp_saldo == 0) e.Appearance.ForeColor = Color.Blue;
                    if (e.Column.FieldName == "fch_pago_programado" && obj.fch_pago_programado < DateTime.Today && obj.imp_saldo != 0) e.Appearance.BackColor = Color.LightSalmon;
                    if (e.Column.FieldName == "periodo_tributario" && obj.cod_estado_pago == "PAG" && (obj.periodo_tributario == "" || obj.periodo_tributario == null)) e.Appearance.BackColor = Color.LightSalmon;
                    if (e.Column.FieldName == "fch_pago_ejecutado_detraccion" && obj.fch_pago_ejecutado_detraccion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_pago_ejecutado" && obj.fch_pago_ejecutado.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_constancia_detraccion" && obj.fch_constancia_detraccion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_aprobado_reg" && obj.fch_aprobado_reg.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_contabilizado" && obj.fch_contabilizado.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_anulacion" && obj.fch_anulacion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "CantCuentas" && obj.CantCuentas == "NO") { e.Appearance.ForeColor = Color.Red; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    if (obj.cod_estado_registro == "RVS") { e.Appearance.ForeColor = Color.Purple; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    if (obj.cod_estado_registro == "REV") { e.Appearance.ForeColor = Color.Green; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    if (e.Column.FieldName == "flg_PDF" && obj.flg_PDF == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgPDF, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "flg_XML" && obj.flg_XML == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgXML, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "cod_estado_pago" && obj.cod_estado_pago == "PAG")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgPago, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "cod_estado_pago" && obj.cod_estado_pago != "PAG")
                    {
                        e.DisplayText = "";
                    }
                    if ((e.Column.FieldName == "flg_PDF" && obj.cod_estado_pago != "SI") || (e.Column.FieldName == "flg_XML" && obj.cod_estado_pago != "SI") )
                    {
                        e.DisplayText = "";
                    }

                    e.DefaultDraw();
                    if (e.Column.FieldName == "ctd_CECO")
                    {
                        Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        string cellValue = e.CellValue.ToString();
                        if (cellValue == "NO") { b = SinCriterios; } else if (cellValue == "SI") { b = ConCriterios; } else { b = NAplCriterio; }
                        e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                    //if (e.Column.FieldName == "ctd_CECO") e.DisplayText = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvFacturasProveedor_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                blGlobal.Pintar_EstiloGrilla(sender, e);
                GridView view = sender as GridView;
                if (view.Columns["dsc_estado_pago"] != null || view.Columns["dsc_estado_documento"] != null)
                {
                    string estado = view.GetRowCellDisplayText(e.RowHandle, view.Columns["dsc_estado_documento"]);
                    if (estado == "Anulado") e.Appearance.ForeColor = Color.Red;
                    string estadoReg = view.GetRowCellDisplayText(e.RowHandle, view.Columns["cod_estado_registro"]);
                    if (estadoReg == "RVS") { e.Appearance.ForeColor = Color.Purple; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    if (estadoReg == "REV") { e.Appearance.ForeColor = Color.Green; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    //string estadoPAGO = view.GetRowCellDisplayText(e.RowHandle, view.Columns["dsc_estado_pago"]);
                    //if (estadoPAGO == "PAGADO") e.Appearance.ForeColor = Color.Blue;
                    //if (view.GetRowCellDisplayText(e.RowHandle, view.Columns["imp_saldo"]) == "") return;
                    //decimal saldo = Convert.ToDecimal(view.GetRowCellDisplayText(e.RowHandle, view.Columns["imp_saldo"]));
                    //if (saldo == 0) e.Appearance.ForeColor = Color.Blue;
                }
            }
        }
        
        private void gvDocumentos_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvDocumentos_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvFacturasProveedor_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void frmListadoFacturaProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) BuscarFacturas();
        }

        private void btnVerDatosProveedor_ItemClick(object sender, ItemClickEventArgs e)
        {
            eFacturaProveedor obj = gvFacturasProveedor.GetFocusedRow() as eFacturaProveedor;
            if (obj != null)
            {

                frmMantProveedor frm = new frmMantProveedor();
                frm.cod_proveedor = obj.cod_proveedor;
                frm.MiAccion = Proveedor.Vista;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.cod_empresa = obj.cod_empresa;
                frm.user = user;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un documento", "Datos del Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void chkcbEstadoRegistro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) chkcbEstadoRegistro.EditValue = null;
        }
        
        private void chkcbTipoDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) chkcbTipoDocumento.EditValue = null;
        }

        private void chkcbEstadoPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) chkcbEstadoPago.EditValue = null;
        }

        private void lkpEmpresa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) lkpEmpresa.EditValue = null;
        }
        
        private void btnAprobarDocumento_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvFacturasProveedor.RefreshData(); 
            if (gvFacturasProveedor.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un documento.", "Aprobar documentos", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Aprobando documentos", "Cargando...");
            foreach (int nRow in gvFacturasProveedor.GetSelectedRows())
            {
                eFacturaProveedor obj = gvFacturasProveedor.GetRow(nRow) as eFacturaProveedor;
                if (obj.cod_estado_registro == "APR")
                {
                    if (gvFacturasProveedor.SelectedRowsCount == 1) 
                    { 
                        MessageBox.Show("El documento ya se encuentra APROBADO PARA CONTABILIZAR.", "Aprobar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                    }
                    continue;
                }
                if (obj.cod_estado_registro == "CON")
                {
                    if (gvFacturasProveedor.SelectedRowsCount == 1)
                    {
                        MessageBox.Show("El documento ya se encuentra CONTABILIZADO.", "Aprobar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    continue;
                }
                if (obj.flg_inventario == "NO" && obj.flg_activo_fijo == "NO")
                {
                    List<eFacturaProveedor.eFacturaProveedor_Distribucion> mylistLineasDetFactura = blFact.Obtener_LineasDetalleFactura<eFacturaProveedor.eFacturaProveedor_Distribucion>(4, obj.tipo_documento, obj.serie_documento, obj.numero_documento, obj.cod_proveedor);
                    if (mylistLineasDetFactura.Count == 0)
                    {
                        MessageBox.Show("Debe asignar un centro de costo para APROBAR el documento.", "Aprobar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BuscarFacturas();
                        return;
                    }
                }
                if (obj.cod_estado_registro == "APR" || obj.cod_estado_registro == "CON") continue;
                obj.cod_estado_registro = "APR"; obj.cod_usuario_registro = user.cod_usuario; obj.cod_usuario_aprobado_reg = user.cod_usuario;
                string result = blFact.Actualiar_EstadoRegistroFactura(obj);
                if (result != "OK") { MessageBox.Show("Error al aprobar documento", "Aprobar documentos", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objProg = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                if (obj.imp_saldo == 0) continue;
                objProg.tipo_documento = obj.tipo_documento; objProg.serie_documento = obj.serie_documento; objProg.numero_documento = obj.numero_documento; objProg.cod_proveedor = obj.cod_proveedor;
                objProg.num_linea = 0; objProg.fch_pago = obj.fch_pago_programado; objProg.dsc_observacion = null; 
                objProg.cod_estado = obj.tipo_documento == "TC006" ? "EJE" : obj.cod_estado_pago == "PAG" ? "EJE" : "PRO"; objProg.cod_pagar_a = "PROV";
                objProg.fch_ejecucion = obj.cod_estado_pago == "PAG" ? obj.fch_pago_ejecutado : new DateTime(); objProg.cod_usuario_ejecucion = null; objProg.cod_usuario_registro = user.cod_usuario;
                objProg.cod_tipo_prog = obj.tipo_documento == "TC006" ? "NOTACRED" : "REGULAR"; objProg.cod_formapago = obj.tipo_documento == "TC006" ? "NOTACRED" : "TRANF";
                objProg.imp_pago = obj.imp_saldo; objProg.cod_empresa = obj.cod_empresa;

                eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                eProgFact = blFact.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(objProg);
                if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SplashScreenManager.CloseForm();
            XtraMessageBox.Show("Se aprobaron los documentos de manera satisfactoria", "Aprobar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BuscarFacturas();
        }

        private async void btnContabilizarDocumento_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvFacturasProveedor.RefreshData();
            if (gvFacturasProveedor.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un documento.", "Contabilizar documentos", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (gvFacturasProveedor.SelectedRowsCount == 1)
            {
                foreach (int nRow in gvFacturasProveedor.GetSelectedRows())
                {
                    eFacturaProveedor objFP = gvFacturasProveedor.GetRow(nRow) as eFacturaProveedor;
                    if (objFP.cod_estado_registro == "PEN")
                    {
                        MessageBox.Show("El documento no se encuentra APROBADO PARA CONTABILIZAR.", "Contabilizar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (objFP.cod_estado_registro == "RVS")
                    {
                        MessageBox.Show("El documento se encuentra solicitado para revisión.", "Contabilizar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (objFP.cod_estado_registro == "CON")
                    {
                        MessageBox.Show("El documento ya se encuentra contabilizado.", "Contabilizar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
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
                foreach (int nRow in gvFacturasProveedor.GetSelectedRows())
                {
                    eFacturaProveedor objTrib = blFact.Obtener_PeriodoTributario<eFacturaProveedor>(50, Convert.ToDateTime(dtFecha.EditValue).ToString("MM-yyyy"), lkpEmpresa.EditValue.ToString());
                    if (objTrib != null && objTrib.flg_cerrado == "SI") { MessageBox.Show("El periodo elegido ya se encuentra CERRADO", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                    eFacturaProveedor objF = gvFacturasProveedor.GetRow(nRow) as eFacturaProveedor;
                    if (objF.cod_estado_registro == "CON" || objF.cod_estado_registro == "PEN" || objF.cod_estado_registro == "RVS") continue;
                    objF.cod_estado_registro = "CON"; objF.cod_usuario_registro = user.cod_usuario;  objF.cod_usuario_contabilizado = user.cod_usuario;
                    objF.periodo_tributario = Convert.ToDateTime(dtFecha.EditValue).ToString("MM-yyyy"); //Convert.ToDateTime(dtFecha.EditValue);
                    string result = blFact.Actualiar_EstadoRegistroFactura(objF);
                    if (result != "OK") { MessageBox.Show("Error al contabilizar documento", "Contabilizar documentos", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    if (objF.imp_percepcion > 0)
                    {
                        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objProg = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                        if (objF.imp_saldo != 0)
                        {
                            objProg.tipo_documento = objF.tipo_documento; objProg.serie_documento = objF.serie_documento; 
                            objProg.numero_documento = objF.numero_documento; objProg.cod_proveedor = objF.cod_proveedor;
                            objProg.num_linea = 0; objProg.cod_empresa = objF.cod_empresa;
                            objProg.imp_pago = objF.imp_saldo; objProg.cod_tipo_prog = objF.tipo_documento == "TC006" ? "NOTACRED" : "REGULAR";
                            objProg.cod_formapago = objF.tipo_documento == "TC006" ? "NOTACRED" : "TRANF";
                            objProg.fch_pago = objF.fch_pago_programado; objProg.dsc_observacion = null;
                            objProg.cod_estado = objF.tipo_documento == "TC006" ? "EJE" : objF.cod_estado_pago == "PAG" ? "EJE" : "PRO"; objProg.cod_pagar_a = "PROV";
                            objProg.fch_ejecucion = objF.cod_estado_pago == "PAG" ? objF.fch_pago_ejecutado : new DateTime(); objProg.cod_usuario_ejecucion = null; objProg.cod_usuario_registro = user.cod_usuario;
                            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                            eProgFact = blFact.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(objProg);
                            if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    ///// MOVEMOS LOS ARCHIVOS A LA CARPETA DEL PERIODO TRIBUTARIO EN EL ONEDRIVE
                    if (objF.idPDF != null || objF.idPDF != "" || objF.idXML != null || objF.idXML != "") await Mover_Eliminar_ArchivoOneDrive(nRow, Convert.ToDateTime(dtFecha.EditValue), objF.idPDF != null && objF.idPDF != "" ? true : false, objF.idXML != null && objF.idXML != "" ? true : false, "MOVER");
                }
                SplashScreenManager.CloseForm();
                XtraMessageBox.Show("Se contabilizaron los documentos de manera satisfactoria", "Contabilizar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BuscarFacturas();
            }
            else
            {
                MessageBox.Show("Debe ingresar el periodo tributario para contabilizar los documentos", "Contabilizar documentos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task Mover_Eliminar_ArchivoOneDrive(int nRow, DateTime FechaPeriodo, bool PDF, bool XML, string opcion)
        {
            try
            {
                eFacturaProveedor obj = gvFacturasProveedor.GetRow(nRow) as eFacturaProveedor;
                obj.periodo_tributario = FechaPeriodo.ToString("MM-yyyy");
                if (gvFacturasProveedor.SelectedRowsCount == 1 && (obj.periodo_tributario == null || obj.periodo_tributario == "")) { MessageBox.Show("Debe asignar un periodo tributario para mover los archivos adjuntos", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                if (obj.periodo_tributario == null || obj.periodo_tributario == "") return;
                string dsc_Carpeta = obj.tipo_documento == "TC008" ? "RxH Proveedor" : "Facturas Proveedor";
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
                var targetItemFolderId = opcion != "ELIMINAR" ? eDatos.idCarpeta : "";

                //eFacturaProveedor IdCarpetaAnho = blFact.ObtenerDatosOneDrive<eFacturaProveedor>(13, lkpEmpresaProveedor.EditValue.ToString(), Convert.ToDateTime(dtFechaRegistro.EditValue).Year);
                eEmpresa.eOnedrive_Empresa IdCarpetaAnho = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(13, obj.cod_empresa, Convert.ToInt32(obj.periodo_tributario.Substring(3, 4)), dsc_Carpeta: dsc_Carpeta);
                if (IdCarpetaAnho == null && opcion != "ELIMINAR") //Si no existe folder lo crea
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
                    IdArchivoAnho = opcion != "ELIMINAR" ? IdCarpetaAnho.idCarpetaAnho : "";
                }
                var targetItemFolderIdAnho = IdArchivoAnho;

                //eFacturaProveedor IdCarpetaMes = blFact.ObtenerDatosOneDrive<eFacturaProveedor>(14, lkpEmpresaProveedor.EditValue.ToString(), Mes: Convert.ToDateTime(dtFechaRegistro.EditValue).Month);
                eEmpresa.eOnedrive_Empresa IdCarpetaMes = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(14, obj.cod_empresa, Convert.ToInt32(obj.periodo_tributario.Substring(3, 4)), Convert.ToInt32(obj.periodo_tributario.Substring(0, 2)), dsc_Carpeta);
                if (IdCarpetaMes == null && opcion != "ELIMINAR")
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
                    IdArchivoMes = opcion != "ELIMINAR" ? IdCarpetaMes.idCarpetaMes : "";
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

                    if (opcion == "MOVER") await GraphClient.Me.Drive.Items[x == 0 ? obj.idPDF : obj.idXML].Request().UpdateAsync(DriveItem);
                    if (opcion == "ELIMINAR") await GraphClient.Me.Drive.Items[x == 0 ? obj.idPDF : obj.idXML].Request().DeleteAsync();
                    //if (opcion == "ELIMINAR") await GraphClient.Directory.DeletedItems[x == 0 ? obj.idPDF : obj.idXML].Request().DeleteAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void btnAplicarDetraccion_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                fmFacturasAplicarDetraccion frm = new fmFacturasAplicarDetraccion();
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorFocus = colorFocus;
                frm.colorEventRow = colorEventRow;
                frm.user.cod_usuario = user.cod_usuario;
                List<eFacturaProveedor> newList = new List<eFacturaProveedor>();
                foreach (int nRow in gvFacturasProveedor.GetSelectedRows())
                {
                    eFacturaProveedor obj = gvFacturasProveedor.GetRow(nRow) as eFacturaProveedor;
                    //FECHA PAGO DETRACCIÓN, es el viernes proximo de la fecha de recepción
                    DateTime fecha = new DateTime(Convert.ToDateTime(obj.fch_registro).Year, Convert.ToDateTime(obj.fch_registro).Month + 1, 1);
                    int nDia = 0, cont = 0;
                    for (int x = 0; x <= 9; x++)
                    {
                        nDia = Convert.ToInt32(fecha.AddDays(x).DayOfWeek);
                        if (nDia >= 1 && nDia <= 5) cont = cont + 1;
                        if (cont == 5 && (nDia >= 1 && nDia <= 5)) obj.fch_constancia_detraccion = fecha.AddDays(x);
                    }
                    //obj.fch_constancia_detraccion = DateTime.Today;
                    newList.Add(obj);
                }
                frm.listFactura = newList;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAdjuntarArchivo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dtFechaInicio_EditValueChanged(object sender, EventArgs e)
        {
            ////dtFechaFin.EditValue = Convert.ToDateTime(dtFechaInicio.EditValue).AddDays(30);
            //DateTime date = Convert.ToDateTime(dtFechaInicio.EditValue);
            //DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            //DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            //dtFechaFin.EditValue = oUltimoDiaDelMes;
        }

        private void dtFechaFin_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dtFechaFin.EditValue) < Convert.ToDateTime(dtFechaInicio.EditValue)) dtFechaFin.EditValue = Convert.ToDateTime(dtFechaInicio.EditValue);
        }

        private void gvFacturasProveedor_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            int offset = 5, posInical = 0;
            e.DefaultDraw(); e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush b = Mensaje; Rectangle markRectangle;
            string priorityText = "Leyenda :";
            for (int i = 0; i < 4; i++)
            {
                if (i == 1) { b = ConCriterios; priorityText = " - Documentos Con CECO asignado"; }
                if (i == 2) { b = SinCriterios; priorityText = " - Documentos Sin CECO asignado"; }
                if (i == 3) { b = NAplCriterio; priorityText = " - Documentos de Inventario o Activo Fijo"; }
                //markRectangle = new Rectangle(e.Bounds.X + offset, e.Bounds.Y + offset + (markWidth + offset) * i, markWidth, markWidth);
                //markRectangle = new Rectangle(e.Bounds.X * (i * 200) + offset, e.Bounds.Y + 10, markWidth, markWidth);
                posInical = i == 0 ? 0 : i == 1 ? 120 : i == 2 ? 400 : 680;
                markRectangle = new Rectangle(e.Bounds.X * (posInical) + offset, e.Bounds.Y + 10, markWidth, markWidth);
                e.Graphics.FillEllipse(b, markRectangle);
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
                e.Appearance.Options.UseTextOptions = true;
                e.Appearance.DrawString(e.Cache, priorityText, new Rectangle(markRectangle.Right + offset, markRectangle.Y, e.Bounds.Width, markRectangle.Height));
            }
        }

        private void btnAnularDocumento_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvFacturasProveedor.RefreshData(); gvFacturasProveedor.PostEditor();
                if (gvFacturasProveedor.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un documento.", "Anular documentos", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (MessageBox.Show("¿Esta seguro de anular el documento?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string result = ""; DateTime fecha;
                    List<eVentana> listPerfil = blSist.ListarPerfilesUsuario<eVentana>(4, user.cod_usuario);
                    eVentana oPerfil = listPerfil.Find(x => x.cod_perfil == 5);
                    foreach (int nRow in gvFacturasProveedor.GetSelectedRows())
                    {
                        eFacturaProveedor obj = gvFacturasProveedor.GetRow(nRow) as eFacturaProveedor;
                        fecha = obj.fch_documento.AddDays(7);
                        obj.cod_usuario_anulacion = user.cod_usuario;
                        if (obj.cod_estado_documento == "A")
                        {
                            if (gvFacturasProveedor.SelectedRowsCount == 1)
                            {
                                MessageBox.Show("El documento ya se encuentra anulado.", "Anular documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            continue;
                        }
                        if (oPerfil == null && DateTime.Today > fecha)
                        {
                            if (gvFacturasProveedor.SelectedRowsCount == 1)
                            {
                                MessageBox.Show("No puede anular un documento ya que superó los 7 dias desde su emisión", "Anular documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            continue;
                        }
                        result = blFact.AnularFacturaProveedor(obj);
                    }
                    if (result == "OK") { MessageBox.Show("Se anuló el documento de manera correcta.", "Anular documentos", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    BuscarFacturas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCerrarPeriodoContable_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Ingrese el periodo tributario a cerrar";
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
                    eFacturaProveedor obj = new eFacturaProveedor();
                    obj = blFact.Obtener_PeriodoTributario<eFacturaProveedor>(50, Convert.ToDateTime(dtFecha.EditValue).ToString("MM-yyyy"), lkpEmpresa.EditValue.ToString());
                    if (obj != null && obj.flg_cerrado == "SI") { MessageBox.Show("El periodo elegido ya se encuentra CERRADO", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                    if (obj == null) obj = new eFacturaProveedor();
                    obj.periodo_tributario = Convert.ToDateTime(dtFecha.EditValue).ToString("MM-yyyy"); 
                    obj.flg_cerrado = "SI"; obj.cod_empresa = lkpEmpresa.EditValue.ToString(); obj.cod_usuario_registro = user.cod_usuario;
                    obj = blFact.Insertar_Actualizar_CerrarPeriodoTributario<eFacturaProveedor>(obj);
                    if (obj != null) MessageBox.Show("Se procedió a cerrar el periodo tributario de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chkAplicarConversion_CheckStateChanged(object sender, EventArgs e)
        {
            grdbConversionMoneda.Enabled = chkAplicarConversion.CheckState == CheckState.Checked ? true : false;
        }

        private void gvFacturasProveedor_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle != 0) return;
                eFacturaProveedor obj = gvFacturasProveedor.GetFocusedRow() as eFacturaProveedor;
                if (obj == null) return;
                if (e.Column.FieldName == "cod_correlativoSISPAG")
                {
                    string mes = "", correlativo = ""; int num_correlativo = 0;
                    mes = obj.cod_correlativoSISPAG.Substring(0, 2);
                    correlativo = obj.cod_correlativoSISPAG.Substring(2, 4);
                    num_correlativo = Convert.ToInt32(correlativo);
                    for (int x = 1; x <= gvFacturasProveedor.RowCount; x++)
                    {
                        eFacturaProveedor obj2 = gvFacturasProveedor.GetRow(x) as eFacturaProveedor;
                        if (obj2 == null) continue;
                        num_correlativo += 1;
                        obj2.cod_correlativoSISPAG = mes + $"{num_correlativo:0000}";
                    }
                    gvFacturasProveedor.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoCompleto_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eFacturaProveedor obj = gvListadoCompleto.GetRow(e.RowHandle) as eFacturaProveedor;
                    //if (gvFacturasProveedor.IsRowSelected(e.RowHandle) && obj.dsc_estado_documento == "Anulado") e.Appearance.ForeColor = Color.Red;
                    //if (gvFacturasProveedor.IsRowSelected(e.RowHandle) && obj.imp_saldo == 0) e.Appearance.ForeColor = Color.Blue;
                    if (e.Column.FieldName == "flg_detraccion" && !obj.fch_pago_ejecutado_detraccion.ToString().Contains("1/01/0001")) e.Appearance.BackColor = Color.Green;
                    if (obj.dsc_estado_pago == "PAGADO") e.Appearance.ForeColor = Color.Blue;
                    if (obj.dsc_estado_documento == "Anulado") e.Appearance.ForeColor = Color.Red;
                    //if (obj.imp_saldo == 0) e.Appearance.ForeColor = Color.Blue;
                    if (e.Column.FieldName == "fch_pago_programado" && obj.fch_pago_programado < DateTime.Today && obj.imp_saldo != 0) e.Appearance.BackColor = Color.LightSalmon;
                    if (e.Column.FieldName == "periodo_tributario" && obj.cod_estado_pago == "PAG" && (obj.periodo_tributario == "" || obj.periodo_tributario == null)) e.Appearance.BackColor = Color.LightSalmon;
                    if (e.Column.FieldName == "fch_pago_ejecutado_detraccion" && obj.fch_pago_ejecutado_detraccion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_pago_ejecutado" && obj.fch_pago_ejecutado.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_constancia_detraccion" && obj.fch_constancia_detraccion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_anulacion" && obj.fch_anulacion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "CantCuentas" && obj.CantCuentas == "NO") { e.Appearance.ForeColor = Color.Red; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    if (e.Column.FieldName == "periodo_tributario" && obj.periodo_tributario != "") e.Appearance.ForeColor = Color.DarkGreen;
                    if (e.Column.FieldName == "flg_PDF" && obj.flg_PDF == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgPDF, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "flg_XML" && obj.flg_XML == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgXML, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "cod_estado_pago" && obj.cod_estado_pago == "PAG")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgPago, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "cod_estado_pago" && obj.cod_estado_pago != "PAG")
                    {
                        e.DisplayText = "";
                    }
                    if ((e.Column.FieldName == "flg_PDF" && obj.cod_estado_pago != "SI") || (e.Column.FieldName == "flg_XML" && obj.cod_estado_pago != "SI"))
                    {
                        e.DisplayText = "";
                    }

                    e.DefaultDraw();
                    if (e.Column.FieldName == "ctd_CECO")
                    {
                        Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        string cellValue = e.CellValue.ToString();
                        if (cellValue == "NO") { b = SinCriterios; } else if (cellValue == "SI") { b = ConCriterios; } else { b = NAplCriterio; }
                        e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                    //if (e.Column.FieldName == "ctd_CECO") e.DisplayText = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoCompleto_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoCompleto_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private async void gvListadoCompleto_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                eFacturaProveedor obj = new eFacturaProveedor();
                //if (e.Clicks == 2 && (e.Column.FieldName != "flg_PDF" && e.Column.FieldName != "flg_XML"))
                //{
                //    obj = gvListadoCompleto.GetFocusedRow() as eFacturaProveedor;
                //    if (obj == null) { return; }

                //    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor(this);
                //    if (Application.OpenForms["frmMantFacturaProveedor"] != null)
                //    {
                //        Application.OpenForms["frmMantFacturaProveedor"].Activate();
                //    }
                //    else
                //    {
                //        frmModif.MiAccion = Factura.Editar;
                //        frmModif.colorVerde = colorVerde;
                //        frmModif.colorPlomo = colorPlomo;
                //        frmModif.colorFocus = colorFocus;
                //        frmModif.colorEventRow = colorEventRow;
                //        frmModif.RUC = obj.dsc_ruc;
                //        frmModif.tipo_documento = obj.tipo_documento;
                //        frmModif.serie_documento = obj.serie_documento;
                //        frmModif.numero_documento = obj.numero_documento;
                //        frmModif.cod_proveedor = obj.cod_proveedor;
                //        frmModif.user = user;
                //        frmModif.ShowDialog();
                //        if (frmModif.ActualizarListado) BuscarFacturas();
                //    }
                //}
                if (e.Clicks == 2 && (e.Column.FieldName == "flg_PDF" || e.Column.FieldName == "flg_XML"))
                {
                    obj = gvListadoCompleto.GetFocusedRow() as eFacturaProveedor;
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
                    eEmpresa eEmp = blFact.ObtenerDatosEmpresa<eEmpresa>(12, obj.cod_empresa);
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

        private void gvListadoCompleto_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            int offset = 5;
            e.DefaultDraw(); e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush b = Mensaje; Rectangle markRectangle;
            string priorityText = "Este listado incluyen los documentos de los modulos de CAJA CHICA y ENTREGAS A RENDIR";
            markRectangle = new Rectangle(e.Bounds.X * (0) + offset, e.Bounds.Y + 10, markWidth, markWidth);
            e.Graphics.FillEllipse(b, markRectangle);
            e.Graphics.FillEllipse(b, markRectangle);
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
            e.Appearance.Options.UseTextOptions = true;
            e.Appearance.DrawString(e.Cache, priorityText, new Rectangle(markRectangle.Right + offset, markRectangle.Y, e.Bounds.Width, markRectangle.Height));
        }

        private void btnSolicitarRevision_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvFacturasProveedor.RefreshData();
                if (gvFacturasProveedor.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un documento.", "Solicitar revisión", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Actualizando documentos", "Cargando...");
                foreach (int nRow in gvFacturasProveedor.GetSelectedRows())
                {
                    eFacturaProveedor obj = gvFacturasProveedor.GetRow(nRow) as eFacturaProveedor;
                    if (obj.cod_estado_registro == "RVS")
                    {
                        if (gvFacturasProveedor.SelectedRowsCount == 1)
                        {
                            MessageBox.Show("El documento ya se encuentra SOLICITADO PARA REVISION.", "Solicitar revisión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        continue;
                    }
                    if (obj.cod_estado_registro == "APR" || obj.cod_estado_registro == "REV")
                    {
                        obj.cod_estado_registro = "RVS"; obj.cod_usuario_registro = user.cod_usuario;
                        string result = blFact.Actualiar_EstadoRegistroFactura(obj);
                        if (result != "OK") { MessageBox.Show("Error al solicitar revisión", "Solicitar revisión", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    }
                }
                SplashScreenManager.CloseForm();
                XtraMessageBox.Show("Se solicitaron los documentos para revisión de manera satisfactoria", "Solicitar revisión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BuscarFacturas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        [DelimitedRecord("|")]
        public class Item
        {
            public string Prop1 { get; set; }
            public string Prop2 { get; set; }
            public string Prop3 { get; set; }
            public string Prop4 { get; set; }
            public string Prop5 { get; set; }
            public string Prop6 { get; set; }
        }
        private void btnValidadorFacturas_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                List<Item> Items = new List<Item>();
                int nCortes = 0, x = 1, y = 0, z = 1;
                //nCortes = (((decimal)gvFacturasProveedor.RowCount / 100) - Math.Truncate((decimal)gvFacturasProveedor.RowCount / 100)) > 0 ? 
                //        Convert.ToInt32(Math.Truncate((decimal)gvFacturasProveedor.RowCount / 100)) + 1 : 
                //        Convert.ToInt32( Math.Truncate((decimal)gvFacturasProveedor.RowCount / 100));


                if (!Directory.Exists("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_Validador_SUNAT")) Directory.CreateDirectory("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_Validador_SUNAT");

                DirectoryInfo source = new DirectoryInfo("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_Validador_SUNAT");
                FileInfo[] filesToCopy = source.GetFiles();
                foreach (FileInfo oFile in filesToCopy)
                {
                    oFile.Delete();
                }
                foreach (string oCarpeta in Directory.GetDirectories("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_Validador_SUNAT"))
                {
                    Directory.Delete(oCarpeta, true);
                }

                for (int nRow = 0; nRow <= gvFacturasProveedor.RowCount; nRow++)
                {
                    eFacturaProveedor obj = gvFacturasProveedor.GetRow(nRow) as eFacturaProveedor;
                    if (obj == null) continue;
                    Item _item = new Item()
                    {
                        Prop1 = obj.dsc_ruc,
                        Prop2 = obj.cod_sunat,
                        Prop3 = obj.serie_documento,
                        Prop4 = obj.numero_documento.ToString(),
                        Prop5 = obj.fch_documento.ToString("dd/MM/yyyy"),
                        Prop6 = Math.Round(obj.imp_total, 4).ToString(),
                    };
                    Items.Add(_item);
                    if (Items.Count == 100)
                    {
                        var engine = new FileHelperEngine<Item>();
                        engine.WriteFile(@"C:\IMPERIUM-Software\Recursos\ArchivosExportados\TXT_Validador_SUNAT\Archivo_validadorSUNAT" + z + ".txt", Items, 100);
                        z = z + 1;
                        Items.Clear();
                    }
                    if (y == gvFacturasProveedor.RowCount - 1)
                    {
                          var engine = new FileHelperEngine<Item>();
                        engine.WriteFile(@"C:\IMPERIUM-Software\Recursos\ArchivosExportados\TXT_Validador_SUNAT\Archivo_validadorSUNAT" + z + ".txt", Items, 100);
                    }
                    y = y + 1;
                }
                if ( 
                MessageBox.Show("Datos Exportados correctamente." + Environment.NewLine + "La ruta es: C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_Validador_SUNAT" 
                             + Environment.NewLine + "¿Desea abrir la carpeta?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Process.Start(@"C:\IMPERIUM-Software\Recursos\ArchivosExportados\TXT_Validador_SUNAT");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminarDocumento_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvFacturasProveedor.RefreshData(); gvFacturasProveedor.PostEditor();
                if (gvFacturasProveedor.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un documento.", "Eliminar documentos", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (MessageBox.Show("¿Esta seguro de eliminar el documento?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Eliminando documentos", "Cargando...");
                    string result = "";
                    foreach (int nRow in gvFacturasProveedor.GetSelectedRows())
                    {
                        eFacturaProveedor obj = gvFacturasProveedor.GetRow(nRow) as eFacturaProveedor;
                        if (obj.cod_estado_documento != "A")
                        {
                            if (gvFacturasProveedor.SelectedRowsCount == 1)
                            {
                                MessageBox.Show("Solo se pueden eliminar los documentos que hayan sido ANULADOS" + Environment.NewLine + "Proceder a ANULAR el documento", "Eliminar documentos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                            }
                            continue;
                        }
                        else
                        {
                            blSistema blSist = new blSistema(); 
                            List<eVentana> listPerfil = blSist.ListarPerfilesUsuario<eVentana>(4, user.cod_usuario);
                            eVentana oPerfilConta = listPerfil.Find(x => x.cod_perfil == 18 || x.cod_perfil == 5);
                            if (obj.cod_estado_registro != "PEN" && oPerfilConta == null)
                            {
                                if (gvFacturasProveedor.SelectedRowsCount == 1)
                                {
                                    MessageBox.Show("Solo se pueden eliminar los documentos que esten PENDIENTES DE APROBACIÓN" + Environment.NewLine + "Solicitar al jefe de Contabilidad que pueda eliminar el documento", "Eliminar documentos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                continue;
                            }
                        }
                        //////////////////////////// ELIMINAR DOCUMENTO DE ONEDRIVE ////////////////////////////
                        if (obj.idPDF != null && obj.idPDF != "") await Mover_Eliminar_ArchivoOneDrive(nRow, new DateTime(), true, false, "ELIMINAR");
                        if (obj.idXML != null && obj.idXML != "") await Mover_Eliminar_ArchivoOneDrive(nRow, new DateTime(), false, true, "ELIMINAR");

                        result = blFact.EliminarDatosFactura(4, obj.tipo_documento, obj.serie_documento, obj.numero_documento, obj.cod_proveedor);
                    }
                    SplashScreenManager.CloseForm();
                    if (result != "OK") { MessageBox.Show("Error al eliminar el documento.", "Eliminar documentos", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    if (result == "OK") { MessageBox.Show("Se eliminó el documento de manera correcta.", "Eliminar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information); BuscarFacturas(); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
                objExcel.Sheets.Add();
                var worksheet = workbook.ActiveSheet;
                worksheet.Name = "Importacion_SISPAG";
                objExcel.ActiveWindow.DisplayGridlines = false;
                //objExcel.Range["A1:ZZ1000"].Font.Name = "Century Gothic"; objExcel.Range["A4:AB4"].Font.Size = 12; objExcel.Range["A5:ZZ1000"].Font.Size = 10;

                //objExcel.Range["B1"].Font.Size = 28; objExcel.Range["B1"].Font.Bold = true;
                //objExcel.Range["B1"].Font.Color = System.Drawing.ColorTranslator.FromHtml("#E8194F");
                //objExcel.Range["B1"].Value = "REPORTE DE BOLETOS"; objExcel.Range["B2"].Font.Size = 18; objExcel.Range["B2"].Font.Bold = true; objExcel.Range["B2"].Font.Color = System.Drawing.Color.Blue;
                //objExcel.Range["A1:AS1"].Select();
                //objExcel.Selection.Borders.Color = System.Drawing.Color.FromArgb(0, 0, 0);
                //objExcel.Selection.Font.Bold = true;
                //objExcel.Selection.Font.Color = System.Drawing.Color.Black;
                //objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#FFC000");

                //procedure = "usp_Reporte_ResumenConcar";

                //procedure = "usp_Reporte_ResumenConcar @cod_empresa = '" + (lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString()) +
                //                                    "', @tipo_documento = '" + (chkcbTipoDocumento.EditValue == null ? "" : chkcbTipoDocumento.EditValue.ToString()) +
                //                                    "', @cod_estado_registro = '" + (chkcbEstadoRegistro.EditValue == null ? "" : chkcbEstadoRegistro.EditValue.ToString()) +
                //                                    "', @cod_estado_pago = '" + (chkcbEstadoPago.EditValue == null ? "" : chkcbEstadoPago.EditValue.ToString()) +
                //                                    "', @cod_tipo_fecha = '" + (lkpTipoFecha.EditValue == null ? "" : lkpTipoFecha.EditValue.ToString()) +
                //                                    "', @FechaInicio = '" + Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd") +
                //                                    "', @FechaFin = '" + Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd") + "'";
                //blFact.pDatosAExcel(cnxl, objExcel, procedure, "Consulta", "A" + 1, true);

                int fila = 0;
                for (int x = 0; x <= gvFacturasProveedor.RowCount; x++)
                {
                    eFacturaProveedor obj = gvFacturasProveedor.GetRow(x) as eFacturaProveedor;
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
                //int fila = nInLastRow;

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
                //objExcel.Range["A:AR"].ColumnWidth = 18;
                

                //objExcel.Range["C4"].Value = "Tipo de Solicitud";
                //objExcel.Range["D4"].Value = "Area";

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
                SplashScreenManager.CloseForm();
                MessageBox.Show(ex.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}