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
using BE_Servicios;
using BL_Servicios;
using DevExpress.Utils.Drawing;
using System.Globalization;
using UI_Servicios.Formularios.Shared;

namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    public partial class frmResumenPresupuestoEjecucion : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public eUsuario user = new eUsuario();
        blProveedores blProv = new blProveedores();
        blGlobales blGlobal = new blGlobales();
        public blFactura blFact = new blFactura();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        List<eProyecto.eProyeto_Presupuesto_Ejecucion> lista = new List<eProyecto.eProyeto_Presupuesto_Ejecucion>();


        public frmResumenPresupuestoEjecucion()
        {
            InitializeComponent();
        }

        private void frmResumenPresupuestoEjecucion_Load(object sender, EventArgs e)
        {
            CultureInfo info = new CultureInfo("es-PE");
            string symbol = info.NumberFormat.CurrencySymbol;
            info.NumberFormat.CurrencySymbol = symbol;
            rtxtImporte.Mask.Culture = info;
            rtxtImporte.Mask.Culture = info;
            rtxtImporte2.Mask.Culture = info;

            blFact.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: user.cod_usuario);
            List<eFacturaProveedor> list = blProv.ListarEmpresasProveedor<eFacturaProveedor>(11, "", user.cod_usuario);
            if (list.Count >= 1) lkpEmpresa.EditValue = list[0].cod_empresa;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Tag == null) { MessageBox.Show("Debe seleccionar un proyecto", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            lista = blFact.FiltroFactura<eProyecto.eProyeto_Presupuesto_Ejecucion>(57, cod_empresa: lkpEmpresa.EditValue.ToString(), cod_proyecto: txtDescripcion.Tag.ToString());
            bsResumen.DataSource = lista;
        }

        private void gvListaTipoProducto_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eProyecto.eProyeto_Presupuesto_Ejecucion obj = gvListaTipoProducto.GetRow(e.RowHandle) as eProyecto.eProyeto_Presupuesto_Ejecucion;
                    if (obj.dsc_tipo_servicio == "TOTALES")
                    {
                        e.Graphics.DrawLine(new Pen(Color.Black) { DashStyle = System.Drawing.Drawing2D.DashStyle.Solid },
                        new Point(e.Bounds.X, e.Bounds.Bottom), new Point(e.Bounds.Right, e.Bounds.Bottom));
                        e.Graphics.DrawLine(new Pen(Color.Black) { DashStyle = System.Drawing.Drawing2D.DashStyle.Solid },
                        new Point(e.Bounds.X, e.Bounds.Top), new Point(e.Bounds.Right, e.Bounds.Top));

                        e.Appearance.ForeColor = Color.Black; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaPresupuesto_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eProyecto.eProyeto_Presupuesto_Ejecucion obj = gvListaTipoProducto.GetRow(e.RowHandle) as eProyecto.eProyeto_Presupuesto_Ejecucion;
                    if (obj.dsc_tipo_servicio == "TOTALES") 
                    {
                        e.Graphics.DrawLine(new Pen(Color.Black) { DashStyle = System.Drawing.Drawing2D.DashStyle.Solid },
                        new Point(e.Bounds.X, e.Bounds.Bottom), new Point(e.Bounds.Right, e.Bounds.Bottom));
                        e.Graphics.DrawLine(new Pen(Color.Black) { DashStyle = System.Drawing.Drawing2D.DashStyle.Solid },
                        new Point(e.Bounds.X, e.Bounds.Top), new Point(e.Bounds.Right, e.Bounds.Top));

                        e.Appearance.ForeColor = Color.Black; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaEjecutado_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eProyecto.eProyeto_Presupuesto_Ejecucion obj = gvListaTipoProducto.GetRow(e.RowHandle) as eProyecto.eProyeto_Presupuesto_Ejecucion;
                    if (obj.dsc_tipo_servicio == "TOTALES")
                    {
                        e.Graphics.DrawLine(new Pen(Color.Black) { DashStyle = System.Drawing.Drawing2D.DashStyle.Solid },
                        new Point(e.Bounds.X, e.Bounds.Bottom), new Point(e.Bounds.Right, e.Bounds.Bottom));
                        e.Graphics.DrawLine(new Pen(Color.Black) { DashStyle = System.Drawing.Drawing2D.DashStyle.Solid },
                        new Point(e.Bounds.X, e.Bounds.Top), new Point(e.Bounds.Right, e.Bounds.Top));

                        e.Appearance.ForeColor = Color.Black; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void picBuscarProyecto_Click(object sender, EventArgs e)
        {
            Busqueda("", "Proyecto");
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
                case "Proyecto":
                    frm.entidad = frmBusquedas.MiEntidad.Proyecto;
                    frm.cod_empresa = lkpEmpresa.EditValue.ToString();
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Proyecto":
                    txtDescripcion.Tag = frm.codigo;
                    txtDescripcion.Text = frm.descripcion;
                    break;
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Busqueda("", "Proyecto");
            }
            string dato = blGlobal.pKeyPress(txtDescripcion, e);
            if (dato != "")
            {
                Busqueda(dato, "Proyecto");
            }
        }

        private void txtDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                blGlobal.pKeyDown(txtDescripcion, e);
                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) { txtDescripcion.Tag = null; txtDescripcion.Text = ""; txtPorcentaje.EditValue = 0; dtFechaActualizacion.EditValue = null; lista.Clear(); bsResumen.DataSource = null; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnListarProyectos_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmListarProyectos frm = new frmListarProyectos();
            frm.user = user;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.cod_empresa = lkpEmpresa.EditValue.ToString();
            frm.dsc_empresa = lkpEmpresa.Text;
            frm.ShowDialog();
        }

        private void btnTarifarioServicioProducto_ItemClick(object sender, ItemClickEventArgs e)
        {

        }


        private void gvListaPresupuesto_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == null) return; 
            System.Drawing.Rectangle rect = e.Bounds;
            rect.Inflate(-1, -1);
            //e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(55, 86, 35)), rect);
            e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(198, 224, 180)), rect);
            e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
            foreach (DrawElementInfo info in e.Info.InnerElements)
            {
                if (!info.Visible) continue;
                ObjectPainter.DrawObject(e.Cache, info.ElementPainter, info.ElementInfo);
            }
            e.Handled = true;
        }

        private void gvListaEjecutado_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == null) return;
            System.Drawing.Rectangle rect = e.Bounds;
            rect.Inflate(-1, -1);
            //e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(131, 60, 12)), rect);
            e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(248, 203, 173)), rect);
            e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
            foreach (DrawElementInfo info in e.Info.InnerElements)
            {
                if (!info.Visible) continue;
                ObjectPainter.DrawObject(e.Cache, info.ElementPainter, info.ElementInfo);
            }
            e.Handled = true;
        }

        private void gvListaTipoProducto_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == null) return;
            System.Drawing.Rectangle rect = e.Bounds;
            rect.Inflate(-1, -1);
            e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(48, 84, 150)), rect);
            e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
            foreach (DrawElementInfo info in e.Info.InnerElements)
            {
                if (!info.Visible) continue;
                ObjectPainter.DrawObject(e.Cache, info.ElementPainter, info.ElementInfo);
            }
            e.Handled = true;
        }

    }
}