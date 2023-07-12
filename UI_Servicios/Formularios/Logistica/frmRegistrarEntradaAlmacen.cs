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
using BE_Servicios;
using BL_Servicios;
using UI_Servicios.Formularios.Cuentas_Pagar;
using UI_Servicios.Formularios.Shared;

namespace UI_Servicios.Formularios.Logistica
{
    internal enum IngresoAlmacen
    {
        Nuevo = 1,
        Editar = 2,
        Vista = 3
    }
    public partial class frmRegistrarEntradaAlmacen : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        internal IngresoAlmacen MiAccion = IngresoAlmacen.Nuevo;
        blFactura blFact = new blFactura();
        blLogistica blLogis = new blLogistica();
        blGlobales blGlobal = new blGlobales();
        blOrdenCompra_Servicio blOrdCom = new blOrdenCompra_Servicio();
        List<eAlmacen.eProductos_Almacen> listaProd = new List<eAlmacen.eProductos_Almacen>();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        string fmt_nro_doc = "";
        Int16 num_ctd_serie, num_ctd_doc;
        public string cod_empresa = "", cod_sede_empresa = "", cod_almacen = "", cod_entrada = "", cod_orden_compra_servicio = "", flg_solicitud = "", dsc_anho = "0";
        public bool ActualizarListado = false;

        public frmRegistrarEntradaAlmacen()
        {
            InitializeComponent();
        }

        private void frmRegistrarEntrada_Load(object sender, EventArgs e)
        {
            Inicializar();
        }


        private void Inicializar()
        {
            CargarCombosGridLookup("TipoComprobante", glkpTipoDocumento, "cod_tipo_comprobante", "dsc_tipo_comprobante", "", valorDefecto: false);
            blLogis.CargaCombosLookUp("TipoMovimiento", lkpTipoMovimiento, "cod_tipo_movimiento", "dsc_tipo_movimiento", "", valorDefecto: true, dsc_variable: "ENTRADA");
            blLogis.CargaCombosLookUp("Almacen", lkpAlmacen, "cod_almacen", "dsc_almacen", "", valorDefecto: true, cod_empresa: cod_empresa, cod_sede_empresa: cod_sede_empresa);

            switch (MiAccion)
            {
                case IngresoAlmacen.Nuevo:
                    dtFechaDocumento.EditValue = DateTime.Today;
                    dtFechaTipoCambio.EditValue = DateTime.Today;
                    lkpAlmacen.EditValue = cod_almacen; lkpTipoMovimiento.EditValue = "003";
                    if (cod_orden_compra_servicio != "")
                    {
                        eOrdenCompra_Servicio eOrden = blOrdCom.Cargar_OrdenCompra_Servicio<eOrdenCompra_Servicio>(2, cod_empresa, cod_sede_empresa, cod_orden_compra_servicio, "C", Convert.ToInt32(dsc_anho));
                        txtNroOC.Text = cod_orden_compra_servicio;
                        txtProveedorOC.Tag = eOrden.cod_proveedor;
                        txtProveedorOC.Text = eOrden.dsc_proveedor;
                        dtFechaOC.EditValue = eOrden.fch_emision;
                        listaProd = blLogis.Obtener_ListaLogistica<eAlmacen.eProductos_Almacen>(17, cod_empresa: cod_empresa, cod_sede_empresa: cod_sede_empresa, cod_orden_compra_servicio: cod_orden_compra_servicio);
                        bsListadoProductos.DataSource = listaProd; gvListadoProductos.RefreshData();
                    }

                    break;
                case IngresoAlmacen.Editar:
                    ObtenerDatos_EntradaAlmacen();
                    BloqueoControles(false, true, false);
                    if (glkpTipoDocumento.EditValue == null) { picBuscarDocumentos.Enabled = true; btnGuardar.Enabled = true; }
                    break;
            }
        }

