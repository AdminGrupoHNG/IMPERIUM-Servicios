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

namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    internal enum EntregaRendir
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmEntregaRendir : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        internal EntregaRendir MiAccion = EntregaRendir.Nuevo;
        blProveedores blProv = new blProveedores();
        blFactura blFact = new blFactura();
        blCajaChica blCaja = new blCajaChica();
        blTrabajador blTrab = new blTrabajador();
        blGlobales blGlobal = new blGlobales();
        public eEntregaRendir eEntrega = new eEntregaRendir();
        public string cod_entregarendir = "", cod_empresa = "", cod_sede_empresa = "";
        public string ActualizarListado = "NO";
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        
        public frmEntregaRendir()
        {
            InitializeComponent();
        }

        private void frmEntregaRendir_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            try
            {
                CargarLookUpEdit();
                lkpEmpresa.EditValue = cod_empresa;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargarLookUpEdit()
        {
            try
            {
                blFact.CargaCombosLookUp("EmpresaProveedor", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true);
                blProv.CargaCombosLookUp("Moneda", lkpTipoMoneda, "cod_moneda", "dsc_moneda", "", valorDefecto: true);
                blCaja.CargaCombosLookUp("ModoReposicion", lkpModoReposicion, "cod_modalidad", "dsc_modalidad", "", valorDefecto: true);
                dtFecCreacion.EditValue = DateTime.Today;
                List<eProveedor_Empresas> listEmpresasUsuario = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(11, "", user.cod_usuario);
                lkpEmpresa.EditValue = listEmpresasUsuario[0].cod_empresa;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtResponsable_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                blGlobal.pKeyDown(txtResponsable, e);
                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) txtResponsable.Tag = null; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtResponsable_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Busqueda("", "Responsable");
            }
            string dato = blGlobal.pKeyPress(txtResponsable, e);
            if (dato != "")
            {
                Busqueda(dato, "Responsable");
            }
        }

        private void frmEntregaRendir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && MiAccion == EntregaRendir.Editar) this.Close();
        }

        private void picResponsable_Click(object sender, EventArgs e)
        {
            Busqueda("", "Responsable");
        }

        public void Busqueda(string dato, string tipo)
        {
            if (lkpEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar una empresa", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            frmBusquedas frm = new frmBusquedas();
            frm.user = user;
            frm.filtro = dato;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            switch (tipo)
            {
                case "Responsable":
                    frm.entidad = frmBusquedas.MiEntidad.Trabajador;
                    frm.cod_empresa = lkpEmpresa.EditValue.ToString();
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Responsable":
                    txtResponsable.Tag = frm.codigo;
                    txtResponsable.Text = frm.descripcion;
                    eTrabajador.eInfoLaboral_Trabajador obj = new eTrabajador.eInfoLaboral_Trabajador();
                    obj = blTrab.Obtener_Trabajador<eTrabajador.eInfoLaboral_Trabajador>(5, frm.codigo, lkpEmpresa.EditValue.ToString());
                    txtUbicacion.Text = obj.dsc_empresa + " - " + obj.dsc_sede_empresa;
                    txtUbicacion.Tag = obj.cod_sede_empresa;
                    cod_empresa = obj.cod_empresa; cod_sede_empresa = obj.cod_sede_empresa;
                    break;
            }
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MiAccion = EntregaRendir.Nuevo;
                txtCodEntregaRendir.Text = "";
                cod_entregarendir = "";
                lkpEmpresa.EditValue = null;
                dtFecCreacion.EditValue = DateTime.Today;
                txtResponsable.Tag = "";
                txtResponsable.Text = "";
                txtUbicacion.Text = "";
                txtMontoTotal.EditValue = 0;
                lkpTipoMoneda.EditValue = null;
                lkpModoReposicion.EditValue = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                txtCodEntregaRendir.Select();
                if (dtFecCreacion.EditValue == null) { MessageBox.Show("Debe seleccionar una fecha", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtFecCreacion.Focus(); return; }
                if (txtResponsable.Text.Trim() == "") { MessageBox.Show("Debe seleccionar un responsable.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtResponsable.Focus(); return; }
                if (lkpTipoMoneda.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de moneda.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoMoneda.Focus(); return; }
                if (Convert.ToDecimal(txtMontoTotal.EditValue) == 0) { MessageBox.Show("El importe debe ser mayor a 0.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtMontoTotal.Focus(); return; }
                if (lkpModoReposicion.EditValue == null) { MessageBox.Show("Debe seleccionar un modo de reposición.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpModoReposicion.Focus(); return; }

                eEntrega = AsignarValores();
                eEntrega = blCaja.InsertarActualizar_EntregasRendir<eEntregaRendir>(eEntrega);
                if (eEntrega == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                cod_entregarendir = eEntrega.cod_entregarendir; txtCodEntregaRendir.Text = eEntrega.cod_entregarendir;
                MiAccion = EntregaRendir.Editar;

                if (eEntrega != null) { MessageBox.Show("Se registraron los datos de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private eEntregaRendir AsignarValores()
        {
            eEntregaRendir obj = new eEntregaRendir();
            obj.cod_entregarendir = txtCodEntregaRendir.Text;
            obj.fch_creacion = Convert.ToDateTime(dtFecCreacion.EditValue);
            obj.cod_entregado_a = txtResponsable.Tag.ToString();
            obj.cod_empresa = cod_empresa;
            obj.cod_sede_empresa = txtUbicacion.Tag.ToString();
            obj.cod_moneda = lkpTipoMoneda.EditValue.ToString();
            obj.imp_monto = Convert.ToDecimal(txtMontoTotal.EditValue);
            obj.cod_modalidad = lkpModoReposicion.Text;
            obj.cod_usuario_registro = user.cod_usuario;

            return obj;
        }

    }
}