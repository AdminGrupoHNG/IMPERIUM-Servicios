using BE_Servicios;
using BL_Servicios;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using UI_Servicios.Clientes_Y_Proveedores.Clientes;
using UI_Servicios.Formularios.Shared;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;
using System.Reflection;
using DevExpress.XtraRichEdit;
using UI_Servicios.Tools;
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraPrinting;
using System.Drawing.Imaging;
using System.IO;
using DevExpress.XtraSplashScreen;

namespace UI_Servicios.Formularios.Cotizaciones
{
    internal enum Analisis
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }

    public partial class frmMantAnalisisServicio : DevExpress.XtraEditors.XtraForm
    {
        internal Analisis accion = Analisis.Nuevo;
        public const string sNullable = "Nullable`1";

        public eUsuario user = new eUsuario();
        blAnalisisServicio blAns = new blAnalisisServicio();
        blProveedores blProv = new blProveedores();
        blGlobales blGlobal = new blGlobales();

        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
        public String empresa, sedeEmpresa, analisis, codigoCliente;
        public int sedeCliente, servicio;

        decimal sueldoG = 0, socialesG = 0, beneficiosG = 0, gastosG = 0, descanseroG = 0, productosG = 0;
        Boolean gAlcs = false, gPers = false, gProd = false, gMqEq = false, gOtrs = false, gMgCt = false;
        decimal totalPersonal, totalProductos, totalMaqEquipos, totalOtros, totalGeneral;
        decimal totalProductosMg, totalMaqEquiposMg, totalOtrosMg, totalGeneralMg;
        decimal mrgPro, mrgMaq, mrgOtrs, mrgTot;

        String mensaje;
        List<eFormatoCotizacion> lstFormatoCotizacion; //LDAC - Se agregó para visualizar propuesta técnica
        List<eDatos> lstGenerales;
        List<eDatos> lstDatos;
        List<eDatos> lstTipos;
        List<eAnalisis.eAnalisis_Sedes_Prestacion> lstAns_SedesPrestacion;
        List<eAnalisis.eAnalisis_Personal> lstPerAns;
        List<eAnalisis.eAnalisis_Personal_Uniformes> lstUnifEpp;
        List<eAnalisis.eAnalisis_Producto> lstProdAns;
        List<eAnalisis.eAnalisis_Maquinaria> lstMaqAns;
        List<eAnalisis.eAnalisis_Otros> lstOtrAns;
        List<eAnalisis.eAnalisis_Est_Cst> lstTot = new List<eAnalisis.eAnalisis_Est_Cst>();
        List<eAnalisis.eAnalisis_Est_Cst> lstCst = new List<eAnalisis.eAnalisis_Est_Cst>(); //-> Esta variable se va llenando en los métodos de CargarMrgCst
        List<eAnalisis.eAnalisis_Personal_Sedes> lstPerSedes = new List<eAnalisis.eAnalisis_Personal_Sedes>();
        List<eAnalisis.eAnalisis_Alcance> lstAlcance = new List<eAnalisis.eAnalisis_Alcance>();

        DataTable dtGeneral;

        List<eAnalisis.eAnalisis_Alcance> lstAlcAnsElim = new List<eAnalisis.eAnalisis_Alcance>();
        List<eAnalisis.eAnalisis_Personal> lstPerAnsElim = new List<eAnalisis.eAnalisis_Personal>();
        List<eAnalisis.eAnalisis_Personal_Uniformes> lstUniAnsElim = new List<eAnalisis.eAnalisis_Personal_Uniformes>();
        List<eAnalisis.eAnalisis_Producto> lstProdAnsElim = new List<eAnalisis.eAnalisis_Producto>();
        List<eAnalisis.eAnalisis_Maquinaria> lstMaqAnsElim = new List<eAnalisis.eAnalisis_Maquinaria>();
        List<eAnalisis.eAnalisis_Otros> lstOtrosElim = new List<eAnalisis.eAnalisis_Otros>();

        public frmMantAnalisisServicio()
        {
            InitializeComponent();
        }

        private void frmMantPresupuesto_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            CargarLookUpEdit();
            ConfigurarDateEdit();
            ConfigurarForm();
        }

        private void CargarLookUpEdit()
        {
            lstGenerales = blAns.ListarGeneral<eDatos>("Generales");

            blAns.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: user.cod_usuario);
            List<eFacturaProveedor> list = blProv.ListarEmpresasProveedor<eFacturaProveedor>(11, "", user.cod_usuario);
            lkpEmpresa.EditValue = list[0].cod_empresa;

            blAns.CargaCombosLookUp("Sedes", lkpSedeEmpresa, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());
            lkpSedeEmpresa.EditValue = "00001";

            blAns.CargaCombosLookUp("TipoPrestacion", lkpTipoServicio, "num_servicio", "dsc_tipo_prestacion", "", valorDefecto: true, cod_empresa: empresa, cod_sede_empresa: sedeEmpresa, cod_analisis: analisis, cod_sede_cliente: sedeCliente);
            blAns.CargaCombosLookUp("Tiempo", lkpTiempo, "cod_tiempo", "dsc_tiempo", "", valorDefecto: true);
            lkpTiempo.EditValue = "A";

            rlkpTurno.DataSource = blAns.ListarGeneral<eDatos>("Turnos");
            rlkpRefrigerio.DataSource = blAns.ListarGeneral<eDatos>("Minutos");
            rlkpConcepto.DataSource = blAns.ListarGeneral<eDatos>("Conceptos");

            List<eDatos> lstDotaciones = blAns.ListarGeneral<eDatos>("Dotaciones");
            rlkpDotacion.DataSource = lstDotaciones;
            rlkpDotacionRes.DataSource = lstDotaciones;
        }

        private void ConfigurarDateEdit()
        {
            DateTime date = DateTime.Now;
            dtpFechaRequerimiento.EditValue = date;
        }

        private void ConfigurarForm()
        {
            switch (accion)
            {
                case Analisis.Nuevo:
                    break;
                case Analisis.Editar:
                    CargarAnalisis();
                    break;
                case Analisis.Vista:
                    CargarAnalisis();
                    BloquearCampos();
                    break;
            }
        }

        private void CargarAnalisis(Boolean cargarSede = true)
        {
            lstAns_SedesPrestacion = blAns.ListarAnalisis<eAnalisis.eAnalisis_Sedes_Prestacion>(2, empresa, sedeEmpresa, analisis);

            lkpEmpresa.EditValue = lstAns_SedesPrestacion[0].cod_empresa;
            lkpSedeEmpresa.EditValue = lstAns_SedesPrestacion[0].cod_sede_empresa;
            this.Text = "Análisis de Cotización - " + lstAns_SedesPrestacion[0].cod_analisis;
            codigoCliente = lstAns_SedesPrestacion[0].cod_cliente;
            txtCliente.EditValue = lstAns_SedesPrestacion[0].dsc_cliente;
            lkpTipoServicio.EditValue = servicio;
            dtpFechaRequerimiento.EditValue = lstAns_SedesPrestacion[0].fch_requerimiento;

            eAnalisis.eAnalisis_Sedes_Prestacion eServicio = lstAns_SedesPrestacion.Find(x => x.num_servicio == servicio);

            txtPeriodo.EditValue = eServicio.dsc_periodo == null || eServicio.dsc_periodo == "" || String.IsNullOrEmpty(lstAns_SedesPrestacion[0].dsc_periodo) ? "1" : lstAns_SedesPrestacion[0].dsc_periodo.Substring(0, 1);
            lkpTiempo.EditValue = eServicio.dsc_periodo == null || eServicio.dsc_periodo == "" || String.IsNullOrEmpty(lstAns_SedesPrestacion[0].dsc_periodo) ? "A" : lstAns_SedesPrestacion[0].dsc_periodo.Substring(2, 1);
            txtMetros2.EditValue = eServicio.num_m2;
            txtMetros3.EditValue = eServicio.num_m3;

            lstFormatoCotizacion = new List<eFormatoCotizacion>();//LDAC - Capturar datos de cotización
            string fechaActual = String.Concat(DateTime.Now.ToString("dddd dd MMMM"), " del ", DateTime.Now.ToString("yyyy"));
            eFormatoCotizacion formato = new eFormatoCotizacion()
            {
                dsc_nombre_cliente = eServicio.dsc_cliente,
                anio = int.Parse(txtPeriodo.EditValue.ToString().Substring(0, 1)),
                dsc_anio = LeerNumeros(int.Parse(txtPeriodo.EditValue.ToString().Substring(0, 1))),
                dsc_direccion = eServicio.dsc_cadena_direccion,
                dsc_documento = lstAns_SedesPrestacion[0].dsc_documento_cliente,
                dsc_sede = eServicio.dsc_sede_cliente,
                dsc_fecha = char.ToUpper(fechaActual[0]) + fechaActual.Substring(1),
            };
            lstFormatoCotizacion.Add(formato);

            if (cargarSede) CargarSedes(lstAns_SedesPrestacion);
        }

        private void CargarSedes(List<eAnalisis.eAnalisis_Sedes_Prestacion> lstAns = null)
        {
            List<eCliente_Direccion> lstSedesCli = blAns.ListarGeneral<eCliente_Direccion>("SedesCliente", cliente: codigoCliente);

            if (lstAns == null)
            {
                bsSedesCliente.DataSource = lstSedesCli;
            }
            else
            {
                List<eCliente_Direccion> lstSedAns = new List<eCliente_Direccion>();

                foreach (eCliente_Direccion obj in lstSedesCli)
                {
                    foreach (eAnalisis.eAnalisis_Sedes_Prestacion obj2 in lstAns)
                    {
                        if (obj.num_linea == obj2.cod_sede_cliente)
                        {
                            lstSedAns.Add(obj);
                            break;
                        }
                    }
                }

                bsSedesCliente.DataSource = lstSedAns;
            }
        }

        private void CargarGrillas()
        {
            lstPerAns = blAns.ListarAnalisis<eAnalisis.eAnalisis_Personal>(4, empresa, sedeEmpresa, analisis, servicio: servicio);
            lstUnifEpp = blAns.ListarAnalisis<eAnalisis.eAnalisis_Personal_Uniformes>(9, empresa, sedeEmpresa, analisis, servicio: servicio);
            lstProdAns = blAns.ListarAnalisis<eAnalisis.eAnalisis_Producto>(3, empresa, sedeEmpresa, analisis, servicio: servicio);
            lstMaqAns = blAns.ListarAnalisis<eAnalisis.eAnalisis_Maquinaria>(11, empresa, sedeEmpresa, analisis, servicio: servicio);
            lstOtrAns = blAns.ListarAnalisis<eAnalisis.eAnalisis_Otros>(8, empresa, sedeEmpresa, analisis, servicio: servicio);
            lstAlcance = blAns.ListarAnalisis<eAnalisis.eAnalisis_Alcance>(5, empresa, sedeEmpresa, analisis, servicio: servicio);

            if (lstOtrAns.Count == 0) lstOtrAns = blAns.ListarGeneral<eAnalisis.eAnalisis_Otros>("Otros");
            if (lstTot.Count == 0) lstTot = blAns.ListarGeneral<eAnalisis.eAnalisis_Est_Cst>("Totales");

            bsPersonalAnalisis.DataSource = lstPerAns;
            bsProductoAnalisis.DataSource = lstProdAns;
            bsMaqEquiposAnalisis.DataSource = lstMaqAns;
            bsOtrosAnalisis.DataSource = lstOtrAns;
            bsAlcanceAnalisis.DataSource = lstAlcance;

            CargarOtros();
            CargarMontosPersonal();
            CargarUniformes();
            CargarTiposProducto();
            CargarMontosProductos();
            CargarMontosMaquinas();
            CargarTotales();
        }

        private void CargarOtros(Boolean montos = true)
        {
            int cantidad = lstPerAns.Sum(x => x.num_cantidad);
            decimal sueldo = (lstPerAns.Sum(x => x.imp_salario_total - (x.num_cantidad * x.imp_movilidad)) + lstPerAns.Sum(x => (x.num_cantidad * lstGenerales[1].num_valor)));

            foreach (eAnalisis.eAnalisis_Otros obj in lstOtrAns)
            {
                switch (obj.dsc_descripcion)
                {
                    case "SEGURO COMPLEMENTARIO (SCTR)": obj.num_cantidad = cantidad; obj.imp_unitario = 0; obj.imp_total = (sueldo * obj.prc_ley) / 100; obj.imp_venta = obj.imp_total + ((obj.imp_total * obj.prc_margen) / 100); break;
                    case "SCTR SOCAVON": obj.num_cantidad = cantidad; obj.imp_unitario = 0; obj.imp_total = (sueldo * obj.prc_ley) / 100; obj.imp_venta = obj.imp_total + ((obj.imp_total * obj.prc_margen) / 100); break;
                    case "SCTR ALTURA": obj.num_cantidad = cantidad; obj.imp_unitario = 0; obj.imp_total = (sueldo * obj.prc_ley) / 100; obj.imp_venta = obj.imp_total + ((obj.imp_total * obj.prc_margen) / 100); break;
                    case "SEGURO VIDA LEY": obj.num_cantidad = cantidad; obj.imp_total = obj.imp_unitario * obj.num_cantidad; obj.imp_venta = obj.imp_total + ((obj.imp_total * obj.prc_margen) / 100); break;
                    case "EXÁMENES MÉDICOS OCUPACIONALES (LEY 29783)": obj.num_cantidad = cantidad; obj.imp_total = obj.imp_unitario * obj.num_cantidad; obj.imp_venta = obj.imp_total + ((obj.imp_total * obj.prc_margen) / 100); break;
                    case "POLIZAS RC/DH": obj.num_cantidad = cantidad; obj.imp_total = obj.imp_unitario * obj.num_cantidad; obj.imp_venta = obj.imp_total + ((obj.imp_total * obj.prc_margen) / 100); break;
                    case "OTROS CONCEPTOS (SIG)": obj.num_cantidad = cantidad; obj.imp_total = obj.imp_unitario * obj.num_cantidad; obj.imp_venta = obj.imp_total + ((obj.imp_total * obj.prc_margen) / 100); break;
                }
            }

            gvOtros.RefreshData();
            if (montos == true) CargarMontosOtros();
        }

        private void CargarMontosPersonal()
        {
            totalGeneral = totalGeneral - totalPersonal;
            totalGeneralMg = totalGeneralMg - totalPersonal;

            totalPersonal = 0;

            GenerarRemuneracion("");
            GenerarLeyesSociales("");
            GenerarBeneficiosSociales("");
            GenerarGastosPersonal("");

            totalPersonal = sueldoG + socialesG + beneficiosG + gastosG;

            totalGeneral = totalGeneral + totalPersonal;
            totalGeneralMg = totalGeneralMg + totalPersonal;

            tbiPersonal.Elements[1].Text = (totalPersonal != 0 ? "S/ " + $"{Math.Round(totalPersonal, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");
            tbiPersonal.Elements[2].Text = (totalPersonal != 0 ? "S/ " + $"{Math.Round(totalPersonal, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");

            tbiTotal.Elements[1].Text = (totalGeneral != 0 ? "S/ " + $"{Math.Round(totalGeneral, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");
            tbiTotal.Elements[2].Text = (totalGeneralMg != 0 ? "S/ " + $"{Math.Round(totalGeneralMg, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");

            gvPuestos.RefreshData();
        }

        private void CargarUniformes()
        {
            foreach (eAnalisis.eAnalisis_Personal obj in lstPerAns)
            {
                foreach (eAnalisis.eAnalisis_Personal_Uniformes uni in lstUnifEpp)
                {
                    if (obj.cod_cargo == uni.cod_cargo && obj.num_item == uni.num_item)
                    {
                        obj.flg_uniforme = true;
                        break;
                    }
                }
            }
        }

        private void CargarTiposProducto()
        {
            decimal imp_total = 0, imp_venta = 0;

            lstTipos = new List<eDatos>();

            lstProdAns = lstProdAns.OrderBy(x => x.cod_tipo_servicio).ToList();

            foreach (eAnalisis.eAnalisis_Producto obj in lstProdAns)
            {
                eDatos eTip = new eDatos(); eDatos eTip2 = new eDatos();
                eTip.AtributoUno = obj.cod_tipo_servicio;
                eTip.AtributoDos = obj.dsc_tipo_servicio;
                eTip.AtributoOnce = obj.prc_margen;
                eTip.cod_dotacion = obj.cod_dotacion;

                eTip2 = lstTipos.Find(x => x.AtributoDos == obj.dsc_tipo_servicio);

                if (eTip2 == null)
                {
                    imp_total = 0;
                    imp_venta = 0;

                    lstTipos.Add(eTip);

                    switch (obj.cod_dotacion)
                    {
                        case "A": imp_total = (obj.imp_total / 12) + imp_total; imp_venta = (obj.imp_venta / 12) + imp_venta; break;
                        case "B": imp_total = (obj.imp_total / 6) + imp_total; imp_venta = (obj.imp_venta / 6) + imp_venta; break;
                        case "T": imp_total = (obj.imp_total / 4) + imp_total; imp_venta = (obj.imp_venta / 4) + imp_venta; break;
                        case "S": imp_total = (obj.imp_total / 2) + imp_total; imp_venta = (obj.imp_venta / 2) + imp_venta; break;
                        default: imp_total = obj.imp_total + imp_total; imp_venta = obj.imp_venta + imp_venta; break;
                    }

                    eTip2 = lstTipos.Find(x => x.AtributoDos == obj.dsc_tipo_servicio);

                    eTip2.AtributoDoce = imp_total;
                    eTip2.AtributoCatorce = imp_venta;
                }
                else
                {
                    switch (obj.cod_dotacion)
                    {
                        case "A": imp_total = (obj.imp_total / 12) + imp_total; imp_venta = (obj.imp_venta / 12) + imp_venta; break;
                        case "B": imp_total = (obj.imp_total / 6) + imp_total; imp_venta = (obj.imp_venta / 6) + imp_venta; break;
                        case "T": imp_total = (obj.imp_total / 4) + imp_total; imp_venta = (obj.imp_venta / 4) + imp_venta; break;
                        case "S": imp_total = (obj.imp_total / 2) + imp_total; imp_venta = (obj.imp_venta / 2) + imp_venta; break;
                        default: imp_total = obj.imp_total + imp_total; imp_venta = obj.imp_venta + imp_venta; break;
                    }

                    eTip2.AtributoDoce = imp_total;
                    eTip2.AtributoCatorce = imp_venta;
                }
            }

            bsResumen.DataSource = lstTipos;
        }

        private void CargarMontosProductos()
        {
            totalGeneral = totalGeneral - totalProductos;
            totalGeneralMg = totalGeneralMg - totalProductosMg;
            mrgTot = mrgMaq + mrgOtrs;

            totalProductos = 0;
            totalProductosMg = 0;
            mrgPro = 0;

            foreach (eDatos obj in lstTipos)
            {
                totalProductos = obj.AtributoDoce + totalProductos;
                totalProductosMg = obj.AtributoCatorce + totalProductosMg;
            }

            mrgPro = lstProdAns.Count != 0 ? lstProdAns.Average(x => x.prc_margen) : 0;

            totalGeneral = totalGeneral + totalProductos;
            totalGeneralMg = totalGeneralMg + totalProductosMg;
            mrgTot = (mrgPro + mrgMaq + mrgOtrs) / 3;

            tbiProductos.Elements[1].Text = (totalProductos != 0 ? "S/ " + $"{Math.Round(totalProductos, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");
            tbiProductos.Elements[2].Text = (totalProductosMg != 0 ? "S/ " + $"{Math.Round(totalProductosMg, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");
            tbiProductos.Elements[3].Text = (mrgPro != 0 ? $"{Math.Round(mrgPro, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" + "%" : "0.00%");

            tbiTotal.Elements[1].Text = (totalGeneral != 0 ? "S/ " + $"{Math.Round(totalGeneral, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");
            tbiTotal.Elements[2].Text = (totalGeneralMg != 0 ? "S/ " + $"{Math.Round(totalGeneralMg, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");
            tbiTotal.Elements[3].Text = (mrgTot != 0 ? $"{Math.Round(mrgTot, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" + "%" : "0.00%");

            gvProducto.RefreshData();
        }

        private void CargarMontosMaquinas()
        {
            totalGeneral = totalGeneral - totalMaqEquipos;
            totalGeneralMg = totalGeneralMg - totalMaqEquiposMg;
            mrgTot = mrgPro + mrgOtrs;

            totalMaqEquipos = 0;
            totalMaqEquiposMg = 0;
            mrgMaq = 0;

            foreach (eAnalisis.eAnalisis_Maquinaria obj in lstMaqAns)
            {
                totalMaqEquipos = obj.imp_mensual + totalMaqEquipos;
                totalMaqEquiposMg = obj.imp_venta + totalMaqEquiposMg;
            }

            mrgMaq = lstMaqAns.Count != 0 ? lstMaqAns.Average(x => x.prc_margen) : 0;

            totalGeneral = totalGeneral + totalMaqEquipos;
            totalGeneralMg = totalGeneralMg + totalMaqEquiposMg;
            mrgTot = (mrgPro + mrgMaq + mrgOtrs) / 3;

            tbiMaqEquipos.Elements[1].Text = (totalMaqEquipos != 0 ? "S/ " + $"{Math.Round(totalMaqEquipos, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");
            tbiMaqEquipos.Elements[2].Text = (totalMaqEquiposMg != 0 ? "S/ " + $"{Math.Round(totalMaqEquiposMg, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");
            tbiMaqEquipos.Elements[3].Text = (mrgMaq != 0 ? $"{Math.Round(mrgMaq, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" + "%" : "0.00%");

            tbiTotal.Elements[1].Text = (totalGeneral != 0 ? "S/ " + $"{Math.Round(totalGeneral, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");
            tbiTotal.Elements[2].Text = (totalGeneralMg != 0 ? "S/ " + $"{Math.Round(totalGeneralMg, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");
            tbiTotal.Elements[3].Text = (mrgTot != 0 ? $"{Math.Round(mrgTot, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" + "%" : "0.00%");

            gvMaqEquipo.RefreshData();
        }

        private void CargarTotales()
        {
            CargarMrgCst("");
            List<eAnalisis.eAnalisis_Est_Cst> lstTotales = lstCst.FindAll(x => x.cod_concepto == "00006");

            int cantidad = lstPerAns.Sum(x => x.num_cantidad);
            decimal sueldo = (lstPerAns.Sum(x => x.imp_salario_total - (x.num_cantidad * x.imp_movilidad)) + lstPerAns.Sum(x => (x.num_cantidad * lstGenerales[1].num_valor)));

            foreach (eAnalisis.eAnalisis_Est_Cst obj in lstTotales)
            {
                foreach (eAnalisis.eAnalisis_Otros obj2 in lstOtrAns)
                {
                    if (obj.dsc_item == obj2.dsc_descripcion)
                    {
                        obj2.num_cantidad = 1;
                        obj2.imp_unitario = obj.imp_unitario;
                        obj2.prc_ley = obj.prc_ley;
                        obj2.imp_total = obj.imp_total;
                        obj2.imp_venta = obj.imp_total + ((obj.imp_total * obj.prc_margen) / 100);
                        break;
                    }
                }
            }

            gvOtros.RefreshData();
            CargarMontosOtros();
        }

        private void CargarMontosOtros()
        {
            totalGeneral = totalGeneral - totalOtros;
            totalGeneralMg = totalGeneralMg - totalOtrosMg;
            mrgTot = mrgMaq + mrgPro;

            totalOtros = 0;
            totalOtrosMg = 0;
            mrgOtrs = 0;

            foreach (eAnalisis.eAnalisis_Otros obj in lstOtrAns)
            {
                if (obj.dsc_descripcion != "SEGURO COMPLEMENTARIO (SCTR)" && obj.dsc_descripcion != "SCTR SOCAVON" && obj.dsc_descripcion != "SCTR ALTURA" && obj.dsc_descripcion != "SEGURO VIDA LEY" && obj.dsc_descripcion != "EXÁMENES MÉDICOS OCUPACIONALES (LEY 29783)" && obj.dsc_descripcion != "POLIZAS RC/DH")
                {
                    totalOtros = obj.dsc_descripcion == "OTROS CONCEPTOS (SIG)" ? obj.imp_total + totalOtros : totalOtros;
                    totalOtrosMg = obj.imp_venta + totalOtrosMg;
                }
            }

            mrgOtrs = lstOtrAns.Count != 0 ? lstOtrAns.Average(x => x.prc_margen) : 0;

            totalGeneral = totalGeneral + totalOtros;
            totalGeneralMg = totalGeneralMg + totalOtrosMg;
            mrgTot = (mrgPro + mrgMaq + mrgOtrs) / 3;

            tbiOtros.Elements[1].Text = (totalOtros != 0 ? "S/ " + $"{Math.Round(totalOtros, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");
            tbiOtros.Elements[2].Text = (totalOtrosMg != 0 ? "S/ " + $"{Math.Round(totalOtrosMg, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");
            tbiOtros.Elements[3].Text = (mrgOtrs != 0 ? $"{Math.Round(mrgOtrs, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" + "%" : "0.00%");

            tbiTotal.Elements[3].Text = (mrgTot != 0 ? $"{Math.Round(mrgTot, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" + "%" : "0.00%");

            gvOtros.RefreshData();
        }

        private void BloquearCampos()
        {
            btnGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnAgregarServicio.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            txtPeriodo.Enabled = false;
            lkpTiempo.Enabled = false;
            txtMetros2.Enabled = false;
            txtMetros3.Enabled = false;

            gvAlcance.OptionsBehavior.Editable = false;
            gvAlcance.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            gvPuestos.OptionsBehavior.Editable = false;
            gvPuestos.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            gvPersonal.OptionsBehavior.Editable = false;
            gvProducto.OptionsBehavior.Editable = false;
            gvProducto.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            gvResumen.OptionsBehavior.Editable = false;
            gvMaqEquipo.OptionsBehavior.Editable = false;
            gvMaqEquipo.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            gvOtros.OptionsBehavior.Editable = false;
            gvOtros.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            gvEstructuraCostos.OptionsBehavior.Editable = false;
        }

        private bool ValidarCampos()
        {
            Boolean respuesta = true;

            if (txtPeriodo.EditValue == null || int.Parse(txtPeriodo.EditValue.ToString()) <= 0)
            {
                mensaje = "Debe ingresar un periodo al registro.";
                respuesta = false;
                txtPeriodo.Focus();

                return respuesta;
            }

            if (lkpTiempo.EditValue == null)
            {
                mensaje = "Debe ingresar un periodo al registro.";
                respuesta = false;
                lkpTiempo.Focus();

                return respuesta;
            }

            if (gvProducto.RowCount != 0)
            {
                for (int x = 0; x < gvProducto.DataRowCount; x++)
                {
                    eAnalisis.eAnalisis_Producto eDetAns = gvProducto.GetRow(x) as eAnalisis.eAnalisis_Producto;

                    if (eDetAns.num_cantidad == 0)
                    {
                        mensaje = "La cantidad de productos no puede ser igual a 0.";
                        respuesta = false;
                        break;
                    }
                }
            }

            if (gvPuestos.RowCount != 0)
            {
                for (int x = 0; x < gvPuestos.DataRowCount; x++)
                {
                    eAnalisis.eAnalisis_Personal eDetAns = gvPuestos.GetRow(x) as eAnalisis.eAnalisis_Personal;

                    if (eDetAns.num_cantidad == 0)
                    {
                        mensaje = "La cantidad de puestos no puede ser igual a 0.";
                        respuesta = false;
                        break;
                    }
                }
            }

            return respuesta;
        }

        //Métodos guardar BD
        private eAnalisis GuardarCabecera()
        {
            eAnalisis eAns = new eAnalisis();

            eAns.cod_empresa = empresa;
            eAns.cod_sede_empresa = sedeEmpresa;
            eAns.cod_analisis = analisis;
            eAns.cod_cliente = codigoCliente;
            eAns.cod_estado_analisis = "PRC";
            eAns.fch_requerimiento = Convert.ToDateTime(dtpFechaRequerimiento.EditValue);

            eAns = blAns.Ins_Act_Analisis<eAnalisis>(eAns, user.cod_usuario);

            return eAns;
        }

        private eAnalisis.eAnalisis_Sedes GuardarDetalleSedes()
        {
            eAnalisis.eAnalisis_Sedes eAnsSed = new eAnalisis.eAnalisis_Sedes();

            eAnsSed.cod_empresa = empresa;
            eAnsSed.cod_sede_empresa = sedeEmpresa;
            eAnsSed.cod_analisis = analisis;

            eCliente_Direccion eDir = tvSedesCliente.GetFocusedRow() as eCliente_Direccion;
            eAnsSed.cod_sede_cliente = eDir.num_linea;

            eAnsSed.num_m2 = Convert.ToDecimal(txtMetros2.EditValue);
            eAnsSed.num_m3 = Convert.ToDecimal(txtMetros3.EditValue);
            eAnsSed.fch_visita = DateTime.Parse("01/01/1900 00:00:00.000");
            eAnsSed.dsc_hora_inicio_visita = DateTime.Parse("01/01/1900 00:00:00.000");
            eAnsSed.dsc_hora_fin_visita = DateTime.Parse("01/01/1900 00:00:00.000");
            eAnsSed.dsc_observaciones = "N";

            eAnsSed = blAns.Ins_Act_Analisis_Sedes<eAnalisis.eAnalisis_Sedes>(eAnsSed, user.cod_usuario);

            return eAnsSed;
        }

        private eAnalisis.eAnalisis_Sedes_Prestacion GuardarPrestacionSede()
        {
            eAnalisis.eAnalisis_Sedes_Prestacion eAnsPre = new eAnalisis.eAnalisis_Sedes_Prestacion();

            eAnsPre.cod_empresa = empresa;
            eAnsPre.cod_sede_empresa = sedeEmpresa;
            eAnsPre.cod_analisis = analisis;
            eAnsPre.num_servicio = servicio;

            eCliente_Direccion eDir = tvSedesCliente.GetFocusedRow() as eCliente_Direccion;
            eAnsPre.cod_sede_cliente = eDir.num_linea;

            eAnsPre.cod_tipo_prestacion = lkpTipoServicio.GetColumnValue("cod_tipo_prestacion").ToString();
            eAnsPre.dsc_periodo = txtPeriodo.EditValue.ToString() + "-" + lkpTiempo.EditValue.ToString();

            eAnsPre = blAns.Ins_Act_Analisis_Sedes_Prestacion<eAnalisis.eAnalisis_Sedes_Prestacion>(eAnsPre, user.cod_usuario);

            return eAnsPre;
        }

        private void GuardarAlcance()
        {
            foreach (eAnalisis.eAnalisis_Alcance eDetAns in lstAlcAnsElim)
            {
                string respuesta = blAns.Eliminar_Reg_Analisis("Alcance", empresa, sedeEmpresa, analisis, servicio: servicio, item: eDetAns.num_item);
            }

            gvAlcance.PostEditor();
            for (int x = 0; x < gvAlcance.DataRowCount; x++)
            {
                eAnalisis.eAnalisis_Alcance eDetAns = gvAlcance.GetRow(x) as eAnalisis.eAnalisis_Alcance;

                eDetAns.cod_empresa = empresa;
                eDetAns.cod_sede_empresa = sedeEmpresa;
                eDetAns.cod_analisis = analisis;
                eDetAns.num_servicio = servicio;
                eDetAns.num_cantidad = 0;
                eDetAns.flg_maq_equipo = "NO";

                eDetAns = blAns.Ins_Act_Alcance_Analisis<eAnalisis.eAnalisis_Alcance>(eDetAns, user.cod_usuario);
            }
        }

        private void GuardarPersonal()
        {
            foreach (eAnalisis.eAnalisis_Personal eDetAns in lstPerAnsElim)
            {
                string respuesta = blAns.Eliminar_Reg_Analisis("Personal", empresa, sedeEmpresa, analisis, servicio: servicio, item: eDetAns.num_item);
            }

            gvPuestos.PostEditor();
            for (int x = 0; x < lstPerAns.Count; x++)
            {
                eAnalisis.eAnalisis_Personal eDetAns = lstPerAns[x];

                eDetAns.cod_empresa = empresa;
                eDetAns.cod_sede_empresa = sedeEmpresa;
                eDetAns.cod_analisis = analisis;
                eDetAns.num_servicio = servicio;

                eDetAns = blAns.Ins_Act_Personal_Analisis<eAnalisis.eAnalisis_Personal>(eDetAns, user.cod_usuario);
            }
        }

        private void GuardarUniforme()
        {
            foreach (eAnalisis.eAnalisis_Personal_Uniformes eUniAns in lstUniAnsElim)
            {
                string respuesta = blAns.Eliminar_Reg_Analisis("Uniformes", empresa, sedeEmpresa, analisis, servicio: servicio, cargo: eUniAns.cod_cargo, item: eUniAns.num_item, producto: eUniAns.cod_producto);
            }

            foreach (eAnalisis.eAnalisis_Personal_Uniformes eUniAns in lstUnifEpp)
            {
                eUniAns.cod_empresa = empresa;
                eUniAns.cod_sede_empresa = sedeEmpresa;
                eUniAns.cod_analisis = analisis;
                eUniAns.num_servicio = servicio;

                eAnalisis.eAnalisis_Personal_Uniformes uni = blAns.Ins_Act_Uniformes_Analisis<eAnalisis.eAnalisis_Personal_Uniformes>(eUniAns, user.cod_usuario);
            }
        }

        private void GuardarProducto()
        {
            foreach (eAnalisis.eAnalisis_Producto eDetAns in lstProdAnsElim)
            {
                string respuesta = blAns.Eliminar_Reg_Analisis("Producto", empresa, sedeEmpresa, analisis, servicio: servicio, producto: eDetAns.cod_producto);
            }

            gvProducto.PostEditor();
            for (int x = 0; x < lstProdAns.Count; x++)
            {
                eAnalisis.eAnalisis_Producto eDetAns = lstProdAns[x];

                eDetAns.cod_empresa = empresa;
                eDetAns.cod_sede_empresa = sedeEmpresa;
                eDetAns.cod_analisis = analisis;
                eDetAns.num_servicio = servicio;

                eDetAns = blAns.Ins_Act_Producto_Analisis<eAnalisis.eAnalisis_Producto>(eDetAns, user.cod_usuario);
            }
        }

        private void GuardarMaquinaria()
        {
            foreach (eAnalisis.eAnalisis_Maquinaria eDetAns in lstMaqAns)
            {
                string respuesta = blAns.Eliminar_Reg_Analisis("Maquinaria", empresa, sedeEmpresa, analisis, servicio: servicio, maquinaria: eDetAns.cod_activo_fijo);
            }

            gvMaqEquipo.PostEditor();
            for (int x = 0; x < lstMaqAns.Count; x++)
            {
                eAnalisis.eAnalisis_Maquinaria eDetAns = lstMaqAns[x];

                eDetAns.cod_empresa = empresa;
                eDetAns.cod_sede_empresa = sedeEmpresa;
                eDetAns.cod_analisis = analisis;
                eDetAns.num_servicio = servicio;

                eDetAns = blAns.Ins_Act_Maquinaria_Analisis<eAnalisis.eAnalisis_Maquinaria>(eDetAns, user.cod_usuario);
            }
        }

        private void GuardarOtros()
        {
            foreach (eAnalisis.eAnalisis_Otros eDetAns in lstOtrosElim)
            {
                string respuesta = blAns.Eliminar_Reg_Analisis("Otros", empresa, sedeEmpresa, analisis, servicio: servicio, item: eDetAns.num_item);
            }

            gvOtros.PostEditor();
            for (int x = 0; x < lstOtrAns.Count; x++)
            {
                eAnalisis.eAnalisis_Otros eDetAns = lstOtrAns[x];

                eDetAns.cod_empresa = empresa;
                eDetAns.cod_sede_empresa = sedeEmpresa;
                eDetAns.cod_analisis = analisis;
                eDetAns.num_servicio = servicio;

                eDetAns = blAns.Ins_Act_Otros_Analisis<eAnalisis.eAnalisis_Otros>(eDetAns, user.cod_usuario);
            }
        }

        public void Busqueda(string dato, string tipo, string filtroRUC = "NO")
        {
            frmBusquedas frm = new frmBusquedas();
            frm.user = user;
            frm.filtro = dato;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            switch (tipo)
            {
                case "Cliente":
                    frm.entidad = frmBusquedas.MiEntidad.ClienteEmpresa;
                    frm.cod_condicion1 = lkpEmpresa.EditValue.ToString();
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Cliente":
                    codigoCliente = frm.codigo;
                    txtCliente.EditValue = frm.descripcion;

                    CargarSedes();

                    picVerCliente.Enabled = true;
                    break;
            }
        }

        private decimal TotalDescansero()
        {
            descanseroG = 0;
            decimal totalDescansero = 0, total = 0;
            decimal monto = 0;

            foreach (eAnalisis.eAnalisis_Personal obj in lstPerAns)
            {
                if (obj.flg_descansero)
                {
                    //totalDescansero = ((obj.imp_salario_total + lstGenerales[1].num_valor) * obj.num_cantidad) + totalDescansero;
                    totalDescansero = ((obj.imp_salario + lstGenerales[1].num_valor + obj.imp_salario_extra + obj.imp_bono_productividad + obj.imp_bono_nocturno) * obj.num_cantidad) + totalDescansero;
                    monto = (totalDescansero * lstGenerales[4].num_valor) / 100;

                    List<eAnalisis.eAnalisis_Otros> lstLeySoc = lstOtrAns.FindAll(x => x.cod_concepto == "00002");
                    foreach (eAnalisis.eAnalisis_Otros ley in lstLeySoc)
                    {
                        if (ley.dsc_descripcion == "SEGURO VIDA LEY")
                        {
                            monto = ((ley.imp_venta / ley.num_cantidad) * obj.num_cantidad) + monto;
                        }
                        else
                        {
                            monto = ((totalDescansero * ley.prc_ley) / 100) + ((((totalDescansero * ley.prc_ley) / 100) * ley.prc_margen) / 100) + monto;
                        }
                    }

                    totalDescansero = totalDescansero + monto;
                    monto = totalDescansero;

                    totalDescansero = totalDescansero + (((monto * 167) / 10) / 100);
                    totalDescansero = totalDescansero + (((monto * 83) / 10) / 100) + (monto / 12);
                    totalDescansero = totalDescansero + (((monto * 97) / 10) / 100);

                    //Movilidad
                    totalDescansero = totalDescansero + (obj.imp_movilidad * obj.num_cantidad);

                    List<eAnalisis.eAnalisis_Otros> lstGstPer = lstOtrAns.FindAll(x => x.cod_concepto == "00004");
                    foreach (eAnalisis.eAnalisis_Otros gst in lstGstPer)
                    {
                        monto = (gst.imp_venta / gst.num_cantidad) * obj.num_cantidad;
                        totalDescansero = totalDescansero + monto;
                    }

                    List<eAnalisis.eAnalisis_Personal_Uniformes> lstUnifUnit = lstUnifEpp.FindAll(i => i.cod_cargo == obj.cod_cargo);
                    foreach (eAnalisis.eAnalisis_Personal_Uniformes uni in lstUnifUnit)
                    {
                        monto = uni.imp_venta;
                        totalDescansero = totalDescansero + monto;
                    }
                    total = (totalDescansero / 6) + total; //Se agregó este código para sumar el acumulado de descanseros. LDAC - 17/02/2023
                    totalDescansero = 0;
                    monto = 0;
                }
                //totalDescansero = totalDescansero / 6;
            }

            return total;
        }

        private void CargarMrgCst(string flag = "")
        {
            GenerarRemuneracion(flag);
            GenerarLeyesSociales(flag);
            GenerarBeneficiosSociales(flag);
            GenerarGastosPersonal(flag);
            GenerarCostosOperativos(flag);
            GenerarTotalGeneral(flag);
        }

        private void GenerarRemuneracion(string flag)
        {
            sueldoG = 0;
            eAnalisis.eAnalisis_Est_Cst eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();

            /*Concepto 1. Remuneracion*/
            if (lstPerAns.Count > 0)
            {
                lstCst.RemoveAll(x => x.cod_concepto == "00001");

                for (int x = 0; x < 7; x++)
                {
                    eDetMrgCst.cod_concepto = "00001";
                    eDetMrgCst.dsc_concepto = "1. REMUNERACION";
                    eDetMrgCst.cod_item = "R0000".Substring(0, 5 - (x + 1).ToString().Length) + (x + 1).ToString();

                    foreach (eAnalisis.eAnalisis_Personal obj in lstPerAns)
                    {
                        switch (x)
                        {
                            case 0: eDetMrgCst.dsc_item = "SUELDO"; eDetMrgCst.imp_total = (obj.num_cantidad * obj.imp_salario) + eDetMrgCst.imp_total; break;
                            case 1: eDetMrgCst.dsc_item = "ASIGNACIÓN FAMILIAR"; eDetMrgCst.prc_ley = lstGenerales[3].num_valor; eDetMrgCst.imp_total = (obj.num_cantidad * lstGenerales[1].num_valor) + eDetMrgCst.imp_total; break;
                            case 2: eDetMrgCst.dsc_item = "HORAS EXTRA"; eDetMrgCst.imp_total = (obj.num_cantidad * obj.imp_salario_extra) + eDetMrgCst.imp_total; break;
                            case 3: eDetMrgCst.dsc_item = "BONIFICACIÓN SEGÚN RENDIMIENTO"; eDetMrgCst.imp_total = (obj.num_cantidad * obj.imp_bono_productividad) + eDetMrgCst.imp_total; break;
                            case 4: eDetMrgCst.dsc_item = "BONIFICACIÓN NOCTURNA"; eDetMrgCst.imp_total = (obj.num_cantidad * obj.imp_bono_nocturno) + eDetMrgCst.imp_total; break;
                            case 5: eDetMrgCst.dsc_item = "FERIADOS"; eDetMrgCst.imp_total = (obj.num_cantidad * obj.imp_feriado) + eDetMrgCst.imp_total; break;
                            case 6: eDetMrgCst.dsc_item = "SUBTOTAL REMUNERACION"; eDetMrgCst.imp_total = 0; break;
                        }
                    }

                    sueldoG = eDetMrgCst.imp_total + sueldoG;
                    if (x == 6) eDetMrgCst.imp_total = sueldoG;

                    if (flag == "G")
                    {
                        eDetMrgCst.cod_empresa = empresa;
                        eDetMrgCst.cod_sede_empresa = sedeEmpresa;
                        eDetMrgCst.cod_analisis = analisis;
                        eDetMrgCst.num_servicio = servicio;

                        blAns.Ins_Act_Est_Cst_Analisis<eAnalisis.eAnalisis_Est_Cst>(eDetMrgCst, user.cod_usuario);
                    }

                    lstCst.Add(eDetMrgCst);

                    eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();
                }
            }
        }

        private void GenerarLeyesSociales(string flag)
        {
            socialesG = 0;
            eAnalisis.eAnalisis_Est_Cst eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();

            /*Concepto 2. Leyes Sociales*/
            if (lstPerAns.Count > 0)
            {
                lstCst.RemoveAll(x => x.cod_concepto == "00002");

                eDetMrgCst.cod_concepto = "00002";
                eDetMrgCst.dsc_concepto = "2. LEYES SOCIALES";
                eDetMrgCst.cod_item = "LS001";
                eDetMrgCst.dsc_item = "ESSALUD";
                eDetMrgCst.prc_ley = lstGenerales[4].num_valor;
                eDetMrgCst.imp_total = (sueldoG * eDetMrgCst.prc_ley) / 100;

                socialesG = eDetMrgCst.imp_total + socialesG;

                if (flag == "G")
                {
                    eDetMrgCst.cod_empresa = empresa;
                    eDetMrgCst.cod_sede_empresa = sedeEmpresa;
                    eDetMrgCst.cod_analisis = analisis;
                    eDetMrgCst.num_servicio = servicio;

                    blAns.Ins_Act_Est_Cst_Analisis<eAnalisis.eAnalisis_Est_Cst>(eDetMrgCst, user.cod_usuario);
                }

                lstCst.Add(eDetMrgCst);

                eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();

                int pos = 2;

                List<eAnalisis.eAnalisis_Otros> lstLeySoc = lstOtrAns.FindAll(x => x.cod_concepto == "00002");
                foreach (eAnalisis.eAnalisis_Otros obj in lstLeySoc)
                {
                    eDetMrgCst.cod_concepto = obj.cod_concepto;
                    eDetMrgCst.dsc_concepto = "2. LEYES SOCIALES";
                    eDetMrgCst.cod_item = "LS000".Substring(0, 5 - pos.ToString().Length) + pos.ToString();
                    eDetMrgCst.dsc_item = obj.dsc_descripcion;
                    eDetMrgCst.prc_ley = obj.prc_ley;
                    eDetMrgCst.prc_margen = obj.prc_margen;
                    eDetMrgCst.imp_unitario = obj.imp_total;
                    eDetMrgCst.imp_total = obj.imp_venta;

                    socialesG = eDetMrgCst.imp_total + socialesG;

                    if (flag == "G")
                    {
                        eDetMrgCst.cod_empresa = empresa;
                        eDetMrgCst.cod_sede_empresa = sedeEmpresa;
                        eDetMrgCst.cod_analisis = analisis;
                        eDetMrgCst.num_servicio = servicio;

                        blAns.Ins_Act_Est_Cst_Analisis<eAnalisis.eAnalisis_Est_Cst>(eDetMrgCst, user.cod_usuario);
                    }

                    lstCst.Add(eDetMrgCst);

                    eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();

                    pos = pos + 1;
                }

                eDetMrgCst.cod_concepto = "00002";
                eDetMrgCst.dsc_concepto = "2. LEYES SOCIALES";
                eDetMrgCst.cod_item = "LS000".Substring(0, 5 - pos.ToString().Length) + pos.ToString();
                eDetMrgCst.dsc_item = "SUBTOTAL LEYES SOCIALES";
                eDetMrgCst.imp_total = socialesG;

                if (flag == "G")
                {
                    eDetMrgCst.cod_empresa = empresa;
                    eDetMrgCst.cod_sede_empresa = sedeEmpresa;
                    eDetMrgCst.cod_analisis = analisis;
                    eDetMrgCst.num_servicio = servicio;

                    blAns.Ins_Act_Est_Cst_Analisis<eAnalisis.eAnalisis_Est_Cst>(eDetMrgCst, user.cod_usuario);
                }

                lstCst.Add(eDetMrgCst);

                eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();
            }
        }

        private void GenerarBeneficiosSociales(string flag)
        {
            beneficiosG = 0;
            eAnalisis.eAnalisis_Est_Cst eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();

            /*Concepto 3. Beneficios Sociales*/
            if (lstPerAns.Count > 0)
            {
                lstCst.RemoveAll(x => x.cod_concepto == "00003");

                for (int x = 0; x < 4; x++)
                {
                    eDetMrgCst.cod_concepto = "00003";
                    eDetMrgCst.dsc_concepto = "3. BENEFICIOS SOCIALES";
                    eDetMrgCst.cod_item = "BS000".Substring(0, 5 - (x + 1).ToString().Length) + (x + 1).ToString();

                    switch (x)
                    {
                        case 0: eDetMrgCst.dsc_item = "GRATIFICACIÓN + BONIFICACIÓN ESPECIAL"; eDetMrgCst.prc_ley = Convert.ToDecimal(16.7); eDetMrgCst.imp_total = ((sueldoG + socialesG) * eDetMrgCst.prc_ley) / 100; break;
                        case 1: eDetMrgCst.dsc_item = "VACACIONES"; eDetMrgCst.prc_ley = Convert.ToDecimal(8.3); eDetMrgCst.imp_total = (((sueldoG + socialesG) * eDetMrgCst.prc_ley) / 100) + ((sueldoG + socialesG) / 12); break;
                        case 2: eDetMrgCst.dsc_item = "COMPENSACIÓN POR TIEMPO DE SERVICIO (CTS)"; eDetMrgCst.prc_ley = Convert.ToDecimal(9.7); eDetMrgCst.imp_total = ((sueldoG + socialesG) * eDetMrgCst.prc_ley) / 100; break;
                        case 3: eDetMrgCst.dsc_item = "SUBTOTAL BENEFICIOS SOCIALES"; break;
                    }

                    beneficiosG = eDetMrgCst.imp_total + beneficiosG;
                    if (x == 3) eDetMrgCst.imp_total = beneficiosG;

                    if (flag == "G")
                    {
                        eDetMrgCst.cod_empresa = empresa;
                        eDetMrgCst.cod_sede_empresa = sedeEmpresa;
                        eDetMrgCst.cod_analisis = analisis;
                        eDetMrgCst.num_servicio = servicio;

                        blAns.Ins_Act_Est_Cst_Analisis<eAnalisis.eAnalisis_Est_Cst>(eDetMrgCst, user.cod_usuario);
                    }

                    lstCst.Add(eDetMrgCst);

                    eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();
                }
            }
        }

        private void GenerarGastosPersonal(string flag)
        {
            gastosG = 0;
            eAnalisis.eAnalisis_Est_Cst eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();

            /*Concepto 4. Gastos de Personal*/
            if (lstPerAns.Count > 0)
            {
                lstCst.RemoveAll(x => x.cod_concepto == "00004");

                eDetMrgCst.cod_concepto = "00004";
                eDetMrgCst.dsc_concepto = "4. GASTOS DE PERSONAL";
                eDetMrgCst.cod_item = "GP001";
                eDetMrgCst.dsc_item = "MOVILIDAD";

                foreach (eAnalisis.eAnalisis_Personal obj in lstPerAns)
                {
                    eDetMrgCst.imp_total = (obj.num_cantidad * obj.imp_movilidad) + eDetMrgCst.imp_total;
                }

                gastosG = eDetMrgCst.imp_total + gastosG;

                if (flag == "G")
                {
                    eDetMrgCst.cod_empresa = empresa;
                    eDetMrgCst.cod_sede_empresa = sedeEmpresa;
                    eDetMrgCst.cod_analisis = analisis;
                    eDetMrgCst.num_servicio = servicio;

                    blAns.Ins_Act_Est_Cst_Analisis<eAnalisis.eAnalisis_Est_Cst>(eDetMrgCst, user.cod_usuario);
                }

                lstCst.Add(eDetMrgCst);

                eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();

                int pos = 2;

                List<eAnalisis.eAnalisis_Otros> lstGastPer = lstOtrAns.FindAll(x => x.cod_concepto == "00004");
                foreach (eAnalisis.eAnalisis_Otros obj in lstGastPer)
                {
                    eDetMrgCst.cod_concepto = obj.cod_concepto;
                    eDetMrgCst.dsc_concepto = "4. GASTOS DE PERSONAL";
                    eDetMrgCst.cod_item = "GP000".Substring(0, 5 - pos.ToString().Length) + pos.ToString();
                    eDetMrgCst.dsc_item = obj.dsc_descripcion;
                    eDetMrgCst.prc_ley = obj.prc_ley;
                    eDetMrgCst.prc_margen = obj.prc_margen;
                    eDetMrgCst.imp_unitario = obj.imp_total;
                    eDetMrgCst.imp_total = obj.imp_venta;

                    gastosG = eDetMrgCst.imp_total + gastosG;

                    if (flag == "G")
                    {
                        eDetMrgCst.cod_empresa = empresa;
                        eDetMrgCst.cod_sede_empresa = sedeEmpresa;
                        eDetMrgCst.cod_analisis = analisis;
                        eDetMrgCst.num_servicio = servicio;

                        blAns.Ins_Act_Est_Cst_Analisis<eAnalisis.eAnalisis_Est_Cst>(eDetMrgCst, user.cod_usuario);
                    }

                    lstCst.Add(eDetMrgCst);

                    eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();

                    pos = pos + 1;
                }

                if (lstUnifEpp != null)
                {
                    eDetMrgCst.cod_concepto = "00004";
                    eDetMrgCst.dsc_concepto = "4. GASTOS DE PERSONAL";
                    eDetMrgCst.cod_item = "GP000".Substring(0, 5 - pos.ToString().Length) + pos.ToString();
                    eDetMrgCst.dsc_item = "UNIFORMES Y EPP";

                    foreach (eAnalisis.eAnalisis_Personal_Uniformes obj in lstUnifEpp)
                    {
                        eDetMrgCst.prc_margen = obj.prc_margen;
                        eDetMrgCst.imp_unitario = obj.imp_total + eDetMrgCst.imp_unitario;
                        eDetMrgCst.imp_total = obj.imp_venta + eDetMrgCst.imp_total;
                    }

                    gastosG = eDetMrgCst.imp_total + gastosG;

                    if (flag == "G")
                    {
                        eDetMrgCst.cod_empresa = empresa;
                        eDetMrgCst.cod_sede_empresa = sedeEmpresa;
                        eDetMrgCst.cod_analisis = analisis;
                        eDetMrgCst.num_servicio = servicio;

                        blAns.Ins_Act_Est_Cst_Analisis<eAnalisis.eAnalisis_Est_Cst>(eDetMrgCst, user.cod_usuario);
                    }

                    lstCst.Add(eDetMrgCst);

                    eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();

                    pos = pos + 1;
                }

                eDetMrgCst.cod_concepto = "00004";
                eDetMrgCst.dsc_concepto = "4. GASTOS DE PERSONAL";
                eDetMrgCst.cod_item = "GP000".Substring(0, 5 - pos.ToString().Length) + pos.ToString();
                eDetMrgCst.dsc_item = "DESCANSEROS";
                descanseroG = TotalDescansero();
                //descanseroG = totalDescansero;
                eDetMrgCst.imp_total = descanseroG;

                if (flag == "G")
                {
                    eDetMrgCst.cod_empresa = empresa;
                    eDetMrgCst.cod_sede_empresa = sedeEmpresa;
                    eDetMrgCst.cod_analisis = analisis;
                    eDetMrgCst.num_servicio = servicio;

                    blAns.Ins_Act_Est_Cst_Analisis<eAnalisis.eAnalisis_Est_Cst>(eDetMrgCst, user.cod_usuario);
                }

                lstCst.Add(eDetMrgCst);

                eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();

                pos = pos + 1;

                for (int x = 0; x < 3; x++)
                {
                    eDetMrgCst.cod_concepto = "00004";
                    eDetMrgCst.dsc_concepto = "4. GASTOS DE PERSONAL";
                    eDetMrgCst.cod_item = "GP000".Substring(0, 5 - pos.ToString().Length) + pos.ToString();

                    foreach (eAnalisis.eAnalisis_Personal obj in lstPerAns)
                    {
                        switch (x)
                        {
                            case 0: eDetMrgCst.dsc_item = "SUBTOTAL GASTOS DE PERSONAL"; eDetMrgCst.imp_total = gastosG; break;
                            case 1: eDetMrgCst.dsc_item = "OPERARIOS"; eDetMrgCst.imp_total = obj.num_cantidad + eDetMrgCst.imp_total; break;
                            case 2: eDetMrgCst.dsc_item = "TOTAL MANO DE OBRA"; break;
                        }
                    }

                    if (x == 2) eDetMrgCst.imp_total = sueldoG + socialesG + beneficiosG + gastosG + descanseroG;

                    if (flag == "G")
                    {
                        eDetMrgCst.cod_empresa = empresa;
                        eDetMrgCst.cod_sede_empresa = sedeEmpresa;
                        eDetMrgCst.cod_analisis = analisis;
                        eDetMrgCst.num_servicio = servicio;

                        blAns.Ins_Act_Est_Cst_Analisis<eAnalisis.eAnalisis_Est_Cst>(eDetMrgCst, user.cod_usuario);
                    }

                    lstCst.Add(eDetMrgCst);

                    eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();

                    pos = pos + 1;
                }
            }
        }

        private void GenerarCostosOperativos(string flag)
        {
            productosG = 0;
            eAnalisis.eAnalisis_Est_Cst eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();

            /*Concepto 5. Costos Operativos*/
            lstCst.RemoveAll(x => x.cod_concepto == "00005"); int i = 0;

            foreach (eDatos obj in lstTipos)
            {
                List<eAnalisis.eAnalisis_Producto> lstMontos = new List<eAnalisis.eAnalisis_Producto>();
                lstMontos = lstProdAns.FindAll(x => x.dsc_tipo_servicio == obj.AtributoDos);

                eDetMrgCst.cod_concepto = "00005";
                eDetMrgCst.dsc_concepto = "5. COSTOS OPERATIVOS";
                eDetMrgCst.cod_item = lstMontos[0].cod_tipo_servicio;
                eDetMrgCst.dsc_item = lstMontos[0].dsc_tipo_servicio;
                eDetMrgCst.prc_margen = lstMontos[0].prc_margen;

                foreach (eAnalisis.eAnalisis_Producto obj2 in lstMontos)
                {
                    if (obj2.cod_dotacion != null)
                    {
                        switch (obj2.cod_dotacion)
                        {
                            case "A": eDetMrgCst.imp_total = (obj2.imp_venta / 12) + eDetMrgCst.imp_total; break;
                            case "B": eDetMrgCst.imp_total = (obj2.imp_venta / 6) + eDetMrgCst.imp_total; break;
                            case "T": eDetMrgCst.imp_total = (obj2.imp_venta / 4) + eDetMrgCst.imp_total; break;
                            case "S": eDetMrgCst.imp_total = (obj2.imp_venta / 2) + eDetMrgCst.imp_total; break;
                            default: eDetMrgCst.imp_total = obj2.imp_venta + eDetMrgCst.imp_total; break;
                        }
                    }
                    else
                    {
                        eDetMrgCst.imp_total = obj2.imp_venta + eDetMrgCst.imp_total;
                    }
                }

                if (flag == "G")
                {
                    eDetMrgCst.cod_empresa = empresa;
                    eDetMrgCst.cod_sede_empresa = sedeEmpresa;
                    eDetMrgCst.cod_analisis = analisis;
                    eDetMrgCst.num_servicio = servicio;

                    blAns.Ins_Act_Est_Cst_Analisis<eAnalisis.eAnalisis_Est_Cst>(eDetMrgCst, user.cod_usuario);
                }

                lstCst.Add(eDetMrgCst);

                eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst(); i = i + 1;
            }

            if (lstMaqAns != null && lstMaqAns.Count > 0)
            {
                eDetMrgCst.cod_concepto = "00005";
                eDetMrgCst.dsc_concepto = "5. COSTOS OPERATIVOS";
                eDetMrgCst.cod_item = "CO001";
                eDetMrgCst.dsc_item = "MAQUINARIA, EQUIPOS Y ACCESORIOS";

                foreach (eAnalisis.eAnalisis_Maquinaria maq in lstMaqAns)
                {
                    eDetMrgCst.imp_total = maq.imp_venta + eDetMrgCst.imp_total;
                }

                if (flag == "G")
                {
                    eDetMrgCst.cod_empresa = empresa;
                    eDetMrgCst.cod_sede_empresa = sedeEmpresa;
                    eDetMrgCst.cod_analisis = analisis;
                    eDetMrgCst.num_servicio = servicio;

                    blAns.Ins_Act_Est_Cst_Analisis<eAnalisis.eAnalisis_Est_Cst>(eDetMrgCst, user.cod_usuario);
                }

                lstCst.Add(eDetMrgCst);

                eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();
            }

            if (lstPerAns.Count > 0 || i == lstTipos.Count && lstTipos.Count > 0)
            {
                int pos = 2;
                List<eAnalisis.eAnalisis_Otros> lstCstOpe = lstOtrAns.FindAll(x => x.cod_concepto == "00005");
                foreach (eAnalisis.eAnalisis_Otros cst in lstCstOpe)
                {
                    eDetMrgCst.cod_concepto = cst.cod_concepto;
                    eDetMrgCst.dsc_concepto = "5. COSTOS OPERATIVOS";
                    eDetMrgCst.cod_item = "CO000".Substring(0, 5 - pos.ToString().Length) + pos.ToString();
                    eDetMrgCst.dsc_item = cst.dsc_descripcion;
                    eDetMrgCst.prc_margen = cst.prc_margen;
                    eDetMrgCst.imp_unitario = cst.imp_total;
                    eDetMrgCst.imp_total = cst.imp_venta;

                    if (flag == "G")
                    {
                        eDetMrgCst.cod_empresa = empresa;
                        eDetMrgCst.cod_sede_empresa = sedeEmpresa;
                        eDetMrgCst.cod_analisis = analisis;
                        eDetMrgCst.num_servicio = servicio;

                        blAns.Ins_Act_Est_Cst_Analisis<eAnalisis.eAnalisis_Est_Cst>(eDetMrgCst, user.cod_usuario);
                    }

                    lstCst.Add(eDetMrgCst);

                    eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();

                    pos = pos + 1;
                }

                eDetMrgCst.cod_concepto = "00005";
                eDetMrgCst.dsc_concepto = "5. COSTOS OPERATIVOS";
                eDetMrgCst.cod_item = "CO000".Substring(0, 5 - pos.ToString().Length) + pos.ToString();
                eDetMrgCst.dsc_item = "SUBTOTAL COSTOS OPERATIVOS";

                List<eAnalisis.eAnalisis_Est_Cst> lstSubtotalPro = lstCst.FindAll(x => x.cod_concepto == "00005");
                productosG = lstSubtotalPro.Sum(x => x.imp_total);
                eDetMrgCst.imp_total = productosG;

                if (flag == "G")
                {
                    eDetMrgCst.cod_empresa = empresa;
                    eDetMrgCst.cod_sede_empresa = sedeEmpresa;
                    eDetMrgCst.cod_analisis = analisis;
                    eDetMrgCst.num_servicio = servicio;

                    blAns.Ins_Act_Est_Cst_Analisis<eAnalisis.eAnalisis_Est_Cst>(eDetMrgCst, user.cod_usuario);
                }

                lstCst.Add(eDetMrgCst);

                eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();
            }
        }

        private void GenerarTotalGeneral(string flag)
        {
            eAnalisis.eAnalisis_Est_Cst eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();

            /*Concepto 6. Total General*/
            if (lstCst.Count > 0)
            {
                decimal subtotal = sueldoG + socialesG + beneficiosG + gastosG + descanseroG + productosG;
                decimal gFi = 0, cSp = 0, cCm = 0, gAD = 0, gOP = 0, uti = 0, igv = 0;

                //Actualizar el card para que el monto salga igual a Total Costo directo
                tbiTotal.Elements[1].Text = (totalGeneral != 0 ? "S/ " + $"{Math.Round(subtotal, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");

                lstCst.RemoveAll(x => x.cod_concepto == "00006");

                foreach (eAnalisis.eAnalisis_Est_Cst obj in lstTot)
                {
                    if (obj.cod_item == "GG001")
                    {
                        obj.imp_total = subtotal;

                        if (flag == "G")
                        {
                            obj.cod_empresa = empresa;
                            obj.cod_sede_empresa = sedeEmpresa;
                            obj.cod_analisis = analisis;
                            obj.num_servicio = servicio;

                            blAns.Ins_Act_Est_Cst_Analisis<eAnalisis.eAnalisis_Est_Cst>(obj, user.cod_usuario);
                        }

                        lstCst.Add(obj);
                        break;
                    }
                }

                eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();

                int pos = 2;

                List<eAnalisis.eAnalisis_Otros> lstTotGen = lstOtrAns.FindAll(x => x.cod_concepto == "00006");
                
                lstTotGen = CambiarOrdenGastosGenerales(lstTotGen); //->Cambiar el ordern de los items de Gastos Generales

                foreach (eAnalisis.eAnalisis_Otros obj in lstTotGen)
                {
                    eDetMrgCst.cod_concepto = obj.cod_concepto;
                    eDetMrgCst.dsc_concepto = "6. GASTOS GENERALES";
                    eDetMrgCst.cod_item = "GG000".Substring(0, 5 - pos.ToString().Length) + pos.ToString();
                    eDetMrgCst.dsc_item = obj.dsc_descripcion;
                    eDetMrgCst.prc_ley = obj.prc_ley;
                    eDetMrgCst.prc_margen = obj.prc_margen;

                    switch (obj.dsc_descripcion)
                    {
                        case "GASTOS FINANCIEROS": eDetMrgCst.imp_unitario = subtotal; eDetMrgCst.imp_total = ((subtotal * obj.prc_ley) / 100); eDetMrgCst.imp_total = eDetMrgCst.imp_total + (eDetMrgCst.imp_total * obj.prc_margen) / 100; gFi = eDetMrgCst.imp_total; break;
                        case "COSTO SUPERVISIÓN": eDetMrgCst.imp_unitario = subtotal; eDetMrgCst.imp_total = (subtotal * obj.prc_ley) / 100; eDetMrgCst.imp_total = eDetMrgCst.imp_total + (eDetMrgCst.imp_total * obj.prc_margen) / 100; cSp = eDetMrgCst.imp_total; break;
                        case "COMISIÓN COMERCIAL": eDetMrgCst.imp_unitario = subtotal; eDetMrgCst.imp_total = (subtotal * obj.prc_ley) / 100; eDetMrgCst.imp_total = eDetMrgCst.imp_total + (eDetMrgCst.imp_total * obj.prc_margen) / 100; cCm = eDetMrgCst.imp_total; break;
                        case "GASTOS ADMINISTRATIVOS": eDetMrgCst.imp_unitario = subtotal; eDetMrgCst.imp_total = (subtotal * obj.prc_ley) / 100; eDetMrgCst.imp_total = eDetMrgCst.imp_total + (eDetMrgCst.imp_total * obj.prc_margen) / 100; gOP = eDetMrgCst.imp_total; break;
                        case "GASTOS OPERATIVOS": eDetMrgCst.imp_unitario = subtotal; eDetMrgCst.imp_total = (subtotal * obj.prc_ley) / 100; eDetMrgCst.imp_total = eDetMrgCst.imp_total + (eDetMrgCst.imp_total * obj.prc_margen) / 100; gAD = eDetMrgCst.imp_total; break;
                        case "UTILIDADES": eDetMrgCst.imp_unitario = subtotal + gFi + cSp + cCm + gOP + gAD; eDetMrgCst.imp_total = ((subtotal + gFi + cSp + cCm + gOP + gAD) * obj.prc_ley) / 100; eDetMrgCst.imp_total = eDetMrgCst.imp_total + (eDetMrgCst.imp_total * obj.prc_margen) / 100; uti = eDetMrgCst.imp_total; break;
                    }

                    if (flag == "G")
                    {
                        eDetMrgCst.cod_empresa = empresa;
                        eDetMrgCst.cod_sede_empresa = sedeEmpresa;
                        eDetMrgCst.cod_analisis = analisis;
                        eDetMrgCst.num_servicio = servicio;

                        blAns.Ins_Act_Est_Cst_Analisis<eAnalisis.eAnalisis_Est_Cst>(eDetMrgCst, user.cod_usuario);
                    }

                    lstCst.Add(eDetMrgCst);

                    eDetMrgCst = new eAnalisis.eAnalisis_Est_Cst();


                    pos = pos + 1;
                }

                foreach (eAnalisis.eAnalisis_Est_Cst obj in lstTot)
                {
                    if (obj.cod_item != "GG001")
                    {
                        switch (obj.dsc_item)
                        {
                            case "TOTAL DEL CONTRATO": obj.cod_item = "GG000".Substring(0, 5 - pos.ToString().Length) + pos.ToString(); obj.imp_total = subtotal + gFi + cSp + cCm + gOP + gAD + uti;
                                //Actualizar el card para que el monto salga igual a Total Costo directo
                                tbiTotal.Elements[2].Text = (totalGeneral != 0 ? "S/ " + $"{Math.Round(obj.imp_total, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");
                                break;
                            case "IGV": obj.cod_item = "GG000".Substring(0, 5 - pos.ToString().Length) + pos.ToString(); obj.imp_unitario = subtotal + gFi + cSp + cCm + gOP + gAD + uti; obj.imp_total = ((subtotal + gFi + cSp + cCm + gOP + gAD + uti) * obj.prc_ley) / 100; igv = obj.imp_total; break;
                            case "TOTAL": obj.cod_item = "GG000".Substring(0, 5 - pos.ToString().Length) + pos.ToString(); obj.imp_total = subtotal + gFi + cSp + cCm + gOP + gAD + uti + igv; break;
                        }

                        if (flag == "G")
                        {
                            obj.cod_empresa = empresa;
                            obj.cod_sede_empresa = sedeEmpresa;
                            obj.cod_analisis = analisis;
                            obj.num_servicio = servicio;

                            blAns.Ins_Act_Est_Cst_Analisis<eAnalisis.eAnalisis_Est_Cst>(obj, user.cod_usuario);
                        }

                        lstCst.Add(obj);

                        pos = pos + 1;
                    }
                }

                if (flag == "DT")
                {
                    for (int x = 0; x < gvEstructuraCostos.Columns.Count; x++)
                    {
                        if (gvEstructuraCostos.Columns[x].FieldName.Contains("Operario"))
                        {
                            gvEstructuraCostos.Columns.RemoveAt(x);
                            x = x - 1;
                        }
                    }

                    GridColumn col = null;
                    int index = 1;

                    lstPerAns = lstPerAns.OrderBy(x => x.num_orden).ToList();

                    foreach (eAnalisis.eAnalisis_Personal obj in lstPerAns)
                    {
                        col = new GridColumn();
                        col.FieldName = "Operario" + index;
                        col.Caption = obj.dsc_cargo + " " + obj.dsc_hora_inicio.ToShortTimeString() + "-" + obj.dsc_hora_fin.ToShortTimeString();
                        col.Width = 80;
                        col.VisibleIndex = index + 2;
                        col.OptionsColumn.FixedWidth = true;
                        col.AppearanceCell.Options.UseTextOptions = true;
                        col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        col.OptionsColumn.AllowEdit = false;

                        gvEstructuraCostos.Columns.Add(col);
                        index = index + 1;
                    }

                    dtGeneral = CrearDataTable();
                    gcEstructuraCostos.DataSource = dtGeneral;
                }
            }
            else
            {
                gcEstructuraCostos.DataSource = null;

                for (int x = 0; x < gvEstructuraCostos.Columns.Count; x++)
                {
                    if (gvEstructuraCostos.Columns[x].FieldName.Contains("Operario"))
                    {
                        gvEstructuraCostos.Columns.RemoveAt(x);
                        x = x - 1;
                    }
                }
            }
            
        }

        private DataTable CrearDataTable()
        {
            decimal sueldo = 0, sociales = 0, beneficios = 0, gastos = 0, descansero = 0, movilidad = 0, examenes = 0, polizas = 0, uniforme = 0, essalud = 0, sctr = 0, sctrSoc = 0, sctrAlt = 0, segVida = 0;
            
            DataTable dt = new DataTable();
            dt.Columns.Add("cod_concepto");
            dt.Columns.Add("dsc_concepto");
            dt.Columns.Add("cod_item");
            dt.Columns.Add("dsc_item");
            dt.Columns.Add("prc_ley");
            dt.Columns.Add("imp_unitario");
            dt.Columns.Add("prc_margen");
            dt.Columns.Add("imp_total");


            foreach (eAnalisis.eAnalisis_Est_Cst obj in lstCst)
            {
                DataRow dr = dt.NewRow();
                dr[0] = obj.cod_concepto;
                dr[1] = obj.dsc_concepto;
                dr[2] = obj.cod_item;
                dr[3] = obj.dsc_item;
                dr[4] = Math.Round(obj.prc_ley, 2);
                dr[5] = Math.Round(obj.imp_unitario, 2);
                dr[6] = Math.Round(obj.prc_margen, 2);
                dr[7] = Math.Round(obj.imp_total, 2);
                dt.Rows.Add(dr);
            }

            int col = 8;
            int pers = 0;
            int index = 1;

            foreach (eAnalisis.eAnalisis_Personal obj in lstPerAns) { pers = obj.num_cantidad + pers; }

            foreach (eAnalisis.eAnalisis_Personal obj in lstPerAns)
            {
                dt.Columns.Add("Operario" + index);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    switch (dt.Rows[x][3].ToString())
                    {
                        case "SUELDO": dt.Rows[x][col] = Math.Round(obj.imp_salario * obj.num_cantidad, 2); break;
                        case "ASIGNACIÓN FAMILIAR": dt.Rows[x][col] = Math.Round((lstGenerales[1].num_valor * obj.num_cantidad), 2); break;
                        case "HORAS EXTRA": dt.Rows[x][col] = Math.Round(obj.imp_salario_extra * obj.num_cantidad, 2); break;
                        case "BONIFICACIÓN SEGÚN RENDIMIENTO": dt.Rows[x][col] = Math.Round(obj.imp_bono_productividad * obj.num_cantidad, 2); break;
                        case "BONIFICACIÓN NOCTURNA": dt.Rows[x][col] = Math.Round(obj.imp_bono_nocturno * obj.num_cantidad, 2); break;
                        case "FERIADOS":
                            dt.Rows[x][col] = Math.Round(obj.imp_feriado * obj.num_cantidad, 2); sueldo = Convert.ToDecimal(dt.Rows[x][col]) + Convert.ToDecimal(dt.Rows[x - 1][col]) + Convert.ToDecimal(dt.Rows[x - 2][col]) + Convert.ToDecimal(dt.Rows[x - 3][col]) + Convert.ToDecimal(dt.Rows[x - 4][col]) + Convert.ToDecimal(dt.Rows[x - 5][col]);
                            break;
                        case "ESSALUD": essalud = Math.Round(((sueldo * Convert.ToDecimal(dt.Rows[x][4])) / 100), 2); var valor = Convert.ToDecimal(dt.Rows[x][4]); dt.Rows[x][col] = essalud; break;
                        case "SEGURO VIDA LEY": segVida = (Convert.ToDecimal(dt.Rows[x][7]) / pers) * obj.num_cantidad; dt.Rows[x][col] = segVida; break;
                        case "SEGURO COMPLEMENTARIO (SCTR)": sctr = Math.Round(((sueldo * Convert.ToDecimal(dt.Rows[x][4])) / 100), 2); sctr = sctr + (sctr * (Convert.ToDecimal(dt.Rows[x][6]) / 100)); dt.Rows[x][col] = sctr; break;
                        case "SCTR SOCAVON": sctrSoc = Math.Round(((sueldo * Convert.ToDecimal(dt.Rows[x][4])) / 100), 2); sctrSoc = sctrSoc + (sctrSoc * (Convert.ToDecimal(dt.Rows[x][6]) / 100)); dt.Rows[x][col] = sctrSoc; break;
                        case "SCTR ALTURA":
                            sctrAlt = Math.Round(((sueldo * Convert.ToDecimal(dt.Rows[x][4])) / 100), 2); sctrAlt = sctrAlt + (sctrAlt * (Convert.ToDecimal(dt.Rows[x][6]) / 100)); dt.Rows[x][col] = sctrAlt;
                            break;
                        case "GRATIFICACIÓN + BONIFICACIÓN ESPECIAL": sociales = sueldo + essalud + segVida + sctr + sctrSoc + sctrAlt; dt.Rows[x][col] = Math.Round(((sociales * Convert.ToDecimal(dt.Rows[x][4])) / 100), 2); break;
                        case "VACACIONES": dt.Rows[x][col] = Math.Round(((sociales * Convert.ToDecimal(dt.Rows[x][4])) / 100), 2) + (sociales / 12); break;
                        case "COMPENSACIÓN POR TIEMPO DE SERVICIO (CTS)":
                            dt.Rows[x][col] = Math.Round(((sociales * Convert.ToDecimal(dt.Rows[x][4])) / 100), 2);
                            beneficios = sociales + Convert.ToDecimal(dt.Rows[x][col]) + Convert.ToDecimal(dt.Rows[x - 1][col]) + Convert.ToDecimal(dt.Rows[x - 2][col]);
                            break;
                        case "MOVILIDAD": movilidad = Math.Round(obj.imp_movilidad * obj.num_cantidad, 2); dt.Rows[x][col] = movilidad; break;
                        case "EXÁMENES MÉDICOS OCUPACIONALES (LEY 29783)": examenes = (Convert.ToDecimal(dt.Rows[x][7]) / pers) * obj.num_cantidad; dt.Rows[x][col] = examenes; break;
                        case "POLIZAS RC/DH": polizas = (Convert.ToDecimal(dt.Rows[x][7]) / pers) * obj.num_cantidad; dt.Rows[x][col] = polizas; break;
                        case "UNIFORMES Y EPP":
                            List<eAnalisis.eAnalisis_Personal_Uniformes> lstUnifUnit = lstUnifEpp.FindAll(i => i.cod_cargo == obj.cod_cargo);
                            uniforme = Math.Round(lstUnifUnit.Sum(i => i.imp_venta) * obj.num_cantidad, 2);
                            dt.Rows[x][col] = uniforme;
                            gastos = beneficios + movilidad + examenes + polizas + uniforme;
                            break;
                        case "DESCANSEROS":
                            descansero = obj.flg_descansero ? Math.Round(gastos / 6, 2) : 0; dt.Rows[x][col] = obj.flg_descansero ? descansero : 0;
                            break;
                        //case "SUBTOTAL":
                        //    dt.Rows[x][col] = dt.Rows[x][0].ToString() == "00004" ? Math.Round(gastos + descansero, 2) : 0; 
                        //    break;
                        case "OPERARIOS": dt.Rows[x][col] = obj.num_cantidad; break;
                        case "OTROS CONCEPTOS (SIG)":
                            dt.Rows[x][col] = Convert.ToDecimal(dt.Rows[x][7]) / pers;
                            break;
                        case "TOTAL MANO DE OBRA": dt.Rows[x][col] = Math.Round((gastos + descansero)/* * obj.num_cantidad */, 2); break;
                        case "SUBTOTAL REMUNERACION":
                            //dt.Rows[x][col] = Math.Round(obj.imp_salario + lstGenerales[1].num_valor + obj.imp_salario_extra + obj.imp_bono_productividad + obj.imp_bono_nocturno + obj.imp_feriado, 2); break;
                            dt.Rows[x][col] = sueldo; break;
                        case "SUBTOTAL LEYES SOCIALES":
                            dt.Rows[x][col] = Math.Round((essalud + segVida + sctr + sctrSoc + sctrAlt), 2); break;
                        case "SUBTOTAL BENEFICIOS SOCIALES":
                            dt.Rows[x][col] = Convert.ToDecimal(dt.Rows[x - 1][col]) + Convert.ToDecimal(dt.Rows[x - 2][col]) + Convert.ToDecimal(dt.Rows[x - 3][col]); break;
                        case "SUBTOTAL GASTOS DE PERSONAL":
                            dt.Rows[x][col] = Math.Round((movilidad + examenes + polizas + uniforme /* + descansero */), 2); break;
                    }
                }

                col = col + 1;
                index = index + 1;
            }

            return dt;
        }

        private void EnviarDatos(string entidad)
        {
            switch (entidad)
            {
                case "Producto":
                    lstDatos = new List<eDatos>();

                    if (lstProdAns == null) return;

                    foreach (eAnalisis.eAnalisis_Producto obj in lstProdAns)
                    {
                        eDatos eDat = new eDatos();

                        eDat.AtributoCinco = obj.cod_producto;
                        eDat.AtributoUno = obj.cod_tipo_servicio;
                        eDat.AtributoTres = obj.cod_subtipo_servicio;

                        lstDatos.Add(eDat);
                    }

                    break;
                case "Personal":
                    lstDatos = new List<eDatos>();

                    if (lstPerAns == null) return;

                    foreach (eAnalisis.eAnalisis_Personal obj in lstPerAns)
                    {
                        eDatos eDat = new eDatos();

                        eDat.AtributoTres = obj.cod_cargo;

                        lstDatos.Add(eDat);
                    }

                    break;
                case "Maquinas":
                    lstDatos = new List<eDatos>();

                    if (lstMaqAns == null) return;

                    foreach (eAnalisis.eAnalisis_Maquinaria obj in lstMaqAns)
                    {
                        eDatos eDat = new eDatos();

                        eDat.AtributoTres = obj.cod_activo_fijo;

                        lstDatos.Add(eDat);
                    }

                    break;
            }
        }

        private void CargarDatos(string entidad)
        {
            switch (entidad)
            {
                case "Producto":
                    if (lstProdAns == null) lstProdAns = new List<eAnalisis.eAnalisis_Producto>();

                    foreach (eDatos obj in lstDatos)
                    {
                        eAnalisis.eAnalisis_Producto eDetAns = new eAnalisis.eAnalisis_Producto();

                        eDetAns.cod_producto = obj.AtributoCinco;
                        eDetAns.dsc_producto = obj.AtributoSeis;
                        eDetAns.cod_tipo_servicio = obj.AtributoUno;
                        eDetAns.dsc_tipo_servicio = obj.AtributoDos;
                        eDetAns.cod_subtipo_servicio = obj.AtributoTres;
                        eDetAns.dsc_subtipo_servicio = obj.AtributoCuatro;
                        eDetAns.cod_dotacion = obj.cod_dotacion;
                        eDetAns.cod_unidad_medida = obj.AtributoSiete;
                        eDetAns.dsc_simbolo = obj.AtributoOcho;
                        eDetAns.imp_unitario = obj.AtributoOnce;
                        eDetAns.prc_margen = obj.AtributoDoce;
                        eDetAns.num_cantidad = obj.AtributoTrece;
                        eDetAns.imp_total = obj.AtributoTrece * obj.AtributoOnce;
                        eDetAns.imp_venta = eDetAns.imp_total * (1 + eDetAns.prc_margen / 100);

                        lstProdAns.Add(eDetAns);
                    }

                    CargarTiposProducto();

                    bsProductoAnalisis.DataSource = lstProdAns;
                    gvProducto.RefreshData();

                    break;
                case "Personal":
                    if (lstPerAns == null) lstPerAns = new List<eAnalisis.eAnalisis_Personal>();

                    int cuenta = lstPerAns.Count == 0 ? 1 : lstPerAns.Max(x => x.num_item) + 1;

                    foreach (eDatos obj in lstDatos)
                    {
                        eAnalisis.eAnalisis_Personal ePerAns = new eAnalisis.eAnalisis_Personal();

                        ePerAns.num_item = cuenta;
                        ePerAns.cod_cargo = obj.AtributoTres;
                        ePerAns.dsc_cargo = obj.AtributoCuatro;
                        ePerAns.num_orden = cuenta;
                        ePerAns.cod_turno = "P";
                        ePerAns.num_dia_semana = lkpTiempo.EditValue.ToString() == "D" ? int.Parse(txtPeriodo.EditValue.ToString()) : 0;
                        ePerAns.dsc_hora_inicio = DateTime.Parse("01/01/1900 " + obj.AtributoSiete + ":00.000");
                        ePerAns.dsc_hora_fin = DateTime.Parse("01/01/1900 " + obj.AtributoOcho + ":00.000");
                        ePerAns.dsc_rango_horario = obj.AtributoSiete + " - " + obj.AtributoOcho;
                        ePerAns.num_horas = obj.AtributoDiez;
                        ePerAns.num_horas_extra = 0;
                        ePerAns.imp_salario = obj.AtributoOnce;
                        ePerAns.imp_salario_min = obj.AtributoOnce;
                        ePerAns.imp_salario_max = obj.AtributoDoce;
                        ePerAns.imp_salario_extra = 0;
                        ePerAns.num_cantidad = obj.AtributoTrece;
                        ePerAns.imp_salario_total = obj.AtributoTrece * obj.AtributoOnce;

                        if (lkpTiempo.EditValue.ToString() == "D")
                        {
                            decimal imp_hora = Math.Round((ePerAns.imp_salario + lstGenerales[1].num_valor + ePerAns.imp_bono_productividad) / lstGenerales[8].num_valor, 2);
                            ePerAns.imp_salario = imp_hora * (Convert.ToInt32(txtPeriodo.EditValue) * 8);
                            ePerAns.imp_salario_total = ePerAns.num_cantidad * ePerAns.imp_salario;
                        }

                        List<eAnalisis.eAnalisis_Personal_Uniformes> lstTemp = blAns.ListarGeneral<eAnalisis.eAnalisis_Personal_Uniformes>("Uniformes", empresa: empresa, cargo: ePerAns.cod_cargo);

                        foreach (eAnalisis.eAnalisis_Personal_Uniformes uni in lstTemp)
                        {
                            uni.num_item = ePerAns.num_item;
                            uni.num_cantidad = ePerAns.num_cantidad;
                            uni.imp_total = uni.num_cantidad * uni.imp_unitario;
                            uni.imp_venta = uni.imp_total * (1 + uni.prc_margen / 100);
                            ePerAns.flg_uniforme = true;
                        }

                        if (lstUnifEpp != null) { lstUnifEpp.AddRange(lstTemp); } else { lstUnifEpp = lstTemp; }

                        lstPerAns.Add(ePerAns);

                        cuenta++;
                    }

                    bsPersonalAnalisis.DataSource = lstPerAns;
                    gvPuestos.RefreshData();
                    if (lkpTiempo.EditValue.ToString() != "D") CargarOtros();

                    break;
                case "Tipos":
                    foreach (eAnalisis.eAnalisis_Producto obj in lstProdAns)
                    {
                        foreach (eDatos obj2 in lstDatos)
                        {
                            if (obj.dsc_tipo_servicio == obj2.AtributoDos)
                            {
                                obj.prc_margen = obj2.AtributoOnce;
                                obj.cod_dotacion = obj2.cod_dotacion;
                                obj.imp_venta = obj.imp_total * (1 + obj.prc_margen / 100);
                            }
                        }
                    }

                    break;
                case "Uniformes":
                    break;
                case "Maquinas":
                    if (lstMaqAns == null) lstMaqAns = new List<eAnalisis.eAnalisis_Maquinaria>();

                    foreach (eDatos obj in lstDatos)
                    {
                        eAnalisis.eAnalisis_Maquinaria eMaqAns = new eAnalisis.eAnalisis_Maquinaria();

                        eMaqAns.cod_activo_fijo = obj.AtributoTres;
                        eMaqAns.dsc_activo_fijo = obj.AtributoCuatro;
                        eMaqAns.dsc_grupo_activo_fijo = obj.AtributoDos;
                        eMaqAns.num_cantidad = obj.AtributoTrece;
                        eMaqAns.imp_unitario = obj.AtributoOnce;
                        eMaqAns.imp_total = eMaqAns.num_cantidad * eMaqAns.imp_unitario;
                        eMaqAns.num_meses_dep = obj.AtributoDiez;
                        eMaqAns.imp_mensual = eMaqAns.num_cantidad * obj.AtributoDoce;
                        eMaqAns.prc_margen = obj.AtributoCatorce;
                        eMaqAns.imp_venta = eMaqAns.imp_mensual * (1 + eMaqAns.prc_margen / 100);

                        lstMaqAns.Add(eMaqAns);
                    }

                    bsMaqEquiposAnalisis.DataSource = lstMaqAns;
                    gvMaqEquipo.RefreshData();

                    break;
            }
        }

        private void GenerarExcel(bool showExcel = true)
        {
            if (tcTablas.SelectedTabPage != tpMargenCostos) { CargarMrgCst("DT"); }

            Excel.Application objExcel = new Excel.Application();
            objExcel.Workbooks.Add();

            var workbook = objExcel.ActiveWorkbook;
            var sheet = workbook.Sheets["Hoja1"];
            //objExcel.Visible = true;

            List<eAnalisis.eAnalisis_Est_Cst> listaGenElim = new List<eAnalisis.eAnalisis_Est_Cst>();
            foreach (DataRow row in dtGeneral.Rows)
            {
                eAnalisis.eAnalisis_Est_Cst obj = new eAnalisis.eAnalisis_Est_Cst();
                obj = ToObject<eAnalisis.eAnalisis_Est_Cst>(row);
                listaGenElim.Add(obj);
            }

            decimal imp_sumaprc = (from item in listaGenElim where item.cod_item == "GG002" || item.cod_item == "GG003" || item.cod_item == "GG004" || item.cod_item == "GG005" select item.prc_ley).Sum();
            decimal imp_suma = (from item in listaGenElim where item.cod_item == "GG002" || item.cod_item == "GG003" || item.cod_item == "GG004" || item.cod_item == "GG005" select item.imp_total).Sum();
            //eAnalisis.eAnalisis_Est_Cst objUtil = listaGenElim.Find(x => x.cod_item == "GG007");
            List<eAnalisis.eAnalisis_Est_Cst> listaGen1 = new List<eAnalisis.eAnalisis_Est_Cst>();

            foreach (eAnalisis.eAnalisis_Est_Cst obj in listaGenElim)
            {
                eAnalisis.eAnalisis_Est_Cst obj2 = new eAnalisis.eAnalisis_Est_Cst();


                if (obj.cod_item != "GG002" && obj.cod_item != "GG003" && obj.cod_item != "GG004" && obj.cod_item != "GG005")
                {
                    if (obj.cod_item == "GG001")
                    {
                        obj2.cod_concepto = obj.cod_concepto; obj2.dsc_concepto = "5. COSTOS OPERATIVOS";
                        obj2.cod_item = obj.cod_item; obj2.dsc_item = obj.dsc_item;
                        obj2.prc_ley = obj.prc_ley; obj2.imp_unitario = obj.imp_unitario;
                        obj2.prc_margen = obj.prc_margen; obj2.imp_total = obj.imp_total;
                        listaGen1.Add(obj2);
                    }
                    else
                    {
                        listaGen1.Add(obj);
                    }
                }
                //if (obj.cod_item == "GG002") listaGen1.Add(objUtil);
                if (obj.cod_item == "GG002")
                {
                    obj2.cod_concepto = obj.cod_concepto; obj2.dsc_concepto = obj.dsc_concepto;
                    obj2.cod_item = "GG002"; obj2.dsc_item = "GASTOS ADMINISTRATIVOS Y OPERATIVOS";
                    obj2.prc_ley = imp_sumaprc; obj2.imp_total = imp_suma;
                    listaGen1.Add(obj2);
                }
            }

            try
            {
                objExcel.Sheets.Add();
                var worksheet = workbook.ActiveSheet;
                worksheet.Name = "Estructura de Costos";

                string colFin = obtenerColumna(6 + lstPerAns.Count);
                Boolean num;

                eCliente_Direccion eDir = tvSedesCliente.GetFocusedRow() as eCliente_Direccion;

                objExcel.ActiveWindow.DisplayGridlines = false;
                objExcel.Range["A:A"].ColumnWidth = 3; objExcel.Range["B:B"].ColumnWidth = 3; objExcel.Range["C:C"].ColumnWidth = 5; objExcel.Range["D:D"].ColumnWidth = 44;
                objExcel.Range["B2:" + colFin + "4"].Select();
                objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#002060");
                objExcel.Cells[6, 2] = "Cliente:"; objExcel.Cells[6, 4] = txtCliente.EditValue.ToString();
                objExcel.Cells[7, 2] = "Dirección:"; objExcel.Cells[7, 4] = eDir.dsc_cadena_direccion;
                objExcel.Cells[8, 2] = "Servicio:"; objExcel.Cells[8, 4] = lkpTipoServicio.Text;
                objExcel.Range["D6:D8"].Select(); objExcel.Selection.Font.Bold = true;
                objExcel.Range["G10:" + colFin + "10"].MergeCells = true; objExcel.Cells[10, 7] = eDir.dsc_nombre_direccion;
                objExcel.Range["G10:" + colFin + "10"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                objExcel.Range["G10:" + colFin + "10"].Select(); objExcel.Selection.Font.Bold = true;
                objExcel.Range["G10:" + colFin + "10"].Select(); objExcel.Selection.Borders.Color = Color.FromArgb(0, 0, 0);
                objExcel.Cells[11, 4] = "CONCEPTO";
                objExcel.Cells[11, 5] = "%";
                objExcel.Cells[11, 6] = "TOTALES";

                for (int x = 0; x < lstPerAns.Count; x++)
                {
                    objExcel.Cells[11, x + 7] = lstPerAns[x].dsc_cargo;
                    string colOp = obtenerColumna(x + 7);
                    objExcel.Range[colOp + ":" + colOp].ColumnWidth = 16;
                    objExcel.Range[colOp + ":" + colOp].WrapText = true;
                }

                objExcel.Range["D11:" + colFin + "11"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                objExcel.Range["D11:" + colFin + "11"].VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                objExcel.Range["D11:" + colFin + "11"].Select(); objExcel.Selection.Font.Bold = true;
                objExcel.Range["D11:" + colFin + "11"].Select(); objExcel.Selection.Borders.Color = Color.FromArgb(0, 0, 0);

                objExcel.Cells[12, 4] = listaGen1[0].dsc_concepto;
                objExcel.Range["D12"].Select(); objExcel.Selection.Font.Bold = true;
                int fila = 13, dec = 1;
                string pos = "1";

                for (int x = 0; x < listaGen1.Count; x++)
                {
                    if (listaGen1[x == 0 ? x : x - 1].dsc_concepto.ToString() != listaGen1[x].dsc_concepto.ToString())
                    {
                        objExcel.Cells[fila + x, 4] = listaGen1[x].dsc_concepto;
                        objExcel.Range["D" + (fila + x) + ":D" + (fila + x)].Select(); objExcel.Selection.Font.Bold = true;
                        fila++;
                        pos = listaGen1[x].cod_concepto.ToString().Substring(4, 1);
                        dec = 1;
                    }

                    if (listaGen1[x].dsc_item.ToString() == "SUBTOTAL REMUNERACION" ||
                        listaGen1[x].dsc_item.ToString() == "SUBTOTAL LEYES SOCIALES" ||
                        listaGen1[x].dsc_item.ToString() == "SUBTOTAL BENEFICIOS SOCIALES" ||
                        listaGen1[x].dsc_item.ToString() == "SUBTOTAL GASTOS DE PERSONAL" ||
                        listaGen1[x].dsc_item.ToString() == "SUBTOTAL COSTOS OPERATIVOS" ||
                        listaGen1[x].dsc_item.ToString() == "OPERARIOS" ||
                        listaGen1[x].dsc_item.ToString() == "TOTAL COSTO DIRECTO" ||
                        listaGen1[x].dsc_item.ToString() == "TOTAL MANO DE OBRA")
                    {
                        objExcel.Range["D" + (fila + x).ToString()].Select(); objExcel.Selection.Font.Bold = true; objExcel.Range["D" + (fila + x).ToString()].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        num = true;
                    }
                    else
                    {
                        num = false;
                    }

                    if (listaGen1[x].dsc_item.ToString() == "TOTAL DEL CONTRATO" || listaGen1[x].dsc_item.ToString() == "IGV" || listaGen1[x].dsc_item.ToString() == "TOTAL")
                    {
                        objExcel.Range["D" + (fila + x).ToString()].Select(); objExcel.Selection.Font.Bold = true;
                        objExcel.Range["E" + (fila + x).ToString()].Select(); objExcel.Selection.Font.Bold = true;
                        objExcel.Range["F" + (fila + x).ToString()].Select(); objExcel.Selection.Font.Bold = true;
                    }

                    objExcel.Cells[fila + x, 3] = listaGen1[x].cod_concepto.ToString() == "00006" || num ? "" : pos + "." + (dec++).ToString();
                    objExcel.Cells[fila + x, 4] = listaGen1[x].dsc_item;
                    objExcel.Cells[fila + x, 5] = listaGen1[x].prc_ley.ToString() == "0" || listaGen1[x].prc_ley.ToString() == "0.0" || listaGen1[x].prc_ley.ToString() == "0.00" ? "" : listaGen1[x].prc_ley.ToString();
                    objExcel.Cells[fila + x, 6] = listaGen1[x].imp_total;

                    for (int i = 8; i < dtGeneral.Columns.Count; i++)
                    {
                        objExcel.Cells[fila + x, i - 1] = dtGeneral.Rows[x][i];
                    }
                }

                objExcel.Range["B6:" + colFin + (listaGen1.Count + 17).ToString()].Font.Size = 9;
                objExcel.Range["C11:" + colFin + "11"].Select(); objExcel.Selection.Borders.Color = Color.FromArgb(0, 0, 0);
                objExcel.Range["C12:" + colFin + (listaGen1.Count + 17).ToString()].Select();
                objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeLeft).Color = Color.FromArgb(0, 0, 0);
                objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeBottom).Color = Color.FromArgb(0, 0, 0);
                objExcel.Range["D12:D" + (listaGen1.Count + 17).ToString()].Select(); objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeRight).Color = Color.FromArgb(0, 0, 0);
                objExcel.Range["E12:E" + (listaGen1.Count + 17).ToString()].Select(); objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeRight).Color = Color.FromArgb(0, 0, 0);
                objExcel.Range["F12:F" + (listaGen1.Count + 17).ToString()].Select(); objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeRight).Color = Color.FromArgb(0, 0, 0);

                for (int x = 8; x < dtGeneral.Columns.Count; x++)
                {
                    string letra = obtenerColumna(x - 1);
                    objExcel.Range[letra + "12:" + letra + (listaGen1.Count + 17).ToString()].Select(); objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeRight).Color = Color.FromArgb(0, 0, 0);
                }

                objExcel.Range["E12:E" + (listaGen1.Count + 17).ToString()].NumberFormat = "#,##0.00 [$%-es-PE]";
                objExcel.Range["F12:" + colFin + (listaGen1.Count + 17).ToString()].NumberFormat = "[$S/-es-PE] #,##0.00";

                objExcel.Range["C11:C11"].Select(); objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeRight).Color = Color.FromArgb(255, 255, 255);

                worksheet.Shapes.AddPicture(@"C:\Fuentes\IMPERIUM-Servicios\UI_Servicios\Resources\LogoFacilita.png", MsoTriState.msoFalse, MsoTriState.msoCTrue, 22.5, 18, 80, 40);

                if (!showExcel)
                {
                    worksheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape; // set orientation to landscape
                    worksheet.PageSetup.Zoom = false; // disable automatic zooming
                    worksheet.PageSetup.FitToPagesWide = 1; // fit the width to one page
                    worksheet.PageSetup.FitToPagesTall = 1; // fit the height to one page
                    worksheet.PageSetup.PrintGridlines = false; // print gridlines
                }

                sheet.Delete();

                objExcel.WindowState = Excel.XlWindowState.xlMaximized;
                objExcel.Visible = showExcel;
                objExcel = null;

                if (!showExcel)
                {
                    string rutaExcel = ToolHelper.downloadsFolderPath + ToolHelper.nameExcelFile;
                    if (File.Exists(rutaExcel)) File.Delete(rutaExcel);

                    workbook.SaveAs(rutaExcel);
                    GenerarImgExcel(rutaExcel);
                    workbook.Close();

                    if (File.Exists(rutaExcel)) File.Delete(rutaExcel);

                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Generar Reporte. Message: " + e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarImgExcel(string rutaExcel)
        {
            SpreadsheetControl spreadsheetControl = new SpreadsheetControl();
            spreadsheetControl.LoadDocument(rutaExcel);

            using (PrintingSystem printingSystem = new PrintingSystem())
            {
                PrintableComponentLink link = new PrintableComponentLink(printingSystem);
                link.Component = spreadsheetControl;
                link.CreateDocument();
                link.PrintingSystem.ExportToImage(ToolHelper.downloadsFolderPath + ToolHelper.imagePngFile, new ImageExportOptions(ImageFormat.Png));
            }
        }

        private string obtenerColumna(int n = 0)
        {
            string columna = "";

            while (n > 26)
            {
                columna = columna + "A";
                n = n - 26;
            }

            columna = columna + obtenerLetra(n);

            return columna;
        }

        private string obtenerLetra(int n)
        {
            string letra = "";

            switch (n)
            {
                case 1: letra = "A"; break;
                case 2: letra = "B"; break;
                case 3: letra = "C"; break;
                case 4: letra = "D"; break;
                case 5: letra = "E"; break;
                case 6: letra = "F"; break;
                case 7: letra = "G"; break;
                case 8: letra = "H"; break;
                case 9: letra = "I"; break;
                case 10: letra = "J"; break;
                case 11: letra = "K"; break;
                case 12: letra = "L"; break;
                case 13: letra = "M"; break;
                case 14: letra = "N"; break;
                case 15: letra = "O"; break;
                case 16: letra = "P"; break;
                case 17: letra = "Q"; break;
                case 18: letra = "R"; break;
                case 19: letra = "S"; break;
                case 20: letra = "T"; break;
                case 21: letra = "U"; break;
                case 22: letra = "V"; break;
                case 23: letra = "W"; break;
                case 24: letra = "X"; break;
                case 25: letra = "Y"; break;
                case 26: letra = "Z"; break;
            }

            return letra;
        }

        private List<eAnalisis.eAnalisis_Otros> CambiarOrdenGastosGenerales(List<eAnalisis.eAnalisis_Otros> lstTotGen)
        {
            List<eAnalisis.eAnalisis_Otros> lstNuevoOrden = new List<eAnalisis.eAnalisis_Otros>();

            foreach (eAnalisis.eAnalisis_Otros obj in lstTotGen)
            {
                switch (obj.dsc_descripcion)
                {
                    case "COSTO SUPERVISIÓN": obj.num_item = 1; break;
                    case "COMISIÓN COMERCIAL": obj.num_item = 2; break;
                    case "GASTOS ADMINISTRATIVOS": obj.num_item = 3; break;
                    case "GASTOS OPERATIVOS": obj.num_item = 4; break;
                    case "GASTOS FINANCIEROS": obj.num_item = 5; break;
                    case "UTILIDADES": obj.num_item = 6; break;
                }
                lstNuevoOrden.Add(obj);
            }

            return lstNuevoOrden.OrderBy(x => x.num_item).ToList();
        }

        private string LeerNumeros(int numero)
        {
            string descripcion = "";
            switch (numero)
            {
                case 1: descripcion = "UN"; break;
                case 2: descripcion = "DOS"; break;
                case 3: descripcion = "TRES"; break;
                case 4: descripcion = "CUATRO"; break;
                case 5: descripcion = "CINCO"; break;
                case 6: descripcion = "SEIS"; break;
                case 7: descripcion = "SIETE"; break;
                case 8: descripcion = "OCHO"; break;
                case 9: descripcion = "NUEVE"; break;
                case 10: descripcion = "DIEZ"; break;
            }
            return descripcion;
        }

        private T ToObject<T>(DataRow row) where T : class, new()
        {
            T obj = new T();

            foreach (DataColumn col in row.Table.Columns)
            {
                PropertyInfo prop = obj.GetType().GetProperty(col.ColumnName);
                if (prop != null)
                {
                    string propName = prop.PropertyType.Name;
                    if (propName == sNullable)
                    {
                        propName = Nullable.GetUnderlyingType(prop.PropertyType).Name;
                    }

                    if (prop.CanWrite & !object.ReferenceEquals(row[col], DBNull.Value) & col.DataType.Name == propName)
                    {
                        prop.SetValue(obj, row[col], null);
                    }

                    if (col.ColumnName == "imp_unitario" || col.ColumnName == "imp_total" || col.ColumnName == "prc_ley")
                    {
                        prop.SetValue(obj, Convert.ToDecimal(row[col]), null);
                    }
                }
            }
            return obj;
        }

        #region Events
        private void frmMantAnalisisServicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (gAlcs || gPers || gProd || gMqEq || gOtrs || gMgCt)
            {
                eAnalisis eAns = GuardarCabecera();
                eAnalisis.eAnalisis_Sedes eASed = GuardarDetalleSedes();
                eAnalisis.eAnalisis_Sedes_Prestacion eAPres = GuardarPrestacionSede();

                if (gAlcs) GuardarAlcance();
                if (gPers) { GuardarPersonal(); GuardarUniforme(); }
                if (gProd) GuardarProducto();
                if (gMqEq) GuardarMaquinaria();
                if (gPers || gOtrs) GuardarOtros();
                if (gPers || gProd || gMgCt) CargarMrgCst("G");
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (ValidarCampos() == false)
                {
                    MessageBox.Show(mensaje, "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                eAnalisis eAns = GuardarCabecera();
                eAnalisis.eAnalisis_Sedes eASed = GuardarDetalleSedes();
                eAnalisis.eAnalisis_Sedes_Prestacion eAPres = GuardarPrestacionSede();

                if (gAlcs) GuardarAlcance();
                if (gPers) { GuardarPersonal(); GuardarUniforme(); }
                if (gProd) GuardarProducto();
                if (gMqEq) GuardarMaquinaria();
                if (gPers || gOtrs) GuardarOtros();
                if (gPers || gProd || gMgCt) CargarMrgCst("G");

                MessageBox.Show("Registro generado de manera éxitosa.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                accion = Analisis.Editar;
                empresa = eAns.cod_empresa; sedeEmpresa = eAns.cod_sede_empresa; analisis = eAns.cod_analisis; servicio = eAPres.num_servicio;
                ConfigurarForm();
                CargarGrillas();
                gProd = false; gPers = false; gMqEq = false; gAlcs = false; gOtrs = false; gMgCt = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnOcultarMostrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (nbDatosCliente.OptionsNavPane.NavPaneState == DevExpress.XtraNavBar.NavPaneState.Expanded)
            {
                btnOcultarMostrar.Caption = btnOcultarMostrar.Caption.Replace("Ocultar", "Mostrar");
                ChangeState(DevExpress.XtraNavBar.NavPaneState.Collapsed);
            }
            else
            {
                btnOcultarMostrar.Caption = btnOcultarMostrar.Caption.Replace("Mostrar", "Ocultar");
                ChangeState(DevExpress.XtraNavBar.NavPaneState.Expanded);
            }
        }

        private void ChangeState(DevExpress.XtraNavBar.NavPaneState state)
        {
            System.Reflection.FieldInfo fi = nbDatosCliente.OptionsNavPane.GetType().GetField("allowAnimation", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fi.SetValue(nbDatosCliente.OptionsNavPane, true);
            nbDatosCliente.OptionsNavPane.NavPaneState = state;
        }

        private void btnAgregarServicio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmMantRequerimientoAnalisis frm = new frmMantRequerimientoAnalisis();
            frm.user = user;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.accion = RequerimientoAns.Editar;
            frm.empresa = empresa;
            frm.sedeEmpresa = sedeEmpresa;
            frm.analisis = analisis;
            frm.codigoCliente = codigoCliente;

            frm.ShowDialog();

            blAns.CargaCombosLookUp("TipoPrestacion", lkpTipoServicio, "num_servicio", "dsc_tipo_prestacion", "", valorDefecto: true, cod_empresa: empresa, cod_sede_empresa: sedeEmpresa, cod_analisis: analisis, cod_sede_cliente: sedeCliente);
        }

        private void btnExportarEstCst_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GenerarExcel();
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Busqueda("", "Cliente");
            }
            string dato = blGlobal.pKeyPress(txtCliente, e);
            if (dato != "")
            {
                Busqueda(dato, "Cliente");
            }
            if (dato == "")
            {
                picVerCliente.Enabled = false;
                codigoCliente = "";
            }
        }

        private void picBuscarCliente_Click(object sender, EventArgs e)
        {
            Busqueda("", "Cliente");
        }

        private void picVerCliente_Click(object sender, EventArgs e)
        {
            frmMantCliente frm = new frmMantCliente();
            frm.cod_cliente = codigoCliente;
            frm.MiAccion = Cliente.Vista;
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.cod_empresa = lkpEmpresa.EditValue.ToString();
            frm.user = user;
            frm.ShowDialog();
        }

        private void tvSedesCliente_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.PrevFocusedRowHandle == -2147483648) return;

            eCliente_Direccion eDir = tvSedesCliente.GetFocusedRow() as eCliente_Direccion;
            sedeCliente = eDir.num_linea;

            blAns.CargaCombosLookUp("TipoPrestacion", lkpTipoServicio, "num_servicio", "dsc_tipo_prestacion", "", valorDefecto: true, cod_empresa: empresa, cod_sede_empresa: sedeEmpresa, cod_analisis: analisis, cod_sede_cliente: sedeCliente);
            lkpTipoServicio.ItemIndex = 0;
            servicio = Convert.ToInt32(lkpTipoServicio.EditValue);

            //lstTot.Clear();
            //lstCst.Clear();

            //CargarAnalisis(false);
            //CargarGrillas();
            //CargarMrgCst("DT");
        }

        private void tvSedesCliente_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (gAlcs || gPers || gProd || gMqEq || gOtrs || gMgCt)
            {
                eAnalisis eAns = GuardarCabecera();
                eAnalisis.eAnalisis_Sedes eASed = GuardarDetalleSedes();
                eAnalisis.eAnalisis_Sedes_Prestacion eAPres = GuardarPrestacionSede();

                if (gAlcs) GuardarAlcance();
                if (gPers) { GuardarPersonal(); GuardarUniforme(); }
                if (gProd) GuardarProducto();
                if (gMqEq) GuardarMaquinaria();
                if (gPers || gOtrs) GuardarOtros();
                if (gPers || gProd || gMgCt) CargarMrgCst("G");
            }
        }

        private void ckMostrarSedes_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMostrarSedes.CheckState == CheckState.Checked)
            {
                CargarSedes();
            }
            else
            {
                CargarSedes(lstAns_SedesPrestacion);
            }
        }

        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            blAns.CargaCombosLookUp("Sedes", lkpSedeEmpresa, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());
            lkpSedeEmpresa.EditValue = "00001";
        }

        private void lkpTipoServicio_EditValueChanged(object sender, EventArgs e) 
        {
            servicio = int.Parse(lkpTipoServicio.EditValue.ToString());

            //Limpio las lista de la grilla Margen y Costos
            lstTot.Clear();
            lstCst.Clear();

            CargarAnalisis(false);
            CargarGrillas();
            CargarMrgCst("DT"); //Para que cambie la grilla Margen Costos cuando cambies el tipo de servicio
        }

        private void txtPeriodo_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpTiempo.EditValue.ToString() == "D")
            {
                foreach (eAnalisis.eAnalisis_Personal obj in lstPerAns)
                {
                    obj.num_dia_semana = int.Parse(txtPeriodo.EditValue.ToString());
                }

                gvPuestos.RefreshData();
            }
        }

        private void lkpTiempo_EditValueChanged(object sender, EventArgs e)
        {

            if (lkpTiempo.EditValue.ToString() == "D")
            {
                foreach (eAnalisis.eAnalisis_Personal obj in lstPerAns)
                {
                    decimal imp_hora = Math.Round((obj.imp_salario + lstGenerales[1].num_valor + obj.imp_bono_productividad) / lstGenerales[8].num_valor, 2);
                    decimal imp_hora_noc = Math.Round((((lstGenerales[2].num_valor * Convert.ToDecimal(1.35)) - obj.imp_salario) / lstGenerales[8].num_valor) + imp_hora, 2);

                    obj.num_dia_semana = int.Parse(txtPeriodo.EditValue.ToString());
                    obj.imp_salario = obj.imp_horas_diu + obj.imp_horas_noc;
                    obj.imp_salario_extra = obj.imp_horas_ext_diu + obj.imp_horas_ext_noc;

                    obj.imp_bono_nocturno = (obj.num_horas_noc * (imp_hora_noc - imp_hora)) * obj.num_dia_semana;
                    obj.imp_salario = obj.imp_salario * obj.num_dia_semana;
                    obj.imp_salario_extra = obj.imp_salario_extra * obj.num_dia_semana;
                }
            }
            else
            {
                if (lstPerAns == null) return;

                foreach (eAnalisis.eAnalisis_Personal obj in lstPerAns)
                {
                    decimal imp_hora = Math.Round((obj.imp_salario + lstGenerales[1].num_valor + obj.imp_bono_productividad) / lstGenerales[8].num_valor, 2);
                    decimal imp_hora_noc = Math.Round((((lstGenerales[2].num_valor * Convert.ToDecimal(1.35)) - obj.imp_salario) / lstGenerales[8].num_valor) + imp_hora, 2);

                    obj.imp_salario_extra = obj.imp_horas_ext_diu + obj.imp_horas_ext_noc;
                    obj.imp_bono_nocturno = (Math.Round(obj.num_horas_noc * (imp_hora_noc - imp_hora), 2)) * lstGenerales[9].num_valor;
                    obj.imp_salario_extra = obj.imp_salario_extra * lstGenerales[9].num_valor;
                }
            }

            gvPuestos.RefreshData();
        }

        private void nbDatosCliente_NavPaneStateChanged(object sender, EventArgs e)
        {
            if (nbDatosCliente.OptionsNavPane.NavPaneState == DevExpress.XtraNavBar.NavPaneState.Collapsed)
            {
                btnOcultarMostrar.Caption = btnOcultarMostrar.Caption.Replace("Ocultar", "Mostrar");

                controlTipoServicio.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                controlFechaRequerimiento.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                controlPeriodo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                controlTiempo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                controlMetros2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                controlMetros3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                btnOcultarMostrar.Caption = btnOcultarMostrar.Caption.Replace("Mostrar", "Ocultar");

                controlTipoServicio.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                controlFechaRequerimiento.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                controlPeriodo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                controlTiempo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                controlMetros2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                controlMetros3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }

        private void tbTotales_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "tbiPersonal":
                    tbiProductos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiPersonal.AppearanceItem.Normal.BackColor = Color.FromArgb(46, 85, 88);
                    tbiMaqEquipos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiOtros.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiTotal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tcTablas.SelectedTabPage = tpPersonal;
                    break;
                case "tbiProductos":
                    tbiProductos.AppearanceItem.Normal.BackColor = Color.FromArgb(46, 85, 88);
                    tbiPersonal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiMaqEquipos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiOtros.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiTotal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tcTablas.SelectedTabPage = tpProductos;
                    break;
                case "tbiMaqEquipos":
                    tbiProductos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiPersonal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiMaqEquipos.AppearanceItem.Normal.BackColor = Color.FromArgb(46, 85, 88);
                    tbiOtros.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiTotal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tcTablas.SelectedTabPage = tpMaqEquipos;
                    break;
                case "tbiOtros":
                    tbiProductos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiPersonal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiMaqEquipos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiOtros.AppearanceItem.Normal.BackColor = Color.FromArgb(46, 85, 88);
                    tbiTotal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tcTablas.SelectedTabPage = tpOtros;
                    break;
                case "tbiTotal":
                    tbiProductos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiPersonal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiMaqEquipos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiOtros.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiTotal.AppearanceItem.Normal.BackColor = Color.FromArgb(46, 85, 88);
                    tcTablas.SelectedTabPage = tpMargenCostos;
                    break;
            }
        }

        private void tbTotales_ItemDoubleClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            if (e.Item.Name == "tbiProductos")
            {
                frmBusquedaItems frm = new frmBusquedaItems();
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.accion = Buscar.Tipos;
                frm.lstDatos = lstTipos;
                frm.ShowDialog();


                lstDatos = frm.lstDatos;
                if (lstDatos != null && lstDatos.Count > 0)
                {
                    CargarDatos(frm.entidad);
                    CargarMontosProductos();
                }
            }
        }

        private void tcTablas_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (e.Page.Name)
            {
                case "tpAlcance":
                    tbiProductos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiPersonal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiMaqEquipos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiOtros.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiTotal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    break;
                case "tpPuestos":
                    tbiProductos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiPersonal.AppearanceItem.Normal.BackColor = Color.FromArgb(46, 85, 88);
                    tbiMaqEquipos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiOtros.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiTotal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tcTablas.SelectedTabPage = tpPuestos;
                    break;
                case "tpProductos":
                    tbiProductos.AppearanceItem.Normal.BackColor = Color.FromArgb(46, 85, 88);
                    tbiPersonal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiMaqEquipos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiOtros.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiTotal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tcTablas.SelectedTabPage = tpProductos;
                    break;
                case "tpMaqEquipos":
                    tbiProductos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiPersonal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiMaqEquipos.AppearanceItem.Normal.BackColor = Color.FromArgb(46, 85, 88);
                    tbiOtros.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiTotal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tcTablas.SelectedTabPage = tpMaqEquipos;
                    break;
                case "tpOtros":
                    tbiProductos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiPersonal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiMaqEquipos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiOtros.AppearanceItem.Normal.BackColor = Color.FromArgb(46, 85, 88);
                    tbiTotal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tcTablas.SelectedTabPage = tpOtros;
                    CargarTotales();
                    break;
                case "tpMargenCostos":
                    tbiProductos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiPersonal.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiMaqEquipos.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiOtros.AppearanceItem.Normal.BackColor = Color.FromArgb(23, 97, 143);
                    tbiTotal.AppearanceItem.Normal.BackColor = Color.FromArgb(46, 85, 88);
                    tcTablas.SelectedTabPage = tpMargenCostos;
                    CargarMrgCst("DT");
                    break;
            }
        }

        private void gvAlcance_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gAlcs = true;
        }

        private void gvAlcance_KeyPress(object sender, KeyPressEventArgs e)
        {
            //gAlcs = true;
            //eAnalisis.eAnalisis_Alcance obj = gvAlcance.GetFocusedRow() as eAnalisis.eAnalisis_Alcance;

            //bsAlcanceAnalisis.Add(obj);
        }

        private void gvAlcance_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvAlcance_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void rbtnClonarArea_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            eAnalisis.eAnalisis_Alcance obj = gvAlcance.GetFocusedRow() as eAnalisis.eAnalisis_Alcance;
            eAnalisis.eAnalisis_Alcance obj2 = new eAnalisis.eAnalisis_Alcance();

            obj2.dsc_actividad = obj.dsc_actividad;

            bsAlcanceAnalisis.Add(obj2);
        }

        private void rbtnEliminarAlcance_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            gAlcs = true;

            eAnalisis.eAnalisis_Alcance obj = gvAlcance.GetFocusedRow() as eAnalisis.eAnalisis_Alcance;

            lstAlcAnsElim.Add(obj);
            bsAlcanceAnalisis.Remove(obj);
        }

        private void gvPuestos_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (gvPuestos.FocusedRowHandle >= 0 && (gvPuestos.FocusedColumn.FieldName == "dsc_cargo" || gvPuestos.FocusedColumn.FieldName == "num_horas" || gvPuestos.FocusedColumn.FieldName == "num_horas_extra" || gvPuestos.FocusedColumn.FieldName == "imp_salario_extra" || gvPuestos.FocusedColumn.FieldName == "imp_salario_total")) e.Cancel = true;

            if (gvPuestos.FocusedRowHandle >= 0 && (gvPuestos.FocusedColumn.FieldName == "cod_turno" || gvPuestos.FocusedColumn.FieldName == "dsc_hora_inicio" || gvPuestos.FocusedColumn.FieldName == "dsc_hora_fin" || gvPuestos.FocusedColumn.FieldName == "flg_almuerzo"))
            {
                eAnalisis.eAnalisis_Personal obj = gvPuestos.GetFocusedRow() as eAnalisis.eAnalisis_Personal;

                if (obj.flg_horario == false)
                {
                    e.Cancel = true;
                }
            }

            if (gvPuestos.FocusedRowHandle < 0)
            {
                gPers = true;

                EnviarDatos("Personal");

                frmBusquedaItems frm = new frmBusquedaItems();
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.empresa = lkpEmpresa.EditValue.ToString();
                frm.sede = lkpSedeEmpresa.EditValue.ToString();
                frm.accion = Buscar.Personal;
                frm.user = user;
                frm.lstDatos = lstDatos;
                frm.ShowDialog();

                lstDatos = frm.lstDatos;
                if (lstDatos != null && lstDatos.Count > 0)
                {
                    CargarDatos(frm.entidad);
                    CargarMontosPersonal();
                    CargarTotales();
                }

                gvPuestos.RefreshData();
                gvPersonal.RefreshData();
            }
        }

        private void gvPuestos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (gvPuestos.FocusedColumn.Name == "colnum_orden")
            {
                eAnalisis.eAnalisis_Personal obj = gvPuestos.GetFocusedRow() as eAnalisis.eAnalisis_Personal;

                lstPerAns = lstPerAns.OrderBy(x => x.num_orden).ToList();

                int ord = 1;

                for (int x = 0; x < lstPerAns.Count; x++)
                {
                    if (lstPerAns[x].num_item != obj.num_item)
                    {
                        if (lstPerAns[x].num_orden == obj.num_orden)
                        {
                            ord = lstPerAns.Count == lstPerAns[x].num_orden ? ord : ord + 1;
                        }

                        lstPerAns[x].num_orden = ord;

                        if ((ord + 1) == lstPerAns[x == 0 ? 0 : x - 1].num_orden)
                        {
                            ord = ord + 1;
                        }

                        ord = ord + 1;
                    }
                }
                bsPersonalAnalisis.DataSource = lstPerAns;
                gvPuestos.RefreshData();
            }

            if (gvPuestos.FocusedColumn.Name == "colnum_dia_semana")
            {
                eAnalisis.eAnalisis_Personal obj = gvPuestos.GetFocusedRow() as eAnalisis.eAnalisis_Personal;

                if (lkpTiempo.EditValue.ToString() == "D" && obj.num_dia_semana > int.Parse(txtPeriodo.EditValue.ToString())) { MessageBox.Show("La cantidad de días laborales no pueden ser mayor al periodo ingresado.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); obj.num_dia_semana = int.Parse(txtPeriodo.EditValue.ToString()); }
                if (lkpTiempo.EditValue.ToString() != "D" && obj.num_dia_semana > 7) { MessageBox.Show("La cantidad de días laborales no pueden ser mayor a 7.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); obj.num_dia_semana = 7; }
            }

            if (gvPuestos.FocusedColumn.Name == "colnum_cantidad_pue")
            {
                eAnalisis.eAnalisis_Personal obj = gvPuestos.GetFocusedRow() as eAnalisis.eAnalisis_Personal;

                if (obj.num_cantidad <= 0) { MessageBox.Show("Debe seleccionar una cantidad mayor a cero.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); obj.num_cantidad = 1; }

                obj.imp_salario_total = obj.num_cantidad * (obj.imp_salario + obj.imp_bono_nocturno + obj.imp_movilidad + obj.imp_salario_extra + obj.imp_bono_productividad + obj.imp_feriado);

                foreach (eAnalisis.eAnalisis_Personal_Uniformes uni in lstUnifEpp)
                {
                    if (uni.cod_cargo == obj.cod_cargo)
                    {
                        uni.num_cantidad = obj.num_cantidad;
                        uni.imp_total = uni.num_cantidad * uni.imp_unitario;
                        uni.imp_venta = uni.imp_total * (1 + uni.prc_margen / 100);
                    }
                }
            }

            if (gvPuestos.FocusedColumn.Name == "colflg_horario")
            {
                eAnalisis.eAnalisis_Personal obj = gvPuestos.GetFocusedRow() as eAnalisis.eAnalisis_Personal;

                obj.dsc_hora_inicio = DateTime.Parse("01/01/1900 00:00:00.000");
                obj.dsc_hora_fin = DateTime.Parse("01/01/1900 00:00:00.000");
                obj.num_horas = 0;
                obj.num_horas_extra = 0;
                obj.flg_feriado = false;
                obj.imp_salario_extra = 0;
                obj.imp_bono_nocturno = 0;
                obj.imp_feriado = 0;
                obj.imp_salario_total = obj.imp_salario + obj.imp_salario_extra + obj.imp_bono_nocturno + obj.imp_feriado + obj.imp_bono_productividad + obj.imp_movilidad;
            }

            if (gvPuestos.FocusedColumn.Name == "coldsc_hora_inicio" || gvPuestos.FocusedColumn.Name == "coldsc_hora_fin" || gvPuestos.FocusedColumn.Name == "colnum_hora_dia" || gvPuestos.FocusedColumn.Name == "colnum_min_almuerzo")
            {
                eAnalisis.eAnalisis_Personal obj = gvPuestos.GetFocusedRow() as eAnalisis.eAnalisis_Personal;

                if (gvPuestos.FocusedColumn.Name == "colnum_hora_dia") obj.dsc_hora_fin = obj.dsc_hora_inicio.AddHours(obj.num_hora_dia);
                if (gvPuestos.FocusedColumn.Name == "coldsc_hora_inicio") obj.dsc_hora_fin = obj.dsc_hora_inicio.AddHours(obj.num_hora_dia);

                obj.dsc_rango_horario = obj.dsc_hora_inicio.ToShortTimeString() + " - " + obj.dsc_hora_fin.ToString("hh:mm tt");

                obj.dsc_hora_fin = obj.dsc_hora_fin >= DateTime.Parse("02/01/1900 00:00:00.000") ? obj.dsc_hora_fin.AddDays(-1) : obj.dsc_hora_fin;
                obj.dsc_hora_fin = obj.dsc_hora_fin < obj.dsc_hora_inicio ? obj.dsc_hora_fin.AddDays(1) : obj.dsc_hora_fin;
                decimal horas = Convert.ToInt32((obj.dsc_hora_fin - obj.dsc_hora_inicio).TotalHours);

                obj.num_horas = horas > 8 ? 8 : obj.num_min_almuerzo == 60 ? horas - 1 : obj.num_min_almuerzo == 30 ? horas - Convert.ToDecimal(0.5) : obj.num_min_almuerzo == 45 ? horas - Convert.ToDecimal(0.75) : horas;
                obj.num_horas_extra = horas > 8 ? obj.num_min_almuerzo == 60 ? (horas - 8) - 1 : obj.num_min_almuerzo == 30 ? (horas - 8) - Convert.ToDecimal(0.5) : obj.num_min_almuerzo == 45 ? (horas - 8) - Convert.ToDecimal(0.75) : (horas - 8) : 0;
                obj.num_hora_dia = Convert.ToInt32(horas);

                obj.num_horas_diu = 0;
                obj.num_horas_ext_diu = 0;
                obj.num_horas_noc = 0;
                obj.num_horas_ext_noc = 0;

                for (int x = 1; x <= obj.num_horas; x++)
                {
                    DateTime horaTemp = obj.dsc_hora_inicio.AddHours(x);

                    if (horaTemp > DateTime.Parse("01/01/1900 06:00:00.000") && horaTemp <= DateTime.Parse("01/01/1900 22:00:00.000"))
                    {
                        obj.num_horas_diu++;
                    }

                    if (horaTemp <= DateTime.Parse("01/01/1900 06:00:00.000"))
                    {
                        obj.num_horas_noc++;
                    }

                    if (horaTemp > DateTime.Parse("01/01/1900 22:00:00.000"))
                    {
                        obj.num_horas_noc++;
                    }
                }

                for (int x = 1; x <= obj.num_horas_extra; x++)
                {
                    DateTime horaTemp = obj.dsc_hora_inicio.AddHours(8).AddHours(x);
                    horaTemp = horaTemp > DateTime.Parse("02/01/1900 00:00:00.000") ? horaTemp.AddDays(-1) : horaTemp;

                    if (horaTemp > DateTime.Parse("01/01/1900 06:00:00.000") && horaTemp <= DateTime.Parse("01/01/1900 22:00:00.000"))
                    {
                        obj.num_horas_ext_diu++;
                    }

                    if (horaTemp <= DateTime.Parse("01/01/1900 06:00:00.000"))
                    {
                        obj.num_horas_ext_noc++;
                    }

                    if (horaTemp > DateTime.Parse("01/01/1900 22:00:00.000"))
                    {
                        obj.num_horas_ext_noc++;
                    }
                }

                decimal dec = obj.num_horas_extra;

                for (int x = 0; x <= obj.num_horas_extra; x++)
                {
                    DateTime horaTemp = obj.dsc_hora_inicio.AddHours(8).AddHours(x + 1);
                    horaTemp = horaTemp > DateTime.Parse("02/01/1900 00:00:00.000") ? horaTemp.AddDays(-1) : horaTemp;

                    if (horaTemp > DateTime.Parse("01/01/1900 06:00:00.000") && horaTemp <= DateTime.Parse("01/01/1900 22:00:00.000"))
                    {
                        if (dec == Convert.ToDecimal(0.25))
                        {
                            obj.num_horas_ext_diu = obj.num_horas_ext_diu + Convert.ToDecimal(0.25);
                            return;
                        }
                    }

                    if (horaTemp <= DateTime.Parse("01/01/1900 06:00:00.000"))
                    {
                        if (dec == Convert.ToDecimal(0.25))
                        {
                            obj.num_horas_ext_noc = obj.num_horas_ext_noc + Convert.ToDecimal(0.25);
                            return;
                        }
                    }

                    if (horaTemp > DateTime.Parse("01/01/1900 22:00:00.000"))
                    {
                        if (dec == Convert.ToDecimal(0.25))
                        {
                            obj.num_horas_ext_noc = obj.num_horas_ext_noc + Convert.ToDecimal(0.25);
                            return;
                        }
                    }

                    dec = dec - 1;
                }

                decimal imp_hora = Math.Round((obj.imp_salario + lstGenerales[1].num_valor + obj.imp_bono_productividad) / lstGenerales[8].num_valor, 4);
                obj.imp_horas_diu = Math.Round(obj.num_horas_diu * imp_hora, 2);
                obj.imp_horas_ext_diu = Math.Round(obj.num_horas_ext_diu > 2 ? (2 * Math.Round((imp_hora * Convert.ToDecimal(1.25)), 2)) + ((obj.num_horas_ext_diu - 2) * Math.Round((imp_hora * Convert.ToDecimal(1.35)), 2)) : obj.num_horas_ext_diu * (imp_hora * Convert.ToDecimal(1.25)), 2);

                decimal imp_hora_noc = Math.Round((((lstGenerales[2].num_valor * Convert.ToDecimal(1.35)) - obj.imp_salario) / lstGenerales[8].num_valor) + imp_hora, 4);
                obj.imp_horas_noc = Math.Round(obj.num_horas_noc * imp_hora_noc, 2);
                obj.imp_horas_ext_noc = Math.Round(obj.num_horas_ext_noc > 2 ? (2 * Math.Round((imp_hora_noc * Convert.ToDecimal(1.25)), 2)) + ((obj.num_horas_ext_noc - 2) * Math.Round((imp_hora_noc * Convert.ToDecimal(1.35)), 2)) : obj.num_horas_ext_noc * (imp_hora_noc * Convert.ToDecimal(1.25)), 2);

                if (lkpTiempo.EditValue.ToString() == "D")
                {
                    obj.imp_salario = obj.imp_horas_diu + obj.imp_horas_noc;
                    obj.imp_salario_extra = obj.imp_horas_ext_diu + obj.imp_horas_ext_noc;

                    obj.imp_bono_nocturno = (obj.num_horas_noc * (imp_hora_noc - imp_hora)) * obj.num_dia_semana;
                    obj.imp_salario = obj.imp_salario * obj.num_dia_semana;
                    obj.imp_salario_extra = obj.imp_salario_extra * obj.num_dia_semana;
                }
                else
                {
                    obj.imp_salario_extra = obj.imp_horas_ext_diu + obj.imp_horas_ext_noc;
                    obj.imp_bono_nocturno = (Math.Round(obj.num_horas_noc * (imp_hora_noc - imp_hora), 2)) * lstGenerales[9].num_valor;
                    obj.imp_salario_extra = obj.imp_salario_extra * lstGenerales[9].num_valor;
                }

                obj.imp_salario_total = obj.num_cantidad * (obj.imp_salario + obj.imp_bono_nocturno + obj.imp_movilidad + obj.imp_salario_extra + obj.imp_bono_productividad + obj.imp_feriado);
            }

            if (gvPuestos.FocusedColumn.Name == "colflg_feriado")
            {
                eAnalisis.eAnalisis_Personal obj = gvPuestos.GetFocusedRow() as eAnalisis.eAnalisis_Personal;

                obj.imp_feriado = obj.flg_feriado ? ((obj.imp_salario / 30) * lstGenerales[10].num_valor * 2) / 12 : 0;

                obj.imp_salario_total = obj.num_cantidad * (obj.imp_salario + obj.imp_bono_nocturno + obj.imp_movilidad + obj.imp_salario_extra + obj.imp_bono_productividad + obj.imp_feriado);
            }

            if (gvPuestos.FocusedColumn.Name == "colnum_cantidad_pue" || gvPuestos.FocusedColumn.Name == "coldsc_hora_inicio" || gvPuestos.FocusedColumn.Name == "coldsc_hora_fin" || gvPuestos.FocusedColumn.Name == "colflg_feriado" || gvPuestos.FocusedColumn.Name == "colnum_hora_dia" || gvPuestos.FocusedColumn.Name == "colnum_min_almuerzo")
            {
                gPers = true;

                CargarOtros(false);
                CargarMontosPersonal();
                CargarTotales();
            }

            if (e.Column.FieldName == "flg_descansero")//LDAC - 01/02/2023 - Calcular descansero en la tarjeta Total al dar check
            {
                //Para el Lunes, corregir (crear método) que calcule el descansero individual. Ahora cuando agregas o eliminas te suma o resta el total de todos los descanseros, debe de calcular solo el descanser que se está agregando
                gPers = true;
                CargarMrgCst("");
                //eAnalisis.eAnalisis_Personal obj = gvPuestos.GetFocusedRow() as eAnalisis.eAnalisis_Personal;
                //decimal totalGeneralCard = 0, totalGeneralCardMg = 0;
                //totalGeneralCard = decimal.Parse(tbiTotal.Elements[1].Text.Substring(3));
                //totalGeneralCardMg = decimal.Parse(tbiTotal.Elements[2].Text.Substring(3));

                //descanseroCard = DescanseroIndividual(obj.num_item);

                //if (obj.flg_descansero)
                //{
                //    totalGeneralCard = totalGeneralCard + descanseroCard;
                //    totalGeneralCardMg = totalGeneralCardMg + descanseroCard;
                //}
                //else
                //{
                //    totalGeneralCard = totalGeneralCard - descanseroCard;
                //    totalGeneralCardMg = totalGeneralCardMg - descanseroCard;
                //}

                //tbiTotal.Elements[1].Text = (totalGeneral != 0 ? "S/ " + $"{Math.Round(totalGeneralCard, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");
                //tbiTotal.Elements[2].Text = (totalGeneralMg != 0 ? "S/ " + $"{Math.Round(totalGeneralCardMg, 2).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "S/. 0.00");

            }

            gvPersonal.RefreshData();
        }

        private void gvPuestos_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvPuestos_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                string cell = gvPuestos.GetRowCellDisplayText(e.RowHandle, e.Column);

                if (cell == "0" || cell == "0.0000")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                }

                if (e.Column.Name == "colrbtnUniformes")
                {
                    eAnalisis.eAnalisis_Personal obj = gvPuestos.GetRow(e.RowHandle) as eAnalisis.eAnalisis_Personal;

                    if (obj != null && obj.flg_uniforme)
                    {
                        e.Appearance.BackColor = Color.FromArgb(23, 97, 143);
                    }
                }
            }
        }

        private void gvPuestos_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvPuestos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eAnalisis.eAnalisis_Personal obj = gvPuestos.GetRow(e.RowHandle) as eAnalisis.eAnalisis_Personal;

                    if (e.Column.FieldName == "num_horas" && obj.num_horas == 0) e.DisplayText = "";
                    if (e.Column.FieldName == "num_horas_extra" && obj.num_horas_extra == 0) e.DisplayText = "";
                    if (e.Column.FieldName == "num_hora_dia" && obj.num_hora_dia == 0) e.DisplayText = "";
                    if (e.Column.FieldName == "num_dia_semana" && obj.num_dia_semana == 0) e.DisplayText = "";

                    e.DefaultDraw();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rckbDescansero_CheckedChanged(object sender, EventArgs e)
        {
            gvPuestos.PostEditor();
        }

        private void rckbHorario_CheckedChanged(object sender, EventArgs e)
        {
            gvPuestos.PostEditor();
        }

        private void rckbFeriado_CheckedChanged(object sender, EventArgs e)
        {
            gvPuestos.PostEditor();
        }

        private void rlkpRefrigerio_EditValueChanged(object sender, EventArgs e)
        {
            gvPuestos.PostEditor();
        }

        private void rbtnUniformes_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            gPers = true;

            eAnalisis.eAnalisis_Personal obj = gvPuestos.GetFocusedRow() as eAnalisis.eAnalisis_Personal;

            EnviarDatos("Uniformes");

            frmBusquedaItems frm = new frmBusquedaItems();
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.Text = "Uniformes";
            frm.empresa = lkpEmpresa.EditValue.ToString();
            frm.accion = Buscar.Uniformes;
            frm.user = user;
            frm.cargo = obj.cod_cargo;
            frm.item = obj.num_item;
            frm.lstPers = lstPerAns;
            frm.lstUnifEpp = lstUnifEpp;
            frm.ShowDialog();

            lstUnifEpp = frm.lstUnifEpp;
            lstUniAnsElim = frm.lstUnifElim;
            CargarUniformes();
            CargarMontosPersonal();
            CargarTotales();
        }

        private void rbtnClonar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            gPers = true;

            eAnalisis.eAnalisis_Personal obj = gvPuestos.GetFocusedRow() as eAnalisis.eAnalisis_Personal;
            eAnalisis.eAnalisis_Personal obj2 = new eAnalisis.eAnalisis_Personal();

            int cuenta = lstPerAns.Max(x => x.num_item) + 1;

            //obj2 = obj;
            obj2.num_item = cuenta;
            obj2.cod_cargo = obj.cod_cargo;
            obj2.dsc_cargo = obj.dsc_cargo;
            obj2.num_orden = 0;
            obj2.flg_descansero = obj.flg_descansero;
            obj2.flg_horario = obj.flg_horario;
            obj2.cod_turno = "P";
            obj2.dsc_hora_inicio = DateTime.Parse(obj.dsc_hora_inicio.ToString());
            obj2.dsc_hora_fin = DateTime.Parse(obj.dsc_hora_fin.ToString());
            obj2.dsc_rango_horario = obj.dsc_rango_horario;
            obj2.num_horas = obj.num_horas;
            obj2.num_horas_diu = obj.num_horas_diu;
            obj2.num_horas_noc = obj.num_horas_noc;
            obj2.num_horas_extra = obj.num_horas_extra;
            obj2.num_horas_ext_diu = obj.num_horas_ext_diu;
            obj2.num_horas_ext_noc = obj.num_horas_ext_noc;
            obj2.num_min_almuerzo = obj.num_min_almuerzo;
            obj2.num_hora_dia = obj.num_hora_dia;
            obj2.num_dia_semana = obj.num_dia_semana;
            obj2.flg_feriado = obj.flg_feriado;
            obj2.num_cantidad = obj.num_cantidad;
            obj2.imp_salario = obj.imp_salario;
            obj2.imp_salario_min = obj.imp_salario_min;
            obj2.imp_salario_max = obj.imp_salario_max;
            obj2.imp_horas_diu = obj.imp_horas_diu;
            obj2.imp_horas_noc = obj.imp_horas_noc;
            obj2.imp_salario_extra = obj.imp_salario_extra;
            obj2.imp_horas_ext_diu = obj.imp_horas_ext_diu;
            obj2.imp_horas_ext_noc = obj.imp_horas_ext_noc;
            obj2.imp_bono_nocturno = obj.imp_bono_nocturno;
            obj2.imp_bono_productividad = obj.imp_bono_productividad;
            obj2.imp_movilidad = obj.imp_movilidad;
            obj2.imp_feriado = obj.imp_feriado;
            obj2.imp_salario_total = obj.imp_salario_total;
            obj2.flg_uniforme = obj.flg_uniforme;

            //obj.num_cantidad = 0;
            obj.imp_bono_nocturno = 0;
            obj.imp_movilidad = 0;
            obj.imp_salario_extra = 0;
            obj.imp_bono_productividad = 0;
            obj.imp_feriado = 0;
            obj.imp_salario_total = 0;

            List<eAnalisis.eAnalisis_Personal_Uniformes> lstTemp = blAns.ListarGeneral<eAnalisis.eAnalisis_Personal_Uniformes>("Uniformes", empresa: empresa, cargo: obj2.cod_cargo);

            foreach (eAnalisis.eAnalisis_Personal_Uniformes uni in lstTemp)
            {
                uni.num_item = obj2.num_item;
                uni.num_cantidad = 1;
                uni.imp_total = uni.num_cantidad * uni.imp_unitario;
                uni.imp_venta = uni.imp_total * (1 + uni.prc_margen / 100);
                obj2.flg_uniforme = true;
            }

            if (lstUnifEpp != null) { lstUnifEpp.AddRange(lstTemp); } else { lstUnifEpp = lstTemp; }


            //bsPersonalAnalisis.Add(obj2); - anteriormente lo agregaba defrente
            lstPerAns.Add(obj2);//LDAC - 01/02/2023 - agrega defrente al List y luego lo asigna al binding source
            bsPersonalAnalisis.DataSource = lstPerAns; //LDAC - 01/02/2023 - agrega defrente al List y luego lo asigna al binding source

            //LDAC - 30/01/2023
            CargarOtros(false);
            CargarMontosPersonal();
            CargarTotales();
        }

        private void rbtnEliminarPersonal_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            gPers = true;

            eAnalisis.eAnalisis_Personal ePer = gvPuestos.GetFocusedRow() as eAnalisis.eAnalisis_Personal;

            lstPerAnsElim.Add(ePer);
            lstPerAns.Remove(ePer);

            //Volver a ordenar el num_orden de los elementos
            lstPerAns = lstPerAns.OrderBy(x => x.num_orden).ToList();
            int ord = 1;
            for (int i = 0; i < lstPerAns.Count; i++)
            {
                lstPerAns[i].num_orden = ord;
                ord = ord + 1;
            }

            bsPersonalAnalisis.DataSource = lstPerAns;

            lstUniAnsElim.AddRange(lstUnifEpp.FindAll(x => x.cod_cargo == ePer.cod_cargo && x.num_item == ePer.num_item));
            lstUnifEpp.RemoveAll(x => x.cod_cargo == ePer.cod_cargo && x.num_item == ePer.num_item);

            CargarOtros(false);
            CargarMontosPersonal();
            CargarTotales();
        }

        private void gvPersonal_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2)
            {
                gPers = true;

                eAnalisis.eAnalisis_Personal obj = gvPuestos.GetFocusedRow() as eAnalisis.eAnalisis_Personal;

                frmMantCargos frm = new frmMantCargos();
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.accion = Cargo.Editar;
                frm.user = user;
                frm.empresa = empresa;
                frm.sedeEmpresa = sedeEmpresa;
                frm.area = "00002";
                frm.cargo = obj.cod_cargo;
                frm.ShowDialog();

                obj.imp_salario_min = frm.salMin;
                obj.imp_salario_max = frm.salMax;
            }
        }

        private void gvPersonal_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (gvPersonal.FocusedColumn.Name == "colnum_cantidad_per" || gvPersonal.FocusedColumn.Name == "colimp_salario" || gvPersonal.FocusedColumn.Name == "colimp_movilidad" || gvPersonal.FocusedColumn.Name == "colimp_bono_productividad")
            {
                eAnalisis.eAnalisis_Personal obj = gvPuestos.GetFocusedRow() as eAnalisis.eAnalisis_Personal;

                if (gvPersonal.FocusedColumn.Name == "colnum_cantidad_per" && obj.num_cantidad <= 0) { MessageBox.Show("Debe seleccionar una cantidad mayor a cero.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); obj.num_cantidad = 1; }
                if (lkpTiempo.EditValue.ToString() != "D" && obj.imp_salario < obj.imp_salario_min) { MessageBox.Show("Debe ingresar un monto mayor al mínimo (" + obj.imp_salario_min.ToString() + ").", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); obj.imp_salario = obj.imp_salario_min; }

                decimal imp_hora = Math.Round((obj.imp_salario + lstGenerales[1].num_valor + obj.imp_bono_productividad) / lstGenerales[8].num_valor, 4);
                obj.imp_horas_diu = Math.Round(obj.num_horas_diu * imp_hora, 2);
                obj.imp_horas_ext_diu = Math.Round(obj.num_horas_ext_diu > 2 ? (2 * Math.Round((imp_hora * Convert.ToDecimal(1.25)), 2)) + ((obj.num_horas_ext_diu - 2) * Math.Round((imp_hora * Convert.ToDecimal(1.35)), 2)) : obj.num_horas_ext_diu * (imp_hora * Convert.ToDecimal(1.25)), 2);

                decimal imp_hora_noc = Math.Round((((lstGenerales[2].num_valor * Convert.ToDecimal(1.35)) - obj.imp_salario) / lstGenerales[8].num_valor) + imp_hora, 4);
                obj.imp_horas_noc = Math.Round(obj.imp_horas_noc * imp_hora_noc, 2);
                obj.imp_horas_ext_noc = Math.Round(obj.num_horas_ext_noc > 2 ? (2 * Math.Round((imp_hora_noc * Convert.ToDecimal(1.25)), 2)) + ((obj.num_horas_ext_noc - 2) * Math.Round((imp_hora_noc * Convert.ToDecimal(1.35)), 2)) : obj.num_horas_ext_noc * (imp_hora_noc * Convert.ToDecimal(1.25)), 2);

                if (lkpTiempo.EditValue.ToString() == "D")
                {
                    obj.imp_salario = obj.imp_horas_diu + obj.imp_horas_noc;
                    obj.imp_salario_extra = obj.imp_horas_ext_diu + obj.imp_horas_ext_noc;

                    obj.imp_bono_nocturno = (obj.num_horas_noc * (imp_hora_noc - imp_hora)) * obj.num_dia_semana;
                    obj.imp_salario = obj.imp_salario * obj.num_dia_semana;
                    obj.imp_salario_extra = obj.imp_salario_extra * obj.num_dia_semana;
                }
                else
                {
                    obj.imp_salario_extra = obj.imp_horas_ext_diu + obj.imp_horas_ext_noc;
                    obj.imp_bono_nocturno = (Math.Round(obj.num_horas_noc * (imp_hora_noc - imp_hora), 2)) * lstGenerales[9].num_valor;
                    obj.imp_salario_extra = obj.imp_salario_extra * lstGenerales[9].num_valor;
                }

                obj.imp_feriado = obj.flg_feriado ? ((obj.imp_salario / 30) * lstGenerales[10].num_valor * 2) / 12 : 0;

                obj.imp_salario_total = obj.num_cantidad * (obj.imp_salario + obj.imp_bono_nocturno + obj.imp_movilidad + obj.imp_salario_extra + obj.imp_bono_productividad + obj.imp_feriado);
            }

            if (gvPersonal.FocusedColumn.Name == "colimp_salario" || gvPersonal.FocusedColumn.Name == "colnum_cantidad_per" || gvPersonal.FocusedColumn.Name == "colimp_movilidad" || gvPersonal.FocusedColumn.Name == "colimp_bono_productividad")
            {
                gPers = true;

                CargarOtros(false);
                CargarMontosPersonal();
                CargarTotales();
            }

            gvPersonal.RefreshData();
        }

        private void gvPersonal_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void btnPropuesta_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            blGlobal.Abrir_SplashScreenManager(typeof(FrmSplashCarga), "Generando word", "Cargando...");

            GenerarExcel(false);

            FormatoXmlHelper xml = new FormatoXmlHelper();

            string[] cabeceraPersonal = new string[] { "SEDE", "N° PERSONAL", "PUESTO", "HORAS DE TRABAJO", "RANGO DE HORARIO", "DIAS DE LABOR" };
            string[] cabeceraSedes = new string[] { "SEDES", "DIRECCIONES" };
            List<String[]> filasPersonal = new List<string[]>();
            List<String[]> filasSedes = new List<string[]>();
            List<string> filasAlcance = lstAlcance.Select(p => p.dsc_actividad).ToList();

            lstPerSedes = blAns.ListarAnalisis<eAnalisis.eAnalisis_Personal_Sedes>(12, empresa, sedeEmpresa, analisis, tipoServicio: lkpTipoServicio.GetColumnValue("cod_tipo_prestacion").ToString());
            //Obtengo sólo un registro por cada sede que se encuentre el el List
            var lstSedes = lstPerSedes.GroupBy(x => x.num_linea_sedes).Select(x => x.First()).ToList();

            foreach (eAnalisis.eAnalisis_Personal_Sedes obj in lstPerSedes)
            {
                filasPersonal.Add(new string[] { obj.dsc_sede_cliente, obj.num_cantidad.ToString(), obj.dsc_cargo, Math.Round(obj.num_horas, 0).ToString(), obj.dsc_rango_horario, obj.num_dia_semana.ToString() });
            }

            foreach (eAnalisis.eAnalisis_Sedes_Prestacion obj in lstSedes)
            {
                filasSedes.Add(new string[] { obj.dsc_sede_cliente, obj.dsc_cadena_direccion });
            }

            xml.ShowReport(lstFormatoCotizacion, cabeceraPersonal,cabeceraSedes,filasPersonal,filasSedes,filasAlcance);
            SplashScreenManager.CloseForm(false);

        }

        private void gvPersonal_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName == "imp_bono_productividad" || e.Column.FieldName == "imp_movilidad")
                {
                    e.Appearance.BackColor = Color.FromArgb(137, 235, 169);
                }
            }
        }

        private void gvPersonal_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvProducto_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (gvProducto.FocusedRowHandle >= 0 && (gvProducto.FocusedColumn.FieldName == "dsc_producto" || gvProducto.FocusedColumn.FieldName == "dsc_simbolo" || gvProducto.FocusedColumn.FieldName == "imp_total" || gvProducto.FocusedColumn.FieldName == "imp_venta")) e.Cancel = true;

            if (gvProducto.FocusedRowHandle < 0)
            {
                gProd = true;

                EnviarDatos("Producto");

                frmBusquedaItems frm = new frmBusquedaItems();
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.empresa = lkpEmpresa.EditValue.ToString();
                frm.accion = Buscar.Producto;
                frm.user = user;
                frm.lstDatos = lstDatos;
                frm.WindowState = FormWindowState.Maximized;
                frm.ShowDialog();

                lstDatos = frm.lstDatos;
                if (lstDatos != null && lstDatos.Count > 0)
                {
                    CargarDatos(frm.entidad);
                    CargarMontosProductos();
                    CargarTotales();
                }
            }
        }

        private void gvProducto_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gProd = true;

            if (gvProducto.FocusedColumn.Name == "colnum_cantidad" || gvProducto.FocusedColumn.Name == "colimp_unitario" || gvProducto.FocusedColumn.Name == "colprc_margen")
            {
                eAnalisis.eAnalisis_Producto obj = gvProducto.GetFocusedRow() as eAnalisis.eAnalisis_Producto;

                if (gvProducto.FocusedColumn.Name == "colnum_cantidad" && obj.num_cantidad <= 0) { MessageBox.Show("Debe seleccionar una cantidad mayor a cero.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); obj.num_cantidad = 1; }
                if (gvProducto.FocusedColumn.Name == "colimp_unitario" && obj.imp_unitario <= 0) { MessageBox.Show("Debe ingresar un monto mayor a cero.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); obj.imp_unitario = 1; }

                obj.imp_total = obj.num_cantidad * obj.imp_unitario;
                obj.imp_venta = obj.imp_total * (1 + obj.prc_margen / 100);
            }

            if (gvProducto.FocusedColumn.Name == "colimp_unitario" || gvProducto.FocusedColumn.Name == "colnum_cantidad" || gvProducto.FocusedColumn.Name == "colprc_margen" || gvProducto.FocusedColumn.Name == "colcod_dotacion")
            {
                CargarTiposProducto();
                CargarMontosProductos();
                CargarTotales();
            }
        }

        private void gvProducto_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvProducto_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            string cell = gvProducto.GetRowCellDisplayText(e.RowHandle, e.Column);

            if (cell == "0" || cell == "0.0000")
            {
                e.Appearance.ForeColor = Color.Red;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void gvProducto_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void rlkpDotacion_EditValueChanged(object sender, EventArgs e)
        {
            gvProducto.PostEditor();
        }

        private void rbtnEliminarProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            gProd = true;

            eAnalisis.eAnalisis_Producto ePro = gvProducto.GetFocusedRow() as eAnalisis.eAnalisis_Producto;

            lstProdAnsElim.Add(ePro);
            lstProdAns.Remove(ePro);

            bsProductoAnalisis.DataSource = lstProdAns;

            CargarTiposProducto();
            CargarMontosProductos();
            CargarTotales();
        }

        private void gvResumen_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gProd = true;

            eDatos obj2 = gvResumen.GetFocusedRow() as eDatos;

            foreach (eAnalisis.eAnalisis_Producto obj in lstProdAns)
            {
                if (obj.dsc_tipo_servicio == obj2.AtributoDos)
                {
                    obj.prc_margen = obj2.AtributoOnce;
                    obj.cod_dotacion = obj2.cod_dotacion;
                    obj.imp_venta = obj.imp_total * (1 + obj.prc_margen / 100);
                }
            }

            CargarTiposProducto();
            CargarMontosProductos();
            CargarTotales();

            gvProducto.RefreshData();
            gvResumen.RefreshData();
        }

        private void gvResumen_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvResumen_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void gvMaqEquipo_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (gvMaqEquipo.FocusedRowHandle >= 0 && (gvMaqEquipo.FocusedColumn.FieldName == "dsc_activo_fijo" || gvMaqEquipo.FocusedColumn.FieldName == "imp_total" || gvMaqEquipo.FocusedColumn.FieldName == "imp_mensual")) e.Cancel = true;

            if (gvMaqEquipo.FocusedRowHandle < 0)
            {
                gMqEq = true;

                EnviarDatos("Maquinas");

                frmBusquedaItems frm = new frmBusquedaItems();
                frm.colorVerde = colorVerde;
                frm.colorPlomo = colorPlomo;
                frm.colorEventRow = colorEventRow;
                frm.colorFocus = colorFocus;
                frm.empresa = lkpEmpresa.EditValue.ToString();
                frm.accion = Buscar.Maquinas;
                frm.user = user;
                frm.lstDatos = lstDatos;
                frm.ShowDialog();

                lstDatos = frm.lstDatos;
                if (lstDatos != null && lstDatos.Count > 0)
                {
                    CargarDatos(frm.entidad);
                    CargarMontosMaquinas();
                    CargarTotales();
                }
            }
        }

        private void gvMaqEquipo_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (gvMaqEquipo.FocusedColumn.Name == "colnum_cantidad_maq" || gvMaqEquipo.FocusedColumn.Name == "colimp_unitario_maq" || gvMaqEquipo.FocusedColumn.Name == "colnum_meses_dep" || gvMaqEquipo.FocusedColumn.Name == "colprc_margen_maq" || gvMaqEquipo.FocusedColumn.Name == "colimp_venta_maq")
            {
                eAnalisis.eAnalisis_Maquinaria obj = gvMaqEquipo.GetFocusedRow() as eAnalisis.eAnalisis_Maquinaria;

                if (gvMaqEquipo.FocusedColumn.Name == "colnum_cantidad_maq" && obj.num_cantidad <= 0) { MessageBox.Show("Debe seleccionar una cantidad mayor a cero.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); obj.num_cantidad = 1; }
                if (gvMaqEquipo.FocusedColumn.Name == "colimp_unitario_maq" && obj.imp_unitario <= 0) { MessageBox.Show("Debe ingresar un monto mayor a cero.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); obj.imp_unitario = 1; }

                obj.imp_total = obj.num_cantidad * obj.imp_unitario;
                obj.imp_mensual = obj.imp_total / obj.num_meses_dep;
                obj.imp_venta = obj.imp_mensual * (1 + obj.prc_margen / 100);
            }

            if (gvMaqEquipo.FocusedColumn.Name == "colnum_cantidad_maq" || gvMaqEquipo.FocusedColumn.Name == "colimp_unitario_maq" || gvMaqEquipo.FocusedColumn.Name == "colnum_meses_dep" || gvMaqEquipo.FocusedColumn.Name == "colprc_margen_maq" || gvMaqEquipo.FocusedColumn.Name == "colimp_venta_maq")
            {
                gMqEq = true;

                CargarMontosMaquinas();
                CargarTotales();
            }
        }

        private void gvMaqEquipo_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvMaqEquipo_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            string cell = gvMaqEquipo.GetRowCellDisplayText(e.RowHandle, e.Column);

            if (cell == "0" || cell == "0.0000")
            {
                e.Appearance.ForeColor = Color.Red;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void gvMaqEquipo_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void rbtnEliminarMaquinaria_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            gMqEq = true;

            eAnalisis.eAnalisis_Maquinaria eMaq = gvMaqEquipo.GetFocusedRow() as eAnalisis.eAnalisis_Maquinaria;

            lstMaqAnsElim.Add(eMaq);
            bsMaqEquiposAnalisis.Remove(eMaq);
            lstMaqAns.Remove(eMaq);

            CargarMontosMaquinas();
            CargarTotales();
        }

        private void gvOtros_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (gvOtros.FocusedRowHandle >= 0 && (gvOtros.FocusedColumn.FieldName == "num_cantidad" || gvOtros.FocusedColumn.FieldName == "dsc_descripcion"))
            {
                eAnalisis.eAnalisis_Otros obj = gvOtros.GetFocusedRow() as eAnalisis.eAnalisis_Otros;

                switch (obj.dsc_descripcion)
                {
                    case "SEGURO COMPLEMENTARIO (SCTR)": e.Cancel = true; break;
                    case "SCTR SOCAVON": e.Cancel = true; break;
                    case "SCTR ALTURA": e.Cancel = true; break;
                    case "SEGURO VIDA LEY": e.Cancel = true; break;
                    case "EXÁMENES MÉDICOS OCUPACIONALES (LEY 29783)": e.Cancel = true; break;
                    case "POLIZAS RC/DH": e.Cancel = true; break;
                    case "OTROS CONCEPTOS (SIG)": e.Cancel = true; break;
                    case "GASTOS FINANCIEROS": e.Cancel = true; break;
                    case "COSTO SUPERVISIÓN": e.Cancel = true; break;
                    case "COMISIÓN COMERCIAL": e.Cancel = true; break;
                    case "GASTOS ADMINISTRATIVOS": e.Cancel = true; break;
                    case "GASTOS OPERATIVOS": e.Cancel = true; break;
                    case "UTILIDADES": e.Cancel = true; break;
                }
            }

            if (gvOtros.FocusedRowHandle >= 0 && (gvOtros.FocusedColumn.FieldName == "prc_ley"))
            {
                eAnalisis.eAnalisis_Otros obj = gvOtros.GetFocusedRow() as eAnalisis.eAnalisis_Otros;

                if (obj.dsc_descripcion == "SEGURO VIDA LEY" || obj.dsc_descripcion == "EXÁMENES MÉDICOS OCUPACIONALES (LEY 29783)" || obj.dsc_descripcion == "POLIZAS RC/DH" || obj.dsc_descripcion == "OTROS CONCEPTOS (SIG)")
                {
                    e.Cancel = true;
                }
            }

            if (gvOtros.FocusedRowHandle >= 0 && (gvOtros.FocusedColumn.FieldName == "imp_unitario"))
            {
                eAnalisis.eAnalisis_Otros obj = gvOtros.GetFocusedRow() as eAnalisis.eAnalisis_Otros;

                switch (obj.dsc_descripcion)
                {
                    case "SEGURO COMPLEMENTARIO (SCTR)": e.Cancel = true; break;
                    case "SCTR SOCAVON": e.Cancel = true; break;
                    case "SCTR ALTURA": e.Cancel = true; break;
                    case "GASTOS FINANCIEROS": e.Cancel = true; break;
                    case "COSTO SUPERVISIÓN": e.Cancel = true; break;
                    case "COMISIÓN COMERCIAL": e.Cancel = true; break;
                    case "GASTOS ADMINISTRATIVOS": e.Cancel = true; break;
                    case "GASTOS OPERATIVOS": e.Cancel = true; break;
                    case "UTILIDADES": e.Cancel = true; break;
                }
            }
        }

        private void gvOtros_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gOtrs = true;

            if (e.RowHandle >= 0 && (gvOtros.FocusedColumn.Name == "colnum_cantidad_otros" || gvOtros.FocusedColumn.Name == "colprc_ley" || gvOtros.FocusedColumn.Name == "colimp_unitario_otros" || gvOtros.FocusedColumn.Name == "colprc_margen_otros"))
            {
                eAnalisis.eAnalisis_Otros obj = gvOtros.GetFocusedRow() as eAnalisis.eAnalisis_Otros;

                if (gvOtros.FocusedColumn.Name == "colnum_cantidad_otros" && obj.num_cantidad <= 0 && !(obj.dsc_descripcion == "SEGURO COMPLEMENTARIO (SCTR)" || obj.dsc_descripcion == "SCTR SOCAVON" || obj.dsc_descripcion == "SCTR ALTURA" || obj.dsc_descripcion == "SEGURO VIDA LEY" || obj.dsc_descripcion == "EXÁMENES MÉDICOS OCUPACIONALES (LEY 29783)" || obj.dsc_descripcion == "POLIZAS RC/DH" || obj.dsc_descripcion == "OTROS CONCEPTOS (SIG)")) { MessageBox.Show("Debe seleccionar una cantidad mayor a cero.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); obj.num_cantidad = 1; }
                if (gvOtros.FocusedColumn.Name == "colimp_unitario_otros" && obj.imp_unitario <= 0 && !(obj.dsc_descripcion == "SEGURO COMPLEMENTARIO (SCTR)" || obj.dsc_descripcion == "SCTR SOCAVON" || obj.dsc_descripcion == "SCTR ALTURA")) { MessageBox.Show("Debe ingresar un monto mayor a cero.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); obj.imp_unitario = 1; }

                decimal sueldo = (lstPerAns.Sum(x => x.imp_salario_total) + lstPerAns.Sum(x => x.imp_salario * 10 / 100));

                switch (obj.dsc_descripcion)
                {
                    case "SEGURO COMPLEMENTARIO (SCTR)": obj.imp_total = (sueldo * obj.prc_ley) / 100; obj.imp_venta = obj.imp_total + ((obj.imp_total * obj.prc_margen) / 100); break;
                    case "SCTR SOCAVON": obj.imp_total = (sueldo * obj.prc_ley) / 100; obj.imp_venta = obj.imp_total + ((obj.imp_total * obj.prc_margen) / 100); break;
                    case "SCTR ALTURA": obj.imp_total = (sueldo * obj.prc_ley) / 100; obj.imp_venta = obj.imp_total + ((obj.imp_total * obj.prc_margen) / 100); break;
                    case "GASTOS FINANCIEROS": obj.imp_total = (obj.imp_unitario * obj.prc_ley) / 100; obj.imp_venta = (obj.imp_total * obj.prc_margen) / 100; break;
                    case "COSTO SUPERVISIÓN": obj.imp_total = (obj.imp_unitario * obj.prc_ley) / 100; obj.imp_venta = (obj.imp_total * obj.prc_margen) / 100; break;
                    case "COMISIÓN COMERCIAL": obj.imp_total = (obj.imp_unitario * obj.prc_ley) / 100; obj.imp_venta = (obj.imp_total * obj.prc_margen) / 100; break;
                    case "GASTOS ADMINISTRATIVOS": obj.imp_total = (obj.imp_unitario * obj.prc_ley) / 100; obj.imp_venta = (obj.imp_total * obj.prc_margen) / 100; break;
                    case "GASTOS OPERATIVOS": obj.imp_total = (obj.imp_unitario * obj.prc_ley) / 100; obj.imp_venta = (obj.imp_total * obj.prc_margen) / 100; break;
                    case "UTILIDADES":
                        obj.imp_unitario = lstOtrAns.FindAll(x => x.dsc_descripcion == "GASTOS FINANCIEROS").Sum(x => x.imp_unitario) + lstOtrAns.FindAll(x => x.dsc_descripcion == "COSTO SUPERVISIÓN").Sum(x => x.imp_total) + lstOtrAns.FindAll(x => x.dsc_descripcion == "COMISIÓN COMERCIAL").Sum(x => x.imp_total) + lstOtrAns.FindAll(x => x.dsc_descripcion == "GASTOS ADMINISTRATIVOS").Sum(x => x.imp_total) + lstOtrAns.FindAll(x => x.dsc_descripcion == "GASTOS OPERATIVOS").Sum(x => x.imp_total);
                        obj.imp_total = (obj.imp_unitario * obj.prc_ley) / 100; obj.imp_venta = (obj.imp_unitario * obj.prc_margen) / 100; break;
                    default: obj.imp_total = obj.imp_unitario * obj.num_cantidad; obj.imp_venta = obj.imp_total + ((obj.imp_total * obj.prc_margen) / 100); break;
                }

                CargarTotales();
                CargarMontosPersonal();
            }

            if (e.RowHandle >= 0 && (gvOtros.FocusedColumn.Name == "colimp_venta_otros"))
            {
                eAnalisis.eAnalisis_Otros obj = gvOtros.GetFocusedRow() as eAnalisis.eAnalisis_Otros;

                obj.prc_margen = ((obj.imp_venta - obj.imp_total) * 100) / obj.imp_total;

                CargarTotales();
                CargarMontosPersonal();
            }
        }

        private void gvOtros_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvOtros_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    string descripcion = gvOtros.GetRowCellValue(e.RowHandle, "dsc_descripcion").ToString();

                    if (e.Column.FieldName == "imp_unitario" && (descripcion == "GASTOS FINANCIEROS" || descripcion == "COSTO SUPERVISIÓN" || descripcion == "COMISIÓN COMERCIAL" || descripcion == "GASTOS ADMINISTRATIVOS" || descripcion == "GASTOS OPERATIVOS" || descripcion == "UTILIDADES")) e.DisplayText = "S/ 0.00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvOtros_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void rbtnEliminarOtros_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            gOtrs = true;

            eAnalisis.eAnalisis_Otros eOtrs = gvOtros.GetFocusedRow() as eAnalisis.eAnalisis_Otros;

            lstOtrosElim.Add(eOtrs);
            bsOtrosAnalisis.Remove(eOtrs);
            lstOtrAns.Remove(eOtrs);

            CargarTotales();
            CargarMontosPersonal();
        }

        private void gvEstructuraCostos_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string concepto = gvEstructuraCostos.GetRowCellValue(gvEstructuraCostos.FocusedRowHandle, "cod_concepto").ToString();

            if (concepto == "00001" || concepto == "00003")
            {
                e.Cancel = true;
            }

            if (concepto == "00002")
            {
                string item = gvEstructuraCostos.GetRowCellValue(gvEstructuraCostos.FocusedRowHandle, "dsc_item").ToString();

                if (item == "ESSALUD" || item == "SUBTOTAL")
                {
                    e.Cancel = true;
                }
            }

            if (concepto == "00004")
            {
                string item = gvEstructuraCostos.GetRowCellValue(gvEstructuraCostos.FocusedRowHandle, "dsc_item").ToString();

                if (item == "MOVILIDAD" || item == "DESCANSEROS" || item == "SUBTOTAL" || item == "OPERARIOS" || item == "TOTAL MANO DE OBRA")
                {
                    e.Cancel = true;
                }
            }

            if (concepto == "00005")
            {
                string item = gvEstructuraCostos.GetRowCellValue(gvEstructuraCostos.FocusedRowHandle, "dsc_item").ToString();

                if (item == "SUBTOTAL")
                {
                    e.Cancel = true;
                }
            }

            if (concepto == "00006")
            {
                string item = gvEstructuraCostos.GetRowCellValue(gvEstructuraCostos.FocusedRowHandle, "dsc_item").ToString();

                if (item == "PRIMER SUBTOTAL" || item == "SEGUNDO SUBTOTAL" || item == "IGV" || item == "TOTAL")
                {
                    e.Cancel = true;
                }
            }
        }

        private void gvEstructuraCostos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gvEstructuraCostos.PostEditor();

            string item = gvEstructuraCostos.GetRowCellValue(e.RowHandle, "cod_item").ToString();
            string descripcion = gvEstructuraCostos.GetRowCellValue(e.RowHandle, "dsc_item").ToString();
            decimal prc_margen = Convert.ToDecimal(gvEstructuraCostos.GetRowCellValue(e.RowHandle, "prc_margen"));
            decimal imp_total = Convert.ToDecimal(gvEstructuraCostos.GetRowCellValue(e.RowHandle, "imp_total"));

            if (e.RowHandle >= 0 && (gvEstructuraCostos.FocusedColumn.Name == "colprc_margen_cst"))
            {
                if (item.Contains("LS") || item.Contains("GP") || item.Contains("CO"))
                {
                    foreach (eAnalisis.eAnalisis_Otros obj in lstOtrAns)
                    {
                        if (obj.dsc_descripcion == descripcion)
                        {
                            obj.prc_margen = prc_margen;
                            obj.imp_venta = obj.imp_total + (obj.imp_total * obj.prc_margen) / 100;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (eAnalisis.eAnalisis_Est_Cst obj in lstTot)
                    {
                        if (obj.cod_item == item)
                        {
                            obj.prc_margen = prc_margen;
                            obj.imp_total = (obj.imp_unitario * obj.prc_margen) / 100;
                            break;
                        }
                    }
                }
            }

            if (e.RowHandle >= 0 && (gvEstructuraCostos.FocusedColumn.Name == "colimp_total_cst"))
            {
                if (item.Contains("LS") || item.Contains("GP") || item.Contains("CO"))
                {
                    foreach (eAnalisis.eAnalisis_Otros obj in lstOtrAns)
                    {
                        if (obj.dsc_descripcion == descripcion)
                        {
                            obj.imp_venta = imp_total;
                            obj.prc_margen = (obj.imp_venta * 100) / obj.imp_total;
                            if (obj.prc_margen == 100) obj.prc_margen = Convert.ToDecimal(0.001);
                            break;
                        }
                    }
                }
                else
                {
                    foreach (eAnalisis.eAnalisis_Est_Cst obj in lstTot)
                    {
                        if (obj.cod_item == item)
                        {
                            obj.imp_total = imp_total;
                            obj.prc_margen = (obj.imp_total * 100) / obj.imp_unitario;
                            if (obj.prc_margen == 100) obj.prc_margen = Convert.ToDecimal(0.001);
                            break;
                        }
                    }
                }
            }

            if (item.Contains("LS")) { socialesG = 0; GenerarLeyesSociales(""); }
            if (item.Contains("GP")) { gastosG = 0; GenerarGastosPersonal(""); }
            if (item.Contains("CO")) { productosG = 0; GenerarCostosOperativos(""); }
            if (item.Contains("TG")) { GenerarTotalGeneral(""); }
        }

        private void gvEstructuraCostos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvEstructuraCostos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    string mrg = gvEstructuraCostos.GetRowCellValue(e.RowHandle, "prc_margen").ToString();
                    string ley = gvEstructuraCostos.GetRowCellValue(e.RowHandle, "prc_ley").ToString();
                    string descripcion = gvEstructuraCostos.GetRowCellValue(e.RowHandle, "dsc_item").ToString();

                    if (e.Column.FieldName == "prc_margen" && (mrg == "0" || mrg == "0.00" || mrg == "0.0")) e.DisplayText = "";
                    if (e.Column.FieldName == "prc_ley" && (ley == "0" || ley == "0.00" || ley == "0.0")) e.DisplayText = "";
                    if (e.Column.FieldName == "imp_total" && descripcion == "OPERARIOS") e.DisplayText = e.DisplayText.Replace("S/ ", "").Replace(".00", "");
                    if (e.Column.FieldName.Contains("Operario") && descripcion != "OPERARIOS") e.DisplayText = e.CellValue.ToString() != "" ? Convert.ToDecimal(e.CellValue) == 0 ? "S/. 0.00" : "S/ " + $"{Convert.ToDecimal(e.CellValue).ToString("0,0.00", CultureInfo.InvariantCulture): 0.00}" : "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvEstructuraCostos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                blGlobal.Pintar_EstiloGrilla(sender, e);
                GridView view = sender as GridView;
                if (view.Columns[1] != null)
                {
                    string descripcion = view.GetRowCellDisplayText(e.RowHandle, view.Columns[1]);

                    if (descripcion == "UTILIDADES" || descripcion == "TOTAL DEL CONTRATO" || descripcion == "IGV" || descripcion == "TOTAL")
                    {
                        e.Appearance.ForeColor = Color.Blue; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }

                    if (descripcion == "TOTAL COSTO DIRECTO" || descripcion == "GASTOS FINANCIEROS" ||
                        descripcion == "COSTO SUPERVISIÓN" || descripcion == "COMISIÓN COMERCIAL" ||
                        descripcion == "GASTOS ADMINISTRATIVOS" || descripcion == "GASTOS OPERATIVOS" ||
                        descripcion == "SUBTOTAL REMUNERACION" || descripcion == "SUBTOTAL LEYES SOCIALES" ||
                        descripcion == "SUBTOTAL BENEFICIOS SOCIALES" || descripcion == "SUBTOTAL GASTOS DE PERSONAL" ||
                        descripcion == "SUBTOTAL COSTOS OPERATIVOS" || descripcion == "TOTAL MANO DE OBRA")
                    {
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                }
            }
        }
        #endregion
    }
}