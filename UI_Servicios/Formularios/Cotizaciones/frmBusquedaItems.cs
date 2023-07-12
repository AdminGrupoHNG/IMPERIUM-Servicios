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
using DevExpress.XtraTreeList;

namespace UI_Servicios.Formularios.Cotizaciones
{
    internal enum Buscar
    {
        Producto = 0,
        Personal = 1,
        Tipos = 2,
        Uniformes = 3,
        Maquinas = 4
    }

    public partial class frmBusquedaItems : DevExpress.XtraEditors.XtraForm
    {
        internal Buscar accion = Buscar.Producto;

        public eUsuario user = new eUsuario();
        blAnalisisServicio blAns = new blAnalisisServicio();
        blGlobales blGlobal = new blGlobales();
        public int[] colorVerde, colorPlomo, colorEventRow, colorFocus;

        public string empresa, sede, tipos, entidad, cargo;
        public int item;
        public List<eDatos> lstDatos;
        public List<eDatos> Productos;
        public List<eDatos> Personal;
        public List<eDatos> Maquinas;
        public List<eAnalisis.eAnalisis_Personal> lstPers;
        public List<eAnalisis.eAnalisis_Personal_Uniformes> lstUnifEpp;
        public List<eAnalisis.eAnalisis_Personal_Uniformes> lstUnifElim = new List<eAnalisis.eAnalisis_Personal_Uniformes>();


        eDatos eCar = new eDatos();

        List<eAnalisis.eAnalisis_Personal_Uniformes> lstUnifTemp = new List<eAnalisis.eAnalisis_Personal_Uniformes>();
        List<eAnalisis.eAnalisis_Personal> lstCargos = new List<eAnalisis.eAnalisis_Personal>();

        public frmBusquedaItems()
        {
            InitializeComponent();
        }

        private void frmBusquedaProducto_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            CargarLookUpEdit();
            ConfigurarForm();
            CargarGrilla();
        }

        private void CargarLookUpEdit()
        {
            rlkpDotacion.DataSource = blAns.ListarGeneral<eDatos>("Dotaciones");
        }

        private void ConfigurarForm()
        {
            switch (accion)
            {
                case Buscar.Producto:
                    InitTreeList();

                    controlCargoUniforme.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlReplicar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlNuevo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlbtnAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlUniformes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
                case Buscar.Personal:
                    this.Text = "Busqueda Cargos";

                    this.gvDatos.GroupCount = 0;
                    colAtributoDos.Visible = false;

                    espacioDos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlTipo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlCargoUniforme.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlReplicar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlbtnAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlUniformes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
                case Buscar.Tipos:
                    this.Text = "Tipos de Producto";

                    this.StartPosition = FormStartPosition.CenterParent;
                    this.MinimumSize = new Size(500, 400);
                    this.Size = new Size(500, 400);

                    espacioDos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    espacioUno.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlReplicar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlNuevo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlEnviar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlTipo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlCargoUniforme.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlbtnAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlUniformes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
                case Buscar.Uniformes:
                    CargarComboBox();

                    espacioDos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlTipo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlNuevo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlbtnAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlProductos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
                case Buscar.Maquinas:
                    this.Text = "Busqueda Máquinas, Equipos y Accesorios";

                    espacioDos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlTipo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlCargoUniforme.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlReplicar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlNuevo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlbtnAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlUniformes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
            }
        }

