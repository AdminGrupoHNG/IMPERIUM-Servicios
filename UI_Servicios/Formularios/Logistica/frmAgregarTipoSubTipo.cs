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
using BE_Servicios;
using BL_Servicios;
using DevExpress.XtraLayout.Utils;

namespace UI_Servicios.Formularios.Logistica
{
    internal enum AgregarTipoSubTipo
    {
        Tipo = 0,
        SubTipo = 1
    }

    public partial class frmAgregarTipoSubTipo : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        blLogistica blLogis = new blLogistica();
        internal AgregarTipoSubTipo MiAccion = AgregarTipoSubTipo.Tipo;
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public string cod_tipo_servicio = "", cod_subtipo_servicio = "", cod_empresa = "";

        public frmAgregarTipoSubTipo()
        {
            InitializeComponent();
        }

        private void frmAgregarSubTipo_Load(object sender, EventArgs e)
        {
            Inicializar();
            btnGuardar.Appearance.BackColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
        }

        private void Inicializar()
        {
            switch (MiAccion)
            {
                case AgregarTipoSubTipo.Tipo:
                    this.Text = "Agregar Tipo";
                    layoutControlItem1.Text = "Desc. Tipo : ";
                    layoutControlItem13.Visibility = LayoutVisibility.Never;
                    layoutControlItem2.Visibility = LayoutVisibility.Never;
                    blLogis.CargaCombosLookUp("TipoCaracteristica", lkpTipoCaracteristica, "cod_tipo_caracteristica", "dsc_tipo_caracteristica", "", valorDefecto: true);
                    lkpTipoCaracteristica.EditValue = 4;
                    break;
                case AgregarTipoSubTipo.SubTipo:
                    layoutControlItem6.Visibility = LayoutVisibility.Never;
                    blLogis.CargaCombosLookUp("TipoProducto", lkpTipoProducto, "cod_tipo_servicio", "dsc_tipo_servicio", "", valorDefecto: true, cod_empresa: cod_empresa);
                    lkpTipoProducto.EditValue = cod_tipo_servicio;
                    break;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescSubTipo.Text.Trim() == "") { MessageBox.Show("Debe ingresar el nombre del subtipo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDescSubTipo.Focus(); return; }
                eProductos obj = new eProductos();
                obj.cod_tipo_servicio = MiAccion == AgregarTipoSubTipo.Tipo ? "" : lkpTipoProducto.EditValue.ToString();
                obj.dsc_tipo_servicio = MiAccion == AgregarTipoSubTipo.Tipo ? txtDescSubTipo.Text.Trim() : "";
                obj.cod_subtipo_servicio = cod_subtipo_servicio;
                obj.dsc_subtipo_servicio = MiAccion == AgregarTipoSubTipo.Tipo ? "" : txtDescSubTipo.Text.Trim();
                obj.ctd_volumen_m3 = Convert.ToDecimal(txtCantidad.Text);
                obj.flg_activo = chkFlgActivo.CheckState == CheckState.Checked ? "SI" : "NO";
                obj.flg_materia_prima = lkpTipoCaracteristica.EditValue == null ? "" : lkpTipoCaracteristica.EditValue.ToString() == "1" ? "SI" : "NO";
                obj.flg_producto_terminado = lkpTipoCaracteristica.EditValue == null ? "" : lkpTipoCaracteristica.EditValue.ToString() == "2" ? "SI" : "NO";
                obj.flg_actividad_apoyo = lkpTipoCaracteristica.EditValue == null ? "" : lkpTipoCaracteristica.EditValue.ToString() == "3" ? "SI" : "NO";
                obj.flg_producto = lkpTipoCaracteristica.EditValue == null ? "" : lkpTipoCaracteristica.EditValue.ToString() == "4" ? "SI" : "NO";
                obj.flg_servicio = lkpTipoCaracteristica.EditValue == null ? "" : lkpTipoCaracteristica.EditValue.ToString() == "5" ? "SI" : "NO";

                if (MiAccion == AgregarTipoSubTipo.Tipo)
                {
                    obj = blLogis.Insertar_Actualizar_TipoServicio<eProductos>(obj);
                }
                else
                {
                    obj = blLogis.Insertar_Actualizar_SubTipoServicio<eProductos>(obj);
                }

                if (obj == null) { MessageBox.Show("Error al crear registro", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                MessageBox.Show("Se creó el registro de manera satisfactoria.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cod_tipo_servicio = MiAccion == AgregarTipoSubTipo.Tipo ? obj.cod_tipo_servicio.ToString() : "";
                cod_subtipo_servicio = MiAccion == AgregarTipoSubTipo.Tipo ? "" : obj.cod_subtipo_servicio.ToString();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}