using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BL_Servicios;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.DataAccess.ConnectionParameters;

namespace UI_Servicios.Formularios.Clientes_Y_Proveedores.Proveedores
{
    public partial class subrptServiciosProveedor : DevExpress.XtraReports.UI.XtraReport
    {
        blEncrypta blEncryp = new blEncrypta();
        SqlConnection Conexion_Reporte = new SqlConnection();
        public subrptServiciosProveedor()
        {
            InitializeComponent();
        }

        private void sqlDataSource1_ConfigureDataConnection(object sender, DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs e)
        {
            string entorno = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("conexion")].ToString());
            string Servidor = blEncryp.Desencrypta(entorno == "LOCAL" ? ConfigurationManager.AppSettings[blEncryp.Encrypta("ServidorLOCAL")].ToString() : ConfigurationManager.AppSettings[blEncryp.Encrypta("ServidorREMOTO")].ToString());
            string BBDD = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("BBDD")].ToString());
            string UserID = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("UserID")].ToString());
            string Password = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("Password")].ToString());

            e.ConnectionParameters = new MsSqlConnectionParameters(Servidor, BBDD, UserID, Password, MsSqlAuthorizationType.SqlServer);
        }
    }
}
