using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA_Servicios;

namespace BL_Servicios
{
    public class blEncrypta
    {
        daEncrypta sql = new daEncrypta();

        public string Encrypta(string valor)
        {
            return sql.Encrypta(valor);
        }
        public string Desencrypta(string valor)
        {
            return sql.Desencrypta(valor);
        }
    }
}
