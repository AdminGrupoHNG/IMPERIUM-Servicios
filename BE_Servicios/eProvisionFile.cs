using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eProvisionFile
    {
        public int nroservicio { get; set; }
        public string idTS { get; set; }
        public string nombreservicio { get; set; }
        public string moneda { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
        public int paxservicio { get; set; }
        public decimal gravado { get; set; }
        public decimal nogravado { get; set; }
        public decimal inafecto { get; set; }
        public decimal exonerado { get; set; }
        public decimal montoigv { get; set; }
        public decimal montofact { get; set; }
        public decimal total { get; set; }
        public string idproveedor { get; set; }
        public string ciudad { get; set; }
        public bool apligaigv { get; set; }
        public bool aplicaigvcosto { get; set; }
        public decimal porcentajerecargo { get; set; }
        public Int16 Pax1 { get; set; }
        public Int16 Pax2 { get; set; }
        public Int16 Pax3 { get; set; }
        public string indicapernocte { get; set; }
        public Int16 segmento { get; set; }
        public string FileD { get; set; }
        public int IdCiudad { get; set; }
        public string idservfile { get; set; }
        public bool Sel { get; set; }
        public string DescripcionFile { get; set; }
        public string EjecutivoFile { get; set; }
        public string EstadoFile { get; set; }
        public string DesTipoServ { get; set; }
        public string RazonSocial { get; set; }
        public decimal montototalexceso { get; set; }
        public int IdServicioCotiza { get; set; }

        //Datos para Programacion 
        public string ProveedorCotizado { get; set; }
        public decimal CostoTotalCotizado { get; set; }
        public string ProveedorProgramado { get; set; }
        public decimal CostoTotalProgramacion { get; set; }
        public string ProveedorFactura { get; set; }

        //Detalle de la Linea 
        public int IdDetalleFactura { get; set; }
        public int IdFactura { get; set; }
        public string NroFactura { get; set; }
        public int Item { get; set; }
        public string TipoLinea { get; set; }
        public decimal MontoLinea { get; set; }
        public string CodigoImpuestos { get; set; }
        public decimal Impuesto { get; set; }
        public decimal SubTotal { get; set; }
        public string DescripcionLinea { get; set; }
        public string CombinacionContableLinea { get; set; }
        public string JuegodeDistribucion { get; set; }
        public DateTime FechaEmisionLinea { get; set; }
        public string NroDocumento { get; set; }
        public int IdEntidad { get; set; }
        public string IdTipoDocAdmin { get; set; }
        public string DesIdTipoDocAdmin { get; set; }
        public int User { get; set; }
        public string TipoFile { get; set; }
        public int AnhoFile { get; set; }
        public int NumeroFile { get; set; }
        public DateTime? FechaServicio { get; set; }
        public DateTime FechaContable { get; set; }
        public string NomComercial { get; set; }
        public string RucProveedor { get; set; }
        public string Usuario { get; set; }
        public decimal montoigvcosto { get; set; }
        public decimal montoigvGasto { get; set; }

        //Cuenta Contable 
        public string Compañia { get; set; }
        public string FileDivision { get; set; }
        public string CuentaOracle { get; set; }
        public string CentroCosto { get; set; }

        //Facturas Proveedor
        public string NombreArchivo { get; set; }
        public string CarpetaDestino { get; set; }
        public bool FlgExistePDF { get; set; }
        public string EstadoFactura { get; set; }
        public string DescLocalidad { get; set; }
        public bool Incluir { get; set; }
    }
}
