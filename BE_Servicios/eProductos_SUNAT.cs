using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eProductos_SUNAT
    {
        public string cod_segmento { get; set; }
        public string dsc_segmento { get; set; }
        public string cod_familia { get; set; }
        public string dsc_familia { get; set; }
        public string cod_clase { get; set; }
        public string dsc_clase { get; set; }
        public string cod_producto { get; set; }
        public string dsc_producto { get; set; }
        public string flg_activo { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
    }
}
