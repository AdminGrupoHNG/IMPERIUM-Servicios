using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eAlmacen
    {
        public string cod_almacen { get; set; }
        public string dsc_almacen { get; set; }
        public string cod_empresa { get; set; }
        public string cod_sede_empresa { get; set; }
        public DateTime fch_creacion { get; set; }
        public string dsc_descripcion { get; set; }
        public string cod_tipo_almacen { get; set; }
        public string cod_distrito { get; set; }
        public string cod_provincia { get; set; }
        public string cod_departamento { get; set; }
        public string cod_pais { get; set; }
        public string dsc_direccion { get; set; }
        public string flg_activo { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }


        public class eProductos_Almacen : eAlmacen
        {
            public int n_Orden { get; set; }
            public string cod_tipo_servicio { get; set; }
            public string dsc_tipo_servicio { get; set; }
            public string cod_subtipo_servicio { get; set; }
            public string dsc_subtipo_servicio { get; set; }
            public string cod_producto { get; set; }
            public string dsc_producto { get; set; }
            public string cod_unidad_medida { get; set; }
            public string dsc_simbolo { get; set; }
            public Int32 num_cantidad { get; set; }
            public Int32 num_cantidad_recibido { get; set; }
            public Int32 num_cantidad_x_recibir { get; set; }
            public Int32 num_cantidad_x_recibir_interno { get; set; }
            public Int32 num_cantidad_stock { get; set; }
            public Int32 num_cantidad_stock_nuevo { get; set; }
            public Int32 num_item_costo { get; set; }
            public decimal imp_costo { get; set; }
            public decimal imp_total { get; set; }
        }

        public class eEntrada_Cabecera : eAlmacen
        {
            public string cod_entrada { get; set; }
            public string dsc_entrada { get; set; }
            public string cod_tipo_movimiento { get; set; }
            public string dsc_tipo_movimiento { get; set; }
            public string cod_orden_compra_servicio { get; set; }
            public DateTime fch_documento { get; set; }
            public DateTime fch_tipocambio { get; set; }
            public decimal imp_tipocambio { get; set; }
            public string dsc_tipo_documento { get; set; }
            public string tipo_documento { get; set; }
            public string serie_documento { get; set; }
            public decimal numero_documento { get; set; }
            public string dsc_documento { get; set; }
            public string cod_proveedor { get; set; }
            public string dsc_ruc { get; set; }
            public string dsc_proveedor { get; set; }
            public DateTime fch_documentoOC { get; set; }
            public DateTime fch_cambio { get; set; }
            public string cod_usuario_cambio { get; set; }
            public decimal imp_total { get; set; }
            public string ctd_DocVinculado { get; set; }
            public string dsc_glosa { get; set; }
        }

        public class eEntrada_Detalle : eAlmacen
        {
            public string cod_entrada { get; set; }
            public string cod_tipo_servicio { get; set; }
            public string cod_subtipo_servicio { get; set; }
            public string cod_producto { get; set; }
            public string cod_unidad_medida { get; set; }
            public string dsc_simbolo { get; set; }
            public int num_cantidad { get; set; }
            public int num_cantidad_recibido { get; set; }
            public int num_cantidad_x_recibir { get; set; }
            public int num_item_costo { get; set; }
            public decimal imp_costo { get; set; }
            public decimal imp_total { get; set; }
            public DateTime fch_vencimiento { get; set; }
        }

        public class eSalida_Cabecera : eAlmacen
        {
            public string cod_salida { get; set; }
            public string dsc_salida { get; set; }
            public string cod_tipo_movimiento { get; set; }
            public string dsc_tipo_movimiento { get; set; }
            public string cod_requerimiento { get; set; }
            public string dsc_solicitante { get; set; }
            public DateTime fch_documento { get; set; }
            public DateTime fch_tipocambio { get; set; }
            public decimal imp_tipocambio { get; set; }
            public string dsc_pref_ceco { get; set; }
            public string dsc_CECO { get; set; }
            public string cod_almacen_destino { get; set; }
            public string dsc_almacen_destino { get; set; }
            public DateTime fch_requerimiento { get; set; }
            public DateTime fch_cambio { get; set; }
            public string cod_usuario_cambio { get; set; }
        }

        public class eSalida_Detalle : eAlmacen
        {
            public string cod_salida { get; set; }
            public string cod_tipo_servicio { get; set; }
            public string cod_subtipo_servicio { get; set; }
            public string cod_producto { get; set; }
            public string cod_unidad_medida { get; set; }
            public string dsc_simbolo { get; set; }
            public int num_cantidad { get; set; }
        }


        public class eGuiaRemision_Cabecera : eAlmacen
        {
            public string cod_guiaremision { get; set; }
            public string cod_tipo_movimiento { get; set; }
            public string dsc_tipo_movimiento { get; set; }
            public DateTime fch_documento { get; set; }
            public string cod_requerimiento { get; set; }
            public string dsc_solicitante { get; set; }
            public DateTime fch_tipocambio { get; set; }
            public decimal imp_tipocambio { get; set; }
            public DateTime fch_traslado { get; set; }
            public string dsc_pref_ceco { get; set; }
            public string dsc_CECO { get; set; }
            public string cod_transportista { get; set; }
            public string placa_transportista { get; set; }
            public string ruc_transportista { get; set; }
            public string dsc_transportista { get; set; }
            public string cod_motivo_traslado { get; set; }
            public string dsc_motivo_traslado { get; set; }
            public DateTime fch_cambio { get; set; }
            public string cod_usuario_cambio { get; set; }
            public string tipo_documento { get; set; }
            public string serie_documento { get; set; }
            public string idPDF { get; set; }
            public string flg_PDF { get; set; }
            public string NombreArchivo { get; set; }
        }

        public class eGuiaRemision_Detalle : eAlmacen
        {
            public string cod_guiaremision { get; set; }
            public string cod_tipo_servicio { get; set; }
            public string cod_subtipo_servicio { get; set; }
            public string cod_producto { get; set; }
            public string cod_unidad_medida { get; set; }
            public string dsc_simbolo { get; set; }
            public int num_cantidad { get; set; }
        }

        public class eReporteInventario : eAlmacen
        {
            public string cod_tipo_servicio { get; set; }
            public string cod_subtipo_servicio { get; set; }
            public string cod_producto_SUNAT { get; set; }
            public string cod_producto { get; set; }
            public string dsc_producto { get; set; }
            public string dsc_simbolo { get; set; }
            public string dsc_tipo_servicio { get; set; }
            public string fch_documento { get; set; }
            public string tipo { get; set; }
            public string serie { get; set; }
            public string numero { get; set; }
            public string dsc_tipo_movimiento { get; set; }
            public int cantidad_entrada { get; set; }
            public decimal costo_entrada { get; set; }
            public decimal total_entrada { get; set; }
            public int cantidad_salida { get; set; }
            public decimal costo_salida { get; set; }
            public decimal total_salida { get; set; }
            public int cantidad_final { get; set; }
            public decimal costo_ponderado { get; set; }
            public decimal total_final { get; set; }
        }
    }
}
