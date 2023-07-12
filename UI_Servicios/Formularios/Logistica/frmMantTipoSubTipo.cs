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

namespace UI_Servicios.Formularios.Logistica
{
    public partial class frmMantTipoSubTipo : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        blLogistica blLogis = new blLogistica();
        blProductos_Empresa blProdEmp = new blProductos_Empresa();

        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;

        Boolean load = true; 

        public frmMantTipoSubTipo()
        {
            InitializeComponent();
        }

        private void frmMantTipoSubTipo_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            BuscarTipoSubTipoEmpresas();

            rlkpCaracteristica.DataSource = blLogis.CombosEnGridControl<eProyecto.eProyecto_Tipo_Servicio>(4);
        }

        private void BuscarTipoSubTipoEmpresas()
        {
            List<eProyecto.eProyecto_Tipo_Servicio> ListTipoServicio = blLogis.ListarTablasTipoSubTipo<eProyecto.eProyecto_Tipo_Servicio>("Tipo", 1, "");
            List<eProyecto.eProyecto_SubTipo_Servicio> ListSubTipoServicio = blLogis.ListarTablasTipoSubTipo<eProyecto.eProyecto_SubTipo_Servicio>("SubTipo", 2, ListTipoServicio[0].cod_tipo_servicio);
            List<eEmpresa> ListEmpresas = blProdEmp.Cargar_Empresas<eEmpresa>(1);
            List<eEmpresa> ListEmpresasTipo = blLogis.ListarTablasTipoSubTipo<eEmpresa>("EmpresaTipo", 3, ListTipoServicio[0].cod_tipo_servicio);

            bsTipoServicio.DataSource = ListTipoServicio;
            bsSubTipoServicio.DataSource = ListSubTipoServicio;
            bsEmpresa.DataSource = ListEmpresas;

            gvEmpresas.RefreshData();

            for (int i = 0; i < ListEmpresasTipo.Count; i++)
            {
                for (int x = 0; x < gvEmpresas.RowCount; x++)
                {
                    eEmpresa obj = gvEmpresas.GetRow(x) as eEmpresa;

                    if (obj.cod_empresa == ListEmpresasTipo[i].cod_empresa)
                    {
                        gvEmpresas.SelectRow(i);
                    }
                }
            }
        }

        private void gvTipoServicio_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            load = true;

            eProyecto.eProyecto_Tipo_Servicio obj = gvTipoServicio.GetFocusedRow() as eProyecto.eProyecto_Tipo_Servicio;

            List<eProyecto.eProyecto_SubTipo_Servicio> ListSubTipoServicio = blLogis.ListarTablasTipoSubTipo<eProyecto.eProyecto_SubTipo_Servicio>("SubTipo", 2, obj.cod_tipo_servicio);
            List<eProductos_Empresa> ListEmpresasTipo = blLogis.ListarTablasTipoSubTipo<eProductos_Empresa>("EmpresaTipo", 3, obj.cod_tipo_servicio);

            bsSubTipoServicio.DataSource = ListSubTipoServicio;

            if (ListEmpresasTipo.Count > 0)
            {
                for (int i = 0; i < gvEmpresas.RowCount; i++)
                {
                    eEmpresa objEmp = gvEmpresas.GetRow(i) as eEmpresa;

                    gvEmpresas.UnselectRow(i);
                    for (int x = 0; x < ListEmpresasTipo.Count; x++)
                    {
                        if (objEmp.cod_empresa == ListEmpresasTipo[x].cod_empresa)
                        {
                            gvEmpresas.SelectRow(i);
                            gvEmpresas.FocusedRowHandle = i;
                        }
                    }
                }

                load = false;
            }
            else
            {
                for (int x = 0; x < gvEmpresas.RowCount; x++)
                {
                    gvEmpresas.UnselectRow(x);
                    gvEmpresas.FocusedRowHandle = 0;
                }

                load = false;
            }
        }

        private void gvTipoServicio_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gvTipoServicio.FocusedRowHandle >= 0)
            {
                e.Cancel = true;
            }
        }

        private void gvTipoServicio_HiddenEditor(object sender, EventArgs e)
        {
            eProyecto.eProyecto_Tipo_Servicio obj = gvTipoServicio.GetFocusedRow() as eProyecto.eProyecto_Tipo_Servicio;

            if (obj == null) return;

            if (obj.dsc_tipo_servicio != null && obj.cod_caracteristica != 0)
            {
                eProductos objProd = new eProductos();

                objProd.cod_tipo_servicio = "";
                objProd.dsc_tipo_servicio = obj.dsc_tipo_servicio;
                objProd.flg_activo = "SI";
                objProd.flg_materia_prima = obj.cod_caracteristica == 1 ? "SI" : "NO";
                objProd.flg_producto_terminado = obj.cod_caracteristica == 2 ? "SI" : "NO";
                objProd.flg_actividad_apoyo = obj.cod_caracteristica == 3 ? "SI" : "NO";
                objProd.flg_producto = obj.cod_caracteristica == 4 ? "SI" : "NO";
                objProd.flg_servicio = obj.cod_caracteristica == 5 ? "SI" : "NO";

                objProd = blLogis.Insertar_Actualizar_TipoServicio<eProductos>(objProd);

                obj.cod_tipo_servicio = objProd.cod_tipo_servicio;
            }
        }

        private void gvTipoServicio_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvTipoServicio_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvTipoServicio_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvTipoServicio_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvSubTipoServicio_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gvSubTipoServicio.FocusedRowHandle >= 0)
            {
                e.Cancel = true;
            }
        }

        private void gvSubTipoServicio_HiddenEditor(object sender, EventArgs e)
        {
            eProyecto.eProyecto_SubTipo_Servicio obj = gvSubTipoServicio.GetFocusedRow() as eProyecto.eProyecto_SubTipo_Servicio;

            if (obj == null) return;

            if (obj.dsc_subtipo_servicio != null)
            {
                eProyecto.eProyecto_Tipo_Servicio objTipo = gvTipoServicio.GetFocusedRow() as eProyecto.eProyecto_Tipo_Servicio;

                eProductos objProd = new eProductos();

                objProd.cod_tipo_servicio = objTipo.cod_tipo_servicio;
                objProd.dsc_subtipo_servicio = obj.dsc_subtipo_servicio;
                objProd.flg_activo = "SI";
                objProd.ctd_volumen_m3 = 0;

                objProd = blLogis.Insertar_Actualizar_SubTipoServicio<eProductos>(objProd);

                obj.cod_subtipo_servicio = objProd.cod_subtipo_servicio;
            }
        }

        private void gvSubTipoServicio_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvSubTipoServicio_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvSubTipoServicio_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvSubTipoServicio_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvEmpresas_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (load == true)
            {
                return;
            }

            string respuesta = "";

            eEmpresa objEmp = gvEmpresas.GetFocusedRow() as eEmpresa;
            eProyecto.eProyecto_Tipo_Servicio objTipo = gvTipoServicio.GetFocusedRow() as eProyecto.eProyecto_Tipo_Servicio;

            if (e.Action == CollectionChangeAction.Add)
            {
                for (int i = 0; i < gvSubTipoServicio.RowCount; i++)
                {
                    eProyecto.eProyecto_SubTipo_Servicio objSubTipo = gvSubTipoServicio.GetRow(i) as eProyecto.eProyecto_SubTipo_Servicio;

                    respuesta = blLogis.Asignar_Tipo_SubTipo_Empresa(objEmp.cod_empresa, objTipo.cod_tipo_servicio, objSubTipo.cod_subtipo_servicio);
                }
            } 
            else
            {
                for (int i = 0; i < gvSubTipoServicio.RowCount; i++)
                {
                    eProyecto.eProyecto_SubTipo_Servicio objSubTipo = gvSubTipoServicio.GetRow(i) as eProyecto.eProyecto_SubTipo_Servicio;

                    respuesta = blLogis.Desasignar_Tipo_SubTipo_Empresa(objEmp.cod_empresa, objTipo.cod_tipo_servicio, objSubTipo.cod_subtipo_servicio);
                }
            }
        }

        private void gvEmpresas_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvEmpresas_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvEmpresas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvEmpresas_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }
    }
}