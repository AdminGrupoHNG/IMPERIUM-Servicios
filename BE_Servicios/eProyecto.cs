using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eProyecto
    {
        public string cod_empresa { get; set; }
        public string cod_proyecto { get; set; }
        public string dsc_proyecto { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
        public string flg_activo { get; set; }


        public class eProyecto_Tipo_Servicio : eProyecto
        {
            public string cod_tipo_servicio { get; set; }
            public string dsc_tipo_servicio { get; set; }
            public string flg_materia_prima { get; set; }
            public string flg_producto_terminado { get; set; }
            public int cod_caracteristica { get; set; }
            public string dsc_caracteristica { get; set; }
        }
        public class eProyecto_SubTipo_Servicio : eProyecto
        {
            public string cod_tipo_servicio { get; set; }
            public string dsc_tipo_servicio { get; set; }
            public string cod_subtipo_servicio { get; set; }
            public string dsc_subtipo_servicio { get; set; }
            public decimal ctd_volumen_m3 { get; set; }
        }
        public class eProyecto_Producto : eProyecto
        {
            public string cod_tipo_servicio { get; set; }
            public string dsc_tipo_servicio { get; set; }
            public string cod_subtipo_servicio { get; set; }
            public string dsc_subtipo_servicio { get; set; }
            public string cod_producto { get; set; }
            public string dsc_producto { get; set; }
            public decimal ctd_requerida { get; set; }
            public int ctd_stock_minimo { get; set; }
            public int ctd_stock_maximo { get; set; }
            public string dsc_observaciones { get; set; }
            public string cod_sislog { get; set; }
            public string dsc_simbolo { get; set; }
            public string cod_unidad_medida { get; set; }
            public string dsc_unidad_medida { get; set; }
            public decimal ctd_volumen_m3 { get; set; }
        }
        public class eProyecto_Producto_Costos : eProyecto
        {
            public string cod_tipo_servicio { get; set; }
            public string dsc_tipo_servicio { get; set; }
            public string cod_subtipo_servicio { get; set; }
            public string dsc_subtipo_servicio { get; set; }
            public string cod_producto { get; set; }
            public string dsc_producto { get; set; }
            public int num_item { get; set; }
            public DateTime fch_inicio { get; set; }
            public DateTime fch_fin { get; set; }
            public decimal imp_costo { get; set; }
            public string dsc_observacion { get; set; }
        }
        public class eProyecto_Producto_Receta : eProyecto
        {
            public string cod_tipo_servicio { get; set; }
            public string dsc_tipo_servicio { get; set; }
            public string cod_subtipo_servicio { get; set; }
            public string dsc_subtipo_servicio { get; set; }
            public string cod_producto { get; set; }
            public string dsc_producto { get; set; }
            public string cod_producto_item { get; set; }
            public string dsc_producto_item { get; set; }
            public decimal ctd_requerida { get; set; }
            public string dsc_observacion { get; set; }
        }

        public class eProyeto_Presupuesto_Ejecucion : eProyecto
        {
            public string cod_tipo_servicio { get; set; }
            public string dsc_tipo_servicio { get; set; }
            public decimal imp_venta_presupuesto { get; set; }
            public decimal imp_costo_presupuesto { get; set; }
            public decimal imp_utilidad_presupuesto { get; set; }
            public decimal prc_utilidad_presupuesto { get; set; }
            public decimal imp_venta_ejecutado { get; set; }
            public decimal imp_costo_ejecutado { get; set; }
            public decimal imp_utilidad_ejecutado { get; set; }
            public decimal prc_utilidad_ejecutado { get; set; }

        }
    }
}