        private void CargarCombosGridLookup(string nCombo, GridLookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", string cod_condicion = "", bool valorDefecto = false)
        {
            DataTable tabla = new DataTable();
            tabla = blFact.ObtenerListadoGridLookup(nCombo, cod_condicion);

            combo.Properties.DataSource = tabla;
            combo.Properties.ValueMember = campoValueMember;
            combo.Properties.DisplayMember = campoDispleyMember;
            if (campoSelectedValue == "") { combo.EditValue = null; } else { combo.EditValue = campoSelectedValue; }
            if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
        }

        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            btnGuardar.Enabled = Enabled;
            txtCodigo.ReadOnly = ReadOnly;
            lkpAlmacen.ReadOnly = ReadOnly;
            lkpTipoMovimiento.ReadOnly = ReadOnly;
            dtFechaDocumento.ReadOnly = ReadOnly;
            txtGlosa.ReadOnly = ReadOnly;
            txtNroOC.ReadOnly = ReadOnly;
            txtProveedorOC.ReadOnly = ReadOnly;
            picBuscarProveedor.Enabled = Enabled;
            dtFechaOC.ReadOnly = ReadOnly;
            dtFechaTipoCambio.ReadOnly = ReadOnly;
            txtTipoCambio.ReadOnly = ReadOnly;
            picBuscarDocumentos.Enabled = Enabled;
            glkpTipoDocumento.ReadOnly = ReadOnly;
            txtSerieDocumento.ReadOnly = ReadOnly;
            txtNumeroDocumento.ReadOnly = ReadOnly;
            txtRucProveedor.ReadOnly = ReadOnly;
            txtProveedor.ReadOnly = ReadOnly;
            chkAtenderTodo.Enabled = Enabled;
            gvListadoProductos.OptionsBehavior.Editable = Editable;
        }

        private void ObtenerDatos_EntradaAlmacen()
        {
            eAlmacen.eEntrada_Cabecera obj = new eAlmacen.eEntrada_Cabecera();
            obj = blLogis.Obtener_DatosLogistica<eAlmacen.eEntrada_Cabecera>(20, cod_almacen, cod_empresa, cod_sede_empresa, cod_entrada);
            txtCodigo.Text = obj.cod_entrada;
            lkpAlmacen.EditValue = obj.cod_almacen;
            lkpTipoMovimiento.EditValue = obj.cod_tipo_movimiento;
            dtFechaDocumento.EditValue = obj.fch_documento;
            txtGlosa.Text = obj.dsc_glosa;
            txtNroOC.Text = obj.cod_orden_compra_servicio;
            txtProveedorOC.Tag = obj.cod_proveedor;
            txtProveedorOC.Text = obj.dsc_proveedor;
            dtFechaOC.EditValue = obj.fch_documentoOC;
            dtFechaTipoCambio.EditValue = obj.fch_tipocambio;
            txtTipoCambio.EditValue = obj.imp_tipocambio;
            glkpTipoDocumento.EditValue = obj.tipo_documento;
            if (glkpTipoDocumento.EditValue != null)
            {
                eTipoComprobante objTC = new eTipoComprobante();
                objTC = blFact.BuscarTipoComprobante<eTipoComprobante>(27, glkpTipoDocumento.EditValue.ToString());
                num_ctd_serie = objTC.num_ctd_serie; num_ctd_doc = objTC.num_ctd_doc;
                fmt_nro_doc = new string('0', num_ctd_doc);
            }
            txtSerieDocumento.Text = obj.serie_documento;
            txtNumeroDocumento.Text = obj.numero_documento == 0 ? "" : String.Format("{0:" + fmt_nro_doc + "}", obj.numero_documento);  //$"{eFact.numero_documento:00000000}";
            txtRucProveedor.Text = obj.dsc_ruc;
            txtProveedor.Tag = obj.cod_proveedor;
            txtProveedor.Text = obj.dsc_proveedor;

            listaProd = blLogis.Obtener_ListaLogistica<eAlmacen.eProductos_Almacen>(21, cod_almacen, cod_empresa, cod_sede_empresa, cod_entrada: cod_entrada);
            bsListadoProductos.DataSource = listaProd; gvListadoProductos.RefreshData();
        }

        private void dtFechaTipoCambio_EditValueChanged(object sender, EventArgs e)
        {
            if (MiAccion == IngresoAlmacen.Nuevo) TraerTipoCambio();
        }

