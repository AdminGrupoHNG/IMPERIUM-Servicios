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
using DevExpress.XtraLayout.Utils;

namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    public partial class frmOpcionesReembolso : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        blCajaChica blCaja = new blCajaChica();
        public string cod_empresa = "", cod_sede_empresa = "", cod_entregado_a = "", dsc_entregado_a = "", dsc_observacion = "", cod_entregarendir = "";
        public decimal imp_entregado = 0;
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public string ActualizarListado = "NO";
        //public int cod_opcion = 0;

        public frmOpcionesReembolso()
        {
            InitializeComponent();
        }

        private void frmOpcionesReembolso_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            grdbOpcionesReembolso_SelectedIndexChanged(grdbOpcionesReembolso, new EventArgs());
            if (cod_sede_empresa != "")
            {
                blCaja.CargaCombosLookUp("Responsable", lkpResponsable, "cod_responsable", "dsc_responsable", "", valorDefecto: true, cod_empresa: cod_empresa, cod_sede_empresa: cod_sede_empresa);
                lkpResponsable.EditValue = null; lkpTipoCaja.EditValue = null;
                List<eTrabajador> lista = new List<eTrabajador>();
                lista = blCaja.ListarDatos_CajaChica<eTrabajador>(12, "", "", cod_empresa: cod_empresa, cod_sede_empresa: cod_sede_empresa);
                if (lista.Count == 1) lkpResponsable.EditValue = lista[0].cod_trabajador;
            }
        }

        private void lkpResponsable_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpResponsable.EditValue != null)
            {
                blCaja.CargaCombosLookUp("TipoCajaResponsable", lkpTipoCaja, "cod_caja", "dsc_caja", "", valorDefecto: true, cod_responsable: lkpResponsable.EditValue.ToString(), cod_empresa: cod_empresa, cod_sede_empresa: cod_sede_empresa);
                lkpTipoCaja.EditValue = null;
                List<eCajaChica> lista = new List<eCajaChica>();
                lista = blCaja.ListarDatos_CajaChica<eCajaChica>(13, "", "", cod_responsable: lkpResponsable.EditValue.ToString());
                if (lista.Count == 1) lkpTipoCaja.EditValue = lista[0].cod_caja;
            }
        }
        private void lkpTipoCaja_EditValueChanged(object sender, EventArgs e)
        {
            txtMontoSaldo.EditValue = 0; 
            if (lkpTipoCaja.EditValue != null)
            {
                ObtenerMovimientos();
                txtMontoSaldo.ForeColor = Color.DarkGreen;
            }
        }
        private void ObtenerMovimientos()
        {
            decimal imp_Caja = 0;
            eCajaChica obj = blCaja.ObtenerDatos_CajaChica<eCajaChica>(10, lkpTipoCaja.EditValue.ToString());
            if (obj == null) return;
            imp_Caja = obj.imp_monto;

            List<eCajaChica.eMovimiento_CajaChica> listMovimientos = blCaja.ListarDatos_CajaChica<eCajaChica.eMovimiento_CajaChica>(3, lkpTipoCaja.EditValue.ToString(), "");
            List<eCajaChica.eMovimiento_CajaChica> listPreRendicion = blCaja.ListarDatos_CajaChica<eCajaChica.eMovimiento_CajaChica>(6, lkpTipoCaja.EditValue.ToString(), "");
            DateTime fch_registro = new DateTime();
            fch_registro = Convert.ToDateTime((from tabla in listPreRendicion
                                               where tabla.cod_tipo == "RP" || tabla.cod_tipo == "AP" //&& tabla.cod_estado == "REN"
                                               select tabla.fch_registro).First());

            decimal imp_salida_REN = (from tabla in listMovimientos
                                      where tabla.cod_tipo == "SA" && tabla.fch_registro >= fch_registro //&& tabla.cod_estado == "REN"
                                      select tabla.imp_entregado).Sum();
            decimal imp_devolucion_REN = (from tabla in listMovimientos
                                          where tabla.cod_tipo == "DV" && tabla.fch_registro >= fch_registro //&& tabla.cod_estado == "REN"
                                          select tabla.imp_entregado).Sum();
            decimal imp_reembolso_REN = (from tabla in listMovimientos
                                         where tabla.cod_tipo == "RB" && tabla.fch_registro >= fch_registro //&& tabla.cod_estado == "REN"
                                         select tabla.imp_entregado).Sum();
            decimal imp_saldo = imp_Caja - imp_salida_REN + imp_devolucion_REN - imp_reembolso_REN;
            txtMontoSaldo.EditValue = imp_saldo;
        }
        private void grdbOpcionesReembolso_SelectedIndexChanged(object sender, EventArgs e)
        {
            layoutControlItem9.Visibility = grdbOpcionesReembolso.SelectedIndex == 0 ? LayoutVisibility.Always : LayoutVisibility.Never;
            layoutControlItem12.Visibility = grdbOpcionesReembolso.SelectedIndex == 0 ? LayoutVisibility.Always : LayoutVisibility.Never;
            layoutControlItem13.Visibility = grdbOpcionesReembolso.SelectedIndex == 0 ? LayoutVisibility.Always : LayoutVisibility.Never;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (grdbOpcionesReembolso.SelectedIndex == 0)
            {
                if (lkpTipoCaja.EditValue == null) { MessageBox.Show("Debe seleccionar una caja para poder registrar un reembolso", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                frmDetalleMovimiento frm = new frmDetalleMovimiento();
                frm.MiAccion = DetMovimiento.EntregaRendir;
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.cod_empresa = cod_empresa;
                frm.cod_caja = lkpTipoCaja.EditValue.ToString();
                frm.cod_entregado_a = cod_entregado_a;
                frm.dsc_entregado_a = dsc_entregado_a;
                frm.imp_entregado = imp_entregado;
                frm.cod_tipo = "SA";
                frm.dsc_observacion = dsc_observacion;
                frm.EntregaRendir = "SI";
                frm.cod_entregarendir = cod_entregarendir;
                frm.ShowDialog();
                if (frm.ActualizarListado == "SI") { ActualizarListado = "SI"; this.Close(); }
            }
            else
            {
                frmDetalleEntregaRendir frm = new frmDetalleEntregaRendir();
                frm.MiAccion = DetEntregaRendir.DevRemb;
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.cod_empresa = cod_empresa;
                frm.cod_entregado_a = cod_entregado_a;
                frm.dsc_entregado_a = dsc_entregado_a;
                frm.imp_entregado = imp_entregado;
                frm.cod_vinculo = cod_entregarendir;
                frm.cod_tipo = "RB";
                frm.ShowDialog();
                if (frm.ActualizarListado == "SI") { ActualizarListado = "SI"; this.Close(); }
            }
        }

    }
}