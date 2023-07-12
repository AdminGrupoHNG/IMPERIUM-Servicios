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
    public partial class frmMantPerfil : DevExpress.XtraEditors.XtraForm
    {

        internal enum Perfil
        {
            Nuevo = 0,
            Editar = 1
        }

        frmAsignacionPermiso frmHandler = new frmAsignacionPermiso();
        internal Perfil MiAccion = Perfil.Nuevo;
        blSistema blSis = new blSistema();
        public eUsuario user = new eUsuario();
        public eGlobales eGlobal = new eGlobales();
        public int cod_perfil = 0;
        public string GrupoSeleccionado = "";
        public string ItemSeleccionado = "";


        public frmMantPerfil()
        {
            InitializeComponent();
        }

        public frmMantPerfil(frmAsignacionPermiso frm)
        {
            InitializeComponent();
            frmHandler = frm;
        }

        private void Inicializar()
        {
            switch (MiAccion)
            {
                case Perfil.Nuevo:
                   
                    Nuevo();
                    break;
                case Perfil.Editar:
                    
                    Editar();
                    break;
            }
        }
        private void frmMantPerfil_Load(object sender, EventArgs e)
        {
            Inicializar();
        }
        public void Nuevo()
        {

            LimpiarCampos();

        }


        public void LimpiarCampos()
        {
            MiAccion = Perfil.Nuevo;
            cod_perfil = 0;
            txtNombrePerfil.Text = "";
            chkActivo.Checked = true;
            chkActivo.Enabled = false;

            picPerfilAnterior.Enabled = false;
            picPerfilSiguiente.Enabled = false;

        }
        public void Editar()
        {

            ePerfil ePer = new ePerfil();
            ePer = blSis.ObtenerPerfil<ePerfil>(4, cod_perfil);
            txtNombrePerfil.Text = ePer.dsc_perfil;
            chkActivo.CheckState = ePer.flg_activo == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkActivo.Enabled = true;


            picPerfilAnterior.Enabled = true;
            picPerfilSiguiente.Enabled = true;
        }


        private void frmMantPerfil_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void picPerfilAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.gvPerfiles.RowCount - 1;
                int nRow = frmHandler.gvPerfiles.FocusedRowHandle;
                frmHandler.gvPerfiles.FocusedRowHandle = nRow == tRow ? 0 : nRow - 1;

                ePerfil obj = frmHandler.gvPerfiles.GetFocusedRow() as ePerfil;
                cod_perfil = obj.cod_perfil;
                MiAccion = Perfil.Editar;
                Editar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picPerfilSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.gvPerfiles.RowCount - 1;
                int nRow = frmHandler.gvPerfiles.FocusedRowHandle;
                frmHandler.gvPerfiles.FocusedRowHandle = nRow == tRow ? 0 : nRow + 1;

                ePerfil obj = frmHandler.gvPerfiles.GetFocusedRow() as ePerfil;
                cod_perfil = obj.cod_perfil;
                MiAccion = Perfil.Editar;
                Editar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void btnGuardar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string Mensaje = "";
                if (txtNombrePerfil.Text == "") { MessageBox.Show("Debe ingresar un nombre para el perfil", "Guardar Perfil", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNombrePerfil.Focus(); return; }
               


                string result = "";
                switch (MiAccion)
                {
                    case Perfil.Nuevo: result = Guardar(); Mensaje = "Se creo el perfil de manera satisfactoria"; break;
                    case Perfil.Editar: result = Modificar(); Mensaje = "Se actualizo el perfil de manera satisfactoria"; break;
                }

                if (result == "OK")
                {

                    MessageBox.Show(Mensaje, "Guardar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    int nRow = 0;
                    if (MiAccion == Perfil.Nuevo)
                    {
                        if (GrupoSeleccionado != "")
                        {
                            frmHandler.CargarPerfiles(GrupoSeleccionado, ItemSeleccionado);
                            for (int x = 0; x <= frmHandler.gvPerfiles.RowCount - 1; x++)
                            {
                                ePerfil obj = frmHandler.gvPerfiles.GetRow(x) as ePerfil;
                                if (obj != null && obj.cod_perfil == cod_perfil) { nRow = x; }
                            }
                            frmHandler.gvPerfiles.FocusedRowHandle = nRow;
                        }

                        MiAccion = Perfil.Editar;
                        chkActivo.Enabled = true;
                        picPerfilAnterior.Enabled = true;
                        picPerfilSiguiente.Enabled = true;
                    }
                    else
                    {
                        if (GrupoSeleccionado != "")
                        {
                            nRow = frmHandler.gvPerfiles.FocusedRowHandle;
                            frmHandler.CargarPerfiles(GrupoSeleccionado, ItemSeleccionado);
                            frmHandler.gvPerfiles.FocusedRowHandle = nRow;
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
            ePerfil ePer = AsignarValoresPerfil();
            ePer = blSis.Guardar_Actualizar_Perfil<ePerfil>(1, ePer, "Nuevo", user.cod_usuario);
            if (ePer != null)
            {
                cod_perfil = ePer.cod_perfil;
                result = "OK";
            }

            return result;
        }

        private string Modificar()
        {
            string result = "";
            ePerfil ePer = AsignarValoresPerfil();
            ePer = blSis.Guardar_Actualizar_Perfil<ePerfil>(1, ePer, "Actualizar", user.cod_usuario);

            if (ePer != null)
            {
                cod_perfil = ePer.cod_perfil;
                result = "OK";
            }

            return result;
        }


        private ePerfil AsignarValoresPerfil()
        {
            string solucion = eGlobal.dsc_solucion;

            ePerfil Eper = new ePerfil();
            Eper.cod_perfil = cod_perfil;
            Eper.dsc_perfil = txtNombrePerfil.Text;
            Eper.cod_usuario_registro = user.cod_usuario;
            Eper.flg_activo = chkActivo.CheckState == CheckState.Checked ? "SI" : "NO";
            Eper.dsc_solucion = solucion;

            return Eper;
        }


    }
}