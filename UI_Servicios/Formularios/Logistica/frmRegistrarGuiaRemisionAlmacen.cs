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
using UI_Servicios.Formularios.Cuentas_Pagar;
using UI_Servicios.Formularios.Shared;
using Microsoft.Identity.Client;
using DevExpress.XtraSplashScreen;
using System.IO;
using System.Security;
using System.Net.Http.Headers;

namespace UI_Servicios.Formularios.Logistica
{
    internal enum GuiaRemision
    {
        Nuevo = 1,
        Editar = 2,
        Vista = 3
    }

    public partial class frmRegistrarGuiaRemisionAlmacen : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        internal GuiaRemision MiAccion = GuiaRemision.Nuevo;
        blFactura blFact = new blFactura();
        blLogistica blLogis = new blLogistica();
        blGlobales blGlobal = new blGlobales();
        blRequerimiento blReq = new blRequerimiento();
        List<eAlmacen.eProductos_Almacen> listaProd = new List<eAlmacen.eProductos_Almacen>();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        string fmt_nro_doc = "";
        Int16 num_ctd_serie, num_ctd_doc;
        public decimal numero_documento = 0;
        public string cod_empresa = "", cod_sede_empresa = "", cod_almacen = "", cod_guiaremision = "", cod_requerimiento = "", flg_solicitud = "", dsc_anho = "0";
        public string tipo_documento = "", serie_documento = "", TD_sunat = "";
        public bool ActualizarListado = false;

        //OneDrive
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        string varPathOrigen = "";
        string varNombreArchivo = "", varNombreArchivoSinExtension = "";

        public frmRegistrarGuiaRemisionAlmacen()
        {
            InitializeComponent();
        }

        private void frmRegistrarEntrada_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            dtFechaDocumento.EditValue = DateTime.Today;
            blLogis.CargaCombosLookUp("Almacen", lkpAlmacen, "cod_almacen", "dsc_almacen", "", valorDefecto: true, cod_empresa: cod_empresa, cod_sede_empresa: cod_sede_empresa);
            blLogis.CargaCombosLookUp("TipoMovimiento", lkpTipoMovimiento, "cod_tipo_movimiento", "dsc_tipo_movimiento", "", valorDefecto: true, dsc_variable: "GUIA_REMISION");
            blLogis.CargaCombosLookUp("TipoMovimiento", lkpMotivoTraslado, "cod_tipo_movimiento", "dsc_tipo_movimiento", "", valorDefecto: true, dsc_variable: "TRASLADO");

