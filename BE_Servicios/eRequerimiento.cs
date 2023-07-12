using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eRequerimiento
    {
        public string cod_empresa { get; set; }
        public string dsc_empresa { get; set; }
        public string cod_sede_empresa { get; set; }
        public string dsc_sede_empresa { get; set; }
        public string cod_cliente { get; set; }
        public string dsc_razon_social { get; set; }
        public Int32 cod_sede_cliente { get; set; }
        public string dsc_sede_cliente { get; set; }
        public string cod_area { get; set; }
        public string dsc_area { get; set; }
        public string cod_requerimiento { get; set; }
        public string cod_estado_requerimiento { get; set; }
        public string dsc_nombre_solicitante { get; set; }
        public DateTime fch_requerimiento { get; set; }
        public DateTime fch_atencion { get; set; }
        public DateTime fch_atendido { get; set; }
        public string cod_usuario_atendido { get; set; }
        public string dsc_usuario_atendido { get; set; }
        public DateTime fch_aprobacion { get; set; }
        public string cod_usuario_aprobacion { get; set; }
        public string dsc_usuario_aprobacion { get; set; }
        public DateTime fch_desaprobacion { get; set; }
        public string cod_usuario_desaprobacion { get; set; }
        public string dsc_usuario_desaprobacion { get; set; }
        public string dsc_observaciones { get; set; }
        public string dsc_justificacion { get; set; }
        public string dsc_items_requeridos { get; set; }
        public string cod_tipo { get; set; }
        public string flg_solicitud { get; set; }
        public DateTime fch_registro { get; set; }
        public Int32 dsc_anho { get; set; }
        public string cod_usuario_registro { get; set; }
        public string dsc_usuario { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
        public string dsc_usuario_cam { get; set; }
        public DateTime fch_anulacion { get; set; }
        public string cod_usuario_anulacion { get; set; }
        public string dsc_usuario_anulacion { get; set; }
        public string dsc_solicitante { get; set; }
        public string dsc_estado_requerimiento { get; set; }
        public string dsc_direccion_cliente { get; set; }

        public class eRequerimiento_Detalle : eRequerimiento
        {
            public string cod_tipo_servicio { get; set; }
            public string dsc_tipo_servicio { get; set; }
            public string cod_subtipo_servicio { get; set; }
            public string dsc_subtipo_servicio { get; set; }
            public string cod_producto { get; set; }
            public string dsc_producto { get; set; }
            public Int32 num_cantidad { get; set; }
            public Int32 num_restante { get; set; }
            public string cod_proveedor { get; set; }
            public string dsc_proveedor { get; set; }
            public decimal imp_unitario { get; set; }
            public decimal imp_total { get; set; }
            public string cod_unidad_medida { get; set; }
            public string dsc_unidad_medida { get; set; }
            public string dsc_simbolo { get; set; }
            public DateTime fch_registro_det { get; set; }
            public string cod_usuario_registro_det { get; set; }
        }
    }
}
