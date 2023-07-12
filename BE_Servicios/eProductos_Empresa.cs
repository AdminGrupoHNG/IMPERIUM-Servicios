using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eProductos_Empresa
    {
        public bool Seleccionado { get; set; }
        public string cod_empresa { get; set; }
        public string dsc_empresa { get; set; }
        public string cod_tipo_servicio { get; set; }
        public string dsc_tipo_servicio { get; set; }
        public string cod_subtipo_servicio { get; set; }
        public string dsc_subtipo_servicio { get; set; }
        public string cod_producto { get; set; }
        public string dsc_producto { get; set; }
        public string cod_cta_contable { get; set; }
        public string flg_activo { get; set; }
    }
}