            switch (MiAccion)
            {
                case GuiaRemision.Nuevo:
                    dtFechaDocumento.EditValue = DateTime.Today;
                    dtFechaTraslado.EditValue = DateTime.Today;
                    lkpAlmacen.EditValue = cod_almacen; lkpTipoMovimiento.EditValue = "009"; lkpMotivoTraslado.EditValue = "014";
                    if (cod_requerimiento != "")
                    {
                        eRequerimiento eReq = blReq.Cargar_Requerimiento<eRequerimiento>(4, cod_empresa, cod_sede_empresa, cod_requerimiento);
                        txtNroRequerimiento.Text = cod_requerimiento;
                        txtGlosaRequerimiento.Text = eReq.dsc_solicitante;
                        txtDireccion.Text = eReq.dsc_direccion_cliente;
                        blFact.CargaCombosLookUp("DistribucionCECO", lkpDistribucionCECO, "cod_CECO", "dsc_CECO", "", valorDefecto: true, cod_empresa: cod_empresa, cod_cliente: eReq.cod_cliente);
                        List<eFacturaProveedor.eFacturaProveedor_Distribucion> listCECOS = blFact.ObtenerListadoCECOS<eFacturaProveedor.eFacturaProveedor_Distribucion>(32, cod_empresa, eReq.cod_cliente);
                        if (listCECOS.Count == 1) lkpDistribucionCECO.EditValue = listCECOS[0].cod_CECO;

                        listaProd = blLogis.Obtener_ListaLogistica<eAlmacen.eProductos_Almacen>(26, lkpAlmacen.EditValue.ToString(), cod_empresa, cod_sede_empresa, cod_requerimiento: cod_requerimiento);
                        bsListadoProductos.DataSource = listaProd; gvListadoProductos.RefreshData();
                    }
                    break;
                case GuiaRemision.Vista:
                    ObtenerDatos_GuiaRemision();
                    BloqueoControles(false, true, false);
                    gvListadoProductos.Columns["num_cantidad_stock"].Visible = false;
                    gvListadoProductos.Columns["num_cantidad_stock_nuevo"].Visible = false;
                    btnAdjuntarArchivo.Enabled = true;
                    btnVerPDF.Enabled = true;
                    break;
            }
        }

        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            btnGuardar.Enabled = Enabled;
            txtCodigo.ReadOnly = ReadOnly;
            lkpAlmacen.ReadOnly = ReadOnly;
            lkpTipoMovimiento.ReadOnly = ReadOnly;
            dtFechaDocumento.ReadOnly = ReadOnly;
            txtNroRequerimiento.ReadOnly = ReadOnly;
            txtGlosaRequerimiento.ReadOnly = ReadOnly;
            dtFechaTraslado.ReadOnly = ReadOnly;
            lkpDistribucionCECO.ReadOnly = ReadOnly;
            txtPlacaTransportista.ReadOnly = ReadOnly;
            txtTransportista.ReadOnly = ReadOnly;
            txtDireccion.ReadOnly = ReadOnly;
            lkpMotivoTraslado.ReadOnly = ReadOnly;
            picBuscarTransportista.Enabled = Enabled;
            picBuscarRequerimiento.Enabled = Enabled;
            gvListadoProductos.OptionsBehavior.Editable = Editable;
        }

        private void ObtenerDatos_GuiaRemision()
        {
            eAlmacen.eGuiaRemision_Cabecera obj = new eAlmacen.eGuiaRemision_Cabecera();
            obj = blLogis.Obtener_DatosLogistica<eAlmacen.eGuiaRemision_Cabecera>(24, cod_almacen, cod_empresa, cod_sede_empresa, cod_guiaremision: cod_guiaremision);
            txtCodigo.Text = obj.cod_guiaremision;
            lkpAlmacen.EditValue = obj.cod_almacen;
            lkpTipoMovimiento.EditValue = obj.cod_tipo_movimiento;
            dtFechaDocumento.EditValue = obj.fch_documento;
            txtNroRequerimiento.Text = obj.cod_requerimiento;
            txtGlosaRequerimiento.Text = obj.dsc_solicitante;
            dtFechaTraslado.EditValue = obj.fch_traslado;
            lkpDistribucionCECO.EditValue = obj.dsc_pref_ceco;
            txtPlacaTransportista.Text = obj.placa_transportista;
            txtRucTransportista1.Text = obj.ruc_transportista;
            txtTransportista.Tag = obj.cod_transportista;
            txtTransportista.Text = obj.dsc_transportista;
            txtDireccion.EditValue = obj.dsc_direccion;
            lkpMotivoTraslado.EditValue = obj.cod_motivo_traslado;
            tipo_documento = obj.tipo_documento;
            serie_documento = obj.serie_documento;
            numero_documento = Convert.ToDecimal(obj.cod_guiaremision);

            listaProd = blLogis.Obtener_ListaLogistica<eAlmacen.eProductos_Almacen>(25, cod_almacen, cod_empresa, cod_sede_empresa, cod_guiaremision: cod_guiaremision);
            bsListadoProductos.DataSource = listaProd; gvListadoProductos.RefreshData();
        }

        private void frmRegistrarGuiaRemisionAlmacen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && MiAccion != GuiaRemision.Nuevo) this.Close();
        }

        private void picBuscarRequerimiento_Click(object sender, EventArgs e)
        {
            //string doc_referencia = "";

            //frmOpcionDocReferencia frm = new frmOpcionDocReferencia();
            //frm.ShowDialog();
            //doc_referencia = frm.doc_referencia;
            //switch (doc_referencia)
            //{
            //    case "01": Busqueda("", "OrdenesCompra"); break;
            //    case "02": Buscar_DocReferencia();  break;
            //}
            Busqueda("", "Requerimiento");
        }

        private void picBuscarTransportista_Click(object sender, EventArgs e)
        {
            Busqueda("", "Transportista");
        }

        private void btnEliminar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MiAccion == GuiaRemision.Nuevo)
            {
                eAlmacen.eProductos_Almacen objP = gvListadoProductos.GetFocusedRow() as eAlmacen.eProductos_Almacen;
                listaProd.Remove(objP);
                int n_Orden = 1;
                foreach (eAlmacen.eProductos_Almacen obj in listaProd)
                {
                    obj.n_Orden = n_Orden;
                    n_Orden += 1;
                }
                bsListadoProductos.DataSource = listaProd;
                gvListadoProductos.RefreshData();
            }
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
                string dsc_Carpeta = "Guia Remision";
                DateTime FechaRegistro = Convert.ToDateTime(dtFechaDocumento.EditValue);
                //VALIDAR SI EL PERIODO SE ENCUENTRA ABIERTO
                eFacturaProveedor objTrib = blFact.Obtener_PeriodoTributario<eFacturaProveedor>(50, FechaRegistro.ToString("MM-yyyy"), cod_empresa);
                if (objTrib != null && objTrib.flg_cerrado == "SI")
                {
                    eFacturaProveedor objTrib2 = blFact.Obtener_PeriodoTributario<eFacturaProveedor>(51, "", cod_empresa);
                    int n_Mes = 0, n_Anho = 0;
                    n_Mes = Convert.ToInt32(objTrib2.periodo_tributario.Substring(0, 2));
                    n_Anho = Convert.ToInt32(objTrib2.periodo_tributario.Substring(3, 4));
                    n_Anho = n_Mes == 12 ? n_Anho + 1 : n_Anho;
                    n_Mes = n_Mes == 12 ? 1 : n_Mes + 1;
                    FechaRegistro = new DateTime(n_Anho, n_Mes, 01);
                }

                int Anho = FechaRegistro.Year; int Mes = FechaRegistro.Month; string NombreMes = FechaRegistro.ToString("MMMM");
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
                        TD_sunat = list.Find(x => x.tipo_documento == tipo_documento).cod_sunat;
                        //varNombreArchivo = RUC + "-" + TD_sunat + "-" + serie_documento + "-" + $"{numero_documento:00000000}" + Path.GetExtension(myFileDialog.SafeFileName);
                        varNombreArchivo = "GUIA_REMISION" + "-" + TD_sunat + "-" + serie_documento + "-" + String.Format("{0:" + fmt_nro_doc + "}", numero_documento) + Path.GetExtension(myFileDialog.SafeFileName);
                        varNombreArchivoSinExtension = "GUIA_REMISION" + "-" + TD_sunat + "-" + serie_documento + "-" + String.Format("{0:" + fmt_nro_doc + "}", numero_documento);
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
                    eEmpresa.eOnedrive_Empresa IdCarpetaAnho = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(13, cod_empresa, FechaRegistro.Year, dsc_Carpeta: dsc_Carpeta);
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
                    eEmpresa.eOnedrive_Empresa IdCarpetaMes = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(14, cod_empresa, FechaRegistro.Year, FechaRegistro.Month, dsc_Carpeta);
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

                    //////////////////////////////////////////////////////// REEMPLAZAR DOCUMENTO DE ONEDRIVE ////////////////////////////////////////////////////////
                    eAlmacen.eGuiaRemision_Cabecera obj = new eAlmacen.eGuiaRemision_Cabecera();
                    obj = blLogis.Obtener_DatosLogistica<eAlmacen.eGuiaRemision_Cabecera>(24, cod_almacen, cod_empresa, cod_sede_empresa, cod_guiaremision: cod_guiaremision);
                    //////////////////////////// ELIMINAR DOCUMENTO DE ONEDRIVE ////////////////////////////
                    if (obj.idPDF != null && obj.idPDF != "" && Extension.ToLower() == ".pdf") await Mover_Eliminar_ArchivoOneDrive(obj, new DateTime(), true, false, "ELIMINAR");
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    //crea archivo en el OneDrive
                    byte[] data = System.IO.File.ReadAllBytes(varPathOrigen);
                    using (Stream stream = new MemoryStream(data))
                    {
                        string res = "";
                        int opcion = Extension.ToLower() == ".pdf" ? 1 : Extension.ToLower() == ".xml" ? 2 : 0;
                        if (opcion == 1 || opcion == 2)
                        {
                            var DriveItem = await GraphClient.Me.Drive.Items[IdArchivoMes].ItemWithPath(varNombreArchivo).Content.Request().PutAsync<Microsoft.Graph.DriveItem>(stream);
                            idArchivoPDF = opcion == 1 ? DriveItem.Id : "";
                            idArchivoXML = opcion == 2 ? DriveItem.Id : "";

                            eFacturaProveedor objFact = new eFacturaProveedor();
                            objFact.tipo_documento = tipo_documento;
                            objFact.serie_documento = serie_documento;
                            objFact.numero_documento = numero_documento;
                            objFact.cod_proveedor = "";
                            objFact.idPDF = idArchivoPDF;
                            objFact.idXML = idArchivoXML;
                            //objFact.NombreArchivo = varNombreArchivo;
                            objFact.NombreArchivo = varNombreArchivoSinExtension;
                            objFact.cod_empresa = cod_empresa;
                            objFact.idCarpetaAnho = IdArchivoAnho;
                            objFact.idCarpetaMes = IdArchivoMes;

                            res = blFact.ActualizarInformacionDocumentos(5, objFact, targetItemFolderId, Anho.ToString(), $"{Mes:00}", dsc_Carpeta, cod_guiaremision, cod_almacen, cod_sede_empresa);
                        }

                        if (res == "OK")
                        {
                            MessageBox.Show("Se registró el documento satisfactoriamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnVerPDF.Enabled = true;
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

        private async Task Mover_Eliminar_ArchivoOneDrive(eAlmacen.eGuiaRemision_Cabecera obj, DateTime FechaPeriodo, bool PDF, bool XML, string opcion)
        {
            try
            {
                string dsc_Carpeta = "Guia Remision";
                int Anho = obj.fch_documento.Year; int Mes = obj.fch_documento.Month; string NombreMes = obj.fch_documento.Month.ToString("MMMM");
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
                eDatos = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(26, obj.cod_empresa, obj.fch_documento.Year, dsc_Carpeta: dsc_Carpeta);
                var targetItemFolderId = opcion != "ELIMINAR" ? eDatos.idCarpeta : "";

                //eFacturaProveedor IdCarpetaAnho = blFact.ObtenerDatosOneDrive<eFacturaProveedor>(13, lkpEmpresaProveedor.EditValue.ToString(), Convert.ToDateTime(dtFechaRegistro.EditValue).Year);
                eEmpresa.eOnedrive_Empresa IdCarpetaAnho = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(13, obj.cod_empresa, obj.fch_documento.Year, dsc_Carpeta: dsc_Carpeta);
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
                eEmpresa.eOnedrive_Empresa IdCarpetaMes = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(14, obj.cod_empresa, obj.fch_documento.Year, obj.fch_documento.Month, dsc_Carpeta);
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
                    if (opcion == "ELIMINAR") await GraphClient.Me.Drive.Items[x == 0 ? obj.idPDF : ""].Request().DeleteAsync();
                    //if (opcion == "ELIMINAR") await GraphClient.Directory.DeletedItems[x == 0 ? obj.idPDF : obj.idXML].Request().DeleteAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void btnVerPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        public void Busqueda(string dato, string tipo)
        {
            if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }

            frmBusquedas frm = new frmBusquedas();
            frm.user = user;
            frm.filtro = dato;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            switch (tipo)
            {
                case "Requerimiento":
                    frm.entidad = frmBusquedas.MiEntidad.Requerimiento;
                    frm.cod_almacen = cod_almacen;
                    frm.cod_empresa = cod_empresa;
                    frm.cod_sede_empresa = cod_sede_empresa;
                    frm.filtro = dato;
                    break;
                case "Transportista":
                    frm.entidad = frmBusquedas.MiEntidad.Proveedor;
                    frm.flg_transportista = "SI";
                    frm.cod_empresa = cod_empresa;
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Requerimiento":
                    //txtNroRequerimiento.Text = frm.codigo;
                    //txtGlosaRequerimiento.Tag = frm.cod_condicion1;
                    //txtGlosaRequerimiento.Text = frm.descripcion;
                    //tipo_documento_REFERENCIA = ""; serie_documento_REFERENCIA = ""; numero_documento_REFERENCIA = 0;
                    txtNroRequerimiento.Text = frm.codigo;
                    txtGlosaRequerimiento.Text = frm.descripcion;
                    txtDireccion.Text = frm.dsc_condicion2;
                    blFact.CargaCombosLookUp("DistribucionCECO", lkpDistribucionCECO, "cod_CECO", "dsc_CECO", "", valorDefecto: true, cod_empresa: cod_empresa, cod_cliente: frm.dsc_condicion1);
                    listaProd = blLogis.Obtener_ListaLogistica<eAlmacen.eProductos_Almacen>(26, lkpAlmacen.EditValue.ToString(), cod_empresa, cod_sede_empresa, cod_requerimiento: frm.codigo);
                    bsListadoProductos.DataSource = listaProd; gvListadoProductos.RefreshData();
                    break;
                case "Transportista":
                    txtPlacaTransportista.Text = frm.cod_condicion1;
                    txtRucTransportista1.Text = frm.ruc;
                    txtTransportista.Tag = frm.codigo;
                    txtTransportista.Text = frm.descripcion;
                    break;
            }
        }

        private void Buscar_DocReferencia()
        {
            frmFacturasDetalle frm = new frmFacturasDetalle();
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorFocus = colorFocus;
            frm.colorEventRow = colorEventRow;
            frm.cod_empresa = cod_empresa;
            frm.cod_proveedor = "";
            frm.cod_moneda = "SOL";
            frm.BusquedaLogistica = true;
            frm.MostrarProveedor = true;
            frm.user = user;
            frm.ShowDialog();
            if (frm.listDocumentosNC.Count > 0)
            {
                eFacturaProveedor eFact = new eFacturaProveedor();
                eFact = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(2, frm.listDocumentosNC[0].tipo_documento, frm.listDocumentosNC[0].serie_documento, frm.listDocumentosNC[0].numero_documento, frm.listDocumentosNC[0].cod_proveedor);

                eTipoComprobante obj = new eTipoComprobante();
                obj = blFact.BuscarTipoComprobante<eTipoComprobante>(27, eFact.tipo_documento);
                num_ctd_serie = obj.num_ctd_serie; num_ctd_doc = obj.num_ctd_doc;
                fmt_nro_doc = new string('0', num_ctd_doc);

                //tipo_documento_REFERENCIA = eFact.tipo_documento;
                //serie_documento_REFERENCIA = eFact.serie_documento;
                //numero_documento_REFERENCIA = eFact.numero_documento; 
                txtNroRequerimiento.Text = eFact.serie_documento + "-" + String.Format("{0:" + fmt_nro_doc + "}", eFact.numero_documento); 
                txtGlosaRequerimiento.Tag = eFact.cod_proveedor;
                txtGlosaRequerimiento.Text = eFact.dsc_proveedor;
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
                if (lkpTipoMovimiento.EditValue == null) { MessageBox.Show("Debe seleccionar el tipo de movimiento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoMovimiento.Focus(); return; }
                if (txtNroRequerimiento.Text.Trim() == "") { MessageBox.Show("Debe seleccionar el requerimiento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNroRequerimiento.Focus(); return; }
                if (lkpDistribucionCECO.EditValue == null) { MessageBox.Show("Debe seleccionar el centro de costo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpDistribucionCECO.Focus(); return; }
                //if (txtTransportista.Tag.ToString().Trim() == "") { MessageBox.Show("Debe seleccionar el transportista.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtTransportista.Focus(); return; }
                if (txtDireccion.Text.Trim() == "") { MessageBox.Show("Debe la dirección.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDireccion.Focus(); return; }
                if (lkpMotivoTraslado.EditValue == null) { MessageBox.Show("Debe seleccionar el motivo de traslado.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpMotivoTraslado.Focus(); return; }
                int nTotal = 0;
                foreach (eAlmacen.eProductos_Almacen obj in listaProd)
                {
                    if (obj.num_cantidad_stock_nuevo < 0) nTotal = nTotal + 1;
                }
                if (nTotal > 0) { MessageBox.Show("La cantidad del producto no puede ser mayor a la del stock actual.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                eAlmacen.eGuiaRemision_Cabecera eGuia = AsignarValores_Cabecera();
                eGuia = blLogis.Insertar_Actualizar_GuiaRemisionCabecera<eAlmacen.eGuiaRemision_Cabecera>(eGuia);
                if (eGuia != null)
                {
                    txtCodigo.Text = eGuia.cod_guiaremision;
                    if (gvListadoProductos.RowCount > 0)
                    {
                        for (int nRow = 0; nRow < gvListadoProductos.RowCount; nRow++)
                        {
                            eAlmacen.eProductos_Almacen eProd = gvListadoProductos.GetRow(nRow) as eAlmacen.eProductos_Almacen;
                            if (eProd.num_cantidad == 0) continue;
                            eAlmacen.eGuiaRemision_Detalle eDet = new eAlmacen.eGuiaRemision_Detalle();
                            eDet.cod_guiaremision = eGuia.cod_guiaremision;
                            eDet.cod_almacen = cod_almacen;
                            eDet.cod_empresa = cod_empresa;
                            eDet.cod_sede_empresa = cod_sede_empresa;
                            eDet.cod_tipo_servicio = eProd.cod_tipo_servicio;
                            eDet.cod_subtipo_servicio = eProd.cod_subtipo_servicio;
                            eDet.cod_producto = eProd.cod_producto;
                            eDet.cod_unidad_medida = eProd.cod_unidad_medida;
                            eDet.num_cantidad = eProd.num_cantidad;
                            eDet.cod_usuario_registro = user.cod_usuario;

                            eDet = blLogis.Insertar_Actualizar_GuiaRemisionDetalle<eAlmacen.eGuiaRemision_Detalle>(eDet);
                            if (eDet == null) MessageBox.Show("Error al registrar producto", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    ActualizarListado = true;
                    MessageBox.Show("Se realizó la salida de productos de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (MiAccion == GuiaRemision.Nuevo)
                    {
                        MiAccion = GuiaRemision.Vista; 
                        BloqueoControles(false, true, false);
                        gvListadoProductos.Columns["num_cantidad_stock"].Visible = false;
                        gvListadoProductos.Columns["num_cantidad_stock_nuevo"].Visible = false;
                        btnAdjuntarArchivo.Enabled = true; btnVerPDF.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Error al registrar salida", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private eAlmacen.eGuiaRemision_Cabecera AsignarValores_Cabecera()
        {
            eAlmacen.eGuiaRemision_Cabecera obj = new eAlmacen.eGuiaRemision_Cabecera();
            obj.cod_guiaremision = txtCodigo.Text;
            obj.cod_almacen = cod_almacen;
            obj.cod_tipo_movimiento = lkpTipoMovimiento.EditValue.ToString();
            obj.fch_documento = Convert.ToDateTime(dtFechaDocumento.EditValue);
            obj.cod_empresa = cod_empresa;
            obj.cod_sede_empresa = cod_sede_empresa;
            obj.cod_requerimiento = txtNroRequerimiento.Text;
            obj.fch_traslado = Convert.ToDateTime(dtFechaTraslado.EditValue);
            obj.dsc_pref_ceco = lkpDistribucionCECO.EditValue.ToString();
            obj.cod_transportista = txtTransportista.Tag.ToString();
            obj.dsc_direccion = txtDireccion.Text;
            obj.cod_motivo_traslado = lkpMotivoTraslado.EditValue.ToString();
            obj.flg_activo = "SI";
            obj.cod_usuario_registro = user.cod_usuario;

            return obj;
        }

        private void gvListadoProductos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoProductos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoProductos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    string colName = e.Column.FieldName;
                    eAlmacen.eProductos_Almacen objProd = gvListadoProductos.GetRow(e.RowHandle) as eAlmacen.eProductos_Almacen;
                    if (colName == "num_cantidad_stock" || colName == "num_cantidad_stock_nuevo") e.Appearance.ForeColor = Color.Blue;
                    if (colName == "num_cantidad_stock" && objProd.num_cantidad_stock <= 0) e.Appearance.ForeColor = Color.Red;
                    if (colName == "num_cantidad_stock_nuevo" && objProd.num_cantidad_stock_nuevo <= 0) e.Appearance.ForeColor = Color.Red;
                    e.DefaultDraw();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoProductos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                eAlmacen.eProductos_Almacen objProd = gvListadoProductos.GetFocusedRow() as eAlmacen.eProductos_Almacen;
                if (objProd != null)
                {
                    if (e.Column.FieldName == "num_cantidad")
                    {
                        if (objProd.num_cantidad > objProd.num_cantidad_x_recibir)
                        {
                            MessageBox.Show("No puede digitar una cantidad mayor al requerimiento inicial", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            objProd.num_cantidad = objProd.num_cantidad_x_recibir;

                            if (objProd.num_cantidad > objProd.num_cantidad_stock)
                            {
                                MessageBox.Show("No puede digitar una cantidad mayor a la del stock", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                objProd.num_cantidad = objProd.num_cantidad_stock;
                                objProd.num_cantidad_stock_nuevo = 0;
                                gvListadoProductos.RefreshData();
                                return;
                            }
                            gvListadoProductos.RefreshData();
                            return;
                        }
                        if (objProd.num_cantidad > objProd.num_cantidad_stock)
                        {
                            MessageBox.Show("No puede digitar una cantidad mayor a la del stock", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            objProd.num_cantidad = objProd.num_cantidad_stock;
                            objProd.num_cantidad_stock_nuevo = 0;
                            gvListadoProductos.RefreshData();
                            return;
                        }
                        objProd.num_cantidad_stock_nuevo = objProd.num_cantidad_stock - objProd.num_cantidad;
                    }
                    gvListadoProductos.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}