using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eProductos
    {
        public Boolean cod_sel { get; set; }
        public string cod_tipo_servicio { get; set; }
        public string dsc_tipo_servicio { get; set; }
        public string cod_subtipo_servicio { get; set; }
        public string dsc_subtipo_servicio { get; set; }
        public string cod_producto { get; set; }
        public string dsc_producto { get; set; }
        public int ctd_stock_minimo { get; set; }
        public int ctd_stock_maximo { get; set; }
        public string dsc_observaciones { get; set; }
        public string cod_sislog { get; set; }
        public string cod_unidad_medida { get; set; }
        public decimal ctd_volumen_m3 { get; set; }
        public string cod_producto_SUNAT { get; set; }
        public string dsc_producto_SUNAT { get; set; }
        public string cod_color { get; set; }
        public string dsc_ruc { get; set; }
        public string cod_proveedor { get; set; }
        public string dsc_proveedor { get; set; }
        public string cod_marca { get; set; }
        public string dsc_marca { get; set; }
        public string cod_modelo { get; set; }
        public string dsc_modelo { get; set; }
        public decimal num_peso { get; set; }
        public string cod_tallauniforme { get; set; }
        public string cod_sexo { get; set; }
        public string flg_activo { get; set; }
        public string flg_compuesto { get; set; }
        public string flg_logo { get; set; }

        public string dsc_unidad_medida { get; set; }
        public string dsc_simbolo { get; set; }
        public int ctd_stock_actual { get; set; }
        public decimal imp_costo_total { get; set; }
        public decimal imp_costo_unitario { get; set; }
        public decimal imp_costo_actual { get; set; }
        public decimal imp_costo_ponderado { get; set; }

        public decimal cant_producto { get; set; }
        public decimal imp_subtotal { get; set; }
        public decimal imp_total { get; set; }
        public string dsc_empresas_vinculadas { get; set; }

        public string flg_materia_prima { get; set; }
        public string flg_producto_terminado { get; set; }
        public string flg_actividad_apoyo { get; set; }
        public string flg_producto { get; set; }
        public string flg_servicio { get; set; }


        public class eSubProductos : eProductos
        {
            public string sub_cod_tipo_servicio { get; set; }
            public string sub_dsc_tipo_servicio { get; set; }
            public string sub_cod_subtipo_servicio { get; set; }
            public string sub_dsc_subtipo_servicio { get; set; }
            public string sub_cod_producto { get; set; }
            public string sub_dsc_producto { get; set; }
            public decimal ctd_requerida { get; set; }
        }

        public class eProductosTarifas : eProductos
        {
            public int num_item { get; set; }
            public DateTime fch_inicio { get; set; }
            public DateTime fch_fin { get; set; }
            public decimal imp_costo { get; set; }
            public string dsc_observacion { get; set; }
        }

        public class eProductosProveedor : eProductos
        {
            public string dsc_ruc { get; set; }
            public string cod_proveedor { get; set; }
            public string flg_activo { get; set; }
            public string flg_vigente { get; set; }
            public DateTime fch_registro { get; set; }
            public string cod_usuario_registro { get; set; }
        }

        public class eProductosDetalle : eProductos
        {
            public int num_cantidad { get; set; }
            public decimal imp_costo { get; set; }
        }
    }
}
