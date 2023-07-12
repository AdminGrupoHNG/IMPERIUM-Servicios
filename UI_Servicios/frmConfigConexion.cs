using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;
using BE_Servicios;
using BL_Servicios;
using DevExpress.XtraSplashScreen;
using System.Xml;
using System.Globalization;

namespace UI_Servicios
{
    public partial class frmConfigConexion : DevExpress.XtraEditors.XtraForm
    {
        blUsuario blUser = new blUsuario();
        blGlobales blGlobal = new blGlobales();
        blEncrypta blEncryp = new blEncrypta();
        public eGlobales eGlobal = new eGlobales();
        public bool DatosGuardados = false;
        //public string Entorno = "";

        public frmConfigConexion()
        {
            InitializeComponent();
        }

        private void frmConfigConexion_Load(object sender, EventArgs e)
        {
            chkServidorLocal.CheckState = eGlobal.Entorno == "LOCAL" ? CheckState.Checked : CheckState.Unchecked;
            chkServidorRemoto.CheckState = eGlobal.Entorno == "REMOTO" ? CheckState.Checked : CheckState.Unchecked;
            txtServidorLOCAL.Text = eGlobal.ServidorLOCAL;
            txtServidorREMOTO.Text = eGlobal.ServidorREMOTO;
            txtBaseDatos.Text = eGlobal.BBDD;
            grdbFormatoFecha.SelectedIndex = eGlobal.FormatoFecha == "DD-MM-YYYY" ? 0 : 1;

            string result = Validar_Conexion();
            //if (result == "OK") blUser.CargaCombosLookUp("Empresa", lkpEmpresaInicio, "cod_empresa", "dsc_empresa", "");
            //lkpEmpresaInicio.EditValue = eGlobal.UltimaEmpresa;
        }
        private string Validar_Conexion()
        {
            string result = "";
            result = blUser.TestConnection();

            return result;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                GuardarDatos();
                string result = Validar_Conexion();
                if (result == "OK") 
                { 
                    XtraMessageBox.Show("Se guardaron los datos de conexión de manera satisfactoria.", "Guardar datos de conexión", MessageBoxButtons.OK);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                DatosGuardados = false;
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GuardarDatos()
        {
            //blEncryp.Desencrypta();
            Actualizar_appSettings();
            DatosGuardados = true;
            Asignar_VariablesGlobales();
            //blEncryp.Encrypta();
        }
        private void Encryptar_AppConfig()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes == null || node.Attributes.Count == 0) continue;
                        node.Attributes[0].Value = blEncryp.Encrypta(node.Attributes[0].Value);
                        node.Attributes[1].Value = blEncryp.Encrypta(node.Attributes[1].Value);
                    }
                }
            }
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void Actualizar_appSettings()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes == null || node.Attributes.Count == 0) continue;
                        switch (blEncryp.Desencrypta(node.Attributes[0].Value))
                        {
                            case "conexion": node.Attributes[1].Value = blEncryp.Encrypta(chkServidorLocal.CheckState == CheckState.Checked ? "LOCAL" : "REMOTO"); break;
                            case "ServidorLOCAL": node.Attributes[1].Value = blEncryp.Encrypta(txtServidorLOCAL.Text); break;
                            case "ServidorREMOTO": node.Attributes[1].Value = blEncryp.Encrypta(txtServidorREMOTO.Text); break;
                            case "BBDD": node.Attributes[1].Value = blEncryp.Encrypta(txtBaseDatos.Text); break;
                            case "FormatoFecha": node.Attributes[1].Value = blEncryp.Encrypta(grdbFormatoFecha.SelectedIndex == 0 ? "DD-MM-YYYY" : "YYYY-MM-DD"); break;
                            //case "UltimaEmpresa": node.Attributes[1].Value = blEncryp.Encrypta(lkpEmpresaInicio.EditValue.ToString()); break;
                            case "SeparadorListas": node.Attributes[1].Value = blEncryp.Encrypta(CultureInfo.CurrentCulture.TextInfo.ListSeparator); break;
                        }
                    }
                }
            }
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            ConfigurationManager.RefreshSection("appSettings");
        }
        
        private void Asignar_VariablesGlobales()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", "").Replace("Config", "config"));
            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes == null || node.Attributes.Count == 0) continue;
                        switch (blEncryp.Desencrypta(node.Attributes[0].Value))
                        {
                            case "conexion": eGlobal.Entorno = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            case "ServidorLOCAL": eGlobal.ServidorLOCAL = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            case "ServidorREMOTO": eGlobal.ServidorREMOTO = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            case "BBDD": eGlobal.BBDD = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            case "FormatoFecha": eGlobal.FormatoFecha = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            case "SeparadorListas": eGlobal.SeparadorListas = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            case "SeparadorDecimal": eGlobal.SeparadorDecimal = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            case "UltimoLocalidad": eGlobal.UltimoLocalidad = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            //case "UltimaEmpresa": eGlobal.UltimaEmpresa = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            case "UltimoUsuario": eGlobal.UltimoUsuario = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                        }
                    }
                }
            }
        }
        private void btnProbarConexion_Click(object sender, EventArgs e)
        {
            try
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Conectandose al servidor", "Espere...");
                //Encryptar_AppConfig();
                GuardarDatos();
                string result = Validar_Conexion();
                if (result == "OK")
                {
                    XtraMessageBox.Show("Conexión exitosa.", "Conexión", MessageBoxButtons.OK);
                }
                else
                {
                    XtraMessageBox.Show(Environment.NewLine + "Conexión fallida." + Environment.NewLine + "Por favor corrija los datos ingresados e intente nuevamente.", "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkServidorLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkServidorLocal.CheckState == CheckState.Checked) chkServidorRemoto.CheckState = CheckState.Unchecked;
        }

        private void chkServidorRemoto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkServidorRemoto.CheckState == CheckState.Checked) chkServidorLocal.CheckState = CheckState.Unchecked;
        }
    }
}