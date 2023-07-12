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
using BL_Servicios;
using BE_Servicios;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Columns;
using UI_Servicios.Formularios.Clientes_Y_Proveedores.Clientes;
using DevExpress.XtraLayout.Utils;
using UI_Servicios.Formularios.Clientes_Y_Proveedores.Proveedores;
using DevExpress.XtraGrid;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Grid;
using UI_Servicios.Clientes_Y_Proveedores.Clientes;

namespace UI_Servicios.Formularios.Shared
{
    public partial class frmBusquedas : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        public int BotonAgregarVisible = 0;  //0 se hace visible la botonera de agregar; 1 se visualiza
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        internal enum MiEntidad
        {
            Cliente = 1,
            ContactoxCliente = 2,
            Trabajador = 3,
            Proveedor = 4,
            ContactosCliente = 5,
            ContactosProveedor= 6,
            Servicios = 7,
            SubTipoServicio = 8,
            ProductosProyecto = 9,
            ProductoCosto = 10,
            ProductoReceta = 11,
            Proyecto = 12,
            MarcaProducto = 13,
            ProveedorMultiple = 14,
            ProveedorTipoServicio = 15,
            Productos = 16,
            OrdenesCompra = 17,
            ClienteEmpresa = 18,
            Requerimiento = 19
        }

        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string rucCliente { get; set; }
        public string cod_condicion1 { get; set; }
        public string dsc_condicion1 { get; set; }
        public string cod_condicion2 { get; set; }
        public string dsc_condicion2 { get; set; }
        public string ruc { get; set; }
        public DateTime fch_generica { get; set; }

        internal MiEntidad entidad = MiEntidad.Cliente;
        public string filtro = "";
        public string cod_cliente = "";
        public string cod_proveedor = "";
        public string filtroRUC = "NO";
        public string cod_empresa = "";
        public string cod_sede_empresa = "";
        public string cod_almacen = "";
        public string cod_proyecto = "";
        public string cod_tipo_servicio = "";
        public string flg_transportista = "";

        blSistema blSistema = new blSistema();
        blClientes blCli = new blClientes();
        blProveedores blProv = new blProveedores();
        blTrabajador blTrab = new blTrabajador();
        blFactura blFac = new blFactura();
        blLogistica blLogis = new blLogistica();
        List<eCliente> ListCliente = new List<eCliente>();
        List<eProveedor> ListProveedor = new List<eProveedor>();
        List<eTrabajador> ListTrabajador = new List<eTrabajador>();
        List<eProveedor_Servicios> ListServicios = new List<eProveedor_Servicios>();
        List<eProyecto> ListProyecto = new List<eProyecto>();
        List<eProyecto.eProyecto_SubTipo_Servicio> ListSubTipoServicio = new List<eProyecto.eProyecto_SubTipo_Servicio>();
        List<eProyecto.eProyecto_Producto> ListProductos = new List<eProyecto.eProyecto_Producto>();
        List<eProyecto.eProyecto_Producto_Costos> ListProductoCosto = new List<eProyecto.eProyecto_Producto_Costos>();
        List<eProyecto.eProyecto_Producto_Receta> ListProductoReceta = new List<eProyecto.eProyecto_Producto_Receta>();
        List<eProveedor_Marca> ListMarca = new List<eProveedor_Marca>();
        List<eOrdenCompra_Servicio> ListOrdenCompra = new List<eOrdenCompra_Servicio>();
        List<eRequerimiento> ListRequerimiento = new List<eRequerimiento>();
        public List<eProveedor> ListProv = new List<eProveedor>();
        public List<eProyecto.eProyecto_Producto> ListProd = new List<eProyecto.eProyecto_Producto>();

        public frmBusquedas()
        {
            InitializeComponent();
        }
        private void frmBusquedas_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        public void Inicializar()
        {
            LlenarDataGrid();
        }

