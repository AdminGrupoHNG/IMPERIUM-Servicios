using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eCliente_Ubicacion
    {
        public string cod_cliente { get; set; }
        public string dsc_cliente { get; set; }
        public string cod_ubicacion { get; set; }
        public string dsc_ubicacion { get; set; }
        public string dsc_larga_ubicacion { get; set; }
        public string cod_nivel { get; set; }
        public string dsc_observacion { get; set; }
        public string cod_ubicacion_sup { get; set; }
        public string flg_activo { get; set; }
        public string cod_localidad { get; set; }
        public string dsc_direccion { get; set; }
        public string cod_ubicacion_per { get; set; }
        public int num_linea { get; set; }
        public int cod_contacto { get; set; }
        public string dsc_contacto { get; set; }

        public string dsc_ubicacion_sup { get; set; }
        public string dsc_ubicacion_sup2 { get; set; }
    }
}
