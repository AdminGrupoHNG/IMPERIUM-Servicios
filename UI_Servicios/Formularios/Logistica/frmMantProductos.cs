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
using UI_Servicios.Formularios.Shared;
using BE_Servicios;
using BL_Servicios;
using UI_Servicios.Formularios.Clientes_Y_Proveedores.Proveedores;
using DevExpress.XtraGrid.Views.Grid;

namespace UI_Servicios.Formularios.Logistica
{
    internal enum Producto
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2,
        Clonar = 3
    }
    public partial class frmMantProductos : DevExpress.XtraEditors.XtraForm
    {
        frmListadoProductoPrecios frmHandler;
        internal Producto MiAccion = Producto.Nuevo;
        public eUsuario user = new eUsuario();
        blSistema blSist = new blSistema();
        blGlobales blGlobal = new blGlobales();
        blLogistica blLogis = new blLogistica();
        blProveedores blProv = new blProveedores();
        blTrabajador blTrab = new blTrabajador();
        blProductos_Empresa blProdEmp = new blProductos_Empresa();
        List<eProductos.eProductosTarifas> listHistoricoTarifas = new List<eProductos.eProductosTarifas>();
        List<eProveedor> listProveedores = new List<eProveedor>();
        List<eProductos.eSubProductos> listSubProductos = new List<eProductos.eSubProductos>();
        public string cod_tipo_servicio = "", cod_subtipo_servicio = "", cod_producto = "", cod_empresa = "";
        string cod_proveedor = "", dsc_proveedor = "", dsc_ruc = "";
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        Image ImgVigente = DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/apply_16x16.png");        
        eProductos objProd = new eProductos();
        public bool ActualizarListado = false;

        public frmMantProductos()
        {
            InitializeComponent();
        }
        
        public frmMantProductos(frmListadoProductoPrecios frm)
        {
            InitializeComponent();
            frmHandler = frm;
        }

        private void frmMantAgregarProductos_Load(object sender, EventArgs e)
        {
            blLogis.CargaCombosLookUp("Color", lkpColor, "cod_color", "dsc_color", "", valorDefecto: true);
            blLogis.CargaCombosLookUp("UnidadMedida", lkpUndMedida, "cod_unidad_medida", "dsc_unidad_medida", "", valorDefecto: true);
            blLogis.CargaCombosLookUp("TipoProducto", lkpTipoProducto, "cod_tipo_servicio", "dsc_tipo_servicio", "", valorDefecto: true, cod_empresa: cod_empresa);
            blLogis.CargaCombosLookUp("Marca", lkpMarca, "cod_marca", "dsc_marca", "", valorDefecto: true);
            blTrab.CargaCombosLookUp("TallasUniforme", lkpTalla, "cod_tallauniforme", "dsc_tallauniforme", "", valorDefecto: true);
            blLogis.CargaCombosLookUp("Sexo", lkpSexo, "cod_sexo", "dsc_sexo", "", valorDefecto: true);
            simpleLabelItem1.AppearanceItemCaption.ForeColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);

            ObtenerListadoEmpresasCliente();

            switch (MiAccion)
            {
                case Producto.Nuevo:
                    gcHistoricoTarifas.Enabled = false;
                    btnClonar.Enabled = false;
                    break;
                case Producto.Editar:
                    ObtenerDatos_Producto();
                    break;
                case Producto.Clonar:
                    ObtenerDatos_Producto();
                    cod_producto = "";
                    txtCodProducto.Text = "";
                    btnClonar.Enabled = false;
                    listHistoricoTarifas.Clear(); gcHistoricoTarifas.DataSource = listHistoricoTarifas; gvHistoricoTarifas.RefreshData();
                    gcHistoricoTarifas.Enabled = false;
                    break;
            }
        }

        private void ObtenerDatos_Producto()
        {
            try
            {
                objProd = blLogis.Obtener_DatosProducto<eProductos>(10, cod_tipo_servicio: cod_tipo_servicio, cod_subtipo_servicio: cod_subtipo_servicio, cod_producto: cod_producto);
                txtCodProductoSUNAT.Text = objProd.cod_producto_SUNAT;
                txtDescProductoSUNAT.Text = objProd.dsc_producto_SUNAT;
                lkpColor.EditValue = objProd.cod_color == null ? null : objProd.cod_color;
                txtPeso.EditValue = objProd.num_peso;
                lkpUndMedida.EditValue = objProd.cod_unidad_medida == null ? null : objProd.cod_unidad_medida;
                txtModeloProducto.Text = objProd.dsc_modelo;
                lkpTipoProducto.EditValue = objProd.cod_tipo_servicio;
                lkpSubTipoProducto.EditValue = objProd.cod_subtipo_servicio;
                lkpTalla.EditValue = objProd.cod_tallauniforme == null ? null : objProd.cod_tallauniforme;
                lkpSexo.EditValue = objProd.cod_sexo == null ? null : objProd.cod_sexo;
                lkpMarca.EditValue = objProd.cod_marca == null || objProd.cod_marca == "" ? 0 : Convert.ToInt32(objProd.cod_marca);
                lkpTalla.EditValue = objProd.cod_tallauniforme == null || objProd.cod_tallauniforme.Trim() == "" ? null : objProd.cod_tallauniforme;
                lkpSexo.EditValue = objProd.cod_sexo == null || objProd.cod_sexo.Trim() == "" ? null : objProd.cod_sexo;
                txtStockMinimo.EditValue = objProd.ctd_stock_minimo;
                chkflgCompuesto.CheckState = objProd.flg_compuesto == "SI" ? CheckState.Checked : CheckState.Unchecked;
                chkllgLogo.CheckState = objProd.flg_logo == "SI" ? CheckState.Checked : CheckState.Unchecked;
                txtCodProducto.Text = objProd.cod_producto;
                mmDescripcionProducto.Text = objProd.dsc_producto;
                ObtenerDatos_ListadoProveedores();
                //if (MiAccion == Producto.Clonar) txtCodProducto.Text = objProd.cod_producto_SUNAT + "-000";
                gcHistoricoTarifas.Enabled = true;
                xtabEmpresas.PageVisible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ObtenerDatos_ListadoProveedores()
        {
            listProveedores.Clear();
            listProveedores = blLogis.Obtener_ListadosProductos<eProveedor>(12, cod_tipo_servicio: cod_tipo_servicio, cod_subtipo_servicio: cod_subtipo_servicio, cod_producto: cod_producto);
            bsListaProveedores.DataSource = listProveedores; gvListaProveedores.RefreshData();
        }

        private void ObtenerDatos_HistoricoPrecios()
        {
            listHistoricoTarifas.Clear();
            listHistoricoTarifas = blLogis.Obtener_ListadosProductos<eProductos.eProductosTarifas>(8, cod_tipo_servicio: cod_tipo_servicio, cod_subtipo_servicio: cod_subtipo_servicio, cod_producto: cod_producto, cod_proveedor: cod_proveedor);
            bsHistoricoTarifas.DataSource = listHistoricoTarifas; gvHistoricoTarifas.RefreshData();
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            cod_tipo_servicio = ""; cod_subtipo_servicio = ""; cod_producto = "";
            txtCodProductoSUNAT.Text = "";
            txtDescProductoSUNAT.Text = "";
            lkpColor.EditValue = null;
            txtPeso.EditValue = 0;
            lkpUndMedida.EditValue = null;
            txtModeloProducto.Text = "";
            lkpTipoProducto.EditValue = null;
            lkpSubTipoProducto.EditValue = null;
            lkpTalla.EditValue = null;
            lkpSexo.EditValue = null;
            txtCodProducto.Text = "";
            lkpMarca.EditValue = null;
            lkpTalla.EditValue = null;
            lkpSexo.EditValue = null;
            txtStockMinimo.EditValue = 0;
            mmDescripcionProducto.Text = "";
            listProveedores.Clear(); bsListaProveedores.DataSource = listProveedores;
            gvListaProveedores.RefreshData();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //if (txtCodProducto.Text.Trim() == "") { MessageBox.Show("Debe seleccionar el producto.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtCodProducto.Focus(); return; }
                if (lkpMarca.EditValue == null) { MessageBox.Show("Debe seleccionar la marca.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpMarca.Focus(); return; }
                if (lkpUndMedida.EditValue == null) { MessageBox.Show("Debe seleccionar la unidad de medida.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpUndMedida.Focus(); return; }
                if (txtPeso.Text.Trim() == "") { MessageBox.Show("Debe ingresar el peso.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtPeso.Focus(); return; }
                if (lkpTipoProducto.EditValue == null) { MessageBox.Show("Debe seleccionar el tipo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoProducto.Focus(); return; }
                if (lkpSubTipoProducto.EditValue == null) { MessageBox.Show("Debe seleccionar el subtipo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSubTipoProducto.Focus(); return; }
                if (mmDescripcionProducto.Text.Trim() == "") { MessageBox.Show("Debe ingresar el nombre del producto.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); mmDescripcionProducto.Focus(); return; }
                //if (gvListaProveedores.RowCount == 0) { MessageBox.Show("Debe seleccionar un proveedor.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); gvListaProveedores.Focus(); return; }

                eProductos obj = new eProductos();
                obj.cod_tipo_servicio = lkpTipoProducto.EditValue.ToString(); 
                obj.cod_subtipo_servicio = lkpSubTipoProducto.EditValue.ToString();
                obj.cod_producto = txtCodProducto.Text;
                //obj.cod_productoREF = txtCodProducto.Text; 
                obj.dsc_producto = mmDescripcionProducto.Text.Trim();
                obj.dsc_observaciones = ""; 
                obj.cod_unidad_medida = lkpUndMedida.EditValue.ToString(); 
                obj.cod_producto_SUNAT = txtCodProductoSUNAT.Text;
                obj.cod_color = lkpColor.EditValue == null ? null : lkpColor.EditValue.ToString(); 
                obj.cod_marca = lkpMarca.EditValue == null ? null : lkpMarca.EditValue.ToString();
                obj.dsc_modelo = txtModeloProducto.Text.Trim();
                obj.num_peso = Convert.ToDecimal(txtPeso.Text); 
                obj.cod_tallauniforme = lkpTalla.EditValue == null ? null : lkpTalla.EditValue.ToString();
                obj.cod_sexo = lkpSexo.EditValue == null ? null : lkpSexo.EditValue.ToString();
                obj.ctd_stock_minimo = Convert.ToInt32(txtStockMinimo.Text);
                obj.flg_activo = "SI";
                obj.flg_compuesto = chkflgCompuesto.CheckState == CheckState.Checked ? "SI" : "NO";
                obj.flg_logo = chkllgLogo.CheckState == CheckState.Checked ? "SI" : "NO";
                obj = blLogis.Insertar_Actualizar_Producto<eProductos>(obj);
                if (obj == null) { MessageBox.Show("Error al crear producto", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                MessageBox.Show("Se creó el producto de manera satisfactoria.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cod_tipo_servicio = obj.cod_tipo_servicio; cod_subtipo_servicio = obj.cod_subtipo_servicio;
                cod_producto = obj.cod_producto;
                txtCodProducto.Text = cod_producto;

                for (int nRow = 0; nRow < gvListaProveedores.RowCount; nRow++)
                {
                    eProveedor objPR = gvListaProveedores.GetRow(nRow) as eProveedor;

                    eProveedor objProv = blLogis.Obtener_DatosProveedor<eProveedor>(14, objPR.cod_proveedor, lkpTipoProducto.EditValue.ToString());
                    if (objProv == null)
                    {
                        eProveedor_Servicios objPSR = new eProveedor_Servicios();
                        objPSR.cod_proveedor = objPR.cod_proveedor; objPSR.cod_tipo_servicio = obj.cod_tipo_servicio;
                        objPSR.flg_activo = "SI"; objPSR.cod_usuario_registro = user.cod_usuario;
                        objPSR = blProv.Guardar_Actualizar_ProveedorServicio<eProveedor_Servicios>(objPSR);
                    }

                    eProductos.eProductosProveedor eProv = new eProductos.eProductosProveedor();
                    eProv.cod_tipo_servicio = obj.cod_tipo_servicio; eProv.cod_subtipo_servicio = obj.cod_subtipo_servicio;
                    eProv.cod_producto = obj.cod_producto; eProv.dsc_ruc = objPR.num_documento; eProv.cod_proveedor = objPR.cod_proveedor;
                    eProv.flg_activo = "SI"; eProv.flg_vigente = objPR.flg_vigente; eProv.cod_usuario_registro = user.cod_usuario;
                    eProv = blLogis.Insertar_Actualizar_ProductosProveedor<eProductos.eProductosProveedor>(eProv);

                    eProveedor_Marca objj = new eProveedor_Marca();
                    objj.cod_marca = Convert.ToInt32(lkpMarca.EditValue);
                    objj.cod_proveedor = objPR.cod_proveedor;
                    objj.cod_usuario_registro = user.cod_usuario;
                    objj.flg_activo = "SI";
                    objj = blLogis.Insertar_Actualizar_MarcasProveedor<eProveedor_Marca>(objj);
                }

                for (int nRow = 0; nRow < gvListadoSubProductos.RowCount; nRow++)
                {
                    eProductos.eSubProductos objPRD = gvListadoSubProductos.GetRow(nRow) as eProductos.eSubProductos;
                    if (objPRD == null) continue;
                    objPRD = blLogis.Insertar_Actualizar_SubProducto<eProductos.eSubProductos>(objPRD);
                    if (objPRD == null) { MessageBox.Show("Error al insertar SubProducto", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                }

                eProductos_Empresa eProdEmp = new eProductos_Empresa();
                List<eFacturaProveedor> list = blProv.ListarEmpresasProveedor<eFacturaProveedor>(11, "", user.cod_usuario);
                eProdEmp.cod_empresa = list[0].cod_empresa;
                eProdEmp.cod_tipo_servicio = lkpTipoProducto.EditValue.ToString();
                eProdEmp.cod_subtipo_servicio = lkpSubTipoProducto.EditValue.ToString();
                eProdEmp.cod_producto = txtCodProducto.EditValue.ToString();
                eProdEmp.cod_cta_contable = "";

                eProdEmp = blProdEmp.Ins_Act_Requerimiento<eProductos_Empresa>(eProdEmp);

                MiAccion = Producto.Editar;
                btnClonar.Enabled = true;
                gcHistoricoTarifas.Enabled = true;
                ActualizarListado = true;
                xtabEmpresas.PageVisible = true;
                if (MiAccion == Producto.Clonar) btnClonar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void picBuscarProductoSUNAT_Click(object sender, EventArgs e)
        {
            BusquedaProductoSUNAT("");
        }

        private void picBuscarProveedor_Click(object sender, EventArgs e)
        {
            Busqueda("", "Proveedor");
        }

        private void txtDescProductoSUNAT_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                blGlobal.pKeyDown(txtDescProductoSUNAT, e);
                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) { txtDescProductoSUNAT.Text = ""; txtCodProductoSUNAT.Text = ""; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtDescProductoSUNAT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                BusquedaProductoSUNAT("");
            }
            string dato = blGlobal.pKeyPress(txtDescProductoSUNAT, e);
            if (dato != "")
            {
                BusquedaProductoSUNAT(dato);
            }
        }

        private void lkpTipoProducto_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpTipoProducto.EditValue != null) blLogis.CargaCombosLookUp("SubTipoProducto", lkpSubTipoProducto, "cod_subtipo_servicio", "dsc_subtipo_servicio", "", valorDefecto: true, cod_tipo_servicio: lkpTipoProducto.EditValue.ToString());
            if (lkpTipoProducto.EditValue == null) lkpSubTipoProducto.EditValue = null;
        }

        private void lkpColor_EditValueChanged(object sender, EventArgs e)
        {
            Construir_NombreProducto();
        }

        private void txtModeloProducto_EditValueChanged(object sender, EventArgs e)
        {
            Construir_NombreProducto();
        }

        private void txtPeso_EditValueChanged(object sender, EventArgs e)
        {
            Construir_NombreProducto();
        }

        private void lkpMarca_EditValueChanged(object sender, EventArgs e)
        {
            Construir_NombreProducto();
        }

        private void lkpSubTipoProducto_EditValueChanged(object sender, EventArgs e)
        {
            Construir_NombreProducto();
        }

        private void lkpUndMedida_EditValueChanged(object sender, EventArgs e)
        {
            Construir_NombreProducto();
        }

        private void lkpTalla_EditValueChanged(object sender, EventArgs e)
        {
            Construir_NombreProducto();
        }

        private void lkpSexo_EditValueChanged(object sender, EventArgs e)
        {
            Construir_NombreProducto();
        }

        private void chkllgLogo_CheckStateChanged(object sender, EventArgs e)
        {
            Construir_NombreProducto();
        }

        private void Construir_NombreProducto()
        {
            mmDescripcionProducto.Text =
                (lkpSubTipoProducto.EditValue == null ? "" : lkpSubTipoProducto.Text) + (txtModeloProducto.Text == "" ? "" : " " + txtModeloProducto.Text) +
                (lkpColor.EditValue == null ? "" : " " + lkpColor.Text) +
                (Convert.ToDecimal(txtPeso.Text) == 0 ? "" : (lkpUndMedida.EditValue == null ? 
                " - TALLA " + (Convert.ToDecimal(txtPeso.EditValue) % 1 == 0 ? Math.Floor(Convert.ToDecimal(txtPeso.EditValue)) : txtPeso.EditValue) : 
                " - " + (Convert.ToDecimal(txtPeso.EditValue) % 1 == 0 ? Math.Floor(Convert.ToDecimal(txtPeso.EditValue)) : txtPeso.EditValue))) +
                (lkpUndMedida.EditValue == null ? "" : " " + lkpUndMedida.Text) + (lkpSexo.EditValue == null ? "" : " " + lkpSexo.Text) +
                (lkpTalla.EditValue == null ? "" : " TALLA " + lkpTalla.Text) + 
                (lkpMarca.EditValue != null && lkpMarca.EditValue.ToString() == "27" ? "" : " " + lkpMarca.Text) +
                (chkllgLogo.CheckState == CheckState.Checked ? " (C/L)" : "");
        }

        private void gvHistoricoTarifas_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvHistoricoTarifas_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void frmMantProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (MiAccion == Producto.Editar && e.KeyCode == Keys.Escape) this.Close();
        }
        private void gvListaProveedores_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListaProveedores_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoSubProductos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoSubProductos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void btnClonar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MiAccion = Producto.Clonar;
            cod_producto = "";
            txtCodProducto.Text = "";
            btnClonar.Enabled = false;
            listHistoricoTarifas.Clear(); gcHistoricoTarifas.DataSource = listHistoricoTarifas; gvHistoricoTarifas.RefreshData();
            gcHistoricoTarifas.Enabled = false;
        }

        private void gvListaProveedores_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                eProveedor obj = gvListaProveedores.GetFocusedRow() as eProveedor;
                eProductos.eProductosProveedor objP = blLogis.Obtener_DatosProducto<eProductos.eProductosProveedor>(13, cod_tipo_servicio: cod_tipo_servicio, cod_subtipo_servicio: cod_subtipo_servicio, cod_producto: cod_producto, cod_proveedor: obj.cod_proveedor);
                if (objP != null)
                {
                    cod_proveedor = obj.cod_proveedor; dsc_proveedor = obj.dsc_proveedor; dsc_ruc = obj.num_documento;
                    ObtenerDatos_HistoricoPrecios();
                    gcHistoricoTarifas.Enabled = true;
                }
                else
                {
                    cod_proveedor = ""; dsc_proveedor = ""; dsc_ruc = "";
                    gcHistoricoTarifas.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rbtnEliminar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (MiAccion == Producto.Editar)
                {

                }
                else
                {
                    eProveedor obj = gvListaProveedores.GetFocusedRow() as eProveedor;
                    listProveedores.Remove(obj);
                    gcListaProveedores.DataSource = listProveedores;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rbtnEliminar2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (MiAccion == Producto.Editar)
                {

                }
                else
                {
                    eProductos.eSubProductos obj = gvListadoSubProductos.GetFocusedRow() as eProductos.eSubProductos;
                    listSubProductos.Remove(obj);
                    gcListadoSubProductos.DataSource = listSubProductos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvListaProveedores_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                gvListaProveedores_FocusedRowChanged(gvListaProveedores, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(gvListaProveedores.FocusedRowHandle - 1, gvListaProveedores.FocusedRowHandle));
               
                if (e.Clicks == 2)
                {
                    eProveedor obj = gvListaProveedores.GetRow(e.RowHandle) as eProveedor;
                    frmMantProveedor frm = new frmMantProveedor();
                    frm.cod_proveedor = obj.cod_proveedor;
                    frm.MiAccion = Proveedor.Vista;
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorEventRow = colorEventRow;
                    frm.colorFocus = colorFocus;
                    //frm.cod_empresa = lkpEmpresaProveedor.EditValue.ToString();
                    frm.user = user;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rchkFlgVigente_CheckStateChanged(object sender, EventArgs e)
        {
            gvListaProveedores.PostEditor();
        }

        private void gvListaProveedores_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eProveedor obj = gvListaProveedores.GetRow(e.RowHandle) as eProveedor;
                    if (obj == null) return;
                    if (e.Column.FieldName == "colFlgVigente" && obj.flg_vigente == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgVigente, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    e.DefaultDraw();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnConvertirVigente_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvListaProveedores.RowCount == 0) { MessageBox.Show("Debe terner vinculado 1 proveedor.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                eProveedor obj = gvListaProveedores.GetFocusedRow() as eProveedor;
                if (obj == null) return;
                if (MiAccion == Producto.Editar)
                {
                    for (int nRow = 0; nRow < gvListaProveedores.RowCount; nRow++)
                    {
                        eProveedor objPR = gvListaProveedores.GetRow(nRow) as eProveedor;

                        eProveedor objProv = blLogis.Obtener_DatosProveedor<eProveedor>(14, objPR.cod_proveedor, lkpTipoProducto.EditValue.ToString());
                        if (objProv == null)
                        {
                            eProveedor_Servicios objPSR = new eProveedor_Servicios();
                            objPSR.cod_proveedor = objPR.cod_proveedor; objPSR.cod_tipo_servicio = cod_tipo_servicio;
                            objPSR.flg_activo = "SI"; objPSR.cod_usuario_registro = user.cod_usuario;
                            objPSR = blProv.Guardar_Actualizar_ProveedorServicio<eProveedor_Servicios>(objPSR);
                        }

                        eProductos.eProductosProveedor eProv = new eProductos.eProductosProveedor();
                        eProv.cod_tipo_servicio = cod_tipo_servicio; eProv.cod_subtipo_servicio = cod_subtipo_servicio;
                        eProv.cod_producto = cod_producto; eProv.dsc_ruc = objPR.num_documento; eProv.cod_proveedor = objPR.cod_proveedor;
                        eProv.flg_activo = "SI"; eProv.flg_vigente = objPR.flg_vigente; eProv.cod_usuario_registro = user.cod_usuario;
                        eProv = blLogis.Insertar_Actualizar_ProductosProveedor<eProductos.eProductosProveedor>(eProv);

                        eProveedor_Marca objj = new eProveedor_Marca();
                        objj.cod_marca = Convert.ToInt32(lkpMarca.EditValue);
                        objj.cod_proveedor = objPR.cod_proveedor;
                        objj.cod_usuario_registro = user.cod_usuario;
                        objj.flg_activo = "SI";
                        objj = blLogis.Insertar_Actualizar_MarcasProveedor<eProveedor_Marca>(objj);
                    }

                    eProductos.eProductosProveedor objP = new eProductos.eProductosProveedor();
                    objP.cod_tipo_servicio = cod_tipo_servicio; objP.cod_subtipo_servicio = cod_subtipo_servicio;
                    objP.cod_producto = cod_producto; objP.flg_activo = "SI"; objP.dsc_ruc = obj.num_documento;
                    objP.cod_proveedor = obj.cod_proveedor; objP.flg_vigente = "SI"; objP.cod_usuario_registro = user.cod_usuario;
                    objP = blLogis.Insertar_Actualizar_ProductosProveedor<eProductos.eProductosProveedor>(objP);
                    ObtenerDatos_ListadoProveedores();
                }
                else
                {
                    for (int x = 0; x <= gvListaProveedores.RowCount; x++)
                    {
                        eProveedor obj2 = gvListaProveedores.GetRow(x) as eProveedor;
                        if (obj2 == null) continue;
                        if (obj2.cod_proveedor != obj.cod_proveedor) obj2.flg_vigente = "NO";
                    }
                    obj.flg_vigente = "SI"; gvListaProveedores.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void picAgregarMarca_Click(object sender, EventArgs e)
        {
            try
            {
                frmAgregarMarca frm = new frmAgregarMarca();
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.user = user;
                frm.ShowDialog();
                if (frm.cod_marca > 0) { blLogis.CargaCombosLookUp("Marca", lkpMarca, "cod_marca", "dsc_marca", "", valorDefecto: true); lkpMarca.EditValue = null; lkpMarca.EditValue = frm.cod_marca; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkProductoCompuesto_CheckStateChanged(object sender, EventArgs e)
        {
            xtabSubProductos.PageVisible = chkflgCompuesto.CheckState == CheckState.Checked ? true : false;
        }

        private void picAgregarSubTipo_Click(object sender, EventArgs e)
        {
            try
            {
                if (lkpTipoProducto.EditValue == null) { MessageBox.Show("Debe seleccionar el TIPO.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoProducto.Focus(); return; }

                frmAgregarTipoSubTipo frm = new frmAgregarTipoSubTipo();
                frm.MiAccion = AgregarTipoSubTipo.SubTipo;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.cod_tipo_servicio = lkpTipoProducto.EditValue.ToString();
                frm.user = user;
                frm.cod_empresa = cod_empresa;
                frm.ShowDialog();
                if (frm.cod_subtipo_servicio != "") { blLogis.CargaCombosLookUp("SubTipoProducto", lkpSubTipoProducto, "cod_subtipo_servicio", "dsc_subtipo_servicio", "", valorDefecto: true, cod_tipo_servicio: lkpTipoProducto.EditValue.ToString()); lkpSubTipoProducto.EditValue = null; lkpSubTipoProducto.EditValue = frm.cod_subtipo_servicio; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarProveedor_Click(object sender, EventArgs e)
        {
            if(lkpTipoProducto.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de servicio", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            Busqueda("", "Proveedor");
        }

        private void gvHistoricoTarifas_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvHistoricoTarifas.PostEditor(); gvHistoricoTarifas.RefreshData();
            eProductos.eProductosTarifas obj = gvHistoricoTarifas.GetFocusedRow() as eProductos.eProductosTarifas;
            obj.cod_proveedor = cod_proveedor; obj.dsc_proveedor = dsc_proveedor; obj.dsc_ruc = dsc_ruc;
            obj.num_item = 0; obj.fch_inicio = DateTime.Today; obj.fch_fin = new DateTime(2999, 12, 31);
        }

        private void gvHistoricoTarifas_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                gvHistoricoTarifas.PostEditor(); gvHistoricoTarifas.RefreshData();
                eProductos.eProductosTarifas obj = gvHistoricoTarifas.GetFocusedRow() as eProductos.eProductosTarifas;
                obj.cod_tipo_servicio = lkpTipoProducto.EditValue.ToString(); obj.cod_subtipo_servicio = lkpSubTipoProducto.EditValue.ToString();
                obj.cod_producto = txtCodProducto.Text;
                eProductos.eProductosTarifas eObj = blLogis.Insertar_Actualizar_ProductoCostos<eProductos.eProductosTarifas>(obj);
                if (eObj == null) MessageBox.Show("Error al insertar costo", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                ObtenerDatos_HistoricoPrecios();
                gvHistoricoTarifas.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void BusquedaProductoSUNAT(string dato)
        {
            frmListaProductosSunat frm = new frmListaProductosSunat();
            frm.user = user;
            frm.filtro = dato;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.filtro = dato;
            frm.ShowDialog();

            if (frm.codigo == "" || frm.codigo == null) { return; }
            txtCodProductoSUNAT.Text = frm.codigo;
            txtDescProductoSUNAT.Text = frm.descripcion;
            //txtCodProducto.Text = frm.codigo; //+ "-000";
            Construir_NombreProducto();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            Busqueda("", "Producto");
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
                    frm.entidad = frmBusquedas.MiEntidad.ProveedorMultiple;
                    frm.BotonAgregarVisible = 1;
                    frm.filtroRUC = filtroRUC;
                    frm.filtro = dato;
                    //frm.cod_tipo_servicio = lkpTipoProducto.EditValue.ToString();
                    break;
                case "Producto":
                    frm.entidad = frmBusquedas.MiEntidad.Productos;
                    frm.BotonAgregarVisible = 1;
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            switch (tipo)
            {
                case "Proveedor":
                    if (frm.ListProv.Count > 0)
                    {
                        List<eProveedor> ListaP = new List<eProveedor>();
                        listProveedores.AddRange(frm.ListProv);
                        ListaP = listProveedores.Distinct().ToList();
                        listProveedores.Clear(); listProveedores.AddRange(ListaP);
                        bsListaProveedores.DataSource = listProveedores;
                        gvListaProveedores.RefreshData();
                    }
                    break;
                case "Producto":
                    if (frm.ListProd.Count > 0)
                    {
                        List<eProductos.eSubProductos> ListaPR = new List<eProductos.eSubProductos>();
                        foreach(eProyecto.eProyecto_Producto obj in frm.ListProd)
                        {
                            eProductos.eSubProductos objSPR = new eProductos.eSubProductos();
                            objSPR.cod_tipo_servicio = cod_tipo_servicio;
                            objSPR.cod_subtipo_servicio = cod_subtipo_servicio;
                            objSPR.cod_producto = cod_producto;
                            objSPR.sub_cod_tipo_servicio = obj.cod_tipo_servicio;
                            objSPR.sub_cod_subtipo_servicio = obj.cod_subtipo_servicio;
                            objSPR.sub_cod_producto = obj.cod_producto;
                            objSPR.sub_dsc_producto = obj.dsc_producto;
                            listSubProductos.Add(objSPR);
                        }
                        ListaPR = listSubProductos.Distinct().ToList();
                        listSubProductos.Clear(); listSubProductos.AddRange(ListaPR);
                        bsListadoSubProductos.DataSource = listSubProductos;
                        gvListaProveedores.RefreshData();
                    }
                    break;
            }

        }

        private void gvEmpresasVinculadas_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (txtCodProducto.EditValue != null)
            {
                if (e.RowHandle < 0) return;

                if (e.Column.FieldName == "Seleccionado")
                {
                    eProductos_Empresa obj = gvEmpresasVinculadas.GetRow(e.RowHandle) as eProductos_Empresa;

                    if (obj.Seleccionado == true)
                    {
                        eProductos_Empresa eProdEmp = new eProductos_Empresa();
                        eProdEmp.cod_empresa = obj.cod_empresa;
                        eProdEmp.cod_tipo_servicio = lkpTipoProducto.EditValue.ToString();
                        eProdEmp.cod_subtipo_servicio = lkpSubTipoProducto.EditValue.ToString();
                        eProdEmp.cod_producto = txtCodProducto.EditValue.ToString();
                        eProdEmp.cod_cta_contable = obj.cod_cta_contable == null ? "" : obj.cod_cta_contable;

                        eProdEmp = blProdEmp.Ins_Act_Requerimiento<eProductos_Empresa>(eProdEmp);
                    }
                    else
                    {
                        string respuesta = blProdEmp.Ina_Productos_Empresa(obj.cod_empresa, lkpTipoProducto.EditValue.ToString(), lkpSubTipoProducto.EditValue.ToString(), txtCodProducto.EditValue.ToString());
                    }
                }

                if (e.Column.FieldName == "cod_cta_contable")
                {
                    eProductos_Empresa obj = gvEmpresasVinculadas.GetRow(e.RowHandle) as eProductos_Empresa;

                    if (obj.Seleccionado == true)
                    {
                        eProductos_Empresa eProdEmp = new eProductos_Empresa();
                        eProdEmp.cod_empresa = obj.cod_empresa;
                        eProdEmp.cod_tipo_servicio = lkpTipoProducto.EditValue.ToString();
                        eProdEmp.cod_subtipo_servicio = lkpSubTipoProducto.EditValue.ToString();
                        eProdEmp.cod_producto = txtCodProducto.EditValue.ToString();
                        eProdEmp.cod_cta_contable = obj.cod_cta_contable == null ? "" : obj.cod_cta_contable;

                        eProdEmp = blProdEmp.Ins_Act_Requerimiento<eProductos_Empresa>(eProdEmp);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe guardar el producto antes de asignarlo a una empresa.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gvEmpresasVinculadas_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            bool estado = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, view.Columns["Seleccionado"]));
            if (estado) e.Appearance.ForeColor = Color.Blue;
        }

        private void gvEmpresasVinculadas_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvEmpresasVinculadas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvEmpresasVinculadas_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void rchkSeleccionado_CheckedChanged(object sender, EventArgs e)
        {
            gvEmpresasVinculadas.PostEditor();
        }

        private void ObtenerListadoEmpresasCliente()
        {
            List<eProductos_Empresa>  ListEmpresas = blProdEmp.Cargar_Empresas<eProductos_Empresa>(1);
            bsProductosEmpresa.DataSource = null; bsProductosEmpresa.DataSource = ListEmpresas;

            if(MiAccion == Producto.Nuevo)
            {
                List<eFacturaProveedor> list = blProv.ListarEmpresasProveedor<eFacturaProveedor>(11, "", user.cod_usuario);
                if (list.Count > 0) 
                {
                    eProductos_Empresa oEmp = ListEmpresas.Find(x => x.cod_empresa == list[0].cod_empresa);
                    if (oEmp != null) { oEmp.Seleccionado = true; }
                }
            }

            if (MiAccion == Producto.Editar)
            {
                List<eProductos_Empresa> lista = blProdEmp.Cargar_Empresas<eProductos_Empresa>(2, user.cod_usuario, cod_producto);
                foreach (eProductos_Empresa obj in lista)
                {
                    eProductos_Empresa oEmp = ListEmpresas.Find(x => x.cod_empresa == obj.cod_empresa);
                    if (oEmp != null) { oEmp.Seleccionado = true; oEmp.cod_cta_contable = obj.cod_cta_contable; }
                }
            }

            gvEmpresasVinculadas.RefreshData();
        }
    }
}