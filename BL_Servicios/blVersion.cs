using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA_Servicios;
using BE_Servicios;

namespace BL_Servicios
{
    public class blVersion
    {
        daSQL sql = new daSQL();
        public eUsuario user = new eUsuario();
        public eVersion eVersion = new eVersion();

        public T ObtenerVersion<T>(int opcion) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "opcion", opcion}
            };

            obj = sql.ConsultarEntidad<T>("usp_ConsultasVarias_Login", dictionary);
            return obj;
        }

        public List<T> ListarHistorialVersiones<T>(int opcion, string cod_version = "", string dsc_version = "") where T : class, new()
        {
            List<T> myList = new List<T>();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_version", cod_version},
                { "dsc_version", dsc_version}
            };

            myList = sql.ListaconSP<T>("usp_Listar_HistorialVersiones", oDictionary);
            return myList;
        }

        public T Ins_Act_HistorialVersiones<T>(eVersion eVer, string cod_usuario = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_version", eVer.cod_version},
                { "dsc_version", eVer.dsc_version},
                { "fch_publicacion", eVer.fch_publicacion.ToString("yyyyMMdd")},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_HistorialVersiones", oDictionary);
            return obj;
        }

        public T Ins_Act_Detalle_HistorialVersiones<T>(eVersion.eVersionDetalle eDet, string cod_usuario = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_version", eDet.cod_version},
                { "dsc_version", eDet.dsc_version},
                { "num_item", eDet.num_item},
                { "dsc_descripcion", eDet.dsc_descripcion},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_HistorialVersiones_Detalle", oDictionary);
            return obj;
        }

        public List<T> Cargar_HistorialVersiones_Detalle<T>(int opcion, int cod_version = 0, string dsc_version = "") where T : class, new()
        {
            List<T> myList = new List<T>();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_version", cod_version},
                { "dsc_version", dsc_version}
            };

            myList = sql.ListaconSP<T>("usp_Listar_HistorialVersiones", oDictionary);
            return myList;
        }

        public string Elim_HistorialVersiones(int cod_version = 0, string dsc_version = "")
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_version", cod_version},
                { "dsc_version", dsc_version}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Eliminar_HistorialVersiones", oDictionary);
            return respuesta;
        }

        public string Elim_HistorialVersiones_Detalle(int cod_version = 0, string dsc_version = "", int num_item = 0)
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_version", cod_version},
                { "dsc_version", dsc_version},
                { "num_item", num_item}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Eliminar_HistorialVersiones_Detalle", oDictionary);
            return respuesta;
        }

        public string Publicar_Version(string dsc_version = "")
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "dsc_version", dsc_version}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Publicar_Version", oDictionary);
            return respuesta;
        }
    }
}
