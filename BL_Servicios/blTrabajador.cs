using DA_Servicios;
using BE_Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using System.Data;

namespace BL_Servicios
{
    public class blTrabajador
    {
        daSQL sql = new daSQL();


        public void CargaCombosLookUp(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", bool valorDefecto = false, string cod_empresa = "", string cod_sede_empresa = "", string cod_area = "", string cod_tipoContrato = "", string cod_segmento = "", string cod_grupo = "", string cod_actividad = "")
        {
            combo.Text = "";
            string procedure = "usp_ConsultasVarias_Trabajador";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "Empresa":
                        dictionary.Add("opcion", 2);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "EstadoCivil":
                        dictionary.Add("opcion", 3);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "SistPension":
                        dictionary.Add("opcion", 4);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "NombreAFP":
                        dictionary.Add("opcion", 5);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "SedesEmpresa":
                        dictionary.Add("opcion", 6);
                        dictionary.Add("cod_empresa", cod_empresa);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "AreaEmpresa":
                        dictionary.Add("opcion", 7);
                        dictionary.Add("cod_empresa", cod_empresa);
                        dictionary.Add("cod_sede_empresa", cod_sede_empresa);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "CargoEmpresa":
                        dictionary.Add("opcion", 8);
                        dictionary.Add("cod_empresa", cod_empresa);
                        dictionary.Add("cod_sede_empresa", cod_sede_empresa);
                        dictionary.Add("cod_area", cod_area);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoContrato":
                        dictionary.Add("opcion", 9);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Modalidad":
                        dictionary.Add("opcion", 10);
                        dictionary.Add("cod_tipoContrato", cod_tipoContrato);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Parentesco":
                        dictionary.Add("opcion", 11);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "GrupoSanguineo":
                        dictionary.Add("opcion", 15);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "EstadoSalud":
                        dictionary.Add("opcion", 16);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoSeguro":
                        dictionary.Add("opcion", 17);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "PeriodoPrueba":
                        dictionary.Add("opcion", 18);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "NivelAcademico":
                        dictionary.Add("opcion", 19);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoPropiedad":
                        dictionary.Add("opcion", 20);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoVivienda":
                        dictionary.Add("opcion", 21);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoComodidad":
                        dictionary.Add("opcion", 22);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TallasUniforme":
                        dictionary.Add("opcion", 23);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Segmento":
                        dictionary.Add("opcion", 24);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Grupo":
                        dictionary.Add("opcion", 25);
                        dictionary.Add("cod_segmento", cod_segmento);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Actividad":
                        dictionary.Add("opcion", 26);
                        dictionary.Add("cod_segmento", cod_segmento);
                        dictionary.Add("cod_grupo", cod_grupo);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Usuarios":
                        procedure = "usp_Consulta_ListarUsuarios";
                        dictionary.Add("opcion", 1);
                        dictionary.Add("flg_activo", "SI");
                        dictionary.Add("cod_perfil", "");
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "UsuariosControlHoras":
                        dictionary.Add("opcion", 28);
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
            string procedure = "usp_ConsultasVarias_Trabajador";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "TipoDocumento":
                        dictionary.Add("opcion", 1);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoDistrito":
                        procedure = "usp_ConsultasVarias_Cliente";
                        dictionary.Add("opcion", 16);
                        dictionary.Add("cod_condicion", cod_condicion);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Banco":
                        procedure = "usp_ConsultasVarias_Proveedor";
                        dictionary.Add("opcion", 6);
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

        public List<T> ListarOpcionesTrabajador<T>(int opcion, string cod_empresa = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_empresa", cod_empresa }
            };

            myList = sql.ListaconSP<T>("usp_ConsultasVarias_Trabajador", oDictionary);
            return myList;
        }

        public T ObtenerDatosOneDrive<T>(int opcion, string cod_empresa = "", string dsc_Carpeta = "", string cod_trabajador = "") where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_empresa", cod_empresa },
                { "dsc_Carpeta", dsc_Carpeta },
                { "cod_trabajador", cod_trabajador }
            };

            obj = sql.ConsultarEntidad<T>("usp_ConsultasVarias_Trabajador", oDictionary);
            return obj;
        }