        public void LlenarDataGrid()
        {

            try
            {
                switch (entidad)
                {
                    case MiEntidad.Cliente:
                        ListCliente = blSistema.ListarEntidad<eCliente>(1, "");
                        gcAyuda.DataSource = ListCliente;

                        this.Text = "Busqueda de Clientes";

                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "cod_cliente" || col.FieldName == "dsc_cliente" || col.FieldName == "dsc_tipo_documento" || col.FieldName == "dsc_documento") { col.Visible = true; }
                        }
                        gvAyuda.Columns["cod_cliente"].Width = 50;
                        gvAyuda.Columns["dsc_cliente"].Width = 170;
                        gvAyuda.Columns["dsc_tipo_documento"].Width = 45;
                        gvAyuda.Columns["dsc_documento"].Width = 70;


                        gvAyuda.Columns["cod_cliente"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_cliente"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_tipo_documento"].VisibleIndex = 2;
                        gvAyuda.Columns["dsc_documento"].VisibleIndex = 3;

                        gvAyuda.Columns["cod_cliente"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["dsc_documento"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["cod_cliente"].Caption = "Còdigo";
                        gvAyuda.Columns["dsc_cliente"].Caption = "Cliente";
                        gvAyuda.Columns["dsc_tipo_documento"].Caption = "Tipo Doc";
                        gvAyuda.Columns["dsc_documento"].Caption = "Documento";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_cliente"];
                        gvAyuda.SetAutoFilterValue(gvAyuda.Columns["dsc_cliente"], filtro, DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains);

                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.ContactoxCliente:
                        if (BotonAgregarVisible == 1)
                        {
                            this.layoutAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            this.layoutEspacioAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        }
                        btnAgregar.Text = "Agregar Contacto";

                        ListCliente = blSistema.ListarEntidad<eCliente>(2, cod_condicion1, cod_condicion2);
                        gcAyuda.DataSource = ListCliente;

                        this.Text = "Busqueda de Contactos del Cliente";

                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "dsc_cadena_direccion" || col.FieldName == "dsc_cliente" || col.FieldName == "dsc_contacto") { col.Visible = true; }
                        }

                        gvAyuda.Columns["dsc_contacto"].Width = 100;
                        gvAyuda.Columns["dsc_cadena_direccion"].Width = 120;
                        gvAyuda.Columns["dsc_cliente"].Width = 120;


                        gvAyuda.Columns["dsc_contacto"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_cadena_direccion"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_cliente"].VisibleIndex = 2;

                        //gvAyuda.Columns["cod_tipo_cliente"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


                        gvAyuda.Columns["dsc_contacto"].Caption = "Contacto";
                        gvAyuda.Columns["dsc_cadena_direccion"].Caption = "Dirección";
                        gvAyuda.Columns["dsc_cliente"].Caption = "Cliente";
                        break;

                    case MiEntidad.Proveedor:
                        layoutControlItem2.Visibility = LayoutVisibility.Always;
                        ListProveedor = blSistema.ListarEntidad<eProveedor>(3, "", "", flg_transportista, cod_empresa);
                        gcAyuda.DataSource = ListProveedor;

                        this.Text = "Busqueda de Proveedores";

                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "num_documento" || /*col.FieldName == "cod_proveedor" ||*/ col.FieldName == "dsc_razon_social" || col.FieldName == "dsc_razon_comercial") { col.Visible = true; }
                        }
                        gvAyuda.Columns["num_documento"].Width = 50;
                        gvAyuda.Columns["dsc_razon_social"].Width = 170;
                        gvAyuda.Columns["dsc_razon_comercial"].Width = 170;

                        gvAyuda.Columns["num_documento"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_razon_social"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_razon_comercial"].VisibleIndex = 2;

                        gvAyuda.Columns["num_documento"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        //gvAyuda.Columns["dsc_razon_comercial"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["num_documento"].Caption = "N° documento";
                        gvAyuda.Columns["dsc_razon_social"].Caption = "Razón Social";
                        gvAyuda.Columns["dsc_razon_comercial"].Caption = "Nombre Comercial";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        if (filtroRUC == "SI")
                        {
                            gvAyuda.FocusedColumn = gvAyuda.Columns["num_documento"];
                            gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["num_documento"], filtro);
                        }
                        else
                        {
                            gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_razon_social"];
                            gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_razon_social"], filtro);
                        }
                        
                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.ContactosCliente:
                        List<eCliente_Contactos> ListClienteContacto = blCli.ListarContactos<eCliente_Contactos>(7, cod_cliente, 0);
                        gcAyuda.DataSource = ListClienteContacto;

                        this.Text = "Busqueda de Contactos";
                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "cod_contacto" || col.FieldName == "dsc_nombre_completo" || col.FieldName == "dsc_cargo") { col.Visible = true; }
                        }
                        gvAyuda.Columns["cod_contacto"].Width = 50;
                        gvAyuda.Columns["dsc_nombre_completo"].Width = 170;
                        gvAyuda.Columns["dsc_cargo"].Width = 70;

                        gvAyuda.Columns["cod_contacto"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_nombre_completo"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_cargo"].VisibleIndex = 2;

                        gvAyuda.Columns["cod_contacto"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["dsc_cargo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["cod_contacto"].Caption = "Código";
                        gvAyuda.Columns["dsc_nombre_completo"].Caption = "Contacto";
                        gvAyuda.Columns["dsc_cargo"].Caption = "Cargo";

                        break;

                    case MiEntidad.ContactosProveedor:
                        List<eProveedor_Contactos> ListProveedorContacto = blSistema.ListarEntidad<eProveedor_Contactos>(5, cod_condicion1);
                        gcAyuda.DataSource = ListProveedorContacto;
                        //dsc_cargo es dsc_proveedor, solo que econtacto no fue declarado
                        this.Text = "Busqueda de Contactos del Proveedor";
                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "dsc_nombre" || col.FieldName == "dsc_cargo") { col.Visible = true; }
                        }
                       
                        gvAyuda.Columns["dsc_nombre"].Width = 170;
                        gvAyuda.Columns["dsc_cargo"].Width = 170;

                      
                        gvAyuda.Columns["dsc_nombre"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_cargo"].VisibleIndex = 1;

                      
                        gvAyuda.Columns["dsc_nombre"].Caption = "Contacto";
                        gvAyuda.Columns["dsc_cargo"].Caption = "Proveedor";

                        break;

                    case MiEntidad.Servicios:
                        if (BotonAgregarVisible == 1)
                        {
                            this.layoutAgregar.Visibility = LayoutVisibility.Always;
                            this.layoutEspacioAgregar.Visibility = LayoutVisibility.Always;
                        }
                        ListServicios = blSistema.ListarEntidad<eProveedor_Servicios>(5, "");
                        gcAyuda.DataSource = ListServicios;

                        this.Text = "Vincular nuevo servicio a proveedor";
                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "cod_tipo_servicio" || col.FieldName == "dsc_tipo_servicio" || col.FieldName == "flg_activo") { col.Visible = true; }
                        }
                        gvAyuda.Columns["cod_tipo_servicio"].Width = 50;
                        gvAyuda.Columns["dsc_tipo_servicio"].Width = 200;
                        gvAyuda.Columns["flg_activo"].Width = 70;

                        gvAyuda.Columns["cod_tipo_servicio"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_tipo_servicio"].VisibleIndex = 1;
                        gvAyuda.Columns["flg_activo"].VisibleIndex = 2;

                        gvAyuda.Columns["cod_tipo_servicio"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["flg_activo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["cod_tipo_servicio"].Caption = "Cod.";
                        gvAyuda.Columns["dsc_tipo_servicio"].Caption = "Servicio";
                        gvAyuda.Columns["flg_activo"].Caption = "Activo";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_tipo_servicio"];
                        gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_tipo_servicio"], filtro);

                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.Trabajador:
                        layoutControlItem2.Visibility = LayoutVisibility.Never;
                        layoutAgregar.Visibility = LayoutVisibility.Never;
                        ListTrabajador = blTrab.ListarTrabajadores<eTrabajador>(1, "", cod_empresa);
                        gcAyuda.DataSource = ListTrabajador;

                        this.Text = "Busqueda de Trabajadores";
                         
                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "cod_trabajador" || col.FieldName == "dsc_nombres_completos") { col.Visible = true; }
                        }
                        gvAyuda.Columns["cod_trabajador"].Width = 50;
                        gvAyuda.Columns["dsc_nombres_completos"].Width = 200;
                        gvAyuda.Columns["dsc_empresa"].Width = 100;
                        
                        gvAyuda.Columns["cod_trabajador"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_nombres_completos"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_empresa"].VisibleIndex = 2;

                        gvAyuda.Columns["cod_trabajador"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["cod_trabajador"].Caption = "Código";
                        gvAyuda.Columns["dsc_nombres_completos"].Caption = "Nombre Completo";
                        gvAyuda.Columns["dsc_empresa"].Caption = "Empresa";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_nombres_completos"];
                        gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_nombres_completos"], filtro);

                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.SubTipoServicio:
                        ListSubTipoServicio = blSistema.Obtener_DatosProductos<eProyecto.eProyecto_SubTipo_Servicio>(55, "");
                        gcAyuda.DataSource = ListSubTipoServicio;

                        this.Text = "Listado de Tipos de Servicio";
                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "cod_tipo_servicio" || col.FieldName == "dsc_tipo_servicio" || col.FieldName == "cod_subtipo_servicio" ||
                                col.FieldName == "dsc_subtipo_servicio" || col.FieldName == "flg_activo"/* || col.FieldName == "ctd_volumen_m3"*/) { col.Visible = true; }
                        }
                        gvAyuda.Columns["cod_tipo_servicio"].Width = 50;
                        gvAyuda.Columns["dsc_tipo_servicio"].Width = 200;
                        gvAyuda.Columns["cod_subtipo_servicio"].Width = 50;
                        gvAyuda.Columns["dsc_subtipo_servicio"].Width = 200;
                        gvAyuda.Columns["flg_activo"].Width = 70;

                        gvAyuda.Columns["cod_tipo_servicio"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_tipo_servicio"].VisibleIndex = 1;
                        gvAyuda.Columns["cod_subtipo_servicio"].VisibleIndex = 2;
                        gvAyuda.Columns["dsc_subtipo_servicio"].VisibleIndex = 3;
                        gvAyuda.Columns["flg_activo"].VisibleIndex = 4;

                        gvAyuda.Columns["cod_tipo_servicio"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["cod_subtipo_servicio"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["flg_activo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["cod_tipo_servicio"].Caption = "Cod. Tipo";
                        gvAyuda.Columns["dsc_tipo_servicio"].Caption = "Tipo Servicio";
                        gvAyuda.Columns["cod_subtipo_servicio"].Caption = "Cod. SubTipo";
                        gvAyuda.Columns["dsc_subtipo_servicio"].Caption = "SubTipo Servicio";
                        gvAyuda.Columns["flg_activo"].Caption = "Activo";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_subtipo_servicio"];
                        gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_subtipo_servicio"], filtro);

                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.ProductosProyecto:
                        if (BotonAgregarVisible == 1)
                        {
                            this.layoutAgregar.Visibility = LayoutVisibility.Always;
                            this.layoutEspacioAgregar.Visibility = LayoutVisibility.Always;
                        }
                        ListProductos = blSistema.Obtener_DatosProductos<eProyecto.eProyecto_Producto>(53, "");
                        gcAyuda.DataSource = ListProductos;

                        this.Text = "Listado de Productos";
                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (/*col.FieldName == "cod_tipo_servicio" || */col.FieldName == "dsc_tipo_servicio"/* || col.FieldName == "cod_subtipo_servicio" */||
                                col.FieldName == "dsc_subtipo_servicio"/* || col.FieldName == "cod_producto" */|| col.FieldName == "dsc_producto" || 
                                col.FieldName == "dsc_simbolo") { col.Visible = true; }
                        }
                        //gvAyuda.Columns["cod_tipo_servicio"].Width = 50;
                        gvAyuda.Columns["dsc_tipo_servicio"].Width = 200;
                        //gvAyuda.Columns["cod_subtipo_servicio"].Width = 50;
                        gvAyuda.Columns["dsc_subtipo_servicio"].Width = 200;
                        //gvAyuda.Columns["cod_producto"].Width = 50;
                        gvAyuda.Columns["dsc_producto"].Width = 300;
                        gvAyuda.Columns["dsc_simbolo"].Width = 50;

                        //gvAyuda.Columns["cod_tipo_servicio"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_tipo_servicio"].VisibleIndex = 1;
                        //gvAyuda.Columns["cod_subtipo_servicio"].VisibleIndex = 2;
                        gvAyuda.Columns["dsc_subtipo_servicio"].VisibleIndex = 3;
                        //gvAyuda.Columns["cod_producto"].VisibleIndex = 4;
                        gvAyuda.Columns["dsc_producto"].VisibleIndex = 5; 
                        gvAyuda.Columns["dsc_simbolo"].VisibleIndex = 6;
                        //gvAyuda.Columns["flg_activo"].VisibleIndex = 7;

                        //gvAyuda.Columns["cod_tipo_servicio"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        //gvAyuda.Columns["cod_subtipo_servicio"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        //gvAyuda.Columns["cod_producto"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["dsc_simbolo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        //gvAyuda.Columns["flg_activo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        //gvAyuda.Columns["cod_tipo_servicio"].Caption = "Cod. Tipo";
                        gvAyuda.Columns["dsc_tipo_servicio"].Caption = "Tipo Servicio";
                        //gvAyuda.Columns["cod_subtipo_servicio"].Caption = "Cod. SubTipo";
                        gvAyuda.Columns["dsc_subtipo_servicio"].Caption = "SubTipo Servicio";
                        //gvAyuda.Columns["cod_producto"].Caption = "Cod. Producto";
                        gvAyuda.Columns["dsc_producto"].Caption = "Producto";
                        gvAyuda.Columns["dsc_simbolo"].Caption = "Unds.";
                        //gvAyuda.Columns["flg_activo"].Caption = "Activo";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_producto"];
                        gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_producto"], filtro);

                        gvAyuda.OptionsSelection.MultiSelect = true;
                        gvAyuda.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                        gvAyuda.OptionsSelection.CheckBoxSelectorColumnWidth = 25;

                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.Proyecto:
                        ListProyecto = blSistema.Obtener_DatosProductos<eProyecto>(52, cod_empresa: cod_empresa);
                        gcAyuda.DataSource = ListProyecto;

                        this.Text = "Listado de Proyectos";
                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "cod_proyecto" || col.FieldName == "dsc_proyecto" || col.FieldName == "flg_activo") { col.Visible = true; }
                        }
                        gvAyuda.Columns["cod_proyecto"].Width = 50;
                        gvAyuda.Columns["dsc_proyecto"].Width = 200;
                        gvAyuda.Columns["flg_activo"].Width = 70;

                        gvAyuda.Columns["cod_proyecto"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_proyecto"].VisibleIndex = 1;
                        gvAyuda.Columns["flg_activo"].VisibleIndex = 2;

                        gvAyuda.Columns["cod_proyecto"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["flg_activo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["cod_proyecto"].Caption = "Cod. Proyecto";
                        gvAyuda.Columns["dsc_proyecto"].Caption = "Proyecto";
                        gvAyuda.Columns["flg_activo"].Caption = "Activo";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_proyecto"];
                        gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_proyecto"], filtro);

                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.ProductoCosto:
                        ListProductos = blSistema.Obtener_DatosProductos<eProyecto.eProyecto_Producto>(53, "");
                        gcAyuda.DataSource = ListProductos;

                        this.Text = "Listado de Productos";
                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "cod_tipo_servicio" || col.FieldName == "dsc_tipo_servicio" || col.FieldName == "cod_subtipo_servicio" ||
                                col.FieldName == "dsc_subtipo_servicio" || col.FieldName == "cod_producto" || col.FieldName == "dsc_producto" ||
                                col.FieldName == "fch_inicio" || col.FieldName == "fch_fin" || col.FieldName == "imp_costo" || col.FieldName == "dsc_observacion") { col.Visible = true; }
                        }
                        gvAyuda.Columns["cod_tipo_servicio"].Width = 50;
                        gvAyuda.Columns["dsc_tipo_servicio"].Width = 200;
                        gvAyuda.Columns["cod_subtipo_servicio"].Width = 50;
                        gvAyuda.Columns["dsc_subtipo_servicio"].Width = 200;
                        gvAyuda.Columns["cod_producto"].Width = 50;
                        gvAyuda.Columns["dsc_producto"].Width = 200;
                        gvAyuda.Columns["fch_inicio"].Width = 70;
                        gvAyuda.Columns["fch_fin"].Width = 70;
                        gvAyuda.Columns["imp_costo"].Width = 40;
                        gvAyuda.Columns["dsc_observacion"].Width = 70;

                        gvAyuda.Columns["cod_tipo_servicio"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_tipo_servicio"].VisibleIndex = 1;
                        gvAyuda.Columns["cod_subtipo_servicio"].VisibleIndex = 2;
                        gvAyuda.Columns["dsc_subtipo_servicio"].VisibleIndex = 3;
                        gvAyuda.Columns["cod_producto"].VisibleIndex = 4;
                        gvAyuda.Columns["dsc_producto"].VisibleIndex = 5;
                        gvAyuda.Columns["fch_inicio"].VisibleIndex = 6;
                        gvAyuda.Columns["fch_fin"].VisibleIndex = 7;
                        gvAyuda.Columns["imp_costo"].VisibleIndex = 8;
                        gvAyuda.Columns["dsc_observacion"].VisibleIndex = 9;

                        gvAyuda.Columns["cod_tipo_servicio"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["cod_subtipo_servicio"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["cod_producto"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["fch_inicio"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["fch_fin"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["imp_costo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["cod_tipo_servicio"].Caption = "Cod. Tipo";
                        gvAyuda.Columns["dsc_tipo_servicio"].Caption = "Tipo Servicio";
                        gvAyuda.Columns["cod_subtipo_servicio"].Caption = "Cod. SubTipo";
                        gvAyuda.Columns["dsc_subtipo_servicio"].Caption = "SubTipo Servicio";
                        gvAyuda.Columns["cod_producto"].Caption = "Cod. Producto";
                        gvAyuda.Columns["dsc_producto"].Caption = "Producto";
                        gvAyuda.Columns["fch_inicio"].Caption = "Fec. Inicio";
                        gvAyuda.Columns["fch_fin"].Caption = "Fec. Fin";
                        gvAyuda.Columns["imp_costo"].Caption = "Costo";
                        gvAyuda.Columns["dsc_observacion"].Caption = "Observación";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_producto"];
                        gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_producto"], filtro);

                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.ProductoReceta:
                        ListProductos = blSistema.Obtener_DatosProductos<eProyecto.eProyecto_Producto>(53, "");
                        gcAyuda.DataSource = ListProductos;

                        this.Text = "Listado de Productos";
                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "cod_tipo_servicio" || col.FieldName == "dsc_tipo_servicio" || col.FieldName == "cod_subtipo_servicio" ||
                                col.FieldName == "dsc_subtipo_servicio" || col.FieldName == "cod_producto" || col.FieldName == "dsc_producto" ||
                                col.FieldName == "cod_producto_item" || col.FieldName == "dsc_producto_item" || col.FieldName == "ctd_requerida" || 
                                col.FieldName == "dsc_observacion") { col.Visible = true; }
                        }
                        gvAyuda.Columns["cod_tipo_servicio"].Width = 50;
                        gvAyuda.Columns["dsc_tipo_servicio"].Width = 200;
                        gvAyuda.Columns["cod_subtipo_servicio"].Width = 50;
                        gvAyuda.Columns["dsc_subtipo_servicio"].Width = 200;
                        gvAyuda.Columns["cod_producto"].Width = 50;
                        gvAyuda.Columns["dsc_producto"].Width = 200;
                        gvAyuda.Columns["cod_producto_item"].Width = 50;
                        gvAyuda.Columns["dsc_producto_item"].Width = 200;
                        gvAyuda.Columns["ctd_requerida"].Width = 30;
                        gvAyuda.Columns["dsc_observacion"].Width = 70;

                        gvAyuda.Columns["cod_tipo_servicio"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_tipo_servicio"].VisibleIndex = 1;
                        gvAyuda.Columns["cod_subtipo_servicio"].VisibleIndex = 2;
                        gvAyuda.Columns["dsc_subtipo_servicio"].VisibleIndex = 3;
                        gvAyuda.Columns["cod_producto"].VisibleIndex = 4;
                        gvAyuda.Columns["dsc_producto"].VisibleIndex = 5;
                        gvAyuda.Columns["cod_producto_item"].VisibleIndex = 6;
                        gvAyuda.Columns["dsc_producto_item"].VisibleIndex = 7;
                        gvAyuda.Columns["ctd_requerida"].VisibleIndex = 8;
                        gvAyuda.Columns["dsc_observacion"].VisibleIndex = 9;

                        gvAyuda.Columns["cod_tipo_servicio"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["cod_subtipo_servicio"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["cod_producto"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["cod_producto_item"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["ctd_requerida"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["cod_tipo_servicio"].Caption = "Cod. Tipo";
                        gvAyuda.Columns["dsc_tipo_servicio"].Caption = "Tipo Servicio";
                        gvAyuda.Columns["cod_subtipo_servicio"].Caption = "Cod. SubTipo";
                        gvAyuda.Columns["dsc_subtipo_servicio"].Caption = "SubTipo Servicio";
                        gvAyuda.Columns["cod_producto"].Caption = "Cod. Producto";
                        gvAyuda.Columns["dsc_producto"].Caption = "Producto";
                        gvAyuda.Columns["cod_producto_item"].Caption = "Cod. Producto Item";
                        gvAyuda.Columns["dsc_producto_item"].Caption = "Producto Item";
                        gvAyuda.Columns["ctd_requerida"].Caption = "Ctd.";
                        gvAyuda.Columns["dsc_observacion"].Caption = "Observación";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_producto"];
                        gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_producto"], filtro);

                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.MarcaProducto:
                        ListMarca = blProv.ListarMarcasProveedor<eProveedor_Marca>(12, "");
                        gcAyuda.DataSource = ListMarca;

                        this.Text = "Listado de Marca-Producto";
                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "cod_proveedor" || col.FieldName == "cod_marca" || col.FieldName == "dsc_proveedor" || col.FieldName == "dsc_marca") { col.Visible = true; }
                        }
                        gvAyuda.Columns["cod_proveedor"].Width = 50;
                        gvAyuda.Columns["dsc_proveedor"].Width = 200;
                        gvAyuda.Columns["cod_marca"].Width = 50;
                        gvAyuda.Columns["dsc_marca"].Width = 200;

                        gvAyuda.Columns["cod_proveedor"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_proveedor"].VisibleIndex = 1;
                        gvAyuda.Columns["cod_marca"].VisibleIndex = 2;
                        gvAyuda.Columns["dsc_marca"].VisibleIndex = 2;

                        gvAyuda.Columns["cod_proveedor"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["cod_marca"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["cod_proveedor"].Caption = "Cod. Proveedor";
                        gvAyuda.Columns["dsc_proveedor"].Caption = "Proveedor";
                        gvAyuda.Columns["cod_marca"].Caption = "Cod. Marca";
                        gvAyuda.Columns["dsc_marca"].Caption = "Marca";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_marca"];
                        gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_marca"], filtro);

                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.ProveedorMultiple:
                        if (BotonAgregarVisible == 1)
                        {
                            this.layoutAgregar.Visibility = LayoutVisibility.Always;
                            this.layoutEspacioAgregar.Visibility = LayoutVisibility.Always;
                        }
                        gvAyuda.OptionsSelection.MultiSelect = true;
                        gvAyuda.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                        gvAyuda.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
                        layoutControlItem2.Visibility = LayoutVisibility.Always;
                        ListProveedor = blSistema.ListarEntidad<eProveedor>(3, "");
                        gcAyuda.DataSource = ListProveedor;

                        this.Text = "Busqueda de Proveedores";

                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "num_documento" || /*col.FieldName == "cod_proveedor" ||*/ col.FieldName == "dsc_razon_social" || col.FieldName == "dsc_razon_comercial") { col.Visible = true; }
                        }
                        gvAyuda.Columns["num_documento"].Width = 50;
                        gvAyuda.Columns["dsc_razon_social"].Width = 170;
                        gvAyuda.Columns["dsc_razon_comercial"].Width = 170;

                        gvAyuda.Columns["num_documento"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_razon_social"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_razon_comercial"].VisibleIndex = 2;

                        gvAyuda.Columns["num_documento"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        //gvAyuda.Columns["dsc_razon_comercial"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["num_documento"].Caption = "N° documento";
                        gvAyuda.Columns["dsc_razon_social"].Caption = "Razón Social";
                        gvAyuda.Columns["dsc_razon_comercial"].Caption = "Nombre Comercial";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        if (filtroRUC == "SI")
                        {
                            gvAyuda.FocusedColumn = gvAyuda.Columns["num_documento"];
                            gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["num_documento"], filtro);
                        }
                        else
                        {
                            gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_razon_social"];
                            gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_razon_social"], filtro);
                        }

                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.ProveedorTipoServicio:
                        if (BotonAgregarVisible == 1)
                        {
                            this.layoutAgregar.Visibility = LayoutVisibility.Always;
                            this.layoutEspacioAgregar.Visibility = LayoutVisibility.Always;
                        }
                        gvAyuda.OptionsSelection.MultiSelect = true;
                        gvAyuda.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                        gvAyuda.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
                        //layoutControlItem2.Visibility = LayoutVisibility.Always;
                        ListProveedor = blProv.ListarMarcasProveedor<eProveedor>(13, "", cod_tipo_servicio);
                        gcAyuda.DataSource = ListProveedor;

                        this.Text = "Busqueda de Proveedores";

                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "num_documento" || /*col.FieldName == "cod_proveedor" ||*/ col.FieldName == "dsc_razon_social" || col.FieldName == "dsc_razon_comercial") { col.Visible = true; }
                        }
                        gvAyuda.Columns["num_documento"].Width = 50;
                        gvAyuda.Columns["dsc_razon_social"].Width = 170;
                        gvAyuda.Columns["dsc_razon_comercial"].Width = 170;

                        gvAyuda.Columns["num_documento"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_razon_social"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_razon_comercial"].VisibleIndex = 2;

                        gvAyuda.Columns["num_documento"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        //gvAyuda.Columns["dsc_razon_comercial"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["num_documento"].Caption = "N° documento";
                        gvAyuda.Columns["dsc_razon_social"].Caption = "Razón Social";
                        gvAyuda.Columns["dsc_razon_comercial"].Caption = "Nombre Comercial";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        if (filtroRUC == "SI")
                        {
                            gvAyuda.FocusedColumn = gvAyuda.Columns["num_documento"];
                            gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["num_documento"], filtro);
                        }
                        else
                        {
                            gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_razon_social"];
                            gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_razon_social"], filtro);
                        }

                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.Productos:
                        if (BotonAgregarVisible == 1)
                        {
                            this.layoutAgregar.Visibility = LayoutVisibility.Always;
                            this.layoutEspacioAgregar.Visibility = LayoutVisibility.Always;
                        }
                        ListProductos = blSistema.Obtener_DatosProductos<eProyecto.eProyecto_Producto>(53, "");
                        gcAyuda.DataSource = ListProductos;

                        this.Text = "Listado de Productos";
                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (/*col.FieldName == "cod_tipo_servicio" || */col.FieldName == "dsc_tipo_servicio"/* || col.FieldName == "cod_subtipo_servicio" */||
                                col.FieldName == "dsc_subtipo_servicio"/* || col.FieldName == "cod_producto" */|| col.FieldName == "dsc_producto" ||
                                col.FieldName == "dsc_simbolo") { col.Visible = true; }
                        }
                        gvAyuda.Columns["dsc_tipo_servicio"].Width = 200;
                        gvAyuda.Columns["dsc_subtipo_servicio"].Width = 200;
                        gvAyuda.Columns["dsc_producto"].Width = 300;
                        gvAyuda.Columns["dsc_simbolo"].Width = 50;

                        gvAyuda.Columns["dsc_tipo_servicio"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_subtipo_servicio"].VisibleIndex = 3;
                        gvAyuda.Columns["dsc_producto"].VisibleIndex = 5;
                        gvAyuda.Columns["dsc_simbolo"].VisibleIndex = 6;

                        gvAyuda.Columns["dsc_simbolo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["dsc_tipo_servicio"].Caption = "Tipo Servicio";
                        gvAyuda.Columns["dsc_subtipo_servicio"].Caption = "SubTipo Servicio";
                        gvAyuda.Columns["dsc_producto"].Caption = "Producto";
                        gvAyuda.Columns["dsc_simbolo"].Caption = "Unds.";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_producto"];
                        gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_producto"], filtro);

                        gvAyuda.OptionsSelection.MultiSelect = true;
                        gvAyuda.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                        gvAyuda.OptionsSelection.CheckBoxSelectorColumnWidth = 25;

                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.OrdenesCompra:
                        ListOrdenCompra = blLogis.Obtener_ListaLogistica<eOrdenCompra_Servicio>(cod_almacen == "" ? 4 : 27, cod_almacen, cod_empresa, cod_sede_empresa, cod_proveedor: cod_proveedor);
                        gcAyuda.DataSource = ListOrdenCompra;

                        this.Text = "Listado de Orden Compra";
                        this.Width = 1200;
                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "cod_orden_compra_servicio" || col.FieldName == "dsc_almacen" || col.FieldName == "dsc_proveedor" || 
                                col.FieldName == "fch_emision" || col.FieldName == "imp_subtotal" || col.FieldName == "imp_igv" || col.FieldName == "imp_total") { col.Visible = true; }
                        }
                        gvAyuda.Columns["cod_orden_compra_servicio"].Width = 70;
                        gvAyuda.Columns["dsc_almacen"].Width = 200;
                        gvAyuda.Columns["dsc_proveedor"].Width = 300;
                        gvAyuda.Columns["fch_emision"].Width = 80;
                        gvAyuda.Columns["imp_subtotal"].Width = 70;
                        gvAyuda.Columns["imp_igv"].Width = 70;
                        gvAyuda.Columns["imp_total"].Width = 70;

                        gvAyuda.Columns["cod_orden_compra_servicio"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_almacen"].VisibleIndex = 2;
                        gvAyuda.Columns["dsc_proveedor"].VisibleIndex = 3;
                        gvAyuda.Columns["fch_emision"].VisibleIndex = 4;
                        gvAyuda.Columns["imp_subtotal"].VisibleIndex = 5;
                        gvAyuda.Columns["imp_igv"].VisibleIndex = 6;
                        gvAyuda.Columns["imp_total"].VisibleIndex = 7;

                        gvAyuda.Columns["cod_orden_compra_servicio"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["fch_emision"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["cod_orden_compra_servicio"].Caption = "Cod. OC";
                        gvAyuda.Columns["dsc_almacen"].Caption = "Almacen";
                        gvAyuda.Columns["dsc_proveedor"].Caption = "Proveedor";
                        gvAyuda.Columns["fch_emision"].Caption = "Fec. Emisión";
                        gvAyuda.Columns["imp_subtotal"].Caption = "SubTotal";
                        gvAyuda.Columns["imp_igv"].Caption = "IGV";
                        gvAyuda.Columns["imp_total"].Caption = "Total";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_proveedor"];
                        gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_proveedor"], filtro);
                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.ClienteEmpresa:
                        layoutControlItem2.Visibility = LayoutVisibility.Always;
                        btnNuevoProveedor.Text = "Nuevo Cliente";

                        ListCliente = blSistema.ListarEntidad<eCliente>(6, cod_condicion1);
                        gcAyuda.DataSource = ListCliente;

                        this.Text = "Busqueda de Clientes";

                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "cod_cliente" || col.FieldName == "dsc_cliente" || col.FieldName == "dsc_tipo_documento" || col.FieldName == "dsc_documento") { col.Visible = true; }
                        }
                        gvAyuda.Columns["cod_cliente"].Width = 50;
                        gvAyuda.Columns["dsc_cliente"].Width = 170;
                        gvAyuda.Columns["dsc_tipo_documento"].Width = 45;
                        gvAyuda.Columns["dsc_documento"].Width = 70;


                        gvAyuda.Columns["cod_cliente"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_cliente"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_tipo_documento"].VisibleIndex = 2;
                        gvAyuda.Columns["dsc_documento"].VisibleIndex = 3;

                        gvAyuda.Columns["cod_cliente"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["dsc_documento"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["cod_cliente"].Caption = "Còdigo";
                        gvAyuda.Columns["dsc_cliente"].Caption = "Cliente";
                        gvAyuda.Columns["dsc_tipo_documento"].Caption = "Tipo Doc";
                        gvAyuda.Columns["dsc_documento"].Caption = "Documento";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_cliente"];
                        gvAyuda.SetAutoFilterValue(gvAyuda.Columns["dsc_cliente"], filtro, DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains);

                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.Requerimiento:
                        ListRequerimiento = blLogis.Obtener_ListaLogistica<eRequerimiento>(3, cod_almacen, cod_empresa, cod_sede_empresa);
                        gcAyuda.DataSource = ListRequerimiento;

                        this.Text = "Listado de Requerimientos";
                        this.Width = 1200;
                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "cod_requerimiento" || col.FieldName == "dsc_solicitante" || col.FieldName == "fch_requerimiento" ||
                                col.FieldName == "dsc_estado_requerimiento" || col.FieldName == "dsc_observaciones" || col.FieldName == "dsc_justificacion") { col.Visible = true; }
                        }
                        gvAyuda.Columns["cod_requerimiento"].Width = 80;
                        gvAyuda.Columns["dsc_solicitante"].Width = 300;
                        gvAyuda.Columns["fch_requerimiento"].Width = 70;
                        gvAyuda.Columns["dsc_estado_requerimiento"].Width = 90;
                        gvAyuda.Columns["dsc_observaciones"].Width = 200;
                        gvAyuda.Columns["dsc_justificacion"].Width = 200;

                        gvAyuda.Columns["cod_requerimiento"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_solicitante"].VisibleIndex = 2;
                        gvAyuda.Columns["fch_requerimiento"].VisibleIndex = 3;
                        gvAyuda.Columns["dsc_estado_requerimiento"].VisibleIndex = 4;
                        gvAyuda.Columns["dsc_observaciones"].VisibleIndex = 5;
                        gvAyuda.Columns["dsc_justificacion"].VisibleIndex = 6;

                        gvAyuda.Columns["cod_requerimiento"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["fch_requerimiento"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["dsc_estado_requerimiento"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["cod_requerimiento"].Caption = "Cod. RQ";
                        gvAyuda.Columns["dsc_solicitante"].Caption = "Solicitante";
                        gvAyuda.Columns["fch_requerimiento"].Caption = "Fecha";
                        gvAyuda.Columns["dsc_estado_requerimiento"].Caption = "Estado";
                        gvAyuda.Columns["dsc_observaciones"].Caption = "Observaciones";
                        gvAyuda.Columns["dsc_justificacion"].Caption = "Justificación";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_solicitante"];
                        gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_solicitante"], filtro);
                        gvAyuda.ShowEditor();
                        break;

                }

            }
            catch
            {
            }
        }

        public void PasarDatos()
        {
            //DataGridViewRow row = (DataGridViewRow)gvAyuda.GetFocusedRow();
            switch (entidad)
            {
                case MiEntidad.Cliente:
                    eCliente eCliente = gvAyuda.GetFocusedRow() as eCliente;
                    descripcion = eCliente.dsc_cliente;
                    codigo = eCliente.cod_cliente;
                    break;
                case MiEntidad.ContactoxCliente:
                    eCliente eContactoxCliente = gvAyuda.GetFocusedRow() as eCliente;
                    codigo = eContactoxCliente.cod_tipo_cliente;
                    cod_condicion1 = eContactoxCliente.cod_cliente;
                    dsc_condicion1 = eContactoxCliente.dsc_cliente;
                    cod_condicion2 = eContactoxCliente.cod_departamento;
                    break;
                case MiEntidad.Proveedor:
                    eProveedor eProv = gvAyuda.GetFocusedRow() as eProveedor;
                    descripcion = eProv.dsc_proveedor;
                    codigo = eProv.cod_proveedor;
                    ruc = eProv.num_documento;
                    cod_condicion1 = eProv.dsc_placavehiculo;
                    break;
                case MiEntidad.ContactosCliente:
                    eCliente_Contactos eContact = gvAyuda.GetFocusedRow() as eCliente_Contactos;
                    descripcion = eContact.dsc_nombre_completo;
                    codigo = eContact.cod_contacto.ToString();
                    break;
                case MiEntidad.ContactosProveedor:
                    eProveedor_Contactos eContactProv = gvAyuda.GetFocusedRow() as eProveedor_Contactos;
                    descripcion = eContactProv.dsc_nombre;
                    codigo = eContactProv.cod_contacto.ToString();
                    cod_condicion1 = eContactProv.cod_proveedor;
                    dsc_condicion1 = eContactProv.dsc_cargo;
                    break;
                case MiEntidad.Trabajador:
                    eTrabajador eTrab = gvAyuda.GetFocusedRow() as eTrabajador;
                    descripcion = eTrab.dsc_nombres_completos;
                    codigo = eTrab.cod_trabajador;
                    break;
                case MiEntidad.Proyecto:
                    eProyecto eProy = gvAyuda.GetFocusedRow() as eProyecto;
                    descripcion = eProy.dsc_proyecto;
                    codigo = eProy.cod_proyecto;
                    break;
                case MiEntidad.ProductosProyecto:
                    eProyecto.eProyecto_Producto eProd = gvAyuda.GetFocusedRow() as eProyecto.eProyecto_Producto;
                    descripcion = eProd.dsc_producto;
                    codigo = eProd.cod_producto;
                    break;
                case MiEntidad.MarcaProducto:
                    eProveedor_Marca eMarca = gvAyuda.GetFocusedRow() as eProveedor_Marca;
                    descripcion = eMarca.dsc_marca;
                    codigo = eMarca.cod_marca.ToString();
                    cod_condicion1 = eMarca.cod_proveedor;
                    break;
                case MiEntidad.OrdenesCompra:
                    eOrdenCompra_Servicio eOrden = gvAyuda.GetFocusedRow() as eOrdenCompra_Servicio;
                    codigo = eOrden.cod_orden_compra_servicio;
                    ruc = eOrden.dsc_ruc;
                    cod_condicion1 = eOrden.cod_proveedor;
                    descripcion = eOrden.dsc_proveedor;
                    fch_generica = eOrden.fch_emision;
                    break;
                case MiEntidad.ClienteEmpresa:
                    eCliente eClienteEmp = gvAyuda.GetFocusedRow() as eCliente;
                    rucCliente = eClienteEmp.dsc_documento;
                    descripcion = eClienteEmp.dsc_cliente;
                    codigo = eClienteEmp.cod_cliente;
                    break;
                case MiEntidad.Requerimiento:
                    eRequerimiento eRequ = gvAyuda.GetFocusedRow() as eRequerimiento;
                    codigo = eRequ.cod_requerimiento;
                    descripcion = eRequ.dsc_solicitante;
                    fch_generica = eRequ.fch_requerimiento;
                    dsc_condicion1 = eRequ.cod_cliente;
                    dsc_condicion2 = eRequ.dsc_direccion_cliente;
                    break;
            }
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            switch (entidad)
            {
                case MiEntidad.ContactoxCliente:
                    frmMantContactoDireccionCliente frm = new frmMantContactoDireccionCliente();
                    frm.user = user;
                    frm.MiAccion = frmMantContactoDireccionCliente.Cliente.NuevoContactoDesdeIncidente;
                    frm.ShowDialog();
                    codigo = frm.codigo;
                    cod_condicion1 = frm.cod_condicion1;
                    dsc_condicion1 = frm.dsc_condicion1;
                    cod_condicion2 = frm.cod_condicion2;
                    this.Close();
                    break;
                case MiEntidad.Servicios:
                    eProveedor_Servicios eProvServ = new eProveedor_Servicios();
                    eProveedor_Servicios obj = gvAyuda.GetFocusedRow() as eProveedor_Servicios;
                    eProvServ.cod_tipo_servicio = obj.cod_tipo_servicio; eProvServ.cod_proveedor = cod_proveedor; eProvServ.flg_activo = "SI" ; 
                    eProvServ.cod_usuario_registro = user.cod_usuario;
                    eProvServ = blProv.Guardar_Actualizar_ProveedorServicio<eProveedor_Servicios>(eProvServ);
                    if (eProvServ == null) { MessageBox.Show("Error al vincular servicio al proveedor", "Vincular servicio", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    codigo = eProvServ.cod_tipo_servicio;
                    this.Close();
                    break;
                case MiEntidad.ProductosProyecto:
                    blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Agregando productos", "Cargando...");
                    foreach (int nRow in gvAyuda.GetSelectedRows())
                    {
                        eProyecto.eProyecto_Producto objProduct = gvAyuda.GetRow(nRow) as eProyecto.eProyecto_Producto;
                        eProyecto.eProyecto_Producto eProduct = new eProyecto.eProyecto_Producto();
                        eProduct.cod_empresa = cod_empresa; eProduct.cod_proyecto = cod_proyecto; eProduct.flg_activo = "SI";
                        eProduct.cod_tipo_servicio = objProduct.cod_tipo_servicio; eProduct.cod_subtipo_servicio = objProduct.cod_subtipo_servicio;
                        eProduct.cod_producto = objProduct.cod_producto; eProduct.cod_usuario_registro = user.cod_usuario;
                        eProduct = blFac.Insertar_Actualizar_ProyectoProductos<eProyecto.eProyecto_Producto>(eProduct);
                        if (eProduct == null) { MessageBox.Show("Error al vincular productos", "Vincular producto", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                        codigo = "-";
                    }
                    gvAyuda.RefreshData();
                    SplashScreenManager.CloseForm();
                    this.Close();
                    break;
                case MiEntidad.ProveedorMultiple:
                    foreach (int nRow in gvAyuda.GetSelectedRows())
                    {
                        eProveedor objP = gvAyuda.GetRow(nRow) as eProveedor;
                        if (objP != null) ListProv.Add(objP);
                    }
                    this.Close();
                    break;
                case MiEntidad.ProveedorTipoServicio:
                    foreach (int nRow in gvAyuda.GetSelectedRows())
                    {
                        eProveedor objP = gvAyuda.GetRow(nRow) as eProveedor;
                        if (objP != null) ListProv.Add(objP);
                    }
                    this.Close();
                    break;
                case MiEntidad.Productos:
                    foreach (int nRow in gvAyuda.GetSelectedRows())
                    {
                        eProyecto.eProyecto_Producto objPR = gvAyuda.GetRow(nRow) as eProyecto.eProyecto_Producto;
                        if (objPR != null) ListProd.Add(objPR);
                    }
                    this.Close();
                    break;
            }
        }

        private void gvAyuda_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gvAyuda.OptionsSelection.MultiSelectMode != DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect)
            {
                if (e.Clicks == 2 && e.RowHandle >= 0 && entidad != MiEntidad.Servicios)
                {
                    PasarDatos();
                    this.Close();
                }
            }
        }

        private void gvAyuda_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }
        private void frmBusquedas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void gvAyuda_ShownEditor(object sender, EventArgs e)
        {
            if (gvAyuda.FocusedRowHandle == GridControl.AutoFilterRowHandle)
            {
                var editor = (TextEdit)gvAyuda.ActiveEditor;
                editor.SelectionLength = 0;
                editor.SelectionStart = editor.Text.Length;
            }
        }

        private void gvAyuda_KeyDown(object sender, KeyEventArgs e)
        {
            if (gvAyuda.FocusedRowHandle >= 0 && e.KeyCode == Keys.Enter && entidad != MiEntidad.Servicios)
            {
                PasarDatos();
                this.Close();
            }
        }

        private void gvAyuda_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void btnNuevoProveedor_Click(object sender, EventArgs e)
        {
            switch (entidad)
            {
                case MiEntidad.ClienteEmpresa:
                    frmMantCliente frmCliente = new frmMantCliente();
                    frmCliente.MiAccion = Cliente.Nuevo;
                    frmCliente.colorVerde = colorVerde;
                    frmCliente.colorPlomo = colorPlomo;
                    frmCliente.colorEventRow = colorEventRow;
                    frmCliente.colorFocus = colorFocus;
                    frmCliente.user = user;
                    frmCliente.ShowDialog();
                    Inicializar();
                    break;
                case MiEntidad.Proveedor:
                    frmMantProveedor frm = new frmMantProveedor();
                    frm.user = user;
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorEventRow = colorEventRow;
                    frm.colorFocus = colorFocus;
                    frm.ShowDialog();
                    Inicializar();
                    break;
            }
        }
    }
}