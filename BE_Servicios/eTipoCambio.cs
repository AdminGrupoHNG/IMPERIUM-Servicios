using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eTipoCambio
    {
        public DateTime fch_cambio { get; set; }
        public decimal imp_cambio_compra { get; set; }
        public decimal imp_cambio_venta { get; set; }
        public string cod_moneda { get; set; }
        public int Anho { get; set; }
        public string dsc_mes { get; set; }
    }
}
