using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BE_Servicios;
using BL_Servicios;
using System.Xml.Serialization;
using DevExpress.XtraBars;
using System.IO;
//using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using DevExpress.XtraSplashScreen;
using System.Configuration;
using System.Security;
using System.Net.Http.Headers;
using UI_Servicios.Formularios.Shared;
using UI_Servicios.Formularios.Clientes_Y_Proveedores.Proveedores;
using Microsoft.Identity.Client;
using DevExpress.XtraGrid.Views.Grid;
using UI_Servicios.Formularios.Sistema.Configuraciones_Maestras;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout.Utils;
using WIA;
using System.Runtime.InteropServices;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DevExpress.XtraNavBar;
//using Microsoft.Identity.Client;

namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    internal enum Factura
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmMantFacturaProveedor : DevExpress.XtraEditors.XtraForm
    {
        frmListadoFacturaProveedor frmHandler;
        frmResumenMovCajaChica frmHandler2;
        frmResumenEntregasRendir frmHandler3;
        internal Factura MiAccion = Factura.Nuevo;
        public eUsuario user = new eUsuario();
        blProveedores blProv = new blProveedores();
        blCajaChica blCaja = new blCajaChica();
        blClientes blCli = new blClientes();
        blSistema blSist = new blSistema();
        public blFactura blFact = new blFactura();
        blGlobales blGlobal = new blGlobales();
        blEncrypta blEncryp = new blEncrypta();
        //Datos Traidos de XML
        public bool FlgExistePDF = false, FlgExisteXML = false;
        public decimal TotalDocumento = 0;
        public List<eFacturaProveedor.eFacturaProveedor_Distribucion> mylistLineasDetFactura = new List<eFacturaProveedor.eFacturaProveedor_Distribucion>();
        public List<eFacturaProveedor.eFacturaProveedor_Observaciones> mylistObservaciones = new List<eFacturaProveedor.eFacturaProveedor_Observaciones>();
        public List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> mylistProgPagos = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
        //List<eFacturaProveedor.eFacturaProveedor_NotaCredito> mylistDocsVinculados = new List<eFacturaProveedor.eFacturaProveedor_NotaCredito>();
        List<eUnidadNegocio> listUnidadNegocio = new List<eUnidadNegocio>();
        List<eTipoGastoCosto> listTipoGastoCosto = new List<eTipoGastoCosto>();
        List<eCliente_Empresas> listClienteEmpresa = new List<eCliente_Empresas>();
        List<eCliente_Empresas> listClienteProyecto = new List<eCliente_Empresas>();
        List<eFacturaProveedor.eFacturaProveedor_NotaCredito> listDocumentosNC = new List<eFacturaProveedor.eFacturaProveedor_NotaCredito>();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public string RUC = "", TD_sunat = "", tipo_documento = "", serie_documento = "", cod_proveedor = "", habilitar_control = "NO", orden_servicio = "";
        public decimal numero_documento = 0;
        string fmt_nro_doc = "";
        Int16 num_ctd_serie, num_ctd_doc;
        public string CajaChica = "NO", cod_caja = "", cod_movimiento = "", cod_empresa = "", cod_sede_empresa = "", cod_cliente="";
        public string EntregaRendir = "NO", cod_entregarendir = "", cod_trabajador = "";

       

        //OneDrive
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        string varPathOrigen = "";
        string varNombreArchivo = "", varNombreArchivoSinExtension = "";
        public bool ActualizarListado = false;
        public frmMantFacturaProveedor()
        {
            InitializeComponent();
        }
        public frmMantFacturaProveedor(frmListadoFacturaProveedor frm)
        {
            InitializeComponent();
            frmHandler = frm;
        }
        public frmMantFacturaProveedor(frmResumenMovCajaChica frm2)
        {
            InitializeComponent();
            frmHandler2 = frm2;
        }
        public frmMantFacturaProveedor(frmResumenEntregasRendir frm3)
        {
            InitializeComponent();
            frmHandler3 = frm3;
        }
        private void frmMantFacturaProveedor_Load(object sender, EventArgs e)
        {
            try
            {
                HabilitarBotones();
                Inicializar();
                simpleLabelItem1.AppearanceItemCaption.ForeColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
                groupControl3.AppearanceCaption.ForeColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
                glkpTipoDocumento.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }


        private void HabilitarBotones()
        {
            blSistema blSist = new blSistema();
            List<eVentana> listPermisos = blSist.ListarMenuxUsuario<eVentana>(user.cod_usuario, frmHandler != null ? frmHandler.Name : frmHandler2 != null ? frmHandler2.Name : frmHandler3 != null ? frmHandler3.Name : "");

            if (listPermisos.Count > 0)
            {
                if (listPermisos[0].flg_escritura == false) BloqueoControles(false, true, false);
            }
        }

        private void Inicializar()
        {
            
            List<eVentana> listPermisos = blSist.ListarMenuxUsuario<eVentana>(user.cod_usuario, this.Name);
            List<eVentana> listPerfil = blSist.ListarPerfilesUsuario<eVentana>(4, user.cod_usuario);
            eVentana oPerfil = listPerfil.Find(x => x.cod_perfil == 4 || x.cod_perfil == 18 || x.cod_perfil == 5);
            eFacturaProveedor eFact = new eFacturaProveedor();
            switch (MiAccion)
            {
                case Factura.Nuevo:
                    Nuevo();
                    break;
                case Factura.Editar:
                    Editar();
                    txtProveedor.Focus();
                    if (user.cod_usuario != "ADMINISTRADOR" && lkpEstadoRegistro.EditValue.ToString() == "CON")
                    {
                        txtMontoSubTotal.ReadOnly = true;
                        chkAplicaIGV.ReadOnly = true;
                        txtMontoIGV.ReadOnly = true;
                        txtMontoTotal.ReadOnly = true;
                        txtMontoPercepcion.ReadOnly = true;
                        txtMontoSaldo.ReadOnly = true;
                    }
                    if (user.cod_usuario != "ADMINISTRADOR" && lkpEstadoPago.EditValue.ToString() == "PAG" && (CajaChica == "NO" && EntregaRendir == "NO"))
                    {
                        BloqueoControles(false, true, false); 
                        btnNuevaFactura.Enabled = true;
                        btnAprobarDocumento.Enabled = lkpEstadoRegistro.EditValue.ToString() == "PEN" ? true : false;
                        if (mylistLineasDetFactura.Count == 0)
                        {
                            gvDetalleFactura.OptionsBehavior.Editable = true; btnGuardarFactura.Enabled = true;
                        }
                    }
                    if (user.cod_usuario == "ADMINISTRADOR") BloqueoControles(true, false, true);
                    eFact = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(2, tipo_documento, serie_documento, numero_documento, cod_proveedor);
                    btnAdjuntarArchivo.Enabled = eFact.flg_PDF == "SI" && eFact.flg_XML == "SI" ? false : true;
                    btnAdjuntarArchivo.Visibility = eFact.flg_PDF == "SI" && eFact.flg_XML == "SI" ? BarItemVisibility.Never : BarItemVisibility.Always;
                    btnReemplazarArchivo.Visibility = eFact.flg_PDF == "SI" || eFact.flg_XML == "SI" ? BarItemVisibility.Always : BarItemVisibility.Never;
                    
                    btnSolicitarRevision.Visibility = oPerfil != null && (eFact.cod_estado_registro == "APR" || eFact.cod_estado_registro == "REV") ? BarItemVisibility.Always : BarItemVisibility.Never;
                    btnGuardarFactura.Visibility = eFact.cod_estado_registro == "RVS" ? BarItemVisibility.Never : BarItemVisibility.Always;
                    //btnSobreescribir.Visibility = eFact.cod_estado_registro == "RVS" ? BarItemVisibility.Always : BarItemVisibility.Never;
                    btnRevisado.Visibility = eFact.cod_estado_registro == "RVS" ? BarItemVisibility.Always : BarItemVisibility.Never;
                    btnEscanearDocumento.Enabled = eFact.flg_PDF == "SI" ? false : true;
                    picBuscarProveedor.Enabled = eFact.cod_estado_registro == "RVS" ? true : picBuscarProveedor.Enabled;
                    txtRucProveedor.ReadOnly = eFact.cod_estado_registro == "RVS" ? true : txtRucProveedor.ReadOnly;
                    txtProveedor.ReadOnly = eFact.cod_estado_registro == "RVS" ? true : txtProveedor.ReadOnly;
                    glkpTipoDocumento.ReadOnly = eFact.cod_estado_registro == "RVS" ? false : glkpTipoDocumento.ReadOnly;
                    txtSerieDocumento.ReadOnly = eFact.cod_estado_registro == "RVS" ? false : txtSerieDocumento.ReadOnly;
                    txtNumeroDocumento.ReadOnly = eFact.cod_estado_registro == "RVS" ? false : txtNumeroDocumento.ReadOnly;

                    btnImportarXML.Visibility = BarItemVisibility.Never;
                    if (user.cod_usuario == "ADMINISTRADOR") { btnRevisado.Visibility = BarItemVisibility.Always; btnGuardarFactura.Visibility = BarItemVisibility.Always; }
                    break;
                case Factura.Vista:
                    Editar();
                    //BloqueoControles(false, true, false);
                    //if (user.cod_usuario == "ADMINISTRADOR") BloqueoControles(true, false, true);
                    BloqueoControles(user.cod_usuario == "ADMINISTRADOR" ? true : false, user.cod_usuario == "ADMINISTRADOR" ? false : true, user.cod_usuario == "ADMINISTRADOR" ? true : false);
                    eFact = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(2, tipo_documento, serie_documento, numero_documento, cod_proveedor);
                    btnAdjuntarArchivo.Enabled = eFact.flg_PDF == "SI" && eFact.flg_XML == "SI" ? false : true;
                    btnAdjuntarArchivo.Visibility = eFact.flg_PDF == "SI" && eFact.flg_XML == "SI" ? BarItemVisibility.Never : BarItemVisibility.Always;
                    btnReemplazarArchivo.Visibility = eFact.flg_PDF == "SI" || eFact.flg_XML == "SI" ? BarItemVisibility.Always : BarItemVisibility.Never;
                    btnEscanearDocumento.Enabled = eFact.flg_PDF == "SI" ? false : true;
                    gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                    gvObservacionesFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.None;

                    btnSolicitarRevision.Visibility = oPerfil != null && (eFact.cod_estado_registro == "APR" || eFact.cod_estado_registro == "REV") ? BarItemVisibility.Always : BarItemVisibility.Never;
                    btnGuardarFactura.Visibility = eFact.cod_estado_registro == "RVS" ? BarItemVisibility.Never : BarItemVisibility.Always;
                    btnRevisado.Visibility = eFact.cod_estado_registro == "RVS" ? BarItemVisibility.Always : BarItemVisibility.Never;
                    picBuscarProveedor.Enabled = eFact.cod_estado_registro == "RVS" ? true : picBuscarProveedor.Enabled;
                    txtRucProveedor.ReadOnly = eFact.cod_estado_registro == "RVS" ? true : txtRucProveedor.ReadOnly;
                    txtProveedor.ReadOnly = eFact.cod_estado_registro == "RVS" ? true : txtProveedor.ReadOnly;
                    glkpTipoDocumento.ReadOnly = eFact.cod_estado_registro == "RVS" ? false : glkpTipoDocumento.ReadOnly;
                    txtSerieDocumento.ReadOnly = eFact.cod_estado_registro == "RVS" ? false : txtSerieDocumento.ReadOnly;
                    txtNumeroDocumento.ReadOnly = eFact.cod_estado_registro == "RVS" ? false : txtNumeroDocumento.ReadOnly;
                    if (user.cod_usuario == "ADMINISTRADOR") { btnRevisado.Visibility = BarItemVisibility.Always; btnGuardarFactura.Visibility = BarItemVisibility.Always; }
                    break;



            }
        }

        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            btnNuevaFactura.Enabled = Enabled;
            btnGuardarFactura.Enabled = Enabled;
            btnImportarXML.Enabled = Enabled;
            btnAdjuntarArchivo.Enabled = Enabled;
            btnEscanearDocumento.Enabled = Enabled;
            btnClonarFactura.Enabled = Enabled;
            btnAnularFactura.Enabled = Enabled;
            btnAprobarDocumento.Enabled = Enabled;
            //btnVerPDF.Enabled = true;
            //btnVerDatosProveedor.Enabled = true;
            txtRucProveedor.ReadOnly = ReadOnly;
            txtProveedor.ReadOnly = ReadOnly;
            picBuscarProveedor.Enabled = Enabled;
            glkpTipoDocumento.ReadOnly = ReadOnly;
            txtSerieDocumento.ReadOnly = ReadOnly;
            txtNumeroDocumento.ReadOnly = ReadOnly;
            lkpEmpresaProveedor.ReadOnly = ReadOnly;
            lkpTipoServicioProveedor.ReadOnly = ReadOnly;
            btnAgregarServicio.Enabled = Enabled;
            txtGlosaFactura.ReadOnly = ReadOnly;
            lkpTipoMoneda.ReadOnly = ReadOnly;
            lkpModalidadPago.ReadOnly = ReadOnly;
            dtFechaDocumento.ReadOnly = ReadOnly;
            dtFechaRegistro.ReadOnly = ReadOnly;
            dtFechaVencimiento.ReadOnly = ReadOnly;
            dtFechaPagoProgramado.ReadOnly = ReadOnly;
            dtFechaPagoEjecutado.ReadOnly = ReadOnly;
            dtFechaTributaria.ReadOnly = ReadOnly;
            lkpEstadoRegistro.ReadOnly = ReadOnly;
            lkpEstadoPago.ReadOnly = ReadOnly;
            txtOrdenCompraServicio.ReadOnly = ReadOnly;
            chkFlagInventario.ReadOnly = ReadOnly;
            chkFlagActivoFijo.ReadOnly = ReadOnly;
            chkDetraccion.ReadOnly = ReadOnly;
            grdbDetraccionAplicada.ReadOnly = ReadOnly;
            lkpTipoTransaccion.ReadOnly = ReadOnly;
            lkpConceptoDetraccion.ReadOnly = ReadOnly;
            txtTasaDetraccion.ReadOnly = ReadOnly;
            txtMontoDetraccion.ReadOnly = ReadOnly;
            txtMontoPagadoDetraccion.ReadOnly = ReadOnly;
            txtConstanciaDetraccion.ReadOnly = ReadOnly;
            dtFechaConstanciaDetraccion.ReadOnly = ReadOnly;
            dtFechaPagoEjecutadoDetraccion.ReadOnly = ReadOnly;
            chkRetencion.ReadOnly = ReadOnly;
            txtTasaRetencion.ReadOnly = ReadOnly;
            txtConstanciaRetencion.ReadOnly = ReadOnly;
            dtFechaConstanciaRetencion.ReadOnly = ReadOnly;
            txtMontoRetencion.ReadOnly = ReadOnly;
            txtMontoSubTotal.ReadOnly = ReadOnly;
            chkAplicaIGV.ReadOnly = ReadOnly;
            txtMontoIGV.ReadOnly = ReadOnly;
            txtMontoTotal.ReadOnly = ReadOnly;
            txtMontoPercepcion.ReadOnly = ReadOnly;
            txtMontoSaldo.ReadOnly = ReadOnly;
            txtTipoCambio.ReadOnly = ReadOnly;

            //Distribución de centro de costos
            gvDetalleFactura.OptionsBehavior.Editable = Editable;

            //Observaciones
            gvObservacionesFactura.OptionsBehavior.Editable = Editable;

            if (habilitar_control == "SI")
            {
                btnGuardarFactura.Enabled = true;
                dtFechaPagoProgramado.ReadOnly = false;
            }
        }
        private void Nuevo()
        {
            try
            {
                CargarLookUpEdit();
                dtFechaRegistro.EditValue = DateTime.Today;
                dtFechaVencimiento.EditValue = DateTime.Today;
                dtFechaDocumento.EditValue = DateTime.Today;
                //dtFechaPagoProgramado.EditValue = DateTime.Today;
                dtFechaAnulacion.EditValue = DBNull.Value;
                btnImportarXML.Visibility = BarItemVisibility.Always;
                btnVerPDF.Enabled = false;
                btnAdjuntarArchivo.Enabled = false;
                btnEscanearDocumento.Enabled = false;
                grdbDetraccionAplicada.SelectedIndex = 1;
                btnAnularFactura.Enabled = false;
                btnAprobarDocumento.Enabled = false;
                dtFechaRegistroReal.EditValue = DateTime.Now;
                txtUsuarioRegistro.Text = user.dsc_usuario;
                dtFechaModificacion.EditValue = DateTime.Now;
                txtUsuarioCambio.Text = user.dsc_usuario;
                txtOrigenDocumento.Text = "MANUAL";
                lkpTipoMoneda.EditValue = "SOL";
                btnAgregarProyecto.Visibility = BarItemVisibility.Never;
                chkDetraccion_CheckStateChanged(chkDetraccion, new EventArgs());
                chkRetencion_CheckStateChanged(chkRetencion, new EventArgs());
                if (CajaChica == "SI" || EntregaRendir == "SI")
                {
                    lkpEstadoRegistro.EditValue = "APR"; lkpEstadoPago.EditValue = "PAG";
                    dtFechaPagoEjecutado.EditValue = dtFechaRegistro.EditValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Editar()
        {
            try
            {
                btnImportarXML.Enabled = false;
                CargarLookUpEdit();
                ObtenerDatos_FacturaProveedor();
                //chkDetraccion_CheckStateChanged(chkDetraccion, new EventArgs());
                //chkRetencion_CheckStateChanged(chkRetencion, new EventArgs());

                //HABILITAR O INHABILITAR SEGUN CHECK DETRACCION
                layoutControlItem48.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;
                layoutControlItem26.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;
                layoutControlItem39.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;
                layoutControlItem25.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;
                layoutControlItem47.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;
                layoutControlItem27.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;
                layoutControlItem29.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;
                layoutControlItem57.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;
                layoutControlItem35.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;

                //HABILITAR O INHABILITAR SEGUN CHECK RETENCION
                layoutControlItem52.Enabled = chkRetencion.CheckState == CheckState.Checked ? true : false;
                layoutControlItem54.Enabled = chkRetencion.CheckState == CheckState.Checked ? true : false;
                layoutControlItem55.Enabled = chkRetencion.CheckState == CheckState.Checked ? true : false;
                layoutControlItem56.Enabled = chkRetencion.CheckState == CheckState.Checked ? true : false;

                ObtenerDatos_DistribucionFactura();
                ObtenerDatos_ObservacionesFactura();
                ObtenerDatos_ProgramacionPagosFactura();
                ObtenerDatos_DocumentosVinculados();
                CargarListadoDetalle();
                if (FlgExistePDF == true) { btnVerPDF.Enabled = true; btnEscanearDocumento.Visibility = BarItemVisibility.Never; } else { btnVerPDF.Visibility = BarItemVisibility.Never; btnEscanearDocumento.Visibility = BarItemVisibility.Always; btnEscanearDocumento.Enabled = true; }
                if (lkpEstadoRegistro.EditValue.ToString() != "PEN") 
                { btnAprobarDocumento.Visibility = BarItemVisibility.Never; /*btnEliminarDetalle.Enabled = false;*/ }

                else { btnAprobarDocumento.Visibility = BarItemVisibility.Always; btnAprobarDocumento.Enabled = true; }
                if (lkpEstadoDocumento.EditValue.ToString() == "A") { btnAnularFactura.Visibility = BarItemVisibility.Never; btnAprobarDocumento.Visibility = BarItemVisibility.Never; } else { btnAnularFactura.Visibility = BarItemVisibility.Always; btnAnularFactura.Enabled = true; }

                //if (FlgExisteXML == true) { btnVerPDF.Enabled = true; } else { btnVerPDF.Enabled = false; }
                //if (Estado == "Anulado") { btnGuardarFactura.Enabled = false; btnAnularFactura.Enabled = false; }
                txtRucProveedor.ReadOnly = true;
                txtProveedor.ReadOnly = true;
                picBuscarProveedor.Enabled = false;
                btnVincularDocumentoNC.Enabled = listDocumentosNC.Count == 0 ? true : false;
                glkpTipoDocumento.ReadOnly = true;
                txtSerieDocumento.ReadOnly = true;
                txtNumeroDocumento.ReadOnly = true;

                if (mylistLineasDetFactura.Count > 0)
                {
                    eFacturaProveedor.eFacturaProveedor_Distribucion objFact = mylistLineasDetFactura[0];
                    if (objFact != null)
                    {
                        if (objFact.cod_tipo_gasto == "00001" || lkpEmpresaProveedor.EditValue.ToString() == "00005")
                        {
                            if (objFact.cod_proyecto != null && objFact.cod_proyecto != "")
                            {
                                if (objFact.cod_cliente != "CLI0000000" && objFact.cod_cliente != "CLI9999999")
                                {
                                    foreach (GridColumn col in gvDetalleFactura.Columns)
                                    {
                                        if (col.FieldName == "cod_und_negocio" || col.FieldName == "cod_cliente" || col.FieldName == "cod_proyecto"/* || col.FieldName == "cod_cta_contable"*/) { col.Visible = true; }
                                        //gvDetalleFactura.Columns["Sel"].VisibleIndex = 0;
                                        gvDetalleFactura.Columns["cod_tipo_gasto"].VisibleIndex = 0;
                                        gvDetalleFactura.Columns["cod_und_negocio"].VisibleIndex = 1;
                                        gvDetalleFactura.Columns["cod_cliente"].VisibleIndex = 2;
                                        gvDetalleFactura.Columns["cod_proyecto"].VisibleIndex = 3;
                                        gvDetalleFactura.Columns["cod_CECO"].VisibleIndex = 4;
                                        gvDetalleFactura.Columns["porc_distribucion"].VisibleIndex = 5;
                                        //gvDetalleFactura.Columns["cod_cta_contable"].VisibleIndex = 6;
                                        gvDetalleFactura.Columns["gridColumn1"].VisibleIndex = 7;
                                    }
                                }
                                else
                                {
                                    foreach (GridColumn col in gvDetalleFactura.Columns)
                                    {
                                        if (col.FieldName == "cod_und_negocio" || col.FieldName == "cod_cliente") { col.Visible = true; }
                                        if (col.FieldName == "cod_proyecto") { col.Visible = false; }
                                        //gvDetalleFactura.Columns["Sel"].VisibleIndex = 0;
                                        gvDetalleFactura.Columns["cod_tipo_gasto"].VisibleIndex = 0;
                                        gvDetalleFactura.Columns["cod_und_negocio"].VisibleIndex = 1;
                                        gvDetalleFactura.Columns["cod_cliente"].VisibleIndex = 2;
                                        gvDetalleFactura.Columns["cod_CECO"].VisibleIndex = 3;
                                        gvDetalleFactura.Columns["porc_distribucion"].VisibleIndex = 4;
                                        //gvDetalleFactura.Columns["cod_cta_contable"].VisibleIndex = 5;
                                        gvDetalleFactura.Columns["gridColumn1"].VisibleIndex = 6;
                                    }
                                }
                            }
                            else
                            {
                                foreach (GridColumn col in gvDetalleFactura.Columns)
                                {
                                    if (col.FieldName == "cod_und_negocio" || col.FieldName == "cod_cliente") { col.Visible = true; }
                                    if (col.FieldName == "cod_proyecto") { col.Visible = false; }
                                    //gvDetalleFactura.Columns["Sel"].VisibleIndex = 0;
                                    gvDetalleFactura.Columns["cod_tipo_gasto"].VisibleIndex = 0;
                                    gvDetalleFactura.Columns["cod_und_negocio"].VisibleIndex = 1;
                                    gvDetalleFactura.Columns["cod_cliente"].VisibleIndex = 2;
                                    gvDetalleFactura.Columns["cod_CECO"].VisibleIndex = 3;
                                    gvDetalleFactura.Columns["porc_distribucion"].VisibleIndex = 4;
                                    //gvDetalleFactura.Columns["cod_cta_contable"].VisibleIndex = 5;
                                    gvDetalleFactura.Columns["gridColumn1"].VisibleIndex = 6;
                                }
                            }
                        }
                        else
                        {
                            foreach (GridColumn col in gvDetalleFactura.Columns)
                            {
                                if (col.FieldName == "cod_und_negocio" || col.FieldName == "cod_cliente" || col.FieldName == "cod_proyecto"/* || col.FieldName == "cod_cta_contable"*/) { col.Visible = false; }
                                //gvDetalleFactura.Columns["Sel"].VisibleIndex = 0;
                                gvDetalleFactura.Columns["cod_tipo_gasto"].VisibleIndex = 0;
                                gvDetalleFactura.Columns["cod_CECO"].VisibleIndex = 1;
                                gvDetalleFactura.Columns["porc_distribucion"].VisibleIndex = 2;
                                //gvDetalleFactura.Columns["cod_cta_contable"].VisibleIndex = 3;
                                gvDetalleFactura.Columns["gridColumn1"].VisibleIndex = 4;
                            }
                        }
                    }
                    rlkpClienteEmpresa.DataSource = blFact.CombosEnGridControl<eCliente_Empresas>("ClienteEmpresa", cod_empresa: lkpEmpresaProveedor.EditValue.ToString(), cod_und_negocio: objFact.cod_und_negocio);
                    listClienteEmpresa = blFact.Obtener_MaestrosGenerales<eCliente_Empresas>(11, lkpEmpresaProveedor.EditValue.ToString(), cod_und_negocio: objFact.cod_und_negocio);
                    rlkpProyectoCliente.DataSource = blFact.Obtener_MaestrosGenerales<eCliente_Empresas>(48, lkpEmpresaProveedor.EditValue.ToString(), cod_und_negocio: objFact.cod_und_negocio, cod_cliente: objFact.cod_cliente);
                }
                //if (chkFlagInventario.CheckState == CheckState.Checked || chkFlagActivoFijo.CheckState == CheckState.Checked) gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ObtenerDatos_DistribucionFactura()
        {
            try
            {
                mylistLineasDetFactura = blFact.Obtener_LineasDetalleFactura<eFacturaProveedor.eFacturaProveedor_Distribucion>(4, tipo_documento, serie_documento, numero_documento, cod_proveedor);
                bsDistribucionFactura.DataSource = mylistLineasDetFactura;
                //mylistLineasDetFactura = blFact.Obtener_LineasCECO<eFacturaProveedor.eFacturaProveedor_Distribucion>(60, tipo_documento, serie_documento, numero_documento, cod_proveedor,lkpEmpresaProveedor.EditValue.ToString());
                //bsListadoCECOAsignados.DataSource = mylistLineasDetFactura;
                decimal porc = 0;
                for (int x = 0; x < gvDetalleFactura.DataRowCount; x++)
                {
                    eFacturaProveedor.eFacturaProveedor_Distribucion obj = gvDetalleFactura.GetRow(x) as eFacturaProveedor.eFacturaProveedor_Distribucion;
                    if (obj == null) continue;
                    porc = porc + obj.porc_distribucion;
                }
                if (porc == 1) gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                if (porc < 1) gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }



        private void ObtenerDatos_ObservacionesFactura()
        {
            try
            {
                mylistObservaciones = blFact.Obtener_LineasDetalleFactura<eFacturaProveedor.eFacturaProveedor_Observaciones>(22, tipo_documento, serie_documento, numero_documento, cod_proveedor);
                bsObservacionesFactura.DataSource = mylistObservaciones;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ObtenerDatos_ProgramacionPagosFactura()
        {
            try
            {
                mylistProgPagos = blFact.FiltroFactura<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(29, tipo_documento: tipo_documento, serie_documento: serie_documento, numero_documento: numero_documento, cod_proveedor: cod_proveedor, cod_empresa: cod_empresa);
                bsProgramacionPagos.DataSource = mylistProgPagos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ObtenerDatos_DocumentosVinculados()
        {
            try
            {
                if (glkpTipoDocumento.EditValue.ToString() == "TC006")
                {
                    listDocumentosNC = blFact.FiltroFactura<eFacturaProveedor.eFacturaProveedor_NotaCredito>(45, tipo_documento: tipo_documento, serie_documento: serie_documento, numero_documento: numero_documento, cod_proveedor: cod_proveedor, cod_empresa: cod_empresa);
                    bsDocumentosVinculados.DataSource = listDocumentosNC;
                }
                else
                {
                    listDocumentosNC = blFact.FiltroFactura<eFacturaProveedor.eFacturaProveedor_NotaCredito>(46, tipo_documento: tipo_documento, serie_documento: serie_documento, numero_documento: numero_documento, cod_proveedor: cod_proveedor, cod_empresa: cod_empresa);
                    if (listDocumentosNC.Count > 0)
                    {
                        bsDocumentosVinculados.DataSource = listDocumentosNC;
                        xtraTabPage4.PageVisible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ObtenerDatos_FacturaProveedor()
        {
            eFacturaProveedor eFact = new eFacturaProveedor();
            eFact = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(2, tipo_documento, serie_documento, numero_documento, cod_proveedor);
            if (eFact == null) return;
            //MiAccion = Factura.Editar;
            txtProveedor.Text = eFact.dsc_proveedor;
            txtProveedor.Tag = eFact.cod_proveedor;
            txtRucProveedor.Text = eFact.dsc_ruc;
            RUC = eFact.dsc_ruc;
            CajaChica = eFact.flg_CajaChica;
            EntregaRendir = eFact.flg_EntregasRendir;
            if (eFact.cod_proveedor != "")
            {
                eProveedor eProv = new eProveedor();
                eProv = blProv.ObtenerProveedor<eProveedor>(2, eFact.cod_proveedor);

                //blFact.CargaCombosLookUp("EmpresaProveedor", lkpEmpresaProveedor, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_proveedor: eFact.cod_proveedor);
                List<eProveedor_Empresas> listEmpresasUsuario = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(11, "", user.cod_usuario);
                List<eProveedor_Empresas> listEmpresas = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(7, eFact.cod_proveedor);
                if (MiAccion == Factura.Vista)
                {
                    lkpEmpresaProveedor.Properties.DataSource = listEmpresas;
                }
                else
                {
                    List<eProveedor_Empresas> listadoEmp = new List<eProveedor_Empresas>();
                    if (listEmpresas.Count > 0)
                    {
                        eProveedor_Empresas objEmp = new eProveedor_Empresas();
                        foreach (eProveedor_Empresas obj in listEmpresasUsuario)
                        {
                            objEmp = listEmpresas.Find(x => x.cod_empresa == obj.cod_empresa);
                            if (objEmp != null) listadoEmp.Add(objEmp);
                        }
                    }
                    lkpEmpresaProveedor.Properties.DataSource = listadoEmp;
                }
                List<eFacturaProveedor> list = blFact.Obtener_MaestrosGenerales<eFacturaProveedor>(25, "", txtProveedor.Tag.ToString());
                lkpTipoServicioProveedor.Properties.DataSource = list;
            }

            lkpTipoServicioProveedor.EditValue = eFact.cod_tipo_servicio;
            lkpEmpresaProveedor.EditValue = eFact.cod_empresa;
            glkpTipoDocumento.EditValue = eFact.tipo_documento;
            txtSerieDocumento.Text = eFact.serie_documento;
            //txtNumeroDocumento.Text = eFact.numero_documento.ToString();
            txtNumeroDocumento.Text = String.Format("{0:" + fmt_nro_doc + "}", eFact.numero_documento);  //$"{eFact.numero_documento:00000000}";
            txtGlosaFactura.Text = eFact.dsc_glosa;
            lkpTipoMoneda.EditValue = eFact.cod_moneda;
            dtFechaDocumento.EditValue = eFact.fch_documento;
            dtFechaRegistro.EditValue = eFact.fch_registro;
            dtFechaTipoCambio.EditValue = eFact.fch_tipocambio;
            txtTipoCambio.Text = eFact.imp_tipocambio.ToString();
            lkpModalidadPago.EditValue = eFact.cod_modalidad_pago;
            dtFechaVencimiento.EditValue = eFact.fch_vencimiento;
            dtFechaPagoProgramado.EditValue = eFact.fch_pago_programado;
            lkpEstadoDocumento.EditValue = eFact.cod_estado_documento;
            lkpEstadoRegistro.EditValue = eFact.cod_estado_registro;
            lkpEstadoPago.EditValue = eFact.cod_estado_pago;
            if (eFact.fch_pago_ejecutado.ToString().Contains("1/01/0001")) { dtFechaPagoEjecutado.EditValue = null; } else { dtFechaPagoEjecutado.EditValue = eFact.fch_pago_ejecutado; }
            txtOrigenDocumento.Text = eFact.cod_origen_documento;
            txtUsuarioAnulacion.Text = eFact.dsc_usuario_anulacion;
            if (eFact.fch_anulacion.ToString().Contains("1/01/0001")) { dtFechaAnulacion.EditValue = null; btnAnularFactura.Visibility = BarItemVisibility.Always; btnEliminarDocumento.Visibility = BarItemVisibility.Never; } else { dtFechaAnulacion.EditValue = eFact.fch_anulacion; btnAnularFactura.Visibility = BarItemVisibility.Never; btnEliminarDocumento.Visibility = BarItemVisibility.Always; }
            txtUsuarioRegistro.Text = eFact.dsc_usuario_registro;
            dtFechaRegistroReal.EditValue = eFact.fch_registro_real;
            txtUsuarioCambio.Text = eFact.dsc_usuario_cambio;
            dtFechaModificacion.EditValue = eFact.fch_cambio_real;
            txtUsuarioAprobado.Text = eFact.dsc_usuario_aprobado_reg;
            if (eFact.fch_aprobado_reg.ToString().Contains("1/01/0001")) { dtFechaAprobado.EditValue = null; } else { dtFechaAprobado.EditValue = eFact.fch_aprobado_reg; }
            txtUsuarioContabilizado.Text = eFact.dsc_usuario_contabilizado;
            if (eFact.fch_contabilizado.ToString().Contains("1/01/0001")) { dtFechaContabilizado.EditValue = null; } else { dtFechaContabilizado.EditValue = eFact.fch_contabilizado; }
            FlgExistePDF = eFact.flg_PDF == "SI" ? true : false;
            FlgExisteXML = eFact.flg_XML == "SI" ? true : false;
            chkFlagInventario.CheckState = eFact.flg_inventario == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkFlagActivoFijo.CheckState = eFact.flg_activo_fijo == "SI" ? CheckState.Checked : CheckState.Unchecked;
            dtFechaTributaria.EditValue = eFact.periodo_tributario;
            chkAplicaIGV.CheckState = eFact.flg_igv == "SI" ? CheckState.Checked : CheckState.Unchecked;
            //if (eFact.periodo_tributario.ToString().Contains("1/01/0001")) { dtFechaTributaria.EditValue = null; } else { dtFechaTributaria.EditValue = eFact.periodo_tributario; }
            txtOrdenCompraServicio.Text = eFact.num_OrdenCompraServ;
            chkDetraccion.CheckState = eFact.flg_detraccion == "SI" ? CheckState.Checked : CheckState.Unchecked;
            lkpTipoTransaccion.EditValue = eFact.cod_tipo_transaccion == "" ? null : eFact.cod_tipo_transaccion;
            lkpConceptoDetraccion.EditValue = eFact.cod_concepto_detraccion == "" ? null : eFact.cod_concepto_detraccion;
            txtTasaDetraccion.EditValue = eFact.prc_tasa_detraccion;
            txtConstanciaDetraccion.Text = eFact.num_constancia_detraccion;
            if (eFact.fch_constancia_detraccion.ToString().Contains("1/01/0001k")) { dtFechaConstanciaDetraccion.EditValue = null; } else { dtFechaConstanciaDetraccion.EditValue = eFact.fch_constancia_detraccion; }
            if (eFact.fch_pago_ejecutado_detraccion.ToString().Contains("1/01/0001")) { dtFechaPagoEjecutadoDetraccion.EditValue = null; } else { dtFechaPagoEjecutadoDetraccion.EditValue = eFact.fch_pago_ejecutado_detraccion; }
            grdbDetraccionAplicada.SelectedIndex = eFact.flg_detraccion_aplicada == "SI" ? 0 : 1;

            chkRetencion.CheckState = eFact.flg_retencion == "SI" ? CheckState.Checked : CheckState.Unchecked;
            txtTasaRetencion.EditValue = eFact.prc_tasa_retencion;
            txtConstanciaRetencion.Text = eFact.num_constancia_retencion;
            if (eFact.fch_constancia_retencion.ToString().Contains("1/01/0001")) { dtFechaConstanciaRetencion.EditValue = null; } else { dtFechaConstanciaRetencion.EditValue = eFact.fch_constancia_retencion; }

            //eFact.imp_descuento = 0;
            //txtMontoRetencion.Text = eFact.imp_retencion.ToString();
            txtMontoPercepcion.Text = eFact.imp_percepcion.ToString();
            txtMontoSubTotal.Text = eFact.imp_subtotal.ToString();
            txtMontoIGV.Text = eFact.imp_igv.ToString();
            txtMontoOtrosCargos.Text = eFact.imp_otros_cargos.ToString();
            txtMontoTotal.Text = eFact.imp_total.ToString();
            txtMontoSaldo.Text = eFact.imp_saldo.ToString();
            txtMontoDetraccion.Text = eFact.imp_detraccion.ToString();
            txtMontoPagadoDetraccion.Text = eFact.imp_detraccion_pagada.ToString();
            txtMontoRetencion.Text = eFact.imp_retencion.ToString();

            txtDescripcionProyecto.Tag = eFact.cod_proyecto;
            txtDescripcionProyecto.Text = eFact.dsc_proyecto != null && eFact.dsc_proyecto != "" ? "PROYECTO -> " + eFact.dsc_proyecto : "";
            layoutControlItem60.Visibility = eFact.dsc_proyecto != null && eFact.dsc_proyecto != "" ? LayoutVisibility.Always : LayoutVisibility.Never;
            btnAgregarProyecto.Visibility = eFact.dsc_proyecto != null && eFact.dsc_proyecto != "" ? BarItemVisibility.Never : BarItemVisibility.Always;

            tipo_documento = eFact.tipo_documento;
            serie_documento = eFact.serie_documento;
            numero_documento = eFact.numero_documento;
            cod_proveedor = eFact.cod_proveedor;
        }

        private void CargarLookUpEdit()
        {
            try
            {
                CargarCombosGridLookup("TipoComprobante", glkpTipoDocumento, "cod_tipo_comprobante", "dsc_tipo_comprobante", "", valorDefecto: true);
                blProv.CargaCombosLookUp("Moneda", lkpTipoMoneda, "cod_moneda", "dsc_moneda", "", valorDefecto: true);
                blFact.CargaCombosLookUp("ModalidadPago", lkpModalidadPago, "cod_modalidad_pago", "dsc_modalidad_pago", "", valorDefecto: true);
                //blFact.CargaCombosLookUp("TipoObligacion", lkpTipoObligacion, "cod_tipo_obligacion", "dsc_obligacion", "", valorDefecto: true);
                blFact.CargaCombosLookUp("EstadoDocumento", lkpEstadoDocumento, "cod_estado_documento", "dsc_estado_documento", "", valorDefecto: true);
                blFact.CargaCombosLookUp("EstadoRegistro", lkpEstadoRegistro, "cod_estado_registro", "dsc_estado_registro", "", valorDefecto: true);
                blFact.CargaCombosLookUp("EstadoPago", lkpEstadoPago, "cod_estado_pago", "dsc_estado_pago", "", valorDefecto: true);
                blFact.CargaCombosLookUp("TipoTransaccion", lkpTipoTransaccion, "cod_tipo_transaccion", "dsc_tipo_transaccion", "", valorDefecto: true);

                rlkpEstado.DataSource = blFact.CombosEnGridControl<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>("EstadoProgramacion");
                rlkpPagar_A.DataSource = blFact.CombosEnGridControl<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>("Pagar_A");

                glkpTipoDocumento.EditValue = "TC002";

                List< eFacturaProveedor.eFacturaProveedor_Distribucion> listCECOS = blFact.ObtenerListadoCECOS<eFacturaProveedor.eFacturaProveedor_Distribucion>(32, cod_empresa);
                bsListadoCECODisponibles.DataSource = listCECOS;
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

        private void btnGuardarFactura_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtGlosaFactura.Focus(); txtGlosaFactura.Select();
            gvDetalleFactura.PostEditor(); gvObservacionesFactura.PostEditor(); gvDetalleFactura.RefreshData(); gvObservacionesFactura.RefreshData();
            if (txtProveedor.Text.Trim() == "") { MessageBox.Show("Debe seleccionar proveedor.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtProveedor.Focus(); return; }
            if (lkpEmpresaProveedor.EditValue == null) { MessageBox.Show("Debe seleccionar una empresa.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpEmpresaProveedor.Focus(); return; }
            if (lkpTipoServicioProveedor.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de servicio.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoServicioProveedor.Focus(); return; }
            if (glkpTipoDocumento.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); glkpTipoDocumento.Focus(); return; }
            if (txtSerieDocumento.Text.Trim() == "") { MessageBox.Show("Debe ingresar una serie de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtSerieDocumento.Focus(); return; }
            if (txtNumeroDocumento.Text.Trim() == "") { MessageBox.Show("Debe ingresar un numero de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNumeroDocumento.Focus(); return; }
            if (txtGlosaFactura.Text.Trim() == "") { MessageBox.Show("Debe ingresar la glosa del documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNumeroDocumento.Focus(); return; }
            //if (dtFechaPagoEjecutado.EditValue != null && Convert.ToDecimal(txtMontoSaldo.Text) > 0) { MessageBox.Show("No puede ingresar una FECHA DE PAGO EJECUTADO ya que no se ha ejecutado el pago en la PROGRAMACIÓN.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtFechaPagoEjecutado.EditValue = null; return; }
            if (chkDetraccion.CheckState == CheckState.Checked)
            {
                if (lkpTipoTransaccion.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de transacción.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoTransaccion.Focus(); return; }
                if (lkpConceptoDetraccion.EditValue == null) { MessageBox.Show("Debe seleccionar un concepto de detracción.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpConceptoDetraccion.Focus(); return; }
                if (Convert.ToDecimal(txtMontoDetraccion.EditValue) == 0) { MessageBox.Show("El valor de la detracción debe ser diferente a 0.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtMontoDetraccion.Focus(); return; }
                //if (txtConstanciaDetraccion.Text == "" && grdbDetraccionAplicada.SelectedIndex == 0) { MessageBox.Show("Debe ingresar la constancia de la detracción", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtConstanciaDetraccion.Focus(); return; }
                //if (dtFechaConstanciaDetraccion.EditValue == null && grdbDetraccionAplicada.SelectedIndex == 0) { MessageBox.Show("Debe ingresar la fecha de la constancia de la detracción", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtFechaConstanciaDetraccion.Focus(); return; }
            }

            if (glkpTipoDocumento.EditValue.ToString() == "TC008" && Convert.ToDecimal(txtMontoTotal.EditValue) > 1500)
            {
                if (chkRetencion.CheckState == CheckState.Unchecked)
                {
                    MessageBox.Show("Debe aplicar retención si el RxH supera los 1500", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    chkRetencion.CheckState = CheckState.Checked;
                    chkRetencion.Focus();
                    return;
                }
            }

            if (chkRetencion.CheckState == CheckState.Checked)
            {
                decimal imp_ret = 0;
                imp_ret = lkpTipoMoneda.EditValue.ToString() == "SOL" ? Convert.ToDecimal(txtMontoTotal.EditValue) :
                Convert.ToDecimal(txtMontoTotal.EditValue) * Convert.ToDecimal(txtTipoCambio.EditValue);
                if (imp_ret <= 1500 && Convert.ToDecimal(txtMontoRetencion.EditValue) == 0)
                {
                    MessageBox.Show("El valor de la retención debe ser diferente a 0.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMontoDetraccion.Focus();
                    return;
                }
                if (imp_ret > 1500 && Convert.ToDecimal(txtMontoRetencion.EditValue) == 0 && txtConstanciaRetencion.Text.Trim() == "")
                {
                    MessageBox.Show("Debe ingresar el número de la constancia.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConstanciaRetencion.Focus();
                    return;
                }
            }
            if (gvDetalleFactura.DataRowCount == 0 && (chkFlagInventario.CheckState == CheckState.Unchecked && chkFlagActivoFijo.CheckState == CheckState.Unchecked)) { MessageBox.Show("Debe ingresar la distribución de Centros de Costos.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (chkFlagInventario.CheckState == CheckState.Unchecked && chkFlagActivoFijo.CheckState == CheckState.Unchecked)
            {
                decimal porc = 0;
                for (int x = 0; x < gvDetalleFactura.DataRowCount; x++)
                {
                    eFacturaProveedor.eFacturaProveedor_Distribucion eObjj = gvDetalleFactura.GetRow(x) as eFacturaProveedor.eFacturaProveedor_Distribucion;
                    if (eObjj == null) continue;
                    porc = porc + eObjj.porc_distribucion;
                }
                if (porc == 0 || porc < 1)
                {
                    MessageBox.Show("La distribución de los porcentajes de CECO no puede ser 0 o menor a 100." + Environment.NewLine + "Por favor vuelva a distribuir nuevamente los porcentajes a cada CECO.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (MiAccion == Factura.Nuevo)
            {
                eFacturaProveedor objF = new eFacturaProveedor();
                objF = blFact.ValidarFacturaProveedor<eFacturaProveedor>(30, glkpTipoDocumento.EditValue.ToString(), txtSerieDocumento.Text.ToUpper(), Convert.ToDecimal(txtNumeroDocumento.Text), txtProveedor.Tag.ToString());
                if (objF != null)
                {
                    MessageBox.Show("Ya existe el documento en el sistema, por favor validar ya que puede generar duplicidad.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (glkpTipoDocumento.EditValue.ToString() == "TC006" && listDocumentosNC.Count == 0)
            {
                MessageBox.Show("Debe vincular el documento al que se le va a aplicar la Nota de Crédito", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            eFacturaProveedor eFact = AsignarValoresFacturasProveedor();
            eFact = blFact.InsertarFacturaProveedor<eFacturaProveedor>(eFact);
            if (eFact == null) { MessageBox.Show("Error al registrar documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            RUC = eFact.dsc_ruc;
            tipo_documento = eFact.tipo_documento;
            serie_documento = eFact.serie_documento;
            numero_documento = eFact.numero_documento;
            cod_proveedor = eFact.cod_proveedor;
            if (MiAccion == Factura.Nuevo || MiAccion == Factura.Editar)
            {
                txtNumeroDocumento.Text = String.Format("{0:" + fmt_nro_doc + "}", eFact.numero_documento); //txtNumeroDocumento.Text = $"{eFact.numero_documento:00000000}";
                if (EntregaRendir == "SI")
                {
                    eEntregaRendir.eDetalle_EntregaRendir objDet = new eEntregaRendir.eDetalle_EntregaRendir();
                    objDet.cod_entregarendir = cod_entregarendir; objDet.cod_empresa = cod_empresa; objDet.cod_sede_empresa = cod_sede_empresa;
                    objDet.num_linea = 0;
                    objDet.tipo_documento = tipo_documento; objDet.serie_documento = serie_documento;
                    objDet.numero_documento = numero_documento; objDet.cod_proveedor = cod_proveedor;
                    objDet = blCaja.InsertarActualizar_DetalleEntregasRendir<eEntregaRendir.eDetalle_EntregaRendir>(objDet);
                    if (objDet == null) MessageBox.Show("Error al insertar detalle de movimiento", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (CajaChica == "SI")
                {
                    eCajaChica.eDetalleMov_CajaChica objDet = new eCajaChica.eDetalleMov_CajaChica();
                    objDet.cod_caja = cod_caja; objDet.cod_movimiento = cod_movimiento;
                    objDet.num_linea = 0;
                    objDet.tipo_documento = tipo_documento; objDet.serie_documento = serie_documento;
                    objDet.numero_documento = numero_documento; objDet.cod_proveedor = cod_proveedor;
                    objDet = blCaja.InsertarActualizar_DetalleMovCajaChica<eCajaChica.eDetalleMov_CajaChica>(objDet);
                    if (objDet == null) MessageBox.Show("Error al insertar detalle de movimiento", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (glkpTipoDocumento.EditValue.ToString() == "TC006" && listDocumentosNC.Count > 0)
                {
                    foreach (eFacturaProveedor.eFacturaProveedor_NotaCredito eNC in listDocumentosNC)
                    {
                        eNC.tipo_documento_NC = tipo_documento; eNC.serie_documento_NC = serie_documento;
                        eNC.numero_documento_NC = numero_documento; eNC.cod_proveedor_NC = cod_proveedor;
                        eFacturaProveedor.eFacturaProveedor_NotaCredito objNC = new eFacturaProveedor.eFacturaProveedor_NotaCredito();
                        objNC = blFact.InsertarNotaCreditoVinculada<eFacturaProveedor.eFacturaProveedor_NotaCredito>(eNC);
                        if (objNC == null)
                        {
                            btnAdjuntarArchivo.Enabled = true;
                            btnEscanearDocumento.Enabled = true;
                            MiAccion = Factura.Editar;
                            gvDetalleFactura.RefreshData();
                            gvObservacionesFactura.RefreshData();
                            ActualizarListado = true;
                            MessageBox.Show("Error al vincular Nota de Credito.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else //se crea una programación de pago EJECUTADO al documento vinculado
                        {
                            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objDet = blFact.ValidarFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(35, eNC.tipo_documento, eNC.serie_documento, eNC.numero_documento, eNC.cod_proveedor, "NOTACRED");
                            if (objDet != null) { if (Math.Abs(objDet.imp_pago) == Math.Abs(Convert.ToDecimal(txtMontoTotal.EditValue))) continue; }
                            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProg = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                            eProg.tipo_documento = eNC.tipo_documento; eProg.serie_documento = eNC.serie_documento;
                            eProg.numero_documento = eNC.numero_documento; eProg.cod_proveedor = eNC.cod_proveedor; eProg.cod_empresa = eNC.cod_empresa;
                            eProg.num_linea = 0; eProg.cod_tipo_prog = "NOTACRED"; eProg.cod_formapago = "NOTACRED";
                            eProg.fch_pago = Convert.ToDateTime(dtFechaDocumento.EditValue);
                            eProg.fch_ejecucion = Convert.ToDateTime(dtFechaDocumento.EditValue);
                            eProg.imp_pago = Convert.ToDecimal(txtMontoTotal.EditValue);
                            eProg.cod_pagar_a = "PROV"; eProg.dsc_observacion = null; eProg.cod_estado = "EJE";
                            eProg.cod_usuario_ejecucion = user.cod_usuario; eProg.cod_usuario_registro = user.cod_usuario;
                            eProg = blFact.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(eProg);
                        }
                    }
                }

                if (lkpEstadoPago.EditValue.ToString() == "PAG" && CajaChica == "NO" && EntregaRendir == "NO" && MiAccion == Factura.Nuevo)
                {
                    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProg2 = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                    eProg2.tipo_documento = tipo_documento; eProg2.serie_documento = serie_documento;
                    eProg2.numero_documento = numero_documento; eProg2.cod_proveedor = cod_proveedor; eProg2.cod_empresa = cod_empresa;
                    eProg2.num_linea = 0; eProg2.cod_formapago = "TRANF";
                    eProg2.cod_tipo_prog = EntregaRendir == "SI" ? "ENTREGAREN" : "REGULAR";
                    eProg2.fch_pago = EntregaRendir == "SI" ? Convert.ToDateTime(dtFechaRegistro.EditValue) : Convert.ToDateTime(dtFechaPagoProgramado.EditValue);
                    eProg2.fch_ejecucion = EntregaRendir == "SI" ? new DateTime() : Convert.ToDateTime(dtFechaPagoEjecutado.EditValue);
                    eProg2.imp_pago = Convert.ToDecimal(txtMontoTotal.EditValue);
                    eProg2.cod_pagar_a = EntregaRendir == "SI" ? "TRAB" : "PROV"; eProg2.dsc_observacion = null;
                    eProg2.cod_estado = EntregaRendir == "SI" ? "PRO" : "EJE";
                    eProg2.cod_destinatario = EntregaRendir == "SI" ? cod_trabajador : "";
                    eProg2.cod_usuario_ejecucion = EntregaRendir == "SI" ? null : user.cod_usuario; eProg2.cod_usuario_registro = user.cod_usuario;
                    eProg2 = blFact.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(eProg2);
                }
            }

            if ((chkDetraccion.CheckState == CheckState.Checked && grdbDetraccionAplicada.SelectedIndex == 0) || chkRetencion.CheckState == CheckState.Checked || glkpTipoDocumento.EditValue.ToString() == "TC006" || glkpTipoDocumento.EditValue.ToString() == "TC044")
            {
                eFacturaProveedor objDet = new eFacturaProveedor();
                string cod_tipo_prog = chkDetraccion.CheckState == CheckState.Checked ? "DETRACC" : chkRetencion.CheckState == CheckState.Checked ? "RETENC" : glkpTipoDocumento.EditValue.ToString() == "TC006" ? "NOTACRED" : glkpTipoDocumento.EditValue.ToString() == "TC044" ? "PERCEP" : "REGULAR";
                objDet = blFact.ValidarFacturaProveedor<eFacturaProveedor>(35, tipo_documento, serie_documento, numero_documento, cod_proveedor, cod_tipo_prog);
                if (objDet == null)
                {
                    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                    eProgFact.tipo_documento = tipo_documento; eProgFact.serie_documento = serie_documento;
                    eProgFact.numero_documento = numero_documento; eProgFact.cod_proveedor = cod_proveedor; eProgFact.cod_empresa = cod_empresa;
                    eProgFact.num_linea = 0;
                    eProgFact.fch_pago = chkDetraccion.CheckState == CheckState.Checked ? Convert.ToDateTime(dtFechaConstanciaDetraccion.EditValue) : chkRetencion.CheckState == CheckState.Checked ? Convert.ToDateTime(dtFechaConstanciaRetencion.EditValue) :
                                         glkpTipoDocumento.EditValue.ToString() == "TC006" ? Convert.ToDateTime(dtFechaDocumento.EditValue) : eProgFact.fch_pago;
                    eProgFact.imp_pago = chkDetraccion.CheckState == CheckState.Checked ? Convert.ToDecimal(txtMontoPagadoDetraccion.Text) : chkRetencion.CheckState == CheckState.Checked ? Convert.ToDecimal(txtMontoRetencion.Text) : Math.Abs(Convert.ToDecimal(txtMontoTotal.EditValue));
                    eProgFact.cod_pagar_a = "PROV"; eProgFact.dsc_observacion = "";
                    eProgFact.cod_estado = glkpTipoDocumento.EditValue.ToString() == "TC006" ? "EJE" : "PRO";
                    eProgFact.cod_usuario_ejecucion = glkpTipoDocumento.EditValue.ToString() == "TC006" ? user.cod_usuario : null; eProgFact.cod_usuario_registro = user.cod_usuario;
                    eProgFact.cod_tipo_prog = cod_tipo_prog; eProgFact.cod_formapago = cod_tipo_prog == "NOTACRED" ? "NOTACRED" : "TRANF";
                    eProgFact.fch_ejecucion = glkpTipoDocumento.EditValue.ToString() == "TC006" ? Convert.ToDateTime(dtFechaDocumento.EditValue) : chkRetencion.CheckState == CheckState.Checked && eProgFact.cod_estado == "EJE" ? Convert.ToDateTime(dtFechaConstanciaRetencion.EditValue) : eProgFact.fch_ejecucion;
                    eProgFact = blFact.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(eProgFact);
                }
            }

            ////AGREGAMOS UNA PROGRAMACIÓN DE PAGOS
            //eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProg = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
            //eProg.tipo_documento = tipo_documento; eProg.serie_documento = serie_documento; eProg.numero_documento = numero_documento;
            //eProg.cod_proveedor = cod_proveedor;
            //eProg.num_linea = 0;
            //eProg.fch_pago = Convert.ToDateTime(dtFechaPagoProgramado.EditValue);
            //eProg.imp_pago = Convert.ToDecimal(txtMontoSaldo.EditValue);
            //eProg.cod_pagar_a = "PROV"; eProg.dsc_observacion = null; eProg.cod_estado = "PRO";
            //eProg.cod_usuario_ejecucion = null; eProg.cod_usuario_registro = user.cod_usuario;
            //eProg = blFact.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(eProg);

            //gvDetalleFactura.PostEditor();
            //for (int x = 0; x < gvDetalleFactura.DataRowCount; x++)
            //{
            //    eFacturaProveedor.eFacturaProveedor_Distribucion obj = gvDetalleFactura.GetRow(x) as eFacturaProveedor.eFacturaProveedor_Distribucion;
            //    if (obj == null) continue;
            //    obj.tipo_documento = tipo_documento; obj.serie_documento = serie_documento; obj.numero_documento = numero_documento;
            //    obj.cod_proveedor = cod_proveedor; obj.cod_usuario_registro = user.cod_usuario; obj.cod_empresa = cod_empresa;
            //    eFacturaProveedor.eFacturaProveedor_Distribucion eDetFact = new eFacturaProveedor.eFacturaProveedor_Distribucion();
            //    eDetFact = blFact.InsertarDistribucionFacturaProveedor<eFacturaProveedor.eFacturaProveedor_Distribucion>(obj);
            //}

            gvDetalleFactura.PostEditor();
            for (int x = 0; x < gvDetalleFactura.DataRowCount; x++)
            {
                eFacturaProveedor.eFacturaProveedor_Distribucion obj = gvDetalleFactura.GetRow(x) as eFacturaProveedor.eFacturaProveedor_Distribucion;
                if (obj == null) continue;
                obj.tipo_documento = tipo_documento; obj.serie_documento = serie_documento; obj.numero_documento = numero_documento;
                obj.cod_proveedor = cod_proveedor; obj.cod_usuario_registro = user.cod_usuario; obj.cod_empresa = cod_empresa;
                eFacturaProveedor.eFacturaProveedor_Distribucion eDetFact = new eFacturaProveedor.eFacturaProveedor_Distribucion();
                eDetFact = blFact.InsertarDistribucionFacturaProveedor<eFacturaProveedor.eFacturaProveedor_Distribucion>(obj);
            }

            gvObservacionesFactura.PostEditor();
            for (int y = 0; y < gvObservacionesFactura.DataRowCount; y++)
            {
                eFacturaProveedor.eFacturaProveedor_Observaciones obj = gvObservacionesFactura.GetRow(y) as eFacturaProveedor.eFacturaProveedor_Observaciones;
                if (obj == null) continue;
                obj.tipo_documento = tipo_documento; obj.serie_documento = serie_documento; obj.numero_documento = numero_documento;
                obj.cod_proveedor = cod_proveedor; obj.cod_usuario_registro = user.cod_usuario; obj.cod_empresa = cod_empresa;
                eFacturaProveedor.eFacturaProveedor_Observaciones eObsFact = new eFacturaProveedor.eFacturaProveedor_Observaciones();
                eObsFact = blFact.InsertarObservacionesFacturaProveedor<eFacturaProveedor.eFacturaProveedor_Observaciones>(obj);
            }

            if (eFact != null)
            {
                MessageBox.Show("Se registro el documento de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnAdjuntarArchivo.Enabled = true;
                btnEscanearDocumento.Enabled = true;
                if (MiAccion == Factura.Nuevo) btnAgregarProyecto.Visibility = BarItemVisibility.Always;
                btnAprobarDocumento.Enabled = CajaChica == "SI" || EntregaRendir == "SI" ? false : true;
                btnAnularFactura.Enabled = CajaChica == "SI" || EntregaRendir == "SI" ? false : true;
                MiAccion = Factura.Editar;
                gvDetalleFactura.RefreshData();
                gvObservacionesFactura.RefreshData();
                //frmHandler.BuscarFacturas();
                ActualizarListado = true;
                //if (chkDetraccion.CheckState == CheckState.Checked || chkRetencion.CheckState == CheckState.Checked || glkpTipoDocumento.EditValue.ToString() == "TC006") { ObtenerDatos_FacturaProveedor(); ObtenerDatos_DocumentosVinculados(); }
                ObtenerDatos_FacturaProveedor(); ObtenerDatos_DistribucionFactura(); ObtenerDatos_ObservacionesFactura(); ObtenerDatos_DocumentosVinculados(); ObtenerDatos_ProgramacionPagosFactura();
                
            }
        }

        private void btnVerDatosProveedor_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (txtProveedor.Tag != null && txtProveedor.Tag.ToString() != "")
                {
                    frmMantProveedor frm = new frmMantProveedor();
                    frm.cod_proveedor = txtProveedor.Tag.ToString();
                    frm.MiAccion = Proveedor.Vista;
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorEventRow = colorEventRow;
                    frm.colorFocus = colorFocus;
                    //frm.cod_empresa = lkpEmpresaProveedor.EditValue.ToString();
                    frm.user = user;
                    frm.ShowDialog();
                    if (frm.ActualizarListado == "SI")
                    {
                        string cod_emp = lkpEmpresaProveedor.EditValue != null ? lkpEmpresaProveedor.EditValue.ToString() : null;
                        List<eProveedor_Empresas> listEmpresasUsuario = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(11, "", user.cod_usuario);
                        List<eProveedor_Empresas> listEmpresas = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(7, txtProveedor.Tag.ToString());
                        List<eProveedor_Empresas> listadoEmp = new List<eProveedor_Empresas>();
                        listEmpresasUsuario = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(11, "", user.cod_usuario);
                        listEmpresas = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(7, txtProveedor.Tag.ToString());
                        listadoEmp = new List<eProveedor_Empresas>();
                        if (listEmpresas.Count > 0)
                        {
                            eProveedor_Empresas objEmp = new eProveedor_Empresas();
                            foreach (eProveedor_Empresas objj in listEmpresasUsuario)
                            {
                                objEmp = listEmpresas.Find(x => x.cod_empresa == objj.cod_empresa);
                                if (objEmp != null) listadoEmp.Add(objEmp);
                            }
                        }
                        lkpEmpresaProveedor.Properties.DataSource = listadoEmp;
                        lkpEmpresaProveedor.EditValue = cod_emp;
                    } 
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un proveedor", "Datos del Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private eFacturaProveedor AsignarValoresFacturasProveedor()
        {
            eFacturaProveedor eFact = new eFacturaProveedor();
            eFact.cod_proveedor = txtProveedor.Tag.ToString();
            eFact.dsc_ruc = txtRucProveedor.Text;
            eFact.cod_tipo_servicio = lkpTipoServicioProveedor.EditValue.ToString();
            eFact.cod_empresa = lkpEmpresaProveedor.EditValue.ToString();
            eFact.tipo_documento = glkpTipoDocumento.EditValue.ToString();
            eFact.serie_documento = txtSerieDocumento.Text.ToUpper();
            eFact.numero_documento = txtNumeroDocumento.Text == "" ? 0 : Convert.ToDecimal(txtNumeroDocumento.Text);
            eFact.dsc_glosa = txtGlosaFactura.Text;
            eFact.cod_moneda = lkpTipoMoneda.EditValue.ToString();
            eFact.cod_modalidad_pago = lkpModalidadPago.EditValue.ToString();
            eFact.fch_documento = Convert.ToDateTime(dtFechaDocumento.EditValue);
            eFact.cod_estado_documento = lkpEstadoDocumento.EditValue.ToString();
            eFact.fch_tipocambio = Convert.ToDateTime(dtFechaTipoCambio.EditValue);
            eFact.imp_tipocambio = Convert.ToDecimal(txtTipoCambio.Text);
            eFact.fch_registro = Convert.ToDateTime(dtFechaRegistro.EditValue);
            eFact.fch_vencimiento = Convert.ToDateTime(dtFechaVencimiento.EditValue);
            eFact.fch_pago_programado = Convert.ToDateTime(dtFechaPagoProgramado.EditValue);
            eFact.fch_pago_ejecutado = dtFechaPagoEjecutado.EditValue == null || dtFechaPagoEjecutado.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtFechaPagoEjecutado.EditValue);
            eFact.cod_estado_registro = lkpEstadoRegistro.EditValue.ToString();
            eFact.cod_estado_pago = lkpEstadoPago.EditValue.ToString();
            eFact.cod_origen_documento = txtOrigenDocumento.Text;
            eFact.flg_detraccion = chkDetraccion.CheckState == CheckState.Checked ? "SI" : "NO";
            eFact.cod_tipo_transaccion = lkpTipoTransaccion.EditValue == null ? "" : lkpTipoTransaccion.EditValue.ToString();
            eFact.cod_concepto_detraccion = lkpConceptoDetraccion.EditValue == null ? "" : lkpConceptoDetraccion.EditValue.ToString();
            eFact.prc_tasa_detraccion = txtTasaDetraccion.Text == "" ? 0 : Convert.ToDecimal(txtTasaDetraccion.EditValue);
            eFact.imp_detraccion = txtMontoDetraccion.Text == "" ? 0 : Convert.ToDecimal(txtMontoDetraccion.Text);
            eFact.imp_detraccion_pagada = txtMontoPagadoDetraccion.Text == "" ? 0 : Convert.ToDecimal(txtMontoPagadoDetraccion.Text);
            eFact.num_constancia_detraccion = txtConstanciaDetraccion.Text;
            eFact.fch_constancia_detraccion = dtFechaConstanciaDetraccion.EditValue == null || dtFechaConstanciaDetraccion.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtFechaConstanciaDetraccion.EditValue);
            eFact.flg_detraccion_aplicada = grdbDetraccionAplicada.SelectedIndex == 0 ? "SI" : "NO";
            eFact.fch_pago_ejecutado_detraccion = dtFechaPagoEjecutadoDetraccion.EditValue == null || dtFechaPagoEjecutadoDetraccion.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtFechaPagoEjecutadoDetraccion.EditValue);

            eFact.flg_retencion = chkRetencion.CheckState == CheckState.Checked ? "SI" : "NO";
            eFact.prc_tasa_retencion = txtTasaRetencion.Text == "" ? 0 : Convert.ToDecimal(txtTasaRetencion.EditValue);
            eFact.num_constancia_retencion = txtConstanciaRetencion.Text;
            eFact.fch_constancia_retencion = dtFechaConstanciaRetencion.EditValue == null || dtFechaConstanciaRetencion.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtFechaConstanciaRetencion.EditValue);

            eFact.flg_igv = chkAplicaIGV.CheckState == CheckState.Checked ? "SI" : "NO";
            eFact.porc_igv = (decimal)0.18;
            eFact.flg_inventario = chkFlagInventario.CheckState == CheckState.Checked ? "SI" : "NO";
            eFact.flg_activo_fijo = chkFlagActivoFijo.CheckState == CheckState.Checked ? "SI" : "NO";
            eFact.periodo_tributario = dtFechaTributaria.EditValue == null || dtFechaTributaria.EditValue == "" ? "" : Convert.ToDateTime(dtFechaTributaria.EditValue).ToString("MM-yyyy");
            //eFact.periodo_tributario = dtFechaTributaria.EditValue == null || dtFechaTributaria.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtFechaTributaria.EditValue);
            eFact.num_OrdenCompraServ = txtOrdenCompraServicio.Text;

            eFact.imp_descuento = 0;
            eFact.imp_retencion = Convert.ToDecimal(txtMontoRetencion.Text);
            eFact.imp_percepcion = Convert.ToDecimal(txtMontoPercepcion.Text);
            eFact.imp_subtotal = Convert.ToDecimal(txtMontoSubTotal.Text);
            eFact.imp_igv = Convert.ToDecimal(txtMontoIGV.Text);
            eFact.imp_otros_cargos = Convert.ToDecimal(txtMontoOtrosCargos.Text);
            eFact.imp_total = Convert.ToDecimal(txtMontoTotal.Text);
            eFact.imp_saldo = Convert.ToDecimal(txtMontoSaldo.Text);
            eFact.cod_usuario_registro = user.cod_usuario;
            eFact.flg_CajaChica = CajaChica;
            eFact.flg_EntregasRendir = EntregaRendir;

            eFact.cod_proyecto = txtDescripcionProyecto.Tag == null || txtDescripcionProyecto.Tag.ToString() == "" ? null : txtDescripcionProyecto.Tag.ToString();

            return eFact;
        }

        private void btnNuevaFactura_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                CargarLookUpEdit();
                txtRucProveedor.Text = "";
                txtProveedor.Text = "";
                txtProveedor.Tag = null;
                lkpEmpresaProveedor.EditValue = null;
                glkpTipoDocumento.EditValue = null;
                txtSerieDocumento.Text = "";
                txtNumeroDocumento.Text = "";
                txtGlosaFactura.Text = "";
                lkpTipoServicioProveedor.EditValue = null;
                lkpTipoMoneda.EditValue = null;
                lkpModalidadPago.EditValue = null;
                lkpEstadoDocumento.EditValue = "C";
                dtFechaPagoProgramado.EditValue = DateTime.Today;
                dtFechaPagoEjecutado.EditValue = null;
                lkpEstadoRegistro.EditValue = "PEN";
                lkpEstadoPago.EditValue = "PEN";
                chkDetraccion.CheckState = CheckState.Unchecked;
                lkpConceptoDetraccion.EditValue = null;
                txtMontoDetraccion.EditValue = 0;
                txtConstanciaDetraccion.Text = "";
                dtFechaConstanciaDetraccion.EditValue = DBNull.Value;
                dtFechaPagoEjecutadoDetraccion.EditValue = DBNull.Value;
                txtTasaDetraccion.EditValue = 0;
                chkRetencion.CheckState = CheckState.Unchecked;
                txtTasaRetencion.EditValue = 0;
                txtConstanciaRetencion.Text = "";
                dtFechaConstanciaRetencion.EditValue = DBNull.Value;
                txtMontoRetencion.EditValue = 0;
                txtUsuarioAnulacion.Text = "";
                txtMontoSaldo.Text = "0.00";
                txtMontoSubTotal.Text = "0.00";
                txtMontoIGV.Text = "0.00";
                txtMontoTotal.Text = "0.00";
                dtFechaRegistro.EditValue = DateTime.Today;
                dtFechaVencimiento.EditValue = DateTime.Today;
                dtFechaDocumento.EditValue = DateTime.Today;
                dtFechaTipoCambio.EditValue = DateTime.Today;
                dtFechaAnulacion.EditValue = DBNull.Value;
                btnVerPDF.Enabled = false;
                btnAdjuntarArchivo.Enabled = false;
                btnEscanearDocumento.Enabled = false;
                grdbDetraccionAplicada.SelectedIndex = 1;
                btnAnularFactura.Enabled = false;
                btnAprobarDocumento.Enabled = false;
                dtFechaRegistroReal.EditValue = DateTime.Now;
                txtUsuarioRegistro.Text = user.dsc_usuario;
                dtFechaModificacion.EditValue = DateTime.Now;
                txtUsuarioCambio.Text = user.dsc_usuario;
                txtOrigenDocumento.Text = "MANUAL";
                lkpTipoMoneda.EditValue = "SOL";
                mylistLineasDetFactura.Clear(); mylistObservaciones.Clear(); mylistProgPagos.Clear();
                bsDistribucionFactura.DataSource = mylistLineasDetFactura;
                bsObservacionesFactura.DataSource = mylistObservaciones;
                bsProgramacionPagos.DataSource = mylistProgPagos;
                gvDetalleFactura.RefreshData(); gvObservacionesFactura.RefreshData();
                glkpTipoDocumento.EditValue = "TC002";
                lkpModalidadPago.EditValue = "MP001";
                dtFechaTributaria.EditValue = null;
                txtOrdenCompraServicio.Text = "";
                chkFlagActivoFijo.CheckState = CheckState.Unchecked;
                chkFlagInventario.CheckState = CheckState.Unchecked;

                if (CajaChica == "SI" || EntregaRendir == "SI")
                {
                    lkpEstadoRegistro.EditValue = "APR"; lkpEstadoPago.EditValue = "PAG";
                    dtFechaPagoEjecutado.EditValue = dtFechaRegistro.EditValue;
                }

                picBuscarProveedor.Enabled = true;
                picBuscarProveedor.Select();
                MiAccion = Factura.Nuevo;
                btnImportarXML.Enabled = true;
                txtRucProveedor.ReadOnly = false;
                txtProveedor.ReadOnly = false;
                glkpTipoDocumento.ReadOnly = false;
                txtSerieDocumento.ReadOnly = false;
                txtNumeroDocumento.ReadOnly = false;
                gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (MiAccion == Factura.Nuevo)
                {
                    blGlobal.pKeyDown(txtProveedor, e);
                    if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) { txtProveedor.Tag = null; txtRucProveedor.Text = ""; lkpEmpresaProveedor.EditValue = null; lkpEmpresaProveedor.Properties.DataSource = null; bsDistribucionFactura.DataSource = null; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (MiAccion == Factura.Nuevo)
            {
                if (e.KeyChar == 13)
                {
                    Busqueda("", "Proveedor");
                }
                string dato = blGlobal.pKeyPress(txtProveedor, e);
                if (dato != "")
                {
                    Busqueda(dato, "Proveedor");
                }
            }
        }

        private void frmMantFacturaProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && MiAccion != Factura.Nuevo) this.Close();
            if (e.KeyCode == Keys.F5 && MiAccion != Factura.Nuevo) this.Refresh();
        }

        private void txtRucProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (MiAccion == Factura.Nuevo)
                {
                    blGlobal.pKeyDown(txtProveedor, e);
                    if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) { txtProveedor.Tag = null; txtRucProveedor.Text = ""; lkpEmpresaProveedor.EditValue = null; lkpEmpresaProveedor.Properties.DataSource = null; bsDistribucionFactura.DataSource = null; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtRucProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (MiAccion == Factura.Nuevo)
            {
                if (e.KeyChar == 13)
                {
                    Busqueda("", "Proveedor", "SI");
                }
                string dato = blGlobal.pKeyPress(txtProveedor, e);
                if (dato != "")
                {
                    Busqueda(dato, "Proveedor", "SI");
                }
            }
        }


        private void chkDetraccion_CheckStateChanged(object sender, EventArgs e)
        {
            layoutControlItem48.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;
            layoutControlItem26.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;
            layoutControlItem39.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;
            layoutControlItem25.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;
            layoutControlItem47.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;
            layoutControlItem27.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;
            layoutControlItem29.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;
            layoutControlItem57.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;
            layoutControlItem35.Enabled = chkDetraccion.CheckState == CheckState.Checked ? true : false;
            //dtFechaConstanciaDetraccion.EditValue = DateTime.Today;
            if (chkDetraccion.CheckState == CheckState.Unchecked)
            {
                lkpTipoTransaccion.EditValue = null;
                lkpConceptoDetraccion.EditValue = null;
                txtMontoPagadoDetraccion.EditValue = 0;
                txtConstanciaDetraccion.Text = "";
                dtFechaConstanciaDetraccion.EditValue = null;
                dtFechaPagoEjecutadoDetraccion.EditValue = null;
            }
            else
            {
                //FECHA PAGO DETRACCIÓN, es el viernes proximo de la fecha de recepción
                //DateTime fecha = new DateTime(Convert.ToDateTime(dtFechaRegistro.EditValue).Year, Convert.ToDateTime(dtFechaRegistro.EditValue).Month + 1, 1);
                DateTime fecha = Convert.ToDateTime(dtFechaRegistro.EditValue).AddMonths(1);
                fecha = new DateTime(fecha.Year, fecha.Month, 1);
                int nDia = 0, cont = 0;
                for (int x = 0; x <= 9; x++)
                {
                    nDia = Convert.ToInt32(fecha.AddDays(x).DayOfWeek);
                    if (nDia >= 1 && nDia <= 5) cont = cont + 1;
                    if (cont == 5 && (nDia >= 1 && nDia <= 5)) dtFechaConstanciaDetraccion.EditValue = fecha.AddDays(x) > Convert.ToDateTime(dtFechaPagoProgramado.EditValue) ? dtFechaPagoProgramado.EditValue : fecha.AddDays(x);
                }
            }
        }

        private void chkRetencion_CheckStateChanged(object sender, EventArgs e)
        {
            layoutControlItem52.Enabled = chkRetencion.CheckState == CheckState.Checked ? true : false;
            layoutControlItem54.Enabled = chkRetencion.CheckState == CheckState.Checked ? true : false;
            layoutControlItem55.Enabled = chkRetencion.CheckState == CheckState.Checked ? true : false;
            layoutControlItem56.Enabled = chkRetencion.CheckState == CheckState.Checked ? true : false;
            if (chkRetencion.CheckState == CheckState.Unchecked)
            {
                txtTasaRetencion.EditValue = 0;
                txtConstanciaRetencion.Text = "";
                dtFechaConstanciaRetencion.EditValue = null;
                txtMontoRetencion.EditValue = 0;
            }
            else
            {
                txtTasaRetencion.EditValue = 0.08;
                txtMontoRetencion.EditValue = (Convert.ToDecimal(txtMontoTotal.EditValue) * Convert.ToDecimal(txtTasaRetencion.EditValue)).ToString();

                //FECHA PAGO DETRACCIÓN, es el viernes proximo de la fecha de recepción
                //DateTime fecha = new DateTime(Convert.ToDateTime(dtFechaRegistro.EditValue).Year, Convert.ToDateTime(dtFechaRegistro.EditValue).Month + 1, 1);
                DateTime fecha = Convert.ToDateTime(dtFechaRegistro.EditValue).AddMonths(1);
                fecha = new DateTime(fecha.Year, fecha.Month, 1);
                int nDia = 0, cont = 0;
                for (int x = 0; x <= 15; x++)
                {
                    nDia = Convert.ToInt32(fecha.AddDays(x).DayOfWeek);
                    if (nDia >= 1 && nDia <= 5) cont = cont + 1;
                    if (cont == 10 && (nDia >= 1 && nDia <= 5)) dtFechaConstanciaRetencion.EditValue = fecha.AddDays(x);
                }
            }
        }

        private void lkpModalidadPago_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lkpModalidadPago.EditValue != null)
                {
                    eFacturaProveedor obj = blFact.BuscarModalidadPago<eFacturaProveedor>(2, lkpModalidadPago.EditValue.ToString());
                    DateTime FechaRegistro;
                    if (obj.num_dias != 0)
                    {
                        FechaRegistro = Convert.ToDateTime(dtFechaRegistro.EditValue);
                        dtFechaVencimiento.EditValue = FechaRegistro.AddDays(obj.num_dias);
                    }
                    else
                    {
                        dtFechaVencimiento.EditValue = dtFechaRegistro.EditValue;
                    }
                }
                else
                {
                    dtFechaVencimiento.EditValue = dtFechaRegistro.EditValue;
                }
                if (dtFechaVencimiento.EditValue.ToString().Contains("0/00/0000")) return;
                //FECHA PAGO PROGRAMADO, es el viernes proximo de la fecha de vencimiento
                int nDia = Convert.ToInt32(Convert.ToDateTime(dtFechaVencimiento.EditValue).DayOfWeek);
                nDia = nDia <= 5 ? 5 - nDia : nDia;
                dtFechaPagoProgramado.EditValue = Convert.ToDateTime(dtFechaVencimiento.EditValue).AddDays(nDia);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dtFechaRegistro_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lkpModalidadPago.EditValue != null)
                {
                    eFacturaProveedor obj = blFact.BuscarModalidadPago<eFacturaProveedor>(2, lkpModalidadPago.EditValue.ToString());
                    DateTime FechaRegistro;
                    if (obj.num_dias != 0)
                    {
                        FechaRegistro = Convert.ToDateTime(dtFechaRegistro.EditValue);
                        dtFechaVencimiento.EditValue = FechaRegistro.AddDays(obj.num_dias);
                    }
                    else
                    {
                        dtFechaVencimiento.EditValue = dtFechaRegistro.EditValue;
                    }
                }
                else
                {
                    dtFechaVencimiento.EditValue = dtFechaRegistro.EditValue;
                }
                if (dtFechaVencimiento.EditValue.ToString().Contains("0/00/0000")) return;
                //FECHA PAGO PROGRAMADO, es el viernes proximo de la fecha de vencimiento
                int nDia = Convert.ToInt32(Convert.ToDateTime(dtFechaVencimiento.EditValue).DayOfWeek);
                nDia = nDia <= 5 ? 5 - nDia : nDia;
                dtFechaPagoProgramado.EditValue = Convert.ToDateTime(dtFechaVencimiento.EditValue).AddDays(nDia);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void dtFechaDocumento_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dtFechaRegistro.EditValue = dtFechaDocumento.EditValue;
                //if (lkpModalidadPago.EditValue != null)
                //{
                //    eFacturaProveedor obj = blFact.BuscarModalidadPago<eFacturaProveedor>(2, lkpModalidadPago.EditValue.ToString());
                //    DateTime FechaRegistro;
                //    if (obj.num_dias != 0)
                //    {
                //        FechaRegistro = Convert.ToDateTime(dtFechaRegistro.EditValue);
                //        dtFechaVencimiento.EditValue = FechaRegistro.AddDays(obj.num_dias);
                //    }
                //    else
                //    {
                //        dtFechaVencimiento.EditValue = dtFechaRegistro.EditValue;
                //    }
                //}
                //else
                //{
                //    dtFechaVencimiento.EditValue = dtFechaRegistro.EditValue;
                //}
                //if (dtFechaVencimiento.EditValue.ToString().Contains("0/00/0000")) return;
                ////FECHA PAGO PROGRAMADO, es el viernes proximo de la fecha de vencimiento
                //int nDia = Convert.ToInt32(Convert.ToDateTime(dtFechaVencimiento.EditValue).DayOfWeek);
                //nDia = nDia <= 5 ? 5 - nDia : nDia;
                //dtFechaPagoProgramado.EditValue = Convert.ToDateTime(dtFechaVencimiento.EditValue).AddDays(nDia);

                dtFechaTipoCambio.EditValue = dtFechaDocumento.EditValue;
                TraerTipoCambio();
                if (txtTipoCambio.Text == "0.00")
                {
                    frmMantTipoCambio frm = new frmMantTipoCambio();
                    frm.user = user;
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorEventRow = colorEventRow;
                    frm.colorFocus = colorFocus;
                    frm.ShowDialog();
                    TraerTipoCambio();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void TraerTipoCambio()
        {
            eTipoCambio objj = blFact.BuscarTipoCambio<eTipoCambio>(9, Convert.ToDateTime(dtFechaTipoCambio.EditValue));
            if (objj != null)
            {
                txtTipoCambio.Text = objj.imp_cambio_venta.ToString();
            }
            else
            {
                MessageBox.Show("No existe tipo de cambio registrado para la fecha seleccionada", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTipoCambio.Text = "0.00";

            }
        }
        private void picBuscarProveedor_Click(object sender, EventArgs e)
        {
            Busqueda("", "Proveedor");
        }

        public void Busqueda(string dato, string tipo, string filtroRUC = "NO")
        {
            frmBusquedas frm = new frmBusquedas();
            frm.user = user;
            frm.filtro = dato;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            switch (tipo)
            {
                case "Proveedor":
                    frm.entidad = frmBusquedas.MiEntidad.Proveedor;
                    frm.filtroRUC = filtroRUC;
                    frm.filtro = dato;
                    
                    break;
                case "Servicio":
                    frm.entidad = frmBusquedas.MiEntidad.Servicios;
                    frm.cod_proveedor = txtProveedor.Tag.ToString();
                    frm.BotonAgregarVisible = 1;
                    break;
                case "Proyecto":
                    frm.entidad = frmBusquedas.MiEntidad.Proyecto;
                    frm.cod_empresa = lkpEmpresaProveedor.EditValue.ToString();
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Proveedor":
                    txtProveedor.Tag = frm.codigo;
                    txtProveedor.Text = frm.descripcion;
                    if (frm.descripcion == "") { txtProveedor.Tag = null; txtRucProveedor.Text = ""; lkpEmpresaProveedor.EditValue = null; lkpEmpresaProveedor.Properties.DataSource = null; bsDistribucionFactura.DataSource = null; }
                    if (frm.codigo != "")
                    {
                        eProveedor eProv = new eProveedor();
                        eProv = blProv.ObtenerProveedor<eProveedor>(2, frm.codigo);
                        txtRucProveedor.Text = eProv.num_documento;

                        //blFact.CargaCombosLookUp("EmpresaProveedor", lkpEmpresaProveedor, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_proveedor: frm.codigo);
                        List<eProveedor_Empresas> listEmpresasUsuario = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(11, "", user.cod_usuario);
                        List<eProveedor_Empresas> listEmpresas = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(7, frm.codigo);
                        List<eProveedor_Empresas> listadoEmp = new List<eProveedor_Empresas>();
                        if (listEmpresas.Count > 0)
                        {
                            eProveedor_Empresas objEmp = new eProveedor_Empresas();
                            foreach (eProveedor_Empresas obj in listEmpresasUsuario)
                            {
                                objEmp = listEmpresas.Find(x => x.cod_empresa == obj.cod_empresa);
                                if (objEmp != null) listadoEmp.Add(objEmp);
                            }
                        }
                        lkpEmpresaProveedor.Properties.DataSource = listadoEmp;
                        //if (listadoEmp.Count == 1) lkpEmpresaProveedor.EditValue = listadoEmp[0].cod_empresa;
                        if (listadoEmp.Count == 0)
                        {
                            if (MessageBox.Show("¿Desea vincular el proveedor a su Empresa?", "Vincular proveedor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                eProveedor_Empresas obj = new eProveedor_Empresas();
                                obj.cod_empresa = listEmpresasUsuario[0].cod_empresa;
                                obj.cod_proveedor = frm.codigo; obj.flg_activo = "SI";
                                obj.valorRating = 0; obj.cod_usuario_registro = user.cod_usuario;
                                eProveedor_Empresas eProvEmp = blProv.Guardar_Actualizar_ProveedorEmpresas<eProveedor_Empresas>(obj);
                                if (eProvEmp == null) { MessageBox.Show("Error al vincular empresa", "Vincular empresa", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                if (eProvEmp != null)
                                {
                                    MessageBox.Show("Se vinculó la empresa de manera satisfactoria", "Vincular empresa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    listEmpresasUsuario = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(11, "", user.cod_usuario);
                                    listEmpresas = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(7, frm.codigo);
                                    listadoEmp = new List<eProveedor_Empresas>();
                                    if (listEmpresas.Count > 0)
                                    {
                                        eProveedor_Empresas objEmp = new eProveedor_Empresas();
                                        foreach (eProveedor_Empresas objj in listEmpresasUsuario)
                                        {
                                            objEmp = listEmpresas.Find(x => x.cod_empresa == objj.cod_empresa);
                                            if (objEmp != null) listadoEmp.Add(objEmp);
                                        }
                                    }
                                    lkpEmpresaProveedor.Properties.DataSource = listadoEmp;
                                }
                            }
                        }
                        if (MiAccion == Factura.Nuevo && lkpEmpresaProveedor.EditValue == null && cod_empresa != "") lkpEmpresaProveedor.EditValue = cod_empresa;
                        if (lkpEmpresaProveedor.EditValue == null && listEmpresasUsuario.Count == 1) lkpEmpresaProveedor.EditValue = listEmpresasUsuario[0].cod_empresa;
                        if (lkpEmpresaProveedor.EditValue == null && listadoEmp.Count == 1) lkpEmpresaProveedor.EditValue = listadoEmp[0].cod_empresa;
                        lkpModalidadPago.EditValue = eProv.cod_modalidad_pago;

                        List<eFacturaProveedor> list = blFact.Obtener_MaestrosGenerales<eFacturaProveedor>(25, "", txtProveedor.Tag.ToString());
                        lkpTipoServicioProveedor.Properties.DataSource = list;
                        if (list.Count == 1) lkpTipoServicioProveedor.EditValue = list[0].cod_tipo_servicio;
                    }
                    break;
                case "Servicio":
                    //blFact.CargaCombosLookUp("EmpresaProveedor", lkpEmpresaProveedor, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_proveedor: txtProveedor.Tag.ToString());
                    List<eFacturaProveedor> lista = blFact.Obtener_MaestrosGenerales<eFacturaProveedor>(25, "", txtProveedor.Tag.ToString());
                    lkpTipoServicioProveedor.Properties.DataSource = lista;
                    lkpTipoServicioProveedor.EditValue = frm.codigo;
                    break;
                case "Proyecto":
                    txtDescripcionProyecto.Tag = frm.codigo;
                    txtDescripcionProyecto.Text = "PROYECTO -> " + frm.descripcion;
                    layoutControlItem60.Visibility = LayoutVisibility.Always;
                    btnAgregarProyecto.Visibility = BarItemVisibility.Never;
                    break;
            }
        }

        private async void btnReemplazarArchivo_ItemClick(object sender, ItemClickEventArgs e)
        {
            await AdjuntarArchivo();
        }

        private async void btnAdjuntarArchivo_ItemClick(object sender, ItemClickEventArgs e)
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
                DateTime FechaRegistro = Convert.ToDateTime(dtFechaRegistro.EditValue);
                //VALIDAR SI EL PERIODO SE ENCUENTRA ABIERTO
                eFacturaProveedor objTrib = blFact.Obtener_PeriodoTributario<eFacturaProveedor>(50, FechaRegistro.ToString("MM-yyyy"), lkpEmpresaProveedor.EditValue.ToString());
                if (objTrib != null && objTrib.flg_cerrado == "SI") 
                {
                    eFacturaProveedor objTrib2 = blFact.Obtener_PeriodoTributario<eFacturaProveedor>(51, "", lkpEmpresaProveedor.EditValue.ToString());
                    int n_Mes = 0, n_Anho = 0;
                    n_Mes = Convert.ToInt32(objTrib2.periodo_tributario.Substring(0, 2));
                    n_Anho = Convert.ToInt32(objTrib2.periodo_tributario.Substring(3, 4));
                    n_Anho = n_Mes == 12 ? n_Anho + 1 : n_Anho;
                    n_Mes = n_Mes == 12 ? 1 : n_Mes + 1; 
                    FechaRegistro = new DateTime(n_Anho, n_Mes, 01);
                }

                int Anho = FechaRegistro.Year; int Mes = FechaRegistro.Month; string NombreMes = FechaRegistro.ToString("MMMM");
                eFacturaProveedor resultado = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(2, tipo_documento, serie_documento, numero_documento, cod_proveedor);
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
                        TD_sunat = list.Find(x => x.tipo_documento == tipo_documento).cod_sunat;
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
                    eEmpresa eEmp = blFact.ObtenerDatosEmpresa<eEmpresa>(12, lkpEmpresaProveedor.EditValue.ToString());
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
                    eDatos = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(26, lkpEmpresaProveedor.EditValue.ToString(), Convert.ToDateTime(dtFechaRegistro.EditValue).Year, dsc_Carpeta: dsc_Carpeta);
                    var targetItemFolderId = eDatos.idCarpeta;

                    //eFacturaProveedor IdCarpetaAnho = blFact.ObtenerDatosOneDrive<eFacturaProveedor>(13, lkpEmpresaProveedor.EditValue.ToString(), Convert.ToDateTime(dtFechaRegistro.EditValue).Year);
                    eEmpresa.eOnedrive_Empresa IdCarpetaAnho = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(13, lkpEmpresaProveedor.EditValue.ToString(), FechaRegistro.Year, dsc_Carpeta: dsc_Carpeta);
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
                    eEmpresa.eOnedrive_Empresa IdCarpetaMes = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(14, lkpEmpresaProveedor.EditValue.ToString(), FechaRegistro.Year, FechaRegistro.Month, dsc_Carpeta);
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
                    eFacturaProveedor eFact = new eFacturaProveedor();
                    eFact = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(2, tipo_documento, serie_documento, numero_documento, cod_proveedor);
                    //////////////////////////// ELIMINAR DOCUMENTO DE ONEDRIVE ////////////////////////////
                    if (eFact.idPDF != null && eFact.idPDF != "" && Extension.ToLower() == ".pdf") await Mover_Eliminar_ArchivoOneDrive(eFact, new DateTime(), true, false, "ELIMINAR");
                    if (eFact.idXML != null && eFact.idXML != "" && Extension.ToLower() == ".xml") await Mover_Eliminar_ArchivoOneDrive(eFact, new DateTime(), false, true, "ELIMINAR");
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
                            objFact.tipo_documento = resultado.tipo_documento;
                            objFact.serie_documento = resultado.serie_documento;
                            objFact.numero_documento = resultado.numero_documento;
                            objFact.cod_proveedor = resultado.cod_proveedor;
                            objFact.idPDF = idArchivoPDF;
                            objFact.idXML = idArchivoXML;
                            //objFact.NombreArchivo = varNombreArchivo;
                            objFact.NombreArchivo = varNombreArchivoSinExtension;
                            objFact.cod_empresa = lkpEmpresaProveedor.EditValue.ToString();
                            objFact.idCarpetaAnho = IdArchivoAnho;
                            objFact.idCarpetaMes = IdArchivoMes;

                            res = blFact.ActualizarInformacionDocumentos(opcion, objFact, targetItemFolderId, Anho.ToString(), $"{Mes:00}", dsc_Carpeta);
                        }

                        if (res == "OK") 
                        { 
                            MessageBox.Show("Se registró el documento satisfactoriamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                            btnVerPDF.Enabled = true; btnEscanearDocumento.Enabled = false;
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

        private void rlkpUnidadNegocio_EditValueChanged(object sender, EventArgs e)
        {
            gvDetalleFactura.PostEditor();
        }

        private void btnAnularFactura_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                List<eVentana> listPerfil = blSist.ListarPerfilesUsuario<eVentana>(4, user.cod_usuario);
                eVentana oPerfil = listPerfil.Find(x => x.cod_perfil == 5);
                if (MessageBox.Show("¿Esta seguro de anular el documento?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string result = ""; DateTime fecha;
                    eFacturaProveedor obj = new eFacturaProveedor();
                    fecha = Convert.ToDateTime(dtFechaDocumento.EditValue).AddDays(7);
                    if (oPerfil == null && DateTime.Today > fecha)
                    {
                        MessageBox.Show("No puede anular un documento ya que superó los 7 dias desde su emisión", "Anular documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    obj.tipo_documento = tipo_documento; obj.serie_documento = serie_documento; obj.numero_documento = numero_documento; obj.cod_usuario_anulacion = user.cod_usuario;
                    obj.cod_proveedor = cod_proveedor; obj.cod_empresa = cod_empresa;
                    result = blFact.AnularFacturaProveedor(obj);
                    if (result == "OK") { MessageBox.Show("Se anulo la factura correctamente.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    ObtenerDatos_FacturaProveedor();
                    //frmHandler.BuscarFacturas();
                    ActualizarListado = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        private void btnClonarFactura_ItemClick(object sender, ItemClickEventArgs e)
        {
            //try
            //{
            //    if (txtEstadoDoc.Text == "Anulado")
            //    {
            //        clonado = 1;
            //        IdFactura = 0;
            //        layoutControlItem61.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //        txtVersionDnum.Text = (Convert.ToInt32(txtVersionDnum.Text) + 1).ToString();
            //        txtEstadoDoc.Text = "Recepcionado";
            //        btnGuardarFactura.Enabled = true; btnAnularFactura.Enabled = true;
            //    }
            //    else
            //    {
            //        MessageBox.Show("No se pueden clonar facturas que no estan anuladas.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void gvDetalleFactura_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvDetalleFactura_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void rlkpTipoGastoCosto_EditValueChanged(object sender, EventArgs e)
        {
            gvDetalleFactura.PostEditor();
        }

        private void rlkpClienteEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            gvDetalleFactura.PostEditor();
        }

        private void rlkpProyectoCliente_EditValueChanged(object sender, EventArgs e)
        {
            gvDetalleFactura.PostEditor();
        }

        private void lkpEmpresaProveedor_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpEmpresaProveedor.EditValue != null)
            {
                rlkpTipoGastoCosto.DataSource = blFact.CombosEnGridControl<eTipoGastoCosto>("TipoGastoCosto", cod_empresa: lkpEmpresaProveedor.EditValue.ToString());
                rlkpUnidadNegocio.DataSource = blFact.CombosEnGridControl<eUnidadNegocio>("UnidadNegocio", cod_empresa: lkpEmpresaProveedor.EditValue.ToString());

                listUnidadNegocio = blFact.Obtener_MaestrosGenerales<eUnidadNegocio>(9, lkpEmpresaProveedor.EditValue.ToString());
                listTipoGastoCosto = blFact.Obtener_MaestrosGenerales<eTipoGastoCosto>(10, lkpEmpresaProveedor.EditValue.ToString());

                mylistLineasDetFactura.Clear();
                bsDistribucionFactura.DataSource = null; bsDistribucionFactura.DataSource = mylistLineasDetFactura;

                if (chkFlagInventario.CheckState == CheckState.Unchecked && chkFlagActivoFijo.CheckState == CheckState.Unchecked)
                {
                    gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                }
                //else
                //{
                //    gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                //}
                
                List<eFacturaProveedor.eFacturaProveedor_Distribucion> ListarCeco = new List<eFacturaProveedor.eFacturaProveedor_Distribucion>();
                ListarCeco = blFact.ObtenerListadoCECOS<eFacturaProveedor.eFacturaProveedor_Distribucion>(32, lkpEmpresaProveedor.EditValue.ToString(), cod_cliente);
                bsListadoCECODisponibles.DataSource = null; bsListadoCECODisponibles.DataSource = ListarCeco;

            }
        }


        private void gvDetalleFactura_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "porc_distribucion" && e.Value != "")
                {
                    if (Convert.ToDecimal(e.Value) == 0)
                    {
                        MessageBox.Show("El porcentaje de distribución no puede ser 0%", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvDetalleFactura_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (lkpEmpresaProveedor.EditValue == null) { MessageBox.Show("Debe seleccionar una empresa", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                eFacturaProveedor.eFacturaProveedor_Distribucion objFact = gvDetalleFactura.GetFocusedRow() as eFacturaProveedor.eFacturaProveedor_Distribucion;
                string cod_und_negocio = "", cod_tipo_gasto = "", cod_cliente = "", cod_proyecto = "";
                decimal porc = 0, tot = 0;
                if (objFact != null)
                {
                    if (e.Column.FieldName == "cod_tipo_gasto")
                    {
                        if ((objFact.cod_tipo_gasto == "00001" && (lkpEmpresaProveedor.EditValue.ToString() != "00008" && lkpEmpresaProveedor.EditValue.ToString() != "00009")) || 
                            lkpEmpresaProveedor.EditValue.ToString() == "00005" ||
                            ((objFact.cod_tipo_gasto == "00003" || objFact.cod_tipo_gasto == "00004") && lkpEmpresaProveedor.EditValue.ToString() == "00009"))
                        {
                            foreach (GridColumn col in gvDetalleFactura.Columns)
                            {
                                if (col.FieldName == "cod_und_negocio" || col.FieldName == "cod_cliente") { col.Visible = true; }
                                if (col.FieldName == "cod_proyecto") { col.Visible = false; }
                                //gvDetalleFactura.Columns["Sel"].VisibleIndex = 0;
                                gvDetalleFactura.Columns["cod_tipo_gasto"].VisibleIndex = 0;
                                gvDetalleFactura.Columns["cod_und_negocio"].VisibleIndex = 1;
                                gvDetalleFactura.Columns["cod_cliente"].VisibleIndex = 2;
                                gvDetalleFactura.Columns["cod_CECO"].VisibleIndex = 3;
                                gvDetalleFactura.Columns["porc_distribucion"].VisibleIndex = 4;
                                //gvDetalleFactura.Columns["cod_cta_contable"].VisibleIndex = 5;
                                gvDetalleFactura.Columns["gridColumn1"].VisibleIndex = 6;
                            }
                            if (lkpEmpresaProveedor.EditValue.ToString() == "00008" || lkpEmpresaProveedor.EditValue.ToString() == "00009")
                            {
                                gvDetalleFactura.Columns["cod_cliente"].Visible = false;
                                cod_cliente = listTipoGastoCosto.Find(x => x.cod_tipo_gasto == objFact.cod_tipo_gasto).dsc_ceco_ALTERNATVO != "" ? listTipoGastoCosto.Find(x => x.cod_tipo_gasto == objFact.cod_tipo_gasto).dsc_ceco_ALTERNATVO : cod_cliente;
                                //cod_cliente = lkpEmpresaProveedor.EditValue.ToString() == "00009" ? objFact.cod_tipo_gasto == "00002" ? "001" : objFact.cod_tipo_gasto == "00003" ? "002" : objFact.cod_tipo_gasto == "00004" ? "003" :
                                //            objFact.cod_tipo_gasto == "00009" ? "001" : cod_cliente : cod_cliente;
                                if (objFact.cod_und_negocio != null && objFact.cod_und_negocio != "") cod_und_negocio = listUnidadNegocio.Find(x => x.cod_und_negocio == objFact.cod_und_negocio).dsc_pref_ceco;
                                if (objFact.cod_tipo_gasto != null && objFact.cod_tipo_gasto != "") cod_tipo_gasto = listTipoGastoCosto.Find(x => x.cod_tipo_gasto == objFact.cod_tipo_gasto).dsc_pref_ceco;
                                objFact.cod_CECO = cod_und_negocio + cod_tipo_gasto + cod_cliente;
                            }
                        }
                        else
                        {
                            foreach (GridColumn col in gvDetalleFactura.Columns)
                            {
                                if (col.FieldName == "cod_und_negocio" || col.FieldName == "cod_cliente" || col.FieldName == "cod_proyecto"/* || col.FieldName == "cod_cta_contable"*/) { col.Visible = false; }
                                //gvDetalleFactura.Columns["Sel"].VisibleIndex = 0;
                                gvDetalleFactura.Columns["cod_tipo_gasto"].VisibleIndex = 0;
                                gvDetalleFactura.Columns["cod_CECO"].VisibleIndex = 1;
                                gvDetalleFactura.Columns["porc_distribucion"].VisibleIndex = 2;
                                //gvDetalleFactura.Columns["cod_cta_contable"].VisibleIndex = 3;
                                gvDetalleFactura.Columns["gridColumn1"].VisibleIndex = 4;
                            }
                            if (lkpEmpresaProveedor.EditValue.ToString() == "00008" || lkpEmpresaProveedor.EditValue.ToString() == "00009") gvDetalleFactura.Columns["cod_cliente"].Visible = false;
                            objFact.cod_und_negocio = null; objFact.cod_cliente = null;
                            eUnidadNegocio eUndNeg = new eUnidadNegocio();
                            eUndNeg = blFact.ObtenerDatosEmpresa<eUnidadNegocio>(34, lkpEmpresaProveedor.EditValue.ToString(), cod_tipo_gasto: objFact.cod_tipo_gasto);
                            eUndNeg = blFact.ObtenerDatosEmpresa<eUnidadNegocio>(25, lkpEmpresaProveedor.EditValue.ToString(), cod_und_negocio: eUndNeg != null ? eUndNeg.cod_und_negocio : "");
                            if (eUndNeg != null) 
                            { 
                                objFact.cod_und_negocio = lkpEmpresaProveedor.EditValue.ToString() == "00009" && (objFact.cod_tipo_gasto == "00008" || objFact.cod_tipo_gasto == "00009") ? "00002" : eUndNeg.cod_und_negocio; 
                                objFact.cod_cliente = "CLI0000000"; 
                            }

                            cod_cliente = "000";
                            //cod_cliente = lkpEmpresaProveedor.EditValue.ToString() == "00004" ? objFact.cod_tipo_gasto == "00007" ? "994" : objFact.cod_tipo_gasto == "00010" ? "995" : objFact.cod_tipo_gasto == "00011" ? "996" :
                            //            objFact.cod_tipo_gasto == "00012" ? "997" : objFact.cod_tipo_gasto == "00003" ? "998" : cod_cliente : cod_cliente;
                            cod_cliente = listTipoGastoCosto.Find(x => x.cod_tipo_gasto == objFact.cod_tipo_gasto).dsc_ceco_ALTERNATVO != "" ? listTipoGastoCosto.Find(x => x.cod_tipo_gasto == objFact.cod_tipo_gasto).dsc_ceco_ALTERNATVO : cod_cliente;
                            //cod_cliente = lkpEmpresaProveedor.EditValue.ToString() == "00009" ? objFact.cod_tipo_gasto == "00002" ? "001" : objFact.cod_tipo_gasto == "00003" ? "002" : objFact.cod_tipo_gasto == "00004" ? "003" :
                            //            objFact.cod_tipo_gasto == "00009" ? "001" : cod_cliente : cod_cliente;
                            if (objFact.cod_und_negocio != null && objFact.cod_und_negocio != "") cod_und_negocio = listUnidadNegocio.Find(x => x.cod_und_negocio == objFact.cod_und_negocio).dsc_pref_ceco;
                            if (objFact.cod_tipo_gasto != null && objFact.cod_tipo_gasto != "") cod_tipo_gasto = listTipoGastoCosto.Find(x => x.cod_tipo_gasto == objFact.cod_tipo_gasto).dsc_pref_ceco;
                            objFact.cod_CECO = cod_und_negocio + cod_tipo_gasto + cod_cliente;

                            //rlkpUnidadNegocio.DataSource = blFact.CombosEnGridControl<eUnidadNegocio>("UnidadNegocio", cod_empresa: lkpEmpresaProveedor.EditValue.ToString());
                        }
                    }
                    else if (e.Column.FieldName == "cod_und_negocio" || e.Column.FieldName == "cod_tipo_gasto" || e.Column.FieldName == "cod_cliente" || e.Column.FieldName == "cod_proyecto"/* || e.Column.FieldName == "cod_cta_contable"*/)
                    {
                        if (objFact.cod_tipo_gasto == "00001" || lkpEmpresaProveedor.EditValue.ToString() == "00005")
                        {
                            if (e.Column.FieldName == "cod_und_negocio")
                            {
                                rlkpClienteEmpresa.DataSource = blFact.CombosEnGridControl<eCliente_Empresas>("ClienteEmpresa", cod_empresa: lkpEmpresaProveedor.EditValue.ToString(), cod_und_negocio: objFact.cod_und_negocio);
                                listClienteEmpresa = blFact.Obtener_MaestrosGenerales<eCliente_Empresas>(11, lkpEmpresaProveedor.EditValue.ToString(), cod_und_negocio: objFact.cod_und_negocio);
                            }
                            if (e.Column.FieldName == "cod_cliente")
                            {
                                objFact.cod_proyecto = null;
                                listClienteProyecto = blFact.Obtener_MaestrosGenerales<eCliente_Empresas>(48, lkpEmpresaProveedor.EditValue.ToString(), cod_und_negocio: objFact.cod_und_negocio, cod_cliente: objFact.cod_cliente);
                                if (listClienteProyecto.Count == 1) objFact.cod_proyecto = listClienteProyecto[0].cod_proyecto;
                                rlkpProyectoCliente.DataSource = listClienteProyecto;

                                foreach (GridColumn col in gvDetalleFactura.Columns)
                                {
                                    if (col.FieldName == "cod_und_negocio" || col.FieldName == "cod_cliente" || col.FieldName == "cod_proyecto") { col.Visible = true; }
                                    //if (listClienteProyecto.Count > 0 && col.FieldName == "cod_proyecto") { col.Visible = true; }
                                    //gvDetalleFactura.Columns["Sel"].VisibleIndex = 0;
                                    gvDetalleFactura.Columns["cod_tipo_gasto"].VisibleIndex = 0;
                                    gvDetalleFactura.Columns["cod_und_negocio"].VisibleIndex = 1;
                                    gvDetalleFactura.Columns["cod_cliente"].VisibleIndex = 2;
                                    gvDetalleFactura.Columns["cod_proyecto"].VisibleIndex = 3;
                                    gvDetalleFactura.Columns["cod_CECO"].VisibleIndex = 4;
                                    gvDetalleFactura.Columns["porc_distribucion"].VisibleIndex = 5;
                                    //gvDetalleFactura.Columns["cod_cta_contable"].VisibleIndex = 6;
                                    gvDetalleFactura.Columns["gridColumn1"].VisibleIndex = 7;
                                }
                                //if (listClienteProyecto.Count == 0) { gvDetalleFactura.Columns["cod_proyecto"].Visible = false; }
                                if (listClienteProyecto.Count == 0 || objFact.cod_cliente == "CLI0000000" || objFact.cod_cliente == "CLI9999999") { gvDetalleFactura.Columns["cod_proyecto"].Visible = false; }

                                //if (listClienteProyecto.Count > 0)
                                //{
                                //    if (objFact.cod_cliente != "CLI0000000" && objFact.cod_cliente != "CLI9999999")
                                //    {
                                //        //if (listClienteProyecto.Count > 0)
                                //        //{
                                //            foreach (GridColumn col in gvDetalleFactura.Columns)
                                //            {
                                //                if (col.FieldName == "cod_und_negocio" || col.FieldName == "cod_cliente" || col.FieldName == "cod_proyecto"/* || col.FieldName == "cod_cta_contable"*/) { col.Visible = true; }
                                //                //gvDetalleFactura.Columns["Sel"].VisibleIndex = 0;
                                //                gvDetalleFactura.Columns["cod_tipo_gasto"].VisibleIndex = 0;
                                //                gvDetalleFactura.Columns["cod_und_negocio"].VisibleIndex = 1;
                                //                gvDetalleFactura.Columns["cod_cliente"].VisibleIndex = 2;
                                //                gvDetalleFactura.Columns["cod_proyecto"].VisibleIndex = 3;
                                //                gvDetalleFactura.Columns["cod_CECO"].VisibleIndex = 4;
                                //                gvDetalleFactura.Columns["porc_distribucion"].VisibleIndex = 5;
                                //                //gvDetalleFactura.Columns["cod_cta_contable"].VisibleIndex = 6;
                                //                gvDetalleFactura.Columns["gridColumn1"].VisibleIndex = 7;
                                //            }
                                //        //}
                                //    }
                                //}
                                //else
                                //{
                                //    //objFact.cod_proyecto = null;
                                //    foreach (GridColumn col in gvDetalleFactura.Columns)
                                //    {
                                //        if (col.FieldName == "cod_und_negocio" || col.FieldName == "cod_cliente") { col.Visible = true; }
                                //        if (col.FieldName == "cod_proyecto") { col.Visible = false; }
                                //        //gvDetalleFactura.Columns["Sel"].VisibleIndex = 0;
                                //        gvDetalleFactura.Columns["cod_tipo_gasto"].VisibleIndex = 0;
                                //        gvDetalleFactura.Columns["cod_und_negocio"].VisibleIndex = 1;
                                //        gvDetalleFactura.Columns["cod_cliente"].VisibleIndex = 2;
                                //        gvDetalleFactura.Columns["cod_CECO"].VisibleIndex = 3;
                                //        gvDetalleFactura.Columns["porc_distribucion"].VisibleIndex = 4;
                                //        //gvDetalleFactura.Columns["cod_cta_contable"].VisibleIndex = 5;
                                //        gvDetalleFactura.Columns["gridColumn1"].VisibleIndex = 6;
                                //    }
                                //}
                            }

                            if (objFact.cod_und_negocio != null) cod_und_negocio = listUnidadNegocio.Find(x => x.cod_und_negocio == objFact.cod_und_negocio).dsc_pref_ceco;
                            if (objFact.cod_tipo_gasto != null) cod_tipo_gasto = listTipoGastoCosto.Find(x => x.cod_tipo_gasto == objFact.cod_tipo_gasto).dsc_pref_ceco;
                            if (objFact.cod_cliente != null) cod_cliente = listClienteEmpresa.Find(x => x.cod_cliente == objFact.cod_cliente).dsc_pref_ceco;
                            if (objFact.cod_proyecto != null) cod_proyecto = listClienteProyecto.Find(x => x.cod_proyecto == objFact.cod_proyecto).dsc_pref_ceco;
                            objFact.cod_CECO = cod_und_negocio + cod_tipo_gasto + cod_cliente + cod_proyecto;
                        }
                        else
                        {
                            objFact.cod_cliente = "CLI0000000"; cod_cliente = "000";
                            cod_cliente = listTipoGastoCosto.Find(x => x.cod_tipo_gasto == objFact.cod_tipo_gasto).dsc_ceco_ALTERNATVO != "" ? listTipoGastoCosto.Find(x => x.cod_tipo_gasto == objFact.cod_tipo_gasto).dsc_ceco_ALTERNATVO : cod_cliente;
                            //cod_cliente = lkpEmpresaProveedor.EditValue.ToString() == "00009" ? objFact.cod_tipo_gasto == "00002" ? "001" : objFact.cod_tipo_gasto == "00003" ? "002" : objFact.cod_tipo_gasto == "00004" ? "003" :
                            //            objFact.cod_tipo_gasto == "00009" ? "001" : cod_cliente : cod_cliente;
                            if (objFact.cod_und_negocio != null && objFact.cod_und_negocio != "") cod_und_negocio = listUnidadNegocio.Find(x => x.cod_und_negocio == objFact.cod_und_negocio).dsc_pref_ceco;
                            if (objFact.cod_tipo_gasto != null && objFact.cod_tipo_gasto != "") cod_tipo_gasto = listTipoGastoCosto.Find(x => x.cod_tipo_gasto == objFact.cod_tipo_gasto).dsc_pref_ceco;
                            objFact.cod_CECO = cod_und_negocio + cod_tipo_gasto + cod_cliente;
                        }
                    }
                    else if (e.Column.FieldName == "porc_distribucion")
                    {
                        for (int x = 0; x < gvDetalleFactura.DataRowCount; x++)
                        {
                            eFacturaProveedor.eFacturaProveedor_Distribucion obj = gvDetalleFactura.GetRow(x) as eFacturaProveedor.eFacturaProveedor_Distribucion;
                            if (obj == null) continue;
                            tot = tot + obj.porc_distribucion;
                            if (x == e.RowHandle) continue;
                            porc = porc + obj.porc_distribucion;
                        }
                        if (porc + (decimal)e.Value > 1)
                        {
                            MessageBox.Show("La suma de los porcentajes de distribución no puede exceder el 100%", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            objFact.porc_distribucion = 1 - porc;
                            gvDetalleFactura.RefreshData();
                        }
                        if (Convert.ToDecimal(e.Value) == 0)
                        {
                            MessageBox.Show("El porcentaje de distribución no puede ser 0%", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            objFact.porc_distribucion = 1 - porc;
                            gvDetalleFactura.RefreshData();
                        }
                        if (tot == 1) gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                        if (tot < 1) gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                    }
                    if (e.Column.FieldName != "porc_distribucion")
                    {
                        eFacturaProveedor.eFacturaProveedor_Distribucion eobjFact = gvDetalleFactura.GetFocusedRow() as eFacturaProveedor.eFacturaProveedor_Distribucion;
                        porc = 0;
                        for (int x = 0; x < gvDetalleFactura.RowCount; x++)
                        {
                            eFacturaProveedor.eFacturaProveedor_Distribucion obj = gvDetalleFactura.GetRow(x) as eFacturaProveedor.eFacturaProveedor_Distribucion;
                            if (obj == null) continue;
                            porc = porc + obj.porc_distribucion;
                        }

                        if (porc == 1 || (eobjFact.porc_distribucion + porc) >= 1) gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                        if (porc < 1) gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvDetalleFactura_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                eFacturaProveedor.eFacturaProveedor_Distribucion objFact = gvDetalleFactura.GetFocusedRow() as eFacturaProveedor.eFacturaProveedor_Distribucion;
                decimal porc = 0;
                for (int x = 0; x < gvDetalleFactura.DataRowCount; x++)
                {
                    eFacturaProveedor.eFacturaProveedor_Distribucion obj = gvDetalleFactura.GetRow(x) as eFacturaProveedor.eFacturaProveedor_Distribucion;
                    porc = porc + obj.porc_distribucion;
                }
                if (objFact != null) objFact.porc_distribucion = 1 - porc;

                if (porc == 1) gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                if (porc < 1) gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        bool editarMontos = true, editarOtrosCargos = false;
        private void chkAplicaIGV_CheckStateChanged(object sender, EventArgs e)
        {
            editarMontos = false;
            txtMontoIGV.EditValue = chkAplicaIGV.CheckState == CheckState.Checked ? 
                            Convert.ToDecimal(txtMontoSubTotal.EditValue) > 0 ?
                            (Convert.ToDecimal(txtMontoSubTotal.EditValue) * (decimal)0.18).ToString() : (Convert.ToDecimal(txtMontoTotal.EditValue) - (Convert.ToDecimal(txtMontoTotal.EditValue) / (decimal)1.18)).ToString()
                            : "0.00";
            txtMontoTotal.EditValue = (Convert.ToDecimal(txtMontoSubTotal.EditValue) + Convert.ToDecimal(txtMontoIGV.EditValue) + Convert.ToDecimal(txtMontoOtrosCargos.EditValue)).ToString();
            txtMontoRetencion.EditValue = (Convert.ToDecimal(txtMontoTotal.EditValue) * Convert.ToDecimal(txtTasaRetencion.EditValue)).ToString();
            //txtMontoSaldo.EditValue = txtMontoTotal.EditValue;
            txtMontoSaldo.EditValue = (Convert.ToDecimal(txtMontoTotal.EditValue) + Convert.ToDecimal(txtMontoPercepcion.EditValue)).ToString();

            txtMontoSubTotal.EditValue = glkpTipoDocumento.EditValue.ToString() == "TC006" ? (-1) * Math.Abs(Convert.ToDecimal(txtMontoSubTotal.EditValue)) : txtMontoSubTotal.EditValue;
            txtMontoIGV.EditValue = glkpTipoDocumento.EditValue.ToString() == "TC006" ? (-1) * Math.Abs(Convert.ToDecimal(txtMontoIGV.EditValue)) : txtMontoIGV.EditValue;
            txtMontoTotal.EditValue = glkpTipoDocumento.EditValue.ToString() == "TC006" ? (-1) * Math.Abs(Convert.ToDecimal(txtMontoTotal.EditValue)) : txtMontoTotal.EditValue;
            txtMontoSaldo.EditValue = glkpTipoDocumento.EditValue.ToString() == "TC006" ? (-1) * Math.Abs(Convert.ToDecimal(txtMontoSaldo.EditValue)) : txtMontoSaldo.EditValue;
            editarMontos = true;
        }

        private void txtMontoSubTotal_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!editarMontos) return;
                txtMontoIGV.EditValue = chkAplicaIGV.CheckState == CheckState.Checked ? (Convert.ToDecimal(txtMontoSubTotal.EditValue) * (decimal)0.18).ToString() : "0.00";
                txtMontoTotal.EditValue = (Convert.ToDecimal(txtMontoSubTotal.EditValue) + Convert.ToDecimal(txtMontoIGV.EditValue) + Convert.ToDecimal(txtMontoOtrosCargos.EditValue)).ToString();
                txtMontoRetencion.EditValue = (Convert.ToDecimal(txtMontoTotal.EditValue) * Convert.ToDecimal(txtTasaRetencion.EditValue)).ToString();
                //txtMontoSaldo.EditValue = txtMontoTotal.EditValue;
                txtMontoSaldo.EditValue = (Convert.ToDecimal(txtMontoTotal.EditValue) + Convert.ToDecimal(txtMontoPercepcion.EditValue)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtMontoOtrosCargos_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!editarMontos) return; editarOtrosCargos = true;
                txtMontoTotal.EditValue = (Convert.ToDecimal(txtMontoSubTotal.EditValue) + Convert.ToDecimal(txtMontoIGV.EditValue) + Convert.ToDecimal(txtMontoOtrosCargos.EditValue)).ToString();
                txtMontoSaldo.EditValue = (Convert.ToDecimal(txtMontoTotal.EditValue) + Convert.ToDecimal(txtMontoPercepcion.EditValue)).ToString();
                editarOtrosCargos = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtMontoPercepcion_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!editarMontos) return;
                txtMontoSaldo.EditValue = (Convert.ToDecimal(txtMontoTotal.EditValue) + Convert.ToDecimal(txtMontoPercepcion.EditValue)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtMontoTotal_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!editarMontos) return;
                if (editarOtrosCargos) return;
                txtMontoIGV.EditValue = chkAplicaIGV.CheckState == CheckState.Checked ? ((Convert.ToDecimal(txtMontoTotal.EditValue) - Convert.ToDecimal(txtMontoOtrosCargos.EditValue)) - ((Convert.ToDecimal(txtMontoTotal.EditValue) - Convert.ToDecimal(txtMontoOtrosCargos.EditValue)) / (decimal)1.18)).ToString() : "0.00";
                txtMontoSubTotal.EditValue = (Convert.ToDecimal(txtMontoTotal.EditValue) - Convert.ToDecimal(txtMontoIGV.EditValue) - Convert.ToDecimal(txtMontoOtrosCargos.EditValue)).ToString();
                txtMontoRetencion.EditValue = (Convert.ToDecimal(txtMontoTotal.EditValue) * Convert.ToDecimal(txtTasaRetencion.EditValue)).ToString();
                //txtMontoSaldo.EditValue = txtMontoTotal.EditValue;
                txtMontoSaldo.EditValue = (Convert.ToDecimal(txtMontoTotal.EditValue) + Convert.ToDecimal(txtMontoPercepcion.EditValue)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvObservacionesFactura_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvObservacionesFactura_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvObservacionesFactura_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            eFacturaProveedor.eFacturaProveedor_Observaciones obj = gvObservacionesFactura.GetFocusedRow() as eFacturaProveedor.eFacturaProveedor_Observaciones;
            obj.fch_registro = DateTime.Today; obj.dsc_usuario_registro = user.dsc_usuario;
            gvObservacionesFactura.RefreshData();
        }

        private void lkpEstadoRegistro_EditValueChanged(object sender, EventArgs e)
        {
            //switch (lkpEstadoRegistro.EditValue.ToString())
            //{
            //    case "CON":
            //        MessageBox.Show("El cambio a contabilizado se debe realizar desde el listado de Facturas.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        lkpEstadoRegistro.EditValue = "APR";
            //        return;
            //        break;
            //    default:
            //        lkpEstadoPago.EditValue = "";
            //        dtFechaPagoEjecutado.EditValue = null;
            //        break;
            //}
            string estado = "";
            estado = lkpEstadoRegistro.Text;
            lkpEstadoRegistro.ToolTip = estado;
        }

        private void lkpEstadoPago_EditValueChanged(object sender, EventArgs e)
        {
            //switch (lkpEstadoRegistro.EditValue.ToString())
            //{
            //    case "APR":
            //        lkpEstadoRegistro.EditValue = "CON";
            //        break;
            //    case "PAG":
            //        lkpEstadoRegistro.EditValue = "CON";
            //        dtFechaPagoEjecutado.EditValue = DateTime.Today;
            //        txtMontoSaldo.EditValue = 0;
            //        break;
            //}
            string estado = "";
            estado = lkpEstadoPago.Text;
            lkpEstadoPago.ToolTip = estado;
        }

        private void dtFechaPagoEjecutado_EditValueChanged(object sender, EventArgs e)
        {
            if (dtFechaPagoEjecutado.EditValue != null) /*lkpEstadoRegistro.EditValue = "CON";*/ lkpEstadoPago.EditValue = "PAG";
         
        }

        private void txtMontoDetraccion_EditValueChanged(object sender, EventArgs e)
        {
            if (chkDetraccion.CheckState == CheckState.Checked && Convert.ToDecimal(txtMontoDetraccion.EditValue) > Convert.ToDecimal(txtMontoIGV.EditValue))
            {
                MessageBox.Show("El monto de la detracción no puede ser mayor al importe del IGV.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //else
            //{
            //    txtMontoSaldo.EditValue = (Convert.ToDecimal(txtMontoSaldo.EditValue) - Convert.ToDecimal(txtMontoDetraccion.EditValue));
            //}
        }

        private void txtMontoRetencion_EditValueChanged(object sender, EventArgs e)
        {
            //if (Convert.ToDecimal(txtMontoRetencion.EditValue) > Convert.ToDecimal(txtMontoIGV.EditValue))
            //{
            //    MessageBox.Show("El monto de la retención no puede ser mayor al importe del IGV.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void bgvProgramacionPagos_CustomDrawBandHeader(object sender, DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnasBandHeader(e);
        }

        private void bgvProgramacionPagos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void bgvProgramacionPagos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                blGlobal.Pintar_EstiloGrilla(sender, e);
                GridView view = sender as GridView;
                if (view.Columns["imp_saldo"] != null || view.Columns["dsc_estado_documento"] != null)
                {
                    decimal saldo = Convert.ToDecimal(view.GetRowCellDisplayText(e.RowHandle, view.Columns["imp_saldo"]));
                    if (saldo == 0) e.Appearance.ForeColor = Color.Blue;
                    string estado = view.GetRowCellDisplayText(e.RowHandle, view.Columns["dsc_estado_documento"]);
                    if (estado == "Anulado") e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void lkpTipoTransaccion_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpTipoTransaccion.EditValue == null) { lkpConceptoDetraccion.EditValue = null; lkpConceptoDetraccion.Properties.DataSource = null; return; }
            blFact.CargaCombosLookUp("ConceptoDetraccion", lkpConceptoDetraccion, "cod_concepto_detraccion", "dsc_concepto_detraccion", "", valorDefecto: true, cod_tipo_transaccion: lkpTipoTransaccion.EditValue.ToString());
            lkpConceptoDetraccion.EditValue = null;
        }

        private void lkpConceptoDetraccion_EditValueChanged(object sender, EventArgs e)
        {
            eFacturaProveedor obj = new eFacturaProveedor();
            if (lkpConceptoDetraccion.EditValue == null) { txtTasaDetraccion.EditValue = 0; txtMontoDetraccion.EditValue = 0; return; }
            obj = blFact.Obtener_TasaDetraccion<eFacturaProveedor>(22, lkpTipoTransaccion.EditValue.ToString(), lkpConceptoDetraccion.EditValue.ToString());
            if (obj == null) return;
            txtTasaDetraccion.EditValue = obj.prc_tasa_detraccion;
            txtMontoDetraccion.EditValue = Convert.ToDecimal(txtMontoTotal.EditValue) * obj.prc_tasa_detraccion;
            var decim = (int)((Convert.ToDecimal(txtMontoDetraccion.EditValue) % 1) * 100);
            //if (decim > 0) { txtMontoPagadoDetraccion.EditValue = Convert.ToInt32(Math.Truncate(Convert.ToDecimal(txtMontoDetraccion.EditValue)) + 1); } else { txtMontoPagadoDetraccion.EditValue = txtMontoDetraccion.EditValue; }
            if (decim >= 50) { txtMontoPagadoDetraccion.EditValue = Convert.ToInt32(Math.Truncate(Convert.ToDecimal(txtMontoDetraccion.EditValue)) + 1); } else { txtMontoPagadoDetraccion.EditValue = txtMontoDetraccion.EditValue; }
        }

        private void txtTasaDetraccion_EditValueChanged(object sender, EventArgs e)
        {
            decimal prc_det = 0;
            prc_det = Convert.ToDecimal(txtTasaDetraccion.EditValue);
            txtMontoDetraccion.EditValue = Convert.ToDecimal(txtMontoTotal.EditValue) * prc_det;
            var decim = (int)((Convert.ToDecimal(txtMontoDetraccion.EditValue) % 1) * 100);
            //if (decim > 0) { txtMontoPagadoDetraccion.EditValue = Convert.ToInt32(Math.Truncate(Convert.ToDecimal(txtMontoDetraccion.EditValue)) + 1); } else { txtMontoPagadoDetraccion.EditValue = txtMontoDetraccion.EditValue; }
            if (decim >= 50) { txtMontoPagadoDetraccion.EditValue = Convert.ToInt32(Math.Truncate(Convert.ToDecimal(txtMontoDetraccion.EditValue)) + 1); } else { txtMontoPagadoDetraccion.EditValue = txtMontoDetraccion.EditValue; }
        }


        private void btnAgregarServicio_Click(object sender, EventArgs e)
        {
            if (txtProveedor.Text == "") { MessageBox.Show("Debe seleccionar un proveedor", "Vincular nuevo servicio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            Busqueda("", "Servicio");
        }

        private void chkFlagInventario_EditValueChanged(object sender, EventArgs e)
        {
            if (chkFlagInventario.CheckState == CheckState.Checked)
            {
                chkFlagActivoFijo.CheckState = CheckState.Unchecked;
                //gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            }
            if (chkFlagInventario.CheckState == CheckState.Unchecked && chkFlagActivoFijo.CheckState == CheckState.Unchecked)
            {
                gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            }
        }

        private void chkFlagActivoFijo_EditValueChanged(object sender, EventArgs e)
        {
            if (chkFlagActivoFijo.CheckState == CheckState.Checked)
            {
                chkFlagInventario.CheckState = CheckState.Unchecked;
                //gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.None;ñ
            }
            if (chkFlagInventario.CheckState == CheckState.Unchecked && chkFlagActivoFijo.CheckState == CheckState.Unchecked)
            {
                gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            }
        }

        private void bgvProgramacionPagos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetRow(e.RowHandle) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                    if (e.Column.FieldName == "fch_pago" && obj.fch_pago.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_ejecucion" && obj.fch_ejecucion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnEliminarDocumento_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Esta seguro de eliminar el documento?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    eFacturaProveedor eFact = new eFacturaProveedor();
                    eFact = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(2, tipo_documento, serie_documento, numero_documento, cod_proveedor);
                    //////////////////////////// ELIMINAR DOCUMENTO DE ONEDRIVE ////////////////////////////
                    if (eFact.idPDF != null && eFact.idPDF != "") await Mover_Eliminar_ArchivoOneDrive(eFact, new DateTime(), true, false, "ELIMINAR");
                    if (eFact.idXML != null && eFact.idXML != "") await Mover_Eliminar_ArchivoOneDrive(eFact, new DateTime(), false, true, "ELIMINAR");

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

        private void btnSolicitarRevision_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                txtGlosaFactura.Select();
                eFacturaProveedor obj = new eFacturaProveedor();
                obj.tipo_documento = glkpTipoDocumento.EditValue.ToString(); obj.serie_documento = txtSerieDocumento.Text;
                obj.numero_documento = txtNumeroDocumento.Text == "" ? 0 : Convert.ToDecimal(txtNumeroDocumento.Text);
                obj.cod_proveedor = txtProveedor.Tag.ToString(); obj.cod_empresa = lkpEmpresaProveedor.EditValue.ToString();
                obj.cod_estado_registro = "RVS"; obj.cod_usuario_registro = user.cod_usuario;
                obj.periodo_tributario = ""; obj.cod_usuario_aprobado_reg = user.cod_usuario;
                string result = blFact.Actualiar_EstadoRegistroFactura(obj);
                if (result != "OK") { MessageBox.Show("Error al solicitar revisión", "Solicitar revisión", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                lkpEstadoRegistro.EditValue = "RVS";
                btnAprobarDocumento.Visibility = BarItemVisibility.Never; btnAgregarProyecto.Visibility = BarItemVisibility.Never;
                btnSolicitarRevision.Visibility = BarItemVisibility.Never; btnRevisado.Visibility = BarItemVisibility.Always;
                ActualizarListado = true;
                XtraMessageBox.Show("Se solicitó la revisión del documento", "Solicitar revisión", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnRevisado_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (txtProveedor.Text.Trim() == "") { MessageBox.Show("Debe seleccionar proveedor.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtProveedor.Focus(); return; }
                if (glkpTipoDocumento.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); glkpTipoDocumento.Focus(); return; }
                if (txtSerieDocumento.Text.Trim() == "") { MessageBox.Show("Debe ingresar una serie de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtSerieDocumento.Focus(); return; }
                if (txtNumeroDocumento.Text.Trim() == "") { MessageBox.Show("Debe ingresar un numero de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNumeroDocumento.Focus(); return; }

                if (MessageBox.Show("¿Esta seguro de REVISAR los datos de la factura?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtGlosaFactura.Select();
                    blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Revisando documento...", "Cargando...");
                    eFacturaProveedor eFact = new eFacturaProveedor(); eFacturaProveedor eFact2 = new eFacturaProveedor();
                    eFact = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(2, tipo_documento, serie_documento, numero_documento, cod_proveedor);
                    eFact2.tipo_documento = glkpTipoDocumento.EditValue.ToString(); eFact2.serie_documento = txtSerieDocumento.Text.Trim();
                    eFact2.numero_documento = txtNumeroDocumento.Text == "" ? 0 : Convert.ToDecimal(txtNumeroDocumento.EditValue);
                    eFact2.cod_proveedor = txtProveedor.Tag.ToString(); eFact2.dsc_ruc = txtRucProveedor.Text;
                    string result2 = blFact.Reemplazar_CabeceraFactura(eFact, eFact2);
                    if (result2 == "OK")
                    {
                        eFact = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(2, eFact2.tipo_documento, eFact2.serie_documento, eFact2.numero_documento, eFact2.cod_proveedor);
                        tipo_documento = eFact.tipo_documento; serie_documento = eFact.serie_documento;
                        numero_documento = eFact.numero_documento; cod_proveedor = eFact.cod_proveedor; RUC = eFact.dsc_ruc;
                        await Renombrar_ArchivoOneDrive(eFact);

                        eFacturaProveedor obj = new eFacturaProveedor();
                        eFact.cod_estado_registro = "REV"; eFact.cod_usuario_registro = user.cod_usuario;
                        eFact.periodo_tributario = ""; eFact.cod_usuario_aprobado_reg = user.cod_usuario;
                        string result = blFact.Actualiar_EstadoRegistroFactura(eFact);
                        if (result != "OK") { MessageBox.Show("Error al revisar documento", "Revisar documento", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                        lkpEstadoRegistro.EditValue = "REV";
                        btnAprobarDocumento.Visibility = BarItemVisibility.Never; btnAgregarProyecto.Visibility = BarItemVisibility.Never;
                        btnSolicitarRevision.Visibility = BarItemVisibility.Never; btnRevisado.Visibility = BarItemVisibility.Never;
                        ActualizarListado = true;
                        SplashScreenManager.CloseForm();
                        XtraMessageBox.Show("Se revisó del documento", "Revisar documento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        SplashScreenManager.CloseForm();
                    }
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnSobreescribir_ItemClick(object sender, ItemClickEventArgs e)
        {
            //eFacturaProveedor eFact = new eFacturaProveedor(); eFacturaProveedor eFact2 = new eFacturaProveedor();
            //eFact = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(2, tipo_documento, serie_documento, numero_documento, cod_proveedor);
            //eFact2.tipo_documento = glkpTipoDocumento.EditValue.ToString(); eFact2.serie_documento = txtSerieDocumento.Text.Trim();
            //eFact2.numero_documento = txtNumeroDocumento.Text == "" ? 0 : Convert.ToDecimal(txtNumeroDocumento.EditValue);
            //eFact2.cod_proveedor = txtProveedor.Tag.ToString();
            //string result = blFact.Reemplazar_CabeceraFactura(eFact, eFact2);
            //if (result == "OK") await Renombrar_ArchivoOneDrive(eFact);
        }

        private async Task Renombrar_ArchivoOneDrive(eFacturaProveedor obj)
        {
            try
            {
                List<eFacturaProveedor> list = blFact.CombosEnGridControl<eFacturaProveedor>("TipoDocumento");
                TD_sunat = list.Find(x => x.tipo_documento == obj.tipo_documento).cod_sunat;
                varNombreArchivoSinExtension = RUC + "-" + TD_sunat + "-" + serie_documento + "-" + String.Format("{0:" + fmt_nro_doc + "}", numero_documento);

                string dsc_Carpeta = glkpTipoDocumento.EditValue.ToString() == "TC008" ? "RxH Proveedor" : "Facturas Proveedor";
                dsc_Carpeta = CajaChica == "SI" ? "Caja Chica" : EntregaRendir == "SI" ? "Entrega Rendir" : dsc_Carpeta;
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

                for (int x = 0; x < 2; x++)
                {
                    string extension = x == 0 ? ".pdf" : ".xml";
                    if (x == 0 && obj.flg_PDF == "NO") continue;
                    if (x == 1 && obj.flg_XML == "NO") continue;
                    string idArchivo = x == 0 ? obj.idPDF : obj.idXML;
                    //////////////////////////////////////////////////////// RENOMBRAR DOCUMENTO DE ONEDRIVE ////////////////////////////////////////////////////////
                    GraphClient = new Microsoft.Graph.GraphServiceClient(
                        new Microsoft.Graph.DelegateAuthenticationProvider((requestMessage) =>
                        {
                            requestMessage
                                .Headers
                                .Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);
                            return Task.FromResult(0);
                        }));

                    var driveItem = new Microsoft.Graph.DriveItem
                    {
                        Name = varNombreArchivoSinExtension + extension
                    };

                    await GraphClient.Me.Drive.Items[idArchivo]
                        .Request()
                        .UpdateAsync(driveItem);
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                int Anho = Convert.ToDateTime(dtFechaRegistro.EditValue).Year; int Mes = Convert.ToDateTime(dtFechaRegistro.EditValue).Month;
                eFacturaProveedor objFact2 = new eFacturaProveedor();
                objFact2.tipo_documento = obj.tipo_documento; objFact2.serie_documento = obj.serie_documento;
                objFact2.numero_documento = obj.numero_documento; objFact2.cod_proveedor = obj.cod_proveedor;
                objFact2.cod_empresa = obj.cod_empresa; objFact2.NombreArchivo = varNombreArchivoSinExtension;
                Anho = obj.periodo_tributario != "" ? Convert.ToInt32(obj.periodo_tributario.Substring(3, 4)) : Anho;
                Mes = obj.periodo_tributario != "" ? Convert.ToInt32(obj.periodo_tributario.Substring(0, 2)) : Mes;
                string res = blFact.ActualizarInformacionDocumentos(4, objFact2, "", Anho.ToString(), $"{Mes:00}", dsc_Carpeta);
                if (res != "OK") MessageBox.Show("Hubieron problemas al renombrar el documento", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void btnRegistrarDetalle_Click(object sender, EventArgs e)
        {
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

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

        private void btnDetalleFactura_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["FrmCrearDetalleFactura"] != null)
            {
                Application.OpenForms["FrmCrearDetalleFactura"].Activate();
            }
            else
            {

                if (glkpTipoDocumento.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); glkpTipoDocumento.Focus(); return; }
                if (txtSerieDocumento.Text.Trim() == "") { MessageBox.Show("Debe ingresar una serie de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtSerieDocumento.Focus(); return; }
                if (txtNumeroDocumento.Text.Trim() == "") { MessageBox.Show("Debe ingresar un numero de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNumeroDocumento.Focus(); return; }
                if (lkpEmpresaProveedor.EditValue == null)
                {
                    MessageBox.Show("Debe seleccionar una empresa.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lkpEmpresaProveedor.Focus(); return;
                }
                try
                {
                    frmCrearDetalleFactura frm = new frmCrearDetalleFactura(this);
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorEventRow = colorEventRow;
                    frm.colorFocus = colorFocus;
                    frm.cod_proveedor = cod_proveedor;//duda
                    frm.cod_empresa = lkpEmpresaProveedor.EditValue.ToString();
                    frm.tipo_documento = glkpTipoDocumento.EditValue.ToString();
                    frm.txtRucProveedor.Text = txtRucProveedor.Text;
                    frm.txtProveedor.Text = txtProveedor.Text;
                    frm.txtSerieDocumento.Text = txtSerieDocumento.Text;
                    frm.txtNumeroDocumento.Text = txtNumeroDocumento.Text;
                    frm.txtEmpresaUsuaria.Text = lkpEmpresaProveedor.Text;
                    frm.txtTipoServicioProveedor.Text = lkpTipoServicioProveedor.Text;
                    frm.txtNumOrden.Text = txtOrdenCompraServicio.Text;
                    frm.user = user;
                    frm.dtFechaRegistro.EditValue = DateTime.Now;
                    frm.txtUsuarioRegistro.Text = user.dsc_usuario;
                    frm.dtFechaModificacion.EditValue = DateTime.Now;
                    frm.txtUsuarioCambio.Text = user.dsc_usuario;
                    frm.MiAccion = DetalleFactura.Nuevo;
                    frm.ShowDialog();

                    eFacturaProveedor fac = new eFacturaProveedor();
                    fac.num_OrdenCompraServ = txtOrdenCompraServicio.Text.ToString();
                    List<eFacturaProveedor.eFacturaProvedor_Detalle> ListadoOrdenesAsignados = new List<eFacturaProveedor.eFacturaProvedor_Detalle>();
                    ListadoOrdenesAsignados = blFact.ListarDetalleFactura<eFacturaProveedor.eFacturaProvedor_Detalle>(5, fac.num_OrdenCompraServ);
                    bsDetalleFactura.DataSource = null; bsDetalleFactura.DataSource = ListadoOrdenesAsignados;



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void gvFacturaDetalle_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoCECODisponible_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoCECODisponible_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoCECOAsignado_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoCECOAsignado_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void btnAsignarCECO_Click(object sender, EventArgs e)
        {
            int valor = 0;
            foreach (int nRow in gvListadoCECODisponible.GetSelectedRows())
            {

                eFacturaProveedor.eFacturaProveedor_Distribucion obj = gvListadoCECODisponible.GetRow(nRow - valor) as eFacturaProveedor.eFacturaProveedor_Distribucion;
                bsListadoCECOAsignados.Add(obj);
                bsListadoCECODisponibles.Remove(obj);
                valor = valor + 1;
            }
        }

        private void btnAsignarPerfil_Click(object sender, EventArgs e)
        {
            int valor = 0;
            foreach (int nRow in gvDetalleFactura.GetSelectedRows())
            {

                eFacturaProveedor.eFacturaProveedor_Distribucion obj = gvListadoCECOAsignado.GetRow(nRow - valor) as eFacturaProveedor.eFacturaProveedor_Distribucion;
                bsListadoCECODisponibles.Add(obj); 
                bsListadoCECOAsignados.Remove(obj);
                valor = valor + 1;
            }
        }

        private void gvFacturaDetalle_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eFacturaProveedor.eFacturaProvedor_Detalle obj = gvFacturaDetalle.GetFocusedRow() as eFacturaProveedor.eFacturaProvedor_Detalle;
                   
                        frmCrearDetalleFactura frm = new frmCrearDetalleFactura(this);
                        frm.MiAccion = DetalleFactura.Editar;
                        frm.user = user;
                        frm.colorVerde = colorVerde;
                        frm.colorPlomo = colorPlomo;
                        frm.colorEventRow = colorEventRow;
                        frm.colorFocus = colorFocus;
                        frm.cod_proveedor = cod_proveedor;//duda
                        frm.cod_empresa = lkpEmpresaProveedor.EditValue.ToString();
                        frm.tipo_documento = glkpTipoDocumento.EditValue.ToString();
                        frm.txtRucProveedor.Text = txtRucProveedor.Text;
                        frm.txtProveedor.Text = txtProveedor.Text;
                        frm.txtSerieDocumento.Text = txtSerieDocumento.Text;
                        frm.txtNumeroDocumento.Text = txtNumeroDocumento.Text;
                        frm.txtEmpresaUsuaria.Text = lkpEmpresaProveedor.Text;
                        frm.txtTipoServicioProveedor.Text = lkpTipoServicioProveedor.Text;
                        frm.txtNumOrden.Text = txtOrdenCompraServicio.Text;

                        //NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
                        //frm.GrupoSeleccionado = navGrupo.Caption;
                        //frm.ItemSeleccionado = navGrupo.SelectedLink.Item.Tag.ToString();
                        //frm.ShowDialog();


                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvFacturaDetalle_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void btnAgregarProyecto_ItemClick(object sender, ItemClickEventArgs e)
        {
            Busqueda("", "Proyecto");
        }

        private void btnVincularDocumentoNC_Click(object sender, EventArgs e)
        {
            if (txtProveedor.Text.Trim() == "") { MessageBox.Show("Debe seleccionar proveedor.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtProveedor.Focus(); return; }
            if (lkpEmpresaProveedor.EditValue == null) { MessageBox.Show("Debe seleccionar una empresa.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpEmpresaProveedor.Focus(); return; }
            if (lkpTipoMoneda.EditValue == null) { MessageBox.Show("Debe seleccionar una moneda.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoMoneda.Focus(); return; }

            frmFacturasDetalle frm = new frmFacturasDetalle();
            frm.BusquedaAutomatica = false;
            frm.BusquedaLogistica = false;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorFocus = colorFocus;
            frm.colorEventRow = colorEventRow;
            frm.cod_tipo_documento = glkpTipoDocumento.EditValue.ToString();
            frm.cod_empresa = lkpEmpresaProveedor.EditValue.ToString();
            frm.cod_proveedor = txtProveedor.Tag.ToString();
            frm.cod_moneda = lkpTipoMoneda.EditValue.ToString();
            frm.user = user;
            frm.ShowDialog();
            if (frm.listDocumentosNC.Count > 0)
            {
                listDocumentosNC = frm.listDocumentosNC;
                bsDocumentosVinculados.DataSource = listDocumentosNC; 
                gvFacturasProveedor.RefreshData();
            }
        }

        private void gvFacturasProveedor_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvFacturasProveedor_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvFacturasProveedor_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 1 && e.Column.FieldName == "dsc_documento")
                {
                    eFacturaProveedor.eFacturaProveedor_NotaCredito obj = gvFacturasProveedor.GetFocusedRow() as eFacturaProveedor.eFacturaProveedor_NotaCredito;
                    if (obj == null) { return; }

                    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                    //if (Application.OpenForms["frmMantFacturaProveedor"] != null)
                    //{
                    //    Application.OpenForms["frmMantFacturaProveedor"].Activate();
                    //}
                    //else
                    //{
                    frmModif.MiAccion = Factura.Vista;
                    frmModif.colorVerde = colorVerde;
                    frmModif.colorPlomo = colorPlomo;
                    frmModif.colorFocus = colorFocus;
                    frmModif.colorEventRow = colorEventRow;
                    frmModif.RUC = obj.dsc_ruc;
                    if (glkpTipoDocumento.EditValue.ToString() == "TC006")
                    {
                        frmModif.tipo_documento = obj.tipo_documento;
                        frmModif.serie_documento = obj.serie_documento;
                        frmModif.numero_documento = obj.numero_documento;
                        frmModif.cod_proveedor = obj.cod_proveedor;
                    }
                    else
                    {
                        frmModif.tipo_documento = obj.tipo_documento_NC;
                        frmModif.serie_documento = obj.serie_documento_NC;
                        frmModif.numero_documento = obj.numero_documento_NC;
                        frmModif.cod_proveedor = obj.cod_proveedor_NC;
                    }
                    
                    frmModif.user.cod_usuario = user.cod_usuario;
                    frmModif.ShowDialog();
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnEscanearDocumento_ItemClick(object sender, ItemClickEventArgs e)
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
                TD_sunat = list.Find(x => x.tipo_documento == tipo_documento).cod_sunat;
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
                imagen.ScaleAbsolute(writer.PageSize.Width -10, writer.PageSize.Height - 10);
                imagen.SetAbsolutePosition(5, 5);
                doc.Add(imagen);
                doc.Close();
                writer.Close();

                //Guardamos PDF en ruta de ONEDRIVE
                await AdjuntarArchivo(archivoEscaneado + ".pdf", ".pdf");
                if (File.Exists(archivoEscaneado + ".png")) File.Delete(archivoEscaneado + ".png");
                if (File.Exists(archivoEscaneado + ".pdf")) File.Delete(archivoEscaneado + ".pdf");

                XtraMessageBox.Show("Se procedió a escanear y cargar el documento al OneDrive de manera satisfactoria.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnVerPDF.Enabled = true; btnEscanearDocumento.Enabled = false;
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
                DateTime FechaRegistro = Convert.ToDateTime(dtFechaRegistro.EditValue);
                int Anho = FechaRegistro.Year; int Mes = FechaRegistro.Month; string NombreMes = FechaRegistro.ToString("MMMM");

                eFacturaProveedor resultado = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(2, tipo_documento, serie_documento, numero_documento, cod_proveedor);
                if (resultado == null) { MessageBox.Show("Antes de adjuntar el PDF debe crear la factura.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                string IdArchivoAnho = "", IdArchivoMes = "";
                var idArchivoPDF = ""; var idArchivoXML = "";
                var TamañoDoc = new FileInfo(FileName).Length / 1024;
                if (TamañoDoc < 4000)
                {
                    varPathOrigen = FileName;
                    //varNombreArchivo = Path.GetFileNameWithoutExtension(myFileDialog.SafeFileName) + Path.GetExtension(myFileDialog.SafeFileName);
                    List<eFacturaProveedor> list = blFact.CombosEnGridControl<eFacturaProveedor>("TipoDocumento");
                    TD_sunat = list.Find(x => x.tipo_documento == tipo_documento).cod_sunat;
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
                eEmpresa eEmp = blFact.ObtenerDatosEmpresa<eEmpresa>(12, lkpEmpresaProveedor.EditValue.ToString());
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
                eDatos = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(26, lkpEmpresaProveedor.EditValue.ToString(), Convert.ToDateTime(dtFechaRegistro.EditValue).Year, dsc_Carpeta: dsc_Carpeta);
                var targetItemFolderId = eDatos.idCarpeta;

                //eFacturaProveedor IdCarpetaAnho = blFact.ObtenerDatosOneDrive<eFacturaProveedor>(13, lkpEmpresaProveedor.EditValue.ToString(), Convert.ToDateTime(dtFechaRegistro.EditValue).Year);
                eEmpresa.eOnedrive_Empresa IdCarpetaAnho = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(13, lkpEmpresaProveedor.EditValue.ToString(), Convert.ToDateTime(dtFechaRegistro.EditValue).Year, dsc_Carpeta: dsc_Carpeta);
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
                eEmpresa.eOnedrive_Empresa IdCarpetaMes = blFact.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(14, lkpEmpresaProveedor.EditValue.ToString(), Convert.ToDateTime(dtFechaRegistro.EditValue).Year, Convert.ToDateTime(dtFechaRegistro.EditValue).Month, dsc_Carpeta);
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
                    int opcion = extension.ToLower() == ".pdf" ? 1 : extension.ToLower() == ".xml" ? 2 : 0;
                    if (opcion == 1 || opcion == 2)
                    {
                        var DriveItem = await GraphClient.Me.Drive.Items[IdArchivoMes].ItemWithPath(varNombreArchivo).Content.Request().PutAsync<Microsoft.Graph.DriveItem>(stream);
                        idArchivoPDF = opcion == 1 ? DriveItem.Id : "";
                        idArchivoXML = opcion == 2 ? DriveItem.Id : "";

                        eFacturaProveedor objFact = new eFacturaProveedor();
                        objFact.tipo_documento = resultado.tipo_documento;
                        objFact.serie_documento = resultado.serie_documento;
                        objFact.numero_documento = resultado.numero_documento;
                        objFact.cod_proveedor = resultado.cod_proveedor;
                        objFact.idPDF = idArchivoPDF;
                        objFact.idXML = idArchivoXML;
                        //objFact.NombreArchivo = varNombreArchivo;
                        objFact.NombreArchivo = varNombreArchivoSinExtension;
                        objFact.cod_empresa = resultado.cod_empresa;
                        objFact.idCarpetaAnho = IdArchivoAnho;
                        objFact.idCarpetaMes = IdArchivoMes;

                        res = blFact.ActualizarInformacionDocumentos(opcion, objFact, targetItemFolderId, Anho.ToString(), $"{Mes:00}", dsc_Carpeta);
                    }

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


        private void gvDetalleFactura_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            gvDetalleFactura.PostEditor(); gvDetalleFactura.RefreshData();
            eFacturaProveedor.eFacturaProveedor_Distribucion eobjFact = gvDetalleFactura.GetFocusedRow() as eFacturaProveedor.eFacturaProveedor_Distribucion;
            decimal porc = 0;
            for (int x = 0; x < gvDetalleFactura.DataRowCount; x++)
            {
                eFacturaProveedor.eFacturaProveedor_Distribucion obj = gvDetalleFactura.GetRow(x) as eFacturaProveedor.eFacturaProveedor_Distribucion;
                if (obj == null) continue;
                porc = porc + obj.porc_distribucion;
            }

            if (porc == 1 || ((eobjFact.porc_distribucion + porc) >= 1 && gvDetalleFactura.RowCount != gvDetalleFactura.DataRowCount)) gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            if (porc < 1 && gvDetalleFactura.RowCount == gvDetalleFactura.DataRowCount) gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
        }

        private void btnAprobarDocumento_ItemClick(object sender, ItemClickEventArgs e)
        {
            txtGlosaFactura.Select();
            gvDetalleFactura.PostEditor(); gvDetalleFactura.RefreshData(); 
            if (gvDetalleFactura.DataRowCount == 0 && (chkFlagInventario.CheckState == CheckState.Unchecked && chkFlagActivoFijo.CheckState == CheckState.Unchecked)) { MessageBox.Show("Debe ingresar la distribución de Centros de Costos.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (chkFlagInventario.CheckState == CheckState.Unchecked && chkFlagActivoFijo.CheckState == CheckState.Unchecked)
            {
                decimal porc = 0;
                for (int x = 0; x < gvDetalleFactura.DataRowCount; x++)
                {
                    eFacturaProveedor.eFacturaProveedor_Distribucion eObjj = gvDetalleFactura.GetRow(x) as eFacturaProveedor.eFacturaProveedor_Distribucion;
                    if (eObjj == null) continue;
                    porc = porc + eObjj.porc_distribucion;
                }
                if (porc == 0 || porc < 1)
                {
                    MessageBox.Show("La distribución de los porcentajes de CECO no puede ser 0 o menor a 100." + Environment.NewLine + "Por favor vuelva a distribuir nuevamente los porcentajes a cada CECO.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (lkpEstadoRegistro.EditValue.ToString() == "APR")
            {
                MessageBox.Show("El documento ya se encuentra aprobado.", "Aprobar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            eFacturaProveedor eFact = AsignarValoresFacturasProveedor();
            eFacturaProveedor eFacts = blFact.InsertarFacturaProveedor<eFacturaProveedor>(eFact);
            if (eFacts == null) { MessageBox.Show("Error al registrar documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }


            gvDetalleFactura.PostEditor();
            for (int x = 0; x < gvDetalleFactura.DataRowCount; x++)
            {
                eFacturaProveedor.eFacturaProveedor_Distribucion objD = gvDetalleFactura.GetRow(x) as eFacturaProveedor.eFacturaProveedor_Distribucion;
                if (objD == null) continue;
                objD.tipo_documento = tipo_documento; objD.serie_documento = serie_documento; objD.numero_documento = numero_documento; 
                objD.cod_proveedor = cod_proveedor; objD.cod_usuario_registro = user.cod_usuario; objD.cod_empresa = cod_empresa;
                eFacturaProveedor.eFacturaProveedor_Distribucion eDetFact = new eFacturaProveedor.eFacturaProveedor_Distribucion();
                eDetFact = blFact.InsertarDistribucionFacturaProveedor<eFacturaProveedor.eFacturaProveedor_Distribucion>(objD);
            }

            gvObservacionesFactura.PostEditor();
            for (int y = 0; y < gvObservacionesFactura.DataRowCount; y++)
            {
                eFacturaProveedor.eFacturaProveedor_Observaciones objO = gvObservacionesFactura.GetRow(y) as eFacturaProveedor.eFacturaProveedor_Observaciones;
                if (objO == null) continue;
                objO.tipo_documento = tipo_documento; objO.serie_documento = serie_documento; objO.numero_documento = numero_documento;
                objO.cod_proveedor = cod_proveedor; objO.cod_usuario_registro = user.cod_usuario; objO.cod_empresa = cod_empresa;
                eFacturaProveedor.eFacturaProveedor_Observaciones eObsFact = new eFacturaProveedor.eFacturaProveedor_Observaciones();
                eObsFact = blFact.InsertarObservacionesFacturaProveedor<eFacturaProveedor.eFacturaProveedor_Observaciones>(objO);
            }

            eFacturaProveedor obj = new eFacturaProveedor();
            obj.tipo_documento = glkpTipoDocumento.EditValue.ToString(); obj.serie_documento = txtSerieDocumento.Text; 
            obj.numero_documento = txtNumeroDocumento.Text == "" ? 0 : Convert.ToDecimal(txtNumeroDocumento.Text);
            obj.cod_proveedor = txtProveedor.Tag.ToString(); obj.cod_empresa = lkpEmpresaProveedor.EditValue.ToString();
            obj.cod_estado_registro = "APR"; obj.cod_usuario_registro = user.cod_usuario;
            obj.periodo_tributario = ""; obj.cod_usuario_aprobado_reg = user.cod_usuario;
            string result = blFact.Actualiar_EstadoRegistroFactura(obj);
            if (result != "OK") { MessageBox.Show("Error al aprobar documento", "Aprobar documento", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            lkpEstadoRegistro.EditValue = "APR";
            btnAprobarDocumento.Enabled = false;
            XtraMessageBox.Show("Se aprobó el documento de manera satisfactoria", "Aprobar documento", MessageBoxButtons.OK, MessageBoxIcon.Information);

            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objProg = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
            if (eFact.imp_saldo == 0) return;
            objProg.tipo_documento = obj.tipo_documento; objProg.serie_documento = obj.serie_documento; objProg.numero_documento = obj.numero_documento; objProg.cod_proveedor = obj.cod_proveedor;
            objProg.num_linea = 0; objProg.cod_empresa = obj.cod_empresa;
            //objProg.fch_pago = Convert.ToDateTime(dtFechaPagoProgramado.EditValue);
            objProg.imp_pago = Convert.ToDecimal(txtMontoSaldo.EditValue); 
            objProg.cod_tipo_prog = EntregaRendir == "SI" ? "ENTREGAREN" : glkpTipoDocumento.EditValue.ToString() == "TC006" ? "NOTACRED" : "REGULAR";
            objProg.cod_formapago = glkpTipoDocumento.EditValue.ToString() == "TC006" ? "NOTACRED" : "TRANF";
            objProg.fch_pago = EntregaRendir == "SI" ? eFact.fch_registro : eFact.fch_pago_programado; objProg.dsc_observacion = null; 
            objProg.cod_estado = EntregaRendir == "SI" ? "PRO" : eFact.cod_estado_pago == "PAG" ? "EJE" : "PRO"; 
            objProg.cod_pagar_a = EntregaRendir == "SI" ? "TRAB" : "PROV";
            objProg.fch_ejecucion = EntregaRendir == "SI" ? new DateTime() : eFact.cod_estado_pago == "PAG" ? eFact.fch_pago_ejecutado : new DateTime(); 
            objProg.cod_usuario_ejecucion = null; objProg.cod_usuario_registro = user.cod_usuario;
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
            eProgFact = blFact.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(objProg);
            if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ObtenerDatos_FacturaProveedor();
            ObtenerDatos_DistribucionFactura();
            ObtenerDatos_ProgramacionPagosFactura();
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

                if (glkpTipoDocumento.EditValue.ToString() == "TC008")
                {
                    chkAplicaIGV.CheckState = CheckState.Unchecked;
                }
                xtraTabPage4.PageVisible = glkpTipoDocumento.EditValue.ToString() == "TC006" ? true : false;
                layoutControlItem58.Visibility = glkpTipoDocumento.EditValue.ToString() == "TC006" ? LayoutVisibility.Always : LayoutVisibility.Never;
                lkpModalidadPago.EditValue = glkpTipoDocumento.EditValue.ToString() == "TC006" ? "MP001" : lkpModalidadPago.EditValue;
                txtMontoSubTotal.EditValue = glkpTipoDocumento.EditValue.ToString() == "TC006" ? -1 * Convert.ToDecimal(txtMontoSubTotal.EditValue) : txtMontoSubTotal.EditValue;
                txtMontoIGV.EditValue = glkpTipoDocumento.EditValue.ToString() == "TC006" ? -1 * Convert.ToDecimal(txtMontoIGV.EditValue) : txtMontoIGV.EditValue;
                txtMontoTotal.EditValue = glkpTipoDocumento.EditValue.ToString() == "TC006" ? -1 * Convert.ToDecimal(txtMontoTotal.EditValue) : txtMontoTotal.EditValue;
                txtMontoSaldo.EditValue = glkpTipoDocumento.EditValue.ToString() == "TC006" ? -1 * Convert.ToDecimal(txtMontoSaldo.EditValue) : txtMontoSaldo.EditValue;
                string dsc_documento = glkpTipoDocumento.Text;
                glkpTipoDocumento.ToolTip = dsc_documento;
            }
        }

        private void dtFechaVencimiento_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtFechaVencimiento.EditValue.ToString().Contains("0/00/0000")) return;
                //FECHA PAGO PROGRAMADO, es el viernes proximo de la fecha de vencimiento
                int nDia = Convert.ToInt32(Convert.ToDateTime(dtFechaVencimiento.EditValue).DayOfWeek);
                nDia = nDia <= 5 ? 5 - nDia : nDia;
                dtFechaPagoProgramado.EditValue = Convert.ToDateTime(dtFechaVencimiento.EditValue).AddDays(nDia);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dtFechaConstanciaDetraccion_EditValueChanged(object sender, EventArgs e)
        {
            if (dtFechaConstanciaDetraccion.EditValue != null)
            {
                grdbDetraccionAplicada.SelectedIndex = 0;
            }
            else
            {
                grdbDetraccionAplicada.SelectedIndex = 1;
            }
        }

        private void rbtnEliminarDistribucionCECO_Click(object sender, EventArgs e)
        {
            decimal porc = 0; 
            if (MiAccion == Factura.Nuevo)
            {
                gvDetalleFactura.DeleteRow(gvDetalleFactura.FocusedRowHandle);
                gvDetalleFactura.PostEditor(); porc = 0;
                for (int x = 0; x < gvDetalleFactura.DataRowCount; x++)
                {
                    eFacturaProveedor.eFacturaProveedor_Distribucion eObjj = gvDetalleFactura.GetRow(x) as eFacturaProveedor.eFacturaProveedor_Distribucion;
                    if (eObjj == null) continue;
                    porc = porc + eObjj.porc_distribucion;
                }
                 if (porc == 0 || porc < 1)
                {
                    MessageBox.Show("La distribución de los porcentajes de CECO no puede ser 0 o menor a 100." + Environment.NewLine + "Por favor vuelva a distribuir nuevamente los porcentajes a cada CECO.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (porc == 1) gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                    if (porc < 1) gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom; 
                    return;
                }
                
            }
            else
            {
                if (MessageBox.Show("¿Esta seguro de eliminar el registro?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    eFacturaProveedor.eFacturaProveedor_Distribucion obj = gvDetalleFactura.GetFocusedRow() as eFacturaProveedor.eFacturaProveedor_Distribucion;
                    if (obj == null) return;
                    string result = blFact.EliminarDatosFactura(1, tipo_documento, serie_documento, numero_documento, cod_proveedor, obj.cod_und_negocio, obj.cod_tipo_gasto, obj.cod_cliente);
                    if (result != "OK") { MessageBox.Show("Error al eliminar registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    ObtenerDatos_DistribucionFactura();
                    gvDetalleFactura.PostEditor(); porc = 0;
                    for (int x = 0; x < gvDetalleFactura.DataRowCount; x++)
                    {
                        eFacturaProveedor.eFacturaProveedor_Distribucion eObjj = gvDetalleFactura.GetRow(x) as eFacturaProveedor.eFacturaProveedor_Distribucion;
                        if (eObjj == null) continue;
                        porc = porc + eObjj.porc_distribucion;
                    }
                    if (porc == 0 || porc < 1)
                    {
                        MessageBox.Show("La distribución de los porcentajes de CECO no puede ser 0 o menor a 100." + Environment.NewLine + "Por favor vuelva a distribuir nuevamente los porcentajes a cada CECO.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (porc == 1) gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                    if (porc < 1) gvDetalleFactura.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                }
            }
        }

        private void rbtnEliminarObs_Click(object sender, EventArgs e)
        {
            if (MiAccion == Factura.Nuevo)
            {
                gvObservacionesFactura.DeleteRow(gvObservacionesFactura.FocusedRowHandle);
            }
            else
            {
                if (MessageBox.Show("¿Esta seguro de eliminar el registro?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    eFacturaProveedor.eFacturaProveedor_Observaciones obj = gvObservacionesFactura.GetFocusedRow() as eFacturaProveedor.eFacturaProveedor_Observaciones;
                    if (obj == null) return;
                    string result = blFact.EliminarDatosFactura(2, tipo_documento, serie_documento, numero_documento, cod_proveedor, num_linea: obj.num_linea);
                    if (result != "OK") { MessageBox.Show("Error al eliminar registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    ObtenerDatos_ObservacionesFactura();
                }  
            }
        }

        private async void btnVerPDF_ItemClick(object sender, ItemClickEventArgs e)
        {
            eFacturaProveedor eFact = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(24, tipo_documento, serie_documento, numero_documento, cod_proveedor);
            
            if (eFact.idPDF == null || eFact.idPDF == "")
            {
                MessageBox.Show("No se cargado ningún PDF", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //try
                //{
                //    if (NombreArchivo == null)
                //    {
                //        eFacturaProveedor eFact = new eFacturaProveedor();
                //        eFact = blFact.GetObtenerAgrupacion<eFacturaProveedor>(83, "", 0, 0, 0, "", 0, 0, 0, 0, 0, 0, "", 0, 0, "", "", resultado.IdFactura, "", "", "", "", "", "", "", "");
                //        if (eFact != null) { NombreArchivo = eFact.NombreArchivo; CarpetaDestino = eFact.CarpetaDestino; } else { return; }
                //    }
                //    eConfigSistema eConfig = new eConfigSistema();
                //    eConfig = blFact.GetObtenerAgrupacion<eConfigSistema>(82, "", 0, 0, 0, "", 0, 0, 0, 0, 0, 0, "", 0, 0, "", "");

                //    string pdfPath = Path.Combine(Application.StartupPath, eConfig.Valor1 + CarpetaDestino + @"\" + NombreArchivo + ".pdf");
                //    Process.Start(pdfPath);
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
            }
            else
            {
                eEmpresa eEmp = blFact.ObtenerDatosEmpresa<eEmpresa>(12, lkpEmpresaProveedor.EditValue.ToString());
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
        
        private void btnImportarXML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                OpenFileDialog OpenXml = new OpenFileDialog();
                string sRutaArchivo = "";

                var withBlock = OpenXml;
                withBlock.Title = "Seleccionar archivo XML";
                withBlock.Filter = "Archivos XML|*.xml";
                withBlock.Multiselect = false;
                var resultado = withBlock.ShowDialog();
                if (resultado == DialogResult.OK)
                {
                    string sArchivox = "";
                    sArchivox = withBlock.FileName;
                    sRutaArchivo = Path.GetFullPath(sArchivox);
                    btnImportarXML.Tag = Path.GetDirectoryName(sArchivox);
                }
                else
                {
                    return;
                }

                rtxtResultados.LoadFile(sRutaArchivo, RichTextBoxStreamType.PlainText);
                string sTextoXML = this.rtxtResultados.Text;

                string sApertura = ""; string sCierre = ""; string TipoDoc = "";
                int nPosA = -1; int nPosB = -1;

                if (nPosA == -1) { nPosA = sTextoXML.IndexOf("<Invoice "); sApertura = "<Invoice "; TipoDoc = "FAC"; }
                if (nPosB == -1) { nPosB = sTextoXML.IndexOf("</Invoice>"); sCierre = "</Invoice>"; }
                if (nPosA == -1) { nPosA = sTextoXML.IndexOf("<DebitNote "); sApertura = "<DebitNote "; TipoDoc = "ND"; }
                if (nPosB == -1) { nPosB = sTextoXML.IndexOf("</DebitNote>"); sCierre = "</DebitNote>"; }
                if (nPosA == -1) { nPosA = sTextoXML.IndexOf("<CreditNote "); sApertura = "<CreditNote "; TipoDoc = "NC"; }
                if (nPosB == -1) { nPosB = sTextoXML.IndexOf("</CreditNote>"); sCierre = "</CreditNote>"; }

                if (nPosA <= 0 || nPosB <= 0) { MessageBox.Show("XML no valido", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                string sInvoice = sTextoXML.Substring(nPosA, (nPosB - nPosA) + sCierre.Length);

                sApertura = "<ext:UBLExtensions>";
                sCierre = "</ext:UBLExtensions>";
                nPosA = sInvoice.IndexOf(sApertura);
                nPosB = sInvoice.IndexOf(sCierre);
                if (nPosA < 0) { sApertura = "<cec:UBLExtensions>"; nPosA = sInvoice.IndexOf(sApertura); }
                if (nPosB < 0) { sCierre = "</cec:UBLExtensions>"; nPosB = sInvoice.IndexOf(sCierre); }

                if (nPosA <= 0 || nPosB <= 0) { MessageBox.Show("XML no valido", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                string sUBLExtensions = sInvoice.Substring(nPosA, (nPosB - nPosA) + sCierre.Length);

                string sDatos = sInvoice.Substring(nPosB + sCierre.Length + 1, sInvoice.Length - (nPosB + sCierre.Length + 1));
                sCierre = "</Invoice>";
                sDatos = sDatos.Substring(1, (sDatos.Length - sCierre.Length) - 1);

                this.rtxtResultados.Text = sDatos;

                //Desmenuzar los datos 
                int SPosRH = sDatos.IndexOf("<cbc:ID>RET 4TA</cbc:ID>");
                bool swEsRH = (SPosRH > 0);
                string sVersionUBL = FExtraerValorXML(sDatos, "cbc:UBLVersionID");
                string sDocumento = FExtraerValorXML(sDatos, "cbc:ID");
                string sFechaDoc = FExtraerValorXML(sDatos, "cbc:IssueDate");
                string sFechaVencimiento = FExtraerValorXML(sDatos, "cbc:DueDate");
                string sTipoDoc = FExtraerValorXML(sDatos, "cbc:InvoiceTypeCode");
                string sMonedaDoc = FExtraerValorXML(sDatos, "cbc:DocumentCurrencyCode");
                string sNote = FExtraerValorXML(sDatos, "cbc:Note ", 0, false);

                //Documento de Referencia a la NOTA DE CREDITO
                string sDatosDocRef = FExtraerValorXML(sDatos, "cac:InvoiceDocumentReference");
                string sDocumentoRef = FExtraerValorXML(sDatosDocRef, "cbc:ID");
                string sFechaDocRef = FExtraerValorXML(sDatosDocRef, "cbc:IssueDate");
                string sTipoDocRef = FExtraerValorXML(sDatosDocRef, "cbc:DocumentTypeCode", swAperturaSinCierre: true, sNotaCredito: true);
                string sSerieDocRef = "";
                string sNumeroDocRef = "";
                if (sDatosDocRef.Trim() != "")
                {
                    
                    if (sTipoDocRef.Length == 2)
                    {
                        List<eTipoComprobante> listTipoComp = blFact.CombosEnGridControl<eTipoComprobante>("TipoDocumento");
                        eTipoComprobante eTipoComp = listTipoComp.Find(x => x.cod_sunat == sTipoDocRef);
                        sTipoDocRef = eTipoComp != null ? eTipoComp.cod_tipo_comprobante : "TC002";
                    }
                    else
                    {
                        sTipoDocRef = "TC002";
                    }
                    sSerieDocRef = sDocumentoRef.Substring(0, 4);
                    sNumeroDocRef = sDocumentoRef.Replace(" ", "").Substring(5, (sDocumentoRef.Replace(" ", "").Length - 5));
                }

                //Seccion signature
                string sSignature = FExtraerValorXML(sDatos, "cac:Signature");
                string sRUCSignatureSIGIN = FExtraerValorXML(sSignature, "cbc:ID");
                string sSignatoryParty = FExtraerValorXML(sDatos, "cac:SignatoryParty");
                string sPartyIdentification = FExtraerValorXML(sDatos, "cac:PartyIdentification");
                string sRUCSignature = FExtraerValorXML(sPartyIdentification, "cbc:ID", ForzarRUCEmpresa: true);
                if (sRUCSignature.Length > 11)
                {
                    sRUCSignature = sRUCSignature.Substring(sRUCSignature.Length - 11, 11);
                }

                //Seccion AccountingSupplierParty(RH)
                string sAccountingSuplierParty = FExtraerValorXML(sDatos, "cac:AccountingSupplierParty");
                string sRUCAssignaedRH = FExtraerValorXML(sDatos, "cbc:CustomerAssignedAccountID");
                string sParty = FExtraerValorXML(sAccountingSuplierParty, "cac:Party");
                string sPartyIdentification2 = FExtraerValorXML(sParty, "cac:PartyIdentification");
                string sRUCSignature2 = FExtraerValorXML(sPartyIdentification2, "cbc:ID", ForzarRUCEmpresa: true);
                if (sRUCSignature2 != "")
                {
                    sRUCSignature2 = sRUCSignature2.Substring(sRUCSignature2.Length - 11, 11);
                }

                ////Validamos a qué EMPRESA pertenece el documento
                string AccountingCustomerParty = FExtraerValorXML(sDatos, "cac:AccountingCustomerParty");
                string PartyIdentification = FExtraerValorXML(AccountingCustomerParty, "cac:PartyIdentification");
                string CustomerAssignedAccountID = FExtraerValorXML(AccountingCustomerParty, "cbc:CustomerAssignedAccountID");
                if (CustomerAssignedAccountID == "" || CustomerAssignedAccountID.Length > 11)
                {
                    CustomerAssignedAccountID = FExtraerValorXML(AccountingCustomerParty, "cbc:ID", ForzarRUCEmpresa: true);
                }

                eEmpresa eEmp = blFact.ObtenerDatosEmpresa<eEmpresa>(18, "", CustomerAssignedAccountID);
                //if (eEmp != null) lkpEmpresaProveedor.EditValue = eEmp.cod_empresa;

                string sLegalMonetaryTotal = "", sPayableAmount = "", sMonto = "", sTaxTotal = "", sTaxSubTotal = "", sTaxableAmount = "", sTaxAmount = "";
                //Monto
                if (swEsRH == false)
                {
                    sLegalMonetaryTotal = FExtraerValorXML(sDatos, TipoDoc == "FAC" ? "cac:LegalMonetaryTotal" : TipoDoc == "ND" ? "cac:RequestedMonetaryTotal" : "cac:LegalMonetaryTotal");
                    sPayableAmount = FExtraerValorXML(sLegalMonetaryTotal, "cbc:PayableAmount", 0, true);
                    int nPosCierreCar = sPayableAmount.IndexOf(">");
                    sMonto = "0.00";
                    if (nPosCierreCar > 0) { sMonto = sPayableAmount.Substring(nPosCierreCar + 1); }
                }
                else
                {
                    sTaxTotal = FExtraerValorXML(sDatos, "cac:TaxTotal");
                    sTaxSubTotal = FExtraerValorXML(sTaxTotal, "cac:TaxSubtotal");
                    sTaxableAmount = FExtraerValorXML(sTaxSubTotal, "cbc:TaxableAmount", 0, true);
                    int nPosCierreCar = sTaxableAmount.IndexOf(">");
                    sTaxAmount = FExtraerValorXML(sTaxSubTotal, "cbc:TaxAmount", 0, true);
                    int nPosCierreCar2 = sTaxableAmount.IndexOf(">");
                    sMonto = "0.00"; chkRetencion.CheckState = CheckState.Checked; txtTasaRetencion.EditValue = 0.08;
                    if (nPosCierreCar > 0) { sMonto = sTaxableAmount.Substring(nPosCierreCar + 1); txtMontoRetencion.EditValue = sTaxAmount.Substring(nPosCierreCar + 1); }
                }

                //Descripcion de la Factura 
                string sItemC = FExtraerValorXML(sDatos, "cac:Item");
                string sDescripcion = FExtraerValorXML(sItemC, "cbc:Description");
                sDescripcion = sDescripcion.Replace("<![CDATA[", "");
                sDescripcion = sDescripcion.Replace("]]>", "");
                sDescripcion = sDescripcion.Replace("\n", " ");

                if (swEsRH == true) { this.rtxtResultados.Text = this.rtxtResultados.Text + " ES RECIBO HONORARIOS "; }

                string TipoMoneda = "";
                //llenar los campos 
                string tipoDoc = sDocumento.Substring(0, 1);
                eProveedor eProv = new eProveedor();
                if (swEsRH == false && sRUCSignature2.Length > 11)
                {
                    eProv = blFact.BuscarModalidadPago<eProveedor>(17, "", sRUCSignature.Trim());
                }
                else if (sRUCSignature2.Length == 11)
                {
                    eProv = blFact.BuscarModalidadPago<eProveedor>(17, "", sRUCSignature2.Trim());
                }
                else
                {
                    eProv = blFact.BuscarModalidadPago<eProveedor>(17, "", sRUCAssignaedRH.Trim());
                }

                if (sDatosDocRef.Trim() != "")
                {
                    //VALIDAR si existe DOCUMENTO REFERENCIA ingresado en el sistema
                    eFacturaProveedor eDocRef = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(2, sTipoDocRef, sSerieDocRef, Convert.ToDecimal(sNumeroDocRef), eProv.cod_proveedor);
                    if (eDocRef == null) { MessageBox.Show("No se encuentra registrado el documento " + sDocumentoRef + " en el sistema, por favor ingresarlo.", "Nota de Crédito", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    eTipoComprobante objTComp = new eTipoComprobante();
                    objTComp = blFact.BuscarTipoComprobante<eTipoComprobante>(27, sTipoDocRef);
                    num_ctd_doc = objTComp.num_ctd_doc;
                    fmt_nro_doc = new string('0', num_ctd_doc);

                    eFacturaProveedor.eFacturaProveedor_NotaCredito eNotaCred = new eFacturaProveedor.eFacturaProveedor_NotaCredito();
                    eNotaCred.tipo_documento_NC = "";
                    if (swEsRH == true) { eNotaCred.tipo_documento_NC = "TC008"; } else { eNotaCred.tipo_documento_NC = TipoDoc == "FAC" ? "TC002" : TipoDoc == "NC" ? "TC006" : TipoDoc == "ND" ? "TC010" : ""; }
                    if (TipoDoc == "FAC") { if (tipoDoc == "B") { eNotaCred.tipo_documento_NC = "TC001"; } }
                    eNotaCred.serie_documento_NC = sDocumento.Substring(0, 4);
                    eNotaCred.numero_documento_NC = Convert.ToInt32(sDocumento.Substring(5, (sDocumento.Length - 5)));
                    eNotaCred.cod_proveedor = eDocRef.cod_proveedor;
                    eNotaCred.tipo_documento = eDocRef.tipo_documento; eNotaCred.serie_documento = eDocRef.serie_documento;
                    eNotaCred.numero_documento = eDocRef.numero_documento; eNotaCred.cod_proveedor = eDocRef.cod_proveedor;
                    eNotaCred.dsc_documento = sTipoDocRef + "-" + String.Format("{0:" + fmt_nro_doc + "}", Convert.ToInt32(sNumeroDocRef));
                    eNotaCred.dsc_tipo_documento = eDocRef.dsc_tipo_documento;
                    eNotaCred.dsc_glosa = eDocRef.dsc_glosa; eNotaCred.cod_moneda = eDocRef.cod_moneda; eNotaCred.fch_documento = eDocRef.fch_documento;
                    eNotaCred.imp_tipocambio = eDocRef.imp_tipocambio; eNotaCred.imp_total = eDocRef.imp_total; eNotaCred.imp_subtotal = eDocRef.imp_subtotal;
                    eNotaCred.imp_igv = eDocRef.imp_igv; eNotaCred.imp_saldo = eDocRef.imp_saldo;
                    listDocumentosNC.Clear(); listDocumentosNC.Add(eNotaCred);
                    bsDocumentosVinculados.DataSource = listDocumentosNC; gvFacturasProveedor.RefreshData();
                    ///////////////////////////////////////////////////////////////////////
                }
                if (swEsRH == true) { glkpTipoDocumento.EditValue = "TC008"; } else { glkpTipoDocumento.EditValue = TipoDoc == "FAC" ? "TC002" : TipoDoc == "NC" ? "TC006" : TipoDoc == "ND" ? "TC010" : ""; }
                if (TipoDoc == "FAC") { if (tipoDoc == "B") { glkpTipoDocumento.EditValue = "TC001"; } }
                txtSerieDocumento.Text = sDocumento.Substring(0, 4);
                //txtNumeroDocumento.Text = $"{Convert.ToInt32(sDocumento.Substring(5, (sDocumento.Length - 5))):00000000}"; //sDocumento.Substring(5, (sDocumento.Length - 5));
                txtNumeroDocumento.Text = String.Format("{0:" + fmt_nro_doc + "}", Convert.ToInt32(sDocumento.Substring(5, (sDocumento.Length - 5)))); //sDocumento.Substring(5, (sDocumento.Length - 5));
                dtFechaDocumento.DateTime = Convert.ToDateTime(sFechaDoc);
                dtFechaRegistro.EditValue = DateTime.Today;
                if (swEsRH == false) { TipoMoneda = sPayableAmount.Substring(12, 3); } else { TipoMoneda = sTaxableAmount.Substring(12, 3); }
                lkpTipoMoneda.EditValue = TipoMoneda == "PEN" ? "SOL" : "DOL";
                if (swEsRH == false) { txtMontoTotal.Text = sMonto; } else { txtMontoTotal.Text = sMonto; }
                if (TipoDoc == "NC") { txtMontoTotal.Text = Convert.ToDecimal(sMonto) < 0 ? Convert.ToDecimal(sMonto).ToString() : (0 - Convert.ToDecimal(sMonto)).ToString(); }
                txtGlosaFactura.Text = sDescripcion;

                if (eProv == null)
                {
                    string sRazonSocial = FExtraerValorXML(sParty, "cbc:RegistrationName", ForzarProveedor: true);
                    string sNombreComercial = FExtraerValorXML(sParty, "cbc:Name", ForzarProveedor: true);
                    sRazonSocial = sRazonSocial == "" ? sNombreComercial : sRazonSocial;
                    string sDireccion = FExtraerValorXML(sParty, "cbc:Line", ForzarProveedor: true);
                    sDireccion = sDireccion == "" ? FExtraerValorXML(sParty, "cbc:StreetName", ForzarProveedor: true) : sDireccion;
                    string sDepartamento = FExtraerValorXML(sParty, "cbc:CountrySubentity", ForzarProveedor: true);
                    string sProvincia = FExtraerValorXML(sParty, "cbc:CityName", ForzarProveedor: true);
                    string sDistrito = FExtraerValorXML(sParty, "cbc:District", ForzarProveedor: true);
                    string sRUCProveedor = swEsRH == false && sRUCSignature2.Length > 11 ? sRUCSignature.Trim() :
                                           sRUCSignature2.Length == 11 ? sRUCSignature2.Trim() : sRUCAssignaedRH.Trim();
                    eProv = new eProveedor();
                    eProv.dsc_proveedor = sRazonSocial; eProv.dsc_razon_social = sRazonSocial; eProv.dsc_razon_comercial = sNombreComercial;
                    eProv.dsc_direccion = sDireccion; eProv.flg_activo = "SI"; eProv.cod_tipo_documento = "DI004"; eProv.num_documento = sRUCProveedor;
                    eProv.flg_venta_consignacion = "RE"; eProv.cod_tipo_proveedor = "NAC"; eProv.flg_agente_retencion = "NO";
                    eProv.flg_agente_percepcion = "NO"; eProv.flg_buen_contribuyente = "NO"; eProv.flg_auto_detraccion = "NO";
                    eProv.flg_codigo_autogenerado = "NO"; eProv.flg_juridico = "SI"; eProv.flg_domiciliado = "NO";
                    eProv.cod_modalidad_pago = "MP001"; eProv.flg_afecto_cuarta = "NO"; eProv.flg_no_habido = "NO";
                    eProv.cod_convenio_trib = "001"; eProv.cod_usuario_registro = user.cod_usuario; eProv.cod_frecuencia = "EVEN";
                    eProv.cod_formapago = "TRANBAN";

                    List<eCiudades> listDepartamento = new List<eCiudades>();
                    List<eCiudades> listProvincia = new List<eCiudades>();
                    List<eCiudades> listDistrito = new List<eCiudades>();
                    eProv.cod_pais = "00001";
                    listDepartamento = blCli.ListarOpcionesVariasCliente<eCiudades>(14, cod_condicion: eProv.cod_pais);
                    eCiudades objDepart = listDepartamento.Find(x => x.dsc_departamento.ToUpper() == sDepartamento.ToUpper());
                    if (objDepart != null)
                    {
                        eProv.cod_departamento = objDepart.cod_departamento;
                        listProvincia = blCli.ListarOpcionesVariasCliente<eCiudades>(15, cod_condicion: objDepart.cod_departamento);
                        eCiudades objProv = listProvincia.Find(x => x.dsc_provincia.ToUpper() == sProvincia);
                        if (objProv != null)
                        {
                            eProv.cod_provincia = objProv.cod_provincia;
                            listDistrito = blCli.ListarOpcionesVariasCliente<eCiudades>(16, cod_condicion: objProv.cod_provincia);
                            eCiudades objDist = listDistrito.Find(x => x.dsc_distrito.ToUpper() == sDistrito);
                            if (objDist != null) eProv.cod_distrito = objDist.cod_distrito;
                        }
                    }
                    else
                    {
                        eProv.cod_departamento = "00015"; eProv.cod_provincia = "00128"; eProv.cod_distrito = "01251";
                    }

                    eProveedor NewProv = blProv.Guardar_Actualizar_Proveedor<eProveedor>(eProv, "Nuevo");

                    if (NewProv == null)
                    {
                        MessageBox.Show("Error al crear proveedor", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    eProv.cod_proveedor = NewProv.cod_proveedor;

                    List<eProveedor_Empresas> listEmpresasUsuario = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(11, "", user.cod_usuario);
                    foreach (eProveedor_Empresas objEmp in listEmpresasUsuario)
                    {
                        //eProveedor_Empresas objEmp = new eProveedor_Empresas();
                        objEmp.cod_proveedor = eProv.cod_proveedor; objEmp.flg_activo = "SI"; objEmp.valorRating = 0; objEmp.cod_usuario_registro = user.cod_usuario;
                        eProveedor_Empresas eProvEmp = blProv.Guardar_Actualizar_ProveedorEmpresas<eProveedor_Empresas>(objEmp);
                        if (eProvEmp == null) { MessageBox.Show("Error al vincular empresa", "Vincular empresa", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    }

                    MessageBox.Show("Se ha procedido a crear el proveedor ya que no existía en la base de datos." + Environment.NewLine + 
                                    "Por favor proceder a vincular los tipos de servicios.", "Nuevo Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frmMantProveedor frm = new frmMantProveedor();
                    frm.cod_proveedor = eProv.cod_proveedor;
                    frm.MiAccion = Proveedor.Editar;
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorEventRow = colorEventRow;
                    frm.colorFocus = colorFocus;
                    frm.user = user;
                    frm.ShowDialog();
                    //MessageBox.Show("No existe proveedor", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }
                //ObtenerProveedor
                lkpModalidadPago.EditValue = eProv.cod_modalidad_pago;
                txtRucProveedor.Text = eProv.num_documento;
                txtProveedor.Tag = eProv.cod_proveedor;
                txtProveedor.Text = eProv.dsc_proveedor;

                blFact.CargaCombosLookUp("EmpresaProveedor", lkpEmpresaProveedor, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_proveedor: eProv.num_documento);
                if (eEmp != null) lkpEmpresaProveedor.EditValue = eEmp.cod_empresa;
                List<eFacturaProveedor> list = blFact.Obtener_MaestrosGenerales<eFacturaProveedor>(25, "", txtProveedor.Tag.ToString());
                lkpTipoServicioProveedor.Properties.DataSource = list;
                if (list.Count == 1) lkpTipoServicioProveedor.EditValue = list[0].cod_tipo_servicio;

                //dtFechaContable.EditValue = DateTime.Now;
                //if (sFechaVencimiento == "") { dtFechaVencimiento.EditValue = dtFechaRegistro.EditValue; } else { dtFechaVencimiento.DateTime = Convert.ToDateTime(sFechaVencimiento); }

                //if (sFechaVencimiento == "")
                //{
                    ////dtFechaVencimiento.EditValue = dtFechaRegistro.EditValue;
                DateTime FechaVencimiento;
                FechaVencimiento = Convert.ToDateTime(dtFechaRegistro.EditValue);
                dtFechaVencimiento.EditValue = FechaVencimiento.AddDays(eProv.num_dias);
                //}
                //else
                //{
                //    dtFechaVencimiento.EditValue = Convert.ToDateTime(sFechaVencimiento);
                //}
                txtOrigenDocumento.Text = "XML";
            }
            catch (Exception ex)
            {
                txtOrigenDocumento.Text = "MANUAL";
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private string FExtraerValorXML(string sAuxiliar, string sCampo, int nPosFin = 0, bool swAperturaSinCierre = false, bool ForzarRUCEmpresa = false, bool ForzarProveedor = false, bool sNotaCredito = false)
        {
            try
            {
                string FExtraeValorXML = "";
                sAuxiliar = sAuxiliar.Replace("<![CDATA[", "").Replace("]]>", "");
                string sAuxiliarA = "<" + sCampo + ">";
                string sAuxiliarB = "</" + sCampo + ">";

                if (swAperturaSinCierre == true) { sAuxiliarA = "<" + sCampo + " "; }
                if (nPosFin == 0) { nPosFin = sAuxiliar.Length; }

                string sAuxiliarC = "";
                int nPosAuxC = 0;
                int nLargoAux = 0;

                int nPosAuxA = sAuxiliar.IndexOf(sAuxiliarA);
                int nPosAuxB = sAuxiliar.IndexOf(sAuxiliarB);

                //if (nPosAuxA <= 0 && nPosAuxB > 0) // PARA RECIBOS POR HONORARIOS
                //{
                //    sAuxiliarA = sAuxiliar.Substring(4, nPosAuxB - 4);
                //    nPosAuxA = sAuxiliarA.IndexOf(">");
                //    sAuxiliarA = sAuxiliarA.Substring(0, nPosAuxA + 1);
                //    nPosAuxA = sAuxiliar.IndexOf(sAuxiliarA);
                //}

                ////////////// PARA FORZAR Y TRAER EL RUC DE LA EMPRESA A FACTURAR///////////////////////////////
                if (ForzarRUCEmpresa) nPosAuxA = nPosAuxB - 19;
                ////////////////////////////////////////////
                ////////////// PARA FORZAR Y TRAER DATOS DEL PROVEEDOR PARA CREAR///////////////////////////////
                if (ForzarProveedor)
                {
                    if (nPosAuxA < 0) { return ""; }
                    sAuxiliarC = sAuxiliar.Substring(nPosAuxA + sAuxiliarA.Length, nPosAuxB - (nPosAuxA + sAuxiliarA.Length));
                    nPosAuxA = sAuxiliarC.Length >= 9 && sAuxiliarC.Substring(0, 9) == "<![CDATA[" ? nPosAuxA + 9 : nPosAuxA;
                    nPosAuxB = sAuxiliarC.Length >= 9 && sAuxiliarC.Substring(0, 9) == "<![CDATA[" ? nPosAuxB - 3 : nPosAuxB;
                }
                ///////////////////////////////////////////////////////////////////////////
                ////////////////////////PARA NOTA DE CREDITO///////////////////////////////
                if (sNotaCredito) nPosAuxA = nPosAuxB - sAuxiliarB.Length - 1;
                //////////////////////////////////////////////////////////////////////////
                if (nPosAuxA < 0) { return ""; }
                if (nPosAuxA == 0 && nPosAuxB == 0) { return ""; }

                if (nPosAuxA <= nPosFin && nPosAuxB <= nPosFin)
                {
                    nPosAuxC = nPosAuxA + sAuxiliarA.Length;
                    nLargoAux = nPosAuxB - nPosAuxC;
                    if (nLargoAux < 0) { return ""; }
                    sAuxiliarC = sAuxiliar.Substring(nPosAuxC, nLargoAux);

                    FExtraeValorXML = sAuxiliarC.Trim();
                }
                return sAuxiliarC.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return "";
            }
        }

        private void CargarListadoDetalle()
        {
            if (txtOrdenCompraServicio != null) {
                eFacturaProveedor fac = new eFacturaProveedor();
                fac.num_OrdenCompraServ = txtOrdenCompraServicio.Text.ToString();
                List<eFacturaProveedor.eFacturaProvedor_Detalle> ListadoOrdenesAsignados = new List<eFacturaProveedor.eFacturaProvedor_Detalle>();
                ListadoOrdenesAsignados = blFact.ListarDetalleFactura<eFacturaProveedor.eFacturaProvedor_Detalle>(5, fac.num_OrdenCompraServ);
                bsDetalleFactura.DataSource = null; bsDetalleFactura.DataSource = ListadoOrdenesAsignados;
            }
        }
        //CECO
        public void CargarCecosAsignados()
        {
            List<eFacturaProveedor> ListarCeco = new List<eFacturaProveedor>();
            ListarCeco = blFact.ObtenerListado_CECO<eFacturaProveedor>(1, cod_empresa, cod_cliente);
            bsListadoCECODisponibles.DataSource = null; bsListadoCECODisponibles.DataSource = ListarCeco;

        }

      
    }
}