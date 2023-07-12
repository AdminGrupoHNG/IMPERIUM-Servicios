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

namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    public partial class frmCrearProyecto : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        blFactura blFact = new blFactura();
        public string cod_empresa, dsc_empresa;

        public frmCrearProyecto()
        {
            InitializeComponent();
        }

        private void frmCrearProyecto_Load(object sender, EventArgs e)
        {
            lblNombreEmpresa.Text = dsc_empresa;
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtNombreProyecto.Text.Trim() == "") { MessageBox.Show("Debe ingresar el nombre del proyecto", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            eProyecto obj = new eProyecto();
            obj.cod_proyecto = txtCodProyecto.Text;
            obj.cod_empresa = cod_empresa;
            obj.dsc_proyecto = txtNombreProyecto.Text;
            obj.cod_usuario_registro = user.cod_usuario;
            obj.flg_activo = chkFlgActivo.CheckState == CheckState.Checked ? "SI" : "NO";
            obj = blFact.Insertar_Actualizar_Proyecto<eProyecto>(obj);
            if (obj == null) { MessageBox.Show("Error al guardar datos", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (obj != null) MessageBox.Show("Se guardaron los datos de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtCodProyecto.Text = obj.cod_proyecto;
        }
    }
}