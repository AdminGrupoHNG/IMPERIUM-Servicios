using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eVentana
    {
        public int cod_ventana { get; set; }
        public int cod_usuario { get; set; }
        public string dsc_ventana { get; set; }
        public int cod_grupo { get; set; }
        public string dsc_grupo { get; set; }
        public string dsc_menu { get; set; }
        public string dsc_formulario { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public string flg_activo { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
        public string cod_menu { get; set; }
        public bool flg_escritura { get; set; }
        public bool flg_lectura { get; set; }
        public string Ventana { get; set; }
        public string Perfil { get; set; }
        public string Modulo { get; set; }
        public int Valor { get; set; }
        public int ValorE { get; set; }
        public int ValorL { get; set; }
        public string Tipo { get; set; }
        public bool dsc_lectura { get; set; }
        public int cod_perfil { get; set; }
        public int num_orden { get; set; }
        public int cod_item { get; set; }
        public string dsc_solucion { get; set; }
    }
}
