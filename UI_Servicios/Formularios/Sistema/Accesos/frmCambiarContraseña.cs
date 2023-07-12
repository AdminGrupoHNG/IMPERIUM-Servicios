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

namespace UI_Servicios.Formularios.Sistema.Accesos
{
    public partial class frmCambiarContraseña : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        blSistema blSis = new blSistema();
        blUsuario blUsu = new blUsuario();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public frmCambiarContraseña()
        {
            InitializeComponent();
        }

        private void frmCambiarContraseña_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (user.flg_cambiar_clave != null) { if (user.flg_cambiar_clave == "SI") { Application.Exit(); } }
        }

        private void frmCambiarContraseña_Load(object sender, EventArgs e)
        {
           
        }

        private void btnGuardar_ItemClick(object sender, ItemClickEventArgs e)
        {
            eUsuario obj = new eUsuario();
            //string ClaveAntigua = DesEncriptar(txtContraseñaActual.Text);
            //}
            obj = blUsu.ObtenerUsuario<eUsuario>(2, user.cod_usuario);

            if (txtContraseñaActual.Text == "")
            {
                MessageBox.Show("Insertar la contraseña actual", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); ; txtContraseñaActual.Focus(); return;
            }
            if (txtContraseñaActual.Text.ToUpper() != obj.dsc_clave.ToUpper())
            {
                MessageBox.Show("La contraseña actual no es correcta", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtContraseñaActual.Text = ""; txtContraseñaActual.Focus(); return;
            }
            if (txtNuevaContraseña.Text == "")
            {
                MessageBox.Show("Insertar una nueva contraseña", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNuevaContraseña.Focus(); return;
            }
            if (txtReconfirmarContraseña.Text == "")
            {
                MessageBox.Show("Falta confirmar su contraseña", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtReconfirmarContraseña.Focus(); return;
            }
            if (txtReconfirmarContraseña.Text != txtNuevaContraseña.Text)
            {
                MessageBox.Show("La nueva contraseña y la reconfirmación no coinciden", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtReconfirmarContraseña.Text = ""; txtReconfirmarContraseña.Focus(); return;
            }

            // string ClaveNueva = Encriptar(txtConfirmarClave.Text);
            obj.dsc_clave = txtReconfirmarContraseña.Text;
            user = blUsu.Guardar_Actualizar_Usuario<eUsuario>(obj, "Actualizar", user.cod_usuario);
            MessageBox.Show("Contraseña Actualizada", "Modificación de contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void frmCambiarContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}