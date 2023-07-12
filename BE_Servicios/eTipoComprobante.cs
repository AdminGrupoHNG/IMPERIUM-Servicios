using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eTipoComprobante
    {
        public string cod_tipo_comprobante { get; set; }
        public string dsc_tipo_comprobante { get; set; }
        public string flg_igv { get; set; }
        public string flg_activo { get; set; }
        public string flg_factura { get; set; }
        public string flg_boleta { get; set; }
        public string flg_default { get; set; }
        public string flg_auto_generado { get; set; }
        public string flg_ticket { get; set; }
        public string flg_nota_credito { get; set; }
        public string cod_sunat { get; set; }
        public string flg_ctrl_serie { get; set; }
        public string flg_retencion_igv { get; set; }
        public string flg_reimpresion { get; set; }
        public string dsc_formato_objeto { get; set; }
        public string dsc_formato_alterno_objeto { get; set; }
        public string flg_letra_cambio { get; set; }
        public string flg_config_x_defecto { get; set; }
        public string flg_doc_detraccion { get; set; }
        public Int16 num_ctd_serie { get; set; }
        public Int16 num_ctd_doc { get; set; }
        public string flg_guia_remision { get; set; }
        public string flg_otras_retenciones { get; set; }
        public string flg_contabiliza { get; set; }
        public string flg_detraccion { get; set; }
        public string flg_honorario { get; set; }
        public string flg_imprimible { get; set; }
        public string flg_boleto_viaje { get; set; }
        public string cod_sunat_anexo_2 { get; set; }
        public string flg_multiple_detalle { get; set; }
        public string flg_genera_trx_caja_automatica { get; set; }
        public string flg_nota_debito { get; set; }
        public string flg_doc_compra { get; set; }
        public string flg_doc_venta { get; set; }
        public string flg_genera_fe { get; set; }
        public string flg_otros { get; set; }
        public string flg_invoice { get; set; }
        public string flg_dua { get; set; }
        public string flg_retencion { get; set; }
    }
}
