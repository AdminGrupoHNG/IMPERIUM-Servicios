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
using BL_Servicios;
using BE_Servicios;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid;

namespace UI_Servicios.Formularios.Logistica
{
    public partial class frmListaProductosSunat : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        blLogistica blLogis = new blLogistica();
        List<eProductos_SUNAT> ListProductos = new List<eProductos_SUNAT>();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public string filtro = "";
        public string codigo { get; set; }
        public string descripcion { get; set; }


        public frmListaProductosSunat()
        {
            InitializeComponent();
        }

        private void frmProductosSunat_Load(object sender, EventArgs e)
        {
            blLogis.CargaCombosLookUp("Segmento", lkpSegmento, "cod_segmento", "dsc_segmento", "", valorDefecto: true);
            Inicializar();
        }

        private void Inicializar()
        {
            if (filtro != "")
            {
                gcListadoProductos.Select();
                gcListadoProductos.ForceInitialize();
                gvListadoProductos.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                gvListadoProductos.FocusedColumn = gvListadoProductos.Columns["dsc_producto"];
                gvListadoProductos.SetRowCellValue(GridControl.AutoFilterRowHandle, gvListadoProductos.Columns["dsc_razon_social"], filtro);
                //gvListadoProductos.SetAutoFilterValue(gvListadoProductos.Columns["dsc_producto"], filtro, DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains);

                gvListadoProductos.ShowEditor();
            }
        }

        private void Obtener_Productos(string cod_segmento, string cod_familia, string cod_clase)
        {
            ListProductos.Clear();
            ListProductos = blLogis.Obtener_ListadosProductos<eProductos_SUNAT>(1, cod_segmento, cod_familia, cod_clase);
            bsListadoProductos.DataSource = ListProductos;
        }

        private void btnMostrarProductos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");
            btnLimpiar_ItemClick(btnLimpiar, new DevExpress.XtraBars.ItemClickEventArgs(null, null));
            Obtener_Productos("", "", "");
            SplashScreenManager.CloseForm();
        }

        private void gvListadoProductos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoProductos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoProductos_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2 && e.RowHandle >= 0)
            {
                PasarDatos();
                this.Close();
            }
        }

        private void gvListadoProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (gvListadoProductos.FocusedRowHandle >= 0 && e.KeyCode == Keys.Enter)
            {
                PasarDatos();
                this.Close();
            }
        }
        public void PasarDatos()
        {
            eProductos_SUNAT eProducto= gvListadoProductos.GetFocusedRow() as eProductos_SUNAT;
            descripcion = eProducto.dsc_producto;
            codigo = eProducto.cod_producto;
        }

        private void btnLimpiar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lkpClase.EditValue = null; lkpFamilia.EditValue = null;
            lkpClase.Properties.DataSource = null; lkpFamilia.Properties.DataSource = null;
            lkpSegmento.EditValue = null; ListProductos.Clear(); gvListadoProductos.RefreshData();
        }

        private void frmListaProductosSunat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) Obtener_Productos(lkpSegmento.EditValue == null ? "" : lkpSegmento.EditValue.ToString(), lkpFamilia.EditValue == null ? "" : lkpFamilia.EditValue.ToString(), lkpClase.EditValue == null ? null : lkpClase.EditValue.ToString());
        }

        private void lkpSegmento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) { lkpSegmento.EditValue = null; lkpSegmento.Properties.DataSource = null; lkpFamilia.EditValue = null; lkpFamilia.Properties.DataSource = null; lkpClase.EditValue = null; lkpClase.Properties.DataSource = null; }
        }

        private void lkpFamilia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) { lkpFamilia.EditValue = null; lkpFamilia.Properties.DataSource = null; lkpClase.EditValue = null; lkpClase.Properties.DataSource = null; }
        }

        private void lkpClase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) { lkpClase.EditValue = null; lkpClase.Properties.DataSource = null; }
        }

        private void lkpSegmento_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpSegmento.EditValue != null)
            {
                lkpFamilia.EditValue = null; lkpFamilia.Properties.DataSource = null; lkpClase.EditValue = null; lkpClase.Properties.DataSource = null;
                blLogis.CargaCombosLookUp("Familia", lkpFamilia, "cod_familia", "dsc_familia", "", valorDefecto: true, lkpSegmento.EditValue.ToString());
                Obtener_Productos(lkpSegmento.EditValue.ToString(), "", "");
            }
        }

        private void lkpFamilia_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpFamilia.EditValue != null)
            {
                lkpClase.EditValue = null; lkpClase.Properties.DataSource = null;
                blLogis.CargaCombosLookUp("Clase", lkpClase, "cod_clase", "dsc_clase", "", valorDefecto: true, lkpSegmento.EditValue.ToString(), lkpFamilia.EditValue.ToString());
                Obtener_Productos(lkpSegmento.EditValue.ToString(), lkpFamilia.EditValue.ToString(), "");
            }
        }

        private void lkpClase_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpClase.EditValue != null) Obtener_Productos(lkpSegmento.EditValue.ToString(), lkpFamilia.EditValue.ToString(), lkpClase.EditValue.ToString());
        }
    }
}