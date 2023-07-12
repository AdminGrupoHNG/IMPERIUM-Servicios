using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eDocumentoInterno
    {
        public string tipo_documento { get; set; }
        public string serie_documento { get; set; }
        public decimal numero_documento { get; set; }
        public string cod_proveedor { get; set; }
        public string dsc_proveedor { get; set; }
        public string dsc_ruc { get; set; }
        public string dsc_glosa { get; set; }
        public string cod_moneda { get; set; }
        public decimal imp_total { get; set; }
        public DateTime fch_documento { get; set; }
        public string dsc_referencia { get; set; }
        public string dsc_observacion { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public string dsc_usuario_registro { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
        public string dsc_usuario_cambio { get; set; }
        public string dsc_pref_ceco { get; set; }
        public string NombreArchivo { get; set; }
        public string flg_PDF { get; set; }
        public string idPDF { get; set; }
    }
}
