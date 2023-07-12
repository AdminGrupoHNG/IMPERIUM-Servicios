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
using DevExpress.XtraPivotGrid;
using System.Configuration;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using DevExpress.Utils.Drawing;
using System.Globalization;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraEditors;

namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    public partial class frmResumenCuentasPagarSemanal : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        blProveedores blProv = new blProveedores();
        blEncrypta blEncryp = new blEncrypta();
        TaskScheduler scheduler;
        Timer oTimerLoadMtto;
        public blFactura blFact = new blFactura();
        public List<eFacturaProveedor> ListResumen;
        public List<eFacturaProveedor> ListResumenTipoDoc;
        public List<eFacturaProveedor> ListResumenProveedor;
        public List<eFacturaProveedor> ListDetalleFactura;
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        Image ImgPdf = DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttopdf_16x16.png");
        string Fecha = "", FechaFinal = "", cod_moneda = "";

        public frmResumenCuentasPagarSemanal()
        {
            InitializeComponent();
            oTimerLoadMtto = new Timer();
            oTimerLoadMtto.Interval = 500;
            oTimerLoadMtto.Tick += oTimerLoadMtto_Tick;
        }

        private void oTimerLoadMtto_Tick(object sender, EventArgs e)
        {
            oTimerLoadMtto.Stop();
            Inicializar();
        }
        
        private void Inicializar()
        {
            try
            {
                //splitContainerControl2.PanelVisibility = SplitPanelVisibility.Panel1;
                CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
                
                dtFechaInicio.EditValue = GetFirstDayOfWeek(DateTime.Today, defaultCultureInfo).AddDays(-2);
                dtFechaFin.EditValue = Obtener_UltimoViernes(Convert.ToDateTime(dtFechaInicio.EditValue)); //.AddMonths(1);
                dtFechaResumen.EditValue = DateTime.Today;
                List<eFacturaProveedor> listaMes = blFact.CombosEnGridControl<eFacturaProveedor>("Meses");
                rlkpOrden.DataSource = listaMes;
                rlkpOrdenTS.DataSource = listaMes;
                rlkpOrdenTD.DataSource = listaMes;

                blFact.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: user.cod_usuario);
                List<eFacturaProveedor> list = blProv.ListarEmpresasProveedor<eFacturaProveedor>(11, "", user.cod_usuario);
                if (list.Count == 1) lkpEmpresa.EditValue = list[0].cod_empresa;

                LlenarReportes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }

        public static DateTime Obtener_UltimoViernes(DateTime dayInWeek)
        {
            int cont = 0;
            DateTime dia = dayInWeek.Date;
            for (int x = cont; cont <= 4; x++)
            {
                if (dia.DayOfWeek == DayOfWeek.Friday)
                {
                    cont = cont + 1;
                }
                if (cont == 4) { cont = cont + 1; continue; }
                dia = dia.AddDays(1);
            }
            if (cont == 5) { return dia.AddDays(1); } else { return dayInWeek.AddDays(30); }
        }

        private void CargarResumenporCuentas()
        {
            //List<eFacturaProveedor> listaMontosDocsSOLES = blFact.FiltroFactura<eFacturaProveedor>(grdbTipoMoneda.SelectedIndex == 0 ? 12 : 13, FechaInicio: Convert.ToDateTime(dtFechaResumen.EditValue).ToString("yyyyMMdd"), Anho: Convert.ToDateTime(dtAnhoBusqueda.EditValue).Year, cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString());
            //List<eFacturaProveedor> listaMontosDocsSOLES = blFact.FiltroFactura<eFacturaProveedor>(12, FechaInicio: Convert.ToDateTime(dtFechaResumen.EditValue).ToString("yyyyMMdd"), Anho: Convert.ToDateTime(dtFechaInicio.EditValue).Year, cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString());
            List<eFacturaProveedor> listaMontosDocsSOLES = blFact.FiltroFactura<eFacturaProveedor>(43, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "SOL");
            tlbiCuentaPagarSOLSemana1.Elements[0].Text = listaMontosDocsSOLES.Count < 1 ? "" : "POR VENCER AL " + listaMontosDocsSOLES[0].dsc_dia;
            tlbiCuentaPagarSOLSemana1.Elements[1].Text = listaMontosDocsSOLES.Count < 1 ? "S/ 0.00" : "S/ " + listaMontosDocsSOLES[0].imp_pagado.ToString("0,0.00", CultureInfo.InvariantCulture);
            tlbiCuentaPagarSOLSemana2.Elements[0].Text = listaMontosDocsSOLES.Count < 2 ? "" : "POR VENCER AL " + listaMontosDocsSOLES[1].dsc_dia;
            tlbiCuentaPagarSOLSemana2.Elements[1].Text = listaMontosDocsSOLES.Count < 2 ? "S/ 0.00" : "S/ " + listaMontosDocsSOLES[1].imp_pagado.ToString("0,0.00", CultureInfo.InvariantCulture);
            tlbiCuentaPagarSOLSemana3.Elements[0].Text = listaMontosDocsSOLES.Count < 3 ? "" : "POR VENCER AL " + listaMontosDocsSOLES[2].dsc_dia;
            tlbiCuentaPagarSOLSemana3.Elements[1].Text = listaMontosDocsSOLES.Count < 3 ? "S/ 0.00" : "S/ " + listaMontosDocsSOLES[2].imp_pagado.ToString("0,0.00", CultureInfo.InvariantCulture);
            tlbiCuentaPagarSOLSemana4.Elements[0].Text = listaMontosDocsSOLES.Count < 4 ? "" : "POR VENCER AL " + listaMontosDocsSOLES[3].dsc_dia;
            tlbiCuentaPagarSOLSemana4.Elements[1].Text = listaMontosDocsSOLES.Count < 4 ? "S/ 0.00" : "S/ " + listaMontosDocsSOLES[3].imp_pagado.ToString("0,0.00", CultureInfo.InvariantCulture);

            //List<eFacturaProveedor> listaMontosDocsDOLARES = blFact.FiltroFactura<eFacturaProveedor>(13, FechaInicio: Convert.ToDateTime(dtFechaResumen.EditValue).ToString("yyyyMMdd"), Anho: Convert.ToDateTime(dtFechaInicio.EditValue).Year, cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString());
            List<eFacturaProveedor> listaMontosDocsDOLARES = blFact.FiltroFactura<eFacturaProveedor>(43, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "DOL");
            tlbiCuentaPagarDOLSemana1.Elements[0].Text = listaMontosDocsDOLARES.Count < 1 ? "" : "POR VENCER AL " + listaMontosDocsDOLARES[0].dsc_dia;
            tlbiCuentaPagarDOLSemana1.Elements[1].Text = listaMontosDocsDOLARES.Count < 1 ? "$ 0.00" : "$ " + listaMontosDocsDOLARES[0].imp_pagado.ToString("0,0.00", CultureInfo.InvariantCulture);
            tlbiCuentaPagarDOLSemana2.Elements[0].Text = listaMontosDocsDOLARES.Count < 2 ? "" : "POR VENCER AL " + listaMontosDocsDOLARES[1].dsc_dia;
            tlbiCuentaPagarDOLSemana2.Elements[1].Text = listaMontosDocsDOLARES.Count < 2 ? "$ 0.00" : "$ " + listaMontosDocsDOLARES[1].imp_pagado.ToString("0,0.00", CultureInfo.InvariantCulture);
            tlbiCuentaPagarDOLSemana3.Elements[0].Text = listaMontosDocsDOLARES.Count < 3 ? "" : "POR VENCER AL " + listaMontosDocsDOLARES[2].dsc_dia;
            tlbiCuentaPagarDOLSemana3.Elements[1].Text = listaMontosDocsDOLARES.Count < 3 ? "$ 0.00" : "$ " + listaMontosDocsDOLARES[2].imp_pagado.ToString("0,0.00", CultureInfo.InvariantCulture);
            tlbiCuentaPagarDOLSemana4.Elements[0].Text = listaMontosDocsDOLARES.Count < 4 ? "" : "POR VENCER AL " + listaMontosDocsDOLARES[3].dsc_dia;
            tlbiCuentaPagarDOLSemana4.Elements[1].Text = listaMontosDocsDOLARES.Count < 4 ? "$ 0.00" : "$ " + listaMontosDocsDOLARES[3].imp_pagado.ToString("0,0.00", CultureInfo.InvariantCulture);
        }

        private void CargarTipoDocumento(int Mes = 0)
        {
            try
            {
                List<eFacturaProveedor.eFacturaProveedor_Distribucion> listaDocumentos = blFact.FiltroFactura<eFacturaProveedor.eFacturaProveedor_Distribucion>(grdbTipoMoneda.SelectedIndex == 0 ? 21 : 28, Anho: Convert.ToDateTime(dtFechaInicio.EditValue).Year, Mes: Mes, flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString());
                bsResumenPorTipoDoc.DataSource = listaDocumentos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void CargarTipoServicio(int Mes = 0)
        {
            try
            {
                List<eFacturaProveedor.eFacturaProveedor_Distribucion> listaServicios = blFact.FiltroFactura<eFacturaProveedor.eFacturaProveedor_Distribucion>(grdbTipoMoneda.SelectedIndex == 0 ? 26 : 27, Anho: Convert.ToDateTime(dtFechaInicio.EditValue).Year, Mes: Mes, flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString());
                //List<eFacturaProveedor.eFacturaProveedor_Distribucion> listaServicios = blFact.FiltroFactura<eFacturaProveedor.eFacturaProveedor_Distribucion>(1, FechaInicio: "20210101", FechaFin: "20211231");
                bsResumenPorTipoServ.DataSource = listaServicios;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargarTipoDocumentoMes(string FechaInicio = "", string FechaFin = "", string cod_moneda = "")
        {
            try
            {
                //List<eFacturaProveedor> listaResumenTipoDocumento = blFact.FiltroFactura<eFacturaProveedor>(grdbTipoMoneda.SelectedIndex == 0 ? 33 : 34, FechaInicio: Convert.ToDateTime(dtFechaResumen.EditValue).ToString("yyyyMMdd"), Anho: Convert.ToDateTime(dtAnhoBusqueda.EditValue).Year, Mes: Mes);
                List<eFacturaProveedor> listaResumenTipoDocumento = blFact.FiltroFactura<eFacturaProveedor>(41, FechaInicio: FechaInicio, FechaFin: FechaFin, cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: cod_moneda);
                bsResumenTipoDocumento.DataSource = listaResumenTipoDocumento;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void CargarTipoServicioMes(string FechaInicio = "", string FechaFin = "", string cod_moneda = "")
        {
            try
            {
                //List<eFacturaProveedor> listaResumenTipoServicio = blFact.FiltroFactura<eFacturaProveedor>(grdbTipoMoneda.SelectedIndex == 0 ? 31 : 32, FechaInicio: Convert.ToDateTime(dtFechaResumen.EditValue).ToString("yyyyMMdd"), Anho: Convert.ToDateTime(dtAnhoBusqueda.EditValue).Year, Mes: Mes);
                List<eFacturaProveedor> listaResumenTipoServicio = blFact.FiltroFactura<eFacturaProveedor>(42, FechaInicio: FechaInicio, FechaFin: FechaFin, cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: cod_moneda);
                bsResumenTipoServicio.DataSource = listaResumenTipoServicio;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void frmResumenCuentasPagarSemanal_Load(object sender, EventArgs e)
        {
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            oTimerLoadMtto.Start();
        }
        
        private void CargarResumenporMeses()
        {
            try
            {
                List<eFacturaProveedor> listaSemanaSOL1 = blFact.FiltroFactura<eFacturaProveedor>(38, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "SOL", Semana: 1);
                List<eFacturaProveedor> listaSemanaSOL2 = blFact.FiltroFactura<eFacturaProveedor>(38, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "SOL", Semana: 2);
                List<eFacturaProveedor> listaSemanaSOL3 = blFact.FiltroFactura<eFacturaProveedor>(38, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "SOL", Semana: 3);
                List<eFacturaProveedor> listaSemanaSOL4 = blFact.FiltroFactura<eFacturaProveedor>(38, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "SOL", Semana: 4);
                bsResumenSemanaSOL1.DataSource = listaSemanaSOL1; bsResumenSemanaSOL2.DataSource = listaSemanaSOL2; bsResumenSemanaSOL3.DataSource = listaSemanaSOL3; bsResumenSemanaSOL4.DataSource = listaSemanaSOL4;

                List<eFacturaProveedor> listaSemanaDOL1 = blFact.FiltroFactura<eFacturaProveedor>(38, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "DOL", Semana: 1);
                List<eFacturaProveedor> listaSemanaDOL2 = blFact.FiltroFactura<eFacturaProveedor>(38, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "DOL", Semana: 2);
                List<eFacturaProveedor> listaSemanaDOL3 = blFact.FiltroFactura<eFacturaProveedor>(38, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "DOL", Semana: 3);
                List<eFacturaProveedor> listaSemanaDOL4 = blFact.FiltroFactura<eFacturaProveedor>(38, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "DOL", Semana: 4);
                bsResumenSemanaDOL1.DataSource = listaSemanaDOL1; bsResumenSemanaDOL2.DataSource = listaSemanaDOL2; bsResumenSemanaDOL3.DataSource = listaSemanaDOL3; bsResumenSemanaDOL4.DataSource = listaSemanaDOL4;


                //List<eFacturaProveedor> listaMesesTotalSOL = blFact.FiltroFactura<eFacturaProveedor>(38, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "SOL");
                //List<eFacturaProveedor> listaMesesPagadoSOL = blFact.FiltroFactura<eFacturaProveedor>(43, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "SOL");
                //bsResumenTOTALMesesSOL.DataSource = listaMesesTotalSOL; bsResumenPagadoMesesSOL.DataSource = listaMesesPagadoSOL;

                //List<eFacturaProveedor> listaMesesTotalDOL = blFact.FiltroFactura<eFacturaProveedor>(38, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "DOL");
                //List<eFacturaProveedor> listaMesesPagadoDOL = blFact.FiltroFactura<eFacturaProveedor>(43, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "DOL");
                //bsResumenTOTALMesesDOL.DataSource = listaMesesTotalDOL; bsResumenPagadoMesesDOL.DataSource = listaMesesPagadoDOL;

                //List<eFacturaProveedor> listaMesesTotalSUMASOL = blFact.FiltroFactura<eFacturaProveedor>(39, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "SOL");
                //List<eFacturaProveedor> listaMesesPagadoSUMASOL = blFact.FiltroFactura<eFacturaProveedor>(44, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "SOL");
                //List<eFacturaProveedor> listaMesesTotalSUMADOL = blFact.FiltroFactura<eFacturaProveedor>(39, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "DOL");
                //List<eFacturaProveedor> listaMesesPagadoSUMADOL = blFact.FiltroFactura<eFacturaProveedor>(44, FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), flg_IGV: grdbFlagIGV.SelectedIndex == 0 ? "SI" : "NO", cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: "DOL");
                //bsResumenTOTALMesesSUMASOL.DataSource = listaMesesTotalSUMASOL; bsResumenPagadoMesesSUMASOL.DataSource = listaMesesPagadoSUMASOL;
                //bsResumenTOTALMesesSUMADOL.DataSource = listaMesesTotalSUMADOL; bsResumenPagadoMesesSUMADOL.DataSource = listaMesesPagadoSUMADOL;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pvtResumenTipoDoc_Click(object sender, EventArgs e)
        {
            try
            {
                //CargarResumenProveedoresxTipoDocumento();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarResumenProveedores(string FechaInicio = "", string FechaFin = "", string cod_tipo_moneda = "")
        {
            try
            {
                cod_moneda = cod_tipo_moneda;
                if (xtabVistaProv.PageVisible == true)
                {
                    //List<eFacturaProveedor> listaResumenProveedor = blFact.FiltroFactura<eFacturaProveedor>(grdbTipoMoneda.SelectedIndex == 0 ? 18 : 19, FechaInicio: Convert.ToDateTime(dtFechaResumen.EditValue).ToString("yyyyMMdd"), Anho: Convert.ToDateTime(dtAnhoBusqueda.EditValue).Year, Mes: Mes, cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString());
                    List<eFacturaProveedor> listaResumenProveedor = blFact.FiltroFactura<eFacturaProveedor>(40, FechaInicio: FechaInicio, FechaFin: FechaFin, cod_empresa: lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(), cod_moneda: cod_tipo_moneda);
                    bsResumenProveedor.DataSource = listaResumenProveedor;
                }

                if (xtabVistaProvMes.PageVisible == true)
                {
                    List<eFacturaProveedor.eFacturaProveedor_VistaProveedor> listaResumenProveedorMeses = blFact.Obtener_ResumenProveedorMeses<eFacturaProveedor.eFacturaProveedor_VistaProveedor>(Convert.ToDateTime(dtFechaInicio.EditValue).Year, grdbTipoMoneda.SelectedIndex, lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString());
                    bsResumenProveedorMeses.DataSource = listaResumenProveedorMeses;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //xtabVistaProv.PageVisible = false; xtabVistaProvMes.PageVisible = true;
                //layoutControlItem14.Visibility = LayoutVisibility.Always;
                //layoutControlItem17.Visibility = LayoutVisibility.Never;
                //simpleLabelItem10.Visibility = LayoutVisibility.Never;
                //layoutControlItem8.Visibility = LayoutVisibility.Always;
                //layoutControlItem18.Visibility = LayoutVisibility.Never;
                //simpleLabelItem11.Visibility = LayoutVisibility.Never;
                LlenarReportes();
                //////xtraTabControl1.SelectedTabPage = xtabVistaProvMes;
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LlenarReportes()
        {
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reportes", "Cargando...");
            CargarResumenporCuentas();
            CargarTipoDocumento();
            CargarTipoServicio();
            CargarResumenporMeses();
            CargarResumenProveedores();
            SplashScreenManager.CloseForm();
        }
        private void chartResumenCxPMesSOLES_SelectedItemsChanged(object sender, DevExpress.XtraCharts.SelectedItemsChangedEventArgs e)
        {
            try
            {
                layoutControlItem3.Visibility = LayoutVisibility.Always;
                layoutControlItem20.Visibility = LayoutVisibility.Never;
                splitContainerControl2.PanelVisibility = SplitPanelVisibility.Both;
                foreach (eFacturaProveedor obj in chartResumenCxPMesSOLES.SelectedItems)
                {
                    if (obj == null || obj.fch_vencimiento.ToString().Contains("1/01/0001")) continue;
                    blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reportes", "Cargando...");
                    xtabVistaProv.PageVisible = true; xtabVistaProvMes.PageVisible = false;
                    layoutControlItem14.Visibility = LayoutVisibility.Never;
                    layoutControlItem17.Visibility = LayoutVisibility.Always;
                    simpleLabelItem10.Visibility = LayoutVisibility.Always;
                    layoutControlItem8.Visibility = LayoutVisibility.Never;
                    layoutControlItem18.Visibility = LayoutVisibility.Always;
                    simpleLabelItem11.Visibility = LayoutVisibility.Always;
                    if ( Convert.ToInt32(obj.fch_vencimiento.DayOfWeek) == 5)
                    {
                        Fecha = Convert.ToDateTime(obj.fch_vencimiento).AddDays(-6).ToString("yyyyMMdd");
                    }
                    else
                    {
                        Fecha = Convert.ToDateTime(obj.fch_vencimiento).ToString("yyyyMMdd");
                    }
                    FechaFinal = Convert.ToDateTime(obj.fch_vencimiento).ToString("yyyyMMdd");
                    CargarResumenProveedores(Fecha, FechaFinal, "SOL");
                    CargarTipoDocumentoMes(Fecha, FechaFinal, "SOL");
                    CargarTipoServicioMes(Fecha, FechaFinal, "SOL");
                    //CargarTipoDocumento(obj.nOrden);
                    //CargarTipoServicio(obj.nOrden);
                    //xtraTabControl1.SelectedTabPage = xtabVistaProv;
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chartResumenCxPMesSOLES_SelectedItemsChanging(object sender, DevExpress.XtraCharts.SelectedItemsChangingEventArgs e)
        {
            string result = "";
            
        }

        private void chartResumenCxPMesDOLARES_SelectedItemsChanged(object sender, DevExpress.XtraCharts.SelectedItemsChangedEventArgs e)
        {
            try
            {
                layoutControlItem20.Visibility = LayoutVisibility.Always;
                layoutControlItem3.Visibility = LayoutVisibility.Never;
                splitContainerControl2.PanelVisibility = SplitPanelVisibility.Both;
                foreach (eFacturaProveedor obj in chartResumenCxPMesDOLARES.SelectedItems)
                {
                    if (obj == null || obj.fch_vencimiento.ToString().Contains("1/01/0001")) continue;
                    blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reportes", "Cargando...");
                    xtabVistaProv.PageVisible = true; xtabVistaProvMes.PageVisible = false;
                    layoutControlItem14.Visibility = LayoutVisibility.Never;
                    layoutControlItem17.Visibility = LayoutVisibility.Always;
                    simpleLabelItem10.Visibility = LayoutVisibility.Always;
                    layoutControlItem8.Visibility = LayoutVisibility.Never;
                    layoutControlItem18.Visibility = LayoutVisibility.Always;
                    simpleLabelItem11.Visibility = LayoutVisibility.Always;
                    //Fecha = Convert.ToDateTime(obj.fch_vencimiento).ToString("yyyyMMdd");
                    if (Convert.ToInt32(obj.fch_vencimiento.DayOfWeek) == 5)
                    {
                        Fecha = Convert.ToDateTime(obj.fch_vencimiento).AddDays(-6).ToString("yyyyMMdd");
                    }
                    else
                    {
                        Fecha = Convert.ToDateTime(obj.fch_vencimiento).ToString("yyyyMMdd");
                    }
                    FechaFinal = Convert.ToDateTime(obj.fch_vencimiento).ToString("yyyyMMdd");
                    CargarResumenProveedores(Fecha, FechaFinal, "DOL");
                    CargarTipoDocumentoMes(Fecha, FechaFinal, "DOL");
                    CargarTipoServicioMes(Fecha, FechaFinal, "DOL");
                    //CargarTipoDocumento(obj.nOrden);
                    //CargarTipoServicio(obj.nOrden);
                    //xtraTabControl1.SelectedTabPage = xtabVistaProv;
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                string archivo = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\ResumenCuentaporPagar" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                bgvResumenVentasProveedor.ExportToXlsx(archivo);
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
        private void gvDetalleFactura_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    //eFacturaProveedor obj = gvDetalleFactura.GetRow(e.RowHandle) as eFacturaProveedor;
                    //if (e.Column.FieldName == "NombreArchivo" && obj.FlgExistePDF == true)
                    //{
                    //    e.Handled = true; e.Graphics.DrawImage(ImgPdf, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmResumenCuentasPagarSemanal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) LlenarReportes();
        }

        private void bgvResumenVentasProveedor_CustomDrawBandHeader(object sender, DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnasBandHeader(e);
        }

        private void grdbFlagIGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarResumenporMeses();
            CargarTipoServicio();
            CargarTipoDocumento();
        }

        private void bgvResumenProveedorMeses_CustomDrawBandHeader(object sender, DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnasBandHeader(e);
        }

        private void bgvResumenProveedorMeses_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void bgvResumenProveedorMeses_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void pvtResumenTipoServ_CustomDrawFieldHeader(object sender, PivotCustomDrawFieldHeaderEventArgs e)
        {
            try
            {
                if (e.Field == null) { return; }
                Rectangle rect = e.Bounds;
                ControlPaint.DrawBorder3D(e.Graphics, e.Bounds);
                rect.Inflate(-1, -1);

                SolidBrush myBrush1 = new SolidBrush(System.Drawing.Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]));
                e.Graphics.FillRectangle(myBrush1, rect);
                e.Appearance.DrawString(e.GraphicsCache, e.Info.Caption, e.Info.CaptionRect);
                foreach (DrawElementInfo info in e.Info.InnerElements)
                {
                    if (!info.Visible) continue;
                    ObjectPainter.DrawObject(e.GraphicsCache, info.ElementPainter, info.ElementInfo);
                }
                e.Appearance.ForeColor = System.Drawing.Color.White;

                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void bgvResumenProveedorMeses_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                eFacturaProveedor obj = bgvResumenProveedorMeses.GetFocusedRow() as eFacturaProveedor;
                if (obj == null) { return; }

                frmFacturasDetalle frm = new frmFacturasDetalle();
                if (Application.OpenForms["frmFacturasDetalle"] != null)
                {
                    Application.OpenForms["frmFacturasDetalle"].Activate();
                }
                else
                {
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorFocus = colorFocus;
                    frm.colorEventRow = colorEventRow;
                    frm.cod_proveedor = obj.cod_proveedor;
                    frm.cod_moneda = cod_moneda;
                    frm.FechaInicio = new DateTime(DateTime.Today.Year, 1, 1).ToString("yyyyMMdd");
                    frm.FechaFin = new DateTime(DateTime.Today.Year, 12, 31).ToString("yyyyMMdd");
                    frm.cod_tipo_fecha = "02"; 
                    frm.user.cod_usuario = user.cod_usuario;
                    frm.ShowDialog();
                }
            }
        }

        private void bgvResumenVentasProveedor_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                eFacturaProveedor obj = bgvResumenVentasProveedor.GetFocusedRow() as eFacturaProveedor;
                if (obj == null) { return; }

                frmFacturasDetalle frm = new frmFacturasDetalle();
                if (Application.OpenForms["frmFacturasDetalle"] != null)
                {
                    Application.OpenForms["frmFacturasDetalle"].Activate();
                }
                else
                {
                    frm.colorVerde = colorVerde;
                    frm.colorPlomo = colorPlomo;
                    frm.colorFocus = colorFocus;
                    frm.colorEventRow = colorEventRow;
                    frm.cod_proveedor = obj.cod_proveedor;
                    frm.cod_moneda = cod_moneda;
                    frm.FechaInicio = Fecha;
                    frm.FechaFin = FechaFinal;
                    frm.cod_tipo_fecha = "02";
                    frm.user.cod_usuario = user.cod_usuario;
                    frm.ShowDialog();
                }
            }
        }

        private void pvtResumenProveedor_CustomDrawFieldHeader(object sender, PivotCustomDrawFieldHeaderEventArgs e)
        {
            try
            {
                if (e.Field == null) { return; }
                Rectangle rect = e.Bounds;
                ControlPaint.DrawBorder3D(e.Graphics, e.Bounds);
                rect.Inflate(-1, -1);

                SolidBrush myBrush1 = new SolidBrush(System.Drawing.Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]));
                e.Graphics.FillRectangle(myBrush1, rect);
                e.Appearance.DrawString(e.GraphicsCache, e.Info.Caption, e.Info.CaptionRect);
                foreach (DrawElementInfo info in e.Info.InnerElements)
                {
                    if (!info.Visible) continue;
                    ObjectPainter.DrawObject(e.GraphicsCache, info.ElementPainter, info.ElementInfo);
                }
                e.Appearance.ForeColor = System.Drawing.Color.White;

                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void pvtResumenProveedor_CellClick(object sender, PivotCellEventArgs e)
        {
            
        }

        private void pvtResumenProveedor_CellDoubleClick(object sender, PivotCellEventArgs e)
        {
            //if (e.ColumnField.FieldName == "dsc_meses")
            //{
            //    object obj = pvtResumenProveedor.GetFieldValue(e.ColumnField, pvtResumenProveedor.Cells.FocusedCell.Y);
            //    string codigo = obj.ToString();
            //    if (obj == null) { return; }

            //    frmFacturasDetalle frm = new frmFacturasDetalle();
            //    if (Application.OpenForms["frmFacturasDetalle"] != null)
            //    {
            //        Application.OpenForms["frmFacturasDetalle"].Activate();
            //    }
            //    else
            //    {
            //        frm.colorVerde = colorVerde;
            //        frm.colorPlomo = colorPlomo;
            //        frm.colorFocus = colorFocus;
            //        frm.colorEventRow = colorEventRow;
            //        frm.cod_proveedor = codigo;
            //        frm.FechaInicio = new DateTime(DateTime.Today.Year, Mes, 1);
            //        DateTime oPrimerDiaDelMes = new DateTime(DateTime.Today.Year, Mes, 1);
            //        DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            //        frm.FechaFin = oUltimoDiaDelMes;
            //        frm.user.cod_usuario = user.cod_usuario;
            //        frm.ShowDialog();
            //    }
            //}
        }

        private void bgvResumenTipoServicio_CustomDrawBandHeader(object sender, DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnasBandHeader(e);
        }

        private void bgvResumenTipoServicio_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void bgvResumenTipoServicio_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void bgvResumenTipoDocumento_CustomDrawBandHeader(object sender, DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnasBandHeader(e);
        }

        private void bgvResumenTipoDocumento_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void bgvResumenTipoDocumento_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void lkpEmpresa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) lkpEmpresa.EditValue = null;
        }

        private void btnVistaResumenGeneral_Click(object sender, EventArgs e)
        {
            splitContainerControl2.PanelVisibility = SplitPanelVisibility.Panel1;
            CargarResumenporMeses();
            layoutControlItem3.Visibility = LayoutVisibility.Always;
            layoutControlItem20.Visibility = LayoutVisibility.Always;
        }

        private void dtFechaInicio_EditValueChanged(object sender, EventArgs e)
        {
            if (dtFechaInicio.EditValue != null) dtFechaFin.EditValue = Obtener_UltimoViernes(Convert.ToDateTime(dtFechaInicio.EditValue)); //.AddMonths(1);
        }

        private void bgvResumenVentasProveedor_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void bgvResumenVentasProveedor_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void pvtResumenTipoDoc_CustomDrawFieldHeader(object sender, PivotCustomDrawFieldHeaderEventArgs e)
        {
            try
            {
                if (e.Field == null) { return; }
                Rectangle rect = e.Bounds;
                ControlPaint.DrawBorder3D(e.Graphics, e.Bounds);
                rect.Inflate(-1, -1);

                SolidBrush myBrush1 = new SolidBrush(System.Drawing.Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]));
                e.Graphics.FillRectangle(myBrush1, rect);
                e.Appearance.DrawString(e.GraphicsCache, e.Info.Caption, e.Info.CaptionRect);
                foreach (DrawElementInfo info in e.Info.InnerElements)
                {
                    if (!info.Visible) continue;
                    ObjectPainter.DrawObject(e.GraphicsCache, info.ElementPainter, info.ElementInfo);
                }
                e.Appearance.ForeColor = System.Drawing.Color.White;

                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}