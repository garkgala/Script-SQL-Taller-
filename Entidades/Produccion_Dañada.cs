using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Entidades
{
    public class Produccion_Dañada:Base
    {
        public DateTime fecha { get; set; }
        public string nombre_emp { get; set; }
        public Int64 id_prod { get; set; }
        public int cantidad { get; set; }
    }
}
