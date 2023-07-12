using BE_Servicios;
using BL_Servicios;
using DevExpress.XtraBars;
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
using UI_Servicios.Formularios.Shared;

namespace UI_Servicios.Formularios.Logistica
{
    public partial class frmListadoRequerimientos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public eUsuario user = new eUsuario();
        blTrabajador blTrab = new blTrabajador();
        blEncrypta blEncryp = new blEncrypta();
        blSistema blSist = new blSistema();
        blGlobales blGlobal = new blGlobales();
        blProveedores blProv = new blProveedores();
        blRequerimiento blReq = new blRequerimiento();
        blOrdenCompra_Servicio blOrdCom = new blOrdenCompra_Servicio();
        TaskScheduler scheduler;
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;

        String codigoCliente = "";
        bool isRunning = false;

        public frmListadoRequerimientos()
        {
            InitializeComponent();
        }

        private void frmListadoRequerimientos_Load(object sender, EventArgs e)
        {
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Inicializar();
        }

        private void Inicializar()
        {
            try
            {
                CargarLookUpEdit();
                
                DateTime date = DateTime.Now;
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
                dtpDesde.EditValue = oPrimerDiaDelMes;
                dtpHasta.EditValue = oUltimoDiaDelMes;
                HabilitarBotones();
                BuscarRequerimientos();
                tcRequerimientos_SelectedPageChanged(tcRequerimientos, new DevExpress.XtraTab.TabPageChangedEventArgs(null, tpReqSolicitados));
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
                blReq.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: user.cod_usuario);
                blReq.CargaCombosLookUp("TipoFecha", lkpTipoFecha, "cod_tipo_fecha", "dsc_tipo_fecha", "", valorDefecto: true);

                List<eFacturaProveedor> list = blProv.ListarEmpresasProveedor<eFacturaProveedor>(11, "", user.cod_usuario);
                if (list.Count >= 1) lkpEmpresa.EditValue = list[0].cod_empresa;

                blReq.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());
                List<eTrabajador.eInfoLaboral_Trabajador> lista = blTrab.ListarOpcionesTrabajador<eTrabajador.eInfoLaboral_Trabajador>(6, lkpEmpresa.EditValue.ToString());
                if (lista.Count == 1) lkpSede.EditValue = lista[0].cod_sede_empresa;

                lkpTipoFecha.ItemIndex = 0;
                lkpSede.EditValue = "00001";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void BuscarRequerimientos()
        {
            try
            {
                List<eRequerimiento> reqSolicitados = blReq.ListarRequerimiento<eRequerimiento>(1, lkpEmpresa.EditValue.ToString(),
                                                                                          lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                                                                                          txtCliente.EditValue == null ? "" : codigoCliente,
                                                                                          lkpArea.EditValue == null ? "" : lkpArea.EditValue.ToString(),
                                                                                          lkpTipoFecha.EditValue.ToString(),
                                                                                          Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                                                                                          Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd")
                                                                                          );

                List<eRequerimiento> reqAprobados = blReq.ListarRequerimiento<eRequerimiento>(3, lkpEmpresa.EditValue.ToString(),
                                                                                          lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                                                                                          txtCliente.EditValue == null ? "" : codigoCliente,
                                                                                          lkpArea.EditValue == null ? "" : lkpArea.EditValue.ToString(),
                                                                                          lkpTipoFecha.EditValue.ToString(),
                                                                                          Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                                                                                          Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd")
                                                                                          );

                List<eRequerimiento> reqAtendidos = blReq.ListarRequerimiento<eRequerimiento>(7, lkpEmpresa.EditValue.ToString(),
                                                                                          lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                                                                                          txtCliente.EditValue == null ? "" : codigoCliente,
                                                                                          lkpArea.EditValue == null ? "" : lkpArea.EditValue.ToString(),
                                                                                          lkpTipoFecha.EditValue.ToString(),
                                                                                          Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                                                                                          Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd")
                                                                                          );

                List<eRequerimiento> reqAnulados = blReq.ListarRequerimiento<eRequerimiento>(9, lkpEmpresa.EditValue.ToString(),
                                                                                          lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                                                                                          txtCliente.EditValue == null ? "" : codigoCliente,
                                                                                          lkpArea.EditValue == null ? "" : lkpArea.EditValue.ToString(),
                                                                                          lkpTipoFecha.EditValue.ToString(),
                                                                                          Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                                                                                          Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd")
                                                                                          );

                bsListadoReqSolicitados.DataSource = reqSolicitados;
                bsListadoReqAprobados.DataSource = reqAprobados;
                bsListadoReqAtendidos.DataSource = reqAtendidos;
                bsListadoReqAnulados.DataSource = reqAnulados;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnNuevoRequerimiento_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMantRequerimientosCompra frm = new frmMantRequerimientosCompra();
            frm.user = user;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.codigoEmpresa = lkpEmpresa.EditValue.ToString();
            frm.WindowState = FormWindowState.Maximized;
            frm.ShowDialog();

            BuscarRequerimientos();
        }

        private void btnNuevoReqServicio_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMantRequerimientosServicio frm = new frmMantRequerimientosServicio();
            frm.user = user;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.codigoEmpresa = lkpEmpresa.EditValue.ToString();
            frm.WindowState = FormWindowState.Maximized;
            frm.ShowDialog();

            BuscarRequerimientos();
        }

