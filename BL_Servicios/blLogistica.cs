using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_Servicios;
using DA_Servicios;
using DevExpress.XtraEditors;
using Excel = Microsoft.Office.Interop.Excel;

namespace BL_Servicios
{
    public class blLogistica
    {
        daSQL sql = new daSQL();

        public void CargaCombosLookUp(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", bool valorDefecto = false,
                                    string cod_segmento = "", string cod_familia = "", string cod_tipo_servicio = "", string cod_empresa = "", string cod_sede_empresa = "",
                                    string dsc_variable = "", string cod_subtipo_servicio = "", string cod_producto = "")
        {
            combo.Text = "";
            string procedure = "usp_ConsultasVarias_Logistica";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "Segmento":
                        dictionary.Add("opcion", 1);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Familia":
                        dictionary.Add("opcion", 2);
                        dictionary.Add("cod_segmento", cod_segmento);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Clase":
                        dictionary.Add("opcion", 3);
                        dictionary.Add("cod_segmento", cod_segmento);
                        dictionary.Add("cod_familia", cod_familia);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Color":
                        dictionary.Add("opcion", 4);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "UnidadMedida":
                        dictionary.Add("opcion", 5);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoProducto":
                        dictionary.Add("opcion", 6);
                        dictionary.Add("cod_empresa", cod_empresa);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "SubTipoProducto":
                        dictionary.Add("opcion", 7);
                        dictionary.Add("cod_tipo_servicio", cod_tipo_servicio);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Marca":
                        dictionary.Add("opcion", 9);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Sexo":
                        dictionary.Add("opcion", 10);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoAlmacen":
                        dictionary.Add("opcion", 11);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoCaracteristica":
                        dictionary.Add("opcion", 12);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Almacen":
                        dictionary.Add("opcion", 13);
                        dictionary.Add("cod_empresa", cod_empresa);
                        dictionary.Add("cod_sede_empresa", cod_sede_empresa);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoMovimiento":
                        dictionary.Add("opcion", 14);
                        dictionary.Add("dsc_variable", dsc_variable);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "DistribucionCECO":
                        dictionary.Add("opcion", 15);
                        dictionary.Add("cod_empresa", cod_empresa);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "ProveedorProducto":
                        procedure = "usp_Consulta_ListarLogistica";
                        dictionary.Add("opcion", 12);
                        dictionary.Add("cod_tipo_servicio", cod_tipo_servicio);
                        dictionary.Add("cod_subtipo_servicio", cod_subtipo_servicio);
                        dictionary.Add("cod_producto", cod_producto);
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

        public List<T> Obtener_ListadosProductos<T>(int opcion, string cod_segmento = "", string cod_familia = "", string cod_clase = "",
                                                    string cod_tipo_servicio = "", string cod_subtipo_servicio = "", string cod_producto = "",
                                                    string cod_proveedor = "", string cod_empresa = "", string cod_empresa_multiple = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>() {
                { "opcion", opcion}, { "cod_segmento", cod_segmento}, { "cod_familia", cod_familia}, { "cod_clase", cod_clase},
                { "cod_tipo_servicio", cod_tipo_servicio}, { "cod_subtipo_servicio", cod_subtipo_servicio}, { "cod_producto", cod_producto},
                { "cod_proveedor", cod_proveedor}, { "cod_empresa", cod_empresa}, { "cod_empresa_multiple", cod_empresa_multiple}
            };
            myList = sql.ListaconSP<T>("usp_Consulta_ListarLogistica", oDictionary);
            return myList;
        }

        public T Obtener_DatosProducto<T>(int opcion, string cod_segmento = "", string cod_familia = "", string cod_clase = "",
                                        string cod_tipo_servicio = "", string cod_subtipo_servicio = "", string cod_producto = "",
                                        string cod_proveedor = "") where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion}, { "cod_segmento", cod_segmento}, { "cod_familia", cod_familia}, { "cod_clase", cod_clase},
                { "cod_tipo_servicio", cod_tipo_servicio}, { "cod_subtipo_servicio", cod_subtipo_servicio}, { "cod_producto", cod_producto},
                { "cod_proveedor", cod_proveedor}
            };

