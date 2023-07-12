using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_Servicios
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //colorVerde = ConfigurationManager.AppSettings["colorVerde"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            //colorPlomo = ConfigurationManager.AppSettings["colorPlomo"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            //colorEventRow = ConfigurationManager.AppSettings["colorEventRow"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            //colorFocus = ConfigurationManager.AppSettings["colorFocus"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();

            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(DevExpress.LookAndFeel.Basic.DefaultSkin.PineLight);
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(DevExpress.LookAndFeel.SkinStyle.Basic);


            //frmLogin frm = new frmLogin();
            //frm.colorVerde = colorVerde;
            //frm.colorPlomo = colorPlomo;
            //frm.colorEventRow = colorEventRow;
            //frm.colorFocus = colorFocus;
            ////frm.ShowDialog();
            //DialogResult result = frm.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    frmPrincipal frmMain = new frmPrincipal();
            //    frmMain.user = frm.user;
            //    frmMain.colorVerde = colorVerde;
            //    frmMain.colorPlomo = colorPlomo;
            //    frmMain.colorEventRow = colorEventRow;
            //    frmMain.colorFocus = colorFocus;
            //    Application.Run(frmMain);
            //}
            //else
            //{
            //    Application.Exit();
            //}

            Application.Run(new frmSplashScreen());
        }
    }
}
