using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_Servicios;
using DA_Servicios;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;

namespace BL_Servicios
{
    public class blAnalisisServicio
    {
        daSQL sql = new daSQL();

        public List<T> ListarGeneral<T>(string entidad, string empresa = "", string sede = "", string usuario = "", string tipo = "", string cliente = "", string area = "", string cargo = "", int opcion = 0) where T : class, new()
        {
            string procedure = "usp_srv_ConsultaVarias_Analisis";
            List<T> myList = new List<T>();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>();

            switch (entidad)
            {
                case "EmpresasxUsuario":
                    oDictionary.Add("opcion", 1);
                    oDictionary.Add("cod_usuario", usuario);
                    break;
                case "Tipo":
                    oDictionary.Add("opcion", opcion == 0 ? 5 : opcion);
                    oDictionary.Add("cod_empresa", empresa);
                    break;
                case "ProductoxTipo":
                    oDictionary.Add("opcion", 6);
                    oDictionary.Add("cod_empresa", empresa);
                    oDictionary.Add("cod_tipo_servicio", tipo);
                    break;
                case "Personal":
                    oDictionary.Add("opcion", 8);
                    oDictionary.Add("cod_empresa", empresa);
                    oDictionary.Add("cod_Sede_empresa", sede);
                    break;
                case "TipoPrestacion":
                    oDictionary.Add("opcion", 9);
                    oDictionary.Add("cod_empresa", empresa);
                    break;
                case "Categorias":
                    oDictionary.Add("opcion", 11);
                    break;
                case "SedesCliente":
                    oDictionary.Add("opcion", 3);
                    oDictionary.Add("cod_cliente", cliente);
                    break;
                case "Cargo":
                    oDictionary.Add("opcion", 13);
                    oDictionary.Add("cod_empresa", empresa);
                    oDictionary.Add("cod_sede_empresa", sede);
                    oDictionary.Add("cod_area", area);
                    oDictionary.Add("cod_cargo", cargo);
                    break;
                case "Turnos":
                    oDictionary.Add("opcion", 14);
                    break;
                case "Minutos":
                    oDictionary.Add("opcion", 16);
                    break;
                case "Otros":
                    oDictionary.Add("opcion", 18);
                    break;
                case "Conceptos":
                    oDictionary.Add("opcion", 19);
                    break;
                case "Uniformes":
                    oDictionary.Add("opcion", 20);
                    oDictionary.Add("cod_empresa", empresa);
                    oDictionary.Add("cod_cargo", cargo);
                    break;
                case "Totales":
                    oDictionary.Add("opcion", 21);
                    break;
                case "Generales":
                    oDictionary.Add("opcion", 22);
                    break;
                case "MaqAlcance":
                    oDictionary.Add("opcion", 23);
                    oDictionary.Add("cod_empresa", empresa);
                    break;
                case "Dotaciones":
                    oDictionary.Add("opcion", 24);
                    break;
                case "Maquinas":
                    oDictionary.Add("opcion", 25);
                    oDictionary.Add("cod_empresa", empresa);
                    break;
            }

            myList = sql.ListaconSP<T>(procedure, oDictionary);
            return myList;
        }

        public void CargaCombosLookUp(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "",
                                      bool valorDefecto = false, string cod_usuario = "", string cod_empresa = "", string cod_sede_empresa = "",
                                      string cod_analisis = "", int cod_sede_cliente = 0)
        {
            combo.Text = "";
            string procedure = "usp_srv_ConsultaVarias_Analisis";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "EmpresasUsuarios":
                        dictionary.Add("opcion", 1);
                        dictionary.Add("cod_usuario", cod_usuario);
                        break;
                    case "Sedes":
                        dictionary.Add("opcion", 7);
                        dictionary.Add("cod_empresa", cod_empresa);
                        break;
                    case "Tiempo":
                        dictionary.Add("opcion", 10);
                        break;
                    case "Area":
                        dictionary.Add("opcion", 12);
                        dictionary.Add("cod_empresa", cod_empresa);
                        dictionary.Add("cod_sede_empresa", cod_sede_empresa);
                        break;
                    case "TipoPrestacion":
                        dictionary.Add("opcion", 15);
                        dictionary.Add("cod_empresa", cod_empresa);
                        dictionary.Add("cod_sede_empresa", cod_sede_empresa);
                        dictionary.Add("cod_analisis", cod_analisis);
                        dictionary.Add("cod_sede_cliente", cod_sede_cliente);
                        break;
                }

