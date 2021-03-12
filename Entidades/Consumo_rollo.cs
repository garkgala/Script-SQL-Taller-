using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Entidades
{
    public class Consumo_rollo:Base
    {
        public DateTime fecha { get; set; }
        public Int64 id_prod { get; set; }
        public Int64 cantidad { get; set; }
    }
}