        private void btnAnularRequerimiento_ItemClick(object sender, ItemClickEventArgs e)
        {
            string respuesta = "";

            try
            {
                foreach (int nRow in gvReqSolicitados.GetSelectedRows())
                {
                    eRequerimiento obj = gvReqSolicitados.GetRow(nRow) as eRequerimiento;

                    respuesta = blReq.Anular_Requerimiento(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_requerimiento, user.cod_usuario, obj.flg_solicitud, obj.dsc_anho);
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Anulación realizada con éxito.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Anular los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarRequerimientos();
        }

        private void btnEliminarRequerimiento_ItemClick(object sender, ItemClickEventArgs e)
        {

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

                switch (tcRequerimientos.SelectedTabPage.Name)
                {
                    case "tpReqSolicitados": gvReqSolicitados.ExportToXlsx(archivo); break;
                    case "tpReqAprobados": gvReqAprobados.ExportToXlsx(archivo); break;
                    case "tpReqAtendidos": gvReqAtendidos.ExportToXlsx(archivo); break;
                    case "tpReqAnulados": gvReqAnulados.ExportToXlsx(archivo); break;
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
            switch (tcRequerimientos.SelectedTabPage.Name)
            {
                case "tpReqSolicitados": gvReqSolicitados.ShowPrintPreview(); break;
                case "tpReqAprobados": gvReqAprobados.ShowPrintPreview(); break;
                case "tpReqAtendidos": gvReqAtendidos.ShowPrintPreview(); break;
                case "tpReqAnulados": gvReqAnulados.ShowPrintPreview(); break;
            }
        }

        private void btnAprobar_ItemClick(object sender, ItemClickEventArgs e)
        {
            int conProv = 0;
            int sinProv = 0;
            string respuesta = "";

            List <eRequerimiento.eRequerimiento_Detalle> eDetRequerimiento;

            string noAprob = "";

            try
            {
                foreach (int nRow in gvReqSolicitados.GetSelectedRows())
                {
                    eRequerimiento obj = gvReqSolicitados.GetRow(nRow) as eRequerimiento;

                    if (obj.flg_solicitud == "COMPRA")
                    {
                        eDetRequerimiento = blReq.Cargar_Detalle_Requerimiento<eRequerimiento.eRequerimiento_Detalle>(5, obj.cod_empresa, obj.cod_sede_empresa, obj.cod_requerimiento, obj.flg_solicitud.ToString().Substring(0, 1), obj.dsc_anho);

                        for (int i = 0; i < eDetRequerimiento.Count; i++)
                        {
                            if (eDetRequerimiento[i].imp_total > 0)
                            {
                                conProv = conProv + 1;
                            }
                            else
                            {
                                sinProv = sinProv + 1;
                            }
                        }

                        if (sinProv == 0)
                        {
                            respuesta = blReq.Aprobar_Requerimiento(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_requerimiento, user.cod_usuario, obj.flg_solicitud, obj.dsc_anho);
                        }
                        else
                        {
                            noAprob = obj.cod_requerimiento + "," + noAprob;
                        }

                        conProv = 0;
                        sinProv = 0;
                    }
                    else
                    {
                        eDetRequerimiento = blReq.Cargar_Detalle_Requerimiento<eRequerimiento.eRequerimiento_Detalle>(5, obj.cod_empresa, obj.cod_sede_empresa, obj.cod_requerimiento, obj.flg_solicitud.ToString().Substring(0, 1), obj.dsc_anho);

                        for (int i = 0; i < eDetRequerimiento.Count; i++)
                        {
                            if (eDetRequerimiento[i].imp_unitario > 0 && eDetRequerimiento[i].cod_proveedor != "PR000000")
                            {
                                conProv = conProv + 1;
                            }
                            else
                            {
                                sinProv = sinProv + 1;
                            }
                        }

                        if (sinProv == 0)
                        {
                            respuesta = blReq.Aprobar_Requerimiento(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_requerimiento, user.cod_usuario, obj.flg_solicitud, obj.dsc_anho);
                        }
                        else
                        {
                            noAprob = obj.cod_requerimiento + "," + noAprob;
                        }

                        conProv = 0;
                        sinProv = 0;
                    }
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Aprobación realizada con éxito.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (noAprob != "")
                {
                    MessageBox.Show("Los siguientes requerimientos no cuentan con un proveedor y/o un precio asignado " + noAprob, "Requerimientos No Aprobados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Aprobar los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarRequerimientos();
        }

        private void btnDesaprobar_ItemClick(object sender, ItemClickEventArgs e)
        {
            string respuesta = "";

            try
            {
                foreach (int nRow in gvReqAprobados.GetSelectedRows())
                {
                    eRequerimiento obj = gvReqAprobados.GetRow(nRow) as eRequerimiento;

                    respuesta = blReq.Desaprobar_Requerimiento(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_requerimiento, user.cod_usuario, obj.flg_solicitud, obj.dsc_anho);
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Desaprobación realizada con éxito.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Desaprobar los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarRequerimientos();
        }

        private void btnGenerarOC_ItemClick(object sender, ItemClickEventArgs e)
        {
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Generando Orden de Compra", "Cargando...");

            string empresa = "";
            string sede = "";
            string solicitud = "";
            Int32 anho = 1;
            string requerimientos = "";

            try
            {
                foreach (int nRow in gvReqAprobados.GetSelectedRows())
                {
                    eRequerimiento obj = gvReqAprobados.GetRow(nRow) as eRequerimiento;

                    if (obj.flg_solicitud == "COMPRA")
                    {
                        empresa = obj.cod_empresa;
                        sede = obj.cod_sede_empresa;
                        solicitud = obj.flg_solicitud;
                        anho = obj.dsc_anho;
                        requerimientos = obj.cod_requerimiento + "," + requerimientos;
                    }
                }

                string cod_orden_compra = "", flg_solicitud = "";
                Int32 dsc_anho = 1;

                List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle> prodProvReq;

                prodProvReq = blReq.Cargar_Prod_Prov_Requerimientos<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>(6, empresa, sede, requerimientos, solicitud, anho);

                for (int i = 0; i < prodProvReq.Count; i++)
                {
                    if (i == 0)
                    {
                        eOrdenCompra_Servicio eOrdCom = CreaCabecera(prodProvReq[i], "C");

                        cod_orden_compra = eOrdCom.cod_orden_compra_servicio;
                        flg_solicitud = eOrdCom.flg_solicitud;
                        dsc_anho = eOrdCom.dsc_anho;

                        prodProvReq[i].cod_orden_compra_servicio = cod_orden_compra;
                        prodProvReq[i].flg_solicitud = flg_solicitud;
                        prodProvReq[i].dsc_anho = dsc_anho;

                        eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = CrearDetalle(prodProvReq[i]);
                    }
                    else
                    {
                        if (prodProvReq[i].cod_proveedor != prodProvReq[i - 1].cod_proveedor)
                        {
                            eOrdenCompra_Servicio eOrdCom = CreaCabecera(prodProvReq[i], "C");

                            cod_orden_compra = eOrdCom.cod_orden_compra_servicio;
                            flg_solicitud = eOrdCom.flg_solicitud;
                            dsc_anho = eOrdCom.dsc_anho;

                            prodProvReq[i].cod_orden_compra_servicio = cod_orden_compra;
                            prodProvReq[i].flg_solicitud = flg_solicitud;
                            prodProvReq[i].dsc_anho = dsc_anho;

                            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = CrearDetalle(prodProvReq[i]);
                        }
                        else
                        {
                            prodProvReq[i].cod_orden_compra_servicio = cod_orden_compra;
                            prodProvReq[i].flg_solicitud = flg_solicitud;
                            prodProvReq[i].dsc_anho = dsc_anho;

                            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = CrearDetalle(prodProvReq[i]);
                        }
                    }
                }

                string respuesta = blReq.GenerarOC_Requerimiento(empresa, sede, requerimientos, user.cod_usuario, solicitud, anho);

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Órdenes generadas con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Crear Órdenes.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SplashScreenManager.CloseForm();
            BuscarRequerimientos();
        }

        private void btnGenerarOS_ItemClick(object sender, ItemClickEventArgs e)
        {
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Generando Orden de Servicio", "Cargando...");

            string empresa = "";
            string sede = "";
            string solicitud = "";
            Int32 anho = 1;
            string requerimientos = "";

            try
            {
                foreach (int nRow in gvReqAprobados.GetSelectedRows())
                {
                    eRequerimiento obj = gvReqAprobados.GetRow(nRow) as eRequerimiento;

                    if (obj.flg_solicitud == "SERVICIO")
                    {
                        empresa = obj.cod_empresa;
                        sede = obj.cod_sede_empresa;
                        solicitud = obj.flg_solicitud;
                        anho = obj.dsc_anho;
                        requerimientos = obj.cod_requerimiento + "," + requerimientos;
                    }
                }

                string cod_orden_servicio = "", flg_solicitud = "";
                Int32 dsc_anho = 1;

                List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle> prodProvReq;

                prodProvReq = blReq.Cargar_Prod_Prov_Requerimientos<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>(8, empresa, sede, requerimientos, solicitud, anho);

                for (int i = 0; i < prodProvReq.Count; i++)
                {
                    if (i == 0)
                    {
                        eOrdenCompra_Servicio eOrdCom = CreaCabecera(prodProvReq[i], "S");

                        cod_orden_servicio = eOrdCom.cod_orden_compra_servicio;
                        flg_solicitud = eOrdCom.flg_solicitud;
                        dsc_anho = eOrdCom.dsc_anho;

                        prodProvReq[i].cod_orden_compra_servicio = cod_orden_servicio;
                        prodProvReq[i].flg_solicitud = flg_solicitud;
                        prodProvReq[i].dsc_anho = dsc_anho;

                        eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = CrearDetalle(prodProvReq[i]);
                    }
                    else
                    {
                        if (prodProvReq[i].cod_proveedor != prodProvReq[i - 1].cod_proveedor)
                        {
                            eOrdenCompra_Servicio eOrdCom = CreaCabecera(prodProvReq[i], "S");

                            cod_orden_servicio = eOrdCom.cod_orden_compra_servicio;
                            flg_solicitud = eOrdCom.flg_solicitud;
                            dsc_anho = eOrdCom.dsc_anho;

                            prodProvReq[i].cod_orden_compra_servicio = cod_orden_servicio;
                            prodProvReq[i].flg_solicitud = flg_solicitud;
                            prodProvReq[i].dsc_anho = dsc_anho;

                            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = CrearDetalle(prodProvReq[i]);
                        }
                        else
                        {
                            prodProvReq[i].cod_orden_compra_servicio = cod_orden_servicio;
                            prodProvReq[i].flg_solicitud = flg_solicitud;
                            prodProvReq[i].dsc_anho = dsc_anho;

                            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = CrearDetalle(prodProvReq[i]);
                        }
                    }
                }

                string respuesta = blReq.GenerarOC_Requerimiento(empresa, sede, requerimientos, user.cod_usuario, solicitud, anho);

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Órdenes generadas con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Crear Órdenes.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SplashScreenManager.CloseForm();
            BuscarRequerimientos();
        }

        private eOrdenCompra_Servicio CreaCabecera(eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle obj, string solicitud)
        {
            eOrdenCompra_Servicio eOC = new eOrdenCompra_Servicio();

            eOC.cod_empresa = obj.cod_empresa;
            eOC.cod_sede_empresa = obj.cod_sede_empresa;
            eOC.cod_orden_compra_servicio = "";
            eOC.num_cotizacion = "N/A";
            eOC.cod_proveedor = obj.cod_proveedor;
            eOC.dsc_ruc = obj.dsc_ruc;
            eOC.flg_solicitud = solicitud;
            eOC.cod_almacen = "";
            eOC.cod_modalidad_pago = "";
            eOC.dsc_direccion_despacho = "";
            eOC.fch_emision = DateTime.Now;
            eOC.fch_despacho = new DateTime(1999, 01, 01);
            eOC.dsc_terminos_condiciones = "";
            eOC.imp_subtotal = 0;
            eOC.imp_igv = 0;
            eOC.imp_total = 0;
            eOC.dsc_imp_total = "";
            eOC.prc_CV = 0;
            eOC.prc_LI = 0;
            eOC.prc_CB = 0;
            eOC.prc_GG = 0;
            eOC.prc_ADM = 0;
            eOC.prc_OPER = 0;
            eOC.prc_GV = 0;
            eOC.dsc_observaciones = "";

            eOC = blOrdCom.Ins_Act_OrdenCompra_Servicio<eOrdenCompra_Servicio>(eOC, user.cod_usuario);

            return eOC;
        }

        private eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle CrearDetalle(eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle obj)
        {
            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOC = new eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle();

            eDetOC.cod_empresa = obj.cod_empresa;
            eDetOC.cod_sede_empresa = obj.cod_sede_empresa;
            eDetOC.cod_orden_compra_servicio = obj.cod_orden_compra_servicio;
            eDetOC.flg_solicitud = obj.flg_solicitud;
            eDetOC.dsc_anho = obj.dsc_anho;
            eDetOC.num_item = obj.num_item;
            eDetOC.cod_requerimiento = obj.cod_requerimiento;
            eDetOC.cod_proveedor = obj.cod_proveedor;
            eDetOC.dsc_ruc = obj.dsc_ruc;
            eDetOC.cod_tipo_servicio = obj.cod_tipo_servicio;
            eDetOC.cod_subtipo_servicio = obj.cod_subtipo_servicio;
            eDetOC.cod_producto = obj.cod_producto;
            eDetOC.dsc_servicio = "";
            eDetOC.cod_unidad_medida = obj.cod_unidad_medida;
            eDetOC.num_cantidad = obj.num_cantidad;
            eDetOC.imp_unitario = obj.imp_unitario;
            eDetOC.imp_total_det = obj.imp_total_det;

            eDetOC = blOrdCom.Ins_Act_Detalle_OrdenCompra_Servicio<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>(eDetOC, user.cod_usuario);

            return eDetOC;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarRequerimientos();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            Busqueda("", "Cliente");
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
                    txtCliente.Text = frm.descripcion;
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
        }

        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            blReq.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());
        }

        private void lkpSede_EditValueChanged(object sender, EventArgs e)
        {
            blReq.CargaCombosLookUp("Areas", lkpArea, "cod_area", "dsc_area", "", valorDefecto: true, cod_empresa : lkpEmpresa.EditValue.ToString(), cod_sede_empresa: lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString());
        }

        private void frmListadoRequerimientos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) BuscarRequerimientos();
        }

        private void tcRequerimientos_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            List<eVentana> listPerfil = blSist.ListarPerfilesUsuario<eVentana>(4, user.cod_usuario);
            eVentana oPerfilAdm = listPerfil.Find(x => x.cod_perfil == 28 || x.cod_perfil == 5);
            eVentana oPerfilLog = listPerfil.Find(x => x.cod_perfil == 28 || x.cod_perfil == 5 || x.cod_perfil == 29 || x.cod_perfil == 31);
            eVentana oPerfilLogOS = listPerfil.Find(x => x.cod_perfil == 28 || x.cod_perfil == 5 || x.cod_perfil == 38 || x.cod_perfil == 40);

            if (tcRequerimientos.SelectedTabPage == tpReqSolicitados)
            {
                btnAnularRequerimiento.Enabled = oPerfilAdm != null ? true : false;
                btnAprobar.Enabled = oPerfilAdm != null ? true : false;
                btnDesaprobar.Enabled = false;
                btnGenerarOC.Enabled = false;
                btnGenerarOS.Enabled = false;
            }
            else if(tcRequerimientos.SelectedTabPage == tpReqAprobados)
            {
                btnAnularRequerimiento.Enabled = false;
                btnAprobar.Enabled = false;
                btnDesaprobar.Enabled = oPerfilAdm != null ? true : false;
                btnGenerarOC.Enabled = oPerfilLog != null ? true : false;
                btnGenerarOS.Enabled = oPerfilLogOS != null ? true : false;
            }
            else
            {
                btnAnularRequerimiento.Enabled = false;
                btnAprobar.Enabled = false;
                btnDesaprobar.Enabled = false;
                btnGenerarOC.Enabled = false;
                btnGenerarOS.Enabled = false;
            }
        }

        private void gvReqSolicitados_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarReq("tpReqSolicitados");
        }

        private void gvReqSolicitados_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvReqSolicitados_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvReqSolicitados_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvReqSolicitados_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvReqAprobados_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarReq("tpReqAprobados");
        }

        private void gvReqAprobados_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (isRunning) return;

            isRunning = true;

            GridView View = sender as GridView;

            if (e.Action == CollectionChangeAction.Add && (string)this.gvReqAprobados.GetRowCellValue(this.gvReqAprobados.FocusedRowHandle, "cod_estado_requerimiento") == "ORDEN GENERADA")
            {
                if (blReq.ValidarOC_Requerimiento((string)this.gvReqAprobados.GetRowCellValue(this.gvReqAprobados.FocusedRowHandle, "cod_empresa"), (string)this.gvReqAprobados.GetRowCellValue(this.gvReqAprobados.FocusedRowHandle, "cod_sede_empresa"), (string)this.gvReqAprobados.GetRowCellValue(this.gvReqAprobados.FocusedRowHandle, "cod_requerimiento")) != "LIBERADA")
                {
                    View.SelectRow(e.ControllerRow);
                }
            }

            if (e.Action == CollectionChangeAction.Add && (string)this.gvReqAprobados.GetRowCellValue(this.gvReqAprobados.FocusedRowHandle, "cod_estado_requerimiento") == "ATENDIDO")
            {
                View.UnselectRow(e.ControllerRow);
            }

            if (e.Action == CollectionChangeAction.Refresh && View.SelectedRowsCount > 0)
            {
                View.BeginSelection();

                foreach (int Row in View.GetSelectedRows())
                {
                    if ((string)this.gvReqAprobados.GetRowCellValue(Row, "cod_estado_requerimiento") == "ORDEN GENERADA")
                    {
                        if (blReq.ValidarOC_Requerimiento((string)this.gvReqAprobados.GetRowCellValue(Row, "cod_empresa"), (string)this.gvReqAprobados.GetRowCellValue(Row, "cod_sede_empresa"), (string)this.gvReqAprobados.GetRowCellValue(Row, "cod_requerimiento")) != "LIBERADA")
                        {
                            View.UnselectRow(Row);
                        }
                    }

                    if ((string)this.gvReqAprobados.GetRowCellValue(Row, "cod_estado_requerimiento") == "ATENDIDO")
                    {
                        View.UnselectRow(Row);
                    }
                }

                View.EndSelection();
            }

            isRunning = false;
        }

        private void gvReqAprobados_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvReqAprobados_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvReqAprobados_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvReqAprobados_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvReqAtendidos_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarReq("tpReqAtendidos");
        }

        private void gvReqAtendidos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvReqAtendidos_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvReqAtendidos_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void gvReqAtendidos_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvReqAnulados_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarReq("tpReqAnulados");
        }

        private void gvReqAnulados_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvReqAnulados_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvReqAnulados_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void gvReqAnulados_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void MostrarReq(string tabName)
        {
            eRequerimiento obj = new eRequerimiento();

            List<eVentana> listPerfil = blSist.ListarPerfilesUsuario<eVentana>(4, user.cod_usuario);
            eVentana oPerfilLog = listPerfil.Find(x => x.cod_perfil == 28 || x.cod_perfil == 5 || x.cod_perfil == 26);

            switch (tabName)
            {
                case "tpReqSolicitados":
                    obj = gvReqSolicitados.GetFocusedRow() as eRequerimiento;
                    break;
                case "tpReqAprobados":
                    obj = gvReqAprobados.GetFocusedRow() as eRequerimiento;
                    break;
                case "tpReqAtendidos":
                    obj = gvReqAtendidos.GetFocusedRow() as eRequerimiento;
                    break;
                case "tpReqAnulados":
                    obj = gvReqAnulados.GetFocusedRow() as eRequerimiento;
                    break;
            }
            
            if (obj.flg_solicitud == "COMPRA")
            {
                frmMantRequerimientosCompra frm = new frmMantRequerimientosCompra();
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.empresa = obj.cod_empresa;
                frm.sede = obj.cod_sede_empresa;
                frm.requerimiento = obj.cod_requerimiento;
                frm.solicitud = obj.flg_solicitud.ToString().Substring(0, 1);
                frm.anho = obj.dsc_anho;
                frm.WindowState = FormWindowState.Maximized;

                if (tabName == "tpReqSolicitados")
                {
                    frm.accion = oPerfilLog != null ? RequerimientoCompra.Editar : RequerimientoCompra.Vista;
                }
                else
                {
                    frm.accion = RequerimientoCompra.Vista;
                }

                frm.ShowDialog();
            } 
            else
            {
                frmMantRequerimientosServicio frm = new frmMantRequerimientosServicio();
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.empresa = obj.cod_empresa;
                frm.sede = obj.cod_sede_empresa;
                frm.requerimiento = obj.cod_requerimiento;
                frm.solicitud = obj.flg_solicitud.ToString().Substring(0, 1);
                frm.anho = obj.dsc_anho;
                frm.WindowState = FormWindowState.Maximized;

                if (tabName == "tpReqSolicitados")
                {
                    frm.accion = oPerfilLog != null ? RequerimientoServicio.Editar : RequerimientoServicio.Vista;
                }
                else
                {
                    frm.accion = RequerimientoServicio.Vista;
                }

                frm.ShowDialog();
            }

            BuscarRequerimientos();
        }
    }
}