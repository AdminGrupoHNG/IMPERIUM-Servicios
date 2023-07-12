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
    public class blFactura
    {
        daSQL sql = new daSQL();

        public void CargaCombosChecked(string nCombo, CheckedComboBoxEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue)
        {
            combo.Text = "";
            string procedure = "usp_ConsultasVarias_FacturasProveedor";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            try
            {
                switch (nCombo)
                {
                    case "TipoDocumento":
                        dictionary.Add("opcion", 1);
                        sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                    case "EstadoRegistro":
                        dictionary.Add("opcion", 5);
                        sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                    case "EstadoPago":
                        dictionary.Add("opcion", 6);
                        sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                }
            }
            catch (Exception generatedExceptionName)
            {
                throw;
            }
        }

        public void CargaCombosLookUp(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", 
                                    bool valorDefecto = false, string cod_proveedor = "", string cod_usuario = "", string cod_tipo_transaccion = "", 
                                    string cod_empresa = "", string cod_cliente = "")
        {
            combo.Text = "";
            string procedure = "usp_ConsultasVarias_FacturasProveedor";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "ModalidadPago":
                        dictionary.Add("opcion", 2);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoObligacion":
                        dictionary.Add("opcion", 3);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "EstadoRegistro":
                        dictionary.Add("opcion", 5);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "EstadoPago":
                        dictionary.Add("opcion", 6);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoFecha":
                        dictionary.Add("opcion", 7);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "EstadoDocumento":
                        dictionary.Add("opcion", 8);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "EmpresaProveedor":
                        procedure = "usp_Consulta_ListarFacturasProveedor";
                        dictionary.Add("opcion", 8);
                        dictionary.Add("cod_proveedor", cod_proveedor);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "EmpresasUsuarios":
                        procedure = "usp_Consulta_ListarProveedores";
                        dictionary.Add("opcion", 11);
                        dictionary.Add("cod_usuario", cod_usuario);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Pagar_A":
                        procedure = "usp_ConsultasVarias_FacturasProveedor";
                        dictionary.Add("opcion", 11);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoTransaccion":
                        dictionary.Add("opcion", 20);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "ConceptoDetraccion":
                        dictionary.Add("opcion", 21);
                        dictionary.Add("cod_tipo_transaccion", cod_tipo_transaccion);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "DistribucionCECO":
                        dictionary.Add("opcion", 32);
                        dictionary.Add("cod_empresa", cod_empresa);
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

        public DataTable ObtenerListadoGridLookup(string nCombo, string cod_condicion = "")
        {
            string procedure = "usp_ConsultasVarias_FacturasProveedor";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "TipoComprobante":
                        dictionary.Add("opcion", 1);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                }
                return tabla;
            }
            catch (Exception ex)
            {
                return new DataTable();
                throw;
            }
        }

        public T GetPrepago<T>(string codprepago) where T : class, new()
        {
            T myList = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("aIdPrepago", codprepago);

            myList = sql.ConsultarEntidad<T>("usp_Buscar_PrePagos_Por_Numero_New", dictionary);
            return myList;
        }

        public T InsertarFacturaProveedor<T>(eFacturaProveedor eFact) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "tipo_documento", eFact.tipo_documento },
                { "serie_documento", eFact.serie_documento },
                { "numero_documento", eFact.numero_documento },
                { "dsc_glosa", eFact.dsc_glosa },
                { "cod_moneda", eFact.cod_moneda },
                { "flg_igv", eFact.flg_igv },
                { "porc_igv", eFact.porc_igv },
                { "fch_tipocambio", eFact.fch_tipocambio },
                { "imp_tipocambio", eFact.imp_tipocambio },
                { "cod_modalidad_pago", eFact.cod_modalidad_pago },
                { "dsc_ruc", eFact.dsc_ruc },
                { "cod_proveedor", eFact.cod_proveedor },
                { "cod_tipo_servicio", eFact.cod_tipo_servicio },
                { "cod_empresa", eFact.cod_empresa },
                { "cod_centroresp", eFact.cod_centroresp },
                { "fch_registro", eFact.fch_registro },
                { "fch_documento", eFact.fch_documento },
                { "fch_vencimiento", eFact.fch_vencimiento },
                { "fch_pago_programado", eFact.fch_pago_programado },
                //{ "fch_pago_ejecutado", eFact.fch_pago_ejecutado },
                { "imp_descuento", eFact.imp_descuento },
                { "imp_retencion", eFact.imp_retencion },
                { "imp_percepcion", eFact.imp_percepcion },
                { "imp_subtotal", eFact.imp_subtotal },
                { "imp_igv", eFact.imp_igv },
                { "imp_otros_cargos", eFact.imp_otros_cargos },
                { "imp_total", eFact.imp_total },
                { "imp_saldo", eFact.imp_saldo },
                { "dsc_observacion", eFact.dsc_observacion },
                { "cod_estado_documento", eFact.cod_estado_documento },
                { "cod_estado_registro", eFact.cod_estado_registro },
                { "cod_estado_pago", eFact.cod_estado_pago },
                { "flg_detraccion", eFact.flg_detraccion },
                { "cod_concepto_detraccion", eFact.cod_concepto_detraccion },
                { "imp_detraccion", eFact.imp_detraccion },
                { "num_constancia_detraccion", eFact.num_constancia_detraccion },
                //{ "fch_constancia_detraccion", eFact.fch_constancia_detraccion },
                { "flg_detraccion_aplicada", eFact.flg_detraccion_aplicada },
                //{ "fch_anulacion", eFact.fch_anulacion },
                { "cod_usuario_anulacion", eFact.cod_usuario_anulacion },
                { "cod_usuario_registro", eFact.cod_usuario_registro },
                { "flg_inventario", eFact.flg_inventario },
                { "flg_activo_fijo", eFact.flg_activo_fijo },
                { "num_OrdenCompraServ", eFact.num_OrdenCompraServ },
                { "prc_tasa_detraccion", eFact.prc_tasa_detraccion },
                { "imp_detraccion_pagada", eFact.imp_detraccion_pagada },
                { "periodo_tributario", eFact.periodo_tributario },
                { "cod_tipo_transaccion", eFact.cod_tipo_transaccion },
                { "flg_retencion", eFact.flg_retencion },
                { "prc_tasa_retencion", eFact.prc_tasa_retencion },
                { "num_constancia_retencion", eFact.num_constancia_retencion },
                { "cod_origen_documento", eFact.cod_origen_documento },
                { "flg_CajaChica", eFact.flg_CajaChica },
                { "flg_EntregasRendir", eFact.flg_EntregasRendir }
            };
            if (eFact.fch_pago_ejecutado.ToString().Contains("1/01/0001")) { dictionary.Add("fch_pago_ejecutado", DBNull.Value); } else { dictionary.Add("fch_pago_ejecutado", eFact.fch_pago_ejecutado); }
            if (eFact.fch_constancia_detraccion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_constancia_detraccion", DBNull.Value); } else { dictionary.Add("fch_constancia_detraccion", eFact.fch_constancia_detraccion); }
            if (eFact.fch_constancia_retencion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_constancia_retencion", DBNull.Value); } else { dictionary.Add("fch_constancia_retencion", eFact.fch_constancia_retencion); }
            if (eFact.fch_anulacion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_anulacion", DBNull.Value); } else { dictionary.Add("fch_anulacion", eFact.fch_anulacion); }
            if (eFact.fch_pago_ejecutado_detraccion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_pago_ejecutado_detraccion", DBNull.Value); } else { dictionary.Add("fch_pago_ejecutado_detraccion", eFact.fch_pago_ejecutado_detraccion); }
            //if (eFact.periodo_tributario.ToString().Contains("1/01/0001")) { dictionary.Add("periodo_tributario", DBNull.Value); } else { dictionary.Add("periodo_tributario", eFact.periodo_tributario); }

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_CabeceraObligacion", dictionary);
            return obj;
        }

        public T InsertarNotaCreditoVinculada<T>(eFacturaProveedor.eFacturaProveedor_NotaCredito eFact) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "tipo_documento", eFact.tipo_documento },
                { "serie_documento", eFact.serie_documento },
                { "numero_documento", eFact.numero_documento },
                { "cod_proveedor", eFact.cod_proveedor },
                { "tipo_documento_NC", eFact.tipo_documento_NC },
                { "serie_documento_NC", eFact.serie_documento_NC },
                { "numero_documento_NC", eFact.numero_documento_NC },
                { "cod_proveedor_NC", eFact.cod_proveedor_NC },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_NotasCreditoObligacion", dictionary);
            return obj;
        }

        public string Actualiar_EstadoRegistroFactura(eFacturaProveedor eFact)
        {
            string result;
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "tipo_documento", eFact.tipo_documento },
                { "serie_documento", eFact.serie_documento },
                { "numero_documento", eFact.numero_documento },
                { "cod_proveedor", eFact.cod_proveedor },
                { "cod_estado_registro", eFact.cod_estado_registro },
                { "cod_usuario_registro", eFact.cod_usuario_registro },
                { "periodo_tributario", eFact.periodo_tributario },
                { "cod_usuario_aprobado_reg", eFact.cod_usuario_aprobado_reg },
                { "cod_usuario_contabilizado", eFact.cod_usuario_contabilizado }
            };
            //if (eFact.periodo_tributario.ToString().Contains("1/01/0001")) { dictionary.Add("periodo_tributario", DBNull.Value); } else { dictionary.Add("periodo_tributario", eFact.periodo_tributario); }

            result = sql.ExecuteScalarWithParams("usp_Actualizar_EstadoRegistroObligacion", dictionary);
            return result;
        }

        public T InsertarDistribucionFacturaProveedor<T>(eFacturaProveedor.eFacturaProveedor_Distribucion eFact) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "tipo_documento", eFact.tipo_documento },
                { "serie_documento", eFact.serie_documento },
                { "numero_documento", eFact.numero_documento },
                { "cod_proveedor", eFact.cod_proveedor },
                { "cod_und_negocio", eFact.cod_und_negocio },
                { "cod_tipo_gasto", eFact.cod_tipo_gasto },
                { "cod_cliente", eFact.cod_cliente },
                { "cod_proyecto", eFact.cod_proyecto },
                { "cod_CECO", eFact.cod_CECO },
                { "porc_distribucion", eFact.porc_distribucion },
                { "cod_cta_contable", eFact.cod_cta_contable },
                { "cod_usuario_registro", eFact.cod_usuario_registro }
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_DistribucionObligacion", dictionary);
            return obj;
        }

        public T InsertarObservacionesFacturaProveedor<T>(eFacturaProveedor.eFacturaProveedor_Observaciones eFact) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "tipo_documento", eFact.tipo_documento },
                { "serie_documento", eFact.serie_documento },
                { "numero_documento", eFact.numero_documento },
                { "cod_proveedor", eFact.cod_proveedor },
                { "num_linea", eFact.num_linea },
                { "dsc_observacion", eFact.dsc_observaciones },
                { "cod_usuario_registro", eFact.cod_usuario_registro },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_ObservacionesObligacion", dictionary);
            return obj;
        }

        public T InsertarProgramacionPagosFacturaProveedor<T>(eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eFact) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "tipo_documento", eFact.tipo_documento },
                { "serie_documento", eFact.serie_documento },
                { "numero_documento", eFact.numero_documento },
                { "cod_proveedor", eFact.cod_proveedor },
                { "num_linea", eFact.num_linea },
                { "fch_pago", eFact.fch_pago },
                { "imp_pago", eFact.imp_pago },
                { "cod_pagar_a", eFact.cod_pagar_a },
                { "dsc_observacion", eFact.dsc_observacion },
                { "cod_estado", eFact.cod_estado },
                { "cod_usuario_ejecucion", eFact.cod_usuario_ejecucion },
                { "cod_usuario_registro", eFact.cod_usuario_registro },
                { "cod_tipo_prog", eFact.cod_tipo_prog },
                { "cod_formapago", eFact.cod_formapago },
                { "cod_destinatario", eFact.cod_destinatario }
            };
            if (eFact.fch_ejecucion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_ejecucion", DBNull.Value); } else { dictionary.Add("fch_ejecucion", eFact.fch_ejecucion); }

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_ProgramacionPagosObligacion", dictionary);
            return obj;
        }
        public T ActualizarEjecutarPago<T>(eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eFact) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "tipo_documento", eFact.tipo_documento },
                { "serie_documento", eFact.serie_documento },
                { "numero_documento", eFact.numero_documento },
                { "cod_proveedor", eFact.cod_proveedor },
                { "num_linea", eFact.num_linea },
                { "cod_estado", eFact.cod_estado },
                { "cod_usuario_ejecucion", eFact.cod_usuario_ejecucion },
                { "cod_usuario_registro", eFact.cod_usuario_registro },
            };
            if (eFact.fch_ejecucion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_ejecucion", DBNull.Value); } else { dictionary.Add("fch_ejecucion", eFact.fch_ejecucion); }

            obj = sql.ConsultarEntidad<T>("usp_Actualizar_EjecutarPago", dictionary);
            return obj;
        }
        public string EliminarDatosFactura(int opcion, string tipo_documento, string serie_documento, decimal numero_documento, string cod_proveedor, string cod_und_negocio = "", string cod_tipo_gasto = "", string cod_cliente = "", int num_linea = 0)
        {
            string result;
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "tipo_documento", tipo_documento },
                { "serie_documento", serie_documento },
                { "numero_documento", numero_documento },
                { "cod_proveedor", cod_proveedor },
                { "cod_und_negocio", cod_und_negocio },
                { "cod_tipo_gasto", cod_tipo_gasto },
                { "cod_cliente", cod_cliente },
                { "num_linea", num_linea }
            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_DatosObligacion", dictionary);
            return result;
        }
        public string EliminarMaestrosGenerales(int opcion, string cod_tipo_gasto = "", string cod_empresa = "", string cod_und_negocio = "")
        {
            string result;
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_tipo_gasto", cod_tipo_gasto },
                { "cod_empresa", cod_empresa },
                { "cod_und_negocio", cod_und_negocio }
            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_MaestrosGenerales", dictionary);
            return result;
        }
        public T InsertarUnidadNegocio<T>(eUnidadNegocio eObj) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eObj.cod_empresa },
                { "cod_und_negocio", eObj.cod_und_negocio },
                { "dsc_und_negocio", eObj.dsc_und_negocio },
                { "dsc_pref_ceco", eObj.dsc_pref_ceco },
                { "flg_activo", eObj.flg_activo },
                { "flg_defecto", eObj.flg_defecto },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_UnidadNegocio", dictionary);
            return obj;
        }

        public T InsertarTipoGastoCosto<T>(eTipoGastoCosto eObj) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_tipo_gasto", eObj.cod_tipo_gasto },
                { "dsc_tipo_gasto", eObj.dsc_tipo_gasto },
                { "dsc_pref_ceco", eObj.dsc_pref_ceco },
                { "dsc_ceco_ALTERNATVO", eObj.dsc_ceco_ALTERNATVO },
                { "cod_empresa", eObj.cod_empresa }
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_TipoGastoCosto", dictionary);
            return obj;
        }

        public T BuscarTipoComprobante<T>(int opcion, string cod_tipo_comprobante) where T : class, new()
        {
            T myList = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("cod_tipo_comprobante", cod_tipo_comprobante);

            myList = sql.ConsultarEntidad<T>("usp_ConsultasVarias_FacturasProveedor", dictionary);
            return myList;
        }

        public T BuscarModalidadPago<T>(int opcion, string cod_modalidad_pago, string num_documento = "") where T : class, new()
        {
            T myList = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("cod_modalidad_pago", cod_modalidad_pago);
            dictionary.Add("num_documento", num_documento);

            myList = sql.ConsultarEntidad<T>("usp_ConsultasVarias_FacturasProveedor", dictionary);
            return myList;
        }

        public T BuscarTipoCambio<T>(int opcion, DateTime fch_cambio) where T : class, new()
        {
            T myList = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("fch_cambio", fch_cambio);

            myList = sql.ConsultarEntidad<T>("usp_ConsultasVarias_FacturasProveedor", dictionary);
            return myList;
        }
        public T InsertarTipoCambio<T>(eTipoCambio eObj) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "fch_cambio", eObj.fch_cambio },
                { "imp_cambio_compra", eObj.imp_cambio_compra },
                { "imp_cambio_venta", eObj.imp_cambio_venta },
                { "cod_moneda", eObj.cod_moneda },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_TipoCambio", dictionary);
            return obj;
        }

        public T Obtener_TasaDetraccion<T>(int opcion, string cod_tipo_transaccion, string cod_concepto_detraccion) where T : class, new()
        {
            T myList = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("cod_tipo_transaccion", cod_tipo_transaccion);
            dictionary.Add("cod_concepto_detraccion", cod_concepto_detraccion);

            myList = sql.ConsultarEntidad<T>("usp_ConsultasVarias_FacturasProveedor", dictionary);
            return myList;
        }

        public List<T> FiltroFactura<T>(int opcion, string cod_empresa = "", string tipo_documento = "", string cod_estado_registro = "", string cod_estado_pago = "", string cod_tipo_fecha = "",
                                        string FechaInicio = "", string FechaFin = "", int Anho = 0, int Mes = 0, string flg_IGV = "", string cod_proveedor = "",
                                        string serie_documento = "", decimal numero_documento = 0, int SinSaldo = 0, string cod_moneda = "", int Semana = 0,
                                        string aplicar_conversion = "NO", string flg_CajaChica = "NO", string flg_EntregasRendir = "NO", string cod_proyecto = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>() {
                { "opcion", opcion}, { "cod_empresa", cod_empresa}, { "tipo_documento", tipo_documento}, { "cod_estado_registro", cod_estado_registro},
                { "cod_estado_pago", cod_estado_pago}, { "cod_tipo_fecha", cod_tipo_fecha}, { "FechaInicio", FechaInicio}, { "FechaFin", FechaFin},
                { "Anho", Anho}, { "Mes", Mes}, { "flg_IGV", flg_IGV }, { "cod_proveedor", cod_proveedor }, { "serie_documento", serie_documento },
                { "numero_documento", numero_documento }, { "SinSaldo", SinSaldo }, { "cod_moneda", cod_moneda }, { "Semana", Semana },
                { "aplicar_conversion", aplicar_conversion }, { "flg_CajaChica", flg_CajaChica }, { "flg_EntregasRendir", flg_EntregasRendir },
                { "cod_proyecto", cod_proyecto }
            };
            myList = sql.ListaconSP<T>("usp_Consulta_ListarFacturasProveedor", oDictionary);
            return myList;
        }

        public List<T> Obtener_ResumenProveedorMeses<T>(int Anho, int TipoMoneda, string cod_empresa) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>() {
                { "Anho", Anho}, { "TipoMoneda", TipoMoneda}, { "cod_empresa", cod_empresa }
            };
            myList = sql.ListaconSP<T>("usp_Reporte_ResumenProveedoreMeses", oDictionary);
            return myList;
        }

        public T ValidarFacturaProveedor<T>(int opcion, string tipo_documento, string serie_documento, decimal numero_documento, string cod_proveedor, string cod_tipo_prog = "") where T : class, new()
        {
            T myList = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("tipo_documento", tipo_documento);
            dictionary.Add("serie_documento", serie_documento);
            dictionary.Add("numero_documento", numero_documento);
            dictionary.Add("cod_proveedor", cod_proveedor);
            dictionary.Add("cod_tipo_prog", cod_tipo_prog);

            myList = sql.ConsultarEntidad<T>("usp_Consulta_ListarFacturasProveedor", dictionary);
            return myList;
        }

        public T ObtenerFacturaProveedor<T>(int opcion, string tipo_documento, string serie_documento, decimal numero_documento, string cod_proveedor) where T : class, new()
        {
            T myList = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("tipo_documento", tipo_documento);
            dictionary.Add("serie_documento", serie_documento);
            dictionary.Add("numero_documento", numero_documento);
            dictionary.Add("cod_proveedor", cod_proveedor);

            myList = sql.ConsultarEntidad<T>("usp_Consulta_ListarFacturasProveedor", dictionary);
            return myList;
        }

        public T ObtenerDatosEmpresa<T>(int opcion, string cod_empresa, string dsc_ruc = "", string cod_tipo_gasto = "", string cod_und_negocio = "") where T : class, new()
        {
            T myList = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("cod_empresa", cod_empresa);
            dictionary.Add("dsc_ruc", dsc_ruc);
            dictionary.Add("cod_tipo_gasto", cod_tipo_gasto);
            dictionary.Add("cod_und_negocio", cod_und_negocio);

            myList = sql.ConsultarEntidad<T>("usp_ConsultasVarias_FacturasProveedor", dictionary);
            return myList;
        }

        public T ObtenerDatosOneDrive<T>(int opcion, string cod_empresa, int Anho = 0, int Mes = 0, string dsc_Carpeta = "") where T : class, new()
        {
            T myList = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("Anho", Anho);
            dictionary.Add("Mes", Mes);
            dictionary.Add("cod_empresa", cod_empresa);
            dictionary.Add("dsc_Carpeta", dsc_Carpeta);

            myList = sql.ConsultarEntidad<T>("usp_ConsultasVarias_FacturasProveedor", dictionary);
            return myList;
        }

        public List<T> Obtener_LineasDetalleFactura<T>(int opcion, string tipo_documento, string serie_documento, decimal numero_documento, string cod_proveedor) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("tipo_documento", tipo_documento);
            dictionary.Add("serie_documento", serie_documento);
            dictionary.Add("numero_documento", numero_documento);
            dictionary.Add("cod_proveedor", cod_proveedor);

            myList = sql.ListaconSP<T>("usp_Consulta_ListarFacturasProveedor", dictionary);
            return myList;
        }

        public List<T> Obtener_MaestrosGenerales<T>(int opcion, string cod_empresa, string cod_proveedor = "", string cod_und_negocio = "", string cod_cliente = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("cod_empresa", cod_empresa);
            dictionary.Add("cod_proveedor", cod_proveedor);
            dictionary.Add("cod_und_negocio", cod_und_negocio);
            dictionary.Add("cod_cliente", cod_cliente);

            myList = sql.ListaconSP<T>("usp_Consulta_ListarFacturasProveedor", dictionary);
            return myList;
        }

        public List<T> GetLineasNotaCredito<T>(int idFactura) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("IdFactura", idFactura);

            myList = sql.ListaconSP<T>("usp_LineasFacturas_NC", dictionary);
            return myList;
        }
        public List<T> CombosEnGridControl<T>(string nCombo, string dato = "", string cod_proveedor = "", string cod_empresa = "", string tipo_documento = "", string documento = "", string cod_und_negocio = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            string procedure = "usp_Consulta_ListarFacturasProveedor";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            //dictionary.Add("dato", dato);

            switch (nCombo)
            {
                case "TipoServicio":
                    procedure = "usp_Consulta_ListarProveedores";
                    dictionary.Add("opcion", 9);
                    dictionary.Add("cod_proveedor", cod_proveedor);
                    break;
                case "UnidadNegocio":
                    dictionary.Add("opcion", 9);
                    dictionary.Add("cod_empresa", cod_empresa);
                    break;
                case "TipoGastoCosto":
                    dictionary.Add("opcion", 10);
                    dictionary.Add("cod_empresa", cod_empresa);
                    break;
                case "ClienteEmpresa":
                    dictionary.Add("opcion", 11);
                    dictionary.Add("cod_empresa", cod_empresa);
                    dictionary.Add("cod_und_negocio", cod_und_negocio);
                    break;
                case "EstadoProgramacion":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 10);
                    break;
                case "Pagar_A":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 11);
                    break;
                case "TipoDocumento":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 1);
                    break;
                case "Documento":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 15);
                    dictionary.Add("tipo_documento", tipo_documento);
                    break;
                case "DatosDocumento":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 16);
                    dictionary.Add("documento", documento);
                    break;
                case "Meses":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 19);
                    break;
                case "TipoGasto":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 23);
                    break;
                case "FlagDefecto":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 24);
                    break;
                case "FormaPago":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 31);
                    break;
            }

            myList = sql.ListaconSP<T>(procedure, dictionary);
            return myList;
        }
        public string ActualizarEstadoFactura(int IdFactura, string Estado, int user, int opcion)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("IdFactura", IdFactura);
            dictionary.Add("EstadoFactura", Estado);
            dictionary.Add("User", user);
            dictionary.Add("Opcion", opcion);

            string result;
            result = sql.ExecuteScalarWithParams("usp_Actualizar_EstadoFactura", dictionary);

            return result;
        }
        public string AplicarDetraccionMasiva(eFacturaProveedor eFact, string cod_tipo_prog)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("tipo_documento", eFact.tipo_documento);
            dictionary.Add("serie_documento", eFact.serie_documento);
            dictionary.Add("numero_documento", eFact.numero_documento);
            dictionary.Add("cod_proveedor", eFact.cod_proveedor);
            dictionary.Add("flg_detraccion", eFact.flg_detraccion);
            dictionary.Add("imp_detraccion", eFact.imp_detraccion);
            dictionary.Add("num_constancia_detraccion", eFact.num_constancia_detraccion);
            dictionary.Add("cod_usuario_registro", eFact.cod_usuario_registro);

            dictionary.Add("cod_tipo_prog", cod_tipo_prog);
            //dictionary.Add("imp_retencion", eFact.imp_retencion);
            dictionary.Add("num_constancia_retencion", eFact.num_constancia_retencion);

            if (eFact.fch_constancia_detraccion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_constancia_detraccion", DBNull.Value); } else { dictionary.Add("fch_constancia_detraccion", eFact.fch_constancia_detraccion); }
            if (eFact.fch_pago_ejecutado_detraccion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_pago_ejecutado_detraccion", DBNull.Value); } else { dictionary.Add("fch_pago_ejecutado_detraccion", eFact.fch_pago_ejecutado_detraccion); }
            //if (eFact.fch_constancia_retencion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_constancia_retencion", DBNull.Value); } else { dictionary.Add("fch_constancia_retencion", eFact.fch_constancia_retencion); }

            string result;
            result = sql.ExecuteScalarWithParams("usp_Actualizar_AplicarDetraccionObligacion", dictionary);

            return result;
        }
        public string ActualizarInformacionDocumentos(int opcion, eFacturaProveedor objFact, string idCarpeta = "", string Anho = "", string Mes = "", 
                                                string dsc_Carpeta = "", string cod_guiaremision = "", string cod_almacen = "", string cod_sede_empresa = "")
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("tipo_documento", objFact.tipo_documento);
            dictionary.Add("serie_documento", objFact.serie_documento);
            dictionary.Add("numero_documento", objFact.numero_documento);
            dictionary.Add("cod_proveedor", objFact.cod_proveedor);
            dictionary.Add("idPDF", objFact.idPDF);
            dictionary.Add("idXML", objFact.idXML);
            dictionary.Add("NombreArchivo", objFact.NombreArchivo);
            dictionary.Add("cod_guiaremision", cod_guiaremision);
            dictionary.Add("cod_almacen", cod_almacen);
            dictionary.Add("cod_sede_empresa", cod_sede_empresa);

            dictionary.Add("cod_empresa", objFact.cod_empresa);
            dictionary.Add("idCarpeta", idCarpeta);
            dictionary.Add("idCarpetaAnho", objFact.idCarpetaAnho);
            dictionary.Add("idCarpetaMes", objFact.idCarpetaMes);
            dictionary.Add("Anho", Anho);
            dictionary.Add("Mes", Mes);
            dictionary.Add("dsc_Carpeta", dsc_Carpeta);

            string result;
            result = sql.ExecuteScalarWithParams("usp_Actualizar_DatosOnedriveObligacion", dictionary);

            return result;
        }
        public string AnularFacturaProveedor(eFacturaProveedor objFact)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("tipo_documento", objFact.tipo_documento);
            dictionary.Add("serie_documento", objFact.serie_documento);
            dictionary.Add("numero_documento", objFact.numero_documento);
            dictionary.Add("cod_proveedor", objFact.cod_proveedor);
            dictionary.Add("cod_usuario_anulacion", objFact.cod_usuario_anulacion);

            string result;
            result = sql.ExecuteScalarWithParams("usp_AnularObligacion", dictionary);

            return result;
        }

        public T Obtener_PeriodoTributario<T>(int opcion, string periodo_tributario, string cod_empresa) where T : class, new()
        {
            T myList = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("cod_empresa", cod_empresa);
            dictionary.Add("periodo_tributario", periodo_tributario);

            myList = sql.ConsultarEntidad<T>("usp_Consulta_ListarFacturasProveedor", dictionary);
            return myList;
        }

        public T Insertar_Actualizar_CerrarPeriodoTributario<T>(eFacturaProveedor eFact) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "periodo_tributario", eFact.periodo_tributario },
                { "cod_empresa", eFact.cod_empresa },
                { "flg_cerrado", eFact.flg_cerrado },
                { "cod_usuario_registro", eFact.cod_usuario_registro },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_PeriodoTributario", dictionary);
            return obj;
        }

        public T InsertarDocumentoInterno<T>(eDocumentoInterno eFact) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "tipo_documento", eFact.tipo_documento },
                { "serie_documento", eFact.serie_documento },
                { "numero_documento", eFact.numero_documento },
                { "cod_proveedor", eFact.cod_proveedor },
                { "dsc_ruc", eFact.dsc_ruc },
                { "dsc_glosa", eFact.dsc_glosa },
                { "dsc_pref_ceco", eFact.dsc_pref_ceco },
                { "cod_moneda", eFact.cod_moneda },
                { "imp_total", eFact.imp_total },
                { "fch_documento", eFact.fch_documento },
                { "dsc_referencia", eFact.dsc_referencia },
                { "dsc_observacion", eFact.dsc_observacion },
                { "cod_usuario_registro", eFact.cod_usuario_registro },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_ObligacionInterna", dictionary);
            return obj;
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

        public List<T> ObtenerDatosProyecto<T>(int opcion, string cod_empresa = "", string cod_proyecto = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>() {
                { "opcion", opcion}, { "cod_empresa", cod_empresa}, { "cod_proyecto", cod_proyecto},
            };
            myList = sql.ListaconSP<T>("usp_Consulta_ListarFacturasProveedor", oDictionary);
            return myList;
        }

        public T Insertar_Actualizar_Proyecto<T>(eProyecto eProy) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eProy.cod_empresa },
                { "cod_proyecto", eProy.cod_proyecto },
                { "dsc_proyecto", eProy.dsc_proyecto },
                { "flg_activo", eProy.flg_activo },
                { "cod_usuario_registro", eProy.cod_usuario_registro },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_Proyecto", dictionary);
            return obj;
        }

        public T Insertar_Actualizar_ProyectoProductos<T>(eProyecto.eProyecto_Producto eProd) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eProd.cod_empresa },
                { "cod_proyecto", eProd.cod_proyecto },
                { "cod_tipo_servicio", eProd.cod_tipo_servicio },
                { "cod_subtipo_servicio", eProd.cod_subtipo_servicio },
                { "cod_producto", eProd.cod_producto },
                { "ctd_requerida", eProd.ctd_requerida },
                { "flg_activo", eProd.flg_activo },
                { "cod_usuario_registro", eProd.cod_usuario_registro },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_ProyectoProductos", dictionary);
            return obj;
        }
        public string Reemplazar_CabeceraFactura(eFacturaProveedor obj, eFacturaProveedor obj2)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "tipo_documento", obj.tipo_documento },
                { "serie_documento", obj.serie_documento },
                { "numero_documento", obj.numero_documento },
                { "cod_proveedor", obj.cod_proveedor },
                { "dsc_ruc", obj.dsc_ruc },
                { "tipo_documento_NEW", obj2.tipo_documento },
                { "serie_documento_NEW", obj2.serie_documento },
                { "numero_documento_NEW", obj2.numero_documento },
                { "cod_proveedor_NEW", obj2.cod_proveedor },
                { "dsc_ruc_NEW", obj2.dsc_ruc },
            };

            string result;
            result = sql.ExecuteScalarWithParams("usp_Reemplazar_CabeceraObligacion", dictionary);

            return result;
        }

        public List<T> ListarOrdenes<T>(int opcion, string cod_empresa, string cod_proveedor, string cod_orden_compra) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_empresa", cod_empresa},
                { "cod_proveedor", cod_proveedor},
                { "cod_orden_compra_servicio", cod_orden_compra},
              

            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarProveedorDetalle", dictionary);
            return myList;
        }

        public T Obt_Prec_Producto<T>(eProductos epro) where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_proveedor", epro.cod_proveedor},
                { "cod_producto", epro.cod_producto}
            };

            obj = sql.ConsultarEntidad<T>("usp_Obtener_Precio_Producto", oDictionary);
            return obj;
        }

        public List<T> ListarSedesProveedor<T>(int opcion, string cod_empresa = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                { "cod_empresa", cod_empresa},

            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarProveedores", oDictionary);
            return myList;
        }

        public List<T> ListarSedesFactura<T>(int opcion, string cod_empresa = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                { "cod_empresa", cod_empresa},

            };

            myList = sql.ListaconSP<T>("usp_ConsultasSedesFactura", oDictionary);
            return myList;
        }

        public void CargaSedes(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", bool valorDefecto = false, string cod_empresa = "")
        {
            combo.Text = "";
            string procedure = "usp_ConsultasSedesFactura";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "sedeempresa":
                        dictionary.Add("opcion", 1);
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

        public T Ins_Act_Facdetalle<T>(eFacturaProveedor.eFacturaProvedor_Detalle eprod, string cod_usuario = "") where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "tipo_documento", eprod.tipo_documento },
                { "serie_documento", eprod.serie_documento},
                { "numero_documento", eprod.numero_documento},
                { "cod_empresa", eprod.cod_empresa}
                ,{"cod_sede_empresa" , eprod.cod_sede_empresa}
                ,{ "cod_orden", eprod.cod_orden_compra_servicio}
                ,{"cod_proveedor",eprod.cod_proveedor}
                ,{"dsc_ruc",eprod.dsc_ruc}
                ,{"cod_producto",eprod.cod_producto}
                ,{"cod_unidad_medida",eprod.cod_unidad_medida}
                ,{"num_cantidad",eprod.num_cantidad}
                ,{"imp_unitario", eprod.imp_unitario}
                ,{"imp_total", eprod.imp_total_det}
               ,{"cod_usuario_registro",eprod.cod_usuario_registro}
                ,{"cod_usuario_cambio",eprod.cod_usuario_cambio}
                ,{"fch_registro",eprod.fch_registro}
                ,{"fch_cambio",eprod.fch_cambio}

                };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_Obligacion_detalle", oDictionary);
            return obj;
        }

        public T Obtener_DatosProducto<T>(string cod_sin_orden = "", string cod_empresa = ""
                                       ) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_sin_orden", cod_sin_orden}, { "cod_empresa", cod_empresa}
            };

            objj = sql.ConsultarEntidad<T>("usp_Consulta_ListarLogistica", dictionary);
            return objj;
        }

        public List<T> ListarDetalleProveedor<T>(string tipo_documento, string serie_documento, decimal numero_documento) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "tipo_documento", tipo_documento },
                { "serie_documento", serie_documento},
                { "numero_documento", numero_documento}           

            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarProveedorDetalle", dictionary);
            return myList;
        }

        public string EliminarDatosDetalle(string tipo_documento,string cod_empresa, string cod_orden_compra)
        {
            string result;
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "tipo_documento", tipo_documento },
                { "cod_empresa", cod_empresa },
                { "cod_orden_compra", cod_orden_compra }

            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_Detalle_factura", dictionary);
            return result;
        }

        public List<T> ListarDetalleFactura<T>(int opcion, string cod_orden_compra) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_orden_compra", cod_orden_compra},


            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarProveedorDetalle", dictionary);
            return myList;
        }

        public List<T> ObtenerListadoCECOS<T>(int opcion, string cod_empresa = "", string cod_cliente = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>() 
            {
                { "opcion", opcion}, { "cod_empresa", cod_empresa}/*, { "cod_cliente", cod_cliente}*/
            };
            myList = sql.ListaconSP<T>("usp_ConsultasVarias_FacturasProveedor", oDictionary);
            return myList;
        }

        public List<T> ObtenerListado_CECO<T>(int opcion, string cod_empresa = "", string cod_cliente = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion}, { "cod_empresa", cod_empresa}, { "cod_cliente", cod_cliente}
            };
            myList = sql.ListaconSP<T>("usp_Consulta_ListarFactura_CECO", oDictionary);
            return myList;
        }
        public List<T> Obtener_LineasCECO<T>(int opcion, string tipo_documento, string serie_documento, decimal numero_documento, string cod_proveedor, string cod_empresa) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("tipo_documento", tipo_documento);
            dictionary.Add("serie_documento", serie_documento);
            dictionary.Add("numero_documento", numero_documento);
            dictionary.Add("cod_proveedor", cod_proveedor);
            dictionary.Add("cod_empresa", cod_empresa);

            myList = sql.ListaconSP<T>("usp_Consulta_ListarFacturasProveedor", dictionary);
            return myList;
        }

    }
}
    