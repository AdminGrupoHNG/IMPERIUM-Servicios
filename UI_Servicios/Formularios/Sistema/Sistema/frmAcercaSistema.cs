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
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Configuration;
using BL_Servicios;
using BE_Servicios;

namespace UI_Servicios.Formularios.Sistema.Sistema
{
    public partial class frmAcercaSistema : DevExpress.XtraEditors.XtraForm
    {
        blEncrypta blEncryp = new blEncrypta();
        blSistema blSis = new blSistema();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;

        public frmAcercaSistema()
        {
            InitializeComponent();
        }

        private void frmAcercaSistema_Load_1(object sender, EventArgs e)
        {
            groupBox1.BackColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
            groupBox2.BackColor = Color.FromArgb(colorPlomo[0], colorPlomo[1], colorPlomo[2]);

            lblHostName.Text = Environment.MachineName;
            lblUsuarioWindows.Text = Environment.UserName;
            lblNombreDominio.Text = Environment.UserDomainName;
            lblIPAddress.Text = ObtenerIP();
            lblMemoriaRAM.Text = PerformanceInfo.GetTotalMemoryInMiB().ToString() + " GB";

          
            try {
                lblModo.Text = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("conexion")].ToString());
                lblServidor.Text = lblModo.Text == "LOCAL" ? blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("ServidorLOCAL")].ToString()) : blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("ServidorREMOTO")].ToString());
                lblIPServidor.Text = lblModo.Text == "LOCAL" ? blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("ServidorLOCAL")].ToString()) : blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("ServidorREMOTO")].ToString());
                //lblEmpresa.Text = ObtenerEmpresa(blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("UltimaEmpresa")].ToString()));
                lblBaseDatos.Text = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("BBDD")].ToString());
                lblVersion.Text = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("VersionApp")].ToString());
            } catch
            {
                lblModo.Text = ConfigurationManager.AppSettings["conexion"].ToString();
                lblServidor.Text = lblModo.Text == "LOCAL" ? ConfigurationManager.AppSettings["ServidorLOCAL"].ToString() : ConfigurationManager.AppSettings["ServidorREMOTO"].ToString(); 
                lblIPServidor.Text = lblModo.Text == "LOCAL" ? ConfigurationManager.AppSettings["ServidorLOCAL"].ToString() : ConfigurationManager.AppSettings["ServidorREMOTO"].ToString();
                //lblEmpresa.Text = ObtenerEmpresa(ConfigurationManager.AppSettings["UltimaEmpresa"].ToString());
                lblBaseDatos.Text = ConfigurationManager.AppSettings["BBDD"].ToString();
                lblVersion.Text=ConfigurationManager.AppSettings["VersionApp"].ToString();
            }


           
        }
        public string ObtenerEmpresa(string cod_empresa)
        {
            string NombreEmpresa = "";
            List<eSistema> eSist = blSis.Obtener_ParamterosSistema<eSistema>(4, cod_empresa:cod_empresa);
            if (eSist.Count > 0) { 
            NombreEmpresa = eSist[0].dsc_valor;
            }
            return NombreEmpresa;
        } 
        private void picCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        {
            //Process.Start(hyperlinkLabelControl1.Text);
        }

        public static class PerformanceInfo
        {
            [DllImport("psapi.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetPerformanceInfo([Out] out PerformanceInformation PerformanceInformation, [In] int Size);

            [StructLayout(LayoutKind.Sequential)]
            public struct PerformanceInformation
            {
                public int Size;
                public IntPtr CommitTotal;
                public IntPtr CommitLimit;
                public IntPtr CommitPeak;
                public IntPtr PhysicalTotal;
                public IntPtr PhysicalAvailable;
                public IntPtr SystemCache;
                public IntPtr KernelTotal;
                public IntPtr KernelPaged;
                public IntPtr KernelNonPaged;
                public IntPtr PageSize;
                public int HandlesCount;
                public int ProcessCount;
                public int ThreadCount;
            }

            public static Int64 GetTotalMemoryInMiB()
            {
                PerformanceInformation pi = new PerformanceInformation();
                if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
                {
                    return Convert.ToInt64(Math.Round((pi.PhysicalTotal.ToInt64() * pi.PageSize.ToInt64() / Convert.ToDecimal(1073741824)), 0));
                }
                else
                {
                    return -1;
                }
            }
        }

        private string ObtenerIP()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        private void frmAcercaSistema_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}