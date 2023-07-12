using BE_Servicios;
using BL_Servicios;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_Servicios.Formularios.Clientes_Y_Proveedores.Proveedores;
using UI_Servicios.Formularios.Shared;

namespace UI_Servicios.Formularios.Logistica
{
    internal enum OrdenServicio
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }

    public partial class frmMantOrdenServicio : DevExpress.XtraEditors.XtraForm
    {
        internal OrdenServicio accion = OrdenServicio.Nuevo;

        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        blLogistica blLog = new blLogistica();
        blRequerimiento blReq = new blRequerimiento();
        blOrdenCompra_Servicio blOrdCom = new blOrdenCompra_Servicio();
        blProveedores blProv = new blProveedores();
        TaskScheduler scheduler;

        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public string codigoEmpresa;
        public string empresa, sede, ordenCompraServicio, solicitud;
        public Int32 anho;

        eOrdenCompra_Servicio eOrdenServicioEdit = new eOrdenCompra_Servicio();
        List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle> eDetOrdenServicioEdit;
        List<eRequerimiento.eRequerimiento_Detalle> eRequerimientoDetalle;
        String codigoCliente = "";
        String codigoProveedor = "";
        String codigoProveedorDet = "";
        String dscProveedorDet = "";
        String rucProveedorDet = "";
        decimal subTotal = 0;
        decimal igv = 0;
        decimal total = 0;

        public frmMantOrdenServicio()
        {
            InitializeComponent();
        }

        private void frmMantRequerimientos_Load(object sender, EventArgs e)
        {
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Inicializar();
        }

        private void Inicializar()
        {
            try
            {
                CargarLookUpEdit();


                switch (accion)
                {
                    case OrdenServicio.Nuevo:
                        ConfigurarForm("Nuevo");

                        DateTime date = DateTime.Now;
                        dtpFechaEmision.EditValue = date;
                        dtpFechaDespacho.Properties.MinValue = date;
                        dtpFechaDespacho.EditValue = date;
                        break;
                    case OrdenServicio.Editar:
                        CargarOrdenServicio();
                        ConfigurarForm("Editar");
                        break;
                    case OrdenServicio.Vista:
                        CargarOrdenServicio();
                        ConfigurarForm("Vista");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargarLookUpEdit()
        {
            try
            {
                blOrdCom.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: user.cod_usuario);

                if (codigoEmpresa == null)
                {
                    List<eFacturaProveedor> list = blProv.ListarEmpresasProveedor<eFacturaProveedor>(11, "", user.cod_usuario);
                    if (list.Count == 1)
                    {
                        lkpEmpresa.EditValue = list[0].cod_empresa;
                        codigoEmpresa = list[0].cod_empresa;
                    }
                    else
                    {
                        lkpEmpresa.EditValue = empresa; codigoEmpresa = empresa;
                    }
                }
                else
                {
                    lkpEmpresa.EditValue = codigoEmpresa;
                }

                blOrdCom.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: codigoEmpresa);
                blOrdCom.CargaCombosLookUp("Moneda", lkpMoneda, "cod_moneda", "dsc_moneda", "", valorDefecto: true);
                blOrdCom.CargaCombosLookUp("ModPago", lkpModPago, "cod_modalidad_pago", "dsc_modalidad_pago", "", valorDefecto: true);

                lkpSede.EditValue = "00001";
                lkpModPago.EditValue = "MP001";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void CargarOrdenServicio()
        {
            eOrdenServicioEdit = blOrdCom.Cargar_OrdenCompra_Servicio<eOrdenCompra_Servicio>(2, empresa, sede, ordenCompraServicio, solicitud, anho);

            txtOC.EditValue = eOrdenServicioEdit.cod_orden_compra_servicio;
            lkpEmpresa.EditValue = eOrdenServicioEdit.cod_empresa;
            lkpSede.EditValue = eOrdenServicioEdit.cod_sede_empresa;
            txtCotizacion.EditValue = eOrdenServicioEdit.num_cotizacion;
            codigoProveedor = eOrdenServicioEdit.cod_proveedor;
            txtProveedor.EditValue = eOrdenServicioEdit.dsc_proveedor;
            txtRUC.EditValue = eOrdenServicioEdit.dsc_ruc;
            txtUndRecepcion.EditValue = eOrdenServicioEdit.dsc_unidad_recepcion;
            txtDireccion.EditValue = eOrdenServicioEdit.dsc_direccion_despacho;
            lkpModPago.EditValue = eOrdenServicioEdit.cod_modalidad_pago;
            lkpMoneda.EditValue = eOrdenServicioEdit.cod_moneda;
            dtpFechaEmision.EditValue = eOrdenServicioEdit.fch_emision;
            if (eOrdenServicioEdit.fch_despacho.ToString().Contains("1/01/0001")) { dtpFechaDespacho.EditValue = null; } else { dtpFechaDespacho.EditValue = eOrdenServicioEdit.fch_despacho; }
            txtTermCond.EditValue = eOrdenServicioEdit.dsc_terminos_condiciones;
            txtSubTotal.EditValue = eOrdenServicioEdit.imp_subtotal;
            txtIGV.EditValue = eOrdenServicioEdit.imp_igv;
            txtTotal.EditValue = eOrdenServicioEdit.imp_total;
            txtTotalLetras.EditValue = eOrdenServicioEdit.dsc_imp_total;
            txtCV.EditValue = eOrdenServicioEdit.prc_CV;
            txtLI.EditValue = eOrdenServicioEdit.prc_LI;
            txtCB.EditValue = eOrdenServicioEdit.prc_CB;
            txtGG.EditValue = eOrdenServicioEdit.prc_GG;
            txtADM.EditValue = eOrdenServicioEdit.prc_ADM;
            txtOPER.EditValue = eOrdenServicioEdit.prc_OPER;
            txtGV.EditValue = eOrdenServicioEdit.prc_GV;
            txtObservaciones.EditValue = eOrdenServicioEdit.dsc_observaciones;

            eDetOrdenServicioEdit = blOrdCom.Cargar_Detalle_OrdenCompra_Servicio<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>(3, empresa, sede, ordenCompraServicio, solicitud, anho);

            bsDetalleOC.DataSource = eDetOrdenServicioEdit;
        }

        private void ConfigurarForm(string opcion)
        {
            switch (opcion)
            {
                case "Nuevo":
                    txtSubTotal.Enabled = false;
                    txtIGV.Enabled = false;
                    txtTotal.Enabled = false;
                    txtTotalLetras.Enabled = false;
                    break;
                case "Editar":
                    lkpEmpresa.Enabled = false;
                    lkpSede.Enabled = false;
                    dtpFechaEmision.Enabled = false;

                    txtSubTotal.Enabled = false;
                    txtIGV.Enabled = false;
                    txtTotal.Enabled = false;
                    txtTotalLetras.Enabled = false;

                    break;
                case "Vista":
                    barOpciones.Visible = false;

                    lkpEmpresa.Enabled = false;
                    lkpSede.Enabled = false;
                    txtCotizacion.Enabled = false;
                    btnVerProveedor.Enabled = false;
                    txtProveedor.Enabled = false;
                    txtRUC.Enabled = false;
                    txtUndRecepcion.Enabled = false;
                    lkpModPago.Enabled = false;
                    lkpMoneda.Enabled = false;
                    txtDireccion.Enabled = false;
                    dtpFechaEmision.Enabled = false;
                    dtpFechaDespacho.Enabled = false;
                    txtTermCond.Enabled = false;
                    txtObservaciones.Enabled = false;

                    txtSubTotal.Enabled = false;
                    txtIGV.Enabled = false;
                    txtTotal.Enabled = false;
                    txtTotalLetras.Enabled = false;

                    colnum_cantidad1.OptionsColumn.AllowEdit = false;
                    colimp_unitario.OptionsColumn.AllowEdit = false;
                    colbtnbuscar_proveedor_det.Visible = false;

                    break;
            }
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (codigoProveedor == "") { MessageBox.Show("Debe seleccionar un proveedor.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtProveedor.Focus(); return; }
                if (txtRUC.EditValue == null) { MessageBox.Show("Debe ingresar el RUC del proveedor.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtRUC.Focus(); return; }
                if (txtUndRecepcion.EditValue == null) { MessageBox.Show("Debe ingresar una Und. de Recepción.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtUndRecepcion.Focus(); return; }
                if (txtDireccion.EditValue == null) { MessageBox.Show("Debe ingresar una Dirección de Despacho.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDireccion.Focus(); return; }
                if (dtpFechaDespacho.EditValue == null) { MessageBox.Show("Debe seleccionar una fecha de despacho.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtpFechaDespacho.Focus(); return; }

                eOrdenCompra_Servicio eOrdCom = AsignarValores();

                eOrdCom = blOrdCom.Ins_Act_OrdenCompra_Servicio<eOrdenCompra_Servicio>(eOrdCom, user.cod_usuario);

                switch (accion)
                {
                    case OrdenServicio.Editar:
                        string respuesta = blOrdCom.Limpiar_Det_OrdenCompra_Servicio(eOrdCom.cod_empresa, eOrdCom.cod_sede_empresa, eOrdCom.cod_orden_compra_servicio, eOrdCom.flg_solicitud, eOrdCom.dsc_anho);
                        break;
                }

                gvDetalleOC.RefreshData();

                List<eProveedor> listProv = new List<eProveedor>();
                List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle> listServ = new List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>();

                foreach (eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle obj in eDetOrdenServicioEdit)
                {
                    eProveedor eProv = new eProveedor(); eProveedor eProv2 = new eProveedor(); eProv.cod_proveedor = obj.cod_proveedor_det; eProv.num_documento = obj.dsc_ruc_det;
                    eProv2 = listProv.Find(x => x.cod_proveedor == obj.cod_proveedor_det);
                    if (eProv2 == null) listProv.Add(eProv);
                }

                foreach (eProveedor obj in listProv)
                {
                    if (obj.cod_proveedor != codigoProveedor)
                    {
                        eOrdCom = AsignarValores();

                        eOrdCom.cod_orden_compra_servicio = "";
                        eOrdCom.cod_proveedor = obj.cod_proveedor;
                        eOrdCom.dsc_ruc = obj.num_documento;

                        eOrdCom = blOrdCom.Ins_Act_OrdenCompra_Servicio<eOrdenCompra_Servicio>(eOrdCom, user.cod_usuario);

                        listServ = eDetOrdenServicioEdit.FindAll(x => x.cod_proveedor_det == obj.cod_proveedor);

                        CrearDetalle(listServ, eOrdCom);
                    }
                    else
                    {
                        listServ = eDetOrdenServicioEdit.FindAll(x => x.cod_proveedor_det == codigoProveedor);
                        CrearDetalle(listServ, eOrdCom);
                    }
                }

                MessageBox.Show("Se registro el documento de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarOrdenServicio();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private eOrdenCompra_Servicio AsignarValores()
        {
            eOrdenCompra_Servicio eOC = new eOrdenCompra_Servicio();

            eOC.cod_empresa = lkpEmpresa.EditValue.ToString();
            eOC.cod_sede_empresa = lkpSede.EditValue.ToString();
            eOC.cod_orden_compra_servicio = txtOC.EditValue.ToString();
            eOC.num_cotizacion = txtCotizacion.EditValue.ToString();
            eOC.cod_proveedor = codigoProveedor;
            eOC.dsc_ruc = txtRUC.EditValue.ToString();
            eOC.flg_solicitud = "S";
            eOC.dsc_unidad_recepcion = txtUndRecepcion.EditValue == null ? "" : txtUndRecepcion.EditValue.ToString();
            eOC.cod_modalidad_pago = lkpModPago.EditValue.ToString();
            eOC.cod_moneda = lkpMoneda.EditValue.ToString();
            eOC.dsc_direccion_despacho = txtDireccion.EditValue == null ? "" : txtDireccion.EditValue.ToString();
            eOC.fch_emision = Convert.ToDateTime(dtpFechaEmision.EditValue);
            eOC.fch_despacho = Convert.ToDateTime(dtpFechaDespacho.EditValue);
            eOC.dsc_terminos_condiciones = txtTermCond.EditValue == null ? "" : txtTermCond.EditValue.ToString();
            eOC.imp_subtotal = 0;
            eOC.imp_igv = 0;
            eOC.imp_total = 0;
            eOC.dsc_imp_total = txtTotalLetras.EditValue == null ? "" : txtTotalLetras.EditValue.ToString();
            eOC.prc_CV = Convert.ToDecimal(txtCV.EditValue);
            eOC.prc_LI = Convert.ToDecimal(txtLI.EditValue);
            eOC.prc_CB = Convert.ToDecimal(txtCB.EditValue);
            eOC.prc_GG = Convert.ToDecimal(txtGG.EditValue);
            eOC.prc_ADM = Convert.ToDecimal(txtADM.EditValue);
            eOC.prc_OPER = Convert.ToDecimal(txtOPER.EditValue);
            eOC.prc_GV = Convert.ToDecimal(txtGV.EditValue);
            eOC.dsc_observaciones = txtObservaciones.EditValue == null ? "" : txtObservaciones.EditValue.ToString();

            return eOC;
        }

        private void CrearDetalle(List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle> listServ, eOrdenCompra_Servicio eOrdCom)
        {
            foreach (eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle objDet in listServ)
            {
                objDet.cod_proveedor = objDet.cod_proveedor_det;
                objDet.dsc_ruc = objDet.dsc_ruc_det;

                objDet.cod_empresa = eOrdCom.cod_empresa;
                objDet.cod_sede_empresa = eOrdCom.cod_sede_empresa;
                objDet.cod_orden_compra_servicio = eOrdCom.cod_orden_compra_servicio;
                objDet.flg_solicitud = eOrdCom.flg_solicitud;
                objDet.dsc_anho = eOrdCom.dsc_anho;

                eProductos.eProductosTarifas eProdTar = new eProductos.eProductosTarifas();

                eProdTar.cod_tipo_servicio = objDet.cod_tipo_servicio;
                eProdTar.cod_subtipo_servicio = objDet.cod_subtipo_servicio;
                eProdTar.cod_producto = objDet.cod_producto;
                eProdTar.num_item = 0;
                eProdTar.fch_inicio = Convert.ToDateTime(DateTime.Now.ToString("d"));
                eProdTar.fch_fin = new DateTime(2999, 12, 31);
                eProdTar.imp_costo = objDet.imp_unitario;
                eProdTar.dsc_observacion = "";
                eProdTar.cod_proveedor = objDet.cod_proveedor;
                eProdTar.dsc_ruc = objDet.dsc_ruc;

                eProdTar = blLog.Insertar_Actualizar_ProductoCostos<eProductos.eProductosTarifas>(eProdTar);

                eProductos.eProductosProveedor eProdProv = new eProductos.eProductosProveedor();

                eProdProv.cod_tipo_servicio = objDet.cod_tipo_servicio;
                eProdProv.cod_subtipo_servicio = objDet.cod_subtipo_servicio;
                eProdProv.cod_producto = objDet.cod_producto;
                eProdProv.dsc_ruc = objDet.dsc_ruc;
                eProdProv.cod_proveedor = objDet.cod_proveedor;
                eProdProv.cod_usuario_registro = user.cod_usuario;

                blLog.Insertar_Actualizar_ProductosProveedor<eProductos.eProductosProveedor>(eProdProv);

                objDet.num_item = eProdTar.num_item;

                eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOC = blOrdCom.Ins_Act_Detalle_OrdenCompra_Servicio<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>(objDet, user.cod_usuario);
            }
        }

        private void btnVerProveedor_Click(object sender, EventArgs e)
        {
            frmMantProveedor frm = new frmMantProveedor();
            frm.cod_proveedor = codigoProveedor;
            frm.MiAccion = Proveedor.Vista;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.cod_empresa = lkpEmpresa.EditValue.ToString();
            frm.user = user;
            frm.ShowDialog();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            Busqueda("", "Cliente");
        }

        public void Busqueda(string dato, string tipo, string filtroRUC = "NO")
        {
            frmBusquedas frm = new frmBusquedas();
            frm.user = user;
            frm.filtro = dato;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;

            switch (tipo)
            {
                case "Proveedor":
                    frm.entidad = frmBusquedas.MiEntidad.Proveedor;
                    frm.filtroRUC = filtroRUC;
                    frm.filtro = dato;
                    break;
                case "ProveedorDet":
                    frm.entidad = frmBusquedas.MiEntidad.Proveedor;
                    frm.filtroRUC = filtroRUC;
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Proveedor":
                    codigoProveedor = frm.codigo;
                    txtProveedor.EditValue = frm.descripcion;
                    txtRUC.EditValue = frm.ruc;

                    for (int i = 0; i < gvDetalleOC.RowCount; i++)
                    {
                        eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eOrdComNue = gvDetalleOC.GetRow(i) as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;

                        eOrdComNue.cod_proveedor_det = frm.codigo;
                        eOrdComNue.dsc_proveedor_det = frm.descripcion;
                        eOrdComNue.dsc_ruc_det = frm.ruc;
                    }

                    gvDetalleOC.RefreshData();
                    break;
                case "ProveedorDet":
                    codigoProveedorDet = frm.codigo;
                    dscProveedorDet = frm.descripcion;
                    rucProveedorDet = frm.ruc;
                    break;
            }
        }

        private void dtpFechaEmision_EditValueChanged(object sender, EventArgs e)
        {
            DateTime date = Convert.ToDateTime(dtpFechaEmision.EditValue);

            dtpFechaDespacho.EditValue = date;
            dtpFechaDespacho.Properties.MinValue = date;
        }

        private void txtSubTotal_TextChanged(object sender, EventArgs e)
        {
            igv = subTotal * Convert.ToDecimal(0.18);
            txtIGV.EditValue = Math.Round(igv, 2).ToString();
        }

        private void txtIGV_TextChanged(object sender, EventArgs e)
        {
            total = subTotal + igv;
            txtTotal.EditValue = Math.Round(total, 2).ToString();
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            txtTotalLetras.EditValue = blOrdCom.Monto_Letras(total).Rows[0].ItemArray[0].ToString();
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            eRequerimientoDetalle = blOrdCom.BuscarRequerimiento<eRequerimiento.eRequerimiento_Detalle>(codigoCliente);

            bsProductosReq.DataSource = eRequerimientoDetalle;
        }

        private void gvProductos_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvProductos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvProductos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvProductos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvProductos_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                
            }
        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Busqueda("", "Proveedor");
            }
            string dato = blGlobal.pKeyPress(txtProveedor, e);
            if (dato != "")
            {
                Busqueda(dato, "Proveedor");
            }
        }

        private void gvDetalleReq_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvDetalleReq_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvDetalleReq_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvDetalleReq_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvDetalleOC_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (gvDetalleOC.FocusedColumn.Name == "colnum_cantidad1")
            {
                eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle obj = gvDetalleOC.GetFocusedRow() as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;
                
                if (obj.num_cantidad == 0) { MessageBox.Show("Debe seleccionar una cantidad mayor a cero.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); obj.num_cantidad = 1; return; }
                if (obj.num_cantidad > obj.num_cantidad_req) { MessageBox.Show("Debe seleccionar una cantidad igual o menor a la requerida.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); obj.num_cantidad = obj.num_cantidad_req; return; }

                obj.imp_total_det = obj.num_cantidad * obj.imp_unitario;

                subTotal = 0;
                int ctd_total = 0, ctd_req = obj.num_cantidad_req;

                if (obj.num_cantidad <= obj.num_cantidad_req)
                {
                    eDetOrdenServicioEdit.RemoveAll(x => x.cod_producto == obj.cod_producto && x.cod_proveedor_det == null);
                    
                    ctd_total = eDetOrdenServicioEdit.Where(x => x.cod_producto == obj.cod_producto).Sum(x => x.num_cantidad);

                    if (ctd_req - ctd_total > 0)
                    {
                        eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle objRestante = new eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle();
                        objRestante.cod_empresa = obj.cod_empresa;
                        objRestante.cod_requerimiento = obj.cod_requerimiento;
                        objRestante.cod_tipo_servicio = obj.cod_tipo_servicio;
                        objRestante.cod_subtipo_servicio = obj.cod_subtipo_servicio;
                        objRestante.cod_producto = obj.cod_producto;
                        objRestante.dsc_producto = obj.dsc_producto;
                        objRestante.cod_unidad_medida = obj.cod_unidad_medida;
                        objRestante.dsc_simbolo = obj.dsc_simbolo;
                        objRestante.cod_proveedor = null;
                        objRestante.cod_proveedor_det = null;
                        objRestante.dsc_ruc = null;
                        objRestante.dsc_ruc_det = null;
                        objRestante.num_cantidad = ctd_req - ctd_total;
                        objRestante.num_cantidad_req = obj.num_cantidad_req;
                        objRestante.num_item = obj.num_item;
                        objRestante.imp_unitario = obj.imp_unitario;
                        objRestante.imp_total_det = objRestante.num_cantidad * objRestante.imp_unitario;

                        eDetOrdenServicioEdit.Add(objRestante);
                    }

                    if (ctd_req - ctd_total < 0) obj.num_cantidad = obj.num_cantidad + (ctd_req - ctd_total);
                }

                bsDetalleOC.DataSource = eDetOrdenServicioEdit;
                gvDetalleOC.RefreshData();

                gvDetalleOC.PostEditor();
                for (int x = 0; x < gvDetalleOC.DataRowCount; x++)
                {
                    eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eOrdCom = gvDetalleOC.GetRow(x) as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;
                    subTotal = eOrdCom.imp_total_det + subTotal;
                }

                txtSubTotal.EditValue = Math.Round(subTotal, 2).ToString();
            }

            if (gvDetalleOC.FocusedColumn.Name == "colimp_unitario")
            {
                subTotal = 0;

                gvDetalleOC.PostEditor();
                for (int x = 0; x < gvDetalleOC.DataRowCount; x++)
                {
                    eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eOrdCom = gvDetalleOC.GetRow(x) as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;
                    eOrdCom.imp_total_det = eOrdCom.num_cantidad * eOrdCom.imp_unitario;
                    subTotal = subTotal + eOrdCom.imp_total_det;
                }

                txtSubTotal.EditValue = Math.Round(subTotal, 2).ToString();
            }
        }

        private void rbtnBuscarProveedorDet_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Busqueda("", "ProveedorDet");

            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eOrdComNue = gvDetalleOC.GetRow(gvDetalleOC.FocusedRowHandle) as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;

            if (codigoProveedorDet != "")
            {
                eOrdComNue.cod_proveedor_det = codigoProveedorDet;
                eOrdComNue.dsc_proveedor_det = dscProveedorDet;
                eOrdComNue.dsc_ruc_det = rucProveedorDet;
            }
            gvDetalleOC.RefreshData();
        }
    }
}