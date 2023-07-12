using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE_Servicios;
using BL_Servicios;
using DevExpress.XtraSplashScreen;
using DevExpress.Images;
using DevExpress.XtraNavBar;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraTreeList;

namespace UI_Servicios.Formularios.Logistica
{
    public partial class frmListadoProductoPrecios : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        blEncrypta blEncryp = new blEncrypta();
        blProveedores blProv = new blProveedores();
        blLogistica blLogis = new blLogistica();
        blAnalisisServicio blAns = new blAnalisisServicio();
        List<eProductos> listProductos = new List<eProductos>();
        List<eProductos.eProductosTarifas> listHistoricoTarifas = new List<eProductos.eProductosTarifas>();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        bool Buscar = false;
        string cod_empresa;

        public frmListadoProductoPrecios()
        {
            InitializeComponent();
        }

        private void frmListadoPreciosProducto_Load(object sender, EventArgs e)
        {
            Inicializar();
            InitTreeList();
            btnBuscar.Appearance.BackColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
        }

        private void Inicializar()
        {
            CargarListado();
            Buscar = true;
            List<eEmpresa> list = blAns.ListarGeneral<eEmpresa>("EmpresasxUsuario", usuario: user.cod_usuario);
            cod_empresa = list[0].cod_empresa;
        }

