using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class ePrePagos
    {
        public int IdEntidad { get; set; }
        public string NomComercial { get; set; }
        public string RazonSocial { get; set; }
        public string Girar { get; set; }
        public decimal Monto { get; set; }
        public decimal ImporteaPagar { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public decimal PrePago { get; set; }
        public int Idciudad { get; set; }
        public string DescCiudad { get; set; }
        public string Moneda { get; set; }
        public string Desglose { get; set; }
        public string RUC { get; set; }
        public DateTime Creado { get; set; }
        public string UsuarioRegistro { get; set; }
        public DateTime Modificado { get; set; }
        public string UsuarioModificado { get; set; }
        public string Ejecutivo { get; set; }
        public string EstadoPrepago { get; set; }
        public string EstadoVinculo { get; set; }
        public decimal ImporteVinculo { get; set; }
        public decimal Saldo { get; set; }
        public int IdEstadoGestion { get; set; }
        public string DescEstadoGestion { get; set; }
        public string ObservacionGestion { get; set; }
        public Int16 Pax { get; set; }

        //Datos para llamar la factura 
        public int IdFactura { get; set; }
        public string NroFactura { get; set; }
        public string NombreArchivo { get; set; }
        public string CarpetaDestino { get; set; }
        public bool FlgExistePDF { get; set; }
        public string EstadoFactura { get; set; }

        //ParaGrabar Prepago
        public DateTime FechaPrepago { get; set; }
        public string TipoFile { get; set; }
        public int AnhoFile { get; set; }
        public int MesFile { get; set; }
        public int NumeroFile { get; set; }
        public string EstadoFile { get; set; }
        public string EjecutivoFile { get; set; }
        public string DescripcionFile { get; set; }
        public string Localizador { get; set; }
        public int users { get; set; }
        public int CtaBancaEnGlosa { get; set; }
        public int Anexo { get; set; }
        public int IdBanco { get; set; }
        public int TipoCuenta { get; set; }
        public string NroPrepago { get; set; }
        public string NroCuenta { get; set; }
        public string Banco { get; set; }
        public bool Anulado { get; set; }
        public bool Cancelado { get; set; }
        public bool indicaVisado { get; set; }
        public string File { get; set; }
        public bool Sel { get; set; }
        public int Id { get; set; }
        public decimal detraccionporc { get; set; }
        public decimal retencionporc { get; set; }
        public decimal tcindicador { get; set; }
        public string Division { get; set; }
    }
}
