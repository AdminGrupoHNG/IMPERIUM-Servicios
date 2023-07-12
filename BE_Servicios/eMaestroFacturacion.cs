using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eMaestroFacturacion
    {
        //Combinacion Contable 
        public string Compañia { get; set; }
        public string Cuenta { get; set; }
        public string CentroCostos { get; set; }
        public string Division { get; set; }
        public string CodigoCombinacion { get; set; }
        public string CombinacionContable { get; set; }
        public string Descripcion { get; set; }
        public string UserInsert { get; set; }
        public string UserUpdate { get; set; }

        //Juegos
        public string CodigoDistribucion { get; set; }
        public string NombreDistribucion { get; set; }

        //Cierre 
        public int Anho { get; set; }
        public int Periodo { get; set; }
        public string DescripcionPeriodo { get; set; }
        public bool Estado { get; set; }
        public string DescripcionEstado { get; set; }
        public DateTime FechaInicioPeriodo { get; set; }
        public DateTime FechaFinPeriodo { get; set; }
        public string FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; }
    }
}
