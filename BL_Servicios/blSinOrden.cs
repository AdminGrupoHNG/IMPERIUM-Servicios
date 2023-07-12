using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_Servicios;
using DA_Servicios;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraEditors;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;

namespace BL_Servicios
{
    public class blSinOrden
    {
        daSQL sql = new daSQL();
        public List<T> ListarDetalleProductos<T>(int opcion) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion",opcion },
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarDetalleProducto", dictionary);
            return myList;

        }

        //public T Obt_Prec_ProductoSO<T>(eSinOrden.eSinOrdenDetalle eDet) where T : class, new()
        //{
        //    T obj = new T();

        //    Dictionary<string, object> oDictionary = new Dictionary<string, object>()
        //    {
        //       { "cod_producto", eDet.cod_producto}
        //    };

        //    obj = sql.ConsultarEntidad<T>("usp_Obtener_Precio_ProductoSO", oDictionary);
        //    return obj;
        //}

        public List<T> ListarProductos<T>(int opcion) where T : class, new()
        {
            List<T> myList = new List<T>();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion}

            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarProducto", oDictionary);
            return myList;
        }

        public List<T> ListarproductoDetalle<T>(int opcion) where T : class, new()
        {
            List<T> myList = new List<T>();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion}

            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarProveedorDetalle", oDictionary);
            return myList;
        }




    }

}