        private void TraerTipoCambio()
        {
            eTipoCambio objj = blFact.BuscarTipoCambio<eTipoCambio>(9, Convert.ToDateTime(dtFechaTipoCambio.EditValue));
            if (objj != null)
            {
                txtTipoCambio.Text = objj.imp_cambio_venta.ToString();
            }
            else
            {
                MessageBox.Show("No existe tipo de cambio registrado para la fecha seleccionada", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTipoCambio.Text = "0.00";

            }
        }

        private void picBuscarProveedor_Click(object sender, EventArgs e)
        {
            Busqueda("", "OrdenesCompra");
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
                if (lkpTipoMovimiento.EditValue == null) { MessageBox.Show("Debe seleccionar el tipo de movimiento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoMovimiento.Focus(); return; }
                if (txtNroOC.Text.Trim() == "") { MessageBox.Show("Debe seleccionar la orden de compra.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNroOC.Focus(); return; }
                //if (glkpTipoDocumento.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); glkpTipoDocumento.Focus(); return; }
                //if (txtSerieDocumento.Text.Trim() == "") { MessageBox.Show("Debe ingresar una serie de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtSerieDocumento.Focus(); return; }
                //if (txtNumeroDocumento.Text.Trim() == "") { MessageBox.Show("Debe ingresar un numero de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNumeroDocumento.Focus(); return; }
                if (txtProveedorOC.Text.Trim() == "") { MessageBox.Show("Debe seleccionar proveedor.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtProveedor.Focus(); return; }
                if (txtGlosa.Text.Trim() == "") { MessageBox.Show("Debe ingresar una glosa.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtGlosa.Focus(); return; }

                eAlmacen.eEntrada_Cabecera eEntr = AsignarValores_Cabecera();
                eEntr = blLogis.Insertar_Actualizar_EntradaCabecera<eAlmacen.eEntrada_Cabecera>(eEntr);

                if (eEntr != null)
                {
                    txtCodigo.Text = eEntr.cod_entrada;
                    if (gvListadoProductos.RowCount > 0)
                    {
                        for (int nRow = 0; nRow < gvListadoProductos.RowCount; nRow++)
                        {
                            eAlmacen.eProductos_Almacen eProd = gvListadoProductos.GetRow(nRow) as eAlmacen.eProductos_Almacen;
                            if (eProd.num_cantidad_recibido == 0) continue;
                            eAlmacen.eEntrada_Detalle eDet = new eAlmacen.eEntrada_Detalle();
                            eDet.cod_entrada = eEntr.cod_entrada;
                            eDet.cod_almacen = cod_almacen;
                            eDet.cod_empresa = cod_empresa;
                            eDet.cod_sede_empresa = cod_sede_empresa;
                            eDet.cod_tipo_servicio = eProd.cod_tipo_servicio;
                            eDet.cod_subtipo_servicio = eProd.cod_subtipo_servicio;
                            eDet.cod_producto = eProd.cod_producto;
                            eDet.cod_unidad_medida = eProd.cod_unidad_medida;
                            eDet.num_cantidad = eProd.num_cantidad;
                            eDet.num_cantidad_recibido = eProd.num_cantidad_recibido;
                            eDet.num_cantidad_x_recibir = eProd.num_cantidad_x_recibir;
                            eDet.num_item_costo = eProd.num_item_costo;
                            eDet.imp_costo = eProd.imp_costo;
                            eDet.imp_total = eProd.imp_total;
                            eDet.cod_usuario_registro = user.cod_usuario;

                            eDet = blLogis.Insertar_Actualizar_EntradaDetalle<eAlmacen.eEntrada_Detalle>(eDet);
                            if (eDet == null) MessageBox.Show("Error al registrar producto", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    /////////Validamos OC para pasarlo a ATENTDIDO/////////////////////////////////
                    eOrdenCompra_Servicio eOC = new eOrdenCompra_Servicio(); string respuesta = "";
                    eOC = blLogis.Obtener_DatosLogistica<eOrdenCompra_Servicio>(30, cod_almacen, cod_empresa, cod_sede_empresa, cod_orden_compra_servicio: txtNroOC.Text.Trim());
                    if (eOC != null && eOC.ctd_Atencion == 2) respuesta = blOrdCom.Atender_Orden(cod_empresa, cod_sede_empresa, txtNroOC.Text.Trim(), "C", Convert.ToInt32(dsc_anho), user.cod_usuario);

                    ActualizarListado = true;
                    MessageBox.Show("Se ingresaron los productos de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MiAccion = IngresoAlmacen.Editar;
                }
                else
                {
                    MessageBox.Show("Error al registrar ingreso", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private eAlmacen.eEntrada_Cabecera AsignarValores_Cabecera()
        {
            eAlmacen.eEntrada_Cabecera obj = new eAlmacen.eEntrada_Cabecera();
            obj.cod_entrada = txtCodigo.Text;
            obj.cod_almacen = cod_almacen;
            obj.cod_tipo_movimiento = lkpTipoMovimiento.EditValue.ToString();
            obj.dsc_glosa = txtGlosa.Text;
            obj.cod_empresa = cod_empresa;
            obj.cod_sede_empresa = cod_sede_empresa;
            obj.cod_orden_compra_servicio = txtNroOC.Text.Trim();
            obj.fch_documento = Convert.ToDateTime(dtFechaDocumento.EditValue);
            obj.fch_tipocambio = Convert.ToDateTime(dtFechaTipoCambio.EditValue);
            obj.imp_tipocambio = Convert.ToDecimal(txtTipoCambio.EditValue);
            obj.tipo_documento = glkpTipoDocumento.EditValue == null ? null : glkpTipoDocumento.EditValue.ToString();
            obj.serie_documento = txtSerieDocumento.Text;
            obj.numero_documento = txtNumeroDocumento.Text == "" ? 0 : Convert.ToDecimal(txtNumeroDocumento.Text);
            obj.cod_proveedor = txtProveedorOC.Tag.ToString();
            obj.flg_activo = "SI";
            obj.cod_usuario_registro = user.cod_usuario;

            return obj;
        }

        public void Busqueda(string dato, string tipo)
        {
            if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }

            frmBusquedas frm = new frmBusquedas();
            frm.user = user;
            frm.filtro = dato;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            switch (tipo)
            {
                case "OrdenesCompra":
                    frm.entidad = frmBusquedas.MiEntidad.OrdenesCompra;
                    frm.cod_almacen = lkpAlmacen.EditValue.ToString();
                    frm.cod_empresa = cod_empresa;
                    frm.cod_sede_empresa = cod_sede_empresa;
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "OrdenesCompra":
                    txtNroOC.Text = frm.codigo;
                    txtProveedorOC.Tag = frm.cod_condicion1;
                    txtProveedorOC.Text = frm.descripcion;
                    dtFechaOC.EditValue = frm.fch_generica;

                    //listaProd = blLogis.Obtener_ListaLogistica<eAlmacen.eProductos_Almacen>(5, cod_orden_compra: frm.codigo);
                    listaProd = blLogis.Obtener_ListaLogistica<eAlmacen.eProductos_Almacen>(17, cod_empresa: cod_empresa, cod_sede_empresa: cod_sede_empresa, cod_orden_compra_servicio: frm.codigo);
                    bsListadoProductos.DataSource = listaProd; gvListadoProductos.RefreshData();
                    break;
            }
        }

        private void frmRegistrarEntradaAlmacen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && MiAccion != IngresoAlmacen.Nuevo) this.Close();
        }

        private void picBuscarDocumentos_Click(object sender, EventArgs e)
        {
            if (txtProveedorOC.Tag == null || txtProveedorOC.Tag.ToString().Trim() == "") { MessageBox.Show("Debe seleccionar una Orden de Compra.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNroOC.Focus(); return; }

            frmFacturasDetalle frm = new frmFacturasDetalle();
            frm.BusquedaAutomatica = false;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorFocus = colorFocus;
            frm.colorEventRow = colorEventRow;
            frm.cod_empresa = cod_empresa;
            frm.cod_proveedor = txtProveedorOC.Tag.ToString();
            frm.cod_moneda = "SOL";
            frm.BusquedaLogistica = true;
            frm.user = user;
            frm.ShowDialog();
            if (frm.listDocumentosNC.Count > 0)
            {
                eFacturaProveedor eFact = new eFacturaProveedor();
                eFact = blFact.ObtenerFacturaProveedor<eFacturaProveedor>(2, frm.listDocumentosNC[0].tipo_documento, frm.listDocumentosNC[0].serie_documento, frm.listDocumentosNC[0].numero_documento, frm.listDocumentosNC[0].cod_proveedor);

                eTipoComprobante obj = new eTipoComprobante();
                obj = blFact.BuscarTipoComprobante<eTipoComprobante>(27, eFact.tipo_documento);
                num_ctd_serie = obj.num_ctd_serie; num_ctd_doc = obj.num_ctd_doc;
                fmt_nro_doc = new string('0', num_ctd_doc);

                glkpTipoDocumento.EditValue = eFact.tipo_documento;
                txtSerieDocumento.Text = eFact.serie_documento;
                txtNumeroDocumento.Text = String.Format("{0:" + fmt_nro_doc + "}", eFact.numero_documento);  //$"{eFact.numero_documento:00000000}";
                txtRucProveedor.Text = eFact.dsc_ruc;
                txtProveedor.Tag = eFact.cod_proveedor;
                txtProveedor.Text = eFact.dsc_proveedor;
            }
        }

        private void gvListadoProductos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoProductos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoProductos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                eAlmacen.eProductos_Almacen objProd = gvListadoProductos.GetFocusedRow() as eAlmacen.eProductos_Almacen;
                if (objProd != null)
                {
                    if (e.Column.FieldName == "num_cantidad_recibido")
                    {
                        if (objProd.num_cantidad_recibido > objProd.num_cantidad_x_recibir_interno)
                        {
                            MessageBox.Show("No puede ingresar una cantidad mayor a la que falta recibir", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            objProd.num_cantidad_recibido = objProd.num_cantidad_x_recibir_interno;
                            objProd.num_cantidad_x_recibir = 0;
                            gvListadoProductos.RefreshData();
                            return;
                        }

                        objProd.num_cantidad_x_recibir = objProd.num_cantidad_x_recibir_interno - objProd.num_cantidad_recibido;
                        objProd.imp_total = objProd.imp_costo * objProd.num_cantidad_recibido;
                    }
                    gvListadoProductos.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chkAtenderTodo_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                for (int nRow = 0; nRow < gvListadoProductos.RowCount; nRow++)
                {
                    eAlmacen.eProductos_Almacen objProd = gvListadoProductos.GetRow(nRow) as eAlmacen.eProductos_Almacen;
                    if (objProd == null) continue;
                    objProd.num_cantidad_recibido = chkAtenderTodo.CheckState == CheckState.Checked ? objProd.num_cantidad_x_recibir_interno : 0;
                    objProd.num_cantidad_x_recibir = objProd.num_cantidad_x_recibir_interno - objProd.num_cantidad_recibido;
                    objProd.imp_total = objProd.imp_costo * objProd.num_cantidad_recibido;
                }
                gvListadoProductos.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rbtnEliminar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MiAccion == IngresoAlmacen.Nuevo)
            {
                eAlmacen.eProductos_Almacen objP = gvListadoProductos.GetFocusedRow() as eAlmacen.eProductos_Almacen;
                listaProd.Remove(objP);
                int n_Orden = 1;
                foreach (eAlmacen.eProductos_Almacen obj in listaProd)
                {
                    obj.n_Orden = n_Orden;
                    n_Orden += 1;
                }
                bsListadoProductos.DataSource = listaProd;
                gvListadoProductos.RefreshData();
            }
        }

        private void gvListadoProductos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eAlmacen.eProductos_Almacen objProd = gvListadoProductos.GetRow(e.RowHandle) as eAlmacen.eProductos_Almacen;
                    if (e.Column.FieldName == "num_cantidad_recibido") e.Appearance.ForeColor = Color.Blue;
                    if (e.Column.FieldName == "num_cantidad_x_recibir" && objProd.num_cantidad_x_recibir > 0) e.Appearance.ForeColor = Color.Red;
                    e.DefaultDraw();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}