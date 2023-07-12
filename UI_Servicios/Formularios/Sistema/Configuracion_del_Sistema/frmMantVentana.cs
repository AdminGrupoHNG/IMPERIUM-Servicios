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
using BL_Servicios;
using BE_Servicios;

namespace UI_Servicios.Formularios.Sistema.Configuracion_del_Sistema
{
    public partial class frmMantVentana : DevExpress.XtraEditors.XtraForm
    {

        internal enum Ventana
        {
            Nuevo = 0,
            Editar = 1
        }

        frmOpcionesSistema frmHandler = new frmOpcionesSistema();
        internal Ventana MiAccion = Ventana.Nuevo;
        blSistema blSis = new blSistema();
        public eUsuario user = new eUsuario();
        public eGlobales eGlobal = new eGlobales();
        public int cod_ventana = 0;
        public string GrupoSeleccionado = "";
        public string ItemSeleccionado = "";
        public frmMantVentana()
        {
            InitializeComponent();
        }
        public frmMantVentana(frmOpcionesSistema frm)
        {
            InitializeComponent();
            frmHandler = frm;
        }

        private void Inicializar()
        {
            switch (MiAccion)
            {
                case Ventana.Nuevo:
                    CargarCombos();
                    Nuevo();
                    break;
                case Ventana.Editar:
                    CargarCombos();
                    Editar();
                    break;
            }
        }
        private void frmMantVentana_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        public void CargarCombos()
        {
            string solucion = eGlobal.dsc_solucion;

            blSis.CargaCombosLookUp("Modulos", lkpModulo, "codvar", "desvar1", dsc_solucion: solucion);
            lkpModulo.ItemIndex = -1;
            lkpModulo.EditValue = null;
        }


        public void Nuevo() {

            LimpiarCampos();

        }

        public void Editar() {

            eVentana eVen = new eVentana();
            eVen = blSis.ObtenerVentana<eVentana>(2, cod_ventana);
            txtNombreVentana.Text = eVen.dsc_ventana;
            chkActivo.CheckState = eVen.flg_activo == "SI" ? CheckState.Checked : CheckState.Unchecked;
            txtMenu.Text = eVen.dsc_menu;
            lkpModulo.EditValue = eVen.cod_grupo;
            txtFormulario.Text = eVen.dsc_formulario;
            txtNumOrden.Text = eVen.num_orden.ToString(); ;
            chkActivo.Enabled = true;

            picAnteriorVentana.Enabled = true;
            picSiguienteVentana.Enabled = true;
        }
        public void LimpiarCampos()
        {
            MiAccion = Ventana.Nuevo;
            cod_ventana =0;
            txtNombreVentana.Text = "";
            chkActivo.Checked = true;
            lkpModulo.ItemIndex = -1;
            lkpModulo.EditValue = null;
            txtMenu.Text = "";
            txtFormulario.Text = "";
            txtNumOrden.Text = "0";
            chkActivo.Enabled = false;

            picAnteriorVentana.Enabled = false;
            picSiguienteVentana.Enabled = false;

        }

