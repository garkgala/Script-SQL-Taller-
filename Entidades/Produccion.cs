using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Entidades
{
    public class Produccion:Base
    {
        public DateTime fecha_produccion { get; set; }
        public Int64 id_prod_ant { get; set; }
        public Int64 id_prod_nuevo { get; set; }
        public Int64 id_emp { get; set; }
        public string tipo_produccion { get; set; }
        public Int64 cantidad { get; set; }
        public string observaciones { get; set; }
    }
}
