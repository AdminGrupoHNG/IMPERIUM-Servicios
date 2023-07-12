using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_Servicios
{
    public class daEncrypta
    {
        //string provider = "DataProtectionConfigurationProvider";
        UnicodeEncoding ByteConverter = new UnicodeEncoding();

        public string Encrypta(string valor)
        {
            string result = "";
            //byte[] codigos = Encoding.ASCII.GetBytes(valor);
            //Byte[] nuevosCodigos = new Byte[codigos.Length];

            //for (int i = 0; i < codigos.Length; i++)
            //{
            //    nuevosCodigos[i] = Convert.ToByte(Convert.ToInt32(codigos[i]) - 3);
            //}
            //result = Encoding.ASCII.GetString(nuevosCodigos);

            result = valor;
            return result;
        }

        public string Desencrypta(string valor)
        {
            string result = "";
            //byte[] codigos = Encoding.ASCII.GetBytes(valor);
            //Byte[] nuevosCodigos = new Byte[codigos.Length];

            //for (int i = 0; i < codigos.Length; i++)
            //{
            //    nuevosCodigos[i] = Convert.ToByte(Convert.ToInt32(codigos[i]) + 3);
            //}
            //result = Encoding.ASCII.GetString(nuevosCodigos);

            result = valor;
            return result;
        }
    }
}
