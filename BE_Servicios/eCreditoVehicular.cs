using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eCreditoVehicular
    {
		public string cod_credito { get; set; }
		public string dsc_apellido_paterno { get; set; }
		public string dsc_apellido_materno { get; set; }
		public string dsc_nombres { get; set; }
		public string dsc_nombres_completos { get; set; }
		public string dsc_cliente { get; set; }
		public string cod_tipo_documento { get; set; }
		public string dsc_documento { get; set; }
		public string dsc_titular { get; set; }
		public string cod_tipo_documento_titular { get; set; }
		public string dsc_documento_titular { get; set; }
		public DateTime fch_registro { get; set; }
		public string cod_usuario_registro { get; set; }
		public DateTime fch_cambio { get; set; }
		public string cod_usuario_cambio { get; set; }
		public string flg_activo { get; set; }

		public decimal imp_capitalvigente { get; set; }
		public decimal imp_totalvigente { get; set; }
		public decimal imp_montopagado { get; set; }
		public decimal imp_capitalatrasado { get; set; }
		public decimal imp_interesatrasado { get; set; }
		public decimal imp_montoatrasado { get; set; }
		public string num_cuotavigente { get; set; }
		public string flg_semaforo { get; set; }
		public string dsc_mensajeImportar { get; set; }
		public int num_diapago { get; set; }
		public int dsc_anho { get; set; }
		public string dsc_mes { get; set; }

		public class eCronogramaCabecera : eCreditoVehicular
        {
			public string cod_cronograma { get; set; }
			public string num_placa { get; set; }
			public decimal imp_Capital { get; set; }
			public DateTime fch_desembolso { get; set; }
			public int num_cuotas { get; set; }
			public int num_diapago { get; set; }
			public decimal num_tasaanual { get; set; }
			public decimal num_tasamensual { get; set; }
			public decimal num_tasaTIRanual { get; set; }
			public decimal num_tasaTIRM { get; set; }
			public string flg_activo { get; set; }
		}

		public class eCronogramaDetalle : eCreditoVehicular
		{
			public string cod_cronograma { get; set; }
			public string num_placa { get; set; }
			public int num_cuota { get; set; }
			public DateTime fch_cuota { get; set; }
			public int num_dias { get; set; }
			public decimal imp_capitalinicial { get; set; }
			public decimal imp_capitalfinal { get; set; }
			public decimal imp_amortizacion { get; set; }
			public decimal imp_interes { get; set; }
			public decimal imp_desgravamen { get; set; }
			public decimal imp_portes { get; set; }
			public decimal imp_otros { get; set; }
			public decimal imp_cuotasinigv { get; set; }
			public decimal imp_coutaigv { get; set; }
			public decimal imp_cuotaconigv { get; set; }
			public string flg_pagado { get; set; }
			public decimal imp_montopagado { get; set; }
			public decimal imp_montoporpagar { get; set; }

			public decimal imp_igv { get; set; }
			public decimal imp_capital { get; set; }
			public decimal imp_total { get; set; }
			public string dsc_destino { get; set; }
		}

		public class ePagosDetalle : eCreditoVehicular
		{
			public string cod_cronograma { get; set; }
			public string num_placa { get; set; }
			public int num_linea { get; set; }
			public DateTime fch_recaudo { get; set; }
			public string dsc_hora { get; set; }
			public string dsc_estacion { get; set; }
			public string cod_chip { get; set; }
			public decimal num_tanqueo { get; set; }
			public decimal imp_bruto { get; set; }
			public decimal imp_cofide { get; set; }
			public decimal imp_neto { get; set; }
			public string dsc_origen { get; set; }
			public DateTime fch_archivo { get; set; }
			public string flg_pagoaplicado { get; set; }
			public decimal imp_aplicado { get; set; }
			public int num_cuota { get; set; }
			public string flg_pagovalidado { get; set; }
			public DateTime fch_pagovalidado { get; set; }
			public DateTime fch_deposito { get; set; }
			public string dsc_destino { get; set; }
		}

		public class ePagoCuota : eCreditoVehicular
        {
			public string cod_cronograma { get; set; }
			public string num_placa { get; set; }
			public int num_cuota { get; set; }
			public int num_linea { get; set; }
			public decimal imp_neto { get; set; }

			public decimal imp_interes { get; set; }
			public decimal imp_igv { get; set; }
			public decimal imp_capital { get; set; }
		}
	}
}
