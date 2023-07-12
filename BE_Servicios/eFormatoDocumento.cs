using System;

namespace BE_Servicios
{
    public class eFormatoMD_Grupo
    {
        public string cod_formatoMD_grupo { get; set; }
        public string cod_solucion { get; set; }
        public string dsc_formatoMD_grupo { get; set; }
        public int num_jerarquia { get; set; }
    }

    public class eFormatoMD_General
    {
        public string cod_formatoMD_general { get; set; }
        public string cod_formatoMD_grupo { get; set; }
        public string cod_solucion { get; set; }
        public string cod_tipo_formato { get; set; }
        public string dsc_formatoMD_general { get; set; }
        public int num_modelo { get; set; }
        public string dsc_observacion { get; set; }
        public string dsc_wordMLText { get; set; }
        public string flg_obligatorio { get; set; }
        public string flg_editado { get; set; }
        public string flg_publicado { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
        public DateTime fch_publicacion { get; set; }
        public string cod_usuario_publicacion { get; set; }
        public string flg_activo { get; set; }

        public class eFormatoMD_General_Vista : eFormatoMD_General
        {
            public string dsc_formatoMD_grupo { get; set; }
            public string dsc_modelo { get; set; }
        }
        public class eFormatoDocumento : eFormatoMD_General
        {
            public string cod_formatoMD_seguimiento { get; set; }
            public string cod_detalle_seguimiento { get; set; }
            public int cod_formatoMD_vinculo { get; set; }
            public string dsc_formatoMD_vinculo { get; set; }
            public string cod_estado { get; set; }
            public DateTime fch_firma { get; set; }
            public string CABECERA { get; set; }
        }
    }

    public class eFormatoMD_Parametro
    {
        public string cod_formatoMD_parametro { get; set; }
        public string dsc_formatoMD_parametro { get; set; }
        public string cod_solucion { get; set; }
        public string dsc_observacion { get; set; }
        public string dsc_columna_asociada { get; set; }
        public string flg_asignado { get; set; }
        public string flg_estado { get; set; }
        public string cod_tipo_parametro { get; set; }
        public string dsc_valor_parametro { get; set; }
    }

    public class eFormatoMD_Vinculo
    {
        public string cod_empresa { get; set; }
        public string cod_formatoMD_general { get; set; }
        public string cod_formatoMD_vinculo { get; set; }
        public string cod_solucion { get; set; }
        public string dsc_formatoMD_vinculo { get; set; }
        public string dsc_observacion { get; set; }
        public string dsc_wordMLText { get; set; }
        public string flg_publicado { get; set; }
        public string flg_cambio_maestro { get; set; }
        public string flg_obligatorio { get; set; }
        public string flg_seguimiento { get; set; }
        public string cod_cargo_firma { get; set; }
        public string dsc_version { get; set; }
        public string flg_estado { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
    }

    public class eFormatoMD_Vinculo_Filtro
    {
        public string cod_formatoMD_grupo { get; set; }
        public string dsc_formatoMD_grupo { get; set; }
        public string cod_formatoMD_vinculo { get; set; }
        public string dsc_formatoMD_vinculo { get; set; }
        public string flg_publicado { get; set; }
        public string flg_cambio_maestro { get; set; }
        public int num_jerarquia { get; set; }
        public string flg_obligatorio { get; set; }
        public string flg_seguimiento { get; set; }
        public string dsc_version { get; set; }
        public string flg_estado { get; set; }
        public DateTime fch_cambio { get; set; }
        public string dsc_wordMLText { get; set; }
    }
    public class eFormatoMDGeneral_Tree
    {
        public string cod_formatoMD_grupo { get; set; }
        public string dsc_formatoMD_grupo { get; set; }
        public string cod_formatoMD_general { get; set; }
        public string dsc_formatoMD_general { get; set; }
        public int num_jerarquia { get; set; }
        public string flg_obligatorio { get; set; }
        public string num_modelo { get; set; }
        public string flg_publicado { get; set; }
    }

    public class pQFormatD
    {
        public pQFormatD()
        {
            _opcion = 0;
            _cod_usuario = string.Empty;
            _cod_estado = string.Empty;
            _cod_empresaSplit = string.Empty;
            _dsc_formatoMD_general = string.Empty;
            _cod_formatoMD_generalSplit = string.Empty;
            _cod_formatoMD_vinculoSplit = string.Empty;
            _cod_trabajadorSplit = string.Empty;
            _cod_formatoMD_seguimiento = string.Empty;
            _cod_solucion = string.Empty;
        }

        private int _opcion;
        private string _cod_usuario;
        private string _cod_estado;
        private string _cod_formatoMD_seguimiento;
        private string _cod_empresaSplit;
        private string _dsc_formatoMD_general;
        private string _cod_formatoMD_generalSplit;
        private string _cod_formatoMD_vinculoSplit;
        private string _cod_trabajadorSplit;
        private string _cod_solucion;

        public int Opcion { get => _opcion; set => _opcion = value; }
        public string Cod_usuario { get => _cod_usuario; set => _cod_usuario = value; }
        public string Cod_estado { get => _cod_estado; set => _cod_estado = value; }
        public string Cod_empresaSplit { get => _cod_empresaSplit; set => _cod_empresaSplit = value; }
        public string Dsc_formatoMD_general { get => _dsc_formatoMD_general; set => _dsc_formatoMD_general = value; }
        public string Cod_formatoMD_generalSplit { get => _cod_formatoMD_generalSplit; set => _cod_formatoMD_generalSplit = value; }
        public string Cod_formatoMD_vinculoSplit { get => _cod_formatoMD_vinculoSplit; set => _cod_formatoMD_vinculoSplit = value; }
        public string Cod_trabajadorSplit { get => _cod_trabajadorSplit; set => _cod_trabajadorSplit = value; }
        public string Cod_formatoMD_seguimiento { get => _cod_formatoMD_seguimiento; set => _cod_formatoMD_seguimiento = value; }
        public string Cod_solucion { get => _cod_solucion; set => _cod_solucion = value; }
    }
}
