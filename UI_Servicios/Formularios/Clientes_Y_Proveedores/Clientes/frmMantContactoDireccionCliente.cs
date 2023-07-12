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
using UI_Servicios.Formularios.Shared;
using static UI_Servicios.Formularios.Shared.frmBusquedas;
using BL_Servicios;
using BE_Servicios;
//using UI_Servicios.Formularios.Gestion_de_Incidentes;

namespace UI_Servicios.Formularios.Clientes_Y_Proveedores.Clientes
{
    public partial class frmMantContactoDireccionCliente : DevExpress.XtraEditors.XtraForm
    {

        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string cod_condicion1 { get; set; }
        public string dsc_condicion1 { get; set; }
        public string cod_condicion2 { get; set; }
        public string dsc_condicion2 { get; set; }


        //blIncidente blInc = new blIncidente();
        public eUsuario user = new eUsuario();

       
        List<eCliente_Contactos> ListDireccionContacto = new List<eCliente_Contactos>();
        List<eCliente_Contactos> ListClienteContacto = new List<eCliente_Contactos>();
        internal Cliente MiAccion = Cliente.NuevoContactoDesdeIncidente;
        //opcion 1 NuevoContactoDesdeIncidente
        public int opcion = 1;

        blClientes blCli = new blClientes();

        public frmMantContactoDireccionCliente()
        {
            InitializeComponent();
        }

        internal enum Cliente
        {
            NuevoContactoDesdeIncidente = 0,
          
        }


        private void frmMantContactoDireccionCliente_Load(object sender, EventArgs e)
        {
            
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            frmBusquedas frm = new frmBusquedas();
            if (txtCliente.Tag == null)
            {
                frm.codigo = "";
                frm.descripcion = txtCliente.Text;
            }
            else
            {
                frm.codigo = txtCliente.Tag.ToString();
                frm.descripcion = txtCliente.Text;
            }
            frm.entidad = MiEntidad.Cliente;
            frm.ShowDialog();
            txtCliente.Text = frm.descripcion;
            txtCliente.Tag = frm.codigo;


            //blInc.CargaCombosLookUp("DireccionxCliente", lkpDireccion, "codvar", "desvar1", cod_cliente: txtCliente.Tag.ToString());
            lkpDireccion.ItemIndex = 0;
            
        }

        private void btnGuardar1_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (txtCliente.Tag == null) { MessageBox.Show("Debe ingresar un cliente", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCliente.Focus(); return; }
                if (txtCliente.Text == "") { MessageBox.Show("Debe ingresar un cliente", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCliente.Focus(); return; }
                if (lkpDireccion.EditValue == null) { MessageBox.Show("Debe ingresar una dirección", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpDireccion.Focus(); return; }
                if (txtNombreDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un nombre", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNombreDireccionContacto.Focus(); return; }
                if (txtApellidoDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un apellido", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtApellidoDireccionContacto.Focus(); return; }
                //if (dtFecNacDireccionContacto.EditValue == null) { MessageBox.Show("Debe ingresar una fecha de nacimiento", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); dtFecNacDireccionContacto.Focus(); return; }
                if (txtCorreoDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un correo", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCorreoDireccionContacto.Focus(); return; }
                if (txtFono1DireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un teléfono", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtFono1DireccionContacto.Focus(); return; }
                if (txtCargoDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un cargo", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCargoDireccionContacto.Focus(); return; }
                if (txtUsuarioWebDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un usuario web", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtUsuarioWebDireccionContacto.Focus(); return; }
                if (txtClaveWebDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar una clave web", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtClaveWebDireccionContacto.Focus(); return; }

                eCliente_Contactos eContact = new eCliente_Contactos();
                eContact = AsignarValoresDireccionContacto();

                //NuevoContactoDesdeIncidente
                if (MiAccion == Cliente.NuevoContactoDesdeIncidente)
                {
                    eContact = blCli.Guardar_Actualizar_ClienteDireccionContacto<eCliente_Contactos>(eContact,  "Nuevo");
                    codigo = eContact.cod_contacto.ToString();
                    cod_condicion1 = txtCliente.Tag.ToString();
                    dsc_condicion1 = txtCliente.Text;
                    cod_condicion2 = lkpDireccion.EditValue.ToString();
                    this.Close();
                }
               

              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private eCliente_Contactos AsignarValoresDireccionContacto()
        {
            eCliente_Contactos eContact = new eCliente_Contactos();
            eContact.cod_cliente = txtCliente.Tag.ToString();
            eContact.num_linea =Convert.ToInt32(lkpDireccion.EditValue);
            eContact.cod_contacto = Convert.ToInt32(txtCodDireccionContacto.Text);
            eContact.dsc_nombre = txtNombreDireccionContacto.Text;
            eContact.dsc_apellidos = txtApellidoDireccionContacto.Text;
            eContact.fch_nacimiento = dtFecNacDireccionContacto.EditValue == null ? new DateTime() : Convert.ToDateTime(dtFecNacDireccionContacto.EditValue);
            eContact.dsc_correo = txtCorreoDireccionContacto.Text;
            eContact.dsc_telefono1 = txtFono1DireccionContacto.Text;
            eContact.dsc_telefono2 = "";
            eContact.dsc_cargo = txtCargoDireccionContacto.Text;
            eContact.dsc_observaciones = mmObservacionDireccionContacto.Text;
            eContact.cod_usuario_web = txtUsuarioWebDireccionContacto.Text;
            eContact.cod_clave_web = txtClaveWebDireccionContacto.Text;
            eContact.cod_usuario_reg = user.cod_usuario;

            
           

            return eContact;
        }

        private void btnNuevo1_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                LimpiarCamposDireccionContacto();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCamposDireccionContacto()
        {
            txtCliente.Text = "";
            txtCliente.Tag = null;
            lkpDireccion.Properties.DataSource = null;
            txtCodDireccionContacto.Text = "0";
            txtNombreDireccionContacto.Text = "";
            txtApellidoDireccionContacto.Text = "";
            dtFecNacDireccionContacto.EditValue = null;
            txtCorreoDireccionContacto.Text = "";
            txtFono1DireccionContacto.Text = "";
           // txtFono2DireccionContacto.Text = "";
            txtCargoDireccionContacto.Text = "";
            mmObservacionDireccionContacto.Text = "";
            txtUsuarioWebDireccionContacto.Text = "";
            txtClaveWebDireccionContacto.Text = "";
            txtNombreDireccionContacto.Focus();
        }

        private void frmMantContactoDireccionCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape) { this.Close(); }
        }
    }
}