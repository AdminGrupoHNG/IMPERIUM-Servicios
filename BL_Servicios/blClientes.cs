using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_Servicios;
using DA_Servicios;
using DevExpress.XtraEditors;
using System.Data;

namespace BL_Servicios
{
    public class blClientes
    {
        daSQL sql = new daSQL();

        public List<T> ListarClientes<T>(int opcion, string cod_tipo_cliente = "", string cod_categoria = "", string cod_tipo_documento = "", string cod_calificacion = "", string cod_tipo_contacto = "", string cod_empresa = "", string cod_empresa_multiple = "", string cod_categoria_multiple = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_tipo_cliente", cod_tipo_cliente },
                {"cod_categoria", cod_categoria },
                {"cod_tipo_documento", cod_tipo_documento },
                {"cod_calificacion", cod_calificacion },
                {"cod_tipo_contacto", cod_tipo_contacto },
                {"cod_empresa", cod_empresa },
                {"cod_empresa_multiple", cod_empresa_multiple },
                {"cod_categoria_multiple", cod_categoria_multiple }
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarClientes", oDictionary);
            return myList;
        }

        public List<T> ListarOpcionesMenu<T>(int opcion) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion }
            };

            myList = sql.ListaconSP<T>("usp_ConsultasVarias_Cliente", oDictionary);
            return myList;
        }

        public T ObtenerCliente<T>(int opcion, string cod_cliente) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "cod_cliente", cod_cliente}
            };

            obj = sql.ConsultarEntidad<T>("usp_Consulta_ListarClientes", dictionary);
            return obj;
        }

        public List<T> ListarDirecciones<T>(int opcion, string cod_cliente) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_cliente", cod_cliente }
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarClientes", oDictionary);
            return myList;
        }

        public List<T> ListarContactos<T>(int opcion, string cod_cliente, int num_linea) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_cliente", cod_cliente },
                {"num_linea", num_linea }
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarClientes", oDictionary);
            return myList;
        }

        public List<T> ListarUbicaciones<T>(int opcion, string cod_cliente, int num_linea) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_cliente", cod_cliente },
                {"num_linea", num_linea }
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarClientes", oDictionary);
            return myList;
        }
        public List<T> ListarCentroResponsabilidad<T>(int opcion, string cod_cliente) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_cliente", cod_cliente }
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarClientes", oDictionary);
            return myList;
        }

        public T ObtenerDireccion<T>(int opcion, string cod_cliente, int num_linea) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "cod_cliente", cod_cliente},
                { "num_linea", num_linea}
            };

            obj = sql.ConsultarEntidad<T>("usp_Consulta_ListarClientes", dictionary);
            return obj;
        }

        public T ObtenerClienteContacto<T>(int opcion, string cod_cliente = "", int cod_contacto = 0) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "cod_cliente", cod_cliente},
                { "cod_contacto", cod_contacto}
            };

            obj = sql.ConsultarEntidad<T>("usp_Consulta_ListarClientes", dictionary);
            return obj;
        }

        public T ObtenerDireccionContacto<T>(int opcion, string cod_cliente, int num_linea, int cod_contacto) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "cod_cliente", cod_cliente},
                { "num_linea", num_linea},
                { "cod_contacto", cod_contacto}
            };

            obj = sql.ConsultarEntidad<T>("usp_Consulta_ListarClientes", dictionary);
            return obj;
        }

        public List<T> ListarOpcionesVariasCliente<T>(int opcion, string cod_cliente = "", string cod_condicion = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_cliente", cod_cliente },
                {"cod_condicion", cod_condicion }
            };

            myList = sql.ListaconSP<T>("usp_ConsultasVarias_Cliente", oDictionary);
            return myList;
        }

        public void CargaCombosLookUp(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", string cod_cliente = "", string cod_nivel = "", int num_linea = 0, int num_nivel = 0, string cod_condicion="", bool valorDefecto = false)
        {
            combo.Text = "";
            string procedure = "usp_ConsultasVarias_Cliente";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "TipoEstadoCivil":
                        dictionary.Add("opcion", 6);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoSexo":
                        dictionary.Add("opcion", 7);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoPais":
                        dictionary.Add("opcion", 13);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoDepartamento":
                        dictionary.Add("cod_condicion", cod_condicion);
                        dictionary.Add("opcion", 14);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoProvincia":
                        dictionary.Add("cod_condicion", cod_condicion);
                        dictionary.Add("opcion", 15);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoDistrito":
                        dictionary.Add("cod_condicion", cod_condicion);
                        dictionary.Add("opcion", 16);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoDocumento":
                        dictionary.Add("opcion", 17);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoCliente":
                        dictionary.Add("opcion", 18);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "NivelUbicacion":
                        dictionary.Add("opcion", 22);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "NivelSuperiorUbicacion":
                        dictionary.Add("opcion", 23);
                        dictionary.Add("cod_cliente", cod_cliente);
                        dictionary.Add("cod_nivel", cod_nivel);
                        dictionary.Add("num_linea", num_linea);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "ResponsableUbicacion":
                        dictionary.Add("opcion", 24);
                        dictionary.Add("cod_cliente", cod_cliente);
                        dictionary.Add("num_linea", num_linea);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "NivelSuperiorCentroResponsabilidad":
                        dictionary.Add("opcion", 25);
                        dictionary.Add("cod_cliente", cod_cliente);
                        dictionary.Add("num_nivel", num_nivel);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "ResponsableCentroResponsabilidad":
                        dictionary.Add("opcion", 26);
                        dictionary.Add("cod_cliente", cod_cliente);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "ResponsableTrabajador":
                        dictionary.Add("opcion", 34);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "NivelSuperiorUbicacionInterna":
                        dictionary.Add("opcion", 35);
                        dictionary.Add("cod_nivel", cod_nivel);
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
        
        public DataTable ObtenerListadoGridLookup(string nCombo)
        {
            string procedure = "usp_ConsultasVarias_Cliente";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "TipoDireccion":
                        dictionary.Add("opcion", 8);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoCalle":
                        dictionary.Add("opcion", 9);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoAvenida":
                        dictionary.Add("opcion", 10);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoUrbanizacion":
                        dictionary.Add("opcion", 11);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoEtapa":
                        dictionary.Add("opcion", 12);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoDistrito":
                        dictionary.Add("opcion", 16);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoDocumento":
                        dictionary.Add("opcion", 17);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoContacto":
                        dictionary.Add("opcion", 19);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoCalificacion":
                        dictionary.Add("opcion", 20);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoCategoria":
                        dictionary.Add("opcion", 21);
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

        public T Guardar_Actualizar_Cliente<T>(eCliente eCli, string MiAccion)  where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = MiAccion == "Nuevo" ? "usp_Insertar_Cliente" : "usp_Actualizar_Cliente";
            Dictionary <string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_cliente", eCli.cod_cliente },
                { "dsc_razon_social", eCli.dsc_razon_social },
                { "dsc_apellido_paterno", eCli.dsc_apellido_paterno },
                { "dsc_apellido_materno", eCli.dsc_apellido_materno },
                { "dsc_nombre", eCli.dsc_nombre },
                { "flg_juridico", eCli.flg_juridico },
                { "cod_tipo_documento", eCli.cod_tipo_documento },
                { "dsc_documento", eCli.dsc_documento },
                { "cod_calificacion", eCli.cod_calificacion },
                { "dsc_email", eCli.dsc_email },
                { "dsc_telefono_1", eCli.dsc_telefono_1 },
                { "dsc_telefono_2", eCli.dsc_telefono_2 },
                { "dsc_cliente", eCli.dsc_cliente },
                { "cod_tipo_contacto", eCli.cod_tipo_contacto },
                { "cod_usuario", eCli.cod_usuario },
                { "cod_sexo", eCli.cod_sexo },
                { "cod_estadocivil", eCli.cod_estadocivil },
                { "cod_categoria", eCli.cod_categoria },
                { "cod_cliente_antiguo", eCli.cod_cliente_antiguo },
                //{ "fch_fallecimiento", eCli.fch_fallecimiento },
                //{ "fch_nacimiento", eCli.fch_nacimiento },
                { "dsc_mail_trabajo", eCli.dsc_mail_trabajo },
                { "cod_tipo_cliente", eCli.cod_tipo_cliente },
                { "flg_domiciliado", eCli.flg_domiciliado },
                { "cod_vendedor", eCli.cod_vendedor },
                { "cod_modalidad_venta", eCli.cod_modalidad_venta },
                { "flg_vinculada", eCli.flg_vinculada },
                //{ "cod_tarjeta_cliente", eCli.cod_tarjeta_cliente },
                { "dsc_mail_fe", eCli.dsc_mail_fe },
                { "cod_cliente_interno", eCli.cod_cliente_interno },
                { "flg_padron_envio", eCli.flg_padron_envio },
                //{ "fch_afiliacion", eCli.fch_afiliacion },
                { "cod_empresa_interna", eCli.cod_empresa_interna },
                { "dsc_cargo", eCli.dsc_cargo },
                { "dsc_carben", eCli.dsc_carben },
                { "flg_tipo_planilla", eCli.flg_tipo_planilla },
                { "num_dias_gracia", eCli.num_dias_gracia },
                { "cod_modulo", eCli.cod_modulo },
                { "cod_modular", eCli.cod_modular },
                { "dsc_contacto", eCli.dsc_contacto },
                { "dsc_razon_comercial", eCli.dsc_razon_comercial },
                { "cod_proveedor_ERP", eCli.cod_proveedor_ERP },
                { "flg_codigo_autogenerado", eCli.flg_codigo_autogenerado },
                { "flg_prospecto", eCli.flg_prospecto },
                { "cod_asesor", DBNull.Value }
            };

            if (eCli.fch_fallecimiento.ToString().Contains("1/01/0001")) { dictionary.Add("fch_fallecimiento", DBNull.Value); } else { dictionary.Add("fch_fallecimiento", eCli.fch_fallecimiento); }
            if (eCli.fch_nacimiento.ToString().Contains("1/01/0001")) { dictionary.Add("fch_nacimiento", DBNull.Value); } else { dictionary.Add("fch_nacimiento", eCli.fch_nacimiento); }
            if (eCli.fch_afiliacion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_afiliacion", DBNull.Value); } else { dictionary.Add("fch_afiliacion", eCli.fch_afiliacion); }


            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T Guardar_Actualizar_ClienteDireccion<T>(eCliente_Direccion eDirec, string MiAccion) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = MiAccion == "Nuevo" ? "usp_Insertar_ClienteDireccion" : "usp_Actualizar_ClienteDireccion";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_cliente", eDirec.cod_cliente },
                { "num_linea", eDirec.num_linea },
                { "cod_pais", eDirec.cod_pais },
                { "cod_departamento", eDirec.cod_departamento },
                { "cod_provincia", eDirec.cod_provincia },
                { "dsc_direccion", eDirec.dsc_direccion },
                { "cod_distrito", eDirec.cod_distrito },
                { "cod_tipo_direccion", eDirec.cod_tipo_direccion },
                { "dsc_referencia", eDirec.dsc_referencia },
                { "dsc_telefono_1", eDirec.dsc_telefono_1 },
                { "dsc_telefono_2", eDirec.dsc_telefono_2 },
                { "flg_comprobante", eDirec.flg_comprobante },
                { "cod_numero", eDirec.cod_numero },
                { "cod_interior", eDirec.cod_interior },
                { "cod_manzana", eDirec.cod_manzana },
                { "cod_lote", eDirec.cod_lote },
                { "cod_sublote", eDirec.cod_sublote },
                { "dsc_urbanizacion", eDirec.dsc_urbanizacion },
                { "dsc_cadena_direccion", eDirec.dsc_cadena_direccion },
                { "flg_direccion_obra", eDirec.flg_direccion_obra },
                { "dsc_observacion", eDirec.dsc_observacion },
                { "dsc_nombre_direccion", eDirec.dsc_nombre_direccion },
                { "cod_zona", eDirec.cod_zona },
                { "flg_direccion_cobranza", eDirec.flg_direccion_cobranza },
                { "cod_calle_direccion", eDirec.cod_calle_direccion },
                { "cod_urbanizacion", eDirec.cod_urbanizacion },
                { "cod_tipo_via", eDirec.cod_tipo_via },
                { "cod_tipo_zona", eDirec.cod_tipo_zona }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public string Eliminar_ClienteDireccion(string cod_cliente, int num_linea)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_cliente", cod_cliente }, { "num_linea", num_linea }
            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_ClienteDirecciones", dictionary);
            return result;
        }

        public T Guardar_Actualizar_ClienteUbicacion<T>(eCliente_Ubicacion eUbic, string MiAccion) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = MiAccion == "Nuevo" ? "usp_Insertar_ClienteUbicacion" : "usp_Actualizar_ClienteUbicacion";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_cliente", eUbic.cod_cliente },
                { "cod_ubicacion", eUbic.cod_ubicacion },
                { "dsc_ubicacion", eUbic.dsc_ubicacion },
                { "cod_nivel", eUbic.cod_nivel },
                { "dsc_observacion", eUbic.dsc_observacion },
                { "cod_ubicacion_sup", eUbic.cod_ubicacion_sup },
                { "flg_activo", eUbic.flg_activo },
                { "cod_localidad", eUbic.cod_localidad },
                { "dsc_direccion", eUbic.dsc_direccion },
                { "cod_ubicacion_per", eUbic.cod_ubicacion_per },
                { "num_linea", eUbic.num_linea },
                { "cod_contacto", eUbic.cod_contacto }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public string Inactivar_ClienteUbicacion(string cod_cliente, string cod_ubicacion)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_cliente", cod_cliente }, { "cod_ubicacion", cod_ubicacion }
            };

            result = sql.ExecuteScalarWithParams("usp_Inactivar_ClienteUbicacion", dictionary);
            return result;
        }

        public T Guardar_Actualizar_ClienteContacto<T>(eCliente_Contactos eContact, string MiAccion) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = MiAccion == "Nuevo" ? "usp_Insertar_ClienteContacto" : "usp_Actualizar_ClienteContacto";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_cliente", eContact.cod_cliente },
                { "cod_contacto", eContact.cod_contacto },
                { "dsc_nombre", eContact.dsc_nombre },
                { "dsc_apellidos", eContact.dsc_apellidos },
                //{ "fch_nacimiento", eContact.fch_nacimiento.ToString("yyyy-MM-dd") },
                { "dsc_correo", eContact.dsc_correo },
                { "dsc_telefono1", eContact.dsc_telefono1 },
                { "dsc_telefono2", eContact.dsc_telefono2 },
                { "dsc_cargo", eContact.dsc_cargo },
                { "cod_usuario_reg", eContact.cod_usuario_reg },
                { "cod_usuario_web", eContact.cod_usuario_web },
                { "cod_clave_web", eContact.cod_clave_web },
                { "dsc_observaciones", eContact.dsc_observaciones }
            };

            if (eContact.fch_nacimiento.ToString().Contains("1/01/0001")) { dictionary.Add("fch_nacimiento", DBNull.Value); } else { dictionary.Add("fch_nacimiento", eContact.fch_nacimiento.ToString("yyyy-MM-dd")); }

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public string Eliminar_ClienteContacto(string cod_cliente, int cod_contacto)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_cliente", cod_cliente }, { "cod_contacto", cod_contacto }
            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_ClienteContacto", dictionary);
            return result;
        }

        public T Guardar_Actualizar_ClienteDireccionContacto<T>(eCliente_Contactos eContact, string MiAccion) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = MiAccion == "Nuevo" ? "usp_Insertar_ClienteDireccionContacto" : "usp_Actualizar_ClienteDireccionContacto";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_cliente", eContact.cod_cliente },
                { "num_linea", eContact.num_linea },
                { "cod_contacto", eContact.cod_contacto },
                { "dsc_nombre", eContact.dsc_nombre },
                { "dsc_apellidos", eContact.dsc_apellidos },
                //{ "fch_nacimiento", eContact.fch_nacimiento.ToString("yyyy-MM-dd") },
                { "dsc_correo", eContact.dsc_correo },
                { "dsc_telefono1", eContact.dsc_telefono1 },
                { "dsc_telefono2", eContact.dsc_telefono2 },
                { "dsc_cargo", eContact.dsc_cargo },
                { "cod_usuario_reg", eContact.cod_usuario_reg },
                { "cod_usuario_web", eContact.cod_usuario_web },
                { "cod_clave_web", eContact.cod_clave_web },
                { "dsc_observaciones", eContact.dsc_observaciones }
            };

            if (eContact.fch_nacimiento.ToString().Contains("1/01/0001")) { dictionary.Add("fch_nacimiento", DBNull.Value); } else { dictionary.Add("fch_nacimiento", eContact.fch_nacimiento.ToString("yyyy-MM-dd")); }

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public string Eliminar_ClienteDireccionContacto(string cod_cliente, int num_linea, int cod_contacto)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_cliente", cod_cliente }, { "num_linea", num_linea }, { "cod_contacto", cod_contacto }
            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_ClienteDireccionContacto", dictionary);
            return result;
        }

        public T Guardar_Actualizar_ClienteCentroResponsabilidad<T>(eCliente_CentroResponsabilidad eCentroResp, string MiAccion) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = MiAccion == "Nuevo" ? "usp_Insertar_ClienteCentroResponsabilidad" : "usp_Actualizar_ClienteCentroResponsabilidad";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_cliente", eCentroResp.cod_cliente },
                { "cod_centroresp", eCentroResp.cod_centroresp },
                { "dsc_centroresp", eCentroResp.dsc_centroresp },
                { "dsc_centroresp_cliente", eCentroResp.dsc_centroresp_cliente },
                { "flg_activo", eCentroResp.flg_activo },
                { "flg_consolidador", eCentroResp.flg_consolidador },
                { "num_nivel", eCentroResp.num_nivel },
                { "cod_centroresp_sup", eCentroResp.cod_centroresp_sup },
                { "num_linea", eCentroResp.num_linea },
                { "cod_contacto", eCentroResp.cod_contacto },
                { "dsc_observaciones", eCentroResp.dsc_observaciones }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public string Inactivar_ClienteCentroResponsabilidad(string cod_cliente, string cod_centroresp)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_cliente", cod_cliente }, { "cod_centroresp", cod_centroresp }
            };

            result = sql.ExecuteScalarWithParams("usp_Inactivar_ClienteCentroResponsabilidad", dictionary);
            return result;
        }
        public List<T> ListarVendedores<T>(int opcion) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion }
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarClientes", oDictionary);
            return myList;
        }

        public T ValidacionEliminar<T>(int opcion, string cod_cliente = "", int num_linea = 0, int cod_contacto = 0) where T : class, new()
        {
            T obj = new T();
            string procedure = "usp_ConsultasVarias_Cliente";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_cliente", cod_cliente },
                { "num_linea", num_linea },
                { "cod_contacto", cod_contacto }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public string Eliminar_Cliente(string cod_cliente)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_cliente", cod_cliente }
            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_Cliente", dictionary);
            return result;
        }
        public T Validar_NroDocumento<T>(int opcion, string dsc_documento) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion }, {"dsc_documento", dsc_documento }
            };

            obj = sql.ConsultarEntidad<T>("usp_ConsultasVarias_Cliente", dictionary);
            return obj;
        }
        public T Guardar_Actualizar_ClienteEmpresas<T>(eCliente_Empresas eCli) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_cliente", eCli.cod_cliente },
                { "cod_empresa", eCli.cod_empresa },
                { "valorRating", eCli.valorRating },
                { "flg_activo", eCli.flg_activo },
                { "cod_usuario_registro", eCli.cod_usuario_registro },
                { "dsc_pref_ceco", eCli.dsc_pref_ceco }
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_ClienteEmpresas", dictionary);
            return obj;
        }
        public List<T> ListarEmpresasCliente<T>(int opcion, string cod_cliente, string cod_usuario = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                { "cod_cliente", cod_cliente},
                { "cod_usuario", cod_usuario}
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarClientes", oDictionary);
            return myList;
        }
    }
}
