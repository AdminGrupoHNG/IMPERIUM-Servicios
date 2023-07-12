using BE_Servicios;
using BL_Servicios;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_Servicios.Clientes_Y_Proveedores.Clientes;
using UI_Servicios.Formularios.Shared;
using Excel = Microsoft.Office.Interop.Excel;

namespace UI_Servicios.Formularios.Logistica
{
    internal enum RequerimientoServicio
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }

    public partial class frmMantRequerimientosServicio : DevExpress.XtraEditors.XtraForm
    {
        internal RequerimientoServicio accion = RequerimientoServicio.Nuevo;

        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        blEncrypta blEncryp = new blEncrypta();
        blSistema blSist = new blSistema();
        blRequerimiento blReq = new blRequerimiento();
        blProveedores blProv = new blProveedores();
        TaskScheduler scheduler;

        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public string codigoEmpresa;
        public string empresa, sede, requerimiento, solicitud;
        public Int32 anho;

        eRequerimiento eRequerimientoEdit = new eRequerimiento();
        List<eRequerimiento.eRequerimiento_Detalle> eDetRequerimientoEdit;
        String codigoCliente = "CLI0000000";
        String codigoProveedorDet = "PR000000";
        String dscProveedorDet = "";
        String rucProveedorDet = "";

        public frmMantRequerimientosServicio()
        {
            InitializeComponent();
        }

        private void frmMantRequerimientos_Load(object sender, EventArgs e)
        {
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Inicializar();
        }

        private void Inicializar()
        {
            try
            {
                CargarLookUpEdit();

                switch (accion)
                {
                    case RequerimientoServicio.Nuevo:
                        ConfigurarForm("Nuevo");
                        CargarListadoProductos();
                        DateTime date = DateTime.Now;
                        dtpFechaRequerimiento.EditValue = date;
                        dtpFechaAtencion.Properties.MinValue = date;
                        dtpFechaAtencion.EditValue = date;
                        break;
                    case RequerimientoServicio.Editar:
                        CargarRequerimiento();
                        ConfigurarForm("Editar");
                        break;
                    case RequerimientoServicio.Vista:
                        CargarRequerimiento();
                        ConfigurarForm("Vista");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargarLookUpEdit()
        {
            try
            {
                blReq.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: user.cod_usuario);
                blReq.CargaCombosLookUp("TiposReq", lkpTipo, "cod_tipo", "dsc_tipo", "", valorDefecto: true);

                if (codigoEmpresa == null)
                {
                    List<eFacturaProveedor> list = blProv.ListarEmpresasProveedor<eFacturaProveedor>(11, "", user.cod_usuario);
                    if (list.Count >= 1) lkpEmpresa.EditValue = list[0].cod_empresa;

                    codigoEmpresa = list[0].cod_empresa;
                }
                else
                {
                    lkpEmpresa.EditValue = codigoEmpresa;
                }

                blReq.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());

                lkpTipo.EditValue = "REQ";
                lkpSede.EditValue = "00001";
                lkpArea.EditValue = null;

                txtSolicitante.EditValue = user.dsc_usuario;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargarListadoProductos()
        {
            try
            {
                List<eProductos> lista = blReq.ListarProductos<eProductos>(8, lkpEmpresa.EditValue.ToString());
                bsProductos.DataSource = lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CargarRequerimiento()
        {
            eRequerimientoEdit = blReq.Cargar_Requerimiento<eRequerimiento>(4, empresa, sede, requerimiento, solicitud, anho);

            txtRequerimiento.EditValue = eRequerimientoEdit.cod_requerimiento;
            lkpEmpresa.EditValue = eRequerimientoEdit.cod_empresa;
            lkpSede.EditValue = eRequerimientoEdit.cod_sede_empresa;
            lkpArea.EditValue = eRequerimientoEdit.cod_area;
            codigoCliente = eRequerimientoEdit.cod_cliente;

            txtCliente.EditValue = eRequerimientoEdit.dsc_razon_social;

            blReq.CargaCombosLookUp("SedesCliente", lkpSedeCliente, "cod_sede_cliente", "dsc_sede_cliente", "", valorDefecto: true, cod_cliente: codigoCliente);

            lkpSedeCliente.EditValue = eRequerimientoEdit.cod_sede_cliente;
            lkpTipo.EditValue = eRequerimientoEdit.cod_tipo;
            txtSolicitante.EditValue = eRequerimientoEdit.dsc_nombre_solicitante;
            dtpFechaRequerimiento.EditValue = eRequerimientoEdit.fch_requerimiento;
            dtpFechaAtencion.EditValue = eRequerimientoEdit.fch_atencion;
            txtJustificacion.EditValue = eRequerimientoEdit.dsc_justificacion;
            txtObservaciones.EditValue = eRequerimientoEdit.dsc_observaciones;
            txtProdRequeridos.EditValue = eRequerimientoEdit.dsc_items_requeridos;

            eDetRequerimientoEdit = blReq.Cargar_Detalle_Requerimiento<eRequerimiento.eRequerimiento_Detalle>(5, empresa, sede, requerimiento, solicitud, anho);

            bsDetalleReq.DataSource = eDetRequerimientoEdit;
        }

        private void ConfigurarForm(string opcion)
        {
            switch (opcion)
            {
                case "Nuevo":
                    colimp_unitario.Visible = false;
                    colimp_total.Visible = false;
                    coldsc_proveedor.Visible = false;
                    colbtnAgregar_proveedor.Visible = false;
                    break;
                case "Editar":
                    btnGenerar.Caption = btnGenerar.Caption.Replace("Generar", "Actualizar");
                    btnAgregarProductos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    btnClonar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    btnExportarExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                    lkpEmpresa.Enabled = false;
                    lkpSede.Enabled = false;
                    lkpArea.Enabled = false;
                    txtCliente.Enabled = false;
                    lkpSedeCliente.Enabled = false;
                    btnBuscarCliente.Enabled = false;
                    if (codigoCliente != "CLI0000000") { btnVerCliente.Enabled = true; } else { btnVerCliente.Enabled = false; }
                    txtSolicitante.Enabled = false;
                    dtpFechaRequerimiento.Enabled = false;
                    lkpTipo.Enabled = false;

                    controlGProductos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    blancoUno.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    blancoDos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlQuitar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    blancoTres.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                    colimp_unitario.Visible = true;
                    colimp_total.Visible = true;
                    coldsc_proveedor.Visible = true;
                    colbtnAgregar_proveedor.Visible = true;
                    colbtnAgregar_proveedor.VisibleIndex = 10;
                    break;
                case "Vista":
                    List<eVentana> listPerfil = blSist.ListarPerfilesUsuario<eVentana>(4, user.cod_usuario);
                    eVentana oPerfilLog = listPerfil.Find(x => x.cod_perfil == 28 || x.cod_perfil == 5 || x.cod_perfil == 26);

                    btnGenerar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    btnClonar.Visibility = oPerfilLog != null ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                    btnExportarExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                    lkpEmpresa.Enabled = false;
                    lkpSede.Enabled = false;
                    lkpArea.Enabled = false;
                    txtCliente.Enabled = false;
                    lkpSedeCliente.Enabled = false;
                    btnBuscarCliente.Enabled = false;
                    if (codigoCliente != "CLI0000000") { btnVerCliente.Enabled = true; } else { btnVerCliente.Enabled = false; }
                    btnBuscarCliente.Enabled = false;
                    dtpFechaRequerimiento.Enabled = false;
                    dtpFechaAtencion.Enabled = false;
                    txtSolicitante.Enabled = false;
                    lkpTipo.Enabled = false;
                    txtJustificacion.Enabled = false;
                    txtObservaciones.Enabled = false;
                    txtProdRequeridos.Enabled = false;

                    controlGProductos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    blancoUno.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    blancoDos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlQuitar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    blancoTres.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                    colnum_cantidad.OptionsColumn.AllowEdit = false;
                    colimp_unitario.OptionsColumn.AllowEdit = false;
                    colbtnAgregar_proveedor.Visible = false;
                    break;
                case "AgregarProducto":
                    controlGProductos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    blancoUno.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    controlAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    blancoDos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    controlQuitar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    blancoTres.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    colimp_unitario.Visible = false;
                    colimp_total.Visible = false;
                    coldsc_proveedor.Visible = false;
                    colbtnAgregar_proveedor.Visible = false;
                    break;
            }
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConfigurarForm("Nuevo");
            CargarListadoProductos();

            DateTime date = DateTime.Now;
            dtpFechaRequerimiento.EditValue = date;
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (accion == RequerimientoServicio.Nuevo)
            {
                btnAgregarProductos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnOcultar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Generando Requerimiento", "Cargando...");

            try
            {
                if (ValidarCantidades() == "0") { MessageBox.Show("Debe seleccionar productos al requerimiento.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (ValidarCantidades() == "1") { MessageBox.Show("Las cantidades no pueden ser iguales a 0.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (txtSolicitante.Text == "") { MessageBox.Show("El campo solicitante no puede estar vacío.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtSolicitante.Focus(); return; }

                eRequerimiento eReq = AsignarValores();
                
                eReq = blReq.Ins_Act_Requerimiento<eRequerimiento>(eReq, user.cod_usuario);

                switch (accion)
                {
                    case RequerimientoServicio.Editar:
                        string respuesta = blReq.Limpiar_Det_Requerimiento(eReq.cod_empresa, eReq.cod_sede_empresa, eReq.cod_requerimiento, eReq.flg_solicitud, eReq.dsc_anho);
                        break;
                }

                gvDetalleReq.PostEditor();
                for (int x = 0; x < gvDetalleReq.DataRowCount; x++)
                {
                    eRequerimiento.eRequerimiento_Detalle eDetReq = gvDetalleReq.GetRow(x) as eRequerimiento.eRequerimiento_Detalle;

                    eDetReq.cod_empresa = eReq.cod_empresa;
                    eDetReq.cod_sede_empresa = eReq.cod_sede_empresa;
                    eDetReq.cod_requerimiento = eReq.cod_requerimiento;
                    eDetReq.flg_solicitud = eReq.flg_solicitud;
                    eDetReq.dsc_anho = eReq.dsc_anho;
                    eDetReq.num_restante = eDetReq.num_cantidad;
                    eDetReq.cod_proveedor = codigoProveedorDet;
                    eDetReq.imp_unitario = eReq.flg_solicitud == "C" ? 0 : eDetReq.imp_unitario;
                    eDetReq.imp_total = eReq.flg_solicitud == "C" ? 0 : eDetReq.imp_total;

                    eDetReq = blReq.Ins_Act_Detalle_Requerimiento<eRequerimiento.eRequerimiento_Detalle>(eDetReq, user.cod_usuario);
                }

                empresa = eReq.cod_empresa; sede = eReq.cod_sede_empresa; requerimiento = eReq.cod_requerimiento; solicitud = eReq.flg_solicitud.ToString().Substring(0, 1); anho = eReq.dsc_anho;

                switch (accion)
                {
                    case RequerimientoServicio.Nuevo:
                        SplashScreenManager.CloseForm();
                        MessageBox.Show("Requerimiento generado de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRequerimiento.EditValue = eReq.cod_requerimiento.ToString();
                        accion = RequerimientoServicio.Editar;
                        btnClonar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        btnExportarExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        btnGenerar.Caption = btnGenerar.Caption.Replace("Generar", "Actualizar");
                        break;
                    case RequerimientoServicio.Editar:
                        SplashScreenManager.CloseForm();
                        MessageBox.Show("Requerimiento actualizado de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarRequerimiento();
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string ValidarCantidades()
        {
            string respuesta = "";

            if (gvDetalleReq.DataRowCount == 0)
            {
                respuesta = "0";
            }
            else
            {
                for (int x = 0; x < gvDetalleReq.DataRowCount; x++)
                {
                    eRequerimiento.eRequerimiento_Detalle eDetReq = gvDetalleReq.GetRow(x) as eRequerimiento.eRequerimiento_Detalle;

                    if (eDetReq.num_cantidad == 0)
                    {
                        respuesta = "1";
                        break;
                    }
                }
            }

            return respuesta;
        }

        private eRequerimiento AsignarValores()
        {
            eRequerimiento eRequ = new eRequerimiento();

            eRequ.cod_empresa = lkpEmpresa.EditValue.ToString();
            eRequ.cod_sede_empresa = lkpSede.EditValue.ToString();
            eRequ.cod_requerimiento = "";
            eRequ.cod_cliente = codigoCliente;
            eRequ.cod_sede_cliente = lkpSedeCliente.EditValue == null ? 0 : Int32.Parse(lkpSedeCliente.EditValue.ToString());
            eRequ.cod_area = lkpArea.EditValue == null ? "" : lkpArea.EditValue.ToString();
            eRequ.flg_solicitud = "S";
            eRequ.cod_estado_requerimiento = "";
            eRequ.dsc_nombre_solicitante = txtSolicitante.EditValue == null ? "" : txtSolicitante.EditValue.ToString();
            eRequ.fch_requerimiento = Convert.ToDateTime(dtpFechaRequerimiento.EditValue);
            eRequ.fch_atencion = Convert.ToDateTime(dtpFechaAtencion.EditValue);
            eRequ.dsc_observaciones = txtObservaciones.EditValue == null ? "" : txtObservaciones.EditValue.ToString();
            eRequ.dsc_justificacion = txtJustificacion.EditValue == null ? "" : txtJustificacion.EditValue.ToString();
            eRequ.dsc_items_requeridos = txtProdRequeridos.EditValue == null ? "" : txtProdRequeridos.EditValue.ToString();
            eRequ.cod_tipo = lkpTipo.EditValue.ToString();

            switch (accion)
            {
                case RequerimientoServicio.Editar:
                    eRequ.cod_requerimiento = txtRequerimiento.EditValue.ToString();
                    break;
            }

            return eRequ;
        }

        private void btnAgregarProductos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CargarListadoProductos();
            ConfigurarForm("AgregarProducto");

            for (int x = 0; x < gvDetalleReq.DataRowCount; x++)
            {
                eRequerimiento.eRequerimiento_Detalle obj = gvDetalleReq.GetRow(x) as eRequerimiento.eRequerimiento_Detalle;

                for (int i = 0; i < gvProductos.DataRowCount; i++)
                {
                    eProductos obj2 = gvProductos.GetRow(i) as eProductos;

                    if (obj2.cod_tipo_servicio == obj.cod_tipo_servicio && obj2.cod_subtipo_servicio == obj.cod_subtipo_servicio && obj2.cod_producto == obj.cod_producto)
                    {
                        bsProductos.Remove(obj2);
                    }
                }
            }

            btnAgregarProductos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnOcultar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void btnOcultar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (accion == RequerimientoServicio.Nuevo)
            {
                controlGProductos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                blancoUno.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                controlAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                blancoDos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                controlQuitar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                blancoTres.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                colimp_unitario.Visible = true;
                colimp_total.Visible = true;
                coldsc_proveedor.Visible = true;
            }
            else
            {
                ConfigurarForm("Editar");
            }

            btnAgregarProductos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            btnOcultar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void btnClonar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnGenerar.Caption = btnGenerar.Caption.Replace("Actualizar", "Generar");
            btnGenerar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            btnAgregarProductos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnOcultar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            accion = RequerimientoServicio.Nuevo;
            txtRequerimiento.EditValue = null;

            lkpSede.Enabled = true;
            if (lkpArea.EditValue == null) { lkpArea.Enabled = false; } else { lkpArea.Enabled = true; }
            if (codigoCliente == "CLI0000000") { btnVerCliente.Enabled = false; } else { btnVerCliente.Enabled = true; btnBuscarCliente.Enabled = true; txtCliente.Enabled = true; lkpSedeCliente.Enabled = true; }
            dtpFechaRequerimiento.Enabled = true;
            dtpFechaAtencion.Enabled = true;
            txtSolicitante.Enabled = true;
            txtJustificacion.Enabled = true;
            txtObservaciones.Enabled = true;
            txtProdRequeridos.Enabled = true;

            ConfigurarForm("AgregarProducto");
        }

        private void btnExportarExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportarExcel();
        }

        private void ExportarExcel()
        {
            try
            {
                string carpeta = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\Requerimiento - " + txtRequerimiento.EditValue.ToString() + " - " + DateTime.Now.ToShortDateString().Replace("/", "-").Replace(":", "") + ".xlsx";

                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

                gvDetalleReq.ExportToXlsx(archivo);

                if (MessageBox.Show("Excel exportado en la ruta " + archivo + Environment.NewLine + "¿Desea abrir el archivo?", "Exportar Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(archivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            Busqueda("", "Cliente");
        }

        private void btnVerCliente_Click(object sender, EventArgs e)
        {
            frmMantCliente frm = new frmMantCliente();
            frm.cod_cliente = codigoCliente;
            frm.MiAccion = Cliente.Vista;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.cod_empresa = lkpEmpresa.EditValue.ToString();
            frm.user = user;
            frm.ShowDialog();
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
                case "ProveedorDet":
                    frm.entidad = frmBusquedas.MiEntidad.Proveedor;
                    frm.filtroRUC = filtroRUC;
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Cliente":
                    codigoCliente = frm.codigo;
                    txtCliente.EditValue = frm.descripcion;

                    blReq.CargaCombosLookUp("SedesCliente", lkpSedeCliente, "cod_sede_cliente", "dsc_sede_cliente", "", valorDefecto: true, cod_cliente: codigoCliente);
                    lkpSedeCliente.EditValue = 1;

                    btnVerCliente.Enabled = true;
                    break;
                case "ProveedorDet":
                    codigoProveedorDet = frm.codigo;
                    dscProveedorDet = frm.descripcion;
                    rucProveedorDet = frm.ruc;
                    break;
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
                lkpArea.Enabled = true;
                btnVerCliente.Enabled = false;
                codigoCliente = "CLI0000000";
            }
        }

        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            blReq.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());
            CargarListadoProductos();
        }

        private void lkpSede_EditValueChanged(object sender, EventArgs e)
        {
            blReq.CargaCombosLookUp("Areas", lkpArea, "cod_area", "dsc_area", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString(), cod_sede_empresa: lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString());
            lkpArea.EditValue = null;
        }

        private void lkpArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) lkpArea.EditValue = null;
        }

        private void dtpFechaRequerimiento_EditValueChanged(object sender, EventArgs e)
        {
            DateTime date = Convert.ToDateTime(dtpFechaRequerimiento.EditValue);

            dtpFechaAtencion.EditValue = date;
            dtpFechaAtencion.Properties.MinValue = date;
        }

        private void gvProductos_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvProductos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvProductos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvProductos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int valor = 0;
            foreach (int nRow in gvProductos.GetSelectedRows())
            {
                eProductos obj = gvProductos.GetRow(nRow - valor) as eProductos;
                eRequerimiento.eRequerimiento_Detalle obj2 = new eRequerimiento.eRequerimiento_Detalle();

                obj2.cod_tipo_servicio = obj.cod_tipo_servicio;
                obj2.dsc_tipo_servicio = obj.dsc_tipo_servicio;
                obj2.cod_subtipo_servicio = obj.cod_subtipo_servicio;
                obj2.dsc_subtipo_servicio = obj.dsc_subtipo_servicio;
                obj2.cod_producto = obj.cod_producto;
                obj2.dsc_producto = obj.dsc_producto;
                obj2.cod_unidad_medida = obj.cod_unidad_medida;
                obj2.dsc_simbolo = obj.dsc_simbolo;


                bsDetalleReq.Add(obj2);
                bsProductos.Remove(obj);
                valor = valor + 1;
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            int valor = 0;
            foreach (int nRow in gvDetalleReq.GetSelectedRows())
            {
                eRequerimiento.eRequerimiento_Detalle obj = gvDetalleReq.GetRow(nRow - valor) as eRequerimiento.eRequerimiento_Detalle;
                eProductos obj2 = new eProductos();

                obj2.cod_tipo_servicio = obj.cod_tipo_servicio;
                obj2.dsc_tipo_servicio = obj.dsc_tipo_servicio;
                obj2.cod_subtipo_servicio = obj.cod_subtipo_servicio;
                obj2.dsc_subtipo_servicio = obj.dsc_subtipo_servicio;
                obj2.cod_producto = obj.cod_producto;
                obj2.dsc_producto = obj.dsc_producto;
                obj2.cod_unidad_medida = obj.cod_unidad_medida;
                obj2.dsc_simbolo = obj.dsc_simbolo;

                bsProductos.Add(obj2);
                bsDetalleReq.Remove(obj);
                valor = valor + 1;
            }
        }

        private void gvDetalleReq_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (gvDetalleReq.FocusedColumn.Name == "colnum_cantidad")
            {
                eRequerimiento.eRequerimiento_Detalle obj = gvDetalleReq.GetFocusedRow() as eRequerimiento.eRequerimiento_Detalle;

                if (obj.num_cantidad == 0) { MessageBox.Show("Debe seleccionar una cantidad mayor a cero.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); obj.num_cantidad = 1; return; }

                obj.imp_total = obj.num_cantidad * obj.imp_unitario;
            }

            if (gvDetalleReq.FocusedColumn.Name == "colimp_unitario")
            {
                eRequerimiento.eRequerimiento_Detalle obj = gvDetalleReq.GetFocusedRow() as eRequerimiento.eRequerimiento_Detalle;

                obj.imp_total = obj.num_cantidad * obj.imp_unitario;
            }
        }

        private void gvDetalleReq_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            string cell = gvDetalleReq.GetRowCellDisplayText(e.RowHandle, e.Column);

            if (cell == "0" || cell == "0.0000")
            {
                e.Appearance.ForeColor = Color.Red;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void gvDetalleReq_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvDetalleReq_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvDetalleReq_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void rbtnAgregarProv_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Busqueda("", "ProveedorDet");

            eRequerimiento.eRequerimiento_Detalle eOrdComNue = gvDetalleReq.GetRow(gvDetalleReq.FocusedRowHandle) as eRequerimiento.eRequerimiento_Detalle;

            if (codigoProveedorDet != "PR000000")
            {
                eOrdComNue.cod_proveedor = codigoProveedorDet;
                eOrdComNue.dsc_proveedor = dscProveedorDet;
            }
            gvDetalleReq.RefreshData();
        }
    }
}