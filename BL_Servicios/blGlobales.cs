using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Mail;
//using MailKit.Net.Smtp;
//using Microsoft.Office.Interop.Outlook;

namespace BL_Servicios
{
    public class blGlobales
    {
        int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        //colorVerde = ConfigurationManager.AppSettings["colorVerde"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
        //colorPlomo = ConfigurationManager.AppSettings["colorPlomo"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
        //colorEventRow = ConfigurationManager.AppSettings["colorEventRow"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
        //colorFocus = ConfigurationManager.AppSettings["colorFocus"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
        public void pKeyDown(TextEdit sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                sender.Text = "";
            }
        }

        public string pKeyPress(TextEdit sender, KeyPressEventArgs e)
        {
            string sAux = "";
            if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 45 || e.KeyChar == 32))
            {
                sAux = e.KeyChar.ToString();
                if (e.KeyChar == 45 || e.KeyChar == 32) { sAux = ""; }
                e.Handled = true;
            }
            return sAux;
        }

        public void Pintar_CabeceraColumnas(ColumnHeaderCustomDrawEventArgs e)
        {
            colorVerde = ConfigurationManager.AppSettings["colorVerde"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            if (e.Column == null) return;
            System.Drawing.Rectangle rect = e.Bounds;
            rect.Inflate(-1, -1);
            e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2])), rect);
            e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
            foreach (DrawElementInfo info in e.Info.InnerElements)
            {
                if (!info.Visible) continue;
                ObjectPainter.DrawObject(e.Cache, info.ElementPainter, info.ElementInfo);
            }
            e.Handled = true;
        }

        public void Pintar_CabeceraColumnasBandHeader(BandHeaderCustomDrawEventArgs e)
        {
            colorVerde = ConfigurationManager.AppSettings["colorVerde"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            if (e.Band == null) return;
            System.Drawing.Rectangle rect = e.Bounds;
            rect.Inflate(-1, -1);
            e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2])), rect);
            e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
            foreach (DrawElementInfo info in e.Info.InnerElements)
            {
                if (!info.Visible) continue;
                ObjectPainter.DrawObject(e.Cache, info.ElementPainter, info.ElementInfo);
            }
            e.Handled = true;
        }

        public void Pintar_EstiloGrilla(object sender, RowStyleEventArgs e)
        {
            colorEventRow = ConfigurationManager.AppSettings["colorEventRow"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            colorFocus = ConfigurationManager.AppSettings["colorFocus"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            GridView view = sender as GridView;
            if (view.Columns["flg_activo"] != null)
            {
                string estado = view.GetRowCellDisplayText(e.RowHandle, view.Columns["flg_activo"]);
                if (estado == "NO") e.Appearance.ForeColor = Color.Red;
            }
            view.Appearance.EvenRow.BackColor = Color.FromArgb(colorEventRow[0], colorEventRow[1], colorEventRow[2]);
            view.Appearance.FocusedRow.BackColor = Color.FromArgb(colorFocus[0], colorFocus[1], colorFocus[2]);
            view.Appearance.FocusedRow.FontStyleDelta = FontStyle.Bold; view.Appearance.FocusedRow.ForeColor = Color.Black;
            view.Appearance.FocusedCell.BackColor = Color.FromArgb(colorFocus[0], colorFocus[1], colorFocus[2]);
            view.Appearance.FocusedCell.FontStyleDelta = FontStyle.Bold; view.Appearance.FocusedCell.ForeColor = Color.Black;
            view.Appearance.HideSelectionRow.BackColor = Color.FromArgb(colorFocus[0], colorFocus[1], colorFocus[2]);
            view.Appearance.HideSelectionRow.FontStyleDelta = FontStyle.Bold; view.Appearance.HideSelectionRow.ForeColor = Color.Black;
            view.Appearance.SelectedRow.BackColor = Color.FromArgb(colorFocus[0], colorFocus[1], colorFocus[2]);
            view.Appearance.SelectedRow.FontStyleDelta = FontStyle.Bold; view.Appearance.SelectedRow.ForeColor = Color.Black;
        }


        public void Abrir_SplashScreenManager(Type splashFormType, string sTitulo, string sSubTitulo = "Cargando...")
        {
            SplashScreenManager.ShowForm(splashFormType);
            string[] oDatos = { sTitulo, sSubTitulo };
            SplashScreenManager.Default.SendCommand(SkinSplashScreenCommand.UpdateLoadingText, oDatos);
        }

        //public Boolean EnviarCorreoElectronico_Outlook(string mailDirection, string mailSubject, string mailContent)
        //{
        //    try
        //    {
        //        var oApp = new Microsoft.Office.Interop.Outlook.Application();
        //        Microsoft.Office.Interop.Outlook.NameSpace ns = oApp.GetNamespace("MAPI");
        //        var f = ns.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
        //        System.Threading.Thread.Sleep(1000);

        //        var mailItem = (Microsoft.Office.Interop.Outlook.MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
        //        mailItem.Subject = mailSubject;
        //        mailItem.HTMLBody = mailContent;
        //        mailItem.To = mailDirection;
        //        mailItem.Send();
        //        MessageBox.Show("El correo fue enviado, revise su bandeja de entrada.", "Recuperación de clave", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show("El correo no pudo ser enviado.", "Recuperación de clave", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return false;
        //    }
        //    finally
        //    {
        //    }
        //    return true;
        //}

        public Boolean EnviarCorreoElectronico_SMTP(string sDestinatario, string sAsunto, string sCuerpo, string RutasAdjunto = "")
        {
            blSistema blSis = new blSistema();
            List<BE_Servicios.eSistema> eSist = blSis.Obtener_ParamterosSistema<BE_Servicios.eSistema>(11);
            String sCredencialUsuario = "", sCredencialClave="";
            if (eSist.Count > 0)
            {
                sCredencialUsuario = eSist[0].dsc_clave;
                sCredencialClave = eSist[0].dsc_valor;
            }

            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            correo.To.Add(new System.Net.Mail.MailAddress(sDestinatario));
            correo.From = new System.Net.Mail.MailAddress(sCredencialUsuario);
            correo.Subject = sAsunto;
            correo.Body = sCuerpo;
            correo.IsBodyHtml = false;

            using (var client = new System.Net.Mail.SmtpClient("smtp.office365.com", 587))
            {
                client.Credentials = new NetworkCredential(sCredencialUsuario, sCredencialClave);
                client.EnableSsl = true;
                try
                {
                    client.Send(correo);
                    MessageBox.Show("El correo fue enviado, revise su bandeja de entrada.", "Recuperación de clave", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El correo no pudo ser enviado.", "Recuperación de clave", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }


    }
}
