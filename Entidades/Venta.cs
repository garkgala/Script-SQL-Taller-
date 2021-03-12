using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Entidades
{
    public class Venta:Base
    {
        public string n_factura { get; set; }
        public DateTime fecha_venta { get; set; }
        public Int64 id_cli { get; set; }
        public Int64 id_prod { get; set; }
        public int cantidad { get; set; }
        public double precio_venta { get; set; }
    }
}
