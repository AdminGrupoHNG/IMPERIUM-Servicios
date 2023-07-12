using BE_Servicios;
using BL_Servicios;
using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;

namespace UI_Servicios.Formularios.Cotizaciones
{
    public partial class frmListadoAnalisisServicio : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public eUsuario user = new eUsuario();
        blAnalisisServicio blAns = new blAnalisisServicio();
        blGlobales blGlobal = new blGlobales();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;

        public string empresas, estado;

        List<eDatos> lstGenerales;
        List<eProyecto.eProyecto_Tipo_Servicio> lstTipos;
        List<eAnalisis.eAnalisis_Sedes_Prestacion> lstAns;
        List<eAnalisis.eAnalisis_Personal> lstPerAns;
        List<eAnalisis.eAnalisis_Personal_Uniformes> lstUnifEpp;
        List<eAnalisis.eAnalisis_Est_Cst> lstCst;

        DataTable dtGeneral;

        public frmListadoAnalisisServicio()
        {
            InitializeComponent();
        }

        private void frmListadoAnalisisServicio_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void frmListadoAnalisisServicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) CargarListado();
        }

        private void Inicializar()
        {
            try
            {
                estado = "REQ";

                InitTreeList();
                CargarLookUpEdit();
                ConfigurarDateEdit();
                CargarListado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void InitTreeList()
        {
            tlstEmpresas.Appearance.Row.BackColor = Color.Transparent;
            tlstEmpresas.Appearance.Empty.BackColor = Color.Transparent;
            tlstEmpresas.BackColor = Color.Transparent;
            tlstEmpresas.CheckBoxFieldName = "Checked";
            tlstEmpresas.TreeViewFieldName = "Name";
            tlstEmpresas.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            tlstEmpresas.OptionsBehavior.Editable = false;
            tlstEmpresas.OptionsBehavior.ReadOnly = true;
            tlstEmpresas.OptionsBehavior.AllowRecursiveNodeChecking = true;
            tlstEmpresas.NodeCellStyle += OnNodeCellStyle;
            tlstEmpresas.BeforeFocusNode += OnBeforeFocusNode;
            var dataSource = GenerateDataSource();
            tlstEmpresas.DataSource = dataSource;
            tlstEmpresas.ForceInitialize();
            tlstEmpresas.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
            tlstEmpresas.Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
            tlstEmpresas.ExpandAll();
        }

        void OnNodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.FontSizeDelta += 1;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
            if (e.Node.Level == 1 && e.Node.Nodes.Count > 0)
                e.Appearance.FontStyleDelta = FontStyle.Bold;
        }

        BindingList<Option> GenerateDataSource()
        {
            BindingList<Option> _options = new BindingList<Option>();

            List<eEmpresa> empresas = blAns.ListarGeneral<eEmpresa>("EmpresasxUsuario", usuario: user.cod_usuario);

            _options.Add(new Option() { ParentID = "0", ID = "1", Name = "EMPRESA", Checked = true });
            foreach (eEmpresa obj in empresas)
            {
                _options.Add(new Option() { ParentID = "1", ID = obj.cod_empresa, Name = obj.dsc_empresa, Checked = true });
            }

            return _options;
        }

        class Option : INotifyPropertyChanged
        {
            public string ParentID { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            bool? checkedCore = false;

            public event PropertyChangedEventHandler PropertyChanged;

            public bool? Checked
            {
                get { return checkedCore; }
                set
                {
                    if (checkedCore == value)
                        return;
                    checkedCore = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Checked"));
                }
            }
        }

        void OnBeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            e.CanFocus = false;
        }

        private void CargarLookUpEdit()
        {
            
        }

        private void ConfigurarDateEdit()
        {
            DateTime date = DateTime.Now;
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
        }

        private void CargarListado()
        {
            List<eAnalisis.eAnalisis_Sedes_Prestacion> lstAnalisis = blAns.ListarAnalisis<eAnalisis.eAnalisis_Sedes_Prestacion>(1, empresas, estado: estado);
            List<eDatos> lstTotales = blAns.ListarAnalisis<eDatos>(7, empresas);

            bsListadoAnalisis.DataSource = lstAnalisis;
            tbiAprobados.Elements[1].Text = lstTotales[0].AtributoDiez.ToString();
            tbiDeclinados.Elements[1].Text = lstTotales[1].AtributoDiez.ToString();
            tbiEnviados.Elements[1].Text = lstTotales[2].AtributoDiez.ToString();
            tbiProceso.Elements[1].Text = lstTotales[3].AtributoDiez.ToString();
            tbiRequeridos.Elements[1].Text = lstTotales[4].AtributoDiez.ToString();
            tbiRevision.Elements[1].Text = lstTotales[5].AtributoDiez.ToString();
        }

        private void btnNuevoReq_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMantRequerimientoAnalisis frm = new frmMantRequerimientoAnalisis();
            frm.user = user;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.ShowDialog();

            CargarListado();
        }

        private void btnEditarReq_ItemClick(object sender, ItemClickEventArgs e)
        {
            eAnalisis.eAnalisis_Sedes_Prestacion eAns = gvListadoAnalisis.GetFocusedRow() as eAnalisis.eAnalisis_Sedes_Prestacion;

            frmMantRequerimientoAnalisis frm = new frmMantRequerimientoAnalisis();
            frm.user = user;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.accion = RequerimientoAns.Editar;
            frm.empresa = eAns.cod_empresa;
            frm.sedeEmpresa = eAns.cod_sede_empresa;
            frm.analisis = eAns.cod_analisis;
            frm.codigoCliente = eAns.cod_cliente;

            frm.ShowDialog();

            CargarListado();
        }

        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnAprobarAnalisis_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void btnAnularAnalisis_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnClonarAnalisis_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea generar una nueva versión del registro?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string observaciones = Microsoft.VisualBasic.Interaction.InputBox("¿Desea agregar alguna observación?", "Observaciones", "");

                eAnalisis.eAnalisis_Sedes_Prestacion eAns = gvListadoAnalisis.GetFocusedRow() as eAnalisis.eAnalisis_Sedes_Prestacion;

                string respuesta = blAns.Clonar_Analisis(eAns.cod_empresa, eAns.cod_sede_empresa, eAns.cod_analisis, eAns.cod_sede_cliente, eAns.num_servicio, observaciones.ToUpper());

                MessageBox.Show("Registro generado de manera éxitosa.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarListado();
            }
        }

        private void btnExportarEstCst_ItemClick(object sender, ItemClickEventArgs e)
        {
            eAnalisis.eAnalisis_Sedes_Prestacion eDir = gvListadoAnalisis.GetFocusedRow() as eAnalisis.eAnalisis_Sedes_Prestacion;
            lstCst = blAns.ListarAnalisis<eAnalisis.eAnalisis_Est_Cst>(6, eDir.cod_empresa, eDir.cod_sede_empresa, eDir.cod_analisis, servicio: eDir.num_servicio);

            if (lstCst != null && lstCst.Count > 0)
            {
                lstGenerales = blAns.ListarGeneral<eDatos>("Generales");
                lstTipos = blAns.ListarGeneral<eProyecto.eProyecto_Tipo_Servicio>("Tipo", eDir.cod_empresa, opcion: 0);
                lstAns = blAns.ListarAnalisis<eAnalisis.eAnalisis_Sedes_Prestacion>(2, eDir.cod_empresa, eDir.cod_sede_empresa, eDir.cod_analisis);

                GenerarExcel();
            }
            else
            {
                MessageBox.Show("No existen datos para exportar.", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnOcultarFiltro_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (controlFiltros.ContentVisible == true)
            {
                controlFiltros.ContentVisible = false;
                controlFiltros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                btnOcultarFiltro.Caption = "Mostrar Filtro";
                return;
            }
            else
            {
                controlFiltros.ContentVisible = true;
                controlFiltros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                btnOcultarFiltro.Caption = "Ocultar Filtro";
                return;
            }
        }

        private void btnSeleccion_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (colrckSeleccionar.Visible == true)
            {
                colrckSeleccionar.Visible = false;
            }
            else
            {
                colrckSeleccionar.Visible = true;
                colrckSeleccionar.VisibleIndex = 0;
            }
        }

        private void tlstEmpresas_NodeChanged(object sender, NodeChangedEventArgs e)
        {
            if (e.Node.GetValue("Checked") == null) return;

            if ((Boolean)e.Node.GetValue("Checked") == true)
            {
                if (e.Node.GetValue("ParentID").ToString() != "0")
                {
                    empresas = e.Node.GetValue("ID").ToString() + "," + empresas;
                }
            }
            else
            {
                if (e.Node.GetValue("ParentID").ToString() != "0")
                {
                    if (empresas != null)
                    {
                        empresas = empresas.Replace(e.Node.GetValue("ID").ToString() + ",", "");
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarListado();
        }

        private void tbEstados_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "tbiRequeridos":
                    tbiRequeridos.AppearanceItem.Normal.BackColor = Color.FromArgb(46, 85, 88);
                    tbiProceso.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiEnviados.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiRevision.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiAprobados.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiDeclinados.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    estado = "REQ";
                    break;
                case "tbiProceso":
                    tbiRequeridos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiProceso.AppearanceItem.Normal.BackColor = Color.FromArgb(46, 85, 88);
                    tbiEnviados.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiRevision.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiAprobados.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiDeclinados.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    estado = "PRC";
                    break;
                case "tbiEnviados":
                    tbiRequeridos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiProceso.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiEnviados.AppearanceItem.Normal.BackColor = Color.FromArgb(46, 85, 88);
                    tbiRevision.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiAprobados.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiDeclinados.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    estado = "ENV";
                    break;
                case "tbiRevision":
                    tbiRequeridos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiProceso.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiEnviados.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiRevision.AppearanceItem.Normal.BackColor = Color.FromArgb(46, 85, 88);
                    tbiAprobados.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiDeclinados.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    estado = "REV";
                    break;
                case "tbiAprobados":
                    tbiRequeridos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiProceso.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiRevision.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiAprobados.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiAprobados.AppearanceItem.Normal.BackColor = Color.FromArgb(46, 85, 88);
                    tbiDeclinados.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    estado = "APR";
                    break;
                case "tbiDeclinados":
                    tbiRequeridos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiProceso.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiRevision.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiAprobados.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiAprobados.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiDeclinados.AppearanceItem.Normal.BackColor = Color.FromArgb(46, 85, 88);
                    estado = "DEC";
                    break;
            }

            CargarListado();
            ConfigurarForm();
        }

        private void ConfigurarForm()
        {
            if (estado != "REQ")
            {
                coldsc_periodo.Visible = true;
                colfch_visita.Visible = false;
                coldsc_observaciones.Visible = false;

                coldsc_periodo.VisibleIndex = 6;
            }
            else
            {
                coldsc_periodo.Visible = false;
                colfch_visita.Visible = true;
                coldsc_observaciones.Visible = true;

                colfch_visita.VisibleIndex = 6;
                coldsc_observaciones.VisibleIndex = 7;
            }
        }

        private void gvListadoAnalisis_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2)
            {
                eAnalisis.eAnalisis_Sedes_Prestacion eAns = gvListadoAnalisis.GetFocusedRow() as eAnalisis.eAnalisis_Sedes_Prestacion;

                frmMantAnalisisServicio frm = new frmMantAnalisisServicio();
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.WindowState = FormWindowState.Maximized;
                frm.accion = Analisis.Editar;
                frm.empresa = eAns.cod_empresa;
                frm.sedeEmpresa = eAns.cod_sede_empresa;
                frm.analisis = eAns.cod_analisis;
                frm.codigoCliente = eAns.cod_cliente;
                frm.sedeCliente = eAns.cod_sede_cliente;
                frm.servicio = eAns.num_servicio;
                frm.ShowDialog();

                CargarListado();
            }
        }

        private void gvListadoAnalisis_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 1 && e.Column.Name == "colnum_version")
            {
                eAnalisis.eAnalisis_Sedes_Prestacion eAns = gvListadoAnalisis.GetFocusedRow() as eAnalisis.eAnalisis_Sedes_Prestacion;

                frmVersionesAnalisis frm = new frmVersionesAnalisis();
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.empresa = eAns.cod_empresa;
                frm.sedeEmpresa = eAns.cod_sede_empresa;
                frm.analisis = eAns.cod_analisis;
                frm.tipoServicio = eAns.cod_tipo_prestacion;

                frm.ShowDialog();

                CargarListado();
            }
        }

        private void gvListadoAnalisis_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            eAnalisis obj = (eAnalisis)gvListadoAnalisis.GetRow(e.RowHandle1);
            eAnalisis obj2 = (eAnalisis)gvListadoAnalisis.GetRow(e.RowHandle2);

            if (obj.cod_analisis == obj2.cod_analisis && e.CellValue1.ToString() == e.CellValue2.ToString())
            {
                e.Merge = true;
                e.Handled = true;
            }
            else
            {
                e.Merge = false;
                e.Handled = true;
            }
        }

        private void gvAnsGeneradosListadoAnalisis_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoAnalisis_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void GenerarExcel()
        {
            lstAns = lstAns.OrderBy(x => x.cod_tipo_prestacion).ToList();

            try
            {
                Excel.Application objExcel = new Excel.Application();
                objExcel.Workbooks.Add();

                var workbook = objExcel.ActiveWorkbook;
                var sheet = workbook.Sheets["Hoja1"];
                //objExcel.Visible = true;

                objExcel.Sheets.Add();
                var worksheet = workbook.ActiveSheet;
                worksheet.Name = "Estructura de Costos";

                int col = 0, sede = 7, lst = 0, max = 56;
                string colIni = obtenerColumna(7);

                foreach (eAnalisis.eAnalisis_Sedes_Prestacion obj in lstAns)
                {
                    if (lstAns[lst == 0 ? 0 : lst - 1].cod_tipo_prestacion != obj.cod_tipo_prestacion)
                    {
                        objExcel.Range["C11:C11"].Select(); objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeRight).Color = Color.FromArgb(255, 255, 255);

                        worksheet.Shapes.AddPicture(@"C:\Users\ALVARO LAVERIANO\OneDrive - GRUPO HNG CORPORACION S.A.C\Escritorio\Avances\Imperium\IMPERIUM-Servicios\UI_Servicios\Resources\LogoFacilita.png", MsoTriState.msoFalse, MsoTriState.msoCTrue, 22.5, 18, 80, 40);

                        sheet.Delete();
                        objExcel.WindowState = Excel.XlWindowState.xlMaximized;
                        objExcel.Visible = true;
                        objExcel = null;

                        lstCst = blAns.ListarAnalisis<eAnalisis.eAnalisis_Est_Cst>(6, obj.cod_empresa, obj.cod_sede_empresa, obj.cod_analisis, servicio: obj.num_servicio);
                        if (lstCst == null || lstCst.Count == 0) { return; }

                        objExcel = new Excel.Application();
                        objExcel.Workbooks.Add();

                        workbook = objExcel.ActiveWorkbook;
                        sheet = workbook.Sheets["Hoja1"];
                        objExcel.Visible = true;

                        objExcel.Sheets.Add();
                        worksheet = workbook.ActiveSheet;
                        worksheet.Name = "Estructura de Costos";

                        col = 0; sede = 7; max = 56;
                        colIni = obtenerColumna(7);
                    }

                    lstCst = blAns.ListarAnalisis<eAnalisis.eAnalisis_Est_Cst>(6, obj.cod_empresa, obj.cod_sede_empresa, obj.cod_analisis, servicio: obj.num_servicio);
                    lstPerAns = blAns.ListarAnalisis<eAnalisis.eAnalisis_Personal>(4, obj.cod_empresa, obj.cod_sede_empresa, obj.cod_analisis, servicio: obj.num_servicio);
                    lstUnifEpp = blAns.ListarAnalisis<eAnalisis.eAnalisis_Personal_Uniformes>(9, obj.cod_empresa, obj.cod_sede_empresa, obj.cod_analisis, servicio: obj.num_servicio);

                    lstPerAns = lstPerAns.OrderBy(x => x.num_orden).ToList();

                    dtGeneral = CrearDataTable();
                    col = col + lstPerAns.Count;
                    string colFin = obtenerColumna(6 + col);
                    Boolean num;

                    objExcel.ActiveWindow.DisplayGridlines = false;
                    objExcel.Range["A:A"].ColumnWidth = 3; objExcel.Range["B:B"].ColumnWidth = 3; objExcel.Range["C:C"].ColumnWidth = 5; objExcel.Range["D:D"].ColumnWidth = 44;
                    objExcel.Range["B2:" + colFin + "4"].Select();
                    objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#002060");
                    objExcel.Cells[6, 2] = "Cliente:"; objExcel.Cells[6, 4] = obj.dsc_cliente;
                    objExcel.Cells[7, 2] = "Dirección:"; objExcel.Cells[7, 4] = obj.dsc_cadena_direccion;
                    objExcel.Cells[8, 2] = "Servicio:"; objExcel.Cells[8, 4] = obj.dsc_tipo_prestacion;
                    objExcel.Range["D6:D8"].Select(); objExcel.Selection.Font.Bold = true;
                    objExcel.Range[colIni + "10:" + colFin + "10"].MergeCells = true; objExcel.Cells[10, sede] = obj.dsc_sede_cliente;
                    objExcel.Range[colIni + "10:" + colFin + "10"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    objExcel.Range[colIni + "10:" + colFin + "10"].Select(); objExcel.Selection.Font.Bold = true;
                    objExcel.Range[colIni + "10:" + colFin + "10"].Select(); objExcel.Selection.Borders.Color = Color.FromArgb(0, 0, 0);
                    objExcel.Cells[11, 4] = "CONCEPTO";
                    objExcel.Cells[11, 5] = "%";
                    objExcel.Cells[11, 6] = "TOTALES";

                    for (int x = 0; x < lstPerAns.Count; x++)
                    {
                        objExcel.Cells[11, sede + x] = lstPerAns[x].dsc_cargo;
                        string colOp = obtenerColumna(sede + x);
                        objExcel.Range[colOp + ":" + colOp].ColumnWidth = 16;
                        objExcel.Range[colOp + ":" + colOp].WrapText = true;
                    }

                    objExcel.Range["D11:" + colFin + "11"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    objExcel.Range["D11:" + colFin + "11"].VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    objExcel.Range["D11:" + colFin + "11"].Select(); objExcel.Selection.Font.Bold = true;
                    objExcel.Range["D11:F11"].Select(); objExcel.Selection.Borders.Color = Color.FromArgb(0, 0, 0);
                    objExcel.Range[colIni + "11:" + colFin + "11"].Select(); objExcel.Selection.Borders.Color = Color.FromArgb(0, 0, 0);

                    objExcel.Cells[12, 4] = dtGeneral.Rows[0][1];
                    objExcel.Range["D12"].Select(); objExcel.Selection.Font.Bold = true;
                    int fila = 13, dec = 1;
                    string pos = "1";

                    for (int x = 0; x < dtGeneral.Rows.Count; x++)
                    {
                        if (dtGeneral.Rows[x == 0 ? x : x - 1][1].ToString() != dtGeneral.Rows[x][1].ToString())
                        {
                            objExcel.Cells[fila + x, 4] = dtGeneral.Rows[x][1];
                            objExcel.Range["D" + (fila + x) + ":D" + (fila + x)].Select(); objExcel.Selection.Font.Bold = true;
                            fila++;
                            pos = dtGeneral.Rows[x][0].ToString().Substring(4, 1);
                            dec = 1;
                        }

                        if (dtGeneral.Rows[x][3].ToString() == "SUBTOTAL REMUNERACION" || dtGeneral.Rows[x][3].ToString() == "SUBTOTAL LEYES SOCIALES" || dtGeneral.Rows[x][3].ToString() == "SUBTOTAL BENEFICIOS SOCIALES" || dtGeneral.Rows[x][3].ToString() == "SUBTOTAL GASTOS DE PERSONAL" || dtGeneral.Rows[x][3].ToString() == "SUBTOTAL COSTOS OPERATIVOS" || dtGeneral.Rows[x][3].ToString() == "OPERARIOS" || dtGeneral.Rows[x][3].ToString() == "TOTAL MANO DE OBRA")
                        {
                            objExcel.Range["D" + (fila + x).ToString()].Select(); objExcel.Selection.Font.Bold = true; objExcel.Range["D" + (fila + x).ToString()].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            num = true;
                        }
                        else
                        {
                            num = false;
                        }

                        if (x == 25) { fila = fila + x; break; }

                        objExcel.Cells[fila + x, 3] = dtGeneral.Rows[x][0].ToString() == "00006" || num ? "" : pos + "." + (dec++).ToString();
                        objExcel.Cells[fila + x, 4] = dtGeneral.Rows[x][3];
                        objExcel.Cells[fila + x, 5] = dtGeneral.Rows[x][4].ToString() == "0" || dtGeneral.Rows[x][4].ToString() == "0.0" || dtGeneral.Rows[x][4].ToString() == "0.00" ? "" : dtGeneral.Rows[x][4];
                        objExcel.Cells[fila + x, 6] = Convert.ToDouble(dtGeneral.Rows[x][7]) + Convert.ToDouble(objExcel.Range["F" + (fila + x).ToString()].Value);

                        for (int i = 8; i < dtGeneral.Columns.Count; i++)
                        {
                            objExcel.Cells[fila + x, sede + (i - 8)] = dtGeneral.Rows[x][i];
                        }
                    }

                    foreach (eProyecto.eProyecto_Tipo_Servicio item in lstTipos)
                    {
                        objExcel.Cells[fila, 4] = item.dsc_tipo_servicio;

                        for (int t = 25; t < dtGeneral.Rows.Count; t++)
                        {
                            if (dtGeneral.Rows[t][3].ToString() == item.dsc_tipo_servicio)
                            {
                                objExcel.Cells[fila, 5] = dtGeneral.Rows[t][4].ToString() == "0" || dtGeneral.Rows[t][4].ToString() == "0.0" || dtGeneral.Rows[t][4].ToString() == "0.00" ? "" : dtGeneral.Rows[t][4];
                                objExcel.Cells[fila, 6] = Convert.ToDouble(dtGeneral.Rows[t][7]) + Convert.ToDouble(objExcel.Range["F" + (fila).ToString()].Value);
                                break;
                            }
                        }

                        fila++;
                    }

                    for (int x = 0; x < dtGeneral.Rows.Count; x++)
                    {
                        if (dtGeneral.Rows[x][0].ToString() == "00006")
                        {
                            if (dtGeneral.Rows[x == 0 ? x : x - 1][1].ToString() != dtGeneral.Rows[x][1].ToString())
                            {


                                objExcel.Cells[fila, 4] = dtGeneral.Rows[x][1];
                                objExcel.Range["D" + (fila) + ":D" + (fila)].Select(); objExcel.Selection.Font.Bold = true;
                                fila++;
                            }

                            if (dtGeneral.Rows[x][3].ToString() == "TOTAL DEL CONTRATO" || dtGeneral.Rows[x][3].ToString() == "IGV" || dtGeneral.Rows[x][3].ToString() == "TOTAL")
                            {
                                objExcel.Range["D" + (fila).ToString()].Select(); objExcel.Selection.Font.Bold = true;
                                objExcel.Range["E" + (fila).ToString()].Select(); objExcel.Selection.Font.Bold = true;
                                objExcel.Range["F" + (fila).ToString()].Select(); objExcel.Selection.Font.Bold = true;
                            }

                            objExcel.Cells[fila, 3] = "";
                            objExcel.Cells[fila, 4] = dtGeneral.Rows[x][3];
                            objExcel.Cells[fila, 5] = dtGeneral.Rows[x][4].ToString() == "0" || dtGeneral.Rows[x][4].ToString() == "0.0" || dtGeneral.Rows[x][4].ToString() == "0.00" ? "" : dtGeneral.Rows[x][4];
                            objExcel.Cells[fila, 6] = Convert.ToDouble(dtGeneral.Rows[x][7]) + Convert.ToDouble(objExcel.Range["F" + (fila).ToString()].Value);

                            fila++;
                        }
                    }

                    fila = fila - 1;

                    objExcel.Range["B6:" + colFin + fila.ToString()].Font.Size = 9;
                    objExcel.Range["C11:F11"].Select(); objExcel.Selection.Borders.Color = Color.FromArgb(0, 0, 0);
                    objExcel.Range[colIni + "11:" + colFin + "11"].Select(); objExcel.Selection.Borders.Color = Color.FromArgb(0, 0, 0);
                    objExcel.Range["C12:F" + fila.ToString()].Select();
                    objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeLeft).Color = Color.FromArgb(0, 0, 0);
                    objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeBottom).Color = Color.FromArgb(0, 0, 0);
                    objExcel.Range[colIni + "12:" + colFin + fila.ToString()].Select();
                    objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeLeft).Color = Color.FromArgb(0, 0, 0);
                    objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeBottom).Color = Color.FromArgb(0, 0, 0);
                    objExcel.Range["D12:D" + fila.ToString()].Select(); objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeRight).Color = Color.FromArgb(0, 0, 0);
                    objExcel.Range["E12:E" + fila.ToString()].Select(); objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeRight).Color = Color.FromArgb(0, 0, 0);
                    objExcel.Range["F12:F" + fila.ToString()].Select(); objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeRight).Color = Color.FromArgb(0, 0, 0);

                    for (int x = 8; x < dtGeneral.Columns.Count; x++)
                    {
                        string letra = obtenerColumna(sede  + (x - 8));
                        objExcel.Range[letra + "12:" + letra + fila.ToString()].Select(); objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeRight).Color = Color.FromArgb(0, 0, 0);
                    }

                    objExcel.Range["E12:E" + fila.ToString()].NumberFormat = "#,##0.00 [$%-es-PE]";
                    objExcel.Range["F12:" + colFin + fila.ToString()].NumberFormat = "[$S/-es-PE] #,##0.00";

                    col = col + 1;
                    sede = sede + lstPerAns.Count + 1;
                    colIni = obtenerColumna(sede);
                    lst++;
                }

                for (int f = 42; f < max; f++)
                {
                    if (Convert.ToDouble(objExcel.Range["F" + (f).ToString()].Value) == Convert.ToDouble(0))
                    {
                        objExcel.Range["A" + f.ToString() + ":A" + f.ToString()].EntireRow.Delete();
                        f = f - 1;
                        max = max - 1;
                    }
                }

                objExcel.Range["C11:C11"].Select(); objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeRight).Color = Color.FromArgb(255, 255, 255);

                worksheet.Shapes.AddPicture(@"C:\Users\ALVARO LAVERIANO\OneDrive - GRUPO HNG CORPORACION S.A.C\Escritorio\Avances\Imperium\IMPERIUM-Servicios\UI_Servicios\Resources\LogoFacilita.png", MsoTriState.msoFalse, MsoTriState.msoCTrue, 22.5, 18, 80, 40);

                sheet.Delete();
                objExcel.WindowState = Excel.XlWindowState.xlMaximized;
                objExcel.Visible = true;
                objExcel = null;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Generar Reporte.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable CrearDataTable()
        {
            decimal sueldo = 0, sociales = 0, beneficios = 0, gastos = 0, descansero = 0, movilidad = 0, examenes = 0, polizas = 0, uniforme = 0, essalud = 0, sctr = 0, sctrSoc = 0, sctrAlt = 0, segVida = 0;

            DataTable dt = new DataTable();
            dt.Columns.Add("cod_concepto");
            dt.Columns.Add("dsc_concepto");
            dt.Columns.Add("cod_item");
            dt.Columns.Add("dsc_item");
            dt.Columns.Add("prc_ley");
            dt.Columns.Add("imp_unitario");
            dt.Columns.Add("prc_margen");
            dt.Columns.Add("imp_total");

            foreach (eAnalisis.eAnalisis_Est_Cst obj in lstCst)
            {
                DataRow dr = dt.NewRow();
                dr[0] = obj.cod_concepto;
                dr[1] = obj.dsc_concepto;
                dr[2] = obj.cod_item;
                dr[3] = obj.dsc_item;
                dr[4] = Math.Round(obj.prc_ley, 2);
                dr[5] = Math.Round(obj.imp_unitario, 2);
                dr[6] = Math.Round(obj.prc_margen, 2);
                dr[7] = Math.Round(obj.imp_total, 2);
                dt.Rows.Add(dr);
            }

            int col = 8;
            int pers = 0;
            int index = 1;

            foreach (eAnalisis.eAnalisis_Personal obj in lstPerAns) { pers = obj.num_cantidad + pers; }

            foreach (eAnalisis.eAnalisis_Personal obj in lstPerAns)
            {
                dt.Columns.Add("Operario" + index);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    switch (dt.Rows[x][3].ToString())
                    {
                        case "SUELDO": dt.Rows[x][col] = Math.Round(obj.imp_salario, 2); break;
                        case "ASIGNACIÓN FAMILIAR": dt.Rows[x][col] = Math.Round((lstGenerales[1].num_valor), 2); break;
                        case "HORAS EXTRA": dt.Rows[x][col] = Math.Round(obj.imp_salario_extra, 2); break;
                        case "BONIFICACIÓN SEGÚN RENDIMIENTO": dt.Rows[x][col] = Math.Round(obj.imp_bono_productividad, 2); break;
                        case "BONIFICACIÓN NOCTURNA": dt.Rows[x][col] = Math.Round(obj.imp_bono_nocturno, 2); break;
                        case "FERIADOS":
                            dt.Rows[x][col] = Math.Round(obj.imp_feriado, 2); sueldo = Convert.ToDecimal(dt.Rows[x][col]) + Convert.ToDecimal(dt.Rows[x - 1][col]) + Convert.ToDecimal(dt.Rows[x - 2][col]) + Convert.ToDecimal(dt.Rows[x - 3][col]) + Convert.ToDecimal(dt.Rows[x - 4][col]) + Convert.ToDecimal(dt.Rows[x - 5][col]);
                            break;
                        case "ESSALUD": essalud = Math.Round(((sueldo * Convert.ToDecimal(dt.Rows[x][4])) / 100), 2); dt.Rows[x][col] = essalud; break;
                        case "SEGURO VIDA LEY": segVida = Convert.ToDecimal(dt.Rows[x][7]) / pers; dt.Rows[x][col] = segVida; break;
                        case "SEGURO COMPLEMENTARIO (SCTR)": sctr = Math.Round(((sueldo * Convert.ToDecimal(dt.Rows[x][4])) / 100), 2); sctr = sctr + (sctr * (Convert.ToDecimal(dt.Rows[x][6]) / 100)); dt.Rows[x][col] = sctr; break;
                        case "SCTR SOCAVON": sctrSoc = Math.Round(((sueldo * Convert.ToDecimal(dt.Rows[x][4])) / 100), 2); sctrSoc = sctrSoc + (sctrSoc * (Convert.ToDecimal(dt.Rows[x][6]) / 100)); dt.Rows[x][col] = sctrSoc; break;
                        case "SCTR ALTURA":
                            sctrAlt = Math.Round(((sueldo * Convert.ToDecimal(dt.Rows[x][4])) / 100), 2); sctrAlt = sctrAlt + (sctrAlt * (Convert.ToDecimal(dt.Rows[x][6]) / 100)); dt.Rows[x][col] = sctrAlt;
                            break;
                        case "GRATIFICACIÓN + BONIFICACIÓN ESPECIAL": sociales = sueldo + essalud + segVida + sctr + sctrSoc + sctrAlt; dt.Rows[x][col] = Math.Round(((sociales * Convert.ToDecimal(dt.Rows[x][4])) / 100), 2); break;
                        case "VACACIONES": dt.Rows[x][col] = Math.Round(((sociales * Convert.ToDecimal(dt.Rows[x][4])) / 100), 2) + (sociales / 12); break;
                        case "COMPENSACIÓN POR TIEMPO DE SERVICIO (CTS)":
                            dt.Rows[x][col] = Math.Round(((sociales * Convert.ToDecimal(dt.Rows[x][4])) / 100), 2);
                            beneficios = sociales + Convert.ToDecimal(dt.Rows[x][col]) + Convert.ToDecimal(dt.Rows[x - 1][col]) + Convert.ToDecimal(dt.Rows[x - 2][col]);
                            break;
                        case "MOVILIDAD": movilidad = Math.Round(obj.imp_movilidad, 2); dt.Rows[x][col] = movilidad; break;
                        case "EXÁMENES MÉDICOS OCUPACIONALES (LEY 29783)": examenes = Convert.ToDecimal(dt.Rows[x][7]) / pers; dt.Rows[x][col] = examenes; break;
                        case "POLIZAS RC/DH": polizas = Convert.ToDecimal(dt.Rows[x][7]) / pers; dt.Rows[x][col] = polizas; break;
                        case "UNIFORMES Y EPP":
                            List<eAnalisis.eAnalisis_Personal_Uniformes> lstUnifUnit = lstUnifEpp.FindAll(i => i.cod_cargo == obj.cod_cargo);
                            uniforme = Math.Round(lstUnifUnit.Sum(i => i.imp_venta) / obj.num_cantidad, 2);
                            dt.Rows[x][col] = uniforme;
                            gastos = beneficios + movilidad + examenes + polizas + uniforme;
                            break;
                        case "DESCANSEROS":
                            descansero = obj.flg_descansero ? Math.Round(gastos / 6, 2) : 0; dt.Rows[x][col] = obj.flg_descansero ? descansero : 0;
                            break;
                        //case "SUBTOTAL":
                        //    dt.Rows[x][col] = dt.Rows[x][0].ToString() == "00004" ? Math.Round(gastos + descansero, 2) : 0; 
                        //    break;
                        case "OPERARIOS": dt.Rows[x][col] = obj.num_cantidad; break;
                        case "OTROS CONCEPTOS (SIG)":
                            dt.Rows[x][col] = Convert.ToDecimal(dt.Rows[x][7]) / pers;
                            break;
                        case "TOTAL MANO DE OBRA": dt.Rows[x][col] = Math.Round((gastos + descansero) * obj.num_cantidad, 2); break;
                    }
                }

                col = col + 1;
                index = index + 1;
            }

            return dt;
        }

        private string obtenerColumna(int n = 0)
        {
            string columna = "";

            while (n > 26)
            {
                columna = columna + "A";
                n = n - 26;
            }

            columna = columna + obtenerLetra(n);

            return columna;
        }

        private string obtenerLetra(int n)
        {
            string letra = "";

            switch (n)
            {
                case 1: letra = "A"; break;
                case 2: letra = "B"; break;
                case 3: letra = "C"; break;
                case 4: letra = "D"; break;
                case 5: letra = "E"; break;
                case 6: letra = "F"; break;
                case 7: letra = "G"; break;
                case 8: letra = "H"; break;
                case 9: letra = "I"; break;
                case 10: letra = "J"; break;
                case 11: letra = "K"; break;
                case 12: letra = "L"; break;
                case 13: letra = "M"; break;
                case 14: letra = "N"; break;
                case 15: letra = "O"; break;
                case 16: letra = "P"; break;
                case 17: letra = "Q"; break;
                case 18: letra = "R"; break;
                case 19: letra = "S"; break;
                case 20: letra = "T"; break;
                case 21: letra = "U"; break;
                case 22: letra = "V"; break;
                case 23: letra = "W"; break;
                case 24: letra = "X"; break;
                case 25: letra = "Y"; break;
                case 26: letra = "Z"; break;
            }

            return letra;
        }

        
    }
}