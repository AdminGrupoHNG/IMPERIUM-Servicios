using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BE_Servicios;
using BL_Servicios;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Columns;

namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    public partial class frmFacturasDetalle : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        public blFactura blFact = new blFactura();
        blLogistica blLogis = new blLogistica();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public string cod_proveedor = "", cod_tipo_fecha = "", cod_moneda = "", cod_empresa = "", cod_tipo_documento = "";
        public string FechaInicio, FechaFin;
        public bool BusquedaAutomatica = true, BusquedaLogistica = false, MostrarProveedor = false;
        public List<eFacturaProveedor.eFacturaProveedor_NotaCredito> listDocumentosNC = new List<eFacturaProveedor.eFacturaProveedor_NotaCredito>();


        public frmFacturasDetalle()
        {
            InitializeComponent();
        }

        private void frmFacturasDetalle_Load(object sender, EventArgs e)
        {
            if (BusquedaAutomatica) BuscarFacturas();
            if (!BusquedaAutomatica)
            {
                layoutControlItem10.Visibility = LayoutVisibility.Always;
                layoutControlItem6.Visibility = LayoutVisibility.Always;
                layoutControlItem23.Visibility = LayoutVisibility.Always;
                layoutControlItem7.Visibility = LayoutVisibility.Always;
                emptySpaceItem2.Visibility = LayoutVisibility.Always;
                layoutControlItem9.Visibility = LayoutVisibility.Always;
                emptySpaceItem1.Visibility = LayoutVisibility.Always;
                emptySpaceItem3.Visibility = LayoutVisibility.Always;
                blFact.CargaCombosChecked("TipoDocumento", chkcbTipoDocumento, "cod_tipo_comprobante", "dsc_tipo_comprobante", "");
                blFact.CargaCombosLookUp("TipoFecha", lkpTipoFecha, "cod_tipo_fecha", "dsc_tipo_fecha", "", valorDefecto: true);
                DateTime date = DateTime.Now;
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
                dtFechaInicio.EditValue = oPrimerDiaDelMes;
                dtFechaFin.EditValue = oUltimoDiaDelMes;
                //chkcbTipoDocumento.EditValue = cod_tipo_documento;
                chkcbTipoDocumento.SetEditValue("TC002");

                if (MostrarProveedor)
                {
                    foreach (GridColumn col in gvFacturasProveedor.Columns)
                    {
                        col.Visible = false;
                        if (col.FieldName == "dsc_tipo_documento" || col.FieldName == "dsc_documento" || col.FieldName == "dsc_glosa" ||
                            col.FieldName == "fch_documento" || col.FieldName == "cod_moneda" || col.FieldName == "imp_tipocambio" ||
                            col.FieldName == "imp_subtotal" || col.FieldName == "imp_igv" || col.FieldName == "imp_total" ||
                            col.FieldName == "imp_saldo" || col.FieldName == "dsc_proveedor") { col.Visible = true; }
                    }
                    gvFacturasProveedor.Columns["dsc_tipo_documento"].VisibleIndex = 0;
                    gvFacturasProveedor.Columns["dsc_documento"].VisibleIndex = 1;
                    gvFacturasProveedor.Columns["dsc_glosa"].VisibleIndex = 2;
                    gvFacturasProveedor.Columns["dsc_proveedor"].VisibleIndex = 3;
                    gvFacturasProveedor.Columns["fch_documento"].VisibleIndex = 4;
                    gvFacturasProveedor.Columns["cod_moneda"].VisibleIndex = 5;
                    gvFacturasProveedor.Columns["imp_tipocambio"].VisibleIndex = 6;
                    gvFacturasProveedor.Columns["imp_subtotal"].VisibleIndex = 7;
                    gvFacturasProveedor.Columns["imp_igv"].VisibleIndex = 8;
                    gvFacturasProveedor.Columns["imp_total"].VisibleIndex = 9;
                    gvFacturasProveedor.Columns["imp_saldo"].VisibleIndex = 10;
                }
                btnBuscar_Click(btnBuscar, new EventArgs());
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo documentos", "Cargando...");
                List<eFacturaProveedor> lista = new List<eFacturaProveedor>();
                if (!BusquedaLogistica)
                {
                    lista = blFact.FiltroFactura<eFacturaProveedor>(1, cod_empresa, chkcbTipoDocumento.EditValue == null ? "" : chkcbTipoDocumento.EditValue.ToString(), cod_tipo_fecha: lkpTipoFecha.EditValue == null ? "" : lkpTipoFecha.EditValue.ToString(), FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), cod_proveedor: cod_proveedor, cod_moneda: cod_moneda);
                }
                else
                {
                    lista = blLogis.Obtener_ListaLogistica<eFacturaProveedor>(18, "", cod_empresa, chkcbTipoDocumento.EditValue == null ? "" : chkcbTipoDocumento.EditValue.ToString(), cod_tipo_fecha: lkpTipoFecha.EditValue == null ? "" : lkpTipoFecha.EditValue.ToString(), FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), cod_proveedor: cod_proveedor, cod_moneda: cod_moneda);
                }
                bsFacturasProveedor.DataSource = lista;
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvFacturasProveedor_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (!BusquedaAutomatica)
                {
                    eFacturaProveedor obj = gvFacturasProveedor.GetFocusedRow() as eFacturaProveedor;
                    eFacturaProveedor.eFacturaProveedor_NotaCredito eNC = new eFacturaProveedor.eFacturaProveedor_NotaCredito();
                    eNC.tipo_documento = obj.tipo_documento; eNC.serie_documento = obj.serie_documento;
                    eNC.numero_documento = obj.numero_documento; eNC.cod_proveedor = obj.cod_proveedor;
                    eNC.dsc_glosa = obj.dsc_glosa; eNC.dsc_tipo_documento = obj.dsc_tipo_documento;
                    eNC.fch_documento = obj.fch_documento; eNC.cod_moneda = obj.cod_moneda; eNC.dsc_documento = obj.dsc_documento;
                    eNC.imp_tipocambio = obj.imp_tipocambio; eNC.imp_subtotal = obj.imp_subtotal;
                    eNC.imp_igv = obj.imp_igv; eNC.imp_total = obj.imp_total; eNC.imp_saldo = obj.imp_saldo;
                    listDocumentosNC.Add(eNC);
                    this.Close();
                }
            }
        }
        private void frmFacturasDetalle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void gvFacturasProveedor_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
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
                    if (obj == null) { return; }

                    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                    //if (Application.OpenForms["frmMantFacturaProveedor"] != null)
                    //{
                    //    Application.OpenForms["frmMantFacturaProveedor"].Activate();
                    //}
                    //else
                    //{
                        frmModif.MiAccion = Factura.Vista;
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
                        frmModif.ShowDialog();
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void BuscarFacturas()
        {
            try
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo documentos", "Cargando...");
                List<eFacturaProveedor> lista = blFact.FiltroFactura<eFacturaProveedor>(1, cod_tipo_fecha: cod_tipo_fecha, FechaInicio: FechaInicio, FechaFin: FechaFin, cod_proveedor: cod_proveedor, cod_moneda: cod_moneda);
                bsFacturasProveedor.DataSource = lista;
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}