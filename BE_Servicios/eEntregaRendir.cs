using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eEntregaRendir
    {
        public string cod_entregarendir { get; set; }
        public string cod_tipo { get; set; }
        public DateTime fch_creacion { get; set; }
        public string cod_entregado_a { get; set; }
		public string dsc_entregado_a { get; set; }
        public string dsc_documento_entregado_a { get; set; }
        public string cod_empresa { get; set; }
		public string dsc_empresa { get; set; }
        public string cod_sede_empresa { get; set; }
		public string dsc_sede_empresa { get; set; }
        public string cod_moneda { get; set; }
        public decimal imp_monto { get; set; }
        public string cod_modalidad { get; set; }
        public string cod_estado { get; set; }
        public string dsc_observacion { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }

        public string cod_proveedor { get; set; }
        public string dsc_proveedor { get; set; }

        public string dsc_tipo_documento { get; set; }
        public string dsc_documento { get; set; }

        public string cod_distribucion_CECO { get; set; }
        public string dsc_distribucion_CECO { get; set; }
        public string flg_PDF { get; set; }
        public int ctd_comprobantes { get; set; }
        public string abv_estado { get; set; }
        public string dsc_tipo { get; set; }

        public string cod_vinculo { get; set; }
        public string dsc_ajuste { get; set; }
        public string cod_rendicion { get; set; }
        public int num_Anho { get; set; }

        public string cod_estado_contabilizado { get; set; }
        public string dsc_estado_contabilizado { get; set; }
        public string periodo_tributario { get; set; }
        public string cod_estado_aprobado { get; set; }
        public class eDetalle_EntregaRendir : eEntregaRendir
        {
            public bool Sel { get; set; }
            public int num_linea { get; set; }
            public string tipo_documento { get; set; }
            public string serie_documento { get; set; }
            public decimal numero_documento { get; set; }
            public DateTime fch_documento { get; set; }
            public string dsc_ruc { get; set; }
            public string dsc_glosa { get; set; }
            public decimal imp_total { get; set; }
            public string cod_correlativoSISPAG { get; set; }
        }
    }
}
