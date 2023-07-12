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
    public class blRequerimiento
    {
        daSQL sql = new daSQL();

        public void CargaCombosChecked(string nCombo, CheckedComboBoxEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue, string cod_usuario = "")
        {
            combo.Text = "";
            string procedure = "usp_ConsultasVarias_Requerimiento";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            try
            {
                switch (nCombo)
                {
                    case "Estados":
                        dictionary.Add("opcion", 1);
                        sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                    case "EmpresasUsuarios":
                        procedure = "usp_Consulta_ListarProveedores";
                        dictionary.Add("opcion", 11);
                        dictionary.Add("cod_usuario", cod_usuario);
                        sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CargaCombosLookUp(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "",
                                      bool valorDefecto = false, string cod_cliente = "", string cod_usuario = "", string cod_tipo_transaccion = "",
                                      string cod_empresa = "", string cod_sede_empresa = "")
        {
            combo.Text = "";
            string procedure = "usp_ConsultasVarias_Requerimiento";
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
                    case "TipoFecha":
                        dictionary.Add("opcion", 2);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TiposReq":
                        dictionary.Add("opcion", 3);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Sedes":
                        dictionary.Add("opcion", 6);
                        dictionary.Add("cod_empresa", cod_empresa);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Areas":
                        dictionary.Add("opcion", 5);
                        dictionary.Add("cod_empresa", cod_empresa);
                        dictionary.Add("cod_sede_empresa", cod_sede_empresa);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "SedesCliente":
                        dictionary.Add("opcion", 7);
                        dictionary.Add("cod_cliente", cod_cliente);
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

        public List<T> CombosEnGridControl<T>(string nCombo, string cod_usuario = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            string procedure = "usp_ConsultasVarias_Requerimiento";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            //dictionary.Add("dato", dato);

            switch (nCombo)
            {
                case "EmpresasUsuarios":
                    procedure = "usp_Consulta_ListarProveedores";
                    dictionary.Add("opcion", 11);
                    dictionary.Add("cod_usuario", cod_usuario);
                    break;
            }

            myList = sql.ListaconSP<T>(procedure, dictionary);
            return myList;
        }

        public List<T> ListarRequerimiento<T>(int opcion, string cod_empresa = "", string cod_sede_empresa = "", string cod_cliente = "", string cod_area = "", string cod_tipo_fecha = "",
                                              string fch_inicio = "", string fch_fin = "", string cod_requerimiento = "") where T : class, new()
        {
            List<T> myList = new List<T>();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_empresa", cod_empresa},
                { "cod_sede_empresa", cod_sede_empresa},
                { "cod_cliente", cod_cliente},
                { "cod_area", cod_area},
                { "cod_tipo_fecha", cod_tipo_fecha},
                { "fch_inicio", fch_inicio},
                { "fch_fin", fch_fin},
                { "cod_requerimiento", cod_requerimiento }
            };

            myList = sql.ListaconSP<T>("usp_Listar_Requerimientos", oDictionary);
            return myList;
        }

        public List<T> ListarProductos<T>(int opcion, string cod_empresa = "") where T : class, new()
        {
            List<T> myList = new List<T>();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_empresa", cod_empresa}
            };

            myList = sql.ListaconSP<T>("usp_ConsultasVarias_Requerimiento", oDictionary);
            return myList;
        }

        public T Ins_Act_Requerimiento<T>(eRequerimiento eReq, string cod_usuario = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eReq.cod_empresa},
                { "cod_sede_empresa", eReq.cod_sede_empresa},
                { "cod_requerimiento", eReq.cod_requerimiento},
                { "cod_cliente", eReq.cod_cliente},
                { "cod_sede_cliente", eReq.cod_sede_cliente},
                { "cod_area", eReq.cod_area},
                { "flg_solicitud", eReq.flg_solicitud},
                { "dsc_nombre_solicitante", eReq.dsc_nombre_solicitante},
                { "fch_requerimiento", eReq.fch_requerimiento.ToString("yyyyMMdd")},
                { "fch_atencion", eReq.fch_atencion.ToString("yyyyMMdd")},
                { "dsc_observaciones", eReq.dsc_observaciones},
                { "dsc_justificacion", eReq.dsc_justificacion},
                { "dsc_items_requeridos", eReq.dsc_items_requeridos},
                { "cod_tipo", eReq.cod_tipo},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_Requerimientos", oDictionary);
            return obj;
        }

        public T Ins_Act_Detalle_Requerimiento<T>(eRequerimiento.eRequerimiento_Detalle eDet, string cod_usuario = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eDet.cod_empresa},
                { "cod_sede_empresa", eDet.cod_sede_empresa},
                { "cod_requerimiento", eDet.cod_requerimiento},
                { "flg_solicitud", eDet.flg_solicitud},
                { "dsc_anho", eDet.dsc_anho},
                { "cod_tipo_servicio", eDet.cod_tipo_servicio},
                { "cod_subtipo_servicio", eDet.cod_subtipo_servicio},
                { "cod_producto", eDet.cod_producto},
                { "num_cantidad", eDet.num_cantidad},
                { "num_restante", eDet.num_restante},
                { "cod_proveedor", eDet.cod_proveedor},
                { "imp_unitario", eDet.imp_unitario},
                { "imp_total", eDet.imp_total},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_Detalle_Requerimientos", oDictionary);
            return obj;
        }

        public T Cargar_Requerimiento<T>(int opcion, string empresa = "", string sede = "", string requerimiento = "", string flg_solicitud = "", int dsc_anho = 1) where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_requerimiento", requerimiento},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho}
            };

            obj = sql.ConsultarEntidad<T>("usp_Listar_Requerimientos", oDictionary);
            return obj;
        }

        public List<T> Cargar_Detalle_Requerimiento<T>(int opcion, string empresa = "", string sede = "", string requerimiento = "", string flg_solicitud = "", int dsc_anho = 1) where T : class, new()
        {
            List<T> myList = new List<T>();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_requerimiento", requerimiento},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho}
            };

            myList = sql.ListaconSP<T>("usp_Listar_Requerimientos", oDictionary);
            return myList;
        }

        public string Limpiar_Det_Requerimiento(string empresa = "", string sede = "", string requerimiento = "", string flg_solicitud = "", int dsc_anho = 1)
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_requerimiento", requerimiento},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Limpiar_Detalle_Requerimiento", oDictionary);
            return respuesta;
        }

        public string Aprobar_Requerimiento(string empresa = "", string sede = "", string requerimiento = "", string cod_usuario = "", string flg_solicitud = "", int dsc_anho = 1)
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_requerimiento", requerimiento},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho},
                { "cod_usuario", cod_usuario}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Aprobar_Requerimiento", oDictionary);
            return respuesta;
        }

        public string Desaprobar_Requerimiento(string empresa = "", string sede = "", string requerimiento = "", string cod_usuario = "", string flg_solicitud = "", int dsc_anho = 1)
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_requerimiento", requerimiento},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho},
                { "cod_usuario", cod_usuario}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Desaprobar_Requerimiento", oDictionary);
            return respuesta;
        }

        public string Atender_Requerimiento(string empresa = "", string sede = "", string requerimiento = "", string flg_solicitud = "", int dsc_anho = 1, string cod_usuario = "")
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_requerimiento", requerimiento},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho},
                { "cod_usuario", cod_usuario}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Atender_Requerimiento", oDictionary);
            return respuesta;
        }

        public string GenerarOC_Requerimiento(string empresa = "", string sede = "", string requerimiento = "", string cod_usuario = "", string flg_solicitud = "", int dsc_anho = 1)
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_requerimiento", requerimiento},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho},
                { "cod_usuario", cod_usuario}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_GenerarOC_Requerimiento", oDictionary);
            return respuesta;
        }

        public List<T> Cargar_Prod_Prov_Requerimientos<T>(int opcion, string empresa = "", string sede = "", string requerimiento = "", string flg_solicitud = "", int dsc_anho = 1) where T : class, new()
        {
            List<T> myList = new List<T>();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_requerimiento", requerimiento},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho}
            };

            myList = sql.ListaconSP<T>("usp_Listar_Requerimientos", oDictionary);
            return myList;
        }

        public string ValidarOC_Requerimiento(string empresa = "", string sede = "", string requerimiento = "", string cod_usuario = "")
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_requerimiento", requerimiento},
                { "cod_usuario", cod_usuario}
            };

            respuesta = sql.GetTable("usp_ValidarOC_Requerimiento", oDictionary).Rows[0].ItemArray[0].ToString();
            return respuesta;
        }

        public string Anular_Requerimiento(string empresa = "", string sede = "", string requerimiento = "", string cod_usuario = "", string flg_solicitud = "", int dsc_anho = 1)
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_requerimiento", requerimiento},
                { "flg_solicitud", flg_solicitud},
                { "dsc_anho", dsc_anho},
                { "cod_usuario", cod_usuario}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Anular_Requerimiento", oDictionary);
            return respuesta;
        }
    }
}
