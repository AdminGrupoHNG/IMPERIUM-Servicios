using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eCajaChica
    {
        public string cod_caja { get; set; }
        public DateTime fch_creacion { get; set; }
		public string cod_tipo_caja { get; set; }
		public string dsc_tipo_caja { get; set; }
		public string cod_responsable { get; set; }
		public string dsc_responsable { get; set; }
		public string cod_empresa { get; set; }
		public string dsc_empresa { get; set; }
		public string cod_sede_empresa { get; set; }
		public string dsc_sede_empresa { get; set; }
		public string cod_moneda { get; set; }
		public decimal imp_monto { get; set; }
		public decimal imp_alertar { get; set; }
		public string cod_modalidad { get; set; }
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

		public string cod_rendicion { get; set; }
		public string flg_rendido { get; set; }

		public string dsc_ajuste { get; set; }
		public string dsc_documento_entregado_a { get; set; }

		public string cod_estado_contabilizado { get; set; }
		public string dsc_estado_contabilizado { get; set; }
		public string periodo_tributario { get; set; }
		public int num_Anho { get; set; }

		public class eMovimiento_CajaChica : eCajaChica
        {
			public string cod_movimiento { get; set; }
			public string cod_tipo { get; set; }
			public string cod_estado { get; set; }
			public string dsc_estado { get; set; }
			public decimal imp_entregado { get; set; }
			public string cod_entregado_a { get; set; }
			public string dsc_entregado_a { get; set; }
			public string dsc_ruc { get; set; }
			public string dsc_glosa { get; set; }
			public string cod_und_negocio { get; set; }
			public string dsc_und_negocio { get; set; }
			public string cod_tipo_gasto { get; set; }
			public string dsc_tipo_gasto { get; set; }
			public string cod_cliente { get; set; }
			public string dsc_cliente { get; set; }
			public string cod_CECO { get; set; }
			public string dsc_observacion { get; set; }
			public string cod_movimiento_vinculo { get; set; }
			public string cod_movimiento_rendido { get; set; }
			public string dsc_referencia { get; set; }
			public string cod_entregarendir { get; set; }
		}

		public class eDetalleMov_CajaChica : eCajaChica
        {
            public bool Sel { get; set; }
			public string cod_movimiento { get; set; }
			public Int32 num_linea { get; set; }
			public string tipo_documento { get; set; }
			public string serie_documento { get; set; }
			public decimal numero_documento { get; set; }
			public DateTime fch_documento { get; set; }
			public string dsc_ruc { get; set; }
			public string dsc_glosa { get; set; }
			public decimal imp_entregado { get; set; }
			public string cod_correlativoSISPAG { get; set; }
		}

        //public class eCronograma : eCajaChica
        //{
        //    public DateTime fch_cuota { get; set; }
        //    public int num_dias { get; set; }
        //    public decimal imp_Capital { get; set; }
        //    public int num_meses { get; set; }
        //    public decimal num_tasaanual { get; set; }
        //    public decimal num_tasamensual { get; set; }
        //    public int num_cuota { get; set; }
        //    public decimal imp_capitalinicial { get; set; }
        //    public decimal imp_capitalfinal { get; set; }
        //    public decimal imp_amortizacion { get; set; }
        //    public decimal imp_interes { get; set; }
        //    public decimal imp_desgravamen { get; set; }
        //    public decimal imp_portes { get; set; }
        //    public decimal imp_otros { get; set; }
        //    public decimal imp_cuotasinigv { get; set; }
        //    public decimal imp_coutaigv { get; set; }
        //    public decimal imp_cuotaconigv { get; set; }

        //}

    }
}
