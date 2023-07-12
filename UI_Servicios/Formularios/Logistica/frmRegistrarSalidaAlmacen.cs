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
using UI_Servicios.Formularios.Shared;
using UI_Servicios.Formularios.Cuentas_Pagar;

namespace UI_Servicios.Formularios.Logistica
{
    internal enum SalidaAlmacen
    {
        Nuevo = 1,
        Editar = 2,
        Vista = 3
    }
    public partial class frmRegistrarSalidaAlmacen : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        internal SalidaAlmacen MiAccion = SalidaAlmacen.Nuevo;
        blFactura blFact = new blFactura();
        blProveedores blProv = new blProveedores();
        blLogistica blLogis = new blLogistica();
        blGlobales blGlobal = new blGlobales();
        blRequerimiento blReq = new blRequerimiento();
        List<eAlmacen.eProductos_Almacen> listaProd = new List<eAlmacen.eProductos_Almacen>();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public string cod_empresa = "", cod_sede_empresa = "", cod_almacen = "", cod_salida = "", cod_requerimiento = "", flg_solicitud = "", dsc_anho = "0";
        public bool ActualizarListado = false;


        public frmRegistrarSalidaAlmacen()
        {
            InitializeComponent();
        }

        private void frmRegistrarEntrada_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            dtFechaDocumento.EditValue = DateTime.Today;
            blLogis.CargaCombosLookUp("Almacen", lkpAlmacen, "cod_almacen", "dsc_almacen", "", valorDefecto: true, cod_empresa: cod_empresa, cod_sede_empresa: cod_sede_empresa);
            blLogis.CargaCombosLookUp("TipoMovimiento", lkpTipoMovimiento, "cod_tipo_movimiento", "dsc_tipo_movimiento", "", valorDefecto: true, dsc_variable: "SALIDA");
            blLogis.CargaCombosLookUp("DistribucionCECO", lkpDistribucionCECO, "cod_CECO", "dsc_CECO", "", valorDefecto: true, cod_empresa: cod_empresa);
            blLogis.CargaCombosLookUp("Almacen", lkpAlmacenDestino, "cod_almacen", "dsc_almacen", "", valorDefecto: true, cod_empresa: cod_empresa, cod_sede_empresa: "");

