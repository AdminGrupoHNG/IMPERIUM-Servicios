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

namespace UI_Servicios.Formularios.Logistica
{
    internal enum Almacen
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmMantAlmacen : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        internal Almacen MiAccion = Almacen.Nuevo;
        blTrabajador blTrab = new blTrabajador();
        blClientes blCli = new blClientes();
        blLogistica blLogis = new blLogistica();
        blProveedores blProv = new blProveedores();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public string cod_empresa = "", cod_almacen = "";
        public bool ActualizarListado = false;

        public frmMantAlmacen()
        {
            InitializeComponent();
        }

        private void frmMantAlmacen_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            CargarLookUpEdit();

            switch (MiAccion)
            {
                case Almacen.Nuevo:
                    txtCodAlmacen.Text = "";
                    lkpPais.EditValue = "00001"; lkpDepartamento.EditValue = "00015"; lkpProvincia.EditValue = "00128";
                    dtFechaCreacion.EditValue = DateTime.Today;
                    txtDescripcion.Select();
                    break;
                case Almacen.Editar:
                    Obtener_DatosAlmacen();
                    break;
            }
        }

        private void Obtener_DatosAlmacen()
        {
            eAlmacen obj = new eAlmacen();
            obj = blLogis.Obtener_DatosLogistica<eAlmacen>(14, cod_almacen);
            txtCodAlmacen.Text = obj.cod_almacen;
            chkflgActivo.CheckState = obj.flg_activo == "SI" ? CheckState.Checked : CheckState.Unchecked;
            lkpSedeEmpresa.EditValue = obj.cod_sede_empresa;
            txtDescripcion.Text = obj.dsc_descripcion;
            lkpTipoAlmacen.EditValue = obj.cod_tipo_almacen;
            dtFechaCreacion.EditValue = obj.fch_creacion;
            lkpPais.EditValue = obj.cod_pais;
            lkpDepartamento.EditValue = obj.cod_departamento;
            lkpProvincia.EditValue = obj.cod_provincia;
            glkpDistrito.EditValue = obj.cod_distrito;
            txtDireccion.Text = obj.dsc_direccion;
        }

        private void CargarLookUpEdit()
        {
            try
            {
                blLogis.CargaCombosLookUp("TipoAlmacen", lkpTipoAlmacen, "cod_tipo_almacen", "dsc_tipo_almacen", "", valorDefecto: true);
                //blTrab.CargaCombosLookUp("Empresa", lkpSedeEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true);
                blTrab.CargaCombosLookUp("SedesEmpresa", lkpSedeEmpresa, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa);
                blCli.CargaCombosLookUp("TipoPais", lkpPais, "cod_pais", "dsc_pais", "");
                blCli.CargaCombosLookUp("TipoDepartamento", lkpDepartamento, "cod_departamento", "dsc_departamento", "");
                blCli.CargaCombosLookUp("TipoProvincia", lkpProvincia, "cod_provincia", "dsc_provincia", "");
                CargarCombosGridLookup("TipoDistrito", glkpDistrito, "cod_distrito", "dsc_distrito", "");
                //List<eProveedor_Empresas> listEmpresasUsuario = blProv.ListarEmpresasProveedor<eProveedor_Empresas>(11, "", user.cod_usuario);
                //lkpSedeEmpresa.EditValue = cod_empresa;
                List<eTrabajador.eInfoLaboral_Trabajador> lista = blTrab.ListarOpcionesTrabajador<eTrabajador.eInfoLaboral_Trabajador>(6, cod_empresa);
                if (lista.Count == 1) lkpSedeEmpresa.EditValue = lista[0].cod_sede_empresa;
                lkpDepartamento.EditValue = "00015"; lkpProvincia.EditValue = "00128";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargarCombosGridLookup(string nCombo, GridLookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", string cod_condicion = "", bool valorDefecto = false)
        {
            DataTable tabla = new DataTable();
            tabla = blTrab.ObtenerListadoGridLookup(nCombo, cod_condicion);

            combo.Properties.DataSource = tabla;
            combo.Properties.ValueMember = campoValueMember;
            combo.Properties.DisplayMember = campoDispleyMember;
            if (campoSelectedValue == "") { combo.EditValue = null; } else { combo.EditValue = campoSelectedValue; }
            if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                txtDescripcion.Select();
                if (txtDescripcion.Text.Trim() == "") { MessageBox.Show("Debe ingresar la descripción.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDescripcion.Focus(); return; }
                if (lkpTipoAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el tipo de almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoAlmacen.Focus(); return; }
                if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede empresa.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
                if (lkpPais.EditValue == null) { MessageBox.Show("Debe seleccionar un país.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpPais.Focus(); return; }
                if (lkpDepartamento.EditValue == null) { MessageBox.Show("Debe seleccionar un departamento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpDepartamento.Focus(); return; }
                if (lkpProvincia.EditValue == null) { MessageBox.Show("Debe seleccionar una provincia.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpProvincia.Focus(); return; }
                if (glkpDistrito.EditValue == null) { MessageBox.Show("Debe seleccionar un distrito.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); glkpDistrito.Focus(); return; }
                if (txtDireccion.Text.Trim() == "") { MessageBox.Show("Debe ingresar una dirección.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDireccion.Focus(); return; }

                eAlmacen eObj = AsignarValores_Almacen();
                eObj = blLogis.Insertar_Actualizar_Almacen<eAlmacen>(eObj);
                if (eObj == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                txtCodAlmacen.Text = eObj.cod_almacen; cod_almacen = eObj.cod_almacen;

                if (eObj != null)
                {
                    MiAccion = Almacen.Editar;
                    ActualizarListado = true;
                    MessageBox.Show("Se registraron los datos de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Inicializar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private eAlmacen AsignarValores_Almacen()
        {
            eAlmacen obj = new eAlmacen();
            obj.cod_almacen = txtCodAlmacen.Text;
            obj.cod_empresa = cod_empresa;
            obj.cod_sede_empresa = lkpSedeEmpresa.EditValue.ToString();
            obj.fch_creacion = Convert.ToDateTime(dtFechaCreacion.EditValue);
            obj.dsc_descripcion = txtDescripcion.Text;
            obj.cod_tipo_almacen = lkpTipoAlmacen.EditValue.ToString();
            obj.cod_distrito = glkpDistrito.EditValue.ToString();
            obj.cod_provincia = lkpProvincia.EditValue.ToString();
            obj.cod_departamento = lkpDepartamento.EditValue.ToString();
            obj.cod_pais = lkpPais.EditValue.ToString();
            obj.dsc_direccion = txtDireccion.Text;
            obj.flg_activo = chkflgActivo.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.cod_usuario_registro = user.cod_usuario;

            return obj;
        }

        private void lkpPais_EditValueChanged(object sender, EventArgs e)
        {
            glkpDistrito.Properties.DataSource = null;
            lkpProvincia.Properties.DataSource = null;
            lkpDepartamento.Properties.DataSource = null;
            blCli.CargaCombosLookUp("TipoDepartamento", lkpDepartamento, "cod_departamento", "dsc_departamento", "", cod_condicion: lkpPais.EditValue == null ? "" : lkpPais.EditValue.ToString());
        }

        private void lkpDepartamento_EditValueChanged(object sender, EventArgs e)
        {
            glkpDistrito.Properties.DataSource = null;
            lkpProvincia.Properties.DataSource = null;
            blCli.CargaCombosLookUp("TipoProvincia", lkpProvincia, "cod_provincia", "dsc_provincia", "", cod_condicion: lkpDepartamento.EditValue == null ? "" : lkpDepartamento.EditValue.ToString());
        }

        private void lkpProvincia_EditValueChanged(object sender, EventArgs e)
        {
            glkpDistrito.Properties.DataSource = null;
            //blCli.CargaCombosLookUp("TipoDistrito", glkpDistrito, "cod_distrito", "dsc_distrito", "", cod_condicion: lkpProvincia.EditValue.ToString());
            CargarCombosGridLookup("TipoDistrito", glkpDistrito, "cod_distrito", "dsc_distrito", "", cod_condicion: lkpProvincia.EditValue == null ? "" : lkpProvincia.EditValue.ToString());
        }

    }
}