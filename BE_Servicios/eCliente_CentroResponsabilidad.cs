using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eCliente_CentroResponsabilidad
    {
        public string cod_cliente { get; set; }
        public string dsc_cliente { get; set; }
        public string cod_centroresp { get; set; }
        public string dsc_centroresp { get; set; }
        public string dsc_centroresp_cliente { get; set; }
        public string flg_activo { get; set; }
        public string flg_consolidador { get; set; }
        public int num_nivel { get; set; }
        public string cod_centroresp_sup { get; set; }
        public int num_linea { get; set; }
        public int cod_contacto { get; set; }
        public string dsc_observaciones { get; set; }
    }
}
