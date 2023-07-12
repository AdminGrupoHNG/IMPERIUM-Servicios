using BE_Servicios;
using DA_Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Servicios
{
    public class blFormatoDocumentos
    {
        daSQL sql = new daSQL();
        public eFormatoMD_Vinculo Obtener_PlantillaDeFormatos(string cod_empresa="00002", string cod_formato= "00002", string cod_solucion = "004")
        {
            var result = ConsultaVarias_FormatoMDocumento<eFormatoMD_Vinculo>(new pQFormatD()
            {
                Opcion = 8,
                Cod_empresaSplit = cod_empresa,
                Cod_solucion = cod_solucion,
                Cod_formatoMD_vinculoSplit = cod_formato
            });

            return result.Count > 0 && result != null ? result.FirstOrDefault() : null;
        }

        public List<T> ConsultaVarias_FormatoMDocumento<T>(pQFormatD param) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", param.Opcion },
                { "cod_usuario", param.Cod_usuario},
                { "cod_estado", param.Cod_estado},
                { "cod_formatoMD_seguimiento", param.Cod_formatoMD_seguimiento},
                { "cod_empresaSplit", param.Cod_empresaSplit},
                { "dsc_formatoMD_general", param.Dsc_formatoMD_general},
                { "cod_formatoMD_generalSplit", param.Cod_formatoMD_generalSplit},
                { "cod_formatoMD_vinculoSplit", param.Cod_formatoMD_vinculoSplit},
                { "cod_trabajadorSplit", param.Cod_trabajadorSplit},
                { "cod_solucion", param.Cod_solucion},
            };

            myList = this.sql.ListaconSP<T>("Usp_RHU_ConsultasVarias_FormatoMDocumento", oDictionary);
            return myList;
        }

        public List<T> ConsultaVariosFormato<T>(int opcion, string cod_formato = "", string cod_solucion = "") where T : class, new()
        {
            List<T> myList = new List<T>();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_solucion", cod_solucion},
                { "cod_formatoMD_generalSplit", cod_formato},
            };

            myList = sql.ListaconSP<T>("Usp_RHU_ConsultasVarias_FormatoMDocumento", oDictionary);
            return myList;
        }
    }
}
