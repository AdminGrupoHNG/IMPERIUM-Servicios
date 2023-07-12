using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_Servicios
{
    public partial class frmSplashScreen : DevExpress.XtraEditors.XtraForm
    {
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;

        public frmSplashScreen()
        {
            InitializeComponent();
        }

        private void frmSplashScreen_Load(object sender, EventArgs e)
        {
            colorVerde = ConfigurationManager.AppSettings["colorVerde"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            colorPlomo = ConfigurationManager.AppSettings["colorPlomo"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            colorEventRow = ConfigurationManager.AppSettings["colorEventRow"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            colorFocus = ConfigurationManager.AppSettings["colorFocus"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();

            panel2.BackColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 5;

            if (panel2.Width >= 700)
            {
                timer1.Stop();
                frmLogin frm = new frmLogin();
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                //frm.ShowDialog();
                this.Hide();
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    frmPrincipal frmMain = new frmPrincipal();
                    frmMain.user = frm.user;
                    frmMain.eGlobal = frm.eGlobal;
                    frmMain.colorVerde = colorVerde;
                    frmMain.colorPlomo = colorPlomo;
                    frmMain.colorEventRow = colorEventRow;
                    frmMain.colorFocus = colorFocus;
                    //Application.Run(frmMain);
                    frmMain.ShowDialog();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

    }
}