                tabla = sql.ListaDatatable(procedure, dictionary);

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

        //public List<T> ListarSedesCliente<T>(string cliente) where T : class, new()
        //{
        //    List<T> myList = new List<T>();

        //    Dictionary<string, object> oDictionary = new Dictionary<string, object>()
        //    {
        //        { "opcion", 3},
        //        { "cod_cliente", cliente }
        //    };

        //    myList = sql.ListaconSP<T>("usp_srv_ConsultaVarias_Analisis", oDictionary);
        //    return myList;
        //}

        public T Ins_Act_Analisis<T>(eAnalisis eAns, string cod_usuario = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eAns.cod_empresa},
                { "cod_sede_empresa", eAns.cod_sede_empresa},
                { "cod_analisis", eAns.cod_analisis},
                { "cod_cliente", eAns.cod_cliente},
                { "cod_estado_analisis", eAns.cod_estado_analisis},
                { "fch_requerimiento", eAns.fch_requerimiento.ToString("yyyyMMdd")},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_srv_Insertar_Actualizar_Analisis", oDictionary);
            return obj;
        }

        public T Ins_Act_Analisis_Sedes<T>(eAnalisis.eAnalisis_Sedes eAns, string cod_usuario = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eAns.cod_empresa},
                { "cod_sede_empresa", eAns.cod_sede_empresa},
                { "cod_analisis", eAns.cod_analisis},
                { "cod_sede_cliente", eAns.cod_sede_cliente},
                { "num_m2", eAns.num_m2},
                { "num_m3", eAns.num_m3},
                { "fch_visita", eAns.fch_visita.ToString("yyyyMMdd")},
                { "dsc_hora_inicio", eAns.dsc_hora_inicio_visita.ToString("HH:mm:ss")},
                { "dsc_hora_fin", eAns.dsc_hora_fin_visita.ToString("HH:mm:ss")},
                { "dsc_observaciones", eAns.dsc_observaciones},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_srv_Insertar_Actualizar_Analisis_Sedes", oDictionary);
            return obj;
        }

        public T Ins_Act_Analisis_Sedes_Prestacion<T>(eAnalisis.eAnalisis_Sedes.eAnalisis_Sedes_Prestacion eAns, string cod_usuario = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eAns.cod_empresa},
                { "cod_sede_empresa", eAns.cod_sede_empresa},
                { "cod_analisis", eAns.cod_analisis},
                { "cod_sede_cliente", eAns.cod_sede_cliente},
                { "cod_tipo_prestacion", eAns.cod_tipo_prestacion},
                { "num_servicio", eAns.num_servicio},
                { "num_version", eAns.num_version},
                { "flg_habilitado", eAns.flg_habilitado},
                { "dsc_periodo", eAns.dsc_periodo},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_srv_Insertar_Actualizar_Analisis_Sedes_Prestacion", oDictionary);
            return obj;
        }

        public T Ins_Act_Alcance_Analisis<T>(eAnalisis.eAnalisis_Alcance eDet, string cod_usuario = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eDet.cod_empresa},
                { "cod_sede_empresa", eDet.cod_sede_empresa},
                { "cod_analisis", eDet.cod_analisis},
                { "num_servicio", eDet.num_servicio},
                { "num_item", eDet.num_item},
                { "dsc_actividad", eDet.dsc_actividad},
                { "num_cantidad", eDet.num_cantidad},
                { "flg_maq_equipo", eDet.flg_maq_equipo},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_srv_Insertar_Actualizar_Alcance_Analisis", oDictionary);
            return obj;
        }

        public T Ins_Act_Personal_Analisis<T>(eAnalisis.eAnalisis_Personal eDet, string cod_usuario = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eDet.cod_empresa},
                { "cod_sede_empresa", eDet.cod_sede_empresa},
                { "cod_analisis", eDet.cod_analisis},
                { "num_servicio", eDet.num_servicio},
                { "num_item", eDet.num_item},
                { "cod_cargo", eDet.cod_cargo},
                { "num_orden", eDet.num_orden},
                { "flg_descansero", eDet.flg_descansero ? "SI" : "NO" },
                { "flg_horario", eDet.flg_horario ? "SI" : "NO" },
                { "cod_turno", eDet.cod_turno},
                { "dsc_hora_inicio", eDet.dsc_hora_inicio.ToString("HH:mm:ss")},
                { "dsc_hora_fin", eDet.dsc_hora_fin.ToString("HH:mm:ss")},
                { "num_horas", eDet.num_horas},
                { "num_horas_diu", eDet.num_horas_diu},
                { "num_horas_noc", eDet.num_horas_noc},
                { "num_horas_extra", eDet.num_horas_extra},
                { "num_horas_ext_diu", eDet.num_horas_ext_diu},
                { "num_horas_ext_noc", eDet.num_horas_ext_noc},
                { "num_min_almuerzo", eDet.num_min_almuerzo},
                { "num_hora_dia", eDet.num_hora_dia},
                { "num_dia_semana", eDet.num_dia_semana},
                { "flg_feriado", eDet.flg_feriado ? "SI" : "NO"},
                { "num_cantidad", eDet.num_cantidad},
                { "imp_salario", eDet.imp_salario},
                { "imp_horas_diu", eDet.imp_horas_diu},
                { "imp_horas_noc", eDet.imp_horas_noc},
                { "imp_salario_extra", eDet.imp_salario_extra},
                { "imp_horas_ext_diu", eDet.imp_horas_ext_diu},
                { "imp_horas_ext_noc", eDet.imp_horas_ext_noc},
                { "imp_bono_nocturno", eDet.imp_bono_nocturno},
                { "imp_bono_productividad", eDet.imp_bono_productividad},
                { "imp_movilidad", eDet.imp_movilidad},
                { "imp_feriado", eDet.imp_feriado},
                { "imp_salario_total", eDet.imp_salario_total},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_srv_Insertar_Actualizar_Personal_Analisis", oDictionary);
            return obj;
        }

        public T Ins_Act_Uniformes_Analisis<T>(eAnalisis.eAnalisis_Personal_Uniformes eDet, string cod_usuario = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eDet.cod_empresa},
                { "cod_sede_empresa", eDet.cod_sede_empresa},
                { "cod_analisis", eDet.cod_analisis},
                { "num_servicio", eDet.num_servicio},
                { "num_item", eDet.num_item},
                { "cod_cargo", eDet.cod_cargo},
                { "cod_producto", eDet.cod_producto},
                { "cod_tipo_servicio", eDet.cod_tipo_servicio},
                { "cod_subtipo_servicio", eDet.cod_subtipo_servicio},
                { "cod_unidad_medida", eDet.cod_unidad_medida},
                { "num_cantidad", eDet.num_cantidad},
                { "imp_unitario", eDet.imp_unitario},
                { "imp_total", eDet.imp_total},
                { "prc_margen", eDet.prc_margen},
                { "imp_venta", eDet.imp_venta},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_srv_Insertar_Actualizar_Uniformes_Analisis", oDictionary);
            return obj;
        }

        public T Ins_Act_Producto_Analisis<T>(eAnalisis.eAnalisis_Producto eDet, string cod_usuario = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eDet.cod_empresa},
                { "cod_sede_empresa", eDet.cod_sede_empresa},
                { "cod_analisis", eDet.cod_analisis},
                { "num_servicio", eDet.num_servicio},
                { "cod_producto", eDet.cod_producto},
                { "cod_tipo_servicio", eDet.cod_tipo_servicio},
                { "cod_subtipo_servicio", eDet.cod_subtipo_servicio},
                { "cod_dotacion", eDet.cod_dotacion},
                { "cod_unidad_medida", eDet.cod_unidad_medida},
                { "num_cantidad", eDet.num_cantidad},
                { "imp_unitario", eDet.imp_unitario},
                { "imp_total", eDet.imp_total},
                { "prc_margen", eDet.prc_margen},
                { "imp_venta", eDet.imp_venta},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_srv_Insertar_Actualizar_Producto_Analisis", oDictionary);
            return obj;
        }

        public T Ins_Act_Maquinaria_Analisis<T>(eAnalisis.eAnalisis_Maquinaria eDet, string cod_usuario = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eDet.cod_empresa},
                { "cod_sede_empresa", eDet.cod_sede_empresa},
                { "cod_analisis", eDet.cod_analisis},
                { "num_servicio", eDet.num_servicio},
                { "cod_activo_fijo", eDet.cod_activo_fijo},
                { "num_cantidad", eDet.num_cantidad},
                { "imp_unitario", eDet.imp_unitario},
                { "imp_total", eDet.imp_total},
                { "num_meses_dep", eDet.num_meses_dep},
                { "imp_mensual", eDet.imp_mensual},
                { "prc_margen", eDet.prc_margen},
                { "imp_venta", eDet.imp_venta},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_srv_Insertar_Actualizar_Maquinaria_Analisis", oDictionary);
            return obj;
        }

        public T Ins_Act_Otros_Analisis<T>(eAnalisis.eAnalisis_Otros eDet, string cod_usuario = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eDet.cod_empresa},
                { "cod_sede_empresa", eDet.cod_sede_empresa},
                { "cod_analisis", eDet.cod_analisis},
                { "num_servicio", eDet.num_servicio},
                { "num_item", eDet.num_item},
                { "cod_concepto", eDet.cod_concepto},
                { "dsc_descripcion", eDet.dsc_descripcion},
                { "num_cantidad", eDet.num_cantidad},
                { "prc_ley", eDet.prc_ley},
                { "imp_unitario", eDet.imp_unitario},
                { "imp_total", eDet.imp_total},
                { "prc_margen", eDet.prc_margen},
                { "imp_venta", eDet.imp_venta},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_srv_Insertar_Actualizar_Otros_Analisis", oDictionary);
            return obj;
        }

        public T Ins_Act_Est_Cst_Analisis<T>(eAnalisis.eAnalisis_Est_Cst eDet, string cod_usuario = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eDet.cod_empresa},
                { "cod_sede_empresa", eDet.cod_sede_empresa},
                { "cod_analisis", eDet.cod_analisis},
                { "num_servicio", eDet.num_servicio},
                { "cod_concepto", eDet.cod_concepto},
                { "cod_item", eDet.cod_item},
                { "dsc_item", eDet.dsc_item},
                { "prc_ley", eDet.prc_ley},
                { "imp_unitario", eDet.imp_unitario},
                { "prc_margen", eDet.prc_margen},
                { "imp_total", eDet.imp_total},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_srv_Insertar_Actualizar_Est_Cst_Analisis", oDictionary);
            return obj;
        }

        public string Eliminar_Reg_Analisis(string tabla, string empresa = "", string sede = "", string analisis = "", string cliente = "", int sedeCliente = 0, int servicio = 0, string producto = "", string maquinaria = "", int item = 0, string tipo_servicio = "", string tipo = "", string concepto = "", string codItem = "", string cargo = "")
        {
            string respuesta = "";
            string procedure = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_analisis", analisis},
            };

            switch (tabla)
            {
                case "Sedes":
                    oDictionary.Add("cod_sede_cliente", sedeCliente);
                    procedure = "usp_srv_Eliminar_Sedes_Analisis";
                    break;
                case "Prestacion":
                    oDictionary.Add("cod_sede_cliente", sedeCliente);
                    oDictionary.Add("cod_tipo_prestacion", tipo_servicio);
                    procedure = "usp_srv_Eliminar_Sedes_Prest_Analisis";
                    break;
                case "Alcance":
                    oDictionary.Add("num_servicio", servicio);
                    oDictionary.Add("num_item", item);
                    procedure = "usp_srv_Eliminar_Alcance_Analisis";
                    break;
                case "Personal":
                    oDictionary.Add("num_servicio", servicio);
                    oDictionary.Add("num_item", item);
                    procedure = "usp_srv_Eliminar_Personal_Analisis";
                    break;
                case "Uniformes":
                    oDictionary.Add("num_servicio", servicio);
                    oDictionary.Add("num_item", item);
                    oDictionary.Add("cod_cargo", cargo);
                    oDictionary.Add("cod_producto", producto);
                    procedure = "usp_srv_Eliminar_Uniforme_Analisis";
                    break;
                case "Maquinaria":
                    oDictionary.Add("num_servicio", servicio);
                    oDictionary.Add("cod_activo_fijo", maquinaria);
                    procedure = "usp_srv_Eliminar_Maquinaria_Analisis";
                    break;
                case "Producto":
                    oDictionary.Add("num_servicio", servicio);
                    oDictionary.Add("cod_producto", producto);
                    procedure = "usp_srv_Eliminar_Producto_Analisis";
                    break;
                case "Otros":
                    oDictionary.Add("num_servicio", servicio);
                    oDictionary.Add("num_item", item);
                    procedure = "usp_srv_Eliminar_Otros_Analisis";
                    break;
                case "EstCst":
                    oDictionary.Add("cod_tipo", tipo);
                    oDictionary.Add("cod_concepto", concepto);
                    oDictionary.Add("cod_item", codItem);
                    procedure = "usp_srv_Eliminar_Est_Cst_Analisis";
                    break;
            }

            respuesta = sql.ExecuteSPRetornoValor(procedure, oDictionary);
            return respuesta;
        }

        public List<T> ListarAnalisis<T>(int opcion, string empresa = "", string sede = "", string analisis = "", int sedeCliente = 0, int servicio = 0, string estado = "", string tipoFecha = "", string fechaIni = "", string fechaFin = "", string area = "", string cargo = "", string tipoServicio = "") where T : class, new()
        {
            List<T> myList = new List<T>();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_analisis", analisis},
                { "cod_sede_cliente", sedeCliente},
                { "num_servicio", servicio},
                { "cod_estado", estado},
                { "cod_tipo_fecha", tipoFecha},
                { "fch_inicio", fechaIni},
                { "fch_fin", fechaFin},
                { "cod_tipo_prestacion", tipoServicio}
            };

            myList = sql.ListaconSP<T>("usp_srv_Consulta_ListarAnalisis", oDictionary);
            return myList;
        }

        public T Ins_Act_Cargo<T>(eDatos eCar) where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eCar.AtributoUno},
                { "cod_sede_empresa", eCar.AtributoDos},
                { "cod_area", eCar.AtributoTres},
                { "cod_cargo", eCar.AtributoCuatro},
                { "dsc_cargo", eCar.AtributoCinco},
                { "imp_salario_min", eCar.AtributoOnce},
                { "imp_salario_max", eCar.AtributoDoce}
            };

            obj = sql.ConsultarEntidad<T>("usp_srv_Insertar_Actualizar_Cargo", oDictionary);
            return obj;
        }

        public string Clonar_Analisis(string empresa = "", string sede = "", string analisis = "", int sedeCliente = 0, int servicio = 0, string obs = "")
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_analisis", analisis},
                { "cod_sede_cliente", sedeCliente},
                { "num_servicio", servicio},
                { "dsc_obs_version", obs}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_srv_Clonar_Analisis", oDictionary);
            return respuesta;
        }
    }
}
