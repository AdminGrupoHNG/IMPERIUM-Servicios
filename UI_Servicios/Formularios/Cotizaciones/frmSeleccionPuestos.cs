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
    public partial class frmSeleccionPuestos : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        blAnalisisServicio blAns = new blAnalisisServicio();
        blGlobales blGlobal = new blGlobales();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public string cargo;
        public int item;
        public List<eAnalisis.eAnalisis_Personal_Uniformes> lstUnifEpp;
        public List<eAnalisis.eAnalisis_Personal_Uniformes> lstUnifTemp = new List<eAnalisis.eAnalisis_Personal_Uniformes>();
        public List<eAnalisis.eAnalisis_Personal> lstCargos = new List<eAnalisis.eAnalisis_Personal>();

        public frmSeleccionPuestos()
        {
            InitializeComponent();
        }

        private void frmSeleccionPuestos_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            List<eAnalisis.eAnalisis_Personal> lstCargosTemp = new List<eAnalisis.eAnalisis_Personal>();
            lstCargosTemp.AddRange(lstCargos);
            lstCargosTemp.RemoveAll(x => x.cod_cargo == cargo && x.num_item == item);
            bsPuestos.DataSource = lstCargosTemp;
        }

        private void chkSeleccionarTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionarTodos.Checked)
            {
                foreach (eAnalisis.eAnalisis_Personal obj in lstCargos)
                {
                    obj.sel = true;
                }
            }
            else
            {
                foreach (eAnalisis.eAnalisis_Personal obj in lstCargos)
                {
                    obj.sel = false;
                }
            }

            gvPuestos.RefreshData();
        }

        private void gvPuestos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gvPuestos.PostEditor(); gvPuestos.RefreshData();
                eAnalisis.eAnalisis_Personal obj = gvPuestos.GetFocusedRow() as eAnalisis.eAnalisis_Personal;

                btnConfirmar_Click(sender, e);
            }
        }

        private void gvPuestos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvPuestos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            int sel = lstUnifTemp.Count(x => x.sel);

            for (int x = 0; x < lstUnifTemp.Count; x++)
            {
                eAnalisis.eAnalisis_Personal_Uniformes obj = lstUnifTemp[x];

                foreach (eAnalisis.eAnalisis_Personal per in lstCargos)
                {
                    if (per.sel)
                    {
                        if (obj.sel || sel == 0)
                        {
                            if (per.cod_cargo != cargo && per.num_item != item)
                            {
                                eAnalisis.eAnalisis_Personal_Uniformes obj2 = new eAnalisis.eAnalisis_Personal_Uniformes();

                                obj2 = lstUnifEpp.Find(u => u.cod_cargo == per.cod_cargo && u.num_item == per.num_item && u.cod_producto == obj.cod_producto);

                                if (obj2 == null)
                                {
                                    obj2 = new eAnalisis.eAnalisis_Personal_Uniformes();

                                    obj2.cod_cargo = per.cod_cargo;
                                    obj2.num_item = per.num_item;
                                    obj2.dsc_cargo = per.dsc_cargo;
                                    obj2.cod_producto = obj.cod_producto;
                                    obj2.dsc_producto = obj.dsc_producto;
                                    obj2.cod_tipo_servicio = obj.cod_tipo_servicio;
                                    obj2.dsc_tipo_servicio = obj.dsc_tipo_servicio;
                                    obj2.cod_subtipo_servicio = obj.cod_subtipo_servicio;
                                    obj2.dsc_subtipo_servicio = obj.dsc_subtipo_servicio;
                                    obj2.cod_unidad_medida = obj.cod_unidad_medida;
                                    obj2.dsc_simbolo = obj.dsc_simbolo;
                                    obj2.imp_unitario = obj.imp_unitario;
                                    obj2.prc_margen = obj.prc_margen;
                                    obj2.num_cantidad = obj.num_cantidad;
                                    obj2.imp_total = obj2.num_cantidad * obj.imp_unitario;
                                    obj2.imp_venta = obj2.imp_total * (1 + obj2.prc_margen / 100);

                                    lstUnifEpp.Add(obj2);
                                }
                                else
                                {
                                    obj2.num_cantidad = obj.num_cantidad;
                                }
                            }
                        }
                    }
                }
            }

            MessageBox.Show("Operación realizada de manera éxitosa.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

    }
}