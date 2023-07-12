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
using DevExpress.Utils.Drawing;

namespace UI_Servicios.Formularios.Shared
{
    public partial class frmBusquedaTrabajador : DevExpress.XtraEditors.XtraForm
    {
        public eTrabajador eTrab = new eTrabajador();
        blGlobales blGlobal = new blGlobales();
        blUsuario blUsu = new blUsuario();
        public int opcion = 0;
        public int multiseleccion = 0;
        public int tiposeleccion = 0;
        public int Vertiposeleccion = 1;
        public frmBusquedaTrabajador()
        {
            InitializeComponent();
            

        }

        private void gcListadoTrabajadores_Click(object sender, EventArgs e)
        {

        }

        private void frmBusquedaTrabajador_Load(object sender, EventArgs e)
        {
            if (multiseleccion == 1) {
                this.layoutBoton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutTipoSeleccion.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.emptySpaceItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                this.gvListadoTrabajadores.OptionsSelection.MultiSelect = true;
                this.gvListadoTrabajadores.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                radioGroup1.SelectedIndex = 0;
                if (Vertiposeleccion == 0) { radioGroup1.SelectedIndex = 1;  this.layoutTipoSeleccion.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;}
            }
            else
            {
                this.gvListadoTrabajadores.OptionsSelection.MultiSelect = false;
                this.gvListadoTrabajadores.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;

            }
            
            CargarPerfilTrabajdor();
        }

        public void CargarPerfilTrabajdor()
        {

            List<eTrabajador> ListadoTrabajadores = new List<eTrabajador>();
            ListadoTrabajadores = blUsu.ObtenerTrabajadores<eTrabajador>(opcion);
            bsListadoTrabajadores.DataSource = null; bsListadoTrabajadores.DataSource = ListadoTrabajadores;
        }

        private void gvListadoTrabajadores_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eTrab = gvListadoTrabajadores.GetFocusedRow() as eTrabajador;
                    this.Close();
                    
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoTrabajadores_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void frmBusquedaTrabajador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregarTrabajador_Click(object sender, EventArgs e)
        {
                if (gvListadoTrabajadores.SelectedRowsCount > 0 && multiseleccion == 1) 
                {
                    eTrab.cod_trabajador = "";
                        for (int x = 0; x <= gvListadoTrabajadores.RowCount - 1; x++)
                        {
                       
                            eTrabajador obj = gvListadoTrabajadores.GetRow(x) as eTrabajador;
                            if (obj != null)
                            {
                                if (obj.seleccionado == 1)
                                {
                                eTrab.cod_trabajador = eTrab.cod_trabajador + obj.cod_trabajador + ",";
                                }
                            }
                        }
                }
            tiposeleccion = Convert.ToInt32(radioGroup1.EditValue);
            this.Close();
        }

        private void gvListadoTrabajadores_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }
    }
}