using DevExpress.XtraEditors;
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
using UI_Servicios.Formularios.Shared;
using Microsoft.Identity.Client;
using System.IO;
using DevExpress.XtraSplashScreen;
using System.Security;
using System.Net.Http.Headers;
using System.Configuration;
using WIA;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Runtime.InteropServices;
using DevExpress.XtraBars;

namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    internal enum DocInterno
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmMantDocumentoInterno : DevExpress.XtraEditors.XtraForm
    {
        internal DocInterno MiAccion = DocInterno.Nuevo;
        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        blFactura blFact = new blFactura();
        blProveedores blProv = new blProveedores();
        blEncrypta blEncryp = new blEncrypta();
        public string RUC = "", TD_sunat = "", tipo_documento = "", serie_documento = "", cod_proveedor = "", cod_empresa = "", habilitar_control = "NO";
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public decimal numero_documento = 0;
        string fmt_nro_doc = "";
        public string CajaChica = "NO", EntregaRendir = "NO";
        Int16 num_ctd_serie, num_ctd_doc;
        public bool ActualizarListado = false;

        //OneDrive
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        string varPathOrigen = "";
        string varNombreArchivo = "", varNombreArchivoSinExtension = "";

        public frmMantDocumentoInterno()
        {
            InitializeComponent();
        }

        private void frmMantDocumentoInterno_Load(object sender, EventArgs e)
        {
            try
            {
                Inicializar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Inicializar()
        {
            switch (MiAccion)
            {
                case DocInterno.Nuevo:
                    Nuevo();
                    txtGlosaFactura.Select();
                    break;
                case DocInterno.Editar:
                    Editar();
                    txtGlosaFactura.Select();
                    break;
                case DocInterno.Vista:
                    Editar();
                    if (user.cod_usuario != "ADMINISTRADOR") BloqueoControles(false, true, false);
                    break;
            }
        }

        private void Nuevo()
        {
            dtFechaDocumento.EditValue = DateTime.Today;
            CargarLookUpEdit();
            dtFechaRegistroReal.EditValue = DateTime.Today;
            txtUsuarioRegistro.Text = user.dsc_usuario;
            dtFechaModificacion.EditValue = DateTime.Today;
            txtUsuarioCambio.Text = user.dsc_usuario;
            txtSerieDocumento.Text = CajaChica == "SI" ? "DI01" : EntregaRendir == "SI" ? "DI02" : "DI00";
        }

        private void Editar()
        {
            try
            {
                CargarLookUpEdit();
                ObtenerDatos_DocumentoInterno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ObtenerDatos_DocumentoInterno()
        {
            eDocumentoInterno eFact = new eDocumentoInterno();
            eFact = blFact.ObtenerFacturaProveedor<eDocumentoInterno>(47, tipo_documento, serie_documento, numero_documento, cod_proveedor);
            if (eFact == null) return;
            RUC = eFact.dsc_ruc;
            glkpTipoDocumento.EditValue = eFact.tipo_documento;
            txtSerieDocumento.Text = eFact.serie_documento;
            txtNumeroDocumento.Text = String.Format("{0:" + fmt_nro_doc + "}", eFact.numero_documento);  //$"{eFact.numero_documento:00000000}";
            txtGlosaFactura.Text = eFact.dsc_glosa;
            lkpDistribucionCECO.EditValue = eFact.dsc_pref_ceco;
            lkpTipoMoneda.EditValue = eFact.cod_moneda;
            dtFechaDocumento.EditValue = eFact.fch_documento;
            txtReferencia.Text = eFact.dsc_referencia;
            mmObservacion.Text = eFact.dsc_observacion;
            txtMontoTotal.Text = eFact.imp_total.ToString();
            txtUsuarioRegistro.Text = eFact.dsc_usuario_registro;
            dtFechaRegistroReal.EditValue = eFact.fch_registro;
            txtUsuarioCambio.Text = eFact.dsc_usuario_cambio;
            dtFechaModificacion.EditValue = eFact.fch_cambio;
            
            if (eFact.flg_PDF == "NO")
            {
                btnAdjuntarArchivo.Enabled = true; btnEscanearDocumento.Enabled = true; btnVerPDF.Enabled = false; btnReemplazarArchivo.Enabled = false;
            }
            else
            {
                btnAdjuntarArchivo.Enabled = false; btnEscanearDocumento.Enabled = false; btnVerPDF.Enabled = true; btnReemplazarArchivo.Enabled = true;
            }
        }

        private void frmMantDocumentoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (MiAccion != DocInterno.Nuevo && e.KeyCode == Keys.Escape) this.Close();
        }

        private void glkpTipoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpTipoDocumento.EditValue != null)
            {
                eTipoComprobante obj = new eTipoComprobante();
                obj = blFact.BuscarTipoComprobante<eTipoComprobante>(27, glkpTipoDocumento.EditValue.ToString());
                num_ctd_serie = obj.num_ctd_serie; num_ctd_doc = obj.num_ctd_doc;
                fmt_nro_doc = new string('0', num_ctd_doc);
                txtSerieDocumento.Properties.MaxLength = num_ctd_serie;
                txtNumeroDocumento.Properties.MaxLength = num_ctd_doc;
                string dsc_documento = glkpTipoDocumento.Text;
                glkpTipoDocumento.ToolTip = dsc_documento;
            }
        }

        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            btnGuardar.Enabled = Enabled;
            btnEliminarDocumento.Enabled = Enabled;
            glkpTipoDocumento.ReadOnly = ReadOnly;
            txtSerieDocumento.ReadOnly = ReadOnly;
            txtNumeroDocumento.ReadOnly = ReadOnly;
            txtGlosaFactura.ReadOnly = ReadOnly;
            lkpDistribucionCECO.ReadOnly = ReadOnly;
            lkpTipoMoneda.ReadOnly = ReadOnly;
            dtFechaDocumento.ReadOnly = ReadOnly;
            txtMontoTotal.ReadOnly = ReadOnly;
            txtReferencia.ReadOnly = ReadOnly;
            mmObservacion.ReadOnly = ReadOnly;
        }

        private void CargarLookUpEdit()
        {
            try
            {
                CargarCombosGridLookup("TipoComprobante", glkpTipoDocumento, "cod_tipo_comprobante", "dsc_tipo_comprobante", "", valorDefecto: true);
                blProv.CargaCombosLookUp("Moneda", lkpTipoMoneda, "cod_moneda", "dsc_moneda", "", valorDefecto: true);
                blFact.CargaCombosLookUp("DistribucionCECO", lkpDistribucionCECO, "cod_CECO", "dsc_CECO", "", valorDefecto: true, cod_empresa: cod_empresa);
                glkpTipoDocumento.EditValue = "TC045";
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

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtGlosaFactura.Focus(); txtGlosaFactura.Select();
            if (lkpDistribucionCECO.EditValue == null) { MessageBox.Show("Debe seleccionar el centro de costo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpDistribucionCECO.Focus(); return; }
            if (glkpTipoDocumento.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); glkpTipoDocumento.Focus(); return; }
            if (txtSerieDocumento.Text.Trim() == "") { MessageBox.Show("Debe ingresar una serie de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtSerieDocumento.Focus(); return; }
            if (txtNumeroDocumento.Text.Trim() == "") { MessageBox.Show("Debe ingresar un numero de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNumeroDocumento.Focus(); return; }
            if (txtGlosaFactura.Text.Trim() == "") { MessageBox.Show("Debe ingresar la glosa del documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNumeroDocumento.Focus(); return; }

            eDocumentoInterno eFact = AsignarValores();
            eFact = blFact.InsertarDocumentoInterno<eDocumentoInterno>(eFact);
            if (eFact == null)  { MessageBox.Show("Error al registrar documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            RUC = eFact.dsc_ruc;
            tipo_documento = eFact.tipo_documento;
            serie_documento = eFact.serie_documento;
            numero_documento = eFact.numero_documento;
            cod_proveedor = eFact.cod_proveedor;
            ActualizarListado = true;
            MiAccion = DocInterno.Editar;
            Inicializar(); 
            MessageBox.Show("Se registro el documento de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnAdjuntarArchivo.Enabled = true;
            btnEscanearDocumento.Enabled = true;
        }

        private async void btnEliminarDocumento_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Esta seguro de eliminar el documento?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    eFacturaProveedor eFact = new eFacturaProveedor();
                    eFact = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(47, tipo_documento, serie_documento, numero_documento, cod_proveedor);
                    eFact.cod_empresa = cod_empresa;
                    //////////////////////////// ELIMINAR DOCUMENTO DE ONEDRIVE ////////////////////////////
                    if (eFact.idPDF != null && eFact.idPDF != "") await Mover_Eliminar_ArchivoOneDrive(eFact, new DateTime(), true, false, "ELIMINAR");

                    string result = "";
                    result = blFact.EliminarDatosFactura(4, tipo_documento, serie_documento, numero_documento, cod_proveedor);
                    if (result != "OK") { MessageBox.Show("Error eliminó el documento de manera correcta.", "Eliminar documentos", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    if (result == "OK") { MessageBox.Show("Se eliminó el documento de manera correcta.", "Eliminar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information); ActualizarListado = true; this.Close(); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

       
        private eDocumentoInterno AsignarValores()
        {
            eDocumentoInterno eFact = new eDocumentoInterno();
            eFact.cod_proveedor = "PR000000";
            eFact.dsc_ruc = "00000000000";
            eProveedor eProv = new eProveedor();
            eProv = blFact.ObtenerDatosEmpresa<eProveedor>(33, cod_empresa);
            if (eProv != null) { eFact.cod_proveedor = eProv.cod_proveedor; eFact.dsc_ruc = eProv.num_documento; }
            eFact.tipo_documento = glkpTipoDocumento.EditValue.ToString();
            eFact.serie_documento = txtSerieDocumento.Text.ToUpper();
            eFact.numero_documento = txtNumeroDocumento.Text == "" ? 0 : Convert.ToDecimal(txtNumeroDocumento.Text);
            eFact.dsc_glosa = txtGlosaFactura.Text;
            eFact.dsc_pref_ceco = lkpDistribucionCECO.EditValue.ToString();
            eFact.cod_moneda = lkpTipoMoneda.EditValue.ToString();
            eFact.fch_documento = Convert.ToDateTime(dtFechaDocumento.EditValue);
            eFact.imp_total = Convert.ToDecimal(txtMontoTotal.Text);
            eFact.dsc_referencia = txtReferencia.Text;
            eFact.dsc_observacion = mmObservacion.Text;
            eFact.cod_usuario_registro = user.cod_usuario;

            return eFact;
        }


        private async void btnVerPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eDocumentoInterno eFact = blFact.ObtenerFacturaProveedor<eDocumentoInterno>(49, tipo_documento, serie_documento, numero_documento, cod_proveedor);

            if (eFact.idPDF == null || eFact.idPDF == "")
            {
                MessageBox.Show("No se cargado ningún PDF", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                eEmpresa eEmp = blFact.ObtenerDatosEmpresa<eEmpresa>(12, cod_empresa);
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
                    //eEmpresa eEmp = blFact.ObtenerDatosEmpresa<eEmpresa>(12, lkpEmpresaProveedor.EditValue.ToString());
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
                    string IdOneDriveDoc = IdPDF;

                    var fileContent = await GraphClient.Me.Drive.Items[IdOneDriveDoc].Content.Request().GetAsync();
                    string ruta = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()) + @"\" + eFact.NombreArchivo + ".pdf";
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
            }
        }


        private async void btnReemplazarArchivo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            await AdjuntarArchivo();
        }
        private async void btnAdjuntarArchivo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            await AdjuntarArchivo();
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

        private async Task AdjuntarArchivo()
        {
            try
            {
                //ObtenerListadeFolders
                string dsc_Carpeta = glkpTipoDocumento.EditValue.ToString() == "TC008" ? "RxH Proveedor" : "Facturas Proveedor";
                dsc_Carpeta = CajaChica == "SI" ? "Caja Chica" : EntregaRendir == "SI" ? "Entrega Rendir" : dsc_Carpeta;
                DateTime FechaRegistro = Convert.ToDateTime(dtFechaDocumento.EditValue);
                int Anho = FechaRegistro.Year; int Mes = FechaRegistro.Month; string NombreMes = FechaRegistro.ToString("MMMM");

                eFacturaProveedor resultado = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(47, tipo_documento, serie_documento, numero_documento, cod_proveedor);
                if (resultado == null) { MessageBox.Show("Antes de adjuntar el PDF debe crear la factura.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                OpenFileDialog myFileDialog = new OpenFileDialog();
                //myFileDialog.Filter = "Archivos (*.pdf;*.docx;*.xlsx;*.pptx;*.xml)|; *.pdf;*.doc;*.docx;*.xls;*.xlsx;*.ppt;*.pptx;*.xml";
                myFileDialog.Filter = "Archivos (*.pdf;*.xml)|; *.pdf;*.xml";
                myFileDialog.FilterIndex = 1;
                myFileDialog.InitialDirectory = "C:\\";
                myFileDialog.Title = "Abrir archivo";
                myFileDialog.CheckFileExists = false;
                myFileDialog.Multiselect = false;

                DialogResult result = myFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string IdArchivoAnho = "", IdArchivoMes = "", Extension = "";
                    var idArchivoPDF = ""; var idArchivoXML = "";
                    var TamañoDoc = new FileInfo(myFileDialog.FileName).Length / 1024;
                    if (TamañoDoc < 4000)
                    {
                        varPathOrigen = myFileDialog.FileName;
                        //varNombreArchivo = Path.GetFileNameWithoutExtension(myFileDialog.SafeFileName) + Path.GetExtension(myFileDialog.SafeFileName);
                        List<eFacturaProveedor> list = blFact.CombosEnGridControl<eFacturaProveedor>("TipoDocumento");
                        //TD_sunat = list.Find(x => x.tipo_documento == tipo_documento).cod_sunat;
                        TD_sunat = "00";
                        //varNombreArchivo = RUC + "-" + TD_sunat + "-" + serie_documento + "-" + $"{numero_documento:00000000}" + Path.GetExtension(myFileDialog.SafeFileName);
                        varNombreArchivo = RUC + "-" + TD_sunat + "-" + serie_documento + "-" + String.Format("{0:" + fmt_nro_doc + "}", numero_documento) + Path.GetExtension(myFileDialog.SafeFileName);
                        varNombreArchivoSinExtension = RUC + "-" + TD_sunat + "-" + serie_documento + "-" + String.Format("{0:" + fmt_nro_doc + "}", numero_documento);
                        Extension = Path.GetExtension(myFileDialog.SafeFileName);
                    }
                    else
                    {
                        MessageBox.Show("Solo puede subir archivos hasta 5MB de tamaño", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Por favor espere...", "Cargando...");
                    eEmpresa eEmp = blFact.ObtenerDatosEmpresa<eEmpresa>(12, cod_empresa);
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
                    eDatos = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(26, cod_empresa, Convert.ToDateTime(dtFechaDocumento.EditValue).Year, dsc_Carpeta: dsc_Carpeta);
                    var targetItemFolderId = eDatos.idCarpeta;

                    //eFacturaProveedor IdCarpetaAnho = blFact.ObtenerDatosOneDrive<eFacturaProveedor>(13, lkpEmpresaProveedor.EditValue.ToString(), Convert.ToDateTime(dtFechaRegistro.EditValue).Year);
                    eEmpresa.eOnedrive_Empresa IdCarpetaAnho = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(13, cod_empresa, Convert.ToDateTime(dtFechaDocumento.EditValue).Year, dsc_Carpeta: dsc_Carpeta);
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
                    eEmpresa.eOnedrive_Empresa IdCarpetaMes = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(14, cod_empresa, Convert.ToDateTime(dtFechaDocumento.EditValue).Year, Convert.ToDateTime(dtFechaDocumento.EditValue).Month, dsc_Carpeta);
                    if (IdCarpetaMes == null)
                    {
                        var driveItem = new Microsoft.Graph.DriveItem
                        {
                            //Name = Mes.ToString() + ". " + NombreMes.ToUpper(),
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


                    //////////////////////////////////////////////////////// REEMPLAZAR DOCUMENTO DE ONEDRIVE ////////////////////////////////////////////////////////
                    eFacturaProveedor eFact = new eFacturaProveedor();
                    eFact = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(49, tipo_documento, serie_documento, numero_documento, cod_proveedor);
                    //////////////////////////// ELIMINAR DOCUMENTO DE ONEDRIVE ////////////////////////////
                    eFact.cod_empresa = cod_empresa;
                    if (eFact.idPDF != null && eFact.idPDF != "" && Extension.ToLower() == ".pdf") await Mover_Eliminar_ArchivoOneDrive(eFact, new DateTime(), true, false, "ELIMINAR");
                    //if (eFact.idXML != null && eFact.idXML != "" && Extension.ToLower() == ".xml") await Mover_Eliminar_ArchivoOneDrive(eFact, new DateTime(), false, true, "ELIMINAR");
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                    //crea archivo en el OneDrive
                    byte[] data = System.IO.File.ReadAllBytes(varPathOrigen);
                    using (Stream stream = new MemoryStream(data))
                    {
                        string res = "";
                        //int opcion = Extension.ToLower() == ".pdf" ? 1 : Extension.ToLower() == ".xml" ? 2 : 0;
                        //if (opcion == 1 || opcion == 2)
                        //{
                            var DriveItem = await GraphClient.Me.Drive.Items[IdArchivoMes].ItemWithPath(varNombreArchivo).Content.Request().PutAsync<Microsoft.Graph.DriveItem>(stream);
                            idArchivoPDF = DriveItem.Id;
                            //idArchivoPDF = opcion == 1 ? DriveItem.Id : "";
                            //idArchivoXML = opcion == 2 ? DriveItem.Id : "";

                            eFacturaProveedor objFact = new eFacturaProveedor();
                            objFact.tipo_documento = resultado.tipo_documento;
                            objFact.serie_documento = resultado.serie_documento;
                            objFact.numero_documento = resultado.numero_documento;
                            objFact.cod_proveedor = resultado.cod_proveedor;
                            objFact.idPDF = idArchivoPDF;
                            //objFact.idXML = idArchivoXML;
                            //objFact.NombreArchivo = varNombreArchivo;
                            objFact.NombreArchivo = varNombreArchivoSinExtension;
                            objFact.cod_empresa = cod_empresa;
                            objFact.idCarpetaAnho = IdArchivoAnho;
                            objFact.idCarpetaMes = IdArchivoMes;

                        res = blFact.ActualizarInformacionDocumentos(3, objFact, targetItemFolderId, Anho.ToString(), $"{Mes:00}", dsc_Carpeta);
                        //}

                        if (res == "OK")
                        {
                            MessageBox.Show("Se registró el documento satisfactoriamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnVerPDF.Enabled = true; btnEscanearDocumento.Enabled = false; btnReemplazarArchivo.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Hubieron problemas al registrar el documento", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    SplashScreenManager.CloseForm();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private async Task Mover_Eliminar_ArchivoOneDrive(eFacturaProveedor obj, DateTime FechaPeriodo, bool PDF, bool XML, string opcion)
        {
            try
            {
                //eFacturaProveedor obj = gvFacturasProveedor.GetRow(nRow) as eFacturaProveedor;
                obj.periodo_tributario = FechaPeriodo.ToString("MM-yyyy");
                if (/*gvFacturasProveedor.SelectedRowsCount == 1 && */(obj.periodo_tributario == null || obj.periodo_tributario == "")) { MessageBox.Show("Debe asignar un periodo tributario para mover los archivos adjuntos", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                if (obj.periodo_tributario == null || obj.periodo_tributario == "") return;
                string dsc_Carpeta = glkpTipoDocumento.EditValue.ToString() == "TC008" ? "RxH Proveedor" : "Facturas Proveedor";
                dsc_Carpeta = CajaChica == "SI" ? "Caja Chica" : EntregaRendir == "SI" ? "Entrega Rendir" : dsc_Carpeta;
                //string dsc_Carpeta = obj.tipo_documento == "TC008" ? "RxH Proveedor" : "Facturas Proveedor";
                int Anho = Convert.ToInt32(obj.periodo_tributario.Substring(3, 4)); int Mes = Convert.ToInt32(obj.periodo_tributario.Substring(0, 2)); string NombreMes = Convert.ToDateTime(obj.periodo_tributario).ToString("MMMM");
                string IdArchivoAnho = "", IdArchivoMes = "";
                //varNombreArchivo = obj.NombreArchivo;

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

                    //if (opcion == "MOVER") await GraphClient.Me.Drive.Items[x == 0 ? obj.idPDF : obj.idXML].Request().UpdateAsync(DriveItem);
                    if (opcion == "ELIMINAR") await GraphClient.Me.Drive.Items[x == 0 ? obj.idPDF : obj.idXML].Request().DeleteAsync();
                    //if (opcion == "ELIMINAR") await GraphClient.Directory.DeletedItems[x == 0 ? obj.idPDF : obj.idXML].Request().DeleteAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private async void btnEscanearDocumento_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //Eliminamos todos los archivos de la carpeta ESCANEOS
                DirectoryInfo source;
                var carpetaScan = ConfigurationManager.AppSettings["RutaArchivosEscaneos"].ToString();
                if (!Directory.Exists(carpetaScan)) Directory.CreateDirectory(carpetaScan);
                source = new DirectoryInfo(carpetaScan);
                FileInfo[] filesScan = source.GetFiles();
                foreach (FileInfo oFile in filesScan)
                {
                    oFile.Delete();
                }

                // Crea una instancia de DeviceManager
                var deviceManager = new DeviceManager();
                // Crea una variable vacía para almacenar la instancia del analizador
                DeviceInfo firstScannerAvailable = null;

                // Recorre la lista de dispositivos
                for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
                {
                    // Omita el dispositivo si no es un escáner
                    if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType) continue;
                    firstScannerAvailable = deviceManager.DeviceInfos[i];
                    break;
                }
                // Conéctate al primer escáner disponible
                var device = firstScannerAvailable.Connect();
                // Selecciona el escáner
                var scannerItem = device.Items[1];

                CommonDialogClass dlg = new CommonDialogClass();
                object scanResult = dlg.ShowTransfer(scannerItem, WIA.FormatID.wiaFormatPNG, true);
                List<eFacturaProveedor> list = blFact.CombosEnGridControl<eFacturaProveedor>("TipoDocumento");
                //TD_sunat = list.Find(x => x.tipo_documento == tipo_documento).cod_sunat;
                TD_sunat = "00";
                //string archivoEscaneado = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosEscaneos")].ToString()) + "\\Escaneo" + DateTime.Now.ToShortDateString().Replace("/", "-").Replace(":", "");
                string archivoEscaneado = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosEscaneos")].ToString()) + "\\Documento" + (RUC + "-" + TD_sunat + "-" + serie_documento + "-" + String.Format("{0:" + fmt_nro_doc + "}", numero_documento));

                if (scanResult != null)
                {
                    // Recupera una imagen en formato JPEG y almacenala en una variable
                    ImageFile image = (ImageFile)scanResult;
                    // Guarda la imagen en alguna ruta con nombre de archivo
                    //var path = archivoEscaneado + ".jpeg";

                    if (File.Exists(archivoEscaneado + ".png")) File.Delete(archivoEscaneado + ".png");
                    if (File.Exists(archivoEscaneado + ".pdf")) File.Delete(archivoEscaneado + ".pdf");

                    // Guardar imagen !
                    image.SaveFile(archivoEscaneado + ".png");
                }

                Document doc = new Document(PageSize.A4);
                //Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(archivoEscaneado + ".pdf", FileMode.Create));
                doc.Open();
                //Creamos la imagen y ajustamos el tamaño
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(archivoEscaneado + ".png");
                imagen.BorderWidth = 0;
                imagen.Alignment = Element.ALIGN_RIGHT;
                //float prc = 0.0f; prc = 150 / imagen.Width;
                //imagen.Width = writer.PageSize.Width;
                imagen.ScaleAbsolute(writer.PageSize.Width - 10, writer.PageSize.Height - 10);
                imagen.SetAbsolutePosition(5, 5);
                doc.Add(imagen);
                doc.Close();
                writer.Close();

                //Guardamos PDF en ruta de ONEDRIVE
                await AdjuntarArchivo(archivoEscaneado + ".pdf", ".pdf");
                if (File.Exists(archivoEscaneado + ".png")) File.Delete(archivoEscaneado + ".png");
                if (File.Exists(archivoEscaneado + ".pdf")) File.Delete(archivoEscaneado + ".pdf");

                XtraMessageBox.Show("Se procedió a escanear y cargar el documento al OneDrive de manera satisfactoria.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnVerPDF.Enabled = true; btnEscanearDocumento.Enabled = false; btnReemplazarArchivo.Enabled = true;
            }
            catch (COMException ex)
            {
                //MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Convierte el código de error a UINT
                uint errorCode = (uint)ex.ErrorCode;

                // Ve los códigos de error
                if (errorCode == 0x80210006)
                {
                    XtraMessageBox.Show("El escáner está ocupado o no está listo");
                }
                else if (errorCode == 0x80210064)
                {
                    XtraMessageBox.Show("Se canceló el proceso de escaneo...");
                }
                else if (errorCode == 0x8021000C)
                {
                    XtraMessageBox.Show("Hay una configuración incorrecta en el dispositivo.");
                }
                else if (errorCode == 0x80210005)
                {
                    XtraMessageBox.Show("El dispositivo está desconectado. Asegúrese de que el dispositivo esté encendido y conectado.");
                }
                else if (errorCode == 0x80210001)
                {
                    XtraMessageBox.Show("Se ha producido un error desconocido con el dispositivo.");
                }
            }
        }

        private async Task AdjuntarArchivo(string FileName, string extension)
        {
            try
            {
                eFacturaProveedor obj = new eFacturaProveedor();
                obj = blFact.BuscarTipoComprobante<eFacturaProveedor>(27, tipo_documento);

                //ObtenerListadeFolders
                string dsc_Carpeta = tipo_documento == "TC008" ? "RxH Proveedor" : "Facturas Proveedor";
                dsc_Carpeta = CajaChica == "SI" ? "Caja Chica" : EntregaRendir == "SI" ? "Entrega Rendir" : dsc_Carpeta;
                DateTime FechaRegistro = Convert.ToDateTime(dtFechaDocumento.EditValue);
                int Anho = FechaRegistro.Year; int Mes = FechaRegistro.Month; string NombreMes = FechaRegistro.ToString("MMMM");

                eFacturaProveedor resultado = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(47, tipo_documento, serie_documento, numero_documento, cod_proveedor);
                if (resultado == null) { MessageBox.Show("Antes de adjuntar el PDF debe crear la factura.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                string IdArchivoAnho = "", IdArchivoMes = "";
                var idArchivoPDF = ""; var idArchivoXML = "";
                var TamañoDoc = new FileInfo(FileName).Length / 1024;
                if (TamañoDoc < 4000)
                {
                    varPathOrigen = FileName;
                    //varNombreArchivo = Path.GetFileNameWithoutExtension(myFileDialog.SafeFileName) + Path.GetExtension(myFileDialog.SafeFileName);
                    List<eFacturaProveedor> list = blFact.CombosEnGridControl<eFacturaProveedor>("TipoDocumento");
                    //TD_sunat = list.Find(x => x.tipo_documento == tipo_documento).cod_sunat;
                    TD_sunat = "00";
                    //varNombreArchivo = RUC + "-" + TD_sunat + "-" + serie_documento + "-" + $"{numero_documento:00000000}" + Path.GetExtension(myFileDialog.SafeFileName);
                    varNombreArchivo = RUC + "-" + TD_sunat + "-" + serie_documento + "-" + String.Format("{0:" + fmt_nro_doc + "}", numero_documento) + extension;
                    varNombreArchivoSinExtension = RUC + "-" + TD_sunat + "-" + serie_documento + "-" + String.Format("{0:" + fmt_nro_doc + "}", numero_documento);
                }
                else
                {
                    MessageBox.Show("Solo puede subir archivos hasta 5MB de tamaño", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Por favor espere...", "Cargando...");
                eEmpresa eEmp = blFact.ObtenerDatosEmpresa<eEmpresa>(12, cod_empresa);
                if (eEmp.ClientIdOnedrive == null || eEmp.ClientIdOnedrive == "") { MessageBox.Show("Debe configurar los datos del Onedrive de la empresa asignada", "Onedrive", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

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
                eDatos = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(26, cod_empresa, Convert.ToDateTime(dtFechaDocumento.EditValue).Year, dsc_Carpeta: dsc_Carpeta);
                var targetItemFolderId = eDatos.idCarpeta;

                //eFacturaProveedor IdCarpetaAnho = blFact.ObtenerDatosOneDrive<eFacturaProveedor>(13, lkpEmpresaProveedor.EditValue.ToString(), Convert.ToDateTime(dtFechaRegistro.EditValue).Year);
                eEmpresa.eOnedrive_Empresa IdCarpetaAnho = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(13, cod_empresa, Convert.ToDateTime(dtFechaDocumento.EditValue).Year, dsc_Carpeta: dsc_Carpeta);
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
                eEmpresa.eOnedrive_Empresa IdCarpetaMes = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(14, cod_empresa, Convert.ToDateTime(dtFechaDocumento.EditValue).Year, Convert.ToDateTime(dtFechaDocumento.EditValue).Month, dsc_Carpeta);
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

                //crea archivo en el OneDrive
                byte[] data = System.IO.File.ReadAllBytes(varPathOrigen);
                using (Stream stream = new MemoryStream(data))
                {
                    string res = "";
                    //int opcion = extension.ToLower() == ".pdf" ? 1 : extension.ToLower() == ".xml" ? 2 : 0;
                    //if (opcion == 1 || opcion == 2)
                    //{
                    var DriveItem = await GraphClient.Me.Drive.Items[IdArchivoMes].ItemWithPath(varNombreArchivo).Content.Request().PutAsync<Microsoft.Graph.DriveItem>(stream);
                    idArchivoPDF = DriveItem.Id;
                    //idArchivoPDF = opcion == 1 ? DriveItem.Id : "";
                    //idArchivoXML = opcion == 2 ? DriveItem.Id : "";

                    eFacturaProveedor objFact = new eFacturaProveedor();
                    objFact.tipo_documento = resultado.tipo_documento;
                    objFact.serie_documento = resultado.serie_documento;
                    objFact.numero_documento = resultado.numero_documento;
                    objFact.cod_proveedor = resultado.cod_proveedor;
                    objFact.idPDF = idArchivoPDF;
                    //objFact.idXML = idArchivoXML;
                    //objFact.NombreArchivo = varNombreArchivo;
                    objFact.NombreArchivo = varNombreArchivoSinExtension;
                    //objFact.cod_empresa = resultado.cod_empresa;
                    //objFact.idCarpetaAnho = IdArchivoAnho;
                    //objFact.idCarpetaMes = IdArchivoMes;

                    res = blFact.ActualizarInformacionDocumentos(3, objFact, targetItemFolderId, Anho.ToString(), $"{Mes:00}", dsc_Carpeta);
                    //}

                    if (res == "OK")
                    {

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}