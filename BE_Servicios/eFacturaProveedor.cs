using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eFacturaProveedor
    {
        public string tipo_documento { get; set; }
        public string serie_documento { get; set; }
        public decimal numero_documento { get; set; }
        public string dsc_glosa { get; set; }
        public string cod_moneda { get; set; }
        public string flg_igv { get; set; }
        public decimal porc_igv { get; set; }
        public DateTime fch_tipocambio { get; set; }
        public decimal imp_tipocambio { get; set; }
        public string cod_modalidad_pago { get; set; }
        public string dsc_ruc { get; set; }
        public string cod_proveedor { get; set; }
        public string cod_empresa { get; set; }
        public string cod_centroresp { get; set; }
        public DateTime fch_registro { get; set; }
        public DateTime fch_documento { get; set; }
        public DateTime fch_vencimiento { get; set; }
        public DateTime fch_pago_programado { get; set; }
        public DateTime fch_pago_ejecutado { get; set; }
        public decimal imp_descuento { get; set; }
        public decimal imp_retencion { get; set; }
        public decimal imp_percepcion { get; set; }
        public decimal imp_subtotal { get; set; }
        public decimal imp_igv { get; set; }
        public decimal imp_otros_cargos { get; set; }
        public decimal imp_total { get; set; }
        public decimal imp_saldo { get; set; }
        public string dsc_observacion { get; set; }
        public string cod_estado_documento { get; set; }
        public string cod_estado_registro { get; set; }
        public string cod_estado_pago { get; set; }
        public string flg_detraccion { get; set; }
        public string cod_concepto_detraccion { get; set; }
        public decimal imp_detraccion { get; set; }
        public string num_constancia_detraccion { get; set; }
        public DateTime fch_constancia_detraccion { get; set; }
        public DateTime fch_pago_ejecutado_detraccion { get; set; }
        public string flg_detraccion_aplicada { get; set; }
        public DateTime fch_anulacion { get; set; }
        public string cod_usuario_anulacion { get; set; }
        public string cod_origen_documento { get; set; }
        public DateTime fch_registro_real { get; set; }
        public string cod_usuario_registro { get; set; }
        public DateTime fch_cambio_real { get; set; }
        public string cod_usuario_cambio { get; set; }
        public string dsc_estado_registro { get; set; }
        public string dsc_estado_pago { get; set; }
        public string dsc_tipo_documento { get; set; }
        public string dsc_proveedor { get; set; }
        public string cod_tipo_proveedor { get; set; }
        public string dsc_tipo_proveedor { get; set; }
        public string dsc_tipo_servicio { get; set; }
        public string dsc_usuario_registro { get; set; }
        public string dsc_usuario_cambio { get; set; }
        public string dsc_usuario_anulacion { get; set; }
        public string dsc_tipo_servicio_detraccion { get; set; }
        public string dsc_documento { get; set; }
        public int cant_documentos { get; set; }
        public Int16 num_dias { get; set; }
        public decimal imp_pagado { get; set; }
        public decimal imp_pago_atrasado { get; set; }
        public decimal imp_pago_vencer { get; set; }
        public string dsc_meses { get; set; }
        public string dsc_dia { get; set; }
        public decimal imp_no_pagado { get; set; }
        public string dsc_estado_documento { get; set; }
        public string cod_distribucion_CECO { get; set; }
        public string dsc_distribucion_CECO { get; set; }
        public string cod_documento { get; set; }
        public string cod_sunat { get; set; }
        public int nOrden { get; set; }

        public string flg_PDF { get; set; }
        public string flg_XML { get; set; }
        public string RutaCarpeta { get; set; }
        public string NombreArchivo { get; set; }
        public string idPDF { get; set; }
        public string idXML { get; set; }
        public string idCarpetaAnho { get; set; }
        public string idCarpetaMes { get; set; }
        public string cod_tipo_servicio { get; set; }
        public string dsc_tipo_comprobante { get; set; }
        //public string flg_guardado { get; set; }
        public string flg_inventario { get; set; }
        public string flg_activo_fijo { get; set; }
        public string periodo_tributario { get; set; }
        public string flg_cerrado { get; set; }
        public string num_OrdenCompraServ { get; set; }
        public decimal prc_tasa_detraccion { get; set; }
        public decimal imp_detraccion_pagada { get; set; }
        public string flg_retencion { get; set; }
        public decimal prc_tasa_retencion { get; set; }
        public string num_constancia_retencion { get; set; }
        public DateTime fch_constancia_retencion { get; set; }
        public string cod_tipo_transaccion { get; set; }
        public string ctd_CECO { get; set; }
        public string flg_CajaChica { get; set; }
        public string flg_EntregasRendir { get; set; }
        public string CantCuentas { get; set; }
        public string flg_exportadoSISPAG { get; set; }
        public string cod_correlativoSISPAG { get; set; }
        public string cod_proyecto { get; set; }
        public string dsc_proyecto { get; set; }

        public DateTime fch_aprobado_reg { get; set; }
        public string cod_usuario_aprobado_reg { get; set; }
        public string dsc_usuario_aprobado_reg { get; set; }
        public DateTime fch_contabilizado { get; set; }
        public string cod_usuario_contabilizado { get; set; }
        public string dsc_usuario_contabilizado { get; set; }
        public int ctd_dias_aprobado { get; set; }

        //public string new_fch_pago_ejecutado
        //{
        //    get
        //    {
        //        if (fch_pago_ejecutado.ToString().Contains("1/01/0001"))
        //        {
        //            return "";
        //        }
        //        else
        //        {
        //            return fch_pago_ejecutado.ToShortDateString();
        //        }
        //    }
        //}

        //eFacturaProveedor_Distribucion
        public class eFacturaProveedor_Distribucion : eFacturaProveedor
        {
            public bool Sel { get; set; }
            public string cod_und_negocio { get; set; }
            public string cod_tipo_gasto { get; set; }
            public string cod_cliente { get; set; }
            public string cod_CECO { get; set; }
            public decimal porc_distribucion { get; set; }
            public DateTime fch_cambio { get; set; }
            public string cod_proyecto { get; set; }
            public string cod_cta_contable { get; set; }

            //public string cod_tipo_gasto { get; set; }
            //public string cod_cta_contable { get; set; }
        }
        public class eFacturaProveedor_Observaciones : eFacturaProveedor
        {
            public Int32 num_linea { get; set; }
            public string dsc_observaciones { get; set; }
        }
        public class eFaturaProveedor_ProgramacionPagos : eFacturaProveedor
        {
            public bool Sel { get; set; }
            public int num_linea { get; set; }
            public DateTime fch_pago { get; set; }
            public decimal imp_pago { get; set; }
            public string cod_pagar_a { get; set; }
            public string dsc_pagar_a { get; set; }
            public string cod_estado { get; set; }
            public string dsc_estado { get; set; }
            public DateTime fch_ejecucion { get; set; }
            public string cod_usuario_ejecucion { get; set; }
            public string dsc_usuario_ejecucion { get; set; }
            public DateTime fch_cambio { get; set; }
            public string cod_tipo_prog { get; set; }
            public string dsc_tipo_prog { get; set; }
            public string cod_formapago { get; set; }
            public string dsc_formapago { get; set; }
            public string cod_destinatario { get; set; }
            public string dsc_destinatario { get; set; }
            //public string new_fch_ejecucion
            //{
            //    get
            //    {
            //        if (fch_ejecucion.ToString().Contains("1/01/0001"))
            //        {
            //            return "";
            //        }
            //        else
            //        {
            //            return fch_ejecucion.ToShortDateString();
            //        }
            //    }
            //}
        }
        public class eFacturaProveedor_NotaCredito : eFacturaProveedor
        {
            public string tipo_documento_NC { get; set; }
            public string serie_documento_NC { get; set; }
            public decimal numero_documento_NC { get; set; }
            public string cod_proveedor_NC { get; set; }
        }

        public class eFacturaProveedor_VistaProveedor : eFacturaProveedor
        {
            public bool Sel { get; set; }
            public DateTime fch_cambio { get; set; }
            public decimal mes_pagado_Enero { get; set; }
            public decimal mes_pagado_Febrero { get; set; }
            public decimal mes_pagado_Marzo { get; set; }
            public decimal mes_pagado_Abril { get; set; }
            public decimal mes_pagado_Mayo { get; set; }
            public decimal mes_pagado_Junio { get; set; }
            public decimal mes_pagado_Julio { get; set; }
            public decimal mes_pagado_Agosto { get; set; }
            public decimal mes_pagado_Septiembre { get; set; }
            public decimal mes_pagado_Octubre { get; set; }
            public decimal mes_pagado_Noviembre { get; set; }
            public decimal mes_pagado_Diciembre { get; set; }

            public decimal mes_no_pagado_Enero { get; set; }
            public decimal mes_no_pagado_Febrero { get; set; }
            public decimal mes_no_pagado_Marzo { get; set; }
            public decimal mes_no_pagado_Abril { get; set; }
            public decimal mes_no_pagado_Mayo { get; set; }
            public decimal mes_no_pagado_Junio { get; set; }
            public decimal mes_no_pagado_Julio { get; set; }
            public decimal mes_no_pagado_Agosto { get; set; }
            public decimal mes_no_pagado_Septiembre { get; set; }
            public decimal mes_no_pagado_Octubre { get; set; }
            public decimal mes_no_pagado_Noviembre { get; set; }
            public decimal mes_no_pagado_Diciembre { get; set; }
        }

        public class eFacturaProvedor_Detalle : eFacturaProveedor
        {
            public bool Sel { get; set; }
            public string cod_sede_empresa { get; set; }
            public string cod_orden_compra_servicio { get; set; }
            public string cod_proveedor_det { get; set; }
            public string dsc_ruc_det { get; set; }
            public string cod_producto { get; set; }
            public string dsc_producto { get; set; }
            public string subtipo_servicio { get; set; }
            public string cod_unidad_medida { get; set; }
            public string dsc_unidad_medida { get; set; }
            public int num_cantidad { get; set; }
            public decimal imp_unitario { get; set; }
            public decimal imp_total_det { get; set; }
            public DateTime fch_registro_det { get; set; }
            public string cod_usuario_registro { get; set; }
            public string cod_usuario_cambio { get; set; }
            public DateTime fch_registro { get; set; }
            public DateTime fch_cambio { get; set; }

            public string dsc_simbolo { get; set; }
        }

    }
}
