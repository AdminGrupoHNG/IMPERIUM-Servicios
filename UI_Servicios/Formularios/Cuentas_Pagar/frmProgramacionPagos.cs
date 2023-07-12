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
using Microsoft.Identity.Client;

namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    public partial class frmProgramacionPagos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public eUsuario user = new eUsuario();
        TaskScheduler scheduler;
        Timer oTimerLoadMtto; 
        blGlobales blGlobal = new blGlobales();
        blEncrypta blEncryp = new blEncrypta();
        public blFactura blFact = new blFactura();
        blSistema blSist = new blSistema();
        blProveedores blProv = new blProveedores();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaProgramacion = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
        Image ImgPDF = DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttopdf_16x16.png");

        //OneDrive
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        string varPathOrigen = "";
        string varNombreArchivo = "";

        public frmProgramacionPagos()
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void frmProgramacionPagos_Load(object sender, EventArgs e)
        {
            //Inicializar();
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            oTimerLoadMtto.Start();
        }

        private void Inicializar()
        {
            CargarLookUpEdit();
            //Fecha
            DateTime date = DateTime.Now;
            //DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            //DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            //dtFechaInicio.EditValue = oPrimerDiaDelMes;
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            dtFechaInicio.EditValue = new DateTime(DateTime.Today.Year, 1, 1);
            dtFechaFin.EditValue = oUltimoDiaDelMes;
            //FECHA PAGO PROGRAMADO, es el viernes proximo de la fecha de vencimiento
            int nDia = Convert.ToInt32(DateTime.Today.DayOfWeek);
            nDia = nDia <= 5 ? 5 - nDia : nDia;
            dtFechaProgramadoAl.EditValue = DateTime.Today.AddDays(nDia);
            chkcbTipoDocumento.CheckAll();

            //Creamos SUPERTOOLTIP
            DevExpress.Utils.SuperToolTip sToolTip = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.SuperToolTipSetupArgs args = new DevExpress.Utils.SuperToolTipSetupArgs();
            Image resImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/support/index_32x32.png");
            args.Title.Text = "Marcar/Desmarcar";
            args.Contents.Text = "Los importes de los totales se <b>reducen/incrementan</b> al <b>marcar/desmarcar</b> los documentos del listado.";
            args.ShowFooterSeparator = true;
            args.Footer.Text = "Toda acción es según la fecha seleccionada";
            args.Contents.ImageOptions.Image = resImage;
            sToolTip.Setup(args);
            sToolTip.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            picAyuda.SuperTip = sToolTip;

            BuscarFacturas();
        }
        private void HabilitarBotones()
        {
            blSistema blSist = new blSistema();
            List<eVentana> listPermisos = blSist.ListarMenuxUsuario<eVentana>(user.cod_usuario, this.Name);

            if (listPermisos.Count > 0)
            {
                //grupoEdicion.Enabled = listPermisos[0].flg_escritura;
                //grupoAcciones.Enabled = listPermisos[0].flg_escritura;
                btnAgregarProgramacion.Enabled = listPermisos[0].flg_escritura;
                btnEjecutarPago.Enabled = listPermisos[0].flg_escritura;
            }
            List<eVentana> listPerfil = blSist.ListarPerfilesUsuario<eVentana>(4, user.cod_usuario);
            eVentana oPerfil = listPerfil.Find(x => x.cod_perfil == 4);
            //btnAgregarProgramacion.Enabled = oPerfil != null ? true : false;
            btnEjecutarPago.Enabled = oPerfil != null ? true : false;
            //bgvProgramacionPagos.Columns["bandedGridColumn1"].OptionsColumn.AllowEdit = oPerfil != null ? true : false;
        }
        private void CargarLookUpEdit()
        {
            try
            {
                blFact.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: user.cod_usuario);
                blFact.CargaCombosChecked("TipoDocumento", chkcbTipoDocumento, "cod_tipo_comprobante", "dsc_tipo_comprobante", "");
                blFact.CargaCombosLookUp("TipoFecha", lkpTipoFecha, "cod_tipo_fecha", "dsc_tipo_fecha", "", valorDefecto: true);

                rlkpTipoDocumento.DataSource = blFact.CombosEnGridControl<eFacturaProveedor>("TipoDocumento");
                rlkpDocumento.DataSource = blFact.CombosEnGridControl<eFacturaProveedor>("Documento"/*, tipo_documento: obj.tipo_documento*/);
                rlkpEstado.DataSource = blFact.CombosEnGridControl<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>("EstadoProgramacion");
                rlkpPagar_A.DataSource = blFact.CombosEnGridControl<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>("Pagar_A");
                rlkpFormaPago.DataSource = blFact.CombosEnGridControl<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>("FormaPago");

                List<eFacturaProveedor> list = blProv.ListarEmpresasProveedor<eFacturaProveedor>(11, "", user.cod_usuario);
                if (list.Count >= 1) lkpEmpresa.EditValue = list[0].cod_empresa;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void bgvProgramacionPagos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void bgvProgramacionPagos_CustomDrawBandHeader(object sender, DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnasBandHeader(e);
        }

        private void bgvProgramacionPagos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                blGlobal.Pintar_EstiloGrilla(sender, e);
                GridView view = sender as GridView;
                if (view.Columns["cod_estado"] != null || view.Columns["dsc_estado_documento"] != null)
                {
                    //decimal saldo = Convert.ToDecimal(view.GetRowCellDisplayText(e.RowHandle, view.Columns["imp_saldo"]));
                    //if (saldo == 0) e.Appearance.ForeColor = Color.Blue;
                    //string estadoP = view.GetRowCellDisplayText(e.RowHandle, view.Columns["cod_estado"]);
                    //if (estadoP == "EJECUTADO") e.Appearance.ForeColor = Color.Blue;
                    string estado = view.GetRowCellDisplayText(e.RowHandle, view.Columns["dsc_estado_documento"]);
                    if (estado == "Anulado") e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void bgvProgramacionPagos_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
            if (obj == null) return;
            obj.cod_estado = "PRO";
        }

        private void bgvProgramacionPagos_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            //eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetRow(e.RowHandle) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
            //if (obj == null) return;
            //obj.cod_usuario_registro = user.cod_usuario;
            //eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
            //eProgFact = blFact.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
            //if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            //BuscarFacturas();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo documentos", "Cargando...");
            BuscarFacturas();
            SplashScreenManager.CloseForm();
        }
        public void BuscarFacturas()
        {
            try
            {
                listaProgramacion = blFact.FiltroFactura<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(23, lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(),
                                                                                        chkcbTipoDocumento.EditValue == null ? "" : chkcbTipoDocumento.EditValue.ToString(),
                                                                                        "",
                                                                                        "",
                                                                                        lkpTipoFecha.EditValue == null ? "" : lkpTipoFecha.EditValue.ToString(),
                                                                                        Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                                        Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"),
                                                                                        SinSaldo: Convert.ToInt32(grdbFiltroSaldo.SelectedIndex));
                bsProgramacionPagos.DataSource = listaProgramacion;

                CalcularTOTALES();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CalcularTOTALES()
        {
            List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listMontoSOLES = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
            List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listMontoDOLARES = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
            List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listMontoRestaSOLES = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
            List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listMontoRestaDOLARES = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
            listMontoSOLES = listaProgramacion.FindAll(x => x.fch_pago.ToShortDateString() == Convert.ToDateTime(dtFechaProgramadoAl.EditValue).ToShortDateString() && x.cod_moneda == "SOL" && x.cod_estado == "PRO");
            listMontoDOLARES = listaProgramacion.FindAll(x => x.fch_pago.ToShortDateString() == Convert.ToDateTime(dtFechaProgramadoAl.EditValue).ToShortDateString() && x.cod_moneda == "DOL" && x.cod_estado == "PRO");
            listMontoRestaSOLES = listaProgramacion.FindAll(x => x.fch_pago.ToShortDateString() == Convert.ToDateTime(dtFechaProgramadoAl.EditValue).ToShortDateString() && x.cod_moneda == "SOL" && x.cod_estado == "PRO" && x.Sel);
            listMontoRestaDOLARES = listaProgramacion.FindAll(x => x.fch_pago.ToShortDateString() == Convert.ToDateTime(dtFechaProgramadoAl.EditValue).ToShortDateString() && x.cod_moneda == "DOL" && x.cod_estado == "PRO" && x.Sel);

            decimal MontoSOLES = (from tabla in listMontoSOLES
                                  select tabla.imp_pago).Sum();
            decimal MontoDOLARES = (from tabla in listMontoDOLARES
                                    select tabla.imp_pago).Sum();
            decimal MontoRestaSOLES = (from tabla in listMontoRestaSOLES
                                       select tabla.imp_pago).Sum();
            decimal MontoRestaDOLARES = (from tabla in listMontoRestaDOLARES
                                         select tabla.imp_pago).Sum();
            txtMontoSOLES.EditValue = MontoSOLES - MontoRestaSOLES; txtMontoDOLARES.EditValue = MontoDOLARES - MontoRestaDOLARES;
        }

        private void rlkpEstado_EditValueChanged(object sender, EventArgs e)
        {
            bgvProgramacionPagos.PostEditor();
        }

        private void frmProgramacionPagos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo documentos", "Cargando...");
                BuscarFacturas();
                SplashScreenManager.CloseForm();
            }
        }
        
        private void rlkpDocumento_EditValueChanged(object sender, EventArgs e)
        {
            bgvProgramacionPagos.PostEditor();
        }

        private void rlkpTipoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            bgvProgramacionPagos.PostEditor();
        }
        
        private void rbtnEliminarAgregarProgramacion_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            GridView view = bgvProgramacionPagos;
            int index = view.FocusedRowHandle;
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
            if (obj == null) return;

            switch (e.Button.Caption)
            {
                case "Agregar":
                    if (obj.imp_saldo == 0) return;
                    obj.num_linea = 0; obj.fch_pago = obj.fch_pago_programado; obj.dsc_observacion = null; obj.cod_estado = "PRO"; obj.cod_pagar_a = "PROV";
                    obj.fch_ejecucion = new DateTime(); obj.cod_usuario_ejecucion = null; obj.cod_usuario_registro = user.cod_usuario;
                    obj.cod_tipo_prog = "REGULAR";
                    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                    eProgFact = blFact.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                    if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "Eliminar":
                    List<eVentana> listPerfil = blSist.ListarPerfilesUsuario<eVentana>(4, user.cod_usuario);
                    eVentana oPerfil = listPerfil.Find(x => x.cod_perfil == 5);
                    if (obj.cod_estado == "EJE" && oPerfil == null) { MessageBox.Show("No se puede eliminar una programación ya ejecutada.", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    if (MessageBox.Show("¿Esta seguro de eliminar el registro?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string result = blFact.EliminarDatosFactura(3, obj.tipo_documento, obj.serie_documento, obj.numero_documento, obj.cod_proveedor, num_linea: obj.num_linea);
                        if (result != "OK") { MessageBox.Show("Error al eliminar registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                        bgvProgramacionPagos.DeleteRow(index);
                    }
                    break;
            }
            BuscarFacturas();
            view.FocusedRowHandle = index;
            bgvProgramacionPagos.RefreshData();
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
        private async void bgvProgramacionPagos_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                eFacturaProveedor obj = new eFacturaProveedor();
                eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objProg = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                if (e.Clicks == 1 && e.Column.FieldName == "dsc_documento")
                {
                    obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor;
                    if (obj == null) { return; }

                    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                    if (Application.OpenForms["frmMantFacturaProveedor"] != null)
                    {
                        Application.OpenForms["frmMantFacturaProveedor"].Activate();
                    }
                    else
                    {
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
                        frmModif.user.cod_usuario = user.cod_usuario;
                        frmModif.habilitar_control = "SI";
                        frmModif.ShowDialog();
                    }
                }
                if (e.Clicks == 1 && e.Column.FieldName == "Sel" && objProg.cod_estado != "EJE")
                {
                    objProg.Sel = objProg.Sel ? false : true;
                    bgvProgramacionPagos.RefreshData();
                    CalcularTOTALES();
                }
                if (e.Clicks == 2 && e.Column.FieldName == "flg_PDF")
                {
                    obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor;
                    if (obj == null) { return; }

                    eFacturaProveedor eFact = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(24, obj.tipo_documento, obj.serie_documento, obj.numero_documento, obj.cod_proveedor);
                    if (eFact.idPDF == null || eFact.idPDF == "")
                    {
                        MessageBox.Show("No se cargado ningún PDF", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bgvProgramacionPagos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Sel") return;
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
            if (obj == null) return;
            if (e.Column.FieldName == "cod_estado" && obj.cod_estado == "EJE")
            {
                if (MessageBox.Show("¿Esta seguro de ejecutar el pago?", "Ejecutar pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    obj.cod_usuario_ejecucion = user.cod_usuario; obj.dsc_usuario_ejecucion = user.dsc_usuario; obj.fch_ejecucion = DateTime.Today;
                }
                else
                {
                    obj.cod_estado = "PRO";
                }
            }

            obj.cod_usuario_registro = user.cod_usuario;
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
            eProgFact = blFact.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
            if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (e.Column.FieldName == "cod_estado" && obj.cod_estado == "EJE")
            {
                int nRow = bgvProgramacionPagos.FocusedRowHandle;
                BuscarFacturas();
                bgvProgramacionPagos.FocusedRowHandle = nRow;
            }
            else
            {
                CalcularTOTALES();
            }
            bgvProgramacionPagos.RefreshData();
        }
        private void bgvProgramacionPagos_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                if (obj == null) return;
                //if ((bgvProgramacionPagos.FocusedColumn.FieldName == "tipo_documento" || bgvProgramacionPagos.FocusedColumn.FieldName == "cod_documento") && 
                //    obj.flg_guardado == "SI")
                //{
                //    e.Cancel = true;
                //}
                List<eVentana> listPerfil = blSist.ListarPerfilesUsuario<eVentana>(4, user.cod_usuario);
                eVentana oPerfil = listPerfil.Find(x => x.cod_perfil == 5);
                if (obj.cod_estado == "EJE" && obj.imp_saldo == 0 && bgvProgramacionPagos.FocusedColumn.FieldName != "bandedGridColumn1")
                {
                    e.Cancel = oPerfil == null ? true : false;
                }
                if (obj.cod_estado == "EJE" && bgvProgramacionPagos.FocusedColumn.FieldName != "bandedGridColumn1")
                {
                    e.Cancel = oPerfil == null ? true : false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bgvProgramacionPagos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetRow(e.RowHandle) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                    if (e.Column.FieldName == "fch_vencimiento" && obj.fch_vencimiento < DateTime.Today && obj.imp_saldo != 0) e.Appearance.BackColor = Color.LightSalmon;
                    if (e.Column.FieldName == "fch_pago" && obj.fch_pago.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_ejecucion" && obj.fch_ejecucion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "CantCuentas" && obj.CantCuentas == "NO") { e.Appearance.ForeColor = Color.Red; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    if (obj.cod_estado == "EJE") e.Appearance.ForeColor = Color.Blue;
                    if (e.Column.FieldName == "flg_PDF" && obj.flg_PDF == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgPDF, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if ((e.Column.FieldName == "flg_PDF" && obj.cod_estado_pago != "SI") || (e.Column.FieldName == "flg_XML" && obj.cod_estado_pago != "SI"))
                    {
                        e.DisplayText = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAgregarProgramacion_ItemClick(object sender, ItemClickEventArgs e)
        {
            bgvProgramacionPagos.RefreshData(); bgvProgramacionPagos.PostEditor();
            //List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> lista = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
            //lista = listaProgramacion.FindAll(x => x.Sel);
            if (bgvProgramacionPagos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            //if (lista.Count == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            
            frmOpcionesProgMasiva frm = new frmOpcionesProgMasiva();
            frm.ShowDialog();
            if (frm.Actualizar == "OK")
            {
                //foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in lista)
                foreach (int nRow in bgvProgramacionPagos.GetSelectedRows())
                {
                    if (nRow < 0) continue;
                    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetRow(nRow) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                    if (obj == null) continue;
                    if (obj.imp_saldo == 0) continue;
                    obj.num_linea = 0; obj.fch_pago = frm.fch_pago; obj.dsc_observacion = frm.dsc_observacion; obj.cod_estado = "PRO"; obj.cod_pagar_a = frm.cod_pagar_a;
                    obj.fch_ejecucion = new DateTime(); obj.cod_usuario_ejecucion = null; obj.cod_usuario_registro = user.cod_usuario; obj.cod_tipo_prog = "REGULAR";
                    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                    eProgFact = blFact.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                    if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                BuscarFacturas();
            }
        }

        private void btnEjecutarPago_ItemClick(object sender, ItemClickEventArgs e)
        {
            bgvProgramacionPagos.RefreshData(); bgvProgramacionPagos.PostEditor();
            List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> lista = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
            lista = listaProgramacion.FindAll(x => x.Sel);

            List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaDet = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
            //listaDet = listaProgramacion.FindAll(x => x.Sel && (x.dsc_observacion == "DETRACCIÓN" || x.dsc_observacion == "RET 4TA"));
            //listaDet = listaProgramacion.FindAll(x => x.Sel && x.cod_tipo_prog == "DETRACC");

            if (lista.Count == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (bgvProgramacionPagos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            if (lista.Count == 1 && lista[0].num_linea == 0) { MessageBox.Show("No hay una programación de pago registrada", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (lista.Count == 1 && lista[0].cod_estado == "EJE") { MessageBox.Show("El pago ya esta ejecutado", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Ingrese la fecha de pago ejecutado";
            DateEdit dtFecha = new DateEdit(); dtFecha.Width = 100; args.DefaultResponse = DateTime.Today; args.Editor = dtFecha;
            var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);

            if ((res == DialogResult.OK || res == DialogResult.Yes) && dtFecha.EditValue != null)
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Actualizando documentos", "Cargando...");
                foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in lista)
                //foreach (int nRow in bgvProgramacionPagos.GetSelectedRows())
                {
                    //if (nRow < 0) continue;
                    //eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetRow(nRow) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                    if (obj == null) continue;

                    if (bgvProgramacionPagos.SelectedRowsCount == 1 && obj.num_linea == 0) { MessageBox.Show("No hay una programación de pago registrada", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                    if (bgvProgramacionPagos.SelectedRowsCount == 1 && obj.cod_estado == "EJE") { MessageBox.Show("El pago ya esta ejecutado", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                    if (obj.cod_tipo_prog == "DETRACC") listaDet.Add(obj);

                    if (obj.imp_saldo == 0 || obj.cod_estado == "EJE" || obj.num_linea == 0) continue;
                    obj.cod_estado = "EJE"; obj.fch_ejecucion = Convert.ToDateTime(dtFecha.EditValue);
                    obj.cod_usuario_ejecucion = user.cod_usuario; obj.cod_usuario_registro = user.cod_usuario;
                    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                    //eProgFact = blFact.ActualizarEjecutarPago<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                    eProgFact = blFact.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                    if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (listaDet.Count > 0)
                {
                    foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eObj in listaDet)
                    {
                        eObj.fch_ejecucion = Convert.ToDateTime(dtFecha.EditValue);
                    }
                    frmFacturasConstanciaDetraccRetenc frmDetRet = new frmFacturasConstanciaDetraccRetenc();
                    frmDetRet.listFacturas = listaDet;
                    frmDetRet.colorVerde = colorVerde;
                    frmDetRet.colorPlomo = colorPlomo;
                    frmDetRet.colorFocus = colorFocus;
                    frmDetRet.colorEventRow = colorEventRow;
                    frmDetRet.user = user;
                    frmDetRet.ShowDialog();
                }
                BuscarFacturas();
                SplashScreenManager.CloseForm();
            }
        }

        private void chkMarcarTodos_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (bgvProgramacionPagos.RowCount == 0) { MessageBox.Show("Debe haber al menos 1 programación", "Marcar todos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); chkMarcarTodos.CheckState = CheckState.Unchecked; return; }
                for (int x = 0; x <= bgvProgramacionPagos.RowCount - 1; x++)
                {
                    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetRow(x) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                    if (obj != null && obj.cod_estado == "PRO")
                    {
                        if (chkMarcarTodos.CheckState == CheckState.Checked)
                        {
                            obj.Sel = true;
                        }
                        else if (chkMarcarTodos.CheckState == CheckState.Unchecked)
                        {
                            obj.Sel = false;
                        }
                    }
                }
                bgvProgramacionPagos.RefreshData();
                CalcularTOTALES();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAplazarPagoProgramado_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                bgvProgramacionPagos.RefreshData(); bgvProgramacionPagos.PostEditor();
                //List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> lista = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
                //lista = listaProgramacion.FindAll(x => x.Sel);
                //if (lista.Count == 0) { MessageBox.Show("Debe seleccionar un registro.", "Aplazar Pago Programado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                if (bgvProgramacionPagos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                if (MessageBox.Show("¿Esta seguro de aplazar 1 semana las programaciones de pagos seleccionadas?", "Aplazar Pago Programado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in lista)
                    foreach (int nRow in bgvProgramacionPagos.GetSelectedRows())
                    {
                        blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Actualizando documentos", "Cargando...");
                        if (nRow < 0) continue;
                        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetRow(nRow) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                        if (obj == null) continue;
                        if (bgvProgramacionPagos.SelectedRowsCount == 1 && obj.cod_estado == "EJE") { MessageBox.Show("El pago ya esta ejecutado", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                        if (obj.imp_saldo == 0 || obj.cod_estado == "EJE" || obj.num_linea == 0) continue;
                        obj.fch_pago = obj.fch_pago.AddDays(7);
                        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                        eProgFact = blFact.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                        if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    BuscarFacturas();
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void chkSel_CheckStateChanged(object sender, EventArgs e)
        {
            //bgvProgramacionPagos.PostEditor();
        }

        private void rbtnEliminarAgregarProgramacion_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void dtFechaProgramadoAl_EditValueChanged(object sender, EventArgs e)
        {
            if (dtFechaProgramadoAl.EditValue != null) CalcularTOTALES();
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
                bgvProgramacionPagos.ExportToXlsx(archivo);
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
            bgvProgramacionPagos.ShowPrintPreview();
        }
    }
}