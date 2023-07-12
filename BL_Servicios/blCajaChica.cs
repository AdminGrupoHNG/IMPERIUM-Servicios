using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_Servicios;
using DA_Servicios;
using DevExpress.XtraEditors;

namespace BL_Servicios
{
    public class blCajaChica
    {
        daSQL sql = new daSQL();

        public void CargaCombosLookUp(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", bool valorDefecto = false, string cod_empresa = "", string cod_und_negocio = "", string cod_sede_empresa = "", string cod_responsable = "")
        {
            combo.Text = "";
            string procedure = "usp_ConsultasVarias_CajaChica";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "ModoReposicion":
                        dictionary.Add("opcion", 1);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoCaja":
                        dictionary.Add("opcion", 2);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoCajaEmpresa":
                        dictionary.Add("opcion", 3); 
                        dictionary.Add("cod_empresa", cod_empresa);
                        dictionary.Add("cod_sede_empresa", cod_sede_empresa);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Responsable":
                        dictionary.Add("opcion", 4);
                        dictionary.Add("cod_empresa", cod_empresa);
                        dictionary.Add("cod_sede_empresa", cod_sede_empresa);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoCajaResponsable":
                        dictionary.Add("opcion", 5);
                        dictionary.Add("cod_responsable", cod_responsable);
                        dictionary.Add("cod_empresa", cod_empresa);
                        dictionary.Add("cod_sede_empresa", cod_sede_empresa);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "EstadoEntregaRendir":
                        dictionary.Add("opcion", 6);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "UnidadNegocio":
                        procedure = "usp_Consulta_ListarFacturasProveedor";
                        dictionary.Add("opcion", 9);
                        dictionary.Add("cod_empresa", cod_empresa);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoGastoCosto":
                        procedure = "usp_Consulta_ListarFacturasProveedor";
                        dictionary.Add("opcion", 10);
                        dictionary.Add("cod_empresa", cod_empresa);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "ClienteEmpresa":
                        procedure = "usp_Consulta_ListarFacturasProveedor";
                        dictionary.Add("opcion", 11);
                        dictionary.Add("cod_empresa", cod_empresa);
                        dictionary.Add("cod_und_negocio", cod_und_negocio);
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

        public List<T> ListarDatos_CajaChica<T>(int opcion, string cod_caja, string cod_movimiento, string cod_empresa = "", string cod_sede_empresa = "", string cod_responsable = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_caja", cod_caja },
                { "cod_movimiento", cod_movimiento },
                { "cod_empresa", cod_empresa },
                { "cod_sede_empresa", cod_sede_empresa },
                { "cod_responsable", cod_responsable }
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarCajaChica", oDictionary);
            return myList;
        }

        public T ObtenerDatos_CajaChica<T>(int opcion, string cod_caja = "", string cod_empresa = "", string cod_sede_empresa = "", string cod_movimiento = "") where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_caja", cod_caja },
                { "cod_empresa", cod_empresa },
                { "cod_sede_empresa", cod_sede_empresa },
                { "cod_movimiento", cod_movimiento }
            };

            obj = sql.ConsultarEntidad<T>("usp_Consulta_ListarCajaChica", dictionary);
            return obj;
        }

        public T InsertarActualizar_AperturaCajaChica<T>(eCajaChica eCaja) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_caja", eCaja.cod_caja },
                { "cod_tipo_caja", eCaja.cod_tipo_caja },
                { "cod_responsable", eCaja.cod_responsable },
                { "cod_empresa", eCaja.cod_empresa },
                { "cod_sede_empresa", eCaja.cod_sede_empresa },
                { "cod_moneda", eCaja.cod_moneda },
                { "imp_monto", eCaja.imp_monto },
                { "imp_alertar", eCaja.imp_alertar },
                { "cod_modalidad", eCaja.cod_modalidad },
                { "cod_usuario_registro", eCaja.cod_usuario_registro },
            };
            if (eCaja.fch_creacion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_creacion", DBNull.Value); } else { dictionary.Add("fch_creacion", eCaja.fch_creacion); }

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_CajaChica", dictionary);
            return obj;
        }

