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
using System.Configuration;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraSplashScreen;
using System.Net.Http;
using System.Net;
using RestSharp;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace UI_Servicios.Formularios.Sistema.Configuraciones_Maestras
{
    public partial class frmMantTipoCambio : DevExpress.XtraEditors.XtraForm
    {
        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        blEncrypta blEncryp = new blEncrypta();
        blVersion blVers = new blVersion();
        public blFactura blFact = new blFactura();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;

        public frmMantTipoCambio()
        {
            InitializeComponent();
        }

        private void frmMantTipoCambio_Load(object sender, EventArgs e)
        {
            simpleLabelItem1.AppearanceItemCaption.ForeColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
            dtAnho.EditValue = DateTime.Today;
            dtMes.EditValue = DateTime.Today;
            Obtener_ListaTipoCambio();
        }
        private void Obtener_ListaTipoCambio()
        {
            List<eTipoCambio> listaTipoCambio = new List<eTipoCambio>();
            listaTipoCambio = blFact.FiltroFactura<eTipoCambio>(20, Anho: Convert.ToDateTime(dtAnho.EditValue).Year, Mes: Convert.ToDateTime(dtMes.EditValue).Month);
            bsTipoCambio.DataSource = listaTipoCambio;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo datos", "Cargando...");
            Obtener_ListaTipoCambio();
            SplashScreenManager.CloseForm();
        }

        private void btnTraerTipoCambio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmTraerTipoCambio_Fechas frm = new frmTraerTipoCambio_Fechas();
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorFocus = colorFocus;
            frm.colorEventRow = colorEventRow;
            frm.ShowDialog();
            if (frm.FechaDesde != "" && frm.FechaHasta != "")
            {
                blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo en SUNAT", "Cargando...");
                //Consulta_TipoCambio(frm.FechaDesde, frm.FechaHasta);
                Consulta2_TipoCambio(frm.FechaDesde);
                SplashScreenManager.CloseForm();
            }
        }

        string captcha = "";
        CookieContainer cokkie = new CookieContainer();
        string[] nrosRuc = new string[] { };
        string texto = "";
        private void Consulta2_TipoCambio(string FechaDesde)
        {
            string endpoint = @"https://api.apis.net.pe/v1/tipo-cambio-sunat?fecha=" + FechaDesde;
            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(endpoint);
            myWebRequest.CookieContainer = cokkie;

            //myWebRequest.Method = "POST";
            //myWebRequest.ContentType = "application/x-www-form-urlencoded";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            HttpWebResponse myhttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            Stream myStream = myhttpWebResponse.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myStream);

            string xDat = "";
            string validar = "";

            while (!myStreamReader.EndOfStream)
            {
                xDat = myStreamReader.ReadLine();
                if (xDat != "")
                {
                    string Datos = xDat;
                    char[] separadores = { ' ', ',', ':'};
                    string[] palabras = Datos.Replace("\"", "").Replace("{", "").Replace("}", "").Split(separadores);

                    eTipoCambio obj = new eTipoCambio();
                    obj.imp_cambio_compra = Convert.ToDecimal(palabras[1]);
                    obj.imp_cambio_venta = Convert.ToDecimal(palabras[3]);
                    obj.cod_moneda = "DOL";
                    obj.fch_cambio = Convert.ToDateTime(palabras[9]);
                    //obj.fch_cambio = Convert.ToDateTime(FechaDesde);
                    eTipoCambio eObj = blFact.InsertarTipoCambio<eTipoCambio>(obj);
                    if (eObj == null) { MessageBox.Show("Error al insertar tipo da cambio para el día " + palabras[9], "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); Obtener_ListaTipoCambio(); }

                    validar = "OK";
                }
            }//TERMINA EL WHILE

            if (validar == "OK")
            {
                MessageBox.Show("Proceso terminado.", "Traer Tipo Cambio SUNAT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Obtener_ListaTipoCambio();
            }
            else
            {
                MessageBox.Show("Error al traer tipo de cambio.", "Traer Tipo Cambio SUNAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void Consulta_TipoCambio(string FechaDesde, string FechaHasta)
        {
            eSistema objLink = blVers.ObtenerVersion<eSistema>(7); //Link Descarga
            eSistema objTC = blVers.ObtenerVersion<eSistema>(10); //Token TIPO CAMBIO
            var client = new RestClient(objLink.dsc_valor);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            EnvioJSONTipoCambio datosTC = new EnvioJSONTipoCambio()
            {
                token = objTC.dsc_valor,
                tipo_cambio = new TipoCambio()
                {
                    moneda = "PEN",
                    fecha_inicio = FechaDesde,
                    fecha_fin = FechaHasta
                    //fecha_inicio = "20/03/2021",
                    //fecha_fin = "25/03/2021"
                }
            };

            var eJSON = "";
            eJSON = JsonConvert.SerializeObject(datosTC);
            request.AddHeader("Authorization", "Basic token");
            request.AddParameter("application/json", eJSON, ParameterType.RequestBody);
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            JavaScriptSerializer js = new JavaScriptSerializer();
            ObtenerJSONTipoCambio objListResult = JsonConvert.DeserializeObject<ObtenerJSONTipoCambio>(response.Content);

            if (objListResult.exchange_rates.Count > 0)
            {
                foreach (TipoCambio_Result objResult in objListResult.exchange_rates)
                {
                    eTipoCambio obj = new eTipoCambio();
                    obj.fch_cambio = Convert.ToDateTime(objResult.fecha); obj.imp_cambio_compra = objResult.compra; obj.imp_cambio_venta = objResult.venta;
                    obj.cod_moneda = "DOL";
                    eTipoCambio eObj = blFact.InsertarTipoCambio<eTipoCambio>(obj);
                    if (eObj == null) { MessageBox.Show("Error al insertar tipo da cambio para el día " + objResult.fecha, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); Obtener_ListaTipoCambio(); }
                }
                MessageBox.Show("Proceso terminado.", "Traer Tipo Cambio SUNAT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Obtener_ListaTipoCambio();
            }
            else
            {
                MessageBox.Show("Error al traer tipo de cambio.", "Traer Tipo Cambio SUNAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public class EnvioJSONTipoCambio
        {
            public string token { get; set; }
            public TipoCambio tipo_cambio { get; set; }
        }

        public class TipoCambio
        {
            public string moneda { get; set; }
            public string fecha_inicio { get; set; }
            public string fecha_fin { get; set; }
        }
        public class ObtenerJSONTipoCambio
        {
            //public string success { get; set; }
            public List<TipoCambio_Result> exchange_rates { get; set; }
        }
        public class TipoCambio_Result
        {
            public string fecha { get; set; }
            public string moneda { get; set; }
            public decimal compra { get; set; }
            public decimal venta { get; set; }
        }

        //private void ConsultaSUNAT_Nuevo()
        //{
        //    //string endpoint = "https://dni.optimizeperu.com/api/tipo-cambio";
        //    string endpoint = @"https://e-consulta.sunat.gob.pe/cl-at-ittipcam/tcS01Alias?accion=consTipoCambio&TipoCambio=" + DateTime.Today.ToShortTimeString();
        //    HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(endpoint);
        //    CookieContainer cokkie = new CookieContainer();
        //    myWebRequest.CookieContainer = cokkie;

        //    //myWebRequest.Method = "POST";
        //    //myWebRequest.ContentType = "application/x-www-form-urlencoded";

        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
        //    HttpWebResponse myhttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
        //    Stream myStream = myhttpWebResponse.GetResponseStream();
        //    StreamReader myStreamReader = new StreamReader(myStream);
        //    string xDat = "";
        //    string dato = "", xml = "";

        //    while (!myStreamReader.EndOfStream)
        //    {
        //        xDat = myStreamReader.ReadLine();
        //        xml = xml + Environment.NewLine + xDat;
        //    }
        //}
        private void btnExportarExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportarExcel();
        }
        private void ExportarExcel()
        {
            try
            {
                string carpeta = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\TipoCambio" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                gvTipoCambio.ExportToXlsx(archivo);
                if (MessageBox.Show("Excel exportado en la ruta " + archivo + Environment.NewLine + "¿Desea abrir el archivo?", "Exportar Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(archivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvTipoCambio.ShowPrintPreview();
        }

        private void gvTipoCambio_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void dtMes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) dtMes.EditValue = null;
        }

        private void gvTipoCambio_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvTipoCambio_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            eTipoCambio obj = gvTipoCambio.GetFocusedRow() as eTipoCambio;
            obj.fch_cambio = DateTime.Today;
        }

        private void gvTipoCambio_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                eTipoCambio obj = gvTipoCambio.GetFocusedRow() as eTipoCambio;
                obj.cod_moneda = "DOL";
                eTipoCambio eObj = blFact.InsertarTipoCambio<eTipoCambio>(obj);
                if (eObj == null) { MessageBox.Show("Error al insertar tipo da cambio", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); Obtener_ListaTipoCambio(); }
                gvTipoCambio.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}