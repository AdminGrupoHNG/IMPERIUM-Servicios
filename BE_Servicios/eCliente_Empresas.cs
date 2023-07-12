using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eCliente_Empresas
    {
        public bool Seleccionado { get; set; }
        public string cod_cliente { get; set; }
        public string dsc_cliente { get; set; }
        public string cod_empresa { get; set; }
        public string dsc_empresa { get; set; }
        public string flg_activo { get; set; }
        public string fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public string dsc_usuario_registro { get; set; }
        public string fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
        public string dsc_usuario_cambio { get; set; }
        public int valorRating { get; set; }
        public string dsc_pref_ceco { get; set; } 
        public string cod_proyecto { get; set; } 
        public string dsc_proyecto { get; set; } 
    }
}
