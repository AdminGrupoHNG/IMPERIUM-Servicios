using BE_Servicios;
using BL_Servicios;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
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
using UI_Servicios.Formularios.Shared;

namespace UI_Servicios.Formularios.Logistica
{
    public partial class frmListadoOrdenesCompra : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public eUsuario user = new eUsuario();
        blEncrypta blEncryp = new blEncrypta();
        blSistema blSist = new blSistema();
        blGlobales blGlobal = new blGlobales();
        blProveedores blProv = new blProveedores();
        blOrdenCompra_Servicio blOrd = new blOrdenCompra_Servicio();
        TaskScheduler scheduler;
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;

        String codigoProveedor = "";
        bool isRunning = false;

        public frmListadoOrdenesCompra()
        {
            InitializeComponent();
        }

        private void frmListadoOrdenesCompra_Load(object sender, EventArgs e)
        {
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Inicializar();
        }

        private void Inicializar()
        {
            try
            {
                btnNuevaOrdCom.Visibility = BarItemVisibility.Never;

                CargarLookUpEdit();
                
                DateTime date = DateTime.Now;
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
                dtpDesde.EditValue = oPrimerDiaDelMes;
                dtpHasta.EditValue = oUltimoDiaDelMes;
                HabilitarBotones();
                BuscarOrdenesCompra();
                tcOrdenCompra_SelectedPageChanged(tcOrdenCompra, new DevExpress.XtraTab.TabPageChangedEventArgs(null, tpOrdenesGeneradas));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = blSist.ListarMenuxUsuario<eVentana>(user.cod_usuario, this.Name);
            if (listPermisos.Count > 0)
            {
                grupoEdicion.Enabled = listPermisos[0].flg_escritura;
                grupoAcciones.Enabled = listPermisos[0].flg_escritura;
            }
        }

        private void CargarLookUpEdit()
        {
            try
            {
                blOrd.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: user.cod_usuario);
                blOrd.CargaCombosLookUp("TipoFecha", lkpTipoFecha, "cod_tipo_fecha", "dsc_tipo_fecha", "", valorDefecto: true);

                List<eFacturaProveedor> list = blProv.ListarEmpresasProveedor<eFacturaProveedor>(11, "", user.cod_usuario);
                if (list.Count >= 1) lkpEmpresa.EditValue = list[0].cod_empresa;

                blOrd.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());

                lkpTipoFecha.ItemIndex = 0;
                lkpSede.EditValue = "00001";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void BuscarOrdenesCompra()
        {
            try
            {
                List<eOrdenCompra_Servicio> ordGeneradas = blOrd.ListarOrdenesCompra<eOrdenCompra_Servicio>(1, lkpEmpresa.EditValue.ToString(),
                                                                                      lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                                                                                      txtProveedor.EditValue == null ? "" : codigoProveedor,
                                                                                      lkpTipoFecha.EditValue.ToString(),
                                                                                      Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                                                                                      Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd"),
                                                                                      "C");

                List<eOrdenCompra_Servicio> ordAprobadas = blOrd.ListarOrdenesCompra<eOrdenCompra_Servicio>(4, lkpEmpresa.EditValue.ToString(),
                                                                                      lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                                                                                      txtProveedor.EditValue == null ? "" : codigoProveedor,
                                                                                      lkpTipoFecha.EditValue.ToString(),
                                                                                      Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                                                                                      Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd"),
                                                                                      "C");

                List<eOrdenCompra_Servicio> ordEnviadas = blOrd.ListarOrdenesCompra<eOrdenCompra_Servicio>(7, lkpEmpresa.EditValue.ToString(),
                                                                                      lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                                                                                      txtProveedor.EditValue == null ? "" : codigoProveedor,
                                                                                      lkpTipoFecha.EditValue.ToString(),
                                                                                      Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                                                                                      Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd"),
                                                                                      "C");

                List<eOrdenCompra_Servicio> ordAtendidas = blOrd.ListarOrdenesCompra<eOrdenCompra_Servicio>(5, lkpEmpresa.EditValue.ToString(),
                                                                                      lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                                                                                      txtProveedor.EditValue == null ? "" : codigoProveedor,
                                                                                      lkpTipoFecha.EditValue.ToString(),
                                                                                      Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                                                                                      Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd"),
                                                                                      "C");

                List<eOrdenCompra_Servicio> ordLiquidadas = blOrd.ListarOrdenesCompra<eOrdenCompra_Servicio>(6, lkpEmpresa.EditValue.ToString(),
                                                                                      lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                                                                                      txtProveedor.EditValue == null ? "" : codigoProveedor,
                                                                                      lkpTipoFecha.EditValue.ToString(),
                                                                                      Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                                                                                      Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd"),
                                                                                      "C");

                List<eOrdenCompra_Servicio> ordAnuladas = blOrd.ListarOrdenesCompra<eOrdenCompra_Servicio>(9, lkpEmpresa.EditValue.ToString(),
                                                                                      lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                                                                                      txtProveedor.EditValue == null ? "" : codigoProveedor,
                                                                                      lkpTipoFecha.EditValue.ToString(),
                                                                                      Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                                                                                      Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd"),
                                                                                      "C");

                bsListadoOrdGeneradas.DataSource = ordGeneradas;
                bsListadoOrdAprobadas.DataSource = ordAprobadas;
                bsListadoOrdEnviadas.DataSource = ordEnviadas;
                bsListadoOrdAtendidas.DataSource = ordAtendidas;
                bsListadoOrdLiquidadas.DataSource = ordLiquidadas;
                bsListadoOrdAnuladas.DataSource = ordAnuladas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAnularOrdCom_ItemClick(object sender, ItemClickEventArgs e)
        {
            string respuesta = "";

            try
            {
                foreach (int nRow in gvOrdGeneradas.GetSelectedRows())
                {
                    eOrdenCompra_Servicio obj = gvOrdGeneradas.GetRow(nRow) as eOrdenCompra_Servicio;

                    respuesta = blOrd.Anular_Orden(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_orden_compra_servicio, obj.flg_solicitud, obj.dsc_anho, user.cod_usuario);
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Anulación realizada con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Anular los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarOrdenesCompra();
        }

        private void btnEliminarOrdCom_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnNuevaOrdCom_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMantOrdenCompra frm = new frmMantOrdenCompra();
            frm.user = user;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.codigoEmpresa = lkpEmpresa.EditValue.ToString();
            frm.ShowDialog();
        }

        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportarExcel();
        }

        private void ExportarExcel()
        {
            try
            {
                string carpeta = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\RequerimientosOC" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                
                switch (tcOrdenCompra.SelectedTabPage.Name)
                {
                    case "tpOrdenesGeneradas": gvOrdGeneradas.ExportToXlsx(archivo); break;
                    case "tpOrdenesAprobadas": gvOrdAprobadas.ExportToXlsx(archivo); break;
                    case "tpOrdenesEnviadas": gvOrdEnviadas.ExportToXlsx(archivo); break;
                    case "tpOrdenesAtendidas": gvOrdAtendidas.ExportToXlsx(archivo); break;
                    case "tpOrdenesLiquidadas": gvOrdLiquidadas.ExportToXlsx(archivo); break;
                    case "tpOrdenesAnuladas": gvOrdAnuladas.ExportToXlsx(archivo); break;
                }
                
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

        private void btnImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            switch (tcOrdenCompra.SelectedTabPage.Name)
            {
                case "tpOrdenesGeneradas": gvOrdGeneradas.ShowPrintPreview(); break;
                case "tpOrdenesAprobadas": gvOrdAprobadas.ShowPrintPreview(); break;
                case "tpOrdenesEnviadas": gvOrdEnviadas.ShowPrintPreview(); break;
                case "tpOrdenesAtendidas": gvOrdAtendidas.ShowPrintPreview(); break;
                case "tpOrdenesLiquidadas": gvOrdLiquidadas.ShowPrintPreview(); break;
                case "tpOrdenesAnuladas": gvOrdAnuladas.ShowPrintPreview(); break;
            }
        }

        private void btnReporteOrdenCompra_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eOrdenCompra_Servicio eProv = new eOrdenCompra_Servicio();

                switch (tcOrdenCompra.SelectedTabPage.Name)
                {
                    case "tpOrdenesGeneradas": eProv = gvOrdGeneradas.GetFocusedRow() as eOrdenCompra_Servicio; break;
                    case "tpOrdenesAprobadas": eProv = gvOrdAprobadas.GetFocusedRow() as eOrdenCompra_Servicio; break;
                    case "tpOrdenesEnviadas": eProv = gvOrdEnviadas.GetFocusedRow() as eOrdenCompra_Servicio; break;
                    case "tpOrdenesAtendidas": eProv = gvOrdAtendidas.GetFocusedRow() as eOrdenCompra_Servicio; break;
                    case "tpOrdenesLiquidadas": eProv = gvOrdLiquidadas.GetFocusedRow() as eOrdenCompra_Servicio; break;
                }

                if (eProv == null) { return; }

                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");

                if (eProv.cod_empresa == "00004")
                {
                    rptOrdenComprask2 report = new rptOrdenComprask2();

                    ReportPrintTool printTool = new ReportPrintTool(report);
                    report.RequestParameters = false;
                    printTool.AutoShowParametersPanel = false;
                    report.Parameters["cod_almacen"].Value = eProv.cod_almacen;
                    report.Parameters["cod_empresa"].Value = eProv.cod_empresa;
                    report.Parameters["cod_sede_empresa"].Value = eProv.cod_sede_empresa;
                    report.Parameters["cod_proveedor"].Value = eProv.cod_proveedor;
                    report.Parameters["cod_orden_compra_servicio"].Value = eProv.cod_orden_compra_servicio;
                    report.xpb_logo.Image = Properties.Resources.logo_k2;
                    report.tblcuadro.BackColor = Color.FromArgb(0, 157, 150);
                    report.lblProveedor.BackColor = Color.FromArgb(0, 157, 150);
                    report.LblRefProv.BackColor = Color.FromArgb(0, 157, 150);
                    report.tbcargo.BackColor = Color.FromArgb(0, 157, 150);
                    report.tbcci.BackColor = Color.FromArgb(0, 157, 150);
                    report.tbentbanc.BackColor = Color.FromArgb(0, 157, 150);
                    report.tboficina.BackColor = Color.FromArgb(0, 157, 150);
                    report.tbusureg.BackColor = Color.FromArgb(0, 157, 150);
                    report.tbnumc.BackColor = Color.FromArgb(0, 157, 150);
                    report.tbtipoc.BackColor = Color.FromArgb(0, 157, 150);
                    report.tbsgi.BackColor = Color.FromArgb(0, 157, 150);
                    report.tbobs.BackColor = Color.FromArgb(0, 157, 150);
                    report.lblempresa.BackColor = Color.FromArgb(0, 157, 150);
                    report.lbl_direccion.BackColor = Color.FromArgb(0, 157, 150);
                    report.lblruc.BackColor = Color.FromArgb(0, 157, 150);
                    report.lblreq.BackColor = Color.FromArgb(0, 157, 150);
                    report.lblorden.BackColor = Color.FromArgb(0, 157, 150);
                    report.lblfecha.BackColor = Color.FromArgb(0, 157, 150);
                    report.lbltipocompra.BackColor = Color.FromArgb(0, 157, 150);
                    report.tblatencion.BackColor = Color.FromArgb(0, 157, 150);
                    report.tbModalidadP.BackColor = Color.FromArgb(0, 157, 150);
                    report.tbMoneda.BackColor = Color.FromArgb(0, 157, 150);

                    if (eProv.cod_moneda == "SOL") { report.chkSoles.Checked = true; report.chkDolares.Checked = false; } else { report.chkSoles.Checked = false; report.chkDolares.Checked = true; }
                    if (eProv.flg_solicitud == "C") { report.chkProductos.Checked = true; } else { report.chkServicios.Checked = true; report.txtOrden.Text = "ORDEN DE SERVICIO"; }
                    if (eProv.cod_formapago == "TRANBAN") { report.chkTransparencia.Checked = true; } else { report.chkCheque.Checked = true; }
                    printTool.ShowPreviewDialog();
                    SplashScreenManager.CloseForm();
                }
                else
                {
                    rptOrdenCompras report1 = new rptOrdenCompras();

                    ReportPrintTool printTool1 = new ReportPrintTool(report1);
                    report1.RequestParameters = false;
                    printTool1.AutoShowParametersPanel = false;
                    report1.Parameters["cod_almacen"].Value = eProv.cod_almacen;
                    report1.Parameters["cod_empresa"].Value = eProv.cod_empresa;
                    report1.Parameters["cod_sede_empresa"].Value = eProv.cod_sede_empresa;
                    report1.Parameters["cod_proveedor"].Value = eProv.cod_proveedor;
                    report1.Parameters["cod_orden_compra_servicio"].Value = eProv.cod_orden_compra_servicio;

                    if (eProv.cod_empresa == "00001")
                    {
                        report1.xpb_logo.Image = Properties.Resources.Logo_HNG1;
                        report1.lblref.BackColor = Color.FromArgb(63, 63, 65);
                        report1.tblcuadro.BackColor = Color.FromArgb(63, 63, 65);

                    }
                    if (eProv.cod_empresa == "00002")
                    {
                        report1.xpb_logo.Image = Properties.Resources.logo_facilita;
                        report1.lblref.BackColor = Color.FromArgb(12, 63, 104);
                        report1.tblcuadro.BackColor = Color.FromArgb(12, 63, 104);
                    }
                    if (eProv.cod_empresa == "00003") { report1.xpb_logo.Image = Properties.Resources.Logo_HNG1; }
                    if (eProv.cod_empresa == "00005") { report1.xpb_logo.Image = Properties.Resources.Logo_HNG1; }

                    if (eProv.cod_empresa == "00006") { report1.xpb_logo.Image = Properties.Resources.Logo_HNG1; }
                    if (eProv.cod_empresa == "00007") { report1.xpb_logo.Image = Properties.Resources.Logo_HNG1; }
                    if (eProv.cod_empresa == "00008") { report1.xpb_logo.Image = Properties.Resources.Logo_HNG1; }
                    if (eProv.cod_empresa == "00009") { report1.xpb_logo.Image = Properties.Resources.Logo_HNG1; }
                    if (eProv.cod_empresa == "00010") { report1.xpb_logo.Image = Properties.Resources.Logo_HNG1; }
                    printTool1.ShowPreviewDialog();
                    SplashScreenManager.CloseForm();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAprobar_ItemClick(object sender, ItemClickEventArgs e)
        {
            string respuesta = "";

            try
            {
                foreach (int nRow in gvOrdGeneradas.GetSelectedRows())
                {
                    eOrdenCompra_Servicio obj = gvOrdGeneradas.GetRow(nRow) as eOrdenCompra_Servicio;

                    if (obj.cod_almacen == null)
                    {
                        MessageBox.Show("Debe ingresar un almacen a la orden " + obj.cod_orden_compra_servicio + ".", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (obj.fch_despacho == null)
                    {
                        MessageBox.Show("Debe ingresar una fecha de despacho a la orden " + obj.cod_orden_compra_servicio + ".", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        respuesta = blOrd.Aprobar_Orden(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_orden_compra_servicio, obj.flg_solicitud, obj.dsc_anho, user.cod_usuario);
                    }
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Aprobación realizada con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Aprobar los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarOrdenesCompra();
        }

        private void btnDesaprobar_ItemClick(object sender, ItemClickEventArgs e)
        {
            string respuesta = "";

            try
            {
                foreach (int nRow in gvOrdAprobadas.GetSelectedRows())
                {
                    eOrdenCompra_Servicio obj = gvOrdAprobadas.GetRow(nRow) as eOrdenCompra_Servicio;

                    respuesta = blOrd.Desaprobar_Orden(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_orden_compra_servicio, obj.flg_solicitud, obj.dsc_anho, user.cod_usuario);
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Desaprobación realizada con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Desaprobar los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarOrdenesCompra();
        }

        private void btnEnviar_ItemClick(object sender, ItemClickEventArgs e)
        {
            string respuesta = "";

            try
            {
                foreach (int nRow in gvOrdAprobadas.GetSelectedRows())
                {
                    eOrdenCompra_Servicio obj = gvOrdAprobadas.GetRow(nRow) as eOrdenCompra_Servicio;

                    respuesta = blOrd.Enviar_Orden(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_orden_compra_servicio, user.cod_usuario, obj.flg_solicitud, obj.dsc_anho);
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Envío realizado con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Enviar los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarOrdenesCompra();
        }

        private void btnAtender_ItemClick(object sender, ItemClickEventArgs e)
        {
            string respuesta = "";

            try
            {
                foreach (int nRow in gvOrdEnviadas.GetSelectedRows())
                {
                    eOrdenCompra_Servicio obj = gvOrdEnviadas.GetRow(nRow) as eOrdenCompra_Servicio;

                    respuesta = blOrd.Atender_Orden(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_orden_compra_servicio, obj.flg_solicitud, obj.dsc_anho, user.cod_usuario);
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Atención realizada con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Atender los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            BuscarOrdenesCompra();
        }

        private void btnLiquidar_ItemClick(object sender, ItemClickEventArgs e)
        {
            string respuesta = "";

            try
            {
                foreach (int nRow in gvOrdAtendidas.GetSelectedRows())
                {
                    eOrdenCompra_Servicio obj = gvOrdAtendidas.GetRow(nRow) as eOrdenCompra_Servicio;

                    respuesta = blOrd.Liquidar_Orden(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_orden_compra_servicio, obj.flg_solicitud, obj.dsc_anho, user.cod_usuario);
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Liquidación realizada con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Liquidar los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarOrdenesCompra();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarOrdenesCompra();
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            Busqueda("", "Proveedor");
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
                case "Proveedor":
                    frm.entidad = frmBusquedas.MiEntidad.Proveedor;
                    frm.filtroRUC = filtroRUC;
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Proveedor":
                    codigoProveedor = frm.codigo;
                    txtProveedor.Text = frm.descripcion;
                    break;
            }
        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Busqueda("", "Proveedor");
            }
            string dato = blGlobal.pKeyPress(txtProveedor, e);
            if (dato != "")
            {
                Busqueda(dato, "Proveedor");
            }
        }

        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            blOrd.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());
        }

        private void frmListadoOrdenesCompra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) BuscarOrdenesCompra();
        }

        private void tcOrdenCompra_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            List<eVentana> listPerfil = blSist.ListarPerfilesUsuario<eVentana>(4, user.cod_usuario);
            eVentana oPerfilAdm = listPerfil.Find(x => x.cod_perfil == 31 || x.cod_perfil == 5);
            eVentana oPerfilLog = listPerfil.Find(x => x.cod_perfil == 31 || x.cod_perfil == 5 || x.cod_perfil == 29);

            if (tcOrdenCompra.SelectedTabPage == tpOrdenesGeneradas)
            {
                btnAnularOrdCom.Enabled = oPerfilAdm != null ? true : false;
                btnReporteOrdenCompra.Enabled = false;
                btnAprobar.Enabled = oPerfilAdm != null ? true : false;
                btnDesaprobar.Enabled = false;
                btnEnviar.Enabled = false;
                btnAtender.Enabled = false;
                btnLiquidar.Enabled = false;
            }
            else if (tcOrdenCompra.SelectedTabPage == tpOrdenesAprobadas)
            {
                btnAnularOrdCom.Enabled = false;
                btnReporteOrdenCompra.Enabled = true;
                btnAprobar.Enabled = false;
                btnDesaprobar.Enabled = oPerfilAdm != null ? true : false;
                btnEnviar.Enabled = oPerfilLog != null ? true : false;
                btnAtender.Enabled = false;
                btnLiquidar.Enabled = false;
            }
            else if (tcOrdenCompra.SelectedTabPage == tpOrdenesEnviadas)
            {
                btnAnularOrdCom.Enabled = false;
                btnReporteOrdenCompra.Enabled = true;
                btnAprobar.Enabled = false;
                btnDesaprobar.Enabled = false;
                btnEnviar.Enabled = false;
                btnAtender.Enabled = oPerfilLog != null ? true : false; ;
                btnLiquidar.Enabled = false;
            }
            else if (tcOrdenCompra.SelectedTabPage == tpOrdenesAtendidas)
            {
                btnAnularOrdCom.Enabled = false;
                btnReporteOrdenCompra.Enabled = true;
                btnAprobar.Enabled = false;
                btnDesaprobar.Enabled = false;
                btnEnviar.Enabled = false;
                btnAtender.Enabled = false;
                btnLiquidar.Enabled = oPerfilLog != null ? true : false; ;
            }
            else
            {
                btnAnularOrdCom.Enabled = false;
                btnReporteOrdenCompra.Enabled = tcOrdenCompra.SelectedTabPage == tpOrdenesLiquidadas ? true : false;
                btnAprobar.Enabled = false;
                btnDesaprobar.Enabled = false;
                btnEnviar.Enabled = false;
                btnAtender.Enabled = false;
                btnLiquidar.Enabled = false;
            }
        }

        private void gvOrdenesCompra_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (isRunning) return;

            isRunning = true;

            GridView View = sender as GridView;

            if (e.Action == CollectionChangeAction.Add && (string)this.gvOrdGeneradas.GetRowCellValue(this.gvOrdGeneradas.FocusedRowHandle, "cod_estado_orden") == "LIQUIDADO")
            {
                View.UnselectRow(e.ControllerRow);
            }

            if (e.Action == CollectionChangeAction.Refresh && View.SelectedRowsCount > 0)
            {
                View.BeginSelection();

                foreach (int Row in View.GetSelectedRows())
                {
                    if ((string)this.gvOrdGeneradas.GetRowCellValue(Row, "cod_estado_orden") == "LIQUIDADO")
                    {
                        View.UnselectRow(Row);
                    }
                }

                View.EndSelection();
            }

            isRunning = false;
        }

        private void gvOrdGeneradas_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarOC("tpOrdenesGeneradas");
        }

        private void gvOrdGeneradas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvOrdGeneradas_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvOrdGeneradas_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvOrdGeneradas_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvOrdAprobadas_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarOC("tpOrdenesAprobadas");
        }

        private void gvOrdAprobadas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvOrdAprobadas_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvOrdAprobadas_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void gvOrdAprobadas_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvOrdEnviadas_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarOC("tpOrdenesEnviadas");
        }

        private void gvOrdEnviadas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvOrdEnviadas_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvOrdEnviadas_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void gvOrdEnviadas_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvOrdAtendidas_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarOC("tpOrdenesAtendidas");
        }

        private void gvOrdAtendidas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvOrdAtendidas_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvOrdAtendidas_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void gvOrdAtendidas_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvOrdLiquidadas_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarOC("tpOrdenesLiquidadas");
        }

        private void gvOrdLiquidadas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvOrdLiquidadas_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvOrdLiquidadas_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void gvOrdLiquidadas_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvOrdAnuladas_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarOC("tpOrdenesAnuladas");
        }

        private void gvOrdAnuladas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvOrdAnuladas_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvOrdAnuladas_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void gvOrdAnuladas_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void MostrarOC(string tabName)
        {
            eOrdenCompra_Servicio obj = new eOrdenCompra_Servicio();
            frmMantOrdenCompra frm = new frmMantOrdenCompra();
            frm.user = user;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;

            List<eVentana> listPerfil = blSist.ListarPerfilesUsuario<eVentana>(4, user.cod_usuario);
            eVentana oPerfilLog = listPerfil.Find(x => x.cod_perfil == 31 || x.cod_perfil == 5 || x.cod_perfil == 29);

            switch (tabName)
            {
                case "tpOrdenesGeneradas": 
                    obj = gvOrdGeneradas.GetFocusedRow() as eOrdenCompra_Servicio;
                    frm.accion = oPerfilLog != null ? OrdenCompra.Editar : OrdenCompra.Vista;
                    break;
                case "tpOrdenesAprobadas": 
                    obj = gvOrdAprobadas.GetFocusedRow() as eOrdenCompra_Servicio;
                    frm.accion = OrdenCompra.Vista;
                    break;
                case "tpOrdenesEnviadas":
                    obj = gvOrdEnviadas.GetFocusedRow() as eOrdenCompra_Servicio;
                    frm.accion = OrdenCompra.Vista;
                    break;
                case "tpOrdenesAtendidas": 
                    obj = gvOrdAtendidas.GetFocusedRow() as eOrdenCompra_Servicio;
                    frm.accion = OrdenCompra.Vista;
                    break;
                case "tpOrdenesLiquidadas": 
                    obj = gvOrdLiquidadas.GetFocusedRow() as eOrdenCompra_Servicio;
                    frm.accion = OrdenCompra.Vista;
                    break;
                case "tpOrdenesAnuladas":
                    obj = gvOrdAnuladas.GetFocusedRow() as eOrdenCompra_Servicio;
                    frm.accion = OrdenCompra.Vista;
                    break;
            }

            frm.empresa = obj.cod_empresa;
            frm.sede = obj.cod_sede_empresa;
            frm.ordenCompraServicio = obj.cod_orden_compra_servicio;
            frm.solicitud = obj.flg_solicitud;
            frm.anho = obj.dsc_anho;
            frm.WindowState = FormWindowState.Maximized;
            frm.ShowDialog();

            BuscarOrdenesCompra();
        }
    }
}