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

namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    public partial class fmFacturasAplicarDetraccion : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        blFactura blFact = new blFactura();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public List<eFacturaProveedor> listFactura = new List<eFacturaProveedor>();

        public fmFacturasAplicarDetraccion()
        {
            InitializeComponent();
        }

        private void fmFacturasAplicarDetraccion_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            bsFacturasProveedor.DataSource = listFactura;
        }

        private void gvFacturasProveedor_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvFacturasProveedor_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void btnAplicar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int x = 0; x <= gvFacturasProveedor.RowCount; x++)
            {
                eFacturaProveedor obj = gvFacturasProveedor.GetRow(x) as eFacturaProveedor;
                eFacturaProveedor objDet = new eFacturaProveedor();
                objDet = blFact.ValidarFacturaProveedor<eFacturaProveedor>(35, obj.tipo_documento, obj.serie_documento, obj.numero_documento, obj.cod_proveedor, "DETRACC");
                if (objDet != null) continue;
                obj.flg_detraccion = "SI"; obj.cod_usuario_registro = user.cod_usuario; obj.dsc_observacion = ""; 
                //FECHA PAGO DETRACCIÓN, es el viernes proximo de la fecha de recepción
                DateTime fecha = new DateTime(Convert.ToDateTime(obj.fch_registro).Year, Convert.ToDateTime(obj.fch_registro).Month + 1, 1);
                int nDia = 0, cont = 0;
                for (int y = 0; y <= 9; y++)
                {
                    nDia = Convert.ToInt32(fecha.AddDays(y).DayOfWeek);
                    if (nDia >= 1 && nDia <= 5) cont = cont + 1;
                    if (cont == 5 && (nDia >= 1 && nDia <= 5)) obj.fch_constancia_detraccion = fecha.AddDays(y);
                }
                string result = blFact.AplicarDetraccionMasiva(obj, "DETRACC");
                if (result != "OK") { MessageBox.Show("Error al aplicar detracción", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); continue; }

                eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                eProgFact.tipo_documento = obj.tipo_documento; eProgFact.serie_documento = obj.serie_documento; eProgFact.numero_documento = obj.numero_documento;
                eProgFact.cod_proveedor = obj.cod_proveedor;
                eProgFact.num_linea = 0; eProgFact.fch_pago = obj.fch_constancia_detraccion;
                eProgFact.imp_pago = obj.imp_detraccion; eProgFact.cod_pagar_a = "PROV";
                eProgFact.dsc_observacion = "DETRACCIÓN"; eProgFact.cod_estado = "EJE";
                eProgFact.cod_tipo_prog = "DETRACC";
                eProgFact.cod_usuario_ejecucion = user.cod_usuario; eProgFact.cod_usuario_registro = user.cod_usuario;
                eProgFact.fch_ejecucion = obj.fch_constancia_detraccion; eProgFact.cod_empresa = obj.cod_empresa;
                eProgFact = blFact.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(eProgFact);
            }
        }
    }
}   