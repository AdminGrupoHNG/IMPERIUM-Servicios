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
using System.Diagnostics;
using System.Configuration;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace UI_Servicios
{
    public partial class frmAlertaVersion : DevExpress.XtraEditors.XtraForm
    {
        public frmLogin frmHandler = new frmLogin();
        public eUsuario user = new eUsuario();
        blGlobales blGlobal = new blGlobales();
        blVersion blVers = new blVersion();
        eVersion eVersion = new eVersion();
        public string Entorno = "";
        public eVersion objDescargaOrigen;
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public frmAlertaVersion()
        {
            InitializeComponent();
        }
        public frmAlertaVersion(frmLogin frm)
        {
            InitializeComponent();
            frmHandler = frm;
        }

        private void frmAlertaVersion_Load(object sender, EventArgs e)
        {
            //layoutControl1.BackColor = Color.FromArgb(colorVerde[0], colorVerde[1], colorVerde[2]);
            Entorno = "REMOTO"; //ConfigurationManager.AppSettings["conexion"];
            if (Entorno == "REMOTO") lblTipoActualizacion.Text = "Actualización remota";
            //else { lblTipoActualizacion.Text = "Actualización local"; }
                bsAlerta.DataSource = null;
            //bsAlerta.DataSource = blVersion.ObtenerListaDetalle<eVersion>(lblVersion.Text);

            CargarHistorialVersiones(lblVersion.Text.Replace("Versión ", ""));
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                lblTipoActualizacion.Text = "Actualizando, por favor espere... ";
                //lblTipoActualizacion.ForeColor = Color.Red ;
                btnAceptar.Enabled = false;
                btnAceptar.Appearance.ForeColor = System.Drawing.Color.Gray;

                //Eliminar carpeta DESCARGAS
                if (Directory.Exists(@"C:\IMPERIUM-Software\Descargas"))
                {
                    System.IO.Directory.Delete(@"C:\IMPERIUM-Software\Descargas");
                }

                //Crear archivo de lotes
                if (File.Exists(@"C:\IMPERIUM-Software\ActualizaIMPERIUM.bat"))
                {
                    File.Delete(@"C:\IMPERIUM-Software\ActualizaIMPERIUM.bat");
                }

                string Ejecutable = @"C:\IMPERIUM-Software\Imperium-Software.exe";
                string CD = @"CD \ ";
                string ArchivoBAT = "";

                if (Entorno == "REMOTO")
                {
                    string LineaCopia = @"C:\IMPERIUM-Software\Descargas C:\IMPERIUM-Software";
                    string LineaBorra = @"C:\IMPERIUM-Software\Descargas";

                    //ArchivoBAT = "ECHO OFF \nECHO Copiando archivos del sistema...\nTASKKILL /F /IM Imperium-Software.exe\nC:\n" + CD + "\nXCOPY " + LineaCopia + " /s/y/d\nRD " + LineaBorra + " /S /Q\nECHO Ejecutando el sistema...\nSTART " + Ejecutable + "\nEXIT";
                    ArchivoBAT = "ECHO OFF \nECHO Copiando archivos del sistema...\nTASKKILL /F /IM Imperium-Software.exe\nC:\n" + CD + "\nXCOPY " + LineaCopia + " /s/y\nRD /S /Q " + LineaBorra + "\nECHO Ejecutando el sistema...\nSTART " + Ejecutable + "\nEXIT";
                }
                //else
                //{
                //    string LineaCopia = @"\\sl-limfs01\NSV-COLTUR\Sistema C:\IMPERIUM-Software";

                //    ArchivoBAT = "ECHO OFF \nECHO Copiando archivos del sistema...\nTASKKILL /F /IM Imperium-Software.exe\nC:\n" + CD + "\nXCOPY " + LineaCopia + " /s/y/d\nECHO Ejecutando el sistema...\nSTART " + Ejecutable + "\nEXIT";
                //}

                File.WriteAllText(@"C:\IMPERIUM-Software\ActualizaIMPERIUM.bat", ArchivoBAT);


                if (Entorno == "REMOTO")
                {
                    WebClient webClient = new WebClient();

                    if (!Directory.Exists(@"C:\IMPERIUM-Software\Descargas"))
                    {
                        Directory.CreateDirectory(@"C:\IMPERIUM-Software\Descargas");
                    }

                    if (System.IO.File.Exists(@"C:\IMPERIUM-Software\Descargas\IMPERIUM-Software.zip"))
                    {
                        System.IO.File.Delete(@"C:\IMPERIUM-Software\Descargas\IMPERIUM-Software.zip");
                    }

                    objDescargaOrigen = blVers.ObtenerVersion<eVersion>(6);
                    string DescargaOrigen = "";
                    if (objDescargaOrigen != null) DescargaOrigen = objDescargaOrigen.OrigenDescarga;

                    if (DescargaOrigen != "")
                    {
                        webClient.DownloadFileAsync(new Uri(DescargaOrigen), @"C:\IMPERIUM-Software\Descargas\IMPERIUM-Software.zip");
                        webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completado);
                        webClient.DownloadProgressChanged += Wc_DownloadProgressChanged;
                    }
                }
                //else //Produccion, Desarrollo, QA
                //{
                //    frmPrincipal frmMain = new frmPrincipal();
                //    this.Close();
                //    frmMain.Close();
                //    Process.Start(ConfigurationManager.AppSettings["rutaBatActualiza"].ToString());

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encontro el sitio web " + objDescargaOrigen.OrigenDescarga + Environment.NewLine + ex.ToString(), "Acceso no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                frmHandler.Close();
            }
        }
        private void Wc_DownloadProgressChanged(object sender,DownloadProgressChangedEventArgs e)
        {
            progressBarActualizado.EditValue = e.ProgressPercentage;
        }

        public void Completado(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (System.IO.File.Exists(@"C:\IMPERIUM-Software\Descargas\IMPERIUM-Software.zip"))
                {
                    ZipFile.ExtractToDirectory(@"C:\IMPERIUM-Software\Descargas\IMPERIUM-Software.zip", @"C:\IMPERIUM-Software\Descargas");
                }
                if (System.IO.File.Exists(@"C:\IMPERIUM-Software\Descargas\IMPERIUM-Software.zip"))
                {
                    System.IO.File.Delete(@"C:\IMPERIUM-Software\Descargas\IMPERIUM-Software.zip");
                }
                if (System.IO.File.Exists(@"C:\IMPERIUM-Software\Descargas\ActualizaIMPERIUM.bat"))
                {
                    System.IO.File.Delete(@"C:\IMPERIUM-Software\Descargas\ActualizaIMPERIUM.bat");
                }
                this.Close();
                frmHandler.Close();
                Process.Start(ConfigurationManager.AppSettings["rutaBatActualiza"].ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show("No se encontro el sitio web "+ objDescargaOrigen.OrigenDescarga + Environment.NewLine + ex.ToString(), "Acceso no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                frmHandler.Close();
            }
        }

        private void CargarHistorialVersiones(string version)
        {
            List<eVersion.eVersionDetalle> histVersion = blVers.Cargar_HistorialVersiones_Detalle<eVersion.eVersionDetalle>(3, 0, version);

            bsListadoHistorialDetalle.DataSource = histVersion;
        }

        private void gvHistorialVersiones_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvHistorialVersiones_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvHistorialVersiones_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvHistorialVersiones_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }
    }
}