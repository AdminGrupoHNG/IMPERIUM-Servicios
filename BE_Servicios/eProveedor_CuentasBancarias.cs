using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eProveedor_CuentasBancarias
    {
        public string cod_proveedor { get; set; }
        public Int16 num_linea { get; set; }
        public string cod_banco { get; set; }
        public string dsc_banco { get; set; }
        public string cod_moneda { get; set; }
        public string dsc_moneda { get; set; }
        public string cod_tipo_cuenta { get; set; }
        public string dsc_tipo_cuenta { get; set; }
        public string dsc_cta_bancaria { get; set; }
        public string dsc_cta_interbancaria { get; set; }
        public string flg_pago_transferencia { get; set; }
        public string dsc_titular_cuenta { get; set; }
        public string dsc_observaciones { get; set; }
    }
}
