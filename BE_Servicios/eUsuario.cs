using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eUsuario
    {
        public string cod_usuario { get; set; }

        public string dsc_usuario { get; set; }
        public string dsc_clave { get; set; }
        public DateTime fch_registro { get; set; }
        public string flg_activo { get; set; }
        public string flg_noexpira { get; set; }
        public DateTime fch_cambioclave { get; set; }
        public string flg_administrador { get; set; }
        public string cod_usuariobd { get; set; }
        public string dsc_clavebd { get; set; }
        public string flg_trabajador { get; set; }
        public string cod_trabajador { get; set; }
        public string dsc_trabajador { get; set; }
        public string flg_audita_login { get; set; }
        public string dsc_ruta_firma { get; set; }
        public string dsc_correo { get; set; }
        public string dsc_contraseña { get; set; }
        public Int16 num_dias_cambio_contraseña { get; set; }
        public string cod_menu { get; set; }
        public string dsc_menu{ get; set; }
        public string dsc_perfil { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_registro { get; set; }
        public string cod_usuario_cambio { get; set; }
        public string dsc_usuario_registro { get; set; }
        public string dsc_usuario_cambio { get; set; }
        public string flg_cambiar_clave { get; set; }

    }
}
