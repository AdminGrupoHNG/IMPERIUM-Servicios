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
    internal enum DetEntregaRendir
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2,
        DevRemb = 3
    }
    public partial class frmDetalleEntregaRendir : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        internal DetEntregaRendir MiAccion = DetEntregaRendir.Nuevo;
        blCajaChica blCaja = new blCajaChica();
        blProveedores blProv = new blProveedores();
        blFactura blFact = new blFactura();
        blTrabajador blTrab = new blTrabajador();
        blGlobales blGlobal = new blGlobales();
        blEncrypta blEncryp = new blEncrypta();
        public eEntregaRendir eEntrega = new eEntregaRendir();
        List<eFacturaProveedor> listFacturas = new List<eFacturaProveedor>();
        public List<eCajaChica.eMovimiento_CajaChica> listMovimientos = new List<eCajaChica.eMovimiento_CajaChica>();
        public string cod_entregarendir = "", cod_empresa = "", cod_sede_empresa = "", cod_vinculo = "";
        public string ActualizarListado = "NO";
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public string cod_tipo = "", cod_entregado_a = "", dsc_entregado_a = "";
        public decimal imp_entregado = 0;

        public frmDetalleEntregaRendir()
        {
            InitializeComponent();
        }

        private void frmDetalleEntregaRendir_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            try
            {
                switch (MiAccion)
                {
                    case DetEntregaRendir.Nuevo:
                        Nuevo();
                        lkpEmpresa.EditValue = cod_empresa;
                        break;
                    case DetEntregaRendir.Editar:
                        Editar();
                        break;
                    case DetEntregaRendir.DevRemb:
                        Nuevo();
                        grdbTipoMovimiento.SelectedIndex = cod_tipo == "ER" ? 0 : cod_tipo == "DV" ? 1 : cod_tipo == "RB" ? 2 : 3;
                        txtMontoEntregado.EditValue = imp_entregado;
                        txtResponsable.Tag = cod_entregado_a;
                        txtResponsable.Text = dsc_entregado_a;
                        chkFlgRendido.CheckState = CheckState.Checked;
                        chkFlgPorRendir.CheckState = CheckState.Unchecked;
                        eTrabajador.eInfoLaboral_Trabajador objT = new eTrabajador.eInfoLaboral_Trabajador();
                        objT = blTrab.Obtener_Trabajador<eTrabajador.eInfoLaboral_Trabajador>(5, cod_entregado_a, cod_empresa);
                        txtUbicacion.Text = objT.dsc_empresa + " - " + objT.dsc_sede_empresa;
                        txtUbicacion.Tag = objT.cod_sede_empresa;
                        cod_empresa = objT.cod_empresa; cod_sede_empresa = objT.cod_sede_empresa;
                        break;
                    case DetEntregaRendir.Vista:
                        Editar();
                        BloqueoControles(false, true, false);
                        btnNuevo.Enabled = false;
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
                blFact.CargaCombosLookUp("EmpresaProveedor", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true);
                blProv.CargaCombosLookUp("Moneda", lkpTipoMoneda, "cod_moneda", "dsc_moneda", "", valorDefecto: true);
                blCaja.CargaCombosLookUp("ModoReposicion", lkpModoReposicion, "cod_modalidad", "dsc_modalidad", "", valorDefecto: true);
                blCaja.CargaCombosLookUp("EstadoEntregaRendir", lkpEstadoAprobado, "cod_estado_aprobado", "dsc_estado_aprobado", "", valorDefecto: true);
                lkpModoReposicion.EditValue = "TRF";
                lkpEstadoAprobado.EditValue = "CRE";
                dtFecCreacion.EditValue = DateTime.Today;
                List<eProveedor_Empresas> listEmpresasUsuario = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(11, "", user.cod_usuario);
                lkpEmpresa.EditValue = listEmpresasUsuario[0].cod_empresa;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Nuevo()
        {
            btnNuevo.Enabled = false;
            dtFecCreacion.EditValue = DateTime.Today;
            chkFlgPorRendir_CheckStateChanged(null, new EventArgs());
            layoutControlItem12.Visibility = LayoutVisibility.Never;
            simpleLabelItem3.Visibility = LayoutVisibility.Never;
            layoutControlItem10.Visibility = LayoutVisibility.Never;
            CargarLookUpEdit();
            this.Size = new Size(825, 340);
        }

        private void Editar()
        {
            chkFlgPorRendir_CheckStateChanged(null, new EventArgs());
            CargarLookUpEdit();
            eEntregaRendir obj = new eEntregaRendir();
            obj = blCaja.ObtenerDatos_EntregasRendir<eEntregaRendir>(3, cod_entregarendir: cod_entregarendir, cod_empresa: cod_empresa, cod_sede_empresa: cod_sede_empresa);
            txtCodEntregaRendir.Text = cod_entregarendir;
            chkFlgPorRendir.CheckState = obj.cod_estado == "PEN" ? CheckState.Checked : CheckState.Unchecked;
            chkFlgRendido.CheckState = obj.cod_estado == "REN" ? CheckState.Checked : CheckState.Unchecked;
            grdbTipoMovimiento.SelectedIndex = obj.cod_tipo == "ER" ? 0 : obj.cod_tipo == "DV" ? 1 : obj.cod_tipo == "RB" ? 2 : 3;
            lkpEstadoAprobado.EditValue = obj.cod_estado_aprobado;
            lkpModoReposicion.EditValue = obj.cod_modalidad;
            dtFecCreacion.EditValue = obj.fch_creacion;
            txtMontoEntregado.EditValue = obj.imp_monto;
            txtResponsable.Tag = obj.cod_entregado_a;
            txtResponsable.Text = obj.dsc_entregado_a;
            lkpEmpresa.EditValue = obj.cod_empresa;
            lkpTipoMoneda.EditValue = obj.cod_moneda;
            eTrabajador.eInfoLaboral_Trabajador objT = new eTrabajador.eInfoLaboral_Trabajador();
            objT = blTrab.Obtener_Trabajador<eTrabajador.eInfoLaboral_Trabajador>(5, obj.cod_entregado_a, cod_empresa);
            txtUbicacion.Text = objT.dsc_empresa + " - " + objT.dsc_sede_empresa;
            txtUbicacion.Tag = objT.cod_sede_empresa;
            //cod_empresa = objT.cod_empresa; cod_sede_empresa = objT.cod_sede_empresa;

            mmComentario.Text = obj.dsc_observacion;
            if (chkFlgPorRendir.CheckState == CheckState.Checked) btnRendirMovimiento.Enabled = true;
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
            if (obj.cod_vinculo != null && obj.cod_vinculo != "")
            {
                gvFacturasProveedor.OptionsView.ShowFooter = false;
                //layoutControlItem13.Visibility = LayoutVisibility.Always; btnAgregarDevolucionReembolso.Enabled = false;
            }
            //else
            //{
            //    btnRendirMovimiento.Enabled = false;
            //}
            txtCodMovVinculado.Text = obj.cod_vinculo;
            if (grdbTipoMovimiento.SelectedIndex > 0)
            {
                this.Size = new Size(825, 340); layoutControlItem12.Visibility = LayoutVisibility.Never; simpleLabelItem3.Visibility = LayoutVisibility.Never; layoutControlItem10.Visibility = LayoutVisibility.Never;
                btnAgregarDevolucionReembolso.Enabled = false; btnAgregarDocumento.Enabled = false; btnAgregarDocumentoInterno.Enabled = false; btnNuevo.Enabled = false;
            }
            if (chkFlgRendido.CheckState == CheckState.Checked) BloqueoControles(false, true, false);

           
        }

        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            lkpEmpresa.ReadOnly = ReadOnly;
            lkpTipoMoneda.ReadOnly = ReadOnly;
            lkpModoReposicion.ReadOnly = ReadOnly;
            chkFlgPorRendir.ReadOnly = ReadOnly; 
            chkFlgRendido.ReadOnly = ReadOnly;
            dtFecCreacion.ReadOnly = ReadOnly; 
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
            listFacturas = blCaja.ListarDatos_EntregasRendir<eFacturaProveedor>(4, cod_entregarendir, cod_empresa, cod_sede_empresa);
            bsListadoFacturas.DataSource = listFacturas; gvFacturasProveedor.RefreshData();
            btnRendirMovimiento.Enabled = listFacturas.Count > 0 ? true : false;
            btnAgregarDevolucionReembolso.Enabled = listFacturas.Count > 0 ? true : false;
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
                    eTrabajador.eInfoLaboral_Trabajador obj = new eTrabajador.eInfoLaboral_Trabajador();
                    obj = blTrab.Obtener_Trabajador<eTrabajador.eInfoLaboral_Trabajador>(5, frm.codigo, cod_empresa);
                    txtUbicacion.Text = obj.dsc_empresa + " - " + obj.dsc_sede_empresa;
                    txtUbicacion.Tag = obj.cod_sede_empresa;
                    cod_empresa = obj.cod_empresa; cod_sede_empresa = obj.cod_sede_empresa;
                    break;
            }
        }

        private void frmDetalleEntregaRendir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && MiAccion != DetEntregaRendir.Nuevo) this.Close();
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
            eEntrega = AsignarValores();
            eEntrega.cod_estado = "REN"; eEntrega.cod_usuario_registro = user.cod_usuario;
            eEntrega = blCaja.InsertarActualizar_EntregasRendir<eEntregaRendir>(eEntrega);
            if (eEntrega == null) MessageBox.Show("Error al actualizar entrega a rendir", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (eEntrega != null)
            {
                ActualizarListado = "SI";
                MiAccion = DetEntregaRendir.Editar;
                chkFlgRendido.CheckState = CheckState.Checked;

                MessageBox.Show("Se rindió movimiento de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                frmModif.EntregaRendir = "SI";
                frmModif.cod_entregarendir = cod_entregarendir;
                frmModif.cod_trabajador = txtResponsable.Tag.ToString();
                frmModif.cod_sede_empresa = cod_sede_empresa;
                frmModif.ShowDialog();
                if (frmModif.ActualizarListado)
                {
                    //eEntregaRendir.eDetalle_EntregaRendir objDet = new eEntregaRendir.eDetalle_EntregaRendir();
                    //objDet.cod_entregarendir = cod_entregarendir; objDet.cod_empresa = cod_empresa; objDet.cod_sede_empresa = cod_sede_empresa;
                    //objDet.num_linea = 0;
                    //objDet.tipo_documento = frmModif.tipo_documento; objDet.serie_documento = frmModif.serie_documento;
                    //objDet.numero_documento = frmModif.numero_documento; objDet.cod_proveedor = frmModif.cod_proveedor;
                    //objDet = blCaja.InsertarActualizar_DetalleEntregasRendir<eEntregaRendir.eDetalle_EntregaRendir>(objDet);
                    //if (objDet == null) MessageBox.Show("Error al insertar detalle de movimiento", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnAgregarDevolucionReembolso.Enabled = false;
                    Obtener_ListadoFacturas();
                    decimal imp_Monto = (from tabla in listFacturas
                                         where tabla.tipo_documento != null
                                         select tabla.imp_total).Sum();

                    if (Convert.ToDecimal(txtMontoEntregado.EditValue) > imp_Monto) { btnAgregarDevolucionReembolso.Enabled = true; btnAgregarDevolucionReembolso.Caption = "Agregar Devolución"; }
                    if (Convert.ToDecimal(txtMontoEntregado.EditValue) < imp_Monto) { btnAgregarDevolucionReembolso.Enabled = true; btnAgregarDevolucionReembolso.Caption = "Agregar Reembolso"; }
                    if (Convert.ToDecimal(txtMontoEntregado.EditValue) == imp_Monto) btnAgregarDevolucionReembolso.Enabled = false;
                    ActualizarListado = "SI";
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
                frmModif.cod_empresa = cod_empresa;
                frmModif.EntregaRendir = "SI";
                frmModif.user = user;
                frmModif.ShowDialog();
                if (frmModif.ActualizarListado)
                {
                    eEntregaRendir.eDetalle_EntregaRendir objDet = new eEntregaRendir.eDetalle_EntregaRendir();
                    objDet.cod_entregarendir = cod_entregarendir; objDet.cod_empresa = cod_empresa; objDet.cod_sede_empresa = cod_sede_empresa;
                    objDet.num_linea = 0;
                    objDet.tipo_documento = frmModif.tipo_documento; objDet.serie_documento = frmModif.serie_documento;
                    objDet.numero_documento = frmModif.numero_documento; objDet.cod_proveedor = frmModif.cod_proveedor;
                    objDet = blCaja.InsertarActualizar_DetalleEntregasRendir<eEntregaRendir.eDetalle_EntregaRendir>(objDet);
                    if (objDet == null) MessageBox.Show("Error al insertar detalle de movimiento", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnAgregarDevolucionReembolso.Enabled = false;
                    Obtener_ListadoFacturas();
                    decimal imp_Monto = (from tabla in listFacturas
                                         where tabla.tipo_documento != null
                                         select tabla.imp_total).Sum();

                    if (Convert.ToDecimal(txtMontoEntregado.EditValue) > imp_Monto) { btnAgregarDevolucionReembolso.Enabled = true; btnAgregarDevolucionReembolso.Caption = "Agregar Devolución"; }
                    if (Convert.ToDecimal(txtMontoEntregado.EditValue) < imp_Monto) { btnAgregarDevolucionReembolso.Enabled = true; btnAgregarDevolucionReembolso.Caption = "Agregar Reembolso"; }
                    if (Convert.ToDecimal(txtMontoEntregado.EditValue) == imp_Monto) btnAgregarDevolucionReembolso.Enabled = false;
                    ActualizarListado = "SI";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                string archivo = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\MovEntregaRendir" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
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

        private void lkpEstadoAprobado_EditValueChanged(object sender, EventArgs e)
        {
            string estado = "";
            estado = lkpEstadoAprobado.Text;
            lkpEstadoAprobado.ToolTip = estado;
        }

        private void txtCodMovVinculado_Click(object sender, EventArgs e)
        {
            frmDetalleEntregaRendir frm = new frmDetalleEntregaRendir();
            frm.MiAccion = DetEntregaRendir.Editar;
            frm.user = user;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.cod_entregarendir = txtCodMovVinculado.Text;
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
                            frmModif.MiAccion = Factura.Editar;
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
                            frmModif.EntregaRendir = "SI";
                            frmModif.cod_entregarendir = cod_entregarendir;
                            frmModif.cod_trabajador = txtResponsable.Tag.ToString();
                            frmModif.cod_empresa = cod_empresa;
                            frmModif.cod_sede_empresa = cod_sede_empresa;
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
                            frmModif.MiAccion = DocInterno.Editar;
                            frmModif.tipo_documento = obj.tipo_documento;
                            frmModif.serie_documento = obj.serie_documento;
                            frmModif.numero_documento = obj.numero_documento;
                            frmModif.cod_proveedor = obj.cod_proveedor;
                            frmModif.cod_empresa = cod_empresa;
                            frmModif.EntregaRendir = "SI";
                            frmModif.user = user;
                            frmModif.ShowDialog();
                        }
                    }
                    else
                    {
                        frmDetalleEntregaRendir frm = new frmDetalleEntregaRendir();
                        frm.MiAccion = DetEntregaRendir.Editar;
                        frm.user = user;
                        frm.colorVerde = colorVerde;
                        frm.colorPlomo = colorPlomo;
                        frm.colorEventRow = colorEventRow;
                        frm.colorFocus = colorFocus;
                        frm.cod_entregarendir = obj. dsc_documento;
                        frm.cod_empresa = cod_empresa;
                        frm.cod_sede_empresa = cod_sede_empresa;
                        //frm.eMovCaja = obj;
                        frm.ShowDialog();
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
                    if (btnAgregarDevolucionReembolso.Caption == "Agregar Reembolso")
                    {
                        frmOpcionesReembolso frmOp = new frmOpcionesReembolso();
                        frmOp.user = user;
                        frmOp.cod_empresa = cod_empresa;
                        frmOp.cod_sede_empresa = cod_sede_empresa;
                        frmOp.cod_entregado_a = txtResponsable.Tag.ToString();
                        frmOp.dsc_entregado_a = txtResponsable.Text;
                        frmOp.imp_entregado = imp_Monto - Convert.ToDecimal(txtMontoEntregado.EditValue);
                        frmOp.cod_entregarendir = txtCodEntregaRendir.Text;
                        frmOp.dsc_observacion = "Reembolso relacionado a la Entrega a Rendir N° " + cod_entregarendir;
                        frmOp.ShowDialog();
                        if (frmOp.ActualizarListado == "SI")
                        {
                            //btnRendirMovimiento_ItemClick(btnRendirMovimiento, new ItemClickEventArgs(null, null));
                            RendirMovimiento();
                            Inicializar();
                        }
                    }
                    else
                    {
                        frmDetalleEntregaRendir frm = new frmDetalleEntregaRendir();
                        frm.MiAccion = DetEntregaRendir.DevRemb;
                        frm.user = user;
                        frm.colorVerde = colorVerde;
                        frm.colorPlomo = colorPlomo;
                        frm.colorEventRow = colorEventRow;
                        frm.colorFocus = colorFocus;
                        frm.cod_empresa = cod_empresa;
                        frm.cod_sede_empresa = cod_sede_empresa;
                        frm.cod_vinculo = txtCodEntregaRendir.Text;
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
            if (chkFlgRendido.CheckState == CheckState.Checked)
            {
                chkFlgPorRendir.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkFlgPorRendir.CheckState = CheckState.Checked;
            }
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                txtCodEntregaRendir.Text = "";
                cod_entregarendir = "";
                chkFlgPorRendir.CheckState = CheckState.Checked;
                lkpEmpresa.EditValue = cod_empresa;
                dtFecCreacion.EditValue = DateTime.Today;
                lkpModoReposicion.EditValue = "TRANSFER";
                lkpEstadoAprobado.EditValue = "CRE";
                txtResponsable.Tag = "";
                txtResponsable.Text = "";
                mmComentario.Text = "";
                txtUbicacion.Text = "";
                txtMontoEntregado.EditValue = 0;
                lkpTipoMoneda.EditValue = "SOL";
                txtCodMovVinculado.Text = "";
                listFacturas.Clear(); gvFacturasProveedor.RefreshData();
                chkFlgPorRendir_CheckStateChanged(null, new EventArgs());
                layoutControlItem12.Visibility = LayoutVisibility.Never;
                simpleLabelItem3.Visibility = LayoutVisibility.Never;
                layoutControlItem10.Visibility = LayoutVisibility.Never;
                this.Size = new Size(825, 340);
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
                if (dtFecCreacion.EditValue == null) { MessageBox.Show("Debe seleccionar una fecha", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtFecCreacion.Focus(); return; }
                //if (Convert.ToDecimal(txtMontoEntregado.EditValue) == 0) { MessageBox.Show("El importe debe ser mayor a 0.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtMontoEntregado.Focus(); return; }
                if (txtResponsable.Text.Trim() == "") { MessageBox.Show("Debe seleccionar un responsable.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtResponsable.Focus(); return; }

                //if (chkFlgRendido.CheckState == CheckState.Checked)
                //{
                //    if (txtRucProveedor.Text.Trim() == "") { MessageBox.Show("Debe seleccionar un proveedor.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtRucProveedor.Focus(); return; }
                //    if (txtProveedor.Text.Trim() == "") { MessageBox.Show("Debe seleccionar un proveedor.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtResponsable.Focus(); return; }
                //    if (txtGlosaMovimiento.Text.Trim() == "") { MessageBox.Show("Debe ingresar una glosa.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtGlosaMovimiento.Focus(); return; }
                //    if (lkpTipoGasto.EditValue == null) { MessageBox.Show("Debe seleccionar el tipo de gasto.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoGasto.Focus(); return; }
                //    if (lkpUnidadNegocio.EditValue == null) { MessageBox.Show("Debe seleccionar la unidad de negocio.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpUnidadNegocio.Focus(); return; }
                //    if (lkpCliente.EditValue == null) { MessageBox.Show("Debe seleccionar un cliente.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpCliente.Focus(); return; }
                //    if (txtPrefCECO.Text.Trim() == "") { MessageBox.Show("Debe ingresar un prefijo de centro de costo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtPrefCECO.Focus(); return; }
                //}

                eEntrega = AsignarValores();
                eEntrega = blCaja.InsertarActualizar_EntregasRendir<eEntregaRendir>(eEntrega);
                if (eEntrega == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                cod_entregarendir = eEntrega.cod_entregarendir; txtCodEntregaRendir.Text = cod_entregarendir;

                if (eEntrega != null)
                {
                    //if (flg_rendido == "SI")
                    //{
                    //    foreach (eCajaChica.eMovimiento_CajaChica obj in listMovimientos)
                    //    {
                    //        obj.flg_rendido = "SI"; obj.cod_usuario_registro = user.cod_usuario;
                    //        obj.cod_movimiento = obj.cod_tipo != "SA" ? obj.cod_rendicion : obj.cod_movimiento;
                    //        eCajaChica.eMovimiento_CajaChica objMov = blCaja.InsertarActualizar_MovimientosCajaChica<eCajaChica.eMovimiento_CajaChica>(obj);
                    //    }
                    //}
                    ActualizarListado = "SI";
                    if (MiAccion != DetEntregaRendir.DevRemb)
                    {
                        Editar();
                        layoutControlItem12.Visibility = LayoutVisibility.Always;
                        simpleLabelItem3.Visibility = LayoutVisibility.Always;
                        layoutControlItem10.Visibility = LayoutVisibility.Never;
                        this.Size = new Size(825, 558);
                    }
                    if (MiAccion == DetEntregaRendir.Nuevo && chkFlgRendido.CheckState == CheckState.Checked)
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
                            eEntrega = AsignarValores(); eEntrega.cod_estado = "PEN";
                            eEntrega = blCaja.InsertarActualizar_EntregasRendir<eEntregaRendir>(eEntrega);
                            if (eEntrega == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                            cod_entregarendir = eEntrega.cod_entregarendir; txtCodEntregaRendir.Text = cod_entregarendir ;
                        }
                        else
                        {
                            btnAgregarDevolucionReembolso_ItemClick(btnAgregarDevolucionReembolso, new ItemClickEventArgs(null, null));
                        }
                    }

                    MiAccion = DetEntregaRendir.Editar;

                    MessageBox.Show("Se registraron los datos de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private eEntregaRendir AsignarValores()
        {
            eEntregaRendir obj = new eEntregaRendir();
            obj.cod_entregarendir = txtCodEntregaRendir.Text;
            obj.cod_vinculo = cod_vinculo;
            obj.fch_creacion = Convert.ToDateTime(dtFecCreacion.EditValue);
            obj.cod_estado = chkFlgPorRendir.CheckState == CheckState.Checked ? "PEN" : "REN";
            obj.cod_estado_aprobado = lkpEstadoAprobado.EditValue.ToString();
            Int32 cod_tipo = Convert.ToInt32(grdbTipoMovimiento.SelectedIndex);
            obj.cod_tipo = cod_tipo == 0 ? "ER" : cod_tipo == 1 ? "DV" : cod_tipo == 2 ? "RB" : "";
            obj.cod_entregado_a = txtResponsable.Tag.ToString();
            obj.cod_empresa = cod_empresa;
            obj.cod_sede_empresa = cod_sede_empresa;
            obj.cod_moneda = lkpTipoMoneda.EditValue.ToString();
            obj.imp_monto = Convert.ToDecimal(txtMontoEntregado.EditValue);
            obj.cod_modalidad = lkpModoReposicion.EditValue.ToString();
            obj.dsc_observacion = mmComentario.Text == "" ? null : mmComentario.Text;
            obj.cod_usuario_registro = user.cod_usuario;
            obj.num_Anho = Convert.ToDateTime(dtFecCreacion.EditValue).Year;
            obj.cod_estado_aprobado = lkpEstadoAprobado.EditValue.ToString();

            return obj;
        }


    }
}