using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eProveedor
    {
        public string cod_proveedor { get; set; }
        public string cod_tipo_documento { get; set; }
        public string dsc_tipo_documento { get; set; }
        public string num_documento { get; set; }
        public string dsc_razon_social { get; set; }
        public string dsc_direccion { get; set; }
        public string cod_pais { get; set; }
        public string dsc_pais { get; set; }
        public string cod_departamento { get; set; }
        public string dsc_departamento { get; set; }
        public string cod_provincia { get; set; }
        public string dsc_provincia { get; set; }
        public string cod_distrito { get; set; }
        public string dsc_distrito { get; set; }
        public string dsc_contacto1 { get; set; }
        public string dsc_contacto2 { get; set; }
        public string dsc_mail_1 { get; set; }
        public string dsc_mail_2 { get; set; }
        public string dsc_fono_1 { get; set; }
        public string dsc_fono_2 { get; set; }
        public string flg_activo { get; set; }
        public string dsc_razon_comercial { get; set; }
        public string flg_venta_consignacion { get; set; }
        public string cod_tipo_proveedor { get; set; }
        public string dsc_tipo_proveedor { get; set; }
        public string flg_agente_retencion { get; set; }
        public string flg_buen_contribuyente { get; set; }
        public string flg_auto_detraccion { get; set; }
        public string flg_codigo_autogenerado { get; set; }
        public string flg_agente_percepcion { get; set; }
        public string dsc_apellido_paterno { get; set; }
        public string dsc_apellido_materno { get; set; }
        public string dsc_nombres { get; set; }
        public string flg_juridico { get; set; }
        public string dsc_proveedor { get; set; }
        public string flg_domiciliado { get; set; }
        public string cod_modalidad_pago { get; set; }
        public string dsc_modalidad_pago { get; set; }
        public string flg_afecto_cuarta { get; set; }
        public string flg_no_habido { get; set; }
        public DateTime fch_no_habido { get; set; }
        public string cod_convenio_trib { get; set; }
        public string dsc_venta_consignacion { get; set; }
        public string Observaciones { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public string dsc_usuario_registro { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
        public string dsc_usuario_cambio { get; set; }

        public string cod_proveedor_ERP { get; set; }
        public string cod_frecuencia { get; set; }
        public string dsc_frecuencia { get; set; }
        public string cod_formapago { get; set; }
        public string dsc_formapago { get; set; }
        public string dsc_representante_legal { get; set; }

        public string cod_servicios_vinculadas { get; set; }
        public string dsc_servicios_vinculadas { get; set; }
        public string cod_empresas_vinculadas { get; set; }
        public string dsc_empresas_vinculadas { get; set; }
        public int num_dias { get; set; }
        public int CantCuentas { get; set; }
        public int CantContactos { get; set; }
        public int CantCriterios { get; set; }
        public decimal valorRating { get; set; }

        public string dsc_banco { get; set; }
        public string cod_moneda { get; set; }
        public string dsc_tipo_cuenta { get; set; }
        public string dsc_cta_bancaria { get; set; }
        public string dsc_cta_interbancaria { get; set; }

        public string dsc_contactos { get; set; }
        public string dsc_correos_contactos { get; set; }
        public string dsc_telefonos_contactos { get; set; }
        public string dsc_empresa_contactos { get; set; }

        public int ctd_digitos { get; set; }
        public string cod_sunat { get; set; }

        public string flg_transportista { get; set; }
        public string dsc_licenciaconducir { get; set; }
        public string dsc_nroautorizMTC { get; set; }
        public string dsc_marcavehiculo { get; set; }
        public string dsc_placavehiculo { get; set; }


        public string flg_vigente { get; set; }


    }
}
