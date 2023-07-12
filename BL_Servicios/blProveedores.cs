using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA_Servicios;
using System.Data;
using DevExpress.XtraEditors;
using BE_Servicios;

namespace BL_Servicios
{
    public class blProveedores
    {
        daSQL sql = new daSQL();

        public List<T> ListarProveedores<T>(int opcion, string cod_tipo_documento, string cod_tipo_proveedor, string cod_modalidad_pago, string cod_empresa = "", string cod_tipo_servicio = "", string cod_usuario = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_tipo_documento", cod_tipo_documento },
                {"cod_tipo_proveedor", cod_tipo_proveedor },
                {"cod_modalidad_pago", cod_modalidad_pago },
                {"cod_empresa", cod_empresa },
                {"cod_tipo_servicio", cod_tipo_servicio },
                {"cod_usuario", cod_usuario },
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarProveedores", oDictionary);
            return myList;
        }

        public List<T> ListarOpcionesMenu<T>(int opcion) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion }
            };

            myList = sql.ListaconSP<T>("usp_ConsultasVarias_Proveedor", oDictionary);
            return myList;
        }

        public T Validar_NroDocumento<T>(int opcion, string num_documento, string cod_tipo_documento = "") where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion }, {"num_documento", num_documento },
                {"cod_tipo_documento", cod_tipo_documento }
            };

            obj = sql.ConsultarEntidad<T>("usp_ConsultasVarias_Proveedor", dictionary);
            return obj;
        }
        public void CargaCombosChecked(string nCombo, CheckedComboBoxEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue)
        {
            combo.Text = "";
            string procedure = "usp_ConsultasVarias_Proveedor";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            try
            {
                switch (nCombo)
                {
                    case "EmpresasContacto":
                        dictionary.Add("opcion", 18);
                        sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                }
            }
            catch (Exception generatedExceptionName)
            {
                throw;
            }
        }
        public void CargaCombosLookUp(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", string cod_cliente = "", string cod_nivel = "", int num_linea = 0, int num_nivel = 0, bool valorDefecto = false)
        {
            combo.Text = "";
            string procedure = "usp_ConsultasVarias_Proveedor";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "Moneda":
                        dictionary.Add("opcion", 7);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoCuentaBancaria":
                        dictionary.Add("opcion", 8);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoProveedor":
                        dictionary.Add("opcion", 9);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoCompra":
                        dictionary.Add("opcion", 10);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Frecuencia":
                        dictionary.Add("opcion", 13);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "FormaPago":
                        dictionary.Add("opcion", 14);
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
            string procedure = "usp_ConsultasVarias_Proveedor";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "ModalidadPago":
                        dictionary.Add("opcion", 4);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "ConvenioTributacion":
                        dictionary.Add("opcion", 5);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Banco":
                        dictionary.Add("opcion", 6);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoDistrito":
                        procedure = "usp_ConsultasVarias_Cliente";
                        dictionary.Add("opcion", 16);
                        dictionary.Add("cod_condicion", cod_condicion);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoDocumento":
                        procedure = "usp_ConsultasVarias_Cliente";
                        dictionary.Add("opcion", 17);
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

        public T ObtenerProveedor<T>(int opcion, string cod_proveedor) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "cod_proveedor", cod_proveedor}
            };

            obj = sql.ConsultarEntidad<T>("usp_Consulta_ListarProveedores", dictionary);
            return obj;
        }

        public T ObtenerCuentaBancaria<T>(int opcion, string cod_proveedor, Int16 num_linea) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "cod_proveedor", cod_proveedor},
                { "num_linea", num_linea}
            };

            obj = sql.ConsultarEntidad<T>("usp_Consulta_ListarProveedores", dictionary);
            return obj;
        }

        public T ObtenerContacto<T>(int opcion, string cod_proveedor, int cod_contacto) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "cod_proveedor", cod_proveedor},
                { "cod_contacto", cod_contacto }
            };

            obj = sql.ConsultarEntidad<T>("usp_Consulta_ListarProveedores", dictionary);
            return obj;
        }

        public T ObtenerTecnico<T>(int opcion, string cod_proveedor, int cod_tecnico) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "cod_proveedor", cod_proveedor},
                { "cod_tecnico", cod_tecnico}
            };

            obj = sql.ConsultarEntidad<T>("usp_Consulta_ListarProveedores", dictionary);
            return obj;
        }

        public List<T> ListarCuentasBancariasProveedor<T>(int opcion, string cod_proveedor) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                { "cod_proveedor", cod_proveedor}
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarProveedores", oDictionary);
            return myList;
        }

        public List<T> ListarContactosProveedor<T>(int opcion, string cod_proveedor) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                { "cod_proveedor", cod_proveedor}
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarProveedores", oDictionary);
            return myList;
        }

        public List<T> ListarEmpresasProveedor<T>(int opcion, string cod_proveedor, string cod_usuario = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                { "cod_proveedor", cod_proveedor},
                { "cod_usuario", cod_usuario}
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarProveedores", oDictionary);
            return myList;
        }

        public List<T> ListarServiciosProveedor<T>(int opcion, string cod_proveedor) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                { "cod_proveedor", cod_proveedor}
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarProveedores", oDictionary);
            return myList;
        }

        public List<T> ListarTecnicosProveedor<T>(int opcion, string cod_proveedor) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                { "cod_proveedor", cod_proveedor}
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarProveedores", oDictionary);
            return myList;
        }

        public List<T> ListarMarcasProveedor<T>(int opcion, string cod_proveedor, string cod_tipo_servicio = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion }, { "cod_proveedor", cod_proveedor}, { "cod_tipo_servicio", cod_tipo_servicio}
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarProveedores", oDictionary);
            return myList;
        }

        public List<T> ListarFacturas<T>(int opcion, string cod_proveedor) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                { "cod_proveedor", cod_proveedor}
            };

            myList = sql.ListaconSP<T>("usp_ConsultasVarias_Proveedor", oDictionary);
            return myList;
        }

        public T Guardar_Actualizar_Proveedor<T>(eProveedor eProv, string MiAccion) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = MiAccion == "Nuevo" ? "usp_Insertar_Proveedor" : "usp_Actualizar_Proveedor";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_proveedor", eProv.cod_proveedor },
                { "cod_tipo_documento", eProv.cod_tipo_documento },
                { "num_documento", eProv.num_documento },
                { "dsc_razon_social", eProv.dsc_razon_social },
                { "dsc_direccion", eProv.dsc_direccion },
                { "cod_pais", eProv.cod_pais },
                { "cod_departamento", eProv.cod_departamento },
                { "cod_provincia", eProv.cod_provincia },
                { "cod_distrito", eProv.cod_distrito },
                //{ "dsc_contacto1", eProv.dsc_contacto1 },
                //{ "dsc_contacto2", eProv.dsc_contacto2 },
                { "dsc_mail_1", eProv.dsc_mail_1 },
                { "dsc_mail_2", eProv.dsc_mail_2 },
                { "dsc_fono_1", eProv.dsc_fono_1 },
                { "dsc_fono_2", eProv.dsc_fono_2 },
                { "flg_activo", eProv.flg_activo },
                { "dsc_razon_comercial", eProv.dsc_razon_comercial },
                //{ "flg_venta_consignacion", eProv.flg_venta_consignacion },
                //{ "cod_tipo_proveedor", eProv.cod_tipo_proveedor },
                { "flg_agente_retencion", eProv.flg_agente_retencion },
                { "flg_buen_contribuyente", eProv.flg_buen_contribuyente },
                { "flg_auto_detraccion", eProv.flg_auto_detraccion },
                { "flg_codigo_autogenerado", eProv.flg_codigo_autogenerado },
                { "flg_agente_percepcion", eProv.flg_agente_percepcion },
                { "dsc_apellido_paterno", eProv.dsc_apellido_paterno },
                { "dsc_apellido_materno", eProv.dsc_apellido_materno },
                { "dsc_nombres", eProv.dsc_nombres },
                { "flg_juridico", eProv.flg_juridico },
                { "dsc_proveedor", eProv.dsc_proveedor },
                { "flg_domiciliado", eProv.flg_domiciliado },
                { "Observaciones", eProv.Observaciones },
                { "cod_modalidad_pago", eProv.cod_modalidad_pago },
                { "flg_afecto_cuarta", eProv.flg_afecto_cuarta },
                { "flg_no_habido", eProv.flg_no_habido },
                //{ "cod_convenio_trib", eProv.cod_convenio_trib },
                { "cod_usuario_registro", eProv.cod_usuario_registro },
                { "cod_frecuencia", eProv.cod_frecuencia },
                { "cod_formapago", eProv.cod_formapago },
                { "dsc_representante_legal", eProv.dsc_representante_legal },
                { "cod_proveedor_ERP", eProv.cod_proveedor_ERP },
                { "flg_transportista", eProv.flg_transportista },
                { "dsc_licenciaconducir", eProv.dsc_licenciaconducir },
                { "dsc_nroautorizMTC", eProv.dsc_nroautorizMTC },
                { "dsc_marcavehiculo", eProv.dsc_marcavehiculo },
                { "dsc_placavehiculo", eProv.dsc_placavehiculo }
            };

            if (eProv.fch_no_habido.ToString().Contains("1/01/0001")) { dictionary.Add("fch_no_habido", DBNull.Value); } else { dictionary.Add("fch_no_habido", eProv.fch_no_habido); }


            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T Guardar_Actualizar_CuentaBancariaProveedor<T>(eProveedor_CuentasBancarias eCuentaBanc, string MiAccion) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = MiAccion == "Nuevo" ? "usp_Insertar_CuentasBancariasProveedor" : "usp_Actualizar_CuentasBancariasProveedor";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_proveedor",  eCuentaBanc.cod_proveedor },
                { "num_linea",  eCuentaBanc.num_linea },
                { "cod_banco",  eCuentaBanc.cod_banco },
                { "cod_moneda",  eCuentaBanc.cod_moneda },
                { "cod_tipo_cuenta",  eCuentaBanc.cod_tipo_cuenta },
                { "dsc_cta_bancaria",  eCuentaBanc.dsc_cta_bancaria },
                { "dsc_cta_interbancaria",  eCuentaBanc.dsc_cta_interbancaria },
                { "flg_pago_transferencia",  eCuentaBanc.flg_pago_transferencia },
                { "dsc_observaciones",  eCuentaBanc.dsc_observaciones },
                { "dsc_titular_cuenta",  eCuentaBanc.dsc_titular_cuenta }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public string Eliminar_CuentaBancariaProveedor(string cod_proveedor, Int16 num_linea)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_proveedor", cod_proveedor }, { "num_linea", num_linea }
            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_CuentasBancariasProveedor", dictionary);
            return result;
        }

        public string Insertar_EmpresasContactoProveedor(string cod_proveedor, int cod_contacto, string cod_lista_empresa)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_proveedor", cod_proveedor }, { "cod_contacto", cod_contacto }, { "cod_lista_empresa", cod_lista_empresa }
            };

            result = sql.ExecuteScalarWithParams("usp_Insertar_EmpresasContactosProveedor", dictionary);
            return result;
        }

        public T Guardar_Actualizar_ContactosProveedor<T>(eProveedor_Contactos eContact, string MiAccion) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = MiAccion == "Nuevo" ? "usp_Insertar_ContactosProveedor" : "usp_Actualizar_ContactosProveedor";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_proveedor", eContact.cod_proveedor },
                { "cod_contacto", eContact.cod_contacto },
                { "dsc_nombre", eContact.dsc_nombre },
                { "dsc_apellidos", eContact.dsc_apellidos },
                //{ "fch_nacimiento", eContact.fch_nacimiento },
                { "dsc_correo", eContact.dsc_correo },
                { "dsc_telefono1", eContact.dsc_telefono1 },
                { "dsc_telefono2", eContact.dsc_telefono2 },
                { "dsc_cargo", eContact.dsc_cargo },
                { "cod_usuario_reg", eContact.cod_usuario_reg },
                { "cod_usuario_web", eContact.cod_usuario_web },
                { "cod_clave_web", eContact.cod_clave_web },
                { "dsc_observaciones", eContact.dsc_observaciones }
            };

            if (eContact.fch_nacimiento.ToString().Contains("1/01/0001")) { dictionary.Add("fch_nacimiento", DBNull.Value); } else { dictionary.Add("fch_nacimiento", eContact.fch_nacimiento); }

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public string Eliminar_ContactosProveedor(string cod_proveedor, int cod_contacto)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_proveedor", cod_proveedor }, { "cod_contacto", cod_contacto }
            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_ContactosProveedor", dictionary);
            return result;
        }

        public T Guardar_Actualizar_TecnicosProveedor<T>(eProveedor_Tecnicos eTec, string MiAccion) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = MiAccion == "Nuevo" ? "usp_Insertar_TecnicosProveedor" : "usp_Actualizar_TecnicosProveedor";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_proveedor", eTec.cod_proveedor },
                { "cod_tecnico", eTec.cod_tecnico },
                { "dsc_nombre", eTec.dsc_nombre },
                { "dsc_apellidos", eTec.dsc_apellidos },
                //{ "fch_nacimiento", eTec.fch_nacimiento },
                { "dsc_correo", eTec.dsc_correo },
                { "dsc_telefono1", eTec.dsc_telefono1 },
                { "dsc_telefono2", eTec.dsc_telefono2 },
                { "flg_supervisor", eTec.flg_supervisor },
                { "cod_usuario_reg", eTec.cod_usuario_reg },
                { "cod_usuario_web", eTec.cod_usuario_web },
                { "cod_clave_web", eTec.cod_clave_web },
                { "dsc_observaciones", eTec.dsc_observaciones }
            };

            if (eTec.fch_nacimiento.ToString().Contains("1/01/0001")) { dictionary.Add("fch_nacimiento", DBNull.Value); } else { dictionary.Add("fch_nacimiento", eTec.fch_nacimiento); }

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }
        public string Eliminar_TecnicosProveedor(string cod_proveedor, int cod_tecnico)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_proveedor", cod_proveedor }, { "cod_tecnico", cod_tecnico }
            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_TecnicosProveedor", dictionary);
            return result;
        }

        public string Activar_Inactivar_Proveedor(string cod_proveedor, string flg_activo)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_proveedor", cod_proveedor }, { "flg_activo", flg_activo }
            };

            result = sql.ExecuteScalarWithParams("usp_Activar_Inactivar_Proveedor", dictionary);
            return result;
        }

        public string Eliminar_Proveedor(string cod_proveedor)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_proveedor", cod_proveedor }
            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_Proveedor", dictionary);
            return result;
        }

        public T Guardar_Actualizar_ProveedorEmpresas<T>(eProveedor_Empresas eProv) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_proveedor", eProv.cod_proveedor },
                { "cod_empresa", eProv.cod_empresa },
                { "valorRating", eProv.valorRating },
                { "flg_activo", eProv.flg_activo },
                { "cod_usuario_registro", eProv.cod_usuario_registro }
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_ProveedorEmpresas", dictionary);
            return obj;
        }

        public T Guardar_Actualizar_ProveedorServicio<T>(eProveedor_Servicios eProv) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_proveedor", eProv.cod_proveedor },
                { "cod_tipo_servicio", eProv.cod_tipo_servicio },
                { "flg_activo", eProv.flg_activo },
                { "cod_usuario_registro", eProv.cod_usuario_registro }
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_ProveedorServicios", dictionary);
            return obj;
        }

        public T Insertar_Actualizar_ProveedorMarca<T>(eProveedor_Marca obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_proveedor", obj.cod_proveedor }, { "cod_marca", obj.cod_marca }, { "dsc_marca",obj.dsc_marca },
                { "dsc_abreviado", obj.dsc_abreviado }, { "flg_activo", obj.flg_activo }, { "cod_usuario_registro", obj.cod_usuario_registro }
            };

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_ProveedorMarca", dictionary);
            return objj;
        }

    }
}
