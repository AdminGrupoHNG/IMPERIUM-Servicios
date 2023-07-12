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
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using System.Net;
using System.IO;
using System.Globalization;
using DevExpress.XtraSplashScreen;
using Newtonsoft.Json;
using DevExpress.XtraReports.UI;
using Tesseract;
using System.Web;
using System.Web.Script.Serialization;
//using OpenQA.Selenium;
//using HtmlAgilityPack;
using Keys = System.Windows.Forms.Keys;
using System.Net.Http.Headers;
using System.Net.Http;
using RestSharp;

namespace UI_Servicios.Formularios.Clientes_Y_Proveedores.Proveedores
{
    internal enum Proveedor
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmMantProveedor : DevExpress.XtraEditors.XtraForm
    {
        frmListadoProveedores frmHandler;
        public eUsuario user = new eUsuario();
        internal Proveedor MiAccion = Proveedor.Nuevo;
        blProveedores blProv = new blProveedores();
        blClientes blCli = new blClientes();
        blUsuario blUser = new blUsuario();
        blVersion blVers = new blVersion();
        blGlobales blGlobal = new blGlobales();
        List<eProveedor_CuentasBancarias> ListCuentasBancarias = new List<eProveedor_CuentasBancarias>();
        List<eProveedor_Contactos> ListContactosProveedor = new List<eProveedor_Contactos>();
        List<eProveedor_Tecnicos> ListTecnicosProveedor = new List<eProveedor_Tecnicos>();
        List<eProveedor_Empresas> ListEmpresasProveedor = new List<eProveedor_Empresas>();
        List<eProveedor_Servicios> ListServiciosProveedor = new List<eProveedor_Servicios>();
        List<eProveedor_Marca> ListMarcasProveedor = new List<eProveedor_Marca>();
        public string cod_proveedor = "", cod_empresa = "";
        public string ActualizarListado = "NO";
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;

        public frmMantProveedor()
        {
            InitializeComponent();
        }
        public frmMantProveedor(frmListadoProveedores frm)
        {
            InitializeComponent();
            frmHandler = frm;
        }

        private void frmMantProveedor_Load(object sender, EventArgs e)
        {
            groupControl1.AppearanceCaption.ForeColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
            groupControl2.AppearanceCaption.ForeColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
            groupControl3.AppearanceCaption.ForeColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
            groupControl4.AppearanceCaption.ForeColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
            Inicializar();
            lkpPais.EditValue = "00001";
            txtNroDocumento.Select();
            HabilitarBotones();
        }

        private void HabilitarBotones()
        {
            blSistema blSist = new blSistema();
            List<eVentana> listPermisos = blSist.ListarMenuxUsuario<eVentana>(user.cod_usuario, frmHandler != null ? frmHandler.Name : "");

            if (listPermisos.Count > 0)
            {
                if (listPermisos[0].flg_escritura == false) BloqueoControles(false, true, false);
            }
        }

        private void Inicializar()
        {
            switch (MiAccion)
            {
                case Proveedor.Nuevo:
                    CargarCombos();
                    Nuevo();
                    break;
                case Proveedor.Editar:
                    CargarCombos();
                    Editar();
                    break;
                case Proveedor.Vista:
                    CargarCombos();
                    Editar();
                    BloqueoControles(false, true, false);
                    gvEmpresasVinculadas.OptionsBehavior.Editable = true;
                    break;
            }
        }
        private void Nuevo()
        {
            chkFlgJuridica_CheckStateChanged(chkFlgJuridica, new EventArgs());
            //xtraTabControl1.Enabled = false;
            xtabCuentasBancarias.PageEnabled = false;
            xtabContactos.PageEnabled = false;
            xtabTecnicos.PageEnabled = false;
            xtabEmpresasVinculadas.PageEnabled = false;
            xtabServicios.PageEnabled = false;
            xtabMarcas.PageEnabled = false;
            txtUsuarioRegistro.Text = user.dsc_usuario;
            txtUsuarioCambio.Text = user.dsc_usuario;
            dtFechaRegistro.EditValue = DateTime.Today;
            dtFechaModificacion.EditValue = DateTime.Today;
            btnConsultarSunat.Enabled = true;
            btnFichaProveedor.Enabled = false;
            //lkpFrecuencia.EditValue = "NAC";
        }
        private void Editar()
        {
            eProveedor eProv = new eProveedor();
            eProv = blProv.ObtenerProveedor<eProveedor>(2, cod_proveedor);
            txtCodProveedor.Text = eProv.cod_proveedor;
            chkFlgJuridica.CheckState = eProv.flg_juridico == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkFlgJuridica_CheckStateChanged(chkFlgJuridica, new EventArgs());
            chkActivoProveedor.CheckState = eProv.flg_activo == "SI" ? CheckState.Checked : CheckState.Unchecked;
            glkpTipoDocumento.EditValue = eProv.cod_tipo_documento;
            txtNroDocumento.Text = eProv.num_documento;
            lkpFrecuencia.EditValue = eProv.cod_frecuencia;
            lkpFormaPago.EditValue = eProv.cod_formapago;
            txtApellPaterno.Text = eProv.dsc_apellido_paterno;
            txtApellMaterno.Text = eProv.dsc_apellido_materno;
            txtNombre.Text = eProv.dsc_nombres;
            txtRazonSocial.Text = eProv.dsc_razon_social;
            txtNombreComercial.Text = eProv.dsc_razon_comercial;
            txtDireccionProveedor.Text = eProv.dsc_direccion;
            txtRepresentanteLegal.Text = eProv.dsc_representante_legal;
            //txtContacto2Proveedor.Text = eProv.dsc_contacto2;
            lkpPais.EditValue = eProv.cod_pais;
            lkpDepartamento.EditValue = eProv.cod_departamento;
            lkpProvincia.EditValue = eProv.cod_provincia;
            glkpDistrito.EditValue = eProv.cod_distrito;
            txtEmail1Proveedor.Text = eProv.dsc_mail_1;
            txtEmail2Proveedor.Text = eProv.dsc_mail_2;
            //glkpConvenioTributacion.EditValue = eProv.cod_convenio_trib;
            glkpModalidadPago.EditValue = eProv.cod_modalidad_pago;
            txtFono1Proveedor.Text = eProv.dsc_fono_1;
            txtFono2Proveedor.Text = eProv.dsc_fono_2;
            txtUsuarioRegistro.Text = eProv.dsc_usuario_registro;
            txtUsuarioCambio.Text = eProv.dsc_usuario_cambio;
            dtFechaRegistro.EditValue = eProv.fch_registro;
            dtFechaModificacion.EditValue = eProv.fch_cambio;
            mmObservacionesProveedor.Text = eProv.Observaciones;
            txtCodigoERP.Text = eProv.cod_proveedor_ERP;
            txtLicenciaConducir.Text = eProv.dsc_licenciaconducir;
            txtNroAutorizMTC.Text = eProv.dsc_nroautorizMTC;
            txtMarcaVehiculo.Text = eProv.dsc_marcavehiculo;
            txtPlacaVehiculo.Text = eProv.dsc_placavehiculo;
            
            chkflgTransportista.CheckState = eProv.flg_transportista == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkAgenteRetencion.CheckState = eProv.flg_agente_retencion == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkAgentePercepcion.CheckState = eProv.flg_agente_percepcion == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkBuenContribuyente.CheckState = eProv.flg_buen_contribuyente == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkAutoDetraccion.CheckState = eProv.flg_auto_detraccion == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkDomiciliado.CheckState = eProv.flg_buen_contribuyente == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkAfectoCuarta.CheckState = eProv.flg_afecto_cuarta == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkNoHabido.CheckState = eProv.flg_no_habido == "SI" ? CheckState.Checked : CheckState.Unchecked;
            if (eProv.fch_no_habido.ToString().Contains("1/01/0001")) { dtNoHabido.EditValue = null; } else { dtNoHabido.EditValue = Convert.ToDateTime(eProv.fch_no_habido); }

            ObtenerListadoCuentasBancarias();
            ObtenerListadoContactosProveedor();
            ObtenerListadoEmpresasProveedor();
            ObtenerListadoServiciosProveedor();
            ObtenerDatos_MarcasProveedor();
            //ObtenerListadoTecnicosProveedor();
            txtNroDocumento.ReadOnly = true;

            List<eProveedor_Empresas> listEmpresasUsuario = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(11, "", user.cod_usuario);
            List<eProveedor_Empresas> listEmpresas = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(7, cod_proveedor);
            if (listEmpresas.Count > 0)
            {
                eProveedor_Empresas objEmp = new eProveedor_Empresas();
                int validar = 0;
                foreach (eProveedor_Empresas obj in listEmpresasUsuario)
                {
                    objEmp = listEmpresas.Find(x => x.cod_empresa == obj.cod_empresa);
                    validar = validar > 0 ? validar : objEmp != null ? 1 : 0;
                }

                if (validar == 0) BloqueoControles(false, true, false);
                if (validar == 1) BloqueoControles(true, false, true);
            }
            gvEmpresasVinculadas.OptionsBehavior.Editable = true;
        }

        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            btnGuardar.Enabled = Enabled;
            btnNuevo.Enabled = Enabled;
            btnConsultarSunat.Enabled = Enabled;
            chkFlgJuridica.ReadOnly = ReadOnly;
            chkCodigoManual.ReadOnly = ReadOnly;
            chkActivoProveedor.ReadOnly = ReadOnly;
            glkpTipoDocumento.ReadOnly = ReadOnly;
            txtNroDocumento.ReadOnly = ReadOnly;
            txtCodigoERP.ReadOnly = ReadOnly;
            txtApellPaterno.ReadOnly = ReadOnly;
            txtApellMaterno.ReadOnly = ReadOnly;
            txtNombre.ReadOnly = ReadOnly;
            txtRazonSocial.ReadOnly = ReadOnly;
            txtNombreComercial.ReadOnly = ReadOnly;
            txtDireccionProveedor.ReadOnly = ReadOnly;
            lkpPais.ReadOnly = ReadOnly;
            lkpDepartamento.ReadOnly = ReadOnly;
            lkpProvincia.ReadOnly = ReadOnly;
            glkpDistrito.ReadOnly = ReadOnly;
            txtEmail1Proveedor.ReadOnly = ReadOnly;
            txtEmail2Proveedor.ReadOnly = ReadOnly;
            txtFono1Proveedor.ReadOnly = ReadOnly;
            txtFono2Proveedor.ReadOnly = ReadOnly;
            lkpFrecuencia.ReadOnly = ReadOnly;
            lkpFormaPago.ReadOnly = ReadOnly;
            txtRepresentanteLegal.ReadOnly = ReadOnly;
            glkpModalidadPago.ReadOnly = ReadOnly;

            //Otros datos
            chkAgenteRetencion.ReadOnly = ReadOnly;
            chkAgentePercepcion.ReadOnly = ReadOnly;
            chkBuenContribuyente.ReadOnly = ReadOnly;
            chkAutoDetraccion.ReadOnly = ReadOnly;
            chkDomiciliado.ReadOnly = ReadOnly;
            chkAfectoCuarta.ReadOnly = ReadOnly;
            chkNoHabido.ReadOnly = ReadOnly;
            dtNoHabido.Enabled = Enabled;
            mmObservacionesProveedor.ReadOnly = ReadOnly;

            //Cuentas Bancarias
            btnNuevoCuentaBancaria.Enabled = Enabled;
            btnGuardarCuentaBancaria.Enabled = Enabled;
            btnEliminarCuentaBancaria.Enabled = Enabled;
            glkpBancoCuentaBancaria.ReadOnly = ReadOnly;
            lkpMonedaCuentaBancaria.ReadOnly = ReadOnly;
            lkpTipoCuentaBancaria.ReadOnly = ReadOnly;
            txtNroCuentaBancaria.ReadOnly = ReadOnly;
            txtNroCuentaInterbancaria.ReadOnly = ReadOnly;
            mmObservacionCuentaBancaria.ReadOnly = ReadOnly;
            txtTitularCuentasBancarias.ReadOnly = ReadOnly;
            chkPagoTransferenciaCuentaBancaria.ReadOnly = ReadOnly;
            gvListadoCuentasBancarias.OptionsBehavior.Editable = Editable;

            //Contactos
            btnNuevoContacto.Enabled = Enabled;
            btnGuardarContacto.Enabled = Enabled;
            btnEliminarContacto.Enabled = Enabled;
            txtNombreContacto.ReadOnly = ReadOnly;
            txtApellidoContacto.ReadOnly = ReadOnly;
            dtFecNacContacto.Enabled = Enabled;
            txtCorreoContacto.ReadOnly = ReadOnly;
            txtFono1Contacto.ReadOnly = ReadOnly;
            txtFono2Contacto.ReadOnly = ReadOnly;
            txtCargoContacto.ReadOnly = ReadOnly;
            mmObservacionContacto.ReadOnly = ReadOnly;
            chkcbEmpresaContacto.ReadOnly = ReadOnly;
            gvListadoContactos.OptionsBehavior.Editable = Editable;

            //Empresas Vinculadas
            gvEmpresasVinculadas.OptionsBehavior.Editable = Editable;

            //Servicios Vinculados
            gvServiciosProveedor.OptionsBehavior.Editable = Editable;
        }

