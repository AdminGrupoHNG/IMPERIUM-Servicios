using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Configuration;
using System.Globalization;
using System.Net;
using UI_Servicios.Clientes_Y_Proveedores.Clientes;
using BE_Servicios;
using BL_Servicios;
using DevExpress.Utils;
using System.Xml;
using UI_Servicios.Formularios.Sistema.Accesos;
using UI_Servicios.Formularios.Sistema.Sistema;
using UI_Servicios.Formularios.Sistema.Configuracion_del_Sistema;
using UI_Servicios.Formularios.Cuentas_Pagar;
using UI_Servicios.Formularios.Sistema.Configuraciones_Maestras;
using System.IO;
using UI_Servicios.Formularios.Logistica;
using UI_Servicios.Formularios.Cotizaciones;

namespace UI_Servicios
{
    public partial class frmPrincipal : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public eUsuario user = new eUsuario();
        public eGlobales eGlobal = new eGlobales();
        blEncrypta blEncryp = new blEncrypta();
        blUsuario blUsu = new blUsuario();
        blSistema blSist = new blSistema();
        public string cod_empresa = "", Entorno = "LOCAL", Servidor = "", BBDD = "", FormatoFecha = "";
        public string formName;
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            InhabilitarBotones();
            Inicializar();
            HabilitarBotones();
            btnEliminarExportados.Enabled = true;

