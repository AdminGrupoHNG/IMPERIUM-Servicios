using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UI_Servicios.Formularios.Shared
{
    public partial class FrmSplashCarga : SplashScreen
    {
        public FrmSplashCarga()
        {
            InitializeComponent();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
            SkinSplashScreenCommand command = (SkinSplashScreenCommand)cmd;
            if (command == SkinSplashScreenCommand.UpdateLoadingText)
            {
                object[] arr = (object[])arg;
                labelStatus.Text = arr[0].ToString();
                labelCopyright.Text = arr[1].ToString();
            }
        }

        #endregion

        public enum SplashScreenCommand
        {
            SetProgress,
            Command2,
            Command3
        }


    }
}