        private void CargarGrilla()
        {
            switch (accion)
            {
                case Buscar.Producto:
                    colAtributoCuatro.Visible = false;
                    break;
                case Buscar.Personal:
                    Personal = blAns.ListarGeneral<eDatos>("Personal", empresa, sede);
                    CargarDatos(Personal, "Personal");

                    colAtributoCuatro.Caption = "Cargo";

                    colAtributoSeis.Visible = false;
                    colAtributoOcho.Visible = false;
                    colAtributoOnce.Visible = true;
                    colAtributoDoce.Visible = true;
                    colAtributoTrece.VisibleIndex = 1;
                    colAtributoCuatro.VisibleIndex = 2;
                    colAtributoOnce.VisibleIndex = 3;
                    colAtributoDoce.VisibleIndex = 4;
                    break;
                case Buscar.Tipos:
                    CargarDatos(lstDatos, "Tipos");

                    colrckbSeleccionar.Visible = false;
                    colAtributoTrece.Visible = false;
                    colAtributoCuatro.Visible = false;
                    colAtributoSeis.Visible = false;
                    colAtributoOcho.Visible = false;

                    colAtributoOnce.Caption = "Margen";

                    colAtributoDos.GroupIndex = -1;
                    colAtributoOnce.Visible = true;
                    colAtributoDos.VisibleIndex = 0;
                    colAtributoOnce.OptionsColumn.AllowEdit = true;
                    colAtributoOnce.ColumnEdit = rtxtMargen;
                    colcod_dotacion.Visible = true;
                    break;
                case Buscar.Uniformes:
                    CargarDatos(lstDatos, "Uniformes");

                    colrckbSeleccionar.Visible = true;
                    colAtributoTrece.Visible = false;
                    colAtributoCuatro.Visible = false;
                    coldsc_subtipo_servicio.Visible = false;
                    colrckbSeleccionar.VisibleIndex = 0;
                    break;
                case Buscar.Maquinas:
                    Maquinas = blAns.ListarGeneral<eDatos>("Maquinas", empresa);
                    CargarDatos(Maquinas, "Maquinas");

                    colAtributoCuatro.Caption = "Maquinaria - Accesorio - Equipo";

                    colAtributoSeis.Visible = false;
                    colAtributoOcho.Visible = false;
                    colAtributoTrece.VisibleIndex = 1;
                    colAtributoCuatro.VisibleIndex = 2;
                    break;
            }
        }

        void InitTreeList()
        {
            tlstTipo.Appearance.Row.BackColor = Color.Transparent;
            tlstTipo.Appearance.Empty.BackColor = Color.Transparent;
            tlstTipo.BackColor = Color.Transparent;
            tlstTipo.CheckBoxFieldName = "Checked";
            tlstTipo.TreeViewFieldName = "Name";
            tlstTipo.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            tlstTipo.OptionsBehavior.Editable = false;
            tlstTipo.OptionsBehavior.ReadOnly = true;
            tlstTipo.OptionsBehavior.AllowRecursiveNodeChecking = true;
            tlstTipo.NodeCellStyle += OnNodeCellStyle;
            tlstTipo.BeforeFocusNode += OnBeforeFocusNode;
            var dataSource = GenerateDataSource();
            tlstTipo.DataSource = dataSource;
            tlstTipo.ForceInitialize();
            tlstTipo.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
            tlstTipo.Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
            tlstTipo.ExpandAll();
        }