        public string ActualizarInformacionDocumentos(string flg_Adjuntos, int opcionDoc, eTrabajador objTrab)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("cod_trabajador", objTrab.cod_trabajador);
            dictionary.Add("cod_empresa", objTrab.cod_empresa);
            dictionary.Add("flg_Adjuntos", flg_Adjuntos);
            dictionary.Add("opcionDoc", opcionDoc);
            dictionary.Add("idCarpeta_Trabajador", objTrab.idCarpeta_Trabajador);
            dictionary.Add("id_DNI", objTrab.id_DNI);
            dictionary.Add("id_CV", objTrab.id_CV);
            dictionary.Add("id_AntPolicial", objTrab.id_AntPolicial);
            dictionary.Add("id_AntPenal", objTrab.id_AntPenal);
            dictionary.Add("id_VerifDomiciliaria", objTrab.id_VerifDomiciliaria);

            string result;
            result = sql.ExecuteScalarWithParams("usp_Insertar_Actualizar_Trabajador", dictionary);

            return result;
        }

        public string EliminarInactivar_DatosTrabajador(int opcion, string cod_trabajador, string cod_usuario_registro, int cod_contactemerg = 0, int cod_infolab = 0, int cod_infofamiliar = 0, int cod_infoacademica = 0, int cod_infoprofesional = 0, int cod_infoeconomica = 0)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("cod_trabajador", cod_trabajador);
            dictionary.Add("cod_contactemerg", cod_contactemerg);
            dictionary.Add("cod_infolab", cod_infolab);
            dictionary.Add("cod_infofamiliar", cod_infofamiliar);
            dictionary.Add("cod_infoacademica", cod_infoacademica);
            dictionary.Add("cod_infoprofesional", cod_infoprofesional);
            dictionary.Add("cod_infoeconomica", cod_infoeconomica);
            dictionary.Add("cod_usuario_registro", cod_usuario_registro);

            string result;
            result = sql.ExecuteScalarWithParams("usp_Eliminar_Inactivar_DatosTrabajador", dictionary);

            return result;
        }

        public List<T> ListarTrabajadores<T>(int opcion, string cod_trabajador, string cod_empresa) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_trabajador", cod_trabajador },
                { "cod_empresa", cod_empresa }
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarTrabajador", oDictionary);
            return myList;
        }

        public T Obtener_Trabajador<T>(int opcion, string cod_trabajador, string cod_empresa) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_trabajador", cod_trabajador },
                { "cod_empresa", cod_empresa }
            };

            obj = sql.ConsultarEntidad<T>("usp_Consulta_ListarTrabajador", dictionary);
            return obj;
        }

        public T Obtener_costo_usuario<T>(string usuario) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", 27 },
                { "cod_usuario", usuario }
            };

            obj = sql.ConsultarEntidad<T>("usp_ConsultasVarias_Trabajador", dictionary);
            return obj;
        }

        public T InsertarActualizar_Trabajador<T>(eTrabajador eTrab) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
				{ "cod_trabajador", eTrab.cod_trabajador },
				{ "cod_usuario", eTrab.cod_usuario },
				{ "cod_empresa", eTrab.cod_empresa },
				{ "dsc_apellido_paterno", eTrab.dsc_apellido_paterno },
				{ "dsc_apellido_materno", eTrab.dsc_apellido_materno },
				{ "dsc_nombres", eTrab.dsc_nombres },
				{ "cod_estadocivil", eTrab.cod_estadocivil },
				{ "fch_nacimiento", eTrab.fch_nacimiento },
				{ "cod_tipo_documento", eTrab.cod_tipo_documento },
				{ "dsc_documento", eTrab.dsc_documento },
				//{ "fch_vcto_documento", eTrab.fch_vcto_documento },
				{ "nro_ubigeo_documento", eTrab.nro_ubigeo_documento },
				{ "cod_nacionalidad", eTrab.cod_nacionalidad },
				{ "dsc_direccion", eTrab.dsc_direccion },
                { "cod_distrito", eTrab.cod_distrito },
				{ "cod_provincia", eTrab.cod_provincia },
				{ "cod_departamento", eTrab.cod_departamento },
				{ "cod_pais", eTrab.cod_pais },
				{ "dsc_referencia", eTrab.dsc_referencia },
				{ "dsc_mail_1", eTrab.dsc_mail_1 },
				{ "dsc_mail_2", eTrab.dsc_mail_2 },
				{ "dsc_telefono", eTrab.dsc_telefono },
				{ "dsc_celular", eTrab.dsc_celular },
				{ "cod_sist_pension", eTrab.cod_sist_pension },
				{ "cod_APF", eTrab.cod_APF },
				{ "cod_CUSPP", eTrab.cod_CUSPP },
				{ "flg_DNI", eTrab.flg_DNI },
				{ "flg_CV", eTrab.flg_CV },
				{ "flg_AntPolicial", eTrab.flg_AntPolicial },
				{ "flg_AntPenal", eTrab.flg_AntPenal },
				{ "flg_VerifDomiciliaria", eTrab.flg_VerifDomiciliaria },
                { "cod_TipoPersonal", eTrab.cod_TipoPersonal },
				{ "flg_activo", eTrab.flg_activo },
				{ "cod_usuario_registro", eTrab.cod_usuario_registro },
			};
            if (eTrab.fch_vcto_documento.ToString().Contains("1/01/0001")) { dictionary.Add("fch_vcto_documento", DBNull.Value); } else { dictionary.Add("fch_vcto_documento", eTrab.fch_vcto_documento); }

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_Trabajador", dictionary);
            return obj;
        }

        public T InsertarActualizar_ContactoTrabajador<T>(eTrabajador.eContactoEmergencia_Trabajador eTrab) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_trabajador", eTrab.cod_trabajador },
				{ "cod_empresa", eTrab.cod_empresa },
                { "cod_contactemerg", eTrab.cod_contactemerg },
                { "dsc_apellido_paterno", eTrab.dsc_apellido_paterno },
                { "dsc_apellido_materno", eTrab.dsc_apellido_materno },
                { "dsc_nombres", eTrab.dsc_nombres },
                { "cod_parentesco", eTrab.cod_parentesco },
                { "cod_tipo_documento", eTrab.cod_tipo_documento },
                { "dsc_documento", eTrab.dsc_documento },
                //{ "fch_nacimiento", eTrab.fch_nacimiento },
                { "dsc_telefono", eTrab.dsc_telefono },
                { "dsc_celular", eTrab.dsc_celular },
                { "flg_activo", eTrab.flg_activo },
                { "cod_usuario_registro", eTrab.cod_usuario_registro },
            };
            if (eTrab.fch_nacimiento.ToString().Contains("1/01/0001")) { dictionary.Add("fch_nacimiento", DBNull.Value); } else { dictionary.Add("fch_nacimiento", eTrab.fch_nacimiento); }

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_ContactEmergenciaTrabajador", dictionary);
            return obj;
        }

        public T InsertarActualizar_InfoLaboralTrabajador<T>(eTrabajador.eInfoLaboral_Trabajador eTrab) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_trabajador", eTrab.cod_trabajador },
				{ "cod_empresa", eTrab.cod_empresa },
                { "cod_infolab", eTrab.cod_infolab },
                { "fch_ingreso", eTrab.fch_ingreso },
                { "cod_sede_empresa", eTrab.cod_sede_empresa },
                { "cod_area", eTrab.cod_area },
                { "cod_cargo", eTrab.cod_cargo },
                { "dsc_pref_ceco", eTrab.dsc_pref_ceco },
                { "cod_tipo_contrato", eTrab.cod_tipo_contrato },
                { "fch_firma", eTrab.fch_firma },
                { "fch_vencimiento", eTrab.fch_vencimiento },
                { "cod_modalidad", eTrab.cod_modalidad },
                //{ "cod_moneda", eTrab.cod_moneda },
                { "imp_sueldo_base", eTrab.imp_sueldo_base },
                { "imp_asig_familiar", eTrab.imp_asig_familiar },
                { "imp_movilidad", eTrab.imp_movilidad },
                { "imp_alimentacion", eTrab.imp_alimentacion },
                { "imp_escolaridad", eTrab.imp_escolaridad },
                { "imp_bono", eTrab.imp_bono },
                //{ "cod_banco", eTrab.cod_banco },
                //{ "cod_tipo_cuenta", eTrab.cod_tipo_cuenta },
                //{ "dsc_cta_bancaria", eTrab.dsc_cta_bancaria },
                //{ "dsc_cta_interbancaria", eTrab.dsc_cta_interbancaria },
                { "flg_activo", eTrab.flg_activo },
                { "cod_usuario_registro", eTrab.cod_usuario_registro },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_InfoLaboralTrabajador", dictionary);
            return obj;
        }

        public T InsertarActualizar_CaractFisicasTrabajador<T>(eTrabajador.eCaractFisicas_Trabajador eTrab) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_trabajador", eTrab.cod_trabajador },
				{ "cod_empresa", eTrab.cod_empresa },
                //{ "cod_caractfisica", eTrab.cod_caractfisica },
                { "dsc_estatura", eTrab.dsc_estatura },
                { "dsc_peso", eTrab.dsc_peso },
                { "flg_lentes", eTrab.flg_lentes },
                { "dsc_IMC", eTrab.dsc_IMC },
                { "flg_activo", eTrab.flg_activo },
                { "cod_usuario_registro", eTrab.cod_usuario_registro },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_CaractFisicasTrabajador", dictionary);
            return obj;
        }

        public T InsertarActualizar_TallaUniformesTrabajador<T>(eTrabajador.eTallaUniforme_Trabajador eTrab) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_trabajador", eTrab.cod_trabajador },
				{ "cod_empresa", eTrab.cod_empresa },
                //{ "cod_tallauniforme", eTrab.cod_tallauniforme },
                { "cod_talla_polo", eTrab.cod_talla_polo },
                { "cod_talla_camisa", eTrab.cod_talla_camisa },
                { "cod_talla_pantalon", eTrab.cod_talla_pantalon },
                { "cod_talla_mameluco", eTrab.cod_talla_mameluco },
                { "cod_talla_casaca", eTrab.cod_talla_casaca },
                { "cod_talla_chaleco", eTrab.cod_talla_chaleco },
                { "cod_talla_calzado", eTrab.cod_talla_calzado },
                { "flg_activo", eTrab.flg_activo },
                { "cod_usuario_registro", eTrab.cod_usuario_registro },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_TallaUniformesTrabajador", dictionary);
            return obj;
        }

        public T InsertarActualizar_InfoFamiliarTrabajador<T>(eTrabajador.eInfoFamiliar_Trabajador eTrab) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_trabajador", eTrab.cod_trabajador },
				{ "cod_empresa", eTrab.cod_empresa },
                { "cod_infofamiliar", eTrab.cod_infofamiliar },
                { "cod_parentesco", eTrab.cod_parentesco },
                { "dsc_apellido_paterno", eTrab.dsc_apellido_paterno },
                { "dsc_apellido_materno", eTrab.dsc_apellido_materno },
                { "dsc_nombres", eTrab.dsc_nombres },
                //{ "fch_nacimiento", eTrab.fch_nacimiento },
                { "flg_vivo", eTrab.flg_vivo },
                { "cod_tipo_documento", eTrab.cod_tipo_documento },
                { "dsc_documento", eTrab.dsc_documento },
                { "dsc_mail", eTrab.dsc_mail },
                { "dsc_telefono", eTrab.dsc_telefono },
                { "dsc_celular", eTrab.dsc_celular },
                { "dsc_profesion", eTrab.dsc_profesion },
                { "dsc_centrolaboral", eTrab.dsc_centrolaboral },
                { "dsc_gradoinstruccion", eTrab.dsc_gradoinstruccion },
                { "dsc_ocupacion", eTrab.dsc_ocupacion },
                { "dsc_discapacidad", eTrab.dsc_discapacidad },
                { "dsc_direccion", eTrab.dsc_direccion },
                { "cod_distrito", eTrab.cod_distrito },
                { "cod_provincia", eTrab.cod_provincia },
                { "cod_departamento", eTrab.cod_departamento },
                { "cod_pais", eTrab.cod_pais },
                { "dsc_referencia", eTrab.dsc_referencia },
                { "flg_enfermedad", eTrab.flg_enfermedad },
                { "dsc_enfermedad", eTrab.dsc_enfermedad },
                { "flg_adiccion", eTrab.flg_adiccion },
                { "dsc_adiccion", eTrab.dsc_adiccion },
                { "flg_dependenciaeconomica", eTrab.flg_dependenciaeconomica },
                { "flg_activo", eTrab.flg_activo },
                { "cod_usuario_registro", eTrab.cod_usuario_registro },
            };
            if (eTrab.fch_nacimiento.ToString().Contains("1/01/0001")) { dictionary.Add("fch_nacimiento", DBNull.Value); } else { dictionary.Add("fch_nacimiento", eTrab.fch_nacimiento); }

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_InfoFamiliarTrabajador", dictionary);
            return obj;
        }

        public T InsertarActualizar_InfoEconomicaTrabajador<T>(eTrabajador.eInfoEconomica_Trabajador eTrab) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_trabajador", eTrab.cod_trabajador },
				{ "cod_empresa", eTrab.cod_empresa },
                { "cod_infoeconomica", eTrab.cod_infoeconomica },
                { "imp_ingresomensual", eTrab.imp_ingresomensual },
                { "imp_gastomensual", eTrab.imp_gastomensual },
                { "imp_totalactivos", eTrab.imp_totalactivos },
                { "imp_totaldeudas", eTrab.imp_totaldeudas },
                { "cod_vivienda", eTrab.cod_vivienda },
                { "cod_tipovivienda", eTrab.cod_tipovivienda },
                { "imp_valorvivienda", eTrab.imp_valorvivienda },
                { "cod_monedavivienda", eTrab.cod_monedavivienda },
                { "dsc_direccion_vivienda", eTrab.dsc_direccion_vivienda },
                { "cod_distrito_vivienda", eTrab.cod_distrito_vivienda },
                { "cod_provincia_vivienda", eTrab.cod_provincia_vivienda },
                { "cod_departamento_vivienda", eTrab.cod_departamento_vivienda },
                { "cod_pais_vivienda", eTrab.cod_pais_vivienda },
                { "dsc_referencia_vivienda", eTrab.dsc_referencia_vivienda },
                { "cod_tipovehiculo", eTrab.cod_tipovehiculo },
                { "dsc_marcavehiculo", eTrab.dsc_marcavehiculo },
                { "dsc_modelovehiculo", eTrab.dsc_modelovehiculo },
                { "dsc_placavehiculo", eTrab.dsc_placavehiculo },
                { "imp_valorvehiculo", eTrab.imp_valorvehiculo },
                { "cod_monedavehiculo", eTrab.cod_monedavehiculo },
                { "cod_tipoempresa", eTrab.cod_tipoempresa },
                { "dsc_participacion_empresa", eTrab.dsc_participacion_empresa },
                { "dsc_RUC_empresa", eTrab.dsc_RUC_empresa },
                { "dsc_giro_empresa", eTrab.dsc_giro_empresa },
                { "dsc_direccion_empresa", eTrab.dsc_direccion_empresa },
                { "cod_distrito_empresa", eTrab.cod_distrito_empresa },
                { "cod_provincia_empresa", eTrab.cod_provincia_empresa },
                { "cod_departamento_empresa", eTrab.cod_departamento_empresa },
                { "cod_pais_empresa", eTrab.cod_pais_empresa },
                { "dsc_referencia_empresa", eTrab.dsc_referencia_empresa },
                { "flg_activo", eTrab.flg_activo },
                { "cod_usuario_registro", eTrab.cod_usuario_registro },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_InfoEconomicaTrabajador", dictionary);
            return obj;
        }

        public T InsertarActualizar_InfoAcademicaTrabajador<T>(eTrabajador.eInfoAcademica_Trabajador eTrab, string flg_documento = "NO") where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_trabajador", eTrab.cod_trabajador },
				{ "cod_empresa", eTrab.cod_empresa },
                { "cod_infoacademica", eTrab.cod_infoacademica },
                { "cod_nivelacademico", eTrab.cod_nivelacademico },
                { "dsc_centroestudios", eTrab.dsc_centroestudios },
                { "dsc_lugar", eTrab.dsc_lugar },
                { "dsc_carrera_curso", eTrab.dsc_carrera_curso },
                { "dsc_grado", eTrab.dsc_grado },
                { "anho_inicio", eTrab.anho_inicio },
                { "anho_fin", eTrab.anho_fin },
                { "imp_promedio", eTrab.imp_promedio },
                { "flg_activo", eTrab.flg_activo },
                { "cod_usuario_registro", eTrab.cod_usuario_registro },
                { "id_certificado", eTrab.id_certificado },
                { "flg_documento", flg_documento },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_InfoAcademicaTrabajador", dictionary);
            return obj;
        }

        public T InsertarActualizar_InfoProfesionalTrabajador<T>(eTrabajador.eInfoProfesional_Trabajador eTrab, string flg_documento = "NO") where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_trabajador", eTrab.cod_trabajador },
				{ "cod_empresa", eTrab.cod_empresa },
                { "cod_infoprofesional", eTrab.cod_infoprofesional },
                { "dsc_razon_social", eTrab.dsc_razon_social },
                { "dsc_cargo", eTrab.dsc_cargo },
                //{ "fch_inicio", eTrab.fch_inicio },
                //{ "fch_fin", eTrab.fch_fin },
                { "dsc_motivo_salida", eTrab.dsc_motivo_salida },
                { "dsc_nombre_jefe", eTrab.dsc_nombre_jefe },
                { "dsc_cargo_jefe", eTrab.dsc_cargo_jefe },
                { "dsc_celular", eTrab.dsc_celular },
                { "dsc_comentarios", eTrab.dsc_comentarios },
                { "flg_activo", eTrab.flg_activo },
                { "cod_usuario_registro", eTrab.cod_usuario_registro },
                { "id_certificado", eTrab.id_certificado },
                { "flg_documento", flg_documento },
            };
            if (eTrab.fch_inicio.ToString().Contains("1/01/0001")) { dictionary.Add("fch_inicio", DBNull.Value); } else { dictionary.Add("fch_inicio", eTrab.fch_inicio); }
            if (eTrab.fch_fin.ToString().Contains("1/01/0001")) { dictionary.Add("fch_fin", DBNull.Value); } else { dictionary.Add("fch_fin", eTrab.fch_fin); }

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_InfoProfesionalTrabajador", dictionary);
            return obj;
        }

        public T InsertarActualizar_InfoBancariaTrabajador<T>(eTrabajador.eInfoBancaria_Trabajador eTrab) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_trabajador", eTrab.cod_trabajador },
				{ "cod_empresa", eTrab.cod_empresa },
                { "cod_banco", eTrab.cod_banco },
                { "cod_moneda", eTrab.cod_moneda },
                { "cod_tipo_cuenta", eTrab.cod_tipo_cuenta },
                { "dsc_cta_bancaria", eTrab.dsc_cta_bancaria },
                { "dsc_cta_interbancaria", eTrab.dsc_cta_interbancaria },
                { "cod_banco_CTS", eTrab.cod_banco_CTS },
                { "cod_moneda_CTS", eTrab.cod_moneda_CTS },
                { "dsc_cta_bancaria_CTS", eTrab.dsc_cta_bancaria_CTS },
                { "dsc_cta_interbancaria_CTS", eTrab.dsc_cta_interbancaria_CTS },
                { "cod_sist_pension", eTrab.cod_sist_pension },
                { "cod_APF", eTrab.cod_APF },
                { "cod_CUSPP", eTrab.cod_CUSPP },
                { "flg_activo", eTrab.flg_activo },
                { "cod_usuario_registro", eTrab.cod_usuario_registro },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_InfoBancariaTrabajador", dictionary);
            return obj;
        }
        public T InsertarActualizar_InfoSaludTrabajador<T>(eTrabajador.eInfoSalud_Trabajador eTrab, string flg_documento = "NO") where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_trabajador", eTrab.cod_trabajador },
				{ "cod_empresa", eTrab.cod_empresa },
                //{ "cod_infosalud", eTrab.cod_infosalud },
                { "flg_alergias", eTrab.flg_alergias },
                { "dsc_alergias", eTrab.dsc_alergias },
                { "flg_operaciones", eTrab.flg_operaciones },
                { "dsc_operaciones", eTrab.dsc_operaciones },
                { "flg_discapacidad", eTrab.flg_discapacidad },
                { "dsc_discapacidad", eTrab.dsc_discapacidad },
                { "flg_enfprexistente", eTrab.flg_enfprexistente },
                { "dsc_enfprexistente", eTrab.dsc_enfprexistente },
                { "flg_tratprexistente", eTrab.flg_tratprexistente },
                { "dsc_tratprexistente", eTrab.dsc_tratprexistente },
                { "flg_enfactual", eTrab.flg_enfactual },
                { "dsc_enfactual", eTrab.dsc_enfactual },
                { "flg_tratactual", eTrab.flg_tratactual },
                { "dsc_tratactual", eTrab.dsc_tratactual },
                { "flg_otros", eTrab.flg_otros },
                { "dsc_otros", eTrab.dsc_otros },
                { "dsc_gruposanguineo", eTrab.dsc_gruposanguineo },
                { "dsc_estadosalud", eTrab.dsc_estadosalud },
                { "dsc_tiposegurosalud", eTrab.dsc_tiposegurosalud },
                { "dsc_segurosalud", eTrab.dsc_segurosalud },
                { "cod_usuario_registro", eTrab.cod_usuario_registro },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_InfoSaludTrabajador", dictionary);
            return obj;
        }


        public T InsertarActualizar_CertificadoEMOTrabajador<T>(eTrabajador.eCertificadoEMO_Trabajador eTrab) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_trabajador", eTrab.cod_trabajador },
				{ "cod_empresa", eTrab.cod_empresa },
                { "cod_EMO", eTrab.cod_EMO },
                { "dsc_descripcion", eTrab.dsc_descripcion },
                { "fch_registro", eTrab.fch_registro },
                { "dsc_anho", eTrab.dsc_anho },
                { "flg_certificado", eTrab.flg_certificado },
                { "id_certificado", eTrab.id_certificado },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_CertificadoEMOTrabajador", dictionary);
            return obj;
        }

        public T InsertarActualizar_InfoViviendaTrabajador<T>(eTrabajador.eInfoVivienda_Trabajador eTrab) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_trabajador", eTrab.cod_trabajador },
				{ "cod_empresa", eTrab.cod_empresa },
                { "cod_infovivienda", eTrab.cod_infovivienda },
                { "cod_tipovivienda", eTrab.cod_tipovivienda },
                { "flg_puertas", eTrab.flg_puertas },
                { "flg_ventanas", eTrab.flg_ventanas },
                { "flg_techo", eTrab.flg_techo },
                { "flg_telefono", eTrab.flg_telefono },
                { "flg_celulares", eTrab.flg_celulares },
                { "flg_internet_comunicacion", eTrab.flg_internet_comunicacion },
                { "cod_tipocomodidad", eTrab.cod_tipocomodidad },
                { "flg_luz", eTrab.flg_luz },
                { "flg_agua", eTrab.flg_agua },
                { "flg_desague", eTrab.flg_desague },
                { "flg_gas", eTrab.flg_gas },
                { "flg_cable", eTrab.flg_cable },
                { "flg_internet_servicio", eTrab.flg_internet_servicio },
                { "dsc_viaacceso", eTrab.dsc_viaacceso },
                { "dsc_iluminacion", eTrab.dsc_iluminacion },
                { "dsc_entorno", eTrab.dsc_entorno },
                { "dsc_puestopolicial", eTrab.dsc_puestopolicial },
                { "dsc_nombre_familiar", eTrab.dsc_nombre_familiar },
                { "dsc_horasencasa", eTrab.dsc_horasencasa },
                { "cod_parentesco", eTrab.cod_parentesco },
                { "dsc_celular", eTrab.dsc_celular },
                { "dsc_mail", eTrab.dsc_mail },
                { "cod_usuario_registro", eTrab.cod_usuario_registro },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_InfoViviendaTrabajador", dictionary);
            return obj;
        }
        public List<T> ListarControlHoras<T>(int opcion, string @cod_usuario = "", string @cod_empresa = "", string @fechaInicio = "", string @fechaFin = "", string @cod_empresa_multiple = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_usuario", @cod_usuario },
                { "fechaInicio", @fechaInicio },
                { "fechaFin", @fechaFin },
                { "cod_empresa", @cod_empresa },
                { "cod_empresa_multiple", @cod_empresa_multiple }
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarControlHoras", oDictionary);
            return myList;
        }

        public T InsertarActualizar_ControlHoras<T>(eControlHoras eControlH) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_control_horas", eControlH.cod_control_horas },
                { "cod_usuario", eControlH.cod_usuario },
                { "dsc_comentario", eControlH.dsc_comentario },
                { "dsc_duracion", eControlH.dsc_duracion },
                { "fch_ejecucion", eControlH.fch_ejecucion },
                { "cod_segmento", eControlH.cod_segmento },
                { "cod_grupo", eControlH.cod_grupo },
                { "cod_actividad", eControlH.cod_actividad },
                { "imp_costo", eControlH.imp_costo },
                { "cod_empresa_usuaria", eControlH.cod_empresa_usuaria },
                { "cod_usuario_registro", eControlH.cod_usuario_registro },
                { "flg_activo", eControlH.flg_activo }
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_ControlHoras", dictionary);
            return obj;
        }

        public T InsertarActualizar_CostoHoras<T>(eControlHoras.eCostoHora eCostoHora) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_usuario", eCostoHora.cod_usuario},
                { "imp_costo", eCostoHora.imp_costo },
                { "cod_usuario_registro", eCostoHora.cod_usuario_registro },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_CostoxHora", dictionary);
            return obj;
        }

    }
}
