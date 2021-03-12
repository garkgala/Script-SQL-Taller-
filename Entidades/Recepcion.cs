using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Entidades
{
    public class Recepcion:Base
    {
        public DateTime fecha { get; set; }
        public Int64 id_proveedor { get; set; }
        public Int64 id_prod { get; set; }
        public int cantidad { get; set; }
    }
}