        void OnNodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.FontSizeDelta += 1;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
            if (e.Node.Level == 1 && e.Node.Nodes.Count > 0)
                e.Appearance.FontStyleDelta = FontStyle.Bold;
        }

        BindingList<Option> GenerateDataSource()
        {
            BindingList<Option> _options = new BindingList<Option>();

            List<eProyecto.eProyecto_Tipo_Servicio> tipos = blAns.ListarGeneral<eProyecto.eProyecto_Tipo_Servicio>("Tipo", empresa, opcion: accion == Buscar.Uniformes ? 17 : 0);

            _options.Add(new Option() { ParentID = "0", ID = "1", Name = "TIPO", Checked = false });
            foreach (eProyecto.eProyecto_Tipo_Servicio obj in tipos)
            {
                _options.Add(new Option() { ParentID = "1", ID = obj.cod_tipo_servicio, Name = obj.dsc_tipo_servicio, Checked = accion == Buscar.Uniformes ? true : false });
            }

            return _options;
        }

        class Option : INotifyPropertyChanged
        {
            public string ParentID { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            bool? checkedCore = false;

            public event PropertyChangedEventHandler PropertyChanged;

            public bool? Checked
            {
                get { return checkedCore; }
                set
                {
                    if (checkedCore == value)
                        return;
                    checkedCore = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Checked"));
                }
            }
        }

        void OnBeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            e.CanFocus = false;
        }

        private void CargarComboBox()
        {
            lstCargos = new List<eAnalisis.eAnalisis_Personal>();

            foreach (eAnalisis.eAnalisis_Personal obj in lstPers)
            {
                eAnalisis.eAnalisis_Personal eCrg = new eAnalisis.eAnalisis_Personal();
                eAnalisis.eAnalisis_Personal eCrg2 = new eAnalisis.eAnalisis_Personal();

                eCrg.cod_cargo = obj.cod_cargo;
                eCrg.dsc_cargo = obj.dsc_cargo;
                eCrg.num_item = obj.num_item;
                eCrg.dsc_rango_horario = obj.dsc_rango_horario;
                eCrg2 = lstCargos.Find(x => x.cod_cargo == obj.cod_cargo && x.num_item == obj.num_item);
                if (eCrg2 == null) lstCargos.Add(eCrg);
            }

            lkpCargoUniforme.Properties.DataSource = lstCargos;
            lkpCargoUniforme.Properties.ValueMember = "num_item";
            lkpCargoUniforme.Properties.DisplayMember = "dsc_cargo";

            lkpCargoUniforme.EditValue = item;
        }

        private void CargarDatos(List<eDatos> datos, string tipo)
        {
            if (tipo == "Uniformes")
            {
                foreach (eAnalisis.eAnalisis_Personal_Uniformes obj in lstUnifTemp)
                {
                    datos.RemoveAll(x => x.AtributoUno == obj.cod_tipo_servicio && x.AtributoTres == obj.cod_subtipo_servicio && x.AtributoCinco == obj.cod_producto);
                }

                bsUniformes.DataSource = lstUnifTemp;
            }
            else
            {
                foreach (eDatos obj in lstDatos)
                {
                    switch (tipo)
                    {
                        case "Producto":
                            datos.RemoveAll(x => x.AtributoUno == obj.AtributoUno && x.AtributoTres == obj.AtributoTres && x.AtributoCinco == obj.AtributoCinco);
                            break;
                        case "Personal":
                            datos.RemoveAll(x => x.AtributoTres == obj.AtributoTres);
                            break;
                        case "Maquinas":
                            datos.RemoveAll(x => x.AtributoTres == obj.AtributoTres);
                            break;
                    }
                }
            }

            bsDatos.DataSource = datos;
        }

        private void frmBusquedaItems_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (accion == Buscar.Uniformes)
            {
                lstUnifEpp.AddRange(lstUnifTemp);
                lstUnifEpp = lstUnifEpp.OrderBy(x => x.cod_cargo).ThenBy(x => x.num_item).ToList();
            }
        }

        private void tlstTipo_NodeChanged(object sender, NodeChangedEventArgs e)
        {
            if (e.Node.GetValue("Checked") == null) return;

            if ((Boolean)e.Node.GetValue("Checked") == true)
            {
                if (e.Node.GetValue("ParentID").ToString() != "0")
                {
                    tipos = e.Node.GetValue("ID").ToString() + "," + tipos;
                }
            }
            else
            {
                if (e.Node.GetValue("ParentID").ToString() != "0")
                {
                    if (tipos != null)
                    {
                        tipos = tipos.Replace(e.Node.GetValue("ID").ToString() + ",", "");
                    }
                }
            }

            btnBuscar_Click(sender, e);
        }

        private void lkpCargoUniforme_EditValueChanged(object sender, EventArgs e)
        {
            lstUnifEpp.AddRange(lstUnifTemp);
            cargo = lkpCargoUniforme.GetColumnValue("cod_cargo").ToString();
            item =  Convert.ToInt32(lkpCargoUniforme.EditValue);
            CargarUniformexPuesto();
            InitTreeList();
            btnBuscar_Click(sender, e);
        }

        private void CargarUniformexPuesto()
        {
            lstUnifTemp = lstUnifEpp.FindAll(x => x.cod_cargo == cargo && x.num_item == item);
            lstUnifEpp.RemoveAll(x => x.cod_cargo == cargo && x.num_item == item);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            switch (accion)
            {
                case Buscar.Producto:
                    Productos = blAns.ListarGeneral<eDatos>("ProductoxTipo", empresa, tipo: tipos);
                    CargarDatos(Productos, "Producto");
                    break;
                case Buscar.Personal:
                    Personal = blAns.ListarGeneral<eDatos>("Personal", empresa, sede);
                    CargarDatos(Personal, "Personal");
                    break;
                case Buscar.Uniformes:
                    lstDatos = blAns.ListarGeneral<eDatos>("ProductoxTipo", empresa, tipo: tipos);
                    CargarDatos(lstDatos, "Uniformes");
                    break;
            }
        }

        private void btnReplicar_Click(object sender, EventArgs e)
        {
            frmSeleccionPuestos frm = new frmSeleccionPuestos();

            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.user = user;
            frm.lstCargos = lstCargos;
            frm.lstUnifEpp = lstUnifEpp;
            frm.lstUnifTemp = lstUnifTemp;
            frm.cargo = cargo;
            frm.item = item;
            frm.ShowDialog();
        }

        private void btnAgregarOcultar_Click(object sender, EventArgs e)
        {
            if (btnAgregarOcultar.Text == "Agregar")
            {
                btnAgregarOcultar.Text = "Ocultar";
                controlbtnAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                controlProductos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                colimp_unitario.Visible = false;
                colimp_total.Visible = false;
                colprc_margen.Visible = false;
                colimp_venta.Visible = false;
                colrckbSeleccionar_Unif.Visible = true;
                colrckbSeleccionar_Unif.VisibleIndex = 0;
            }
            else
            {
                btnAgregarOcultar.Text = "Agregar";
                controlbtnAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                controlProductos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                colimp_unitario.Visible = true;
                colimp_total.Visible = true;
                colprc_margen.Visible = true;
                colimp_venta.Visible = true;
                colrckbSeleccionar_Unif.Visible = false;
                colnum_cantidad.VisibleIndex = 0;
                coldsc_producto.VisibleIndex = 1;
                coldsc_simbolo.VisibleIndex = 2;
                colimp_unitario.VisibleIndex = 3;
                colimp_total.VisibleIndex = 4;
                colprc_margen.VisibleIndex = 5;
                colimp_venta.VisibleIndex = 6;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmMantCargos frm = new frmMantCargos();
            frm.colorVerde = colorVerde;
            frm.colorPlomo = colorPlomo;
            frm.colorEventRow = colorEventRow;
            frm.colorFocus = colorFocus;
            frm.user = user;
            frm.empresa = empresa;
            frm.ShowDialog();

            if (frm.eCar != null)
            {
                eCar = new eDatos();

                eCar.AtributoTres = frm.eCar.AtributoCuatro;
                eCar.AtributoCuatro = frm.eCar.AtributoCinco;
                eCar.AtributoSiete = "00:00";
                eCar.AtributoOcho = "00:00";
                eCar.AtributoDiez = 0;
                eCar.AtributoOnce = frm.eCar.AtributoOnce;
                eCar.AtributoDoce = frm.eCar.AtributoDoce;

                bsDatos.Add(eCar);
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            switch (accion)
            {
                case Buscar.Producto:
                    entidad = "Producto";
                    break;
                case Buscar.Personal:
                    entidad = "Personal";
                    break;
                case Buscar.Tipos:
                    entidad = "Tipos";
                    break;
                case Buscar.Uniformes:
                    entidad = "Uniformes";
                    break;
                case Buscar.Maquinas:
                    entidad = "Maquinas";
                    break;
            }

            if (accion != Buscar.Tipos)
            {
                lstDatos = new List<eDatos>();
                switch (accion)
                {
                    case Buscar.Producto: lstDatos = Productos.FindAll(x => x.cod_sel == true || x.AtributoTrece > 0); break;
                    case Buscar.Personal: lstDatos = Personal.FindAll(x => x.cod_sel == true || x.AtributoTrece > 0); break;
                    case Buscar.Maquinas: lstDatos = Maquinas.FindAll(x => x.cod_sel == true || x.AtributoTrece > 0); break;
                }
            }

            this.Close();
        }

        private void gvDatos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && accion != Buscar.Uniformes)
            {
                gvDatos.PostEditor(); gvDatos.RefreshData();
                eDatos obj = gvDatos.GetFocusedRow() as eDatos;
                if (obj.AtributoTrece > 0) obj.cod_sel = true;

                btnEnviar_Click(sender, e);
            }
        }

        private void gvDatos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (gvDatos.FocusedColumn.Name == "colAtributoTrece")
            {
                eDatos obj = gvDatos.GetFocusedRow() as eDatos;

                if (obj.AtributoTrece > 0) obj.cod_sel = true;
                if (obj.AtributoTrece == 0) obj.cod_sel = false;
            }

            if (gvDatos.FocusedColumn.Name == "colAtributoOnce" || gvDatos.FocusedColumn.Name == "colAtributoDoce")
            {
                eDatos obj = gvDatos.GetFocusedRow() as eDatos;
                eDatos eCar = new eDatos();

                eCar.AtributoUno = empresa;
                eCar.AtributoDos = sede;
                eCar.AtributoTres = "00002";
                eCar.AtributoCuatro = obj.AtributoTres;
                eCar.AtributoCinco = obj.AtributoCuatro;
                eCar.AtributoOnce = obj.AtributoOnce;
                eCar.AtributoDoce = obj.AtributoDoce;

                eCar = blAns.Ins_Act_Cargo<eDatos>(eCar);
            }

            var n = gvDatos.DataRowCount;
            var g = gvDatos.DataSource;
        }

        private void gvDatos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eDatos obj = gvDatos.GetRow(e.RowHandle) as eDatos;

                    if (e.Column.FieldName == "AtributoTrece" && obj.AtributoTrece == 0) e.DisplayText = "";

                    e.DefaultDraw();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvDatos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvDatos_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            string cell = gvDatos.GetRowCellDisplayText(e.RowHandle, e.Column);

            if (cell == "0" || cell == "0.0000")
            {
                e.Appearance.ForeColor = Color.Red;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void gvDatos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < lstDatos.Count; x++)
            {
                eDatos obj = lstDatos[x];

                if (obj.cod_sel)
                {
                    eAnalisis.eAnalisis_Personal_Uniformes obj2 = new eAnalisis.eAnalisis_Personal_Uniformes();

                    obj2.cod_cargo = cargo;
                    obj2.num_item = item;
                    obj2.dsc_cargo = lkpCargoUniforme.Text;
                    obj2.cod_producto = obj.AtributoCinco;
                    obj2.dsc_producto = obj.AtributoSeis;
                    obj2.cod_tipo_servicio = obj.AtributoUno;
                    obj2.dsc_tipo_servicio = obj.AtributoDos;
                    obj2.cod_subtipo_servicio = obj.AtributoTres;
                    obj2.dsc_subtipo_servicio = obj.AtributoCuatro;
                    obj2.cod_unidad_medida = obj.AtributoSiete;
                    obj2.dsc_simbolo = obj.AtributoOcho;
                    obj2.imp_unitario = obj.AtributoOnce;
                    obj2.prc_margen = obj.AtributoDoce;
                    obj2.num_cantidad = 1;
                    obj2.imp_total = obj2.num_cantidad * obj.AtributoOnce;
                    obj2.imp_venta = obj2.imp_total * (1 + obj2.prc_margen / 100);

                    bsUniformes.Add(obj2);
                    bsDatos.Remove(obj);

                    x = x - 1;
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < lstUnifTemp.Count; x++)
            {
                eAnalisis.eAnalisis_Personal_Uniformes obj = lstUnifTemp[x];

                if (obj.sel)
                {
                    eDatos obj2 = new eDatos();

                    obj2.AtributoCinco = obj.cod_producto;
                    obj2.AtributoSeis = obj.dsc_producto;
                    obj2.AtributoUno = obj.cod_tipo_servicio;
                    obj2.AtributoDos = obj.dsc_tipo_servicio;
                    obj2.AtributoTres = obj.cod_subtipo_servicio;
                    obj2.AtributoCuatro = obj.dsc_subtipo_servicio;
                    obj2.AtributoSiete = obj.cod_unidad_medida;
                    obj2.AtributoOcho = obj.dsc_simbolo;
                    obj2.AtributoOnce = obj.imp_unitario;
                    obj2.AtributoDoce = obj.prc_margen;

                    bsDatos.Add(obj2);
                    bsUniformes.Remove(obj);
                    lstUnifElim.Add(obj); //LDAC - Agregar los items que se van a eliminar

                    x = x - 1;
                }
            }
        }

        private void gvUniformes_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            eAnalisis.eAnalisis_Personal_Uniformes obj = gvUniformes.GetFocusedRow() as eAnalisis.eAnalisis_Personal_Uniformes;

            obj.imp_total = obj.num_cantidad * obj.imp_unitario;
            obj.imp_venta = obj.imp_total + ((obj.imp_total * obj.prc_margen) / 100);
        }

        private void gvUniformes_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eAnalisis.eAnalisis_Personal_Uniformes obj = gvUniformes.GetRow(e.RowHandle) as eAnalisis.eAnalisis_Personal_Uniformes;

                    if (e.Column.FieldName == "colnum_cantidad" && obj.num_cantidad == 0) e.DisplayText = "";

                    e.DefaultDraw();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvUniformes_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            blGlobal.Pintar_CabeceraColumnas(e);
        }

        private void gvUniformes_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            string cell = gvUniformes.GetRowCellDisplayText(e.RowHandle, e.Column);

            if (cell == "0" || cell == "0.0000")
            {
                e.Appearance.ForeColor = Color.Red;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void gvUniformes_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) blGlobal.Pintar_EstiloGrilla(sender, e);
        }

        private void rbtnEliminarUnif_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            eAnalisis.eAnalisis_Personal_Uniformes eUni = gvUniformes.GetFocusedRow() as eAnalisis.eAnalisis_Personal_Uniformes;

            eDatos obj2 = new eDatos();

            obj2.AtributoCinco = eUni.cod_producto;
            obj2.AtributoSeis = eUni.dsc_producto;
            obj2.AtributoUno = eUni.cod_tipo_servicio;
            obj2.AtributoDos = eUni.dsc_tipo_servicio;
            obj2.AtributoTres = eUni.cod_subtipo_servicio;
            obj2.AtributoCuatro = eUni.dsc_subtipo_servicio;
            obj2.AtributoSiete = eUni.cod_unidad_medida;
            obj2.AtributoOcho = eUni.dsc_simbolo;
            obj2.AtributoOnce = eUni.imp_unitario;
            obj2.AtributoDoce = eUni.prc_margen;

            bsDatos.Add(obj2);
            bsUniformes.Remove(eUni);
            lstUnifElim.Add(eUni); //LDAC - Agregar los items que se van a eliminar
        }
    }
}