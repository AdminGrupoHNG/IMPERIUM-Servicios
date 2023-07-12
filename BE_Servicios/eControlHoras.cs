using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eControlHoras
    {
        public string cod_control_horas { get; set; }
        public string cod_usuario { get; set; }
        public string dsc_comentario { get; set; }
        public string dsc_duracion { get; set; }
        public DateTime fch_ejecucion { get; set; }
        public string cod_segmento { get; set; }
        public string dsc_segmento { get; set; }
        public string cod_grupo { get; set; }
        public string dsc_grupo { get; set; }
        public string cod_actividad { get; set; }
        public string dsc_actividad { get; set; }
        public string cod_empresa_usuaria { get; set; }
        public decimal imp_costo { get; set; }
        public string cod_empresa { get; set; }
        public string dsc_empresa { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
        public string flg_activo { get; set; }

        public class eCostoHora : eControlHoras
        {
            public string cod_usuario { get; set; }
            public decimal imp_costo { get; set; }
            public DateTime fch_registro { get; set; }
            public string cod_usuario_registro { get; set; }
            public DateTime fch_cambio { get; set; }
            public string cod_usuario_cambio { get; set; }
        }

        public class eActividadesGestionada : eControlHoras
        {
            public string cod_segmento { get; set; }
            public string dsc_segmento { get; set; }
            public string cod_grupo { get; set; }
            public string dsc_grupo { get; set; }
            public string cod_actividad { get; set; }
            public string dsc_actividad { get; set; }
        }
    }
}
