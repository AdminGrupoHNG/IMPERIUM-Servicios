using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eVersion
    {
        public string VersionAPP { get; set; }
        public string OrigenDescarga { get; set; }
        public int cod_version { get; set; }
        public string dsc_version { get; set; }
        public DateTime fch_publicacion { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }

        public class eVersionDetalle : eVersion
        {
            public int num_item { get; set; }
            public string dsc_descripcion { get; set; }
            public DateTime fch_registro_det { get; set; }
            public string cod_usuario_registro_det { get; set; }
        }
    }
}
