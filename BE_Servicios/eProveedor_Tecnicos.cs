using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eProveedor_Tecnicos
    {
        public string cod_proveedor { get; set; }
        public int cod_tecnico { get; set; }
        public string dsc_nombre { get; set; }
        public string dsc_apellidos { get; set; }
        public DateTime fch_nacimiento { get; set; }
        public string dsc_correo { get; set; }
        public string dsc_telefono1 { get; set; }
        public string dsc_telefono2 { get; set; }
        public string flg_supervisor { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_reg { get; set; }
        public string cod_usuario_web { get; set; }
        public string cod_clave_web { get; set; }
        public string dsc_observaciones { get; set; }
    }
}
