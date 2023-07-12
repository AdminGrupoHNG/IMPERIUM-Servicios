using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BE_Servicios;
using BL_Servicios;

namespace UI_Servicios.Formularios.Sistema.Configuraciones_Maestras
{
    public partial class frmMantTipoGastoCosto : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        blEncrypta blEncryp = new blEncrypta();
        public blFactura blFact = new blFactura();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;

        public frmMantTipoGastoCosto()
        {
            InitializeComponent();
        }

        private void frmMantGastoCosto_Load(object sender, EventArgs e)
        {
            Obtener_ListaGastoCosto();
        }

        private void Obtener_ListaGastoCosto()
        {
            List<eTipoGastoCosto> lista = new List<eTipoGastoCosto>();
            lista = blFact.Obtener_MaestrosGenerales<eTipoGastoCosto>(10, "");
            bsTipoGastoCosto.DataSource = lista;
        }

        private void frmMantTipoGastoCosto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void rbtnEliminar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de eliminar el registro?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eTipoGastoCosto obj = gvTipoGastoCosto.GetFocusedRow() as eTipoGastoCosto;
                if (obj == null) return;
                obj.cod_tipo_gasto = "00001";
                string result = blFact.EliminarMaestrosGenerales(1, cod_tipo_gasto: obj.cod_tipo_gasto);
                if (result != "OK") { MessageBox.Show("Error al eliminar registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                Obtener_ListaGastoCosto();
            }
        }

        private void gvTipoGastoCosto_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvTipoGastoCosto_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvTipoGastoCosto_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                eTipoGastoCosto objTip = gvTipoGastoCosto.GetFocusedRow() as eTipoGastoCosto;
                if (objTip != null)
                {
                    //objTip.cod_tipo_gasto = "00001";
                    eTipoGastoCosto obj = blFact.InsertarTipoGastoCosto<eTipoGastoCosto>(objTip);
                    if (obj == null)
                    {
                        MessageBox.Show("Error al insertar unidad de negocio", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Obtener_ListaGastoCosto();
                        return;
                    }
                    objTip.dsc_pref_ceco = obj.dsc_pref_ceco;
                    Obtener_ListaGastoCosto();
                    gvTipoGastoCosto.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}