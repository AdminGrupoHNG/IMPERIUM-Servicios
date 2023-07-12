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
using DevExpress.XtraGrid.Views.Grid;

namespace UI_Servicios.Formularios.Sistema.Configuraciones_Maestras
{
    public partial class frmMantUnidades_Negocio : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        public blFactura blFact = new blFactura();
        List<eEmpresa> listEmpresa = new List<eEmpresa>();
        List<eUnidadNegocio> listUnidadNegocio = new List<eUnidadNegocio>();
        List<eTipoGastoCosto> listTipoGastoCosto = new List<eTipoGastoCosto>();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;

        public frmMantUnidades_Negocio()
        {
            InitializeComponent();
        }

        private void frmUnidades_Negocio_Load(object sender, EventArgs e)
        {
            Inicializar();
        }
        private void Inicializar()
        {
            listEmpresa = blFact.Obtener_MaestrosGenerales<eEmpresa>(8, "");
            bsEmpresas.DataSource = listEmpresa;
            rlkpflgDefecto.DataSource = blFact.CombosEnGridControl<eUnidadNegocio>("FlagDefecto");
        }

        private void gvEmpresas_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eEmpresa obj = gvEmpresas.GetFocusedRow() as eEmpresa;
                    listUnidadNegocio = blFact.Obtener_MaestrosGenerales<eUnidadNegocio>(9, obj.cod_empresa);
                    bsUnidadNegocio.DataSource = listUnidadNegocio;
                    listTipoGastoCosto = blFact.Obtener_MaestrosGenerales<eTipoGastoCosto>(10, obj.cod_empresa);
                    bsTipoGastoCosto.DataSource = listTipoGastoCosto;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvEmpresas_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eEmpresa obj = gvEmpresas.GetFocusedRow() as eEmpresa;
                    listUnidadNegocio = blFact.Obtener_MaestrosGenerales<eUnidadNegocio>(9, obj.cod_empresa);
                    bsUnidadNegocio.DataSource = listUnidadNegocio;
                    listTipoGastoCosto = blFact.Obtener_MaestrosGenerales<eTipoGastoCosto>(10, obj.cod_empresa);
                    bsTipoGastoCosto.DataSource = listTipoGastoCosto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvUnidadNegocio_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                eEmpresa objEmp = gvEmpresas.GetFocusedRow() as eEmpresa;
                eUnidadNegocio objUN = gvUnidadNegocio.GetFocusedRow() as eUnidadNegocio;
                if (objUN != null)
                {
                    objUN.cod_empresa = objEmp.cod_empresa;
                    eUnidadNegocio obj = blFact.InsertarUnidadNegocio<eUnidadNegocio>(objUN);
                    if (obj == null)
                    {
                        MessageBox.Show("Error al insertar unidad de negocio", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        listUnidadNegocio = blFact.Obtener_MaestrosGenerales<eUnidadNegocio>(9, objEmp.cod_empresa);
                        bsUnidadNegocio.DataSource = listUnidadNegocio;
                        return;
                    }
                    objUN.cod_und_negocio = obj.cod_und_negocio;
                    gvUnidadNegocio.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvEmpresas_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvEmpresas_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvUnidadNegocio_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void rbtnEliminar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de eliminar el registro?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eUnidadNegocio obj = gvUnidadNegocio.GetFocusedRow() as eUnidadNegocio;
                if (obj == null) return;
                string result = blFact.EliminarMaestrosGenerales(2, cod_empresa: obj.cod_empresa, cod_und_negocio: obj.cod_und_negocio);
                if (result != "OK") { MessageBox.Show("Error al eliminar registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                eEmpresa objEmp = gvEmpresas.GetFocusedRow() as eEmpresa;
                listUnidadNegocio = blFact.Obtener_MaestrosGenerales<eUnidadNegocio>(9, objEmp.cod_empresa);
                bsUnidadNegocio.DataSource = listUnidadNegocio;
            }
        }

        private void gvTipoGastoCosto_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvTipoGastoCosto_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvTipoGastoCosto_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                eEmpresa objEmp = gvEmpresas.GetFocusedRow() as eEmpresa;
                eTipoGastoCosto objTip = gvTipoGastoCosto.GetFocusedRow() as eTipoGastoCosto;
                if (objTip != null)
                {
                    objTip.cod_empresa = objEmp.cod_empresa; //objTip.cod_tipo_gasto = "00001";
                    eTipoGastoCosto obj = blFact.InsertarTipoGastoCosto<eTipoGastoCosto>(objTip);
                    if (obj == null)
                    {
                        MessageBox.Show("Error al insertar tipo de gasto-costo", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        listTipoGastoCosto = blFact.Obtener_MaestrosGenerales<eTipoGastoCosto>(10, objEmp.cod_empresa);
                        bsTipoGastoCosto.DataSource = listTipoGastoCosto;
                        return;
                    }
                    objTip.cod_tipo_gasto = obj.cod_tipo_gasto;
                    gvTipoGastoCosto.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rbtnEliminarTipoGasto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de eliminar el registro?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eEmpresa objEmp = gvEmpresas.GetFocusedRow() as eEmpresa;
                eTipoGastoCosto obj = gvTipoGastoCosto.GetFocusedRow() as eTipoGastoCosto;
                if (obj == null) return;
                //obj.cod_tipo_gasto = "00001";
                string result = blFact.EliminarMaestrosGenerales(1, cod_tipo_gasto: obj.cod_tipo_gasto, cod_empresa: objEmp.cod_empresa);
                if (result != "OK") { MessageBox.Show("Error al eliminar registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                listTipoGastoCosto = blFact.Obtener_MaestrosGenerales<eTipoGastoCosto>(10, objEmp.cod_empresa);
                bsTipoGastoCosto.DataSource = listTipoGastoCosto;
            }
        }

        private void gvUnidadNegocio_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvUnidadNegocio_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView view = sender as GridView;
                    string campo = e.Column.FieldName;
                    if (view.GetRowCellValue(e.RowHandle, "flg_activo") != null && view.GetRowCellValue(e.RowHandle, "flg_activo").ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvUnidadNegocio_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            eUnidadNegocio obj = gvUnidadNegocio.GetRow(e.RowHandle) as eUnidadNegocio;
            obj.flg_activo = "SI"; obj.flg_defecto = "NO";
        }
    }
}