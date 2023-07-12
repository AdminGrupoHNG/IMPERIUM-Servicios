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
using UI_Servicios.Clientes_Y_Proveedores.Clientes;
using UI_Servicios.Formularios.Shared;

namespace UI_Servicios.Formularios.Cotizaciones
{
    internal enum RequerimientoAns
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }

    public partial class frmMantRequerimientoAnalisis : DevExpress.XtraEditors.XtraForm
    {
        internal RequerimientoAns accion = RequerimientoAns.Nuevo;

        public eUsuario user = new eUsuario();
        blAnalisisServicio blAns = new blAnalisisServicio();
        blProveedores blProv = new blProveedores();
        blGlobales blGlobal = new blGlobales();

        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public String empresa, sedeEmpresa, analisis, codigoCliente;

        Boolean gReq = false;

        String mensaje;
        List<eAnalisis.eAnalisis_Sedes.eAnalisis_Sedes_Prestacion> lstAns;

        public frmMantRequerimientoAnalisis()
        {
            InitializeComponent();
        }

        private void frmMantRequerimientoAnalisis_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            CargarLookUpEdit();
            ConfigurarDateEdit();
            CargarTiposServicio();
            ConfigurarForm();
        }

        private void CargarLookUpEdit()
        {
            blAns.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: user.cod_usuario);
            List<eFacturaProveedor> list = blProv.ListarEmpresasProveedor<eFacturaProveedor>(11, "", user.cod_usuario);
            lkpEmpresa.EditValue = list[0].cod_empresa;

            blAns.CargaCombosLookUp("Sedes", lkpSedeEmpresa, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());
            lkpSedeEmpresa.EditValue = "00001";
        }

        private void ConfigurarDateEdit()
        {
            DateTime date = DateTime.Now;
            dtpFechaRequerimiento.EditValue = date;
            dtpFechaVisita.EditValue = date;
            dtpHoraInicio.EditValue = DateTime.Parse("01/01/1900 00:00:00.000");
            dtpHoraFin.EditValue = DateTime.Parse("01/01/1900 00:00:00.000");
        }

        private void CargarTiposServicio()
        {
            bsTipoServicio.DataSource = blAns.ListarGeneral<eDatos>("TipoPrestacion", empresa: lkpEmpresa.EditValue.ToString());
        }

        private void ConfigurarForm()
        {
            switch (accion)
            {
                case RequerimientoAns.Nuevo:
                    controlMostrarSedes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
                case RequerimientoAns.Editar:
                    btnGuardar.Caption = "Guardar";
                    controlMostrarSedes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    dtpFechaRequerimiento.Enabled = false;
                    lkpEmpresa.Enabled = false;
                    lkpSedeEmpresa.Enabled = false;
                    txtCliente.Enabled = false;
                    picBuscarCliente.Enabled = false;
                    picVerCliente.Enabled = true;

                    CargarAnalisis();
                    break;
                case RequerimientoAns.Vista:
                    break;
            }
        }

        private void CargarAnalisis()
        {
            lstAns = blAns.ListarAnalisis<eAnalisis.eAnalisis_Sedes_Prestacion>(2, empresa, sedeEmpresa, analisis);

            lkpEmpresa.EditValue = lstAns[0].cod_empresa;
            lkpSedeEmpresa.EditValue = lstAns[0].cod_sede_empresa;
            txtAnalisis.EditValue = lstAns[0].cod_analisis;
            codigoCliente = lstAns[0].cod_cliente;
            txtCliente.EditValue = lstAns[0].dsc_cliente;
            dtpFechaRequerimiento.EditValue = lstAns[0].fch_requerimiento;
            dtpFechaVisita.EditValue = lstAns[0].fch_visita;
            dtpHoraInicio.EditValue = lstAns[0].dsc_hora_inicio_visita;
            dtpHoraFin.EditValue = lstAns[0].dsc_hora_fin_visita;
            meObservaciones.EditValue = lstAns[0].dsc_observaciones;

            //CargarSedes(lstAns);
            CargarSedes(null);
        }

        private void CargarSedes(List<eAnalisis.eAnalisis_Sedes_Prestacion> lstAns = null)
        {
            List<eCliente_Direccion> lstSedesCli = blAns.ListarGeneral<eCliente_Direccion>("SedesCliente", cliente: codigoCliente);

            if (lstAns == null)
            {
                bsSedesCliente.DataSource = lstSedesCli;
            }
            else
            {
                List<eCliente_Direccion> lstSedAns = new List<eCliente_Direccion>();

                foreach (eCliente_Direccion obj in lstSedesCli)
                {
                    foreach (eAnalisis.eAnalisis_Sedes_Prestacion obj2 in lstAns)
                    {
                        if (obj.num_linea == obj2.cod_sede_cliente)
                        {
                            lstSedAns.Add(obj);
                            break;
                        }
                    }
                }

                bsSedesCliente.DataSource = lstSedAns;

                eCliente_Direccion eDir = tvSedesCliente.GetFocusedRow() as eCliente_Direccion;
                CargarTiposxSede(eDir == null ? lstSedAns[0].num_linea : eDir.num_linea);
            }
        }

        private void CargarTiposxSede(int sede = 0)
        {
            if (lstAns == null) return;

            List<eAnalisis.eAnalisis_Sedes_Prestacion> lstTipos = lstAns.FindAll(x => x.cod_sede_cliente == sede);

            foreach (eDatos obj in bsTipoServicio)
            {
                obj.AtributoDiez = 0;
                obj.cod_sel = false;
            }

            foreach (eAnalisis.eAnalisis_Sedes_Prestacion obj in lstTipos)
            {
                foreach (eDatos obj2 in bsTipoServicio)
                {
                    if (obj2.AtributoUno == obj.cod_tipo_prestacion)
                    {
                        obj2.AtributoDiez = obj.num_servicio;
                        obj2.cod_sel = true;
                    }
                }
            }
        }

        private bool ValidarCampos()
        {
            Boolean respuesta = true;

            if (codigoCliente == null)
            {
                mensaje = "Debe seleccionar los datos del cliente.";
                respuesta = false;
                txtCliente.Focus();

                return respuesta;
            }

            gvTipoServicio.PostEditor();
            int cuenta = 0;
            for (int x = 0; x < gvTipoServicio.DataRowCount; x++)
            {
                eDatos obj = gvTipoServicio.GetRow(x) as eDatos;

                if (obj.cod_sel == false)
                {
                    cuenta++;
                }
            }

            if (cuenta == gvTipoServicio.DataRowCount)
            {
                mensaje = "Debe seleccionar un servicio al registro.";
                respuesta = false;
                gvTipoServicio.Focus();

                return respuesta;
            }

            return respuesta;
        }

        private eAnalisis GuardarCabecera()
        {
            eAnalisis eAns = new eAnalisis();

            eAns.cod_empresa = lkpEmpresa.EditValue.ToString();
            eAns.cod_sede_empresa = lkpSedeEmpresa.EditValue.ToString();
            eAns.cod_analisis = accion == RequerimientoAns.Nuevo ? "" : analisis;
            eAns.cod_cliente = codigoCliente;
            eAns.cod_estado_analisis = "REQ";
            eAns.fch_requerimiento = Convert.ToDateTime(dtpFechaRequerimiento.EditValue);

            eAns = blAns.Ins_Act_Analisis<eAnalisis>(eAns, user.cod_usuario);

            return eAns;
        }

        private eAnalisis.eAnalisis_Sedes GuardarDetalleSedes()
        {
            eAnalisis.eAnalisis_Sedes eAnsSed = new eAnalisis.eAnalisis_Sedes();

            eAnsSed.cod_empresa = empresa;
            eAnsSed.cod_sede_empresa = sedeEmpresa;
            eAnsSed.cod_analisis = analisis;

            eCliente_Direccion eDir = tvSedesCliente.GetFocusedRow() as eCliente_Direccion;
            eAnsSed.cod_sede_cliente = eDir.num_linea;

            eAnsSed.num_m2 = 0;
            eAnsSed.num_m3 = 0;
            eAnsSed.fch_visita = Convert.ToDateTime(dtpFechaVisita.EditValue);
            eAnsSed.dsc_hora_inicio_visita = Convert.ToDateTime(dtpHoraInicio.EditValue);
            eAnsSed.dsc_hora_fin_visita = Convert.ToDateTime(dtpHoraFin.EditValue);
            eAnsSed.dsc_observaciones = meObservaciones.EditValue == null ? "" : meObservaciones.EditValue.ToString();

            eAnsSed = blAns.Ins_Act_Analisis_Sedes<eAnalisis.eAnalisis_Sedes>(eAnsSed, user.cod_usuario);

            return eAnsSed;
        }

        private void GuardarPrestacionSede(eAnalisis.eAnalisis_Sedes eAnsSed)
        {
            eAnalisis.eAnalisis_Sedes_Prestacion eAnsSP = new eAnalisis.eAnalisis_Sedes_Prestacion();

            gvTipoServicio.PostEditor();
            for (int x = 0; x < gvTipoServicio.DataRowCount; x++)
            {
                eDatos obj = gvTipoServicio.GetRow(x) as eDatos;

                if (obj.cod_sel)
                {
                    eAnsSP.cod_empresa = empresa;
                    eAnsSP.cod_sede_empresa = sedeEmpresa;
                    eAnsSP.cod_analisis = analisis;
                    eAnsSP.cod_sede_cliente = eAnsSed.cod_sede_cliente;
                    eAnsSP.num_servicio = obj.AtributoDiez;
                    eAnsSP.cod_tipo_prestacion = obj.AtributoUno;
                    eAnsSP.dsc_periodo = "";
                    eAnsSP.num_version = 1;
                    eAnsSP.flg_habilitado = "SI";

                    eAnsSP = blAns.Ins_Act_Analisis_Sedes_Prestacion<eAnalisis.eAnalisis_Sedes_Prestacion>(eAnsSP, user.cod_usuario);
                }
                else
                {
                    string respuesta = blAns.Eliminar_Reg_Analisis("Prestacion", empresa, sedeEmpresa, analisis, sedeCliente: eAnsSed.cod_sede_cliente, tipo_servicio: obj.AtributoUno);
                }
            }
        }

        public void Busqueda(string dato, string tipo, string filtroRUC = "NO")
        {
            frmBusquedas frm = new frmBusquedas();
            frm.user = user;
            frm.filtro = dato;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            switch (tipo)
            {
                case "Cliente":
                    frm.entidad = frmBusquedas.MiEntidad.ClienteEmpresa;
                    frm.cod_condicion1 = lkpEmpresa.EditValue.ToString();
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Cliente":
                    codigoCliente = frm.codigo;
                    txtCliente.EditValue = frm.descripcion;

                    CargarSedes();

                    picVerCliente.Enabled = true;
                    break;
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (ValidarCampos() == false)
                {
                    MessageBox.Show(mensaje, "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                eAnalisis eAns = GuardarCabecera();
                empresa = eAns.cod_empresa; sedeEmpresa = eAns.cod_sede_empresa; analisis = eAns.cod_analisis;
                eAnalisis.eAnalisis_Sedes eAnsSed = GuardarDetalleSedes();
                GuardarPrestacionSede(eAnsSed);

                MessageBox.Show("Registro generado de manera éxitosa.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                accion = RequerimientoAns.Editar;
                gReq = false;
                ConfigurarForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Busqueda("", "Cliente");
            }
            string dato = blGlobal.pKeyPress(txtCliente, e);
            if (dato != "")
            {
                Busqueda(dato, "Cliente");
            }
            if (dato == "")
            {
                picVerCliente.Enabled = false;
                codigoCliente = "";
            }
        }

        private void picBuscarCliente_Click(object sender, EventArgs e)
        {
            Busqueda("", "Cliente");
        }

        private void picVerCliente_Click(object sender, EventArgs e)
        {
            frmMantCliente frm = new frmMantCliente();
            frm.cod_cliente = codigoCliente;
            frm.MiAccion = Cliente.Editar;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.cod_empresa = lkpEmpresa.EditValue.ToString();
            frm.user = user;
            frm.ShowDialog();

            CargarSedes();
        }

        private void tvSedesCliente_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.PrevFocusedRowHandle == -2147483648) return;

            //tvSedesCliente.DataRowCount;

            eCliente_Direccion eDir = tvSedesCliente.GetFocusedRow() as eCliente_Direccion;



            dtpFechaVisita.EditValue = DateTime.Now;
            dtpHoraInicio.EditValue = DateTime.Parse("01/01/1900 00:00:00.000");
            dtpHoraFin.EditValue = DateTime.Parse("01/01/1900 00:00:00.000");
            meObservaciones.EditValue = "";

            if (lstAns == null) return;

            lstAns = blAns.ListarAnalisis<eAnalisis.eAnalisis_Sedes_Prestacion>(2, empresa, sedeEmpresa, analisis);

            foreach (eAnalisis.eAnalisis_Sedes_Prestacion obj in lstAns)
            {
                if (obj.cod_sede_cliente == eDir.num_linea)
                {
                    dtpFechaVisita.EditValue = obj.fch_visita;
                    dtpHoraInicio.EditValue = obj.dsc_hora_inicio_visita;
                    dtpHoraFin.EditValue = obj.dsc_hora_fin_visita;
                    meObservaciones.EditValue = obj.dsc_observaciones;
                    break;
                }
            }

            CargarTiposxSede(eDir.num_linea);
            gvTipoServicio.RefreshData();
        }

        private void tvSedesCliente_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (gReq)
            {
                eAnalisis eAns = GuardarCabecera();
                empresa = eAns.cod_empresa; sedeEmpresa = eAns.cod_sede_empresa; analisis = eAns.cod_analisis;
                eAnalisis.eAnalisis_Sedes eAnsSed = GuardarDetalleSedes();
                GuardarPrestacionSede(eAnsSed);

                accion = RequerimientoAns.Editar;
                gReq = false;
                ConfigurarForm();

                //DialogResult result = MessageBox.Show("¿Desea cambiar de sede? Los datos no guardados se perderían.", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                //if (result == DialogResult.No) e.Allow = false;
            }
        }

        private void ckMostrarSedes_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMostrarSedes.CheckState == CheckState.Checked)
            {
                CargarSedes();
            }
            else
            {
                CargarSedes(lstAns);
            }
        }

        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            blAns.CargaCombosLookUp("Sedes", lkpSedeEmpresa, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());
            lkpSedeEmpresa.EditValue = "00001";

            bsTipoServicio.DataSource = blAns.ListarGeneral<eDatos>("TipoPrestacion", empresa: lkpEmpresa.EditValue.ToString());
        }

        private void dtpFechaVisita_Enter(object sender, EventArgs e)
        {
            gReq = true;
        }

        private void meObservaciones_Enter(object sender, EventArgs e)
        {
            gReq = true;
        }

        private void gvTipoServicio_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gReq = true;
        }

        private void gvTipoServicio_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvTipoServicio_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void rckbSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            gvTipoServicio.PostEditor();
        }

        private void frmMantRequerimientoAnalisis_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (gReq)
            {
                eAnalisis eAns = GuardarCabecera();
                empresa = eAns.cod_empresa; sedeEmpresa = eAns.cod_sede_empresa; analisis = eAns.cod_analisis;
                eAnalisis.eAnalisis_Sedes eAnsSed = GuardarDetalleSedes();
                GuardarPrestacionSede(eAnsSed);
            }
        }
    }
}