            objj = sql.ConsultarEntidad<T>("usp_Consulta_ListarLogistica", dictionary);
            return objj;
        }

        public T Insertar_Actualizar_Producto<T>(eProductos obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_tipo_servicio", obj.cod_tipo_servicio }, { "cod_subtipo_servicio", obj.cod_subtipo_servicio },
                { "cod_producto",obj.cod_producto }, { "dsc_producto", obj.dsc_producto }, { "ctd_stock_minimo", obj.ctd_stock_minimo },
                { "ctd_stock_maximo", obj.ctd_stock_maximo }, { "dsc_observaciones", obj.dsc_observaciones }, { "cod_sislog", obj.cod_sislog },
                { "cod_unidad_medida", obj.cod_unidad_medida }, { "ctd_volumen_m3", obj.ctd_volumen_m3 }, { "cod_producto_SUNAT", obj.cod_producto_SUNAT },
                { "cod_color", obj.cod_color }, { "cod_marca", obj.cod_marca }, { "cod_modelo", obj.cod_modelo }, { "num_peso", obj.num_peso },
                { "ctd_stock_actual", obj.ctd_stock_actual }, { "cod_tallauniforme", obj.cod_tallauniforme }, { "cod_sexo", obj.cod_sexo },
                { "flg_activo", obj.flg_activo }, { "flg_compuesto", obj.flg_compuesto }, { "flg_logo", obj.flg_logo }, { "dsc_modelo", obj.dsc_modelo },
            };

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_Productos", dictionary);
            return objj;
        }

        public T Insertar_Actualizar_SubProducto<T>(eProductos.eSubProductos obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_tipo_servicio", obj.cod_tipo_servicio }, { "cod_subtipo_servicio", obj.cod_subtipo_servicio },
                { "cod_producto",obj.cod_producto }, { "sub_cod_tipo_servicio", obj.sub_cod_tipo_servicio }, 
                { "sub_cod_subtipo_servicio", obj.sub_cod_subtipo_servicio }, { "sub_cod_producto",obj.sub_cod_producto }, 
                { "ctd_requerida",obj.ctd_requerida },
            };

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_SubProductos", dictionary);
            return objj;
        }

        public T Insertar_Actualizar_ProductoCostos<T>(eProductos.eProductosTarifas obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_tipo_servicio", obj.cod_tipo_servicio }, { "cod_subtipo_servicio", obj.cod_subtipo_servicio }, 
                { "cod_producto",obj.cod_producto }, { "num_item", obj.num_item }, { "fch_inicio", obj.fch_inicio }, 
                { "fch_fin", obj.fch_fin }, { "imp_costo", obj.imp_costo }, { "dsc_observacion", obj.dsc_observacion },
                { "dsc_ruc", obj.dsc_ruc }, { "cod_proveedor", obj.cod_proveedor },
            };

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_ProductoCostos", dictionary);
            return objj;
        }

        public T Insertar_Actualizar_ProductosProveedor<T>(eProductos.eProductosProveedor obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_tipo_servicio", obj.cod_tipo_servicio }, { "cod_subtipo_servicio", obj.cod_subtipo_servicio },
                { "cod_producto",obj.cod_producto }, { "dsc_ruc", obj.dsc_ruc }, { "cod_proveedor", obj.cod_proveedor }, 
                { "flg_activo", obj.flg_activo }, { "flg_vigente", obj.flg_vigente }, { "cod_usuario_registro", obj.cod_usuario_registro }
            };

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_ProductosProveedor", dictionary);
            return objj;
        }

        public T Insertar_Actualizar_Marcas<T>(eProveedor_Marca obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_marca", obj.cod_marca }, { "dsc_marca", obj.dsc_marca }, { "dsc_abreviado", obj.dsc_abreviado },
                { "flg_activo", obj.flg_activo }, { "cod_usuario_registro", obj.cod_usuario_registro }
            };

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_Marcas", dictionary);
            return objj;
        }

        public T Insertar_Actualizar_MarcasProveedor<T>(eProveedor_Marca obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_marca", obj.cod_marca }, { "cod_proveedor", obj.cod_proveedor }, 
                { "flg_activo", obj.flg_activo }, { "cod_usuario_registro", obj.cod_usuario_registro }
            };

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_MarcasProveedor", dictionary);
            return objj;
        }

        public T Insertar_Actualizar_TipoServicio<T>(eProductos obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_tipo_servicio", obj.cod_tipo_servicio }, { "dsc_tipo_servicio", obj.dsc_tipo_servicio },
                { "flg_activo", obj.flg_activo }, { "flg_materia_prima", obj.flg_materia_prima } ,
                { "flg_producto_terminado", obj.flg_producto_terminado }, { "flg_actividad_apoyo", obj.flg_actividad_apoyo }
            };

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_Tipo_Servicio", dictionary);
            return objj;
        }

        public T Insertar_Actualizar_SubTipoServicio<T>(eProductos obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_tipo_servicio", obj.cod_tipo_servicio }, { "cod_subtipo_servicio", obj.cod_subtipo_servicio },
                { "dsc_subtipo_servicio", obj.dsc_subtipo_servicio }, { "flg_activo", obj.flg_activo } , 
                { "ctd_volumen_m3", obj.ctd_volumen_m3 }
            };

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_SubTipo_Servicio", dictionary);
            return objj;
        }

        public T Obtener_DatosProveedor<T>(int opcion, string cod_proveedor = "", string cod_tipo_servicio = "") where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion}, { "cod_proveedor", cod_proveedor}, { "cod_tipo_servicio", cod_tipo_servicio}
            };

            objj = sql.ConsultarEntidad<T>("usp_Consulta_ListarProveedores", dictionary);
            return objj;
        }

        public T Insertar_Actualizar_Almacen<T>(eAlmacen obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_almacen", obj.cod_almacen }, { "cod_empresa", obj.cod_empresa }, { "cod_sede_empresa", obj.cod_sede_empresa }, 
                { "fch_creacion", obj.fch_creacion }, { "dsc_descripcion", obj.dsc_descripcion }, { "cod_tipo_almacen", obj.cod_tipo_almacen },
                { "cod_distrito", obj.cod_distrito }, { "cod_provincia", obj.cod_provincia }, { "cod_departamento", obj.cod_departamento },
                { "cod_pais", obj.cod_pais }, { "dsc_direccion", obj.dsc_direccion }, { "flg_activo", obj.flg_activo },
                { "cod_usuario_registro", obj.cod_usuario_registro }
            };

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_Almacen", dictionary);
            return objj;
        }

        public T Obtener_DatosLogistica<T>(int opcion, string cod_almacen = "", string cod_empresa = "", string cod_sede_empresa = "", 
                                        string cod_entrada = "", string cod_salida = "", string cod_guiaremision = "",
                                        string cod_orden_compra_servicio = "") where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion}, { "cod_almacen", cod_almacen}, { "cod_empresa", cod_empresa}, { "cod_sede_empresa", cod_sede_empresa},
                { "cod_entrada", cod_entrada }, { "cod_salida", cod_salida }, { "cod_guiaremision", cod_guiaremision }, 
                { "cod_orden_compra_servicio", cod_orden_compra_servicio }
            };

            objj = sql.ConsultarEntidad<T>("usp_Consulta_ListarLogistica", dictionary);
            return objj;
        }

        public List<T> Obtener_ListaLogistica<T>(int opcion, string cod_almacen = "", string cod_empresa = "", string cod_sede_empresa = "", 
                                                string cod_orden_compra_servicio = "", string cod_proveedor = "", string tipo_documento = "", 
                                                string cod_tipo_fecha = "", string FechaInicio = "", string FechaFin = "",
                                                string cod_moneda = "", string cod_entrada = "", string cod_salida = "", 
                                                string cod_guiaremision = "", string cod_requerimiento = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>() 
            {
                { "opcion", opcion}, { "cod_almacen", cod_almacen}, { "cod_empresa", cod_empresa}, { "cod_sede_empresa", cod_sede_empresa},
                { "cod_orden_compra_servicio", cod_orden_compra_servicio}, { "cod_proveedor", cod_proveedor}, { "tipo_documento", tipo_documento},
                { "cod_tipo_fecha", cod_tipo_fecha}, { "FechaInicio", FechaInicio}, { "FechaFin", FechaFin}, { "cod_moneda", cod_moneda},
                { "cod_entrada", cod_entrada }, { "cod_salida", cod_salida}, { "cod_guiaremision", cod_guiaremision},
                { "cod_requerimiento", cod_requerimiento}
            };
            myList = sql.ListaconSP<T>("usp_Consulta_ListarLogistica", oDictionary);
            return myList;
        }

        public List<T> Obtener_ListaVariasLogistica<T>(int opcion, string cod_empresa = "", string cod_sede_empresa = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                   { "opcion", opcion}, { "cod_empresa", cod_empresa}, { "cod_sede_empresa", cod_sede_empresa},
            };
            myList = sql.ListaconSP<T>("usp_ConsultasVarias_Logistica", oDictionary);
            return myList;
        }


        public T Insertar_Actualizar_EntradaCabecera<T>(eAlmacen.eEntrada_Cabecera obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_entrada", obj.cod_entrada }, { "cod_almacen", obj.cod_almacen }, { "cod_tipo_movimiento", obj.cod_tipo_movimiento }, { "cod_empresa", obj.cod_empresa },
                { "cod_sede_empresa", obj.cod_sede_empresa }, { "cod_orden_compra_servicio", obj.cod_orden_compra_servicio }, { "fch_documento", obj.fch_documento },
                { "fch_tipocambio", obj.fch_tipocambio }, { "imp_tipocambio", obj.imp_tipocambio }, { "tipo_documento", obj.tipo_documento },
                { "serie_documento", obj.serie_documento }, { "numero_documento", obj.numero_documento }, { "cod_proveedor", obj.cod_proveedor },
                { "flg_activo", obj.flg_activo }, { "cod_usuario_registro", obj.cod_usuario_registro }, { "dsc_glosa", obj.dsc_glosa }
            };

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_EntradaAlmacen_Cabecera", dictionary);
            return objj;
        }

        public T Insertar_Actualizar_EntradaDetalle<T>(eAlmacen.eEntrada_Detalle obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_entrada", obj.cod_entrada }, { "cod_almacen", obj.cod_almacen }, { "cod_empresa", obj.cod_empresa },
                { "cod_sede_empresa", obj.cod_sede_empresa }, { "cod_tipo_servicio", obj.cod_tipo_servicio }, { "cod_subtipo_servicio", obj.cod_subtipo_servicio },
                { "cod_producto", obj.cod_producto }, { "cod_unidad_medida", obj.cod_unidad_medida }, { "num_cantidad", obj.num_cantidad },
                { "num_cantidad_recibido", obj.num_cantidad_recibido }, { "num_cantidad_x_recibir", obj.num_cantidad_x_recibir }, { "num_item_costo", obj.num_item_costo },
                { "imp_costo", obj.imp_costo }, { "imp_total", obj.imp_total }, { "cod_usuario_registro", obj.cod_usuario_registro }
            };
            if (obj.fch_vencimiento.ToString().Contains("1/01/0001")) { dictionary.Add("fch_vencimiento", DBNull.Value); } else { dictionary.Add("fch_vencimiento", obj.fch_vencimiento); }

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_EntradaAlmacen_Detalle", dictionary);
            return objj;
        }

        public T Insertar_Actualizar_SalidaCabecera<T>(eAlmacen.eSalida_Cabecera obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_salida", obj.cod_salida }, { "cod_almacen", obj.cod_almacen }, { "cod_tipo_movimiento", obj.cod_tipo_movimiento }, { "cod_empresa", obj.cod_empresa },
                { "cod_sede_empresa", obj.cod_sede_empresa }, { "cod_requerimiento", obj.cod_requerimiento }, { "fch_documento", obj.fch_documento },
                { "fch_tipocambio", obj.fch_tipocambio }, { "imp_tipocambio", obj.imp_tipocambio }, { "dsc_pref_ceco", obj.dsc_pref_ceco }, 
                { "cod_almacen_destino", obj.cod_almacen_destino }, { "flg_activo", obj.flg_activo }, { "cod_usuario_registro", obj.cod_usuario_registro }
            };

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_SalidaAlmacen_Cabecera", dictionary);
            return objj;
        }

        public T Insertar_Actualizar_SalidaDetalle<T>(eAlmacen.eSalida_Detalle obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_salida", obj.cod_salida }, { "cod_almacen", obj.cod_almacen }, { "cod_empresa", obj.cod_empresa },
                { "cod_sede_empresa", obj.cod_sede_empresa }, { "cod_tipo_servicio", obj.cod_tipo_servicio }, { "cod_subtipo_servicio", obj.cod_subtipo_servicio },
                { "cod_producto", obj.cod_producto }, { "cod_unidad_medida", obj.cod_unidad_medida }, { "num_cantidad", obj.num_cantidad },
                { "cod_usuario_registro", obj.cod_usuario_registro }
            };

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_SalidaAlmacen_Detalle", dictionary);
            return objj;
        }

        public T Insertar_Actualizar_GuiaRemisionCabecera<T>(eAlmacen.eGuiaRemision_Cabecera obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_guiaremision", obj.cod_guiaremision }, { "cod_almacen", obj.cod_almacen }, { "cod_tipo_movimiento", obj.cod_tipo_movimiento }, 
                { "cod_empresa", obj.cod_empresa }, { "cod_sede_empresa", obj.cod_sede_empresa }, { "fch_documento", obj.fch_documento },
                { "cod_requerimiento", obj.cod_requerimiento }, { "imp_tipocambio", obj.imp_tipocambio }, { "fch_traslado", obj.fch_traslado },
                { "dsc_pref_ceco", obj.dsc_pref_ceco }, { "cod_transportista", obj.cod_transportista }, { "dsc_direccion", obj.dsc_direccion },
                { "cod_motivo_traslado", obj.cod_motivo_traslado }, { "flg_activo", obj.flg_activo }, { "cod_usuario_registro", obj.cod_usuario_registro }
            };
            if (obj.fch_tipocambio.ToString().Contains("1/01/0001")) { dictionary.Add("fch_tipocambio", DBNull.Value); } else { dictionary.Add("fch_tipocambio", obj.fch_tipocambio); }

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_GuiaRemisionAlmacen_Cabecera", dictionary);
            return objj;
        }

        public T Insertar_Actualizar_GuiaRemisionDetalle<T>(eAlmacen.eGuiaRemision_Detalle obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_guiaremision", obj.cod_guiaremision }, { "cod_almacen", obj.cod_almacen }, { "cod_empresa", obj.cod_empresa },
                { "cod_sede_empresa", obj.cod_sede_empresa }, { "cod_tipo_servicio", obj.cod_tipo_servicio }, { "cod_subtipo_servicio", obj.cod_subtipo_servicio },
                { "cod_producto", obj.cod_producto }, { "cod_unidad_medida", obj.cod_unidad_medida }, { "num_cantidad", obj.num_cantidad },
                { "cod_usuario_registro", obj.cod_usuario_registro }
            };

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_GuiaRemisionAlmacen_Detalle", dictionary);
            return objj;
        }

        public DataTable ReporteKardex(string empresa = "", string sede = "", string almacen = "", string fechaInicio = "", string fechaFin = "")
        {
            DataTable tabla = new DataTable();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_almacen", almacen},
                { "fch_inicio", fechaInicio},
                { "fch_fin", fechaFin}
            };

            tabla = sql.GetTable("usp_Reporte_Kardex_Valorizado", oDictionary);
            return tabla;
        }

        public DataTable ReporteKardex_Saldo(string empresa = "", string sede = "", string almacen = "", string fechaFin = "")
        {
            DataTable tabla = new DataTable();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "cod_almacen", almacen},
                { "fch_fin", fechaFin}
            };

            tabla = sql.GetTable("usp_Reporte_Kardex_Valorizado_Saldo", oDictionary);
            return tabla;
        }

        public void pDatosAExcel(string Coneccion, Excel.Application xls, string cadSql, string Nombre, string celda, bool AjustarColumnas = false, bool NoCab = false)
        {
            {
                var withBlock = xls.ActiveSheet.QueryTables.Add(Connection: Coneccion
                  , Destination: xls.Range[celda]);
                withBlock.CommandText = cadSql;
                withBlock.Name = Nombre;
                withBlock.FieldNames = !(NoCab); // True
                withBlock.RowNumbers = false;
                withBlock.FillAdjacentFormulas = false;
                withBlock.PreserveFormatting = true;
                withBlock.RefreshOnFileOpen = false;
                withBlock.BackgroundQuery = true;
                withBlock.SavePassword = true;
                withBlock.SaveData = true;
                if (AjustarColumnas == true)
                    withBlock.AdjustColumnWidth = true;
                withBlock.RefreshPeriod = 0;
                withBlock.PreserveColumnInfo = true;
                withBlock.Refresh(BackgroundQuery: false);
            }
        }

        public List<T> Obtener_ReporteLogistica_InventarioPermanenteValorizado<T>(string cod_almacen = "", string cod_empresa = "", string cod_sede_empresa = "", string FechaInicio = "", string FechaFin = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_almacen", cod_almacen}, { "cod_empresa", cod_empresa}, { "cod_sede_empresa", cod_sede_empresa},
                { "FechaInicio", FechaInicio}, { "FechaFin", FechaFin}
            };
            myList = sql.ListaconSP<T>("usp_Reporte_Logistica_InventarioPermanenteValorizado", oDictionary);
            return myList;
        }

        public List<T> Reporte_InventariounidadesFisicas<T>(string cod_almacen = "", string cod_empresa = "", string cod_sede_empresa = "", string FechaInicio = "", string FechaFin = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_almacen", cod_almacen},
                { "cod_empresa", cod_empresa},
                { "cod_sede_empresa", cod_sede_empresa},
                { "FechaInicio", FechaInicio},
                { "FechaFin", FechaFin}
            };
            myList = sql.ListaconSP<T>("usp_Reporte_InventarioUnidadesFisicas", oDictionary);
            return myList;
        }

        public DataTable ReporteOrdenesCompra(int opcion, string empresa = "", string sede = "", string almacen = "", string fechaInicio = "", string fechaFin = "")
        {
            DataTable tabla = new DataTable();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_empresa", empresa},
                { "cod_sede_empresa", sede},
                { "fch_inicio", fechaInicio},
                { "fch_fin", fechaFin},
                { "cod_almacen", almacen}
            };

            tabla = sql.GetTable("usp_Listar_OrdenCompra", oDictionary);
            return tabla;
        }

        public List<T> ListarTablasTipoSubTipo<T>(string tabla, int opcion, string cod_tipo_servicio = "") where T : class, new()
        {
            string procedure = "usp_ConsultasVarias_Tipo_Subtipo";
            Dictionary<string, object> oDictionary = new Dictionary<string, object>();
            List<T> myList = new List<T>();

            switch (tabla)
            {
                case "Tipo":
                    oDictionary.Add("opcion", 1);
                    break;
                case "SubTipo":
                    oDictionary.Add("opcion", 2);
                    oDictionary.Add("cod_tipo_servicio", cod_tipo_servicio);
                    break;
                case "EmpresaTipo":
                    oDictionary.Add("opcion", 3);
                    oDictionary.Add("cod_tipo_servicio", cod_tipo_servicio);
                    break;
            }

            myList = sql.ListaconSP<T>(procedure, oDictionary);
            return myList;
        }
        public List<T> CombosEnGridControl<T>(int opcion) where T : class, new()
        {
            List<T> myList = new List<T>();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion}
            };

            myList = sql.ListaconSP<T>("usp_ConsultasVarias_Tipo_Subtipo", oDictionary);
            return myList;
        }

        public string Asignar_Tipo_SubTipo_Empresa(string empresa = "", string cod_tipo_servicio = "", string cod_subtipo_servicio = "")
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_tipo_servicio", cod_tipo_servicio},
                { "cod_subtipo_servicio", cod_subtipo_servicio}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Asignar_Tipo_SubTipo_Empresa", oDictionary);
            return respuesta;
        }

        public string Desasignar_Tipo_SubTipo_Empresa(string empresa = "", string cod_tipo_servicio = "", string cod_subtipo_servicio = "")
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_tipo_servicio", cod_tipo_servicio},
                { "cod_subtipo_servicio", cod_subtipo_servicio}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Desasignar_Tipo_SubTipo_Empresa", oDictionary);
            return respuesta;
        }
    }
}