        private void CargarCombos()
        {
            CargarCombosGridLookup("TipoDocumento", glkpTipoDocumento, "cod_tipo_documento", "dsc_tipo_documento", "", valorDefecto: true);
            CargarCombosGridLookup("TipoDistrito", glkpDistrito, "cod_distrito", "dsc_distrito", "");
            //CargarCombosGridLookup("ConvenioTributacion", glkpConvenioTributacion, "cod_convenio", "dsc_convenio", "", valorDefecto: true);
            CargarCombosGridLookup("ModalidadPago", glkpModalidadPago, "cod_modalidad_pago", "dsc_modalidad_pago", "", valorDefecto: true);
            CargarCombosGridLookup("Banco", glkpBancoCuentaBancaria, "cod_banco", "dsc_banco", "");

            blCli.CargaCombosLookUp("TipoPais", lkpPais, "cod_pais", "dsc_pais", "");
            blCli.CargaCombosLookUp("TipoDepartamento", lkpDepartamento, "cod_departamento", "dsc_departamento", "");
            blCli.CargaCombosLookUp("TipoProvincia", lkpProvincia, "cod_provincia", "dsc_provincia", "");
            blProv.CargaCombosLookUp("Moneda", lkpMonedaCuentaBancaria, "cod_moneda", "dsc_moneda", "", valorDefecto: true);
            blProv.CargaCombosLookUp("TipoCuentaBancaria", lkpTipoCuentaBancaria, "cod_tipo_cuenta", "dsc_tipo_cuenta", "");
            blProv.CargaCombosLookUp("Frecuencia", lkpFrecuencia, "cod_frecuencia", "dsc_frecuencia", "", valorDefecto: true);
            blProv.CargaCombosLookUp("FormaPago", lkpFormaPago, "cod_formapago", "dsc_formapago", "", valorDefecto: true);

            blProv.CargaCombosChecked("EmpresasContacto", chkcbEmpresaContacto, "cod_empresa", "dsc_empresa", "");

            if (MiAccion == Proveedor.Nuevo)
            {
                picAnteriorProveedor.Enabled = false; picSiguienteProveedor.Enabled = false; btnNuevo.Enabled = false;
            }
        }
        private void CargarCombosGridLookup(string nCombo, GridLookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", string cod_condicion = "", bool valorDefecto = false)
        {
            DataTable tabla = new DataTable();
            tabla = blProv.ObtenerListadoGridLookup(nCombo, cod_condicion);

            combo.Properties.DataSource = tabla;
            combo.Properties.ValueMember = campoValueMember;
            combo.Properties.DisplayMember = campoDispleyMember;
            if (campoSelectedValue == "") { combo.EditValue = null; } else { combo.EditValue = campoSelectedValue; }
            if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
        }

        private void ObtenerListadoCuentasBancarias()
        {
            ListCuentasBancarias = blProv.ListarCuentasBancariasProveedor<eProveedor_CuentasBancarias>(3, cod_proveedor);
            bsListadoCuentasBancarias.DataSource = null; bsListadoCuentasBancarias.DataSource = ListCuentasBancarias;
            gvListadoCuentasBancarias.RefreshData();
        }

        private void ObtenerListadoContactosProveedor()
        {
            ListContactosProveedor = blProv.ListarContactosProveedor<eProveedor_Contactos>(5, cod_proveedor);
            bsListadoContactos.DataSource = null; bsListadoContactos.DataSource = ListContactosProveedor;
            gvListadoContactos.RefreshData();
        }

        private void ObtenerListadoEmpresasProveedor()
        {
            ListEmpresasProveedor = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(8, cod_proveedor, user.cod_usuario);
            bsEmpresasProveedor.DataSource = null; bsEmpresasProveedor.DataSource = ListEmpresasProveedor;

            if (MiAccion != Proveedor.Nuevo)
            {
                List<eProveedor_Empresas> lista = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(7, cod_proveedor);
                foreach (eProveedor_Empresas obj in lista)
                {
                    eProveedor_Empresas oProvEmp = ListEmpresasProveedor.Find(x => x.cod_empresa == obj.cod_empresa);
                    if (oProvEmp != null) { oProvEmp.Seleccionado = true; oProvEmp.valorRating = obj.valorRating; }
                }
            }

            gvEmpresasVinculadas.RefreshData();
        }

        private void ObtenerListadoServiciosProveedor()
        {
            ListServiciosProveedor = blProv.ListarServiciosProveedor<eProveedor_Servicios>(10, cod_proveedor);
            bsServiciosProveedor.DataSource = null; bsServiciosProveedor.DataSource = ListServiciosProveedor;

            if (MiAccion != Proveedor.Nuevo)
            {
                List<eProveedor_Servicios> lista = blProv.ListarServiciosProveedor<eProveedor_Servicios>(9, cod_proveedor);
                foreach (eProveedor_Servicios obj in lista)
                {
                    eProveedor_Servicios oProvEmp = ListServiciosProveedor.Find(x => x.cod_tipo_servicio == obj.cod_tipo_servicio);
                    if (oProvEmp != null) oProvEmp.Seleccionado = true;
                }
            }

            gvServiciosProveedor.RefreshData();
        }

        private void ObtenerListadoTecnicosProveedor()
        {
            //ListTecnicosProveedor = blProv.ListarTecnicosProveedor<eProveedor_Tecnicos>(7, cod_proveedor);
            //bsListadoTecnicos.DataSource = null; bsListadoTecnicos.DataSource = ListTecnicosProveedor;
            //gvListadoTecnicos.RefreshData();
        }

        private void frmMantProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
        }

