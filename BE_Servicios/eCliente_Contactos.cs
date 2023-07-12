using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eCliente_Contactos
    {
        public string cod_cliente { get; set; }
        public int num_linea { get; set; }
        public int cod_contacto { get; set; }
        public string dsc_nombre { get; set; }
        public string dsc_apellidos { get; set; }
        public string dsc_nombre_completo { get; set; }
        public DateTime fch_nacimiento { get; set; }
        public string dsc_correo { get; set; }
        public string dsc_telefono1 { get; set; }
        public string dsc_telefono2 { get; set; }
        public string dsc_cargo { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_reg { get; set; }
        public string cod_usuario_web { get; set; }
        public string cod_clave_web { get; set; }
        public string dsc_observaciones { get; set; }
    }
}
