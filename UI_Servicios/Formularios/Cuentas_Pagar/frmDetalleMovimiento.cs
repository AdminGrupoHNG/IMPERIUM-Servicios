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
using DevExpress.XtraBars;
using DevExpress.XtraLayout.Utils;
using System.Configuration;
using System.IO;
using System.Diagnostics;

namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    internal enum DetMovimiento
    {
        Nuevo = 0,
        Editar = 1,
        DevRemb = 2,
        EntregaRendir = 3,
        Vista = 4
    }
    public partial class frmDetalleMovimiento : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        internal DetMovimiento MiAccion = DetMovimiento.Nuevo;
        blCajaChica blCaja = new blCajaChica();
        blFactura blFact = new blFactura();
        blGlobales blGlobal = new blGlobales();
        blEncrypta blEncryp = new blEncrypta();
        public eCajaChica.eMovimiento_CajaChica eMovCaja = new eCajaChica.eMovimiento_CajaChica();
        public eEntregaRendir eEntrega = new eEntregaRendir();
        List<eFacturaProveedor> listFacturas = new List<eFacturaProveedor>();
        public List<eCajaChica.eMovimiento_CajaChica> listMovimientos = new List<eCajaChica.eMovimiento_CajaChica>();
        public string cod_caja = "", cod_movimiento = "", cod_empresa = "", cod_movimiento_vinculo = "", dsc_observacion = "", cod_entregarendir = "";
        public string ActualizarListado = "NO", EntregaRendir = "NO";
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public string cod_tipo = "", cod_entregado_a = "", dsc_entregado_a = "", flg_rendido = "NO";
        public decimal imp_entregado = 0;

        public frmDetalleMovimiento()
        {
            InitializeComponent();
        }

        private void frmDetalleMovimiento_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            try
            {
                switch (MiAccion)
                {
                    case DetMovimiento.Nuevo:
                        Nuevo();
                        break;
                    case DetMovimiento.Editar:
                        Editar();
                        if (grdbTipoMovimiento.SelectedIndex == 3) layoutControlItem7.Visibility = LayoutVisibility.Always; 
                        break;
                    case DetMovimiento.DevRemb:
                        Nuevo();
                        grdbTipoMovimiento.SelectedIndex = cod_tipo == "SA" ? 0 : cod_tipo == "DV" ? 1 : cod_tipo == "RB" ? 2 : 3;
                        txtMontoEntregado.EditValue = imp_entregado;
                        txtResponsable.Tag = cod_entregado_a;
                        txtResponsable.Text = dsc_entregado_a;
                        chkFlgRendido.CheckState = CheckState.Checked;
                        chkFlgPorRendir.CheckState = CheckState.Unchecked;
                        if (cod_tipo == "RP") { chkFlgRendido.CheckState = CheckState.Unchecked; chkFlgPorRendir.CheckState = CheckState.Checked; layoutControlItem7.Visibility = LayoutVisibility.Always; }
                        txtMontoEntregado.ReadOnly = true;
                        break;
                    case DetMovimiento.EntregaRendir:
                        Nuevo();
                        grdbTipoMovimiento.SelectedIndex = cod_tipo == "SA" ? 0 : cod_tipo == "DV" ? 1 : cod_tipo == "RB" ? 2 : 3;
                        txtMontoEntregado.EditValue = imp_entregado;
                        txtResponsable.Tag = cod_entregado_a;
                        txtResponsable.Text = dsc_entregado_a;
                        chkFlgRendido.CheckState = CheckState.Checked;
                        chkFlgPorRendir.CheckState = CheckState.Unchecked;
                        mmComentario.Text = dsc_observacion;
                        break;
                    case DetMovimiento.Vista:
                        Editar();
                        if (grdbTipoMovimiento.SelectedIndex == 3) layoutControlItem7.Visibility = LayoutVisibility.Always; 
                        BloqueoControles(false, true, false);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Nuevo()
        {
            btnNuevo.Enabled = false;
            dtFechaEmision.EditValue = DateTime.Today;
            chkFlgPorRendir_CheckStateChanged(null, new EventArgs());
            layoutControlItem12.Visibility = LayoutVisibility.Never;
            simpleLabelItem3.Visibility = LayoutVisibility.Never;
            layoutControlItem8.Visibility = LayoutVisibility.Never;
            this.Size = new Size(825, 260);
        }

        private void Editar()
        {
            chkFlgPorRendir_CheckStateChanged(null, new EventArgs());
            eCajaChica.eMovimiento_CajaChica obj = new eCajaChica.eMovimiento_CajaChica();
            obj = blCaja.ObtenerDatos_CajaChica<eCajaChica.eMovimiento_CajaChica>(4, cod_caja: cod_caja, cod_movimiento: cod_movimiento);
            chkFlgPorRendir.CheckState = obj.cod_estado == "PEN" ? CheckState.Checked : CheckState.Unchecked;
            chkFlgRendido.CheckState = obj.cod_estado == "REN" ? CheckState.Checked : CheckState.Unchecked;
            grdbTipoMovimiento.SelectedIndex = obj.cod_tipo == "SA" ? 0 : obj.cod_tipo == "DV" ? 1 : obj.cod_tipo == "RB" ? 2 : 3;
            dtFechaEmision.EditValue = obj.fch_creacion;
            txtMontoEntregado.EditValue = obj.imp_entregado;
            txtResponsable.Tag = obj.cod_entregado_a;
            txtResponsable.Text = obj.dsc_entregado_a;
            mmComentario.Text = obj.dsc_observacion;
            txtReferencia.Text = obj.dsc_referencia;
            btnRendirMovimiento.Enabled = chkFlgPorRendir.CheckState == CheckState.Checked ? true : false;
            Obtener_ListadoFacturas();
            btnAgregarDocumento.Enabled = true; btnAgregarDocumentoInterno.Enabled = true;
            btnAgregarDevolucionReembolso.Enabled = false;
            decimal imp_Monto = (from tabla in listFacturas
                                 where tabla.tipo_documento != null
                                 select tabla.imp_total).Sum();
            //if (imp_Monto == Convert.ToDecimal(txtMontoEntregado.EditValue)) btnRendirMovimiento.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            if (Convert.ToDecimal(txtMontoEntregado.EditValue) > imp_Monto) { btnAgregarDevolucionReembolso.Enabled = true; btnAgregarDevolucionReembolso.Caption = "Agregar Devolución"; }
            if (Convert.ToDecimal(txtMontoEntregado.EditValue) < imp_Monto) { btnAgregarDevolucionReembolso.Enabled = true; btnAgregarDevolucionReembolso.Caption = "Agregar Reembolso"; }
            if (listFacturas.Count == 0) btnAgregarDevolucionReembolso.Enabled = false;
            if (obj.cod_movimiento_vinculo != null && obj.cod_movimiento_vinculo != "")
            {
                gvFacturasProveedor.OptionsView.ShowFooter = false;
                //layoutControlItem13.Visibility = LayoutVisibility.Always; btnAgregarDevolucionReembolso.Enabled = false;
            }
            txtCodMovVinculado.Text = obj.cod_movimiento_vinculo;
            if (grdbTipoMovimiento.SelectedIndex > 0) 
            { 
                this.Size = new Size(825, 260); layoutControlItem12.Visibility = LayoutVisibility.Never; simpleLabelItem3.Visibility = LayoutVisibility.Never; layoutControlItem8.Visibility = LayoutVisibility.Never;
                btnAgregarDevolucionReembolso.Enabled = false; btnAgregarDocumento.Enabled = false; btnAgregarDocumentoInterno.Enabled = false; btnNuevo.Enabled = false;
            }
            if (obj.cod_tipo != "RP") { chkFlgPorRendir.Enabled = false; chkFlgRendido.Enabled = false; }
            if (chkFlgRendido.CheckState == CheckState.Checked) BloqueoControles(false, true, false);
        }

        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            chkFlgPorRendir.ReadOnly = ReadOnly; 
            chkFlgRendido.ReadOnly = ReadOnly;
            dtFechaEmision.ReadOnly = ReadOnly; 
            txtReferencia.ReadOnly = ReadOnly;
            txtMontoEntregado.ReadOnly = ReadOnly;
            txtResponsable.ReadOnly = ReadOnly; 
            picResponsable.Enabled = Enabled; 
            mmComentario.ReadOnly = ReadOnly;
            btnGuardar.Enabled = Enabled; 
            btnRendirMovimiento.Enabled = Enabled;
            btnAgregarDevolucionReembolso.Enabled = Enabled;
            btnAgregarDocumento.Enabled = Enabled;
            btnAgregarDocumentoInterno.Enabled = Enabled;
        }

        private void Obtener_ListadoFacturas()
        {
            listFacturas = blCaja.ListarDatos_CajaChica<eFacturaProveedor>(5, cod_caja, cod_movimiento);
            bsListadoFacturas.DataSource = listFacturas; 
            btnRendirMovimiento.Enabled = listFacturas.Count > 0 ? true : false;
            btnAgregarDevolucionReembolso.Enabled = listFacturas.Count > 0 ? true : false;
            gvFacturasProveedor.RefreshData();
        }

        private void chkFlgPorRendir_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkFlgPorRendir.CheckState == CheckState.Checked)
            {
                chkFlgRendido.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkFlgRendido.CheckState = CheckState.Checked;
            }
        }

        private void txtResponsable_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                blGlobal.pKeyDown(txtResponsable, e);
                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) txtResponsable.Tag = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtResponsable_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Busqueda("", "Responsable");
            }
            string dato = blGlobal.pKeyPress(txtResponsable, e);
            if (dato != "")
            {
                Busqueda(dato, "Responsable");
            }
        }

        private void picResponsable_Click(object sender, EventArgs e)
        {
            Busqueda("", "Responsable");
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
                case "Responsable":
                    frm.entidad = frmBusquedas.MiEntidad.Trabajador;
                    frm.cod_empresa = cod_empresa;
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Responsable":
                    txtResponsable.Tag = frm.codigo;
                    txtResponsable.Text = frm.descripcion;
                    break;
            }
        }

        private void frmDetalleMovimiento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && MiAccion != DetMovimiento.Nuevo) this.Close();
        }

        private void btnRendirMovimiento_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                btnAgregarDevolucionReembolso_ItemClick(btnAgregarDevolucionReembolso, new ItemClickEventArgs(null, null));
                //RendirMovimiento();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void RendirMovimiento()
        {
            eMovCaja = AsignarValores();
            eMovCaja.cod_estado = "REN"; eMovCaja.cod_usuario_registro = user.cod_usuario;
            eMovCaja = blCaja.InsertarActualizar_MovimientosCajaChica<eCajaChica.eMovimiento_CajaChica>(eMovCaja);
            if (eMovCaja == null) MessageBox.Show("Error al actualizar movimiento", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (eMovCaja != null)
            {
                ActualizarListado = "SI";
                MiAccion = DetMovimiento.Editar;
                chkFlgRendido.CheckState = CheckState.Checked;
                MessageBox.Show("Se rindió movimiento de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void picExportarExcel_Click(object sender, EventArgs e)
        {
            ExportarExcel();
        }

        private void ExportarExcel()
        {
            try
            {
                string carpeta = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\MovCajaChica" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                gvFacturasProveedor.ExportToXlsx(archivo);
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

        private void btnAgregarDocumento_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //if (chkFlgPorRendir.CheckState == CheckState.Checked)
                //{
                frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                frmModif.MiAccion = Factura.Nuevo;
                frmModif.colorVerde = colorVerde;
                frmModif.colorPlomo = colorPlomo;
                frmModif.colorFocus = colorFocus;
                frmModif.colorEventRow = colorEventRow;
                frmModif.user = user;
                frmModif.cod_empresa = cod_empresa;
                frmModif.CajaChica = "SI";
                frmModif.cod_caja = cod_caja;
                frmModif.cod_movimiento = cod_movimiento;
                frmModif.ShowDialog();
                if (frmModif.ActualizarListado)
                {
                    //eCajaChica.eDetalleMov_CajaChica objDet = new eCajaChica.eDetalleMov_CajaChica();
                    //objDet.cod_caja = cod_caja; objDet.cod_movimiento = cod_movimiento;
                    //objDet.num_linea = 0;
                    //objDet.tipo_documento = frmModif.tipo_documento; objDet.serie_documento = frmModif.serie_documento;
                    //objDet.numero_documento = frmModif.numero_documento; objDet.cod_proveedor = frmModif.cod_proveedor;
                    //objDet = blCaja.InsertarActualizar_DetalleMovCajaChica<eCajaChica.eDetalleMov_CajaChica>(objDet);
                    //if (objDet == null) MessageBox.Show("Error al insertar detalle de movimiento", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnAgregarDevolucionReembolso.Enabled = false;
                    Obtener_ListadoFacturas();
                    decimal imp_Monto = (from tabla in listFacturas
                                         where tabla.tipo_documento != null
                                         select tabla.imp_total).Sum();

                    if (Convert.ToDecimal(txtMontoEntregado.EditValue) > imp_Monto) { btnAgregarDevolucionReembolso.Enabled = true; btnAgregarDevolucionReembolso.Caption = "Agregar Devolución"; }
                    if (Convert.ToDecimal(txtMontoEntregado.EditValue) < imp_Monto) { btnAgregarDevolucionReembolso.Enabled = true; btnAgregarDevolucionReembolso.Caption = "Agregar Reembolso"; }
                    if (Convert.ToDecimal(txtMontoEntregado.EditValue) == imp_Monto) btnAgregarDevolucionReembolso.Enabled = false;

                    //eMovCaja = AsignarValores();
                    //eMovCaja.cod_estado = "REN"; eMovCaja.cod_usuario_registro = user.cod_usuario;
                    //////eMovCaja.dsc_ruc = frmModif.RUC; eMovCaja.cod_proveedor = frmModif.cod_proveedor;
                    //eMovCaja = blCaja.InsertarActualizar_MovimientosCajaChica<eCajaChica.eMovimiento_CajaChica>(eMovCaja);
                    //if (eMovCaja == null) MessageBox.Show("Error al actualizar movimiento", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //}
                //else
                //{
                //    MessageBox.Show("Solo se puede rendir los movimientos que no estan por rendir.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvFacturasProveedor_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvFacturasProveedor_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eFacturaProveedor obj = gvFacturasProveedor.GetRow(e.RowHandle) as eFacturaProveedor;
                    if (obj == null) return;
                    if (obj.dsc_tipo_documento.Trim().Substring(0, 3) == "REE") { e.Appearance.ForeColor = Color.Purple; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                    if (obj.dsc_tipo_documento.Trim().Substring(0, 3) == "DEV") { e.Appearance.ForeColor = Color.DarkGreen; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAgregarDocumentoInterno_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmMantDocumentoInterno frmModif = new frmMantDocumentoInterno();
                frmModif.MiAccion = DocInterno.Nuevo;
                frmModif.CajaChica = "SI";
                frmModif.user = user;
                frmModif.cod_empresa = cod_empresa;
                frmModif.ShowDialog();
                if (frmModif.ActualizarListado)
                {
                    eCajaChica.eDetalleMov_CajaChica objDet = new eCajaChica.eDetalleMov_CajaChica();
                    objDet.cod_caja = cod_caja; objDet.cod_movimiento = cod_movimiento;
                    objDet.num_linea = 0;
                    objDet.tipo_documento = frmModif.tipo_documento; objDet.serie_documento = frmModif.serie_documento;
                    objDet.numero_documento = frmModif.numero_documento; objDet.cod_proveedor = frmModif.cod_proveedor;
                    objDet = blCaja.InsertarActualizar_DetalleMovCajaChica<eCajaChica.eDetalleMov_CajaChica>(objDet);
                    if (objDet == null) MessageBox.Show("Error al insertar detalle de movimiento", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnAgregarDevolucionReembolso.Enabled = false;
                    Obtener_ListadoFacturas();
                    decimal imp_Monto = (from tabla in listFacturas
                                         where tabla.tipo_documento != null
                                         select tabla.imp_total).Sum();

                    if (Convert.ToDecimal(txtMontoEntregado.EditValue) > imp_Monto) { btnAgregarDevolucionReembolso.Enabled = true; btnAgregarDevolucionReembolso.Caption = "Agregar Devolución"; }
                    if (Convert.ToDecimal(txtMontoEntregado.EditValue) < imp_Monto) { btnAgregarDevolucionReembolso.Enabled = true; btnAgregarDevolucionReembolso.Caption = "Agregar Reembolso"; }
                    if (Convert.ToDecimal(txtMontoEntregado.EditValue) == imp_Monto) btnAgregarDevolucionReembolso.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtCodMovVinculado_Click(object sender, EventArgs e)
        {
            frmDetalleMovimiento frm = new frmDetalleMovimiento();
            frm.MiAccion = DetMovimiento.Editar;
            frm.user = user;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.cod_caja = cod_caja;
            frm.cod_movimiento = txtCodMovVinculado.Text;
            frm.cod_movimiento_vinculo = cod_movimiento;
            frm.cod_empresa = cod_empresa;
            //frm.eMovCaja = obj;
            frm.ShowDialog();
        }

        private void gvFacturasProveedor_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvFacturasProveedor_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 1 && e.Column.FieldName == "dsc_documento")
                {
                    eFacturaProveedor obj = gvFacturasProveedor.GetFocusedRow() as eFacturaProveedor;
                    if (obj == null) return;

                    if (obj.tipo_documento != null)
                    {
                        if (obj.tipo_documento != "TC045")
                        {
                            frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                            frmModif.MiAccion = chkFlgRendido.CheckState == CheckState.Checked ? Factura.Vista : Factura.Editar;
                            frmModif.colorVerde = colorVerde;
                            frmModif.colorPlomo = colorPlomo;
                            frmModif.colorFocus = colorFocus;
                            frmModif.colorEventRow = colorEventRow;
                            frmModif.RUC = obj.dsc_ruc;
                            frmModif.tipo_documento = obj.tipo_documento;
                            frmModif.serie_documento = obj.serie_documento;
                            frmModif.numero_documento = obj.numero_documento;
                            frmModif.cod_proveedor = obj.cod_proveedor;
                            frmModif.user = user;
                            frmModif.cod_empresa = cod_empresa;
                            frmModif.CajaChica = "SI";
                            frmModif.cod_caja = cod_caja;
                            frmModif.cod_movimiento = cod_movimiento;
                            frmModif.ShowDialog();
                            if (frmModif.ActualizarListado)
                            {
                                Obtener_ListadoFacturas();
                                decimal imp_Monto = (from tabla in listFacturas
                                                     where tabla.tipo_documento != null
                                                     select tabla.imp_total).Sum();

                                if (Convert.ToDecimal(txtMontoEntregado.EditValue) > imp_Monto) { btnAgregarDevolucionReembolso.Enabled = true; btnAgregarDevolucionReembolso.Caption = "Agregar Devolución"; }
                                if (Convert.ToDecimal(txtMontoEntregado.EditValue) < imp_Monto) { btnAgregarDevolucionReembolso.Enabled = true; btnAgregarDevolucionReembolso.Caption = "Agregar Reembolso"; }
                                if (Convert.ToDecimal(txtMontoEntregado.EditValue) == imp_Monto) btnAgregarDevolucionReembolso.Enabled = false;
                            }
                        }
                        else
                        {
                            frmMantDocumentoInterno frmModif = new frmMantDocumentoInterno();
                            frmModif.MiAccion = chkFlgRendido.CheckState == CheckState.Checked ? DocInterno.Vista : DocInterno.Editar;
                            frmModif.tipo_documento = obj.tipo_documento;
                            frmModif.serie_documento = obj.serie_documento;
                            frmModif.numero_documento = obj.numero_documento;
                            frmModif.cod_proveedor = obj.cod_proveedor;
                            frmModif.CajaChica = "SI";
                            frmModif.user = user;
                            frmModif.cod_empresa = cod_empresa;
                            frmModif.ShowDialog();
                            if (frmModif.ActualizarListado)
                            {
                                Obtener_ListadoFacturas();
                                decimal imp_Monto = (from tabla in listFacturas
                                                     where tabla.tipo_documento != null
                                                     select tabla.imp_total).Sum();

                                if (Convert.ToDecimal(txtMontoEntregado.EditValue) > imp_Monto) { btnAgregarDevolucionReembolso.Enabled = true; btnAgregarDevolucionReembolso.Caption = "Agregar Devolución"; }
                                if (Convert.ToDecimal(txtMontoEntregado.EditValue) < imp_Monto) { btnAgregarDevolucionReembolso.Enabled = true; btnAgregarDevolucionReembolso.Caption = "Agregar Reembolso"; }
                                if (Convert.ToDecimal(txtMontoEntregado.EditValue) == imp_Monto) btnAgregarDevolucionReembolso.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        frmDetalleMovimiento frm = new frmDetalleMovimiento();
                        frm.MiAccion = chkFlgRendido.CheckState == CheckState.Checked ? DetMovimiento.Vista : DetMovimiento.Editar;
                        frm.user = user;
                        frm.colorVerde = colorVerde;
                        frm.colorPlomo = colorPlomo;
                        frm.colorEventRow = colorEventRow;
                        frm.colorFocus = colorFocus;
                        frm.cod_caja = cod_caja;
                        frm.cod_movimiento = txtCodMovVinculado.Text;
                        frm.cod_movimiento_vinculo = cod_movimiento;
                        frm.cod_empresa = cod_empresa;
                        frm.ShowDialog();
                        if (frm.ActualizarListado == "SI")
                        {
                            Obtener_ListadoFacturas();
                            decimal imp_Monto = (from tabla in listFacturas
                                                 where tabla.tipo_documento != null
                                                 select tabla.imp_total).Sum();

                            if (Convert.ToDecimal(txtMontoEntregado.EditValue) > imp_Monto) { btnAgregarDevolucionReembolso.Enabled = true; btnAgregarDevolucionReembolso.Caption = "Agregar Devolución"; }
                            if (Convert.ToDecimal(txtMontoEntregado.EditValue) < imp_Monto) { btnAgregarDevolucionReembolso.Enabled = true; btnAgregarDevolucionReembolso.Caption = "Agregar Reembolso"; }
                            if (Convert.ToDecimal(txtMontoEntregado.EditValue) == imp_Monto) btnAgregarDevolucionReembolso.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAgregarDevolucionReembolso_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                decimal imp_Monto = (from tabla in listFacturas
                                     where tabla.tipo_documento != null
                                     select tabla.imp_total).Sum();

                if (Convert.ToDecimal(txtMontoEntregado.EditValue) != imp_Monto)
                {

                    frmDetalleMovimiento frm = new frmDetalleMovimiento();
                    frm.MiAccion = DetMovimiento.DevRemb;
                    frm.user = user;
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorEventRow = colorEventRow;
                    frm.colorFocus = colorFocus;
                    frm.cod_caja = cod_caja;
                    frm.cod_empresa = cod_empresa;
                    frm.cod_movimiento_vinculo = cod_movimiento;
                    frm.cod_tipo = btnAgregarDevolucionReembolso.Caption == "Agregar Devolución" ? "DV" : "RB";
                    frm.cod_entregado_a = txtResponsable.Tag.ToString();
                    frm.dsc_entregado_a = txtResponsable.Text;
                    //decimal imp_Monto = (from tabla in listFacturas
                    //                     where tabla.tipo_documento != null
                    //                     select tabla.imp_total).Sum();
                    frm.imp_entregado = Math.Abs(Convert.ToDecimal(txtMontoEntregado.EditValue) - imp_Monto);
                    frm.chkFlgPorRendir.Enabled = false;
                    frm.chkFlgRendido.Enabled = false;
                    frm.grdbTipoMovimiento.Enabled = false;
                    frm.ShowDialog();
                    if (frm.ActualizarListado == "SI")
                    {
                        //btnRendirMovimiento_ItemClick(btnRendirMovimiento, new ItemClickEventArgs(null, null));
                        RendirMovimiento();
                        Inicializar();
                    }
                }
                else
                {
                    RendirMovimiento();
                    Inicializar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void chkFlgRendido_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkFlgRendido.CheckState == CheckState.Checked/* && grdbTipoMovimiento.SelectedIndex == 0*/)
            {
                chkFlgPorRendir.CheckState = CheckState.Unchecked;
                //txtRucProveedor.Enabled = true;
                //txtProveedor.Enabled = true;
                //picBuscarProveedor.Enabled = true;
                //txtGlosaMovimiento.Enabled = true;
                //lkpTipoGasto.Enabled = true;
                //lkpUnidadNegocio.Enabled = true;
                //lkpCliente.Enabled = true;
                //txtPrefCECO.Enabled = true;
            }
            else
            {
                //chkFlgPorRendir.CheckState = CheckState.Unchecked;
                chkFlgPorRendir.CheckState = CheckState.Checked;
            }
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                /*cod_caja = ""; */cod_movimiento = ""; cod_movimiento_vinculo = "";
                MiAccion = DetMovimiento.Nuevo;
                chkFlgPorRendir.CheckState = CheckState.Checked;
                grdbTipoMovimiento.SelectedIndex = 0;
                dtFechaEmision.EditValue = DateTime.Today;
                txtMontoEntregado.EditValue = 0;
                txtResponsable.Tag = "";
                txtResponsable.Text = "";
                mmComentario.Text = "";
                txtCodMovVinculado.Text = "";
                listFacturas.Clear(); gvFacturasProveedor.RefreshData();
                chkFlgPorRendir_CheckStateChanged(null, new EventArgs());
                layoutControlItem12.Visibility = LayoutVisibility.Never;
                simpleLabelItem3.Visibility = LayoutVisibility.Never;
                layoutControlItem8.Visibility = LayoutVisibility.Never;
                this.Size = new Size(825, 260);
                BloqueoControles(true, false, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                txtResponsable.Select();
                if (dtFechaEmision.EditValue == null) { MessageBox.Show("Debe seleccionar una fecha", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtFechaEmision.Focus(); return; }
                if (Convert.ToDecimal(txtMontoEntregado.EditValue) == 0) { MessageBox.Show("El importe debe ser mayor a 0.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtMontoEntregado.Focus(); return; }
                if (txtResponsable.Text.Trim() == "") { MessageBox.Show("Debe seleccionar un responsable.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtResponsable.Focus(); return; }

                //if (chkFlgRendido.CheckState == CheckState.Checked)
                //{
                //    //if (txtRucProveedor.Text.Trim() == "") { MessageBox.Show("Debe seleccionar un proveedor.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtRucProveedor.Focus(); return; }
                //    //if (txtProveedor.Text.Trim() == "") { MessageBox.Show("Debe seleccionar un proveedor.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtResponsable.Focus(); return; }
                //    //if (txtGlosaMovimiento.Text.Trim() == "") { MessageBox.Show("Debe ingresar una glosa.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtGlosaMovimiento.Focus(); return; }
                //    //if (lkpTipoGasto.EditValue == null) { MessageBox.Show("Debe seleccionar el tipo de gasto.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoGasto.Focus(); return; }
                //    //if (lkpUnidadNegocio.EditValue == null) { MessageBox.Show("Debe seleccionar la unidad de negocio.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpUnidadNegocio.Focus(); return; }
                //    //if (lkpCliente.EditValue == null) { MessageBox.Show("Debe seleccionar un cliente.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpCliente.Focus(); return; }
                //    //if (txtPrefCECO.Text.Trim() == "") { MessageBox.Show("Debe ingresar un prefijo de centro de costo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtPrefCECO.Focus(); return; }
                //    if (listMovimientos.Count == 0) { MessageBox.Show("Debe ingresar documentos para rendir el movimiento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                //}

                eMovCaja = AsignarValores();
                eMovCaja = blCaja.InsertarActualizar_MovimientosCajaChica<eCajaChica.eMovimiento_CajaChica>(eMovCaja);
                if (eMovCaja == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                cod_caja = eMovCaja.cod_caja; cod_movimiento = eMovCaja.cod_movimiento;

                if (eMovCaja != null)
                {
                    if (flg_rendido == "SI")
                    {
                        foreach (eCajaChica.eMovimiento_CajaChica obj in listMovimientos)
                        {
                            obj.flg_rendido = "SI"; obj.cod_usuario_registro = user.cod_usuario; obj.cod_movimiento_rendido = cod_movimiento;
                            obj.cod_movimiento = obj.cod_tipo == "SA" ? obj.cod_movimiento : obj.cod_rendicion;
                            eCajaChica.eMovimiento_CajaChica objMov = blCaja.InsertarActualizar_MovimientosCajaChica<eCajaChica.eMovimiento_CajaChica>(obj);
                        }
                    }
                    ActualizarListado = "SI";
                    if (MiAccion != DetMovimiento.DevRemb)
                    {
                        Editar();
                        layoutControlItem12.Visibility = grdbTipoMovimiento.SelectedIndex == 0 ? LayoutVisibility.Always : LayoutVisibility.Never;
                        simpleLabelItem3.Visibility = grdbTipoMovimiento.SelectedIndex == 0 ? LayoutVisibility.Always : LayoutVisibility.Never;
                        layoutControlItem8.Visibility = grdbTipoMovimiento.SelectedIndex == 0 ? LayoutVisibility.Always : LayoutVisibility.Never;
                        if (grdbTipoMovimiento.SelectedIndex == 0) this.Size = new Size(825, 539);
                        if (chkFlgRendido.CheckState == CheckState.Checked) btnRendirMovimiento.Enabled = false;
                    }
                    if (MiAccion == DetMovimiento.Nuevo && chkFlgRendido.CheckState == CheckState.Checked && grdbTipoMovimiento.SelectedIndex != 3) 
                    {
                        if (MessageBox.Show("Si desea agregar una FACTURA marque SI." + Environment.NewLine + "Si desea agregar un DOCUMENTO INTERNO marque NO.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            btnAgregarDocumento_ItemClick(null, new ItemClickEventArgs(btnAgregarDocumento, null));
                        }
                        else
                        {
                            btnAgregarDocumentoInterno_ItemClick(null, new ItemClickEventArgs(btnAgregarDocumentoInterno, null));
                        }
                        decimal imp_Monto = (from tabla in listFacturas
                                             where tabla.tipo_documento != null
                                             select tabla.imp_total).Sum();
                        if (imp_Monto == 0)
                        {
                            eMovCaja = AsignarValores(); eMovCaja.cod_estado = "PEN";
                            eMovCaja = blCaja.InsertarActualizar_MovimientosCajaChica<eCajaChica.eMovimiento_CajaChica>(eMovCaja);
                            if (eMovCaja == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                            cod_caja = eMovCaja.cod_caja; cod_movimiento = eMovCaja.cod_movimiento;
                        }
                        else
                        {
                            btnAgregarDevolucionReembolso_ItemClick(btnAgregarDevolucionReembolso, new ItemClickEventArgs(null, null));
                        }
                    }
                    MiAccion = DetMovimiento.Editar;

                    MessageBox.Show("Se registraron los datos de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private eCajaChica.eMovimiento_CajaChica AsignarValores()
        {
            eCajaChica.eMovimiento_CajaChica obj = new eCajaChica.eMovimiento_CajaChica();
            obj.cod_caja = cod_caja;
            obj.cod_movimiento = cod_movimiento;
            obj.cod_movimiento_vinculo = cod_movimiento_vinculo;

            // POR RENDIR
            Int32 cod_tipo = Convert.ToInt32(grdbTipoMovimiento.SelectedIndex);
            obj.cod_tipo = cod_tipo == 0 ? "SA" : cod_tipo == 1 ? "DV" : cod_tipo == 2 ? "RB" : cod_tipo == 3 ? "RP" : "";
            obj.fch_creacion = Convert.ToDateTime(dtFechaEmision.EditValue);
            obj.imp_entregado = Convert.ToDecimal(txtMontoEntregado.EditValue);
            obj.cod_entregado_a = txtResponsable.Tag.ToString();
            
            obj.dsc_observacion = mmComentario.Text == "" ? null : mmComentario.Text;
            obj.cod_usuario_registro = user.cod_usuario;
            obj.cod_estado = MiAccion == DetMovimiento.DevRemb && obj.cod_tipo == "RP" ? "PEN" : chkFlgPorRendir.CheckState == CheckState.Checked ? "PEN" : "REN";
            obj.flg_rendido = obj.cod_tipo == "RP" ? "SI" : flg_rendido;
            obj.dsc_referencia = txtReferencia.Text;
            obj.num_Anho = Convert.ToDateTime(dtFechaEmision.EditValue).Year;
            obj.cod_entregarendir = EntregaRendir == "SI" ? cod_entregarendir : null;

            return obj;
        }


    }
}