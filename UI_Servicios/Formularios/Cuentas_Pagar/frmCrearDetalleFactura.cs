using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using BE_Servicios;
using BL_Servicios;
using UI_Servicios.Formularios.Shared;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Views.Grid;

namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    internal enum DetalleFactura
    {
        Nuevo = 0,
        Editar = 1
    }

    public partial class frmCrearDetalleFactura : DevExpress.XtraEditors.XtraForm
    {
        
        public eUsuario user = new eUsuario();
        blUsuario blUsu = new blUsuario();
        blGlobales blGlobal = new blGlobales();
        blFactura blfact = new blFactura();
        blTrabajador blTrab = new blTrabajador();
        blRequerimiento blReq = new blRequerimiento();
        frmMantFacturaProveedor frmHandler = new frmMantFacturaProveedor();
        internal DetalleFactura MiAccion = DetalleFactura.Nuevo;
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;

        public string cod_producto = "";
        public string GrupoSeleccionado = "";
        public string ItemSeleccionado = "";
        public string cod_empresa = "";
        public string cod_proveedor = "";
        public string cod_orden_compra_servicio = "";
        public string cod_sede_empresa;
        public string tipo_documento = "";
        public string serie_documento = "";
        public string numero_documento = "";
        decimal subTotal = 0;
        decimal total = 0;

        //public string GrupoSeleccionado = "";
        public frmCrearDetalleFactura()
        {
            InitializeComponent();
        }
        public frmCrearDetalleFactura(frmMantFacturaProveedor frm)
        {
            InitializeComponent();
            frmHandler = frm;
            xtabSinOrden.PageVisible = false;
        }
       

        private void frmCrearDetalleFactura_Load(object sender, EventArgs e)
        {
            
            groupControl4.AppearanceCaption.ForeColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
            groupControl2.AppearanceCaption.ForeColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
            groupControl3.AppearanceCaption.ForeColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
        }

        private void btnBuscarOrden_Click(object sender, EventArgs e)
        {
            if (lkp_sedes.EditValue == null) 
            { MessageBox.Show("Debe seleccionar la sede empresa.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                lkp_sedes.Focus(); return; }
            Busqueda("OrdenesCompra");
        }

        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            blTrab.CargaCombosLookUp("SedesEmpresa", lkp_sedes, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa);
            lkp_sedes.EditValue = "00001";
            List<eTrabajador.eInfoLaboral_Trabajador> lista = blTrab.ListarOpcionesTrabajador<eTrabajador.eInfoLaboral_Trabajador>(6, cod_empresa);
            if (lista.Count == 1) lkp_sedes.EditValue = lista[0].cod_sede_empresa;
        }

        private void btnAsignarOrden_Click(object sender, EventArgs e)
        {
            int valor = 0;
            foreach (int nRow in gvOdenDisponible.GetSelectedRows())
            {

                eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle obj = gvOdenDisponible.GetRow(nRow - valor) as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;
                obj.imp_total = obj.num_cantidad * obj.imp_unitario;

                subTotal = subTotal + obj.imp_total;
                bsOrdenAsignado.Add(obj);
                bsOrdenDisponible.Remove(obj);
                valor = valor + 1;
                               
                gvOrdenAsignado.RefreshData();
            }
           
            txtSubTotal2.EditValue = Math.Round(subTotal, 2).ToString();
        }

        public void btnQuitarOrden_Click(object sender, EventArgs e)
        {
            int valor = 0;
            foreach (int nRow in gvOrdenAsignado.GetSelectedRows())
            {

                eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle obj = gvOrdenAsignado.GetRow(nRow - valor) as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;
                obj.imp_total = obj.num_cantidad * obj.imp_unitario;

                subTotal = subTotal - obj.imp_total;
                bsOrdenDisponible.Add(obj);
                bsOrdenAsignado.Remove(obj);
                valor = valor + 1;
                gvOrdenAsignado.RefreshData();
            }
            txtSubTotal2.EditValue = Math.Round(subTotal, 2).ToString();
        }

            public void CargarOrdenesDisponibles()
        {
            List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle> ListadoOrdenesDisponible = new List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>();
            ListadoOrdenesDisponible = blfact.ListarOrdenes<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>(3,cod_empresa, cod_proveedor, cod_orden_compra_servicio);
            bsOrdenDisponible.DataSource = null; bsOrdenDisponible.DataSource = ListadoOrdenesDisponible;
        }


        private void btneliminar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Esta seguro de eliminar el documento?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    eFacturaProveedor.eFacturaProvedor_Detalle eFact = new eFacturaProveedor.eFacturaProvedor_Detalle();
                    
                    string result = "";
                    result = blfact.EliminarDatosDetalle(tipo_documento,cod_empresa, cod_orden_compra_servicio);
                    if (result != "OK") { MessageBox.Show("Error eliminó el documento de manera correcta.", "Eliminar documentos", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    if (result == "OK") { MessageBox.Show("Se eliminó el documento de manera correcta.", "Eliminar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information); } 
                      
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvOdenDisponible_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvOrdenAsignado_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvOrdenAsignado_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvOdenDisponible_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvProductoAsignado_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvProductoDisponible_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvProductoAsignado_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvProductoDisponible_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void grdbTipoOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            int opcion = 0;
            opcion = grdbTipoOrden.SelectedIndex;
            xtabOrdenCompra.PageVisible = false;
            xtabSinOrden.PageVisible = false;

            switch (opcion)
            {
                case 0:
                    xtabOrdenCompra.PageVisible = true;
                    txtNumOrden.Enabled = true;
                    btnBuscarOrden.Enabled = true;
                    lkp_sedes.Enabled = true;
                    txtNumOrden.Text = "";
                    break;
                case 1:
                    xtabSinOrden.PageVisible = true;
                    txtNumOrden.Enabled = false;
                    btnBuscarOrden.Enabled = false;
                    txtNumOrden.Text = "SO-"+cod_empresa;

                    CargarListadoProductos();
                    break;
            }
        }

        private void btnguardar_ItemClick(object sender, ItemClickEventArgs e)
        {

            try
            {
                gvOrdenAsignado.PostEditor();
                for (int x = 0; x < gvOrdenAsignado.DataRowCount; x++)

                {
                    eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle obj = gvOrdenAsignado.GetRow(x) as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;
                    eFacturaProveedor.eFacturaProvedor_Detalle eprod = new eFacturaProveedor.eFacturaProvedor_Detalle();

                    eprod.tipo_documento = tipo_documento;
                    eprod.serie_documento = txtSerieDocumento.Text;
                    eprod.numero_documento = decimal.Parse(txtNumeroDocumento.Text);
                    eprod.cod_empresa = cod_empresa;
                    eprod.cod_sede_empresa = lkp_sedes.EditValue.ToString();
                    eprod.cod_orden_compra_servicio = obj.cod_orden_compra_servicio;
                    eprod.cod_proveedor = cod_proveedor;
                    eprod.dsc_ruc = txtRucProveedor.Text;
                    eprod.cod_producto = obj.cod_producto;
                    eprod.cod_unidad_medida = obj.cod_unidad_medida;
                    eprod.num_cantidad = obj.num_cantidad;
                    eprod.imp_unitario = obj.imp_unitario;
                    eprod.imp_total_det = obj.imp_total_det;
                    eprod.cod_usuario_registro = user.cod_usuario;
                    eprod.cod_usuario_cambio = user.cod_usuario;
                    eprod.fch_registro = obj.fch_registro;
                    eprod.fch_cambio = obj.fch_cambio;
                    
                    eprod = blfact.Ins_Act_Facdetalle<eFacturaProveedor.eFacturaProvedor_Detalle>(eprod, user.cod_usuario);
                }
                MessageBox.Show("Se registro el documento de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnAsignarProducto_Click(object sender, EventArgs e)
        {
            int valor = 0;
            foreach (int nRow in gvProductoDisponible.GetSelectedRows())
            {
                eProductos obj = gvProductoDisponible.GetRow(nRow - valor) as eProductos;
                eFacturaProveedor.eFacturaProvedor_Detalle obj2 = new eFacturaProveedor.eFacturaProvedor_Detalle();

                obj2.dsc_tipo_servicio = obj.dsc_tipo_servicio;
                obj2.subtipo_servicio = obj.dsc_subtipo_servicio;
                obj2.cod_producto = obj.cod_producto;
                obj2.dsc_producto = obj.dsc_producto;
                obj2.dsc_simbolo = obj.dsc_simbolo;


                bsProductosAsignado.Add(obj2);
                bsProductosDisponibles.Remove(obj);
                valor = valor + 1;
                
            }
            
        }

        private void gvProductoAsignado_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (gvProductoAsignado.FocusedColumn.Name == "colnum_cantidad1")
            {
                eFacturaProveedor.eFacturaProvedor_Detalle obj = gvProductoAsignado.GetFocusedRow() as eFacturaProveedor.eFacturaProvedor_Detalle;

                if (obj.num_cantidad == 0) { MessageBox.Show("Debe escribir una cantidad mayor a cero.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); obj.num_cantidad = 1; return; }

                obj.imp_total_det = obj.num_cantidad * obj.imp_subtotal;
                 
                subTotal = subTotal + obj.imp_total_det;
                gvProductoAsignado.RefreshData();
            }
            
            txtSubTotal.EditValue = Math.Round(subTotal, 2).ToString();
        }

        private void txtSubTotal_EditValueChanged(object sender, EventArgs e)
        {
            txtSubTotal.EditValue = Math.Round(subTotal, 2).ToString();
        }

        private void btnQuitarProducto_Click(object sender, EventArgs e)
        {
            int valor = 0;
            foreach (int nRow in gvProductoAsignado.GetSelectedRows())
            {
                eFacturaProveedor.eFacturaProvedor_Detalle obj = gvProductoAsignado.GetRow(nRow - valor) as eFacturaProveedor.eFacturaProvedor_Detalle;
                eProductos obj2 = new eProductos();

                obj2.dsc_tipo_servicio = obj.dsc_tipo_servicio;
                obj2.dsc_subtipo_servicio = obj.subtipo_servicio;
                obj2.dsc_producto = obj.dsc_producto;
                obj2.cod_producto = obj.cod_producto;
                obj2.dsc_simbolo = obj.dsc_simbolo;

                bsProductosDisponibles.Add(obj2);
                bsProductosAsignado.Remove(obj);
                valor = valor + 1;

                subTotal = subTotal - obj.imp_total_det;
            }

            txtSubTotal.EditValue = Math.Round(subTotal, 2).ToString();

        }
            public void CargarOrdenesAsignados()
        {

            List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle> ListadoOrdenesAsignados = new List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>();
            ListadoOrdenesAsignados = blfact.ListarOrdenes<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>(4, cod_empresa, cod_proveedor, cod_orden_compra_servicio);
            bsOrdenAsignado.DataSource = null; bsOrdenAsignado.DataSource = ListadoOrdenesAsignados;

                   }



        public void Busqueda(string tipo)
        {
            frmBusquedas frm = new frmBusquedas();
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;

           
            switch (tipo)
            {
                case "OrdenesCompra":
                    frm.entidad = frmBusquedas.MiEntidad.OrdenesCompra;
                    frm.cod_empresa = cod_empresa;
                    frm.cod_proveedor = cod_proveedor;
                    frm.cod_sede_empresa = cod_sede_empresa;
                    break;
            }
            frm.ShowDialog();


            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "OrdenesCompra":
                    txtNumOrden.Text = frm.codigo;
                    cod_orden_compra_servicio = frm.codigo;


                    eOrdenCompra_Servicio eOrd = new eOrdenCompra_Servicio();
                    CargarOrdenesDisponibles();
                    break;
            }




        }
        private void CargarListadoProductos()
        {
            try
            {
                List<eProductos> lista = blReq.ListarProductos<eProductos>(4, cod_empresa);
                bsProductosDisponibles.DataSource = lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

       


    }
}