            switch (MiAccion)
            {
                case SalidaAlmacen.Nuevo:
                    dtFechaDocumento.EditValue = DateTime.Today;
                    dtFechaTipoCambio.EditValue = DateTime.Today;
                    lkpAlmacen.EditValue = cod_almacen; lkpTipoMovimiento.EditValue = "010";
                    if (cod_requerimiento != "")
                    {
                        eRequerimiento eReq = blReq.Cargar_Requerimiento<eRequerimiento>(4, cod_empresa, cod_sede_empresa, cod_requerimiento);
                        txtNroRequerimiento.Text = cod_requerimiento;
                        txtGlosaRequerimiento.Text = eReq.dsc_solicitante;
                        dtFechaRequerimiento.EditValue = eReq.fch_requerimiento;
                        listaProd = blLogis.Obtener_ListaLogistica<eAlmacen.eProductos_Almacen>(26, lkpAlmacen.EditValue.ToString(), cod_empresa, cod_sede_empresa, cod_requerimiento: cod_requerimiento);
                        bsListadoProductos.DataSource = listaProd; gvListadoProductos.RefreshData();
                    }
                    break;
                case SalidaAlmacen.Vista:
                    ObtenerDatos_SalidaAlmacen();
                    BloqueoControles(false, true, false);
                    gvListadoProductos.Columns["num_cantidad_stock"].Visible = false;
                    gvListadoProductos.Columns["num_cantidad_stock_nuevo"].Visible = false;
                    break;
            }
        }

        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            btnGuardar.Enabled = Enabled;
            txtCodigo.ReadOnly = ReadOnly;
            lkpAlmacen.ReadOnly = ReadOnly;
            lkpTipoMovimiento.ReadOnly = ReadOnly;
            dtFechaDocumento.ReadOnly = ReadOnly;
            txtNroRequerimiento.ReadOnly = ReadOnly;
            txtGlosaRequerimiento.ReadOnly = ReadOnly;
            dtFechaRequerimiento.ReadOnly = ReadOnly;
            dtFechaTipoCambio.ReadOnly = ReadOnly;
            txtTipoCambio.ReadOnly = ReadOnly;
            lkpDistribucionCECO.ReadOnly = ReadOnly;
            lkpAlmacenDestino.ReadOnly = ReadOnly;
            picBuscarRequerimiento.Enabled = Enabled;
            gvListadoProductos.OptionsBehavior.Editable = Editable;
        }

        private void ObtenerDatos_SalidaAlmacen()
        {
            eAlmacen.eSalida_Cabecera obj = new eAlmacen.eSalida_Cabecera();
            obj = blLogis.Obtener_DatosLogistica<eAlmacen.eSalida_Cabecera>(22, cod_almacen, cod_empresa, cod_sede_empresa, "", cod_salida);
            txtCodigo.Text = obj.cod_salida;
            lkpAlmacen.EditValue = obj.cod_almacen;
            lkpTipoMovimiento.EditValue = obj.cod_tipo_movimiento;
            dtFechaDocumento.EditValue = obj.fch_documento;
            txtNroRequerimiento.Text = obj.cod_requerimiento;
            txtGlosaRequerimiento.Text = obj.dsc_solicitante;
            dtFechaRequerimiento.EditValue = obj.fch_requerimiento;
            dtFechaTipoCambio.EditValue = obj.fch_tipocambio;
            txtTipoCambio.EditValue = obj.imp_tipocambio;
            lkpDistribucionCECO.EditValue = obj.dsc_pref_ceco;
            lkpAlmacenDestino.EditValue = obj.cod_almacen_destino;

            listaProd = blLogis.Obtener_ListaLogistica<eAlmacen.eProductos_Almacen>(23, cod_almacen, cod_empresa, cod_sede_empresa, cod_salida: cod_salida);
            bsListadoProductos.DataSource = listaProd; gvListadoProductos.RefreshData();
        }

        private void picBuscarRequerimiento_Click(object sender, EventArgs e)
        {
            Busqueda("", "Requerimiento");
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
                case "Requerimiento":
                    frm.entidad = frmBusquedas.MiEntidad.Requerimiento;
                    frm.cod_almacen = cod_almacen;
                    frm.cod_empresa = cod_empresa;
                    frm.cod_sede_empresa = cod_sede_empresa;
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Requerimiento":
                    txtNroRequerimiento.Text = frm.codigo;
                    txtGlosaRequerimiento.Text = frm.descripcion;
                    dtFechaRequerimiento.EditValue = frm.fch_generica;

                    listaProd = blLogis.Obtener_ListaLogistica<eAlmacen.eProductos_Almacen>(26, lkpAlmacen.EditValue.ToString(), cod_empresa, cod_sede_empresa, cod_requerimiento: frm.codigo);
                    bsListadoProductos.DataSource = listaProd; gvListadoProductos.RefreshData();
                    break;
            }
        }

        private void dtFechaTipoCambio_EditValueChanged(object sender, EventArgs e)
        {
            if (MiAccion == SalidaAlmacen.Nuevo) TraerTipoCambio();
        }

        private void frmRegistrarSalidaAlmacen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && MiAccion != SalidaAlmacen.Nuevo) this.Close();
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

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
                if (lkpTipoMovimiento.EditValue == null) { MessageBox.Show("Debe seleccionar el tipo de movimiento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoMovimiento.Focus(); return; }
                if (txtNroRequerimiento.Text.Trim() == "") { MessageBox.Show("Debe seleccionar la orden de compra.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNroRequerimiento.Focus(); return; }
                if (lkpDistribucionCECO.EditValue == null) { MessageBox.Show("Debe seleccionar el centro de costo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpDistribucionCECO.Focus(); return; }
                //if (lkpAlmacenDestino.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen de destino.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacenDestino.Focus(); return; }
                int nTotal = 0;
                foreach (eAlmacen.eProductos_Almacen obj in listaProd)
                {
                    if (obj.num_cantidad_stock_nuevo < 0) nTotal = nTotal + 1;
                }
                if (nTotal > 0) { MessageBox.Show("La cantidad del producto no puede ser mayor a la del stock actual.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                eAlmacen.eSalida_Cabecera eSalida = AsignarValores_Cabecera();
                eSalida = blLogis.Insertar_Actualizar_SalidaCabecera<eAlmacen.eSalida_Cabecera>(eSalida);
                if (eSalida != null)
                {
                    txtCodigo.Text = eSalida.cod_salida;
                    if (gvListadoProductos.RowCount > 0)
                    {
                        for (int nRow = 0; nRow < gvListadoProductos.RowCount; nRow++)
                        {
                            eAlmacen.eProductos_Almacen eProd = gvListadoProductos.GetRow(nRow) as eAlmacen.eProductos_Almacen;
                            if (eProd.num_cantidad == 0) continue;
                            eAlmacen.eSalida_Detalle eDet = new eAlmacen.eSalida_Detalle();
                            eDet.cod_salida = eSalida.cod_salida;
                            eDet.cod_almacen = cod_almacen;
                            eDet.cod_empresa = cod_empresa;
                            eDet.cod_sede_empresa = cod_sede_empresa;
                            eDet.cod_tipo_servicio = eProd.cod_tipo_servicio;
                            eDet.cod_subtipo_servicio = eProd.cod_subtipo_servicio;
                            eDet.cod_producto = eProd.cod_producto;
                            eDet.cod_unidad_medida = eProd.cod_unidad_medida;
                            eDet.num_cantidad = eProd.num_cantidad;
                            eDet.cod_usuario_registro = user.cod_usuario;

                            eDet = blLogis.Insertar_Actualizar_SalidaDetalle<eAlmacen.eSalida_Detalle>(eDet);
                            if (eDet == null) MessageBox.Show("Error al registrar producto", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    //if (lkpAlmacenDestino.EditValue != null)
                    //{
                    //    eAlmacen.eEntrada_Cabecera eEntr = AsignarValores_CabeceraIngreso();
                    //    eEntr = blLogis.Insertar_Actualizar_EntradaCabecera<eAlmacen.eEntrada_Cabecera>(eEntr);
                    //    if (gvListadoProductos.RowCount > 0)
                    //    {
                    //        for (int nRow2 = 0; nRow2 < gvListadoProductos.RowCount; nRow2++)
                    //        {
                    //            eAlmacen.eProductos_Almacen eProd = gvListadoProductos.GetRow(nRow2) as eAlmacen.eProductos_Almacen;
                    //            if (eProd.num_cantidad_recibido == 0) continue;
                    //            eAlmacen.eEntrada_Detalle eDet = new eAlmacen.eEntrada_Detalle();
                    //            eDet.cod_entrada = eEntr.cod_entrada;
                    //            eDet.cod_almacen = cod_almacen;
                    //            eDet.cod_empresa = cod_empresa;
                    //            eDet.cod_sede_empresa = cod_sede_empresa;
                    //            eDet.cod_tipo_servicio = eProd.cod_tipo_servicio;
                    //            eDet.cod_subtipo_servicio = eProd.cod_subtipo_servicio;
                    //            eDet.cod_producto = eProd.cod_producto;
                    //            eDet.cod_unidad_medida = eProd.cod_unidad_medida;
                    //            eDet.num_cantidad = eProd.num_cantidad;
                    //            eDet.num_cantidad_recibido = eProd.num_cantidad_recibido;
                    //            eDet.num_cantidad_x_recibir = eProd.num_cantidad_x_recibir;
                    //            eDet.num_item_costo = eProd.num_item_costo;
                    //            eDet.imp_costo = eProd.imp_costo;
                    //            eDet.imp_total = eProd.imp_total;
                    //            eDet.cod_usuario_registro = user.cod_usuario;

                    //            eDet = blLogis.Insertar_Actualizar_EntradaDetalle<eAlmacen.eEntrada_Detalle>(eDet);
                    //            if (eDet == null) MessageBox.Show("Error al registrar producto", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        }
                    //    }
                    //}
                    ActualizarListado = true;
                    MessageBox.Show("Se realizó la salida de productos de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MiAccion = SalidaAlmacen.Editar;
                }
                else
                {
                    MessageBox.Show("Error al registrar salida", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private eAlmacen.eEntrada_Cabecera AsignarValores_CabeceraIngreso()
        {
            eAlmacen.eEntrada_Cabecera obj = new eAlmacen.eEntrada_Cabecera();
            obj.cod_entrada = txtCodigo.Text;
            obj.cod_almacen = cod_almacen;
            obj.cod_tipo_movimiento = "003";
            obj.dsc_glosa = "TRASLADO ENTRE ALMACEN";
            obj.cod_empresa = cod_empresa;
            obj.cod_sede_empresa = cod_sede_empresa;
            obj.cod_orden_compra_servicio = "";
            obj.fch_documento = Convert.ToDateTime(dtFechaDocumento.EditValue);
            obj.fch_tipocambio = Convert.ToDateTime(dtFechaTipoCambio.EditValue);
            obj.imp_tipocambio = Convert.ToDecimal(txtTipoCambio.EditValue);
            obj.tipo_documento = "";
            obj.serie_documento = "";
            obj.numero_documento = 0;
            obj.cod_proveedor = "";
            obj.flg_activo = "SI";
            obj.cod_usuario_registro = user.cod_usuario;

            return obj;
        }
        private eAlmacen.eSalida_Cabecera AsignarValores_Cabecera()
        {
            eAlmacen.eSalida_Cabecera obj = new eAlmacen.eSalida_Cabecera();
            obj.cod_salida = txtCodigo.Text;
            obj.cod_almacen = cod_almacen;
            obj.cod_tipo_movimiento = lkpTipoMovimiento.EditValue.ToString();
            obj.cod_empresa = cod_empresa;
            obj.cod_sede_empresa = cod_sede_empresa;
            obj.cod_requerimiento = txtNroRequerimiento.Text;
            obj.fch_documento = Convert.ToDateTime(dtFechaDocumento.EditValue);
            obj.fch_tipocambio = Convert.ToDateTime(dtFechaTipoCambio.EditValue);
            obj.imp_tipocambio = Convert.ToDecimal(txtTipoCambio.EditValue);
            obj.dsc_pref_ceco = lkpDistribucionCECO.EditValue.ToString();
            obj.cod_almacen_destino = lkpAlmacenDestino.EditValue == null ? null : lkpAlmacenDestino.EditValue.ToString();
            obj.flg_activo = "SI";
            obj.cod_usuario_registro = user.cod_usuario;

            return obj;
        }


        private void rbtnEliminar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MiAccion == SalidaAlmacen.Nuevo)
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

        private void gvListadoProductos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoProductos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoProductos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    string colName = e.Column.FieldName;
                    eAlmacen.eProductos_Almacen objProd = gvListadoProductos.GetRow(e.RowHandle) as eAlmacen.eProductos_Almacen;
                    if (colName == "num_cantidad_stock" || colName == "num_cantidad_stock_nuevo") e.Appearance.ForeColor = Color.Blue;
                    if (colName == "num_cantidad_stock" && objProd.num_cantidad_stock <= 0) e.Appearance.ForeColor = Color.Red;
                    if (colName == "num_cantidad_stock_nuevo" && objProd.num_cantidad_stock_nuevo <= 0) e.Appearance.ForeColor = Color.Red;
                    e.DefaultDraw();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoProductos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                eAlmacen.eProductos_Almacen objProd = gvListadoProductos.GetFocusedRow() as eAlmacen.eProductos_Almacen;
                if (objProd != null)
                {
                    if (e.Column.FieldName == "num_cantidad")
                    {
                        if (objProd.num_cantidad > objProd.num_cantidad_x_recibir)
                        {
                            MessageBox.Show("No puede digitar una cantidad mayor al requerimiento inicial", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            objProd.num_cantidad = objProd.num_cantidad_x_recibir;

                            if (objProd.num_cantidad > objProd.num_cantidad_stock)
                            {
                                MessageBox.Show("No puede digitar una cantidad mayor a la del stock", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                objProd.num_cantidad = objProd.num_cantidad_stock;
                                objProd.num_cantidad_stock_nuevo = 0;
                                gvListadoProductos.RefreshData();
                                return;
                            }
                            gvListadoProductos.RefreshData();
                            return;
                        }
                        if (objProd.num_cantidad > objProd.num_cantidad_stock)
                        {
                            MessageBox.Show("No puede digitar una cantidad mayor a la del stock", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            objProd.num_cantidad = objProd.num_cantidad_stock;
                            objProd.num_cantidad_stock_nuevo = 0;
                            gvListadoProductos.RefreshData();
                            return;
                        }
                        objProd.num_cantidad_stock_nuevo = objProd.num_cantidad_stock - objProd.num_cantidad;
                    }
                    gvListadoProductos.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}