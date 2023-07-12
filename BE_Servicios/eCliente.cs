using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eCliente
    {
        public string cod_cliente { get; set; }
        public string dsc_razon_social { get; set; }
        public string dsc_apellido_paterno { get; set; }
        public string dsc_apellido_materno { get; set; }
        public string dsc_nombre { get; set; }
        public string cod_proveedor_ERP { get; set; }
        public string dsc_razon_comercial { get; set; }
        public string flg_juridico { get; set; }
        public string cod_tipo_documento { get; set; }
        public string dsc_tipo_documento { get; set; }
        public string dsc_documento { get; set; }
        public string cod_calificacion { get; set; }
        public string dsc_calificacion { get; set; }
        public string dsc_email { get; set; }
        public string dsc_telefono_1 { get; set; }
        public string dsc_telefono_2 { get; set; }
        public string dsc_cliente { get; set; }
        public string cod_tipo_contacto { get; set; }
        public string dsc_tipo_contacto { get; set; }
        public string cod_usuario { get; set; }
        public string dsc_usuario_registro { get; set; }
        public DateTime fch_registro { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
        public string dsc_usuario_cambio { get; set; }
        public string flg_codigo_autogenerado { get; set; }
        public string cod_sexo { get; set; }
        public string cod_estadocivil { get; set; }
        public string cod_categoria { get; set; }
        public string dsc_categoria { get; set; }
        public string cod_cliente_antiguo { get; set; }
        public DateTime fch_fallecimiento { get; set; }
        public DateTime fch_nacimiento { get; set; }
        public string dsc_mail_trabajo { get; set; }
        public string cod_tipo_cliente { get; set; }
        public string dsc_tipo_cliente { get; set; }
        public string flg_domiciliado { get; set; }
        public string cod_vendedor { get; set; }
        public string dsc_vendedor { get; set; }
        public string cod_modalidad_venta { get; set; }
        public string flg_vinculada { get; set; }
        //public string cod_tarjeta_cliente { get; set; }
        public string dsc_mail_fe { get; set; }
        public string cod_cliente_interno { get; set; }
        public string flg_padron_envio { get; set; }
        public DateTime fch_afiliacion { get; set; }
        public string cod_empresa_interna { get; set; }
        public string dsc_cargo { get; set; }
        public string dsc_carben { get; set; }
        public string flg_tipo_planilla { get; set; }
        public Int16 num_dias_gracia { get; set; }
        public string cod_modulo { get; set; }
        public string cod_modular { get; set; }
        public string dsc_contacto { get; set; }


        public string dsc_sexo { get; set; }
        public string cod_tipo_direccion { get; set; }
		public string dsc_tipo_direccion { get; set; }
		public string cod_pais { get; set; }
		public string dsc_pais { get; set; }
		public string cod_distrito { get; set; }
		public string dsc_distrito { get; set; }
		public string cod_provincia { get; set; }
		public string dsc_provincia { get; set; }
		public string cod_departamento { get; set; }

        public string dsc_departamento { get; set; }
        public string dsc_cadena_direccion { get; set; }

        public string cod_empresas_vinculadas { get; set; }
        public string dsc_empresas_vinculadas { get; set; }
        public decimal valorRating { get; set; }

        public string flg_prospecto { get; set; }
    }
}
