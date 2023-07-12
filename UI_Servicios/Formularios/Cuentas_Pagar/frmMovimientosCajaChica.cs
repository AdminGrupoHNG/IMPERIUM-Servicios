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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;

namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    internal enum Movimiento
    {
        Nuevo = 0,
        Editar = 1,
        Rendir = 2
    }
    public partial class frmMovimientosCajaChica : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        internal Movimiento MiAccion = Movimiento.Nuevo;
        blProveedores blProv = new blProveedores();
        blFactura blFact = new blFactura();
        blGlobales blGlobal = new blGlobales();
        blCajaChica blCaja = new blCajaChica();
        blTrabajador blTrab = new blTrabajador();
        List<eCajaChica.eMovimiento_CajaChica> listMovimientos = new List<eCajaChica.eMovimiento_CajaChica>();
        public string ActualizarListado = "NO", cod_empresa = "", cod_sede_empresa = "", cod_caja = "";
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        Brush FlgPorRendir = Brushes.Red;
        Brush FlgRendido = Brushes.Green;
        Brush FlgApertura = Brushes.Violet;
        int markWidth = 16;

        public decimal imp_saldo = 0;
        //string cod_caja = "";

        public frmMovimientosCajaChica()
        {
            InitializeComponent();
        }

        private void frmMovimientosCajaChica_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            try
            {
                CargarLookUpEdit();
                switch (MiAccion)
                {
                    case Movimiento.Nuevo:
                        break;
                    case Movimiento.Editar:
                        lkpEmpresa.EditValue = cod_empresa;
                        gvHistorialMovimientos.OptionsView.AllowCellMerge = false;
                        gvHistorialMovimientos.Columns["cod_rendicion"].Visible = false;
                        btnRendirCaja.Enabled = false;
                        break;
                    case Movimiento.Rendir:
                        lkpEmpresa.EditValue = cod_empresa;
                        lkpTipoCaja.EditValue = cod_caja;
                        //btnNuevo.Enabled = false;
                        //btnEliminar.Enabled = false;
                        layoutControlItem12.Visibility = LayoutVisibility.Always;
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
            blFact.CargaCombosLookUp("EmpresaProveedor", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true);
            blProv.CargaCombosLookUp("Moneda", lkpTipoMoneda, "cod_moneda", "dsc_moneda", "", valorDefecto: true);
        }

        private void gvHistorialMovimientos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvHistorialMovimientos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    e.DefaultDraw();
                    if (e.Column.FieldName == "cod_estado")
                    {
                        Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        string cellValue = e.CellValue.ToString();
                        if (cellValue == "PEN") { b = FlgPorRendir; } else if (cellValue == "REN") { b = FlgRendido; } else { b = FlgApertura; }
                        e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvHistorialMovimientos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                blGlobal.Pintar_EstiloGrilla(sender, e);
                GridView view = sender as GridView;
                if (view.Columns["dsc_estado"] != null)
                {
                    string estado = view.GetRowCellDisplayText(e.RowHandle, view.Columns["dsc_tipo"]);
                    //if (estado == "RENDIDO") e.Appearance.ForeColor = Color.Blue;
                    if (estado != "SALIDA") { e.Appearance.ForeColor = Color.DarkGoldenrod; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                }
            }
        }

        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            blTrab.CargaCombosLookUp("SedesEmpresa", lkpSedeEmpresaInfoLaboral, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, lkpEmpresa.EditValue.ToString());
            lkpSedeEmpresaInfoLaboral.EditValue = null; txtResponsable.Tag = null; txtResponsable.Text = ""; listMovimientos.Clear(); gvHistorialMovimientos.RefreshData();
            List<eTrabajador.eInfoLaboral_Trabajador> lista = blTrab.ListarOpcionesTrabajador<eTrabajador.eInfoLaboral_Trabajador>(6, lkpEmpresa.EditValue.ToString());
            if (lista.Count == 1) lkpSedeEmpresaInfoLaboral.EditValue = lista[0].cod_sede_empresa;
            if (lkpSedeEmpresaInfoLaboral.EditValue == null) lkpSedeEmpresaInfoLaboral.EditValue = cod_sede_empresa;
        }

        private void lkpSedeEmpresaInfoLaboral_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpSedeEmpresaInfoLaboral.EditValue != null)
            {
                blCaja.CargaCombosLookUp("TipoCajaEmpresa", lkpTipoCaja, "cod_caja", "dsc_caja", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString(), cod_sede_empresa: lkpSedeEmpresaInfoLaboral.EditValue.ToString());
                lkpTipoCaja.EditValue = null;
                List<eCajaChica> lista = new List<eCajaChica>();
                lista = blCaja.ListarDatos_CajaChica<eCajaChica>(9, "", "", cod_empresa: lkpEmpresa.EditValue.ToString(), cod_sede_empresa: lkpSedeEmpresaInfoLaboral.EditValue.ToString());
                if (lista.Count == 1) lkpTipoCaja.EditValue = lista[0].cod_caja;
                if (MiAccion == Movimiento.Editar && cod_caja != "") lkpTipoCaja.EditValue = cod_caja;
            }
            else
            {
                txtResponsable.Tag = null; txtResponsable.Text = ""; txtImporteCaja.EditValue = 0; txtMontoSaldo.EditValue = 0;
            }
        }

        private void lkpTipoCaja_EditValueChanged(object sender, EventArgs e)
        {
            txtResponsable.Tag = null; txtResponsable.Text = ""; txtImporteCaja.EditValue = 0; txtMontoSaldo.EditValue = 0;
            if (lkpTipoCaja.EditValue != null) ObtenerMovimientos();
        }

        private void ObtenerMovimientos()
        {
            eCajaChica.eMovimiento_CajaChica obj = new eCajaChica.eMovimiento_CajaChica();
            //obj = blCaja.ObtenerDatos_CajaChica<eCajaChica.eMovimiento_CajaChica>(2, cod_empresa: lkpEmpresa.EditValue.ToString(), cod_sede_empresa: lkpSedeEmpresaInfoLaboral.EditValue.ToString());
            obj = blCaja.ObtenerDatos_CajaChica<eCajaChica.eMovimiento_CajaChica>(10, cod_caja: lkpTipoCaja.EditValue.ToString());
            lkpTipoMoneda.EditValue = obj.cod_moneda; txtResponsable.Tag = obj.cod_responsable; txtResponsable.Text = obj.dsc_responsable;
            txtImporteCaja.EditValue = obj.imp_monto; cod_caja = obj.cod_caja;

            listMovimientos = blCaja.ListarDatos_CajaChica<eCajaChica.eMovimiento_CajaChica>(3, lkpTipoCaja.EditValue.ToString(), "");
            bsListadoMovimientos.DataSource = listMovimientos;

            //decimal imp_salida_REN = (from tabla in listMovimientos
            //                          where tabla.cod_tipo == "SA"// && tabla.cod_estado == "REN"
            //                          select tabla.imp_entregado).Sum();
            //decimal imp_devolucion_REN = (from tabla in listMovimientos
            //                              where tabla.cod_tipo == "DV" //&& tabla.cod_estado == "REN"
            //                              select tabla.imp_entregado).Sum();
            //decimal imp_reembolso_REN = (from tabla in listMovimientos
            //                             where tabla.cod_tipo == "RB" //&& tabla.cod_estado == "REN"
            //                             select tabla.imp_entregado).Sum();

            if (MiAccion == Movimiento.Editar)
            {
                List<eCajaChica.eMovimiento_CajaChica> NewlistMovimientos = new List<eCajaChica.eMovimiento_CajaChica>();
                NewlistMovimientos = listMovimientos.FindAll(x => x.cod_estado == "PEN");
                listMovimientos.Clear(); listMovimientos.AddRange(NewlistMovimientos);
            }
            decimal imp_salida_PEN = 0, imp_devolucion_PEN = 0, imp_reembolso_PEN = 0;
            if (MiAccion == Movimiento.Rendir)
            {
                List<eCajaChica.eMovimiento_CajaChica> NewlistMovimientos = new List<eCajaChica.eMovimiento_CajaChica>();
                NewlistMovimientos = listMovimientos.FindAll(x => x.cod_estado == "REN");
                listMovimientos.Clear(); listMovimientos.AddRange(NewlistMovimientos);

                imp_salida_PEN = (from tabla in NewlistMovimientos
                                    where tabla.cod_tipo == "SA"
                                    select tabla.imp_entregado).Sum();
                imp_devolucion_PEN = (from tabla in NewlistMovimientos
                                    where tabla.cod_tipo == "DV"
                                    select tabla.imp_entregado).Sum();
                imp_reembolso_PEN = (from tabla in NewlistMovimientos
                                    where tabla.cod_tipo == "RB"
                                    select tabla.imp_entregado).Sum();
            }

            //decimal imp_salida_PEN = (from tabla in listMovimientos 
            //                      where tabla.cod_tipo == "SA"
            //                     select tabla.imp_entregado).Sum();
            //decimal imp_devolucion_PEN = (from tabla in listMovimientos 
            //                          where tabla.cod_tipo == "DV"
            //                        select tabla.imp_entregado).Sum();
            //decimal imp_reembolso_PEN = (from tabla in listMovimientos
            //                         where tabla.cod_tipo == "RB"
            //                         select tabla.imp_entregado).Sum();
            //decimal imp_saldo_REN = Convert.ToDecimal(txtImporteCaja.EditValue) - imp_salida_REN + imp_devolucion_REN - imp_reembolso_REN;
            ////txtMontoSaldo.EditValue = Convert.ToDecimal(txtMontoApertura.EditValue) - imp_salida_PEN + imp_devolucion_PEN - imp_reembolso_PEN;
            ////txtMontoSaldo.EditValue = imp_saldo_REN - imp_salida_PEN + imp_devolucion_PEN - imp_reembolso_PEN;
            ////txtMontoSaldo.EditValue = obj.imp_monto - imp_salida_REN;
            //decimal imp_saldo_caja = Convert.ToDecimal(txtImporteCaja.EditValue) - imp_salida_REN + imp_devolucion_REN - imp_reembolso_REN;

            //txtMontoSaldo.EditValue = imp_saldo;
            //txtMontoSaldo.ForeColor = imp_saldo < 0 ? Color.DarkRed : Color.Black;

            gvHistorialMovimientos.RefreshData();
            if (MiAccion == Movimiento.Rendir)
            {
                txtMontoSaldo.EditValue = imp_saldo;
                txtMontoSaldo.ForeColor = imp_saldo < 0 ? Color.DarkRed : Color.Black;
                //decimal imp_reponer = Convert.ToDecimal(txtImporteCaja.EditValue) - Convert.ToDecimal(txtMontoSaldo.EditValue);
                
                decimal imp_reponer = imp_salida_PEN + imp_reembolso_PEN - imp_devolucion_PEN;
                txtMontoReponer.EditValue = imp_reponer;
                txtMontoReponer.ForeColor = imp_reponer < 0 ? Color.DarkRed : Color.Black;
            }
        }

        private void frmMovimientosCajaChica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && MiAccion != Movimiento.Nuevo) this.Close();
            if (e.KeyCode == Keys.F5) ObtenerMovimientos();
        }

        private void gvHistorialMovimientos_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                eCajaChica.eMovimiento_CajaChica obj = gvHistorialMovimientos.GetFocusedRow() as eCajaChica.eMovimiento_CajaChica;
                frmDetalleMovimiento frm = new frmDetalleMovimiento();
                frm.MiAccion = DetMovimiento.Editar;
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.cod_caja = obj.cod_caja;
                frm.cod_movimiento = obj.cod_movimiento;
                frm.cod_empresa = lkpEmpresa.EditValue.ToString();
                frm.eMovCaja = obj;
                frm.ShowDialog();
            }
        }

        private void btnRendirMovimiento_Click(object sender, EventArgs e)
        {
            try
            {
                eCajaChica.eMovimiento_CajaChica obj = gvHistorialMovimientos.GetFocusedRow() as eCajaChica.eMovimiento_CajaChica;
                if (obj.cod_estado == "PEN")
                {
                    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                    frmModif.MiAccion = Factura.Nuevo;
                    frmModif.colorVerde = colorVerde;
                    frmModif.colorPlomo = colorPlomo;
                    frmModif.colorFocus = colorFocus;
                    frmModif.colorEventRow = colorEventRow;
                    frmModif.user = user;
                    frmModif.CajaChica = "SI";
                    frmModif.cod_caja = obj.cod_caja;
                    frmModif.cod_movimiento = obj.cod_movimiento;
                    frmModif.ShowDialog();
                    if (frmModif.ActualizarListado)
                    {
                        eCajaChica.eDetalleMov_CajaChica objDet = new eCajaChica.eDetalleMov_CajaChica();
                        objDet.cod_caja = obj.cod_caja; objDet.cod_movimiento = obj.cod_movimiento;
                        objDet.num_linea = 0;
                        objDet.tipo_documento = frmModif.tipo_documento; objDet.serie_documento = frmModif.serie_documento;
                        objDet.numero_documento = frmModif.numero_documento; objDet.cod_proveedor = frmModif.cod_proveedor;
                        objDet = blCaja.InsertarActualizar_DetalleMovCajaChica<eCajaChica.eDetalleMov_CajaChica>(objDet);
                        if (objDet == null) MessageBox.Show("Error al insertar detalle de movimiento", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        obj.cod_estado = "REN"; obj.cod_usuario_registro = user.cod_usuario; 
                        obj.dsc_ruc = frmModif.RUC; obj.cod_proveedor = frmModif.cod_proveedor;
                        obj = blCaja.InsertarActualizar_MovimientosCajaChica<eCajaChica.eMovimiento_CajaChica>(obj);
                        if (obj == null) MessageBox.Show("Error al actualizar movimiento", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        ObtenerMovimientos();
                    }
                }
                else
                {
                    MessageBox.Show("Solo se puede rendir los movimientos que no estan por rendir.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnRendirCaja_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtMontoReponer.EditValue) < 0) { MessageBox.Show("No puede reponer monto menor a cero.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                frmDetalleMovimiento frm = new frmDetalleMovimiento();
                frm.MiAccion = DetMovimiento.DevRemb;
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.cod_caja = cod_caja;
                frm.cod_empresa = lkpEmpresa.EditValue.ToString();
                frm.cod_tipo = "RP";
                frm.cod_entregado_a = txtResponsable.Tag.ToString();
                frm.dsc_entregado_a = txtResponsable.Text;
                frm.chkFlgPorRendir.Enabled = false;
                frm.chkFlgRendido.Enabled = false;
                frm.grdbTipoMovimiento.Enabled = false;
                frm.flg_rendido = "SI";
                frm.imp_entregado = Convert.ToDecimal(txtMontoReponer.Text); //Convert.ToDecimal(txtImporteCaja.EditValue) - Convert.ToDecimal(txtMontoSaldo.EditValue);
                frm.listMovimientos = listMovimientos;
                frm.ShowDialog();
                if (frm.ActualizarListado == "SI") { ObtenerMovimientos(); ActualizarListado = frm.ActualizarListado; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                frmDetalleMovimiento frm = new frmDetalleMovimiento();
                frm.MiAccion = DetMovimiento.Nuevo;
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.cod_caja = cod_caja;
                frm.cod_empresa = lkpEmpresa.EditValue.ToString();
                frm.ShowDialog();
                if (frm.ActualizarListado == "SI") { ObtenerMovimientos(); ActualizarListado = frm.ActualizarListado; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                eCajaChica.eMovimiento_CajaChica obj = gvHistorialMovimientos.GetFocusedRow() as eCajaChica.eMovimiento_CajaChica;
                if (obj == null) return;
                List<eFacturaProveedor> listFacturas = blCaja.ListarDatos_CajaChica<eFacturaProveedor>(5, cod_caja, obj.cod_movimiento);
                if (listFacturas.Count > 0) { MessageBox.Show("No se puede eliminar un movimiento con documentos vinculados", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                string result = blCaja.Eliminar_MovimientoCajaChica(1, obj);
                if (result != "OK") MessageBox.Show("Error al eliminar movimiento", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == "OK") MessageBox.Show("Se eliminó el movimiento de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ObtenerMovimientos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}