        private void btnGuardar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string Mensaje = "";
                if (txtNombreVentana.Text == "") { MessageBox.Show("Debe ingresar un nombre para la ventana", "Guardar Ventana", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNombreVentana.Focus(); return; }
                if (lkpModulo.EditValue.ToString() == "") { MessageBox.Show("Debe seleccionar un módulo", "Guardar Ventana", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpModulo.Focus(); return; }
                if (txtFormulario.Text == "") { MessageBox.Show("Debe ingresar el formulario al que pertenece", "Guardar Ventana", MessageBoxButtons.OK, MessageBoxIcon.Error); txtFormulario.Focus(); return; }
                if (txtMenu.Text == "") { MessageBox.Show("Debe ingresar ela opción del menú al que pertenece", "Guardar Ventana", MessageBoxButtons.OK, MessageBoxIcon.Error); txtMenu.Focus(); return; }
               
                string result = "";
                switch (MiAccion)
                {
                    case Ventana.Nuevo: result = Guardar(); Mensaje = "Se creo la ventana de manera satisfactoria"; break;
                    case Ventana.Editar: result = Modificar(); Mensaje = "Se actualizo la ventana de manera satisfactoria"; break;
                }

                if (result == "OK")
                {
                    
                    MessageBox.Show(Mensaje, "Guardar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    int nRow = 0;
                    if (MiAccion == Ventana.Nuevo)
                    {
                        if (GrupoSeleccionado != "")
                        {
                            frmHandler.CargarListadoVentanas(GrupoSeleccionado, ItemSeleccionado);
                            for (int x = 0; x <= frmHandler.gvVentana.RowCount - 1; x++)
                            {
                                eVentana obj = frmHandler.gvVentana.GetRow(x) as eVentana;
                                if (obj != null && obj.cod_ventana == cod_ventana) { nRow = x; }
                            }
                            frmHandler.gvVentana.FocusedRowHandle = nRow;
                        }

                        MiAccion = Ventana.Editar;
                        chkActivo.Enabled = true;
                        picAnteriorVentana.Enabled = true;
                        picSiguienteVentana.Enabled = true;
                    }
                    else {
                        if (GrupoSeleccionado != "")
                        {
                            nRow = frmHandler.gvVentana.FocusedRowHandle;
                            frmHandler.CargarListadoVentanas(GrupoSeleccionado, ItemSeleccionado);
                            frmHandler.gvVentana.FocusedRowHandle = nRow;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private string Guardar()
        {
            string result = "";
            eVentana eVen = AsignarValoresVentana();
            eVen = blSis.Guardar_Actualizar_Ventana<eVentana>(1, eVen, "Nuevo", user.cod_usuario);
            if (eVen != null)
            {
                cod_ventana = eVen.cod_ventana;
                result = "OK";
            }

            return result;
        }

        private string Modificar()
        {
            string result = "";
            eVentana eVen = AsignarValoresVentana();
            eVen = blSis.Guardar_Actualizar_Ventana<eVentana>(1,eVen, "Actualizar", user.cod_usuario);

            if (eVen != null)
            {
                cod_ventana = eVen.cod_ventana;
                result = "OK";
            }

            return result;
        }

        private eVentana AsignarValoresVentana()
        {
            string solucion = eGlobal.dsc_solucion;

            eVentana eVen = new eVentana();
            eVen.cod_ventana = cod_ventana;
            eVen.dsc_ventana = txtNombreVentana.Text;
            eVen.dsc_menu = txtMenu.Text;
            eVen.cod_grupo = Convert.ToInt32(lkpModulo.EditValue);
            eVen.dsc_formulario = txtFormulario.Text;
            eVen.cod_usuario_registro = user.cod_usuario;
            eVen.flg_activo = chkActivo.CheckState == CheckState.Checked ? "SI" : "NO";
            eVen.num_orden = Convert.ToInt32(txtNumOrden.Text);
            eVen.dsc_solucion = solucion;

            return eVen;
        }

        private void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActivo.Checked == false)
            {
                this.layoutActivo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                this.layoutActivo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void picAnteriorVentana_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.gvVentana.RowCount - 1;
                int nRow = frmHandler.gvVentana.FocusedRowHandle;
                frmHandler.gvVentana.FocusedRowHandle = nRow == tRow ? 0 : nRow -1;

                eVentana obj = frmHandler.gvVentana.GetFocusedRow() as eVentana;
                cod_ventana = obj.cod_ventana;
                MiAccion = Ventana.Editar;
                Editar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picSiguienteVentana_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.gvVentana.RowCount - 1;
                int nRow = frmHandler.gvVentana.FocusedRowHandle;
                frmHandler.gvVentana.FocusedRowHandle = nRow == tRow ? 0 : nRow + 1;

                eVentana obj = frmHandler.gvVentana.GetFocusedRow() as eVentana;
                cod_ventana = obj.cod_ventana;
                MiAccion = Ventana.Editar;
                Editar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void picAnteriorVentana_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void frmMantVentana_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}