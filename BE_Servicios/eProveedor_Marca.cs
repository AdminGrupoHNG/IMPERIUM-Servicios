using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eProveedor_Marca
    {
        public string cod_proveedor { get; set; }
        public string dsc_proveedor { get; set; }
        public int cod_marca { get; set; }
        public string dsc_marca { get; set; }
        public string dsc_abreviado { get; set; }
        public string flg_activo { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
    }
}
