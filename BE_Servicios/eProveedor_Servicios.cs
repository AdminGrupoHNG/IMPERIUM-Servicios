﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eProveedor_Servicios
    {
        public bool Seleccionado { get; set; }
        public string cod_proveedor { get; set; }
        public string dsc_proveedor { get; set; }
        public string cod_tipo_servicio { get; set; }
        public string dsc_tipo_servicio { get; set; }
        public string flg_activo { get; set; }
        public string fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public string dsc_usuario_registro { get; set; }
        public string fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
        public string dsc_usuario_cambio { get; set; }
    }
}