        public T InsertarActualizar_MovimientosCajaChica<T>(eCajaChica.eMovimiento_CajaChica eMovCaja) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_caja", eMovCaja.cod_caja },
                { "cod_movimiento", eMovCaja.cod_movimiento },
                { "cod_tipo", eMovCaja.cod_tipo },
                { "cod_estado", eMovCaja.cod_estado },
                { "imp_entregado", eMovCaja.imp_entregado },
                { "cod_entregado_a", eMovCaja.cod_entregado_a },
                { "dsc_observacion", eMovCaja.dsc_observacion },
                { "cod_movimiento_vinculo", eMovCaja.cod_movimiento_vinculo },
                { "cod_usuario_registro", eMovCaja.cod_usuario_registro },
                { "flg_rendido", eMovCaja.flg_rendido },
                { "cod_movimiento_rendido", eMovCaja.cod_movimiento_rendido },
                { "dsc_referencia", eMovCaja.dsc_referencia },
                { "num_Anho", eMovCaja.num_Anho },
                { "cod_entregarendir", eMovCaja.cod_entregarendir },
            };
            if (eMovCaja.fch_creacion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_creacion", DBNull.Value); } else { dictionary.Add("fch_creacion", eMovCaja.fch_creacion); }

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_MovimientosCajaChica", dictionary);
            return obj;
        }

        public T InsertarActualizar_DetalleMovCajaChica<T>(eCajaChica.eDetalleMov_CajaChica eDetCaja) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_caja", eDetCaja.cod_caja },
                { "cod_movimiento", eDetCaja.cod_movimiento },
                { "num_linea", eDetCaja.num_linea },
                { "tipo_documento", eDetCaja.tipo_documento },
                { "serie_documento", eDetCaja.serie_documento },
                { "numero_documento", eDetCaja.numero_documento },
                { "cod_proveedor", eDetCaja.cod_proveedor },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_DetalleMovCajaChica", dictionary);
            return obj;
        }


        public List<T> ListarDatos_EntregasRendir<T>(int opcion, string cod_entregarendir, string cod_empresa = "", string cod_sede_empresa = "", string FechaInicio = "", string FechaFin = "", string flg_ConFecha = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_entregarendir", cod_entregarendir },
                { "cod_empresa", cod_empresa },
                { "cod_sede_empresa", cod_sede_empresa },
                { "FechaInicio", FechaInicio },
                { "FechaFin", FechaFin },
                { "flg_ConFecha", flg_ConFecha }
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarEntregasRendir", oDictionary);
            return myList;
        }

        public T ObtenerDatos_EntregasRendir<T>(int opcion, string cod_entregarendir = "", string cod_empresa = "", string cod_sede_empresa = "") where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_entregarendir", cod_entregarendir },
                { "cod_empresa", cod_empresa },
                { "cod_sede_empresa", cod_sede_empresa }
            };

            obj = sql.ConsultarEntidad<T>("usp_Consulta_ListarEntregasRendir", dictionary);
            return obj;
        }

        public T InsertarActualizar_EntregasRendir<T>(eEntregaRendir eEntrega) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_entregarendir", eEntrega.cod_entregarendir },
                { "cod_tipo", eEntrega.cod_tipo },
                { "cod_entregado_a", eEntrega.cod_entregado_a },
                { "cod_empresa", eEntrega.cod_empresa },
                { "cod_sede_empresa", eEntrega.cod_sede_empresa },
                { "cod_moneda", eEntrega.cod_moneda },
                { "imp_monto", eEntrega.imp_monto },
                { "cod_modalidad", eEntrega.cod_modalidad },
                { "cod_estado", eEntrega.cod_estado },
                { "dsc_observacion", eEntrega.dsc_observacion },
                { "cod_vinculo", eEntrega.cod_vinculo },
                { "cod_usuario_registro", eEntrega.cod_usuario_registro },
                { "num_Anho", eEntrega.num_Anho },
                { "cod_estado_aprobado", eEntrega.cod_estado_aprobado }
            };
            if (eEntrega.fch_creacion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_creacion", DBNull.Value); } else { dictionary.Add("fch_creacion", eEntrega.fch_creacion); }

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_EntregasRendir", dictionary);
            return obj;
        }

        public T InsertarActualizar_DetalleEntregasRendir<T>(eEntregaRendir.eDetalle_EntregaRendir eMovEntrega) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_entregarendir", eMovEntrega.cod_entregarendir },
                { "num_linea", eMovEntrega.num_linea },
                { "tipo_documento", eMovEntrega.tipo_documento },
                { "serie_documento", eMovEntrega.serie_documento },
                { "numero_documento", eMovEntrega.numero_documento },
                { "cod_proveedor", eMovEntrega.cod_proveedor },
                { "cod_empresa", eMovEntrega.cod_empresa },
                { "cod_sede_empresa", eMovEntrega.cod_sede_empresa },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_DetalleEntregasRendir", dictionary);
            return obj;
        }

        public string Eliminar_MovimientoCajaChica(int opcion, eCajaChica.eMovimiento_CajaChica eMov)
        {
            string result;
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_caja", eMov.cod_caja },
                { "cod_movimiento", eMov.cod_movimiento },
            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_MovimientoCajaChica", dictionary);
            return result;
        }

        public string Eliminar_MovimientoEntregaRendir(int opcion, eEntregaRendir eMov)
        {
            string result;
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_empresa", eMov.cod_empresa },
                { "cod_sede_empresa", eMov.cod_sede_empresa },
                { "cod_entregarendir", eMov.cod_entregarendir },
            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_MovimientoEntregaRendir", dictionary);
            return result;
        }
        public string Reemplazar_CabeceraEntregarRendir(eEntregaRendir obj)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_estado_aprobado", obj.cod_estado_aprobado },
                { "cod_empresa", obj.cod_empresa },
                { "cod_sede_empresa", obj.cod_sede_empresa },
                { "cod_entregarendir", obj.cod_entregarendir },

            };

            string result;
            result = sql.ExecuteScalarWithParams("usp_Reemplazar_CabeceraEntregaRendir", dictionary);

            return result;
        }


    }
}
