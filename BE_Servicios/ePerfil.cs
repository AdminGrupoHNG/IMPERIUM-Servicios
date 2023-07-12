using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class ePerfil
    {
        public int cod_perfil { get; set; }
        public string dsc_perfil { get; set; }
        public string cod_usuario { get; set; }
        public string dsc_usuario { get; set; }
        public int cod_scfvi_perfil { get; set; }
        public string flg_activo { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public DateTime fch_cambio{ get; set; }
        public string cod_usuario_cambio { get; set; }
        public string  dsc_solucion { get; set; }
    }
}
