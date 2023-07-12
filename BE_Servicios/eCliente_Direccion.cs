using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eCliente_Direccion
    {
        public int Orden { get; set; }
        public string cod_cliente { get; set; }
        public int num_linea { get; set; }
        public string cod_pais { get; set; }
        public string cod_departamento { get; set; }
        public string cod_provincia { get; set; }
        public string dsc_direccion { get; set; }
        public string cod_distrito { get; set; }
        public string cod_tipo_direccion { get; set; }
        public string dsc_tipo_direccion { get; set; }
        public string dsc_referencia { get; set; }
        public string dsc_telefono_1 { get; set; }
        public string dsc_telefono_2 { get; set; }
        public string flg_comprobante { get; set; }
        public string cod_numero { get; set; }
        public string cod_interior { get; set; }
        public string cod_manzana { get; set; }
        public string cod_lote { get; set; }
        public string cod_sublote { get; set; }
        public string dsc_urbanizacion { get; set; }
        public string dsc_cadena_direccion { get; set; }
        public string flg_direccion_obra { get; set; }
        public string dsc_observacion { get; set; }
        public string dsc_nombre_direccion { get; set; }
        public string cod_zona { get; set; }
        public string flg_direccion_cobranza { get; set; }
        public string cod_calle_direccion { get; set; }
        public string cod_urbanizacion { get; set; }
        public string cod_tipo_via { get; set; }
        public string cod_tipo_zona { get; set; }
        public DateTime fch_visita { get; set; }
    }
}