            btnNuevoReq.Enabled = true;
            btnListadoAnalisis.Enabled = true;
            btnListadoCliente.Enabled = true;
            btnRegistroCliente.Enabled = true;
        }
       
        private void Inicializar()
        {
            string IP = ObtenerIP();
            ObtenerUsuario();

            Entorno = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("conexion")].ToString());
            string Servidor = Entorno == "LOCAL" ? blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("ServidorLOCAL")].ToString()) : blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("ServidorREMOTO")].ToString());
            string BBDD = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("BBDD")].ToString());
            string Version = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("VersionApp")].ToString());
            string nombrePC = Environment.MachineName;
            lblServidor.Caption = "Conectado a -> " + Servidor + " - " + BBDD;
            lblIPAddress.Caption = "IP : " + IP;
            lblHostName.Caption = "Nombre Equipo : " + nombrePC;
            lblVersion.Caption = "Versión: " + Version;
            lblUsuario.Caption = user.dsc_usuario.ToUpper();
            //entorno = Entorno;
            switch (Entorno)
            {
                case "LOCAL": lblEntorno.Caption = "LOCAL"; lblEntorno.ItemAppearance.Normal.BackColor = Color.Green; lblEntorno.ItemAppearance.Normal.ForeColor = Color.White; break;
                case "REMOTO": lblEntorno.Caption = "REMOTO"; lblEntorno.ItemAppearance.Normal.BackColor = Color.DarkKhaki; lblEntorno.ItemAppearance.Normal.ForeColor = Color.Black; break;
            }
            lblEntorno.Caption = Entorno;
            SuperToolTip tool = new SuperToolTip();
            SuperToolTipSetupArgs args = new SuperToolTipSetupArgs();
            args.Contents.Text = Servidor + " -> " + BBDD;
            tool.Setup(args);
            lblServidor.SuperTip = tool;
        }
        private void ObtenerUsuario()
        {
            user = blUsu.ObtenerUsuarioLogin<eUsuario>(1, user.cod_usuario);
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

        private void InhabilitarBotones()
        {
            foreach (var item in ribbon.Items)
            {
                if (item.GetType() == typeof(BarButtonItem))
                {
                    if (((BarButtonItem)item).Name != "btnCambiarContraseña" && ((BarButtonItem)item).Name != "btnHistorialVersiones" &&
                        ((BarButtonItem)item).Name != "btnAcercaDeSistema")
                    {
                        ((BarButtonItem)item).Enabled = false;
                    }
                }
            }
        }
        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = blSist.ListarMenuxUsuario<eVentana>(user.cod_usuario, "SERVICIOS");

            if (listPermisos.Count > 0)
            {
                for (int i = 0; i < listPermisos.Count; i++)
                {
                    foreach (var item in ribbon.Items)
                    {
                        if (item.GetType() == typeof(BarButtonItem))
                        {
                            if (((BarButtonItem)item).Name == listPermisos[i].dsc_menu)
                            {
                                ((BarButtonItem)item).Enabled = true;
                            }
                        }
                    }
                }
            }
        }

        private void btnListadoUsuario_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaUsuarios";
            if (Application.OpenForms["frmListadoUsuario"] != null)
            {
                Application.OpenForms["frmListadoUsuario"].Activate();
            }
            else
            {
                frmListadoUsuario frm = new frmListadoUsuario();
                frm.user = user;
                frm.MdiParent = this;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.Show();
            }
        }

        private void btnOpcionesSistema_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaOpcionesSistema";
            if (Application.OpenForms["frmOpcionesSistema"] != null)
            {
                Application.OpenForms["frmOpcionesSistema"].Activate();
            }
            else
            {
                frmOpcionesSistema frm = new frmOpcionesSistema();
                frm.user = user;
                frm.eGlobal = eGlobal;
                frm.MdiParent = this;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.Show();
            }
        }

        private void btnAsignacionPermiso_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaPermisos";
            if (Application.OpenForms["frmAsignacionPermiso"] != null)
            {
                Application.OpenForms["frmAsignacionPermiso"].Activate();
            }
            else
            {
                frmAsignacionPermiso frm = new frmAsignacionPermiso();
                frm.user = user;
                frm.eGlobal = eGlobal;
                frm.MdiParent = this;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.Show();
            }
        }

        private void btnFacturaProveedor_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Facturas Proveedor";
            frmListadoFacturaProveedor frm = new frmListadoFacturaProveedor();
            if (Application.OpenForms["frmListadoFacturaProveedor"] != null)
            {
                Application.OpenForms["frmListadoFacturaProveedor"].Activate();
            }
            else
            {
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.MdiParent = this;
                frm.Show();
            }
        }
        
        private void btnResumenCuentasxPagar_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Resumen Cuentas por Cobrar";
            if (Application.OpenForms["frmResumenCuentasCobrar"] != null)
            {
                Application.OpenForms["frmResumenCuentasCobrar"].Activate();
            }
            else
            {
                frmResumenCuentasPagar frm = new frmResumenCuentasPagar();
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnUndNegocioTipoGastoCostoEmpresa_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmMantUnidades_Negocio"] != null)
            {
                Application.OpenForms["frmMantUnidades_Negocio"].Activate();
            }
            else
            {
                frmMantUnidades_Negocio frm = new frmMantUnidades_Negocio();
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnTipoCambio_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmMantTipoCambio"] != null)
            {
                Application.OpenForms["frmMantTipoCambio"].Activate();
            }
            else
            {
                frmMantTipoCambio frm = new frmMantTipoCambio();
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.ShowDialog();
            }
        }

        private void btnProgramacionPagos_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmProgramacionPagos"] != null)
            {
                Application.OpenForms["frmProgramacionPagos"].Activate();
            }
            else
            {
                frmProgramacionPagos frm = new frmProgramacionPagos();
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnTipoGastoCosto_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmMantTipoGastoCosto"] != null)
            {
                Application.OpenForms["frmMantTipoGastoCosto"].Activate();
            }
            else
            {
                frmMantTipoGastoCosto frm = new frmMantTipoGastoCosto();
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                //frm.MdiParent = this;
                frm.ShowDialog();
            }
        }

        private void btnResumenCuentasxPagarSemanal_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Resumen Cuentas por Cobrar Semanal";
            if (Application.OpenForms["frmResumenCuentasCobrarSemanal"] != null)
            {
                Application.OpenForms["frmResumenCuentasCobrarSemanal"].Activate();
            }
            else
            {
                frmResumenCuentasPagarSemanal frm = new frmResumenCuentasPagarSemanal();
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnResumenPresupuestoEjecucion_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmResumenPresupuestoEjecucion"] != null)
            {
                Application.OpenForms["frmResumenPresupuestoEjecucion"].Activate();
            }
            else
            {
                frmResumenPresupuestoEjecucion frm = new frmResumenPresupuestoEjecucion();
                frm.user = user;
                frm.MdiParent = this;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.Show();
            }
        }

        private void btnEliminarExportados_ItemClick(object sender, ItemClickEventArgs e)
        {
            //OBTENEMOS LA RUTA DONDE ESTAN LOS ARCHIVOS DESCARGADOS
            var carpeta = ConfigurationManager.AppSettings["RutaArchivosLocalExportar"].ToString();
            DirectoryInfo source = new DirectoryInfo(carpeta);
            FileInfo[] filesToCopy = source.GetFiles();
            foreach (FileInfo oFile in filesToCopy)
            {
                oFile.Delete();
            }
            MessageBox.Show("Se procedió a eliminar los archivos exportados del sistema", "Eliminar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCrearTipoServicioProducto_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmAgregarTipoSubTipo frm = new frmAgregarTipoSubTipo();
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.MiAccion = AgregarTipoSubTipo.Tipo;
                frm.user = user;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCrearSubTipoServicioSubProducto_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmMantTipoSubTipo frm = new frmMantTipoSubTipo();
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                //frm.MiAccion = AgregarTipoSubTipo.SubTipo;
                frm.user = user;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultaProductosSunat_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaProductosSUNAT";
            if (Application.OpenForms["frmListaProductosSunat"] != null)
            {
                Application.OpenForms["frmListaProductosSunat"].Activate();
            }
            else
            {
                frmListaProductosSunat frm = new frmListaProductosSunat();
                frm.user = user;
                //frm.MdiParent = this;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.ShowDialog();
            }
        }

        private void btnCrearProductos_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "CrearProductos";
            if (Application.OpenForms["frmMantProductos"] != null)
            {
                Application.OpenForms["frmMantProductos"].Activate();
            }
            else
            {
                frmMantProductos frm = new frmMantProductos();
                frm.user = user;
                //frm.MdiParent = this;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.ShowDialog();
            }
        }

        private void btnListaPreciosProducto_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaPrecios-Producto";
            if (Application.OpenForms["frmListadoProductoPrecios"] != null)
            {
                Application.OpenForms["frmListadoProductoPrecios"].Activate();
            }
            else
            {
                frmListadoProductoPrecios frm = new frmListadoProductoPrecios();
                frm.user = user;
                frm.MdiParent = this;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.Show();
            }
        }

        private void btnInventarioAlmacen_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "InventarioAlmacen";
            if (Application.OpenForms["frmListaInventarioAlmacen"] != null)
            {
                Application.OpenForms["frmListaInventarioAlmacen"].Activate();
            }
            else
            {
                frmListaInventarioAlmacen frm = new frmListaInventarioAlmacen();
                frm.user = user;
                frm.MdiParent = this;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.Show();
            }
        }

        private void btnListadoRequerimientos_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaRequerimientos";
            frmListadoRequerimientos frm = new frmListadoRequerimientos();
            if (Application.OpenForms["frmListadoRequerimientos"] != null)
            {
                Application.OpenForms["frmListadoRequerimientos"].Activate();
            }
            else
            {
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnListadoOrdenesCompra_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaOrdenesCompra";
            frmListadoOrdenesCompra frm = new frmListadoOrdenesCompra();
            if (Application.OpenForms["frmListadoOrdenesCompra"] != null)
            {
                Application.OpenForms["frmListadoOrdenesCompra"].Activate();
            }
            else
            {
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnListadoOrdenesServicio_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Listado de Ordenes de Servicio";
            frmListadoOrdenesServicio frm = new frmListadoOrdenesServicio();
            if (Application.OpenForms["frmListadoOrdenesServicio"] != null)
            {
                Application.OpenForms["frmListadoOrdenesServicio"].Activate();
            }
            else
            {
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnNuevoReq_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Mantenimiento Requerimiento Analisis";
            frmMantRequerimientoAnalisis frm = new frmMantRequerimientoAnalisis();
            if (Application.OpenForms["frmMantRequerimientoAnalisis"] != null)
            {
                Application.OpenForms["frmMantRequerimientoAnalisis"].Activate();
            }
            else
            {
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.ShowDialog();
            }
        }

        private void btnNuevoAnalisis_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Mantenimiento Analisis";
            frmMantAnalisisServicio frm = new frmMantAnalisisServicio();
            if (Application.OpenForms["frmMantAnalisisServicio"] != null)
            {
                Application.OpenForms["frmMantAnalisisServicio"].Activate();
            }
            else
            {
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.WindowState = FormWindowState.Maximized;
                frm.ShowDialog();
            }
        }

        private void btnListadoAnalisis_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Listado de Analisis";
            frmListadoAnalisisServicio frm = new frmListadoAnalisisServicio();
            if (Application.OpenForms["frmListadoAnalisisServicio"] != null)
            {
                Application.OpenForms["frmListadoAnalisisServicio"].Activate();
            }
            else
            {
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnHistorialVersiones_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "HistorialVersiones";
            if (Application.OpenForms["frmHistorialVersiones"] != null)
            {
                Application.OpenForms["frmHistorialVersiones"].Activate();
            }
            else
            {
                frmVersionesAnalisis frm = new frmVersionesAnalisis();
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.ShowDialog();
            }
        }

        private void ribbon_Merge(object sender, DevExpress.XtraBars.Ribbon.RibbonMergeEventArgs e)
        {
            e.MergeOwner.SelectedPage = e.MergeOwner.MergedPages.GetPageByName(e.MergedChild.SelectedPage.Name);
        }

        private void btnListadoCliente_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaClientes";
            if (Application.OpenForms["frmListadoClientes"] != null)
            {
                Application.OpenForms["frmListadoClientes"].Activate();
            }
            else
            {
                frmListadoClientes frm = new frmListadoClientes();
                frm.user = user;
                frm.MdiParent = this;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.Show();
            }
        }
        
        private void btnRegistroCliente_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Clientes";
            if (Application.OpenForms["frmMantCliente"] != null)
            {
                Application.OpenForms["frmMantCliente"].Activate();
            }
            else
            {
                frmMantCliente frm = new frmMantCliente();
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.ShowDialog();
            }
        }

        private void btnCambiarContraseña_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmCambiarContraseña frm = new frmCambiarContraseña();
            frm.user = user;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.Show();
        }
        
        private void btnAcercaDeSistema_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmAcercaSistema frm = new frmAcercaSistema();
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.ShowDialog();
        }
    }
}