using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eFormatoCotizacion
    {
        public string dsc_nombre_cliente { get; set; }
        public string dsc_fecha { get; set; }
        public string dsc_contacto { get; set; }
        public string dsc_direccion { get; set; }
        public string dsc_sede { get; set; }
        public string dsc_documento { get; set; }
        public int anio { get; set; }
        public string dsc_anio { get; set; }

        public override string ToString()
        {
            return dsc_nombre_cliente + " " + dsc_documento +" " + dsc_contacto + " " + dsc_fecha + " " + dsc_direccion + " " + dsc_sede + " " + anio + " " + dsc_anio;
        }
    }
}
