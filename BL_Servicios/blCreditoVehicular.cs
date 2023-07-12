using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_Servicios;
using DA_Servicios;
using DevExpress.XtraEditors;

namespace BL_Servicios
{
    public class blCreditoVehicular
    {
        daSQL sql = new daSQL();

        public List<T> ListarDatos_CreditoVehicular<T>(int opcion, string cod_credito, string cod_cronograma = "", string num_placa = "", Int32 num_linea = 0, 
                                                    Int32 num_cuota = 0, string dsc_origen = "", DateTime fch_inicial = new DateTime(), DateTime fch_final = new DateTime(),
                                                    DateTime fch_proximo_pago = new DateTime()) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_credito", cod_credito },
                { "cod_cronograma", cod_cronograma },
                { "num_placa", num_placa },
                { "num_linea", num_linea },
                { "num_cuota", num_cuota },
                { "dsc_origen", dsc_origen}
            };
            if (fch_inicial.ToString().Contains("1/01/0001")) { oDictionary.Add("fch_inicial", DBNull.Value); } else { oDictionary.Add("fch_inicial", fch_inicial); }
            if (fch_final.ToString().Contains("1/01/0001")) { oDictionary.Add("fch_final", DBNull.Value); } else { oDictionary.Add("fch_final", fch_final); }
            if (fch_proximo_pago.ToString().Contains("1/01/0001")) { oDictionary.Add("fch_proximo_pago", DBNull.Value); } else { oDictionary.Add("fch_proximo_pago", fch_proximo_pago); }

