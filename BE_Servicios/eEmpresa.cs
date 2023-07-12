using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eEmpresa
    {
        public string cod_empresa { get; set; }
        public string dsc_empresa { get; set; }
        public string dsc_ruc { get; set; }
        public string dsc_direccion { get; set; }
        public string dsc_apoderado { get; set; }
        public string dsc_database { get; set; }
        public string flg_activo { get; set; }
        public string flg_principal { get; set; }
        public string dsc_telefono { get; set; }
        public string dsc_movil { get; set; }
        public string dsc_codigo_licencia { get; set; }
        public string flg_independiente { get; set; }
        public string flg_sincronizado { get; set; }
        public string flg_agente_retencion { get; set; }
        public string dsc_logo_empresa { get; set; }
        public string cod_trabajador_rep { get; set; }
        public string flg_tipo_empresa { get; set; }
        public string cod_categoria { get; set; }
        public string cod_trabajador_rrhh { get; set; }
        public string dsc_web { get; set; }
        public string dsc_horario { get; set; }
        public string dsc_ubigeo_direccion { get; set; }
        public string dsc_urbanizacion { get; set; }
        public string cod_pais { get; set; }
        public string cod_departamento { get; set; }
        public string cod_provincia { get; set; }
        public string cod_distrito { get; set; }
        public string dsc_razon_comercial { get; set; }
        public string dsc_dominio { get; set; }
        public string dsc_ruta_onedrive { get; set; }

        public string UsuarioOnedrive { get; set; }
        public string ClaveOnedrive { get; set; }
        public string ClientIdOnedrive { get; set; }
        public string TenantOnedrive { get; set; }
        public string idCarpetaOnedrive { get; set; }
        public string idRepositorioOnedrive { get; set; }
        public string idCarpetaFacturasOnedrive { get; set; }

        public string UsuarioOnedrivePersonal { get; set; }
        public string ClaveOnedrivePersonal { get; set; }


        public class eOnedrive_Empresa : eEmpresa
        {
            public string dsc_Carpeta { get; set; }
            public string idCarpeta { get; set; }
            public string dsc_CarpetaAnho { get; set; }
            public string idCarpetaAnho { get; set; }
            public string dsc_CarpetaMes { get; set; }
            public string idCarpetaMes { get; set; }
            public string idCarpeta_Trabajador { get; set; }
        }

    }
}
