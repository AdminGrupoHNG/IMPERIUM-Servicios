using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Servicios
{
    public class eAnalisis
    {
        public Boolean sel { get; set; }
        public string cod_empresa { get; set; }
        public string dsc_empresa { get; set; }
        public string cod_sede_empresa { get; set; }
        public string dsc_sede_empresa { get; set; }
        public string cod_analisis { get; set; }
        public string cod_cliente { get; set; }
        public string dsc_documento_cliente { get; set; } //--> Se agregó para el formato de cotizaciones - LDAC
        public string dsc_cliente { get; set; }
        public string dsc_cadena_direccion { get; set; }
        public string cod_estado_analisis { get; set; }
        public DateTime fch_requerimiento { get; set; }
        public string cod_usuario_aprobacion { get; set; }
        public string dsc_usuario_aprobacion { get; set; }
        public DateTime fch_aprobacion { get; set; }
        public string cod_usuario_registro { get; set; }
        public string dsc_usuario_registro { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_cambio { get; set; }
        public string dsc_usuario_cambio { get; set; }
        public DateTime fch_cambio { get; set; }

        public class eAnalisis_Sedes : eAnalisis
        {
            public int cod_sede_cliente { get; set; }
            public string dsc_sede_cliente { get; set; }
            public decimal num_m2 { get; set; }
            public decimal num_m3 { get; set; }
            public DateTime fch_visita { get; set; }
            public DateTime dsc_hora_inicio_visita { get; set; }
            public DateTime dsc_hora_fin_visita { get; set; }
            public string dsc_observaciones { get; set; }
        }

        public class eAnalisis_Sedes_Prestacion : eAnalisis_Sedes
        {
            public int num_servicio { get; set; }
            public string dsc_periodo { get; set; }
            public int num_version { get; set; }
            public string dsc_version { get; set; }
            public string dsc_obs_version { get; set; }
            public string flg_habilitado { get; set; }
            public string cod_tipo_prestacion { get; set; }
            public string dsc_tipo_prestacion { get; set; }
        }

        public class eAnalisis_Alcance : eAnalisis_Sedes_Prestacion
        {
            public int num_item { get; set; }
            public string dsc_actividad { get; set; }
            public int num_cantidad { get; set; }
            public string flg_maq_equipo { get; set; }
        }

        public class eAnalisis_Personal : eAnalisis_Sedes_Prestacion
        {
            public int num_item { get; set; }
            public string cod_cargo { get; set; }
            public string dsc_cargo { get; set; }
            public int num_orden { get; set; }
            public Boolean flg_descansero { get; set; }
            public Boolean flg_horario { get; set; }
            public string cod_turno { get; set; }
            public DateTime dsc_hora_inicio { get; set; }
            public DateTime dsc_hora_fin { get; set; }
            public string dsc_rango_horario { get; set; }
            public decimal num_horas { get; set; }
            public decimal num_horas_diu { get; set; }
            public decimal num_horas_noc { get; set; }
            public decimal num_horas_extra { get; set; }
            public decimal num_horas_ext_diu { get; set; }
            public decimal num_horas_ext_noc { get; set; }
            public int num_min_almuerzo { get; set; }
            public int num_hora_dia { get; set; }
            public int num_dia_semana { get; set; }
            public Boolean flg_feriado { get; set; }
            public int num_cantidad { get; set; }
            public decimal imp_salario { get; set; }
            public decimal imp_salario_min { get; set; }
            public decimal imp_salario_max { get; set; }
            public decimal imp_horas_diu { get; set; }
            public decimal imp_horas_noc { get; set; }
            public decimal imp_salario_extra { get; set; }
            public decimal imp_horas_ext_diu { get; set; }
            public decimal imp_horas_ext_noc { get; set; }
            public decimal  imp_bono_nocturno { get; set; }
            public decimal  imp_bono_productividad { get; set; }
            public decimal  imp_movilidad { get; set; }
            public decimal  imp_feriado { get; set; }
            public decimal imp_salario_total { get; set; }
            public Boolean flg_uniforme { get; set; }
        }

        public class eAnalisis_Personal_Sedes : eAnalisis_Sedes_Prestacion //-->LDAC - Se agregó para efectos de visualizarlo en la Propuesta técnica
        {
            public string cod_cargo { get; set; }
            public int num_linea_sedes { get; set; }
            public int num_cantidad { get; set; }
            public string dsc_cargo { get; set; }
            public decimal num_horas { get; set; }
            public string dsc_rango_horario { get; set; }
            public int num_dia_semana { get; set; }
        }

        public class eAnalisis_Personal_Uniformes : eAnalisis_Sedes_Prestacion
        {
            public int num_item { get; set; }
            public string cod_cargo { get; set; }
            public string dsc_cargo { get; set; }
            public string cod_producto { get; set; }
            public string dsc_producto { get; set; }
            public string cod_tipo_servicio { get; set; }
            public string dsc_tipo_servicio { get; set; }
            public string cod_subtipo_servicio { get; set; }
            public string dsc_subtipo_servicio { get; set; }
            public string cod_unidad_medida { get; set; }
            public string dsc_unidad_medida { get; set; }
            public string dsc_simbolo { get; set; }
            public int num_cantidad { get; set; }
            public decimal imp_unitario { get; set; }
            public decimal imp_total { get; set; }
            public decimal prc_margen { get; set; }
            public decimal imp_venta { get; set; }
        }

        public class eAnalisis_Producto : eAnalisis_Sedes_Prestacion
        {
            public string cod_producto { get; set; }
            public string dsc_producto { get; set; }
            public string cod_tipo_servicio { get; set; }
            public string dsc_tipo_servicio { get; set; }
            public string cod_subtipo_servicio { get; set; }
            public string dsc_subtipo_servicio { get; set; }
            public string cod_dotacion { get; set; }
            public string dsc_dotacion { get; set; }
            public string cod_unidad_medida { get; set; }
            public string dsc_unidad_medida { get; set; }
            public string dsc_simbolo { get; set; }
            public int num_cantidad { get; set; }
            public decimal imp_unitario { get; set; }
            public decimal imp_total { get; set; }
            public decimal prc_margen { get; set; }
            public decimal imp_venta { get; set; }
        }

        public class eAnalisis_Maquinaria : eAnalisis_Sedes_Prestacion
        {
            public string cod_activo_fijo { get; set; }
            public string dsc_activo_fijo { get; set; }
            public string dsc_grupo_activo_fijo { get; set; }
            public int num_cantidad { get; set; }
            public decimal imp_unitario { get; set; }
            public decimal imp_total { get; set; }
            public int num_meses_dep { get; set; }
            public decimal imp_mensual { get; set; }
            public decimal prc_margen { get; set; }
            public decimal imp_venta { get; set; }
        }

        public class eAnalisis_Otros : eAnalisis_Sedes_Prestacion
        {
            public int num_item { get; set; }
            public string cod_concepto { get; set; }
            public string dsc_descripcion { get; set; }
            public int num_cantidad { get; set; }
            public decimal prc_ley { get; set; }
            public decimal imp_unitario { get; set; }
            public decimal imp_total { get; set; }
            public decimal prc_margen { get; set; }
            public decimal imp_venta { get; set; }
        }

        public class eAnalisis_Est_Cst : eAnalisis_Sedes_Prestacion
        {
            public string cod_concepto { get; set; }
            public string dsc_concepto { get; set; }
            public string cod_item { get; set; }
            public string dsc_item { get; set; }
            public decimal prc_ley { get; set; }
            public decimal imp_unitario { get; set; }
            public decimal prc_margen { get; set; }
            public decimal imp_total { get; set; }
        }
    }
}
