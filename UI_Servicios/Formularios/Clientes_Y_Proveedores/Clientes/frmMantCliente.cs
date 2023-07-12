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
using DevExpress.XtraGrid.Views.Grid;
using UI_Servicios.Formularios.Clientes_Y_Proveedores.Clientes;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using DevExpress.Utils.Drawing;
using DevExpress.XtraSplashScreen;
using System.Net;
using Tesseract;
using RestSharp;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace UI_Servicios.Clientes_Y_Proveedores.Clientes
{
    internal enum Cliente
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2,
        Contacto = 3
    }
    public partial class frmMantCliente : DevExpress.XtraEditors.XtraForm
    {
        frmListadoClientes frmHandler;
        public eUsuario user = new eUsuario();
        internal Cliente MiAccion = Cliente.Nuevo;
        blClientes blCli = new blClientes();
        blVersion blVers = new blVersion();
        List<eCliente_Contactos> ListClienteContacto = new List<eCliente_Contactos>();
        List<eCliente_Direccion> ListDirecc = new List<eCliente_Direccion>();
        List<eCliente_Contactos> ListDireccionContacto = new List<eCliente_Contactos>();
        List<eCliente_Ubicacion> ListUbic = new List<eCliente_Ubicacion>();
        List<eCliente_CentroResponsabilidad> ListCentroResp = new List<eCliente_CentroResponsabilidad>();
        List<eCliente_Empresas> ListEmpresasCliente = new List<eCliente_Empresas>();
        blEncrypta blEncryp = new blEncrypta();
        blGlobales blGlobal = new blGlobales();
        blProveedores blProv = new blProveedores();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public string cod_cliente = "", cod_empresa = "";
        public string ActualizarListado = "NO";

        public string GrupoSeleccionado = "";
        public string ItemSeleccionado = "";

        public frmMantCliente()
        {
            InitializeComponent();
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
        }

        internal frmMantCliente(frmListadoClientes frm)
        {
            InitializeComponent();
            frmHandler = frm;
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
        }

        private void frmMantCliente_Load(object sender, EventArgs e)
        {
            groupControl1.AppearanceCaption.ForeColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
            Inicializar();
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
                case Cliente.Nuevo:
                    CargarCombos();
                    Nuevo();
                    break;
                case Cliente.Editar:
                    CargarCombos();
                    Editar();
                    gvEmpresasVinculadas.OptionsBehavior.Editable = true;
                    break;
                case Cliente.Vista:
                    CargarCombos();
                    Editar();
                    layoutControlGroup2.Enabled = false;
                    layoutControlGroup7.Enabled = false;
                    layoutControlGroup9.Enabled = false;
                    layoutControlGroup4.Enabled = false;
                    layoutControlGroup11.Enabled = false;
                    layoutControlItem61.Enabled = false;
                    layoutControlItem4.Enabled = false;
                    layoutControlItem62.Enabled = false;

                    BloqueoControles(false, false, false);
                    break;
                case Cliente.Contacto:
                    CargarCombos();
                    Editar();
                    layoutControlGroup2.Enabled = false;
                    layoutControlGroup7.Enabled = false;
                    //layoutControlGroup9.Enabled = false;
                    layoutControlGroup4.Enabled = false;
                    //layoutControlGroup11.Enabled = false;
                    layoutControlItem61.Enabled = false;
                    layoutControlItem4.Enabled = false;
                    layoutControlItem62.Enabled = false;
                    break;
            }
        }

        private void Nuevo()
        {
            chkFlgJuridica_CheckStateChanged(chkFlgJuridica, new EventArgs());
        }
        
        private void Editar()
        {
            eCliente eCli = new eCliente();
            eCli = blCli.ObtenerCliente<eCliente>(2, cod_cliente);

            txtCodCliente.Text = eCli.cod_cliente;
            chkFlgJuridica.Checked = eCli.flg_juridico == "SI" ? true : false;
            txtUsuarioRegistro.Text = eCli.dsc_usuario_registro;
            if (eCli.fch_registro.Year == 1) { dtFechaRegistro.EditValue = null; } else { dtFechaRegistro.EditValue = eCli.fch_registro; }
            txtUsuarioCambio.Text = eCli.dsc_usuario_cambio;
            if (eCli.fch_cambio.Year == 1) { dtFechaModificacion.EditValue = null; } else { dtFechaModificacion.EditValue = eCli.fch_cambio; }
            glkpTipoDocumento.EditValue = eCli.cod_tipo_documento;
            txtNroDocumento.Text = eCli.dsc_documento;
            //if (eCli.fch_nacimiento.Year == 1) { dtFecNacimiento.EditValue = null; } else { dtFecNacimiento.EditValue = eCli.fch_nacimiento; }
            if (eCli.fch_nacimiento.ToString().Contains("1/01/0001")) { dtFecNacimiento.EditValue = null; } else { dtFecNacimiento.EditValue = Convert.ToDateTime(eCli.fch_nacimiento); }
            txtApellPaterno.Text = eCli.dsc_apellido_paterno;
            txtApellMaterno.Text = eCli.dsc_apellido_materno;
            txtNombre.Text = eCli.dsc_nombre;
            txtRazonSocial.Text = eCli.dsc_razon_social;
            lkpTipoCliente.EditValue = eCli.cod_tipo_cliente;
            txtFono1.Text = eCli.dsc_telefono_1;
            txtFono2.Text = eCli.dsc_telefono_2;
            //txtCodTarjeta.Text = eCli.cod_tarjeta_cliente;
            txtCorreoPersonal.Text = eCli.dsc_email;
            txtCorreoTrabajo.Text = eCli.dsc_mail_trabajo;
            txtCorreoFE.Text = eCli.dsc_mail_fe;
            glkpTipoContacto.EditValue = eCli.cod_tipo_contacto;
            glkpCalificacion.EditValue = eCli.cod_calificacion;
            glkpCategoria.EditValue = eCli.cod_categoria;
            chkCodigoManual.CheckState = eCli.flg_codigo_autogenerado == "SI" ? CheckState.Checked : CheckState.Unchecked;
            txtCodigoERP.Text = eCli.cod_proveedor_ERP;
            txtNombreComercial.Text = eCli.dsc_razon_comercial;
            chkProspecto.Checked = eCli.flg_prospecto == "SI" ? true : false;

            ConfigurarProspecto();
            chkFlgJuridica_CheckStateChanged(chkFlgJuridica, new EventArgs());
            ObtenerListadoDirecciones();
            ObtenerListadoClientesContactos();
            ObtenerListadoCentroResponsabilidad();
            ObtenerListadoEmpresasCliente();
            btnNuevo.Enabled = true;
            xtraTabControl1.Enabled = true;

            List<eCliente_Empresas> listEmpresasUsuario = blProv.ListarEmpresasProveedor<eCliente_Empresas>(11, "", user.cod_usuario);
            List<eCliente_Empresas> listEmpresas = blCli.ListarEmpresasCliente<eCliente_Empresas>(15, cod_cliente);
            eCliente_Empresas objEmp = new eCliente_Empresas();
            int validar = 0;
            foreach (eCliente_Empresas obj in listEmpresasUsuario)
            {
                objEmp = listEmpresas.Find(x => x.cod_empresa == obj.cod_empresa);
                validar = validar > 0 ? validar : objEmp != null ? 1 : 0;
            }
            if (validar == 0) BloqueoControles(false, true, false);
        }

        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            btnNuevo.Enabled = Enabled;
            btnGuardar.Enabled = Enabled;
            btnConsultarSunat.Enabled = Enabled;
            btnProspecto.Enabled = Enabled;
            //txtCodCliente.ReadOnly = ReadOnly;
            chkFlgJuridica.ReadOnly = ReadOnly;
            chkCodigoManual.ReadOnly = ReadOnly;
            chkActivoCliente.ReadOnly = ReadOnly;
            glkpTipoDocumento.ReadOnly = ReadOnly;
            txtNroDocumento.ReadOnly = ReadOnly;
            dtFecNacimiento.ReadOnly = ReadOnly;
            txtCodigoERP.ReadOnly = ReadOnly;
            txtApellPaterno.ReadOnly = ReadOnly;
            txtApellMaterno.ReadOnly = ReadOnly;
            txtNombre.ReadOnly = ReadOnly;
            txtRazonSocial.ReadOnly = ReadOnly;
            txtNombreComercial.ReadOnly = ReadOnly;
            txtCorreoPersonal.ReadOnly = ReadOnly;
            txtCorreoTrabajo.ReadOnly = ReadOnly;
            txtCorreoFE.ReadOnly = ReadOnly;
            txtFono1.ReadOnly = ReadOnly;
            txtFono2.ReadOnly = ReadOnly;
            glkpTipoContacto.ReadOnly = ReadOnly;
            glkpCalificacion.ReadOnly = ReadOnly;
            glkpCategoria.ReadOnly = ReadOnly;
            lkpTipoCliente.ReadOnly = ReadOnly;

            //Direccion Cliente
            btnNuevoDireccion.Enabled = Enabled;
            btnGuardarDireccion.Enabled = Enabled;
            btnEliminarDireccion.Enabled = Enabled;
            gvListaDirecciones.OptionsBehavior.Editable = Editable;
            txtCodClienteContacto.ReadOnly = ReadOnly;
            glkpTipoDireccion.ReadOnly = ReadOnly;
            mmDireccion.ReadOnly = ReadOnly;
            txtReferecia.ReadOnly = ReadOnly;
            lkpPais.ReadOnly = ReadOnly;
            lkpDepartamento.ReadOnly = ReadOnly;
            lkpProvincia.ReadOnly = ReadOnly;
            glkpDistrito.ReadOnly = ReadOnly;
            txtFono1Direccion.ReadOnly = ReadOnly;
            txtFono2.ReadOnly = ReadOnly;
            chkFlgComprobante.ReadOnly = ReadOnly;
            chkFlgCobranza.ReadOnly = ReadOnly;

            //Direccion Contacto
            btnNuevoDireccionContacto.Enabled = Enabled;
            btnGuardarDireccionContacto.Enabled = Enabled;
            btnEliminarDireccionContacto.Enabled = Enabled;
            gvListaDireccionContactos.OptionsBehavior.Editable = Editable;
            txtCodDireccionContacto.ReadOnly = ReadOnly;
            txtNombreDireccionContacto.ReadOnly = ReadOnly;
            txtApellidoDireccionContacto.ReadOnly = ReadOnly;
            dtFecNacDireccionContacto.ReadOnly = ReadOnly;
            txtCorreoDireccionContacto.ReadOnly = ReadOnly;
            txtFono1DireccionContacto.ReadOnly = ReadOnly;
            txtFono2DireccionContacto.ReadOnly = ReadOnly;
            txtCargoDireccionContacto.ReadOnly = ReadOnly;
            mmObservacionDireccionContacto.ReadOnly = ReadOnly;
            
            //Direccion Ubicaciones
            btnExportarUbicaciones.Enabled = Enabled;
            btnUbicacionMasiva.Enabled = Enabled;
            btnNuevoUbicacion.Enabled = Enabled;
            btnGuardarUbicacion.Enabled = Enabled;
            btnInactivarUbicacion.Enabled = Enabled;
            txtCodUbicacion.ReadOnly = ReadOnly;
            chkActivoUbicacion.ReadOnly = ReadOnly;
            txtCodPerUbicacion.ReadOnly = ReadOnly;
            mmDescripcionUbicacion.ReadOnly = ReadOnly;
            lkpNivelUbicacion.ReadOnly = ReadOnly;
            lkpNivelSuperiorUbicacion.ReadOnly = ReadOnly;
            lkpResponsableUbicacion.ReadOnly = ReadOnly;
            mmObservacionUbicacion.ReadOnly = ReadOnly;

            //Contactos Cliente
            btnNuevoClienteContacto.Enabled = Enabled;
            btnGuardarClienteContacto.Enabled = Enabled;
            btnEliminarClienteContacto.Enabled = Enabled;
            txtCodClienteContacto.ReadOnly = ReadOnly;
            txtNombreClienteContacto.ReadOnly = ReadOnly;
            txtApellidoClienteContacto.ReadOnly = ReadOnly;
            dtFecNacClienteContacto.ReadOnly = ReadOnly;
            txtCorreoClienteContacto.ReadOnly = ReadOnly;
            txtFono1ClienteContacto.ReadOnly = ReadOnly;
            txtFono2ClienteContacto.ReadOnly = ReadOnly;
            txtCargoClienteContacto.ReadOnly = ReadOnly;
            mmObservacionClienteContacto.ReadOnly = ReadOnly;

            //Empresas Vinculadas Cliente
            gvEmpresasVinculadas.OptionsBehavior.Editable = Editable;
        }

        private void CargarCombos()
        {
            CargarCombosGridLookup("TipoDocumento", glkpTipoDocumento, "cod_tipo_documento", "dsc_tipo_documento", "", valorDefecto: true);
            CargarCombosGridLookup("TipoContacto", glkpTipoContacto, "cod_tipo_contacto", "dsc_tipo_contacto", "", valorDefecto: true);
            CargarCombosGridLookup("TipoCalificacion", glkpCalificacion, "cod_calificacion", "dsc_calificacion", "", valorDefecto: true);
            CargarCombosGridLookup("TipoCategoria", glkpCategoria, "cod_categoria", "dsc_categoria", "", valorDefecto: true);
            CargarCombosGridLookup("TipoDireccion", glkpTipoDireccion, "cod_tipo_direccion", "dsc_tipo_direccion", "");
            //CargarCombosGridLookup("TipoCalle", glkpTipoCalle, "cod_tipo_via", "dsc_tipo_via", "");
            //CargarCombosGridLookup("TipoAvenida", glkpTipoAvenida, "cod_calle", "dsc_calle", "");
            //CargarCombosGridLookup("TipoUrbanizacion", glkpTipoUrbanizacion, "cod_tipo_zona", "dsc_tipo_zona", "");
         //   CargarCombosGridLookup("TipoEtapa", glkpTipoEtapa, "cod_urbanizacion", "dsc_urbanizacion", "");
        //    CargarCombosGridLookup("TipoDistrito", glkpDistrito, "cod_distrito", "dsc_distrito", "");

            blCli.CargaCombosLookUp("TipoCliente", lkpTipoCliente, "cod_tipo_cliente", "dsc_tipo_cliente", "", valorDefecto: true);
            blCli.CargaCombosLookUp("TipoPais", lkpPais, "cod_pais", "dsc_pais", "");
            lkpPais.EditValue = "00001";

            blCli.CargaCombosLookUp("NivelUbicacion", lkpNivelUbicacion, "cod_nivel", "dsc_nivel", "");
            blCli.CargaCombosLookUp("NivelUbicacion", lkpNivelCentroResponsabilidad, "nro_nivel", "dsc_nivel", "");
            blCli.CargaCombosLookUp("ResponsableCentroResponsabilidad", lkpResponsableCentroResponsabilidad, "cod_contacto", "dsc_nombre_completo", "", cod_cliente);

            if (MiAccion == Cliente.Nuevo)
            {
                picAnteriorCliente.Enabled = false; picSiguienteCliente.Enabled = false; btnNuevo.Enabled = false;
            }
        }

        private void CargarCombosGridLookup(string nCombo, GridLookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", bool valorDefecto = false)
        {
            DataTable tabla = new DataTable();
            tabla = blCli.ObtenerListadoGridLookup(nCombo);

            combo.Properties.DataSource = tabla;
            combo.Properties.ValueMember = campoValueMember;
            combo.Properties.DisplayMember = campoDispleyMember;
            if (campoSelectedValue == "") { combo.EditValue = null; } else { combo.EditValue = campoSelectedValue; }
            if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
        }
        private void ObtenerListadoClientesContactos()
        {
            ListClienteContacto = blCli.ListarContactos<eCliente_Contactos>(7, cod_cliente, 0);
            bsClienteContactos.DataSource = null; bsClienteContactos.DataSource = ListClienteContacto;
            gvListaClienteContactos.RefreshData();
        }

        private void ObtenerListadoDirecciones()
        {
            ListDirecc = blCli.ListarDirecciones<eCliente_Direccion>(3, cod_cliente);
            bsListaDirecciones.DataSource = null; bsListaDirecciones.DataSource = ListDirecc;
            gvListaDirecciones.RefreshData();
        }

        private void ObtenerListadoDireccionesContactos()
        {
            eCliente_Direccion eDirec = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;
            ListDireccionContacto = blCli.ListarContactos<eCliente_Contactos>(9, cod_cliente, eDirec.num_linea);
            bsDireccionContactos.DataSource = null; bsDireccionContactos.DataSource = ListDireccionContacto;
            gvListaDireccionContactos.RefreshData();
        }

        private void ObtenerListadoUbicaciones()
        {
            eCliente_Direccion eDirec = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;
            ListUbic = blCli.ListarUbicaciones<eCliente_Ubicacion>(5, cod_cliente, eDirec.num_linea);
            bsDireccionUbicaciones.DataSource = null; bsDireccionUbicaciones.DataSource = ListUbic;
            //tlUbicacionesDireccion.RefreshData();
        }

        private void ObtenerListadoCentroResponsabilidad()
        {
            ListCentroResp = blCli.ListarCentroResponsabilidad<eCliente_CentroResponsabilidad>(11, cod_cliente);
            bsClienteCentroResponsabilidad.DataSource = null; bsClienteCentroResponsabilidad.DataSource = ListCentroResp;
        }

        private void ObtenerListadoEmpresasCliente()
        {
            ListEmpresasCliente = blCli.ListarEmpresasCliente<eCliente_Empresas>(16, cod_cliente, user.cod_usuario);
            bsEmpresasCliente.DataSource = null; bsEmpresasCliente.DataSource = ListEmpresasCliente;

            if (MiAccion != Cliente.Nuevo)
            {
                List<eCliente_Empresas> lista = blCli.ListarEmpresasCliente<eCliente_Empresas>(15, cod_cliente);
                foreach (eCliente_Empresas obj in lista)
                {
                    eCliente_Empresas oCliEmp = ListEmpresasCliente.Find(x => x.cod_empresa == obj.cod_empresa);
                    if (oCliEmp != null) { oCliEmp.Seleccionado = true; oCliEmp.valorRating = obj.valorRating; oCliEmp.dsc_pref_ceco = obj.dsc_pref_ceco; }
                }
            }

            gvEmpresasVinculadas.RefreshData();
        }
        private void chkFlgJuridica_CheckStateChanged(object sender, EventArgs e)
        {
            txtRazonSocial.Enabled = chkFlgJuridica.CheckState == CheckState.Checked ? true : false;
            txtNombreComercial.Enabled = chkFlgJuridica.CheckState == CheckState.Checked ? true : false;
            txtApellPaterno.Enabled = chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            txtApellMaterno.Enabled = chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            txtNombre.Enabled = chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            dtFecNacimiento.Enabled = chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            dtFecNacimiento.EditValue = chkFlgJuridica.CheckState == CheckState.Checked ? null : dtFecNacimiento.EditValue;
            //txtEdad.Enabled = chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            glkpTipoDocumento.Enabled = chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            if (chkFlgJuridica.Checked == true) glkpTipoDocumento.EditValue = "DI004";
        }

        private void frmMantCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
        }

        private void gvListaDirecciones_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if(e.FocusedRowHandle >= 0)
                {
                    LimpiarCamposDireccion();
                    LimpiarCamposDireccionContacto();
                    LimpiarCamposUbicacion();
                    eCliente_Direccion obj = gvListaDirecciones.GetRow(e.FocusedRowHandle) as eCliente_Direccion;
                    eCliente_Direccion eDirec = new eCliente_Direccion();
                    eDirec = blCli.ObtenerDireccion<eCliente_Direccion>(4, cod_cliente, obj.num_linea);

                    txtCodDireccion.Text = eDirec.num_linea.ToString();
                    glkpTipoDireccion.EditValue = eDirec.cod_tipo_direccion;
                    txtNombreDireccion.Text = eDirec.dsc_nombre_direccion;
                    //glkpTipoCalle.EditValue = eDirec.cod_tipo_via;
                    //glkpTipoAvenida.Text = eDirec.cod_calle_direccion;
                    //glkpTipoUrbanizacion.EditValue = eDirec.cod_tipo_zona;
                    //glkpTipoEtapa.Text = eDirec.cod_urbanizacion;
                    //txtNumero.Text = eDirec.cod_numero;
                    //txtInterior.Text = eDirec.cod_interior;
                    //txtManzana.Text = eDirec.cod_manzana;
                    //txtLote.Text = eDirec.cod_lote;
                    //txtSubLote.Text = eDirec.cod_sublote;
                    mmDireccion.Text = eDirec.dsc_cadena_direccion;
                    lkpPais.EditValue = eDirec.cod_pais;
                    lkpDepartamento.EditValue = eDirec.cod_departamento;
                    lkpProvincia.EditValue = eDirec.cod_provincia;
                    glkpDistrito.EditValue = eDirec.cod_distrito;
                    txtFono1Direccion.Text = eDirec.dsc_telefono_1;
                    txtFono2Direccion.Text = eDirec.dsc_telefono_2;
                    txtReferecia.Text = eDirec.dsc_referencia;
                    chkFlgComprobante.Checked = eDirec.flg_comprobante == "SI" ? true : false;
                    chkFlgCobranza.Checked = eDirec.flg_direccion_cobranza == "SI" ? true : false;

                    blCli.CargaCombosLookUp("ResponsableUbicacion", lkpResponsableUbicacion, "cod_contacto", "dsc_nombre_completo", "", cod_cliente, num_linea: obj.num_linea);
                    ObtenerListadoDireccionesContactos();
                    ObtenerListadoUbicaciones();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvListaDirecciones_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                gvListaDirecciones_FocusedRowChanged(gvListaDirecciones, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, e.RowHandle));
            }
        }

        private void gvlkpTipoDocumento_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpTipoDocumento_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpTipoContacto_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpTipoContacto_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpCalificacion_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpCalificacion_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpCategoria_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpCategoria_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpTipoDireccion_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpTipoDireccion_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpTipoCalle_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpTipoCalle_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpTipoAvenida_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpTipoAvenida_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpTipoUrbanizacion_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpTipoUrbanizacion_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpTipoEtapa_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpTipoEtapa_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpDistrito_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpDistrito_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                if (chkProspecto.CheckState == CheckState.Unchecked)
                {
                    if (glkpTipoDocumento.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de documento", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpTipoDocumento.Focus(); return; }
                    if (txtNroDocumento.Text.Trim() == "") { MessageBox.Show("Debe ingresar un número de documento", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNroDocumento.Focus(); return; }
                    string NroDoc = ValidarLongitudDocumento();
                    if (NroDoc != "OK") { MessageBox.Show("Longitud incorrecta, el " + glkpTipoDocumento.Text + " debe tener " + NroDoc + " dígitos.", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNroDocumento.Focus(); return; }
                    if (txtApellPaterno.Text == "" && chkFlgJuridica.CheckState == CheckState.Unchecked) { MessageBox.Show("Debe ingresar un apellido paterno", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtApellPaterno.Focus(); return; }
                    if (txtApellMaterno.Text == "" && chkFlgJuridica.CheckState == CheckState.Unchecked) { MessageBox.Show("Debe ingresar un apellido materno", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtApellMaterno.Focus(); return; }
                    if (txtNombre.Text == "" && chkFlgJuridica.CheckState == CheckState.Unchecked) { MessageBox.Show("Debe ingresar un nombre", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNombre.Focus(); return; }
                    int Anho = DateTime.Now.Year - Convert.ToDateTime(dtFecNacimiento.EditValue).Year;
                    if (chkFlgJuridica.CheckState == CheckState.Unchecked) { 
                    if (Anho < 18 && chkFlgJuridica.CheckState == CheckState.Unchecked) { MessageBox.Show("La edad del cliente es menor de 18 años, corregir el año de nacimiento", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); dtFecNacimiento.Focus(); return; }
                    }
                    if (lkpTipoCliente.EditValue == null) { MessageBox.Show("Debe seleccionar el tipo de cliente", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpTipoCliente.Focus(); return; }
                    //if (txtFono1.Text == "") { MessageBox.Show("Debe ingresar un teléfono", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtFono1.Focus(); return; }
                    //if (txtCodTarjeta.Text == "") { MessageBox.Show("Debe ingresar el código de tarjeta", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCodTarjeta.Focus(); return; }
                    //if (txtCorreoPersonal.Text == "") { MessageBox.Show("Debe ingresar un correo personal", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCorreoPersonal.Focus(); return; }
                    //if (txtCorreoTrabajo.Text == "") { MessageBox.Show("Debe ingresar un correo de trabajo", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCorreoTrabajo.Focus(); return; }
                    if (txtCorreoFE.Text == "") { MessageBox.Show("Debe ingresar un correo para la facturación electrónica", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCorreoFE.Focus(); return; }
                    if (glkpCategoria.EditValue == null) { MessageBox.Show("Debe seleccionar la categoría", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpCategoria.Focus(); return; }
                }

                if (glkpTipoContacto.EditValue == null) { MessageBox.Show("Debe seleccionar el tipo de contacto", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpTipoContacto.Focus(); return; }
                if (glkpCalificacion.EditValue == null) { MessageBox.Show("Debe seleccionar la calificación", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpCalificacion.Focus(); return; }
                if (txtRazonSocial.Text == "" && chkFlgJuridica.CheckState == CheckState.Checked) { MessageBox.Show("Debe ingresar la razón social", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtRazonSocial.Focus(); return; }
                if (mmDireccion.EditValue == null) { MessageBox.Show("Debe ingresar la dirección del cliente", "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpTipoContacto.Focus(); return; }

                string result = "";
                switch (MiAccion)
                {
                    case Cliente.Nuevo: result = Guardar(); break;
                    case Cliente.Editar: result = Modificar(); break;
                }

                if (result == "OK")
                {
                    MessageBox.Show("Se guardó el cliente de manera satisfactoria", "Guardar cliente", MessageBoxButtons.OK);

                    ActualizarListado = "SI";
                    if (frmHandler != null)
                    {
                        int nRow = frmHandler.gvListaClientes.FocusedRowHandle;
                        frmHandler.frmListadoClientes_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                        frmHandler.gvListaClientes.FocusedRowHandle = nRow;
                    }

                    if (MiAccion == Cliente.Nuevo)
                    {
                        MiAccion = Cliente.Editar;
                        xtraTabControl1.Enabled = true;
                        eCliente eCli = new eCliente();
                        eCli = blCli.ObtenerCliente<eCliente>(2, cod_cliente);
                        txtUsuarioRegistro.Text = eCli.dsc_usuario_registro;
                        if (eCli.fch_registro.Year == 1) { dtFechaRegistro.EditValue = null; } else { dtFechaRegistro.EditValue = eCli.fch_registro; }
                        txtUsuarioCambio.Text = eCli.dsc_usuario_cambio;
                        if (eCli.fch_cambio.Year == 1) { dtFechaModificacion.EditValue = null; } else { dtFechaModificacion.EditValue = eCli.fch_cambio; }

                        ObtenerListadoEmpresasCliente();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string Guardar()
        {
            string result = "";
            eCliente eCli = AsignarValoresCliente();
            eCli = blCli.Guardar_Actualizar_Cliente<eCliente>(eCli, "Nuevo");
            if (eCli != null)
            {
                cod_cliente = eCli.cod_cliente;
                txtCodCliente.Text = cod_cliente;

                GuardarClienteDirecciones();
                //GuardarClienteCentroResponsabilidad();
                //GuardarDireccionContactos();
                //GuardarDireccionUbicaciones();

                eCliente_Direccion eDirec = new eCliente_Direccion();
                eDirec = AsignarValoresDireccion();

                if (MiAccion == Cliente.Nuevo)
                {
                    eDirec = blCli.Guardar_Actualizar_ClienteDireccion<eCliente_Direccion>(eDirec, txtCodDireccion.Text == "0" ? "Nuevo" : "Actualizar");
                    GuardarClienteContactos(eDirec);
                }

                ObtenerListadoDirecciones();
                ObtenerListadoClientesContactos();
                //ObtenerListadoCentroResponsabilidad();

                List<eCliente_Empresas> listEmpresasUsuario = blProv.ListarEmpresasProveedor<eCliente_Empresas>(11, "", user.cod_usuario);
                eCliente_Empresas eCliEmp = new eCliente_Empresas();
                eCliEmp.cod_cliente = cod_cliente; eCliEmp.cod_empresa = listEmpresasUsuario[0].cod_empresa; eCliEmp.valorRating = 0; eCliEmp.flg_activo = "SI"; eCliEmp.cod_usuario_registro = user.cod_usuario; eCliEmp.dsc_pref_ceco = "";
                eCliEmp = blCli.Guardar_Actualizar_ClienteEmpresas<eCliente_Empresas>(eCliEmp);

                result = "OK";
            }
            return result;
        }
        private string Modificar()
        {
            string result = "";
            eCliente eCli = AsignarValoresCliente();
            eCli = blCli.Guardar_Actualizar_Cliente<eCliente>(eCli, "Actualizar");

            if (eCli != null)
            {
                cod_cliente = eCli.cod_cliente;
                result = "OK";
            }
            return result;
        }

        private eCliente AsignarValoresCliente()
        {
            eCliente eCli = new eCliente();

            eCli.cod_cliente = txtCodCliente.Text;
            eCli.flg_juridico = chkFlgJuridica.CheckState == CheckState.Checked ? "SI" : "NO";
            eCli.cod_usuario = user.cod_usuario;
            //eCli.fch_registro = Convert.ToDateTime(dtFechaRegistro.EditValue);
            eCli.cod_tipo_documento = glkpTipoDocumento.EditValue.ToString();
            eCli.dsc_documento = txtNroDocumento.Text;
            eCli.fch_nacimiento = dtFecNacimiento.EditValue == null ? new DateTime() : Convert.ToDateTime(dtFecNacimiento.EditValue);
            eCli.dsc_apellido_paterno = txtApellPaterno.Text;
            eCli.dsc_apellido_materno = txtApellMaterno.Text;
            eCli.dsc_nombre = txtNombre.Text;
            eCli.dsc_razon_social = txtRazonSocial.Text;
            eCli.dsc_cliente = chkFlgJuridica.CheckState == CheckState.Checked ? txtRazonSocial.Text : txtApellPaterno.Text + " " + txtApellMaterno.Text + " " + txtNombre.Text;
            eCli.cod_tipo_cliente = lkpTipoCliente.EditValue.ToString();
            eCli.dsc_telefono_1 = txtFono1.Text;
            eCli.dsc_telefono_2 = txtFono2.Text;
            //eCli.cod_tarjeta_cliente = txtCodTarjeta.Text;
            eCli.dsc_email = txtCorreoPersonal.Text;
            eCli.dsc_mail_trabajo = txtCorreoTrabajo.Text;
            eCli.dsc_mail_fe = txtCorreoFE.Text;
            eCli.cod_tipo_contacto = glkpTipoContacto.EditValue.ToString();
            eCli.cod_calificacion = glkpCalificacion.EditValue.ToString();
            eCli.cod_categoria = glkpCategoria.EditValue.ToString();
            eCli.cod_usuario = user.cod_usuario;
            eCli.dsc_razon_comercial = txtNombreComercial.Text;
            eCli.cod_proveedor_ERP = txtCodigoERP.Text;
            eCli.flg_codigo_autogenerado = chkCodigoManual.CheckState == CheckState.Checked ? "SI" : "NO";
            eCli.flg_prospecto = chkProspecto.CheckState == CheckState.Checked ? "SI" : "NO";

            return eCli;
        }

        private void btnGuardarDireccion_Click(object sender, EventArgs e)
        {
            try
            {
                if (glkpTipoDireccion.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de dirección", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpTipoDireccion.Focus(); return; }
                if (txtNombreDireccion.Text == "") { MessageBox.Show("Debe ingresar el nombre de la dirección", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNombreDireccion.Focus(); return; }
                if (mmDireccion.Text == "") { MessageBox.Show("Debe ingresar la dirección", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); mmDireccion.Focus(); return; }
                if (lkpPais.EditValue == null) { MessageBox.Show("Debe seleccionar un país", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpPais.Focus(); return; }
                if (lkpDepartamento.EditValue == null) { MessageBox.Show("Debe seleccionar un departamento", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpDepartamento.Focus(); return; }
                if (lkpProvincia.EditValue == null) { MessageBox.Show("Debe seleccionar una provincia", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpProvincia.Focus(); return; }
                if (glkpDistrito.EditValue == null) { MessageBox.Show("Debe seleccionar un distrito", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpDistrito.Focus(); return; }
                if (txtFono1Direccion.Text == "") { MessageBox.Show("Debe ingresar un teléfono", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtFono1Direccion.Focus(); return; }

                eCliente_Direccion eDirec = new eCliente_Direccion();
                eDirec = AsignarValoresDireccion();

                if(MiAccion == Cliente.Nuevo)
                {
                    ListDirecc.Add(eDirec);
                    gvListaDirecciones.RefreshData();
                }
                else
                {
                    eDirec = blCli.Guardar_Actualizar_ClienteDireccion<eCliente_Direccion>(eDirec, txtCodDireccion.Text == "0" ? "Nuevo" : "Actualizar");
                }

                ObtenerListadoDirecciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GuardarClienteDirecciones()
        {
            for(int x = 0; x <= gvListaDirecciones.RowCount - 1; x++)
            {
                eCliente_Direccion eDirec = gvListaDirecciones.GetRow(x) as eCliente_Direccion;
                if(eDirec != null)
                {
                    eDirec.cod_cliente = cod_cliente;
                    eDirec = blCli.Guardar_Actualizar_ClienteDireccion<eCliente_Direccion>(eDirec, "Nuevo");
                }
            }
        }
        private void GuardarClienteContactos(eCliente_Direccion eDirec)
        {
            eCliente_Contactos eContact = new eCliente_Contactos();
            eContact = AsignarValoresDireccionContacto(eDirec);

            eContact = blCli.Guardar_Actualizar_ClienteDireccionContacto<eCliente_Contactos>(eContact, txtCodDireccionContacto.Text == "0" ? "Nuevo" : "Actualizar");
        }

        private void GuardarClienteCentroResponsabilidad()
        {
            for (int x = 0; x <= tlClienteCentroResponsabilidad.AllNodesCount - 1; x++)
            {
                eCliente_CentroResponsabilidad eCentroResp = tlClienteCentroResponsabilidad.GetRow(x) as eCliente_CentroResponsabilidad;
                if (eCentroResp != null)
                {
                    eCentroResp.cod_cliente = cod_cliente;
                    eCentroResp = blCli.Guardar_Actualizar_ClienteCentroResponsabilidad<eCliente_CentroResponsabilidad>(eCentroResp, "Nuevo");
                }
            }
        }

        private void GuardarDireccionContactos()
        {
            for (int x = 0; x <= gvListaDireccionContactos.RowCount - 1; x++)
            {
                eCliente_Contactos eContact = gvListaDireccionContactos.GetRow(x) as eCliente_Contactos;
                if (eContact != null)
                {
                    eContact.cod_cliente = cod_cliente;
                    eContact = blCli.Guardar_Actualizar_ClienteDireccionContacto<eCliente_Contactos>(eContact, "Nuevo");
                }
            }
        }

        private void GuardarDireccionUbicaciones()
        {
            for (int x = 0; x <= tlUbicacionesDireccion.AllNodesCount - 1; x++)
            {
                eCliente_Ubicacion eUbic = tlUbicacionesDireccion.GetRow(x) as eCliente_Ubicacion;
                if (eUbic != null)
                {
                    eUbic.cod_cliente = cod_cliente;
                    eUbic = blCli.Guardar_Actualizar_ClienteUbicacion<eCliente_Ubicacion>(eUbic, "Nuevo");
                }
            }
        }

        private eCliente_Direccion AsignarValoresDireccion()
        {
            eCliente_Direccion eDirec = new eCliente_Direccion();
            eDirec.cod_cliente = cod_cliente;
            eDirec.num_linea = Convert.ToInt32(txtCodDireccion.Text);
            eDirec.cod_tipo_direccion = glkpTipoDireccion.EditValue == null ? "" : glkpTipoDireccion.EditValue.ToString();
            eDirec.dsc_tipo_direccion = glkpTipoDireccion.EditValue == null ? "" : glkpTipoDireccion.Text;
            eDirec.dsc_nombre_direccion = txtNombreDireccion.Text;
            //eDirec.cod_tipo_via = glkpTipoCalle.EditValue == null ? "" : glkpTipoCalle.EditValue.ToString();
            //eDirec.cod_calle_direccion = glkpTipoAvenida.EditValue;
            //eDirec.cod_tipo_zona = glkpTipoUrbanizacion.EditValue == null ? "" : glkpTipoUrbanizacion.EditValue.ToString();
            //eDirec.cod_urbanizacion = glkpTipoEtapa.Text;
            //eDirec.dsc_urbanizacion = glkpTipoEtapa.Text;
            //eDirec.cod_numero = txtNumero.Text;
            //eDirec.cod_interior = txtInterior.Text;
            //eDirec.cod_manzana = txtManzana.Text;
            //eDirec.cod_lote = txtLote.Text;
            //eDirec.cod_sublote = txtSubLote.Text;
            eDirec.dsc_cadena_direccion = mmDireccion.Text;
            eDirec.cod_pais = lkpPais.EditValue == null ? "" : lkpPais.EditValue.ToString();
            eDirec.cod_departamento = lkpDepartamento.EditValue == null ? "" : lkpDepartamento.EditValue.ToString();
            eDirec.cod_provincia = lkpProvincia.EditValue == null ? "" : lkpProvincia.EditValue.ToString();
            eDirec.cod_distrito = glkpDistrito.EditValue == null ? "" : glkpDistrito.EditValue.ToString();
            eDirec.dsc_telefono_1 = txtFono1Direccion.Text;
            eDirec.dsc_telefono_2 = txtFono2Direccion.Text;
            eDirec.dsc_referencia = txtReferecia.Text;
            eDirec.flg_comprobante = chkFlgComprobante.CheckState == CheckState.Checked ? "SI" : "NO";
            eDirec.flg_direccion_cobranza = chkFlgCobranza.CheckState == CheckState.Checked ? "SI" : "NO";
            
            return eDirec;
        }

        private void rbtnEliminarDireccion_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar esta dirección?", "Eliminar dirección", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCliente_Direccion eDirec = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;
                    eCliente_Contactos eContact = new eCliente_Contactos();
                    eContact = blCli.ValidacionEliminar<eCliente_Contactos>(29, cod_cliente, eDirec.num_linea);
                    if (eContact != null) { MessageBox.Show("No se puede eliminar la dirección ya que tiene contactos vinculados.", "Eliminar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    eCliente_Ubicacion eUbic = new eCliente_Ubicacion();
                    eUbic = blCli.ValidacionEliminar<eCliente_Ubicacion>(30, cod_cliente, eDirec.num_linea);
                    if (eUbic != null) { MessageBox.Show("No se puede eliminar la dirección ya que tiene ubicaciones vinculadas.", "Eliminar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    string result = blCli.Eliminar_ClienteDireccion(cod_cliente, eDirec.num_linea);
                    ObtenerListadoDirecciones();
                    if (ListDirecc.Count == 0) LimpiarCamposDireccion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevoDireccion_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposDireccion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarCamposDireccion()
        {
            txtCodDireccion.Text = "0";
            glkpTipoDireccion.EditValue = null;
            //txtNombreDireccion.Text = "";
            //glkpTipoCalle.EditValue = null;
            //glkpTipoAvenida.Text= "";
            //txtNumero.Text = "";
            //txtInterior.Text = "";
            //glkpTipoUrbanizacion.EditValue = null;
            //glkpTipoEtapa.Text = "";
            //txtManzana.Text = "";
            //txtLote.Text = "";
            //txtSubLote.Text = "";
            mmDireccion.Text = "";
           
            //lkpDepartamento.EditValue = null;
            //lkpProvincia.EditValue = null;
            //glkpDistrito.EditValue = null;
            txtFono1Direccion.Text = "";
            txtFono2Direccion.Text = "";
            txtReferecia.Text = "";
            chkFlgComprobante.CheckState = CheckState.Unchecked;
            chkFlgCobranza.CheckState = CheckState.Unchecked;

            glkpDistrito.Properties.DataSource = null;
            lkpProvincia.Properties.DataSource = null;
            lkpDepartamento.Properties.DataSource = null;
            lkpPais.EditValue = "00001";
            blCli.CargaCombosLookUp("TipoDepartamento", lkpDepartamento, "cod_departamento", "dsc_departamento", "00015", cod_condicion: "00001");
            blCli.CargaCombosLookUp("TipoProvincia", lkpProvincia, "cod_provincia", "dsc_provincia", "00128", cod_condicion: "00015");
            blCli.CargaCombosLookUp("TipoDistrito", glkpDistrito, "cod_distrito", "dsc_distrito", "", cod_condicion: "00128");
          
            glkpTipoDireccion.Focus();
        }

        private void btnEliminarDireccion_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar esta dirección?", "Eliminar dirección", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCliente_Direccion eDirec = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;
                    eCliente_Contactos eContact = new eCliente_Contactos();
                    eContact = blCli.ValidacionEliminar<eCliente_Contactos>(29, cod_cliente, eDirec.num_linea);
                    if (eContact != null) { MessageBox.Show("No se puede eliminar la dirección ya que tiene contactos vinculados.", "Eliminar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    eCliente_Ubicacion eUbic = new eCliente_Ubicacion();
                    eUbic = blCli.ValidacionEliminar<eCliente_Ubicacion>(30, cod_cliente, eDirec.num_linea);
                    if (eUbic != null) { MessageBox.Show("No se puede eliminar la dirección ya que tiene ubicaciones vinculadas.", "Eliminar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    string result = blCli.Eliminar_ClienteDireccion(cod_cliente, eDirec.num_linea);
                    ObtenerListadoDirecciones();
                    if (ListDirecc.Count == 0) LimpiarCamposDireccion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCodDireccion_TextChanged(object sender, EventArgs e)
        {
            btnEliminarDireccion.Enabled = txtCodDireccion.Text != "0" && txtCodDireccion.Text != "" ? true : false;
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MiAccion = Cliente.Nuevo;
            LimpiarCamposCliente();
            //LimpiarCamposDireccion();
            xtraTabControl1.Enabled = false;
        }
        private void LimpiarCamposCliente()
        {
            txtCodCliente.Text = "";
            chkFlgJuridica.CheckState = CheckState.Unchecked;
            txtUsuarioRegistro.Text = user.cod_usuario;
            dtFechaRegistro.EditValue = DateTime.Today;
            glkpTipoDocumento.EditValue = null;
            txtNroDocumento.Text = "";
            dtFecNacimiento.EditValue = null;
            txtApellPaterno.Text = "";
            txtApellMaterno.Text = "";
            txtNombre.Text = "";
            txtRazonSocial.Text = "";
            lkpTipoCliente.EditValue = null;
            txtFono1.Text = "";
            txtFono2.Text = "";
            //txtCodTarjeta.Text = "";
            txtCorreoPersonal.Text = "";
            txtCorreoTrabajo.Text = "";
            txtCorreoFE.Text = "";
            glkpTipoContacto.EditValue = null;
            glkpCalificacion.EditValue = null;
            glkpCategoria.EditValue = null;
            if (MiAccion == Cliente.Nuevo)
            {
                picAnteriorCliente.Enabled = false; picSiguienteCliente.Enabled = false;
            }

            bsListaDirecciones.DataSource = null; gvListaDirecciones.RefreshData();
            bsDireccionContactos.DataSource = null; gvListaDireccionContactos.RefreshData();
            bsDireccionUbicaciones.DataSource = null; tlUbicacionesDireccion.Refresh();
            LimpiarCamposDireccion();
            LimpiarCamposDireccionContacto();
            LimpiarCamposUbicacion();

            bsClienteContactos.DataSource = null; gvListaClienteContactos.RefreshData();
            bsClienteCentroResponsabilidad.DataSource = null; tlClienteCentroResponsabilidad.Refresh();
            LimpiarCamposClienteContacto();
            LimpiarCamposCentroResponsabilidad();

            btnNuevo.Enabled = false;
            glkpTipoDocumento.Focus();
        }

        private void picAnteriorCliente_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.gvListaClientes.RowCount - 1;
                int nRow = frmHandler.gvListaClientes.FocusedRowHandle;
                frmHandler.gvListaClientes.FocusedRowHandle = nRow == 0 ? tRow : nRow - 1;

                eCliente obj = frmHandler.gvListaClientes.GetFocusedRow() as eCliente;
                cod_cliente = obj.cod_cliente;
                MiAccion = Cliente.Editar;
                Editar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picSiguienteCliente_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.gvListaClientes.RowCount - 1;
                int nRow = frmHandler.gvListaClientes.FocusedRowHandle;
                frmHandler.gvListaClientes.FocusedRowHandle = nRow == tRow ? 0 : nRow + 1;

                eCliente obj = frmHandler.gvListaClientes.GetFocusedRow() as eCliente;
                cod_cliente = obj.cod_cliente;
                MiAccion = Cliente.Editar;
                Editar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tlUbicacionesDireccion_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                if (e.Node == null) { LimpiarCamposUbicacion(); return; }
                if (e.Node.Nodes != null)
                {
                    txtCodUbicacion.Text = e.Node.GetValue(colcod_ubicacion) == null ? "" : e.Node.GetValue(colcod_ubicacion).ToString();
                    chkActivoUbicacion.CheckState = e.Node.GetValue(colflg_activo) == null ? CheckState.Unchecked : e.Node.GetValue(colflg_activo).ToString() == "SI" ? CheckState.Checked : CheckState.Unchecked;
                    mmDescripcionUbicacion.Text = e.Node.GetValue(coldsc_ubicacion) == null ? "" : e.Node.GetValue(coldsc_ubicacion).ToString();
                    lkpNivelUbicacion.EditValue = e.Node.GetValue(colcod_nivel) == null ? "" : e.Node.GetValue(colcod_nivel).ToString();
                    lkpNivelSuperiorUbicacion.EditValue = e.Node.GetValue(colcod_ubicacion_sup) == null ? "" : e.Node.GetValue(colcod_ubicacion_sup).ToString();
                    lkpResponsableUbicacion.EditValue = e.Node.GetValue(colcod_contacto) == null ? "" : e.Node.GetValue(colcod_contacto).ToString();
                    mmObservacionUbicacion.Text = e.Node.GetValue(coldsc_observacion) == null ? "" : e.Node.GetValue(coldsc_observacion).ToString();
                    txtCodPerUbicacion.Text = e.Node.GetValue(colcod_ubicacion_per) == null ? "" : e.Node.GetValue(colcod_ubicacion_per).ToString();
                    //mmDireccionUbicacion.Text = e.Node.GetValue(coldsc_direccion) == null ? "" : e.Node.GetValue(coldsc_direccion).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevoUbicacion_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposUbicacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarUbicacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (mmDescripcionUbicacion.Text == "") { MessageBox.Show("Debe ingresar una descripcion", "Guardar ubicación", MessageBoxButtons.OK, MessageBoxIcon.Error); mmDescripcionUbicacion.Focus(); return; }
                if (lkpNivelUbicacion.EditValue == null) { MessageBox.Show("Debe seleccionar un nivel", "Guardar ubicación", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpNivelUbicacion.Focus(); return; }
                if (lkpNivelSuperiorUbicacion.EditValue == null && lkpNivelUbicacion.EditValue.ToString() != "N01") { MessageBox.Show("Debe seleccionar un nivel superior", "Guardar ubicación", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpNivelSuperiorUbicacion.Focus(); return; }
                //if (lkpResponsableUbicacion.EditValue == null) { MessageBox.Show("Debe seleccionar un contacto responsable", "Guardar ubicación", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpResponsableUbicacion.Focus(); return; }

                eCliente_Ubicacion eUbic = new eCliente_Ubicacion();
                eUbic = AsignarValoresUbicacion();

                if (MiAccion == Cliente.Nuevo)
                {
                    ListUbic.Add(eUbic);
                }
                else
                {
                    eUbic = blCli.Guardar_Actualizar_ClienteUbicacion<eCliente_Ubicacion>(eUbic, txtCodUbicacion.Text == "00000000" ? "Nuevo" : "Actualizar");
                }

                ObtenerListadoUbicaciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInactivarUbicacion_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de inactivar esta ubicación?", "Inactivar ubicación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCliente_Ubicacion eUbic = tlUbicacionesDireccion.GetFocusedRow() as eCliente_Ubicacion;
                    string result = blCli.Inactivar_ClienteUbicacion(cod_cliente, eUbic.cod_ubicacion);
                    ObtenerListadoUbicaciones();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private eCliente_Ubicacion AsignarValoresUbicacion()
        {
            eCliente_Ubicacion eUbic = new eCliente_Ubicacion();
            eUbic.cod_cliente = cod_cliente;
            eUbic.cod_ubicacion = txtCodUbicacion.Text;
            eUbic.flg_activo = chkActivoUbicacion.CheckState == CheckState.Checked ? "SI" : "NO";
            eUbic.dsc_ubicacion = mmDescripcionUbicacion.Text;
            eUbic.cod_nivel = lkpNivelUbicacion.EditValue.ToString();
            eUbic.cod_ubicacion_sup = lkpNivelSuperiorUbicacion.EditValue == null ? "" : lkpNivelSuperiorUbicacion.EditValue.ToString();
            eUbic.dsc_observacion = mmObservacionUbicacion.Text;
            eUbic.cod_ubicacion_per = txtCodPerUbicacion.Text;
            eUbic.cod_contacto = lkpResponsableUbicacion.EditValue == null ? 0 : Convert.ToInt32(lkpResponsableUbicacion.EditValue);

            eCliente_Direccion eDirec = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;
            eUbic.num_linea = eDirec.num_linea;

            return eUbic;
        }
        private void LimpiarCamposUbicacion()
        {
            txtCodUbicacion.Text = "00000000";
            chkActivoUbicacion.CheckState = CheckState.Checked;
            mmDescripcionUbicacion.Text = "";
            lkpNivelUbicacion.EditValue = null;
            lkpNivelSuperiorUbicacion.EditValue = null;
            mmObservacionUbicacion.Text = "";
            txtCodPerUbicacion.Text = "";
            lkpResponsableUbicacion.EditValue = null;
            mmDescripcionUbicacion.Focus();
        }

        private void lkpNivelUbicacion_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lkpNivelUbicacion.EditValue != null)
                {
                    eCliente_Direccion obj = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;
                    blCli.CargaCombosLookUp("NivelSuperiorUbicacion", lkpNivelSuperiorUbicacion, "cod_ubicacion", "dsc_ubicacion", "", cod_cliente, lkpNivelUbicacion.EditValue.ToString(), obj.num_linea);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tlUbicacionesDireccion_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            try
            {
                if (e.Node.Nodes != null) { if (e.Node.GetValue(colflg_activo).ToString() == "NO") e.Appearance.ForeColor = Color.Red; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevoClienteContacto_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposClienteContacto();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarCamposClienteContacto()
        {
            txtCodClienteContacto.Text = "0";
            txtNombreClienteContacto.Text = "";
            txtApellidoClienteContacto.Text = "";
            dtFecNacClienteContacto.EditValue = null;
            txtCorreoClienteContacto.Text = "";
            txtFono1ClienteContacto.Text = "";
            txtFono2ClienteContacto.Text = "";
            txtCargoClienteContacto.Text = "";
            txtUsuarioWebClienteContacto.Text = "";
            txtClaveWebClienteContacto.Text = "";
            mmObservacionClienteContacto.Text = "";
            txtNombreClienteContacto.Focus();
        }
        private void btnGuardarClienteContacto_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombreClienteContacto.Text == "") { MessageBox.Show("Debe ingresar un nombre", "Guardar contacto del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNombreClienteContacto.Focus(); return; }
                if (txtApellidoClienteContacto.Text == "") { MessageBox.Show("Debe ingresar un apellido", "Guardar contacto del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtApellidoClienteContacto.Focus(); return; }
                //if (dtFecNacClienteContacto.EditValue == null) { MessageBox.Show("Debe ingresar una fecha de nacimiento", "Guardar contacto del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); dtFecNacClienteContacto.Focus(); return; }
                if (txtCorreoClienteContacto.Text == "") { MessageBox.Show("Debe ingresar un correo", "Guardar contacto del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCorreoClienteContacto.Focus(); return; }
                if (txtFono1ClienteContacto.Text == "") { MessageBox.Show("Debe ingresar un teléfono", "Guardar contacto del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtFono1ClienteContacto.Focus(); return; }
                if (txtCargoClienteContacto.Text == "") { MessageBox.Show("Debe ingresar un cargo", "Guardar contacto del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCargoClienteContacto.Focus(); return; }
                //if (txtUsuarioWebClienteContacto.Text == "") { MessageBox.Show("Debe ingresar un usuario web", "Guardar contacto del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtUsuarioWebClienteContacto.Focus(); return; }
                //if (txtClaveWebClienteContacto.Text == "") { MessageBox.Show("Debe ingresar una clave web", "Guardar contacto del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtClaveWebClienteContacto.Focus(); return; }

                eCliente_Contactos eContact = new eCliente_Contactos();
                eContact = AsignarValoresClienteContacto();

                if (MiAccion == Cliente.Nuevo)
                {
                    ListClienteContacto.Add(eContact);
                }
                else
                {
                    eContact = blCli.Guardar_Actualizar_ClienteContacto<eCliente_Contactos>(eContact, txtCodClienteContacto.Text == "0" ? "Nuevo" : "Actualizar");
                }

                ObtenerListadoClientesContactos();
                blCli.CargaCombosLookUp("ResponsableCentroResponsabilidad", lkpResponsableCentroResponsabilidad, "cod_contacto", "dsc_nombre_completo", "", cod_cliente);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private eCliente_Contactos AsignarValoresClienteContacto()
        {
            eCliente_Contactos eContact = new eCliente_Contactos();
            eContact.cod_cliente = cod_cliente;
            eContact.cod_contacto = Convert.ToInt32(txtCodClienteContacto.Text);
            eContact.dsc_nombre = txtNombreClienteContacto.Text;
            eContact.dsc_apellidos = txtApellidoClienteContacto.Text;
            eContact.fch_nacimiento = dtFecNacClienteContacto.EditValue == null ? new DateTime() : Convert.ToDateTime(dtFecNacClienteContacto.EditValue);
            eContact.dsc_correo = txtCorreoClienteContacto.Text;
            eContact.dsc_telefono1 = txtFono1ClienteContacto.Text;
            eContact.dsc_telefono2 = txtFono2ClienteContacto.Text;
            eContact.dsc_cargo = txtCargoClienteContacto.Text;
            eContact.cod_usuario_web = txtUsuarioWebClienteContacto.Text;
            eContact.cod_clave_web = txtClaveWebClienteContacto.Text;
            eContact.dsc_observaciones = mmObservacionClienteContacto.Text;
            eContact.cod_usuario_reg = user.cod_usuario;
            
            return eContact;
        }
        private void btnEliminarClienteContacto_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este contacto?", "Eliminar contacto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCliente_Contactos eContact = gvListaClienteContactos.GetFocusedRow() as eCliente_Contactos;
                    eCliente_CentroResponsabilidad eCentroResp = new eCliente_CentroResponsabilidad();
                    eCentroResp = blCli.ValidacionEliminar<eCliente_CentroResponsabilidad>(27, cod_cliente, cod_contacto: eContact.cod_contacto);
                    if(eCentroResp != null) { MessageBox.Show("No se puede eliminar el contacto ya que está vinculado a un centro de responsabilidad.", "Eliminar contacto", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    string result = blCli.Eliminar_ClienteContacto(cod_cliente, eContact.cod_contacto);
                    ObtenerListadoClientesContactos();
                    if (ListClienteContacto.Count == 0) LimpiarCamposClienteContacto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevoDireccionContacto_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposDireccionContacto();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarCamposDireccionContacto()
        {
            txtCodDireccionContacto.Text = "0";
            txtNombreDireccionContacto.Text = "";
            txtApellidoDireccionContacto.Text = "";
            dtFecNacDireccionContacto.EditValue = null;
            txtCorreoDireccionContacto.Text = "";
            txtFono1DireccionContacto.Text = "";
            txtFono2DireccionContacto.Text = "";
            txtCargoDireccionContacto.Text = "";
            mmObservacionDireccionContacto.Text = "";
            txtUsuarioWebDireccionContacto.Text = "";
            txtClaveWebDireccionContacto.Text = "";
            txtNombreDireccionContacto.Focus();
        }
        private void btnGuardarDireccionContacto_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombreDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un nombre", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNombreDireccionContacto.Focus(); return; }
                if (txtApellidoDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un apellido", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtApellidoDireccionContacto.Focus(); return; }
                //if (dtFecNacDireccionContacto.EditValue == null) { MessageBox.Show("Debe ingresar una fecha de nacimiento", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); dtFecNacDireccionContacto.Focus(); return; }
                //if (txtCorreoDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un correo", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCorreoDireccionContacto.Focus(); return; }
                if (txtFono1DireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un teléfono", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtFono1DireccionContacto.Focus(); return; }
                if (txtCargoDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un cargo", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCargoDireccionContacto.Focus(); return; }
                //if (txtUsuarioWebDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un usuario web", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtUsuarioWebDireccionContacto.Focus(); return; }
                //if (txtClaveWebDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar una clave web", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtClaveWebDireccionContacto.Focus(); return; }

                eCliente_Contactos eContact = new eCliente_Contactos();
                eContact = AsignarValoresDireccionContacto();

                if (MiAccion == Cliente.Nuevo)
                {
                    ListClienteContacto.Add(eContact);
                }
                else
                {
                    eContact = blCli.Guardar_Actualizar_ClienteDireccionContacto<eCliente_Contactos>(eContact, txtCodDireccionContacto.Text == "0" ? "Nuevo" : "Actualizar");
                }

                ObtenerListadoDireccionesContactos();
                eCliente_Direccion obj = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;
                blCli.CargaCombosLookUp("ResponsableUbicacion", lkpResponsableUbicacion, "cod_contacto", "dsc_nombre_completo", "", cod_cliente, num_linea: obj.num_linea);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private eCliente_Contactos AsignarValoresDireccionContacto(eCliente_Direccion eDir = null)
        {
            eCliente_Contactos eContact = new eCliente_Contactos();
            eContact.cod_cliente = cod_cliente;
            eContact.cod_contacto = Convert.ToInt32(txtCodDireccionContacto.Text);
            eContact.dsc_nombre = txtNombreDireccionContacto.Text;
            eContact.dsc_apellidos = txtApellidoDireccionContacto.Text;
            eContact.fch_nacimiento = dtFecNacDireccionContacto.EditValue == null ? new DateTime() : Convert.ToDateTime(dtFecNacDireccionContacto.EditValue);
            eContact.dsc_correo = txtCorreoDireccionContacto.Text;
            eContact.dsc_telefono1 = txtFono1DireccionContacto.Text;
            eContact.dsc_telefono2 = txtFono2DireccionContacto.Text;
            eContact.dsc_cargo = txtCargoDireccionContacto.Text;
            eContact.dsc_observaciones = mmObservacionDireccionContacto.Text;
            eContact.cod_usuario_web = txtUsuarioWebDireccionContacto.Text;
            eContact.cod_clave_web = txtClaveWebDireccionContacto.Text;
            eContact.cod_usuario_reg = user.cod_usuario;

            eCliente_Direccion eDirec = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;
            eContact.num_linea = eDir != null ? eDir.num_linea : eDirec.num_linea;

            return eContact;
        }
        private void btnEliminarDireccionContacto_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este contacto?", "Eliminar contacto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCliente_Contactos eContact = gvListaDireccionContactos.GetFocusedRow() as eCliente_Contactos;
                    eCliente_Ubicacion eUbic = new eCliente_Ubicacion();
                    eUbic = blCli.ValidacionEliminar<eCliente_Ubicacion>(28, cod_cliente, eContact.num_linea, eContact.cod_contacto);
                    if (eUbic != null) { MessageBox.Show("No se puede eliminar el contacto ya que está vinculado a una ubicación.", "Eliminar contacto", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    string result = blCli.Eliminar_ClienteDireccionContacto(cod_cliente, eContact.num_linea, eContact.cod_contacto);
                    ObtenerListadoDireccionesContactos();
                    if (ListDireccionContacto.Count == 0) LimpiarCamposDireccionContacto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevoCentroResponsabilidad_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposCentroResponsabilidad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarCamposCentroResponsabilidad()
        {
            txtCodCentroResponsabilidad.Text = "00000";
            txtDescripcionCentroResponsabilidad.Text = "";
            lkpNivelCentroResponsabilidad.EditValue = null;
            lkpNivelSupCentroResponsabilidad.EditValue = null;
            chkActivoCentroResponsabilidad.CheckState = CheckState.Checked;
            chkConsolidadorCentroResponsabilidad.CheckState = CheckState.Unchecked;
            mmObservacionCentroResponsabilidad.Text = "";
            lkpResponsableCentroResponsabilidad.EditValue = null;
            txtDescripcionCentroResponsabilidad.Focus();
        }
        private void btnGuardarCentroResponsabilidad_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescripcionCentroResponsabilidad.Text == "") { MessageBox.Show("Debe ingresar una descripción", "Guardar centro de responsabilidad", MessageBoxButtons.OK, MessageBoxIcon.Error); txtDescripcionCentroResponsabilidad.Focus(); return; }
                if (lkpNivelCentroResponsabilidad.EditValue == null) { MessageBox.Show("Debe seleccionar un nivel", "Guardar centro de responsabilidad", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpNivelCentroResponsabilidad.Focus(); return; }
                if (lkpNivelSupCentroResponsabilidad.EditValue == null && Convert.ToInt32(lkpNivelCentroResponsabilidad.EditValue) != 1) { MessageBox.Show("Debe seleccionar un nivel superior", "Guardar centro de responsabilidad", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpNivelSupCentroResponsabilidad.Focus(); return; }
                //if (lkpResponsableCentroResponsabilidad.EditValue == null) { MessageBox.Show("Debe seleccionar un responsable", "Guardar centro de responsabilidad", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpResponsableCentroResponsabilidad.Focus(); return; }

                eCliente_CentroResponsabilidad eCentroResp = new eCliente_CentroResponsabilidad();
                eCentroResp = AsignarValoresClienteCentroResponsabilidad();

                if (MiAccion == Cliente.Nuevo)
                {
                    ListCentroResp.Add(eCentroResp);
                }
                else
                {
                    eCentroResp = blCli.Guardar_Actualizar_ClienteCentroResponsabilidad<eCliente_CentroResponsabilidad>(eCentroResp, txtCodCentroResponsabilidad.Text == "00000" ? "Nuevo" : "Actualizar");
                }

                ObtenerListadoCentroResponsabilidad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private eCliente_CentroResponsabilidad AsignarValoresClienteCentroResponsabilidad()
        {
            eCliente_CentroResponsabilidad eCentroResp = new eCliente_CentroResponsabilidad();
            eCentroResp.cod_cliente = cod_cliente;
            eCentroResp.cod_centroresp = txtCodCentroResponsabilidad.Text;
            eCentroResp.dsc_centroresp = txtDescripcionCentroResponsabilidad.Text;
            eCentroResp.num_nivel = Convert.ToInt32(lkpNivelCentroResponsabilidad.EditValue);
            eCentroResp.cod_centroresp_sup = lkpNivelSupCentroResponsabilidad.EditValue == null ? "" : lkpNivelSupCentroResponsabilidad.EditValue.ToString();
            eCentroResp.flg_activo = chkActivoCentroResponsabilidad.CheckState == CheckState.Checked ? "SI" : "NO";
            eCentroResp.dsc_observaciones = mmObservacionCentroResponsabilidad.Text;
            eCentroResp.cod_contacto = lkpResponsableCentroResponsabilidad.EditValue == null ? 0 :Convert.ToInt32(lkpResponsableCentroResponsabilidad.EditValue);
            eCentroResp.flg_consolidador = chkConsolidadorCentroResponsabilidad.CheckState == CheckState.Checked ? "SI" : "NO";

            return eCentroResp;
        }

        private void btnInactivarCentroResponsabilidad_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de inactivar este centro de responsabilidad?", "Inactivar centro de responsabilidad", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCliente_CentroResponsabilidad eCentroResp = tlClienteCentroResponsabilidad.GetFocusedRow() as eCliente_CentroResponsabilidad;
                    string result = blCli.Inactivar_ClienteCentroResponsabilidad(cod_cliente, eCentroResp.cod_centroresp);
                    ObtenerListadoCentroResponsabilidad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tlClienteCentroResponsabilidad_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            try
            {
                if (e.Node.Nodes != null) if (e.Node.GetValue(colflg_activo).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch
            {
                //txtCodCentroResponsabilidad.Text = "00000";
                //MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tlClienteCentroResponsabilidad_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                if (e.Node == null) { LimpiarCamposUbicacion(); return; }
                if (e.Node.Nodes != null)
                {
                    txtCodCentroResponsabilidad.Text = e.Node.GetValue(colcod_centroresp) == null ? "" : e.Node.GetValue(colcod_centroresp).ToString();
                    chkActivoCentroResponsabilidad.CheckState = e.Node.GetValue(colflg_activo1) == null ? CheckState.Unchecked : e.Node.GetValue(colflg_activo1).ToString() == "SI" ? CheckState.Checked : CheckState.Unchecked;
                    txtDescripcionCentroResponsabilidad.Text = e.Node.GetValue(coldsc_centroresp) == null ? "" : e.Node.GetValue(coldsc_centroresp).ToString();
                    lkpNivelCentroResponsabilidad.EditValue = e.Node.GetValue(colnum_nivel) == null ? 0 : Convert.ToInt32(e.Node.GetValue(colnum_nivel));
                    lkpNivelSupCentroResponsabilidad.EditValue = e.Node.GetValue(colcod_centroresp_sup) == null ? "" : e.Node.GetValue(colcod_centroresp_sup).ToString();
                    lkpResponsableCentroResponsabilidad.EditValue = e.Node.GetValue(colcod_contacto1) == null ? 0 : Convert.ToInt32(e.Node.GetValue(colcod_contacto1));
                    mmObservacionCentroResponsabilidad.Text = e.Node.GetValue(coldsc_observaciones) == null ? "" : e.Node.GetValue(coldsc_observaciones).ToString();
                    chkConsolidadorCentroResponsabilidad.CheckState = e.Node.GetValue(colflg_consolidador) == null ? CheckState.Unchecked : e.Node.GetValue(colflg_consolidador).ToString() == "SI" ? CheckState.Checked : CheckState.Unchecked;
                }
            }
            catch (Exception ex)
            {
                txtCodCentroResponsabilidad.Text = "00000";
               // MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lkpNivelCentroResponsabilidad_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lkpNivelCentroResponsabilidad.EditValue != null)
                {
                    blCli.CargaCombosLookUp("NivelSuperiorCentroResponsabilidad", lkpNivelSupCentroResponsabilidad, "cod_centroresp", "dsc_centroresp", "", cod_cliente, num_nivel: lkpNivelCentroResponsabilidad.EditValue == null ? 0 : Convert.ToInt32(lkpNivelCentroResponsabilidad.EditValue));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvListaDireccionContactos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eCliente_Contactos obj = gvListaDireccionContactos.GetRow(e.FocusedRowHandle) as eCliente_Contactos;
                    eCliente_Contactos eContact = new eCliente_Contactos();
                    eContact = blCli.ObtenerDireccionContacto<eCliente_Contactos>(10, cod_cliente, obj.num_linea, obj.cod_contacto);

                    txtCodDireccionContacto.Text = eContact.cod_contacto.ToString();
                    txtNombreDireccionContacto.Text = eContact.dsc_nombre;
                    txtApellidoDireccionContacto.Text = eContact.dsc_apellidos;
                    //dtFecNacDireccionContacto.EditValue = eContact.fch_nacimiento;
                    if (eContact.fch_nacimiento.ToString().Contains("1/01/0001")) { dtFecNacDireccionContacto.EditValue = null; } else { dtFecNacDireccionContacto.EditValue = Convert.ToDateTime(eContact.fch_nacimiento); }
                    txtCorreoDireccionContacto.Text = eContact.dsc_correo;
                    txtFono1DireccionContacto.Text = eContact.dsc_telefono1;
                    txtFono2DireccionContacto.Text = eContact.dsc_telefono2;
                    mmObservacionDireccionContacto.Text = eContact.dsc_observaciones;
                    txtUsuarioWebDireccionContacto.Text = eContact.cod_usuario_web;
                    txtClaveWebDireccionContacto.Text = eContact.cod_clave_web;
                    txtCargoDireccionContacto.Text = eContact.dsc_cargo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvListaDireccionContactos_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                gvListaDireccionContactos_FocusedRowChanged(gvListaDireccionContactos, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, e.RowHandle));
            }
        }

        private void gvListaClienteContactos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eCliente_Contactos obj = gvListaClienteContactos.GetRow(e.FocusedRowHandle) as eCliente_Contactos;
                    eCliente_Contactos eContact = new eCliente_Contactos();
                    eContact = blCli.ObtenerClienteContacto<eCliente_Contactos>(8, cod_cliente, cod_contacto: obj.cod_contacto);

                    txtCodClienteContacto.Text = eContact.cod_contacto.ToString();
                    txtNombreClienteContacto.Text = eContact.dsc_nombre;
                    txtApellidoClienteContacto.Text = eContact.dsc_apellidos;
                    //dtFecNacClienteContacto.EditValue = eContact.fch_nacimiento;
                    if (eContact.fch_nacimiento.ToString().Contains("1/01/0001")) { dtFecNacClienteContacto.EditValue = null; } else { dtFecNacClienteContacto.EditValue = Convert.ToDateTime(eContact.fch_nacimiento); }
                    txtCorreoClienteContacto.Text = eContact.dsc_correo;
                    txtFono1ClienteContacto.Text = eContact.dsc_telefono1;
                    txtFono2ClienteContacto.Text = eContact.dsc_telefono2;
                    txtUsuarioWebClienteContacto.Text = eContact.cod_usuario_web;
                    txtClaveWebClienteContacto.Text = eContact.cod_clave_web;
                    mmObservacionClienteContacto.Text = eContact.dsc_observaciones;
                    txtCargoClienteContacto.Text = eContact.dsc_cargo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvListaClienteContactos_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                gvListaClienteContactos_FocusedRowChanged(gvListaClienteContactos, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, e.RowHandle));
            }
        }

        private void rbtnEliminarDireccionContacto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este contacto?", "Eliminar contacto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCliente_Contactos eContact = gvListaDireccionContactos.GetFocusedRow() as eCliente_Contactos;
                    eCliente_Ubicacion eUbic = new eCliente_Ubicacion();
                    eUbic = blCli.ValidacionEliminar<eCliente_Ubicacion>(28, cod_cliente, eContact.num_linea, eContact.cod_contacto);
                    if (eUbic != null) { MessageBox.Show("No se puede eliminar el contacto ya que está vinculado a una ubicación.", "Eliminar contacto", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    string result = blCli.Eliminar_ClienteDireccionContacto(cod_cliente, eContact.num_linea, eContact.cod_contacto);
                    ObtenerListadoDireccionesContactos();
                    if (ListDireccionContacto.Count == 0) LimpiarCamposDireccionContacto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbtnEliminarClienteContacto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este contacto?", "Eliminar contacto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCliente_Contactos eContact = gvListaClienteContactos.GetFocusedRow() as eCliente_Contactos;
                    eCliente_CentroResponsabilidad eCentroResp = new eCliente_CentroResponsabilidad();
                    eCentroResp = blCli.ValidacionEliminar<eCliente_CentroResponsabilidad>(27, cod_cliente, cod_contacto: eContact.cod_contacto);
                    if(eCentroResp != null) { MessageBox.Show("No se puede eliminar el contacto ya que está vinculado a un centro de responsabilidad.", "Eliminar contacto", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    string result = blCli.Eliminar_ClienteContacto(cod_cliente, eContact.cod_contacto);
                    ObtenerListadoClientesContactos();
                    if (ListClienteContacto.Count == 0) LimpiarCamposClienteContacto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picBuscarVendedor_Click(object sender, EventArgs e)
        {
            try
            {
                frmBusquedaVendedor frm = new frmBusquedaVendedor();
                frm.ShowDialog();
                if (frm.codigo == "") { return; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtFecNacimiento_EditValueChanged(object sender, EventArgs e)
        {
            //if (dtFecNacimiento.EditValue != null) {
            //int Anho = DateTime.Now.Year- Convert.ToDateTime(dtFecNacimiento.EditValue).Year;

            //if (DateTime.Today < Convert.ToDateTime(dtFecNacimiento.EditValue).AddYears(Anho))
            //{
            //    txtEdad.Text = (Anho - 1).ToString();
            //}
            //else {
            //    txtEdad.Text = (Anho).ToString();
            //}
            //}
        }

        private void lkpPais_EditValueChanged(object sender, EventArgs e)
        {
            glkpDistrito.Properties.DataSource = null;
            lkpProvincia.Properties.DataSource = null;
            lkpDepartamento.Properties.DataSource = null;
            
            blCli.CargaCombosLookUp("TipoDepartamento", lkpDepartamento, "cod_departamento", "dsc_departamento", "",cod_condicion:lkpPais.EditValue.ToString());
            lkpDepartamento.EditValue = "00015";


        }
        private void lkpDepartamento_EditValueChanged(object sender, EventArgs e)
        {
            glkpDistrito.Properties.DataSource = null;
            lkpProvincia.Properties.DataSource = null;

            blCli.CargaCombosLookUp("TipoProvincia", lkpProvincia, "cod_provincia", "dsc_provincia", "", cod_condicion: lkpDepartamento.EditValue.ToString());
            lkpProvincia.EditValue = "00128";
        }

        private void lkpProvincia_EditValueChanged(object sender, EventArgs e)
        {
            glkpDistrito.Properties.DataSource = null;
            blCli.CargaCombosLookUp("TipoDistrito", glkpDistrito, "cod_distrito", "dsc_distrito", "", cod_condicion: lkpProvincia.EditValue.ToString());
        }

        private void glkpTipoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            chkFlgJuridica.Checked = glkpTipoDocumento.EditValue != null && glkpTipoDocumento.EditValue.ToString() == "DI004" ? true : false;
        }

        private void btnUbicacionMasiva_Click(object sender, EventArgs e)
        {
            if (lkpNivelUbicacion.EditValue == null) { MessageBox.Show("Debe seleccionar un nivel.", "Carga ubicación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (lkpNivelSuperiorUbicacion.EditValue == null) { MessageBox.Show("Debe seleccionar un nivel superior.", "Carga ubicación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            eCliente_Direccion eDirec = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;
            frmCargaMasivaUbicaciones frm = new frmCargaMasivaUbicaciones();
            frm.cod_cliente = cod_cliente;
            frm.num_linea = eDirec.num_linea;
            frm.cod_nivel = lkpNivelUbicacion.EditValue.ToString();
            frm.cod_ubicacion_sup = lkpNivelSuperiorUbicacion.EditValue.ToString();
            frm.dsc_larga_ubicacion = lkpNivelSuperiorUbicacion.Text;
            frm.cod_contacto = lkpResponsableUbicacion.EditValue == null ? null : lkpResponsableUbicacion.EditValue.ToString();
            frm.ShowDialog();
            if (frm.ActualizarListado == "SI") ObtenerListadoUbicaciones();
        }

        private void btnExportarUbicaciones_Click(object sender, EventArgs e)
        {
            ExportarExcel();
        }
        private void ExportarExcel()
        {
            try
            {
                string carpeta = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\DireccionUbicacion" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                tlUbicacionesDireccion.ExportToXlsx(archivo);
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

        private void gvListaDirecciones_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListaDirecciones_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListaDireccionContactos_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListaClienteContactos_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
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

        private void gvEmpresasVinculadas_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvEmpresasVinculadas_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                eCliente_Empresas eCliEmp = new eCliente_Empresas();
                eCliente_Empresas obj = gvEmpresasVinculadas.GetRow(e.RowHandle) as eCliente_Empresas;

                if ((e.Column.FieldName == "valorRating" || e.Column.FieldName == "dsc_pref_ceco") && !obj.Seleccionado) { MessageBox.Show("Debe vincular la empresa para poder calificarla.", "Calificar empresa", MessageBoxButtons.OK, MessageBoxIcon.Information); obj.valorRating = 0; return; }
                if (e.Column.FieldName == "Seleccionado" || e.Column.FieldName == "valorRating" || e.Column.FieldName == "dsc_pref_ceco")
                {
                    obj.cod_cliente = cod_cliente; obj.flg_activo = obj.Seleccionado ? "SI" : "NO"; obj.cod_usuario_registro = user.cod_usuario;
                    eCliEmp = blCli.Guardar_Actualizar_ClienteEmpresas<eCliente_Empresas>(obj);
                    if (eCliEmp == null) { MessageBox.Show("Error al vincular empresa", "Vincular empresa", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rchkSeleccionado_CheckedChanged(object sender, EventArgs e)
        {
            gvEmpresasVinculadas.PostEditor();
        }

        private void rRatingCalificacion_EditValueChanged(object sender, EventArgs e)
        {
            gvEmpresasVinculadas.PostEditor();
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
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic blogObject = js.Deserialize<dynamic>(response.Content);

            if (glkpTipoDocumento.EditValue.ToString() == "DI004")
            {
                txtRazonSocial.Text = blogObject["nombre_o_razon_social"];
                txtNombreComercial.Text = blogObject["nombre_o_razon_social"];
                mmDireccion.Text = blogObject["direccion_completa"];

                if (mmDireccion.Text.Trim() != "")
                {
                    glkpTipoDireccion.EditValue = "TD001";

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
                    char[] separadores = { ',' };
                    string[] palabras = Datos.Replace("\"", "").Replace("{", "").Replace("}", "").Split(separadores);

                    if (glkpTipoDocumento.EditValue.ToString() == "DI004")
                    {
                        txtRazonSocial.Text = palabras[0].Substring(palabras[0].IndexOf(":") + 1, palabras[0].Length - palabras[0].IndexOf(":") - 1).ToUpper();
                        txtNombreComercial.Text = palabras[0].Substring(palabras[0].IndexOf(":") + 1, palabras[0].Length - palabras[0].IndexOf(":") - 1).ToUpper();
                        mmDireccion.Text = palabras[5].Substring(palabras[5].IndexOf(":") + 1, palabras[5].Length - palabras[5].IndexOf(":") - 1).ToUpper();
                        txtNombreDireccion.Text = "OFICINA PRINCIPAL";

                        if (mmDireccion.Text.Trim() != "")
                        {
                            glkpTipoDireccion.EditValue = "TD001";
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
            string endpoint = @"http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc=" + txtNroDocumento.Text.Trim() + "&codigo= " + texto.ToUpper() + "&tipdoc=1";

            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(endpoint);
            myWebRequest.CookieContainer = cokkie;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            HttpWebResponse myhttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            Stream myStream = myhttpWebResponse.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myStream);

            string xDat = "";
            int pos = 0;
            int pocision = 0;
            string dato = "";
            int posicionPersonaNatural = 0;
            string validar = "";

            while (!myStreamReader.EndOfStream)
            {
                xDat = myStreamReader.ReadLine();
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
                        //chkNoHabido.CheckState = estado == "habido" ? CheckState.Unchecked : CheckState.Checked;
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
                        mmDireccion.Text = direccion;

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

        string captcha = "";
        CookieContainer cokkie = new CookieContainer();
        string[] nrosRuc = new string[] { };
        string texto = "";
        public void ObtenerCap()
        {

            try
            {
                ///////https://cors-anywhere.herokuapp.com/wmtechnology.org/Consultar-RUC/?modo=1&btnBuscar=Buscar&nruc=
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=image");
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
            string direccion = mmDireccion.Text;
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
        private void gvEmpresasVinculadas_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            bool estado = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, view.Columns["Seleccionado"]));
            if (estado) e.Appearance.ForeColor = Color.Blue;
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
                if (MiAccion == Cliente.Nuevo)
                {
                    if (txtNroDocumento.Text.Trim() == "" || chkProspecto.Checked) return;
                    eCliente obj = new eCliente();
                    obj = blCli.Validar_NroDocumento<eCliente>(37, txtNroDocumento.Text.Trim());
                    if (obj != null)
                    {
                        if (MessageBox.Show("El número de documento ingresado ya se encuentra registrado en el sistema." + Environment.NewLine +
                                        "¿Desea visualizar la información del cliente?", //y vincularlo a su empresa?", 
                                        "Validar número de documento", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            cod_cliente = obj.cod_cliente;
                            MiAccion = Cliente.Editar;
                            Editar();
                            BloqueoControles(true, false, true);
                            xtabDireccionCliente.PageEnabled = true;
                            xtabDetalleDireccion.Enabled = true;
                            xtabContactoCliente.PageEnabled = true;
                            xtabContactosDireccion.PageEnabled = true;
                            xtabUbicacionesDireccion.PageEnabled = true;
                            xtabCentroResponsabilidad.PageEnabled = true;
                            xtabEmpresasVinculadas.PageEnabled = true;
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

        private void btnProspecto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (chkProspecto.CheckState == CheckState.Unchecked)
            {
                chkProspecto.CheckState = CheckState.Checked;
            }
            else
            {
                chkProspecto.CheckState = CheckState.Unchecked;
            }
        }

        private void ConfigurarProspecto()
        {
            if (chkProspecto.CheckState == CheckState.Checked)
            {
                chkProspecto.Properties.Appearance.Options.UseFont = true;
                chkProspecto.Properties.Appearance.Options.UseForeColor = true;
                chkProspecto.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                chkProspecto.Properties.Appearance.ForeColor = System.Drawing.Color.Red;

                AsteriscoUno.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                AsteriscoDos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                AsteriscoTres.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                AsteriscoCuatro.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                espacioUno.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                txtNroDocumento.EditValue = "POR DEFINIR";
            }
            else
            {
                chkProspecto.Properties.Appearance.Options.UseFont = true;
                chkProspecto.Properties.Appearance.Options.UseForeColor = true;
                chkProspecto.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
                chkProspecto.Properties.Appearance.ForeColor = System.Drawing.Color.Black;

                AsteriscoUno.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                AsteriscoDos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                AsteriscoTres.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                AsteriscoCuatro.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                espacioUno.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                txtNroDocumento.EditValue = "";
            }
        }

        private void chkProspecto_EditValueChanged(object sender, EventArgs e)
        {
            ConfigurarProspecto();
        }

        private void chkCodigoManual_CheckStateChanged(object sender, EventArgs e)
        {
            if (MiAccion == Cliente.Nuevo) txtCodCliente.Enabled = chkCodigoManual.CheckState == CheckState.Checked ? true : false;
        }

        private void gvListaClienteContactos_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListaDireccionContactos_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void tlUbicacionesDireccion_CustomDrawColumnHeader(object sender, DevExpress.XtraTreeList.CustomDrawColumnHeaderEventArgs e)
        {
            
        }
    }
}