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
using DevExpress.Utils.Drawing;
using DevExpress.XtraSplashScreen;
using BE_Servicios;
using BL_Servicios;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using DevExpress.Images;
using DevExpress.XtraNavBar;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;

namespace UI_Servicios.Clientes_Y_Proveedores.Clientes
{
    public partial class frmListadoClientes : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public eUsuario user = new eUsuario();
        blEncrypta blEncryp = new blEncrypta();
        blClientes blCli = new blClientes();
        blGlobales blGlobal = new blGlobales();
        blAnalisisServicio blAns = new blAnalisisServicio();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        bool Buscar = false;

        public frmListadoClientes()
        {
            InitializeComponent();
        }

        private void frmListadoClientes_Load(object sender, EventArgs e)
        {
            HabilitarBotones();
            Inicializar();
            btnBuscar.Appearance.BackColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
        }
        private void Inicializar()
        {
            cargarCheckEdit();
            CargarOpcionesMenu();
            InitTreeList();
            CargarListado("TODOS", "");
            Buscar = true;
        }

        private void cargarCheckEdit()
        {
            try
            {
                List<eEmpresa> empresas = blAns.ListarGeneral<eEmpresa>("EmpresasxUsuario", usuario: user.cod_usuario);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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
            TreeList_empresas.Nodes[1].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
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

            List<eCliente> clientes = blAns.ListarGeneral<eCliente>("Categorias");
            _options.Add(new Option() { ParentID = "0", ID = "2", Name = "CATEGORIA", Checked = true });
     
            foreach (eCliente obj in clientes)
            {
                _options.Add(new Option() { ParentID = "2", ID = obj.cod_categoria, Name = obj.dsc_categoria, Checked = true });
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

        private void HabilitarBotones()
        {
            blSistema blSist = new blSistema();
            List<eVentana> listPermisos = blSist.ListarMenuxUsuario<eVentana>(user.cod_usuario, this.Name);

            if (listPermisos.Count > 0)
            {
                grupoEdicion.Enabled = listPermisos[0].flg_escritura;
            }
        }

        internal void CargarOpcionesMenu()
        {
            List<eCliente> ListCliente = new List<eCliente>();
            ListCliente = blCli.ListarOpcionesMenu<eCliente>(1);
            Image imgTipoCliLarge = ImageResourceCache.Default.GetImage("images/business%20objects/bodepartment_32x32.png");
            Image imgTipoCliSmall = ImageResourceCache.Default.GetImage("images/business%20objects/bodepartment_16x16.png");

            ListCliente = blCli.ListarOpcionesMenu<eCliente>(2);
            Image imgCategCliLarge = ImageResourceCache.Default.GetImage("images/richedit/differentoddevenpages_32x32.png");
            Image imgCategCliSmall = ImageResourceCache.Default.GetImage("images/richedit/differentoddevenpages_16x16.png");


            ListCliente = blCli.ListarOpcionesMenu<eCliente>(4);
            Image imgCalifCliLarge = ImageResourceCache.Default.GetImage("images/filter%20elements/checkbuttons_32x32.png");
            Image imgCalifCliSmall = ImageResourceCache.Default.GetImage("images/filter%20elements/checkbuttons_16x16.png");

            List<eCliente_Empresas> ListClienteEmp = blCli.ListarOpcionesMenu<eCliente_Empresas>(36);
            Image imgEmpresaLarge = ImageResourceCache.Default.GetImage("images/navigation/home_32x32.png");
            Image imgEmpresaSmall = ImageResourceCache.Default.GetImage("images/navigation/home_16x16.png");

            //NavTipoCont.SelectedLinkIndex = 0;
        }
        private void NavDetalle_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
            CargarListado(e.Link.Group.Caption, e.Link.Item.Tag.ToString());
            SplashScreenManager.CloseForm();
        }
        
        public void CargarListado(string NombreGrupo, string Codigo)
        {
            try
            {
                string empresas = "", categorias = ""; 
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode n in TreeList_empresas.GetAllCheckedNodes())
                {
                    if (n.GetValue("ID").ToString().Length > 1 && n.GetValue("ID").ToString().Substring(0, 2) == "CT") categorias = n.GetValue("ID").ToString() + "," + categorias;
                    if (n.GetValue("ID").ToString().Length > 1 && n.GetValue("ID").ToString().Substring(0, 2) != "CT") empresas = n.GetValue("ID").ToString() + "," + empresas;
                }

                string cod_tipo_cliente = "", cod_categoria = "", cod_tipo_documento = "", cod_calificacion = "", cod_tipo_contacto = "", cod_empresa = "";
                switch (NombreGrupo)
                {
                    case "Por Tipo Cliente": cod_tipo_cliente = Codigo; break; 
                    case "Por Categoría Cliente": cod_categoria = Codigo; break;
                    case "Por Tipo Documento": cod_tipo_documento = Codigo; break;
                    case "Por Calificación": cod_calificacion = Codigo; break;
                    case "Por Tipo Contacto": cod_tipo_contacto = Codigo; break;
                    case "Por Empresa": cod_empresa = Codigo; break;
                }

                //blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                List<eCliente> ListCliente = new List<eCliente>();
                ListCliente = blCli.ListarClientes<eCliente>(18, cod_empresa_multiple: empresas, cod_categoria_multiple: categorias);
                /*bsListaClientes.DataSource = null; */bsListaClientes.DataSource = ListCliente;
                //SplashScreenManager.CloseForm();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvListaClientes_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        internal void frmListadoClientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                CargarListado("", "");
                SplashScreenManager.CloseForm();
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
                string archivo = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\Clientes" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                gvListaClientes.ExportToXlsx(archivo);
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
        private void btnImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvListaClientes.ShowPrintPreview();
        }

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmMantCliente frm = new frmMantCliente();
                frm.MiAccion = Cliente.Nuevo;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.user = user;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaClientes_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eCliente obj = gvListaClientes.GetFocusedRow() as eCliente;

                    frmMantCliente frm = new frmMantCliente(this);
                    frm.cod_cliente = obj.cod_cliente;
                    frm.MiAccion = Cliente.Editar;
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorEventRow = colorEventRow;
                    frm.colorFocus = colorFocus;
                    
                    
                    //CAMBIAR
                    //frm.cod_empresa = navBarControl1.SelectedLink.Item.Tag.ToString();
                    
                    
                    frm.user = user;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaClientes_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    eCliente obj = gvListaClientes.GetFocusedRow() as eCliente;
                
            //    frmMantCliente frm = new frmMantCliente(this);
            //    frm.cod_cliente = obj.cod_cliente;
            //    frm.MiAccion = Cliente.Editar;
            //    frm.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }
        
        private void gvListaClientes_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void TreeList_empresas_NodeChanged(object sender, NodeChangedEventArgs e)
        {
           
        }

        private void TreeList_empresas_RowClick(object sender, DevExpress.XtraTreeList.RowClickEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarListado("","");
        }

        private void btnActivar_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnInactivar_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnEliminar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eCliente eCli = gvListaClientes.GetFocusedRow() as eCliente;

                eCliente eCliVal = blCli.ValidacionEliminar<eCliente>(31, eCli.cod_cliente);
                if (eCliVal != null) { MessageBox.Show("No se puede eliminar el cliente ya que tiene comprobantess.", "Eliminar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                eCliVal = blCli.ValidacionEliminar<eCliente>(32, eCli.cod_cliente);
                if (eCliVal != null) { MessageBox.Show("No se puede eliminar el cliente ya que tiene pedidos.", "Eliminar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                eCliVal = blCli.ValidacionEliminar<eCliente>(33, eCli.cod_cliente);
                if (eCliVal != null) { MessageBox.Show("No se puede eliminar el cliente ya que tiene ventas.", "Eliminar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este cliente?", "Eliminar cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {

                    string result = blCli.Eliminar_Cliente(eCli.cod_cliente);
                    blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    CargarListado("", "");
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}