        private void chkFlgJuridica_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkFlgJuridica.CheckState == CheckState.Checked)
            {
                txtNombre.Text = ""; txtApellPaterno.Text = ""; txtApellMaterno.Text = "";
            }
            else
            {
                txtRazonSocial.Text = "";
            }
            glkpTipoDocumento.EditValue = chkFlgJuridica.CheckState == CheckState.Checked ? "DI004" : "DI001";
            txtRazonSocial.Enabled = chkFlgJuridica.CheckState == CheckState.Checked ? true : false;
            txtApellPaterno.Enabled = chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            txtApellMaterno.Enabled = chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            txtNombre.Enabled = chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
        }

        private void picAnteriorProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.gvListaProveedores.RowCount - 1;
                int nRow = frmHandler.gvListaProveedores.FocusedRowHandle;
                frmHandler.gvListaProveedores.FocusedRowHandle = nRow == 0 ? tRow : nRow - 1;

                eProveedor obj = frmHandler.gvListaProveedores.GetFocusedRow() as eProveedor;
                cod_proveedor = obj.cod_proveedor;
                MiAccion = Proveedor.Editar;
                Editar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picSiguienteProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.gvListaProveedores.RowCount - 1;
                int nRow = frmHandler.gvListaProveedores.FocusedRowHandle;
                frmHandler.gvListaProveedores.FocusedRowHandle = nRow == tRow ? 0 : nRow + 1;

                eProveedor obj = frmHandler.gvListaProveedores.GetFocusedRow() as eProveedor;
                cod_proveedor = obj.cod_proveedor;
                MiAccion = Proveedor.Editar;
                Editar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MiAccion = Proveedor.Nuevo;
            Nuevo();
            LimpiarCamposProveedor();
            CargarCombos();
            lkpPais.EditValue = "00001";
            txtNroDocumento.ReadOnly = false;
            txtNroDocumento.Select();
        }

        private void LimpiarCamposProveedor()
        {
            txtCodProveedor.Text = "";
            chkFlgJuridica.CheckState = CheckState.Unchecked;
            chkCodigoManual.CheckState = CheckState.Unchecked;
            chkActivoProveedor.CheckState = CheckState.Checked;
            glkpTipoDocumento.EditValue = null;
            txtNroDocumento.Text = "";
            lkpFrecuencia.EditValue = null;
            lkpFormaPago.EditValue = null;
            txtApellPaterno.Text = "";
            txtApellMaterno.Text = "";
            txtNombre.Text = "";
            txtRazonSocial.Text = "";
            txtNombreComercial.Text = "";
            txtDireccionProveedor.Text = "";
            txtRepresentanteLegal.Text = "";
            //txtContacto2Proveedor.Text = "";
            //lkpPais.EditValue = null;
            lkpDepartamento.EditValue = null;
            lkpProvincia.EditValue = null;
            glkpDistrito.EditValue = null;
            txtEmail1Proveedor.Text = "";
            txtEmail2Proveedor.Text = "";
            //glkpConvenioTributacion.EditValue = null;
            glkpModalidadPago.EditValue = null;
            txtFono1Proveedor.Text = "";
            txtFono2Proveedor.Text = "";
            mmObservacionesProveedor.Text = "";
            txtCodigoERP.Text = "";
            if (MiAccion != Proveedor.Editar)
            {
                picAnteriorProveedor.Enabled = false; picSiguienteProveedor.Enabled = false;
            }

            bsListadoCuentasBancarias.DataSource = null;
            bsListadoContactos.DataSource = null;
            bsListadoTecnicos.DataSource = null;
            LimpiarCamposConfiguracionSunat();
            LimpiarCamposCuentasBancarias();
            LimpiarCamposContactosProveedor();
            LimpiarCamposTecnicosProveedor();
            LimpiarCamposEmpresaProveedor();
            LimpiarCamposServiciosProveedor();

            btnNuevo.Enabled = false;
        }

        private void LimpiarCamposConfiguracionSunat()
        {
            chkAgenteRetencion.CheckState = CheckState.Unchecked;
            chkAgentePercepcion.CheckState = CheckState.Unchecked;
            chkBuenContribuyente.CheckState = CheckState.Unchecked;
            chkAutoDetraccion.CheckState = CheckState.Unchecked;
            chkDomiciliado.CheckState = CheckState.Unchecked;
            chkAfectoCuarta.CheckState = CheckState.Unchecked;
            chkNoHabido.CheckState = CheckState.Unchecked;
            dtNoHabido.EditValue = null;
        }

        private void LimpiarCamposCuentasBancarias()
        {
            txtCodCuentaBancaria.Text = "0";
            glkpBancoCuentaBancaria.EditValue = null;
            lkpMonedaCuentaBancaria.EditValue = null;
            lkpTipoCuentaBancaria.EditValue = null;
            txtNroCuentaBancaria.Text = "";
            txtNroCuentaInterbancaria.Text = "";
            mmObservacionCuentaBancaria.Text = "";
            chkPagoTransferenciaCuentaBancaria.CheckState = CheckState.Unchecked;
            txtTitularCuentasBancarias.Text = "";
            glkpBancoCuentaBancaria.Focus();
        }

        private void LimpiarCamposContactosProveedor()
        {
            txtCodContacto.Text = "0";
            txtNombreContacto.Text = "";
            txtApellidoContacto.Text = "";
            dtFecNacContacto.EditValue = null;
            txtCorreoContacto.Text = "";
            txtFono1Contacto.Text = "";
            txtFono2Contacto.Text = "";
            txtCargoContacto.Text = "";
            txtUsuarioWebContacto.Text = "";
            txtClaveWebContacto.Text = "";
            mmObservacionContacto.Text = "";
            chkcbEmpresaContacto.EditValue = null;
            txtNombreContacto.Focus();
        }

        private void LimpiarCamposTecnicosProveedor()
        {
            txtCodTecnico.Text = "0";
            txtNombreTecnico.Text = "";
            txtApellidoTecnico.Text = "";
            dtFecNacTecnico.EditValue = null;
            txtCorreoTecnico.Text = "";
            txtFono1Tecnico.Text = "";
            txtFono2Tecnico.Text = "";
            chkflgSupervisorTecnico.CheckState = CheckState.Unchecked;
            txtUsuarioWebTecnico.Text = "";
            txtClaveWebTecnico.Text = "";
            mmObservacionTecnico.Text = "";
            txtNombreTecnico.Focus();
        }

        private void LimpiarCamposEmpresaProveedor()
        {
            bsEmpresasProveedor.DataSource = null;
            ListEmpresasProveedor.Clear();
        }

        private void LimpiarCamposServiciosProveedor()
        {
            bsServiciosProveedor.DataSource = null;
            ListServiciosProveedor.Clear();
        }

        private string ValidarLongitudDocumento()
        {
            string result = "";
            int ctd = Convert.ToInt32(((System.Data.DataRowView)(glkpTipoDocumento.Properties.GetRowByKeyValue(glkpTipoDocumento.EditValue))).Row.ItemArray[4]);
            result = ctd == txtNroDocumento.Text.Trim().Length ? "OK" : ctd.ToString();

            return result;
        }
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (glkpTipoDocumento.EditValue != null && glkpTipoDocumento.EditValue.ToString() == "DI004" && txtNroDocumento.Text.Length != 11)
                {
                    MessageBox.Show("El RUC debe tener 11 digitos", "Validación RUC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNroDocumento.Select();
                    return;
                }
                if (glkpTipoDocumento.EditValue != null && glkpTipoDocumento.EditValue.ToString() == "DI001" && txtNroDocumento.Text.Length != 8)
                {
                    MessageBox.Show("El DNI debe tener 8 digitos", "Validación DNI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNroDocumento.Select();
                    return;
                }
                if (chkCodigoManual.CheckState == CheckState.Checked && txtCodProveedor.Text == "") { MessageBox.Show("Debe ingresar un código de proveedor", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCodProveedor.Focus(); return; }
                if (glkpTipoDocumento.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de documento", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpTipoDocumento.Focus(); return; }
                if (txtNroDocumento.Text.Trim() == "") { MessageBox.Show("Debe ingresar un número de documento", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNroDocumento.Focus(); return; }
                string NroDoc = ValidarLongitudDocumento();
                if (NroDoc != "OK") { MessageBox.Show("Longitud incorrecta, el " + glkpTipoDocumento.Text + " debe tener " + NroDoc + " dígitos.", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNroDocumento.Focus(); return; }
                if (lkpFrecuencia.EditValue == null) { MessageBox.Show("Debe seleccionar una frecuencia", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpFrecuencia.Focus(); return; }
                if (lkpFormaPago.EditValue == null) { MessageBox.Show("Debe seleccionar una forma de pago", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpFormaPago.Focus(); return; }
                if (txtApellPaterno.Text == "" && chkFlgJuridica.CheckState == CheckState.Unchecked) { MessageBox.Show("Debe ingresar un apellido paterno", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); txtApellPaterno.Focus(); return; }
                if (txtApellMaterno.Text == "" && chkFlgJuridica.CheckState == CheckState.Unchecked) { MessageBox.Show("Debe ingresar un apellido materno", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); txtApellMaterno.Focus(); return; }
                if (txtNombre.Text == "" && chkFlgJuridica.CheckState == CheckState.Unchecked) { MessageBox.Show("Debe ingresar un nombre", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNombre.Focus(); return; }
                if (txtRazonSocial.Text == "" && chkFlgJuridica.CheckState == CheckState.Checked) { MessageBox.Show("Debe ingresar la razón social", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); txtRazonSocial.Focus(); return; }
                if (txtDireccionProveedor.Text == "") { MessageBox.Show("Debe ingresar una dirección", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); txtDireccionProveedor.Focus(); return; }
                if (lkpPais.EditValue == null) { MessageBox.Show("Debe seleccionar un país", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpPais.Focus(); return; }
                if (lkpDepartamento.EditValue == null) { MessageBox.Show("Debe seleccionar un departamento", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpDepartamento.Focus(); return; }
                if (lkpProvincia.EditValue == null) { MessageBox.Show("Debe seleccionar una provincia", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpProvincia.Focus(); return; }
                if (glkpDistrito.EditValue == null) { MessageBox.Show("Debe seleccionar un distrito", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpDistrito.Focus(); return; }
                if (txtFono1Proveedor.Text == "") { MessageBox.Show("Debe ingresar un teléfono", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); txtFono1Proveedor.Focus(); return; }
                if (txtEmail1Proveedor.Text == "") { MessageBox.Show("Debe ingresar un correo", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); txtEmail1Proveedor.Focus(); return; }
                //if (glkpConvenioTributacion.EditValue == null) { MessageBox.Show("Debe seleccionar un convenio tributación", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpConvenioTributacion.Focus(); return; }
                if (glkpModalidadPago.EditValue == null) { MessageBox.Show("Debe seleccionar una modalidad de pago", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpModalidadPago.Focus(); return; }
                if (dtNoHabido.EditValue == null && chkNoHabido.CheckState == CheckState.Checked) { MessageBox.Show("Debe ingresar la fecha en la que adquirió la condicipon de No Habido", "Guardar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); dtNoHabido.Focus(); return; }

                string result = "";
                switch (MiAccion)
                {
                    case Proveedor.Nuevo: result = Guardar(); break;
                    case Proveedor.Editar: result = Modificar(); break;
                }

                if (result == "OK")
                {
                    MessageBox.Show("Se guardó el proveedor de manera satisfactoria", "Guardar proveedor", MessageBoxButtons.OK);
                    ActualizarListado = "SI";
                    if (frmHandler != null)
                    {
                        int nRow = frmHandler.gvListaProveedores.FocusedRowHandle;
                        frmHandler.frmListadoProveedores_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                        frmHandler.gvListaProveedores.FocusedRowHandle = nRow;
                        //frmHandler.CargarOpcionesMenu();
                    }

                    if (MiAccion == Proveedor.Nuevo)
                    {
                        MiAccion = Proveedor.Editar;
                        ObtenerListadoEmpresasProveedor();
                        ObtenerListadoServiciosProveedor();
                        //xtraTabControl1.Enabled = true;
                        xtabCuentasBancarias.PageEnabled = true;
                        xtabContactos.PageEnabled = true;
                        //xtabTecnicos.PageEnabled = true;
                        xtabEmpresasVinculadas.PageEnabled = true;
                        xtabServicios.PageEnabled = true;
                        btnConsultarSunat.Enabled = true;
                        btnFichaProveedor.Enabled = true;
                        xtabMarcas.PageEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private eProveedor AsignarValoresProveedor()
        {
            eProveedor eProv = new eProveedor();
            eProv.cod_proveedor = txtCodProveedor.Text;
            eProv.flg_juridico = chkFlgJuridica.CheckState == CheckState.Checked ? "SI" : "NO";
            eProv.flg_codigo_autogenerado = chkCodigoManual.CheckState == CheckState.Checked ? "SI" : "NO";
            eProv.flg_activo = chkActivoProveedor.CheckState == CheckState.Checked ? "SI" : "NO";
            eProv.cod_tipo_documento = glkpTipoDocumento.EditValue.ToString();
            eProv.num_documento = txtNroDocumento.Text;
            eProv.dsc_apellido_paterno = txtApellPaterno.Text;
            eProv.dsc_apellido_materno = txtApellMaterno.Text;
            eProv.dsc_nombres = txtNombre.Text;
            eProv.dsc_razon_social = txtRazonSocial.Text;
            eProv.dsc_razon_comercial = txtNombreComercial.Text;
            eProv.dsc_proveedor = chkFlgJuridica.CheckState == CheckState.Checked ? txtRazonSocial.Text : txtApellPaterno.Text + " " + txtApellMaterno.Text + " " + txtNombre.Text;
            eProv.dsc_direccion = txtDireccionProveedor.Text;
            //eProv.dsc_contacto1 = txtRepresentanteLegal.Text;
            //eProv.dsc_contacto2 = txtContacto2Proveedor.Text;
            eProv.cod_pais = lkpPais.EditValue.ToString();
            eProv.cod_departamento = lkpDepartamento.EditValue == null ? null : lkpDepartamento.EditValue.ToString();
            eProv.cod_provincia = lkpProvincia.EditValue == null ? null : lkpProvincia.EditValue.ToString();
            eProv.cod_distrito = glkpDistrito.EditValue == null ? null : glkpDistrito.EditValue.ToString();
            eProv.dsc_mail_1 = txtEmail1Proveedor.Text;
            eProv.dsc_mail_2 = txtEmail2Proveedor.Text;
            //eProv.cod_convenio_trib = glkpConvenioTributacion.EditValue.ToString();
            eProv.cod_modalidad_pago = glkpModalidadPago.EditValue.ToString();
            eProv.dsc_fono_1 = txtFono1Proveedor.Text;
            eProv.dsc_fono_2 = txtFono2Proveedor.Text;
            //eProv.cod_tipo_proveedor = lkpFrecuencia.EditValue.ToString();
            //eProv.flg_venta_consignacion = lkpFormaPago.EditValue.ToString();
            eProv.Observaciones = mmObservacionesProveedor.Text;
            eProv.cod_frecuencia = lkpFrecuencia.EditValue.ToString();
            eProv.cod_formapago = lkpFormaPago.EditValue.ToString();
            eProv.dsc_representante_legal = txtRepresentanteLegal.Text;
            eProv.cod_proveedor_ERP = txtCodigoERP.Text;
            eProv.dsc_licenciaconducir = txtLicenciaConducir.Text;
            eProv.dsc_nroautorizMTC = txtNroAutorizMTC.Text;
            eProv.dsc_marcavehiculo = txtMarcaVehiculo.Text;
            eProv.dsc_placavehiculo = txtPlacaVehiculo.Text;
            eProv.flg_transportista = chkflgTransportista.CheckState == CheckState.Checked ? "SI" : "NO";

            eProv.flg_agente_retencion = chkAgenteRetencion.CheckState == CheckState.Checked ? "SI" : "NO";
            eProv.flg_agente_percepcion = chkAgentePercepcion.CheckState == CheckState.Checked ? "SI" : "NO";
            eProv.flg_buen_contribuyente = chkBuenContribuyente.CheckState == CheckState.Checked ? "SI" : "NO";
            eProv.flg_auto_detraccion = chkAutoDetraccion.CheckState == CheckState.Checked ? "SI" : "NO";
            eProv.flg_domiciliado = chkDomiciliado.CheckState == CheckState.Checked ? "SI" : "NO";
            eProv.flg_afecto_cuarta = chkAfectoCuarta.CheckState == CheckState.Checked ? "SI" : "NO";
            eProv.flg_no_habido = chkNoHabido.CheckState == CheckState.Checked ? "SI" : "NO";
            eProv.fch_no_habido = dtNoHabido.EditValue == null ? new DateTime() : Convert.ToDateTime(dtNoHabido.EditValue);
            eProv.cod_usuario_registro = user.cod_usuario;
            return eProv;
        }

        private eProveedor_CuentasBancarias AsignarValoresCuentasBancarias()
        {
            eProveedor_CuentasBancarias eCuentaBanc = new eProveedor_CuentasBancarias();
            eCuentaBanc.cod_proveedor = cod_proveedor;
            eCuentaBanc.num_linea = Convert.ToInt16(txtCodCuentaBancaria.Text);
            eCuentaBanc.cod_banco = glkpBancoCuentaBancaria.EditValue.ToString();
            eCuentaBanc.cod_moneda = lkpMonedaCuentaBancaria.EditValue.ToString();
            eCuentaBanc.cod_tipo_cuenta = lkpTipoCuentaBancaria.EditValue.ToString();
            eCuentaBanc.dsc_cta_bancaria = txtNroCuentaBancaria.Text;
            eCuentaBanc.dsc_cta_interbancaria = txtNroCuentaInterbancaria.Text;
            eCuentaBanc.dsc_observaciones = mmObservacionCuentaBancaria.Text;
            eCuentaBanc.flg_pago_transferencia = chkPagoTransferenciaCuentaBancaria.CheckState == CheckState.Checked ? "SI" : "NO";
            eCuentaBanc.dsc_titular_cuenta = txtTitularCuentasBancarias.Text;

            return eCuentaBanc;
        }

        private eProveedor_Contactos AsignarValoresContactos()
        {
            eProveedor_Contactos eContact = new eProveedor_Contactos();
            eContact.cod_proveedor = cod_proveedor;
            eContact.cod_contacto = Convert.ToInt32(txtCodContacto.Text);
            eContact.dsc_nombre = txtNombreContacto.Text;
            eContact.dsc_apellidos = txtApellidoContacto.Text;
            eContact.fch_nacimiento = dtFecNacContacto.EditValue == null ? new DateTime() : Convert.ToDateTime(dtFecNacContacto.EditValue);
            eContact.dsc_correo = txtCorreoContacto.Text;
            eContact.dsc_telefono1 = txtFono1Contacto.Text;
            eContact.dsc_telefono2 = txtFono2Contacto.Text;
            eContact.dsc_cargo = txtCargoContacto.Text;
            eContact.cod_usuario_web = txtUsuarioWebContacto.Text;
            eContact.cod_clave_web = txtClaveWebContacto.Text;
            eContact.dsc_observaciones = mmObservacionContacto.Text;
            eContact.cod_usuario_reg = user.cod_usuario;

            return eContact;
        }

        private eProveedor_Tecnicos AsignarValoresTecnicos()
        {
            eProveedor_Tecnicos eTec = new eProveedor_Tecnicos();
            eTec.cod_proveedor = cod_proveedor;
            eTec.cod_tecnico = Convert.ToInt32(txtCodTecnico.Text);
            eTec.dsc_nombre = txtNombreTecnico.Text;
            eTec.dsc_apellidos = txtApellidoTecnico.Text;
            eTec.fch_nacimiento = dtFecNacTecnico.EditValue == null ? new DateTime() : Convert.ToDateTime(dtFecNacTecnico.EditValue);
            eTec.dsc_correo = txtCorreoTecnico.Text;
            eTec.dsc_telefono1 = txtFono1Tecnico.Text;
            eTec.dsc_telefono2 = txtFono2Tecnico.Text;
            eTec.flg_supervisor = chkflgSupervisorTecnico.CheckState == CheckState.Checked ? "SI" : "NO";
            eTec.cod_usuario_web = txtUsuarioWebTecnico.Text;
            eTec.cod_clave_web = txtClaveWebTecnico.Text;
            eTec.dsc_observaciones = mmObservacionTecnico.Text;
            eTec.cod_usuario_reg = user.cod_usuario;

            return eTec;
        }

        private string Guardar()
        {
            string result = "";
            eProveedor eProv = AsignarValoresProveedor();
            eProv = blProv.Guardar_Actualizar_Proveedor<eProveedor>(eProv, "Nuevo");
            if (eProv != null)
            {
                cod_proveedor = eProv.cod_proveedor;
                txtCodProveedor.Text = cod_proveedor;

                ObtenerListadoCuentasBancarias();
                ObtenerListadoContactosProveedor();
                ObtenerListadoEmpresasProveedor();
                ObtenerListadoServiciosProveedor();
                //ObtenerListadoTecnicosProveedor();

                result = "OK";
            }

            return result;
        }

        private string Modificar()
        {
            string result = "";
            eProveedor eProv = AsignarValoresProveedor();
            eProv = blProv.Guardar_Actualizar_Proveedor<eProveedor>(eProv, "Actualizar");

            if (eProv != null)
            {
                cod_proveedor = eProv.cod_proveedor;
                result = "OK";
            }

            return result;
        }

        private void btnNuevoCuentaBancaria_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposCuentasBancarias();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarCuentaBancaria_Click(object sender, EventArgs e)
        {
            try
            {
                if (glkpBancoCuentaBancaria.EditValue == null) { MessageBox.Show("Debe seleccionar un banco", "Guardar cuenta bancaria", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpBancoCuentaBancaria.Focus(); return; }
                if (lkpMonedaCuentaBancaria.EditValue == null) { MessageBox.Show("Debe seleccionar una moneda", "Guardar cuenta bancaria", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpMonedaCuentaBancaria.Focus(); return; }
                if (lkpTipoCuentaBancaria.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de cuenta", "Guardar cuenta bancaria", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpTipoCuentaBancaria.Focus(); return; }
                if (txtNroCuentaBancaria.Text == "") { MessageBox.Show("Debe ingresar un número de cuenta", "Guardar cuenta bancaria", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNroCuentaBancaria.Focus(); return; }

                eProveedor_CuentasBancarias eCuentaBanc = new eProveedor_CuentasBancarias();
                eCuentaBanc = AsignarValoresCuentasBancarias();

                if (MiAccion == Proveedor.Nuevo)
                {
                    ListCuentasBancarias.Add(eCuentaBanc);
                }
                else
                {
                    eCuentaBanc = blProv.Guardar_Actualizar_CuentaBancariaProveedor<eProveedor_CuentasBancarias>(eCuentaBanc, txtCodCuentaBancaria.Text == "0" ? "Nuevo" : "Actualizar");
                }

                ObtenerListadoCuentasBancarias();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarCuentaBancaria_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar esta cuenta bancaria?", "Eliminar cuenta bancaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eProveedor_CuentasBancarias eCuentaBanc = gvListadoCuentasBancarias.GetFocusedRow() as eProveedor_CuentasBancarias;

                    string result = blProv.Eliminar_CuentaBancariaProveedor(cod_proveedor, eCuentaBanc.num_linea);
                    ObtenerListadoCuentasBancarias();
                    if (ListCuentasBancarias.Count == 0) LimpiarCamposCuentasBancarias();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbtnEliminarCuentaBancaria_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar esta cuenta bancaria?", "Eliminar cuenta bancaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eProveedor_CuentasBancarias eCuentaBanc = gvListadoCuentasBancarias.GetFocusedRow() as eProveedor_CuentasBancarias;

                    string result = blProv.Eliminar_CuentaBancariaProveedor(cod_proveedor, eCuentaBanc.num_linea);
                    ObtenerListadoCuentasBancarias();
                    if (ListCuentasBancarias.Count == 0) LimpiarCamposCuentasBancarias();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevoContacto_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposContactosProveedor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombreContacto.Text == "") { MessageBox.Show("Debe ingresar un nombre", "Guardar contacto", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNombreContacto.Focus(); return; }
                if (txtApellidoContacto.Text == "") { MessageBox.Show("Debe ingresar un apellido", "Guardar contacto", MessageBoxButtons.OK, MessageBoxIcon.Error); txtApellidoContacto.Focus(); return; }
                if (chkcbEmpresaContacto.EditValue == null) { MessageBox.Show("Debe ingresar la empresa relacionada", "Guardar contacto", MessageBoxButtons.OK, MessageBoxIcon.Error); chkcbEmpresaContacto.Focus(); return; }
                //if (txtUsuarioWebContacto.Text == "") { MessageBox.Show("Debe ingresar un usuario web", "Guardar contacto", MessageBoxButtons.OK, MessageBoxIcon.Error); txtUsuarioWebContacto.Focus(); return; }
                //if (txtClaveWebContacto.Text == "") { MessageBox.Show("Debe ingresar una clave web", "Guardar contacto", MessageBoxButtons.OK, MessageBoxIcon.Error); txtClaveWebContacto.Focus(); return; }

                int cod_contacto = 0;
                eProveedor_Contactos eContact = new eProveedor_Contactos();
                eContact = AsignarValoresContactos();

                if (MiAccion == Proveedor.Nuevo)
                {
                    ListContactosProveedor.Add(eContact);
                }
                else
                {
                    eContact = blProv.Guardar_Actualizar_ContactosProveedor<eProveedor_Contactos>(eContact, txtCodContacto.Text == "0" ? "Nuevo" : "Actualizar");
                    cod_contacto = eContact.cod_contacto;
                    string result = blProv.Insertar_EmpresasContactoProveedor(cod_proveedor, cod_contacto, chkcbEmpresaContacto.EditValue == null ? "" : chkcbEmpresaContacto.EditValue.ToString());
                }

                ObtenerListadoContactosProveedor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este contacto?", "Eliminar contacto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eProveedor_Contactos eContact = gvListadoContactos.GetFocusedRow() as eProveedor_Contactos;

                    string result = blProv.Eliminar_ContactosProveedor(cod_proveedor, eContact.cod_contacto);
                    ObtenerListadoContactosProveedor();
                    if (ListContactosProveedor.Count == 0) LimpiarCamposContactosProveedor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbtnEliminarContacto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este contacto?", "Eliminar contacto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eProveedor_Contactos eContact = gvListadoContactos.GetFocusedRow() as eProveedor_Contactos;

                    string result = blProv.Eliminar_ContactosProveedor(cod_proveedor, eContact.cod_contacto);
                    ObtenerListadoContactosProveedor();
                    if (ListContactosProveedor.Count == 0) LimpiarCamposContactosProveedor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevoTecnico_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposTecnicosProveedor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarTecnico_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombreTecnico.Text == "") { MessageBox.Show("Debe ingresar un nombre", "Guardar Tecnico", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNombreTecnico.Focus(); return; }
                if (txtApellidoTecnico.Text == "") { MessageBox.Show("Debe ingresar un apellido", "Guardar Tecnico", MessageBoxButtons.OK, MessageBoxIcon.Error); txtApellidoTecnico.Focus(); return; }
                //if (dtFecNacTecnico.EditValue == null) { MessageBox.Show("Debe ingresar una fecha de nacimiento", "Guardar Tecnico", MessageBoxButtons.OK, MessageBoxIcon.Error); dtFecNacTecnico.Focus(); return; }
                if (txtUsuarioWebTecnico.Text == "") { MessageBox.Show("Debe ingresar un usuario web", "Guardar Tecnico", MessageBoxButtons.OK, MessageBoxIcon.Error); txtUsuarioWebTecnico.Focus(); return; }
                if (txtClaveWebTecnico.Text == "") { MessageBox.Show("Debe ingresar una clave web", "Guardar Tecnico", MessageBoxButtons.OK, MessageBoxIcon.Error); txtClaveWebTecnico.Focus(); return; }

                eProveedor_Tecnicos eTec = new eProveedor_Tecnicos();
                eTec = AsignarValoresTecnicos();

                if (MiAccion == Proveedor.Nuevo)
                {
                    ListTecnicosProveedor.Add(eTec);
                }
                else
                {
                    eTec = blProv.Guardar_Actualizar_TecnicosProveedor<eProveedor_Tecnicos>(eTec, txtCodTecnico.Text == "0" ? "Nuevo" : "Actualizar");
                }

                ObtenerListadoTecnicosProveedor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarTecnico_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este tenico?", "Eliminar tenico", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eProveedor_Tecnicos eTec = gvListadoTecnicos.GetFocusedRow() as eProveedor_Tecnicos;

                    string result = blProv.Eliminar_TecnicosProveedor(cod_proveedor, eTec.cod_tecnico);
                    ObtenerListadoTecnicosProveedor();
                    if (ListTecnicosProveedor.Count == 0) LimpiarCamposTecnicosProveedor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbtnEliminarTecnico_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este tenico?", "Eliminar tenico", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eProveedor_Tecnicos eTec = gvListadoTecnicos.GetFocusedRow() as eProveedor_Tecnicos;

                    string result = blProv.Eliminar_TecnicosProveedor(cod_proveedor, eTec.cod_tecnico);
                    ObtenerListadoTecnicosProveedor();
                    if (ListTecnicosProveedor.Count == 0) LimpiarCamposTecnicosProveedor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvListadoCuentasBancarias_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eProveedor_CuentasBancarias obj = gvListadoCuentasBancarias.GetRow(e.FocusedRowHandle) as eProveedor_CuentasBancarias;
                    eProveedor_CuentasBancarias eDirec = new eProveedor_CuentasBancarias();
                    eDirec = blProv.ObtenerCuentaBancaria<eProveedor_CuentasBancarias>(4, cod_proveedor, obj.num_linea);

                    txtCodCuentaBancaria.Text = eDirec.num_linea.ToString();
                    glkpBancoCuentaBancaria.EditValue = eDirec.cod_banco;
                    lkpMonedaCuentaBancaria.EditValue = eDirec.cod_moneda;
                    lkpTipoCuentaBancaria.EditValue = eDirec.cod_tipo_cuenta;
                    txtNroCuentaBancaria.Text = eDirec.dsc_cta_bancaria;
                    txtNroCuentaInterbancaria.Text = eDirec.dsc_cta_interbancaria;
                    mmObservacionCuentaBancaria.Text = eDirec.dsc_observaciones;
                    chkPagoTransferenciaCuentaBancaria.CheckState = eDirec.flg_pago_transferencia == "SI" ? CheckState.Checked : CheckState.Unchecked;
                    txtTitularCuentasBancarias.Text = eDirec.dsc_titular_cuenta;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvListadoCuentasBancarias_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                gvListadoCuentasBancarias_FocusedRowChanged(gvListadoCuentasBancarias, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, e.RowHandle));
            }
        }

        private void gvListadoContactos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eProveedor_Contactos obj = gvListadoContactos.GetRow(e.FocusedRowHandle) as eProveedor_Contactos;
                    eProveedor_Contactos eContact = new eProveedor_Contactos();
                    eContact = blProv.ObtenerContacto<eProveedor_Contactos>(6, cod_proveedor, obj.cod_contacto);

                    txtCodContacto.Text = eContact.cod_contacto.ToString();
                    txtNombreContacto.Text = eContact.dsc_nombre;
                    txtApellidoContacto.Text = eContact.dsc_apellidos;
                    //dtFecNacContacto.EditValue = eContact.fch_nacimiento;
                    if (eContact.fch_nacimiento.ToString().Contains("1/01/0001")) { dtFecNacContacto.EditValue = null; } else { dtFecNacContacto.EditValue = Convert.ToDateTime(eContact.fch_nacimiento); }
                    txtCorreoContacto.Text = eContact.dsc_correo;
                    txtFono1Contacto.Text = eContact.dsc_telefono1;
                    txtFono2Contacto.Text = eContact.dsc_telefono2;
                    txtCargoContacto.Text = eContact.dsc_cargo;
                    txtUsuarioWebContacto.Text = eContact.cod_usuario_web;
                    txtClaveWebContacto.Text = eContact.cod_clave_web;
                    mmObservacionContacto.Text = eContact.dsc_observaciones;
                    chkcbEmpresaContacto.SetEditValue(eContact.cod_empresa);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvListadoContactos_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                gvListadoContactos_FocusedRowChanged(gvListadoContactos, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, e.RowHandle));
            }
        }

        private void gvListadoTecnicos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eProveedor_Tecnicos obj = gvListadoTecnicos.GetRow(e.FocusedRowHandle) as eProveedor_Tecnicos;
                    eProveedor_Tecnicos eTec = new eProveedor_Tecnicos();
                    eTec = blProv.ObtenerTecnico<eProveedor_Tecnicos>(8, cod_proveedor, obj.cod_tecnico);

                    txtCodTecnico.Text = eTec.cod_tecnico.ToString();
                    txtNombreTecnico.Text = eTec.dsc_nombre;
                    txtApellidoTecnico.Text = eTec.dsc_apellidos;
                    //dtFecNacTecnico.EditValue = eTec.fch_nacimiento;
                    if (eTec.fch_nacimiento.ToString().Contains("1/01/0001")) { dtFecNacTecnico.EditValue = null; } else { dtFecNacTecnico.EditValue = Convert.ToDateTime(eTec.fch_nacimiento); }
                    txtCorreoTecnico.Text = eTec.dsc_correo;
                    txtFono1Tecnico.Text = eTec.dsc_telefono1;
                    txtFono2Tecnico.Text = eTec.dsc_telefono2;
                    chkflgSupervisorTecnico.Checked = eTec.flg_supervisor == "SI" ? true : false;
                    txtUsuarioWebTecnico.Text = eTec.cod_usuario_web;
                    txtClaveWebTecnico.Text = eTec.cod_clave_web;
                    mmObservacionTecnico.Text = eTec.dsc_observaciones;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvListadoTecnicos_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                gvListadoTecnicos_FocusedRowChanged(gvListadoTecnicos, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, e.RowHandle));
            }

        }

        private void gvListadoCuentasBancarias_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoContactos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void lkpPais_EditValueChanged(object sender, EventArgs e)
        {
            glkpDistrito.Properties.DataSource = null;
            lkpProvincia.Properties.DataSource = null;
            lkpDepartamento.Properties.DataSource = null;

            blCli.CargaCombosLookUp("TipoDepartamento", lkpDepartamento, "cod_departamento", "dsc_departamento", "", cod_condicion: lkpPais.EditValue == null ? "" : lkpPais.EditValue.ToString());
        }

        private void chkCodigoManual_CheckStateChanged(object sender, EventArgs e)
        {
            if (MiAccion == Proveedor.Nuevo) txtCodProveedor.Enabled = chkCodigoManual.CheckState == CheckState.Checked ? true : false;
        }

        private async void btnConsultarSunat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (glkpTipoDocumento.EditValue != null && glkpTipoDocumento.EditValue.ToString() == "DI004" && txtNroDocumento.Text.Length != 11)
                {
                    MessageBox.Show("El RUC debe tener 11 digitos", "Validación RUC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNroDocumento.Select();
                    return;
                }
                if (glkpTipoDocumento.EditValue != null && glkpTipoDocumento.EditValue.ToString() == "DI001" && txtNroDocumento.Text.Length != 8)
                {
                    MessageBox.Show("El DNI debe tener 8 digitos", "Validación DNI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNroDocumento.Select();
                    return;
                }
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Consultando en SUNAT", "Cargando...");
                //ConsultaSUNAT_Nuevo();
                //ConsultaSUNAT();
                //ConsultaSUNAT4(txtNroDocumento.Text.Trim());
                await ConsultaSUNAT5(txtNroDocumento.Text.Trim());
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //private static RestClient client = new
        //RestClient("https://ruc.com.pe/api/v1/consultas");
        //static void ConsultaSUNAT5(string RUC)
        //{
        //    RestRequest request = new RestRequest("Default",
        //    Method.POST);
        //    IRestResponse<List<string>> response = client.Execute<List<string>>(request);
        //    Console.ReadLine();
        //}

        private async void ConsultaSUNAT4(string nDocumento)
        {
            eSistema objLink = blVers.ObtenerVersion<eSistema>(7); //Link Descarga
            eSistema objRUC = blVers.ObtenerVersion<eSistema>(8); //Token RUC
            eSistema objDNI = blVers.ObtenerVersion<eSistema>(9); //Token DNI
            var client = new RestClient(objLink.dsc_valor);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            EnvioJSONRUC datosRUC = new EnvioJSONRUC()
            {
                token = objRUC.dsc_valor,
                ruc = nDocumento
            };
            EnvioJSONDNI datosDNI = new EnvioJSONDNI()
            {
                token = objDNI.dsc_valor,
                dni = nDocumento
            };

            var eJSON = "";
            if (glkpTipoDocumento.EditValue.ToString() == "DI004")
            {
                eJSON = JsonConvert.SerializeObject(datosRUC);
            }
            else
            {
                eJSON = JsonConvert.SerializeObject(datosDNI);
            }
            request.AddHeader("Authorization", "Basic token");
            request.AddParameter("application/json", eJSON, ParameterType.RequestBody);
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic blogObject = js.Deserialize<dynamic>(response.Content);
            //string error = blogObject["error"];
            //if (blogObject["error"] != null) { MessageBox.Show(blogObject["error"], "Consulta RUC Sunat", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            if (glkpTipoDocumento.EditValue.ToString() == "DI004")
            {
                txtRazonSocial.Text = blogObject["nombre_o_razon_social"];
                txtNombreComercial.Text = blogObject["nombre_o_razon_social"];
                chkBuenContribuyente.CheckState = blogObject["estado_del_contribuyente"] == "ACTIVO" ? CheckState.Checked : CheckState.Unchecked;
                chkNoHabido.CheckState = blogObject["condicion_de_domicilio"] == "HABIDO" ? CheckState.Unchecked : CheckState.Checked;
                txtDireccionProveedor.Text = blogObject["direccion_completa"];

                List<eCiudades> listDepartamento = new List<eCiudades>();
                List<eCiudades> listProvincia = new List<eCiudades>();
                List<eCiudades> listDistrito = new List<eCiudades>();
                lkpPais.EditValue = "00001";
                listDepartamento = blCli.ListarOpcionesVariasCliente<eCiudades>(14, cod_condicion: lkpPais.EditValue.ToString());
                eCiudades objDepart = listDepartamento.Find(x => x.dsc_departamento.ToUpper() == blogObject["departamento"].ToUpper());
                if (objDepart != null)
                {
                    lkpDepartamento.EditValue = objDepart.cod_departamento;
                    listProvincia = blCli.ListarOpcionesVariasCliente<eCiudades>(15, cod_condicion: objDepart.cod_departamento);
                    eCiudades objProv = listProvincia.Find(x => x.dsc_provincia.ToUpper() == blogObject["provincia"].ToUpper());
                    if (objProv != null)
                    {
                        lkpProvincia.EditValue = objProv.cod_provincia;
                        listDistrito = blCli.ListarOpcionesVariasCliente<eCiudades>(16, cod_condicion: objProv.cod_provincia);
                        eCiudades objDist = listDistrito.Find(x => x.dsc_distrito.ToUpper() == blogObject["distrito"].ToUpper());
                        if (objDist != null) glkpDistrito.EditValue = objDist.cod_distrito;
                    }
                }
            }
            else
            {
                string nombre_completo = blogObject["nombre_completo"];
                string[] nombres = nombre_completo.Split(' ');
                txtApellPaterno.Text = nombres[0];
                txtApellMaterno.Text = nombres[1];
                txtNombre.Text = nombres.Length > 3 ? nombres[2] + ' ' + nombres[3] : nombres[2];
            }
        }

        private async Task ConsultaSUNAT5(string nDocumento)
        {
            string endpoint = @"https://api.apis.net.pe/v1/ruc?numero=" + nDocumento;
            if (glkpTipoDocumento.EditValue.ToString() == "DI004")
            {
                endpoint = @"https://api.apis.net.pe/v1/ruc?numero=" + nDocumento;
            }
            else
            {
                endpoint = @"https://api.apis.net.pe/v1/dni?numero=" + nDocumento;
            }
            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(endpoint);
            myWebRequest.CookieContainer = cokkie;

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebResponse myhttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            Stream myStream = myhttpWebResponse.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myStream);

            string xDat = "";
            string validar = "";

            while (!myStreamReader.EndOfStream)
            {
                xDat = myStreamReader.ReadLine();
                if (xDat != "")
                {
                    string Datos = xDat;
                    char[] separadores = {','};
                    string[] palabras = Datos.Replace("\"", "").Replace("{", "").Replace("}", "").Split(separadores);

                    if (glkpTipoDocumento.EditValue.ToString() == "DI004")
                    {
                        txtRazonSocial.Text = palabras[0].Substring(palabras[0].IndexOf(":") + 1, palabras[0].Length - palabras[0].IndexOf(":") - 1).ToUpper();
                        txtNombreComercial.Text = palabras[0].Substring(palabras[0].IndexOf(":") + 1, palabras[0].Length - palabras[0].IndexOf(":") - 1).ToUpper();
                        chkBuenContribuyente.CheckState = palabras[3].Substring(palabras[3].IndexOf(":") + 1, palabras[3].Length - palabras[3].IndexOf(":") - 1).ToUpper() == "ACTIVO" ? CheckState.Checked : CheckState.Unchecked;
                        chkNoHabido.CheckState = palabras[4].Substring(palabras[4].IndexOf(":") + 1, palabras[4].Length - palabras[4].IndexOf(":") - 1).ToUpper() == "HABIDO" ? CheckState.Unchecked : CheckState.Checked;
                        txtDireccionProveedor.Text = palabras[5].Substring(palabras[5].IndexOf(":") + 1, palabras[5].Length - palabras[5].IndexOf(":") - 1).ToUpper();

                        List<eCiudades> listDepartamento = new List<eCiudades>();
                        List<eCiudades> listProvincia = new List<eCiudades>();
                        List<eCiudades> listDistrito = new List<eCiudades>();
                        lkpPais.EditValue = "00001";
                        listDepartamento = blCli.ListarOpcionesVariasCliente<eCiudades>(14, cod_condicion: lkpPais.EditValue.ToString());
                        eCiudades objDepart = listDepartamento.Find(x => x.dsc_departamento.ToUpper() == palabras[19].Substring(palabras[19].IndexOf(":") + 1, palabras[19].Length - palabras[19].IndexOf(":") - 1).ToUpper());
                        if (objDepart != null)
                        {
                            lkpDepartamento.EditValue = objDepart.cod_departamento;
                            listProvincia = blCli.ListarOpcionesVariasCliente<eCiudades>(15, cod_condicion: objDepart.cod_departamento);
                            eCiudades objProv = listProvincia.Find(x => x.dsc_provincia.ToUpper() == palabras[18].Substring(palabras[18].IndexOf(":") + 1, palabras[18].Length - palabras[18].IndexOf(":") - 1).ToUpper());
                            if (objProv != null)
                            {
                                lkpProvincia.EditValue = objProv.cod_provincia;
                                listDistrito = blCli.ListarOpcionesVariasCliente<eCiudades>(16, cod_condicion: objProv.cod_provincia);
                                eCiudades objDist = listDistrito.Find(x => x.dsc_distrito.ToUpper() == palabras[17].Substring(palabras[17].IndexOf(":") + 1, palabras[17].Length - palabras[17].IndexOf(":") - 1).ToUpper());
                                if (objDist != null) glkpDistrito.EditValue = objDist.cod_distrito;
                            }
                        }
                    }
                    else
                    {
                        //string nombre_completo = palabras[0].Substring(palabras[0].IndexOf(":") + 1, palabras[0].Length - palabras[0].IndexOf(":") - 1).ToUpper(); ;
                        //string[] nombres = nombre_completo.Split(' ');
                        //txtApellPaterno.Text = nombres[0];
                        //txtApellMaterno.Text = nombres[1];
                        //txtNombre.Text = nombres.Length > 3 ? nombres[2] + ' ' + nombres[3] : nombres[2];
                        txtApellPaterno.Text = palabras[21].Substring(palabras[21].IndexOf(":") + 1, palabras[21].Length - palabras[21].IndexOf(":") - 1).ToUpper();
                        txtApellMaterno.Text = palabras[22].Substring(palabras[22].IndexOf(":") + 1, palabras[22].Length - palabras[22].IndexOf(":") - 1).ToUpper();
                        txtNombre.Text = palabras[20].Substring(palabras[20].IndexOf(":") + 1, palabras[20].Length - palabras[20].IndexOf(":") - 1).ToUpper();
                    }

                    validar = "OK";
                }
            }//TERMINA EL WHILE

            if (validar == "OK")
            {
            }
            else
            {
                MessageBox.Show("Error al traer datos del proveedor.", "Traer datos SUNAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public class EnvioJSONRUC
        {
            public string token { get; set; }
            public string ruc { get; set; }
        }
        public class EnvioJSONDNI
        {
            public string token { get; set; }
            public string dni { get; set; }
        }
        private void ConsultaSUNAT()
        {
            ObtenerCap();
            //string endpoint = @"http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc=" + txtNroDocumento.Text.Trim() + "&codigo= " + texto.ToUpper() + "&tipdoc=1";
            //string endpoint = @"http://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc=" + txtNroDocumento.Text.Trim() + "&codigo=" + texto.ToUpper() + "&tipdoc=1";
            string endpoint = @"https://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/FrameCriterioBusquedaMovil.jsp?accion=consPorRuc&nroRuc=" + txtNroDocumento.Text.Trim() + "&codigo=" + texto.ToUpper() + "&tipdoc=1";


            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(endpoint);
            myWebRequest.CookieContainer = cokkie;

            //myWebRequest.Method = "POST";
            //myWebRequest.ContentType = "application/x-www-form-urlencoded";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            HttpWebResponse myhttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            Stream myStream = myhttpWebResponse.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myStream);

            string xDat = "";
            int pos = 0;
            int pocision = 0;
            string dato = "", xml = "";
            int posicionPersonaNatural = 0;
            string validar = "";

            while (!myStreamReader.EndOfStream)
            {
                xDat = myStreamReader.ReadLine();
                xml = xml + Environment.NewLine + xDat;
                pos += 1;
                if (xDat.ToString() == "            <td width=\"18%\" colspan=1  class=\"bgn\">N&uacute;mero de RUC: </td>")
                {
                    dato = "Razon Social";
                    pocision = pos + 1;
                    validar = "OK";
                }
                else if (xDat.ToString() == "            <td class=\"bgn\" colspan=1>Tipo Contribuyente: </td>")
                {
                    dato = "Persona Natural";
                    pocision = pos + 1;
                }

                else if (xDat.ToString() == "            <td class=\"bgn\" colspan=1>Tipo de Documento: </td>")
                {
                    dato = "Tipo Documento";
                    pocision = pos + 1;
                }
                else if (xDat.ToString() == "\t              <td class=\"bgn\"colspan=1>Condici&oacute;n del Contribuyente:</td>")
                {
                    dato = "Condicion";   //habido y no habido
                    pocision = pos + 3;
                }
                else if (xDat.ToString() == "              <td class=\"bgn\" colspan=1 >Nombre Comercial: </td>")
                {
                    dato = "Nombre Comercial";
                    pocision = pos + 1;
                }
                else if (xDat.ToString() == "              <td class=\"bgn\" colspan=1>Direcci&oacute;n del Domicilio Fiscal:</td>")
                {
                    dato = "Direccion";
                    pocision = pos + 1;
                }

                if (posicionPersonaNatural == pos)
                {
                    string NombresaApellidos = xDat.Substring(16);
                    char[] separadores = { ' ', ',' };
                    string[] palabras1 = NombresaApellidos.Split(separadores);
                    string ApellidosPat = palabras1[0];
                    string ApellidosMat = palabras1[1];
                    string Nombres = NombresaApellidos.Replace(ApellidosPat + " " + ApellidosMat + ", ", "");

                    txtNombre.Text = Nombres;
                    txtApellPaterno.Text = ApellidosPat;
                    txtApellMaterno.Text = ApellidosMat;
                    glkpTipoDocumento.EditValue = "DI001";

                    txtRazonSocial.Text = "";
                    posicionPersonaNatural = 0;
                }

                if (pocision == pos)
                {
                    if (dato == "Razon Social")
                    {
                        string razon = getDatafromXML(xDat, 25);
                        txtRazonSocial.Text = razon.Substring(15);
                        glkpTipoDocumento.EditValue = "DI004";
                    }
                    else if (dato == "Nombre Comercial")
                    {
                        txtNombreComercial.Text = getDatafromXML(xDat, 25);
                        if (txtNombreComercial.Text == "-")
                        {
                            txtNombreComercial.Text = txtRazonSocial.Text;
                        }
                    }
                    else if (dato == "Condicion")
                    {
                        string estado = getDatafromXML(xDat, 0).ToLower();
                        chkNoHabido.CheckState = estado == "habido" ? CheckState.Unchecked : CheckState.Checked;
                    }

                    else if (dato == "Persona Natural")
                    {
                        string personanatural = getDatafromXML(xDat, 25);
                        if (personanatural.Contains("PERSONA NATURAL"))
                        {
                            chkFlgJuridica.CheckState = CheckState.Unchecked;
                        }
                        else
                        {
                            txtNombre.Text = "";
                            txtApellPaterno.Text = "";
                            txtApellMaterno.Text = "";
                        }

                    }
                    else if (dato == "Tipo Documento")
                    {
                        string personanatural = getDatafromXML(xDat, 25).ToString();
                        char[] separadores = { ' ' };
                        string[] palabras = personanatural.Split(separadores);

                        string TipoDocumento = palabras[0];
                        string NumeroDocumento = palabras[1];

                        txtNroDocumento.Text = NumeroDocumento;
                        glkpTipoDocumento.Text = TipoDocumento;
                        posicionPersonaNatural = pos + 2;
                    }

                    else if (dato == "Direccion")
                    {
                        string direccion = getDatafromXML(xDat, 25);
                        txtDireccionProveedor.Text = direccion;

                        if (direccion != "-")
                        {
                            ObtenerUbigeo();
                            List<eCiudades> listDepartamento = new List<eCiudades>();
                            List<eCiudades> listProvincia = new List<eCiudades>();
                            List<eCiudades> listDistrito = new List<eCiudades>();
                            lkpPais.EditValue = "00001";
                            listDepartamento = blCli.ListarOpcionesVariasCliente<eCiudades>(14, cod_condicion: lkpPais.EditValue.ToString());
                            eCiudades objDepart = listDepartamento.Find(x => x.dsc_departamento.ToUpper() == SuDepartamento.ToUpper());
                            if (objDepart != null)
                            {
                                lkpDepartamento.EditValue = objDepart.cod_departamento;
                                listProvincia = blCli.ListarOpcionesVariasCliente<eCiudades>(15, cod_condicion: objDepart.cod_departamento);
                                eCiudades objProv = listProvincia.Find(x => x.dsc_provincia.ToUpper() == SuProvincia.ToUpper());
                                if (objProv != null)
                                {
                                    lkpProvincia.EditValue = objProv.cod_provincia;
                                    listDistrito = blCli.ListarOpcionesVariasCliente<eCiudades>(16, cod_condicion: objProv.cod_provincia);
                                    eCiudades objDist = listDistrito.Find(x => x.dsc_distrito.ToUpper() == SUDistrito.ToUpper());
                                    if (objDist != null) glkpDistrito.EditValue = objDist.cod_distrito;
                                }
                            }
                        }
                        pocision = 0;
                        dato = "";
                    }
                }
            }//TERMINA EL WHILE

            if (validar == "OK")
            {
            }
            else
            {
                MessageBox.Show("El RUC " + txtNroDocumento.Text + "  no existe", "Validación RUC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void ConsultaSUNAT_Nuevo()
        {
            //string endpoint = "https://api.sunat.cloud/ruc/" + txtNroDocumento.Text;
            string endpoint = @"http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc=" + txtNroDocumento.Text.Trim();

            HttpWebRequest request = WebRequest.Create(endpoint) as HttpWebRequest;
            request.CookieContainer = cokkie;
            request.Method = "POST";
            //request.ContentType = "application/json";
            request.ContentType = "application/x-www-form-urlencoded";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = reader.ReadToEnd();

            eRUC_SUNAT ruc = JsonConvert.DeserializeObject<eRUC_SUNAT>(json);
            if (ruc != null)
            {
                if (ruc.contribuyente_tipo.ToUpper().Contains("PERSONA NATURAL"))
                {
                    char[] separadores = { ' ', ',' };
                    string[] palabras1 = ruc.razon_social.Split(separadores);
                    string ApellidosPat = palabras1[0];
                    string ApellidosMat = palabras1[1];
                    string Nombres = ruc.razon_social.Replace(ApellidosPat + " " + ApellidosMat + ", ", "");

                    txtNombre.Text = Nombres;
                    txtApellPaterno.Text = ApellidosPat;
                    txtApellMaterno.Text = ApellidosMat;
                    glkpTipoDocumento.EditValue = "DI001";
                    txtRazonSocial.Text = "";
                    txtNombreComercial.Text = "";
                }
                else
                {
                    chkFlgJuridica.CheckState = CheckState.Checked;
                    glkpTipoDocumento.EditValue = "DI004";
                    txtRazonSocial.Text = ruc.razon_social;
                    txtNombreComercial.Text = ruc.nombre_comercial;
                    if (txtNombreComercial.Text == "-") txtNombreComercial.Text = txtRazonSocial.Text;
                    txtNombre.Text = "";
                    txtApellPaterno.Text = "";
                    txtApellMaterno.Text = "";
                }
                chkNoHabido.CheckState = ruc.contribuyente_condicion == "HABIDO" ? CheckState.Unchecked : CheckState.Checked;
                txtDireccionProveedor.Text = ruc.domicilio_fiscal;

                if (ruc.domicilio_fiscal != "-")
                {
                    ObtenerUbigeo();
                    List<eCiudades> ListPais = blCli.ListarOpcionesVariasCliente<eCiudades>(13);
                    eCiudades Pais = new eCiudades();
                    List<eCiudades> ListDepart = blCli.ListarOpcionesVariasCliente<eCiudades>(14);
                    eCiudades Depart = ListDepart.Find(x => x.dsc_departamento.ToLower() == SuDepartamento.ToLower());
                    if (Depart == null) Depart = ListDepart.Find(x => x.dsc_departamento.ToLower() == SuProvincia.ToLower());

                    if (Depart != null)
                    {
                        Pais = ListPais.Find(x => x.cod_pais == Depart.cod_pais);

                        List<eCiudades> ListProvinc = blCli.ListarOpcionesVariasCliente<eCiudades>(15, cod_condicion: Depart.cod_departamento);
                        eCiudades Provinc = ListProvinc.Find(x => x.dsc_provincia.ToLower() == SuProvincia.ToLower());

                        List<eCiudades> ListDist = blCli.ListarOpcionesVariasCliente<eCiudades>(16, cod_condicion: Provinc.cod_provincia);
                        eCiudades Dist = ListDist.Find(x => x.dsc_distrito.ToLower() == SUDistrito.ToLower());

                        if (Pais != null) lkpPais.EditValue = Pais.cod_pais;
                        if (Depart != null) lkpDepartamento.EditValue = Depart.cod_departamento;
                        if (Provinc != null) lkpProvincia.EditValue = Provinc.cod_provincia;
                        if (Dist != null) glkpDistrito.EditValue = Dist.cod_distrito;
                    }
                    else
                    {
                        MessageBox.Show("Departamento no encontrado", "Validación RUC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("El RUC " + txtNroDocumento.Text + "  no existe", "Validación RUC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        string captcha = "";
        CookieContainer cokkie = new CookieContainer();
        string[] nrosRuc = new string[] { };
        string texto = "";
        public void ObtenerCap()
        {

            try
            {
                ///////https://cors-anywhere.herokuapp.com/wmtechnology.org/Consultar-RUC/?modo=1&btnBuscar=Buscar&nruc=
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=image");
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=image");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=image");

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                request.CookieContainer = cokkie;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                var image = new Bitmap(responseStream);
                //pictureLogo.EditValue = image;
                string ruta = "C:\\IMPERIUM-Software\\tessdata";
                var ocr = new TesseractEngine(ruta, "eng", EngineMode.Default);
                ocr.DefaultPageSegMode = PageSegMode.SingleBlock;
                Tesseract.Page p = ocr.Process(image);
                texto = p.GetText().Trim().ToUpper().Replace(" ", "");
            }
            catch (Exception ex)
            {
                //mensaje de error
            }
        }

        public string getDatafromXML(string lineRead, int len = 0)
        {

            try
            {
                lineRead = lineRead.Trim();
                lineRead = lineRead.Remove(0, len);
                lineRead = lineRead.Replace("</td>", "");
                while (lineRead.Contains("  "))
                {
                    lineRead = lineRead.Replace("  ", " ");
                }
                return lineRead;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        string SUDistrito;
        string SuProvincia;
        string SuDepartamento;
        public void ObtenerUbigeo()
        {
            string direccion = txtDireccionProveedor.Text;
            int index = 0;
            for (int i = 0; i < direccion.Length; i++)
            {
                if (direccion[i].ToString() == "-")
                {
                    index = i + 2;
                }
            }
            SUDistrito = direccion.Substring(index).ToLower();
            direccion = direccion.Replace(" - " + SUDistrito.ToUpper(), "");
            index = 0;

            for (int i = 0; i < direccion.Length; i++)
            {
                if (direccion[i].ToString() == "-")
                {
                    index = i + 2;
                }
            }
            SuProvincia = direccion.Substring(index).ToLower();
            direccion = direccion.Replace(" - " + SuProvincia.ToUpper(), "");
            index = 0;

            for (int i = 0; i < direccion.Length; i++)
            {
                if (direccion[i].ToString() == "-")
                {
                    index = i + 3;
                }
            }
            SuDepartamento = direccion.Substring(index).ToLower();
            if (index == 0) SuDepartamento = SuProvincia;
            direccion = direccion.Replace(" - " + SuDepartamento.ToUpper(), "");
            index = 0;
        }

        private void gvEmpresasVinculadas_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvEmpresasVinculadas_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
            if (e.RowHandle >= 0)
            {
                GridView vw = sender as GridView;
                bool estado = Convert.ToBoolean(vw.GetRowCellValue(e.RowHandle, vw.Columns["Seleccionado"]));
                if (estado) e.Appearance.ForeColor = Color.Blue;
            }
        }

        private void gvListadoContactos_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoCuentasBancarias_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void rchkSeleccionado_CheckStateChanged(object sender, EventArgs e)
        {
            gvEmpresasVinculadas.PostEditor();
        }

        private void gvEmpresasVinculadas_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                int nRow = e.RowHandle;
                eProveedor_Empresas eProvEmp = new eProveedor_Empresas();
                eProveedor_Empresas obj = gvEmpresasVinculadas.GetRow(e.RowHandle) as eProveedor_Empresas;

                if (e.Column.FieldName == "valorRating" && !obj.Seleccionado) { MessageBox.Show("Debe vincular la empresa para poder calificarla.", "Calificar empresa", MessageBoxButtons.OK, MessageBoxIcon.Information); obj.valorRating = 0; gvEmpresasVinculadas.RefreshData(); return; }
                if (e.Column.FieldName == "Seleccionado" || e.Column.FieldName == "valorRating")
                {
                    //if (MiAccion == Proveedor.Nuevo)
                    //{
                    List<eProveedor_Empresas> listEmpresasUsuario = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(11, "", user.cod_usuario);
                    eProveedor_Empresas objEmp = listEmpresasUsuario.Find(x => x.cod_empresa == obj.cod_empresa);
                    if (objEmp == null) { MessageBox.Show("Usted no se encuentra vinculado a la empresa seleccionada.", "Vincular empresa", MessageBoxButtons.OK, MessageBoxIcon.Information); obj.Seleccionado = false; gvEmpresasVinculadas.RefreshData(); return; }
                    //}

                    obj.cod_proveedor = cod_proveedor; obj.flg_activo = obj.Seleccionado ? "SI" : "NO"; obj.cod_usuario_registro = user.cod_usuario;
                    eProvEmp = blProv.Guardar_Actualizar_ProveedorEmpresas<eProveedor_Empresas>(obj);
                    if (eProvEmp == null) { MessageBox.Show("Error al vincular empresa", "Vincular empresa", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    ActualizarListado = "SI";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rRatingCalificacion_EditValueChanged(object sender, EventArgs e)
        {
            gvEmpresasVinculadas.PostEditor();
        }

        private void btnFichaProveedor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                rptFichaProveedor report = new rptFichaProveedor();
                ReportPrintTool printTool = new ReportPrintTool(report);
                report.RequestParameters = false;
                printTool.AutoShowParametersPanel = false;
                report.Parameters["cod_proveedor"].Value = cod_proveedor;
                report.Parameters["cod_usuario"].Value = user.cod_usuario;
                printTool.ShowPreviewDialog();
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rchkSeleccionado1_CheckStateChanged(object sender, EventArgs e)
        {
            gvServiciosProveedor.PostEditor();
        }

        private void gvServiciosProveedor_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                eProveedor_Servicios eProvServ = new eProveedor_Servicios();
                eProveedor_Servicios obj = gvServiciosProveedor.GetRow(e.RowHandle) as eProveedor_Servicios;

                if (e.Column.FieldName == "Seleccionado")
                {
                    obj.cod_proveedor = cod_proveedor; obj.flg_activo = obj.Seleccionado ? "SI" : "NO"; obj.cod_usuario_registro = user.cod_usuario;
                    eProvServ = blProv.Guardar_Actualizar_ProveedorServicio<eProveedor_Servicios>(obj);
                    if (eProvServ == null) { MessageBox.Show("Error al vincular servicio", "Vincular servicio", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvServiciosProveedor_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvServiciosProveedor_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
            if (e.RowHandle >= 0)
            {
                GridView vw = sender as GridView;
                bool estado = Convert.ToBoolean(vw.GetRowCellValue(e.RowHandle, vw.Columns["Seleccionado"]));
                if (estado) e.Appearance.ForeColor = Color.Blue;
            }
        }

        private void txtNroDocumento_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtNroDocumento.Text.Trim() == "") return;
                if (glkpTipoDocumento.EditValue != null && glkpTipoDocumento.EditValue.ToString() == "DI004" && txtNroDocumento.Text.Length != 11)
                {
                    MessageBox.Show("El RUC debe tener 11 digitos", "Validación RUC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNroDocumento.Select();
                    return;
                }
                if (glkpTipoDocumento.EditValue != null && glkpTipoDocumento.EditValue.ToString() == "DI001" && txtNroDocumento.Text.Length != 8)
                {
                    MessageBox.Show("El DNI debe tener 8 digitos", "Validación DNI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNroDocumento.Select();
                    return;
                }
                if (MiAccion == Proveedor.Nuevo)
                {
                    if (txtNroDocumento.Text.Trim() == "") return;
                    eProveedor obj = new eProveedor();
                    obj = blProv.Validar_NroDocumento<eProveedor>(17, txtNroDocumento.Text.Trim());
                    if (obj != null)
                    {
                        if (MessageBox.Show("El número de documento ingresado ya se encuentra registrado en el sistema." + Environment.NewLine +
                                        "¿Desea visualizar la información del proveedor?", //y vincularlo a su empresa?", 
                                        "Validar número de documento", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            cod_proveedor = obj.cod_proveedor;
                            MiAccion = Proveedor.Editar;
                            Editar();
                            BloqueoControles(true, false, true);
                            xtabCuentasBancarias.PageEnabled = true;
                            xtabContactos.PageEnabled = true;
                            xtabEmpresasVinculadas.PageEnabled = true;
                            xtabServicios.PageEnabled = true;
                            xtabMarcas.PageEnabled = true;
                            btnFichaProveedor.Enabled = true;
                        }
                        txtNroDocumento.Select();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvEmpresasVinculadas_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            bool estado = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, view.Columns["Seleccionado"]));
            if (estado) e.Appearance.ForeColor = Color.Blue;
        }

        private void gvMarcasProveedor_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvMarcasProveedor_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                blGlobal.Pintar_EstiloGrilla(sender, e);
                GridView view = sender as GridView;
                if (view.Columns["Activo"] != null)
                {
                    string estado = view.GetRowCellValue(e.RowHandle, view.Columns["Activo"]).ToString();
                    if (estado == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void gvMarcasProveedor_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            
        }

        private void gvMarcasProveedor_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gvMarcasProveedor.PostEditor(); gvMarcasProveedor.RefreshData();
            eProveedor_Marca obj = gvMarcasProveedor.GetFocusedRow() as eProveedor_Marca;
            obj.flg_activo = "SI";
        }

        private void gvMarcasProveedor_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                gvMarcasProveedor.PostEditor(); gvMarcasProveedor.RefreshData();
                eProveedor_Marca obj = gvMarcasProveedor.GetFocusedRow() as eProveedor_Marca;
                obj.cod_usuario_registro = user.cod_usuario; obj.cod_proveedor = cod_proveedor;
                eProveedor_Marca eObj = blProv.Insertar_Actualizar_ProveedorMarca<eProveedor_Marca>(obj);
                if (eObj == null) { MessageBox.Show("Error al insertar marca", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                ObtenerDatos_MarcasProveedor();
                gvMarcasProveedor.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ObtenerDatos_MarcasProveedor()
        {
            ListMarcasProveedor = blProv.ListarMarcasProveedor<eProveedor_Marca>(12, cod_proveedor);
            bsMarcasProveedor.DataSource = null; bsMarcasProveedor.DataSource = ListMarcasProveedor;
            gvMarcasProveedor.RefreshData();
        }

        private void rbtnInactivar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de inactivar el registro?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eProveedor_Marca obj = gvMarcasProveedor.GetFocusedRow() as eProveedor_Marca; obj.flg_activo = "NO";
                if (obj == null) return;
                obj = blProv.Insertar_Actualizar_ProveedorMarca<eProveedor_Marca>(obj);
                if (obj == null) { MessageBox.Show("Error al inactivar registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                ObtenerDatos_MarcasProveedor();
            }
        }

        private void txtFono1Proveedor_EditValueChanged(object sender, EventArgs e)
        {
            //if (txtFono1Proveedor.Text == "0-") txtFono1Proveedor.Properties.MaskSettings.MaskExpression = "(\\(\\d\\d\\d\\) )?\\d{1,2}-(\\(\\d\\d\\d\\) )?\\d{1,3}-(\\(\\d\\d\\d\\d\\) )?\\d{1,4}";
            //if (txtFono1Proveedor.Text == "9-") txtFono1Proveedor.Properties.MaskSettings.MaskExpression = "(\\(\\d\\d\\d\\) )?\\d{1,3}-(\\(\\d\\d\\d\\) )?\\d{1,3}-(\\(\\d\\d\\d\\d\\) )?\\d{1,3}";
        }

        private void gvServiciosProveedor_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            bool estado = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, view.Columns["Seleccionado"]));
            if (estado) e.Appearance.ForeColor = Color.Blue;
        }

        private void lkpDepartamento_EditValueChanged(object sender, EventArgs e)
        {
            glkpDistrito.Properties.DataSource = null;
            lkpProvincia.Properties.DataSource = null;
            blCli.CargaCombosLookUp("TipoProvincia", lkpProvincia, "cod_provincia", "dsc_provincia", "", cod_condicion: lkpDepartamento.EditValue == null ? "" : lkpDepartamento.EditValue.ToString());
        }

        private void lkpProvincia_EditValueChanged(object sender, EventArgs e)
        {
            glkpDistrito.Properties.DataSource = null;
            //blCli.CargaCombosLookUp("TipoDistrito", glkpDistrito, "cod_distrito", "dsc_distrito", "", cod_condicion: lkpProvincia.EditValue.ToString());
            CargarCombosGridLookup("TipoDistrito", glkpDistrito, "cod_distrito", "dsc_distrito", "", cod_condicion: lkpProvincia.EditValue == null ? "" : lkpProvincia.EditValue.ToString());
        }

        private void glkpTipoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            //if (glkpTipoDocumento.EditValue.ToString() == "DI004") chkFlgJuridica.Checked = true;
            //chkFlgJuridica.Checked = glkpTipoDocumento.EditValue != null && glkpTipoDocumento.EditValue.ToString() == "DI004" ? true : false;
            if (glkpTipoDocumento.EditValue != null)
            {
                eProveedor obj = new eProveedor();
                obj = blProv.Validar_NroDocumento<eProveedor>(19, "", glkpTipoDocumento.EditValue.ToString());
                //ctd_digitos = obj.ctd_digitos; cod_sunat = obj.cod_sunat;
                txtNroDocumento.Properties.MaxLength = obj.ctd_digitos;
            }
        }
    }
}