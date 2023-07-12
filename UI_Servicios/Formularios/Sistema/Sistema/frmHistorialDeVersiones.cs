using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using BE_Servicios;
using BL_Servicios;

namespace UI_Servicios.Formularios.Sistema.Sistema
{
    public partial class frmHistorialVersiones : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        blVersion blVers = new blVersion();

        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;

        public frmHistorialVersiones()
        {
            InitializeComponent();
        }

        private void frmHistorialVersiones_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            DateTime date = DateTime.Now;
            dtpFecPublicacion.EditValue = date;

            BuscarVersiones();
        }

        private void BuscarVersiones()
        {
            List<eVersion> versiones = blVers.ListarHistorialVersiones<eVersion>(1);
            if (versiones.Count == 0) return;
            List<eVersion.eVersionDetalle> detVersiones = blVers.Cargar_HistorialVersiones_Detalle<eVersion.eVersionDetalle>(2, versiones[0].cod_version, versiones[0].dsc_version);

            bsListadoVersiones.DataSource = versiones;
            bsListadoDetalle.DataSource = detVersiones;

            gvVersiones.FocusedRowHandle = 0;
        }

        private void btnPublicar_Click(object sender, EventArgs e)
        {
            try
            {
                eVersion eVer = gvVersiones.GetFocusedRow() as eVersion;

                if (eVer.dsc_version != null)
                {
                    string respuesta = "";
                    DialogResult result = MessageBox.Show("¿Desea publicar la versión " + eVer.dsc_version.ToString() + "?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.No) return;

                    respuesta = blVers.Publicar_Version(eVer.dsc_version);

                    if (respuesta == "OK") MessageBox.Show("Cambios Publicados con éxito.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Publicar los Cambios.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvVersiones_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                eVersion obj = gvVersiones.GetFocusedRow() as eVersion;
                List<eVersion.eVersionDetalle> detVersiones = new List<eVersion.eVersionDetalle>();
                if (obj != null) detVersiones = blVers.Cargar_HistorialVersiones_Detalle<eVersion.eVersionDetalle>(2, obj.cod_version, obj.dsc_version);
                bsListadoDetalle.DataSource = detVersiones;
            }
        }

        private void gvVersiones_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gvVersiones.FocusedRowHandle >= 0)
            {
                if (gvVersiones.FocusedColumn.Name != "colbtn_eliminar")
                {
                    e.Cancel = true;
                }
            }
        }

        private void gvVersiones_HiddenEditor(object sender, EventArgs e)
        {
            if (gvVersiones.FocusedColumn.Name != "colbtn_eliminar")
            {
                eVersion eVer = gvVersiones.GetFocusedRow() as eVersion;

                if (eVer.dsc_version != null)
                {
                    eVer.fch_publicacion = Convert.ToDateTime(dtpFecPublicacion.EditValue);

                    eVer = blVers.Ins_Act_HistorialVersiones<eVersion>(eVer, user.cod_usuario);
                }

                BuscarVersiones();
            }
        }

        private void gvVersiones_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvVersiones_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvVersiones_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvVersiones_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void rbtnElimVersion_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string respuesta = "";

            try
            {
                eVersion obj = gvVersiones.GetRow(gvVersiones.FocusedRowHandle) as eVersion;

                respuesta = blVers.Elim_HistorialVersiones(obj.cod_version, obj.dsc_version);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al eliminar el registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarVersiones();
        }

        private void gvDetalle_HiddenEditor(object sender, EventArgs e)
        {
            eVersion eVer = gvVersiones.GetFocusedRow() as eVersion;
            eVersion.eVersionDetalle eDet = gvDetalle.GetFocusedRow() as eVersion.eVersionDetalle;
            if (eDet == null) return;
            eDet.cod_version = eVer.cod_version;
            eDet.dsc_version = eVer.dsc_version;

            eDet = blVers.Ins_Act_Detalle_HistorialVersiones<eVersion.eVersionDetalle>(eDet, user.cod_usuario);

            BuscarVersiones();
        }

        private void gvDetalle_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvDetalle_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvDetalle_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvDetalle_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void rbtnElimDetalle_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string respuesta = "";

            try
            {
                eVersion.eVersionDetalle obj = gvDetalle.GetRow(gvDetalle.FocusedRowHandle) as eVersion.eVersionDetalle;

                respuesta = blVers.Elim_HistorialVersiones_Detalle(obj.cod_version, obj.dsc_version, obj.num_item);

                bsListadoDetalle.Remove(obj);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al eliminar el registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}