        void OnNodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.FontSizeDelta += 1;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
            if (e.Node.Level == 1 && e.Node.Nodes.Count > 0)
                e.Appearance.FontStyleDelta = FontStyle.Bold;
        }
        void OnBeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            e.CanFocus = false;
        }

        void InitTreeList()
        {
            TreeList_empresas.Appearance.Row.BackColor = Color.Transparent;
            TreeList_empresas.Appearance.Empty.BackColor = Color.Transparent;
            TreeList_empresas.BackColor = Color.Transparent;
            TreeList_empresas.CheckBoxFieldName = "Checked";
            TreeList_empresas.TreeViewFieldName = "Name";
            TreeList_empresas.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            TreeList_empresas.OptionsBehavior.Editable = false;
            TreeList_empresas.OptionsBehavior.ReadOnly = true;
            TreeList_empresas.OptionsBehavior.AllowRecursiveNodeChecking = true;
            TreeList_empresas.NodeCellStyle += OnNodeCellStyle;
            TreeList_empresas.BeforeFocusNode += OnBeforeFocusNode;
            var dataSource = GenerateDataSource();
            TreeList_empresas.DataSource = dataSource;
            TreeList_empresas.ForceInitialize();
            TreeList_empresas.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
            TreeList_empresas.Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
            TreeList_empresas.ExpandAll();
        }

        BindingList<Option> GenerateDataSource()
        {
            BindingList<Option> _options = new BindingList<Option>();

            List<eEmpresa> empresas = blAns.ListarGeneral<eEmpresa>("EmpresasxUsuario", usuario: user.cod_usuario);
            _options.Add(new Option() { ParentID = "0", ID = "1", Name = "EMPRESA", Checked = true });
            foreach (eEmpresa obj in empresas)
            {
                _options.Add(new Option() { ParentID = "1", ID = obj.cod_empresa, Name = obj.dsc_empresa, Checked = true });
            }

            return _options;
        }

        class Option : INotifyPropertyChanged
        {
            public string ParentID { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            bool? checkedCore = false;

            public event PropertyChangedEventHandler PropertyChanged;

            public bool? Checked
            {
                get { return checkedCore; }
                set
                {
                    if (checkedCore == value)
                        return;
                    checkedCore = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Checked"));
                }
            }
        }

        private void NavDetalle_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
            CargarListado();
            SplashScreenManager.CloseForm();
        }

        public void CargarListado()
        {
            try
            {
                string empresas = "";
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode n in TreeList_empresas.GetAllCheckedNodes())
                {
                    empresas = empresas + "," + n.GetValue("ID").ToString();
                }

                listProductos.Clear();
                listProductos = blLogis.Obtener_ListadosProductos<eProductos>(31, cod_empresa_multiple: empresas);
                bsListadoProductos.DataSource = listProductos;
                gvListadoProductos.RefreshData();

                listHistoricoTarifas.Clear();
                listHistoricoTarifas = blLogis.Obtener_ListadosProductos<eProductos.eProductosTarifas>(32, cod_empresa_multiple: empresas);
                bsListadoProductosTarifa.DataSource = listHistoricoTarifas;
                gvListadoProductosTarifa.RefreshData();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void navBarControl1_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {

        }

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMantProductos frm = new frmMantProductos();
            frm.user = user;


            frm.cod_empresa = cod_empresa; 
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.ShowDialog();
            
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");
            CargarListado();
            SplashScreenManager.CloseForm();
        }

        private void gvListadoProductosTarifa_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoProductosTarifa_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoProductosTarifa_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eProductos.eProductosTarifas obj = gvListadoProductosTarifa.GetFocusedRow() as eProductos.eProductosTarifas;

                    frmMantProductoPrecio frm = new frmMantProductoPrecio(this);
                    frm.MiAccion = TarifaProducto.Editar;
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorEventRow = colorEventRow;
                    frm.colorFocus = colorFocus;
                    frm.cod_tipo_servicio = obj.cod_tipo_servicio;
                    frm.cod_subtipo_servicio = obj.cod_subtipo_servicio;
                    frm.cod_producto = obj.cod_producto;
                    frm.dsc_ruc = obj.dsc_ruc;
                    frm.cod_proveedor = obj.cod_proveedor;
                    frm.dsc_proveedor = obj.dsc_proveedor;
                    frm.dsc_producto = obj.dsc_producto;
                    frm.fch_inicio = obj.fch_inicio;
                    frm.imp_costo = obj.imp_costo;
                    frm.user = user;
                    frm.ShowDialog();

                    blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");
                    CargarListado();
                    SplashScreenManager.CloseForm();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmListadoProductoPrecios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");
                CargarListado();
                SplashScreenManager.CloseForm();
            }
        }

        private void btnClonar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eProductos obj = gvListadoProductos.GetFocusedRow() as eProductos;

                frmMantProductos frm = new frmMantProductos(this);
                frm.MiAccion = Producto.Clonar;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.cod_tipo_servicio = obj.cod_tipo_servicio;
                frm.cod_subtipo_servicio = obj.cod_subtipo_servicio;
                frm.cod_producto = obj.cod_producto;
                //frm.cod_productoREF = obj.cod_productoREF;
                frm.user = user;

                frm.cod_empresa = cod_empresa;
                frm.ShowDialog();
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");
                CargarListado();
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportarExcel();
        }

        private void ExportarExcel()
        {
            try
            {
                string carpeta = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\ListaProductoPrecios" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";

                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                gvListadoProductos.ExportToXlsx(archivo);
                if (MessageBox.Show("Excel exportado en la ruta " + archivo + Environment.NewLine + "¿Desea abrir el archivo?", "Exportar Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(archivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarListado();
        }

        private void gvListadoProductos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoProductos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoProductos_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eProductos obj = gvListadoProductos.GetRow(e.RowHandle) as eProductos;
                    frmMantProductos frm = new frmMantProductos(this);
                    frm.MiAccion = Producto.Editar;
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorEventRow = colorEventRow;
                    frm.colorFocus = colorFocus;
                    frm.cod_tipo_servicio = obj.cod_tipo_servicio;
                    frm.cod_subtipo_servicio = obj.cod_subtipo_servicio;
                    frm.cod_producto = obj.cod_producto;
                    //frm.cod_productoREF = obj.cod_productoREF;
                    frm.user = user;
                    
                    frm.cod_empresa = cod_empresa;
                    frm.ShowDialog();
                    blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");
                    CargarListado();
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}