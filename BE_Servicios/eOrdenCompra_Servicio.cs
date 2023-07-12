using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eOrdenCompra_Servicio
    {
        public string cod_empresa { get; set; }
        public string dsc_empresa { get; set; }
        public string cod_sede_empresa { get; set; }
        public string dsc_sede_empresa { get; set; }
        public string cod_orden_compra_servicio { get; set; }
        public string num_cotizacion { get; set; }
        public string cod_proveedor { get; set; }
        public string dsc_proveedor { get; set; }
        public string dsc_ruc { get; set; }
        public DateTime fch_emision { get; set; }
        public string cod_almacen { get; set; }
        public string dsc_unidad_recepcion { get; set; }
        public decimal imp_subtotal { get; set; }
        public decimal imp_igv { get; set; }
        public decimal imp_total { get; set; }
        public string dsc_imp_total { get; set; }
        public string dsc_terminos_condiciones { get; set; }
        public string dsc_direccion_despacho { get; set; }
        public string cod_modalidad_pago { get; set; }
        public string dsc_modalidad_pago { get; set; }
        public DateTime fch_despacho { get; set; }
        public string cod_moneda { get; set; }
        public string cod_estado_orden { get; set; }
        public string flg_solicitud { get; set; }
        public decimal prc_CV { get; set; }
        public decimal prc_LI { get; set; }
        public decimal prc_CB { get; set; }
        public decimal prc_GG { get; set; }
        public decimal prc_ADM { get; set; }
        public decimal prc_OPER { get; set; }
        public decimal prc_GV { get; set; }
        public string dsc_observaciones { get; set; }
        public DateTime fch_aprobacion { get; set; }
        public string cod_usuario_aprobacion { get; set; }
        public string dsc_usuario_aprobacion { get; set; }
        public DateTime fch_envio { get; set; }
        public string cod_usuario_envio { get; set; }
        public string dsc_usuario_envio { get; set; }
        public DateTime fch_atencion { get; set; }
        public string cod_usuario_atencion { get; set; }
        public string dsc_usuario_atencion { get; set; }
        public DateTime fch_liquidacion { get; set; }
        public string cod_usuario_liquidacion { get; set; }
        public string dsc_usuario_liquidacion { get; set; }
        public DateTime fch_desaprobacion { get; set; }
        public string cod_usuario_desaprobacion { get; set; }
        public string dsc_usuario_desaprobacion { get; set; }
        public DateTime fch_registro { get; set; }
        public int dsc_anho { get; set; }
        public string cod_usuario_registro { get; set; }
        public string dsc_usuario { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
        public string dsc_usuario_cam { get; set; }
        public DateTime fch_anulacion { get; set; }
        public string cod_usuario_anulacion { get; set; }
        public string dsc_usuario_anulacion { get; set; }
        public string dsc_almacen { get; set; }
        public Int32 n_Orden { get; set; }
        public int ctd_Atencion { get; set; }
        public string cod_formapago { get; set; }

        public class eOrdenCompra_Servicio_Detalle : eOrdenCompra_Servicio
        {
            public bool Sel { get; set; }
            public int num_item { get; set; }
            public string cod_requerimiento { get; set; }
            public string cod_proveedor_det { get; set; }
            public string dsc_proveedor_det { get; set; }
            public string dsc_ruc_det { get; set; }
            public string cod_tipo_servicio { get; set; }
            public string dsc_tipo_servicio { get; set; }
            public string cod_subtipo_servicio { get; set; }
            public string dsc_subtipo_servicio { get; set; }
            public string cod_producto { get; set; }
            public string dsc_producto { get; set; }
            public string dsc_servicio { get; set; }
            public string cod_unidad_medida { get; set; }
            public string dsc_unidad_medida { get; set; }
            public string dsc_simbolo { get; set; }
            public int num_cantidad { get; set; }
            public int num_cantidad_req { get; set; }
            public decimal imp_unitario { get; set; }
            public decimal imp_total_det { get; set; }
            public DateTime fch_registro_det { get; set; }
            public string cod_usuario_registro_det { get; set; }
        }
    }
}