            myList = sql.ListaconSP<T>("usp_Consulta_ListarCreditosVehicular", oDictionary);
            return myList;
        }

        public T ObtenerDatos_CreditoVehicular<T>(int opcion, string cod_credito, string cod_cronograma = "", string num_placa = "", Int32 num_linea = 0, Int32 num_cuota = 0) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_credito", cod_credito },
                { "cod_cronograma", cod_cronograma },
                { "num_placa", num_placa },
                { "num_linea", num_linea },
                { "num_cuota", num_cuota }
            };

            obj = sql.ConsultarEntidad<T>("usp_Consulta_ListarCreditosVehicular", oDictionary);
            return obj;
        }

        public T InsertarActualizar_CreditoVehicular<T>(eCreditoVehicular eCred) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_credito", eCred.cod_credito },
                { "dsc_apellido_paterno", eCred.dsc_apellido_paterno },
                { "dsc_apellido_materno", eCred.dsc_apellido_materno },
                { "dsc_nombres", eCred.dsc_nombres },
                { "cod_tipo_documento", eCred.cod_tipo_documento },
                { "dsc_documento", eCred.dsc_documento },
                { "dsc_titular", eCred.dsc_titular },
                { "cod_tipo_documento_titular", eCred.cod_tipo_documento_titular },
                { "dsc_documento_titular", eCred.dsc_documento_titular },
                { "flg_activo", eCred.flg_activo },
                { "cod_usuario_registro", eCred.cod_usuario_registro },
            };
            //if (eCaja.fch_creacion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_creacion", DBNull.Value); } else { dictionary.Add("fch_creacion", eCaja.fch_creacion); }

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_CreditosVehiculares", dictionary);
            return obj;
        }

        public T InsertarActualizar_CreditoVeh_CronogramaCabecera<T>(eCreditoVehicular.eCronogramaCabecera eCred) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_credito", eCred.cod_credito },
                { "cod_cronograma", eCred.cod_cronograma },
                { "num_placa", eCred.num_placa },
                { "imp_Capital", eCred.imp_Capital },
                { "fch_desembolso", eCred.fch_desembolso },
                { "num_cuotas", eCred.num_cuotas },
                { "num_diapago", eCred.num_diapago },
                { "num_tasaanual", eCred.num_tasaanual },
                { "num_tasamensual", eCred.num_tasamensual },
                { "num_tasaTIRanual", eCred.num_tasaTIRanual },
                { "num_tasaTIRM", eCred.num_tasaTIRM },
                { "flg_activo", eCred.flg_activo },
                { "cod_usuario_registro", eCred.cod_usuario_registro },
            };
            //if (eCaja.fch_creacion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_creacion", DBNull.Value); } else { dictionary.Add("fch_creacion", eCaja.fch_creacion); }

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_CreditosVehiculares_CronogramaCabecera", dictionary);
            return obj;
        }

        public T InsertarActualizar_CreditoVeh_CronogramaDetalle<T>(eCreditoVehicular.eCronogramaDetalle eCred) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_credito", eCred.cod_credito },
                { "cod_cronograma", eCred.cod_cronograma },
                { "num_placa", eCred.num_placa },
                { "num_cuota", eCred.num_cuota },
                { "fch_cuota", eCred.fch_cuota },
                { "num_dias", eCred.num_dias },
                { "imp_capitalinicial", eCred.imp_capitalinicial },
                { "imp_capitalfinal", eCred.imp_capitalfinal },
                { "imp_amortizacion", eCred.imp_amortizacion },
                { "imp_interes", eCred.imp_interes },
                { "imp_desgravamen", eCred.imp_desgravamen },
                { "imp_portes", eCred.imp_portes },
                { "imp_otros", eCred.imp_otros },
                { "imp_cuotasinigv", eCred.imp_cuotasinigv },
                { "imp_coutaigv", eCred.imp_coutaigv },
                { "imp_cuotaconigv", eCred.imp_cuotaconigv },
                { "flg_pagado", eCred.flg_pagado },
                { "cod_usuario_registro", eCred.cod_usuario_registro },
            };
            //if (eCaja.fch_creacion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_creacion", DBNull.Value); } else { dictionary.Add("fch_creacion", eCaja.fch_creacion); }

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_CreditosVehiculares_CronogramaDetalle", dictionary);
            return obj;
        }

        public string InsertarActualizar_CreditoVeh_PagosDetalle(eCreditoVehicular.ePagosDetalle eCred)
        {
            string result;
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_credito", eCred.cod_credito },
                { "cod_cronograma", eCred.cod_cronograma },
                { "num_placa", eCred.num_placa },
                { "num_linea", eCred.num_linea },
                { "fch_recaudo", eCred.fch_recaudo },
                { "dsc_hora", eCred.dsc_hora },
                { "dsc_estacion", eCred.dsc_estacion },
                { "cod_chip", eCred.cod_chip },
                { "num_tanqueo", eCred.num_tanqueo },
                { "imp_bruto", eCred.imp_bruto },
                { "imp_cofide", eCred.imp_cofide },
                { "imp_neto", eCred.imp_neto },
                { "dsc_origen", eCred.dsc_origen },
                { "fch_archivo", eCred.fch_archivo },
                { "flg_pagoaplicado", eCred.flg_pagoaplicado },
                { "flg_pagovalidado", eCred.flg_pagovalidado },
                { "dsc_destino", eCred.dsc_destino },
                { "cod_usuario_registro", eCred.cod_usuario_registro },
            };
            if (eCred.fch_pagovalidado.ToString().Contains("1/01/0001")) { dictionary.Add("fch_pagovalidado", DBNull.Value); } else { dictionary.Add("fch_pagovalidado", eCred.fch_pagovalidado); }
            if (eCred.fch_deposito.ToString().Contains("1/01/0001")) { dictionary.Add("fch_deposito", DBNull.Value); } else { dictionary.Add("fch_deposito", eCred.fch_deposito); }

            result = sql.ExecuteScalarWithParams("usp_Insertar_Actualizar_CreditosVehiculares_PagosDetalle", dictionary);
            return result;
        }

        public T InsertarActualizar_CreditoVeh_PagosDetalleCuota<T>(eCreditoVehicular.ePagoCuota eCred) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_credito", eCred.cod_credito },
                { "cod_cronograma", eCred.cod_cronograma },
                { "num_placa", eCred.num_placa },
                { "num_cuota", eCred.num_cuota },
                { "num_linea", eCred.num_linea },
                { "imp_neto", eCred.imp_neto },
                { "imp_interes", eCred.imp_interes },
                { "imp_igv", eCred.imp_igv },
                { "imp_capital", eCred.imp_capital },
                { "cod_usuario_registro", eCred.cod_usuario_registro },
            };
            //if (eCaja.fch_creacion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_creacion", DBNull.Value); } else { dictionary.Add("fch_creacion", eCaja.fch_creacion); }

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_CreditosVehiculares_PagosDetalleCuota", dictionary);
            return obj;
        }

        public void CargaCombosLookUp(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", bool valorDefecto = false, string cod_proveedor = "", string cod_usuario = "", string cod_tipo_transaccion = "", string cod_empresa = "")
        {
            combo.Text = "";
            string procedure = "usp_Consulta_ListarCreditosVehicular";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "Destino":
                        dictionary.Add("opcion", 18);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                }

                combo.Properties.DataSource = tabla;
                combo.Properties.ValueMember = campoValueMember;
                combo.Properties.DisplayMember = campoDispleyMember;
                if (campoSelectedValue == "") { combo.ItemIndex = -1; } else { combo.EditValue = campoSelectedValue; }
                if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string EliminarCronogramaDetalle(int opcion, string cod_credito, string cod_cronograma, string num_placa)
        {
            string result;
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_credito", cod_credito },
                { "num_placa", num_placa },
                { "cod_cronograma", cod_cronograma },
            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_CreditosVehiculares_CronogramaDetalle", dictionary);
            return result;
        }
    }
}
