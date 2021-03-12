using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Entidades
{
    public class Tipos_Produccion:Base
    {
        public string tipo_produccion { get; set; }
        public override string ToString()
        {
            return string.Format("{0}", tipo_produccion);
        }
    }
}
