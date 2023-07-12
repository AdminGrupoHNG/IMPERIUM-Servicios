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
using DevExpress.XtraSplashScreen;

namespace UI_Servicios.Formularios.Cuentas_Pagar
{
    public partial class frmFacturasConstanciaDetraccRetenc : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        blEncrypta blEncryp = new blEncrypta();
        public blFactura blFact = new blFactura();
        public List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listFacturas = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public string cod_proveedor = "", cod_tipo_fecha = "";
        public string FechaInicio, FechaFin;

        public frmFacturasConstanciaDetraccRetenc()
        {
            InitializeComponent();
        }

        private void frmFacturasConstanciaDetracc_Load(object sender, EventArgs e)
        {
            //BuscarFacturas();
            bsFacturasProveedor.DataSource = listFacturas;
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvFacturasProveedor.PostEditor(); gvFacturasProveedor.RefreshData();
            for (int x = 0; x <= gvFacturasProveedor.RowCount; x++)
            {
                eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = gvFacturasProveedor.GetRow(x) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                if (obj == null) continue;
                eFacturaProveedor objDetRet = new eFacturaProveedor();
                objDetRet.tipo_documento = obj.tipo_documento; objDetRet.serie_documento = obj.serie_documento; 
                objDetRet.numero_documento = obj.numero_documento; objDetRet.cod_proveedor = obj.cod_proveedor; objDetRet.cod_empresa = obj.cod_empresa;
                objDetRet.cod_usuario_registro = user.cod_usuario; objDetRet.dsc_observacion = obj.dsc_observacion; 
                objDetRet.fch_pago_ejecutado_detraccion = obj.cod_tipo_prog == "DETRACC" ? obj.fch_ejecucion : objDetRet.fch_pago_ejecutado_detraccion;
                objDetRet.imp_detraccion_pagada = obj.imp_pago; objDetRet.flg_detraccion = obj.cod_tipo_prog == "DETRACC" ? "SI" : "NO";
                objDetRet.num_constancia_detraccion = obj.cod_tipo_prog == "DETRACC" ? obj.num_constancia_detraccion : objDetRet.num_constancia_detraccion;
                objDetRet.num_constancia_retencion = obj.cod_tipo_prog == "RETENC" ? obj.num_constancia_detraccion : objDetRet.num_constancia_retencion;
                objDetRet.cod_usuario_registro = user.cod_usuario; objDetRet.fch_constancia_detraccion = obj.fch_constancia_detraccion;
                string result = blFact.AplicarDetraccionMasiva(objDetRet, obj.cod_tipo_prog);
                if (result != "OK") { MessageBox.Show("Error al aplicar detracción", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); continue; }

                //eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = gvFacturasProveedor.GetRow(x) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                //eProgFact = blFact.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(eProgFact);
            }
            MessageBox.Show("Se actualizaron los documentos de manera satisfactoria.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmFacturasConstanciaDetracc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void gvFacturasProveedor_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvFacturasProveedor_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvFacturasProveedor_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 1 && e.Column.FieldName == "dsc_documento")
                {
                    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = gvFacturasProveedor.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                    if (obj == null) { return; }

                    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                    if (Application.OpenForms["frmMantFacturaProveedor"] != null)
                    {
                        Application.OpenForms["frmMantFacturaProveedor"].Activate();
                    }
                    else
                    {
                        frmModif.MiAccion = Factura.Vista;
                        frmModif.colorVerde = colorVerde;
                        frmModif.colorPlomo = colorPlomo;
                        frmModif.colorFocus = colorFocus;
                        frmModif.colorEventRow = colorEventRow;
                        frmModif.RUC = obj.dsc_ruc;
                        frmModif.tipo_documento = obj.tipo_documento;
                        frmModif.serie_documento = obj.serie_documento;
                        frmModif.numero_documento = obj.numero_documento;
                        frmModif.cod_proveedor = obj.cod_proveedor;
                        frmModif.user.cod_usuario = user.cod_usuario;
                        frmModif.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //public void BuscarFacturas()
        //{
        //    try
        //    {
        //        blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo documentos", "Cargando...");
        //        List<eFacturaProveedor> lista = blFact.FiltroFactura<eFacturaProveedor>(1, cod_tipo_fecha: cod_tipo_fecha, FechaInicio: FechaInicio, FechaFin: FechaFin, cod_proveedor: cod_proveedor);
        //        bsFacturasProveedor.DataSource = lista;
        //        SplashScreenManager.CloseForm();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}
    }
}