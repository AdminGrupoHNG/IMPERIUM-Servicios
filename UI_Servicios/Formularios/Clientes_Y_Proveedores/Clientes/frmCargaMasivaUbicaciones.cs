using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BE_Servicios;
using BL_Servicios;

namespace UI_Servicios.Formularios.Clientes_Y_Proveedores.Clientes
{
    public partial class frmCargaMasivaUbicaciones : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        blClientes blCli = new blClientes();
        blGlobales blGlobal = new blGlobales();
        List<eCliente_Ubicacion> ListUbic = new List<eCliente_Ubicacion>();
        public string cod_cliente = "", cod_ubicacion_sup = "", cod_nivel = "", dsc_larga_ubicacion = "", cod_contacto = "";
        public int num_linea = 0;
        public string ActualizarListado = "NO";

        public frmCargaMasivaUbicaciones()
        {
            InitializeComponent();
        }

        private void gvUbicaciones_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvUbicaciones_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void frmCargaMasivaUbicaciones_Load(object sender, EventArgs e)
        {
            Inicializar();
        }
        private void Inicializar()
        {

        }
        private void gvUbicaciones_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            eCliente_Ubicacion obj = gvUbicaciones.GetRow(e.RowHandle) as eCliente_Ubicacion;
            obj.cod_cliente = cod_cliente;
            obj.cod_ubicacion_sup = cod_ubicacion_sup;
            obj.cod_nivel = cod_nivel;
            obj.dsc_larga_ubicacion = dsc_larga_ubicacion;
            obj.flg_activo = "SI";
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for(int x = 0; x <= gvUbicaciones.RowCount - 1; x++)
            {
                eCliente_Ubicacion obj = gvUbicaciones.GetRow(x) as eCliente_Ubicacion;
                if (obj == null && obj.dsc_ubicacion == "") continue;
                //ListUbic.Add(obj);
                eCliente_Ubicacion eUbic = new eCliente_Ubicacion();
                eUbic = blCli.Guardar_Actualizar_ClienteUbicacion<eCliente_Ubicacion>(eUbic, "Nuevo");
                if (eUbic != null) ActualizarListado = "SI";
            }
            this.Close();
        }
    }
}