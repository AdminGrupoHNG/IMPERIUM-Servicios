using BE_Servicios;
using BL_Servicios;
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

namespace UI_Servicios.Formularios.Cotizaciones
{
    internal enum Cargo
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }

    public partial class frmMantCargos : DevExpress.XtraEditors.XtraForm
    {
        internal Cargo accion = Cargo.Nuevo;

        public eUsuario user = new eUsuario();
        blAnalisisServicio blAns = new blAnalisisServicio();
        blProveedores blProv = new blProveedores();
        blGlobales blGlobal = new blGlobales();

        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public String empresa, sedeEmpresa, area, cargo;
        public int sedeCliente;

        public eDatos eCar;
        public decimal salMin, salMax;

        public frmMantCargos()
        {
            InitializeComponent();
        }

        private void frmMantCargos_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            CargarLookUpEdit();
            ConfigurarForm();
        }

        private void CargarLookUpEdit()
        {
            blAns.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: user.cod_usuario);
            lkpEmpresa.EditValue = empresa;

            blAns.CargaCombosLookUp("Sedes", lkpSedeEmpresa, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());
            lkpSedeEmpresa.EditValue = "00001";

            blAns.CargaCombosLookUp("Area", lkpArea, "cod_area", "dsc_area", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString(), cod_sede_empresa: lkpSedeEmpresa.EditValue.ToString());
            lkpArea.EditValue = "00002";
        }

        private void ConfigurarForm()
        {
            switch (accion)
            {
                case Cargo.Nuevo:
                    break;
                case Cargo.Editar:
                    CargarCargo();
                    break;
                case Cargo.Vista:
                    break;
            }
        }

        private void CargarCargo()
        {
            List<eDatos> lstDat = blAns.ListarGeneral<eDatos>("Cargo", empresa, sedeEmpresa, area: area, cargo: cargo);

            lkpEmpresa.EditValue = lstDat[0].AtributoUno;
            lkpSedeEmpresa.EditValue = lstDat[0].AtributoDos;
            lkpArea.EditValue = lstDat[0].AtributoTres;
            area = lstDat[0].AtributoCuatro;
            txtCargo.EditValue = lstDat[0].AtributoCinco;
            txtSalMin.EditValue = lstDat[0].AtributoOnce;
            txtSalMax.EditValue = lstDat[0].AtributoDoce;
        }

        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            blAns.CargaCombosLookUp("Sedes", lkpSedeEmpresa, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());
            lkpSedeEmpresa.EditValue = "00001";
        }

        private void lkpSedeEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            blAns.CargaCombosLookUp("Area", lkpArea, "cod_area", "dsc_area", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString(), cod_sede_empresa: lkpSedeEmpresa.EditValue.ToString());
            lkpArea.EditValue = "00002";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtCargo.EditValue == null)
            {
                MessageBox.Show("Debe ingresar la descripción del cargo", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            eCar = CargarCabecera();

            eCar = blAns.Ins_Act_Cargo<eDatos>(eCar);

            MessageBox.Show("Registro generado de manera éxitosa.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);

            accion = Cargo.Editar;
            empresa = eCar.AtributoUno; sedeEmpresa = eCar.AtributoDos; area = eCar.AtributoTres; cargo = eCar.AtributoCuatro;

            this.Close();
        }

        private eDatos CargarCabecera()
        {
            eDatos eCar = new eDatos();

            eCar.AtributoUno = lkpEmpresa.EditValue.ToString();
            eCar.AtributoDos = lkpSedeEmpresa.EditValue.ToString();
            eCar.AtributoTres = lkpArea.EditValue.ToString();
            eCar.AtributoCuatro = accion == Cargo.Nuevo ? "" : cargo;
            eCar.AtributoCinco = txtCargo.EditValue.ToString();
            salMin = decimal.Parse(txtSalMin.EditValue.ToString());
            eCar.AtributoOnce = salMin;
            salMax = decimal.Parse(txtSalMax.EditValue.ToString());
            eCar.AtributoDoce = salMax;

            return eCar;
        }
    }
}