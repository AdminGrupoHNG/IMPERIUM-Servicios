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
    public class blOrdenCompra_Servicio
    {
        daSQL sql = new daSQL();

        public void CargaCombosLookUp(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", bool valorDefecto = false, string cod_proveedor = "", string cod_usuario = "", string cod_tipo_transaccion = "", string cod_empresa = "")
        {
            combo.Text = "";
            string procedure = "usp_ConsultasVarias_OrdenCompra_Servicio";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "EmpresasUsuarios":
                        procedure = "usp_Consulta_ListarProveedores";
                        dictionary.Add("opcion", 11);
                        dictionary.Add("cod_usuario", cod_usuario);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Estados":
                        dictionary.Add("opcion", 1);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoFecha":
                        dictionary.Add("opcion", 2);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Sedes":
                        dictionary.Add("opcion", 3);
                        dictionary.Add("@cod_empresa", cod_empresa);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Almacenes":
                        dictionary.Add("opcion", 4);
                        dictionary.Add("@cod_empresa", cod_empresa);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "ModPago":
                        dictionary.Add("opcion", 5);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Moneda":
                        dictionary.Add("opcion", 7);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                }

                combo.Properties.DataSource = tabla;
                combo.Properties.ValueMember = campoValueMember;
                combo.Properties.DisplayMember = campoDispleyMember;

                if (campoSelectedValue == "") { combo.ItemIndex = -1; } else { combo.EditValue = campoSelectedValue; }

                if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public T BuscarAlmacen<T>(string cod_almacen = "", string cod_empresa = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", 6},
                { "cod_almacen", cod_almacen},
                { "@cod_empresa", cod_empresa }
        };

            obj = sql.ConsultarEntidad<T>("usp_ConsultasVarias_OrdenCompra_Servicio", oDictionary);
            return obj;
        }

        public List<T> ListarOrdenesCompra<T>(int opcion, string cod_empresa = "", string cod_sede_empresa = "", string cod_proveedor = "", string cod_tipo_fecha = "",
                                              string fch_inicio = "", string fch_fin = "", string solicitud = "") where T : class, new()
        {
            List<T> myList = new List<T>();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "cod_empresa", cod_empresa},
                { "cod_sede_empresa", cod_sede_empresa},
                { "cod_proveedor", cod_proveedor },
                { "cod_tipo_fecha", cod_tipo_fecha},
                { "fch_inicio", fch_inicio},
                { "fch_fin", fch_fin},
                { "flg_solicitud", solicitud},
            };

            myList = sql.ListaconSP<T>("usp_Listar_OrdenCompra_Servicio", oDictionary);
            return myList;
        }

        public List<T> BuscarRequerimiento<T>(string cod_cliente = "") where T : class, new()
        {
            List<T> myList = new List<T>();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", 2},
                { "cod_empresa", ""},
                { "cod_cliente", cod_cliente},
                { "cod_estado_requerimiento", ""},
                { "cod_tipo_fecha", ""},
                { "fch_inicio", ""},
                { "fch_fin", ""},
                { "cod_requerimiento", "" }
            };

            myList = sql.ListaconSP<T>("usp_Listar_Requerimientos", oDictionary);
            return myList;
        }

        public T Ins_Act_OrdenCompra_Servicio<T>(eOrdenCompra_Servicio eOrdCom, string cod_usuario = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eOrdCom.cod_empresa},
                { "cod_sede_empresa", eOrdCom.cod_sede_empresa},
                { "cod_orden_compra_servicio", eOrdCom.cod_orden_compra_servicio},
                { "num_cotizacion", eOrdCom.num_cotizacion},
                { "cod_proveedor", eOrdCom.cod_proveedor},
                { "dsc_ruc", eOrdCom.dsc_ruc},
                { "flg_solicitud", eOrdCom.flg_solicitud},
                { "fch_emision", eOrdCom.fch_emision.ToString("yyyyMMdd")},
                { "cod_almacen", eOrdCom.cod_almacen},
                { "dsc_unidad_recepcion", eOrdCom.dsc_unidad_recepcion},
                { "imp_subtotal", eOrdCom.imp_subtotal},
                { "imp_igv", eOrdCom.imp_igv},
                { "imp_total", eOrdCom.imp_total},
                { "dsc_imp_total", eOrdCom.dsc_imp_total},
                { "dsc_terminos_condiciones", eOrdCom.dsc_terminos_condiciones},
                { "dsc_direccion_despacho", eOrdCom.dsc_direccion_despacho},
                { "cod_modalidad_pago", eOrdCom.cod_modalidad_pago},
                { "fch_despacho", eOrdCom.fch_despacho.ToString("yyyyMMdd")},
                { "cod_moneda", eOrdCom.cod_moneda},
                { "prc_CV", eOrdCom.prc_CV},
                { "prc_LI", eOrdCom.prc_LI},
                { "prc_CB", eOrdCom.prc_CB},
                { "prc_GG", eOrdCom.prc_GG},
                { "prc_ADM", eOrdCom.prc_ADM},
                { "prc_OPER", eOrdCom.prc_OPER},
                { "prc_GV", eOrdCom.prc_GV},
                { "dsc_observaciones", eOrdCom.dsc_observaciones},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_OrdenCompra_Servicio", oDictionary);
            return obj;
        }

        public T Ins_Act_Detalle_OrdenCompra_Servicio<T>(eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDet, string cod_usuario = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eDet.cod_empresa},
                { "cod_sede_empresa", eDet.cod_sede_empresa},
                { "cod_orden_compra_servicio", eDet.cod_orden_compra_servicio},
                { "flg_solicitud", eDet.flg_solicitud},
                { "dsc_anho", eDet.dsc_anho},
                { "cod_requerimiento", eDet.cod_requerimiento},
                { "num_item", eDet.num_item},
                { "cod_proveedor", eDet.cod_proveedor},
                { "dsc_ruc", eDet.dsc_ruc},
                { "cod_tipo_servicio", eDet.cod_tipo_servicio},
                { "cod_subtipo_servicio", eDet.cod_subtipo_servicio},
                { "cod_producto", eDet.cod_producto},
                { "dsc_servicio", eDet.dsc_servicio},
                { "cod_unidad_medida", eDet.cod_unidad_medida},
                { "num_cantidad", eDet.num_cantidad},
                { "imp_unitario", eDet.imp_unitario},
                { "imp_total", eDet.imp_total_det},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_OrdenCompra_Servicio_Detalle", oDictionary);
            return obj;
        }

        public T Obt_Prec_Producto<T>(eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDet) where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_proveedor", eDet.cod_proveedor},
                { "cod_producto", eDet.cod_producto}
            };

            obj = sql.ConsultarEntidad<T>("usp_Obtener_Precio_Producto", oDictionary);
            return obj;
        }

        public T Cargar_OrdenCompra_Servicio<T>(int opcion, string empresa = "", string sede = "", string ordenCompraServicio = "", string flg_solicitud = "", int dsc_anho = 1) where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_orden_compra_servicio", ordenCompraServicio},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho}
            };

            obj = sql.ConsultarEntidad<T>("usp_Listar_OrdenCompra_Servicio", oDictionary);
            return obj;
        }

        public List<T> Cargar_Detalle_OrdenCompra_Servicio<T>(int opcion, string empresa = "", string sede = "", string ordenCompraServicio = "", string flg_solicitud = "", int dsc_anho = 1) where T : class, new()
        {
            List<T> myList = new List<T>();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_orden_compra_servicio", ordenCompraServicio},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho}
            };

            myList = sql.ListaconSP<T>("usp_Listar_OrdenCompra_Servicio", oDictionary);
            return myList;
        }

        public string Limpiar_Det_OrdenCompra_Servicio(string empresa = "", string sede = "", string ordenCompraservicio = "", string flg_solicitud = "", int dsc_anho = 1)
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_orden_compra_servicio", ordenCompraservicio},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Limpiar_Detalle_OrdenCompra_Servicio", oDictionary);
            return respuesta;
        }

        public string Aprobar_Orden(string empresa = "", string sede = "", string ordenCompra = "", string flg_solicitud = "", int dsc_anho = 1, string cod_usuario = "")
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_orden_compra_servicio", ordenCompra},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho},
                { "cod_usuario", cod_usuario}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Aprobar_OrdenCompra_Servicio", oDictionary);
            return respuesta;
        }

        public string Desaprobar_Orden(string empresa = "", string sede = "", string ordenCompra = "", string flg_solicitud = "", int dsc_anho = 1, string cod_usuario = "")
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_orden_compra_servicio", ordenCompra},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho},
                { "cod_usuario", cod_usuario}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Desaprobar_OrdenCompra_Servicio", oDictionary);
            return respuesta;
        }

        public string Enviar_Orden(string empresa = "", string sede = "", string ordenCompra = "", string cod_usuario = "", string flg_solicitud = "", int dsc_anho = 1)
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_orden_compra_servicio", ordenCompra},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho},
                { "cod_usuario", cod_usuario}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Enviar_OrdenCompra_Servicio", oDictionary);
            return respuesta;
        }

        public string Atender_Orden(string empresa = "", string sede = "", string ordenCompra = "", string flg_solicitud = "", int dsc_anho = 1, string cod_usuario = "")
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_orden_compra_servicio", ordenCompra},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho},
                { "cod_usuario", cod_usuario}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Atender_OrdenCompra_Servicio", oDictionary);
            return respuesta;
        }

        public string Liquidar_Orden(string empresa = "", string sede = "", string ordenCompra = "", string flg_solicitud = "", int dsc_anho = 1, string cod_usuario = "")
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_orden_compra_servicio", ordenCompra},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho},
                { "cod_usuario", cod_usuario}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Liquidar_OrdenCompra_Servicio", oDictionary);
            return respuesta;
        }

        public DataTable Monto_Letras(decimal imp_total = 0)
        {
            DataTable dt = new DataTable();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "imp_total", imp_total}
            };

            dt = sql.GetTable("usp_Monto_Letras", oDictionary);
            return dt;
        }

        public string Anular_Orden(string empresa = "", string sede = "", string ordenCompra = "", string flg_solicitud = "", int dsc_anho = 1, string cod_usuario = "")
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_orden_compra_servicio", ordenCompra},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho},
                { "cod_usuario", cod_usuario}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Anular_OrdenCompra_Servicio", oDictionary);
            return respuesta;
        }
    }
}
