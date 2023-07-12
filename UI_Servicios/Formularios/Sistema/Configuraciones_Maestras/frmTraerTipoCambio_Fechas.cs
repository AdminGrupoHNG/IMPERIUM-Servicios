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

namespace UI_Servicios.Formularios.Sistema.Configuraciones_Maestras
{
    public partial class frmTraerTipoCambio_Fechas : DevExpress.XtraEditors.XtraForm
    {
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public string FechaDesde = "", FechaHasta = "";

        public frmTraerTipoCambio_Fechas()
        {
            InitializeComponent();
        }

        private void frmTraerTipoCambio_Fechas_Load(object sender, EventArgs e)
        {
            Inicializar();
        }
        private void Inicializar()
        {
            lblTitulo.ForeColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
            dtFechaDesde.EditValue = DateTime.Today;
            dtFechatHasta.EditValue = DateTime.Today;
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lblTitulo.Select();
            FechaDesde = Convert.ToDateTime(dtFechaDesde.EditValue).ToString("yyyy-MM-dd");
            FechaHasta = Convert.ToDateTime(dtFechatHasta.EditValue).ToString("dd/MM/yyyy");
            this.Close();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

    }
}