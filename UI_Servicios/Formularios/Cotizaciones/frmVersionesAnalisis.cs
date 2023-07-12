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
    public partial class frmVersionesAnalisis : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        blAnalisisServicio blAns = new blAnalisisServicio();
        blGlobales blGlobal = new blGlobales();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;

        public String empresa, sedeEmpresa, analisis, tipoServicio;

        public int sedeCliente, servicio;

        List<eAnalisis.eAnalisis_Sedes_Prestacion> lstAns;

        public frmVersionesAnalisis()
        {
            InitializeComponent();
        }

        private void frmHistorialVersiones_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            CargarGrilla();
            //this.Size = new Size(500, 400);
            //this.StartPosition = FormStartPosition.CenterParent;
        }

        private void CargarGrilla()
        {
            lstAns = blAns.ListarAnalisis<eAnalisis.eAnalisis_Sedes_Prestacion>(10, empresa, sedeEmpresa, analisis, tipoServicio: tipoServicio);
            bsVersiones.DataSource = lstAns;
        }

        private void gvVersiones_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2)
            {
                eAnalisis.eAnalisis_Sedes_Prestacion eAns = gvVersiones.GetFocusedRow() as eAnalisis.eAnalisis_Sedes_Prestacion;

                frmMantAnalisisServicio frm = new frmMantAnalisisServicio();
                frm.user = user;
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.WindowState = FormWindowState.Maximized;
                frm.accion = Analisis.Vista;
                frm.empresa = eAns.cod_empresa;
                frm.sedeEmpresa = eAns.cod_sede_empresa;
                frm.analisis = eAns.cod_analisis;
                frm.codigoCliente = eAns.cod_cliente;
                frm.sedeCliente = eAns.cod_sede_cliente;
                frm.servicio = eAns.num_servicio;
                frm.ShowDialog();
            }
        }

        private void gvVersiones_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvVersiones_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }
    }
}