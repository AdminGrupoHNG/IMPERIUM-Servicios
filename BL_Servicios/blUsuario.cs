using BE_Servicios;
using DA_Servicios;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;

namespace BL_Servicios
{
    public class blUsuario
    {

        daSQL sql = new daSQL();

        public List<T> ListarUsuarios<T>(int opcion, string flg_activo="", string cod_perfil="") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"flg_activo", flg_activo },
                {"cod_perfil", cod_perfil }

            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarUsuarios", oDictionary);
            return myList;
        }


        public List<T> ListarOpcionesMenu<T>(int opcion) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion }
            };

            myList = sql.ListaconSP<T>("usp_ConsultasVarias_Usuario", oDictionary);
            return myList;
        }

        public T ObtenerUsuario<T>(int opcion, string cod_usuario) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_Consulta_ListarUsuarios", dictionary);
            return obj;
        }

        public List<T> ListarPerfiles<T>(int opcion, string cod_usuario) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "cod_usuario", cod_usuario}
            };
            myList = sql.ListaconSP<T>("usp_Consulta_ListarPerfiles", dictionary);
            return myList;
            
        }

        public T Guardar_Actualizar_Usuario<T>(eUsuario eUsu, string MiAccion,string coduser) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = MiAccion == "Nuevo" ? "usp_Insertar_Usuario" : "usp_Actualizar_Usuario";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_usuario", eUsu.cod_usuario },
                { "dsc_usuario", eUsu.dsc_usuario },
                { "dsc_clave", eUsu.dsc_clave },
                { "flg_activo", eUsu.flg_activo },
                { "flg_noexpira", eUsu.flg_noexpira },
                { "flg_administrador", eUsu.flg_administrador },
                { "cod_usuariobd", eUsu.cod_usuariobd },
                { "dsc_clavebd", eUsu.dsc_clavebd },
                { "flg_trabajador", eUsu.flg_trabajador },
                { "cod_trabajador", eUsu.cod_trabajador },
                { "flg_audita_login", eUsu.flg_audita_login },
                { "dsc_ruta_firma", eUsu.dsc_ruta_firma },
                { "dsc_correo", eUsu.dsc_correo },
                { "dsc_contraseña", eUsu.dsc_contraseña },
                { "num_dias_cambio_contraseña", eUsu.num_dias_cambio_contraseña },
                { "fch_cambioclave", eUsu.fch_cambioclave },
                { "cod_usuario_cambio",coduser }

            };

           
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T Eliminar_Usuario<T>(int opcion ,string cod_usuario) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Eliminar_Usuario";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_usuario", cod_usuario },
               

            };


            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T Guardar_PerfilUsuario<T>(int opcion,string cod_usuario, string cod_perfiles , string cod_usuario_registro) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
           
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_usuario", cod_usuario },
                { "cod_perfiles", cod_perfiles },
                { "cod_usuario_registro", cod_usuario_registro }
                

            };
            procedure = "usp_Insertar_PerfilUsuario";

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public List<T> ObtenerTrabajadores<T>(int opcion) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "opcion", opcion}
            };
            myList = sql.ListaconSP<T>("usp_Consulta_Trabajadores", dictionary);
            return myList;

        }


        public void CargaCombosLookUp(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "")
        {
            combo.Text = "";
            string procedure = "usp_ConsultasVarias_Usuario";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "DominioCorreo":
                        dictionary.Add("opcion", 2);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    
                }

                combo.Properties.DataSource = tabla;
                combo.Properties.ValueMember = campoValueMember;
                combo.Properties.DisplayMember = campoDispleyMember;
                if (campoSelectedValue == "") { combo.ItemIndex = -1; } else { combo.EditValue = campoSelectedValue; }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
		
		public T ObtenerUsuarioLogin<T>(int opcion, string cod_usuario) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "cod_usuario", cod_usuario}
            };

            obj = sql.ConsultarEntidad<T>("usp_ConsultasVarias_Login", dictionary);
            return obj;
        }






        public string TestConnection()
        {
            string result = "";
            result = sql.TestConnection();

            return result;
        }

        //public void CargaCombosLookUp(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", string cod_cliente = "", string cod_nivel = "", int num_linea = 0, int num_nivel = 0)
        //{
        //    combo.Text = "";
        //    string procedure = "usp_ConsultasVarias_Login";
        //    Dictionary<string, object> dictionary = new Dictionary<string, object>();
        //    DataTable tabla = new DataTable();

        //    try
        //    {
        //        switch (nCombo)
        //        {
        //            case "Empresa":
        //                dictionary.Add("opcion", 2);
        //                tabla = sql.ListaDatatable(procedure, dictionary);
        //                break;
        //            case "Localidad":
        //                dictionary.Add("opcion", 3);
        //                tabla = sql.ListaDatatable(procedure, dictionary);
        //                break;
        //        }

        //        combo.Properties.DataSource = tabla;
        //        combo.Properties.ValueMember = campoValueMember;
        //        combo.Properties.DisplayMember = campoDispleyMember;
        //        if (campoSelectedValue == "") { combo.ItemIndex = -1; } else { combo.EditValue = campoSelectedValue; }
        //    }
        //    catch (Exception ex)
        //    {
        //        return;
        //        //throw;
        //    }
        //}
        //public List<T> ListarEmpresas<T>(int opcion) where T : class, new()
        //{
        //    List<T> myList = new List<T>();
        //    Dictionary<string, object> oDictionary = new Dictionary<string, object>()
        //    {
        //        {"opcion", opcion }
        //    };

        //    myList = sql.ListaconSP<T>("usp_ConsultasVarias_Login", oDictionary);
        //    return myList;
        //}
        //public List<T> ListarMenuSoluciones<T>(int opcion) where T : class, new()
        //{
        //    List<T> myList = new List<T>();
        //    Dictionary<string, object> oDictionary = new Dictionary<string, object>()
        //    {
        //        {"opcion", opcion }
        //    };

        //    myList = sql.ListaconSP<T>("usp_ConsultasVarias_Login", oDictionary);
        //    return myList;
        //}
        public string Actualizar_ClaveUsuario(eUsuario user)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "dsc_clave", user.dsc_clave }, { "fch_cambioclave", user.fch_cambioclave }, { "cod_usuario", user.cod_usuario }
            };

            result = sql.ExecuteScalarWithParams("usp_Actualizar_ClaveUsuario", dictionary);
            return result;
        }

        public List<T> ListarEmpresas<T>(int opcion, string cod_usuario, string cod_empresa = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "cod_usuario", cod_usuario},
                { "cod_empresa", cod_empresa}
            };
            myList = sql.ListaconSP<T>("usp_Consulta_ListarUsuarios", dictionary);
            return myList;
        }

        public T Guardar_Eliminar_UsuarioEmpresas<T>(eUsuario_Empresas eUsu) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_usuario", eUsu.cod_usuario },
                { "cod_empresa", eUsu.cod_empresa }
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Eliminar_UsuarioEmpresas", dictionary);
            return obj;
        }
        
    }
}
